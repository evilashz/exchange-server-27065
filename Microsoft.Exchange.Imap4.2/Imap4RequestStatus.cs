using System;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000048 RID: 72
	internal sealed class Imap4RequestStatus : Imap4RequestWithStringParameters
	{
		// Token: 0x0600029B RID: 667 RVA: 0x000134CC File Offset: 0x000116CC
		public Imap4RequestStatus(Imap4ResponseFactory factory, string tag, string data) : base(factory, tag, data, 2, 2)
		{
			this.PerfCounterTotal = base.Imap4CountersInstance.PerfCounter_STATUS_Total;
			this.PerfCounterFailures = base.Imap4CountersInstance.PerfCounter_STATUS_Failures;
			base.AllowedStates = (Imap4State.Authenticated | Imap4State.Selected);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00013504 File Offset: 0x00011704
		public override ProtocolResponse Process()
		{
			string text;
			if (!Imap4Mailbox.TryNormalizeMailboxPath(base.ArrayOfArguments[0], out text))
			{
				return new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 11");
			}
			Imap4Mailbox selectedMailbox;
			if (base.Factory.SelectedMailbox != null && base.Factory.SelectedMailbox.Equals(text))
			{
				selectedMailbox = base.Factory.SelectedMailbox;
			}
			else
			{
				if (!((Imap4Session)base.Factory.Session).TryGetMailbox(text, out selectedMailbox, false))
				{
					return new Imap4Response(this, Imap4Response.Type.no, string.Format("{0} doesn't exist.", Imap4Mailbox.PathToString(base.ArrayOfArguments[0])));
				}
				if (selectedMailbox.IsNonselect)
				{
					return new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 11");
				}
				selectedMailbox.ExploreMailbox(true, false);
				selectedMailbox.DisposeViews();
			}
			Imap4Response imap4Response = new Imap4Response(this, Imap4Response.Type.ok);
			string encodedPath = selectedMailbox.GetEncodedPath();
			if (encodedPath.IndexOfAny(Imap4RequestList.QuotedSpecials) > -1)
			{
				imap4Response.AppendFormat("* STATUS {{{0}}}\r\n{1} (", new object[]
				{
					encodedPath.Length,
					encodedPath
				});
			}
			else if (string.IsNullOrEmpty(encodedPath) || encodedPath.IndexOfAny(Imap4RequestList.AtomSpecials) > -1)
			{
				imap4Response.AppendFormat("* STATUS \"{0}\" (", encodedPath);
			}
			else
			{
				imap4Response.AppendFormat("* STATUS {0} (", encodedPath);
			}
			string text2 = base.ArrayOfArguments[1];
			if (text2.Length > 1 && text2[0] == '(' && text2[text2.Length - 1] == ')')
			{
				text2 = text2.Substring(1, text2.Length - 2);
			}
			string[] array = text2.Split(ResponseFactory.WordDelimiter);
			bool flag = true;
			int i = 0;
			while (i < array.Length)
			{
				string a;
				if ((a = array[i].ToLower()) != null)
				{
					object obj;
					if (!(a == "messages"))
					{
						if (!(a == "recent"))
						{
							if (!(a == "uidnext"))
							{
								if (!(a == "uidvalidity"))
								{
									if (!(a == "unseen"))
									{
										goto IL_2BF;
									}
									obj = selectedMailbox.Unseen;
									if (base.Session.LightLogSession != null)
									{
										base.Session.LightLogSession.Unseen = new int?(selectedMailbox.Unseen);
									}
								}
								else
								{
									obj = selectedMailbox.UidValidity;
								}
							}
							else
							{
								obj = selectedMailbox.UidNext;
							}
						}
						else
						{
							obj = selectedMailbox.Recent;
							if (base.Session.LightLogSession != null)
							{
								base.Session.LightLogSession.Recent = new int?(selectedMailbox.Recent);
							}
						}
					}
					else
					{
						obj = selectedMailbox.Exists;
						if (base.Session.LightLogSession != null)
						{
							base.Session.LightLogSession.RowsProcessed = new int?(selectedMailbox.Exists);
						}
					}
					if (flag)
					{
						flag = false;
					}
					else
					{
						imap4Response.Append(' ');
					}
					imap4Response.AppendFormat("{0} {1}", new object[]
					{
						array[i],
						obj.ToString()
					});
					i++;
					continue;
				}
				IL_2BF:
				imap4Response.Dispose();
				return new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 11");
			}
			imap4Response.Append(") \r\n[*] STATUS completed.");
			return imap4Response;
		}

		// Token: 0x040001FD RID: 509
		internal const string STATUSResponseBegining = "* STATUS {0} (";

		// Token: 0x040001FE RID: 510
		internal const string STATUSResponseBeginingWithQuotes = "* STATUS \"{0}\" (";

		// Token: 0x040001FF RID: 511
		internal const string STATUSResponseBeginingLiteral = "* STATUS {{{0}}}\r\n{1} (";

		// Token: 0x04000200 RID: 512
		internal const string STATUSResponseItem = "{0} {1}";

		// Token: 0x04000201 RID: 513
		internal const string STATUSResponseCompleted = ") \r\n[*] STATUS completed.";

		// Token: 0x04000202 RID: 514
		internal const string STATUSResponseNotFound = "{0} doesn't exist.";
	}
}
