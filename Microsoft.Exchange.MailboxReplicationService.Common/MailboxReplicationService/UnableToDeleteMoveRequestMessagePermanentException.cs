using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002D2 RID: 722
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToDeleteMoveRequestMessagePermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060023D9 RID: 9177 RVA: 0x0004F1D0 File Offset: 0x0004D3D0
		public UnableToDeleteMoveRequestMessagePermanentException() : base(MrsStrings.UnableToDeleteMoveRequestMessage)
		{
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x0004F1DD File Offset: 0x0004D3DD
		public UnableToDeleteMoveRequestMessagePermanentException(Exception innerException) : base(MrsStrings.UnableToDeleteMoveRequestMessage, innerException)
		{
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x0004F1EB File Offset: 0x0004D3EB
		protected UnableToDeleteMoveRequestMessagePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x0004F1F5 File Offset: 0x0004D3F5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
