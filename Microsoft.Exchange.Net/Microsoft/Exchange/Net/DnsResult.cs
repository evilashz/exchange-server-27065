using System;
using System.Net;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000C05 RID: 3077
	internal class DnsResult
	{
		// Token: 0x06004356 RID: 17238 RVA: 0x000B4F78 File Offset: 0x000B3178
		internal DnsResult(DnsStatus status, IPAddress server, TimeSpan timeToLive) : this(status, server, null, timeToLive)
		{
		}

		// Token: 0x06004357 RID: 17239 RVA: 0x000B4F84 File Offset: 0x000B3184
		internal DnsResult(DnsStatus status, IPAddress server, DnsRecordList list, TimeSpan timeToLive)
		{
			this.status = status;
			this.list = list;
			this.server = server;
			if (timeToLive == DnsResult.NoExpiration)
			{
				this.expires = DnsResult.PermanentEntryDate;
			}
			else
			{
				this.expires = DateTime.UtcNow + timeToLive;
			}
			this.UpdateLastAccess();
		}

		// Token: 0x170010D6 RID: 4310
		// (get) Token: 0x06004358 RID: 17240 RVA: 0x000B4FDF File Offset: 0x000B31DF
		internal DnsStatus Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x170010D7 RID: 4311
		// (get) Token: 0x06004359 RID: 17241 RVA: 0x000B4FE7 File Offset: 0x000B31E7
		internal DnsRecordList List
		{
			get
			{
				return this.list;
			}
		}

		// Token: 0x170010D8 RID: 4312
		// (get) Token: 0x0600435A RID: 17242 RVA: 0x000B4FEF File Offset: 0x000B31EF
		internal IPAddress Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x170010D9 RID: 4313
		// (get) Token: 0x0600435B RID: 17243 RVA: 0x000B4FF7 File Offset: 0x000B31F7
		// (set) Token: 0x0600435C RID: 17244 RVA: 0x000B4FFF File Offset: 0x000B31FF
		internal DateTime LastAccess
		{
			get
			{
				return this.lastAccess;
			}
			set
			{
				this.lastAccess = value;
			}
		}

		// Token: 0x170010DA RID: 4314
		// (get) Token: 0x0600435D RID: 17245 RVA: 0x000B5008 File Offset: 0x000B3208
		// (set) Token: 0x0600435E RID: 17246 RVA: 0x000B5010 File Offset: 0x000B3210
		internal DateTime Expires
		{
			get
			{
				return this.expires;
			}
			set
			{
				this.expires = value;
			}
		}

		// Token: 0x170010DB RID: 4315
		// (get) Token: 0x0600435F RID: 17247 RVA: 0x000B5019 File Offset: 0x000B3219
		internal bool HasExpired
		{
			get
			{
				return this.expires <= DateTime.UtcNow;
			}
		}

		// Token: 0x170010DC RID: 4316
		// (get) Token: 0x06004360 RID: 17248 RVA: 0x000B502B File Offset: 0x000B322B
		internal bool IsPermanentEntry
		{
			get
			{
				return this.expires == DnsResult.PermanentEntryDate;
			}
		}

		// Token: 0x06004361 RID: 17249 RVA: 0x000B503D File Offset: 0x000B323D
		internal static TimeSpan TimeToLiveWithinLimits(TimeSpan timeToLive)
		{
			if (timeToLive < DnsResult.MinTimeToLive)
			{
				return DnsResult.MinTimeToLive;
			}
			if (timeToLive > DnsResult.MaxTimeToLive)
			{
				return DnsResult.MaxTimeToLive;
			}
			return timeToLive;
		}

		// Token: 0x06004362 RID: 17250 RVA: 0x000B5066 File Offset: 0x000B3266
		internal void UpdateLastAccess()
		{
			if (!this.IsPermanentEntry)
			{
				this.lastAccess = DateTime.UtcNow;
			}
		}

		// Token: 0x06004363 RID: 17251 RVA: 0x000B507C File Offset: 0x000B327C
		public override string ToString()
		{
			return string.Format("Server={0}; Status={1}; Results={2}; Expires={3}", new object[]
			{
				this.server,
				this.Status,
				this.List,
				this.expires
			});
		}

		// Token: 0x04003956 RID: 14678
		internal static readonly TimeSpan DefaultTimeToLive = TimeSpan.FromMinutes(60.0);

		// Token: 0x04003957 RID: 14679
		internal static readonly TimeSpan MinTimeToLive = TimeSpan.FromMinutes(5.0);

		// Token: 0x04003958 RID: 14680
		internal static readonly TimeSpan MaxTimeToLive = TimeSpan.FromMinutes(60.0);

		// Token: 0x04003959 RID: 14681
		internal static readonly TimeSpan ErrorTimeToLive = TimeSpan.FromMinutes(5.0);

		// Token: 0x0400395A RID: 14682
		internal static readonly TimeSpan NoExpiration = TimeSpan.MaxValue;

		// Token: 0x0400395B RID: 14683
		private static readonly DateTime PermanentEntryDate = DateTime.MaxValue;

		// Token: 0x0400395C RID: 14684
		private readonly DnsStatus status;

		// Token: 0x0400395D RID: 14685
		private readonly DnsRecordList list;

		// Token: 0x0400395E RID: 14686
		private IPAddress server;

		// Token: 0x0400395F RID: 14687
		private DateTime expires;

		// Token: 0x04003960 RID: 14688
		private DateTime lastAccess;
	}
}
