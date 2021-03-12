using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Forefront.Hygiene.Rus.EngineUpdateCommon;
using Microsoft.Win32;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000017 RID: 23
	public class RusEngine
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x000057C9 File Offset: 0x000039C9
		public RusEngine(string engineName, string platform)
		{
			this.EngineName = engineName;
			this.Platform = platform;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x000057DF File Offset: 0x000039DF
		public static string RusEngineDownloadUrl
		{
			get
			{
				return RusEngine.GetExchangeLabsRegKeyValue("RusEngineDownloadUrl");
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x000057EB File Offset: 0x000039EB
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x000057F3 File Offset: 0x000039F3
		public string EngineName { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x000057FC File Offset: 0x000039FC
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00005804 File Offset: 0x00003A04
		public string Platform { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00005810 File Offset: 0x00003A10
		public string ForefrontdlManifestCabUrl
		{
			get
			{
				return Path.Combine(new string[]
				{
					RusEngine.RusEngineDownloadUrl,
					this.Platform,
					this.EngineName,
					"Package",
					this.GetLatestPackageVersionFromForeFrontdl(),
					"Manifest.cab"
				}).Replace("\\", "/");
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x0000586C File Offset: 0x00003A6C
		public string ManifestTempCabFolder
		{
			get
			{
				return Path.Combine(Path.GetTempPath(), "EngineManifestFromForefrontdl", this.Platform, this.EngineName);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000BA RID: 186 RVA: 0x0000588C File Offset: 0x00003A8C
		public DateTime ForefrontdlManifestCreatedTimeInUtc
		{
			get
			{
				string manifestCabFilePath = RusEngine.DownLoadManifestFile(this.ForefrontdlManifestCabUrl, this.ManifestTempCabFolder, "Manifest.cab");
				string text = RusEngine.ExtractManifestCabFileToXml(manifestCabFilePath, "Manifest.xml");
				ManifestFile manifestFile = ManifestManager.OpenManifest(text);
				DateTime dateTime = DateTime.Parse(manifestFile.created);
				TimeZoneInfo sourceTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
				return TimeZoneInfo.ConvertTimeToUtc(dateTime, sourceTimeZone);
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000058E8 File Offset: 0x00003AE8
		public static bool DownloadFile(string remoteFileURL, string localFile)
		{
			IFileDownloader fileDownloader = FileDownloaderFactory.CreateFileDownloader(0);
			bool result;
			try
			{
				result = (fileDownloader.DownloadFile(remoteFileURL, localFile) == 1);
			}
			catch (EngineDownloadException arg)
			{
				throw new ApplicationException(string.Format("Failed to download file from remote url. DownloadUrl: [{0}] LocalFilePath: [{1}]. Exception: {2}", remoteFileURL, localFile, arg));
			}
			return result;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00005930 File Offset: 0x00003B30
		public static string DownLoadManifestFile(string remoteManifestUrl, string localManifestFolder, string manifestFileName)
		{
			string text = Path.Combine(localManifestFolder, manifestFileName);
			if (File.Exists(text))
			{
				File.Delete(text);
			}
			if (!Directory.Exists(localManifestFolder))
			{
				Directory.CreateDirectory(localManifestFolder);
			}
			if (!RusEngine.DownloadFile(remoteManifestUrl, text))
			{
				throw new ApplicationException(string.Format("Manifest file download from remote url failed. DownloadUrl: [{0}] DownloadFolderPath: [{1}] FilePath: [{2}]", remoteManifestUrl, localManifestFolder, text));
			}
			return text;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00005984 File Offset: 0x00003B84
		public static string ExtractManifestCabFileToXml(string manifestCabFilePath, string xmlFileName)
		{
			string directoryName = Path.GetDirectoryName(manifestCabFilePath);
			string text = Path.Combine(directoryName, xmlFileName);
			if (File.Exists(text))
			{
				File.Delete(text);
			}
			if (new ExtractCab(directoryName).ExtractFiles(manifestCabFilePath) != 0)
			{
				throw new ApplicationException(string.Format("File extraction failed. Path: {0}", manifestCabFilePath));
			}
			return text;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000059D0 File Offset: 0x00003BD0
		public static string GetExchangeLabsRegKeyValue(string regStringName)
		{
			string result = null;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeLabs"))
				{
					if (registryKey == null)
					{
						throw new ApplicationException(string.Format("HKLM ExchangeLabs registry key [{0}] is found to be null", "SOFTWARE\\Microsoft\\ExchangeLabs"));
					}
					if (registryKey.GetValue(regStringName) == null)
					{
						throw new ApplicationException(string.Format("RegistryKey.GetValue() returned null for the reg string [{0}]", regStringName));
					}
					result = (registryKey.GetValue(regStringName) as string);
				}
			}
			catch (Exception ex)
			{
				throw new ApplicationException(string.Format("An error occured while loading registry key value [{0}]. Exception: {1}", regStringName, ex.ToString()));
			}
			return result;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00005A74 File Offset: 0x00003C74
		public DateTime GetEngineFilesDownloadedTimeFromQds(string rusPrimaryFileShareRootPath, string rusAlternateFileShareRootPath, bool isLatest = true)
		{
			Dictionary<string, DateTime> dictionary = null;
			Dictionary<string, DateTime> dictionary2 = null;
			string text = string.Empty;
			string text2 = string.Empty;
			DateTime result;
			try
			{
				if (!string.IsNullOrEmpty(rusPrimaryFileShareRootPath))
				{
					text = Path.Combine(new string[]
					{
						rusPrimaryFileShareRootPath,
						"EngineFileShare",
						this.Platform,
						this.EngineName,
						"DownloadedTime.txt"
					});
					dictionary = this.GetDownloadedDates(text);
				}
				if (!string.IsNullOrEmpty(rusAlternateFileShareRootPath))
				{
					text2 = Path.Combine(new string[]
					{
						rusAlternateFileShareRootPath,
						"EngineFileShare",
						this.Platform,
						this.EngineName,
						"DownloadedTime.txt"
					});
					dictionary2 = this.GetDownloadedDates(text2);
				}
				if (dictionary == null && dictionary2 == null)
				{
					throw new ApplicationException(string.Format("Both Primary and Alternate QDS share engine files DownloadedTime files are not reachable. Primary share path: {0}. Alternate share path: {1}.", text, text2));
				}
				string key = isLatest ? "LatestDownloadedTime" : "PreviousDownloadedTime";
				if (dictionary2 == null)
				{
					result = dictionary[key];
				}
				else if (dictionary == null)
				{
					result = dictionary2[key];
				}
				else
				{
					result = ((dictionary[key] > dictionary2[key]) ? dictionary[key] : dictionary2[key]);
				}
			}
			catch (KeyNotFoundException ex)
			{
				string message = string.Format("Expected {0} or {1} strings not found in {2} file. Error: {3}", new object[]
				{
					"LatestDownloadedTime",
					"PreviousDownloadedTime",
					"DownloadedTime.txt",
					ex.ToString()
				});
				throw new ApplicationException(message);
			}
			return result;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00005C04 File Offset: 0x00003E04
		private Dictionary<string, DateTime> GetDownloadedDates(string qdsShareEngineFilesDownloadedTextFilePath)
		{
			if (!File.Exists(qdsShareEngineFilesDownloadedTextFilePath))
			{
				return null;
			}
			Dictionary<string, DateTime> dictionary = new Dictionary<string, DateTime>(StringComparer.OrdinalIgnoreCase);
			string text = IniFileHelper.IniReadValue(qdsShareEngineFilesDownloadedTextFilePath, "SignaturePollingDownloadTimes", "LatestDownloadedTime");
			DateTime value;
			if (!DateTime.TryParse(text, out value))
			{
				throw new ApplicationException(string.Format("Failed to convert [{0}: {1}] string value to dateTime", "LatestDownloadedTime", text));
			}
			dictionary.Add("LatestDownloadedTime", value);
			string text2 = IniFileHelper.IniReadValue(qdsShareEngineFilesDownloadedTextFilePath, "SignaturePollingDownloadTimes", "PreviousDownloadedTime");
			DateTime value2;
			if (DateTime.TryParse(text2, out value2))
			{
				dictionary.Add("PreviousDownloadedTime", value2);
				return dictionary;
			}
			throw new ApplicationException(string.Format("Failed to convert [{0}: {1}] string value to dateTime", "PreviousDownloadedTime", text2));
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005CA8 File Offset: 0x00003EA8
		private string GetLatestPackageVersionFromForeFrontdl()
		{
			string text = string.Empty;
			string text2 = string.Empty;
			string text3 = Path.Combine(Path.GetTempPath(), "UMPackageFromForefrontdl", this.EngineName);
			try
			{
				text2 = RusPublishingPipelineBase.ForeFrontdlUniversalManifestCabUrl;
				string manifestCabFilePath = RusEngine.DownLoadManifestFile(text2, text3, "UniversalManifest.cab");
				string text4 = RusEngine.ExtractManifestCabFileToXml(manifestCabFilePath, "UniversalManifest.xml");
				UniversalManifest universalManifest = UniversalManifestManager.OpenManifest(text4);
				EnginePackagingInfoEngineName engineName;
				if (!Enum.TryParse<EnginePackagingInfoEngineName>(this.EngineName, true, out engineName))
				{
					throw new ArgumentException("Engine Name is invalid.");
				}
				EnginePackagingInfoPlatform platform;
				if (!Enum.TryParse<EnginePackagingInfoPlatform>(this.Platform, true, out platform))
				{
					throw new ArgumentException("Platform is invalid.");
				}
				EnginePackagingInfo enginePackagingInfo = new EnginePackagingInfo
				{
					category = "Antivirus",
					engineName = engineName,
					platform = platform
				};
				text = UniversalManifestManager.GetEngineInfo(universalManifest, enginePackagingInfo).Package[0].version;
			}
			catch (Exception ex)
			{
				throw new ApplicationException(string.Format("An error occured while fetching package version from UM. Url: {0}, LocalFolder: {1}. Error: {2}", text2, text3, ex.ToString()));
			}
			if (string.IsNullOrWhiteSpace(text))
			{
				throw new ApplicationException("Package version cannot be null or empty");
			}
			return text;
		}

		// Token: 0x0400007B RID: 123
		private const string TempEngineManifestFolderName = "EngineManifestFromForefrontdl";

		// Token: 0x0400007C RID: 124
		private const string EngineFilesFolderName = "EngineFileShare";

		// Token: 0x0400007D RID: 125
		private const string PackageFolderName = "Package";

		// Token: 0x0400007E RID: 126
		private const string EngineFilesDownloadedFileName = "DownloadedTime.txt";

		// Token: 0x0400007F RID: 127
		private const string LatestDownloadedTimeKey = "LatestDownloadedTime";

		// Token: 0x04000080 RID: 128
		private const string PreviousDownloadedTimeKey = "PreviousDownloadedTime";

		// Token: 0x04000081 RID: 129
		private const string DownloadedTimeTextFileHeader = "SignaturePollingDownloadTimes";

		// Token: 0x04000082 RID: 130
		private const string UMPackageVersionFolderFromForefrontdl = "UMPackageFromForefrontdl";

		// Token: 0x04000083 RID: 131
		private const string AntivirusNodeInUMXml = "Antivirus";

		// Token: 0x04000084 RID: 132
		private const string ExchangeLabsRegKey = "SOFTWARE\\Microsoft\\ExchangeLabs";

		// Token: 0x04000085 RID: 133
		private const string RusEngineDownloadUrlRegistryKeyName = "RusEngineDownloadUrl";
	}
}
