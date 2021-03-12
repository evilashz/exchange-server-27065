using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security.AntiXss;
using Microsoft.Exchange.Core;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Transport.Sync.Common.SendAsVerification
{
	// Token: 0x020000A9 RID: 169
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SendAsVerificationEmail : DisposeTrackableBase
	{
		// Token: 0x06000425 RID: 1061 RVA: 0x00016BF0 File Offset: 0x00014DF0
		internal SendAsVerificationEmail(ExchangePrincipal subscriptionExchangePrincipal, string senderAddress, PimAggregationSubscription subscription, Guid sharedSecret, SyncLogSession syncLogSession)
		{
			SyncUtilities.ThrowIfArgumentNull("subscriptionExchangePrincipal", subscriptionExchangePrincipal);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("senderAddress", senderAddress);
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			if (!subscription.SendAsNeedsVerification)
			{
				throw new ArgumentException("subscription is not SendAs verified.  Type: " + subscription.SubscriptionType.ToString(), "subscription");
			}
			SyncUtilities.ThrowIfGuidEmpty("sharedSecret", sharedSecret);
			this.messageId = SendAsVerificationEmail.GenerateMessageId();
			this.subscriptionAddress = subscription.UserEmailAddress.ToString();
			string emailSubject = Strings.SendAsVerificationSubject;
			this.syncLogSession = syncLogSession;
			string htmlBodyContent = this.MakeHtmlContent(subscriptionExchangePrincipal, this.subscriptionAddress, subscription.SubscriptionType, subscription.SubscriptionGuid, sharedSecret);
			this.messageData = EmailGenerationUtilities.GenerateEmailMimeStream(this.messageId, Strings.SendAsVerificationSender, senderAddress, this.subscriptionAddress, emailSubject, htmlBodyContent, this.syncLogSession);
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x00016CE7 File Offset: 0x00014EE7
		internal string MessageId
		{
			get
			{
				base.CheckDisposed();
				return this.messageId;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x00016CF5 File Offset: 0x00014EF5
		internal string SubscriptionAddress
		{
			get
			{
				base.CheckDisposed();
				return this.subscriptionAddress;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x00016D03 File Offset: 0x00014F03
		internal MemoryStream MessageData
		{
			get
			{
				base.CheckDisposed();
				return this.messageData;
			}
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00016D11 File Offset: 0x00014F11
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.messageData.Close();
				this.messageData = null;
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00016D28 File Offset: 0x00014F28
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SendAsVerificationEmail>(this);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00016D30 File Offset: 0x00014F30
		private static string GenerateMessageId()
		{
			return SyncUtilities.GenerateMessageId(string.Format(CultureInfo.InvariantCulture, "{0}-SAV", new object[]
			{
				Guid.NewGuid().ToString()
			}));
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00016D74 File Offset: 0x00014F74
		private string GenerateVerificationURLFor(ExchangePrincipal subscriptionExchangePrincipal, AggregationSubscriptionType subscriptionType, Guid subscriptionGuid, Guid sharedSecret)
		{
			SendAsVerificationUrlGenerator sendAsVerificationUrlGenerator = new SendAsVerificationUrlGenerator();
			return sendAsVerificationUrlGenerator.GenerateURLFor(subscriptionExchangePrincipal, subscriptionType, subscriptionGuid, sharedSecret, this.syncLogSession);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00016D9C File Offset: 0x00014F9C
		private string MakeHtmlContent(ExchangePrincipal subscriptionExchangePrincipal, string subscriptionAddress, AggregationSubscriptionType subscriptionType, Guid subscriptionGuid, Guid sharedSecret)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<html>").AppendLine();
			stringBuilder.Append("<body>").AppendLine();
			stringBuilder.Append(SystemMessages.BodyBlockFontTag);
			stringBuilder.Append("<p>");
			stringBuilder.Append(AntiXssEncoder.HtmlEncode(Strings.SendAsVerificationSalutation(subscriptionExchangePrincipal.MailboxInfo.DisplayName), false));
			stringBuilder.Append("</p>").AppendLine().AppendLine();
			string text = string.Format(CultureInfo.InvariantCulture, "<a href=\"mailto:{0}\">{0}</a>", new object[]
			{
				HttpUtility.HtmlEncode(subscriptionAddress)
			});
			stringBuilder.Append("<p>");
			stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, AntiXssEncoder.HtmlEncode(Strings.SendAsVerificationTopBlock, false), new object[]
			{
				text
			}));
			stringBuilder.Append("</p>").AppendLine().AppendLine();
			string text2 = this.GenerateVerificationURLFor(subscriptionExchangePrincipal, subscriptionType, subscriptionGuid, sharedSecret);
			stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "<p><a href=\"{0}\">{0}</a></p>", new object[]
			{
				text2
			})).AppendLine().AppendLine();
			stringBuilder.Append("<p>");
			stringBuilder.Append(AntiXssEncoder.HtmlEncode(Strings.SendAsVerificationBottomBlock, false));
			stringBuilder.Append("</p>").AppendLine().AppendLine();
			stringBuilder.Append("<p>");
			stringBuilder.Append(AntiXssEncoder.HtmlEncode(Strings.SendAsVerificationSignatureTopPart, false));
			stringBuilder.Append("<br>").AppendLine();
			stringBuilder.Append(AntiXssEncoder.HtmlEncode(Strings.SendAsVerificationSender, false));
			stringBuilder.Append("</p>");
			stringBuilder.Append("</font>").AppendLine();
			stringBuilder.Append("</body>").AppendLine();
			stringBuilder.Append("</html>");
			return stringBuilder.ToString();
		}

		// Token: 0x04000287 RID: 647
		private static readonly Trace Tracer = ExTraceGlobals.SendAsTracer;

		// Token: 0x04000288 RID: 648
		private string messageId;

		// Token: 0x04000289 RID: 649
		private string subscriptionAddress;

		// Token: 0x0400028A RID: 650
		private MemoryStream messageData;

		// Token: 0x0400028B RID: 651
		private SyncLogSession syncLogSession;
	}
}
