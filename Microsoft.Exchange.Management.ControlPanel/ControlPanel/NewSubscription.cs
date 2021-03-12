using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200029F RID: 671
	[DataContract]
	public class NewSubscription : PimSubscriptionParameter
	{
		// Token: 0x06002B78 RID: 11128 RVA: 0x00087C89 File Offset: 0x00085E89
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			base["Name"] = base.EmailAddress;
		}

		// Token: 0x17001D73 RID: 7539
		// (get) Token: 0x06002B79 RID: 11129 RVA: 0x00087C9C File Offset: 0x00085E9C
		public override string AssociatedCmdlet
		{
			get
			{
				return "New-Subscription";
			}
		}

		// Token: 0x17001D74 RID: 7540
		// (get) Token: 0x06002B7A RID: 11130 RVA: 0x00087CA3 File Offset: 0x00085EA3
		public override string RbacScope
		{
			get
			{
				return "@W:Self";
			}
		}

		// Token: 0x0400219D RID: 8605
		public new const string RbacParameters = "?Mailbox&Force&DisplayName&Name";

		// Token: 0x0400219E RID: 8606
		public new const string RbacParametersWithIdentity = "?Mailbox&Force&DisplayName&Name&Identity";
	}
}
