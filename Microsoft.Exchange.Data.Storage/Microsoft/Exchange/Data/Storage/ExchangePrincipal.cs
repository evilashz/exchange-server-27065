using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000265 RID: 613
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ExchangePrincipal : IExchangePrincipal
	{
		// Token: 0x060018CE RID: 6350 RVA: 0x000786F8 File Offset: 0x000768F8
		public ExchangePrincipal(IGenericADUser adUser, IEnumerable<IMailboxInfo> allMailboxes, Func<IMailboxInfo, bool> mailboxSelector, RemotingOptions remotingOptions)
		{
			ArgumentValidator.ThrowIfNull("adUser", adUser);
			ArgumentValidator.ThrowIfNull("mailboxes", allMailboxes);
			ArgumentValidator.ThrowIfNull("mailboxSelector", mailboxSelector);
			EnumValidator<RemotingOptions>.ThrowIfInvalid(remotingOptions);
			this.MailboxInfo = allMailboxes.FirstOrDefault(mailboxSelector);
			if (this.MailboxInfo == null && (remotingOptions & RemotingOptions.AllowHybridAccess) != RemotingOptions.AllowHybridAccess)
			{
				throw new UserHasNoMailboxException();
			}
			this.AllMailboxes = allMailboxes;
			this.ObjectId = adUser.ObjectId;
			this.LegacyDn = adUser.LegacyDn;
			this.Alias = (adUser.Alias ?? string.Empty);
			this.DefaultPublicFolderMailbox = adUser.DefaultPublicFolderMailbox;
			this.Sid = adUser.Sid;
			this.MasterAccountSid = adUser.MasterAccountSid;
			this.SidHistory = adUser.SidHistory;
			this.Delegates = from delegateUser in adUser.GrantSendOnBehalfTo
			where delegateUser != null
			select delegateUser;
			this.PreferredCultures = ((adUser.Languages == null) ? Enumerable.Empty<CultureInfo>() : new PreferredCultures(adUser.Languages));
			this.RecipientType = adUser.RecipientType;
			this.RecipientTypeDetails = adUser.RecipientTypeDetails;
			this.IsResource = adUser.IsResource;
			this.ModernGroupType = adUser.ModernGroupType;
			this.PublicToGroupSids = adUser.PublicToGroupSids;
			this.ExternalDirectoryObjectId = adUser.ExternalDirectoryObjectId;
			this.AggregatedMailboxGuids = (adUser.AggregatedMailboxGuids ?? ((IEnumerable<Guid>)Array<Guid>.Empty));
			this.remotingOptions = remotingOptions;
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x00078874 File Offset: 0x00076A74
		protected ExchangePrincipal(ExchangePrincipal sourceExchangePrincipal)
		{
			this.ObjectId = sourceExchangePrincipal.ObjectId;
			this.LegacyDn = sourceExchangePrincipal.LegacyDn;
			this.Alias = sourceExchangePrincipal.Alias;
			this.DefaultPublicFolderMailbox = sourceExchangePrincipal.DefaultPublicFolderMailbox;
			this.Sid = sourceExchangePrincipal.Sid;
			this.MasterAccountSid = sourceExchangePrincipal.MasterAccountSid;
			this.SidHistory = sourceExchangePrincipal.SidHistory;
			this.Delegates = sourceExchangePrincipal.Delegates;
			this.PreferredCultures = sourceExchangePrincipal.PreferredCultures;
			this.RecipientType = sourceExchangePrincipal.RecipientType;
			this.RecipientTypeDetails = sourceExchangePrincipal.RecipientTypeDetails;
			this.IsResource = sourceExchangePrincipal.IsResource;
			this.ModernGroupType = sourceExchangePrincipal.ModernGroupType;
			this.PublicToGroupSids = sourceExchangePrincipal.PublicToGroupSids;
			this.ExternalDirectoryObjectId = sourceExchangePrincipal.ExternalDirectoryObjectId;
			this.AggregatedMailboxGuids = sourceExchangePrincipal.AggregatedMailboxGuids;
			this.MailboxInfo = sourceExchangePrincipal.MailboxInfo;
			this.AllMailboxes = sourceExchangePrincipal.AllMailboxes;
			this.remotingOptions = sourceExchangePrincipal.remotingOptions;
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x060018D0 RID: 6352 RVA: 0x0007896B File Offset: 0x00076B6B
		// (set) Token: 0x060018D1 RID: 6353 RVA: 0x00078973 File Offset: 0x00076B73
		public string LegacyDn { get; private set; }

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x060018D2 RID: 6354 RVA: 0x0007897C File Offset: 0x00076B7C
		// (set) Token: 0x060018D3 RID: 6355 RVA: 0x00078984 File Offset: 0x00076B84
		public string Alias { get; private set; }

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x060018D4 RID: 6356 RVA: 0x0007898D File Offset: 0x00076B8D
		// (set) Token: 0x060018D5 RID: 6357 RVA: 0x00078995 File Offset: 0x00076B95
		public ADObjectId DefaultPublicFolderMailbox { get; private set; }

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x060018D6 RID: 6358 RVA: 0x0007899E File Offset: 0x00076B9E
		// (set) Token: 0x060018D7 RID: 6359 RVA: 0x000789A6 File Offset: 0x00076BA6
		public SecurityIdentifier Sid { get; private set; }

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x060018D8 RID: 6360 RVA: 0x000789AF File Offset: 0x00076BAF
		// (set) Token: 0x060018D9 RID: 6361 RVA: 0x000789B7 File Offset: 0x00076BB7
		public SecurityIdentifier MasterAccountSid { get; private set; }

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x060018DA RID: 6362 RVA: 0x000789C0 File Offset: 0x00076BC0
		// (set) Token: 0x060018DB RID: 6363 RVA: 0x000789C8 File Offset: 0x00076BC8
		public IEnumerable<SecurityIdentifier> SidHistory { get; private set; }

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x060018DC RID: 6364 RVA: 0x000789D1 File Offset: 0x00076BD1
		// (set) Token: 0x060018DD RID: 6365 RVA: 0x000789D9 File Offset: 0x00076BD9
		public IEnumerable<ADObjectId> Delegates { get; private set; }

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x060018DE RID: 6366 RVA: 0x000789E2 File Offset: 0x00076BE2
		// (set) Token: 0x060018DF RID: 6367 RVA: 0x000789EA File Offset: 0x00076BEA
		public IEnumerable<CultureInfo> PreferredCultures { get; private set; }

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x060018E0 RID: 6368 RVA: 0x000789F3 File Offset: 0x00076BF3
		// (set) Token: 0x060018E1 RID: 6369 RVA: 0x000789FB File Offset: 0x00076BFB
		public ADObjectId ObjectId { get; private set; }

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x060018E2 RID: 6370 RVA: 0x00078A04 File Offset: 0x00076C04
		// (set) Token: 0x060018E3 RID: 6371 RVA: 0x00078A0C File Offset: 0x00076C0C
		public RecipientType RecipientType { get; private set; }

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x060018E4 RID: 6372 RVA: 0x00078A15 File Offset: 0x00076C15
		// (set) Token: 0x060018E5 RID: 6373 RVA: 0x00078A1D File Offset: 0x00076C1D
		public RecipientTypeDetails RecipientTypeDetails { get; private set; }

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x060018E6 RID: 6374 RVA: 0x00078A26 File Offset: 0x00076C26
		// (set) Token: 0x060018E7 RID: 6375 RVA: 0x00078A2E File Offset: 0x00076C2E
		public bool? IsResource { get; private set; }

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x060018E8 RID: 6376 RVA: 0x00078A37 File Offset: 0x00076C37
		// (set) Token: 0x060018E9 RID: 6377 RVA: 0x00078A3F File Offset: 0x00076C3F
		public ModernGroupObjectType ModernGroupType { get; private set; }

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x060018EA RID: 6378 RVA: 0x00078A48 File Offset: 0x00076C48
		// (set) Token: 0x060018EB RID: 6379 RVA: 0x00078A50 File Offset: 0x00076C50
		public IEnumerable<SecurityIdentifier> PublicToGroupSids { get; private set; }

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x060018EC RID: 6380 RVA: 0x00078A59 File Offset: 0x00076C59
		// (set) Token: 0x060018ED RID: 6381 RVA: 0x00078A61 File Offset: 0x00076C61
		public string ExternalDirectoryObjectId { get; private set; }

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x060018EE RID: 6382 RVA: 0x00078A6A File Offset: 0x00076C6A
		// (set) Token: 0x060018EF RID: 6383 RVA: 0x00078A72 File Offset: 0x00076C72
		public IEnumerable<Guid> AggregatedMailboxGuids { get; private set; }

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x060018F0 RID: 6384 RVA: 0x00078A7B File Offset: 0x00076C7B
		// (set) Token: 0x060018F1 RID: 6385 RVA: 0x00078A83 File Offset: 0x00076C83
		public IMailboxInfo MailboxInfo { get; private set; }

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x060018F2 RID: 6386 RVA: 0x00078A8C File Offset: 0x00076C8C
		// (set) Token: 0x060018F3 RID: 6387 RVA: 0x00078A94 File Offset: 0x00076C94
		public IEnumerable<IMailboxInfo> AllMailboxes { get; private set; }

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x060018F4 RID: 6388 RVA: 0x00078A9D File Offset: 0x00076C9D
		public bool IsCrossSiteAccessAllowed
		{
			get
			{
				return (this.remotingOptions & RemotingOptions.AllowCrossSite) == RemotingOptions.AllowCrossSite;
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x060018F5 RID: 6389 RVA: 0x00078AAA File Offset: 0x00076CAA
		public virtual bool IsMailboxInfoRequired
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x00078AB0 File Offset: 0x00076CB0
		public override string ToString()
		{
			return string.Format("LegacyDn: {0}, RecipientType: {1}, RecipientTypeDetails: {2}, Selected Mailbox: {3}", new object[]
			{
				this.LegacyDn,
				this.RecipientType,
				this.RecipientTypeDetails,
				this.MailboxInfo
			});
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x00078B00 File Offset: 0x00076D00
		public ExchangePrincipal WithDelegates(IEnumerable<ADObjectId> delegates)
		{
			ArgumentValidator.ThrowIfNull("delegates", delegates);
			ExchangePrincipal exchangePrincipal = this.Clone();
			exchangePrincipal.Delegates = delegates;
			return exchangePrincipal;
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x00078B28 File Offset: 0x00076D28
		public ExchangePrincipal WithPreferredCultures(IEnumerable<CultureInfo> cultures)
		{
			ArgumentValidator.ThrowIfNull("cultures", cultures);
			ExchangePrincipal exchangePrincipal = this.Clone();
			exchangePrincipal.PreferredCultures = cultures;
			return exchangePrincipal;
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x00078B50 File Offset: 0x00076D50
		public ExchangePrincipal WithSelectedMailbox(IMailboxInfo selectedMailboxInfo, RemotingOptions? remotingOptions)
		{
			remotingOptions = new RemotingOptions?(remotingOptions ?? this.remotingOptions);
			bool flag = (remotingOptions & RemotingOptions.AllowCrossPremise) == RemotingOptions.AllowCrossPremise;
			if (selectedMailboxInfo == null || (selectedMailboxInfo.IsRemote && !flag))
			{
				throw new UserHasNoMailboxException();
			}
			if (!this.AllMailboxes.Contains(selectedMailboxInfo))
			{
				throw new InvalidOperationException("Selected mailbox not found in all mailboxes collection");
			}
			ExchangePrincipal exchangePrincipal = this.Clone();
			exchangePrincipal.MailboxInfo = selectedMailboxInfo;
			exchangePrincipal.remotingOptions = remotingOptions.Value;
			return exchangePrincipal;
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x060018FA RID: 6394
		public abstract string PrincipalId { get; }

		// Token: 0x060018FB RID: 6395
		protected abstract ExchangePrincipal Clone();

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x060018FC RID: 6396 RVA: 0x00078C03 File Offset: 0x00076E03
		private static IDatabaseLocationProvider DatabaseLocationProvider
		{
			get
			{
				if (ExchangePrincipal.databaseLocationProvider == null)
				{
					ExchangePrincipal.databaseLocationProvider = new DatabaseLocationProvider();
				}
				return ExchangePrincipal.databaseLocationProvider;
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x060018FD RID: 6397 RVA: 0x00078C1B File Offset: 0x00076E1B
		private static IDirectoryAccessor DirectoryAccessor
		{
			get
			{
				if (ExchangePrincipal.directoryAccessor == null)
				{
					ExchangePrincipal.directoryAccessor = new DirectoryAccessor();
				}
				return ExchangePrincipal.directoryAccessor;
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x060018FE RID: 6398 RVA: 0x00078C33 File Offset: 0x00076E33
		private static ExchangePrincipalFactory ExchangePrincipalFactory
		{
			get
			{
				if (ExchangePrincipal.exchangePrincipalFactory == null)
				{
					ExchangePrincipal.exchangePrincipalFactory = new ExchangePrincipalFactory(ExchangePrincipal.DirectoryAccessor, ExchangePrincipal.DatabaseLocationProvider);
				}
				return ExchangePrincipal.exchangePrincipalFactory;
			}
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x00078C55 File Offset: 0x00076E55
		public static ExchangePrincipal FromADUser(ADUser user, string domainController = null)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromADUser(user, domainController);
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x00078C63 File Offset: 0x00076E63
		public static ExchangePrincipal FromADUser(ADUser user, RemotingOptions remotingOptions)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromADUser(user, remotingOptions);
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x00078C71 File Offset: 0x00076E71
		public static ExchangePrincipal FromADUser(IGenericADUser user, RemotingOptions remotingOptions)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromADUser(user, remotingOptions);
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x00078C7F File Offset: 0x00076E7F
		public static ExchangePrincipal FromADUser(ADSessionSettings adSettings, ADUser user)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromADUser(adSettings, user);
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x00078C8D File Offset: 0x00076E8D
		public static ExchangePrincipal FromADUser(ADSessionSettings adSettings, ADUser user, RemotingOptions remotingOptions)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromADUser(adSettings, user, remotingOptions, null);
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x00078C9D File Offset: 0x00076E9D
		public static ExchangePrincipal FromADUser(ADSessionSettings adSettings, IGenericADUser user, RemotingOptions remotingOptions)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromADUser(adSettings, user, remotingOptions, null);
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x00078CAD File Offset: 0x00076EAD
		public static ExchangePrincipal FromADUser(ADUser user, DatabaseLocationInfo databaseLocationInfo, RemotingOptions remotingOptions)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromADUser(user, databaseLocationInfo, remotingOptions);
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x00078CBC File Offset: 0x00076EBC
		public static ExchangePrincipal FromADUser(IGenericADUser user, DatabaseLocationInfo databaseLocationInfo, RemotingOptions remotingOptions)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromADUser(user, databaseLocationInfo, remotingOptions);
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x00078CCB File Offset: 0x00076ECB
		public static IExchangePrincipal FromADMailboxRecipient(IADMailboxRecipient mailbox, RemotingOptions remotingOptions = RemotingOptions.LocalConnectionsOnly)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromADMailboxRecipient(mailbox, remotingOptions);
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x00078CD9 File Offset: 0x00076ED9
		public static IExchangePrincipal FromADMailboxRecipient(IADMailboxRecipient user, DatabaseLocationInfo databaseLocationInfo, RemotingOptions remotingOptions = RemotingOptions.LocalConnectionsOnly)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromADMailboxRecipient(user, databaseLocationInfo, remotingOptions);
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x00078CE8 File Offset: 0x00076EE8
		public static ExchangePrincipal FromWindowsIdentity(ADSessionSettings adSettings, WindowsIdentity windowsIdentity)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromWindowsIdentity(adSettings, windowsIdentity);
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x00078CF6 File Offset: 0x00076EF6
		public static ExchangePrincipal FromWindowsIdentity(ADSessionSettings adSettings, WindowsIdentity windowsIdentity, RemotingOptions remotingOptions)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromWindowsIdentity(adSettings, windowsIdentity, remotingOptions);
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x00078D05 File Offset: 0x00076F05
		public static ExchangePrincipal FromWindowsIdentity(IRecipientSession recipientSession, WindowsIdentity windowsIdentity)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromWindowsIdentity(recipientSession, windowsIdentity);
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x00078D13 File Offset: 0x00076F13
		public static ExchangePrincipal FromWindowsIdentity(IRecipientSession recipientSession, WindowsIdentity windowsIdentity, RemotingOptions remotingOptions)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromWindowsIdentity(recipientSession, windowsIdentity, remotingOptions);
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x00078D22 File Offset: 0x00076F22
		public static ExchangePrincipal FromUserSid(ADSessionSettings adSettings, SecurityIdentifier userSid)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromUserSid(adSettings, userSid);
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x00078D30 File Offset: 0x00076F30
		public static ExchangePrincipal FromUserSid(ADSessionSettings adSettings, SecurityIdentifier userSid, RemotingOptions remotingOptions)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromUserSid(adSettings, userSid, remotingOptions);
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x00078D3F File Offset: 0x00076F3F
		public static ExchangePrincipal FromUserSid(IRecipientSession recipientSession, SecurityIdentifier userSid)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromUserSid(recipientSession, userSid);
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x00078D4D File Offset: 0x00076F4D
		public static ExchangePrincipal FromUserSid(IRecipientSession recipientSession, SecurityIdentifier userSid, RemotingOptions remotingOptions)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromUserSid(recipientSession, userSid, remotingOptions);
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x00078D5C File Offset: 0x00076F5C
		public static ExchangePrincipal FromProxyAddress(ADSessionSettings adSettings, string proxyAddress)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromProxyAddress(adSettings, proxyAddress);
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x00078D6A File Offset: 0x00076F6A
		public static ExchangePrincipal FromProxyAddress(ADSessionSettings adSettings, string proxyAddress, RemotingOptions remotingOptions)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromProxyAddress(adSettings, proxyAddress, remotingOptions);
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x00078D79 File Offset: 0x00076F79
		public static ExchangePrincipal FromProxyAddress(IRecipientSession session, string proxyAddress)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromProxyAddress(session, proxyAddress);
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x00078D87 File Offset: 0x00076F87
		public static ExchangePrincipal FromProxyAddress(IRecipientSession session, string proxyAddress, RemotingOptions remotingOptions)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromProxyAddress(session, proxyAddress, remotingOptions);
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x00078D96 File Offset: 0x00076F96
		public static ExchangePrincipal FromLegacyDN(ADSessionSettings adSettings, string legacyDN)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromLegacyDN(adSettings, legacyDN);
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x00078DA4 File Offset: 0x00076FA4
		public static ExchangePrincipal FromLegacyDN(ADSessionSettings adSettings, string legacyDN, RemotingOptions remotingOptions)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromLegacyDN(adSettings, legacyDN, remotingOptions);
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x00078DB3 File Offset: 0x00076FB3
		public static ExchangePrincipal FromLegacyDN(IRecipientSession recipientSession, string legacyDN, RemotingOptions remotingOptions)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromLegacyDN(recipientSession, legacyDN, remotingOptions);
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x00078DC2 File Offset: 0x00076FC2
		public static ExchangePrincipal FromLegacyDNByMiniRecipient(ADSessionSettings adSettings, string legacyDN, RemotingOptions remotingOptions, PropertyDefinition[] miniRecipientProperties, out StorageMiniRecipient miniRecipient)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromLegacyDNByMiniRecipient(adSettings, legacyDN, remotingOptions, miniRecipientProperties, out miniRecipient);
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x00078DD4 File Offset: 0x00076FD4
		public static ExchangePrincipal FromLegacyDNByMiniRecipient(IRecipientSession recipientSession, string legacyDN, RemotingOptions remotingOptions, PropertyDefinition[] miniRecipientProperties, out StorageMiniRecipient miniRecipient)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromLegacyDNByMiniRecipient(recipientSession, legacyDN, remotingOptions, miniRecipientProperties, out miniRecipient);
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x00078DE6 File Offset: 0x00076FE6
		public static ExchangePrincipal FromADSystemMailbox(ADSessionSettings adSettings, ADSystemMailbox adSystemMailbox, Server server)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromADSystemMailbox(adSettings, adSystemMailbox, server);
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x00078DF5 File Offset: 0x00076FF5
		public static ExchangePrincipal FromADSystemMailbox(IRecipientSession recipientSession, IGenericADUser adSystemMailbox, string serverFqdn, string serverLegacyDn)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromADSystemMailbox(recipientSession, adSystemMailbox, serverFqdn, serverLegacyDn);
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x00078E05 File Offset: 0x00077005
		public static ExchangePrincipal FromMailboxGuid(ADSessionSettings adSettings, Guid mailboxGuid, string domainController = null)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromMailboxGuid(adSettings, mailboxGuid, domainController);
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x00078E14 File Offset: 0x00077014
		public static ExchangePrincipal FromMailboxGuid(ADSessionSettings adSettings, Guid mailboxGuid, RemotingOptions remotingOptions, string domainController = null)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromMailboxGuid(adSettings, mailboxGuid, remotingOptions, domainController);
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x00078E24 File Offset: 0x00077024
		public static ExchangePrincipal FromMailboxGuid(ADSessionSettings adSettings, Guid mailboxGuid, Guid mdbGuid, RemotingOptions remotingOptions, string domainController = null, bool isContentIndexing = false)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromMailboxGuid(adSettings, mailboxGuid, mdbGuid, remotingOptions, domainController, isContentIndexing);
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x00078E38 File Offset: 0x00077038
		public static ExchangePrincipal FromMailboxGuid(IRecipientSession recipientSession, Guid mailboxGuid, Guid mdbGuid, RemotingOptions remotingOptions, bool isContentIndexing = false)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromMailboxGuid(recipientSession, mailboxGuid, mdbGuid, remotingOptions, isContentIndexing);
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x00078E4A File Offset: 0x0007704A
		public static ExchangePrincipal FromLocalServerMailboxGuid(ADSessionSettings adSettings, Guid mdbGuid, Guid mailboxGuid)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromLocalServerMailboxGuid(adSettings, mdbGuid, mailboxGuid);
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x00078E59 File Offset: 0x00077059
		public static ExchangePrincipal FromLocalServerMailboxGuid(ADSessionSettings adSettings, Guid mdbGuid, Guid mailboxGuid, bool isContentIndexing)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromLocalServerMailboxGuid(adSettings, mdbGuid, mailboxGuid, isContentIndexing);
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x00078E69 File Offset: 0x00077069
		public static ExchangePrincipal FromLocalServerMailboxGuid(IRecipientSession recipientSession, Guid mdbGuid, Guid mailboxGuid, DatabaseLocationInfo databaseLocationInfo)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromLocalServerMailboxGuid(recipientSession, mdbGuid, mailboxGuid, databaseLocationInfo);
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x00078E79 File Offset: 0x00077079
		public static ExchangePrincipal FromDirectoryObjectId(IRecipientSession session, ADObjectId directoryEntry, RemotingOptions remoteOptions = RemotingOptions.LocalConnectionsOnly)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromDirectoryObjectId(session, directoryEntry, remoteOptions);
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x00078E88 File Offset: 0x00077088
		public static ExchangePrincipal FromMailboxData(ADSessionSettings adSettings, Guid mdbGuid, Guid mailboxGuid, string mailboxLegacyDN, string primarySmtpAddress, ICollection<CultureInfo> preferredCultures, bool bypassRemoteCheck = false)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromMailboxData(adSettings, mdbGuid, mailboxGuid, mailboxLegacyDN, primarySmtpAddress, preferredCultures, bypassRemoteCheck);
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x00078EA0 File Offset: 0x000770A0
		public static ExchangePrincipal FromMailboxData(string displayName, ADSessionSettings adSettings, Guid mdbGuid, Guid mailboxGuid, string mailboxLegacyDN, string primarySmtpAddress, ICollection<CultureInfo> preferredCultures, bool bypassRemoteCheck = false)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromMailboxData(displayName, adSettings, mdbGuid, mailboxGuid, mailboxLegacyDN, primarySmtpAddress, preferredCultures, bypassRemoteCheck);
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x00078EC4 File Offset: 0x000770C4
		public static ExchangePrincipal FromMailboxData(string displayName, ADSessionSettings adSettings, Guid mdbGuid, Guid mailboxGuid, string mailboxLegacyDN, string primarySmtpAddress, ICollection<CultureInfo> preferredCultures, bool bypassRemoteCheck, RecipientType recipientType, RecipientTypeDetails recipientTypeDetails)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromMailboxData(displayName, adSettings, mdbGuid, mailboxGuid, mailboxLegacyDN, primarySmtpAddress, preferredCultures, bypassRemoteCheck, recipientType, recipientTypeDetails);
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x00078EEC File Offset: 0x000770EC
		public static ExchangePrincipal FromAnyVersionMailboxData(string displayName, Guid mailboxGuid, Guid mdbGuid, string primarySmtpAddress, string legacyExchangeDN, ADObjectId id, RecipientType recipientType, SecurityIdentifier masterAccountSid, OrganizationId organizationId)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromAnyVersionMailboxData(displayName, mailboxGuid, mdbGuid, primarySmtpAddress, legacyExchangeDN, id, recipientType, masterAccountSid, organizationId);
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x00078F14 File Offset: 0x00077114
		public static ExchangePrincipal FromAnyVersionMailboxData(string displayName, Guid mailboxGuid, Guid mdbGuid, string primarySmtpAddress, string legacyExchangeDN, ADObjectId id, RecipientType recipientType, SecurityIdentifier masterAccountSid, OrganizationId organizationId, bool isArchive)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromAnyVersionMailboxData(displayName, mailboxGuid, mdbGuid, primarySmtpAddress, legacyExchangeDN, id, recipientType, masterAccountSid, organizationId, isArchive);
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x00078F3C File Offset: 0x0007713C
		public static ExchangePrincipal FromAnyVersionMailboxData(string displayName, Guid mailboxGuid, Guid mdbGuid, string primarySmtpAddress, string legacyExchangeDN, ADObjectId id, RecipientType recipientType, SecurityIdentifier masterAccountSid, OrganizationId organizationId, RemotingOptions remotingOptions, bool isArchive)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromAnyVersionMailboxData(displayName, mailboxGuid, mdbGuid, primarySmtpAddress, legacyExchangeDN, id, recipientType, masterAccountSid, organizationId, remotingOptions, isArchive);
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x00078F68 File Offset: 0x00077168
		public static ExchangePrincipal FromMailboxData(ADSessionSettings adSettings, string displayName, string serverFqdn, string serverLegacyDN, string mailboxLegacyDN, Guid mailboxGuid, Guid mdbGuid, string primarySmtpAddress, ICollection<CultureInfo> preferredCultures, IEnumerable<Guid> aggregatedMailboxGuids)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromMailboxData(adSettings, displayName, serverFqdn, serverLegacyDN, mailboxLegacyDN, mailboxGuid, mdbGuid, primarySmtpAddress, preferredCultures, aggregatedMailboxGuids);
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x00078F90 File Offset: 0x00077190
		public static ExchangePrincipal FromMailboxData(ADSessionSettings adSessionSettings, string displayName, string serverFqdn, string serverLegacyDN, string mailboxLegacyDN, Guid mailboxGuid, Guid mdbGuid, string primarySmtpAddress, ADObjectId id, ICollection<CultureInfo> preferredCultures, IEnumerable<Guid> aggregatedMailboxGuids, RecipientType userRecipientType = RecipientType.Invalid, RemotingOptions remotingOptions = RemotingOptions.AllowCrossSite)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromMailboxData(adSessionSettings, displayName, serverFqdn, serverLegacyDN, mailboxLegacyDN, mailboxGuid, mdbGuid, primarySmtpAddress, id, preferredCultures, aggregatedMailboxGuids, userRecipientType, remotingOptions);
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x00078FBD File Offset: 0x000771BD
		public static ExchangePrincipal FromMailboxData(Guid mailboxGuid, Guid mdbGuid, ICollection<CultureInfo> preferredCultures)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromMailboxData(mailboxGuid, mdbGuid, preferredCultures);
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x00078FCC File Offset: 0x000771CC
		public static ExchangePrincipal FromMailboxData(Guid mailboxGuid, Guid mdbGuid, OrganizationId organizationId, ICollection<CultureInfo> preferredCultures)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromMailboxData(mailboxGuid, mdbGuid, organizationId, preferredCultures);
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x00078FDC File Offset: 0x000771DC
		public static ExchangePrincipal FromMailboxData(Guid mailboxGuid, Guid mdbGuid, ICollection<CultureInfo> preferredCultures, RemotingOptions remotingOptions)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromMailboxData(mailboxGuid, mdbGuid, preferredCultures, remotingOptions);
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x00078FEC File Offset: 0x000771EC
		public static ExchangePrincipal FromMailboxData(Guid mailboxGuid, Guid mdbGuid, OrganizationId organizationId, ICollection<CultureInfo> preferredCultures, RemotingOptions remotingOptions)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromMailboxData(mailboxGuid, mdbGuid, organizationId, preferredCultures, remotingOptions);
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x00078FFE File Offset: 0x000771FE
		public static ExchangePrincipal FromMailboxData(Guid mailboxGuid, Guid mdbGuid, OrganizationId organizationId, ICollection<CultureInfo> preferredCultures, RemotingOptions remotingOptions, DatabaseLocationInfo databaseLocationInfo)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromMailboxData(mailboxGuid, mdbGuid, organizationId, preferredCultures, remotingOptions, databaseLocationInfo);
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x00079012 File Offset: 0x00077212
		public static ExchangePrincipal FromMiniRecipient(StorageMiniRecipient miniRecipient)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromMiniRecipient(miniRecipient);
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x0007901F File Offset: 0x0007721F
		public static ExchangePrincipal FromMiniRecipient(StorageMiniRecipient miniRecipient, RemotingOptions remotingOptions)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromMiniRecipient(miniRecipient, remotingOptions);
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x0007902D File Offset: 0x0007722D
		public static ExchangePrincipal FromMiniRecipient(IGenericADUser miniRecipient, RemotingOptions remotingOptions)
		{
			return ExchangePrincipal.ExchangePrincipalFactory.FromMiniRecipient(miniRecipient, remotingOptions);
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x0007903B File Offset: 0x0007723B
		public static void SetDatabaseLocationProviderForTest(IDatabaseLocationProvider databaseLocationProvider)
		{
			ExchangePrincipal.databaseLocationProvider = databaseLocationProvider;
			ExchangePrincipal.SetFactory(null);
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x00079049 File Offset: 0x00077249
		public static void SetDirectoryAccessorForTest(IDirectoryAccessor directoryAccessor)
		{
			ExchangePrincipal.directoryAccessor = directoryAccessor;
			ExchangePrincipal.SetFactory(null);
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x00079057 File Offset: 0x00077257
		internal static void SetFactory(ExchangePrincipalFactory exchangePrincipalFactory)
		{
			ExchangePrincipal.exchangePrincipalFactory = exchangePrincipalFactory;
		}

		// Token: 0x04001239 RID: 4665
		private RemotingOptions remotingOptions;

		// Token: 0x0400123A RID: 4666
		private static IDatabaseLocationProvider databaseLocationProvider;

		// Token: 0x0400123B RID: 4667
		private static IDirectoryAccessor directoryAccessor;

		// Token: 0x0400123C RID: 4668
		private static ExchangePrincipalFactory exchangePrincipalFactory;
	}
}
