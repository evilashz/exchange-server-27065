using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004EF RID: 1263
	[DataContract]
	public class UpdateDistributionGroupMember : UpdateDistributionGroupMemberBase
	{
		// Token: 0x1700240B RID: 9227
		// (get) Token: 0x06003D2F RID: 15663 RVA: 0x000B832F File Offset: 0x000B652F
		public override string RbacScope
		{
			get
			{
				return "@W:Organization";
			}
		}
	}
}
