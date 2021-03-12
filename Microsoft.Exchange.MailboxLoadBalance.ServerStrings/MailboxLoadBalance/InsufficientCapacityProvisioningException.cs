using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x0200001A RID: 26
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InsufficientCapacityProvisioningException : MailboxLoadBalancePermanentException
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00002F7D File Offset: 0x0000117D
		public InsufficientCapacityProvisioningException() : base(MigrationWorkflowServiceStrings.ErrorInsufficientCapacityProvisioning)
		{
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002F8A File Offset: 0x0000118A
		public InsufficientCapacityProvisioningException(Exception innerException) : base(MigrationWorkflowServiceStrings.ErrorInsufficientCapacityProvisioning, innerException)
		{
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002F98 File Offset: 0x00001198
		protected InsufficientCapacityProvisioningException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002FA2 File Offset: 0x000011A2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
