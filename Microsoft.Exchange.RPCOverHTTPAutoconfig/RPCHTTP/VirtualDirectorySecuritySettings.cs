using System;

namespace Microsoft.Exchange.Servicelets.RPCHTTP
{
	// Token: 0x0200000C RID: 12
	public sealed class VirtualDirectorySecuritySettings
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000042 RID: 66 RVA: 0x0000442D File Offset: 0x0000262D
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00004435 File Offset: 0x00002635
		public bool Anonymous
		{
			get
			{
				return this.anonymous;
			}
			set
			{
				this.anonymous = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000044 RID: 68 RVA: 0x0000443E File Offset: 0x0000263E
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00004446 File Offset: 0x00002646
		public bool Basic
		{
			get
			{
				return this.basic;
			}
			set
			{
				this.basic = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000046 RID: 70 RVA: 0x0000444F File Offset: 0x0000264F
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00004457 File Offset: 0x00002657
		public bool Windows
		{
			get
			{
				return this.windows;
			}
			set
			{
				this.windows = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00004460 File Offset: 0x00002660
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00004468 File Offset: 0x00002668
		public bool ClientCertificateMapping
		{
			get
			{
				return this.clientCertificateMapping;
			}
			set
			{
				this.clientCertificateMapping = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00004471 File Offset: 0x00002671
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00004479 File Offset: 0x00002679
		public bool Digest
		{
			get
			{
				return this.digest;
			}
			set
			{
				this.digest = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00004482 File Offset: 0x00002682
		// (set) Token: 0x0600004D RID: 77 RVA: 0x0000448A File Offset: 0x0000268A
		public bool IisClientCertificateMapping
		{
			get
			{
				return this.iisClientCertificateMapping;
			}
			set
			{
				this.iisClientCertificateMapping = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00004493 File Offset: 0x00002693
		// (set) Token: 0x0600004F RID: 79 RVA: 0x0000449B File Offset: 0x0000269B
		public bool NtlmProvider
		{
			get
			{
				return this.ntlmProvider;
			}
			set
			{
				this.windows = (this.windows || value);
				this.ntlmProvider = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000044B6 File Offset: 0x000026B6
		// (set) Token: 0x06000051 RID: 81 RVA: 0x000044BE File Offset: 0x000026BE
		public bool NegotiateProvider
		{
			get
			{
				return this.negotiateProvider;
			}
			set
			{
				this.windows = (this.windows || value);
				this.negotiateProvider = value;
			}
		}

		// Token: 0x04000046 RID: 70
		private bool anonymous;

		// Token: 0x04000047 RID: 71
		private bool basic;

		// Token: 0x04000048 RID: 72
		private bool windows;

		// Token: 0x04000049 RID: 73
		private bool clientCertificateMapping;

		// Token: 0x0400004A RID: 74
		private bool digest;

		// Token: 0x0400004B RID: 75
		private bool iisClientCertificateMapping;

		// Token: 0x0400004C RID: 76
		private bool ntlmProvider;

		// Token: 0x0400004D RID: 77
		private bool negotiateProvider;
	}
}
