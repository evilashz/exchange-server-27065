using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000510 RID: 1296
	[DataContract]
	public class MailboxPlanFilter : WebServiceParameters
	{
		// Token: 0x1700245C RID: 9308
		// (get) Token: 0x06003E18 RID: 15896 RVA: 0x000BA7C9 File Offset: 0x000B89C9
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-MailboxPlan";
			}
		}

		// Token: 0x1700245D RID: 9309
		// (get) Token: 0x06003E19 RID: 15897 RVA: 0x000BA7D0 File Offset: 0x000B89D0
		public override string RbacScope
		{
			get
			{
				return "@R:Organization";
			}
		}
	}
}
