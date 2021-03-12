using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AnchorService
{
	// Token: 0x02000028 RID: 40
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ToggleableCapabilityCacheEntry : CapabilityCacheEntry
	{
		// Token: 0x060001B8 RID: 440 RVA: 0x00006B98 File Offset: 0x00004D98
		public ToggleableCapabilityCacheEntry(ActiveAnchorContext context, ADUser user) : base(context, user)
		{
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00006BA2 File Offset: 0x00004DA2
		public override void Activate()
		{
			base.ADProvider.AddCapability(base.ObjectId, this.ActiveCapability);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00006BBB File Offset: 0x00004DBB
		public override void Deactivate()
		{
			base.ADProvider.RemoveCapability(base.ObjectId, this.ActiveCapability);
		}
	}
}
