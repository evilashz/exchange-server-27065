using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000005 RID: 5
	internal static class ImapEntryId
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002624 File Offset: 0x00000824
		public static byte[] CreateFolderEntryId(string input)
		{
			string hash = ImapEntryId.GetHash(input);
			return Encoding.UTF8.GetBytes(string.Format(CultureInfo.InvariantCulture, "{0}={1};{2}={3};{4}={5}", new object[]
			{
				"V",
				"1",
				"P",
				"IMAP",
				"FP",
				hash
			}));
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002688 File Offset: 0x00000888
		public static byte[] CreateMessageEntryId(uint uid, uint uidValidity, string folderPath, string logonName)
		{
			ImapEntryId.ValidateCreateEntryIdInput(folderPath);
			ImapEntryId.ValidateCreateEntryIdInput(logonName);
			string hash = ImapEntryId.GetHash(folderPath + logonName);
			return Encoding.UTF8.GetBytes(string.Format(CultureInfo.InvariantCulture, "{0}={1};{2}={3};{4}={5};{6}={7};{8}={9}", new object[]
			{
				"V",
				"1",
				"P",
				"IMAP",
				"U",
				uid,
				"UV",
				uidValidity,
				"LF",
				hash
			}));
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002724 File Offset: 0x00000924
		public static uint ParseUid(byte[] messageEntryId)
		{
			Dictionary<string, string> dictionary = ImapEntryId.ParseMessageEntryId(messageEntryId);
			string s;
			uint result;
			if (dictionary.TryGetValue("U", out s) && uint.TryParse(s, out result))
			{
				return result;
			}
			throw new ParsingMessageEntryIdFailedException(TraceUtils.DumpBytes(messageEntryId), new ArgumentException("Cannot parse uid.", "messageEntryId"));
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002770 File Offset: 0x00000970
		private static string GetHash(string input)
		{
			ImapEntryId.ValidateCreateEntryIdInput(input);
			byte[] sha1Hash = CommonUtils.GetSHA1Hash(input.ToLowerInvariant());
			StringBuilder stringBuilder = new StringBuilder(BitConverter.ToString(sha1Hash));
			stringBuilder = stringBuilder.Replace("-", string.Empty);
			return stringBuilder.ToString();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000027B4 File Offset: 0x000009B4
		private static Dictionary<string, string> ParseMessageEntryId(byte[] messageEntryId)
		{
			if (messageEntryId == null)
			{
				throw new ParsingMessageEntryIdFailedException(null, new ArgumentNullException("messageEntryId"));
			}
			string text = null;
			try
			{
				text = Encoding.UTF8.GetString(messageEntryId);
			}
			catch (Exception innerException)
			{
				throw new ParsingMessageEntryIdFailedException(TraceUtils.DumpBytes(messageEntryId), innerException);
			}
			string[] keyValuePairs = text.Split(new char[]
			{
				';'
			});
			return ImapEntryId.ParseKeyValuePairs(messageEntryId, keyValuePairs);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002820 File Offset: 0x00000A20
		private static Dictionary<string, string> ParseKeyValuePairs(byte[] messageEntryId, string[] keyValuePairs)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(ImapEntryId.MessageEntryIdKeys.Length);
			foreach (string text in keyValuePairs)
			{
				string[] array = text.Split(new char[]
				{
					'='
				});
				if (array.Length == 2)
				{
					string text2 = array[0];
					string text3 = array[1];
					if (string.IsNullOrEmpty(text2) || string.IsNullOrEmpty(text3))
					{
						throw new ParsingMessageEntryIdFailedException(TraceUtils.DumpBytes(messageEntryId), new ArgumentException("messageEntryId", string.Format("While parsing message entry id, key {0} value {1}", text2, text3)));
					}
					if (dictionary.ContainsKey(text2))
					{
						throw new ParsingMessageEntryIdFailedException(TraceUtils.DumpBytes(messageEntryId), new ArgumentException("messageEntryId", string.Format("Duplicate key {0} found while parsing message entry id.", text2)));
					}
					dictionary.Add(text2, text3);
				}
			}
			foreach (string text4 in ImapEntryId.MessageEntryIdKeys)
			{
				if (!dictionary.ContainsKey(text4))
				{
					throw new ParsingMessageEntryIdFailedException(TraceUtils.DumpBytes(messageEntryId), new ArgumentException("messageEntryId", string.Format("Key {0} not found in result.", text4)));
				}
			}
			string text5 = dictionary["V"];
			if (!text5.Equals("1", StringComparison.InvariantCultureIgnoreCase))
			{
				throw new UnsupportedImapMessageEntryIdVersionException(text5);
			}
			string text6 = dictionary["P"];
			if (!text6.Equals("IMAP", StringComparison.InvariantCultureIgnoreCase))
			{
				throw new UnsupportedSyncProtocolException(text6);
			}
			return dictionary;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000297F File Offset: 0x00000B7F
		private static void ValidateCreateEntryIdInput(string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				throw new CannotCreateEntryIdException(input, new ArgumentNullException("input"));
			}
		}

		// Token: 0x0400000E RID: 14
		public const char EntryIdDelimiter = ';';

		// Token: 0x0400000F RID: 15
		public const char EntryIdKeyValueSeparator = '=';

		// Token: 0x04000010 RID: 16
		public const string EntryIdVersionKey = "V";

		// Token: 0x04000011 RID: 17
		public const string EntryIdVersionValue = "1";

		// Token: 0x04000012 RID: 18
		public const string EntryIdProtocolKey = "P";

		// Token: 0x04000013 RID: 19
		public const string EntryIdProtocolValue = "IMAP";

		// Token: 0x04000014 RID: 20
		public const string EntryIdUidKey = "U";

		// Token: 0x04000015 RID: 21
		public const string EntryIdUidValidityKey = "UV";

		// Token: 0x04000016 RID: 22
		public const string EntryIdLogonNameAndFolderPathHashKey = "LF";

		// Token: 0x04000017 RID: 23
		private const string EntryIdFolderPathHashKey = "FP";

		// Token: 0x04000018 RID: 24
		private static readonly string[] MessageEntryIdKeys = new string[]
		{
			"V",
			"P",
			"U",
			"UV",
			"LF"
		};
	}
}
