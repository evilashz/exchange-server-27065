using System;
using System.Diagnostics.SymbolStore;

namespace System.Reflection.Emit
{
	// Token: 0x0200061A RID: 1562
	internal class LocalSymInfo
	{
		// Token: 0x06004A4D RID: 19021 RVA: 0x0010CA22 File Offset: 0x0010AC22
		internal LocalSymInfo()
		{
			this.m_iLocalSymCount = 0;
			this.m_iNameSpaceCount = 0;
		}

		// Token: 0x06004A4E RID: 19022 RVA: 0x0010CA38 File Offset: 0x0010AC38
		private void EnsureCapacityNamespace()
		{
			if (this.m_iNameSpaceCount == 0)
			{
				this.m_namespace = new string[16];
				return;
			}
			if (this.m_iNameSpaceCount == this.m_namespace.Length)
			{
				string[] array = new string[checked(this.m_iNameSpaceCount * 2)];
				Array.Copy(this.m_namespace, array, this.m_iNameSpaceCount);
				this.m_namespace = array;
			}
		}

		// Token: 0x06004A4F RID: 19023 RVA: 0x0010CA94 File Offset: 0x0010AC94
		private void EnsureCapacity()
		{
			if (this.m_iLocalSymCount == 0)
			{
				this.m_strName = new string[16];
				this.m_ubSignature = new byte[16][];
				this.m_iLocalSlot = new int[16];
				this.m_iStartOffset = new int[16];
				this.m_iEndOffset = new int[16];
				return;
			}
			if (this.m_iLocalSymCount == this.m_strName.Length)
			{
				int num = checked(this.m_iLocalSymCount * 2);
				int[] array = new int[num];
				Array.Copy(this.m_iLocalSlot, array, this.m_iLocalSymCount);
				this.m_iLocalSlot = array;
				array = new int[num];
				Array.Copy(this.m_iStartOffset, array, this.m_iLocalSymCount);
				this.m_iStartOffset = array;
				array = new int[num];
				Array.Copy(this.m_iEndOffset, array, this.m_iLocalSymCount);
				this.m_iEndOffset = array;
				string[] array2 = new string[num];
				Array.Copy(this.m_strName, array2, this.m_iLocalSymCount);
				this.m_strName = array2;
				byte[][] array3 = new byte[num][];
				Array.Copy(this.m_ubSignature, array3, this.m_iLocalSymCount);
				this.m_ubSignature = array3;
			}
		}

		// Token: 0x06004A50 RID: 19024 RVA: 0x0010CBA8 File Offset: 0x0010ADA8
		internal void AddLocalSymInfo(string strName, byte[] signature, int slot, int startOffset, int endOffset)
		{
			this.EnsureCapacity();
			this.m_iStartOffset[this.m_iLocalSymCount] = startOffset;
			this.m_iEndOffset[this.m_iLocalSymCount] = endOffset;
			this.m_iLocalSlot[this.m_iLocalSymCount] = slot;
			this.m_strName[this.m_iLocalSymCount] = strName;
			this.m_ubSignature[this.m_iLocalSymCount] = signature;
			checked
			{
				this.m_iLocalSymCount++;
			}
		}

		// Token: 0x06004A51 RID: 19025 RVA: 0x0010CC11 File Offset: 0x0010AE11
		internal void AddUsingNamespace(string strNamespace)
		{
			this.EnsureCapacityNamespace();
			this.m_namespace[this.m_iNameSpaceCount] = strNamespace;
			checked
			{
				this.m_iNameSpaceCount++;
			}
		}

		// Token: 0x06004A52 RID: 19026 RVA: 0x0010CC38 File Offset: 0x0010AE38
		internal virtual void EmitLocalSymInfo(ISymbolWriter symWriter)
		{
			for (int i = 0; i < this.m_iLocalSymCount; i++)
			{
				symWriter.DefineLocalVariable(this.m_strName[i], FieldAttributes.PrivateScope, this.m_ubSignature[i], SymAddressKind.ILOffset, this.m_iLocalSlot[i], 0, 0, this.m_iStartOffset[i], this.m_iEndOffset[i]);
			}
			for (int i = 0; i < this.m_iNameSpaceCount; i++)
			{
				symWriter.UsingNamespace(this.m_namespace[i]);
			}
		}

		// Token: 0x04001E6E RID: 7790
		internal string[] m_strName;

		// Token: 0x04001E6F RID: 7791
		internal byte[][] m_ubSignature;

		// Token: 0x04001E70 RID: 7792
		internal int[] m_iLocalSlot;

		// Token: 0x04001E71 RID: 7793
		internal int[] m_iStartOffset;

		// Token: 0x04001E72 RID: 7794
		internal int[] m_iEndOffset;

		// Token: 0x04001E73 RID: 7795
		internal int m_iLocalSymCount;

		// Token: 0x04001E74 RID: 7796
		internal string[] m_namespace;

		// Token: 0x04001E75 RID: 7797
		internal int m_iNameSpaceCount;

		// Token: 0x04001E76 RID: 7798
		internal const int InitialSize = 16;
	}
}
