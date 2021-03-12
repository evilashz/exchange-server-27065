using System;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.SendAsVerification;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x02000CCD RID: 3277
	internal static class AggregationTaskUtils
	{
		// Token: 0x06007E4D RID: 32333 RVA: 0x0020436C File Offset: 0x0020256C
		internal static void ValidateEmailAddress(IConfigDataProvider dataProvider, PimSubscriptionProxy subscriptionProxy, Task.TaskErrorLoggingDelegate taskErrorLoggingDelegate)
		{
			if (subscriptionProxy.AggregationType != AggregationType.Migration)
			{
				ADUser aduser = ((AggregationSubscriptionDataProvider)dataProvider).ADUser;
				string text = subscriptionProxy.EmailAddress.ToString();
				if (AggregationTaskUtils.IsUserProxyAddress(aduser, text))
				{
					taskErrorLoggingDelegate(new LocalizedException(Strings.SubscriptionInvalidEmailAddress(text)), ErrorCategory.InvalidArgument, null);
				}
			}
		}

		// Token: 0x06007E4E RID: 32334 RVA: 0x002043C0 File Offset: 0x002025C0
		internal static IRecipientSession VerifyIsWithinWriteScopes(IRecipientSession recipientSession, ADUser adUser, Task.TaskErrorLoggingDelegate taskErrorLoggingDelegate)
		{
			SyncUtilities.ThrowIfArgumentNull("recipientSession", recipientSession);
			SyncUtilities.ThrowIfArgumentNull("adUser", adUser);
			SyncUtilities.ThrowIfArgumentNull("taskErrorLoggingDelegate", taskErrorLoggingDelegate);
			IRecipientSession recipientSession2 = (IRecipientSession)TaskHelper.UnderscopeSessionToOrganization(recipientSession, adUser.OrganizationId, true);
			ADScopeException ex;
			if (!recipientSession2.TryVerifyIsWithinScopes(adUser, true, out ex))
			{
				taskErrorLoggingDelegate(new InvalidOperationException(Strings.ErrorCannotChangeMailboxOutOfWriteScope(adUser.Identity.ToString(), (ex == null) ? string.Empty : ex.Message), ex), ErrorCategory.InvalidOperation, adUser.Identity);
			}
			return recipientSession2;
		}

		// Token: 0x06007E4F RID: 32335 RVA: 0x00204446 File Offset: 0x00202646
		internal static void ValidateUserName(string userName, Task.TaskErrorLoggingDelegate taskErrorLoggingDelegate)
		{
			if (string.IsNullOrEmpty(userName))
			{
				taskErrorLoggingDelegate(new InvalidOperationException(Strings.IncomingUserNameEmpty), ErrorCategory.InvalidData, null);
			}
		}

		// Token: 0x06007E50 RID: 32336 RVA: 0x00204468 File Offset: 0x00202668
		internal static void ValidateUnicodeInfoOnUserNameAndPassword(string userName, SecureString password, Task.TaskErrorLoggingDelegate taskErrorLoggingDelegate)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("userName", userName);
			SyncUtilities.ThrowIfArgumentNull("password", password);
			SyncUtilities.ThrowIfArgumentNull("taskErrorLoggingDelegate", taskErrorLoggingDelegate);
			if (SyncUtilities.HasUnicodeCharacters(userName) || (password.Length > 0 && SyncUtilities.HasUnicodeCharacters(password)))
			{
				taskErrorLoggingDelegate(new InvalidOperationException(Strings.InvalidUnicodeCharacterUsage), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x06007E51 RID: 32337 RVA: 0x002044C6 File Offset: 0x002026C6
		internal static void ValidateIncomingServerLength(string incomingServer, Task.TaskErrorLoggingDelegate taskErrorLoggingDelegate)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("incomingServer", incomingServer);
			SyncUtilities.ThrowIfArgumentNull("taskErrorLoggingDelegate", taskErrorLoggingDelegate);
			if (incomingServer.Length > SyncUtilities.MaximumFqdnLength)
			{
				taskErrorLoggingDelegate(new IncomingServerTooLongException(), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x06007E52 RID: 32338 RVA: 0x002044F8 File Offset: 0x002026F8
		internal static void ProcessSendAsSpecificParameters(PimSubscriptionProxy subscriptionProxy, string validateSecret, bool resendVerification, AggregationSubscriptionDataProvider dataProvider, Task.TaskErrorLoggingDelegate taskErrorLoggingDelegate)
		{
			SyncUtilities.ThrowIfArgumentNull("subscriptionProxy", subscriptionProxy);
			SyncUtilities.ThrowIfArgumentNull("dataProvider", dataProvider);
			SyncUtilities.ThrowIfArgumentNull("taskErrorLoggingDelegate", taskErrorLoggingDelegate);
			SendAsManager sendAsManager = new SendAsManager();
			if (validateSecret != null)
			{
				if (sendAsManager.ValidateSharedSecret(subscriptionProxy.Subscription, validateSecret))
				{
					AggregationTaskUtils.EnableAlwaysShowFrom(dataProvider.SubscriptionExchangePrincipal);
				}
				else
				{
					taskErrorLoggingDelegate(new ValidateSecretFailureException(), (ErrorCategory)1003, subscriptionProxy);
				}
			}
			if (resendVerification)
			{
				IEmailSender emailSender = subscriptionProxy.Subscription.CreateEmailSenderFor(dataProvider.ADUser, dataProvider.SubscriptionExchangePrincipal);
				sendAsManager.ResendVerificationEmail(subscriptionProxy.Subscription, emailSender);
			}
		}

		// Token: 0x06007E53 RID: 32339 RVA: 0x00204588 File Offset: 0x00202788
		internal static void EnableAlwaysShowFrom(ExchangePrincipal subscriptionExchangePrincipal)
		{
			using (XsoDictionaryDataProvider xsoDictionaryDataProvider = new XsoDictionaryDataProvider(subscriptionExchangePrincipal, "EnableAlwaysShowFromForSendAsSubscription"))
			{
				MailboxMessageConfiguration mailboxMessageConfiguration = (MailboxMessageConfiguration)xsoDictionaryDataProvider.Read<MailboxMessageConfiguration>(null);
				mailboxMessageConfiguration.AlwaysShowFrom = true;
				xsoDictionaryDataProvider.Save(mailboxMessageConfiguration);
			}
		}

		// Token: 0x06007E54 RID: 32340 RVA: 0x002045D8 File Offset: 0x002027D8
		private static bool IsUserProxyAddress(ADUser user, string email)
		{
			ProxyAddress proxyAddress = ProxyAddress.Parse(email);
			foreach (ProxyAddress other in user.EmailAddresses)
			{
				if (proxyAddress.Equals(other))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04003E25 RID: 15909
		private const string EnableAlwaysShowFromAction = "EnableAlwaysShowFromForSendAsSubscription";

		// Token: 0x04003E26 RID: 15910
		internal static readonly Trace Tracer = ExTraceGlobals.SubscriptionTaskTracer;
	}
}
