using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000337 RID: 823
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RestartMoveSignatureChangeTransientException : MailboxReplicationTransientException
	{
		// Token: 0x060025D0 RID: 9680 RVA: 0x00052292 File Offset: 0x00050492
		public RestartMoveSignatureChangeTransientException() : base(MrsStrings.ReportMoveRestartedDueToSignatureChange)
		{
		}

		// Token: 0x060025D1 RID: 9681 RVA: 0x0005229F File Offset: 0x0005049F
		public RestartMoveSignatureChangeTransientException(Exception innerException) : base(MrsStrings.ReportMoveRestartedDueToSignatureChange, innerException)
		{
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x000522AD File Offset: 0x000504AD
		protected RestartMoveSignatureChangeTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060025D3 RID: 9683 RVA: 0x000522B7 File Offset: 0x000504B7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
