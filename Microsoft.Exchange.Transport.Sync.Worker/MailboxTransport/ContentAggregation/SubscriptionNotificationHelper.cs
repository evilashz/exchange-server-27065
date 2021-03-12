using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using Microsoft.Exchange.Core;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000210 RID: 528
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SubscriptionNotificationHelper
	{
		// Token: 0x0600121D RID: 4637 RVA: 0x0003B28E File Offset: 0x0003948E
		protected SubscriptionNotificationHelper()
		{
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x0600121E RID: 4638 RVA: 0x0003B296 File Offset: 0x00039496
		// (set) Token: 0x0600121F RID: 4639 RVA: 0x0003B29D File Offset: 0x0003949D
		public static SubscriptionNotificationHelper Instance
		{
			get
			{
				return SubscriptionNotificationHelper.instance;
			}
			set
			{
				SubscriptionNotificationHelper.instance = value;
			}
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x0003B2A8 File Offset: 0x000394A8
		public virtual bool ShouldGenerateSubscriptionNotification(AggregationStatus initialStatus, ISyncWorkerData subscription, Exception syncException, SyncLogSession syncLogSession)
		{
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			SyncUtilities.ThrowIfArgumentNull("syncException", syncException);
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			if (!AggregationConfiguration.Instance.SubscriptionNotificationEnabled)
			{
				return false;
			}
			if (subscription.AggregationType != AggregationType.Aggregation)
			{
				return false;
			}
			if (initialStatus != AggregationStatus.Succeeded && initialStatus != AggregationStatus.InProgress)
			{
				return false;
			}
			AggregationStatus status = subscription.Status;
			if (status != AggregationStatus.Delayed && status != AggregationStatus.Disabled && status != AggregationStatus.Poisonous)
			{
				return false;
			}
			SyncTransientException ex = syncException as SyncTransientException;
			SyncPermanentException ex2 = syncException as SyncPermanentException;
			DetailedAggregationStatus detailedAggregationStatus;
			if (ex != null)
			{
				detailedAggregationStatus = ex.DetailedAggregationStatus;
			}
			else
			{
				detailedAggregationStatus = ex2.DetailedAggregationStatus;
			}
			if (subscription.DetailedAggregationStatus != detailedAggregationStatus)
			{
				return false;
			}
			if (subscription.Status != AggregationStatus.Poisonous)
			{
				switch (subscription.DetailedAggregationStatus)
				{
				default:
					return false;
				case DetailedAggregationStatus.AuthenticationError:
				case DetailedAggregationStatus.ConnectionError:
				case DetailedAggregationStatus.CommunicationError:
				case DetailedAggregationStatus.LabsMailboxQuotaWarning:
				case DetailedAggregationStatus.MaxedOutSyncRelationshipsError:
				case DetailedAggregationStatus.LeaveOnServerNotSupported:
				case DetailedAggregationStatus.RemoteAccountDoesNotExist:
				case DetailedAggregationStatus.RemoteServerIsSlow:
				case DetailedAggregationStatus.TooManyFolders:
				case DetailedAggregationStatus.RemoteServerIsPoisonous:
				case DetailedAggregationStatus.SyncStateSizeError:
					break;
				}
			}
			return true;
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x0003B3B0 File Offset: 0x000395B0
		public virtual bool TrySendSubscriptionNotificationEmail(MailboxSession userMailboxSession, AggregationSubscription subscription, SyncLogSession syncLogSession, out bool retry)
		{
			if (!AggregationConfiguration.Instance.SubscriptionNotificationEnabled)
			{
				retry = false;
				return false;
			}
			SyncUtilities.ThrowIfArgumentNull("userMailboxSession", userMailboxSession);
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			syncLogSession.LogDebugging((TSLID)979UL, "Trying to send a subscription notification.", new object[0]);
			SyncErrorNotificationEmail syncErrorNotificationEmail;
			if (!this.TryBuildSubscriptionNotificationEmail(userMailboxSession.MailboxOwner, subscription, syncLogSession, out syncErrorNotificationEmail))
			{
				retry = true;
				return false;
			}
			bool result;
			using (syncErrorNotificationEmail)
			{
				try
				{
					using (MessageItem messageItem = this.CreateSubscriptionErrorNotificationMessage(userMailboxSession, subscription, syncLogSession, syncErrorNotificationEmail))
					{
						messageItem.Save(SaveMode.NoConflictResolution);
						syncLogSession.LogVerbose((TSLID)980UL, "Generated subscription notification message.", new object[0]);
						retry = false;
						result = true;
					}
				}
				catch (StorageTransientException ex)
				{
					syncLogSession.LogError((TSLID)981UL, "Failed to generate subscription notification message due to {0}.", new object[]
					{
						ex
					});
					retry = true;
					result = false;
				}
				catch (StoragePermanentException ex2)
				{
					bool flag = ExceptionUtilities.ShouldPermanentMapiOrXsoExceptionBeTreatedAsTransient(syncLogSession, ex2);
					syncLogSession.LogError((TSLID)982UL, "Failed to generate subscription notification message. Treat as transient: {0}. Exception: {1}.", new object[]
					{
						flag,
						ex2
					});
					retry = flag;
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x0003B524 File Offset: 0x00039724
		internal string TestGetConnectionErrorBody(AggregationSubscription subscription, string connectedAccountsDetailsLinkedText)
		{
			return this.GetConnectionErrorBody(subscription, connectedAccountsDetailsLinkedText, PreferredCultureLocalizer.DefaultThreadCulture);
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x0003B534 File Offset: 0x00039734
		private bool TryBuildSubscriptionNotificationEmail(IExchangePrincipal userExchangePrincipal, AggregationSubscription subscription, SyncLogSession syncLogSession, out SyncErrorNotificationEmail subscriptionNotificationEmail)
		{
			subscriptionNotificationEmail = null;
			PreferredCultureLocalizer preferredCultureLocalizer = new PreferredCultureLocalizer(userExchangePrincipal);
			string htmlBodyContent;
			if (!this.TryGetSubscriptionNotificationEmailBodyHtmlContent(userExchangePrincipal, subscription, preferredCultureLocalizer, syncLogSession, out htmlBodyContent))
			{
				return false;
			}
			string defaultDisplayName = ADMicrosoftExchangeRecipient.DefaultDisplayName;
			string fromSmtpAddress;
			if (!EmailGenerationUtilities.TryGetMicrosoftExchangeRecipientSmtpAddress(userExchangePrincipal.MailboxInfo.OrganizationId.ToADSessionSettings(), syncLogSession, out fromSmtpAddress))
			{
				syncLogSession.LogError((TSLID)983UL, "Failed to get Microsoft Exchange Recipient SMTP Address.", new object[0]);
				return false;
			}
			PimAggregationSubscription pimAggregationSubscription = (PimAggregationSubscription)subscription;
			string userAddressInRfc822SmtpFormat = pimAggregationSubscription.GetUserAddressInRfc822SmtpFormat();
			string subscriptionEmailAddress = subscription.Email.ToString();
			string text = preferredCultureLocalizer.Apply(Strings.SubscriptionNotificationEmailSubject(subscriptionEmailAddress));
			text = EmailGenerationUtilities.SanitizeSubject(text);
			Stream mimeStream = EmailGenerationUtilities.GenerateEmailMimeStream(Guid.NewGuid().ToString(), defaultDisplayName, fromSmtpAddress, userAddressInRfc822SmtpFormat, text, htmlBodyContent, syncLogSession);
			subscriptionNotificationEmail = new SyncErrorNotificationEmail(ExDateTime.UtcNow, mimeStream);
			return true;
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x0003B610 File Offset: 0x00039810
		private bool TryGetSubscriptionNotificationEmailBodyHtmlContent(IExchangePrincipal userExchangePrincipal, AggregationSubscription subscription, PreferredCultureLocalizer localizer, SyncLogSession syncLogSession, out string bodyHtmlContent)
		{
			bodyHtmlContent = null;
			string value;
			if (!this.TryGetSubscriptionNotificationErrorDetails(userExchangePrincipal, subscription, localizer, syncLogSession, out value))
			{
				return false;
			}
			string value2 = subscription.Email.ToString();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("<html>");
			stringBuilder.AppendLine("<body>");
			stringBuilder.AppendLine("<p><b>");
			stringBuilder.AppendLine(SystemMessages.BodyHeaderFontTag);
			stringBuilder.AppendLine(HttpUtility.HtmlEncode(localizer.Apply(Strings.SubscriptionNotificationEmailBodyStartText)));
			stringBuilder.AppendLine("</font>");
			stringBuilder.AppendLine("</b></p>");
			stringBuilder.AppendLine(SystemMessages.BodyBlockFontTag);
			stringBuilder.AppendLine("<p>");
			stringBuilder.AppendLine(value2);
			stringBuilder.AppendLine("</p>");
			stringBuilder.AppendLine("<p>");
			stringBuilder.AppendLine(value);
			stringBuilder.AppendLine("</p>");
			stringBuilder.AppendLine("</font>");
			stringBuilder.AppendLine("</body>");
			stringBuilder.AppendLine("</html>");
			bodyHtmlContent = stringBuilder.ToString();
			return true;
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x0003B730 File Offset: 0x00039930
		private bool TryGetSubscriptionNotificationErrorDetails(IExchangePrincipal userExchangePrincipal, AggregationSubscription subscription, PreferredCultureLocalizer localizer, SyncLogSession syncLogSession, out string errorDetails)
		{
			errorDetails = null;
			string text;
			if (!SyncUtilities.TryGetConnectedAccountsDetailsUrl(userExchangePrincipal, subscription, syncLogSession, out text))
			{
				syncLogSession.LogError((TSLID)984UL, "Failed to get 'Connected Accounts Details' link.", new object[0]);
				return false;
			}
			string connectedAccountsDetailsLinkedText = string.Format(CultureInfo.InvariantCulture, "<a href=\"{0}\" target=\"_blank\">{1}</a>", new object[]
			{
				text,
				HttpUtility.HtmlEncode(localizer.Apply(Strings.ConnectedAccountsDetails))
			});
			switch (subscription.Status)
			{
			case AggregationStatus.Delayed:
			case AggregationStatus.Disabled:
				switch (subscription.DetailedAggregationStatus)
				{
				case DetailedAggregationStatus.AuthenticationError:
					errorDetails = localizer.Apply(Strings.AuthenticationErrorBody(connectedAccountsDetailsLinkedText));
					break;
				case DetailedAggregationStatus.ConnectionError:
					errorDetails = this.GetConnectionErrorBody(subscription, connectedAccountsDetailsLinkedText, localizer);
					break;
				case DetailedAggregationStatus.CommunicationError:
					errorDetails = localizer.Apply(Strings.CommunicationErrorBody(connectedAccountsDetailsLinkedText));
					break;
				case DetailedAggregationStatus.LabsMailboxQuotaWarning:
					errorDetails = localizer.Apply(Strings.LabsMailboxQuoteWarningBody(connectedAccountsDetailsLinkedText));
					break;
				case DetailedAggregationStatus.MaxedOutSyncRelationshipsError:
					errorDetails = localizer.Apply(Strings.MaxedOutSyncRelationshipsErrorBody);
					break;
				case DetailedAggregationStatus.LeaveOnServerNotSupported:
					errorDetails = localizer.Apply(Strings.LeaveOnServerNotSupportedErrorBody(connectedAccountsDetailsLinkedText));
					break;
				case DetailedAggregationStatus.RemoteAccountDoesNotExist:
					errorDetails = localizer.Apply(Strings.RemoteAccountDoesNotExistBody(connectedAccountsDetailsLinkedText));
					break;
				case DetailedAggregationStatus.RemoteServerIsSlow:
				case DetailedAggregationStatus.RemoteServerIsPoisonous:
					errorDetails = localizer.Apply(Strings.RemoteServerIsSlowErrorBody(connectedAccountsDetailsLinkedText));
					break;
				case DetailedAggregationStatus.TooManyFolders:
					errorDetails = localizer.Apply(Strings.TooManyFoldersErrorBody(FrameworkAggregationConfiguration.Instance.ImapMaxFoldersSupported, connectedAccountsDetailsLinkedText));
					break;
				case DetailedAggregationStatus.SyncStateSizeError:
					errorDetails = localizer.Apply(Strings.SyncStateSizeErrorBody(connectedAccountsDetailsLinkedText));
					break;
				}
				break;
			case AggregationStatus.Poisonous:
				errorDetails = localizer.Apply(Strings.PoisonousErrorBody);
				break;
			}
			return true;
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x0003B8D8 File Offset: 0x00039AD8
		private MessageItem CreateSubscriptionErrorNotificationMessage(MailboxSession userMailboxSession, AggregationSubscription subscription, SyncLogSession syncLogSession, SyncErrorNotificationEmail subscriptionNotificationEmail)
		{
			StoreObjectId defaultFolderId = userMailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox);
			return subscriptionNotificationEmail.CreateSyncErrorNotificationMessage(userMailboxSession, defaultFolderId, subscription, syncLogSession);
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x0003B8FC File Offset: 0x00039AFC
		private string GetConnectionErrorBody(AggregationSubscription subscription, string connectedAccountsDetailsLinkedText, PreferredCultureLocalizer localizer)
		{
			int num;
			int num2;
			SyncUtilities.GetHoursAndDaysWithoutSuccessfulSync(subscription, true, out num, out num2);
			return SyncUtilities.SelectTimeBasedString(num, num2, localizer.Apply(Strings.ConnectionErrorBodyDay(num, connectedAccountsDetailsLinkedText)), localizer.Apply(Strings.ConnectionErrorBodyDays(num, connectedAccountsDetailsLinkedText)), localizer.Apply(Strings.ConnectionErrorBodyHour(num2, connectedAccountsDetailsLinkedText)), localizer.Apply(Strings.ConnectionErrorBodyHours(num2, connectedAccountsDetailsLinkedText)), localizer.Apply(Strings.ConnectionErrorBody(connectedAccountsDetailsLinkedText)));
		}

		// Token: 0x040009BD RID: 2493
		private static SubscriptionNotificationHelper instance = new SubscriptionNotificationHelper();
	}
}
