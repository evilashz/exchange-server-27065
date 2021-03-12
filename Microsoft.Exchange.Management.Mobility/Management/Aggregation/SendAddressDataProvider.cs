using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.SendAsDefaults;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x02000033 RID: 51
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SendAddressDataProvider : IConfigDataProvider
	{
		// Token: 0x060001E0 RID: 480 RVA: 0x00009F4D File Offset: 0x0000814D
		public SendAddressDataProvider(ExchangePrincipal userPrincipal, string mailboxIdParameterString)
		{
			SyncUtilities.ThrowIfArgumentNull("userPrincipal", userPrincipal);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("mailboxIdParameterString", mailboxIdParameterString);
			this.userPrincipal = userPrincipal;
			this.mailboxIdParameterString = mailboxIdParameterString;
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00009F84 File Offset: 0x00008184
		public string Source
		{
			get
			{
				return "SendAddressDataProvider";
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00009F8C File Offset: 0x0000818C
		public IConfigurable[] Find<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy) where T : IConfigurable, new()
		{
			List<SendAddress> allSendAddresses = this.sendAsDefaultsManager.GetAllSendAddresses(this.mailboxIdParameterString, this.userPrincipal.MailboxInfo.PrimarySmtpAddress.ToString(), this.GetAllSendAsSubscriptions());
			return allSendAddresses.ToArray();
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00009FD8 File Offset: 0x000081D8
		public IEnumerable<T> FindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize) where T : IConfigurable, new()
		{
			List<SendAddress> allSendAddresses = this.sendAsDefaultsManager.GetAllSendAddresses(this.mailboxIdParameterString, this.userPrincipal.MailboxInfo.PrimarySmtpAddress.ToString(), this.GetAllSendAsSubscriptions());
			List<T> list = new List<T>(allSendAddresses.Count);
			foreach (IConfigurable configurable in allSendAddresses)
			{
				list.Add((T)((object)configurable));
			}
			return list;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000A070 File Offset: 0x00008270
		public IConfigurable Read<T>(ObjectId identity) where T : IConfigurable, new()
		{
			SendAddressIdentity sendAddressIdentity = (SendAddressIdentity)identity;
			return this.sendAsDefaultsManager.LookUpSendAddress(sendAddressIdentity.AddressId, this.mailboxIdParameterString, this.userPrincipal.MailboxInfo.PrimarySmtpAddress.ToString(), this.GetAllSendAsSubscriptions());
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000A0BF File Offset: 0x000082BF
		public void Delete(IConfigurable instance)
		{
			throw new NotSupportedException("Delete: SendAddress");
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000A0CB File Offset: 0x000082CB
		public void Save(IConfigurable instance)
		{
			throw new NotSupportedException("Save: SendAddress");
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000A0D8 File Offset: 0x000082D8
		private List<PimAggregationSubscription> GetAllSendAsSubscriptions()
		{
			List<PimAggregationSubscription> allSendAsSubscriptions;
			using (MailboxSession mailboxSession = SubscriptionManager.OpenMailbox(this.userPrincipal, ExchangeMailboxOpenType.AsAdministrator, SendAddressDataProvider.ClientInfoString))
			{
				allSendAsSubscriptions = SubscriptionManager.GetAllSendAsSubscriptions(mailboxSession, false);
			}
			return allSendAsSubscriptions;
		}

		// Token: 0x04000091 RID: 145
		private static readonly string ClientInfoString = "Client=TransportSync;Action=SendAddress";

		// Token: 0x04000092 RID: 146
		private readonly ExchangePrincipal userPrincipal;

		// Token: 0x04000093 RID: 147
		private readonly string mailboxIdParameterString;

		// Token: 0x04000094 RID: 148
		private SendAsDefaultsManager sendAsDefaultsManager = new SendAsDefaultsManager();
	}
}
