using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000235 RID: 565
	[DataContract]
	public class UpdateMyDistributionGroupMember : UpdateDistributionGroupMemberBase
	{
		// Token: 0x17001C2E RID: 7214
		// (get) Token: 0x060027D0 RID: 10192 RVA: 0x0007D230 File Offset: 0x0007B430
		public override string RbacScope
		{
			get
			{
				return "@W:MyDistributionGroups";
			}
		}
	}
}
