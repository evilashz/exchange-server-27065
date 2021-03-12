using System;
using System.Web;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200015C RID: 348
	internal sealed class MailToParser
	{
		// Token: 0x06000C1B RID: 3099 RVA: 0x000533B6 File Offset: 0x000515B6
		private MailToParser()
		{
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x000533C0 File Offset: 0x000515C0
		public static bool TryParseMailTo(string mailToUrlValue, UserContext userContext, out StoreObjectId mailToItemId)
		{
			if (mailToUrlValue == null)
			{
				throw new ArgumentNullException("mailToUrlValue");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			mailToItemId = null;
			if (null == Utilities.TryParseUri(mailToUrlValue))
			{
				return false;
			}
			using (MessageItem messageItem = MessageItem.Create(userContext.MailboxSession, userContext.DraftsFolderId))
			{
				messageItem[ItemSchema.ConversationIndexTracking] = true;
				if (Globals.ArePerfCountersEnabled)
				{
					OwaSingleCounters.ItemsCreated.Increment();
				}
				if (!MailToParser.TryMailToParse(messageItem, mailToUrlValue))
				{
					return false;
				}
				messageItem.Save(SaveMode.ResolveConflicts);
				messageItem.Load();
				mailToItemId = messageItem.Id.ObjectId;
			}
			return true;
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x0005347C File Offset: 0x0005167C
		private static bool TryMailToParse(MessageItem mailToMessage, string mailTo)
		{
			char[] separators = new char[]
			{
				',',
				'?'
			};
			char[] separators2 = new char[]
			{
				',',
				'&'
			};
			int num = 0;
			if (!mailTo.StartsWith("mailto:", StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			num += "mailto:".Length;
			MailToParser.ProcessingFlags processType = MailToParser.ProcessingFlags.ProcessTo;
			num = MailToParser.ProcessTokensFromCommaSeparatedString(mailToMessage, mailTo, processType, num, separators, '?');
			if (num < 0 && num >= mailTo.Length)
			{
				return true;
			}
			while (num >= 0 && num < mailTo.Length)
			{
				processType = MailToParser.ProcessingFlags.None;
				if (object.Equals(mailTo[num], 'b') || object.Equals(mailTo[num], 'B'))
				{
					if (mailTo.Length > num + "bcc=".Length && string.Compare(mailTo, num, "bcc=", 0, "bcc=".Length, StringComparison.OrdinalIgnoreCase) == 0)
					{
						num += "bcc=".Length;
						processType = MailToParser.ProcessingFlags.ProcessBcc;
					}
					else if (mailTo.Length > num + "body=".Length && string.Compare(mailTo, num, "body=", 0, "body=".Length, StringComparison.OrdinalIgnoreCase) == 0)
					{
						num += "body=".Length;
						processType = MailToParser.ProcessingFlags.ProcessBody;
					}
				}
				else if (object.Equals(mailTo[num], 'c') || object.Equals(mailTo[num], 'C'))
				{
					if (mailTo.Length > num + "cc=".Length && string.Compare(mailTo, num, "cc=", 0, "cc=".Length, StringComparison.OrdinalIgnoreCase) == 0)
					{
						num += "cc=".Length;
						processType = MailToParser.ProcessingFlags.ProcessCc;
					}
				}
				else if (object.Equals(mailTo[num], 's') || object.Equals(mailTo[num], 'S'))
				{
					if (mailTo.Length > num + "subject=".Length && string.Compare(mailTo, num, "subject=", 0, "subject=".Length, StringComparison.OrdinalIgnoreCase) == 0)
					{
						num += "subject=".Length;
						processType = MailToParser.ProcessingFlags.ProcessSubject;
					}
				}
				else if ((object.Equals(mailTo[num], 't') || object.Equals(mailTo[num], 'T')) && mailTo.Length > num + "to=".Length && string.Compare(mailTo, num, "to=", 0, "to=".Length, StringComparison.OrdinalIgnoreCase) == 0)
				{
					num += "to=".Length;
					processType = MailToParser.ProcessingFlags.ProcessTo;
				}
				num = MailToParser.ProcessTokensFromCommaSeparatedString(mailToMessage, mailTo, processType, num, separators2, '&');
			}
			return true;
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x00053744 File Offset: 0x00051944
		private static int ProcessTokensFromCommaSeparatedString(MessageItem mailToMessage, string mailTo, MailToParser.ProcessingFlags processType, int currentIndex, char[] separators, char terminator)
		{
			bool flag = false;
			int num;
			while ((num = mailTo.IndexOfAny(separators, currentIndex)) != -1)
			{
				string token = mailTo.Substring(currentIndex, num - currentIndex);
				MailToParser.ProcessToken(mailToMessage, token, processType);
				if (mailTo[num] == terminator)
				{
					currentIndex = num + 1;
					flag = true;
					break;
				}
				currentIndex = num + 1;
			}
			if (!flag)
			{
				MailToParser.ProcessToken(mailToMessage, mailTo.Substring(currentIndex), processType);
			}
			if (num < 0)
			{
				return num;
			}
			return currentIndex;
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x000537AC File Offset: 0x000519AC
		private static void ProcessToken(MessageItem mailToMessage, string token, MailToParser.ProcessingFlags processType)
		{
			switch (processType)
			{
			case MailToParser.ProcessingFlags.None:
				break;
			case MailToParser.ProcessingFlags.ProcessBcc:
				MailToParser.ProcessRecipients(mailToMessage, token, RecipientItemType.Bcc);
				return;
			case MailToParser.ProcessingFlags.ProcessCc:
				MailToParser.ProcessRecipients(mailToMessage, token, RecipientItemType.Cc);
				return;
			case MailToParser.ProcessingFlags.ProcessTo:
				MailToParser.ProcessRecipients(mailToMessage, token, RecipientItemType.To);
				return;
			case MailToParser.ProcessingFlags.ProcessSubject:
				if (!string.IsNullOrEmpty(token))
				{
					token = HttpUtility.UrlDecode(token);
					mailToMessage.Subject = token;
					return;
				}
				break;
			case MailToParser.ProcessingFlags.ProcessBody:
				if (!string.IsNullOrEmpty(token))
				{
					token = HttpUtility.UrlDecode(token);
					ItemUtility.SetItemBody(mailToMessage, BodyFormat.TextPlain, token);
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x00053828 File Offset: 0x00051A28
		private static void ProcessRecipients(MessageItem mailToMessage, string value, RecipientItemType recipientItemType)
		{
			if (string.IsNullOrEmpty(value))
			{
				return;
			}
			value = HttpUtility.UrlDecode(value);
			Participant participant;
			bool flag = Participant.TryParse(value, out participant);
			if (flag)
			{
				ProxyAddress proxyAddress;
				if (ImceaAddress.IsImceaAddress(participant.EmailAddress) && SmtpProxyAddress.TryDeencapsulate(participant.EmailAddress, out proxyAddress))
				{
					participant = new Participant((participant.DisplayName != participant.EmailAddress) ? participant.DisplayName : proxyAddress.AddressString, proxyAddress.AddressString, proxyAddress.PrefixString);
				}
				mailToMessage.Recipients.Add(participant, recipientItemType);
			}
		}

		// Token: 0x04000893 RID: 2195
		public const string MailToParameter = "email";

		// Token: 0x0200015D RID: 349
		private enum ProcessingFlags
		{
			// Token: 0x04000895 RID: 2197
			None,
			// Token: 0x04000896 RID: 2198
			ProcessBcc,
			// Token: 0x04000897 RID: 2199
			ProcessCc,
			// Token: 0x04000898 RID: 2200
			ProcessTo,
			// Token: 0x04000899 RID: 2201
			ProcessSubject,
			// Token: 0x0400089A RID: 2202
			ProcessBody
		}
	}
}
