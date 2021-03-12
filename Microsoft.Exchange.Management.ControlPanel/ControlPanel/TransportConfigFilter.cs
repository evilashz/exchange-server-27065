using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000453 RID: 1107
	[DataContract]
	public class TransportConfigFilter : WebServiceParameters
	{
		// Token: 0x1700213A RID: 8506
		// (get) Token: 0x06003649 RID: 13897 RVA: 0x000A7CA7 File Offset: 0x000A5EA7
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-TransportConfig";
			}
		}

		// Token: 0x1700213B RID: 8507
		// (get) Token: 0x0600364A RID: 13898 RVA: 0x000A7CAE File Offset: 0x000A5EAE
		public override string RbacScope
		{
			get
			{
				return "@C:OrganizationConfig";
			}
		}
	}
}
