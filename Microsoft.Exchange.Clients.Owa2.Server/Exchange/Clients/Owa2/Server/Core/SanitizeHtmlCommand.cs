using System;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200034C RID: 844
	internal class SanitizeHtmlCommand : ServiceCommand<string>
	{
		// Token: 0x06001B93 RID: 7059 RVA: 0x00069C71 File Offset: 0x00067E71
		public SanitizeHtmlCommand(CallContext callContext, string body) : base(callContext)
		{
			this.body = body;
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x00069C84 File Offset: 0x00067E84
		public static string CleanHtml(string input)
		{
			string text = null;
			HtmlToHtml htmlToHtml = new HtmlToHtml();
			htmlToHtml.FilterHtml = true;
			htmlToHtml.OutputHtmlFragment = true;
			string result;
			using (TextReader textReader = new StringReader(input))
			{
				using (TextWriter textWriter = new StringWriter())
				{
					try
					{
						htmlToHtml.Convert(textReader, textWriter);
						text = textWriter.ToString();
					}
					catch (ExchangeDataException innerException)
					{
						throw FaultExceptionUtilities.CreateFault(new OwaCannotSanitizeHtmlException("Sanitization of the HTML failed", innerException, htmlToHtml), FaultParty.Sender);
					}
					result = text;
				}
			}
			return result;
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x00069D20 File Offset: 0x00067F20
		protected override string InternalExecute()
		{
			return SanitizeHtmlCommand.CleanHtml(this.body);
		}

		// Token: 0x04000F93 RID: 3987
		private readonly string body;
	}
}
