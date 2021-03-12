using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000378 RID: 888
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GetIdsFromNamesCalledOnDestinationException : MailboxReplicationPermanentException
	{
		// Token: 0x06002713 RID: 10003 RVA: 0x00054154 File Offset: 0x00052354
		public GetIdsFromNamesCalledOnDestinationException() : base(MrsStrings.GetIdsFromNamesCalledOnDestination)
		{
		}

		// Token: 0x06002714 RID: 10004 RVA: 0x00054161 File Offset: 0x00052361
		public GetIdsFromNamesCalledOnDestinationException(Exception innerException) : base(MrsStrings.GetIdsFromNamesCalledOnDestination, innerException)
		{
		}

		// Token: 0x06002715 RID: 10005 RVA: 0x0005416F File Offset: 0x0005236F
		protected GetIdsFromNamesCalledOnDestinationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002716 RID: 10006 RVA: 0x00054179 File Offset: 0x00052379
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
