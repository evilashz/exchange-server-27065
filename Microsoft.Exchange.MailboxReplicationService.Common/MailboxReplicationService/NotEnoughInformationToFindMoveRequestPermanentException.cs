using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002D1 RID: 721
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NotEnoughInformationToFindMoveRequestPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060023D5 RID: 9173 RVA: 0x0004F1A1 File Offset: 0x0004D3A1
		public NotEnoughInformationToFindMoveRequestPermanentException() : base(MrsStrings.NotEnoughInformationToFindMoveRequest)
		{
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x0004F1AE File Offset: 0x0004D3AE
		public NotEnoughInformationToFindMoveRequestPermanentException(Exception innerException) : base(MrsStrings.NotEnoughInformationToFindMoveRequest, innerException)
		{
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x0004F1BC File Offset: 0x0004D3BC
		protected NotEnoughInformationToFindMoveRequestPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060023D8 RID: 9176 RVA: 0x0004F1C6 File Offset: 0x0004D3C6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
