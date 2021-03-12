using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x02000003 RID: 3
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class AdrList
	{
		// Token: 0x06000008 RID: 8 RVA: 0x0000229C File Offset: 0x0000049C
		internal unsafe static AdrEntry[] Unmarshal(_AdrList* padrList)
		{
			AdrEntry[] array = new AdrEntry[padrList->cEntries];
			_AdrEntry* ptr = &padrList->adrEntry1;
			uint num = 0U;
			while ((ulong)num < (ulong)((long)padrList->cEntries))
			{
				array[(int)((UIntPtr)num)] = new AdrEntry(ptr + (ulong)num * (ulong)((long)sizeof(_AdrEntry)) / (ulong)sizeof(_AdrEntry));
				num += 1U;
			}
			return array;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000022E8 File Offset: 0x000004E8
		internal static int GetBytesToMarshal(params AdrEntry[] adrEntries)
		{
			int num = _AdrList.SizeOf + 7 & -8;
			for (int i = 0; i < adrEntries.Length; i++)
			{
				num += adrEntries[i].GetBytesToMarshal();
			}
			return num;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000231C File Offset: 0x0000051C
		internal unsafe static void MarshalToNative(byte* pb, params AdrEntry[] adrEntries)
		{
			((_AdrList*)pb)->cEntries = adrEntries.Length;
			_AdrEntry* ptr = &((_AdrList*)pb)->adrEntry1;
			SPropValue* ptr2 = (SPropValue*)(pb + (_AdrList.SizeOf + 7 & -8) + (IntPtr)(_AdrEntry.SizeOf + 7 & -8) * (IntPtr)(adrEntries.Length - 1));
			int num = 0;
			for (int i = 0; i < adrEntries.Length; i++)
			{
				num += adrEntries[i].propValues.Length;
			}
			byte* ptr3 = (byte*)(ptr2 + (IntPtr)(SPropValue.SizeOf + 7 & -8) * (IntPtr)num / (IntPtr)sizeof(SPropValue));
			for (int j = 0; j < adrEntries.Length; j++)
			{
				adrEntries[j].MarshalToNative(ptr, ref ptr2, ref ptr3);
				ptr++;
			}
		}
	}
}
