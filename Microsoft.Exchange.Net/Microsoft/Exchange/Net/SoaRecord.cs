using System;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000C69 RID: 3177
	internal class SoaRecord
	{
		// Token: 0x06004664 RID: 18020 RVA: 0x000BC749 File Offset: 0x000BA949
		public SoaRecord(string primaryServer, string administrator, int serialNumber, int refresh, int retry, int expire, int defaultTimeToLive)
		{
			this.primaryServer = primaryServer;
			this.administrator = administrator;
			this.serialNumber = serialNumber;
			this.refresh = refresh;
			this.retry = retry;
			this.expire = expire;
			this.defaultTimeToLive = defaultTimeToLive;
		}

		// Token: 0x06004665 RID: 18021 RVA: 0x000BC786 File Offset: 0x000BA986
		private SoaRecord()
		{
		}

		// Token: 0x170011B6 RID: 4534
		// (get) Token: 0x06004666 RID: 18022 RVA: 0x000BC78E File Offset: 0x000BA98E
		public string PrimaryServer
		{
			get
			{
				return this.primaryServer;
			}
		}

		// Token: 0x170011B7 RID: 4535
		// (get) Token: 0x06004667 RID: 18023 RVA: 0x000BC796 File Offset: 0x000BA996
		public string Administrator
		{
			get
			{
				return this.administrator;
			}
		}

		// Token: 0x170011B8 RID: 4536
		// (get) Token: 0x06004668 RID: 18024 RVA: 0x000BC79E File Offset: 0x000BA99E
		public int SerialNumber
		{
			get
			{
				return this.serialNumber;
			}
		}

		// Token: 0x170011B9 RID: 4537
		// (get) Token: 0x06004669 RID: 18025 RVA: 0x000BC7A6 File Offset: 0x000BA9A6
		public int Refresh
		{
			get
			{
				return this.refresh;
			}
		}

		// Token: 0x170011BA RID: 4538
		// (get) Token: 0x0600466A RID: 18026 RVA: 0x000BC7AE File Offset: 0x000BA9AE
		public int Retry
		{
			get
			{
				return this.retry;
			}
		}

		// Token: 0x170011BB RID: 4539
		// (get) Token: 0x0600466B RID: 18027 RVA: 0x000BC7B6 File Offset: 0x000BA9B6
		public int Expire
		{
			get
			{
				return this.expire;
			}
		}

		// Token: 0x170011BC RID: 4540
		// (get) Token: 0x0600466C RID: 18028 RVA: 0x000BC7BE File Offset: 0x000BA9BE
		public int DefaultTimeToLive
		{
			get
			{
				return this.defaultTimeToLive;
			}
		}

		// Token: 0x04003AB1 RID: 15025
		private readonly string primaryServer;

		// Token: 0x04003AB2 RID: 15026
		private readonly string administrator;

		// Token: 0x04003AB3 RID: 15027
		private readonly int serialNumber;

		// Token: 0x04003AB4 RID: 15028
		private readonly int refresh;

		// Token: 0x04003AB5 RID: 15029
		private readonly int retry;

		// Token: 0x04003AB6 RID: 15030
		private readonly int expire;

		// Token: 0x04003AB7 RID: 15031
		private readonly int defaultTimeToLive;
	}
}
