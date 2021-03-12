using System;
using System.Diagnostics;
using System.Globalization;
using System.Web;
using System.Web.Security.AntiXss;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.GroupMailbox.Escalation
{
	// Token: 0x02000809 RID: 2057
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class GroupEscalationFooter
	{
		// Token: 0x06004C9D RID: 19613 RVA: 0x0013D920 File Offset: 0x0013BB20
		public GroupEscalationFooter(string groupDisplayName, CultureInfo cultureInfo, EscalationLinkBuilder linkBuilder)
		{
			Util.ThrowOnNullArgument(groupDisplayName, "groupDisplayName");
			Util.ThrowOnNullArgument(cultureInfo, "cultureInfo");
			Util.ThrowOnNullArgument(linkBuilder, "linkBuilder");
			this.groupDisplayName = groupDisplayName;
			this.cultureInfo = cultureInfo;
			this.lastLinkBuildTimeMs = 0L;
			this.lastLinkOnBodyDetectionTimeMs = 0L;
			this.lastLinkInsertOnBodyTimeMs = 0L;
			this.lastBodySizeBytes = 0L;
			this.linkBuilder = linkBuilder;
		}

		// Token: 0x170015EB RID: 5611
		// (get) Token: 0x06004C9E RID: 19614 RVA: 0x0013D989 File Offset: 0x0013BB89
		public long LastLinkBuildTimeMs
		{
			get
			{
				return this.lastLinkBuildTimeMs;
			}
		}

		// Token: 0x170015EC RID: 5612
		// (get) Token: 0x06004C9F RID: 19615 RVA: 0x0013D991 File Offset: 0x0013BB91
		public long LastLinkOnBodyDetectionTimeMs
		{
			get
			{
				return this.lastLinkOnBodyDetectionTimeMs;
			}
		}

		// Token: 0x170015ED RID: 5613
		// (get) Token: 0x06004CA0 RID: 19616 RVA: 0x0013D999 File Offset: 0x0013BB99
		public long LastLinkInsertOnBodyTimeMs
		{
			get
			{
				return this.lastLinkInsertOnBodyTimeMs;
			}
		}

		// Token: 0x170015EE RID: 5614
		// (get) Token: 0x06004CA1 RID: 19617 RVA: 0x0013D9A1 File Offset: 0x0013BBA1
		public long LastBodySizeBytes
		{
			get
			{
				return this.lastBodySizeBytes;
			}
		}

		// Token: 0x06004CA2 RID: 19618 RVA: 0x0013D9AC File Offset: 0x0013BBAC
		public bool InsertFooterToTheBody(IMessageItem originalMessage, IMessageItem escalatedMessage)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			string escalationLink = this.linkBuilder.GetEscalationLink(EscalationLinkType.Unsubscribe);
			stopwatch.Stop();
			this.lastLinkBuildTimeMs = stopwatch.ElapsedMilliseconds;
			if (!originalMessage.IBody.IsBodyDefined || this.BodyContainsFooter(originalMessage.IBody, escalationLink))
			{
				return false;
			}
			stopwatch = new Stopwatch();
			stopwatch.Start();
			bool flag = this.InsertLinkToBodyFooter(escalationLink, originalMessage.IBody, escalatedMessage.IBody);
			if (flag)
			{
				escalatedMessage.StampMessageBodyTag();
			}
			stopwatch.Stop();
			this.lastLinkInsertOnBodyTimeMs = stopwatch.ElapsedMilliseconds;
			return flag;
		}

		// Token: 0x06004CA3 RID: 19619 RVA: 0x0013DA40 File Offset: 0x0013BC40
		private string GetHtmlLink(string unsubscribeUrl)
		{
			string link = string.Format("<a id='{0}' href=\"{1}\">{2}</a>", "BD5134C6-8D33-4ABA-A0C4-08581FDF89DB", unsubscribeUrl, ClientStrings.GroupSubscriptionUnsubscribeLinkWord.ToString(this.cultureInfo));
			string groupName = AntiXssEncoder.HtmlEncode(this.groupDisplayName, false);
			return "<br /><div style=\"display:inline-block\" ><table border=\"0\" cellspacing=\"0\" style=\"background-color:#F4F4F4;\" ><tr><td style=\"padding:20px; font-size:12px; color:#666666\" >" + ClientStrings.GroupSubscriptionUnsubscribeInfoHtml(groupName, link).ToString(this.cultureInfo) + "</tr></td></table></div>";
		}

		// Token: 0x06004CA4 RID: 19620 RVA: 0x0013DAA4 File Offset: 0x0013BCA4
		private string GetPlainTextLink(string unsubscribeUrl)
		{
			return "\n\n" + ClientStrings.GroupSubscriptionUnsubscribeInfoText(this.groupDisplayName, unsubscribeUrl).ToString(this.cultureInfo);
		}

		// Token: 0x06004CA5 RID: 19621 RVA: 0x0013DAD8 File Offset: 0x0013BCD8
		protected virtual bool InsertLinkToBodyFooter(string unsubscribeUrl, IBody originalBody, IBody escalatedMessageBody)
		{
			BodyInjectionFormat injectionFormat;
			string suffixInjectionText;
			switch (originalBody.Format)
			{
			case BodyFormat.TextPlain:
				injectionFormat = BodyInjectionFormat.Text;
				suffixInjectionText = this.GetPlainTextLink(unsubscribeUrl);
				break;
			case BodyFormat.TextHtml:
			case BodyFormat.ApplicationRtf:
				injectionFormat = BodyInjectionFormat.Html;
				suffixInjectionText = this.GetHtmlLink(unsubscribeUrl);
				break;
			default:
				throw new ArgumentException("Unsupported body format: " + originalBody.Format);
			}
			originalBody.CopyBodyInjectingText(escalatedMessageBody, injectionFormat, null, suffixInjectionText);
			return true;
		}

		// Token: 0x06004CA6 RID: 19622 RVA: 0x0013DB40 File Offset: 0x0013BD40
		protected virtual bool BodyContainsFooter(IBody originalMessageBody, string link)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			bool result = false;
			string text;
			this.lastBodySizeBytes = (long)originalMessageBody.GetLastNBytesAsString(4096, out text);
			if (this.lastBodySizeBytes > 0L)
			{
				if (originalMessageBody.Format == BodyFormat.TextHtml)
				{
					text = HttpUtility.HtmlDecode(text);
				}
				if (text.Contains(link))
				{
					result = true;
				}
			}
			stopwatch.Stop();
			this.lastLinkOnBodyDetectionTimeMs = stopwatch.ElapsedMilliseconds;
			return result;
		}

		// Token: 0x040029D0 RID: 10704
		public const string GroupEscalationUnsubscribeLinkId = "BD5134C6-8D33-4ABA-A0C4-08581FDF89DB";

		// Token: 0x040029D1 RID: 10705
		public const int FooterLinkScanSize = 4096;

		// Token: 0x040029D2 RID: 10706
		private readonly string groupDisplayName;

		// Token: 0x040029D3 RID: 10707
		private EscalationLinkBuilder linkBuilder;

		// Token: 0x040029D4 RID: 10708
		private CultureInfo cultureInfo;

		// Token: 0x040029D5 RID: 10709
		private long lastLinkBuildTimeMs;

		// Token: 0x040029D6 RID: 10710
		private long lastLinkOnBodyDetectionTimeMs;

		// Token: 0x040029D7 RID: 10711
		private long lastLinkInsertOnBodyTimeMs;

		// Token: 0x040029D8 RID: 10712
		private long lastBodySizeBytes;
	}
}
