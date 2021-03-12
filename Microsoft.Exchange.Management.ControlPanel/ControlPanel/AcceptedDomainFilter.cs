using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200024F RID: 591
	[DataContract]
	public class AcceptedDomainFilter : WebServiceParameters
	{
		// Token: 0x17001C62 RID: 7266
		// (get) Token: 0x06002898 RID: 10392 RVA: 0x0007FDBD File Offset: 0x0007DFBD
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-AcceptedDomain";
			}
		}

		// Token: 0x17001C63 RID: 7267
		// (get) Token: 0x06002899 RID: 10393 RVA: 0x0007FDC4 File Offset: 0x0007DFC4
		public override string RbacScope
		{
			get
			{
				return "@C:OrganizationConfig";
			}
		}
	}
}
