using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x02000011 RID: 17
	public abstract class ClientBinding
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060005FD RID: 1533
		public abstract string ClientAddress { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060005FE RID: 1534
		public abstract string ServerAddress { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060005FF RID: 1535
		public abstract string ClientEndpoint { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000600 RID: 1536
		public abstract bool IsEncrypted { [return: MarshalAs(UnmanagedType.U1)] get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000601 RID: 1537
		public abstract string ProtocolSequence { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000602 RID: 1538
		public abstract AuthenticationService AuthenticationType { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000603 RID: 1539
		public abstract Guid AssociationGuid { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000604 RID: 1540
		public abstract string MailboxId { get; }

		// Token: 0x06000605 RID: 1541
		internal abstract ClientSecurityContext GetClientSecurityContext();

		// Token: 0x06000606 RID: 1542 RVA: 0x00001688 File Offset: 0x00000A88
		public ClientBinding()
		{
		}
	}
}
