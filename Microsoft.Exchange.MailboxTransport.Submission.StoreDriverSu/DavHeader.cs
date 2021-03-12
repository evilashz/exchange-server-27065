using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriverSubmission;
using Microsoft.Exchange.MailboxTransport.StoreDriver;
using Microsoft.Exchange.Protocols.Smtp;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x02000036 RID: 54
	internal sealed class DavHeader
	{
		// Token: 0x0600021B RID: 539 RVA: 0x0000C815 File Offset: 0x0000AA15
		public DavHeader(string headers)
		{
			this.headers = headers;
			this.rcptToList = new List<RcptToCommand>();
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000C830 File Offset: 0x0000AA30
		public static bool CopySenderAndRecipientsFromHeaders(string headers, TransportMailItem mailItem)
		{
			if (string.IsNullOrEmpty(headers))
			{
				TraceHelper.MapiStoreDriverSubmissionTracer.TracePass(TraceHelper.MessageProbeActivityId, 0L, "DAV header is not present.");
				return false;
			}
			try
			{
				DavHeader davHeader = new DavHeader(headers);
				davHeader.Parse();
				davHeader.CopyTo(mailItem);
				return true;
			}
			catch (FormatException arg)
			{
				TraceHelper.MapiStoreDriverSubmissionTracer.TracePass<FormatException>(TraceHelper.MessageProbeActivityId, 0L, "Parsing Error: {0}", arg);
			}
			return false;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000C8A4 File Offset: 0x0000AAA4
		public void Parse()
		{
			byte[] nextLine = this.GetNextLine();
			if (nextLine == null)
			{
				throw new FormatException("No Sender");
			}
			this.mailFrom = new MailFromCommand(nextLine);
			for (byte[] nextLine2 = this.GetNextLine(); nextLine2 != null; nextLine2 = this.GetNextLine())
			{
				this.rcptToList.Add(new RcptToCommand(nextLine2));
			}
			if (this.rcptToList.Count == 0)
			{
				throw new FormatException("No Recipients");
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000C910 File Offset: 0x0000AB10
		private byte[] GetNextLine()
		{
			int num = this.headers.IndexOf("\r\n", this.current, StringComparison.Ordinal);
			if (num == -1)
			{
				return null;
			}
			byte[] result = Util.AsciiStringToBytes(this.headers, this.current, num - this.current);
			this.current = num + "\r\n".Length;
			return result;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000C968 File Offset: 0x0000AB68
		private void CopyTo(TransportMailItem mailItem)
		{
			mailItem.From = this.mailFrom.Address;
			mailItem.Auth = this.mailFrom.Auth;
			mailItem.EnvId = this.mailFrom.EnvId;
			mailItem.DsnFormat = this.mailFrom.Ret;
			mailItem.BodyType = this.mailFrom.BodyType;
			TraceHelper.MapiStoreDriverSubmissionTracer.TracePass(TraceHelper.MessageProbeActivityId, 0L, "Add sender {0} from Dav Header, Auth:{1}, EnvId:{2}, DsnFormat:{3}, BodyType: {4}", new object[]
			{
				this.mailFrom.Address,
				this.mailFrom.Auth,
				this.mailFrom.EnvId,
				this.mailFrom.Ret,
				this.mailFrom.BodyType
			});
			foreach (RcptToCommand rcptToCommand in this.rcptToList)
			{
				MailRecipient mailRecipient = mailItem.Recipients.Add((string)rcptToCommand.Address);
				mailRecipient.ORcpt = rcptToCommand.ORcpt;
				mailRecipient.DsnRequested = rcptToCommand.Notify;
				TraceHelper.MapiStoreDriverSubmissionTracer.TracePass<string, string, DsnRequestedFlags>(TraceHelper.MessageProbeActivityId, 0L, "Add Recipient {0} from Dav Header, ORcpt:{1}, Notify:{2}", (string)rcptToCommand.Address, rcptToCommand.ORcpt, rcptToCommand.Notify);
			}
		}

		// Token: 0x04000121 RID: 289
		private const string CRLF = "\r\n";

		// Token: 0x04000122 RID: 290
		private static readonly Trace diag = ExTraceGlobals.MapiStoreDriverSubmissionTracer;

		// Token: 0x04000123 RID: 291
		private readonly string headers;

		// Token: 0x04000124 RID: 292
		private int current;

		// Token: 0x04000125 RID: 293
		private MailFromCommand mailFrom;

		// Token: 0x04000126 RID: 294
		private List<RcptToCommand> rcptToList;
	}
}
