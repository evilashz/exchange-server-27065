using System;

namespace Microsoft.Exchange.SenderId
{
	// Token: 0x0200002F RID: 47
	internal class MacroExpandSpfNode : MacroTermSpfNode
	{
		// Token: 0x060000CE RID: 206 RVA: 0x00004FCF File Offset: 0x000031CF
		public MacroExpandSpfNode(char macroCharacter, int transformerDigits, bool transformerReverse, string delimiters) : base(false, true)
		{
			this.MacroCharacter = macroCharacter;
			this.TransformerDigits = transformerDigits;
			this.TransformerReverse = transformerReverse;
			this.Delimiters = delimiters;
		}

		// Token: 0x0400007F RID: 127
		public char MacroCharacter;

		// Token: 0x04000080 RID: 128
		public int TransformerDigits;

		// Token: 0x04000081 RID: 129
		public bool TransformerReverse;

		// Token: 0x04000082 RID: 130
		public string Delimiters;
	}
}
