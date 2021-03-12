using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002B1 RID: 689
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MoveJobDeserializationFailedPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002336 RID: 9014 RVA: 0x0004E269 File Offset: 0x0004C469
		public MoveJobDeserializationFailedPermanentException() : base(MrsStrings.MoveJobDeserializationFailed)
		{
		}

		// Token: 0x06002337 RID: 9015 RVA: 0x0004E276 File Offset: 0x0004C476
		public MoveJobDeserializationFailedPermanentException(Exception innerException) : base(MrsStrings.MoveJobDeserializationFailed, innerException)
		{
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x0004E284 File Offset: 0x0004C484
		protected MoveJobDeserializationFailedPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x0004E28E File Offset: 0x0004C48E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
