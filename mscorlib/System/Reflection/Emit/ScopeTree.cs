using System;
using System.Diagnostics.SymbolStore;

namespace System.Reflection.Emit
{
	// Token: 0x02000614 RID: 1556
	internal sealed class ScopeTree
	{
		// Token: 0x060049D6 RID: 18902 RVA: 0x0010ADA2 File Offset: 0x00108FA2
		internal ScopeTree()
		{
			this.m_iOpenScopeCount = 0;
			this.m_iCount = 0;
		}

		// Token: 0x060049D7 RID: 18903 RVA: 0x0010ADB8 File Offset: 0x00108FB8
		internal int GetCurrentActiveScopeIndex()
		{
			int num = 0;
			int num2 = this.m_iCount - 1;
			if (this.m_iCount == 0)
			{
				return -1;
			}
			while (num > 0 || this.m_ScopeActions[num2] == ScopeAction.Close)
			{
				if (this.m_ScopeActions[num2] == ScopeAction.Open)
				{
					num--;
				}
				else
				{
					num++;
				}
				num2--;
			}
			return num2;
		}

		// Token: 0x060049D8 RID: 18904 RVA: 0x0010AE04 File Offset: 0x00109004
		internal void AddLocalSymInfoToCurrentScope(string strName, byte[] signature, int slot, int startOffset, int endOffset)
		{
			int currentActiveScopeIndex = this.GetCurrentActiveScopeIndex();
			if (this.m_localSymInfos[currentActiveScopeIndex] == null)
			{
				this.m_localSymInfos[currentActiveScopeIndex] = new LocalSymInfo();
			}
			this.m_localSymInfos[currentActiveScopeIndex].AddLocalSymInfo(strName, signature, slot, startOffset, endOffset);
		}

		// Token: 0x060049D9 RID: 18905 RVA: 0x0010AE44 File Offset: 0x00109044
		internal void AddUsingNamespaceToCurrentScope(string strNamespace)
		{
			int currentActiveScopeIndex = this.GetCurrentActiveScopeIndex();
			if (this.m_localSymInfos[currentActiveScopeIndex] == null)
			{
				this.m_localSymInfos[currentActiveScopeIndex] = new LocalSymInfo();
			}
			this.m_localSymInfos[currentActiveScopeIndex].AddUsingNamespace(strNamespace);
		}

		// Token: 0x060049DA RID: 18906 RVA: 0x0010AE80 File Offset: 0x00109080
		internal void AddScopeInfo(ScopeAction sa, int iOffset)
		{
			if (sa == ScopeAction.Close && this.m_iOpenScopeCount <= 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_UnmatchingSymScope"));
			}
			this.EnsureCapacity();
			this.m_ScopeActions[this.m_iCount] = sa;
			this.m_iOffsets[this.m_iCount] = iOffset;
			this.m_localSymInfos[this.m_iCount] = null;
			checked
			{
				this.m_iCount++;
			}
			if (sa == ScopeAction.Open)
			{
				this.m_iOpenScopeCount++;
				return;
			}
			this.m_iOpenScopeCount--;
		}

		// Token: 0x060049DB RID: 18907 RVA: 0x0010AF08 File Offset: 0x00109108
		internal void EnsureCapacity()
		{
			if (this.m_iCount == 0)
			{
				this.m_iOffsets = new int[16];
				this.m_ScopeActions = new ScopeAction[16];
				this.m_localSymInfos = new LocalSymInfo[16];
				return;
			}
			if (this.m_iCount == this.m_iOffsets.Length)
			{
				int num = checked(this.m_iCount * 2);
				int[] array = new int[num];
				Array.Copy(this.m_iOffsets, array, this.m_iCount);
				this.m_iOffsets = array;
				ScopeAction[] array2 = new ScopeAction[num];
				Array.Copy(this.m_ScopeActions, array2, this.m_iCount);
				this.m_ScopeActions = array2;
				LocalSymInfo[] array3 = new LocalSymInfo[num];
				Array.Copy(this.m_localSymInfos, array3, this.m_iCount);
				this.m_localSymInfos = array3;
			}
		}

		// Token: 0x060049DC RID: 18908 RVA: 0x0010AFC0 File Offset: 0x001091C0
		internal void EmitScopeTree(ISymbolWriter symWriter)
		{
			for (int i = 0; i < this.m_iCount; i++)
			{
				if (this.m_ScopeActions[i] == ScopeAction.Open)
				{
					symWriter.OpenScope(this.m_iOffsets[i]);
				}
				else
				{
					symWriter.CloseScope(this.m_iOffsets[i]);
				}
				if (this.m_localSymInfos[i] != null)
				{
					this.m_localSymInfos[i].EmitLocalSymInfo(symWriter);
				}
			}
		}

		// Token: 0x04001E38 RID: 7736
		internal int[] m_iOffsets;

		// Token: 0x04001E39 RID: 7737
		internal ScopeAction[] m_ScopeActions;

		// Token: 0x04001E3A RID: 7738
		internal int m_iCount;

		// Token: 0x04001E3B RID: 7739
		internal int m_iOpenScopeCount;

		// Token: 0x04001E3C RID: 7740
		internal const int InitialSize = 16;

		// Token: 0x04001E3D RID: 7741
		internal LocalSymInfo[] m_localSymInfos;
	}
}
