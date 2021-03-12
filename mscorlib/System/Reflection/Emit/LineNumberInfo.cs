using System;
using System.Diagnostics.SymbolStore;

namespace System.Reflection.Emit
{
	// Token: 0x02000615 RID: 1557
	internal sealed class LineNumberInfo
	{
		// Token: 0x060049DD RID: 18909 RVA: 0x0010B01F File Offset: 0x0010921F
		internal LineNumberInfo()
		{
			this.m_DocumentCount = 0;
			this.m_iLastFound = 0;
		}

		// Token: 0x060049DE RID: 18910 RVA: 0x0010B038 File Offset: 0x00109238
		internal void AddLineNumberInfo(ISymbolDocumentWriter document, int iOffset, int iStartLine, int iStartColumn, int iEndLine, int iEndColumn)
		{
			int num = this.FindDocument(document);
			this.m_Documents[num].AddLineNumberInfo(document, iOffset, iStartLine, iStartColumn, iEndLine, iEndColumn);
		}

		// Token: 0x060049DF RID: 18911 RVA: 0x0010B064 File Offset: 0x00109264
		private int FindDocument(ISymbolDocumentWriter document)
		{
			if (this.m_iLastFound < this.m_DocumentCount && this.m_Documents[this.m_iLastFound].m_document == document)
			{
				return this.m_iLastFound;
			}
			for (int i = 0; i < this.m_DocumentCount; i++)
			{
				if (this.m_Documents[i].m_document == document)
				{
					this.m_iLastFound = i;
					return this.m_iLastFound;
				}
			}
			this.EnsureCapacity();
			this.m_iLastFound = this.m_DocumentCount;
			this.m_Documents[this.m_iLastFound] = new REDocument(document);
			checked
			{
				this.m_DocumentCount++;
				return this.m_iLastFound;
			}
		}

		// Token: 0x060049E0 RID: 18912 RVA: 0x0010B104 File Offset: 0x00109304
		private void EnsureCapacity()
		{
			if (this.m_DocumentCount == 0)
			{
				this.m_Documents = new REDocument[16];
				return;
			}
			if (this.m_DocumentCount == this.m_Documents.Length)
			{
				REDocument[] array = new REDocument[this.m_DocumentCount * 2];
				Array.Copy(this.m_Documents, array, this.m_DocumentCount);
				this.m_Documents = array;
			}
		}

		// Token: 0x060049E1 RID: 18913 RVA: 0x0010B160 File Offset: 0x00109360
		internal void EmitLineNumberInfo(ISymbolWriter symWriter)
		{
			for (int i = 0; i < this.m_DocumentCount; i++)
			{
				this.m_Documents[i].EmitLineNumberInfo(symWriter);
			}
		}

		// Token: 0x04001E3E RID: 7742
		private int m_DocumentCount;

		// Token: 0x04001E3F RID: 7743
		private REDocument[] m_Documents;

		// Token: 0x04001E40 RID: 7744
		private const int InitialSize = 16;

		// Token: 0x04001E41 RID: 7745
		private int m_iLastFound;
	}
}
