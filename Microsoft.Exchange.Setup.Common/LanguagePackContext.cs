using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.CabUtility;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Deployment;
using Microsoft.Exchange.Setup.AcquireLanguagePack;
using Microsoft.Exchange.Setup.CommonBase;
using Microsoft.Win32;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000040 RID: 64
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class LanguagePackContext
	{
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000A309 File Offset: 0x00008509
		// (set) Token: 0x060002FF RID: 767 RVA: 0x0000A32C File Offset: 0x0000852C
		public LongPath LanguagePackPath
		{
			get
			{
				if (this.languagePackPath == null)
				{
					this.languagePackPath = this.sourceDir;
				}
				return this.languagePackPath;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.languagePackPath = value;
				try
				{
					if (this.installationMode == InstallationModes.Install || this.installationMode == InstallationModes.BuildToBuildUpgrade || this.installationMode == InstallationModes.DisasterRecovery)
					{
						this.ComputeLanguagesToInstall();
					}
				}
				catch (IOException e)
				{
					SetupLogger.Log(Strings.LanguagePackPathNotFoundError);
					SetupLogger.LogError(e);
					throw new LPPathNotFoundException();
				}
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000A39C File Offset: 0x0000859C
		public bool LanguagePackSourceIsBundle
		{
			get
			{
				bool result = false;
				if (this.LanguagePackPath != null && this.LanguagePackPath.PathName != null)
				{
					result = (Path.GetFileName(this.languagePackPath.PathName).ToLower() == "languagepackbundle.exe");
				}
				return result;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000A3E7 File Offset: 0x000085E7
		public Dictionary<string, LanguageInfo> CollectedLanguagePacks
		{
			get
			{
				return this.collectedLanguagePacks;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000A3EF File Offset: 0x000085EF
		public Dictionary<string, LanguageInfo> SourceLanguagePacks
		{
			get
			{
				return this.sourceLanguagePacks;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000A3F7 File Offset: 0x000085F7
		public Dictionary<string, Array> LanguagePacksToInstall
		{
			get
			{
				return this.languagePacksToInstall;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0000A3FF File Offset: 0x000085FF
		public Dictionary<string, long> LanguagesToInstall
		{
			get
			{
				return this.languagesToInstall;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0000A407 File Offset: 0x00008607
		public HashSet<string> InstalledLanguagePacks
		{
			get
			{
				return this.installedLanguagePacks;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000306 RID: 774 RVA: 0x0000A40F File Offset: 0x0000860F
		// (set) Token: 0x06000307 RID: 775 RVA: 0x0000A417 File Offset: 0x00008617
		public bool IsLanguagePackOperation
		{
			get
			{
				return this.isLanguagePackOperation;
			}
			set
			{
				this.isLanguagePackOperation = value;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0000A420 File Offset: 0x00008620
		// (set) Token: 0x06000309 RID: 777 RVA: 0x0000A428 File Offset: 0x00008628
		public bool IsLanaguagePacksInstalled
		{
			get
			{
				return this.isLanaguagePacksInstalled;
			}
			set
			{
				this.isLanaguagePacksInstalled = value;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600030A RID: 778 RVA: 0x0000A431 File Offset: 0x00008631
		// (set) Token: 0x0600030B RID: 779 RVA: 0x0000A439 File Offset: 0x00008639
		public bool NeedToUpdateLanguagePacks
		{
			get
			{
				return this.needToUpdateLanguagePacks;
			}
			set
			{
				this.needToUpdateLanguagePacks = value;
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000A444 File Offset: 0x00008644
		public LanguagePackContext(InstallationModes installMode, bool fSetupTokensHasLangPack, LongPath languagePackPath, bool fCleanMachine, bool isUmLangPackOperation, LongPath srcDir)
		{
			this.isCleanMachine = fCleanMachine;
			this.installationMode = installMode;
			this.fSetupTokensHasLanguagePack = fSetupTokensHasLangPack;
			this.isUmLanguagePackOperation = isUmLangPackOperation;
			this.sourceDir = srcDir;
			if (!this.IsLanguagePackOperation && srcDir != null)
			{
				this.LanguagePackPath = this.sourceDir;
			}
			if (fSetupTokensHasLangPack)
			{
				this.LanguagePackPath = languagePackPath;
				this.IsLanguagePackOperation = true;
			}
			this.CollectInstalledLanguagePacks();
			this.ComputeInstalledLanguagePacks();
			this.ComputeLanguagePacksToInstall();
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000A4CC File Offset: 0x000086CC
		private void ComputeLanguagesToInstall()
		{
			this.languagesToInstall = new Dictionary<string, long>();
			this.sourceLanguagePacks = new Dictionary<string, LanguageInfo>();
			LanguageInfo value = default(LanguageInfo);
			string text = "ClientLanguagePack.msi";
			string text2 = "ServerLanguagePack.msi";
			string text3 = string.Empty;
			if (this.isUmLanguagePackOperation && !this.fSetupTokensHasLanguagePack)
			{
				text3 = ConfigurationContext.Setup.GetExecutingVersion().ToString();
			}
			else
			{
				text3 = this.GetVersionInfo();
			}
			if (this.installationMode == InstallationModes.Install || this.installationMode == InstallationModes.BuildToBuildUpgrade || this.installationMode == InstallationModes.DisasterRecovery || this.IsLanguagePackOperation)
			{
				if (this.LanguagePackSourceIsBundle)
				{
					this.languagesToInstall = EmbeddedCabWrapper.FindAllBaseDirectoriesAndMSIFiles(this.LanguagePackPath.PathName);
					using (Dictionary<string, long>.KeyCollection.Enumerator enumerator = this.languagesToInstall.Keys.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string text4 = enumerator.Current;
							string[] array = text4.Split(new char[]
							{
								'\\'
							});
							if (array.Length == 2)
							{
								string key = array[0];
								if (this.SourceLanguagePacks.ContainsKey(key))
								{
									value = this.SourceLanguagePacks[key];
								}
								else
								{
									value.ClientVersion = text3;
									value.ServerVersion = text3;
									value.ClientPresent = false;
									value.ServerPresent = false;
								}
								if (array[1] == text)
								{
									value.ClientPresent = true;
								}
								else if (array[1] == text2)
								{
									value.ServerPresent = true;
								}
								this.sourceLanguagePacks[key] = value;
							}
							else
							{
								SetupLogger.LogWarning(Strings.UnexpectedFileFromBundle(text4));
							}
						}
						return;
					}
				}
				string[] directories;
				try
				{
					directories = Directory.GetDirectories(this.LanguagePackPath.PathName);
				}
				catch (DirectoryNotFoundException e)
				{
					SetupLogger.Log(Strings.LanguagePackPathNotFoundError);
					SetupLogger.LogError(e);
					throw new LPPathNotFoundException();
				}
				catch (Exception ex)
				{
					SetupLogger.LogError(ex);
					throw ex;
				}
				string[] array2 = directories;
				int i = 0;
				while (i < array2.Length)
				{
					string text5 = array2[i];
					string text6 = string.Empty;
					int num = text5.LastIndexOf('\\');
					text6 = text5.Substring(num + 1);
					try
					{
						CultureInfo.GetCultureInfo(text6);
					}
					catch (ArgumentException)
					{
						goto IL_29C;
					}
					goto IL_200;
					IL_29C:
					i++;
					continue;
					IL_200:
					value.ClientVersion = text3;
					value.ServerVersion = text3;
					value.ClientPresent = File.Exists(Path.Combine(text5, text));
					value.ServerPresent = File.Exists(Path.Combine(text5, text2));
					this.sourceLanguagePacks.Add(text6, value);
					long num2 = 0L;
					string[] files = Directory.GetFiles(text5, "*", SearchOption.AllDirectories);
					foreach (string fileName in files)
					{
						num2 += new FileInfo(fileName).Length;
					}
					this.languagesToInstall.Add(Path.GetFileName(text5), num2);
					goto IL_29C;
				}
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000A7BC File Offset: 0x000089BC
		private void ComputeInstalledLanguagePacks()
		{
			this.installedLanguagePacks = new HashSet<string>();
			if (!this.isCleanMachine)
			{
				string name = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall";
				Regex regex = new Regex("{DEDFFB[0-9a-fA-F]{2}-42EC-4E26-[0-9a-fA-F]{4}-430E86DF378C}");
				Regex regex2 = new Regex("{521E60[0-9a-fA-F]{2}-B4B1-4CBC-[0-9a-fA-F]{4}-25AD697801FA}");
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(name))
				{
					string[] subKeyNames = registryKey.GetSubKeyNames();
					foreach (string text in subKeyNames)
					{
						if (regex.IsMatch(text) || regex2.IsMatch(text))
						{
							this.installedLanguagePacks.Add(text);
						}
					}
				}
			}
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000A868 File Offset: 0x00008A68
		private void CollectInstalledLanguagePacks()
		{
			string text = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Language Packs\\";
			string text2 = text + "Server";
			string text3 = text + "Client";
			LanguageInfo value = default(LanguageInfo);
			this.collectedLanguagePacks = new Dictionary<string, LanguageInfo>();
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(text))
			{
				if (registryKey != null)
				{
					string text4 = string.Empty;
					string text5 = string.Empty;
					using (RegistryKey registryKey2 = Registry.LocalMachine.OpenSubKey(text3))
					{
						using (RegistryKey registryKey3 = Registry.LocalMachine.OpenSubKey(text2))
						{
							if (registryKey2 != null && registryKey3 != null)
							{
								string[] subKeyNames = registryKey2.GetSubKeyNames();
								string[] subKeyNames2 = registryKey3.GetSubKeyNames();
								string[] array = new string[subKeyNames.Length + subKeyNames2.Length];
								subKeyNames.CopyTo(array, 0);
								subKeyNames2.CopyTo(array, subKeyNames.Length);
								Array.Sort<string>(array, StringComparer.InvariantCultureIgnoreCase);
								if (array.Length > 0)
								{
									string strB = string.Empty;
									foreach (string text6 in array)
									{
										if (string.Compare(text6, strB, true, CultureInfo.InvariantCulture) != 0)
										{
											strB = text6;
											using (RegistryKey registryKey4 = Registry.LocalMachine.OpenSubKey(Path.Combine(text3, text6)))
											{
												using (RegistryKey registryKey5 = Registry.LocalMachine.OpenSubKey(Path.Combine(text2, text6)))
												{
													bool clientPresent;
													bool serverPresent;
													if (registryKey4 != null)
													{
														text4 = (string)registryKey4.GetValue("Version");
														clientPresent = true;
														if (registryKey5 != null)
														{
															text5 = (string)registryKey5.GetValue("Version");
															serverPresent = true;
														}
														else
														{
															text5 = text4;
															serverPresent = false;
														}
													}
													else
													{
														text5 = (string)registryKey5.GetValue("Version");
														serverPresent = true;
														text4 = text5;
														clientPresent = false;
													}
													value.ClientVersion = text4;
													value.ClientPresent = clientPresent;
													value.ServerVersion = text5;
													value.ServerPresent = serverPresent;
													this.collectedLanguagePacks.Add(text6, value);
												}
											}
										}
									}
								}
								else
								{
									this.IsLanaguagePacksInstalled = false;
								}
							}
							else
							{
								this.IsLanaguagePacksInstalled = false;
							}
						}
						goto IL_215;
					}
				}
				this.IsLanaguagePacksInstalled = false;
				IL_215:;
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000AB14 File Offset: 0x00008D14
		private void ComputeLanguagePacksToInstall()
		{
			this.languagePacksToInstall = new Dictionary<string, Array>();
			if (this.installationMode == InstallationModes.Install || this.installationMode == InstallationModes.BuildToBuildUpgrade || this.installationMode == InstallationModes.DisasterRecovery || this.IsLanguagePackOperation)
			{
				foreach (KeyValuePair<string, LanguageInfo> keyValuePair in this.sourceLanguagePacks)
				{
					bool[] array = new bool[2];
					string key = keyValuePair.Key.ToString();
					if (this.IsLanaguagePacksInstalled)
					{
						LanguageInfo languageInfo;
						if (this.collectedLanguagePacks.TryGetValue(key, out languageInfo))
						{
							bool clientPresent = languageInfo.ClientPresent;
							bool serverPresent = languageInfo.ServerPresent;
							string clientVersion = languageInfo.ClientVersion;
							string serverVersion = languageInfo.ServerVersion;
							Version v = new Version(clientVersion);
							Version v2 = new Version(serverVersion);
							bool clientPresent2 = keyValuePair.Value.ClientPresent;
							bool serverPresent2 = keyValuePair.Value.ServerPresent;
							string clientVersion2 = keyValuePair.Value.ClientVersion;
							string serverVersion2 = keyValuePair.Value.ServerVersion;
							Version v3 = new Version(clientVersion2);
							Version v4 = new Version(serverVersion2);
							if (clientPresent != clientPresent2 || serverPresent != serverPresent2 || !(v >= v3) || !(v2 >= v4))
							{
								if (clientPresent == clientPresent2 && clientVersion == clientVersion2)
								{
									array[0] = false;
								}
								else
								{
									array[0] = keyValuePair.Value.ClientPresent;
								}
								if (serverPresent == serverPresent2 && serverVersion == serverVersion2)
								{
									array[1] = false;
								}
								else
								{
									array[1] = keyValuePair.Value.ServerPresent;
								}
								this.languagePacksToInstall.Add(key, array);
							}
						}
						else
						{
							array[0] = keyValuePair.Value.ClientPresent;
							array[1] = keyValuePair.Value.ServerPresent;
							this.languagePacksToInstall.Add(key, array);
						}
					}
					else
					{
						array[0] = keyValuePair.Value.ClientPresent;
						array[1] = keyValuePair.Value.ServerPresent;
						this.languagePacksToInstall.Add(key, array);
					}
				}
			}
			if (this.languagePacksToInstall.Count < 1)
			{
				this.needToUpdateLanguagePacks = false;
			}
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000AD44 File Offset: 0x00008F44
		private string GetVersionInfo()
		{
			string empty = string.Empty;
			string buildVersion = string.Empty;
			string inputPath = string.Empty;
			if (this.LanguagePackSourceIsBundle)
			{
				string text = Path.Combine(Path.GetTempPath(), "LBVersioningFromBundle");
				EmbeddedCabWrapper.ExtractFiles(this.LanguagePackPath.ToString(), text, "LPVersioning.xml");
				text = Path.Combine(text, "LPVersioning.xml");
				if (!File.Exists(text))
				{
					throw new LanguagePackBundleLoadException(Strings.LPVersioningExtractionFailed(text));
				}
				inputPath = text;
			}
			else if (File.Exists(Path.Combine(this.LanguagePackPath.ToString(), "LPVersioning.xml")))
			{
				inputPath = Path.Combine(this.LanguagePackPath.ToString(), "LPVersioning.xml");
			}
			else
			{
				if (!File.Exists(Path.Combine(this.LanguagePackPath.ToString(), "Setup\\ServerRoles\\Common\\LPVersioning.xml")))
				{
					throw new LPVersioningValueException(Strings.UnableToFindLPVersioning);
				}
				inputPath = Path.Combine(this.LanguagePackPath.ToString(), "Setup\\ServerRoles\\Common\\LPVersioning.xml");
			}
			buildVersion = LanguagePackVersion.GetBuildVersion(inputPath);
			return this.ConvertVersionToNumericLike(buildVersion);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000AE44 File Offset: 0x00009044
		private string ConvertVersionToNumericLike(string buildVersion)
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			try
			{
				string[] array = buildVersion.Split(new char[]
				{
					'.'
				});
				for (int i = 0; i < 4; i++)
				{
					stringBuilder.Append(((int)Convert.ToInt16(array[i])).ToString());
					if (i != 3)
					{
						stringBuilder.Append('.');
					}
				}
			}
			catch (Exception e)
			{
				SetupLogger.LogError(e);
				SetupLogger.Log(Strings.UnableToFindBuildVersion);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040000AF RID: 175
		private LongPath languagePackPath;

		// Token: 0x040000B0 RID: 176
		private Dictionary<string, LanguageInfo> collectedLanguagePacks;

		// Token: 0x040000B1 RID: 177
		private Dictionary<string, LanguageInfo> sourceLanguagePacks;

		// Token: 0x040000B2 RID: 178
		private Dictionary<string, Array> languagePacksToInstall;

		// Token: 0x040000B3 RID: 179
		private Dictionary<string, long> languagesToInstall;

		// Token: 0x040000B4 RID: 180
		private HashSet<string> installedLanguagePacks;

		// Token: 0x040000B5 RID: 181
		private bool isLanguagePackOperation;

		// Token: 0x040000B6 RID: 182
		private bool isLanaguagePacksInstalled = true;

		// Token: 0x040000B7 RID: 183
		private bool needToUpdateLanguagePacks = true;

		// Token: 0x040000B8 RID: 184
		private bool isCleanMachine;

		// Token: 0x040000B9 RID: 185
		private InstallationModes installationMode;

		// Token: 0x040000BA RID: 186
		private bool fSetupTokensHasLanguagePack;

		// Token: 0x040000BB RID: 187
		private bool isUmLanguagePackOperation;

		// Token: 0x040000BC RID: 188
		private LongPath sourceDir;
	}
}
