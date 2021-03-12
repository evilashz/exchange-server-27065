using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	// Token: 0x020003D2 RID: 978
	[ComVisible(true)]
	public interface ISymbolMethod
	{
		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06003285 RID: 12933
		SymbolToken Token { get; }

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06003286 RID: 12934
		int SequencePointCount { get; }

		// Token: 0x06003287 RID: 12935
		void GetSequencePoints(int[] offsets, ISymbolDocument[] documents, int[] lines, int[] columns, int[] endLines, int[] endColumns);

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06003288 RID: 12936
		ISymbolScope RootScope { get; }

		// Token: 0x06003289 RID: 12937
		ISymbolScope GetScope(int offset);

		// Token: 0x0600328A RID: 12938
		int GetOffset(ISymbolDocument document, int line, int column);

		// Token: 0x0600328B RID: 12939
		int[] GetRanges(ISymbolDocument document, int line, int column);

		// Token: 0x0600328C RID: 12940
		ISymbolVariable[] GetParameters();

		// Token: 0x0600328D RID: 12941
		ISymbolNamespace GetNamespace();

		// Token: 0x0600328E RID: 12942
		bool GetSourceStartEnd(ISymbolDocument[] docs, int[] lines, int[] columns);
	}
}
