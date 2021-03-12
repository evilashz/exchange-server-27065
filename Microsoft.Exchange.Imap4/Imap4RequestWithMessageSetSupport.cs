using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x0200001E RID: 30
	internal abstract class Imap4RequestWithMessageSetSupport : Imap4RequestWithStringParameters
	{
		// Token: 0x06000176 RID: 374 RVA: 0x0000A59B File Offset: 0x0000879B
		protected Imap4RequestWithMessageSetSupport(Imap4ResponseFactory factory, string tag, string data, bool useUids, int min_arguments, int max_arguments) : base(factory, tag, data, min_arguments, max_arguments)
		{
			this.UseUids = useUids;
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000177 RID: 375 RVA: 0x0000A5B2 File Offset: 0x000087B2
		// (set) Token: 0x06000178 RID: 376 RVA: 0x0000A5BA File Offset: 0x000087BA
		protected bool UseUids { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000179 RID: 377 RVA: 0x0000A5C3 File Offset: 0x000087C3
		// (set) Token: 0x0600017A RID: 378 RVA: 0x0000A5CB File Offset: 0x000087CB
		protected bool MessageSetIsInvalid { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000A5D4 File Offset: 0x000087D4
		// (set) Token: 0x0600017C RID: 380 RVA: 0x0000A5DC File Offset: 0x000087DC
		protected bool DeletedMessages { get; set; }

		// Token: 0x0600017D RID: 381 RVA: 0x0000A5E8 File Offset: 0x000087E8
		protected List<ProtocolMessage> GetMessages(string messageSet)
		{
			HashSet<ProtocolMessage> collection = this.InternalGetMessages(messageSet);
			List<ProtocolMessage> list = new List<ProtocolMessage>(collection);
			list.Sort();
			return list;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000A60C File Offset: 0x0000880C
		public int GetIndex(string indexString, int lastMessageIndex)
		{
			if (indexString == "*")
			{
				return lastMessageIndex;
			}
			uint num;
			if (!uint.TryParse(indexString, out num) || num < 1U)
			{
				this.ParseResult = ParseResult.invalidMessageSet;
				return 0;
			}
			if (num == 4294967295U)
			{
				if (base.Factory.DontReturnLastMessageForUInt32MaxValue)
				{
					return int.MaxValue;
				}
				return lastMessageIndex;
			}
			else
			{
				if (num > 2147483647U)
				{
					this.ParseResult = ParseResult.invalidMessageSet;
					return 0;
				}
				return (int)num;
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000A66A File Offset: 0x0000886A
		private void AddMessage(HashSet<ProtocolMessage> messages, ProtocolMessage message)
		{
			if (message == null)
			{
				if (!this.UseUids)
				{
					this.MessageSetIsInvalid = true;
					return;
				}
			}
			else
			{
				if (message.IsDeleted)
				{
					this.DeletedMessages = true;
					return;
				}
				messages.Add(message);
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000A698 File Offset: 0x00008898
		private HashSet<ProtocolMessage> InternalGetMessages(string messageSet)
		{
			Imap4Mailbox selectedMailbox = base.Factory.SelectedMailbox;
			Match match = Imap4RequestWithMessageSetSupport.messageSetRegEx.Match(messageSet);
			HashSet<ProtocolMessage> hashSet = new HashSet<ProtocolMessage>();
			this.MessageSetIsInvalid = false;
			this.DeletedMessages = false;
			int lastMessageIndex = selectedMailbox.MessagesTotal;
			if (this.UseUids && selectedMailbox.MessagesTotal > 0)
			{
				lastMessageIndex = selectedMailbox.GetMessage(selectedMailbox.MessagesTotal, false).Id;
			}
			while (match.Success)
			{
				if (match.Groups["single"].Value.Length != 0)
				{
					int i = this.GetIndex(match.Groups["single"].Value, lastMessageIndex);
					if (this.ParseResult > ParseResult.success)
					{
						return hashSet;
					}
					this.AddMessage(hashSet, selectedMailbox.GetMessage(i, this.UseUids));
				}
				else
				{
					if (match.Groups["groupStart"].Value.Length == 0 || match.Groups["groupEnd"].Value.Length == 0)
					{
						this.ParseResult = ParseResult.invalidMessageSet;
						return hashSet;
					}
					int num = this.GetIndex(match.Groups["groupStart"].Value, lastMessageIndex);
					if (this.ParseResult > ParseResult.success)
					{
						return hashSet;
					}
					int num2 = this.GetIndex(match.Groups["groupEnd"].Value, lastMessageIndex);
					if (this.ParseResult > ParseResult.success)
					{
						return hashSet;
					}
					if (num2 < num)
					{
						int i = num2;
						num2 = num;
						num = i;
					}
					if (this.UseUids)
					{
						for (int i = 1; i <= selectedMailbox.MessagesTotal; i++)
						{
							Imap4Message message = selectedMailbox.GetMessage(i, false);
							if (message.Id >= num)
							{
								if (message.Id > num2)
								{
									break;
								}
								this.AddMessage(hashSet, message);
							}
						}
					}
					else
					{
						if (num2 > selectedMailbox.MessagesTotal)
						{
							this.MessageSetIsInvalid = true;
							num2 = selectedMailbox.MessagesTotal;
						}
						for (int i = num; i <= num2; i++)
						{
							this.AddMessage(hashSet, selectedMailbox.GetMessage(i, false));
						}
					}
				}
				if (match.Index + match.Length == messageSet.Length)
				{
					return hashSet;
				}
				match = match.NextMatch();
			}
			this.ParseResult = ParseResult.invalidMessageSet;
			return hashSet;
		}

		// Token: 0x0400010F RID: 271
		internal const string InvalidMessageSet = "[*] The specified message set is invalid.";

		// Token: 0x04000110 RID: 272
		internal const string MessagesDeleted = "[*] Some of the requested messages no longer exist.";

		// Token: 0x04000111 RID: 273
		private static Regex messageSetRegEx = new Regex("\\G((?<single>[\\d]+)|(?<groupStart>[\\d]+|\\*):(?<groupEnd>[\\d]+|\\*))(,|$)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
	}
}
