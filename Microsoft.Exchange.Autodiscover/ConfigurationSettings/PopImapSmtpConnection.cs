using System;

namespace Microsoft.Exchange.Autodiscover.ConfigurationSettings
{
	// Token: 0x0200003D RID: 61
	public class PopImapSmtpConnection
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000875F File Offset: 0x0000695F
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00008767 File Offset: 0x00006967
		public string EncryptionMethod
		{
			get
			{
				return this.encryptionMethod;
			}
			set
			{
				this.encryptionMethod = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00008770 File Offset: 0x00006970
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x00008778 File Offset: 0x00006978
		public string Hostname
		{
			get
			{
				return this.hostname;
			}
			set
			{
				this.hostname = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00008781 File Offset: 0x00006981
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x00008789 File Offset: 0x00006989
		public int Port
		{
			get
			{
				return this.port;
			}
			set
			{
				this.port = value;
			}
		}

		// Token: 0x0400019B RID: 411
		private string encryptionMethod;

		// Token: 0x0400019C RID: 412
		private string hostname;

		// Token: 0x0400019D RID: 413
		private int port;
	}
}
