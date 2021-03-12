using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x02000013 RID: 19
	public class RpcClientBinding : ClientBinding
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x000016B0 File Offset: 0x00000AB0
		public override string ClientAddress
		{
			get
			{
				return this.clientAddress;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x000016C4 File Offset: 0x00000AC4
		public override string ServerAddress
		{
			get
			{
				return this.serverAddress;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x000016D8 File Offset: 0x00000AD8
		public override string ClientEndpoint
		{
			get
			{
				return this.clientEndpoint;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x000016EC File Offset: 0x00000AEC
		public override bool IsEncrypted
		{
			[return: MarshalAs(UnmanagedType.U1)]
			get
			{
				return this.isEncrypted;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x00001700 File Offset: 0x00000B00
		public override string ProtocolSequence
		{
			get
			{
				return this.protocolSequence;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x00001714 File Offset: 0x00000B14
		public override AuthenticationService AuthenticationType
		{
			get
			{
				return this.authenticationType;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x00001728 File Offset: 0x00000B28
		public override Guid AssociationGuid
		{
			get
			{
				return this.associationGuid;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x00001740 File Offset: 0x00000B40
		public override string MailboxId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00001750 File Offset: 0x00000B50
		internal RpcClientBinding(IntPtr binding, SafeRpcAsyncStateHandle asyncState)
		{
			this.bindingHandle = binding;
			RpcServerBase.GetBindingInformation(this.bindingHandle, out this.clientAddress, out this.clientEndpoint, out this.serverAddress, out this.serverEndpoint, out this.protocolSequence);
			if (string.Equals(this.protocolSequence, "ncacn_http", StringComparison.InvariantCultureIgnoreCase))
			{
				Guid clientIdentifier = RpcServerBase.GetClientIdentifier(this.bindingHandle.ToPointer());
				this.associationGuid = clientIdentifier;
			}
			else
			{
				this.associationGuid = Guid.Empty;
			}
			this.authenticationType = RpcServerBase.GetAuthenticationType(this.bindingHandle.ToPointer());
			this.isEncrypted = RpcServerBase.IsConnectionEncrypted(this.bindingHandle.ToPointer());
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x000017FC File Offset: 0x00000BFC
		internal override ClientSecurityContext GetClientSecurityContext()
		{
			if (this.bindingHandle != IntPtr.Zero)
			{
				return RpcServerBase.GetClientSecurityContext(this.bindingHandle);
			}
			return null;
		}

		// Token: 0x0400082C RID: 2092
		private IntPtr bindingHandle;

		// Token: 0x0400082D RID: 2093
		private AuthenticationService authenticationType;

		// Token: 0x0400082E RID: 2094
		private string protocolSequence;

		// Token: 0x0400082F RID: 2095
		private string clientAddress;

		// Token: 0x04000830 RID: 2096
		private string serverAddress;

		// Token: 0x04000831 RID: 2097
		private string clientEndpoint;

		// Token: 0x04000832 RID: 2098
		private string serverEndpoint;

		// Token: 0x04000833 RID: 2099
		private bool isEncrypted;

		// Token: 0x04000834 RID: 2100
		private Guid associationGuid;

		// Token: 0x04000835 RID: 2101
		private ClientSecurityContext clientSecurityContext;
	}
}
