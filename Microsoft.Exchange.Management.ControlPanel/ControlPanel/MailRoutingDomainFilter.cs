using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000419 RID: 1049
	[DataContract]
	public class MailRoutingDomainFilter : WebServiceParameters
	{
		// Token: 0x170020E8 RID: 8424
		// (get) Token: 0x0600353D RID: 13629 RVA: 0x000A5885 File Offset: 0x000A3A85
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-AcceptedDomain";
			}
		}

		// Token: 0x170020E9 RID: 8425
		// (get) Token: 0x0600353E RID: 13630 RVA: 0x000A588C File Offset: 0x000A3A8C
		public override string RbacScope
		{
			get
			{
				return "@C:OrganizationConfig";
			}
		}
	}
}
