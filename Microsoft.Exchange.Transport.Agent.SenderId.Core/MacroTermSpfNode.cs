using System;

namespace Microsoft.Exchange.SenderId
{
	// Token: 0x0200002E RID: 46
	internal class MacroTermSpfNode
	{
		// Token: 0x060000CD RID: 205 RVA: 0x00004FB9 File Offset: 0x000031B9
		public MacroTermSpfNode(bool isLiteral, bool isExpand)
		{
			this.IsLiteral = isLiteral;
			this.IsExpand = isExpand;
		}

		// Token: 0x0400007C RID: 124
		public MacroTermSpfNode Next;

		// Token: 0x0400007D RID: 125
		public bool IsLiteral;

		// Token: 0x0400007E RID: 126
		public bool IsExpand;
	}
}
