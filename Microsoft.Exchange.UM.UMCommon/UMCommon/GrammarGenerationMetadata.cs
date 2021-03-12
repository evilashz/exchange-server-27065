using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200009D RID: 157
	internal class GrammarGenerationMetadata
	{
		// Token: 0x0600056F RID: 1391 RVA: 0x00015548 File Offset: 0x00013748
		public GrammarGenerationMetadata(int metadataVersion, string tenantId, Guid runId, string serverName, string serverVersion, DateTime generationTimeUtc, List<GrammarFileMetadata> grammarFiles)
		{
			ValidateArgument.NotNullOrEmpty(tenantId, "tenantId");
			ValidateArgument.NotNullOrEmpty(serverName, "serverName");
			ValidateArgument.NotNullOrEmpty(serverVersion, "serverVersion");
			ValidateArgument.NotNull(grammarFiles, "grammarFiles");
			CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this, "GrammarGenerationMetadata constructor - metadataVersion='{0}', tenantId='{1}', runId='{2}', serverName='{3}', serverVersion='{4}', generationTimeUtc='{5}'", new object[]
			{
				metadataVersion,
				tenantId,
				runId.ToString(),
				serverName,
				serverVersion,
				generationTimeUtc.ToString("u", DateTimeFormatInfo.InvariantInfo)
			});
			this.MetadataVersion = metadataVersion;
			this.TenantId = tenantId;
			this.RunId = runId;
			this.ServerName = serverName;
			this.ServerVersion = serverVersion;
			this.GenerationTimeUtc = generationTimeUtc;
			this.GrammarFiles = grammarFiles;
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x00015612 File Offset: 0x00013812
		// (set) Token: 0x06000571 RID: 1393 RVA: 0x0001561A File Offset: 0x0001381A
		public int MetadataVersion { get; private set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x00015623 File Offset: 0x00013823
		// (set) Token: 0x06000573 RID: 1395 RVA: 0x0001562B File Offset: 0x0001382B
		public string TenantId { get; private set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x00015634 File Offset: 0x00013834
		// (set) Token: 0x06000575 RID: 1397 RVA: 0x0001563C File Offset: 0x0001383C
		public Guid RunId { get; private set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x00015645 File Offset: 0x00013845
		// (set) Token: 0x06000577 RID: 1399 RVA: 0x0001564D File Offset: 0x0001384D
		public string ServerName { get; private set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x00015656 File Offset: 0x00013856
		// (set) Token: 0x06000579 RID: 1401 RVA: 0x0001565E File Offset: 0x0001385E
		public string ServerVersion { get; private set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x00015667 File Offset: 0x00013867
		// (set) Token: 0x0600057B RID: 1403 RVA: 0x0001566F File Offset: 0x0001386F
		public DateTime GenerationTimeUtc { get; private set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x00015678 File Offset: 0x00013878
		// (set) Token: 0x0600057D RID: 1405 RVA: 0x00015680 File Offset: 0x00013880
		public List<GrammarFileMetadata> GrammarFiles { get; private set; }

		// Token: 0x0600057E RID: 1406 RVA: 0x0001568C File Offset: 0x0001388C
		public static void Serialize(GrammarGenerationMetadata metadata, string filePath)
		{
			ValidateArgument.NotNull(metadata, "metadata");
			ValidateArgument.NotNullOrEmpty(filePath, "filePath");
			CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, null, "GrammarGenerationMetadata.Serialize - filePath='{0}'", new object[]
			{
				filePath
			});
			using (Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
			{
				using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stream, Encoding.UTF8))
				{
					xmlTextWriter.WriteStartDocument();
					xmlTextWriter.WriteStartElement("GrammarGeneration");
					xmlTextWriter.WriteAttributeString("metadataVersion", metadata.MetadataVersion.ToString("d"));
					xmlTextWriter.WriteAttributeString("tenantId", metadata.TenantId);
					xmlTextWriter.WriteAttributeString("runId", metadata.RunId.ToString());
					xmlTextWriter.WriteAttributeString("serverName", metadata.ServerName);
					xmlTextWriter.WriteAttributeString("serverVersion", metadata.ServerVersion);
					xmlTextWriter.WriteAttributeString("generationTimeUtc", metadata.GenerationTimeUtc.ToString("u", DateTimeFormatInfo.InvariantInfo));
					xmlTextWriter.WriteStartElement("GrammarFiles");
					foreach (GrammarFileMetadata grammarFileMetadata in metadata.GrammarFiles)
					{
						xmlTextWriter.WriteStartElement("File");
						xmlTextWriter.WriteAttributeString("SHA", grammarFileMetadata.Hash);
						xmlTextWriter.WriteAttributeString("size", grammarFileMetadata.Size.ToString("d"));
						xmlTextWriter.WriteString(grammarFileMetadata.Path);
						xmlTextWriter.WriteEndElement();
					}
					xmlTextWriter.WriteEndElement();
					xmlTextWriter.WriteEndElement();
					xmlTextWriter.WriteEndDocument();
				}
			}
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00015888 File Offset: 0x00013A88
		public static GrammarGenerationMetadata Deserialize(string filePath)
		{
			ValidateArgument.NotNullOrEmpty(filePath, "filePath");
			CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, null, "GrammarGenerationMetadata.Deserialize - filePath='{0}'", new object[]
			{
				filePath
			});
			List<GrammarFileMetadata> list = new List<GrammarFileMetadata>();
			GrammarGenerationMetadata result;
			using (Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
			{
				using (XmlTextReader xmlTextReader = new XmlTextReader(stream))
				{
					xmlTextReader.ReadToFollowing("GrammarGeneration");
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
					xmlTextReader.MoveToAttribute("generationTimeUtc");
					string attribute6 = xmlTextReader.GetAttribute("generationTimeUtc");
					DateTime generationTimeUtc = DateTime.Parse(attribute6, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.RoundtripKind);
					xmlTextReader.ReadToFollowing("GrammarFiles");
					while (xmlTextReader.ReadToFollowing("File"))
					{
						xmlTextReader.MoveToAttribute("SHA");
						string attribute7 = xmlTextReader.GetAttribute("SHA");
						xmlTextReader.MoveToAttribute("size");
						string attribute8 = xmlTextReader.GetAttribute("size");
						long size = long.Parse(attribute8);
						string path = xmlTextReader.ReadString();
						list.Add(new GrammarFileMetadata(path, attribute7, size));
					}
					result = new GrammarGenerationMetadata(metadataVersion, attribute2, runId, attribute4, attribute5, generationTimeUtc, list);
				}
			}
			return result;
		}

		// Token: 0x0400034E RID: 846
		private const string RootElement = "GrammarGeneration";

		// Token: 0x0400034F RID: 847
		private const string MetadataVersionAttribute = "metadataVersion";

		// Token: 0x04000350 RID: 848
		private const string TenantIdAttribute = "tenantId";

		// Token: 0x04000351 RID: 849
		private const string RunIdAttribute = "runId";

		// Token: 0x04000352 RID: 850
		private const string ServerNameAttribute = "serverName";

		// Token: 0x04000353 RID: 851
		private const string ServerVersionAttribute = "serverVersion";

		// Token: 0x04000354 RID: 852
		private const string GenerationTimeUtcAttribute = "generationTimeUtc";

		// Token: 0x04000355 RID: 853
		private const string GrammarFilesElement = "GrammarFiles";

		// Token: 0x04000356 RID: 854
		private const string GrammarFileElement = "File";

		// Token: 0x04000357 RID: 855
		private const string FileHashAttribute = "SHA";

		// Token: 0x04000358 RID: 856
		private const string FileSizeAttribute = "size";

		// Token: 0x04000359 RID: 857
		public const int CurrentMetadataVersion = 1;
	}
}
