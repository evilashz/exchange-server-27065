using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.MessagingPolicies.UnJournalAgent
{
	// Token: 0x02000019 RID: 25
	internal class ReportReader : IDisposable
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00004077 File Offset: 0x00002277
		public ReportReader(Stream stream, bool plaintext)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.contentReader = new StreamReader(stream);
			this.plaintext = plaintext;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000040A0 File Offset: 0x000022A0
		public string ReadLine()
		{
			string text = this.contentReader.ReadLine();
			if (!string.IsNullOrEmpty(text) && !this.plaintext)
			{
				text = ReportReader.ParseHtmlTokens(text);
			}
			return text;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000040D1 File Offset: 0x000022D1
		public void Dispose()
		{
			this.contentReader.Dispose();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000040E0 File Offset: 0x000022E0
		private static string ParseHtmlTokens(string line)
		{
			MatchEvaluator evaluator = new MatchEvaluator(ReportReader.ReplaceHtmlToken);
			return ReportReader.RegexHtmlToken.Replace(line, evaluator);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004108 File Offset: 0x00002308
		private static string ReplaceHtmlToken(Match match)
		{
			string text = match.Value.ToUpperInvariant();
			string a;
			if ((a = text) != null)
			{
				if (a == "&QUOT;")
				{
					return "\"";
				}
				if (a == "&LT;")
				{
					return "<";
				}
				if (a == "&GT;")
				{
					return ">";
				}
				if (a == "&NBSP;")
				{
					return " ";
				}
			}
			return string.Empty;
		}

		// Token: 0x040000BB RID: 187
		private static readonly Regex RegexHtmlToken = new Regex("(</?[^>]*>|&nbsp;|&quot;|&lt;|&gt;)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x040000BC RID: 188
		private readonly bool plaintext;

		// Token: 0x040000BD RID: 189
		private StreamReader contentReader;
	}
}
