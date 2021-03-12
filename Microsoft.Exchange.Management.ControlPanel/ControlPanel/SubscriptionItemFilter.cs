using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000450 RID: 1104
	[DataContract]
	public class SubscriptionItemFilter : WebServiceParameters
	{
		// Token: 0x17002138 RID: 8504
		// (get) Token: 0x06003642 RID: 13890 RVA: 0x000A7C18 File Offset: 0x000A5E18
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-Subscription";
			}
		}

		// Token: 0x17002139 RID: 8505
		// (get) Token: 0x06003643 RID: 13891 RVA: 0x000A7C1F File Offset: 0x000A5E1F
		public override string RbacScope
		{
			get
			{
				return "@R:Self";
			}
		}
	}
}
