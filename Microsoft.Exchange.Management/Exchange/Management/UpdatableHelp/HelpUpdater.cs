using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.UpdatableHelp
{
	// Token: 0x02000BFA RID: 3066
	internal class HelpUpdater
	{
		// Token: 0x06007399 RID: 29593 RVA: 0x001D5F38 File Offset: 0x001D4138
		internal HelpUpdater(UpdatableExchangeHelpCommand cmdlet)
		{
			this.localCabinetFilename = null;
			this.CurrentHelpVersion = null;
			this.Cmdlet = cmdlet;
			this.ProgressNumerator = 0.0;
		}

		// Token: 0x17002395 RID: 9109
		// (get) Token: 0x0600739A RID: 29594 RVA: 0x001D5F64 File Offset: 0x001D4164
		// (set) Token: 0x0600739B RID: 29595 RVA: 0x001D5F6C File Offset: 0x001D416C
		internal string PowerShellPsmamlSchemaFilePath { get; private set; }

		// Token: 0x17002396 RID: 9110
		// (get) Token: 0x0600739C RID: 29596 RVA: 0x001D5F75 File Offset: 0x001D4175
		// (set) Token: 0x0600739D RID: 29597 RVA: 0x001D5F7D File Offset: 0x001D417D
		internal double ProgressNumerator { get; set; }

		// Token: 0x17002397 RID: 9111
		// (get) Token: 0x0600739E RID: 29598 RVA: 0x001D5F86 File Offset: 0x001D4186
		// (set) Token: 0x0600739F RID: 29599 RVA: 0x001D5F8E File Offset: 0x001D418E
		internal string ModuleBase { get; private set; }

		// Token: 0x17002398 RID: 9112
		// (get) Token: 0x060073A0 RID: 29600 RVA: 0x001D5F97 File Offset: 0x001D4197
		// (set) Token: 0x060073A1 RID: 29601 RVA: 0x001D5F9F File Offset: 0x001D419F
		internal string LocalTempBase { get; private set; }

		// Token: 0x17002399 RID: 9113
		// (get) Token: 0x060073A2 RID: 29602 RVA: 0x001D5FA8 File Offset: 0x001D41A8
		// (set) Token: 0x060073A3 RID: 29603 RVA: 0x001D5FB0 File Offset: 0x001D41B0
		internal string ManifestUrl { get; private set; }

		// Token: 0x1700239A RID: 9114
		// (get) Token: 0x060073A4 RID: 29604 RVA: 0x001D5FB9 File Offset: 0x001D41B9
		// (set) Token: 0x060073A5 RID: 29605 RVA: 0x001D5FC1 File Offset: 0x001D41C1
		internal UpdatableHelpVersion CurrentHelpVersion { get; private set; }

		// Token: 0x1700239B RID: 9115
		// (get) Token: 0x060073A6 RID: 29606 RVA: 0x001D5FCA File Offset: 0x001D41CA
		// (set) Token: 0x060073A7 RID: 29607 RVA: 0x001D5FD2 File Offset: 0x001D41D2
		internal int CurrentHelpRevision { get; private set; }

		// Token: 0x1700239C RID: 9116
		// (get) Token: 0x060073A8 RID: 29608 RVA: 0x001D5FDB File Offset: 0x001D41DB
		// (set) Token: 0x060073A9 RID: 29609 RVA: 0x001D5FE3 File Offset: 0x001D41E3
		internal int ThrottlingPeriodHours { get; private set; }

		// Token: 0x1700239D RID: 9117
		// (get) Token: 0x060073AA RID: 29610 RVA: 0x001D5FEC File Offset: 0x001D41EC
		// (set) Token: 0x060073AB RID: 29611 RVA: 0x001D5FF4 File Offset: 0x001D41F4
		internal DateTime LastSuccessfulCheckTimestampValue { get; private set; }

		// Token: 0x1700239E RID: 9118
		// (get) Token: 0x060073AC RID: 29612 RVA: 0x001D5FFD File Offset: 0x001D41FD
		// (set) Token: 0x060073AD RID: 29613 RVA: 0x001D6005 File Offset: 0x001D4205
		internal UpdatableExchangeHelpCommand Cmdlet { get; private set; }

		// Token: 0x1700239F RID: 9119
		// (get) Token: 0x060073AE RID: 29614 RVA: 0x001D600E File Offset: 0x001D420E
		// (set) Token: 0x060073AF RID: 29615 RVA: 0x001D6016 File Offset: 0x001D4216
		internal string ExchangeVersion { get; private set; }

		// Token: 0x170023A0 RID: 9120
		// (get) Token: 0x060073B0 RID: 29616 RVA: 0x001D601F File Offset: 0x001D421F
		internal string LocalManifestPath
		{
			get
			{
				return this.LocalTempBase + "UpdateHelp.$$$\\ExchangeHelpInfo.xml";
			}
		}

		// Token: 0x170023A1 RID: 9121
		// (get) Token: 0x060073B1 RID: 29617 RVA: 0x001D6034 File Offset: 0x001D4234
		internal string LocalCabinetPath
		{
			get
			{
				string str = this.LocalTempBase + "UpdateHelp.$$$\\";
				if (string.IsNullOrEmpty(this.localCabinetFilename))
				{
					string str2 = string.Empty;
					do
					{
						str2 = Path.GetRandomFileName() + ".cab";
					}
					while (File.Exists(str + str2));
					this.localCabinetFilename = str2;
				}
				return str + this.localCabinetFilename;
			}
		}

		// Token: 0x170023A2 RID: 9122
		// (get) Token: 0x060073B2 RID: 29618 RVA: 0x001D6096 File Offset: 0x001D4296
		internal string LocalCabinetExtractionTargetPath
		{
			get
			{
				return this.LocalTempBase + "UpdateHelp.$$$\\Files\\";
			}
		}

		// Token: 0x060073B3 RID: 29619 RVA: 0x001D60A8 File Offset: 0x001D42A8
		internal void UpdateProgress(UpdatePhase phase, LocalizedString subTask, int numerator, int denominator)
		{
			UpdatableExchangeHelpProgressEventArgs e = new UpdatableExchangeHelpProgressEventArgs(phase, subTask, numerator, denominator);
			this.Cmdlet.HandleProgressChanged(null, e);
		}

		// Token: 0x060073B4 RID: 29620 RVA: 0x001D60D0 File Offset: 0x001D42D0
		internal void LoadConfiguration()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup"))
			{
				this.ModuleBase = null;
				if (registryKey != null)
				{
					string text = (string)registryKey.GetValue("MsiInstallpath", string.Empty);
					if (string.IsNullOrEmpty(text))
					{
						throw new UpdatableExchangeHelpSystemException(UpdatableHelpStrings.UpdateInstallationNotFoundErrorID, UpdatableHelpStrings.UpdateInstallationNotFound, ErrorCategory.MetadataError, null, null);
					}
					this.ModuleBase = text;
					if (!this.ModuleBase.EndsWith("\\"))
					{
						this.ModuleBase += "\\";
					}
					this.ModuleBase = text + "bin\\";
					this.Cmdlet.WriteVerbose(UpdatableHelpStrings.UpdateInstallationFound(this.ModuleBase));
					int num = (int)registryKey.GetValue("MsiProductMajor");
					int num2 = (int)registryKey.GetValue("MsiProductMinor");
					int num3 = (int)registryKey.GetValue("MsiBuildMajor");
					int num4 = (int)registryKey.GetValue("MsiBuildMinor");
					this.ExchangeVersion = string.Format("{0}.{1}.{2}.{3}", new object[]
					{
						num,
						num2,
						num3,
						num4
					});
					this.CurrentHelpVersion = new UpdatableHelpVersion(this.ExchangeVersion);
					this.CurrentHelpRevision = 0;
				}
				this.LocalTempBase = this.ModuleBase;
			}
			this.PowerShellPsmamlSchemaFilePath = null;
			RegistryKey registryKey2 = Registry.LocalMachine.OpenSubKey(string.Format("SOFTWARE\\Microsoft\\PowerShell\\{0}\\PowerShellEngine", 3));
			if (registryKey2 == null)
			{
				registryKey2 = Registry.LocalMachine.OpenSubKey(string.Format("SOFTWARE\\Microsoft\\PowerShell\\{0}\\PowerShellEngine", 1));
			}
			if (registryKey2 != null)
			{
				try
				{
					string text2 = registryKey2.GetValue("ApplicationBase", null).ToString();
					if (!string.IsNullOrEmpty(text2))
					{
						this.PowerShellPsmamlSchemaFilePath = text2 + "\\Schemas\\PSMaml\\maml.xsd";
					}
				}
				finally
				{
					registryKey2.Dispose();
				}
			}
			this.ManifestUrl = string.Empty;
			this.ThrottlingPeriodHours = 24;
			this.LastSuccessfulCheckTimestampValue = new DateTime(1980, 1, 1).ToUniversalTime();
			RegistryKey registryKey3 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\UpdateExchangeHelp");
			if (registryKey3 == null)
			{
				registryKey3 = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\UpdateExchangeHelp");
			}
			if (registryKey3 != null)
			{
				try
				{
					this.ManifestUrl = registryKey3.GetValue("ManifestUrl", "http://go.microsoft.com/fwlink/p/?LinkId=287244").ToString();
					if (string.IsNullOrEmpty(this.ManifestUrl))
					{
						throw new UpdatableExchangeHelpSystemException(UpdatableHelpStrings.UpdateRegkeyNotFoundErrorID, UpdatableHelpStrings.UpdateRegkeyNotFound("SOFTWARE\\Microsoft\\ExchangeServer\\v15", "\\UpdateExchangeHelp", "ManifestUrl"), ErrorCategory.MetadataError, null, null);
					}
					int num5 = (int)registryKey3.GetValue("CurrentHelpRevision", 0);
					if (num5 > 0)
					{
						this.CurrentHelpVersion = new UpdatableHelpVersion(this.ExchangeVersion);
						this.CurrentHelpRevision = num5;
					}
					int throttlingPeriodHours;
					if (int.TryParse(registryKey3.GetValue("ThrottlingPeriodHours", this.ThrottlingPeriodHours).ToString(), out throttlingPeriodHours))
					{
						this.ThrottlingPeriodHours = throttlingPeriodHours;
					}
					DateTime lastSuccessfulCheckTimestampValue;
					if (DateTime.TryParse(registryKey3.GetValue("LastSuccessfulCheckTimestamp", this.LastSuccessfulCheckTimestampValue.ToString()).ToString(), out lastSuccessfulCheckTimestampValue))
					{
						this.LastSuccessfulCheckTimestampValue = lastSuccessfulCheckTimestampValue;
					}
					this.Cmdlet.WriteVerbose(UpdatableHelpStrings.UpdateManifestUrl(this.ManifestUrl));
					this.Cmdlet.WriteVerbose(UpdatableHelpStrings.UpdateCurrentHelpVersion(this.CurrentHelpVersion.NormalizedVersionNumberWithRevision(this.CurrentHelpRevision)));
					return;
				}
				finally
				{
					registryKey3.Close();
					registryKey3.Dispose();
				}
			}
			throw new UpdatableExchangeHelpSystemException(UpdatableHelpStrings.UpdateConfigRegKeyNotFoundErrorID, UpdatableHelpStrings.UpdateConfigRegKeyNotFound("SOFTWARE\\Microsoft\\ExchangeServer\\v15", "\\UpdateExchangeHelp"), ErrorCategory.MetadataError, null, null);
		}

		// Token: 0x060073B5 RID: 29621 RVA: 0x001D64B4 File Offset: 0x001D46B4
		internal UpdatableExchangeHelpSystemException UpdateHelp()
		{
			double num = 90.0;
			UpdatableExchangeHelpSystemException result = null;
			this.ProgressNumerator = 0.0;
			if (!this.Cmdlet.Force)
			{
				if (!this.DownloadThrottleExpired())
				{
					this.Cmdlet.WriteVerbose(UpdatableHelpStrings.UpdateUseForceToUpdateHelp(this.ThrottlingPeriodHours));
					return result;
				}
			}
			try
			{
				this.UpdateProgress(UpdatePhase.Checking, LocalizedString.Empty, (int)this.ProgressNumerator, 100);
				string path = this.LocalTempBase + "UpdateHelp.$$$\\";
				this.CleanDirectory(path);
				this.EnsureDirectory(path);
				HelpDownloader helpDownloader = new HelpDownloader(this);
				helpDownloader.DownloadManifest();
				if (!this.Cmdlet.Abort)
				{
					UpdatableHelpVersionRange updatableHelpVersionRange = helpDownloader.SearchManifestForApplicableUpdates(this.CurrentHelpVersion, this.CurrentHelpRevision);
					if (updatableHelpVersionRange != null)
					{
						double num2 = 20.0;
						this.ProgressNumerator = 10.0;
						this.UpdateProgress(UpdatePhase.Downloading, LocalizedString.Empty, (int)this.ProgressNumerator, 100);
						string[] array = this.EnumerateAffectedCultures(updatableHelpVersionRange.CulturesAffected);
						if (array.Length > 0)
						{
							this.Cmdlet.WriteVerbose(UpdatableHelpStrings.UpdateApplyingRevision(updatableHelpVersionRange.HelpRevision, string.Join(", ", array)));
							helpDownloader.DownloadPackage(updatableHelpVersionRange.CabinetUrl);
							if (this.Cmdlet.Abort)
							{
								return result;
							}
							this.ProgressNumerator += num2;
							this.UpdateProgress(UpdatePhase.Extracting, LocalizedString.Empty, (int)this.ProgressNumerator, 100);
							HelpInstaller helpInstaller = new HelpInstaller(this, array, num);
							helpInstaller.ExtractToTemp();
							if (this.Cmdlet.Abort)
							{
								return result;
							}
							this.ProgressNumerator += num2;
							this.UpdateProgress(UpdatePhase.Validating, LocalizedString.Empty, (int)this.ProgressNumerator, 100);
							Dictionary<string, LocalizedString> dictionary = helpInstaller.ValidateFiles();
							if (this.Cmdlet.Abort)
							{
								return result;
							}
							if (dictionary != null && dictionary.Count > 0)
							{
								this.Cmdlet.WriteVerbose(UpdatableHelpStrings.UpdateInvalidHelpFiles);
								foreach (KeyValuePair<string, LocalizedString> keyValuePair in dictionary)
								{
									this.Cmdlet.WriteVerbose(UpdatableHelpStrings.UpdateInvalidFileDescription(keyValuePair.Key, keyValuePair.Value));
								}
								throw new UpdatableExchangeHelpSystemException(UpdatableHelpStrings.UpdateContentXmlValidationFailureErrorID, UpdatableHelpStrings.UpdateContentXmlValidationFailure, ErrorCategory.NotInstalled, null, null);
							}
							this.ProgressNumerator += num2;
							this.UpdateProgress(UpdatePhase.Installing, LocalizedString.Empty, (int)this.ProgressNumerator, 100);
							if (!helpInstaller.AtomicInstallFiles())
							{
								throw new UpdatableExchangeHelpSystemException(UpdatableHelpStrings.UpdateInstallFilesExceptionErrorID, UpdatableHelpStrings.UpdateInstallFilesException, ErrorCategory.NotInstalled, null, null);
							}
						}
						else
						{
							this.Cmdlet.WriteVerbose(UpdatableHelpStrings.UpdateSkipRevision(updatableHelpVersionRange.HelpRevision));
						}
						this.UpdateCurrentVersionInRegistry(updatableHelpVersionRange.HelpRevision);
						this.Cmdlet.WriteVerbose(UpdatableHelpStrings.UpdateRevisionApplied(updatableHelpVersionRange.HelpRevision));
						this.ProgressNumerator += num2;
					}
					else
					{
						this.Cmdlet.WriteVerbose(UpdatableHelpStrings.UpdateNoApplicableUpdates);
					}
					this.ProgressNumerator = num;
					this.UpdateProgress(UpdatePhase.Finalizing, LocalizedString.Empty, (int)this.ProgressNumerator, 100);
					try
					{
						this.CleanDirectory(path);
						if (Directory.Exists(path))
						{
							Directory.Delete(path);
						}
					}
					catch
					{
					}
					this.UpdateLastSuccessfulCheckTimestamp(DateTime.UtcNow);
				}
			}
			catch (Exception ex)
			{
				if (ex.GetType() == typeof(UpdatableExchangeHelpSystemException))
				{
					result = (UpdatableExchangeHelpSystemException)ex;
				}
				else
				{
					result = new UpdatableExchangeHelpSystemException(UpdatableHelpStrings.UpdateInstallFilesExceptionErrorID, UpdatableHelpStrings.UpdateInstallFilesException, ErrorCategory.InvalidOperation, null, ex);
				}
			}
			this.ProgressNumerator = 100.0;
			this.UpdateProgress(UpdatePhase.Finalizing, LocalizedString.Empty, (int)this.ProgressNumerator, 100);
			return result;
		}

		// Token: 0x060073B6 RID: 29622 RVA: 0x001D68A8 File Offset: 0x001D4AA8
		internal void EnsureDirectory(string path)
		{
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
		}

		// Token: 0x060073B7 RID: 29623 RVA: 0x001D68B9 File Offset: 0x001D4AB9
		internal void CleanDirectory(string path)
		{
			if (Directory.Exists(path))
			{
				this.RecursiveDescent(0, path, string.Empty, null, true, null);
			}
		}

		// Token: 0x060073B8 RID: 29624 RVA: 0x001D68D4 File Offset: 0x001D4AD4
		internal void RecursiveDescent(int recursionLevel, string path, string relativePath, string[] topLevelFilter, bool delete, Dictionary<string, List<string>> files)
		{
			if (recursionLevel >= 10)
			{
				throw new UpdatableExchangeHelpSystemException(UpdatableHelpStrings.UpdateTooManySubdirectoryLevelsErrorID, UpdatableHelpStrings.UpdateTooManySubdirectoryLevels, ErrorCategory.InvalidData, null, null);
			}
			string text = (path + relativePath).ToLower();
			string[] directories = Directory.GetDirectories(text);
			foreach (string path2 in directories)
			{
				string fileName = Path.GetFileName(path2);
				if (recursionLevel != 0 || topLevelFilter == null || topLevelFilter.Length <= 0 || topLevelFilter.Contains(fileName.ToLower()))
				{
					this.RecursiveDescent(recursionLevel + 1, path, relativePath + fileName + "\\", topLevelFilter, delete, files);
				}
			}
			string[] files2 = Directory.GetFiles(text);
			foreach (string path3 in files2)
			{
				string fileName2 = Path.GetFileName(path3);
				if (files != null)
				{
					if (!files.ContainsKey(relativePath))
					{
						files.Add(relativePath, new List<string>());
					}
					files[relativePath].Add(fileName2.ToLower());
				}
				if (delete)
				{
					string path4 = text + fileName2;
					FileAttributes fileAttributes = File.GetAttributes(path4);
					if (fileAttributes.HasFlag(FileAttributes.ReadOnly))
					{
						fileAttributes &= ~FileAttributes.ReadOnly;
						File.SetAttributes(path4, fileAttributes);
					}
					File.Delete(path4);
				}
			}
			if (delete && recursionLevel > 0)
			{
				Directory.Delete(text);
			}
		}

		// Token: 0x060073B9 RID: 29625 RVA: 0x001D6A20 File Offset: 0x001D4C20
		internal bool DownloadThrottleExpired()
		{
			return DateTime.UtcNow > this.LastSuccessfulCheckTimestampValue.AddHours((double)this.ThrottlingPeriodHours);
		}

		// Token: 0x060073BA RID: 29626 RVA: 0x001D6A4C File Offset: 0x001D4C4C
		internal string[] EnumerateAffectedCultures(string[] culturesUpdated)
		{
			List<string> list = new List<string>();
			foreach (string text in culturesUpdated)
			{
				if (Directory.Exists(this.ModuleBase + text))
				{
					list.Add(text);
				}
			}
			return list.ToArray();
		}

		// Token: 0x060073BB RID: 29627 RVA: 0x001D6A94 File Offset: 0x001D4C94
		internal void UpdateCurrentVersionInRegistry(int newRevision)
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\UpdateExchangeHelp", true))
			{
				registryKey.SetValue("CurrentHelpRevision", newRevision, RegistryValueKind.DWord);
			}
		}

		// Token: 0x060073BC RID: 29628 RVA: 0x001D6AE0 File Offset: 0x001D4CE0
		internal void UpdateLastSuccessfulCheckTimestamp(DateTime timestampUtc)
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\UpdateExchangeHelp", true))
			{
				registryKey.SetValue("LastSuccessfulCheckTimestamp", timestampUtc.ToString(), RegistryValueKind.String);
			}
		}

		// Token: 0x04003AB1 RID: 15025
		private const int ExpectedPowerShellMajorVersion = 3;

		// Token: 0x04003AB2 RID: 15026
		private const int MinimumPowerShellMajorVersion = 1;

		// Token: 0x04003AB3 RID: 15027
		private const int MaxRecursionLevel = 10;

		// Token: 0x04003AB4 RID: 15028
		private const string ManifestName = "ExchangeHelpInfo.xml";

		// Token: 0x04003AB5 RID: 15029
		private const string ConfigPowerShellRegistryKey = "SOFTWARE\\Microsoft\\PowerShell\\{0}\\PowerShellEngine";

		// Token: 0x04003AB6 RID: 15030
		private const string ConfigPowerShellApplicationBaseValueName = "ApplicationBase";

		// Token: 0x04003AB7 RID: 15031
		private const string ConfigRegistryKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15";

		// Token: 0x04003AB8 RID: 15032
		private const string ConfigSetupRegistrySubkey = "\\Setup";

		// Token: 0x04003AB9 RID: 15033
		private const string ConfigUpdaterRegistrySubkey = "\\UpdateExchangeHelp";

		// Token: 0x04003ABA RID: 15034
		private const string ConfigMsiInstallPathValueName = "MsiInstallpath";

		// Token: 0x04003ABB RID: 15035
		private const string ConfigMsiBuildMajorVersionKey = "MsiBuildMajor";

		// Token: 0x04003ABC RID: 15036
		private const string ConfigMsiBuildMinorVersionKey = "MsiBuildMinor";

		// Token: 0x04003ABD RID: 15037
		private const string ConfigMsiProductMajorVersionKey = "MsiProductMajor";

		// Token: 0x04003ABE RID: 15038
		private const string ConfigMsiProductMinorVersionKey = "MsiProductMinor";

		// Token: 0x04003ABF RID: 15039
		private const string ConfigManifestUrlValueName = "ManifestUrl";

		// Token: 0x04003AC0 RID: 15040
		private const string DefaultManifestLink = "http://go.microsoft.com/fwlink/p/?LinkId=287244";

		// Token: 0x04003AC1 RID: 15041
		private const string ConfigCurrentHelpRevisionValueName = "CurrentHelpRevision";

		// Token: 0x04003AC2 RID: 15042
		private const string ConfigThrottlingPeriodHoursValueName = "ThrottlingPeriodHours";

		// Token: 0x04003AC3 RID: 15043
		private const string ConfigLastSuccessfulCheckTimestampValueName = "LastSuccessfulCheckTimestamp";

		// Token: 0x04003AC4 RID: 15044
		private const string LocalTempDirectoryName = "UpdateHelp.$$$\\";

		// Token: 0x04003AC5 RID: 15045
		private const string ExtractionDirectoryName = "Files\\";

		// Token: 0x04003AC6 RID: 15046
		private const string CabinetExtension = ".cab";

		// Token: 0x04003AC7 RID: 15047
		private string localCabinetFilename;
	}
}
