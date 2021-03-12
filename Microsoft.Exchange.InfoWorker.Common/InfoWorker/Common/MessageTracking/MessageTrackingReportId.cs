using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002D1 RID: 721
	[Serializable]
	internal sealed class MessageTrackingReportId
	{
		// Token: 0x06001481 RID: 5249 RVA: 0x0005F8E4 File Offset: 0x0005DAE4
		public MessageTrackingReportId()
		{
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x0005F8EC File Offset: 0x0005DAEC
		public MessageTrackingReportId(string messageId, string server, long internalMessageId, SmtpAddress mailbox, string domain, bool isSender)
		{
			if (!mailbox.IsValidAddress)
			{
				throw new ArgumentException("Invalid mailbox", "mailbox");
			}
			this.mailbox = mailbox;
			this.server = server;
			this.messageId = messageId;
			this.internalMessageId = internalMessageId;
			this.domain = domain;
			this.isSender = isSender;
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x0005F945 File Offset: 0x0005DB45
		public MessageTrackingReportId(string messageId, string server, long internalMessageId, Guid guid, string domain, bool isSender)
		{
			this.userGuid = guid;
			this.server = server;
			this.messageId = messageId;
			this.internalMessageId = internalMessageId;
			this.domain = domain;
			this.isSender = isSender;
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x0005F97C File Offset: 0x0005DB7C
		public static bool TryParse(string identity, out MessageTrackingReportId identityObject)
		{
			if (identity == MessageTrackingReportId.LegacyExchangeValue)
			{
				identityObject = MessageTrackingReportId.LegacyExchange;
				return true;
			}
			string value = null;
			string value2 = null;
			long? num = null;
			Guid? guid = null;
			bool? flag = null;
			string text = null;
			identityObject = null;
			string[] array = identity.Split(new char[]
			{
				','
			});
			string[] array2 = array;
			int i = 0;
			while (i < array2.Length)
			{
				string text2 = array2[i];
				string[] array3 = text2.Split(new char[]
				{
					'='
				});
				bool result;
				string text3;
				if (array3.Length != 2 || string.IsNullOrEmpty(array3[1]))
				{
					result = false;
				}
				else if (!MessageTrackingReportId.TryUnescapeSpecialCharacters(array3[1], out text3))
				{
					result = false;
				}
				else
				{
					string key;
					if ((key = array3[0]) != null)
					{
						if (<PrivateImplementationDetails>{D2048E21-D4EF-4439-A685-E89D4FF3D975}.$$method0x6001364-1 == null)
						{
							<PrivateImplementationDetails>{D2048E21-D4EF-4439-A685-E89D4FF3D975}.$$method0x6001364-1 = new Dictionary<string, int>(6)
							{
								{
									"Message-Id",
									0
								},
								{
									"Server",
									1
								},
								{
									"Internal-Id",
									2
								},
								{
									"Sender",
									3
								},
								{
									"Recipient",
									4
								},
								{
									"Domain",
									5
								}
							};
						}
						int num2;
						if (<PrivateImplementationDetails>{D2048E21-D4EF-4439-A685-E89D4FF3D975}.$$method0x6001364-1.TryGetValue(key, out num2))
						{
							switch (num2)
							{
							case 0:
								value = text3;
								break;
							case 1:
								value2 = text3;
								break;
							case 2:
							{
								long value3;
								if (!long.TryParse(text3, out value3))
								{
									return false;
								}
								num = new long?(value3);
								break;
							}
							case 3:
							{
								Guid value4;
								if (!GuidHelper.TryParseGuid(text3, out value4))
								{
									TraceWrapper.SearchLibraryTracer.TraceError<string>(0, "The value {0} does not represent a valid GUID", text3);
									return false;
								}
								guid = new Guid?(value4);
								flag = new bool?(true);
								break;
							}
							case 4:
								guid = new Guid?(new Guid(text3));
								flag = new bool?(false);
								break;
							case 5:
								text = text3;
								break;
							default:
								goto IL_1C6;
							}
							i++;
							continue;
						}
					}
					IL_1C6:
					result = false;
				}
				return result;
			}
			if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(value2) || num == null || flag == null)
			{
				return false;
			}
			if (guid == null)
			{
				return false;
			}
			Guid value5 = guid.Value;
			identityObject = new MessageTrackingReportId(value, value2, num.Value, value5, text, flag.Value);
			return true;
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x0005FBBC File Offset: 0x0005DDBC
		public override string ToString()
		{
			if (this == MessageTrackingReportId.LegacyExchange)
			{
				return MessageTrackingReportId.LegacyExchangeValue;
			}
			string text = this.isSender ? "Sender" : "Recipient";
			return string.Format("{0}={1},{2}={3},{4}={5},{6}={7},{8}={9}", new object[]
			{
				"Message-Id",
				MessageTrackingReportId.EscapeSpecialCharacters(this.messageId),
				"Server",
				MessageTrackingReportId.EscapeSpecialCharacters(this.server),
				"Internal-Id",
				MessageTrackingReportId.EscapeSpecialCharacters(this.internalMessageId.ToString()),
				text,
				MessageTrackingReportId.EscapeSpecialCharacters(this.userGuid.ToString()),
				"Domain",
				MessageTrackingReportId.EscapeSpecialCharacters(this.domain)
			});
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06001486 RID: 5254 RVA: 0x0005FC7A File Offset: 0x0005DE7A
		public string MessageId
		{
			get
			{
				return this.messageId;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001487 RID: 5255 RVA: 0x0005FC82 File Offset: 0x0005DE82
		public long InternalMessageId
		{
			get
			{
				return this.internalMessageId;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001488 RID: 5256 RVA: 0x0005FC8A File Offset: 0x0005DE8A
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06001489 RID: 5257 RVA: 0x0005FC92 File Offset: 0x0005DE92
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x0600148A RID: 5258 RVA: 0x0005FC9A File Offset: 0x0005DE9A
		public SmtpAddress Mailbox
		{
			get
			{
				return this.mailbox;
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x0600148B RID: 5259 RVA: 0x0005FCA2 File Offset: 0x0005DEA2
		public Guid UserGuid
		{
			get
			{
				return this.userGuid;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x0600148C RID: 5260 RVA: 0x0005FCAA File Offset: 0x0005DEAA
		public bool IsSender
		{
			get
			{
				return this.isSender;
			}
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x0005FCB4 File Offset: 0x0005DEB4
		private static string EscapeSpecialCharacters(string value)
		{
			StringBuilder stringBuilder = null;
			for (int i = 0; i < value.Length; i++)
			{
				if (MessageTrackingReportId.IsSpecial(value[i]))
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder();
						stringBuilder.Append(value, 0, i);
					}
					if ((ulong)value[i] >= 256UL)
					{
						throw new FormatException(string.Format("Position: {0}", i));
					}
					stringBuilder.AppendFormat("+{0:x2}", (long)((ulong)value[i]));
				}
				else if (stringBuilder != null)
				{
					stringBuilder.Append(value[i]);
				}
			}
			if (stringBuilder != null)
			{
				return stringBuilder.ToString();
			}
			return value;
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x0005FD54 File Offset: 0x0005DF54
		private static bool TryUnescapeSpecialCharacters(string value, out string unescapedValue)
		{
			unescapedValue = value;
			StringBuilder stringBuilder = null;
			int i = 0;
			while (i < value.Length)
			{
				if (value[i] != '+')
				{
					goto IL_51;
				}
				if (i >= value.Length - 2)
				{
					return false;
				}
				if (stringBuilder != null)
				{
					goto IL_51;
				}
				stringBuilder = new StringBuilder();
				stringBuilder.Append(value, 0, i);
				char value2;
				if (!MessageTrackingReportId.TryParseHex(value, i + 1, out value2))
				{
					return false;
				}
				stringBuilder.Append(value2);
				i += 2;
				IL_62:
				i++;
				continue;
				IL_51:
				if (stringBuilder != null)
				{
					stringBuilder.Append(value[i]);
					goto IL_62;
				}
				goto IL_62;
			}
			if (stringBuilder != null)
			{
				unescapedValue = stringBuilder.ToString();
			}
			return true;
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x0005FDDC File Offset: 0x0005DFDC
		private static bool IsSpecial(char ch)
		{
			foreach (char c in MessageTrackingReportId.SpecialChars)
			{
				if (c == ch)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x0005FE0C File Offset: 0x0005E00C
		private static bool TryParseHex(string s, int start, out char result)
		{
			if (s.Length - start < 2)
			{
				result = '\0';
				return false;
			}
			string s2 = s.Substring(start, 2);
			int num = 0;
			if (!int.TryParse(s2, NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo, out num))
			{
				result = '\0';
				return false;
			}
			result = (char)num;
			return true;
		}

		// Token: 0x04000D60 RID: 3424
		public const string MessageIdTag = "Message-Id";

		// Token: 0x04000D61 RID: 3425
		public const string ServerTag = "Server";

		// Token: 0x04000D62 RID: 3426
		public const string InternalIdTag = "Internal-Id";

		// Token: 0x04000D63 RID: 3427
		public const string SenderTag = "Sender";

		// Token: 0x04000D64 RID: 3428
		public const string RecipientTag = "Recipient";

		// Token: 0x04000D65 RID: 3429
		public const string DomainTag = "Domain";

		// Token: 0x04000D66 RID: 3430
		public static readonly MessageTrackingReportId LegacyExchange = new MessageTrackingReportId();

		// Token: 0x04000D67 RID: 3431
		private static readonly string LegacyExchangeValue = "LegacyExchangeReportId";

		// Token: 0x04000D68 RID: 3432
		private static readonly char[] SpecialChars = new char[]
		{
			'=',
			',',
			'+'
		};

		// Token: 0x04000D69 RID: 3433
		private string messageId;

		// Token: 0x04000D6A RID: 3434
		private string server;

		// Token: 0x04000D6B RID: 3435
		private long internalMessageId;

		// Token: 0x04000D6C RID: 3436
		private SmtpAddress mailbox;

		// Token: 0x04000D6D RID: 3437
		private Guid userGuid;

		// Token: 0x04000D6E RID: 3438
		private string domain;

		// Token: 0x04000D6F RID: 3439
		private bool isSender;
	}
}
