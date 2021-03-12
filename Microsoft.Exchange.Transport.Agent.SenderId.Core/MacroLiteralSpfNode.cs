using System;

namespace Microsoft.Exchange.SenderId
{
	// Token: 0x02000030 RID: 48
	internal class MacroLiteralSpfNode : MacroTermSpfNode
	{
		// Token: 0x060000CF RID: 207 RVA: 0x00004FF6 File Offset: 0x000031F6
		public MacroLiteralSpfNode(string literal) : base(true, false)
		{
			this.Literal = literal;
		}

		// Token: 0x04000083 RID: 131
		public string Literal;
	}
}
