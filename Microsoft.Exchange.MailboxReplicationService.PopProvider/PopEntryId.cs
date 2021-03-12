using System;
using System.Globalization;
using System.Text;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000002 RID: 2
	internal static class PopEntryId
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public static byte[] CreateFolderEntryId(string input)
		{
			string hash = PopEntryId.GetHash(input);
			return Encoding.UTF8.GetBytes(string.Format(CultureInfo.InvariantCulture, "{0}={1};{2}={3};{4}={5}", new object[]
			{
				"V",
				"1",
				"P",
				"POP",
				"FP",
				hash
			}));
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002133 File Offset: 0x00000333
		public static byte[] CreateMessageEntryId(string uid)
		{
			return Encoding.UTF8.GetBytes(uid);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002140 File Offset: 0x00000340
		public static string ParseUid(byte[] messageEntryId)
		{
			return Encoding.UTF8.GetString(messageEntryId);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002150 File Offset: 0x00000350
		private static string GetHash(string input)
		{
			byte[] sha1Hash = CommonUtils.GetSHA1Hash(input.ToLowerInvariant());
			StringBuilder stringBuilder = new StringBuilder(BitConverter.ToString(sha1Hash));
			stringBuilder = stringBuilder.Replace("-", string.Empty);
			return stringBuilder.ToString();
		}

		// Token: 0x04000001 RID: 1
		public const char EntryIdDelimiter = ';';

		// Token: 0x04000002 RID: 2
		public const char EntryIdKeyValueSeparator = '=';

		// Token: 0x04000003 RID: 3
		public const string EntryIdVersionKey = "V";

		// Token: 0x04000004 RID: 4
		public const string EntryIdVersionValue = "1";

		// Token: 0x04000005 RID: 5
		public const string EntryIdProtocolKey = "P";

		// Token: 0x04000006 RID: 6
		public const string EntryIdProtocolValue = "POP";

		// Token: 0x04000007 RID: 7
		private const string EntryIdFolderPathHashKey = "FP";

		// Token: 0x04000008 RID: 8
		private static readonly string[] MessageEntryIdKeys = new string[]
		{
			"V",
			"P"
		};
	}
}
