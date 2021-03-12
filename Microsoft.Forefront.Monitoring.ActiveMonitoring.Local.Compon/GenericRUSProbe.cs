using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Forefront.Hygiene.Rus.Client;
using Microsoft.Forefront.Hygiene.Rus.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000206 RID: 518
	public class GenericRUSProbe : ProbeWorkItem
	{
		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06000FCD RID: 4045 RVA: 0x000295C9 File Offset: 0x000277C9
		// (set) Token: 0x06000FCE RID: 4046 RVA: 0x000295D1 File Offset: 0x000277D1
		private ClientId GenericRUSClientId { get; set; }

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x000295DA File Offset: 0x000277DA
		// (set) Token: 0x06000FD0 RID: 4048 RVA: 0x000295E2 File Offset: 0x000277E2
		private bool IsUploader { get; set; }

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x000295EB File Offset: 0x000277EB
		// (set) Token: 0x06000FD2 RID: 4050 RVA: 0x000295F3 File Offset: 0x000277F3
		private int MaxMinorUpdate { get; set; }

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x000295FC File Offset: 0x000277FC
		// (set) Token: 0x06000FD4 RID: 4052 RVA: 0x00029604 File Offset: 0x00027804
		private int ProbabilityPercentCount { get; set; }

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x0002960D File Offset: 0x0002780D
		// (set) Token: 0x06000FD6 RID: 4054 RVA: 0x00029615 File Offset: 0x00027815
		private int MinorUpdateSize { get; set; }

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x0002961E File Offset: 0x0002781E
		// (set) Token: 0x06000FD8 RID: 4056 RVA: 0x00029626 File Offset: 0x00027826
		private int MajorUpdateSize { get; set; }

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x0002962F File Offset: 0x0002782F
		// (set) Token: 0x06000FDA RID: 4058 RVA: 0x00029637 File Offset: 0x00027837
		private bool IsMinorDownloader { get; set; }

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x00029640 File Offset: 0x00027840
		// (set) Token: 0x06000FDC RID: 4060 RVA: 0x00029648 File Offset: 0x00027848
		private int GoBehindCurrentMinor { get; set; }

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x00029651 File Offset: 0x00027851
		// (set) Token: 0x06000FDE RID: 4062 RVA: 0x00029659 File Offset: 0x00027859
		private bool VerifyUpdate { get; set; }

		// Token: 0x06000FDF RID: 4063 RVA: 0x00029662 File Offset: 0x00027862
		protected override void DoWork(CancellationToken cancellationToken)
		{
			this.ReadInputXML();
			if (this.IsUploader)
			{
				if (this.GenerateUpdate(this.ProbabilityPercentCount))
				{
					this.CreateAndUploadNextUpdate().Wait();
					return;
				}
			}
			else
			{
				this.DownloadUpdates().Wait();
			}
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x00029944 File Offset: 0x00027B44
		private async Task DownloadUpdates()
		{
			RusClient client = new RusClient();
			Version latestVersion = await client.GetLatestVersionAsync(this.GenericRUSClientId);
			if (latestVersion == null)
			{
				string text = string.Format("SpamDB has no data for client Id - {0}", this.GenericRUSClientId);
				base.Result.Error = text;
				this.TraceError(text);
				throw new ApplicationException(text);
			}
			if (this.IsMinorDownloader)
			{
				int numberOfUpdates;
				Version currentVersion = this.GetCurrentVersion(latestVersion, out numberOfUpdates);
				await this.GetUpdates(client, currentVersion, latestVersion, numberOfUpdates);
			}
			else
			{
				Version currentVersion = new Version(0, 0, 0, 0);
				await this.GetUpdates(client, currentVersion, latestVersion, latestVersion.Minor + 1);
			}
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x00029D74 File Offset: 0x00027F74
		private async Task GetUpdates(RusClient client, Version currentVersion, Version latestVersion, int numberOfUpdates)
		{
			IDictionary<Version, byte[]> downloadedData = null;
			string errorMsg = string.Empty;
			if (currentVersion == null || latestVersion == null)
			{
				errorMsg = string.Format("Current version or latest version cannot be null. Client Id - {0}", this.GenericRUSClientId);
				base.Result.Error = errorMsg;
				this.TraceError(errorMsg);
				throw new ApplicationException(errorMsg);
			}
			int i = 0;
			while (i < 3)
			{
				downloadedData = await this.DownloadData(client, this.GenericRUSClientId, currentVersion);
				if (numberOfUpdates != 0 && this.VerifyUpdate)
				{
					if (downloadedData != null && downloadedData.Count >= numberOfUpdates)
					{
						if (!this.IsMinorDownloader)
						{
							currentVersion = new Version(latestVersion.Major, 0, 0, 0);
							if (currentVersion == latestVersion && downloadedData.First<KeyValuePair<Version, byte[]>>().Key.Major < currentVersion.Major)
							{
								Thread.Sleep(60000);
								currentVersion = new Version(0, 0, 0, 0);
								goto IL_28C;
							}
						}
						else if (new Version(this.GetNextVersion(currentVersion)) < downloadedData.First<KeyValuePair<Version, byte[]>>().Key)
						{
							return;
						}
						this.VerifyDownloadedUpdates(downloadedData, currentVersion, latestVersion);
						return;
					}
					if (downloadedData != null && downloadedData.Count > 0)
					{
						Version key = downloadedData.First<KeyValuePair<Version, byte[]>>().Key;
						if (key.Major == currentVersion.Major + 1)
						{
							return;
						}
					}
					Thread.Sleep(60000);
					IL_28C:
					i++;
					continue;
				}
				return;
			}
			errorMsg = string.Format("GenericRusProbe Failed to download all the updates in GetUpdates Method - \r\nCurrentVersion {0} \r\nLatestVersion {1} \r\nClientId: {2}", currentVersion.ToString(), latestVersion.ToString(), this.GenericRUSClientId);
			if (downloadedData != null)
			{
				foreach (KeyValuePair<Version, byte[]> keyValuePair in downloadedData)
				{
					errorMsg = errorMsg + "\r\n" + keyValuePair.Key.ToString();
				}
			}
			base.Result.Error = errorMsg;
			this.TraceError(errorMsg);
			throw new ApplicationException(errorMsg);
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x0002A0A8 File Offset: 0x000282A8
		private async Task<IDictionary<Version, byte[]>> DownloadData(RusClient client, ClientId clientId, Version currentVersion)
		{
			IEnumerable<ContentInfo> updateVersionInfoList = await client.GetUpdateVersions(clientId, currentVersion);
			Dictionary<Version, byte[]> updates = new Dictionary<Version, byte[]>();
			Version version;
			if (updateVersionInfoList != null && ((List<ContentInfo>)updateVersionInfoList).Count > 0 && (version = ((List<ContentInfo>)updateVersionInfoList)[0].Version) < currentVersion)
			{
				string text = string.Format("Failed to download correct versions for Client ID {0}, CurrentVersion {1} First available download version {2}", clientId, currentVersion, version);
				base.Result.Error = text;
				this.TraceError(text);
				throw new ApplicationException(text);
			}
			foreach (ContentInfo contentInfo in updateVersionInfoList)
			{
				using (IResponseStreamManager responseStreamManager = ResponseStreamManagerFactory.Create(clientId, contentInfo.BlobId, contentInfo.Version, contentInfo.DataSize))
				{
					Task<Stream> task = client.DownloadBlobAsync(clientId, contentInfo.Version, responseStreamManager, null);
					using (Stream result = task.GetAwaiter().GetResult())
					{
						if (result == null)
						{
							return updates;
						}
						byte[] array = new byte[result.Length];
						result.Read(array, 0, array.Count<byte>());
						updates.Add(contentInfo.Version, array);
					}
				}
			}
			return updates;
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x0002A108 File Offset: 0x00028308
		private void VerifyDownloadedUpdates(IDictionary<Version, byte[]> downloadedData, Version currentVersion, Version latestVersion)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = string.Empty;
			if (currentVersion < latestVersion && downloadedData.Count == 0)
			{
				text = string.Format("GenericRusProbe Failed to download updates in VerifyDownloadedUpdates #1- \r\nCurrent Version {0}\r\nLatest Version {1}\r\nDownloaded Version : Null \r\nClientId: {2}", currentVersion.ToString(), latestVersion.ToString(), this.GenericRUSClientId);
				base.Result.Error = text;
				this.TraceError(text);
				throw new ApplicationException(text);
			}
			Version version = currentVersion;
			if (this.IsMinorDownloader)
			{
				version = new Version(this.GetNextVersion(currentVersion));
				if (version.Minor == 0)
				{
					version = new Version(this.GetNextVersion(version));
				}
				if (latestVersion.Minor == 0 && latestVersion.Major != 1)
				{
					latestVersion = new Version(latestVersion.Major - 1, this.MaxMinorUpdate, 0, 0);
				}
			}
			Encoding encoding = new ASCIIEncoding();
			foreach (KeyValuePair<Version, byte[]> keyValuePair in downloadedData)
			{
				if (!(keyValuePair.Key == version))
				{
					stringBuilder.Clear();
					stringBuilder.Append(string.Format("GenericRusProbe Failed to download update VerifyDownloadedUpdates #2 Next version mismatch - \r\nFailure Version {0} \r\nClientId: {1} \r\n Current Version - {2}\r\n Latest Version  - {3}\r\n IsMinorDownloader - {4}", new object[]
					{
						version.ToString(),
						this.GenericRUSClientId,
						currentVersion.ToString(),
						latestVersion.ToString(),
						this.IsMinorDownloader.ToString()
					}));
					foreach (KeyValuePair<Version, byte[]> keyValuePair2 in downloadedData)
					{
						stringBuilder.Append("\r\n" + keyValuePair2.Key.ToString());
					}
					text = stringBuilder.ToString();
					base.Result.Error = text;
					this.TraceError(text);
					throw new ApplicationException(text);
				}
				Stream stream = this.GenerateTestData(version.ToString());
				StreamReader streamReader = new StreamReader(stream);
				string a = streamReader.ReadToEnd();
				string @string = encoding.GetString(keyValuePair.Value);
				if (!string.Equals(a, @string))
				{
					text = string.Format("Incorrect Data downloaded - \r\nCurrent Version {0}\r\nClientId: {1}", version.ToString(), this.GenericRUSClientId);
					base.Result.Error = text;
					this.TraceError(text);
					throw new ApplicationException(text);
				}
				if (keyValuePair.Key.Equals(latestVersion))
				{
					this.TraceDebug(string.Format("GenericRusProbe Successfully downloaded the updates - \r\nCurrent Version {0}\r\n Latest Version {1}\r\n ClientId: {2}", currentVersion.ToString(), latestVersion.ToString(), this.GenericRUSClientId));
					return;
				}
				version = new Version(this.GetNextVersion(keyValuePair.Key));
				if (this.IsMinorDownloader && version.Minor == 0)
				{
					version = new Version(this.GetNextVersion(version));
				}
			}
			stringBuilder.Clear();
			stringBuilder.Append(string.Format("GenericRusProbe Failed to download all the updates VerifyDownloadedUpdates #3 - \r\nCurrent Version {0}\r\nLatest Version {1}\r\nFinal downloaded Version {2} \r\nClientId: {3}", new object[]
			{
				currentVersion.ToString(),
				latestVersion.ToString(),
				version.ToString(),
				this.GenericRUSClientId
			}));
			foreach (KeyValuePair<Version, byte[]> keyValuePair3 in downloadedData)
			{
				stringBuilder.Append("\r\n" + keyValuePair3.Key.ToString());
			}
			text = stringBuilder.ToString();
			base.Result.Error = text;
			this.TraceError(text);
			throw new ApplicationException(text);
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x0002A4C0 File Offset: 0x000286C0
		private Version GetCurrentVersion(Version latestVersion, out int numberOfUpdates)
		{
			numberOfUpdates = 0;
			Version result;
			if (latestVersion.Minor - this.GoBehindCurrentMinor >= 0)
			{
				int major = latestVersion.Major;
				int minor = latestVersion.Minor - this.GoBehindCurrentMinor;
				result = new Version(major, minor, 0, 0);
				numberOfUpdates = this.GoBehindCurrentMinor;
			}
			else
			{
				int major;
				int minor;
				if (latestVersion.Major == 1)
				{
					major = 1;
					minor = 0;
					numberOfUpdates = latestVersion.Minor;
				}
				else
				{
					major = latestVersion.Major - 1;
					minor = this.MaxMinorUpdate - (this.GoBehindCurrentMinor - latestVersion.Minor);
					numberOfUpdates = this.GoBehindCurrentMinor;
				}
				result = new Version(major, minor, 0, 0);
			}
			return result;
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x0002A7C8 File Offset: 0x000289C8
		private async Task CreateAndUploadNextUpdate()
		{
			RusClient client = new RusClient();
			Version currentVersion = await client.GetLatestVersionAsync(this.GenericRUSClientId);
			string nextVersion;
			if (currentVersion == null)
			{
				nextVersion = "1.0.0.0";
			}
			else
			{
				nextVersion = this.GetNextVersion(currentVersion);
			}
			Stream data = this.GenerateTestData(nextVersion);
			Version dataVersion = new Version(nextVersion);
			bool uploadingResult = await client.PublishBlobAsync(this.GenericRUSClientId, data, dataVersion, null, false, 2);
			if (uploadingResult)
			{
				this.TraceDebug(string.Format("GenericRusProbe Successfully published an update - \r\nVersion {0} \r\nClientId: {1}", nextVersion, this.GenericRUSClientId));
				return;
			}
			string text = string.Format("GenericRusProbe Failed to publish an update - \r\nVersion {0} \r\nClientId: {1}", nextVersion, this.GenericRUSClientId);
			base.Result.Error = text;
			this.TraceError(text);
			throw new ApplicationException(text);
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0002A810 File Offset: 0x00028A10
		private void ReadInputXML()
		{
			string extensionAttributes = base.Definition.ExtensionAttributes;
			if (string.IsNullOrWhiteSpace(extensionAttributes))
			{
				throw new ArgumentNullException("WorkContext definition Xml is null or empty");
			}
			XmlDocument xmlDocument = new SafeXmlDocument();
			xmlDocument.LoadXml(extensionAttributes);
			XmlElement xmlElement = Utils.CheckXmlElement(xmlDocument.SelectSingleNode("//WorkContext"), "//WorkContext");
			string attribute = xmlElement.GetAttribute("IsUploader");
			if (string.IsNullOrWhiteSpace(attribute))
			{
				throw new ArgumentNullException(string.Format("WorkContext definition Xml does not have {0} attribute", "IsUploader"));
			}
			this.IsUploader = (attribute == "1");
			string attribute2 = xmlElement.GetAttribute("ClientId");
			if (string.IsNullOrWhiteSpace(attribute2))
			{
				throw new ArgumentNullException(string.Format("WorkContext definition Xml does not have {0} attribute", "ClientId"));
			}
			ClientId genericRUSClientId;
			if (!Enum.TryParse<ClientId>(attribute2, out genericRUSClientId))
			{
				throw new ArgumentException(string.Format("Incorrect ClientId - {0}", attribute2));
			}
			this.GenericRUSClientId = genericRUSClientId;
			XmlElement xmlElement2 = Utils.CheckXmlElement(xmlDocument.SelectSingleNode("//Uploader"), "//Uploader");
			string attribute3 = xmlElement2.GetAttribute("MinorUpdateSizeInBytes");
			if (string.IsNullOrWhiteSpace(attribute3))
			{
				this.MinorUpdateSize = 20480;
			}
			else
			{
				this.MinorUpdateSize = int.Parse(attribute3);
			}
			string attribute4 = xmlElement2.GetAttribute("MajorUpdateSizeInBytes");
			if (string.IsNullOrWhiteSpace(attribute4))
			{
				this.MajorUpdateSize = 10485760;
			}
			else
			{
				this.MajorUpdateSize = int.Parse(attribute4);
			}
			string attribute5 = xmlElement2.GetAttribute("MaxNumberMinorUpdate");
			if (string.IsNullOrWhiteSpace(attribute5))
			{
				this.MaxMinorUpdate = 60;
			}
			else
			{
				this.MaxMinorUpdate = int.Parse(attribute5);
			}
			string attribute6 = xmlElement2.GetAttribute("GenerateUpdateProbabilityInPercent");
			if (string.IsNullOrWhiteSpace(attribute6))
			{
				this.ProbabilityPercentCount = 100;
			}
			else
			{
				this.ProbabilityPercentCount = int.Parse(attribute6);
			}
			XmlElement xmlElement3 = Utils.CheckXmlElement(xmlDocument.SelectSingleNode("//Downloader"), "//Downloader");
			string attribute7 = xmlElement3.GetAttribute("IsMinorDownloader");
			if (string.IsNullOrWhiteSpace(attribute7))
			{
				this.IsMinorDownloader = true;
			}
			else if (int.Parse(attribute7) == 1)
			{
				this.IsMinorDownloader = true;
			}
			else
			{
				this.IsMinorDownloader = false;
			}
			string attribute8 = xmlElement3.GetAttribute("GoBehindCurrentMinor");
			if (string.IsNullOrWhiteSpace(attribute8))
			{
				this.GoBehindCurrentMinor = 2;
			}
			else
			{
				this.GoBehindCurrentMinor = int.Parse(attribute8);
			}
			string attribute9 = xmlElement3.GetAttribute("VerifyUpdate");
			if (string.IsNullOrWhiteSpace(attribute9))
			{
				this.VerifyUpdate = true;
				return;
			}
			this.VerifyUpdate = (int.Parse(attribute9) == 1);
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x0002AA70 File Offset: 0x00028C70
		private string GetNextVersion(Version currentVersion)
		{
			int major = currentVersion.Major;
			int num = currentVersion.Minor;
			if (num < this.MaxMinorUpdate)
			{
				num++;
				return string.Concat(new object[]
				{
					major.ToString(),
					".",
					num,
					".0.0"
				});
			}
			return (major + 1).ToString() + ".0.0.0";
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x0002AAE0 File Offset: 0x00028CE0
		private bool GenerateUpdate(int percentageCount)
		{
			Random random = new Random();
			int num = random.Next(1, 101);
			return num <= percentageCount;
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0002AB04 File Offset: 0x00028D04
		private Stream GenerateTestData(string versionNumber)
		{
			Version version = new Version(versionNumber);
			int num;
			if (version.Minor == 0)
			{
				num = this.MajorUpdateSize;
			}
			else
			{
				num = this.MinorUpdateSize;
			}
			StringBuilder stringBuilder = new StringBuilder(num);
			stringBuilder.Append(versionNumber);
			num -= versionNumber.Length;
			short num2 = 0;
			for (int i = 0; i < num; i++)
			{
				if (num2 == 26)
				{
					num2 = 0;
				}
				stringBuilder.Append((char)(num2 + 97));
				num2 += 1;
			}
			byte[] bytes = Encoding.ASCII.GetBytes(stringBuilder.ToString());
			return new MemoryStream(bytes);
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x0002AB8F File Offset: 0x00028D8F
		private void TraceDebug(string debugMsg)
		{
			ProbeResult result = base.Result;
			result.ExecutionContext = result.ExecutionContext + debugMsg + " ";
			WTFDiagnostics.TraceDebug(ExTraceGlobals.BackgroundTracer, base.TraceContext, debugMsg, null, "TraceDebug", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\GenericRus\\GenericRUSProbe.cs", 731);
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x0002ABCE File Offset: 0x00028DCE
		private void TraceError(string errorMsg)
		{
			ProbeResult result = base.Result;
			result.ExecutionContext = result.ExecutionContext + errorMsg + " ";
			WTFDiagnostics.TraceError(ExTraceGlobals.BackgroundTracer, base.TraceContext, errorMsg, null, "TraceError", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\GenericRus\\GenericRUSProbe.cs", 741);
		}

		// Token: 0x0400078E RID: 1934
		private const string UploaderXMLNodeString = "//Uploader";

		// Token: 0x0400078F RID: 1935
		private const string DownloaderXMLNodeString = "//Downloader";

		// Token: 0x04000790 RID: 1936
		private const string IsUploaderAttributeString = "IsUploader";

		// Token: 0x04000791 RID: 1937
		private const string ClientIdAttributeString = "ClientId";

		// Token: 0x04000792 RID: 1938
		private const string MinorUpdateSizeAttributeString = "MinorUpdateSizeInBytes";

		// Token: 0x04000793 RID: 1939
		private const string MajorUpdateSizeAttributeString = "MajorUpdateSizeInBytes";

		// Token: 0x04000794 RID: 1940
		private const string HighestMinorUpdateString = "MaxNumberMinorUpdate";

		// Token: 0x04000795 RID: 1941
		private const string UpdateProbabilityString = "GenerateUpdateProbabilityInPercent";

		// Token: 0x04000796 RID: 1942
		private const string IsMinorDownloaderString = "IsMinorDownloader";

		// Token: 0x04000797 RID: 1943
		private const string GoBehindCurrentMinorString = "GoBehindCurrentMinor";

		// Token: 0x04000798 RID: 1944
		private const string VerifyUpdateString = "VerifyUpdate";

		// Token: 0x04000799 RID: 1945
		private const int DefaultMaxMinorUpdate = 60;

		// Token: 0x0400079A RID: 1946
		private const int DefaultProbabilityPercentCount = 100;

		// Token: 0x0400079B RID: 1947
		private const int DefaultMinorUpdateSize = 20480;

		// Token: 0x0400079C RID: 1948
		private const int DefaultMajorUpdateSize = 10485760;

		// Token: 0x0400079D RID: 1949
		private const bool DefaultIsMinorDownloader = true;

		// Token: 0x0400079E RID: 1950
		private const int DefaultGoBehindMinor = 2;

		// Token: 0x0400079F RID: 1951
		private const bool DefaultVerifyUpdate = true;
	}
}
