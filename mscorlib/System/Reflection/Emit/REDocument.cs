using System;
using System.Diagnostics.SymbolStore;

namespace System.Reflection.Emit
{
	// Token: 0x02000616 RID: 1558
	internal sealed class REDocument
	{
		// Token: 0x060049E2 RID: 18914 RVA: 0x0010B18C File Offset: 0x0010938C
		internal REDocument(ISymbolDocumentWriter document)
		{
			this.m_iLineNumberCount = 0;
			this.m_document = document;
		}

		// Token: 0x060049E3 RID: 18915 RVA: 0x0010B1A4 File Offset: 0x001093A4
		internal void AddLineNumberInfo(ISymbolDocumentWriter document, int iOffset, int iStartLine, int iStartColumn, int iEndLine, int iEndColumn)
		{
			this.EnsureCapacity();
			this.m_iOffsets[this.m_iLineNumberCount] = iOffset;
			this.m_iLines[this.m_iLineNumberCount] = iStartLine;
			this.m_iColumns[this.m_iLineNumberCount] = iStartColumn;
			this.m_iEndLines[this.m_iLineNumberCount] = iEndLine;
			this.m_iEndColumns[this.m_iLineNumberCount] = iEndColumn;
			checked
			{
				this.m_iLineNumberCount++;
			}
		}

		// Token: 0x060049E4 RID: 18916 RVA: 0x0010B210 File Offset: 0x00109410
		private void EnsureCapacity()
		{
			if (this.m_iLineNumberCount == 0)
			{
				this.m_iOffsets = new int[16];
				this.m_iLines = new int[16];
				this.m_iColumns = new int[16];
				this.m_iEndLines = new int[16];
				this.m_iEndColumns = new int[16];
				return;
			}
			if (this.m_iLineNumberCount == this.m_iOffsets.Length)
			{
				int num = checked(this.m_iLineNumberCount * 2);
				int[] array = new int[num];
				Array.Copy(this.m_iOffsets, array, this.m_iLineNumberCount);
				this.m_iOffsets = array;
				array = new int[num];
				Array.Copy(this.m_iLines, array, this.m_iLineNumberCount);
				this.m_iLines = array;
				array = new int[num];
				Array.Copy(this.m_iColumns, array, this.m_iLineNumberCount);
				this.m_iColumns = array;
				array = new int[num];
				Array.Copy(this.m_iEndLines, array, this.m_iLineNumberCount);
				this.m_iEndLines = array;
				array = new int[num];
				Array.Copy(this.m_iEndColumns, array, this.m_iLineNumberCount);
				this.m_iEndColumns = array;
			}
		}

		// Token: 0x060049E5 RID: 18917 RVA: 0x0010B324 File Offset: 0x00109524
		internal void EmitLineNumberInfo(ISymbolWriter symWriter)
		{
			if (this.m_iLineNumberCount == 0)
			{
				return;
			}
			int[] array = new int[this.m_iLineNumberCount];
			Array.Copy(this.m_iOffsets, array, this.m_iLineNumberCount);
			int[] array2 = new int[this.m_iLineNumberCount];
			Array.Copy(this.m_iLines, array2, this.m_iLineNumberCount);
			int[] array3 = new int[this.m_iLineNumberCount];
			Array.Copy(this.m_iColumns, array3, this.m_iLineNumberCount);
			int[] array4 = new int[this.m_iLineNumberCount];
			Array.Copy(this.m_iEndLines, array4, this.m_iLineNumberCount);
			int[] array5 = new int[this.m_iLineNumberCount];
			Array.Copy(this.m_iEndColumns, array5, this.m_iLineNumberCount);
			symWriter.DefineSequencePoints(this.m_document, array, array2, array3, array4, array5);
		}

		// Token: 0x04001E42 RID: 7746
		private int[] m_iOffsets;

		// Token: 0x04001E43 RID: 7747
		private int[] m_iLines;

		// Token: 0x04001E44 RID: 7748
		private int[] m_iColumns;

		// Token: 0x04001E45 RID: 7749
		private int[] m_iEndLines;

		// Token: 0x04001E46 RID: 7750
		private int[] m_iEndColumns;

		// Token: 0x04001E47 RID: 7751
		internal ISymbolDocumentWriter m_document;

		// Token: 0x04001E48 RID: 7752
		private int m_iLineNumberCount;

		// Token: 0x04001E49 RID: 7753
		private const int InitialSize = 16;
	}
}
