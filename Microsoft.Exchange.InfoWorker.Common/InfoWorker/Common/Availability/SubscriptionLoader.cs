using System;
using System.Globalization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000D3 RID: 211
	internal sealed class SubscriptionLoader
	{
		// Token: 0x0600056D RID: 1389 RVA: 0x00017EE6 File Offset: 0x000160E6
		public SubscriptionLoader(ADUser adUser, IBudget requesterBudget)
		{
			this.adUser = adUser;
			this.requesterBudget = requesterBudget;
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x00017EFC File Offset: 0x000160FC
		// (set) Token: 0x0600056F RID: 1391 RVA: 0x00017F04 File Offset: 0x00016104
		public Exception HandledException { get; private set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x00017F0D File Offset: 0x0001610D
		public bool IsValid
		{
			get
			{
				return this.HandledException == null;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x00017F18 File Offset: 0x00016118
		private SharingSubscriptionData[] Subscriptions
		{
			get
			{
				if (this.subscriptions == null)
				{
					this.subscriptions = this.LoadAllSubscriptions();
				}
				return this.subscriptions;
			}
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00017F34 File Offset: 0x00016134
		public SharingSubscriptionData GetUserSubscription(EmailAddress emailAddress)
		{
			foreach (SharingSubscriptionData sharingSubscriptionData in this.Subscriptions)
			{
				if (StringComparer.InvariantCultureIgnoreCase.Equals(sharingSubscriptionData.SharerIdentity, emailAddress.Address))
				{
					return sharingSubscriptionData;
				}
			}
			return null;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00017F7C File Offset: 0x0001617C
		private SharingSubscriptionData[] LoadAllSubscriptions()
		{
			try
			{
				if (this.requesterBudget != null)
				{
					this.requesterBudget.CheckOverBudget();
				}
				ExchangePrincipal mailboxOwner = ExchangePrincipal.FromADUser(this.adUser, null);
				using (MailboxSession mailboxSession = MailboxSession.OpenAsSystemService(mailboxOwner, CultureInfo.InvariantCulture, "Client=AS"))
				{
					if (this.requesterBudget != null)
					{
						mailboxSession.AccountingObject = this.requesterBudget;
					}
					try
					{
						using (SharingSubscriptionManager sharingSubscriptionManager = new SharingSubscriptionManager(mailboxSession))
						{
							return sharingSubscriptionManager.GetAll();
						}
					}
					catch (ObjectNotFoundException)
					{
					}
				}
			}
			catch (OverBudgetException handledException)
			{
				this.HandledException = handledException;
			}
			catch (ConnectionFailedPermanentException handledException2)
			{
				this.HandledException = handledException2;
			}
			catch (ObjectNotFoundException handledException3)
			{
				this.HandledException = handledException3;
			}
			catch (ConnectionFailedTransientException handledException4)
			{
				this.HandledException = handledException4;
			}
			catch (AccountDisabledException handledException5)
			{
				this.HandledException = handledException5;
			}
			catch (VirusScanInProgressException innerException)
			{
				LocalizedString localizedString = Strings.descVirusScanInProgress(this.adUser.PrimarySmtpAddress.ToString());
				this.HandledException = new LocalizedException(localizedString, innerException);
			}
			catch (VirusDetectedException innerException2)
			{
				LocalizedString localizedString2 = Strings.descVirusDetected(this.adUser.PrimarySmtpAddress.ToString());
				this.HandledException = new LocalizedException(localizedString2, innerException2);
			}
			catch (StoragePermanentException handledException6)
			{
				this.HandledException = handledException6;
			}
			catch (StorageTransientException handledException7)
			{
				this.HandledException = handledException7;
			}
			return new SharingSubscriptionData[0];
		}

		// Token: 0x0400032C RID: 812
		private ADUser adUser;

		// Token: 0x0400032D RID: 813
		private IBudget requesterBudget;

		// Token: 0x0400032E RID: 814
		private SharingSubscriptionData[] subscriptions;
	}
}
