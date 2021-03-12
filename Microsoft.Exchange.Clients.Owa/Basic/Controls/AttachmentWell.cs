using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000014 RID: 20
	public sealed class AttachmentWell
	{
		// Token: 0x06000089 RID: 137 RVA: 0x00005199 File Offset: 0x00003399
		private AttachmentWell()
		{
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000051A1 File Offset: 0x000033A1
		public static void RenderAttachmentWell(TextWriter output, AttachmentWellType wellType, ArrayList attachmentList, string itemId, UserContext userContext)
		{
			AttachmentWell.RenderAttachmentWell(output, wellType, attachmentList, itemId, userContext, AttachmentWell.AttachmentWellFlags.None);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000051B0 File Offset: 0x000033B0
		public static void RenderAttachmentWell(TextWriter output, AttachmentWellType wellType, ArrayList attachmentList, string itemId, UserContext userContext, AttachmentWell.AttachmentWellFlags flags)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (itemId == null)
			{
				throw new ArgumentNullException("itemId");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (wellType == AttachmentWellType.ReadOnly && AttachmentUtility.IsLevelOneAndBlockOnly(attachmentList))
			{
				return;
			}
			output.Write("<div id=\"divAtt\" class=\"aw\">");
			AttachmentWell.RenderAttachments(output, wellType, attachmentList, itemId, userContext, flags);
			output.Write("</div>");
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00005217 File Offset: 0x00003417
		internal static ArrayList GetAttachmentInformation(Item item, IList<AttachmentLink> attachmentLinks, bool isLoggedOnFromPublicComputer)
		{
			return AttachmentWell.GetAttachmentInformation(item, attachmentLinks, isLoggedOnFromPublicComputer, false);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00005224 File Offset: 0x00003424
		internal static ArrayList GetAttachmentInformation(Item item, IList<AttachmentLink> attachmentLinks, bool isLoggedOnFromPublicComputer, bool isEmbeddedItem)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (item.AttachmentCollection == null)
			{
				return null;
			}
			int count = item.AttachmentCollection.Count;
			ArrayList arrayList = new ArrayList();
			return AttachmentUtility.GetAttachmentList(item, attachmentLinks, isLoggedOnFromPublicComputer, isEmbeddedItem, false);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00005268 File Offset: 0x00003468
		private static void RenderAttachments(TextWriter output, AttachmentWellType wellType, ArrayList attachmentList, string itemId, UserContext userContext, AttachmentWell.AttachmentWellFlags flags)
		{
			if (attachmentList == null)
			{
				return;
			}
			int count = attachmentList.Count;
			if (count <= 0)
			{
				return;
			}
			if (Utilities.GetEmbeddedDepth(HttpContext.Current.Request) >= AttachmentPolicy.MaxEmbeddedDepth)
			{
				flags |= AttachmentWell.AttachmentWellFlags.RenderReachedMaxEmbeddedDepth;
			}
			ArrayList previousAttachmentDisplayNames = new ArrayList();
			bool prependSemicolon = false;
			foreach (object obj in attachmentList)
			{
				AttachmentWellInfo attachmentWellInfo = (AttachmentWellInfo)obj;
				AttachmentUtility.AttachmentLinkFlags attachmentLinkFlag = AttachmentUtility.GetAttachmentLinkFlag(wellType, attachmentWellInfo);
				if (AttachmentUtility.AttachmentLinkFlags.Skip != (AttachmentUtility.AttachmentLinkFlags.Skip & attachmentLinkFlag) && (!attachmentWellInfo.IsInline || (flags & AttachmentWell.AttachmentWellFlags.RenderInLine) == AttachmentWell.AttachmentWellFlags.RenderInLine) && ((flags & AttachmentWell.AttachmentWellFlags.RenderInLine) == AttachmentWell.AttachmentWellFlags.RenderInLine || (!attachmentWellInfo.IsInline && attachmentWellInfo.AttachmentType != AttachmentType.Ole)))
				{
					Item item = null;
					ItemAttachment itemAttachment = null;
					try
					{
						if (attachmentWellInfo.AttachmentType == AttachmentType.EmbeddedMessage)
						{
							itemAttachment = (ItemAttachment)attachmentWellInfo.OpenAttachment();
							item = itemAttachment.GetItemAsReadOnly(null);
						}
						if (item != null)
						{
							AttachmentWell.RenderAttachmentLinkForItem(output, attachmentWellInfo, item, itemId, userContext, previousAttachmentDisplayNames, flags, prependSemicolon);
						}
						else
						{
							AttachmentWell.RenderAttachmentLink(output, wellType, attachmentWellInfo, itemId, userContext, previousAttachmentDisplayNames, flags | AttachmentWell.AttachmentWellFlags.RenderAttachmentSize, prependSemicolon);
						}
						prependSemicolon = true;
					}
					catch (ObjectNotFoundException)
					{
					}
					finally
					{
						if (item != null)
						{
							item.Dispose();
							item = null;
						}
						if (itemAttachment != null)
						{
							itemAttachment.Dispose();
							itemAttachment = null;
						}
					}
				}
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000053C0 File Offset: 0x000035C0
		internal static void RenderAttachmentLinkForItem(TextWriter output, AttachmentWellInfo attachmentInfoObject, Item item, string itemId, UserContext userContext, ArrayList previousAttachmentDisplayNames, AttachmentWell.AttachmentWellFlags flags, bool prependSemicolon = false)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (attachmentInfoObject == null)
			{
				throw new ArgumentNullException("attachmentInfoObject");
			}
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			SanitizedHtmlString value;
			bool flag;
			if ((attachmentInfoObject.AttachmentLevel == AttachmentPolicy.Level.ForceSave || attachmentInfoObject.AttachmentLevel == AttachmentPolicy.Level.Allow) && AttachmentWell.AttachmentWellFlags.RenderEmbeddedItem == (flags & AttachmentWell.AttachmentWellFlags.RenderEmbeddedItem))
			{
				string format = string.Empty;
				if (attachmentInfoObject.AttachmentLevel == AttachmentPolicy.Level.Allow)
				{
					format = "<a id=\"{0}\" href=\"#\" onclick=\"{1}\" title=\"{2}\" oncontextmenu=\"return false;\">{3}</a>";
				}
				else
				{
					format = "<a id=\"{0}\" href=\"#\" onclick=\"{1}\" title=\"{2}\">{3}</a>";
				}
				SanitizingStringBuilder<OwaHtml> sanitizingStringBuilder = new SanitizingStringBuilder<OwaHtml>();
				sanitizingStringBuilder.Append("?ae=Item&t=");
				sanitizingStringBuilder.Append(Utilities.UrlEncode(item.ClassName));
				sanitizingStringBuilder.Append("&atttyp=embdd");
				if (ObjectClass.IsCalendarItemCalendarItemOccurrenceOrRecurrenceException(item.ClassName))
				{
					sanitizingStringBuilder.Append("&a=Read");
				}
				SanitizingStringBuilder<OwaHtml> sanitizingStringBuilder2 = new SanitizingStringBuilder<OwaHtml>();
				if ((flags & AttachmentWell.AttachmentWellFlags.RenderReachedMaxEmbeddedDepth) == (AttachmentWell.AttachmentWellFlags)0)
				{
					sanitizingStringBuilder2.Append("return onClkEmbItem('");
					sanitizingStringBuilder2.Append<SanitizedHtmlString>(sanitizingStringBuilder.ToSanitizedString<SanitizedHtmlString>());
					sanitizingStringBuilder2.Append("','");
					sanitizingStringBuilder2.Append(Utilities.UrlEncode(attachmentInfoObject.AttachmentId.ToBase64String()));
					sanitizingStringBuilder2.Append("');");
				}
				else
				{
					sanitizingStringBuilder2.Append("return alert(L_ErrRchMxEmbDpth)");
				}
				string embeddedAttachmentDisplayName = AttachmentUtility.GetEmbeddedAttachmentDisplayName(item);
				value = SanitizedHtmlString.Format(format, new object[]
				{
					"lnkAtmt",
					sanitizingStringBuilder2.ToSanitizedString<SanitizedHtmlString>(),
					embeddedAttachmentDisplayName,
					AttachmentUtility.TrimAttachmentDisplayName(embeddedAttachmentDisplayName, previousAttachmentDisplayNames, true)
				});
				flag = false;
			}
			else
			{
				value = Utilities.SanitizeHtmlEncode(AttachmentUtility.TrimAttachmentDisplayName(AttachmentUtility.GetEmbeddedAttachmentDisplayName(item), previousAttachmentDisplayNames, true));
				flag = true;
			}
			if (prependSemicolon)
			{
				output.Write("; ");
			}
			output.Write("<span id=\"spnAtmt\" tabindex=\"-1\" level=\"3\"");
			if (flag)
			{
				output.Write(" class=\"dsbl\"");
			}
			output.Write(">");
			output.Write("<img class=\"sI\" src=\"");
			SmallIconManager.RenderItemIconUrl(output, userContext, item.ClassName);
			output.Write("\" alt=\"\">");
			output.Write(value);
			output.Write("</span>");
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000055B4 File Offset: 0x000037B4
		internal static void RenderAttachmentLink(TextWriter output, AttachmentWellType wellType, AttachmentWellInfo attachmentInfoObject, string itemId, UserContext userContext, ArrayList previousAttachmentDisplayNames, AttachmentWell.AttachmentWellFlags flags, bool prependSemicolon = false)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (attachmentInfoObject == null)
			{
				throw new ArgumentNullException("attachmentInfoObject");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (string.IsNullOrEmpty(itemId))
			{
				throw new ArgumentException("itemId may not be null or empty string");
			}
			SanitizedHtmlString value = null;
			string fileExtension = string.Empty;
			SanitizedHtmlString sanitizedHtmlString = null;
			AttachmentUtility.AttachmentLinkFlags attachmentLinkFlag = AttachmentUtility.GetAttachmentLinkFlag(wellType, attachmentInfoObject);
			if ((flags & AttachmentWell.AttachmentWellFlags.RenderReachedMaxEmbeddedDepth) != (AttachmentWell.AttachmentWellFlags)0)
			{
				string format = "<a id=\"{0}\" href=\"#\" onclick=\"{1}\" title=\"{2}\">{3}";
				string text = AttachmentUtility.TrimAttachmentDisplayName(attachmentInfoObject.AttachmentName, previousAttachmentDisplayNames, false);
				value = SanitizedHtmlString.Format(format, new object[]
				{
					"lnkAtmt",
					"return alert(L_ErrRchMxEmbDpth)",
					attachmentInfoObject.AttachmentName,
					text
				});
			}
			else
			{
				string format2 = "<a id=\"{0}\" href=\"attachment.ashx?attach=1&{1}\" target=_blank onclick=\"{2}\" title=\"{3}\">{4}";
				SanitizingStringBuilder<OwaHtml> sanitizingStringBuilder = new SanitizingStringBuilder<OwaHtml>();
				if (AttachmentWell.AttachmentWellFlags.RenderEmbeddedAttachment != (flags & AttachmentWell.AttachmentWellFlags.RenderEmbeddedAttachment))
				{
					sanitizingStringBuilder.Append("id=");
					sanitizingStringBuilder.Append(Utilities.UrlEncode(itemId));
					sanitizingStringBuilder.Append("&attid0=");
					sanitizingStringBuilder.Append(Utilities.UrlEncode(attachmentInfoObject.AttachmentId.ToBase64String()));
					sanitizingStringBuilder.Append("&attcnt=1");
				}
				else
				{
					sanitizingStringBuilder.Append(AttachmentWell.RenderEmbeddedQueryString(itemId));
					sanitizingStringBuilder.Append(Utilities.UrlEncode(attachmentInfoObject.AttachmentId.ToBase64String()));
				}
				sanitizedHtmlString = sanitizingStringBuilder.ToSanitizedString<SanitizedHtmlString>();
				if (attachmentInfoObject.AttachmentLevel == AttachmentPolicy.Level.ForceSave)
				{
					string text2 = AttachmentUtility.TrimAttachmentDisplayName(attachmentInfoObject.AttachmentName, previousAttachmentDisplayNames, false);
					value = SanitizedHtmlString.Format(format2, new object[]
					{
						"lnkAtmt",
						sanitizedHtmlString,
						"return onClkAtmt(2)",
						attachmentInfoObject.AttachmentName,
						text2
					});
				}
				if (attachmentInfoObject.AttachmentLevel == AttachmentPolicy.Level.Allow)
				{
					string text3 = AttachmentUtility.TrimAttachmentDisplayName(attachmentInfoObject.AttachmentName, previousAttachmentDisplayNames, false);
					value = SanitizedHtmlString.Format(format2, new object[]
					{
						"lnkAtmt",
						sanitizedHtmlString,
						"return onClkAtmt(3)",
						attachmentInfoObject.AttachmentName,
						text3
					});
				}
			}
			if (prependSemicolon)
			{
				output.Write("; ");
			}
			output.Write("<span id=\"spnAtmt\" tabindex=\"-1\" level=\"");
			output.Write((int)attachmentInfoObject.AttachmentLevel);
			if (AttachmentUtility.AttachmentLinkFlags.AttachmentClickLink != (AttachmentUtility.AttachmentLinkFlags.AttachmentClickLink & attachmentLinkFlag) && (flags & AttachmentWell.AttachmentWellFlags.RenderReachedMaxEmbeddedDepth) == (AttachmentWell.AttachmentWellFlags)0)
			{
				output.Write("\" class=\"dsbl");
			}
			output.Write("\">");
			output.Write("<img class=\"sI\" src=\"");
			if (attachmentInfoObject.FileExtension != null)
			{
				fileExtension = attachmentInfoObject.FileExtension;
			}
			SmallIconManager.RenderFileIconUrl(output, userContext, fileExtension);
			output.Write("\" alt=\"\">");
			if (AttachmentUtility.AttachmentLinkFlags.AttachmentClickLink == (AttachmentUtility.AttachmentLinkFlags.AttachmentClickLink & attachmentLinkFlag) || (flags & AttachmentWell.AttachmentWellFlags.RenderReachedMaxEmbeddedDepth) != (AttachmentWell.AttachmentWellFlags)0)
			{
				output.Write(value);
			}
			else
			{
				Utilities.SanitizeHtmlEncode(AttachmentUtility.TrimAttachmentDisplayName(attachmentInfoObject.AttachmentName, previousAttachmentDisplayNames, false), output);
			}
			if (AttachmentWell.AttachmentWellFlags.RenderAttachmentSize == (flags & AttachmentWell.AttachmentWellFlags.RenderAttachmentSize))
			{
				long size = attachmentInfoObject.Size;
				if (size > 0L)
				{
					output.Write(userContext.DirectionMark);
					output.Write(" ");
					output.Write(SanitizedHtmlString.FromStringId(6409762));
					Utilities.RenderSizeWithUnits(output, size, true);
					output.Write(userContext.DirectionMark);
					output.Write(SanitizedHtmlString.FromStringId(-1023695022));
				}
			}
			output.Write("</a>");
			if (AttachmentUtility.AttachmentLinkFlags.OpenAsWebPageLink == (AttachmentUtility.AttachmentLinkFlags.OpenAsWebPageLink & attachmentLinkFlag))
			{
				output.Write("<span class=\"wvsn\">[<a id=\"wvLnk\" href=\"#\" onclick=\"");
				output.Write("opnWin('WebReadyView.aspx?t=att&");
				output.Write(sanitizedHtmlString);
				output.Write("')\">");
				output.Write(SanitizedHtmlString.FromStringId(1547877601));
				output.Write("</a>]</span>");
			}
			output.Write("</span>");
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000058FC File Offset: 0x00003AFC
		public static string RenderEmbeddedUrl(string parentItemId)
		{
			return AttachmentWell.RenderEmbeddedUrl(parentItemId, false);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00005905 File Offset: 0x00003B05
		public static string RenderEmbeddedQueryString(string parentItemId)
		{
			return AttachmentWell.RenderEmbeddedUrl(parentItemId, true);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00005910 File Offset: 0x00003B10
		private static string RenderEmbeddedUrl(string parentItemId, bool queryStringOnly)
		{
			string queryStringParameter = Utilities.GetQueryStringParameter(HttpContext.Current.Request, "attcnt");
			int num;
			if (!int.TryParse(queryStringParameter, out num))
			{
				throw new OwaInvalidRequestException("Invalid attachment count querystring parameter");
			}
			StringBuilder stringBuilder = new StringBuilder(200);
			if (!queryStringOnly)
			{
				stringBuilder.Append("attachment.ashx?attach=1&");
			}
			stringBuilder.Append("attcnt=");
			stringBuilder.Append(num + 1);
			stringBuilder.Append("&id=");
			stringBuilder.Append(Utilities.UrlEncode(parentItemId));
			for (int i = 0; i < num; i++)
			{
				string text = "attid" + i.ToString(CultureInfo.InvariantCulture);
				string queryStringParameter2 = Utilities.GetQueryStringParameter(HttpContext.Current.Request, text);
				stringBuilder.Append("&");
				stringBuilder.Append(text);
				stringBuilder.Append("=");
				stringBuilder.Append(Utilities.UrlEncode(queryStringParameter2));
			}
			stringBuilder.Append("&attid" + num.ToString(CultureInfo.InvariantCulture) + "=");
			return stringBuilder.ToString();
		}

		// Token: 0x0400005B RID: 91
		public const string AttachmentSemicolonSeparator = "; ";

		// Token: 0x0400005C RID: 92
		public static readonly string AttachmentInfobarHtmlTag = "divIDA";

		// Token: 0x02000015 RID: 21
		[Flags]
		public enum AttachmentWellFlags
		{
			// Token: 0x0400005E RID: 94
			None = 1,
			// Token: 0x0400005F RID: 95
			RenderEmbeddedItem = 2,
			// Token: 0x04000060 RID: 96
			RenderEmbeddedAttachment = 4,
			// Token: 0x04000061 RID: 97
			RenderInLine = 8,
			// Token: 0x04000062 RID: 98
			RenderReachedMaxEmbeddedDepth = 16,
			// Token: 0x04000063 RID: 99
			RenderAttachmentSize = 32
		}
	}
}
