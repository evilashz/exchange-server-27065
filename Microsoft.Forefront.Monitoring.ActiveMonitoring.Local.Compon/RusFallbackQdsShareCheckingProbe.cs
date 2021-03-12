using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Threading;
using Microsoft.Exchange.Hygiene.Data.Rus;
using Microsoft.Forefront.Hygiene.Rus.EngineUpdateCommon;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000019 RID: 25
	public class RusFallbackQdsShareCheckingProbe : RusPublishingPipelineBase
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00006617 File Offset: 0x00004817
		// (set) Token: 0x060000CA RID: 202 RVA: 0x0000661F File Offset: 0x0000481F
		private TimeSpan AllowedDownloadDelayBetweenAkamaiAndQDSShare { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00006628 File Offset: 0x00004828
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00006630 File Offset: 0x00004830
		private string Phase1PrimaryPublishedEnginesPath { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00006639 File Offset: 0x00004839
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00006641 File Offset: 0x00004841
		private string Phase1AlternatePublishedEnginesPath { get; set; }

		// Token: 0x060000CF RID: 207 RVA: 0x0000664C File Offset: 0x0000484C
		protected override void DoWork(CancellationToken cancellationToken)
		{
			base.TraceDebug(string.Format("{0} (Utc Time): {1} started.", DateTime.UtcNow.ToString(), base.Definition.Name));
			if (base.GetRegionTag().IndexOf("SIP", StringComparison.OrdinalIgnoreCase) >= 0)
			{
				base.TraceDebug("Skipping execution of RusFallbackQdsShareCheckingProbe on SIP environment.");
				return;
			}
			this.Phase1PrimaryPublishedEnginesPath = Path.Combine(base.RusPrimaryFileShareRootPath, "PublishedEngines");
			this.Phase1AlternatePublishedEnginesPath = Path.Combine(base.RusAlternateFileShareRootPath, "PublishedEngines");
			bool flag = this.IsFallBackQdsSharePublishingEnabled();
			base.TraceDebug(string.Format("[IsFallBackQdsSharePublishingEnabled: {0}]", flag));
			if (flag)
			{
				bool flag2 = Convert.ToBoolean(base.GetExtensionAttributeStringFromXml(base.Definition.ExtensionAttributes, "//RusFallbackQdsShareCheckingProbeParam", "AlertOnQdsShareNotAccessible", true));
				bool flag3 = !Directory.Exists(this.Phase1PrimaryPublishedEnginesPath);
				bool flag4 = !Directory.Exists(this.Phase1AlternatePublishedEnginesPath);
				if (flag2)
				{
					if (flag3 || flag4)
					{
						string errorMsg = string.Format("One or more RUS Qds shares are not accessible. Primary share: {0} not accessible: {1}, Alternate share: {2} not accessible: {3}.", new object[]
						{
							this.Phase1PrimaryPublishedEnginesPath,
							flag3,
							this.Phase1AlternatePublishedEnginesPath,
							flag4
						});
						base.LogTraceErrorAndThrowApplicationException(errorMsg);
						return;
					}
					base.TraceDebug(string.Format("{0} (Utc Time): Both Primary {1} and Alternate {2} shares exist. Successfully completed the {3} probe.", new object[]
					{
						DateTime.UtcNow.ToString(),
						flag3,
						flag4,
						base.Definition.Name
					}));
					return;
				}
				else
				{
					this.AllowedDownloadDelayBetweenAkamaiAndQDSShare = base.GetTimeSpanExtensionAttributeFromXml(base.Definition.ExtensionAttributes, "//RusFallbackQdsShareCheckingProbeParam", "AllowedDownloadDelayTimeSpan", RusFallbackQdsShareCheckingProbe.defaultAllowedDownloadDelay, RusFallbackQdsShareCheckingProbe.minimumAllowedDownloadDelay, RusFallbackQdsShareCheckingProbe.maximumAllowedDownloadDelay);
					base.TraceDebug(string.Format("[AllowedDownloadDelayBetweenAkamaiAndQDSShare value from probe xml: {0}]", this.AllowedDownloadDelayBetweenAkamaiAndQDSShare));
					string[] engines = base.GetExtensionAttributeStringFromXml(base.Definition.ExtensionAttributes, "//RusFallbackQdsShareCheckingProbeParam", "Engines", true).Split(new char[]
					{
						','
					});
					base.Platforms = base.GetExtensionAttributeStringFromXml(base.Definition.ExtensionAttributes, "//RusFallbackQdsShareCheckingProbeParam", "Platforms", true).Split(new char[]
					{
						','
					});
					string text = this.DownloadUMCabFileFromAkamaiToTemp();
					DateTime postedDateTimeInUtcFromUMCabFile = this.GetPostedDateTimeInUtcFromUMCabFile(text);
					DateTime dateTime = default(DateTime);
					DateTime dateTime2 = default(DateTime);
					base.TraceDebug(string.Format("[ExpectedPostedDateTimeInUtcFromForefrontdl: {0}]", postedDateTimeInUtcFromUMCabFile));
					bool flag5 = false;
					bool flag6 = false;
					string empty = string.Empty;
					string text2 = this.CopyQdsShareUMCabFileToLocal(this.Phase1PrimaryPublishedEnginesPath, out empty, true);
					if (!string.IsNullOrWhiteSpace(text2))
					{
						dateTime = this.GetPostedDateTimeInUtcFromUMCabFile(text2);
						base.TraceDebug(string.Format("[ActualPrimaryQDSSharePostedDateTimeInUtc: {0}]", dateTime));
						bool flag7 = base.AreManifestFilesOutOfSync(postedDateTimeInUtcFromUMCabFile, dateTime, this.AllowedDownloadDelayBetweenAkamaiAndQDSShare);
						base.TraceDebug(string.Format("[isPrimaryShareUMOutOfSync: {0}]", flag7));
						if (flag7)
						{
							flag5 = this.IsAnyEnginePackageVersionOutOfSync(text, text2, engines, base.Platforms);
						}
						base.TraceDebug(string.Format("[IsPrimaryShareOutOfSync: {0}]", flag5));
					}
					else
					{
						base.TraceDebug(string.Format("[primaryUmCabError: {0}]", empty));
						flag5 = true;
					}
					string empty2 = string.Empty;
					string text3 = this.CopyQdsShareUMCabFileToLocal(this.Phase1AlternatePublishedEnginesPath, out empty2, false);
					if (!string.IsNullOrWhiteSpace(text3))
					{
						dateTime2 = this.GetPostedDateTimeInUtcFromUMCabFile(text3);
						base.TraceDebug(string.Format("[ActualAlternateQDSSharePostedDateTimeInUtc: {0}]", dateTime2));
						bool flag8 = base.AreManifestFilesOutOfSync(postedDateTimeInUtcFromUMCabFile, dateTime2, this.AllowedDownloadDelayBetweenAkamaiAndQDSShare);
						base.TraceDebug(string.Format("[isAlternateShareUMOutOfSync: {0}]", flag8));
						if (flag8)
						{
							flag6 = this.IsAnyEnginePackageVersionOutOfSync(text, text3, engines, base.Platforms);
						}
						base.TraceDebug(string.Format("[IsAlternateShareOutOfSync: {0}]", flag6));
					}
					else
					{
						base.TraceDebug(string.Format("[alternateUmCabError: {0}]", empty2));
						flag6 = true;
					}
					if (flag5 && flag6)
					{
						string text4 = string.Empty;
						if (flag3 && flag4)
						{
							text4 = string.Format("Both Phase1PrimaryPublishedEnginesPath {0} and Phase1AlternatePublishedEnginesPath {1} don't exist.", this.Phase1PrimaryPublishedEnginesPath, this.Phase1AlternatePublishedEnginesPath);
						}
						else if (string.IsNullOrWhiteSpace(text2) && string.IsNullOrWhiteSpace(text3))
						{
							text4 = string.Format("The UniversalManifest cab in Phase1PrimaryPublishedEnginesPath {0} and Phase1AlternatePublishedEnginesPath {1} doesn't exist. Errors: \r\n{2} \r\n{3}.", new object[]
							{
								this.Phase1PrimaryPublishedEnginesPath,
								this.Phase1AlternatePublishedEnginesPath,
								empty,
								empty2
							});
						}
						else if (string.IsNullOrWhiteSpace(text2) || string.IsNullOrWhiteSpace(text3))
						{
							string arg = string.IsNullOrWhiteSpace(text2) ? "Primary" : "Alternate";
							string text5 = string.IsNullOrWhiteSpace(text2) ? "Alternate" : "Primary";
							text4 = string.Format("Phase1{0}PublishedEnginesPath doesn't exist. ErrorInformation: {1}.", arg, string.IsNullOrWhiteSpace(text2) ? empty : empty2);
							DateTime dateTime3 = string.IsNullOrWhiteSpace(text2) ? dateTime2 : dateTime;
							text4 += string.Format("And the UnviersalManifest.xml file in {0} QDS share is older than Forefrontdl file by more than [{1}] minutes.\r\n                            Akamai UM file postedDate: {2}. {3} Qds share UM postedDate: {4}.", new object[]
							{
								text5,
								this.AllowedDownloadDelayBetweenAkamaiAndQDSShare.TotalMinutes,
								postedDateTimeInUtcFromUMCabFile,
								text5,
								dateTime3
							});
						}
						else
						{
							text4 = string.Format("UnviersalManifest.xml file in both Primary and Alternate QDS shares are older than Forefrontdl file by more than [{0}] minutes. \r\n                            Akamai UM file postedDate: {1}. Qds Primary share UM postedDate: {2}. Alternate share UM postedDate: {3}", new object[]
							{
								this.AllowedDownloadDelayBetweenAkamaiAndQDSShare.TotalMinutes,
								postedDateTimeInUtcFromUMCabFile,
								dateTime,
								dateTime2
							});
						}
						Collection<PSObject> rusEngineUpdatePublisherBgdTaskResults = this.GetRusEngineUpdatePublisherBgdTaskResults(360);
						string arg2 = base.FormatBackgroundJobTaskResultsToString(rusEngineUpdatePublisherBgdTaskResults);
						text4 += string.Format("\n Following are Rus EngineUpdatePublisher BGD task results in last {0} minutes: \n {1}", 360, arg2);
						base.LogTraceErrorAndThrowApplicationException(text4);
					}
				}
			}
			base.TraceDebug(string.Format("{0} (Utc Time): {1} finished with success.", DateTime.UtcNow.ToString(), base.Definition.Name));
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00006C44 File Offset: 0x00004E44
		private bool IsFallBackQdsSharePublishingEnabled()
		{
			string exchangeLabsRegKeyValue = RusEngine.GetExchangeLabsRegKeyValue("IsRusPublishingEnabled");
			if (string.IsNullOrEmpty(exchangeLabsRegKeyValue))
			{
				base.LogTraceErrorAndThrowApplicationException(string.Format("Registry string [{0}] value is found to be empty", "IsRusPublishingEnabled"));
			}
			bool result = false;
			if (!bool.TryParse(exchangeLabsRegKeyValue.Trim(), out result))
			{
				base.LogTraceErrorAndThrowApplicationException(string.Format("Registry string [{0}] value is found to be incorrect. Value: [{1}]", "IsRusPublishingEnabled", exchangeLabsRegKeyValue));
			}
			return result;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00006CA4 File Offset: 0x00004EA4
		private string DownloadUMCabFileFromAkamaiToTemp()
		{
			string text = Path.Combine(Path.GetTempPath(), "UMFromForefrontdl");
			string foreFrontdlUniversalManifestCabUrl = RusPublishingPipelineBase.ForeFrontdlUniversalManifestCabUrl;
			base.TraceDebug(string.Format("[LocalForefrontdlUMCabFolder: {0}, ForeFrontdl UniversalManifest Cab Url: {1}]", text, foreFrontdlUniversalManifestCabUrl));
			return RusEngine.DownLoadManifestFile(foreFrontdlUniversalManifestCabUrl, text, "UniversalManifest.cab");
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00006CE8 File Offset: 0x00004EE8
		private DateTime GetPostedDateTimeInUtcFromUMCabFile(string umCabFilePath)
		{
			UniversalManifest universalManifest = null;
			try
			{
				string text = RusEngine.ExtractManifestCabFileToXml(umCabFilePath, "UniversalManifest.xml");
				universalManifest = UniversalManifestManager.OpenManifest(text);
			}
			catch (Exception arg)
			{
				base.LogTraceErrorAndThrowApplicationException(string.Format("An error occured while extracting and fetching UM posted date from UniversalManifest.Cab file. FilePath: {0}, Exception: {1}", umCabFilePath, arg));
			}
			return universalManifest.postdate;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00006D58 File Offset: 0x00004F58
		private bool IsAnyEnginePackageVersionOutOfSync(string foreFrontdlCabFile, string qdsShareCabFile, string[] engines, string[] platforms)
		{
			bool result = false;
			try
			{
				string text = RusEngine.ExtractManifestCabFileToXml(foreFrontdlCabFile, "UniversalManifest.xml");
				UniversalManifest universalManifest = UniversalManifestManager.OpenManifest(text);
				string text2 = RusEngine.ExtractManifestCabFileToXml(qdsShareCabFile, "UniversalManifest.xml");
				UniversalManifest universalManifest2 = UniversalManifestManager.OpenManifest(text2);
				foreach (string text3 in platforms)
				{
					foreach (string text4 in engines)
					{
						try
						{
							if (!text3.Equals("amd64", StringComparison.OrdinalIgnoreCase) || !RusFallbackQdsShareCheckingProbe.x86PlatformOnlyEngines.Contains(text4, StringComparer.OrdinalIgnoreCase))
							{
								string category = RusFallbackQdsShareCheckingProbe.listCategoryEngines.Contains(text4, StringComparer.OrdinalIgnoreCase) ? "List" : (RusFallbackQdsShareCheckingProbe.antispamCategoryEngines.Contains(text4, StringComparer.OrdinalIgnoreCase) ? "Antispam" : "Antivirus");
								EnginePackagingInfo enginePackagingInfo = new EnginePackagingInfo
								{
									category = category,
									engineName = (EnginePackagingInfoEngineName)Enum.Parse(typeof(EnginePackagingInfoEngineName), text4, true),
									platform = (EnginePackagingInfoPlatform)Enum.Parse(typeof(EnginePackagingInfoPlatform), text3, true)
								};
								UniversalManifestPlatformCategoryEngine engineInfo = UniversalManifestManager.GetEngineInfo(universalManifest, enginePackagingInfo);
								string version = engineInfo.Package.First((UniversalManifestPlatformCategoryEnginePackage p) => !string.IsNullOrEmpty(p.id)).version;
								UniversalManifestPlatformCategoryEngine engineInfo2 = UniversalManifestManager.GetEngineInfo(universalManifest2, enginePackagingInfo);
								string version2 = engineInfo2.Package.First((UniversalManifestPlatformCategoryEnginePackage p) => !string.IsNullOrEmpty(p.id)).version;
								if (!string.Equals(version, version2, StringComparison.OrdinalIgnoreCase))
								{
									base.TraceDebug(string.Format("[Platform-Engine:{0}-{1} AkamaiPackageVersion:{2} QdsSharePackageVersion: {3}]", new object[]
									{
										text3,
										text4,
										version,
										version2
									}));
									result = true;
								}
							}
						}
						catch (Exception arg)
						{
							base.LogTraceErrorAndThrowApplicationException(string.Format("Error occured while fetching PackageVersion from UniversalManifest. Platform-Engine:{0}-{1}. Exception: {2}", text3, text4, arg));
						}
					}
				}
			}
			catch (Exception arg2)
			{
				base.LogTraceErrorAndThrowApplicationException(string.Format("Error occured while extracting UM cab files. Forefront UM FilePath: {0}, Qds UM FilePath: {1}, Exception: {2}", foreFrontdlCabFile, qdsShareCabFile, arg2));
			}
			return result;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00006FA8 File Offset: 0x000051A8
		private string CopyQdsShareUMCabFileToLocal(string rusQdsSharePath, out string error, bool isPrimaryQds)
		{
			string arg = isPrimaryQds ? "Primary" : "Alternate";
			error = string.Empty;
			string fallBackQDSShareVersion = this.GetFallBackQDSShareVersion();
			base.TraceDebug(string.Format("[Current Qds version from database: {0}]", fallBackQDSShareVersion));
			string text = Path.Combine(rusQdsSharePath, fallBackQDSShareVersion);
			if (!Directory.Exists(text))
			{
				error = string.Format("Fallback {0} QDSShareVersion folder {1} is not available", arg, text);
				return string.Empty;
			}
			string text2 = Path.Combine(text, "metadata", "UniversalManifest.cab");
			base.TraceDebug(string.Format("[{0} qdsShareUMCabFilePath: {1}]", arg, text2));
			if (!File.Exists(text2))
			{
				error = string.Format("UniversalManifest.cab file is not available under fallback {0} QDS share path {1}", arg, text2);
				return string.Empty;
			}
			string text3 = Path.Combine(Path.GetTempPath(), string.Format("UMFrom{0}QdsShare", arg));
			string text4 = Path.Combine(text3, "UniversalManifest.cab");
			if (File.Exists(text4))
			{
				File.Delete(text4);
			}
			if (!Directory.Exists(text3))
			{
				Directory.CreateDirectory(text3);
			}
			FileInfo fileInfo = new FileInfo(text2);
			fileInfo.CopyTo(text4, true);
			return text4;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000070A4 File Offset: 0x000052A4
		private string GetFallBackQDSShareVersion()
		{
			string text = string.Empty;
			try
			{
				RusConfigSession rusConfigSession = new RusConfigSession();
				RusConfig rusConfig = rusConfigSession.FindRusConfigProperty();
				if (rusConfig == null)
				{
					base.LogTraceErrorAndThrowApplicationException("RusConfig is found to be empty");
				}
				text = rusConfig.UniversalManifestVersion;
				if (string.IsNullOrEmpty(text))
				{
					base.LogTraceErrorAndThrowApplicationException("QDSShareVersion in Directory database is found to be empty");
				}
			}
			catch (Exception ex)
			{
				base.LogTraceErrorAndThrowApplicationException(string.Format("An error occured while fetching QDSShareVersion from directory database. Exception: {0}", ex.ToString()));
			}
			return text;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00007118 File Offset: 0x00005318
		private Collection<PSObject> GetRusEngineUpdatePublisherBgdTaskResults(int startTimeLookbackMinutesFromCurrentUtc)
		{
			string psScript = string.Format("{0} -FileName {1} | {2} -Region {3} | {4} | where {5}$_.StartTime -gt [DateTime]::UtcNow.AddMinutes(-{6}){7}", new object[]
			{
				"Get-BackgroundJobDefinition",
				"Microsoft.Forefront.Hygiene.Rus.EngineUpdatePublisher.exe",
				"Get-BackgroundJobSchedule",
				base.GetRegionTag(),
				"Get-BackgroundJobTask",
				"{",
				startTimeLookbackMinutesFromCurrentUtc,
				"}"
			});
			return base.ExecuteForeFrontManagementShellScript(psScript, false);
		}

		// Token: 0x0400008F RID: 143
		private const string IsRusFallbackPublishingEnabledRegistryKeyName = "IsRusPublishingEnabled";

		// Token: 0x04000090 RID: 144
		private const string ProbeParamXmlNode = "//RusFallbackQdsShareCheckingProbeParam";

		// Token: 0x04000091 RID: 145
		private const string AllowedDownloadDelayTimeSpanXmlAttribute = "AllowedDownloadDelayTimeSpan";

		// Token: 0x04000092 RID: 146
		private const string AlertOnQdsShareNotAccessibleXmlAttribute = "AlertOnQdsShareNotAccessible";

		// Token: 0x04000093 RID: 147
		private const string EnginesXmlAttribute = "Engines";

		// Token: 0x04000094 RID: 148
		private const string RusPhase1PublishedEnginesFolder = "PublishedEngines";

		// Token: 0x04000095 RID: 149
		private const int DefaultBgdTaskLookBackMinutes = 360;

		// Token: 0x04000096 RID: 150
		private static TimeSpan defaultAllowedDownloadDelay = TimeSpan.FromMinutes(90.0);

		// Token: 0x04000097 RID: 151
		private static TimeSpan minimumAllowedDownloadDelay = TimeSpan.FromMinutes(5.0);

		// Token: 0x04000098 RID: 152
		private static TimeSpan maximumAllowedDownloadDelay = TimeSpan.FromDays(2.0);

		// Token: 0x04000099 RID: 153
		private static List<string> x86PlatformOnlyEngines = new List<string>
		{
			"Kaspersky5"
		};

		// Token: 0x0400009A RID: 154
		private static List<string> antispamCategoryEngines = new List<string>
		{
			"Cloudmark"
		};

		// Token: 0x0400009B RID: 155
		private static List<string> listCategoryEngines = new List<string>
		{
			"WormList"
		};
	}
}
