using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Security.Authentication.FederatedAuthService
{
	// Token: 0x02000066 RID: 102
	internal abstract class STSBase
	{
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0001B35C File Offset: 0x0001955C
		protected static ExEventLog eventLogger
		{
			get
			{
				return AuthServiceHelper.EventLogger;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0001B363 File Offset: 0x00019563
		protected static LiveIdBasicAuthenticationCountersInstance counters
		{
			get
			{
				return AuthServiceHelper.PerformanceCounters;
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0001B36A File Offset: 0x0001956A
		protected STSBase()
		{
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0001B374 File Offset: 0x00019574
		public STSBase(int traceId, LiveIdInstanceType instance, NamespaceStats stats)
		{
			this.traceId = traceId;
			this.ClockSkew = TimeSpan.Zero;
			this.ClockSkewThreshold = TimeSpan.FromMinutes(5.0);
			this.ServerTime = DateTime.MinValue;
			this.Instance = instance;
			this.namespaceStats = stats;
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0001B3C6 File Offset: 0x000195C6
		// (set) Token: 0x0600035C RID: 860 RVA: 0x0001B3CE File Offset: 0x000195CE
		public LiveIdInstanceType Instance { get; private set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0001B3D7 File Offset: 0x000195D7
		public virtual string StsTag
		{
			get
			{
				return "";
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0001B3DE File Offset: 0x000195DE
		// (set) Token: 0x0600035F RID: 863 RVA: 0x0001B3E6 File Offset: 0x000195E6
		public NameValueCollection ExtraHeaders { get; set; }

		// Token: 0x06000360 RID: 864 RVA: 0x0001B3F0 File Offset: 0x000195F0
		public bool CalculateClockSkew(HttpWebResponse hwr)
		{
			object obj = hwr.Headers["Date"];
			DateTime serverTime;
			if (obj != null && DateTime.TryParse(obj.ToString(), out serverTime))
			{
				this.ServerTime = serverTime;
				this.ClockSkew = serverTime.ToUniversalTime() - DateTime.UtcNow;
				return true;
			}
			return false;
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0001B441 File Offset: 0x00019641
		// (set) Token: 0x06000362 RID: 866 RVA: 0x0001B449 File Offset: 0x00019649
		public DateTime ServerTime { get; protected set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0001B452 File Offset: 0x00019652
		// (set) Token: 0x06000364 RID: 868 RVA: 0x0001B45A File Offset: 0x0001965A
		public TimeSpan ClockSkew { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0001B463 File Offset: 0x00019663
		// (set) Token: 0x06000366 RID: 870 RVA: 0x0001B46B File Offset: 0x0001966B
		public TimeSpan ClockSkewThreshold { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0001B474 File Offset: 0x00019674
		// (set) Token: 0x06000368 RID: 872 RVA: 0x0001B47C File Offset: 0x0001967C
		public long Latency { get; protected set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0001B485 File Offset: 0x00019685
		// (set) Token: 0x0600036A RID: 874 RVA: 0x0001B48D File Offset: 0x0001968D
		public long RPSParseLatency { get; protected set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0001B496 File Offset: 0x00019696
		// (set) Token: 0x0600036C RID: 876 RVA: 0x0001B49E File Offset: 0x0001969E
		public long SSLConnectionLatency { get; protected set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0001B4A7 File Offset: 0x000196A7
		// (set) Token: 0x0600036E RID: 878 RVA: 0x0001B4AF File Offset: 0x000196AF
		public bool IsBadCredentials { get; protected set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0001B4B8 File Offset: 0x000196B8
		// (set) Token: 0x06000370 RID: 880 RVA: 0x0001B4C0 File Offset: 0x000196C0
		public bool IsExpiredCreds { get; protected set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0001B4C9 File Offset: 0x000196C9
		// (set) Token: 0x06000372 RID: 882 RVA: 0x0001B4D1 File Offset: 0x000196D1
		public bool AppPasswordRequired { get; protected set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0001B4DA File Offset: 0x000196DA
		// (set) Token: 0x06000374 RID: 884 RVA: 0x0001B4E2 File Offset: 0x000196E2
		public bool IsAccountNotProvisioned { get; protected set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0001B4EB File Offset: 0x000196EB
		// (set) Token: 0x06000376 RID: 886 RVA: 0x0001B4F3 File Offset: 0x000196F3
		public bool IsUnfamiliarLocation { get; protected set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0001B4FC File Offset: 0x000196FC
		// (set) Token: 0x06000378 RID: 888 RVA: 0x0001B504 File Offset: 0x00019704
		public string RecoveryUrl { get; protected set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0001B50D File Offset: 0x0001970D
		// (set) Token: 0x0600037A RID: 890 RVA: 0x0001B515 File Offset: 0x00019715
		public bool PossibleClockSkew { get; protected set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0001B51E File Offset: 0x0001971E
		// (set) Token: 0x0600037C RID: 892 RVA: 0x0001B526 File Offset: 0x00019726
		public string ErrorString { get; protected internal set; }

		// Token: 0x0600037D RID: 893 RVA: 0x0001B52F File Offset: 0x0001972F
		protected static void WriteBytes(Stream stream, byte[] bytes)
		{
			stream.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x04000355 RID: 853
		protected NamespaceStats namespaceStats;

		// Token: 0x04000356 RID: 854
		protected int traceId;
	}
}
