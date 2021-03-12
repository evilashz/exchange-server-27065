using System;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x0200002E RID: 46
	public class ConnectEventArgs : ReceiveEventArgs
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00005D25 File Offset: 0x00003F25
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00005D32 File Offset: 0x00003F32
		public SmtpResponse Banner
		{
			get
			{
				return base.SmtpSession.Banner;
			}
			set
			{
				if (value.Equals(SmtpResponse.Empty))
				{
					throw new ArgumentException("value of banner cannot be SmtpResponse.Emtpy", "value");
				}
				base.SmtpSession.Banner = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00005D5E File Offset: 0x00003F5E
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00005D6B File Offset: 0x00003F6B
		public bool DisableStartTls
		{
			get
			{
				return base.SmtpSession.DisableStartTls;
			}
			set
			{
				base.SmtpSession.DisableStartTls = value;
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005D79 File Offset: 0x00003F79
		internal ConnectEventArgs(SmtpSession smtpSession) : base(smtpSession)
		{
		}
	}
}
