using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x0200002E RID: 46
	internal sealed class Imap4RequestLsub : Imap4RequestList
	{
		// Token: 0x060001DE RID: 478 RVA: 0x0000C56D File Offset: 0x0000A76D
		public Imap4RequestLsub(Imap4ResponseFactory factory, string tag, string data) : base(factory, tag, data)
		{
			this.PerfCounterTotal = base.Imap4CountersInstance.PerfCounter_LSUB_Total;
			this.PerfCounterFailures = base.Imap4CountersInstance.PerfCounter_LSUB_Failures;
			base.AllowedStates = (Imap4State.Authenticated | Imap4State.Selected);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000C5A4 File Offset: 0x0000A7A4
		public override ProtocolResponse Process()
		{
			Regex regex;
			bool flag;
			Imap4Response imap4Response = base.Preprocess(out regex, out flag);
			if (imap4Response != null)
			{
				return imap4Response;
			}
			imap4Response = new Imap4Response(this, Imap4Response.Type.ok);
			if (base.Session.LightLogSession != null)
			{
				base.Session.LightLogSession.RowsProcessed = new int?(0);
			}
			if (!string.IsNullOrEmpty(base.ArrayOfArguments[1]))
			{
				string[] array;
				if (!SubscribeListHelper.TryGetList(base.Factory, out array, base.Factory.ReloadMailboxBeforeGettingSubscriptionList))
				{
					imap4Response.Dispose();
					return new Imap4Response(this, Imap4Response.Type.bad, "LSUB failed.");
				}
				List<string> list = new List<string>(array.Length);
				List<string> list2 = new List<string>(array.Length);
				foreach (string text in array)
				{
					if (regex.IsMatch(text))
					{
						list.Add(text);
						if (list2.Contains(text))
						{
							list2.Remove(text);
						}
					}
					else if (!flag && text.IndexOf('/') > -1)
					{
						string text2 = text;
						for (;;)
						{
							text2 = text2.Remove(text2.IndexOf('/'));
							if (regex.IsMatch(text2))
							{
								break;
							}
							if (text2.IndexOf('/') <= -1)
							{
								goto IL_12A;
							}
						}
						if (!list.Contains(text2) && !list2.Contains(text2))
						{
							list2.Add(text2);
						}
					}
					IL_12A:;
				}
				foreach (string mailboxName in list)
				{
					this.AddOneMailbox(imap4Response, mailboxName);
				}
				using (List<string>.Enumerator enumerator2 = list2.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						string mailboxName2 = enumerator2.Current;
						this.AddOneMailbox(imap4Response, mailboxName2, "\\Noselect");
					}
					goto IL_1BD;
				}
			}
			this.AddOneMailbox(imap4Response, string.Empty, "\\Noselect");
			IL_1BD:
			imap4Response.Append("[*] LSUB completed.");
			return imap4Response;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000C798 File Offset: 0x0000A998
		private void AddOneMailbox(Imap4Response response, string mailboxName)
		{
			this.AddOneMailbox(response, mailboxName, string.Empty);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000C7A8 File Offset: 0x0000A9A8
		private void AddOneMailbox(Imap4Response response, string mailboxName, string flags)
		{
			if (mailboxName.IndexOfAny(Imap4RequestList.QuotedSpecials) > -1)
			{
				string text = Imap4UTF7Encoding.Encode(mailboxName);
				response.AppendFormat("* LSUB ({0}) \"{1}\" {{{2}}}\r\n{3}\r\n", new object[]
				{
					flags,
					'/',
					text.Length,
					text
				});
			}
			else if (mailboxName.IndexOfAny(Imap4RequestList.AtomSpecials) > -1 || string.IsNullOrEmpty(mailboxName))
			{
				response.AppendFormat("* LSUB ({0}) \"{1}\" \"{2}\"\r\n", new object[]
				{
					flags,
					'/',
					Imap4UTF7Encoding.Encode(mailboxName)
				});
			}
			else
			{
				response.AppendFormat("* LSUB ({0}) \"{1}\" {2}\r\n", new object[]
				{
					flags,
					'/',
					Imap4UTF7Encoding.Encode(mailboxName)
				});
			}
			if (base.Session.LightLogSession != null)
			{
				base.Session.LightLogSession.RowsProcessed++;
			}
		}

		// Token: 0x04000163 RID: 355
		internal const string LSUBResponseCompleted = "[*] LSUB completed.";

		// Token: 0x04000164 RID: 356
		internal const string LSUBResponseLine = "* LSUB ({0}) \"{1}\" {2}\r\n";

		// Token: 0x04000165 RID: 357
		internal const string LSUBResponseLineWithQuotes = "* LSUB ({0}) \"{1}\" \"{2}\"\r\n";

		// Token: 0x04000166 RID: 358
		internal const string LSUBResponseLineLiteral = "* LSUB ({0}) \"{1}\" {{{2}}}\r\n{3}\r\n";

		// Token: 0x04000167 RID: 359
		internal const string LSUBResponseFailed = "LSUB failed.";

		// Token: 0x04000168 RID: 360
		private const string FlagNoselect = "\\Noselect";
	}
}
