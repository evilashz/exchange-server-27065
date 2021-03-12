using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ContentTypes.Tnef;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001F7 RID: 503
	internal class ResolverMessage
	{
		// Token: 0x06001657 RID: 5719 RVA: 0x0005B3D4 File Offset: 0x000595D4
		public ResolverMessage(EmailMessage message, long mimeSize)
		{
			this.emailMessage = message;
			this.headers = this.emailMessage.MimeDocument.RootPart.Headers;
			this.messageType = new ResolverMessageType?(this.GetMessageType());
			this.originalMessageSize = ResolverMessage.GetOriginalMessageSize(this.headers, mimeSize);
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001658 RID: 5720 RVA: 0x0005B45C File Offset: 0x0005965C
		public ResolverMessageType Type
		{
			get
			{
				return this.messageType.Value;
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001659 RID: 5721 RVA: 0x0005B46C File Offset: 0x0005966C
		public bool AutoForwarded
		{
			get
			{
				bool flag;
				this.emailMessage.TryGetMapiProperty<bool>(TnefPropertyTag.AutoForwarded, out flag);
				if (flag)
				{
					return true;
				}
				bool result = false;
				Header header = this.headers.FindFirst("X-MS-Exchange-Organization-AutoForwarded");
				if (header != null)
				{
					bool.TryParse(header.Value, out result);
				}
				return result;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x0600165A RID: 5722 RVA: 0x0005B4B6 File Offset: 0x000596B6
		// (set) Token: 0x0600165B RID: 5723 RVA: 0x0005B4E4 File Offset: 0x000596E4
		public AutoResponseSuppress AutoResponseSuppress
		{
			get
			{
				if (this.suppress == null)
				{
					this.suppress = new AutoResponseSuppress?(this.GetAutoResponseSuppress());
				}
				return this.suppress.Value;
			}
			set
			{
				if ((value & AutoResponseSuppress.RN) != (AutoResponseSuppress)0)
				{
					this.headers.RemoveAll(HeaderId.DispositionNotificationTo);
					this.headers.RemoveAll(HeaderId.ReturnReceiptTo);
				}
				if (this.suppress != value)
				{
					this.SetAutoResponseSuppress(value);
					this.suppress = new AutoResponseSuppress?(value);
				}
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x0600165C RID: 5724 RVA: 0x0005B545 File Offset: 0x00059745
		// (set) Token: 0x0600165D RID: 5725 RVA: 0x0005B560 File Offset: 0x00059760
		public bool RecipientLimitVerified
		{
			get
			{
				return this.headers.FindFirst("X-MS-Exchange-Organization-Recipient-Limit-Verified") != null;
			}
			set
			{
				this.headers.RemoveAll("X-MS-Exchange-Organization-Recipient-Limit-Verified");
				if (value)
				{
					Header newChild = new AsciiTextHeader("X-MS-Exchange-Organization-Recipient-Limit-Verified", "True");
					this.headers.AppendChild(newChild);
				}
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x0600165E RID: 5726 RVA: 0x0005B59D File Offset: 0x0005979D
		public History History
		{
			get
			{
				if (!this.loadedHistory)
				{
					this.history = History.ReadFrom(this.headers);
					this.loadedHistory = true;
				}
				return this.history;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x0600165F RID: 5727 RVA: 0x0005B5C8 File Offset: 0x000597C8
		// (set) Token: 0x06001660 RID: 5728 RVA: 0x0005B608 File Offset: 0x00059808
		public bool RedirectHandled
		{
			get
			{
				if (!this.loadedRedirectHandled)
				{
					Header header = this.headers.FindFirst("X-MS-Exchange-Organization-Recipient-Redirection-Handled");
					this.redirectHandled = (null != header);
					this.loadedRedirectHandled = true;
				}
				return this.redirectHandled;
			}
			set
			{
				this.headers.RemoveAll("X-MS-Exchange-Organization-Recipient-Redirection-Handled");
				if (value)
				{
					Header newChild = new AsciiTextHeader("X-MS-Exchange-Organization-Recipient-Redirection-Handled", "True");
					this.headers.AppendChild(newChild);
				}
				this.redirectHandled = value;
				this.loadedRedirectHandled = true;
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001661 RID: 5729 RVA: 0x0005B654 File Offset: 0x00059854
		// (set) Token: 0x06001662 RID: 5730 RVA: 0x0005B68B File Offset: 0x0005988B
		public bool SuppressRecallReport
		{
			get
			{
				Header header = this.headers.FindFirst("X-MS-Exchange-Send-Outlook-Recall-Report");
				return header != null && header.Value.Equals("False", StringComparison.OrdinalIgnoreCase);
			}
			set
			{
				ResolverMessage.SetSuppressRecallReport(this.headers, value);
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001663 RID: 5731 RVA: 0x0005B69C File Offset: 0x0005989C
		// (set) Token: 0x06001664 RID: 5732 RVA: 0x0005B6D0 File Offset: 0x000598D0
		public bool BypassChildModeration
		{
			get
			{
				Header header = this.headers.FindFirst("X-MS-Exchange-Organization-Bypass-Child-Moderation");
				return header != null && header.Value.Equals("True", StringComparison.OrdinalIgnoreCase);
			}
			set
			{
				this.headers.RemoveAll("X-MS-Exchange-Organization-Bypass-Child-Moderation");
				if (value)
				{
					Header newChild = new AsciiTextHeader("X-MS-Exchange-Organization-Bypass-Child-Moderation", "True");
					this.headers.AppendChild(newChild);
				}
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001665 RID: 5733 RVA: 0x0005B70D File Offset: 0x0005990D
		public long OriginalMessageSize
		{
			get
			{
				return this.originalMessageSize;
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001666 RID: 5734 RVA: 0x0005B718 File Offset: 0x00059918
		internal bool DlExpansionProhibited
		{
			get
			{
				if (this.dlExpansionProhibited == null)
				{
					Header header = this.headers.FindFirst("X-MS-Exchange-Organization-DL-Expansion-Prohibited");
					this.dlExpansionProhibited = new bool?(header != null);
				}
				return this.dlExpansionProhibited.Value;
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001667 RID: 5735 RVA: 0x0005B760 File Offset: 0x00059960
		internal bool AltRecipientProhibited
		{
			get
			{
				if (this.altRecipientProhibited == null)
				{
					Header header = this.headers.FindFirst("X-MS-Exchange-Organization-Alt-Recipient-Prohibited");
					this.altRecipientProhibited = new bool?(header != null);
				}
				return this.altRecipientProhibited.Value;
			}
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x0005B7A8 File Offset: 0x000599A8
		public static long GetOriginalMessageSize(HeaderList headers, long mimeSize)
		{
			Header header = headers.FindFirst("X-MS-Exchange-Organization-OriginalSize");
			if (header != null)
			{
				long num = 0L;
				if (long.TryParse(header.Value, NumberStyles.Any, NumberFormatInfo.InvariantInfo, out num) && num >= 0L)
				{
					return num;
				}
				ExTraceGlobals.ResolverTracer.TraceError<Header>(0L, "Failed to parse original-message-size header '{0}'; deleting it", header);
				headers.RemoveAll("X-MS-Exchange-Organization-OriginalSize");
			}
			headers.AppendChild(new AsciiTextHeader("X-MS-Exchange-Organization-OriginalSize", mimeSize.ToString(NumberFormatInfo.InvariantInfo)));
			ExTraceGlobals.ResolverTracer.TraceDebug<long>(0L, "Stamped original-message-size header with value {0}", mimeSize);
			return mimeSize;
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x0005B834 File Offset: 0x00059A34
		public static string FormatAutoResponseSuppressHeaderValue(AutoResponseSuppress suppress)
		{
			return ResolverMessage.autoResponseSuppressFormatter.Format(suppress);
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x0005B844 File Offset: 0x00059A44
		public static void SetSuppressRecallReport(HeaderList headers, bool suppress)
		{
			Header header = headers.FindFirst("X-MS-Exchange-Send-Outlook-Recall-Report");
			if (!suppress)
			{
				if (header != null)
				{
					headers.RemoveAll("X-MS-Exchange-Send-Outlook-Recall-Report");
				}
				return;
			}
			if (header != null)
			{
				header.Value = "False";
				return;
			}
			headers.AppendChild(new AsciiTextHeader("X-MS-Exchange-Send-Outlook-Recall-Report", "False"));
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x0005B894 File Offset: 0x00059A94
		public void AddResentFrom(string resentFrom)
		{
			Header newChild = AddressHeader.Parse("Resent-From", resentFrom, AddressParserFlags.None);
			this.headers.PrependChild(newChild);
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x0005B8BB File Offset: 0x00059ABB
		public void ClearHistory(TransportMailItem transportMailItem)
		{
			History.Clear(transportMailItem);
			this.history = null;
			this.loadedHistory = true;
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x0005B8D4 File Offset: 0x00059AD4
		private static AutoResponseSuppress ParseAutoResponseSuppressHeaderValue(string value)
		{
			AutoResponseSuppress autoResponseSuppress = (AutoResponseSuppress)0;
			AutoResponseSuppress result = (AutoResponseSuppress)0;
			if (EnumValidator<AutoResponseSuppress>.TryParse(value, EnumParseOptions.IgnoreCase, out autoResponseSuppress))
			{
				result = autoResponseSuppress;
			}
			return result;
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x0005B8F4 File Offset: 0x00059AF4
		private ResolverMessageType GetMessageType()
		{
			string text = this.emailMessage.MapiMessageClass;
			ExTraceGlobals.ResolverTracer.TraceDebug<string>((long)this.GetHashCode(), "GetMessageType:Class = {0}", text);
			if (text == null)
			{
				return ResolverMessageType.Note;
			}
			if (text.StartsWith("envelope.", StringComparison.OrdinalIgnoreCase))
			{
				text = text.Substring("envelope.".Length);
			}
			if (text.StartsWith("IPM.Note.Rules.OofTemplate.", StringComparison.OrdinalIgnoreCase))
			{
				int num;
				if (!this.emailMessage.TryGetMapiProperty<int>(TnefPropertyTag.OofReplyType, out num))
				{
					num = 2;
				}
				if (num == 2)
				{
					return ResolverMessageType.InternalOOF;
				}
				return ResolverMessageType.LegacyOOF;
			}
			else
			{
				if (text.StartsWith("IPM.Note.Rules.ExternalOofTemplate.", StringComparison.OrdinalIgnoreCase))
				{
					return ResolverMessageType.ExternalOOF;
				}
				if (text.StartsWith("IPM.Note.Rules.ReplyTemplate.", StringComparison.OrdinalIgnoreCase))
				{
					return ResolverMessageType.AutoReply;
				}
				if (ObjectClass.IsOutlookRecall(text))
				{
					return ResolverMessageType.Recall;
				}
				if (text.StartsWith("IPM.Recall.Report.", StringComparison.OrdinalIgnoreCase))
				{
					return ResolverMessageType.RecallReport;
				}
				if (ObjectClass.IsMeetingForwardNotification(text))
				{
					return ResolverMessageType.MeetingForwardNotification;
				}
				if (ObjectClass.IsUMMessage(text))
				{
					return ResolverMessageType.UM;
				}
				if (!text.StartsWith("report.", StringComparison.OrdinalIgnoreCase))
				{
					return ResolverMessageType.Note;
				}
				if (text.EndsWith(".ipnrn", StringComparison.OrdinalIgnoreCase))
				{
					return ResolverMessageType.RN;
				}
				if (text.EndsWith(".ipnnrn", StringComparison.OrdinalIgnoreCase))
				{
					return ResolverMessageType.NRN;
				}
				if (text.EndsWith("delayed.dr", StringComparison.OrdinalIgnoreCase))
				{
					return ResolverMessageType.DelayedDSN;
				}
				if (text.EndsWith("relayed.dr", StringComparison.OrdinalIgnoreCase))
				{
					return ResolverMessageType.RelayedDSN;
				}
				if (text.EndsWith("expanded.dr", StringComparison.OrdinalIgnoreCase))
				{
					return ResolverMessageType.ExpandedDSN;
				}
				if (text.EndsWith(".dr", StringComparison.OrdinalIgnoreCase))
				{
					return ResolverMessageType.DR;
				}
				if (text.EndsWith(".ndr", StringComparison.OrdinalIgnoreCase))
				{
					return ResolverMessageType.NDR;
				}
				return ResolverMessageType.Note;
			}
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x0005BA4C File Offset: 0x00059C4C
		private AutoResponseSuppress GetAutoResponseSuppress()
		{
			AutoResponseSuppress autoResponseSuppress = (AutoResponseSuppress)0;
			foreach (Header header in this.headers)
			{
				try
				{
					if (string.Equals(header.Name, "X-Auto-Response-Suppress", StringComparison.OrdinalIgnoreCase))
					{
						autoResponseSuppress |= ResolverMessage.ParseAutoResponseSuppressHeaderValue(header.Value);
					}
				}
				catch (ExchangeDataException)
				{
				}
			}
			return autoResponseSuppress;
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x0005BAD0 File Offset: 0x00059CD0
		private void SetAutoResponseSuppress(AutoResponseSuppress suppress)
		{
			Header[] array = this.headers.FindAll("X-Auto-Response-Suppress");
			if (array.Length == 0)
			{
				Header newChild = new AsciiTextHeader("X-Auto-Response-Suppress", ResolverMessage.FormatAutoResponseSuppressHeaderValue(suppress));
				this.headers.AppendChild(newChild);
				return;
			}
			array[0].Value = ResolverMessage.FormatAutoResponseSuppressHeaderValue(suppress);
			for (int i = 1; i < array.Length; i++)
			{
				this.headers.RemoveChild(array[i]);
			}
		}

		// Token: 0x04000B29 RID: 2857
		private const string AutoResponseSuppressHeaderName = "X-Auto-Response-Suppress";

		// Token: 0x04000B2A RID: 2858
		private const string SendRecallReportHeaderName = "X-MS-Exchange-Send-Outlook-Recall-Report";

		// Token: 0x04000B2B RID: 2859
		private const string RecipientRedirectionHandledHeaderName = "X-MS-Exchange-Organization-Recipient-Redirection-Handled";

		// Token: 0x04000B2C RID: 2860
		private const string RecipientLimitVerifiedHeaderName = "X-MS-Exchange-Organization-Recipient-Limit-Verified";

		// Token: 0x04000B2D RID: 2861
		private const string BypassChildModerationHeaderName = "X-MS-Exchange-Organization-Bypass-Child-Moderation";

		// Token: 0x04000B2E RID: 2862
		private static AutoResponseSuppressFormatter autoResponseSuppressFormatter = new AutoResponseSuppressFormatter();

		// Token: 0x04000B2F RID: 2863
		private EmailMessage emailMessage;

		// Token: 0x04000B30 RID: 2864
		private HeaderList headers;

		// Token: 0x04000B31 RID: 2865
		private ResolverMessageType? messageType = null;

		// Token: 0x04000B32 RID: 2866
		private AutoResponseSuppress? suppress = null;

		// Token: 0x04000B33 RID: 2867
		private bool? dlExpansionProhibited = null;

		// Token: 0x04000B34 RID: 2868
		private bool? altRecipientProhibited = null;

		// Token: 0x04000B35 RID: 2869
		private History history;

		// Token: 0x04000B36 RID: 2870
		private bool loadedHistory;

		// Token: 0x04000B37 RID: 2871
		private bool redirectHandled;

		// Token: 0x04000B38 RID: 2872
		private bool loadedRedirectHandled;

		// Token: 0x04000B39 RID: 2873
		private long originalMessageSize;
	}
}
