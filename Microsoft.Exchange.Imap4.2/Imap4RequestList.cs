using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x0200002B RID: 43
	internal class Imap4RequestList : Imap4RequestWithStringParameters
	{
		// Token: 0x060001B9 RID: 441 RVA: 0x0000B9B0 File Offset: 0x00009BB0
		public Imap4RequestList(Imap4ResponseFactory factory, string tag, string data) : base(factory, tag, data, 2, 2)
		{
			this.PerfCounterTotal = base.Imap4CountersInstance.PerfCounter_LIST_Total;
			this.PerfCounterFailures = base.Imap4CountersInstance.PerfCounter_LIST_Failures;
			base.AllowedStates = (Imap4State.Authenticated | Imap4State.Selected);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000B9E8 File Offset: 0x00009BE8
		public override ProtocolResponse Process()
		{
			Regex regex;
			bool flag;
			Imap4Response imap4Response = this.Preprocess(out regex, out flag);
			if (imap4Response != null)
			{
				return imap4Response;
			}
			if (base.ArrayOfArguments[1].Length == 0)
			{
				imap4Response = new Imap4Response(this, Imap4Response.Type.ok);
				using (Imap4Mailbox rootMailbox = Imap4Mailbox.GetRootMailbox(base.Factory))
				{
					Imap4RequestList.AddOneMailbox(imap4Response, rootMailbox.GetEncodedPath(), rootMailbox.ListFlags);
					if (base.Session.LightLogSession != null)
					{
						base.Session.LightLogSession.FolderCount = new int?(1);
					}
					goto IL_1AF;
				}
			}
			bool showHiddenFolders = ((Imap4Server)base.Factory.Session.Server).ShowHiddenFolders;
			ICollection<Imap4Mailbox> mailboxTree = Imap4Mailbox.GetMailboxTree(base.Factory);
			if (mailboxTree == null)
			{
				return null;
			}
			imap4Response = new Imap4Response(this, Imap4Response.Type.ok);
			((Imap4Session)base.Session).PopulateInternalMailboxes(base.Factory, mailboxTree);
			if (base.Session.LightLogSession != null)
			{
				base.Session.LightLogSession.FolderCount = new int?(0);
			}
			foreach (Imap4Mailbox imap4Mailbox in mailboxTree)
			{
				if (!imap4Mailbox.IsRoot && !imap4Mailbox.MailboxDoesNotExist && (showHiddenFolders || !imap4Mailbox.IsHidden) && regex.IsMatch(imap4Mailbox.FullPath))
				{
					string encodedPath = imap4Mailbox.GetEncodedPath();
					Imap4RequestList.AddOneMailbox(imap4Response, encodedPath, imap4Mailbox.ListFlags);
					if (base.Session.LightLogSession != null)
					{
						base.Session.LightLogSession.FolderCount++;
					}
				}
			}
			IL_1AF:
			imap4Response.Append("[*] LIST completed.");
			return imap4Response;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000BBCC File Offset: 0x00009DCC
		protected Imap4Response Preprocess(out Regex nameFilter, out bool deepTraversal)
		{
			nameFilter = null;
			deepTraversal = false;
			string referenceName;
			if (!Imap4UTF7Encoding.TryDecode(base.ArrayOfArguments[0], out referenceName))
			{
				return new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 11");
			}
			string text;
			if (!Imap4UTF7Encoding.TryDecode(base.ArrayOfArguments[1], out text))
			{
				return new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 11");
			}
			int num = 0;
			foreach (char c in text.ToCharArray())
			{
				if (c == '*' || c == '%')
				{
					num++;
					if (num > 10)
					{
						return new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 11");
					}
				}
			}
			if (base.ArrayOfArguments[0].Length > 0)
			{
				string empty = string.Empty;
				if (!Imap4Mailbox.TryNormalizeMailboxPath(base.ArrayOfArguments[0], out empty))
				{
					return new Imap4Response(this, Imap4Response.Type.bad, "Command Argument Error. 11");
				}
			}
			deepTraversal = (text.IndexOf('*') > -1 || text.IndexOf('/') > -1);
			nameFilter = this.GetNameFilter(referenceName, text);
			return null;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000BCD0 File Offset: 0x00009ED0
		private static void AddOneMailbox(Imap4Response response, string mailboxName, string flags)
		{
			if (mailboxName.IndexOfAny(Imap4RequestList.QuotedSpecials) > -1)
			{
				response.AppendFormat("* LIST ({0}) \"{1}\" {{{2}}}\r\n{3}\r\n", new object[]
				{
					flags,
					'/',
					mailboxName.Length,
					mailboxName
				});
				return;
			}
			if (string.IsNullOrEmpty(mailboxName) || mailboxName.IndexOfAny(Imap4RequestList.AtomSpecials) > -1)
			{
				response.AppendFormat("* LIST ({0}) \"{1}\" \"{2}\"\r\n", new object[]
				{
					flags,
					'/',
					mailboxName
				});
				return;
			}
			response.AppendFormat("* LIST ({0}) \"{1}\" {2}\r\n", new object[]
			{
				flags,
				'/',
				mailboxName
			});
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000BD80 File Offset: 0x00009F80
		private Regex GetNameFilter(string referenceName, string wildcardMailbox)
		{
			while (wildcardMailbox.IndexOf("**") != -1 || wildcardMailbox.IndexOf("%%") != -1)
			{
				wildcardMailbox = wildcardMailbox.Replace("**", "*");
				wildcardMailbox = wildcardMailbox.Replace("%%", "%");
			}
			referenceName = Regex.Escape(referenceName);
			wildcardMailbox = Regex.Escape(wildcardMailbox);
			wildcardMailbox = wildcardMailbox.Replace("\\" + '*', ".*");
			wildcardMailbox = wildcardMailbox.Replace(string.Empty + '%', "[^/]*");
			string text = "^" + referenceName + wildcardMailbox + "$";
			ProtocolBaseServices.SessionTracer.TraceDebug<string>(base.Session.SessionId, "The wildcard string is \"{0}\"", text);
			return new Regex(text, RegexOptions.IgnoreCase);
		}

		// Token: 0x0400014A RID: 330
		private const string LISTResponse = "[*] LIST completed.";

		// Token: 0x0400014B RID: 331
		private const string LISTResponseLine = "* LIST ({0}) \"{1}\" {2}\r\n";

		// Token: 0x0400014C RID: 332
		private const string LISTResponseLineWithQuotes = "* LIST ({0}) \"{1}\" \"{2}\"\r\n";

		// Token: 0x0400014D RID: 333
		private const string LISTResponseLineLiteral = "* LIST ({0}) \"{1}\" {{{2}}}\r\n{3}\r\n";

		// Token: 0x0400014E RID: 334
		private const char RecursiveWildcard = '*';

		// Token: 0x0400014F RID: 335
		private const char OneLevelWildcard = '%';

		// Token: 0x04000150 RID: 336
		private const int MaxWildcardsInImapList = 10;

		// Token: 0x04000151 RID: 337
		public static readonly char[] AtomSpecials = new char[]
		{
			'(',
			')',
			'{',
			' ',
			'%',
			'*',
			']'
		};

		// Token: 0x04000152 RID: 338
		public static readonly char[] QuotedSpecials = new char[]
		{
			'"',
			'\\'
		};

		// Token: 0x04000153 RID: 339
		private static readonly char[] wildcards = new char[]
		{
			'*',
			'%'
		};
	}
}
