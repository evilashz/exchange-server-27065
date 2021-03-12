using System;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000D8 RID: 216
	public class AtomResultPaneProfile : ResultPaneProfile
	{
		// Token: 0x06000797 RID: 1943 RVA: 0x000198CA File Offset: 0x00017ACA
		public override bool HasPermission()
		{
			return NodeProfile.CanAddAtomResultPane(base.Type);
		}
	}
}
