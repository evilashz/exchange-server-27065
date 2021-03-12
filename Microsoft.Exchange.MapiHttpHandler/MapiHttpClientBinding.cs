using System;
using System.Security.Principal;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000041 RID: 65
	internal class MapiHttpClientBinding : ClientBinding
	{
		// Token: 0x06000268 RID: 616 RVA: 0x0000E2BC File Offset: 0x0000C4BC
		public MapiHttpClientBinding(string mailboxId, string clientAddress, string serverAddress, bool isSecureConnection, IIdentity userIdentity, Func<ClientSecurityContext> clientSecurityContextGetter)
		{
			this.clientSecurityContextGetter = clientSecurityContextGetter;
			this.mailboxId = mailboxId;
			this.clientAddress = clientAddress;
			this.serverAddress = serverAddress;
			this.isSecureConnection = isSecureConnection;
			this.userIdentity = userIdentity;
			if (userIdentity == null)
			{
				throw ProtocolException.FromResponseCode((LID)64928, "User identity null", ResponseCode.AnonymousNotAllowed, null);
			}
			if (!(userIdentity is LiveIDIdentity) && !(userIdentity is SidBasedIdentity) && !(userIdentity is WindowsTokenIdentity) && !(userIdentity is WindowsIdentity))
			{
				throw ProtocolException.FromResponseCode((LID)58912, string.Format("Unable to determine user identity [{0}]", userIdentity.GetType().ToString()), ResponseCode.UnknownFailure, null);
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000E364 File Offset: 0x0000C564
		public override string ClientAddress
		{
			get
			{
				return this.clientAddress;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000E36C File Offset: 0x0000C56C
		public override string ServerAddress
		{
			get
			{
				return this.serverAddress;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000E374 File Offset: 0x0000C574
		public override string ClientEndpoint
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000E37B File Offset: 0x0000C57B
		public override bool IsEncrypted
		{
			get
			{
				return this.isSecureConnection;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000E383 File Offset: 0x0000C583
		public override string ProtocolSequence
		{
			get
			{
				return "MapiHttp";
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000E38A File Offset: 0x0000C58A
		public override AuthenticationService AuthenticationType
		{
			get
			{
				return AuthenticationService.None;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000E38D File Offset: 0x0000C58D
		public override Guid AssociationGuid
		{
			get
			{
				return this.associationGuid;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000E395 File Offset: 0x0000C595
		public override string MailboxId
		{
			get
			{
				return this.mailboxId;
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000E39D File Offset: 0x0000C59D
		internal void ClearClientSecurityContextGetter()
		{
			this.clientSecurityContextGetter = null;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000E3A8 File Offset: 0x0000C5A8
		internal override ClientSecurityContext GetClientSecurityContext()
		{
			Func<ClientSecurityContext> func = this.clientSecurityContextGetter;
			this.clientSecurityContextGetter = null;
			if (func != null)
			{
				ClientSecurityContext clientSecurityContext = func();
				if (clientSecurityContext != null)
				{
					return clientSecurityContext;
				}
			}
			return this.userIdentity.CreateClientSecurityContext(false);
		}

		// Token: 0x04000101 RID: 257
		private readonly string mailboxId;

		// Token: 0x04000102 RID: 258
		private readonly string clientAddress;

		// Token: 0x04000103 RID: 259
		private readonly string serverAddress;

		// Token: 0x04000104 RID: 260
		private readonly Guid associationGuid = Guid.NewGuid();

		// Token: 0x04000105 RID: 261
		private readonly bool isSecureConnection;

		// Token: 0x04000106 RID: 262
		private readonly IIdentity userIdentity;

		// Token: 0x04000107 RID: 263
		private Func<ClientSecurityContext> clientSecurityContextGetter;
	}
}
