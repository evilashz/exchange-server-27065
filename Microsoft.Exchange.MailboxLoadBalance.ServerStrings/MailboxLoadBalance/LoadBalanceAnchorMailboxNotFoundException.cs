using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x0200000A RID: 10
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class LoadBalanceAnchorMailboxNotFoundException : MailboxLoadBalancePermanentException
	{
		// Token: 0x0600002D RID: 45 RVA: 0x000025FB File Offset: 0x000007FB
		public LoadBalanceAnchorMailboxNotFoundException(string capability) : base(MigrationWorkflowServiceStrings.ErrorMissingAnchorMailbox(capability))
		{
			this.capability = capability;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002610 File Offset: 0x00000810
		public LoadBalanceAnchorMailboxNotFoundException(string capability, Exception innerException) : base(MigrationWorkflowServiceStrings.ErrorMissingAnchorMailbox(capability), innerException)
		{
			this.capability = capability;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002626 File Offset: 0x00000826
		protected LoadBalanceAnchorMailboxNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.capability = (string)info.GetValue("capability", typeof(string));
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002650 File Offset: 0x00000850
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("capability", this.capability);
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000266B File Offset: 0x0000086B
		public string Capability
		{
			get
			{
				return this.capability;
			}
		}

		// Token: 0x0400001E RID: 30
		private readonly string capability;
	}
}
