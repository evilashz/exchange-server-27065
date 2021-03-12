using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000324 RID: 804
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidProxyOperationOrderPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600256A RID: 9578 RVA: 0x000516FF File Offset: 0x0004F8FF
		public InvalidProxyOperationOrderPermanentException() : base(MrsStrings.InvalidProxyOperationOrder)
		{
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x0005170C File Offset: 0x0004F90C
		public InvalidProxyOperationOrderPermanentException(Exception innerException) : base(MrsStrings.InvalidProxyOperationOrder, innerException)
		{
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x0005171A File Offset: 0x0004F91A
		protected InvalidProxyOperationOrderPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x00051724 File Offset: 0x0004F924
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
