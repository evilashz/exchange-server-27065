using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002D5 RID: 725
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MoveRequestMissingInfoSavePermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060023E5 RID: 9189 RVA: 0x0004F25D File Offset: 0x0004D45D
		public MoveRequestMissingInfoSavePermanentException() : base(MrsStrings.MoveRequestMissingInfoSave)
		{
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x0004F26A File Offset: 0x0004D46A
		public MoveRequestMissingInfoSavePermanentException(Exception innerException) : base(MrsStrings.MoveRequestMissingInfoSave, innerException)
		{
		}

		// Token: 0x060023E7 RID: 9191 RVA: 0x0004F278 File Offset: 0x0004D478
		protected MoveRequestMissingInfoSavePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x0004F282 File Offset: 0x0004D482
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
