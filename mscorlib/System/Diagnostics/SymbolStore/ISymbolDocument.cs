using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	// Token: 0x020003D0 RID: 976
	[ComVisible(true)]
	public interface ISymbolDocument
	{
		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06003279 RID: 12921
		string URL { get; }

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x0600327A RID: 12922
		Guid DocumentType { get; }

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x0600327B RID: 12923
		Guid Language { get; }

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x0600327C RID: 12924
		Guid LanguageVendor { get; }

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x0600327D RID: 12925
		Guid CheckSumAlgorithmId { get; }

		// Token: 0x0600327E RID: 12926
		byte[] GetCheckSum();

		// Token: 0x0600327F RID: 12927
		int FindClosestLine(int line);

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06003280 RID: 12928
		bool HasEmbeddedSource { get; }

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06003281 RID: 12929
		int SourceLength { get; }

		// Token: 0x06003282 RID: 12930
		byte[] GetSourceRange(int startLine, int startColumn, int endLine, int endColumn);
	}
}
