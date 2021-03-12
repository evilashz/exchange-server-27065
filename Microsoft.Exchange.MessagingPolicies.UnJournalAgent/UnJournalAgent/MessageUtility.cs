using System;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mime;

namespace Microsoft.Exchange.MessagingPolicies.UnJournalAgent
{
	// Token: 0x02000013 RID: 19
	internal static class MessageUtility
	{
		// Token: 0x06000039 RID: 57 RVA: 0x000031A0 File Offset: 0x000013A0
		public static Stream GetMimePartReadStream(MimePart mimePart)
		{
			if (mimePart == null)
			{
				throw new ArgumentNullException("mimePart");
			}
			Stream stream = null;
			try
			{
				if (mimePart.TryGetContentReadStream(out stream))
				{
					if (stream.Length >= 0L)
					{
					}
				}
				else
				{
					stream = null;
				}
			}
			catch (ExchangeDataException)
			{
				stream = null;
			}
			finally
			{
				if (stream == null)
				{
					stream = mimePart.GetRawContentReadStream();
				}
			}
			return stream;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003208 File Offset: 0x00001408
		public static bool IsBlankLine(string line)
		{
			if (line == null)
			{
				return true;
			}
			Match match = MessageUtility.RegexBlankLine.Match(line);
			return Match.Empty != match;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003234 File Offset: 0x00001434
		public static string GetHeaderValue(Header header)
		{
			if (header == null)
			{
				throw new ArgumentNullException("header");
			}
			string text = null;
			if (!header.TryGetValue(out text))
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					header.WriteTo(memoryStream);
					memoryStream.Position = 0L;
					using (StreamReader streamReader = new StreamReader(memoryStream))
					{
						text = streamReader.ReadToEnd();
						text = text.Substring(header.Name.Length + 1).Trim();
					}
				}
			}
			return text;
		}

		// Token: 0x04000095 RID: 149
		private static readonly Regex RegexBlankLine = new Regex("^\\s*$", RegexOptions.Compiled);
	}
}
