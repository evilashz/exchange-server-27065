using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003DA RID: 986
	[DataContract]
	public class MailboxSearchFilter : WebServiceParameters
	{
		// Token: 0x17001FEE RID: 8174
		// (get) Token: 0x060032E4 RID: 13028 RVA: 0x0009E1ED File Offset: 0x0009C3ED
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-MailboxSearch";
			}
		}

		// Token: 0x17001FEF RID: 8175
		// (get) Token: 0x060032E5 RID: 13029 RVA: 0x0009E1F4 File Offset: 0x0009C3F4
		public override string RbacScope
		{
			get
			{
				return string.Empty;
			}
		}
	}
}
