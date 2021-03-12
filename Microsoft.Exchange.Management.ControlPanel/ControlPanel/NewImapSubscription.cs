using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002B4 RID: 692
	[DataContract]
	public class NewImapSubscription : ImapSubscriptionBaseParameter
	{
		// Token: 0x06002BF2 RID: 11250 RVA: 0x00088734 File Offset: 0x00086934
		public NewImapSubscription()
		{
			this.OnDeserialized(default(StreamingContext));
		}

		// Token: 0x17001DA8 RID: 7592
		// (get) Token: 0x06002BF3 RID: 11251 RVA: 0x00088756 File Offset: 0x00086956
		public override string AssociatedCmdlet
		{
			get
			{
				return "New-ImapSubscription";
			}
		}

		// Token: 0x17001DA9 RID: 7593
		// (get) Token: 0x06002BF4 RID: 11252 RVA: 0x0008875D File Offset: 0x0008695D
		public override string RbacScope
		{
			get
			{
				return "@W:Self";
			}
		}

		// Token: 0x06002BF5 RID: 11253 RVA: 0x00088764 File Offset: 0x00086964
		[OnDeserialized]
		private void OnDeserialized(StreamingContext contex)
		{
			base["Name"] = base.EmailAddress;
		}

		// Token: 0x040021BF RID: 8639
		public new const string RbacParameters = "?Mailbox&Name";

		// Token: 0x040021C0 RID: 8640
		public new const string RbacParametersWithIdentity = "?Mailbox&Name&Identity";
	}
}
