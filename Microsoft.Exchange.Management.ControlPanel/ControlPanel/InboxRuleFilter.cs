using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003FB RID: 1019
	[DataContract]
	public class InboxRuleFilter : WebServiceParameters
	{
		// Token: 0x17002069 RID: 8297
		// (get) Token: 0x06003422 RID: 13346 RVA: 0x000A2ADF File Offset: 0x000A0CDF
		public sealed override string AssociatedCmdlet
		{
			get
			{
				return "Get-InboxRule";
			}
		}

		// Token: 0x1700206A RID: 8298
		// (get) Token: 0x06003423 RID: 13347 RVA: 0x000A2AE6 File Offset: 0x000A0CE6
		public override string RbacScope
		{
			get
			{
				return "@R:Self";
			}
		}
	}
}
