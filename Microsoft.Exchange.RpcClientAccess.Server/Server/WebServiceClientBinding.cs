using System;
using System.Security.Principal;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x0200004C RID: 76
	internal class WebServiceClientBinding : ClientBinding
	{
		// Token: 0x060002BB RID: 699 RVA: 0x0000F1F4 File Offset: 0x0000D3F4
		public WebServiceClientBinding(string protocolSequence, WindowsIdentity userIdentity)
		{
			this.protocolSequence = protocolSequence;
			this.userIdentity = userIdentity;
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000F20A File Offset: 0x0000D40A
		public override Guid AssociationGuid
		{
			get
			{
				return Guid.Empty;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000F211 File Offset: 0x0000D411
		public override AuthenticationService AuthenticationType
		{
			get
			{
				return AuthenticationService.None;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000F214 File Offset: 0x0000D414
		public override string ClientAddress
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000F21B File Offset: 0x0000D41B
		public override string ClientEndpoint
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000F222 File Offset: 0x0000D422
		public override bool IsEncrypted
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000F225 File Offset: 0x0000D425
		public override string ProtocolSequence
		{
			get
			{
				return this.protocolSequence;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000F22D File Offset: 0x0000D42D
		public override string ServerAddress
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000F234 File Offset: 0x0000D434
		public override string MailboxId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000F237 File Offset: 0x0000D437
		internal override ClientSecurityContext GetClientSecurityContext()
		{
			if (this.userIdentity == null)
			{
				return null;
			}
			return new ClientSecurityContext(this.userIdentity);
		}

		// Token: 0x0400016B RID: 363
		private readonly string protocolSequence;

		// Token: 0x0400016C RID: 364
		private readonly WindowsIdentity userIdentity;
	}
}
