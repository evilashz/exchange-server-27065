using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002D6 RID: 726
	[DataContract]
	public class NewPopSubscription : PopSubscriptionBaseParameter
	{
		// Token: 0x06002CB5 RID: 11445 RVA: 0x00089988 File Offset: 0x00087B88
		public NewPopSubscription()
		{
			this.OnDeserialized(default(StreamingContext));
		}

		// Token: 0x17001DFA RID: 7674
		// (get) Token: 0x06002CB6 RID: 11446 RVA: 0x000899AA File Offset: 0x00087BAA
		public override string AssociatedCmdlet
		{
			get
			{
				return "New-PopSubscription";
			}
		}

		// Token: 0x17001DFB RID: 7675
		// (get) Token: 0x06002CB7 RID: 11447 RVA: 0x000899B1 File Offset: 0x00087BB1
		public override string RbacScope
		{
			get
			{
				return "@W:Self";
			}
		}

		// Token: 0x06002CB8 RID: 11448 RVA: 0x000899B8 File Offset: 0x00087BB8
		[OnDeserialized]
		private void OnDeserialized(StreamingContext contex)
		{
			base["Name"] = base.EmailAddress;
		}

		// Token: 0x04002203 RID: 8707
		public new const string RbacParameters = "?Mailbox&Name";

		// Token: 0x04002204 RID: 8708
		public new const string RbacParametersWithIdentity = "?Mailbox&Name&Identity";
	}
}
