using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x0200001F RID: 31
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CmdletPoolExhaustedException : MailboxLoadBalancePermanentException
	{
		// Token: 0x0600009B RID: 155 RVA: 0x00003199 File Offset: 0x00001399
		public CmdletPoolExhaustedException() : base(MigrationWorkflowServiceStrings.ErrorCmdletPoolExhausted)
		{
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000031A6 File Offset: 0x000013A6
		public CmdletPoolExhaustedException(Exception innerException) : base(MigrationWorkflowServiceStrings.ErrorCmdletPoolExhausted, innerException)
		{
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000031B4 File Offset: 0x000013B4
		protected CmdletPoolExhaustedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000031BE File Offset: 0x000013BE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
