using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x02000802 RID: 2050
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class UserMailboxLocator : MailboxLocator
	{
		// Token: 0x06004C7A RID: 19578 RVA: 0x0013CAE9 File Offset: 0x0013ACE9
		public UserMailboxLocator(IRecipientSession adSession, string externalDirectoryObjectId, string legacyDn) : base(adSession, externalDirectoryObjectId, legacyDn)
		{
		}

		// Token: 0x06004C7B RID: 19579 RVA: 0x0013CAF4 File Offset: 0x0013ACF4
		private UserMailboxLocator(IRecipientSession adSession) : base(adSession)
		{
		}

		// Token: 0x170015E9 RID: 5609
		// (get) Token: 0x06004C7C RID: 19580 RVA: 0x0013CAFD File Offset: 0x0013ACFD
		public override string LocatorType
		{
			get
			{
				return UserMailboxLocator.MailboxLocatorType;
			}
		}

		// Token: 0x06004C7D RID: 19581 RVA: 0x0013CB04 File Offset: 0x0013AD04
		public static UserMailboxLocator Instantiate(IRecipientSession adSession, ProxyAddress proxyAddress)
		{
			UserMailboxLocator userMailboxLocator = new UserMailboxLocator(adSession);
			userMailboxLocator.InitializeFromAd(proxyAddress);
			return userMailboxLocator;
		}

		// Token: 0x06004C7E RID: 19582 RVA: 0x0013CB20 File Offset: 0x0013AD20
		public static List<UserMailboxLocator> Instantiate(IRecipientSession adSession, params ProxyAddress[] proxyAddresses)
		{
			ArgumentValidator.ThrowIfNull("adSession", adSession);
			ArgumentValidator.ThrowIfNull("proxyAddresses", proxyAddresses);
			ArgumentValidator.ThrowIfZeroOrNegative("proxyAddresses.Length", proxyAddresses.Length);
			Result<ADUser>[] array = adSession.FindByProxyAddresses<ADUser>(proxyAddresses);
			List<UserMailboxLocator> list = new List<UserMailboxLocator>(proxyAddresses.Length);
			if (array == null)
			{
				MailboxLocator.Tracer.TraceDebug(0L, "UserMailboxLocator::Instantiate. FindByProxyAddresses returned no results");
				return null;
			}
			for (int i = 0; i < proxyAddresses.Length; i++)
			{
				Result<ADUser> result = array[i];
				if (result.Data == null)
				{
					MailboxLocator.Tracer.TraceError<string, ProviderError>(0L, "UserMailboxLocator::Instantiate. FindByProxyAddresses returned error for address {0}. Error: {1}", proxyAddresses[i].ProxyAddressString, result.Error);
					throw new MailboxNotFoundException(ServerStrings.InvalidAddressError(proxyAddresses[i].ProxyAddressString));
				}
				UserMailboxLocator userMailboxLocator = new UserMailboxLocator(adSession);
				userMailboxLocator.InitializeFromAd(result.Data);
				list.Add(userMailboxLocator);
				MailboxLocator.Tracer.TraceDebug<string, UserMailboxLocator>(0L, "UserMailboxLocator::Instantiate. FindByProxyAddresses found user. Address: {0}. Locator: {1}", proxyAddresses[i].ProxyAddressString, userMailboxLocator);
			}
			return list;
		}

		// Token: 0x06004C7F RID: 19583 RVA: 0x0013CC10 File Offset: 0x0013AE10
		public static List<UserMailboxLocator> Instantiate(IRecipientSession adSession, params ADUser[] users)
		{
			ArgumentValidator.ThrowIfNull("adSession", adSession);
			ArgumentValidator.ThrowIfNull("users", users);
			ArgumentValidator.ThrowIfZeroOrNegative("users.Length", users.Length);
			List<UserMailboxLocator> list = new List<UserMailboxLocator>(users.Length);
			foreach (ADUser adUser in users)
			{
				UserMailboxLocator userMailboxLocator = new UserMailboxLocator(adSession);
				userMailboxLocator.InitializeFromAd(adUser);
				list.Add(userMailboxLocator);
			}
			return list;
		}

		// Token: 0x06004C80 RID: 19584 RVA: 0x0013CC78 File Offset: 0x0013AE78
		public static UserMailboxLocator Instantiate(IRecipientSession adSession, ADUser adUser)
		{
			UserMailboxLocator userMailboxLocator = new UserMailboxLocator(adSession);
			userMailboxLocator.InitializeFromAd(adUser);
			return userMailboxLocator;
		}

		// Token: 0x06004C81 RID: 19585 RVA: 0x0013CC94 File Offset: 0x0013AE94
		public override bool IsValidReplicationTarget()
		{
			ADUser aduser = base.FindAdUser();
			return aduser.RecipientTypeDetails == RecipientTypeDetails.UserMailbox;
		}

		// Token: 0x06004C82 RID: 19586 RVA: 0x0013CCB2 File Offset: 0x0013AEB2
		protected override bool IsValidAdUser(ADUser adUser)
		{
			return MailboxLocatorValidator.IsValidUserLocator(adUser);
		}

		// Token: 0x040029B9 RID: 10681
		public static readonly string MailboxLocatorType = "User Mailbox";
	}
}
