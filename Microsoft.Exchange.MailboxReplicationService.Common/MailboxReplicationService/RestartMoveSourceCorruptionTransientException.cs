using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000338 RID: 824
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RestartMoveSourceCorruptionTransientException : MailboxReplicationTransientException
	{
		// Token: 0x060025D4 RID: 9684 RVA: 0x000522C1 File Offset: 0x000504C1
		public RestartMoveSourceCorruptionTransientException() : base(MrsStrings.ReportMoveRestartedDueToSourceCorruption)
		{
		}

		// Token: 0x060025D5 RID: 9685 RVA: 0x000522CE File Offset: 0x000504CE
		public RestartMoveSourceCorruptionTransientException(Exception innerException) : base(MrsStrings.ReportMoveRestartedDueToSourceCorruption, innerException)
		{
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x000522DC File Offset: 0x000504DC
		protected RestartMoveSourceCorruptionTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x000522E6 File Offset: 0x000504E6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
