using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x02000015 RID: 21
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class AutomaticMailboxLoadBalancingNotAllowedException : MailboxLoadBalancePermanentException
	{
		// Token: 0x06000068 RID: 104 RVA: 0x00002C71 File Offset: 0x00000E71
		public AutomaticMailboxLoadBalancingNotAllowedException() : base(MigrationWorkflowServiceStrings.ErrorAutomaticMailboxLoadBalancingNotAllowed)
		{
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002C7E File Offset: 0x00000E7E
		public AutomaticMailboxLoadBalancingNotAllowedException(Exception innerException) : base(MigrationWorkflowServiceStrings.ErrorAutomaticMailboxLoadBalancingNotAllowed, innerException)
		{
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002C8C File Offset: 0x00000E8C
		protected AutomaticMailboxLoadBalancingNotAllowedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002C96 File Offset: 0x00000E96
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
