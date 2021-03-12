using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002D6 RID: 726
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MoveRequestMissingInfoDeletePermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060023E9 RID: 9193 RVA: 0x0004F28C File Offset: 0x0004D48C
		public MoveRequestMissingInfoDeletePermanentException() : base(MrsStrings.MoveRequestMissingInfoDelete)
		{
		}

		// Token: 0x060023EA RID: 9194 RVA: 0x0004F299 File Offset: 0x0004D499
		public MoveRequestMissingInfoDeletePermanentException(Exception innerException) : base(MrsStrings.MoveRequestMissingInfoDelete, innerException)
		{
		}

		// Token: 0x060023EB RID: 9195 RVA: 0x0004F2A7 File Offset: 0x0004D4A7
		protected MoveRequestMissingInfoDeletePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060023EC RID: 9196 RVA: 0x0004F2B1 File Offset: 0x0004D4B1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
