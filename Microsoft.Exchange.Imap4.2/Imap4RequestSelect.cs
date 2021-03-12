using System;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000026 RID: 38
	internal class Imap4RequestSelect : Imap4RequestWithStringParameters
	{
		// Token: 0x060001A8 RID: 424 RVA: 0x0000B305 File Offset: 0x00009505
		public Imap4RequestSelect(Imap4ResponseFactory factory, string tag, string data) : base(factory, tag, data, 1, 1)
		{
			this.PerfCounterTotal = base.Imap4CountersInstance.PerfCounter_SELECT_Total;
			this.PerfCounterFailures = base.Imap4CountersInstance.PerfCounter_SELECT_Failures;
			base.AllowedStates = (Imap4State.Authenticated | Imap4State.Selected);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000B33B File Offset: 0x0000953B
		public override ProtocolResponse Process()
		{
			return this.DoSelect(false);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000B344 File Offset: 0x00009544
		public virtual ProtocolResponse DoSelect(bool examineMode)
		{
			string text;
			if (!Imap4Mailbox.TryNormalizeMailboxPath(base.ArrayOfArguments[0], out text))
			{
				return new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 11");
			}
			Imap4Mailbox selectedMailbox;
			if (base.Factory.SelectedMailbox != null && base.Factory.SelectedMailbox.Equals(text) && base.Factory.SelectedMailbox.ExamineMode == examineMode)
			{
				selectedMailbox = base.Factory.SelectedMailbox;
				selectedMailbox.ReloadMailboxProperties();
				selectedMailbox.ResetNotifications();
			}
			else
			{
				if (!((Imap4Session)base.Factory.Session).TryGetMailbox(text, out selectedMailbox, false))
				{
					base.Factory.SelectedMailbox = null;
					return new Imap4Response(this, Imap4Response.Type.no, string.Format("{0} doesn't exist.", Imap4Mailbox.PathToString(base.ArrayOfArguments[0])));
				}
				if (selectedMailbox.IsNonselect)
				{
					return new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 11");
				}
				selectedMailbox.ExploreMailbox(examineMode, true);
				if (selectedMailbox.Rights == MessageRights.None)
				{
					return new Imap4Response(this, Imap4Response.Type.no, "access denied.");
				}
				base.Factory.SelectedMailbox = selectedMailbox;
			}
			Imap4Response imap4Response = new Imap4Response(this, Imap4Response.Type.ok);
			imap4Response.AppendFormat("* {0} EXISTS\r\n* {1} RECENT\r\n* FLAGS {2}\r\n* OK [PERMANENTFLAGS {3}] Permanent flags\r\n", new object[]
			{
				selectedMailbox.Exists,
				selectedMailbox.Recent,
				Imap4FlagsHelper.ToString(selectedMailbox.Flags),
				Imap4FlagsHelper.ToString(selectedMailbox.PermanentFlags)
			});
			if (selectedMailbox.FirstUnseenIndex > 0)
			{
				imap4Response.AppendFormat("* OK [UNSEEN {0}] Is the first unseen message\r\n", selectedMailbox.FirstUnseenIndex);
			}
			imap4Response.AppendFormat("* OK [UIDVALIDITY {0}] UIDVALIDITY value\r\n", selectedMailbox.UidValidity);
			if (selectedMailbox.UidNext > 0)
			{
				imap4Response.AppendFormat("* OK [UIDNEXT {0}] The next unique identifier value\r\n", selectedMailbox.UidNext);
			}
			if (examineMode)
			{
				imap4Response.AppendFormat("[*] [{0}] EXAMINE completed.", "READ-ONLY");
			}
			else
			{
				imap4Response.AppendFormat("[*] [{0}] SELECT completed.", selectedMailbox.IsReadOnly ? "READ-ONLY" : "READ-WRITE");
			}
			if (base.Session.LightLogSession != null)
			{
				base.Session.LightLogSession.RowsProcessed = new int?(selectedMailbox.Exists);
				base.Session.LightLogSession.Recent = new int?(selectedMailbox.Recent);
			}
			imap4Response.ResponseType = Imap4Response.Type.ok;
			return imap4Response;
		}

		// Token: 0x04000138 RID: 312
		internal const string SELECTReadonly = "READ-ONLY";

		// Token: 0x04000139 RID: 313
		internal const string SELECTReadWrite = "READ-WRITE";

		// Token: 0x0400013A RID: 314
		internal const string SELECTResponse = "* {0} EXISTS\r\n* {1} RECENT\r\n* FLAGS {2}\r\n* OK [PERMANENTFLAGS {3}] Permanent flags\r\n";

		// Token: 0x0400013B RID: 315
		internal const string SELECTResponseUnseenIdx = "* OK [UNSEEN {0}] Is the first unseen message\r\n";

		// Token: 0x0400013C RID: 316
		internal const string SELECTResponseUidValidity = "* OK [UIDVALIDITY {0}] UIDVALIDITY value\r\n";

		// Token: 0x0400013D RID: 317
		internal const string SELECTResponseUidNext = "* OK [UIDNEXT {0}] The next unique identifier value\r\n";

		// Token: 0x0400013E RID: 318
		internal const string SELECTResponseComplete = "[*] [{0}] SELECT completed.";

		// Token: 0x0400013F RID: 319
		internal const string SELECTResponseNotFound = "{0} doesn't exist.";

		// Token: 0x04000140 RID: 320
		internal const string SELECTResponseAccessDenied = "access denied.";

		// Token: 0x04000141 RID: 321
		internal const string EXAMINEResponseComplete = "[*] [{0}] EXAMINE completed.";
	}
}
