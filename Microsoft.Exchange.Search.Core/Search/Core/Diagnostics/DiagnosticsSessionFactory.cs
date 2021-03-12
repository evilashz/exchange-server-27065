using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Search.Core.Diagnostics
{
	// Token: 0x0200009A RID: 154
	internal class DiagnosticsSessionFactory : IDiagnosticsSessionFactory
	{
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x0000E000 File Offset: 0x0000C200
		internal static DiagnosticsLogConfig.LogDefaults GracefulDegradationLogDefaults
		{
			get
			{
				if (DiagnosticsSessionFactory.gracefulDegradationLogDefaults == null)
				{
					DiagnosticsSessionFactory.gracefulDegradationLogDefaults = DiagnosticsSessionFactory.CreateLogDefaults(DiagnosticsSessionFactory.GracefulDegradationLogGuid, "Search Graceful Degradation Logs", "Search\\GracefulDegradation", "GracefulDegradation_");
				}
				return DiagnosticsSessionFactory.gracefulDegradationLogDefaults;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x0000E02C File Offset: 0x0000C22C
		internal static DiagnosticsLogConfig.LogDefaults DictionaryLogDefaults
		{
			get
			{
				if (DiagnosticsSessionFactory.dictionaryLogDefaults == null)
				{
					DiagnosticsSessionFactory.dictionaryLogDefaults = DiagnosticsSessionFactory.CreateLogDefaults(DiagnosticsSessionFactory.DictionaryLogGuid, "Search Dictionary Logs", "Search\\Dictionary", "Dictionary_");
				}
				return DiagnosticsSessionFactory.dictionaryLogDefaults;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0000E058 File Offset: 0x0000C258
		private static DiagnosticsLogConfig.LogDefaults LogDefaults
		{
			get
			{
				if (DiagnosticsSessionFactory.logDefaults == null)
				{
					DiagnosticsSessionFactory.logDefaults = DiagnosticsSessionFactory.CreateLogDefaults(DiagnosticsSessionFactory.ServiceLogGuid, "Search Diagnostics Logs", "Search", "Search_");
				}
				return DiagnosticsSessionFactory.logDefaults;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x0000E084 File Offset: 0x0000C284
		private static DiagnosticsLogConfig.LogDefaults CrawlerLogDefaults
		{
			get
			{
				if (DiagnosticsSessionFactory.crawlerLogDefaults == null)
				{
					DiagnosticsSessionFactory.crawlerLogDefaults = DiagnosticsSessionFactory.CreateLogDefaults(DiagnosticsSessionFactory.CrawlerLogGuid, "Search Crawler Logs", "Search\\Crawler", "SearchCrawler_");
				}
				return DiagnosticsSessionFactory.crawlerLogDefaults;
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000E0B0 File Offset: 0x0000C2B0
		public static void SetDefaults(Guid eventLogComponentGuid, string serviceName, string logTypeName, string logFilePath, string logFilePrefix, string logComponent)
		{
			DiagnosticsSessionFactory.logDefaults = new DiagnosticsLogConfig.LogDefaults(eventLogComponentGuid, serviceName, logTypeName, logFilePath, logFilePrefix, logComponent);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000E0C4 File Offset: 0x0000C2C4
		public IDiagnosticsSession CreateComponentDiagnosticsSession(string componentName, Trace tracer, long traceContext)
		{
			return this.CreateComponentDiagnosticsSession(componentName, null, tracer, traceContext);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000E0D0 File Offset: 0x0000C2D0
		public IDiagnosticsSession CreateComponentDiagnosticsSession(string componentName, string eventLogSourceName, Trace tracer, long traceContext)
		{
			return new DiagnosticsSession(componentName, eventLogSourceName, (tracer == null) ? ExTraceGlobals.GeneralTracer : tracer, traceContext, null, DiagnosticsSessionFactory.LogDefaults, DiagnosticsSessionFactory.CrawlerLogDefaults, DiagnosticsSessionFactory.GracefulDegradationLogDefaults, DiagnosticsSessionFactory.DictionaryLogDefaults);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000E108 File Offset: 0x0000C308
		public IDiagnosticsSession CreateDocumentDiagnosticsSession(IIdentity documentId, Trace tracer)
		{
			return new DiagnosticsSession("Document", null, (tracer == null) ? ExTraceGlobals.GeneralTracer : tracer, (long)documentId.GetHashCode(), documentId, DiagnosticsSessionFactory.LogDefaults, DiagnosticsSessionFactory.CrawlerLogDefaults, DiagnosticsSessionFactory.GracefulDegradationLogDefaults, DiagnosticsSessionFactory.DictionaryLogDefaults);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000E14C File Offset: 0x0000C34C
		private static DiagnosticsLogConfig.LogDefaults CreateLogDefaults(Guid guid, string logTypeName, string path, string prefix)
		{
			string path2;
			try
			{
				path2 = ExchangeSetupContext.InstallPath;
			}
			catch (SetupVersionInformationCorruptException)
			{
				path2 = string.Empty;
			}
			return new DiagnosticsLogConfig.LogDefaults(guid, ComponentInstance.Globals.Search.ServiceName, logTypeName, Path.Combine(path2, "Logging", path), prefix, "SearchLogs");
		}

		// Token: 0x04000204 RID: 516
		private static readonly Guid ServiceLogGuid = Guid.Parse("c87fb454-7dfe-4559-af8c-3905438e1398");

		// Token: 0x04000205 RID: 517
		private static readonly Guid CrawlerLogGuid = Guid.Parse("f58e945b-181f-412e-8c46-96bd3b05727d");

		// Token: 0x04000206 RID: 518
		private static readonly Guid GracefulDegradationLogGuid = Guid.Parse("2f753801-8857-477b-a4c5-8605253df53d");

		// Token: 0x04000207 RID: 519
		private static readonly Guid DictionaryLogGuid = Guid.Parse("9151E90B-B707-42F1-A24B-98B47079450B");

		// Token: 0x04000208 RID: 520
		private static DiagnosticsLogConfig.LogDefaults logDefaults;

		// Token: 0x04000209 RID: 521
		private static DiagnosticsLogConfig.LogDefaults crawlerLogDefaults;

		// Token: 0x0400020A RID: 522
		private static DiagnosticsLogConfig.LogDefaults gracefulDegradationLogDefaults;

		// Token: 0x0400020B RID: 523
		private static DiagnosticsLogConfig.LogDefaults dictionaryLogDefaults;
	}
}
