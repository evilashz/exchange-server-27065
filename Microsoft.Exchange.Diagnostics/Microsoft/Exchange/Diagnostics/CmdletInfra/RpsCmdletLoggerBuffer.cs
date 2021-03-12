using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Diagnostics.CmdletInfra
{
	// Token: 0x0200010D RID: 269
	internal class RpsCmdletLoggerBuffer
	{
		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x00020186 File Offset: 0x0001E386
		internal Dictionary<Enum, object> MetadataLogCache
		{
			get
			{
				return this.metadataLogCache;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x0002018E File Offset: 0x0001E38E
		internal Dictionary<string, string> GenericInfoLogCache
		{
			get
			{
				return this.genericInfoLogCache;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x00020196 File Offset: 0x0001E396
		internal Dictionary<string, string> GenericErrorLogCache
		{
			get
			{
				return this.genericErrorLogCache;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x0002019E File Offset: 0x0001E39E
		internal Dictionary<Enum, double> LatencyLogCache
		{
			get
			{
				return this.latencyLogCache;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x000201A6 File Offset: 0x0001E3A6
		internal Dictionary<Enum, Dictionary<string, string>> GenericColumnLogCache
		{
			get
			{
				return this.genericColumnLogCache;
			}
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x000201B0 File Offset: 0x0001E3B0
		internal static RpsCmdletLoggerBuffer Get(Guid cmdletUniqueId)
		{
			if (cmdletUniqueId == Guid.Empty && !CmdletThreadStaticData.TryGetCurrentCmdletUniqueId(out cmdletUniqueId))
			{
				return null;
			}
			RpsCmdletLoggerBuffer rpsCmdletLoggerBuffer;
			if (!CmdletStaticDataWithUniqueId<RpsCmdletLoggerBuffer>.TryGet(cmdletUniqueId, out rpsCmdletLoggerBuffer))
			{
				rpsCmdletLoggerBuffer = new RpsCmdletLoggerBuffer();
				CmdletStaticDataWithUniqueId<RpsCmdletLoggerBuffer>.Set(cmdletUniqueId, rpsCmdletLoggerBuffer);
			}
			return rpsCmdletLoggerBuffer;
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x000201ED File Offset: 0x0001E3ED
		internal void AddMetadataLog(Enum key, object value)
		{
			this.metadataLogCache[key] = value;
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x000201FC File Offset: 0x0001E3FC
		internal void AppendGenericInfo(string key, string value)
		{
			this.genericInfoLogCache[key] = value;
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0002020B File Offset: 0x0001E40B
		internal void AppendGenericError(string key, string value)
		{
			this.genericErrorLogCache[key] = value;
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0002021A File Offset: 0x0001E41A
		internal void UpdateLatency(Enum latencyMetadata, double latencyInMilliseconds)
		{
			this.latencyLogCache[latencyMetadata] = latencyInMilliseconds;
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00020229 File Offset: 0x0001E429
		internal void AppendColumn(Enum column, string key, string value)
		{
			if (!this.genericColumnLogCache.ContainsKey(column))
			{
				this.genericColumnLogCache[column] = new Dictionary<string, string>();
			}
			this.genericColumnLogCache[column][key] = value;
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0002025D File Offset: 0x0001E45D
		internal void Reset()
		{
			this.MetadataLogCache.Clear();
			this.LatencyLogCache.Clear();
			this.GenericInfoLogCache.Clear();
			this.GenericErrorLogCache.Clear();
			this.GenericColumnLogCache.Clear();
		}

		// Token: 0x04000501 RID: 1281
		private readonly Dictionary<Enum, object> metadataLogCache = new Dictionary<Enum, object>();

		// Token: 0x04000502 RID: 1282
		private readonly Dictionary<string, string> genericInfoLogCache = new Dictionary<string, string>();

		// Token: 0x04000503 RID: 1283
		private readonly Dictionary<string, string> genericErrorLogCache = new Dictionary<string, string>();

		// Token: 0x04000504 RID: 1284
		private readonly Dictionary<Enum, double> latencyLogCache = new Dictionary<Enum, double>();

		// Token: 0x04000505 RID: 1285
		private readonly Dictionary<Enum, Dictionary<string, string>> genericColumnLogCache = new Dictionary<Enum, Dictionary<string, string>>();
	}
}
