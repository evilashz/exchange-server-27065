using System;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000A97 RID: 2711
	internal class SSLChainPolicyParameters : ChainPolicyParameters
	{
		// Token: 0x06003A75 RID: 14965 RVA: 0x0009564C File Offset: 0x0009384C
		public SSLChainPolicyParameters(string name, ChainPolicyOptions options, SSLPolicyAuthorizationType type) : this(name, options, SSLPolicyAuthorizationOptions.None, type)
		{
		}

		// Token: 0x06003A76 RID: 14966 RVA: 0x00095658 File Offset: 0x00093858
		public SSLChainPolicyParameters(string name, ChainPolicyOptions options, SSLPolicyAuthorizationOptions policy, SSLPolicyAuthorizationType type) : base(options)
		{
			this.serverName = name;
			this.type = type;
			this.options = policy;
		}

		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x06003A77 RID: 14967 RVA: 0x00095677 File Offset: 0x00093877
		// (set) Token: 0x06003A78 RID: 14968 RVA: 0x0009567F File Offset: 0x0009387F
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
			set
			{
				this.serverName = value;
			}
		}

		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x06003A79 RID: 14969 RVA: 0x00095688 File Offset: 0x00093888
		// (set) Token: 0x06003A7A RID: 14970 RVA: 0x00095690 File Offset: 0x00093890
		public SSLPolicyAuthorizationOptions Options
		{
			get
			{
				return this.options;
			}
			set
			{
				this.options = value;
			}
		}

		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x06003A7B RID: 14971 RVA: 0x00095699 File Offset: 0x00093899
		// (set) Token: 0x06003A7C RID: 14972 RVA: 0x000956A1 File Offset: 0x000938A1
		public SSLPolicyAuthorizationType Type
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

		// Token: 0x040032C1 RID: 12993
		private string serverName;

		// Token: 0x040032C2 RID: 12994
		private SSLPolicyAuthorizationOptions options;

		// Token: 0x040032C3 RID: 12995
		private SSLPolicyAuthorizationType type;
	}
}
