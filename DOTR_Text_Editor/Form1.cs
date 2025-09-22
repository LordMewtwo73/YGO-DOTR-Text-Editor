using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DOTR_Text_Editor
{
	public partial class Form1 : Form
	{
		public List<string> CurrentStrings;
		public Dictionary<int, ushort[]> CurrentBytes;

		public string thislocation;

		public bool FileLoaded;
		public bool FileEdited;
		public bool isUpdating;

		public string[] CardNames, CharacterNames, LocationNames, SectionNames;
		public List<int> SectionIndexes;

		string loadedfile = "";

		public Form1()
		{
			InitializeComponent();

			Initialize();
			LoadTextFiles();
			PopulateComboBoxes();
        }

		private void Initialize()
		{
			thislocation = Directory.GetCurrentDirectory();

			lblFilePath.Text = "NO FILE SELECTED";

			numIndex.Value = 0;

			txtTitle.Text = "";
			txtString.Text = "";

			this.FileLoaded = false;
			this.FileEdited = false;

            txtString.ReadOnly = true;

			progBar.Visible = false;

			numTotalBytes.Visible = false;
			lblTotalBytes.Visible = false;
			lblByteLimit.Visible = false;

		}

		private void LoadTextFiles()
		{
			CardNames = File.ReadAllLines(thislocation + "\\Assets\\CardList.txt");
            CharacterNames = File.ReadAllLines(thislocation + "\\Assets\\CharacterList.txt");
            LocationNames = File.ReadAllLines(thislocation + "\\Assets\\LocationList.txt");
            SectionNames = File.ReadAllLines(thislocation + "\\Assets\\Sections.txt");

			SectionIndexes = new List<int>();
			for (int i = 0; i < SectionNames.Length; i++)
			{
				int ind = Convert.ToInt32(SectionNames[i].Split('-')[0]);
				SectionIndexes.Add(ind);
			}
        }

		private void PopulateComboBoxes()
		{
            comboCardName.Items.Clear();
            comboCardAbility.Items.Clear();
            comboSection.Items.Clear();

			foreach (string card in CardNames)
				comboCardName.Items.Add(card);
            foreach (string card in CardNames)
                comboCardAbility.Items.Add(card);
            foreach (string section in SectionNames)
                comboSection.Items.Add(section);

			comboCardName.SelectedIndex = -1;
			comboCardAbility.SelectedIndex = -1;
			comboSection.SelectedIndex = -1;
        }

		private void UpdateFront()
		{
			isUpdating = true;

			if (this.FileLoaded)
			{
				string currstring = CurrentStrings[(int)numIndex.Value];

				txtString.Text = ReplacePlaceholderChars(currstring);
				txtString.ReadOnly = !isEditableIndex(numIndex.Value);
			}

			int sectionoffset = 0;
			comboSection.SelectedIndex = GetSectionIndex((int)numIndex.Value);
			if(comboSection.SelectedIndex != -1)
				sectionoffset = (int)numIndex.Value - SectionIndexes[comboSection.SelectedIndex];


			if (comboSection.SelectedIndex == -1)
				txtTitle.Text = "Misc Game Text - Do Not Edit";
			else
			{
				txtTitle.Text = GetTitle(comboSection.SelectedIndex, sectionoffset);

				if (txtTitle.Text.ToLower().Contains("card names"))
					comboCardName.SelectedIndex = sectionoffset;
				else
					comboCardName.SelectedIndex = -1;


				if (txtTitle.Text.ToLower().Contains("card abilities"))
					comboCardAbility.SelectedIndex = sectionoffset;
				else
					comboCardAbility.SelectedIndex = -1;
			}

			isUpdating = false;
		}

		private int GetSectionIndex(int index)
		{
			for (int i = 0; i < SectionIndexes.Count; i++)
			{
				if (index < SectionIndexes[i])
				{
					return i - 1;
				}
				else if (i == SectionIndexes.Count - 1)
				{
					return i;
				}
			}

			return -1;
		}

		private string GetTitle(int sectionindex, int sectionoffset)
		{
			string title = SectionNames[sectionindex].Split('-')[1];

			if (title.ToLower().Contains("character names"))
				title += " - " + CharacterNames[sectionoffset];
			else if (title.ToLower().Contains("locations"))
				title += " - " + LocationNames[sectionoffset];
			else if (title.ToLower().Contains("card names") | title.ToLower().Contains("card abilities"))
				title += " - " + CardNames[sectionoffset];
			else
				title += " (" + sectionoffset.ToString() + ")";

			return title;
		}

		private string ReplacePlaceholderChars(string input)
		{
			string output = input;

			output = output.Replace("\uFFF2", "{PNAME_CHAR}");
			output = output.Replace("\uFFF3", "{III}");
			output = output.Replace("\uFFF1", System.Environment.NewLine);

			return output;
		}

		private string ReenterPlaceholderChars(string input)
		{
			string output = input;

			if (output == "")
				output = "~";

			output = output.Replace("{PNAME_CHAR}", '\uFFF2'.ToString());
			output = output.Replace("{III}", '\uFFF3'.ToString());
			output = output.Replace(System.Environment.NewLine, '\uFFF1'.ToString());

			return output;
		}

		private bool isEditableIndex(decimal index)
		{
			return (!(index < 30 | Constants.Uneditable.Contains((int)index)));

		}

		private void SaveToISO()
		{
			using (var stream = new FileStream(this.loadedfile, FileMode.Open, FileAccess.ReadWrite))
			{
				stream.Position = Constants.EnglishOffsetStart;
				foreach (byte b in TextCompressor.OffsetBytes)
					stream.WriteByte(b);


				stream.Position = Constants.EnglishTextStart;
				foreach (byte b in TextCompressor.StringBytes)
					stream.WriteByte(b);
			}

		}
		


		private void openISOToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (openISOFD.ShowDialog() == DialogResult.OK)
            {
				if (openISOFD.FileName.ToLower().EndsWith(".iso"))
				{
					loadedfile = openISOFD.FileName;
					lblFilePath.Text = openISOFD.FileName;

					TextReader.Load(this.loadedfile);
				}

				CurrentStrings = new List<string>();

				for (int i = 0; i < TextReader.VanillaStrings.Count; i++)
				{
					CurrentStrings.Add(TextReader.VanillaStrings[i]);
				}

				numIndex.Maximum = CurrentStrings.Count;
				numIndex.Value = 30; 
				
				FileLoaded = true;
            }

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            UpdateFront();
        }

        private void comboSection_SelectedIndexChanged(object sender, EventArgs e)
        {
			if (!isUpdating)
				if (comboSection.SelectedIndex != -1)
					numIndex.Value = SectionIndexes[comboSection.SelectedIndex];
        }

        private void comboCardAbility_SelectedIndexChanged(object sender, EventArgs e)
        {
			if (!isUpdating)
				if (comboCardAbility.SelectedIndex != -1)
					numIndex.Value = Constants.CardAbilityStart + comboCardAbility.SelectedIndex;
        }

        private void comboCardName_SelectedIndexChanged(object sender, EventArgs e)
        {
			if (!isUpdating)
				if (comboCardName.SelectedIndex != -1)
					numIndex.Value = Constants.CardNameStart + comboCardName.SelectedIndex;
        }

        private void txtString_TextChanged(object sender, EventArgs e)
        {
			if (!isUpdating)
			{
				if (txtString.Text != ReplacePlaceholderChars(CurrentStrings[(int)numIndex.Value]))
				{
					CurrentStrings[(int)numIndex.Value] = ReenterPlaceholderChars(txtString.Text);
					this.FileEdited = true;
				}
			}
		}

        private void btnResetAll_Click(object sender, EventArgs e)
        {
			if (this.FileLoaded)
			{
				if (MessageBox.Show("Are you sure you want to reset all strings?", "Warning!", MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					CurrentStrings = new List<string>();

					for (int i = 0; i < TextReader.VanillaStrings.Count; i++)
					{
						CurrentStrings.Add(TextReader.VanillaStrings[i]);
					}

					UpdateFront();
				}

				this.FileEdited = false;
			}
        }

		private void saveToISOToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (FileEdited)
			{
				progBar.Value = 0;
				progBar.Visible = true;

				TextCompressor.Initialize(this.CurrentStrings);
				for (int i = 0; i < this.CurrentStrings.Count; i++)
				{
					if (i % 10 == 0)
					{
						progBar.Value = (int)(((float)i / (float)CurrentStrings.Count) * 100);
						Application.DoEvents();
					}
					TextCompressor.CompressStrings(i);
				}

				TextCompressor.ExportToBytes();

				if (TextCompressor.StringBytes.Count > (Constants.TotalTextLength * 2))
				{
					MessageBox.Show("Total Length of Compressed Strings is longer than the maximum!", "Unable to Save!", MessageBoxButtons.OK);

					numTotalBytes.Value = TextCompressor.StringBytes.Count;
					numTotalBytes.Visible = true;
					lblTotalBytes.Visible = true;
					lblByteLimit.Visible = true;
				}
				else
				{
					numTotalBytes.Visible = false;
					lblTotalBytes.Visible = false;
					lblByteLimit.Visible = false;

					SaveToISO();

					progBar.Value = 100;
					MessageBox.Show("Save Complete!");
					progBar.Visible = false;
					this.FileEdited = false;
				}
			}
			else
			{
				MessageBox.Show("No changes, nothing to save.", "Unable to Save!", MessageBoxButtons.OK);
			}
		}

		private void exportToCSVToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (FileLoaded)
			{
				if (saveFD.ShowDialog() == DialogResult.OK)
				{
					StreamWriter writer = new StreamWriter(saveFD.FileName);
					for (int i = 30; i < CurrentStrings.Count; i++)
					{
						if (isEditableIndex(i))
						{
							int sectionindex = GetSectionIndex(i);
							string title = GetTitle(sectionindex, i- SectionIndexes[sectionindex]);

							string outstring = ReplacePlaceholderChars(CurrentStrings[i]);
							string[] split = outstring.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
							foreach (string s in split)
								writer.WriteLine(i.ToString() + "\t" + s + "\t" + title);
						}
					}
					writer.Close();

					MessageBox.Show("TSV Exported!");
				}
			}
			else
				MessageBox.Show("Load ISO First!", "!", MessageBoxButtons.OK);
		}

		private void importFromCSVToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (FileLoaded)
			{
				if (openTSVFD.ShowDialog() == DialogResult.OK)
				{
					StreamReader reader = new StreamReader(openTSVFD.FileName);
					string[] allstrings = reader.ReadToEnd().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    reader.Close();

                    int index = 0;
					string savestring = "";
					foreach (string s in allstrings)
					{
						if (s != "")
						{
							string[] split = s.Split('\t');
							if (Convert.ToInt32(split[0]) == index)
								savestring += System.Environment.NewLine + split[1];
							else
							{
								index = Convert.ToInt32(split[0]);
								savestring = split[1];
							}
							if (isEditableIndex(index))
							{
								CurrentStrings[index] = ReenterPlaceholderChars(savestring);
							}
						}
					}

					MessageBox.Show("TSV Imported!");
					FileEdited = true;
				}
			}
			else
				MessageBox.Show("Load ISO First!", "!", MessageBoxButtons.OK);
		}

		private void btnReset_Click(object sender, EventArgs e)
        {
			if (this.FileLoaded & isEditableIndex(numIndex.Value))
			{
				int index = (int)numIndex.Value;

				CurrentStrings[index] = TextReader.VanillaStrings[index];

				UpdateFront();
			}
        }

		private void btnClearTutorial_Click(object sender, EventArgs e)
		{
			if (FileLoaded)
			{
				for (int i = Constants.TutorialIndexStart; i < CurrentStrings.Count; i++)
					CurrentStrings[i] = ".";

				FileEdited = true;
				MessageBox.Show("Tutorial Strings Cleared!");
			}
		}


	}
}
