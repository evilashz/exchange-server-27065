using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000029 RID: 41
	internal class MailboxTransportDeliveryResult
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x00003F95 File Offset: 0x00002195
		private MailboxTransportDeliveryResult(SmtpResponse messageLevelSmtpResponse, TimeSpan? messageLevelRetryInterval, bool messageLevelResubmit, bool retryQueue, IList<MailboxTransportDeliveryResult.RecipientResponse> recipientResponses)
		{
			this.messageLevelSmtpResponse = messageLevelSmtpResponse;
			this.messageLevelRetryInterval = messageLevelRetryInterval;
			this.messageLevelResubmit = messageLevelResubmit;
			this.retryQueue = retryQueue;
			this.recipientResponses = recipientResponses;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003FC4 File Offset: 0x000021C4
		public static bool TryParse(SmtpResponse smtpResponse, out MailboxTransportDeliveryResult result, out string parseError)
		{
			result = null;
			parseError = null;
			if (smtpResponse.SmtpResponseType == SmtpResponseType.Unknown)
			{
				parseError = "smtpResponse is of Unknown response type";
			}
			else if (smtpResponse.StatusText.Length <= 1)
			{
				parseError = "smtp response should be multi-line";
			}
			else if (!smtpResponse.StatusText[0].EndsWith("STOREDRV.Deliver; delivery result banner", StringComparison.OrdinalIgnoreCase))
			{
				parseError = string.Format(CultureInfo.InvariantCulture, "first line of response is not the expected banner: <{0}>", new object[]
				{
					smtpResponse.StatusText[0]
				});
			}
			else if (smtpResponse.StatusText.Length == 2)
			{
				parseError = MailboxTransportDeliveryResult.ParseSingleLineResponse(smtpResponse, out result);
			}
			else
			{
				parseError = MailboxTransportDeliveryResult.ParseMultiLineResponse(smtpResponse, out result);
			}
			return parseError == null;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00004060 File Offset: 0x00002260
		public SmtpResponse MessageLevelSmtpResponse
		{
			get
			{
				return this.messageLevelSmtpResponse;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00004068 File Offset: 0x00002268
		public TimeSpan? MessageLevelRetryInterval
		{
			get
			{
				return this.messageLevelRetryInterval;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00004070 File Offset: 0x00002270
		public bool MessageLevelResubmit
		{
			get
			{
				return this.messageLevelResubmit;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004078 File Offset: 0x00002278
		public bool RetryQueue
		{
			get
			{
				return this.retryQueue;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00004080 File Offset: 0x00002280
		public int RecipientResponseCount
		{
			get
			{
				return this.recipientResponses.Count;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000DC RID: 220 RVA: 0x0000408D File Offset: 0x0000228D
		public IEnumerable<MailboxTransportDeliveryResult.RecipientResponse> RecipientResponses
		{
			get
			{
				return this.recipientResponses;
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004095 File Offset: 0x00002295
		public MailboxTransportDeliveryResult.RecipientResponse GetRecipientResponseAt(int index)
		{
			return this.recipientResponses[index];
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000040A4 File Offset: 0x000022A4
		private static string ParseSingleLineResponse(SmtpResponse smtpResponse, out MailboxTransportDeliveryResult result)
		{
			result = null;
			SmtpResponse smtpResponse2;
			bool flag;
			bool flag2;
			bool flag3;
			bool flag4;
			TimeSpan? timeSpan;
			string text = MailboxTransportDeliveryResult.ParseLine(smtpResponse.StatusText[1], out smtpResponse2, out flag, out flag2, out flag3, out flag4, out timeSpan);
			if (text != null)
			{
				return text;
			}
			result = new MailboxTransportDeliveryResult(smtpResponse2, timeSpan, flag, flag2, new List<MailboxTransportDeliveryResult.RecipientResponse>());
			return null;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000040EC File Offset: 0x000022EC
		private static string ParseMultiLineResponse(SmtpResponse smtpResponse, out MailboxTransportDeliveryResult result)
		{
			result = null;
			IList<MailboxTransportDeliveryResult.RecipientResponse> list = new List<MailboxTransportDeliveryResult.RecipientResponse>();
			string text;
			for (int i = 2; i < smtpResponse.StatusText.Length; i++)
			{
				string stringToParse = smtpResponse.StatusText[i];
				SmtpResponse smtpResponse2;
				bool flag;
				bool flag2;
				bool retryOnDuplicateDelivery;
				bool flag3;
				TimeSpan? timeSpan;
				text = MailboxTransportDeliveryResult.ParseLine(stringToParse, out smtpResponse2, out flag, out flag2, out retryOnDuplicateDelivery, out flag3, out timeSpan);
				if (text != null)
				{
					return text;
				}
				AckStatus ackStatus;
				if (flag)
				{
					ackStatus = AckStatus.Resubmit;
				}
				else if (flag3)
				{
					ackStatus = AckStatus.SuccessNoDsn;
				}
				else if (smtpResponse2.SmtpResponseType == SmtpResponseType.Success)
				{
					ackStatus = AckStatus.Success;
				}
				else if (smtpResponse2.SmtpResponseType == SmtpResponseType.TransientError)
				{
					ackStatus = AckStatus.Retry;
				}
				else
				{
					if (smtpResponse2.SmtpResponseType != SmtpResponseType.PermanentError)
					{
						return string.Format(CultureInfo.InvariantCulture, "invalid recipient response type: <{0}>", new object[]
						{
							smtpResponse2.SmtpResponseType
						});
					}
					ackStatus = AckStatus.Fail;
				}
				list.Add(new MailboxTransportDeliveryResult.RecipientResponse(ackStatus, smtpResponse2, retryOnDuplicateDelivery));
			}
			SmtpResponse smtpResponse3;
			bool flag4;
			bool flag5;
			bool flag6;
			bool flag7;
			TimeSpan? timeSpan2;
			text = MailboxTransportDeliveryResult.ParseLine(smtpResponse.StatusText[1], out smtpResponse3, out flag4, out flag5, out flag6, out flag7, out timeSpan2);
			if (text != null)
			{
				return text;
			}
			result = new MailboxTransportDeliveryResult(smtpResponse3, timeSpan2, flag4, flag5, list);
			return null;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000041EC File Offset: 0x000023EC
		private static string ParseLine(string stringToParse, out SmtpResponse response, out bool resubmit, out bool retryQueue, out bool retryOnDuplicateDelivery, out bool skipDsn, out TimeSpan? retryInterval)
		{
			resubmit = false;
			retryQueue = false;
			retryOnDuplicateDelivery = false;
			skipDsn = false;
			retryInterval = null;
			response = SmtpResponse.Empty;
			Match match = MailboxTransportDeliveryResult.responseSuffixRegex.Match(stringToParse);
			if (!match.Success)
			{
				return string.Format(CultureInfo.InvariantCulture, "string did not match smtp response suffix regex <{0}>", new object[]
				{
					stringToParse
				});
			}
			string text = stringToParse.Substring(0, match.Index);
			if (!SmtpResponse.TryParse(text, out response))
			{
				return string.Format(CultureInfo.InvariantCulture, "parsing smtp response line failed: <{0}>", new object[]
				{
					text
				});
			}
			string value = match.Groups["KeyCapture"].Value;
			string value2 = match.Groups["ValueCapture"].Value;
			if (string.Equals(value, "retryinterval", StringComparison.OrdinalIgnoreCase))
			{
				TimeSpan value3;
				if (!TimeSpan.TryParse(value2, out value3))
				{
					return string.Format(CultureInfo.InvariantCulture, "failed to parse retry interval value <{0}>", new object[]
					{
						value2
					});
				}
				retryInterval = new TimeSpan?(value3);
			}
			else if (string.Equals(value, "resubmit", StringComparison.OrdinalIgnoreCase))
			{
				if (!bool.TryParse(value2, out resubmit))
				{
					return string.Format(CultureInfo.InvariantCulture, "failed to parse resubmit value <{0}>", new object[]
					{
						value2
					});
				}
			}
			else if (string.Equals(value, "retryonduplicatedelivery", StringComparison.OrdinalIgnoreCase))
			{
				if (!bool.TryParse(value2, out retryOnDuplicateDelivery))
				{
					return string.Format(CultureInfo.InvariantCulture, "failed to parse retryOnDuplicateDelivery value <{0}>", new object[]
					{
						value2
					});
				}
			}
			else if (string.Equals(value, "retryqueue", StringComparison.OrdinalIgnoreCase))
			{
				if (!bool.TryParse(value2, out retryQueue))
				{
					return string.Format(CultureInfo.InvariantCulture, "failed to parse retryQueue value <{0}>", new object[]
					{
						value2
					});
				}
			}
			else if (string.Equals(value, "skipdsn", StringComparison.OrdinalIgnoreCase) && !bool.TryParse(value2, out skipDsn))
			{
				return string.Format(CultureInfo.InvariantCulture, "failed to parse skipDsn value <{0}>", new object[]
				{
					value2
				});
			}
			return null;
		}

		// Token: 0x04000054 RID: 84
		public const int MaxNumRecipientsToSend = 47;

		// Token: 0x04000055 RID: 85
		public const string RetryQueueKeyName = "retryqueue";

		// Token: 0x04000056 RID: 86
		public const string RetryIntervalKeyName = "retryinterval";

		// Token: 0x04000057 RID: 87
		public const string ResubmitKeyName = "resubmit";

		// Token: 0x04000058 RID: 88
		public const string RetryOnDuplicateDeliveryKeyName = "retryonduplicatedelivery";

		// Token: 0x04000059 RID: 89
		public const string SkipDsnKeyName = "skipdsn";

		// Token: 0x0400005A RID: 90
		public const string ResponseBanner = "STOREDRV.Deliver; delivery result banner";

		// Token: 0x0400005B RID: 91
		private const string KeyCaptureName = "KeyCapture";

		// Token: 0x0400005C RID: 92
		private const string ValueCaptureName = "ValueCapture";

		// Token: 0x0400005D RID: 93
		private static readonly string responseSuffixRegexString = string.Format("\\[(?<{0}>{1})\\=(?<{2}>{3})\\]$", new object[]
		{
			"KeyCapture",
			"[a-zA-Z]{1,30}",
			"ValueCapture",
			"[\\w\\.\\:\\-]{1,50}"
		});

		// Token: 0x0400005E RID: 94
		private static readonly Regex responseSuffixRegex = new Regex(MailboxTransportDeliveryResult.responseSuffixRegexString, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		// Token: 0x0400005F RID: 95
		private readonly SmtpResponse messageLevelSmtpResponse;

		// Token: 0x04000060 RID: 96
		private readonly bool retryQueue;

		// Token: 0x04000061 RID: 97
		private readonly TimeSpan? messageLevelRetryInterval;

		// Token: 0x04000062 RID: 98
		private readonly bool messageLevelResubmit;

		// Token: 0x04000063 RID: 99
		private readonly IList<MailboxTransportDeliveryResult.RecipientResponse> recipientResponses;

		// Token: 0x0200002A RID: 42
		internal struct RecipientResponse
		{
			// Token: 0x060000E2 RID: 226 RVA: 0x00004431 File Offset: 0x00002631
			public RecipientResponse(AckStatus ackStatus, SmtpResponse smtpResponse, bool retryOnDuplicateDelivery)
			{
				this.ackStatus = ackStatus;
				this.smtpResponse = smtpResponse;
				this.retryOnDuplicateDelivery = retryOnDuplicateDelivery;
			}

			// Token: 0x1700003A RID: 58
			// (get) Token: 0x060000E3 RID: 227 RVA: 0x00004448 File Offset: 0x00002648
			public AckStatus AckStatus
			{
				get
				{
					return this.ackStatus;
				}
			}

			// Token: 0x1700003B RID: 59
			// (get) Token: 0x060000E4 RID: 228 RVA: 0x00004450 File Offset: 0x00002650
			public SmtpResponse SmtpResponse
			{
				get
				{
					return this.smtpResponse;
				}
			}

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x060000E5 RID: 229 RVA: 0x00004458 File Offset: 0x00002658
			public bool RetryOnDuplicateDelivery
			{
				get
				{
					return this.retryOnDuplicateDelivery;
				}
			}

			// Token: 0x04000064 RID: 100
			private readonly AckStatus ackStatus;

			// Token: 0x04000065 RID: 101
			private readonly SmtpResponse smtpResponse;

			// Token: 0x04000066 RID: 102
			private readonly bool retryOnDuplicateDelivery;
		}
	}
}
