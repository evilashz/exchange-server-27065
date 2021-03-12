using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004E7 RID: 1255
	[DataContract]
	public class SoftDeletedMailboxFilter : WebServiceParameters
	{
		// Token: 0x17002406 RID: 9222
		// (get) Token: 0x06003D09 RID: 15625 RVA: 0x000B7273 File Offset: 0x000B5473
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-Mailbox";
			}
		}

		// Token: 0x17002407 RID: 9223
		// (get) Token: 0x06003D0A RID: 15626 RVA: 0x000B727A File Offset: 0x000B547A
		public override string RbacScope
		{
			get
			{
				return string.Empty;
			}
		}
	}
}
