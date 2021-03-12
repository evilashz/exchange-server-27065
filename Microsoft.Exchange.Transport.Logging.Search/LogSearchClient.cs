using System;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc.LogSearch;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000024 RID: 36
	internal class LogSearchClient : LogSearchRpcClient
	{
		// Token: 0x06000072 RID: 114 RVA: 0x000071F2 File Offset: 0x000053F2
		public LogSearchClient(string server, ServerVersion version) : base(server)
		{
			if (version == null)
			{
				throw new ArgumentNullException("version must not be null.");
			}
			this.remoteServerVersion = version;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00007218 File Offset: 0x00005418
		public int Search(string logName, LogQuery query, bool continueInBackground, byte[] results, out Guid sessionId, out bool more, out int progress)
		{
			byte[] query2 = LogSearchClient.SerializeQuery(query);
			sessionId = Guid.Empty;
			more = false;
			progress = 0;
			if (this.remoteServerVersion >= LogSearchConstants.LowestModernInterfaceBuildVersion)
			{
				return base.SearchExtensibleSchema(CsvFieldCache.LocalVersion.ToString(), logName, query2, continueInBackground, results, ref sessionId, ref more, ref progress);
			}
			return base.Search(logName, query2, continueInBackground, results, ref sessionId, ref more, ref progress);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00007284 File Offset: 0x00005484
		public new int Continue(Guid sessionId, bool continueInBackground, byte[] results, out bool more, out int progress)
		{
			more = false;
			progress = 0;
			return base.Continue(sessionId, continueInBackground, results, ref more, ref progress);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000729C File Offset: 0x0000549C
		private static byte[] SerializeQuery(LogQuery query)
		{
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
			xmlWriterSettings.OmitXmlDeclaration = true;
			xmlWriterSettings.Encoding = Encoding.UTF8;
			xmlWriterSettings.Indent = false;
			byte[] result = null;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings))
				{
					LogSearchClient.logQuerySerializer.Serialize(xmlWriter, query);
					result = memoryStream.ToArray();
				}
			}
			return result;
		}

		// Token: 0x04000058 RID: 88
		private static LogQuerySerializer logQuerySerializer = new LogQuerySerializer();

		// Token: 0x04000059 RID: 89
		private ServerVersion remoteServerVersion;
	}
}
