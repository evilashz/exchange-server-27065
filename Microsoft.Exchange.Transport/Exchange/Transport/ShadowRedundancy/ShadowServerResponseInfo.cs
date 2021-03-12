using System;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Transport.ShadowRedundancy
{
	// Token: 0x02000393 RID: 915
	internal class ShadowServerResponseInfo
	{
		// Token: 0x06002863 RID: 10339 RVA: 0x0009DC14 File Offset: 0x0009BE14
		public ShadowServerResponseInfo(string shadowServer, SmtpResponse response)
		{
			this.shadowServer = shadowServer;
			this.response = response;
		}

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x06002864 RID: 10340 RVA: 0x0009DC2A File Offset: 0x0009BE2A
		public string ShadowServer
		{
			get
			{
				return this.shadowServer;
			}
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x06002865 RID: 10341 RVA: 0x0009DC32 File Offset: 0x0009BE32
		public SmtpResponse Response
		{
			get
			{
				return this.response;
			}
		}

		// Token: 0x06002866 RID: 10342 RVA: 0x0009DC3A File Offset: 0x0009BE3A
		public override string ToString()
		{
			return string.Format("{0}={1}", this.shadowServer, this.response);
		}

		// Token: 0x04001467 RID: 5223
		private readonly string shadowServer;

		// Token: 0x04001468 RID: 5224
		private readonly SmtpResponse response;
	}
}
