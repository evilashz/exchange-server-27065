using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Text
{
	// Token: 0x02000A2E RID: 2606
	[Serializable]
	internal abstract class BaseCodePageEncoding : EncodingNLS, ISerializable
	{
		// Token: 0x06006634 RID: 26164 RVA: 0x00158415 File Offset: 0x00156615
		[SecurityCritical]
		internal BaseCodePageEncoding(int codepage) : this(codepage, codepage)
		{
		}

		// Token: 0x06006635 RID: 26165 RVA: 0x0015841F File Offset: 0x0015661F
		[SecurityCritical]
		internal BaseCodePageEncoding(int codepage, int dataCodePage)
		{
			this.bFlagDataTable = true;
			this.pCodePage = null;
			base..ctor((codepage == 0) ? Win32Native.GetACP() : codepage);
			this.dataTableCodePage = dataCodePage;
			this.LoadCodePageTables();
		}

		// Token: 0x06006636 RID: 26166 RVA: 0x0015844E File Offset: 0x0015664E
		[SecurityCritical]
		internal BaseCodePageEncoding(SerializationInfo info, StreamingContext context)
		{
			this.bFlagDataTable = true;
			this.pCodePage = null;
			base..ctor(0);
			throw new ArgumentNullException("this");
		}

		// Token: 0x06006637 RID: 26167 RVA: 0x00158470 File Offset: 0x00156670
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.SerializeEncoding(info, context);
			info.AddValue(this.m_bUseMlangTypeForSerialization ? "m_maxByteSize" : "maxCharSize", this.IsSingleByte ? 1 : 2);
			info.SetType(this.m_bUseMlangTypeForSerialization ? typeof(MLangCodePageEncoding) : typeof(CodePageEncoding));
		}

		// Token: 0x06006638 RID: 26168 RVA: 0x001584D0 File Offset: 0x001566D0
		[SecurityCritical]
		private unsafe void LoadCodePageTables()
		{
			BaseCodePageEncoding.CodePageHeader* ptr = BaseCodePageEncoding.FindCodePage(this.dataTableCodePage);
			if (ptr == null)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_NoCodepageData", new object[]
				{
					this.CodePage
				}));
			}
			this.pCodePage = ptr;
			this.LoadManagedCodePage();
		}

		// Token: 0x06006639 RID: 26169 RVA: 0x00158520 File Offset: 0x00156720
		[SecurityCritical]
		private unsafe static BaseCodePageEncoding.CodePageHeader* FindCodePage(int codePage)
		{
			for (int i = 0; i < (int)BaseCodePageEncoding.m_pCodePageFileHeader->CodePageCount; i++)
			{
				BaseCodePageEncoding.CodePageIndex* ptr = &BaseCodePageEncoding.m_pCodePageFileHeader->CodePages + i;
				if ((int)ptr->CodePage == codePage)
				{
					return (BaseCodePageEncoding.CodePageHeader*)(BaseCodePageEncoding.m_pCodePageFileHeader + ptr->Offset / sizeof(BaseCodePageEncoding.CodePageDataFileHeader));
				}
			}
			return null;
		}

		// Token: 0x0600663A RID: 26170 RVA: 0x00158574 File Offset: 0x00156774
		[SecurityCritical]
		internal unsafe static int GetCodePageByteSize(int codePage)
		{
			BaseCodePageEncoding.CodePageHeader* ptr = BaseCodePageEncoding.FindCodePage(codePage);
			if (ptr == null)
			{
				return 0;
			}
			return (int)ptr->ByteCount;
		}

		// Token: 0x0600663B RID: 26171
		[SecurityCritical]
		protected abstract void LoadManagedCodePage();

		// Token: 0x0600663C RID: 26172 RVA: 0x00158598 File Offset: 0x00156798
		[SecurityCritical]
		protected unsafe byte* GetSharedMemory(int iSize)
		{
			string memorySectionName = this.GetMemorySectionName();
			IntPtr intPtr;
			byte* ptr = EncodingTable.nativeCreateOpenFileMapping(memorySectionName, iSize, out intPtr);
			if (ptr == null)
			{
				throw new OutOfMemoryException(Environment.GetResourceString("Arg_OutOfMemoryException"));
			}
			if (intPtr != IntPtr.Zero)
			{
				this.safeMemorySectionHandle = new SafeViewOfFileHandle((IntPtr)((void*)ptr), true);
				this.safeFileMappingHandle = new SafeFileMappingHandle(intPtr, true);
			}
			return ptr;
		}

		// Token: 0x0600663D RID: 26173 RVA: 0x001585F8 File Offset: 0x001567F8
		[SecurityCritical]
		protected unsafe virtual string GetMemorySectionName()
		{
			int num = this.bFlagDataTable ? this.dataTableCodePage : this.CodePage;
			return string.Format(CultureInfo.InvariantCulture, "NLS_CodePage_{0}_{1}_{2}_{3}_{4}", new object[]
			{
				num,
				this.pCodePage->VersionMajor,
				this.pCodePage->VersionMinor,
				this.pCodePage->VersionRevision,
				this.pCodePage->VersionBuild
			});
		}

		// Token: 0x0600663E RID: 26174
		[SecurityCritical]
		protected abstract void ReadBestFitTable();

		// Token: 0x0600663F RID: 26175 RVA: 0x00158688 File Offset: 0x00156888
		[SecuritySafeCritical]
		internal override char[] GetBestFitUnicodeToBytesData()
		{
			if (this.arrayUnicodeBestFit == null)
			{
				this.ReadBestFitTable();
			}
			return this.arrayUnicodeBestFit;
		}

		// Token: 0x06006640 RID: 26176 RVA: 0x0015869E File Offset: 0x0015689E
		[SecuritySafeCritical]
		internal override char[] GetBestFitBytesToUnicodeData()
		{
			if (this.arrayBytesBestFit == null)
			{
				this.ReadBestFitTable();
			}
			return this.arrayBytesBestFit;
		}

		// Token: 0x06006641 RID: 26177 RVA: 0x001586B4 File Offset: 0x001568B4
		[SecurityCritical]
		internal void CheckMemorySection()
		{
			if (this.safeMemorySectionHandle != null && this.safeMemorySectionHandle.DangerousGetHandle() == IntPtr.Zero)
			{
				this.LoadManagedCodePage();
			}
		}

		// Token: 0x04002D6E RID: 11630
		internal const string CODE_PAGE_DATA_FILE_NAME = "codepages.nlp";

		// Token: 0x04002D6F RID: 11631
		[NonSerialized]
		protected int dataTableCodePage;

		// Token: 0x04002D70 RID: 11632
		[NonSerialized]
		protected bool bFlagDataTable;

		// Token: 0x04002D71 RID: 11633
		[NonSerialized]
		protected int iExtraBytes;

		// Token: 0x04002D72 RID: 11634
		[NonSerialized]
		protected char[] arrayUnicodeBestFit;

		// Token: 0x04002D73 RID: 11635
		[NonSerialized]
		protected char[] arrayBytesBestFit;

		// Token: 0x04002D74 RID: 11636
		[NonSerialized]
		protected bool m_bUseMlangTypeForSerialization;

		// Token: 0x04002D75 RID: 11637
		[SecurityCritical]
		private unsafe static BaseCodePageEncoding.CodePageDataFileHeader* m_pCodePageFileHeader = (BaseCodePageEncoding.CodePageDataFileHeader*)GlobalizationAssembly.GetGlobalizationResourceBytePtr(typeof(CharUnicodeInfo).Assembly, "codepages.nlp");

		// Token: 0x04002D76 RID: 11638
		[SecurityCritical]
		[NonSerialized]
		protected unsafe BaseCodePageEncoding.CodePageHeader* pCodePage;

		// Token: 0x04002D77 RID: 11639
		[SecurityCritical]
		[NonSerialized]
		protected SafeViewOfFileHandle safeMemorySectionHandle;

		// Token: 0x04002D78 RID: 11640
		[SecurityCritical]
		[NonSerialized]
		protected SafeFileMappingHandle safeFileMappingHandle;

		// Token: 0x02000C77 RID: 3191
		[StructLayout(LayoutKind.Explicit)]
		internal struct CodePageDataFileHeader
		{
			// Token: 0x040037AA RID: 14250
			[FieldOffset(0)]
			internal char TableName;

			// Token: 0x040037AB RID: 14251
			[FieldOffset(32)]
			internal ushort Version;

			// Token: 0x040037AC RID: 14252
			[FieldOffset(40)]
			internal short CodePageCount;

			// Token: 0x040037AD RID: 14253
			[FieldOffset(42)]
			internal short unused1;

			// Token: 0x040037AE RID: 14254
			[FieldOffset(44)]
			internal BaseCodePageEncoding.CodePageIndex CodePages;
		}

		// Token: 0x02000C78 RID: 3192
		[StructLayout(LayoutKind.Explicit, Pack = 2)]
		internal struct CodePageIndex
		{
			// Token: 0x040037AF RID: 14255
			[FieldOffset(0)]
			internal char CodePageName;

			// Token: 0x040037B0 RID: 14256
			[FieldOffset(32)]
			internal short CodePage;

			// Token: 0x040037B1 RID: 14257
			[FieldOffset(34)]
			internal short ByteCount;

			// Token: 0x040037B2 RID: 14258
			[FieldOffset(36)]
			internal int Offset;
		}

		// Token: 0x02000C79 RID: 3193
		[StructLayout(LayoutKind.Explicit)]
		internal struct CodePageHeader
		{
			// Token: 0x040037B3 RID: 14259
			[FieldOffset(0)]
			internal char CodePageName;

			// Token: 0x040037B4 RID: 14260
			[FieldOffset(32)]
			internal ushort VersionMajor;

			// Token: 0x040037B5 RID: 14261
			[FieldOffset(34)]
			internal ushort VersionMinor;

			// Token: 0x040037B6 RID: 14262
			[FieldOffset(36)]
			internal ushort VersionRevision;

			// Token: 0x040037B7 RID: 14263
			[FieldOffset(38)]
			internal ushort VersionBuild;

			// Token: 0x040037B8 RID: 14264
			[FieldOffset(40)]
			internal short CodePage;

			// Token: 0x040037B9 RID: 14265
			[FieldOffset(42)]
			internal short ByteCount;

			// Token: 0x040037BA RID: 14266
			[FieldOffset(44)]
			internal char UnicodeReplace;

			// Token: 0x040037BB RID: 14267
			[FieldOffset(46)]
			internal ushort ByteReplace;

			// Token: 0x040037BC RID: 14268
			[FieldOffset(48)]
			internal short FirstDataWord;
		}
	}
}
