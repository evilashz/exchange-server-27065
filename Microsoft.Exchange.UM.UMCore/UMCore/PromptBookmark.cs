using System;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001B3 RID: 435
	internal class PromptBookmark : Prompt
	{
		// Token: 0x06000CB8 RID: 3256 RVA: 0x0003732D File Offset: 0x0003552D
		public override string ToString()
		{
			return base.Config.PromptName;
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x0003733A File Offset: 0x0003553A
		internal override string ToSsml()
		{
			return "<mark name=\"" + base.Config.PromptName + "\" />";
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x00037356 File Offset: 0x00035556
		protected override void InternalInitialize()
		{
		}
	}
}
