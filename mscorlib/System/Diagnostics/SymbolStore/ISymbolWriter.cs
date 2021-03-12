using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	// Token: 0x020003D7 RID: 983
	[ComVisible(true)]
	public interface ISymbolWriter
	{
		// Token: 0x060032AC RID: 12972
		void Initialize(IntPtr emitter, string filename, bool fFullBuild);

		// Token: 0x060032AD RID: 12973
		ISymbolDocumentWriter DefineDocument(string url, Guid language, Guid languageVendor, Guid documentType);

		// Token: 0x060032AE RID: 12974
		void SetUserEntryPoint(SymbolToken entryMethod);

		// Token: 0x060032AF RID: 12975
		void OpenMethod(SymbolToken method);

		// Token: 0x060032B0 RID: 12976
		void CloseMethod();

		// Token: 0x060032B1 RID: 12977
		void DefineSequencePoints(ISymbolDocumentWriter document, int[] offsets, int[] lines, int[] columns, int[] endLines, int[] endColumns);

		// Token: 0x060032B2 RID: 12978
		int OpenScope(int startOffset);

		// Token: 0x060032B3 RID: 12979
		void CloseScope(int endOffset);

		// Token: 0x060032B4 RID: 12980
		void SetScopeRange(int scopeID, int startOffset, int endOffset);

		// Token: 0x060032B5 RID: 12981
		void DefineLocalVariable(string name, FieldAttributes attributes, byte[] signature, SymAddressKind addrKind, int addr1, int addr2, int addr3, int startOffset, int endOffset);

		// Token: 0x060032B6 RID: 12982
		void DefineParameter(string name, ParameterAttributes attributes, int sequence, SymAddressKind addrKind, int addr1, int addr2, int addr3);

		// Token: 0x060032B7 RID: 12983
		void DefineField(SymbolToken parent, string name, FieldAttributes attributes, byte[] signature, SymAddressKind addrKind, int addr1, int addr2, int addr3);

		// Token: 0x060032B8 RID: 12984
		void DefineGlobalVariable(string name, FieldAttributes attributes, byte[] signature, SymAddressKind addrKind, int addr1, int addr2, int addr3);

		// Token: 0x060032B9 RID: 12985
		void Close();

		// Token: 0x060032BA RID: 12986
		void SetSymAttribute(SymbolToken parent, string name, byte[] data);

		// Token: 0x060032BB RID: 12987
		void OpenNamespace(string name);

		// Token: 0x060032BC RID: 12988
		void CloseNamespace();

		// Token: 0x060032BD RID: 12989
		void UsingNamespace(string fullName);

		// Token: 0x060032BE RID: 12990
		void SetMethodSourceRange(ISymbolDocumentWriter startDoc, int startLine, int startColumn, ISymbolDocumentWriter endDoc, int endLine, int endColumn);

		// Token: 0x060032BF RID: 12991
		void SetUnderlyingWriter(IntPtr underlyingWriter);
	}
}
