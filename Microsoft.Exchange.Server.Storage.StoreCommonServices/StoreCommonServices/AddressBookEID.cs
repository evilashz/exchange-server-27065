using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200001C RID: 28
	public static class AddressBookEID
	{
		// Token: 0x06000107 RID: 263 RVA: 0x0000F3B0 File Offset: 0x0000D5B0
		private static bool ArrayMatch(byte[] entryid, int offset, byte[] valueToMatch)
		{
			if (entryid.Length < offset + valueToMatch.Length)
			{
				DiagnosticContext.TraceLocation((LID)40936U);
				return false;
			}
			for (int i = 0; i < valueToMatch.Length; i++)
			{
				if (entryid[i + offset] != valueToMatch[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000F3F4 File Offset: 0x0000D5F4
		internal static byte[] MakeSearchKey(string addrType, string emailAddr)
		{
			if (emailAddr == null)
			{
				emailAddr = string.Empty;
			}
			if (addrType == null)
			{
				addrType = string.Empty;
			}
			addrType = addrType.ToUpperInvariant();
			emailAddr = emailAddr.ToUpperInvariant();
			byte[] array = new byte[addrType.Length + 1 + emailAddr.Length + 1];
			int num = 0;
			ParseSerialize.SetASCIIString(array, ref num, addrType);
			array[num - 1] = 58;
			ParseSerialize.SetASCIIString(array, ref num, emailAddr);
			return array;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000F458 File Offset: 0x0000D658
		public static byte[] MakeOneOffEntryID(string addrType, string emailAddr, string displayName, bool getSendRichInfo, int getSendInternetEncoding)
		{
			uint dataType = (uint)(268435456 | (getSendRichInfo ? 0 : 65536) | getSendInternetEncoding);
			return AddressBookEID.MakeOneOffEntryID(addrType, emailAddr, displayName, dataType);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000F484 File Offset: 0x0000D684
		public static byte[] EnsureOneOffEntryIdUnicodeEncoding(Context context, byte[] entryId)
		{
			string addrType = null;
			string emailAddr = null;
			string displayName = null;
			MapiAPIFlags mapiAPIFlags;
			if (entryId != null && AddressBookEID.IsOneOffEntryId(context, entryId, out mapiAPIFlags, ref addrType, ref emailAddr, ref displayName) && (mapiAPIFlags & (MapiAPIFlags)2147483648U) == (MapiAPIFlags)0U)
			{
				entryId = AddressBookEID.MakeOneOffEntryID(addrType, emailAddr, displayName, (uint)mapiAPIFlags);
			}
			return entryId;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000F4C0 File Offset: 0x0000D6C0
		private static byte[] MakeOneOffEntryID(string addrType, string emailAddr, string displayName, uint dataType)
		{
			if (emailAddr == null)
			{
				emailAddr = string.Empty;
			}
			if (addrType == null)
			{
				addrType = string.Empty;
			}
			if (displayName == null || displayName.Length == 0)
			{
				displayName = emailAddr;
			}
			int num = 4 + AddressBookEID.ExchAddrGuid.Length + 4 + (displayName.Length + 1) * 2 + (addrType.Length + 1) * 2 + (emailAddr.Length + 1) * 2;
			byte[] array = new byte[num];
			int num2 = 0;
			ParseSerialize.SetDword(array, ref num2, 0);
			Buffer.BlockCopy(AddressBookEID.OneOffGuid, 0, array, num2, AddressBookEID.ExchAddrGuid.Length);
			num2 += AddressBookEID.OneOffGuid.Length;
			dataType |= 2147483648U;
			ParseSerialize.SetDword(array, ref num2, dataType);
			ParseSerialize.SetUnicodeString(array, ref num2, displayName);
			ParseSerialize.SetUnicodeString(array, ref num2, addrType);
			ParseSerialize.SetUnicodeString(array, ref num2, emailAddr);
			return array;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000F57C File Offset: 0x0000D77C
		internal static string SzAddrFromABEID(byte[] entryId)
		{
			int num = 28;
			int num2 = entryId.Length - num;
			if (entryId[entryId.Length - 1] == 0)
			{
				num2--;
			}
			return CTSGlobals.AsciiEncoding.GetString(entryId, num, num2);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000F5AC File Offset: 0x0000D7AC
		public static byte[] MakeAddressBookEntryID(string legacyDN, bool isDL)
		{
			byte[] array = new byte[AddressBookEID.AddressBookEntryIdSize + legacyDN.Length + 1];
			Eidt dw = isDL ? Eidt.List : Eidt.User;
			int num = 0;
			ParseSerialize.SetDword(array, ref num, 0);
			Buffer.BlockCopy(AddressBookEID.ExchAddrGuid, 0, array, num, AddressBookEID.ExchAddrGuid.Length);
			num += AddressBookEID.ExchAddrGuid.Length;
			ParseSerialize.SetDword(array, ref num, 1);
			ParseSerialize.SetDword(array, ref num, (int)dw);
			ParseSerialize.SetASCIIString(array, ref num, legacyDN.ToUpperInvariant());
			return array;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000F620 File Offset: 0x0000D820
		public static bool IsAddressBookEntryId(Context context, byte[] entryId)
		{
			Eidt eidt;
			string text;
			return AddressBookEID.IsAddressBookEntryId(context, entryId, out eidt, out text);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000F638 File Offset: 0x0000D838
		public static bool IsAddressBookEntryId(Context context, byte[] entryId, out Eidt eidt, out string emailAddr)
		{
			int num = 0;
			int num2 = entryId.Length;
			eidt = Eidt.User;
			emailAddr = null;
			if (num2 <= AddressBookEID.AddressBookEntryIdSize || !AddressBookEID.ArrayMatch(entryId, AddressBookEID.SkipEntryIdFlagsOffset, AddressBookEID.ExchAddrGuid) || entryId[num2 - 1] != 0)
			{
				return false;
			}
			num += AddressBookEID.SkipEntryIdFlagsOffset + AddressBookEID.ExchAddrGuid.Length + 4;
			uint num3;
			if (!ParseSerialize.TryGetDword(entryId, ref num, num2, out num3))
			{
				return false;
			}
			eidt = (Eidt)num3;
			return ParseSerialize.TryGetStringFromASCII(entryId, ref num, num2, out emailAddr);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000F6A8 File Offset: 0x0000D8A8
		public static bool IsOneOffEntryId(Context context, byte[] entryId, out MapiAPIFlags dataType, ref string addrType, ref string emailAddr, ref string dispName)
		{
			int num = 0;
			int posMax = entryId.Length;
			bool flag = false;
			Guid empty = Guid.Empty;
			string text = null;
			string text2 = null;
			string text3 = null;
			dataType = (MapiAPIFlags)0U;
			if (!AddressBookEID.ArrayMatch(entryId, AddressBookEID.SkipEntryIdFlagsOffset, AddressBookEID.OneOffGuid))
			{
				return flag;
			}
			num += AddressBookEID.SkipEntryIdFlagsOffset + AddressBookEID.OneOffGuid.Length;
			flag = true;
			uint num2;
			if (!ParseSerialize.TryGetDword(entryId, ref num, posMax, out num2))
			{
				flag = false;
			}
			if (flag)
			{
				dataType = (MapiAPIFlags)num2;
				if ((MapiAPIFlags)2147483648U == (dataType & (MapiAPIFlags)2147483648U))
				{
					if (!ParseSerialize.TryGetStringFromUnicode(entryId, ref num, posMax, out text3) || !ParseSerialize.TryGetStringFromUnicode(entryId, ref num, posMax, out text) || !ParseSerialize.TryGetStringFromUnicode(entryId, ref num, posMax, out text2))
					{
						flag = false;
					}
				}
				else if (!ParseSerialize.TryGetStringFromASCII(entryId, ref num, posMax, out text3) || !ParseSerialize.TryGetStringFromASCII(entryId, ref num, posMax, out text) || !ParseSerialize.TryGetStringFromASCII(entryId, ref num, posMax, out text2))
				{
					flag = false;
				}
			}
			if (flag)
			{
				if (dispName == null)
				{
					dispName = text3;
				}
				if (addrType == null)
				{
					addrType = text;
				}
				if (emailAddr == null)
				{
					emailAddr = text2;
				}
			}
			return flag;
		}

		// Token: 0x040001C7 RID: 455
		private const uint OOPSimple = 0U;

		// Token: 0x040001C8 RID: 456
		private const uint OOPCont = 1U;

		// Token: 0x040001C9 RID: 457
		private const uint OOPNewEntry = 2U;

		// Token: 0x040001CA RID: 458
		private const uint OOPUnicode = 2147483648U;

		// Token: 0x040001CB RID: 459
		private const uint OOPNoTnef = 65536U;

		// Token: 0x040001CC RID: 460
		private const uint OOPDontLookup = 268435456U;

		// Token: 0x040001CD RID: 461
		private const uint OOPExtended = 1073741824U;

		// Token: 0x040001CE RID: 462
		private const uint DataTypeMask = 65535U;

		// Token: 0x040001CF RID: 463
		private const uint SendInternetEncodingMask = 8257536U;

		// Token: 0x040001D0 RID: 464
		private const uint NontransmittableMask = 805306368U;

		// Token: 0x040001D1 RID: 465
		internal static readonly int BytesEntryIdFlags = 4;

		// Token: 0x040001D2 RID: 466
		internal static readonly int SkipEntryIdFlagsOffset = AddressBookEID.BytesEntryIdFlags;

		// Token: 0x040001D3 RID: 467
		internal static readonly int AddressBookEntryIdSize = 28;

		// Token: 0x040001D4 RID: 468
		internal static readonly uint AddressBookEntryIdVersion = 1U;

		// Token: 0x040001D5 RID: 469
		internal static readonly byte[] ExchAddrGuid = new byte[]
		{
			220,
			167,
			64,
			200,
			192,
			66,
			16,
			26,
			180,
			185,
			8,
			0,
			43,
			47,
			225,
			130
		};

		// Token: 0x040001D6 RID: 470
		internal static readonly int OneOffEntryIdSize = 24;

		// Token: 0x040001D7 RID: 471
		internal static readonly byte[] OneOffGuid = new byte[]
		{
			129,
			43,
			31,
			164,
			190,
			163,
			16,
			25,
			157,
			110,
			0,
			221,
			1,
			15,
			84,
			2
		};
	}
}
