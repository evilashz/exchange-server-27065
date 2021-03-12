using System;

namespace Microsoft.Exchange.Autodiscover.ConfigurationSettings
{
	// Token: 0x0200002F RID: 47
	public class AlternateMailbox
	{
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00007E04 File Offset: 0x00006004
		// (set) Token: 0x06000163 RID: 355 RVA: 0x00007E0C File Offset: 0x0000600C
		public string Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00007E15 File Offset: 0x00006015
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00007E1D File Offset: 0x0000601D
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.displayName = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00007E26 File Offset: 0x00006026
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00007E2E File Offset: 0x0000602E
		public string LegacyDN
		{
			get
			{
				return this.legacyDN;
			}
			set
			{
				this.legacyDN = value;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00007E37 File Offset: 0x00006037
		// (set) Token: 0x06000169 RID: 361 RVA: 0x00007E3F File Offset: 0x0000603F
		public string Server
		{
			get
			{
				return this.server;
			}
			set
			{
				this.server = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00007E48 File Offset: 0x00006048
		// (set) Token: 0x0600016B RID: 363 RVA: 0x00007E50 File Offset: 0x00006050
		public string SmtpAddress
		{
			get
			{
				return this.smtpAddress;
			}
			set
			{
				this.smtpAddress = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00007E59 File Offset: 0x00006059
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00007E61 File Offset: 0x00006061
		public string OwnerSmtpAddress
		{
			get
			{
				return this.ownerSmtpAddress;
			}
			set
			{
				this.ownerSmtpAddress = value;
			}
		}

		// Token: 0x04000176 RID: 374
		private string type;

		// Token: 0x04000177 RID: 375
		private string displayName;

		// Token: 0x04000178 RID: 376
		private string legacyDN;

		// Token: 0x04000179 RID: 377
		private string server;

		// Token: 0x0400017A RID: 378
		private string smtpAddress;

		// Token: 0x0400017B RID: 379
		private string ownerSmtpAddress;
	}
}
