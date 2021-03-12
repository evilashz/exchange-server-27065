using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001B3 RID: 435
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class AddressBookEntryId
	{
		// Token: 0x060017C7 RID: 6087 RVA: 0x000746BC File Offset: 0x000728BC
		private static bool ArrayMatch(byte[] entryid, int offset, byte[] valueToMatch)
		{
			for (int i = 0; i < valueToMatch.Length; i++)
			{
				if (entryid[i + offset] != valueToMatch[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x000746E4 File Offset: 0x000728E4
		public static byte[] MakeAddressBookEntryID(IExchangePrincipal exchangePrincipal)
		{
			Util.ThrowOnNullArgument(exchangePrincipal, "exchangePrincipal");
			return AddressBookEntryId.MakeAddressBookEntryID(exchangePrincipal.LegacyDn, false);
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x00074700 File Offset: 0x00072900
		public static byte[] MakeAddressBookEntryID(string legacyDN, bool isDL)
		{
			Eidt eidt = isDL ? Eidt.List : Eidt.User;
			return AddressBookEntryId.MakeAddressBookEntryID(legacyDN, eidt);
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x0007471C File Offset: 0x0007291C
		public static byte[] MakeAddressBookEntryID(string legacyDN, Eidt eidt)
		{
			Util.ThrowOnNullOrEmptyArgument(legacyDN, "legacyDN");
			EnumValidator.ThrowIfInvalid<Eidt>(eidt, "eidt");
			byte[] array = new byte[AddressBookEntryId.AddressBookEntryIdSize + legacyDN.Length + 1];
			int num = 0;
			AddressBookEntryId.BinaryHelper.SetDword(array, ref num, 0U);
			Buffer.BlockCopy(AddressBookEntryId.ExchAddrGuid, 0, array, num, AddressBookEntryId.ExchAddrGuid.Length);
			num += AddressBookEntryId.ExchAddrGuid.Length;
			AddressBookEntryId.BinaryHelper.SetDword(array, ref num, AddressBookEntryId.AddressBookEntryIdVersion);
			AddressBookEntryId.BinaryHelper.SetDword(array, ref num, (uint)eidt);
			AddressBookEntryId.BinaryHelper.SetASCIIString(array, ref num, legacyDN);
			return array;
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x0007479C File Offset: 0x0007299C
		public static byte[] MakeAddressBookEntryIDFromLocalDirectorySid(SecurityIdentifier sid)
		{
			if (!ExternalUser.IsExternalUserSid(sid))
			{
				throw new InvalidParamException(ServerStrings.InvalidLocalDirectorySecurityIdentifier(sid.ToString()));
			}
			byte[] array = new byte[AddressBookEntryId.AddressBookEntryIdSize + sid.BinaryLength];
			Eidt dw = Eidt.User;
			int num = 0;
			AddressBookEntryId.BinaryHelper.SetDword(array, ref num, 0U);
			Buffer.BlockCopy(AddressBookEntryId.MuidLocalDirectoryUser, 0, array, num, AddressBookEntryId.MuidLocalDirectoryUser.Length);
			num += AddressBookEntryId.ExchAddrGuid.Length;
			AddressBookEntryId.BinaryHelper.SetDword(array, ref num, AddressBookEntryId.AddressBookEntryIdVersion);
			AddressBookEntryId.BinaryHelper.SetDword(array, ref num, (uint)dw);
			sid.GetBinaryForm(array, num);
			return array;
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x00074820 File Offset: 0x00072A20
		public static bool IsAddressBookEntryId(byte[] entryId)
		{
			Eidt eidt;
			string text;
			return AddressBookEntryId.IsAddressBookEntryId(entryId, out eidt, out text);
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x00074838 File Offset: 0x00072A38
		public static bool IsAddressBookEntryId(byte[] entryId, out Eidt eidt, out string emailAddr)
		{
			eidt = Eidt.User;
			emailAddr = null;
			if (entryId == null)
			{
				return false;
			}
			int num = 0;
			int num2 = entryId.Length;
			bool result = false;
			if (num2 <= AddressBookEntryId.AddressBookEntryIdSize || !AddressBookEntryId.ArrayMatch(entryId, AddressBookEntryId.SkipEntryIdFlagsOffset, AddressBookEntryId.ExchAddrGuid) || entryId[num2 - 1] != 0)
			{
				return result;
			}
			num += AddressBookEntryId.SkipEntryIdFlagsOffset + AddressBookEntryId.ExchAddrGuid.Length + AddressBookEntryId.BinaryHelper.DWordSize;
			try
			{
				eidt = (Eidt)AddressBookEntryId.BinaryHelper.GetDword(entryId, ref num, num2);
				emailAddr = AddressBookEntryId.BinaryHelper.GetStringFromASCII(entryId, ref num, num2);
				result = true;
			}
			catch (ArgumentOutOfRangeException)
			{
			}
			return result;
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x000748C0 File Offset: 0x00072AC0
		public static bool IsLocalDirctoryAddressBookEntryId(byte[] entryId)
		{
			return entryId != null && entryId.Length >= AddressBookEntryId.MinLocalDirectoryAddressBookEntryIdSize && entryId.Length <= AddressBookEntryId.MaxLocalDirectoryAddressBookEntryIdSize && AddressBookEntryId.ArrayMatch(entryId, AddressBookEntryId.SkipEntryIdFlagsOffset, AddressBookEntryId.MuidLocalDirectoryUser);
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x000748EE File Offset: 0x00072AEE
		public static SecurityIdentifier MakeSidFromLocalDirctoryAddressBookEntryId(byte[] entryId)
		{
			if (!AddressBookEntryId.IsLocalDirctoryAddressBookEntryId(entryId))
			{
				throw new InvalidParamException(new LocalizedString("Invalid local directory address book entry ID."));
			}
			return new SecurityIdentifier(entryId, AddressBookEntryId.AddressBookEntryIdSize);
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x00074914 File Offset: 0x00072B14
		public static string MakeLegacyDnFromLocalDirctoryAddressBookEntryId(byte[] entryId)
		{
			SecurityIdentifier securityIdentifier = AddressBookEntryId.MakeSidFromLocalDirctoryAddressBookEntryId(entryId);
			return string.Format("{0}{1}", "LocalUser:", securityIdentifier.ToString());
		}

		// Token: 0x04000C87 RID: 3207
		internal const string LocalDirectoryUserLegacyDnPrefix = "LocalUser:";

		// Token: 0x04000C88 RID: 3208
		private static readonly int BytesEntryIdFlags = 4;

		// Token: 0x04000C89 RID: 3209
		private static readonly int SkipEntryIdFlagsOffset = AddressBookEntryId.BytesEntryIdFlags;

		// Token: 0x04000C8A RID: 3210
		private static readonly int AddressBookEntryIdSize = 4 * AddressBookEntryId.BinaryHelper.ByteSize + AddressBookEntryId.BinaryHelper.GuidSize + AddressBookEntryId.BinaryHelper.DWordSize + AddressBookEntryId.BinaryHelper.DWordSize;

		// Token: 0x04000C8B RID: 3211
		private static readonly int MinLocalDirectoryAddressBookEntryIdSize = AddressBookEntryId.AddressBookEntryIdSize + 2 * AddressBookEntryId.BinaryHelper.DWordSize + AddressBookEntryId.BinaryHelper.GuidSize;

		// Token: 0x04000C8C RID: 3212
		private static readonly int MaxLocalDirectoryAddressBookEntryIdSize = AddressBookEntryId.AddressBookEntryIdSize + 2 * AddressBookEntryId.BinaryHelper.DWordSize + AddressBookEntryId.BinaryHelper.GuidSize + 4;

		// Token: 0x04000C8D RID: 3213
		private static readonly uint AddressBookEntryIdVersion = 1U;

		// Token: 0x04000C8E RID: 3214
		private static readonly byte[] ExchAddrGuid = new byte[]
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

		// Token: 0x04000C8F RID: 3215
		private static readonly byte[] MuidLocalDirectoryUser = new byte[]
		{
			212,
			186,
			25,
			39,
			241,
			181,
			79,
			27,
			184,
			59,
			20,
			115,
			118,
			55,
			226,
			105
		};

		// Token: 0x020001B4 RID: 436
		private static class BinaryHelper
		{
			// Token: 0x060017D2 RID: 6098 RVA: 0x00074A01 File Offset: 0x00072C01
			private static void CheckBounds(int pos, int posMax, int sizeNeeded)
			{
				if (posMax < pos + sizeNeeded)
				{
					throw new ArgumentOutOfRangeException();
				}
			}

			// Token: 0x060017D3 RID: 6099 RVA: 0x00074A0F File Offset: 0x00072C0F
			internal static void SetDword(byte[] buff, ref int pos, uint dw)
			{
				if (buff != null)
				{
					AddressBookEntryId.BinaryHelper.CheckBounds(pos, buff.Length, AddressBookEntryId.BinaryHelper.DWordSize);
					ExBitConverter.Write(dw, buff, pos);
				}
				pos += AddressBookEntryId.BinaryHelper.DWordSize;
			}

			// Token: 0x060017D4 RID: 6100 RVA: 0x00074A38 File Offset: 0x00072C38
			internal static void SetASCIIString(byte[] buff, ref int pos, string str)
			{
				if (buff != null)
				{
					AddressBookEntryId.BinaryHelper.CheckBounds(pos, buff.Length, str.Length + 1);
					CTSGlobals.AsciiEncoding.GetBytes(str, 0, str.Length, buff, pos);
					buff[pos + str.Length] = 0;
				}
				pos += str.Length + 1;
			}

			// Token: 0x060017D5 RID: 6101 RVA: 0x00074A8C File Offset: 0x00072C8C
			internal static uint GetDword(byte[] buff, ref int pos, int posMax)
			{
				AddressBookEntryId.BinaryHelper.CheckBounds(pos, posMax, AddressBookEntryId.BinaryHelper.DWordSize);
				uint result = (uint)BitConverter.ToInt32(buff, pos);
				pos += AddressBookEntryId.BinaryHelper.DWordSize;
				return result;
			}

			// Token: 0x060017D6 RID: 6102 RVA: 0x00074ABC File Offset: 0x00072CBC
			internal static string GetStringFromASCII(byte[] buff, ref int pos, int posMax)
			{
				int num = 0;
				while (pos + num <= posMax && buff[pos + num] != 0)
				{
					num++;
				}
				if (pos + num > posMax)
				{
					throw new ArgumentOutOfRangeException();
				}
				return AddressBookEntryId.BinaryHelper.GetStringFromASCII(buff, ref pos, posMax, num + AddressBookEntryId.BinaryHelper.ByteSize);
			}

			// Token: 0x060017D7 RID: 6103 RVA: 0x00074AFC File Offset: 0x00072CFC
			internal static string GetStringFromASCII(byte[] buff, ref int pos, int posMax, int charCount)
			{
				AddressBookEntryId.BinaryHelper.CheckBounds(pos, posMax, charCount);
				string @string = CTSGlobals.AsciiEncoding.GetString(buff, pos, charCount - 1);
				pos += charCount;
				return @string;
			}

			// Token: 0x04000C90 RID: 3216
			internal static readonly int ByteSize = Marshal.SizeOf(typeof(byte));

			// Token: 0x04000C91 RID: 3217
			internal static readonly int DWordSize = Marshal.SizeOf(typeof(uint));

			// Token: 0x04000C92 RID: 3218
			internal static readonly int GuidSize = Marshal.SizeOf(typeof(Guid));
		}
	}
}
