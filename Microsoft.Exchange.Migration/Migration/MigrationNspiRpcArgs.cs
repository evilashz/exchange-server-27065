using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Migration.DataAccessLayer;
using Microsoft.Exchange.Nspi;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000E4 RID: 228
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class MigrationNspiRpcArgs : MigrationProxyRpcArgs
	{
		// Token: 0x06000BBD RID: 3005 RVA: 0x00033EC4 File Offset: 0x000320C4
		protected MigrationNspiRpcArgs(ExchangeOutlookAnywhereEndpoint endpoint, MigrationProxyRpcType type) : base(endpoint.Username, endpoint.EncryptedPassword, endpoint.Domain, type)
		{
			this.RpcHostServer = endpoint.NspiServer;
			this.RpcProxyServer = endpoint.RpcProxyServer;
			this.RpcAuthenticationMethod = ((endpoint.AuthenticationMethod == AuthenticationMethod.Ntlm) ? HTTPAuthentication.Ntlm : HTTPAuthentication.Basic);
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x00033F1A File Offset: 0x0003211A
		protected MigrationNspiRpcArgs(byte[] requestBlob, MigrationProxyRpcType type) : base(requestBlob, type)
		{
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000BBF RID: 3007 RVA: 0x00033F24 File Offset: 0x00032124
		// (set) Token: 0x06000BC0 RID: 3008 RVA: 0x00033F31 File Offset: 0x00032131
		public string RpcHostServer
		{
			get
			{
				return base.GetProperty<string>(2415984671U);
			}
			set
			{
				base.SetPropertyAsString(2415984671U, value);
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x00033F3F File Offset: 0x0003213F
		// (set) Token: 0x06000BC2 RID: 3010 RVA: 0x00033F4C File Offset: 0x0003214C
		public string RpcProxyServer
		{
			get
			{
				return base.GetProperty<string>(2416050207U);
			}
			set
			{
				base.SetPropertyAsString(2416050207U, value);
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x00033F5C File Offset: 0x0003215C
		// (set) Token: 0x06000BC4 RID: 3012 RVA: 0x00033F94 File Offset: 0x00032194
		public HTTPAuthentication RpcAuthenticationMethod
		{
			get
			{
				string property = base.GetProperty<string>(2416836639U);
				if (!string.IsNullOrEmpty(property))
				{
					return (HTTPAuthentication)Enum.Parse(typeof(HTTPAuthentication), property);
				}
				return HTTPAuthentication.Basic;
			}
			set
			{
				base.SetPropertyAsString(2416836639U, value.ToString());
			}
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x00033FAC File Offset: 0x000321AC
		public override bool Validate(out string errorMsg)
		{
			if (!base.Validate(out errorMsg))
			{
				return false;
			}
			if (string.IsNullOrEmpty(this.RpcHostServer))
			{
				errorMsg = "RPC Host cannot be null or empty.";
				return false;
			}
			if (string.IsNullOrEmpty(this.RpcProxyServer))
			{
				errorMsg = "RPC Http Proxy cannot be null or empty.";
				return false;
			}
			return true;
		}
	}
}
