using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Xml;
using System.Xml.Schema;
using Microsoft.Exchange.CabUtility;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.UpdatableHelp
{
	// Token: 0x02000BFD RID: 3069
	internal class HelpInstaller
	{
		// Token: 0x060073CE RID: 29646 RVA: 0x001D7344 File Offset: 0x001D5544
		internal HelpInstaller(HelpUpdater updater, string[] culturesToInstall, double progressUpperLimit)
		{
			this.helpUpdater = updater;
			this.cabinetFiles = null;
			this.filesAffected = 0;
			this.affectedCultures = culturesToInstall;
			this.progressUpperBound = progressUpperLimit;
		}

		// Token: 0x060073CF RID: 29647 RVA: 0x001D7370 File Offset: 0x001D5570
		internal int ExtractToTemp()
		{
			this.filesAffected = 0;
			this.helpUpdater.EnsureDirectory(this.helpUpdater.LocalCabinetExtractionTargetPath);
			this.helpUpdater.CleanDirectory(this.helpUpdater.LocalCabinetExtractionTargetPath);
			bool flag = false;
			string text = "";
			int result = EmbeddedCabWrapper.ExtractCabFiles(this.helpUpdater.LocalCabinetPath, this.helpUpdater.LocalCabinetExtractionTargetPath, text, flag);
			this.cabinetFiles = new Dictionary<string, List<string>>();
			this.helpUpdater.RecursiveDescent(0, this.helpUpdater.LocalCabinetExtractionTargetPath, string.Empty, this.affectedCultures, false, this.cabinetFiles);
			this.filesAffected = result;
			return result;
		}

		// Token: 0x060073D0 RID: 29648 RVA: 0x001D7414 File Offset: 0x001D5614
		public bool AtomicInstallFiles()
		{
			bool flag = false;
			int num = 0;
			string localCabinetExtractionTargetPath = this.helpUpdater.LocalCabinetExtractionTargetPath;
			Dictionary<string, List<KeyValuePair<string, bool>>> dictionary = new Dictionary<string, List<KeyValuePair<string, bool>>>();
			try
			{
				string text = null;
				foreach (KeyValuePair<string, List<string>> keyValuePair in this.cabinetFiles)
				{
					if (this.helpUpdater.Cmdlet.Abort)
					{
						break;
					}
					if (!keyValuePair.Key.Equals(text))
					{
						this.helpUpdater.EnsureDirectory(localCabinetExtractionTargetPath + keyValuePair.Key);
						text = keyValuePair.Key;
						dictionary.Add(keyValuePair.Key, new List<KeyValuePair<string, bool>>());
					}
					foreach (string text2 in keyValuePair.Value)
					{
						if (this.helpUpdater.Cmdlet.Abort)
						{
							break;
						}
						this.helpUpdater.UpdateProgress(UpdatePhase.Installing, new LocalizedString(text2), ++num, this.filesAffected);
						string text3 = localCabinetExtractionTargetPath + text + text2;
						string text4 = this.helpUpdater.ModuleBase + text + text2;
						bool flag2 = !File.Exists(text4);
						if (flag2)
						{
							File.Move(text3, text4);
						}
						else
						{
							File.Move(text4, text3 + ".bak");
							File.Move(text3, text4);
						}
						dictionary[text].Add(new KeyValuePair<string, bool>(text2, flag2));
					}
				}
				flag = true;
			}
			catch
			{
				flag = false;
			}
			if (!flag || this.helpUpdater.Cmdlet.Abort)
			{
				double num2 = (this.progressUpperBound - this.helpUpdater.ProgressNumerator) / (double)num;
				foreach (KeyValuePair<string, List<KeyValuePair<string, bool>>> keyValuePair2 in dictionary)
				{
					foreach (KeyValuePair<string, bool> keyValuePair3 in keyValuePair2.Value)
					{
						this.helpUpdater.UpdateProgress(UpdatePhase.Rollback, LocalizedString.Empty, (int)this.helpUpdater.ProgressNumerator, 100);
						try
						{
							if (keyValuePair3.Value)
							{
								File.Delete(this.helpUpdater.ModuleBase + keyValuePair2.Key + keyValuePair3);
							}
							else
							{
								string text5 = this.helpUpdater.ModuleBase + keyValuePair2.Key + keyValuePair3;
								string text6 = localCabinetExtractionTargetPath + keyValuePair2.Key + keyValuePair3;
								File.Move(text6, text5);
								File.Move(text5 + ".bak", text6);
							}
						}
						catch
						{
						}
						this.helpUpdater.ProgressNumerator += num2;
					}
				}
				this.helpUpdater.UpdateProgress(UpdatePhase.Rollback, LocalizedString.Empty, (int)this.progressUpperBound, 100);
			}
			return flag;
		}

		// Token: 0x060073D1 RID: 29649 RVA: 0x001D77AC File Offset: 0x001D59AC
		internal Dictionary<string, LocalizedString> ValidateFiles()
		{
			Dictionary<string, LocalizedString> dictionary = new Dictionary<string, LocalizedString>();
			string text = this.LoadFileAsString(this.helpUpdater.PowerShellPsmamlSchemaFilePath);
			if (!string.IsNullOrEmpty(text))
			{
				HelpSchema helpSchema = new HelpSchema("http://schemas.microsoft.com/maml/2004/10", text);
				string localCabinetExtractionTargetPath = this.helpUpdater.LocalCabinetExtractionTargetPath;
				string text2 = null;
				int num = 0;
				foreach (KeyValuePair<string, List<string>> keyValuePair in this.cabinetFiles)
				{
					if (this.helpUpdater.Cmdlet.Abort)
					{
						return null;
					}
					if (!keyValuePair.Key.Equals(text2))
					{
						text2 = keyValuePair.Key;
					}
					foreach (string text3 in keyValuePair.Value)
					{
						if (this.helpUpdater.Cmdlet.Abort)
						{
							return null;
						}
						this.helpUpdater.UpdateProgress(UpdatePhase.Validating, new LocalizedString(text3), ++num, this.filesAffected);
						string text4 = Path.GetExtension(text3).ToLower();
						string text5 = text2 + text3;
						string path = localCabinetExtractionTargetPath + text5;
						if (File.Exists(path))
						{
							if (text4.Equals(".xml"))
							{
								string s = this.LoadFileAsString(path);
								XmlReader reader = XmlReader.Create(new StringReader(s));
								XmlDocument xmlDocument = new XmlDocument();
								XmlNode xmlNode = null;
								xmlDocument.Load(reader);
								if (xmlDocument.HasChildNodes)
								{
									bool flag = false;
									foreach (object obj in xmlDocument.ChildNodes)
									{
										XmlNode xmlNode2 = (XmlNode)obj;
										if (xmlNode2.LocalName.Equals("helpitems", StringComparison.InvariantCultureIgnoreCase))
										{
											if (xmlNode != null)
											{
												flag = true;
												break;
											}
											xmlNode = xmlNode2;
										}
									}
									if (flag)
									{
										dictionary.Add(text5, UpdatableHelpStrings.UpdateMultipleHelpItems);
										continue;
									}
								}
								if (xmlNode != null)
								{
									for (int i = 0; i < xmlNode.ChildNodes.Count; i++)
									{
										XmlNode xmlNode3 = xmlNode.ChildNodes[i];
										if (xmlNode3.NodeType == XmlNodeType.Element && !xmlNode3.LocalName.StartsWith("_"))
										{
											if (!xmlNode3.LocalName.Equals("providerHelp", StringComparison.OrdinalIgnoreCase))
											{
												if (xmlNode3.LocalName.Equals("para", StringComparison.OrdinalIgnoreCase))
												{
													if (!xmlNode3.NamespaceURI.Equals("http://schemas.microsoft.com/maml/2004/10", StringComparison.OrdinalIgnoreCase))
													{
														dictionary.Add(text5, UpdatableHelpStrings.UpdateInvalidXmlNamespace("http://schemas.microsoft.com/maml/2004/10"));
														break;
													}
													goto IL_2DD;
												}
												else if (!xmlNode3.NamespaceURI.Equals("http://schemas.microsoft.com/maml/dev/command/2004/10", StringComparison.OrdinalIgnoreCase))
												{
													dictionary.Add(text5, UpdatableHelpStrings.UpdateInvalidXmlNamespace("http://schemas.microsoft.com/maml/2004/10"));
													break;
												}
											}
											if (this.helpUpdater.Cmdlet.Abort)
											{
												return null;
											}
											try
											{
												helpSchema.CreateValidXmlDocument(xmlNode3.OuterXml, new ValidationEventHandler(this.ContentValidationHandler), false);
											}
											catch
											{
												dictionary.Add(text5, UpdatableHelpStrings.UpdateInvalidXml);
											}
										}
										IL_2DD:;
									}
								}
								else
								{
									dictionary.Add(text5, UpdatableHelpStrings.UpdateInvalidXmlRoot);
								}
							}
							else
							{
								if (text4.Equals(".txt"))
								{
									FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
									try
									{
										if (fileStream.Length > 2L)
										{
											byte[] array = new byte[2];
											fileStream.Read(array, 0, 2);
											if (array[0] == 77 && array[1] == 90)
											{
												dictionary.Add(text5, UpdatableHelpStrings.UpdateInvalidTextCharacters);
											}
										}
										continue;
									}
									finally
									{
										fileStream.Close();
									}
								}
								dictionary.Add(text5, UpdatableHelpStrings.UpdateUnsupportedFileType);
							}
						}
						else
						{
							dictionary.Add(text2 + text3, UpdatableHelpStrings.UpdateFileNotFound);
						}
					}
				}
				return dictionary;
			}
			return dictionary;
		}

		// Token: 0x060073D2 RID: 29650 RVA: 0x001D7C00 File Offset: 0x001D5E00
		private void ContentValidationHandler(object sender, ValidationEventArgs arg)
		{
			switch (arg.Severity)
			{
			case XmlSeverityType.Error:
				throw new UpdatableExchangeHelpSystemException(UpdatableHelpStrings.UpdateContentXmlValidationFailureErrorID, UpdatableHelpStrings.UpdateContentXmlValidationFailure, ErrorCategory.NotInstalled, null, null);
			case XmlSeverityType.Warning:
				return;
			default:
				return;
			}
		}

		// Token: 0x060073D3 RID: 29651 RVA: 0x001D7C38 File Offset: 0x001D5E38
		private string LoadFileAsString(string path)
		{
			StreamReader streamReader = null;
			string result = null;
			if (!string.IsNullOrEmpty(path) && File.Exists(path))
			{
				try
				{
					streamReader = new StreamReader(path);
					result = streamReader.ReadToEnd();
				}
				catch
				{
					result = null;
				}
				finally
				{
					if (streamReader != null)
					{
						streamReader.Close();
						streamReader.Dispose();
					}
				}
			}
			return result;
		}

		// Token: 0x04003AE3 RID: 15075
		private const string HelpContentSchemaNamespace = "http://schemas.microsoft.com/maml/2004/10";

		// Token: 0x04003AE4 RID: 15076
		private const string BackupFileExtension = ".bak";

		// Token: 0x04003AE5 RID: 15077
		private HelpUpdater helpUpdater;

		// Token: 0x04003AE6 RID: 15078
		private Dictionary<string, List<string>> cabinetFiles;

		// Token: 0x04003AE7 RID: 15079
		private string[] affectedCultures;

		// Token: 0x04003AE8 RID: 15080
		private int filesAffected;

		// Token: 0x04003AE9 RID: 15081
		private readonly double progressUpperBound;
	}
}
