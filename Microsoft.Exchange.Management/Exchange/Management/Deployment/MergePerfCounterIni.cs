using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020002AF RID: 687
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MergePerfCounterIni
	{
		// Token: 0x06001846 RID: 6214 RVA: 0x000664C8 File Offset: 0x000646C8
		public void ParseDirectories(string sourceDir)
		{
			TaskLogger.Log(Strings.LanguagePackPerfCounterMergeStart);
			if (!Directory.Exists(sourceDir))
			{
				throw new ArgumentException();
			}
			this.GetLangugageDirs(sourceDir);
			if (this.languageDirs.Count == 0)
			{
				return;
			}
			foreach (string text in Directory.GetFiles(sourceDir, "*.ini"))
			{
				string str = Path.Combine(sourceDir, Path.GetFileNameWithoutExtension(text));
				if ((File.Exists(str + ".h") && File.Exists(str + ".xml")) || File.Exists(str + ".dll"))
				{
					TaskLogger.Log(Strings.LanguagePackPerfCounterMergeStatus(Path.GetFileName(text)));
					this.CreateLocalizedIniFile(text);
				}
			}
			TaskLogger.Log(Strings.LanguagePackPerfCounterMergeEnd);
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x00066584 File Offset: 0x00064784
		private void GetLangugageDirs(string sourceDir)
		{
			int count = sourceDir.Length + 1;
			foreach (string text in Directory.GetDirectories(sourceDir))
			{
				try
				{
					string name = text.Remove(0, count);
					CultureInfo.GetCultureInfo(name);
					this.languageDirs.Add(text);
				}
				catch (ArgumentException)
				{
				}
			}
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x000665EC File Offset: 0x000647EC
		private void CreateLocalizedIniFile(string iniFile)
		{
			string fileName = Path.GetFileName(iniFile);
			Dictionary<int, MergePerfCounterIni.PerfCounterIniData> dictionary = new Dictionary<int, MergePerfCounterIni.PerfCounterIniData>(this.languageDirs.Count);
			bool flag = false;
			string value = string.Empty;
			foreach (string text in this.languageDirs)
			{
				string path = Path.Combine(text, fileName);
				string langDirctoryName = text.Substring(text.LastIndexOf('\\') + 1);
				if (File.Exists(path))
				{
					using (StreamReader streamReader = new StreamReader(path))
					{
						string empty = string.Empty;
						List<int> list = new List<int>();
						if (!flag)
						{
							value = this.ReadInfoSection(streamReader, ref empty);
						}
						else
						{
							this.ReadInfoSection(streamReader, ref empty);
						}
						if (!(empty.Trim().ToLower() != "[languages]"))
						{
							if (!flag)
							{
								flag = true;
							}
							this.ReadLanguagesSection(streamReader, ref empty, list, dictionary, langDirctoryName);
							if ((!(empty.Trim().ToLower() != "[objects]") || !(empty.Trim().ToLower() != "[text]")) && list.Count != 0)
							{
								if (empty.Trim().ToLower() == "[objects]")
								{
									this.ReadObjectsSection(streamReader, ref empty, list, dictionary, langDirctoryName);
								}
								if (!(empty.Trim().ToLower() != "[text]"))
								{
									this.ReadTextSection(streamReader, ref empty, list, dictionary, langDirctoryName);
								}
							}
						}
					}
				}
			}
			if (dictionary.Count > 0)
			{
				string text2 = iniFile + ".tmp";
				using (StreamWriter streamWriter = new StreamWriter(text2, false, Encoding.Unicode))
				{
					streamWriter.Write(value);
					streamWriter.WriteLine("\n[languages]");
					foreach (KeyValuePair<int, MergePerfCounterIni.PerfCounterIniData> keyValuePair in dictionary)
					{
						string arg;
						if (keyValuePair.Key == 0)
						{
							arg = "Neutral";
						}
						else
						{
							arg = CultureInfo.GetCultureInfo(keyValuePair.Key).EnglishName;
						}
						streamWriter.WriteLine(string.Format("{0:X3}={1}", keyValuePair.Key, arg));
					}
					bool flag2 = false;
					foreach (KeyValuePair<int, MergePerfCounterIni.PerfCounterIniData> keyValuePair2 in dictionary)
					{
						if (keyValuePair2.Value.Objects.Count > 0)
						{
							if (!flag2)
							{
								flag2 = true;
								streamWriter.WriteLine("\n[objects]");
							}
							foreach (string value2 in keyValuePair2.Value.Objects)
							{
								streamWriter.WriteLine(value2);
							}
						}
					}
					flag2 = false;
					foreach (KeyValuePair<int, MergePerfCounterIni.PerfCounterIniData> keyValuePair3 in dictionary)
					{
						if (keyValuePair3.Value.Data.Count > 0)
						{
							if (!flag2)
							{
								flag2 = true;
								streamWriter.WriteLine("\n[text]");
							}
							foreach (string value3 in keyValuePair3.Value.Data)
							{
								streamWriter.WriteLine(value3);
							}
						}
					}
				}
				this.DeleteINIFile(iniFile);
				if (!File.Exists(iniFile))
				{
					File.Move(text2, iniFile);
					return;
				}
				File.Delete(text2);
			}
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x00066A40 File Offset: 0x00064C40
		private string ReadInfoSection(StreamReader sr, ref string lastLine)
		{
			bool flag = false;
			bool flag2 = false;
			StringBuilder stringBuilder = new StringBuilder(256);
			while (!flag)
			{
				lastLine = sr.ReadLine();
				if (lastLine == null)
				{
					flag = true;
				}
				else if (!lastLine.Trim().Equals(""))
				{
					if (!flag2 && lastLine.Length > 0)
					{
						if (lastLine.Length > 0 && lastLine.Trim().ToLower() == "[info]")
						{
							flag2 = true;
							stringBuilder.AppendLine(lastLine);
						}
						else if (!lastLine.Trim().ToLower().StartsWith(";") && !lastLine.Trim().ToLower().StartsWith("//"))
						{
							flag = true;
						}
					}
					else if (lastLine.Trim().ToLower() == "[languages]")
					{
						flag = true;
					}
					else
					{
						stringBuilder.AppendLine(lastLine);
					}
				}
			}
			if (!flag2)
			{
				lastLine = string.Empty;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x00066B38 File Offset: 0x00064D38
		private void ReadLanguagesSection(StreamReader sr, ref string lastLine, List<int> newLanguages, Dictionary<int, MergePerfCounterIni.PerfCounterIniData> localizedData, string langDirctoryName)
		{
			bool flag = false;
			while (!flag)
			{
				int num = 0;
				lastLine = sr.ReadLine();
				if (lastLine == null || lastLine.Trim().ToLower() == "[objects]" || lastLine.Trim().ToLower() == "[text]")
				{
					flag = true;
				}
				else
				{
					this.arrLcid = this.SetLanguageLCID(langDirctoryName);
					foreach (int num2 in this.arrLcid)
					{
						if (num2 == 0)
						{
							break;
						}
						if (num2 == -1)
						{
							int num3 = lastLine.IndexOf('=');
							if (num3 > -1)
							{
								num = int.Parse(lastLine.Remove(num3), NumberStyles.HexNumber);
							}
						}
						else if (num2 != 0)
						{
							num = num2;
						}
						if (num > 0 && !localizedData.ContainsKey(num) && !newLanguages.Contains(num))
						{
							newLanguages.Add(num);
						}
					}
				}
			}
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x00066C10 File Offset: 0x00064E10
		private void ReadObjectsSection(StreamReader sr, ref string lastLine, List<int> newLanguages, Dictionary<int, MergePerfCounterIni.PerfCounterIniData> localizedData, string langDirctoryName)
		{
			bool flag = false;
			bool flag2 = false;
			while (!flag)
			{
				lastLine = sr.ReadLine();
				if (lastLine == null || lastLine.Trim().ToLower() == "[text]")
				{
					flag = true;
				}
				else if (lastLine.Length > 0)
				{
					int num = 0;
					this.arrLcid = this.SetLanguageLCID(langDirctoryName);
					foreach (int num2 in this.arrLcid)
					{
						if (num2 == -1)
						{
							flag2 = true;
							int num3 = lastLine.LastIndexOf("_NAME=", StringComparison.OrdinalIgnoreCase) - 3;
							if (num3 > 0)
							{
								num = int.Parse(lastLine.Remove(0, num3).Remove(3), NumberStyles.HexNumber);
							}
						}
						else if (!flag2 && num2 != 0)
						{
							string[] array2 = lastLine.Split(new char[]
							{
								'='
							});
							string[] array3 = array2[0].Split(new char[]
							{
								'_'
							});
							if (array3[array3.Length - 2].Equals("009"))
							{
								num = int.Parse(array3[array3.Length - 2]);
							}
							else
							{
								num = num2;
								StringBuilder stringBuilder = new StringBuilder(lastLine);
								int startIndex = array2[0].Length - (array3[array3.Length - 1].Length + 1 + array3[array3.Length - 2].Length);
								stringBuilder.Replace(lastLine.Substring(startIndex, 3), string.Format("{0:X3}", num));
								lastLine = stringBuilder.ToString();
							}
						}
						if (num > 0 && num2 != 0 && newLanguages.Contains(num))
						{
							if (!localizedData.ContainsKey(num))
							{
								localizedData.Add(num, new MergePerfCounterIni.PerfCounterIniData
								{
									Objects = new List<string>(),
									Data = new List<string>()
								});
							}
							localizedData[num].Objects.Add(lastLine);
						}
					}
				}
				flag2 = false;
			}
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x00066DFC File Offset: 0x00064FFC
		private void ReadTextSection(StreamReader sr, ref string lastLine, List<int> newLanguages, Dictionary<int, MergePerfCounterIni.PerfCounterIniData> localizedData, string langDirctoryName)
		{
			bool flag = false;
			lastLine = sr.ReadLine();
			while (lastLine != null)
			{
				if (lastLine.Length > 0)
				{
					int num = 0;
					this.arrLcid = this.SetLanguageLCID(langDirctoryName);
					foreach (int num2 in this.arrLcid)
					{
						if (num2 == -1)
						{
							flag = true;
							int num3 = lastLine.LastIndexOf("_NAME=", StringComparison.OrdinalIgnoreCase) - 3;
							if (num3 < 1)
							{
								num3 = lastLine.LastIndexOf("_HELP=", StringComparison.OrdinalIgnoreCase) - 3;
							}
							if (num3 > 0)
							{
								num = int.Parse(lastLine.Remove(0, num3).Remove(3), NumberStyles.HexNumber);
							}
						}
						else if (!flag && num2 != 0)
						{
							string[] array2 = lastLine.Split(new char[]
							{
								'='
							});
							string[] array3 = array2[0].Split(new char[]
							{
								'_'
							});
							if (array3[array3.Length - 2].Equals("009"))
							{
								num = int.Parse(array3[array3.Length - 2]);
							}
							else
							{
								num = num2;
								StringBuilder stringBuilder = new StringBuilder(lastLine);
								int startIndex = array2[0].Length - (array3[array3.Length - 1].Length + 1 + array3[array3.Length - 2].Length);
								stringBuilder.Replace(lastLine.Substring(startIndex, 3), string.Format("{0:X3}", num));
								lastLine = stringBuilder.ToString();
							}
						}
						if (num > 0 && num2 != 0 && newLanguages.Contains(num))
						{
							if (!localizedData.ContainsKey(num))
							{
								localizedData.Add(num, new MergePerfCounterIni.PerfCounterIniData
								{
									Objects = new List<string>(),
									Data = new List<string>()
								});
							}
							localizedData[num].Data.Add(lastLine);
						}
					}
				}
				lastLine = sr.ReadLine();
				flag = false;
			}
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x00066FDC File Offset: 0x000651DC
		private int[] SetLanguageLCID(string langDirectoryName)
		{
			int[] array = new int[2];
			int num = 0;
			bool flag = false;
			if (this.langCodes.Count == 0)
			{
				this.FillLanguageLCID();
			}
			IDictionaryEnumerator enumerator = this.langCodes.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (langDirectoryName == enumerator.Key.ToString())
				{
					flag = true;
					if (enumerator.Value.ToString().Contains(","))
					{
						string[] array2 = enumerator.Value.ToString().Split(new char[]
						{
							','
						});
						for (int i = 0; i < array2.Length; i++)
						{
							array[i] = int.Parse(array2[i], NumberStyles.HexNumber);
						}
					}
					else
					{
						array[num] = int.Parse(enumerator.Value.ToString(), NumberStyles.HexNumber);
					}
				}
			}
			if (!flag)
			{
				array[num] = -1;
			}
			return array;
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x000670BC File Offset: 0x000652BC
		private void FillLanguageLCID()
		{
			this.langCodes.Add("zh-hans", "0804");
			this.langCodes.Add("zh-hant", "0404,0c04");
			this.langCodes.Add("pt", "0416");
			this.langCodes.Add("pt-pt", "0816");
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x00067120 File Offset: 0x00065320
		private void DeleteINIFile(string iniFile)
		{
			for (int i = 0; i < 3; i++)
			{
				try
				{
					File.Delete(iniFile);
					break;
				}
				catch (Exception e)
				{
					TaskLogger.LogError(e);
					Thread.Sleep(2000);
				}
			}
		}

		// Token: 0x04000A9A RID: 2714
		private List<string> languageDirs = new List<string>();

		// Token: 0x04000A9B RID: 2715
		private Hashtable langCodes = new Hashtable();

		// Token: 0x04000A9C RID: 2716
		private int[] arrLcid = new int[2];

		// Token: 0x020002B0 RID: 688
		private struct PerfCounterIniData
		{
			// Token: 0x04000A9D RID: 2717
			public List<string> Objects;

			// Token: 0x04000A9E RID: 2718
			public List<string> Data;
		}
	}
}
