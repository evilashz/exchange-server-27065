using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security.AntiXss;
using Microsoft.Exchange.Data.ApplicationLogic.Directory;
using Microsoft.Exchange.Data.GroupMailbox.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x02000042 RID: 66
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class GroupJoinRequestMessageBodyBuilder
	{
		// Token: 0x060001FD RID: 509 RVA: 0x0000D7D1 File Offset: 0x0000B9D1
		public static void WriteMessageToStream(StreamWriter writer, string senderDisplayName, string groupDisplayName, string attachedMessageBody, MailboxUrls mailboxUrls, CultureInfo cultureInfo)
		{
			writer.Write(GroupJoinRequestMessageBodyBuilder.RenderTemplate(senderDisplayName, groupDisplayName, attachedMessageBody, GroupJoinRequestMessageBodyBuilder.ConvertFragmentToQuery(mailboxUrls.PeopleUrl), cultureInfo));
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000D7F0 File Offset: 0x0000B9F0
		private static string RenderTemplate(string requestingMemberName, string groupName, string requestMessage, string groupMembersUrl, CultureInfo cultureInfo)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Uri uri = new Uri(groupMembersUrl);
			dictionary["header"] = AntiXssEncoder.HtmlEncode(Strings.JoinRequestMessageHeading(requestingMemberName, groupName).ToString(cultureInfo), false);
			if (!string.IsNullOrEmpty(requestMessage))
			{
				dictionary["intro_text"] = AntiXssEncoder.HtmlEncode(Strings.JoinRequestMessageAttachedBodyPrefix.ToString(cultureInfo), false);
				dictionary["user_message"] = AntiXssEncoder.HtmlEncode(requestMessage, false);
			}
			else
			{
				dictionary["intro_text"] = AntiXssEncoder.HtmlEncode(Strings.JoinRequestMessageNoAttachedBodyPrefix.ToString(cultureInfo), false);
				dictionary["user_message"] = string.Empty;
			}
			dictionary["footer_text"] = Strings.JoinRequestMessageFooterTextWithLink(uri.ToString()).ToString(cultureInfo);
			return GroupJoinRequestMessageBodyBuilder.RenderTemplate(GroupJoinRequestMessageBodyBuilder.JoinRequestHtmlTemplate.Value, dictionary);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000D8CC File Offset: 0x0000BACC
		private static string ReadHtmlFromEmbeddedResources(string templateName)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("templateName", templateName);
			string text = null;
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(templateName))
			{
				using (StreamReader streamReader = new StreamReader(manifestResourceStream, Encoding.UTF8))
				{
					GroupJoinRequestMessageBodyBuilder.Tracer.TraceDebug<string>(0L, "Found template {0} as embedded resource.", templateName);
					text = streamReader.ReadToEnd();
					GroupJoinRequestMessageBodyBuilder.Tracer.TraceDebug<int, string>(0L, "Read {0} bytes for image {1}.", text.Length, templateName);
				}
			}
			return text;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000D9B8 File Offset: 0x0000BBB8
		private static string RenderTemplate(string template, Dictionary<string, object> replacements)
		{
			return GroupJoinRequestMessageBodyBuilder.TemplateVariableMatchRegex.Replace(template, delegate(Match m)
			{
				string value = m.Groups[1].Value;
				return replacements.ContainsKey(value) ? replacements[value].ToString() : m.Value;
			});
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000D9EC File Offset: 0x0000BBEC
		private static string ConvertFragmentToQuery(string url)
		{
			Uri uri = new Uri(url);
			NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(uri.Query);
			if (uri.Fragment.Length > 1)
			{
				NameValueCollection c = HttpUtility.ParseQueryString(uri.Fragment.Substring(1));
				nameValueCollection.Add(c);
			}
			return string.Concat(new string[]
			{
				uri.Scheme,
				"://",
				uri.Authority,
				uri.AbsolutePath,
				"?",
				nameValueCollection.ToString()
			});
		}

		// Token: 0x0400011B RID: 283
		private static readonly Trace Tracer = ExTraceGlobals.GroupEmailNotificationHandlerTracer;

		// Token: 0x0400011C RID: 284
		private static readonly Lazy<string> JoinRequestHtmlTemplate = new Lazy<string>(() => GroupJoinRequestMessageBodyBuilder.ReadHtmlFromEmbeddedResources("group_join_request_message_template.thtm"));

		// Token: 0x0400011D RID: 285
		private static readonly Regex TemplateVariableMatchRegex = new Regex("\\${([^}]+)}", RegexOptions.Compiled);
	}
}
