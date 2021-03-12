using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Security;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.Protocols.DeltaSync;
using Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.DeltaSync;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x0200005C RID: 92
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DeltaSyncAutoProvision : IAutoProvision
	{
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x0001517C File Offset: 0x0001337C
		public string[] Hostnames
		{
			get
			{
				return this.hostnames;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x00015184 File Offset: 0x00013384
		public int[] ConnectivePorts
		{
			get
			{
				return this.connectivePorts;
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0001518C File Offset: 0x0001338C
		public DeltaSyncAutoProvision(SmtpAddress emailAddress, SecureString password)
		{
			if (emailAddress == SmtpAddress.Empty)
			{
				throw new ArgumentNullException("emailAddress");
			}
			this.emailAddress = emailAddress;
			this.password = password;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x000151E0 File Offset: 0x000133E0
		public static bool ValidateUserHotmailAccount(DeltaSyncUserAccount account, SyncLogSession syncLogSession, out DeltaSyncSettings accountSettings, out LocalizedException validationException)
		{
			AsyncOperationResult<DeltaSyncResultData> asyncOperationResult = null;
			validationException = null;
			accountSettings = null;
			using (DeltaSyncClient deltaSyncClient = new DeltaSyncClient(account, 30000, WebRequest.DefaultWebProxy, long.MaxValue, null, syncLogSession, null))
			{
				IAsyncResult asyncResult = deltaSyncClient.BeginVerifyAccount(null, null, null);
				asyncOperationResult = deltaSyncClient.EndVerifyAccount(asyncResult);
			}
			bool flag = false;
			Exception ex = null;
			if (!asyncOperationResult.IsSucceeded)
			{
				ex = asyncOperationResult.Exception;
			}
			else
			{
				DeltaSyncResultData data = asyncOperationResult.Data;
				if (data.IsTopLevelOperationSuccessful)
				{
					flag = true;
					Exception ex2;
					DeltaSyncResultData.TryGetSettings(data.SettingsResponse, out accountSettings, out ex2);
				}
				else
				{
					ex = data.GetStatusException();
				}
			}
			if (!flag)
			{
				if (ex is AuthenticationException || ex is UserAccessException)
				{
					validationException = new LocalizedException(Strings.HotmailAccountVerificationFailedException);
				}
				else
				{
					validationException = new LocalizedException(Strings.RetryLaterException);
				}
			}
			return flag;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x000152B8 File Offset: 0x000134B8
		public static void UpdateSubscriptionSettings(DeltaSyncSettings deltaSyncSettings, ref DeltaSyncAggregationSubscription subscription)
		{
			subscription.MaxNumberOfEmailAdds = deltaSyncSettings.MaxNumberOfEmailAdds;
			subscription.MaxNumberOfFolderAdds = deltaSyncSettings.MaxNumberOfFolderAdds;
			subscription.MaxObjectInSync = deltaSyncSettings.MaxObjectsInSync;
			subscription.MinSettingPollInterval = deltaSyncSettings.MinSettingsPollInterval;
			subscription.MinSyncPollInterval = deltaSyncSettings.MinSyncPollInterval;
			subscription.SyncMultiplier = deltaSyncSettings.SyncMultiplier;
			subscription.MaxAttachments = deltaSyncSettings.MaxAttachments;
			subscription.MaxMessageSize = deltaSyncSettings.MaxMessageSize;
			subscription.MaxRecipients = deltaSyncSettings.MaxRecipients;
			switch (deltaSyncSettings.AccountStatus)
			{
			case AccountStatusType.OK:
				subscription.AccountStatus = DeltaSyncAccountStatus.Normal;
				return;
			case AccountStatusType.Blocked:
				subscription.AccountStatus = DeltaSyncAccountStatus.Blocked;
				return;
			case AccountStatusType.RequiresHIP:
				subscription.AccountStatus = DeltaSyncAccountStatus.HipRequired;
				return;
			default:
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "unknown AccountStatus: {0}", new object[]
				{
					deltaSyncSettings.AccountStatus
				}));
			}
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x000153A4 File Offset: 0x000135A4
		public DiscoverSettingsResult DiscoverSetting(SyncLogSession syncLogSession, bool testOnlyInsecure, Dictionary<Authority, bool> connectiveAuthority, AutoProvisionProgress progressCallback, out PimSubscriptionProxy sub)
		{
			sub = null;
			if (this.password == null)
			{
				throw new InvalidOperationException("Password not set");
			}
			if (testOnlyInsecure)
			{
				return DiscoverSettingsResult.InsecureSettingsNotSupported;
			}
			progressCallback(Strings.AutoProvisionTestHotmail, new LocalizedString(this.emailAddress.ToString()));
			HotmailSubscriptionProxy hotmailSubscriptionProxy = new HotmailSubscriptionProxy();
			DeltaSyncUserAccount deltaSyncUserAccount = DeltaSyncUserAccount.CreateDeltaSyncUserForTrustedPartnerAuthWithPassword(this.emailAddress.ToString(), SyncUtilities.SecureStringToString(this.password));
			DeltaSyncSettings deltaSyncSettings;
			LocalizedException ex;
			if (DeltaSyncAutoProvision.ValidateUserHotmailAccount(deltaSyncUserAccount, syncLogSession.OpenWithContext(hotmailSubscriptionProxy.Subscription.SubscriptionGuid), out deltaSyncSettings, out ex))
			{
				hotmailSubscriptionProxy.SetLiveAccountPuid(deltaSyncUserAccount.AuthToken.PUID);
				if (deltaSyncSettings != null)
				{
					DeltaSyncAggregationSubscription deltaSyncAggregationSubscription = hotmailSubscriptionProxy.Subscription as DeltaSyncAggregationSubscription;
					DeltaSyncAutoProvision.UpdateSubscriptionSettings(deltaSyncSettings, ref deltaSyncAggregationSubscription);
				}
				sub = hotmailSubscriptionProxy;
				return DiscoverSettingsResult.Succeeded;
			}
			return DiscoverSettingsResult.SettingsNotFound;
		}

		// Token: 0x04000220 RID: 544
		internal const int DeltaSyncVerificationTimeout = 30000;

		// Token: 0x04000221 RID: 545
		private readonly SmtpAddress emailAddress;

		// Token: 0x04000222 RID: 546
		private readonly SecureString password;

		// Token: 0x04000223 RID: 547
		private readonly int[] connectivePorts = new int[0];

		// Token: 0x04000224 RID: 548
		private readonly string[] hostnames = new string[0];
	}
}
