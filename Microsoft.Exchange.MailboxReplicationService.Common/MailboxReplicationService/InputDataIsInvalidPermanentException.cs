using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002BB RID: 699
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InputDataIsInvalidPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600236D RID: 9069 RVA: 0x0004E8E9 File Offset: 0x0004CAE9
		public InputDataIsInvalidPermanentException() : base(MrsStrings.InputDataIsInvalid)
		{
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x0004E8F6 File Offset: 0x0004CAF6
		public InputDataIsInvalidPermanentException(Exception innerException) : base(MrsStrings.InputDataIsInvalid, innerException)
		{
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x0004E904 File Offset: 0x0004CB04
		protected InputDataIsInvalidPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x0004E90E File Offset: 0x0004CB0E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
