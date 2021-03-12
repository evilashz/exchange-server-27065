using System;
using System.Globalization;
using System.IO;
using System.Security;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.DirectoryProcessorAssistant;
using Microsoft.Exchange.UM;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant.UMDtmfMapGenerator
{
	// Token: 0x0200019E RID: 414
	internal class DtmfMapGenerationMetadata
	{
		// Token: 0x06001042 RID: 4162 RVA: 0x0005EBC8 File Offset: 0x0005CDC8
		public DtmfMapGenerationMetadata(int metadataVersion, string tenantId, Guid runId, string serverName, string serverVersion, DateTime lastIncrementalUpdateTimeUtc, DateTime lastFullUpdateTimeUtc)
		{
			ValidateArgument.NotNullOrEmpty(tenantId, "tenantId");
			ValidateArgument.NotNullOrEmpty(serverName, "serverName");
			ValidateArgument.NotNullOrEmpty(serverVersion, "serverVersion");
			Utilities.DebugTrace(ExTraceGlobals.DtmfMapGeneratorTracer, "DtmfMapGenerationMetadata constructor - metadataVersion='{0}', tenantId='{1}', runId='{2}', serverName='{3}', serverVersion='{4}', lastIncrementalUpdateTimeUtc='{5}', lastFullUpdateTimeUtc='{6}'", new object[]
			{
				metadataVersion,
				tenantId,
				runId.ToString(),
				serverName,
				serverVersion,
				lastIncrementalUpdateTimeUtc,
				lastFullUpdateTimeUtc
			});
			this.MetadataVersion = metadataVersion;
			this.TenantId = tenantId;
			this.RunId = runId;
			this.ServerName = serverName;
			this.ServerVersion = serverVersion;
			this.LastIncrementalUpdateTimeUtc = lastIncrementalUpdateTimeUtc;
			this.LastFullUpdateTimeUtc = lastFullUpdateTimeUtc;
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001043 RID: 4163 RVA: 0x0005EC85 File Offset: 0x0005CE85
		// (set) Token: 0x06001044 RID: 4164 RVA: 0x0005EC8D File Offset: 0x0005CE8D
		public int MetadataVersion { get; private set; }

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001045 RID: 4165 RVA: 0x0005EC96 File Offset: 0x0005CE96
		// (set) Token: 0x06001046 RID: 4166 RVA: 0x0005EC9E File Offset: 0x0005CE9E
		public string TenantId { get; private set; }

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001047 RID: 4167 RVA: 0x0005ECA7 File Offset: 0x0005CEA7
		// (set) Token: 0x06001048 RID: 4168 RVA: 0x0005ECAF File Offset: 0x0005CEAF
		public Guid RunId { get; private set; }

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001049 RID: 4169 RVA: 0x0005ECB8 File Offset: 0x0005CEB8
		// (set) Token: 0x0600104A RID: 4170 RVA: 0x0005ECC0 File Offset: 0x0005CEC0
		public string ServerName { get; private set; }

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x0600104B RID: 4171 RVA: 0x0005ECC9 File Offset: 0x0005CEC9
		// (set) Token: 0x0600104C RID: 4172 RVA: 0x0005ECD1 File Offset: 0x0005CED1
		public string ServerVersion { get; private set; }

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x0600104D RID: 4173 RVA: 0x0005ECDA File Offset: 0x0005CEDA
		// (set) Token: 0x0600104E RID: 4174 RVA: 0x0005ECE2 File Offset: 0x0005CEE2
		public DateTime LastIncrementalUpdateTimeUtc { get; set; }

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x0005ECEB File Offset: 0x0005CEEB
		// (set) Token: 0x06001050 RID: 4176 RVA: 0x0005ECF3 File Offset: 0x0005CEF3
		public DateTime LastFullUpdateTimeUtc { get; set; }

		// Token: 0x06001051 RID: 4177 RVA: 0x0005ECFC File Offset: 0x0005CEFC
		public static string Serialize(DtmfMapGenerationMetadata metadata, string fileName, string folderPath)
		{
			ValidateArgument.NotNull(metadata, "metadata");
			ValidateArgument.NotNullOrEmpty(fileName, "fileName");
			ValidateArgument.NotNullOrEmpty(folderPath, "folderPath");
			Utilities.DebugTrace(ExTraceGlobals.DtmfMapGeneratorTracer, "DtmfMapGenerationMetadata.Serialize - fileName='{0}', folderPath='{1}'", new object[]
			{
				fileName,
				folderPath
			});
			string text = Path.Combine(folderPath, fileName);
			try
			{
				Directory.CreateDirectory(folderPath);
				using (Stream stream = new FileStream(text, FileMode.Create, FileAccess.ReadWrite))
				{
					using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stream, Encoding.UTF8))
					{
						xmlTextWriter.WriteStartDocument();
						xmlTextWriter.WriteStartElement("DtmfMapGeneration");
						xmlTextWriter.WriteAttributeString("metadataVersion", metadata.MetadataVersion.ToString("d"));
						xmlTextWriter.WriteAttributeString("tenantId", metadata.TenantId);
						xmlTextWriter.WriteAttributeString("runId", metadata.RunId.ToString());
						xmlTextWriter.WriteAttributeString("serverName", metadata.ServerName);
						xmlTextWriter.WriteAttributeString("serverVersion", metadata.ServerVersion);
						xmlTextWriter.WriteAttributeString("lastIncrementalUpdateTimeUtc", metadata.LastIncrementalUpdateTimeUtc.ToString("u", DateTimeFormatInfo.InvariantInfo));
						xmlTextWriter.WriteAttributeString("lastFullUpdateTimeUtc", metadata.LastFullUpdateTimeUtc.ToString("u", DateTimeFormatInfo.InvariantInfo));
						xmlTextWriter.WriteEndElement();
						xmlTextWriter.WriteEndDocument();
					}
				}
			}
			catch (Exception ex)
			{
				Utilities.DebugTrace(ExTraceGlobals.DtmfMapGeneratorTracer, "DtmfMapGenerationMetadata.Serialize - exception='{0}'", new object[]
				{
					ex
				});
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_SaveDtmfMapGenerationMetadataFailed, null, new object[]
				{
					text,
					CommonUtil.ToEventLogString(ex)
				});
				if (!DtmfMapGenerationMetadata.IsExpectedException(ex))
				{
					throw;
				}
				text = null;
			}
			return text;
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x0005EF08 File Offset: 0x0005D108
		public static DtmfMapGenerationMetadata Deserialize(string filePath)
		{
			ValidateArgument.NotNullOrEmpty(filePath, "filePath");
			Utilities.DebugTrace(ExTraceGlobals.DtmfMapGeneratorTracer, "DtmfMapGenerationMetadata.Deserialize - filePath='{0}'", new object[]
			{
				filePath
			});
			DtmfMapGenerationMetadata result = null;
			try
			{
				using (Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
				{
					using (XmlTextReader xmlTextReader = new XmlTextReader(stream))
					{
						xmlTextReader.ReadToFollowing("DtmfMapGeneration");
						xmlTextReader.MoveToAttribute("metadataVersion");
						string attribute = xmlTextReader.GetAttribute("metadataVersion");
						int metadataVersion = int.Parse(attribute);
						xmlTextReader.MoveToAttribute("tenantId");
						string attribute2 = xmlTextReader.GetAttribute("tenantId");
						xmlTextReader.MoveToAttribute("runId");
						string attribute3 = xmlTextReader.GetAttribute("runId");
						Guid runId = Guid.Parse(attribute3);
						xmlTextReader.MoveToAttribute("serverName");
						string attribute4 = xmlTextReader.GetAttribute("serverName");
						xmlTextReader.MoveToAttribute("serverVersion");
						string attribute5 = xmlTextReader.GetAttribute("serverVersion");
						xmlTextReader.MoveToAttribute("lastIncrementalUpdateTimeUtc");
						string attribute6 = xmlTextReader.GetAttribute("lastIncrementalUpdateTimeUtc");
						DateTime lastIncrementalUpdateTimeUtc = DateTime.Parse(attribute6, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.RoundtripKind);
						xmlTextReader.MoveToAttribute("lastFullUpdateTimeUtc");
						string attribute7 = xmlTextReader.GetAttribute("lastFullUpdateTimeUtc");
						DateTime lastFullUpdateTimeUtc = DateTime.Parse(attribute7, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.RoundtripKind);
						result = new DtmfMapGenerationMetadata(metadataVersion, attribute2, runId, attribute4, attribute5, lastIncrementalUpdateTimeUtc, lastFullUpdateTimeUtc);
					}
				}
			}
			catch (Exception ex)
			{
				Utilities.DebugTrace(ExTraceGlobals.DtmfMapGeneratorTracer, "DtmfMapGenerationMetadata.Serialize - exception='{0}'", new object[]
				{
					ex
				});
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_LoadDtmfMapGenerationMetadataFailed, null, new object[]
				{
					filePath,
					CommonUtil.ToEventLogString(ex)
				});
				if (!DtmfMapGenerationMetadata.IsExpectedException(ex))
				{
					throw;
				}
			}
			return result;
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x0005F118 File Offset: 0x0005D318
		private static bool IsExpectedException(Exception e)
		{
			return e is IOException || e is UnauthorizedAccessException || e is SecurityException || e is XmlException;
		}

		// Token: 0x04000A3B RID: 2619
		private const string RootElement = "DtmfMapGeneration";

		// Token: 0x04000A3C RID: 2620
		private const string MetadataVersionAttribute = "metadataVersion";

		// Token: 0x04000A3D RID: 2621
		private const string TenantIdAttribute = "tenantId";

		// Token: 0x04000A3E RID: 2622
		private const string RunIdAttribute = "runId";

		// Token: 0x04000A3F RID: 2623
		private const string ServerNameAttribute = "serverName";

		// Token: 0x04000A40 RID: 2624
		private const string ServerVersionAttribute = "serverVersion";

		// Token: 0x04000A41 RID: 2625
		private const string LastIncrementalUpdateTimeUtcAttribute = "lastIncrementalUpdateTimeUtc";

		// Token: 0x04000A42 RID: 2626
		private const string LastFullUpdateTimeUtcAttribute = "lastFullUpdateTimeUtc";

		// Token: 0x04000A43 RID: 2627
		public const int CurrentMetadataVersion = 1;
	}
}
