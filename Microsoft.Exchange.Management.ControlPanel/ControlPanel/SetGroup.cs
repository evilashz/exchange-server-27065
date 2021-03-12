using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004EE RID: 1262
	[DataContract]
	public class SetGroup : SetGroupBase
	{
		// Token: 0x1700240A RID: 9226
		// (get) Token: 0x06003D2D RID: 15661 RVA: 0x000B8320 File Offset: 0x000B6520
		public override string RbacScope
		{
			get
			{
				return "@W:Organization";
			}
		}
	}
}
