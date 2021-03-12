using System;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x0200003C RID: 60
	internal class RpcHttpConnectionProperties
	{
		// Token: 0x06000220 RID: 544 RVA: 0x0000C2BB File Offset: 0x0000A4BB
		public RpcHttpConnectionProperties(string clientIp, string serverTarget, string sessionCookie, string[] requestIds)
		{
			this.clientIp = clientIp;
			this.serverTarget = serverTarget;
			this.sessionCookie = sessionCookie;
			this.requestIds = requestIds;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0000C2E0 File Offset: 0x0000A4E0
		public string ClientIp
		{
			get
			{
				return this.clientIp;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000C2E8 File Offset: 0x0000A4E8
		public string ServerTarget
		{
			get
			{
				return this.serverTarget;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000C2F0 File Offset: 0x0000A4F0
		public string SessionCookie
		{
			get
			{
				return this.sessionCookie;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000C2F8 File Offset: 0x0000A4F8
		public string[] RequestIds
		{
			get
			{
				return this.requestIds;
			}
		}

		// Token: 0x04000113 RID: 275
		private readonly string clientIp;

		// Token: 0x04000114 RID: 276
		private readonly string serverTarget;

		// Token: 0x04000115 RID: 277
		private readonly string sessionCookie;

		// Token: 0x04000116 RID: 278
		private readonly string[] requestIds;
	}
}
