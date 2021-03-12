using System;
using System.IO;
using System.Text;
using System.Web;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Data.TextConverters.Internal;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000036 RID: 54
	internal static class AttachmentUtilities
	{
		// Token: 0x06000127 RID: 295 RVA: 0x00004F24 File Offset: 0x00003124
		internal static string ToHexString(string fileName, bool chrome)
		{
			StringBuilder stringBuilder = new StringBuilder(fileName.Length);
			byte[] bytes = Encoding.UTF8.GetBytes(fileName);
			for (int i = 0; i < bytes.Length; i++)
			{
				if (bytes[i] >= 0 && bytes[i] <= 127)
				{
					if (AttachmentUtilities.IsMIMEAttributeSpecialChar((char)bytes[i], chrome))
					{
						stringBuilder.AppendFormat("%{0}", Convert.ToString(bytes[i], 16));
					}
					else
					{
						stringBuilder.Append((char)bytes[i]);
					}
				}
				else
				{
					stringBuilder.AppendFormat("%{0}", Convert.ToString(bytes[i], 16));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00004FB0 File Offset: 0x000031B0
		internal static Stream GetFilteredStream(ConfigurationContextBase configurationContext, Stream inputStream, Charset charset, BlockStatus blockStatus)
		{
			HtmlToHtml htmlToHtml = new HtmlToHtml();
			TextConvertersInternalHelpers.SetPreserveDisplayNoneStyle(htmlToHtml, true);
			WebBeaconFilterLevels filterWebBeaconsAndHtmlForms = configurationContext.FilterWebBeaconsAndHtmlForms;
			bool flag = filterWebBeaconsAndHtmlForms == WebBeaconFilterLevels.DisableFilter || blockStatus == BlockStatus.NoNeverAgain;
			Encoding encoding = null;
			if (charset != null && charset.TryGetEncoding(out encoding))
			{
				htmlToHtml.DetectEncodingFromMetaTag = false;
				htmlToHtml.InputEncoding = encoding;
				htmlToHtml.OutputEncoding = encoding;
			}
			else
			{
				htmlToHtml.DetectEncodingFromMetaTag = true;
				htmlToHtml.InputEncoding = Encoding.ASCII;
				htmlToHtml.OutputEncoding = null;
			}
			htmlToHtml.FilterHtml = true;
			if (!flag)
			{
				htmlToHtml.HtmlTagCallback = new HtmlTagCallback(AttachmentUtilities.webBeaconFilter.ProcessTag);
			}
			return new ConverterStream(inputStream, htmlToHtml, ConverterStreamAccess.Read);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005048 File Offset: 0x00003248
		internal static string GetContentType(Attachment attachment)
		{
			string text = attachment.ContentType;
			if (string.IsNullOrEmpty(text))
			{
				text = attachment.CalculatedContentType;
			}
			if (string.Equals(text, "audio/mp3", StringComparison.OrdinalIgnoreCase))
			{
				text = "audio/mpeg";
			}
			return text;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005080 File Offset: 0x00003280
		internal static bool NeedToFilterHtml(string contentType, string fileExtension, AttachmentPolicyLevel level, ConfigurationContextBase configurationContext)
		{
			bool flag = AttachmentUtilities.IsHtmlAttachment(contentType, fileExtension);
			bool flag2 = AttachmentPolicyLevel.ForceSave == level;
			bool flag3 = configurationContext.IsFeatureEnabled(Feature.ForceSaveAttachmentFiltering);
			return flag && (!flag2 || flag3);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000050B5 File Offset: 0x000032B5
		internal static bool GetDoNotSniff(AttachmentPolicyLevel level, ConfigurationContextBase configurationContext)
		{
			if (configurationContext == null)
			{
				throw new ArgumentNullException("configurationContext");
			}
			return AttachmentPolicyLevel.ForceSave == level && !configurationContext.IsFeatureEnabled(Feature.ForceSaveAttachmentFiltering);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000050DD File Offset: 0x000032DD
		internal static bool GetIsHtmlOrXml(string contentType, string fileExtension)
		{
			if (contentType == null)
			{
				throw new ArgumentNullException("contentType");
			}
			return AttachmentUtilities.IsXmlAttachment(contentType, fileExtension) || AttachmentUtilities.IsHtmlAttachment(contentType, fileExtension);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00005100 File Offset: 0x00003300
		internal static string TryGetMailboxIdentityName()
		{
			UserContext userContext = UserContextManager.GetUserContext(HttpContext.Current, CallContext.Current.EffectiveCaller, true);
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			string result = string.Empty;
			if (userContext.MailboxIdentity != null)
			{
				result = userContext.MailboxIdentity.SafeGetRenderableName();
			}
			return result;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000514C File Offset: 0x0000334C
		public static bool DeleteAttachment(AttachmentIdType attachmentId)
		{
			bool result = false;
			if (attachmentId != null)
			{
				DeleteAttachmentJsonRequest deleteAttachmentJsonRequest = new DeleteAttachmentJsonRequest();
				DeleteAttachmentRequest deleteAttachmentRequest = new DeleteAttachmentRequest();
				deleteAttachmentRequest.AttachmentIds = new AttachmentIdType[1];
				deleteAttachmentRequest.AttachmentIds[0] = attachmentId;
				deleteAttachmentJsonRequest.Body = deleteAttachmentRequest;
				OWAService owaservice = new OWAService();
				IAsyncResult asyncResult = owaservice.BeginDeleteAttachment(deleteAttachmentJsonRequest, null, null);
				asyncResult.AsyncWaitHandle.WaitOne();
				DeleteAttachmentResponse body = owaservice.EndDeleteAttachment(asyncResult).Body;
				if (body != null && body.ResponseMessages != null && body.ResponseMessages.Items != null && body.ResponseMessages.Items[0] != null)
				{
					result = (body.ResponseMessages.Items[0].ResponseCode == ResponseCodeType.NoError);
				}
			}
			return result;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000051FC File Offset: 0x000033FC
		private static bool IsHtmlAttachment(string contentType, string fileExtension)
		{
			if (contentType == null)
			{
				throw new ArgumentNullException("contentType");
			}
			return contentType.ToLowerInvariant().Contains("text/html") || contentType.ToLowerInvariant().Contains("application/xhtml+xml") || StringComparer.OrdinalIgnoreCase.Compare(fileExtension, ".htm") == 0 || StringComparer.OrdinalIgnoreCase.Compare(fileExtension, ".html") == 0 || StringComparer.OrdinalIgnoreCase.Compare(fileExtension, ".xhtml") == 0 || StringComparer.OrdinalIgnoreCase.Compare(fileExtension, ".xht") == 0 || StringComparer.OrdinalIgnoreCase.Compare(fileExtension, ".shtml") == 0 || StringComparer.OrdinalIgnoreCase.Compare(fileExtension, ".shtm") == 0 || StringComparer.OrdinalIgnoreCase.Compare(fileExtension, ".stm") == 0;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000052BF File Offset: 0x000034BF
		private static bool IsXmlAttachment(string contentType, string fileExtension)
		{
			if (contentType == null)
			{
				throw new ArgumentNullException("contentType");
			}
			return contentType.ToLowerInvariant().Contains("text/xml") || StringComparer.OrdinalIgnoreCase.Compare(fileExtension, ".xml") == 0;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000052F8 File Offset: 0x000034F8
		private static bool IsMIMEAttributeSpecialChar(char c, bool chrome)
		{
			if (chrome && (c == '(' || c == ')'))
			{
				return false;
			}
			if (char.IsControl(c))
			{
				return true;
			}
			switch (c)
			{
			case ' ':
			case '"':
			case '%':
			case '\'':
			case '(':
			case ')':
			case '*':
			case ',':
			case '/':
				break;
			case '!':
			case '#':
			case '$':
			case '&':
			case '+':
			case '-':
			case '.':
				return false;
			default:
				switch (c)
				{
				case ':':
				case ';':
				case '<':
				case '=':
				case '>':
				case '?':
				case '@':
					break;
				default:
					switch (c)
					{
					case '[':
					case '\\':
					case ']':
						break;
					default:
						return false;
					}
					break;
				}
				break;
			}
			return true;
		}

		// Token: 0x04000073 RID: 115
		private static readonly AttachmentWebBeaconFilterCallback webBeaconFilter = new AttachmentWebBeaconFilterCallback();
	}
}
