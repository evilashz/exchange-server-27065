using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000234 RID: 564
	[DataContract]
	public class SetMyGroup : SetGroupBase
	{
		// Token: 0x17001C2D RID: 7213
		// (get) Token: 0x060027CE RID: 10190 RVA: 0x0007D221 File Offset: 0x0007B421
		public override string RbacScope
		{
			get
			{
				return "@W:MyDistributionGroups";
			}
		}
	}
}
