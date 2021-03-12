using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;

namespace System.Globalization
{
	// Token: 0x0200038B RID: 907
	internal static class EncodingTable
	{
		// Token: 0x06002E2F RID: 11823 RVA: 0x000B108C File Offset: 0x000AF28C
		[SecuritySafeCritical]
		private unsafe static int internalGetCodePageFromName(string name)
		{
			int i = 0;
			int num = EncodingTable.lastEncodingItem;
			while (num - i > 3)
			{
				int num2 = (num - i) / 2 + i;
				int num3 = string.nativeCompareOrdinalIgnoreCaseWC(name, EncodingTable.encodingDataPtr[num2].webName);
				if (num3 == 0)
				{
					return (int)EncodingTable.encodingDataPtr[num2].codePage;
				}
				if (num3 < 0)
				{
					num = num2;
				}
				else
				{
					i = num2;
				}
			}
			while (i <= num)
			{
				if (string.nativeCompareOrdinalIgnoreCaseWC(name, EncodingTable.encodingDataPtr[i].webName) == 0)
				{
					return (int)EncodingTable.encodingDataPtr[i].codePage;
				}
				i++;
			}
			throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_EncodingNotSupported"), name), "name");
		}

		// Token: 0x06002E30 RID: 11824 RVA: 0x000B1148 File Offset: 0x000AF348
		[SecuritySafeCritical]
		internal unsafe static EncodingInfo[] GetEncodings()
		{
			if (EncodingTable.lastCodePageItem == 0)
			{
				int num = 0;
				while (EncodingTable.codePageDataPtr[num].codePage != 0)
				{
					num++;
				}
				EncodingTable.lastCodePageItem = num;
			}
			EncodingInfo[] array = new EncodingInfo[EncodingTable.lastCodePageItem];
			for (int i = 0; i < EncodingTable.lastCodePageItem; i++)
			{
				array[i] = new EncodingInfo((int)EncodingTable.codePageDataPtr[i].codePage, CodePageDataItem.CreateString(EncodingTable.codePageDataPtr[i].Names, 0U), Environment.GetResourceString("Globalization.cp_" + EncodingTable.codePageDataPtr[i].codePage));
			}
			return array;
		}

		// Token: 0x06002E31 RID: 11825 RVA: 0x000B1204 File Offset: 0x000AF404
		internal static int GetCodePageFromName(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			object obj = EncodingTable.hashByName[name];
			if (obj != null)
			{
				return (int)obj;
			}
			int num = EncodingTable.internalGetCodePageFromName(name);
			EncodingTable.hashByName[name] = num;
			return num;
		}

		// Token: 0x06002E32 RID: 11826 RVA: 0x000B1250 File Offset: 0x000AF450
		[SecuritySafeCritical]
		internal unsafe static CodePageDataItem GetCodePageDataItem(int codepage)
		{
			CodePageDataItem codePageDataItem = (CodePageDataItem)EncodingTable.hashByCodePage[codepage];
			if (codePageDataItem != null)
			{
				return codePageDataItem;
			}
			int num = 0;
			int codePage;
			while ((codePage = (int)EncodingTable.codePageDataPtr[num].codePage) != 0)
			{
				if (codePage == codepage)
				{
					codePageDataItem = new CodePageDataItem(num);
					EncodingTable.hashByCodePage[codepage] = codePageDataItem;
					return codePageDataItem;
				}
				num++;
			}
			return null;
		}

		// Token: 0x06002E33 RID: 11827
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern InternalEncodingDataItem* GetEncodingData();

		// Token: 0x06002E34 RID: 11828
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetNumEncodingItems();

		// Token: 0x06002E35 RID: 11829
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern InternalCodePageDataItem* GetCodePageData();

		// Token: 0x06002E36 RID: 11830
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern byte* nativeCreateOpenFileMapping(string inSectionName, int inBytesToAllocate, out IntPtr mappedFileHandle);

		// Token: 0x0400135A RID: 4954
		private static int lastEncodingItem = EncodingTable.GetNumEncodingItems() - 1;

		// Token: 0x0400135B RID: 4955
		private static volatile int lastCodePageItem;

		// Token: 0x0400135C RID: 4956
		[SecurityCritical]
		internal unsafe static InternalEncodingDataItem* encodingDataPtr = EncodingTable.GetEncodingData();

		// Token: 0x0400135D RID: 4957
		[SecurityCritical]
		internal unsafe static InternalCodePageDataItem* codePageDataPtr = EncodingTable.GetCodePageData();

		// Token: 0x0400135E RID: 4958
		private static Hashtable hashByName = Hashtable.Synchronized(new Hashtable(StringComparer.OrdinalIgnoreCase));

		// Token: 0x0400135F RID: 4959
		private static Hashtable hashByCodePage = Hashtable.Synchronized(new Hashtable());
	}
}
