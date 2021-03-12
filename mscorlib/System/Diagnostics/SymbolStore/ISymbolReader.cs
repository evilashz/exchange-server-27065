using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	// Token: 0x020003D4 RID: 980
	[ComVisible(true)]
	public interface ISymbolReader
	{
		// Token: 0x06003292 RID: 12946
		ISymbolDocument GetDocument(string url, Guid language, Guid languageVendor, Guid documentType);

		// Token: 0x06003293 RID: 12947
		ISymbolDocument[] GetDocuments();

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06003294 RID: 12948
		SymbolToken UserEntryPoint { get; }

		// Token: 0x06003295 RID: 12949
		ISymbolMethod GetMethod(SymbolToken method);

		// Token: 0x06003296 RID: 12950
		ISymbolMethod GetMethod(SymbolToken method, int version);

		// Token: 0x06003297 RID: 12951
		ISymbolVariable[] GetVariables(SymbolToken parent);

		// Token: 0x06003298 RID: 12952
		ISymbolVariable[] GetGlobalVariables();

		// Token: 0x06003299 RID: 12953
		ISymbolMethod GetMethodFromDocumentPosition(ISymbolDocument document, int line, int column);

		// Token: 0x0600329A RID: 12954
		byte[] GetSymAttribute(SymbolToken parent, string name);

		// Token: 0x0600329B RID: 12955
		ISymbolNamespace[] GetNamespaces();
	}
}
