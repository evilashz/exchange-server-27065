using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.ContentTypes.Tnef;

namespace Microsoft.Exchange.Data.MsgStorage.Internal
{
	// Token: 0x020000BD RID: 189
	internal static class Util
	{
		// Token: 0x06000634 RID: 1588 RVA: 0x0001BA96 File Offset: 0x00019C96
		internal static void ThrowOnNullArgument(object argument, string argumentName)
		{
			if (argument == null)
			{
				throw new ArgumentNullException(argumentName);
			}
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0001BAA2 File Offset: 0x00019CA2
		internal static void ThrowOnOutOfRange(bool condition, string argumentName)
		{
			if (!condition)
			{
				throw new ArgumentOutOfRangeException(argumentName);
			}
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001BAAE File Offset: 0x00019CAE
		internal static void ThrowOnFailure(MsgStorageErrorCode errorCode, int hResult, string errorMessage)
		{
			if (hResult != 0)
			{
				throw new MsgStorageException(errorCode, errorMessage, hResult);
			}
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0001BABC File Offset: 0x00019CBC
		internal static string AttachmentStorageName(int attachmentIndex)
		{
			return string.Format(CultureInfo.InvariantCulture, "__attach_version1.0_#{0:X8}", new object[]
			{
				attachmentIndex
			});
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0001BAEC File Offset: 0x00019CEC
		internal static string RecipientStorageName(int recipientIndex)
		{
			return string.Format(CultureInfo.InvariantCulture, "__recip_version1.0_#{0:X8}", new object[]
			{
				recipientIndex
			});
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0001BB1C File Offset: 0x00019D1C
		internal static string PropertyStreamName(TnefPropertyTag propertyTag)
		{
			return string.Format(CultureInfo.InvariantCulture, "__substg1.0_{0:X8}", new object[]
			{
				(uint)propertyTag
			});
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001BB50 File Offset: 0x00019D50
		internal static string PropertyStreamName(TnefPropertyTag propertyTag, int index)
		{
			return string.Format(CultureInfo.InvariantCulture, "__substg1.0_{0:X8}-{1:X8}", new object[]
			{
				(uint)propertyTag,
				index
			});
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0001BB8B File Offset: 0x00019D8B
		internal static int GetUnicodeByteCount(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return 2;
			}
			if (value[value.Length - 1] == '\0')
			{
				return Util.UnicodeEncoding.GetByteCount(value);
			}
			return Util.UnicodeEncoding.GetByteCount(value) + 2;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0001BBC0 File Offset: 0x00019DC0
		internal static int StringToUnicodeBytes(string value, byte[] buffer)
		{
			if (string.IsNullOrEmpty(value))
			{
				buffer[1] = (buffer[0] = 0);
				return 2;
			}
			if (value[value.Length - 1] == '\0')
			{
				return Util.UnicodeEncoding.GetBytes(value, 0, value.Length, buffer, 0);
			}
			int bytes = Util.UnicodeEncoding.GetBytes(value, 0, value.Length, buffer, 0);
			buffer[bytes++] = 0;
			buffer[bytes++] = 0;
			return bytes;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0001BC2C File Offset: 0x00019E2C
		internal static string UnicodeBytesToString(byte[] bytes, int length)
		{
			if (length >= 2 && bytes[length - 1] == 0 && bytes[length - 2] == 0)
			{
				length -= 2;
			}
			return Util.UnicodeEncoding.GetString(bytes, 0, length);
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0001BC52 File Offset: 0x00019E52
		internal static string AnsiBytesToString(byte[] bytes, int length, Encoding encoding)
		{
			if (length >= 1 && bytes[length - 1] == 0)
			{
				length--;
			}
			return encoding.GetString(bytes, 0, length);
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0001BC70 File Offset: 0x00019E70
		internal static void InvokeComCall(MsgStorageErrorCode errorCode, Util.ComCall comCall)
		{
			try
			{
				comCall();
			}
			catch (IOException exc)
			{
				throw new MsgStorageException(errorCode, MsgStorageStrings.ComExceptionThrown, exc);
			}
			catch (COMException ex)
			{
				if (ex.ErrorCode == -2147287038)
				{
					throw new MsgStorageNotFoundException(errorCode, MsgStorageStrings.ComExceptionThrown, ex);
				}
				throw new MsgStorageException(errorCode, MsgStorageStrings.ComExceptionThrown, ex);
			}
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0001BCD8 File Offset: 0x00019ED8
		// Note: this type is marked as 'beforefieldinit'.
		static Util()
		{
			byte[] emptyStringBytes = new byte[2];
			Util.EmptyStringBytes = emptyStringBytes;
			Util.EmptyByteArray = new byte[0];
			Util.ClassIdFileAttachment = new Guid("00020D05-0000-0000-C000-000000000046");
			Util.ClassIdMessageAttachment = new Guid("00020D09-0000-0000-C000-000000000046");
			Util.ClassIdMessage = new Guid("00020D0B-0000-0000-C000-000000000046");
			Util.UnicodeEncoding = new UnicodeEncoding(false, false);
		}

		// Token: 0x040005CD RID: 1485
		internal const string PropertiesStreamName = "__properties_version1.0";

		// Token: 0x040005CE RID: 1486
		internal const string NamedStorageName = "__nameid_version1.0";

		// Token: 0x040005CF RID: 1487
		internal const string NamedEntriesStreamName = "__substg1.0_00030102";

		// Token: 0x040005D0 RID: 1488
		internal const string NamedGuidStreamName = "__substg1.0_00020102";

		// Token: 0x040005D1 RID: 1489
		internal const string NamedStringsStreamName = "__substg1.0_00040102";

		// Token: 0x040005D2 RID: 1490
		private const string RecipientStorageNameFormat = "__recip_version1.0_#{0:X8}";

		// Token: 0x040005D3 RID: 1491
		private const string AttachmentStorageNameFormat = "__attach_version1.0_#{0:X8}";

		// Token: 0x040005D4 RID: 1492
		private const string PropertyStreamNameFormat = "__substg1.0_{0:X8}";

		// Token: 0x040005D5 RID: 1493
		private const string PropertyIndexStreamNameFormat = "__substg1.0_{0:X8}-{1:X8}";

		// Token: 0x040005D6 RID: 1494
		private const int ErrorCodeObjectNotFound = -2147287038;

		// Token: 0x040005D7 RID: 1495
		internal const int MaxShortValueLength = 32768;

		// Token: 0x040005D8 RID: 1496
		internal const int MaxMultivaluedArraySize = 2048;

		// Token: 0x040005D9 RID: 1497
		internal const int AttachMethodMessage = 5;

		// Token: 0x040005DA RID: 1498
		internal const int AttachMethodOle = 6;

		// Token: 0x040005DB RID: 1499
		internal static readonly byte[] EmptyStringBytes;

		// Token: 0x040005DC RID: 1500
		internal static readonly byte[] EmptyByteArray;

		// Token: 0x040005DD RID: 1501
		internal static readonly Guid ClassIdFileAttachment;

		// Token: 0x040005DE RID: 1502
		internal static readonly Guid ClassIdMessageAttachment;

		// Token: 0x040005DF RID: 1503
		internal static readonly Guid ClassIdMessage;

		// Token: 0x040005E0 RID: 1504
		internal static readonly Encoding UnicodeEncoding;

		// Token: 0x020000BE RID: 190
		// (Invoke) Token: 0x06000642 RID: 1602
		internal delegate void ComCall();
	}
}
