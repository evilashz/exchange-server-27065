using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200026E RID: 622
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ExchangePrincipalFactory
	{
		// Token: 0x06001979 RID: 6521 RVA: 0x00079D98 File Offset: 0x00077F98
		public ExchangePrincipalFactory(IDirectoryAccessor directoryAccessor, IDatabaseLocationProvider databaseLocationProvider)
		{
			this.directoryAccessor = directoryAccessor;
			this.databaseLocationProvider = databaseLocationProvider;
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x00079DAE File Offset: 0x00077FAE
		public ExchangePrincipal FromADUser(ADUser user, string domainController = null)
		{
			Util.ThrowOnNullArgument(user, "user");
			return this.FromADUser(user.OrganizationId.ToADSessionSettings(), user, RemotingOptions.LocalConnectionsOnly, domainController);
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x00079DCF File Offset: 0x00077FCF
		public ExchangePrincipal FromADUser(ADUser user, RemotingOptions remotingOptions)
		{
			Util.ThrowOnNullArgument(user, "user");
			return this.FromADUser(new ADUserGenericWrapper(user), remotingOptions);
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x00079DE9 File Offset: 0x00077FE9
		public ExchangePrincipal FromADUser(IGenericADUser user, RemotingOptions remotingOptions)
		{
			Util.ThrowOnNullArgument(user, "user");
			EnumValidator.ThrowIfInvalid<RemotingOptions>(remotingOptions, "remotingOptions");
			return this.InternalFromADUser(user, remotingOptions);
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x00079E09 File Offset: 0x00078009
		public ExchangePrincipal FromADUser(ADSessionSettings adSettings, ADUser user)
		{
			return this.FromADUser(adSettings, user, RemotingOptions.LocalConnectionsOnly, null);
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x00079E15 File Offset: 0x00078015
		public ExchangePrincipal FromADUser(ADSessionSettings adSettings, ADUser user, RemotingOptions remotingOptions, string domainController = null)
		{
			Util.ThrowOnNullArgument(user, "user");
			return this.FromADUser(adSettings, new ADUserGenericWrapper(user), remotingOptions, domainController);
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x00079E34 File Offset: 0x00078034
		public ExchangePrincipal FromADUser(ADSessionSettings adSettings, IGenericADUser user, RemotingOptions remotingOptions, string domainController = null)
		{
			Util.ThrowOnNullArgument(adSettings, "adSettings");
			EnumValidator.ThrowIfInvalid<RemotingOptions>(remotingOptions, "remotingOptions");
			Util.ThrowOnNullArgument(user, "user");
			return this.InternalFromADUser(user, remotingOptions);
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x00079E6C File Offset: 0x0007806C
		public ExchangePrincipal FromADUser(ADUser user, DatabaseLocationInfo databaseLocationInfo, RemotingOptions remotingOptions)
		{
			Util.ThrowOnNullArgument(user, "user");
			return this.FromADUser(new ADUserGenericWrapper(user), databaseLocationInfo, remotingOptions);
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x00079E88 File Offset: 0x00078088
		public ExchangePrincipal FromADUser(IGenericADUser user, DatabaseLocationInfo databaseLocationInfo, RemotingOptions remotingOptions)
		{
			EnumValidator.ThrowIfInvalid<RemotingOptions>(remotingOptions, "remotingOptions");
			Util.ThrowOnNullArgument(user, "user");
			ADObjectId mdb;
			bool asArchive = this.UpdateArchiveStatus(user.MailboxGuid, user, out mdb);
			return this.InternalFromADUser(user, mdb, databaseLocationInfo, remotingOptions, asArchive, false, null);
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x00079ED0 File Offset: 0x000780D0
		public IExchangePrincipal FromADMailboxRecipient(IADMailboxRecipient mailbox, RemotingOptions remotingOptions)
		{
			return this.FromADMailboxRecipient(mailbox, null, remotingOptions);
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x00079EDC File Offset: 0x000780DC
		public IExchangePrincipal FromADMailboxRecipient(IADMailboxRecipient mailbox, DatabaseLocationInfo databaseLocationInfo, RemotingOptions remotingOptions)
		{
			Util.ThrowOnNullArgument(mailbox, "mailbox");
			IADUser iaduser = mailbox as IADUser;
			if (iaduser != null)
			{
				return this.InternalFromADUser(new ADUserGenericWrapper(iaduser), null, databaseLocationInfo, remotingOptions, false, false, null);
			}
			IADGroup iadgroup = mailbox as IADGroup;
			if (iadgroup != null)
			{
				return this.InternalFromADUser(new ADGroupGenericWrapper(iadgroup), null, databaseLocationInfo, remotingOptions, false, false, null);
			}
			throw new InvalidOperationException("ExchangePrincipal.FromADMailboxRecipient doesn't support type " + mailbox.GetType().Name);
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x00079F58 File Offset: 0x00078158
		public ExchangePrincipal FromWindowsIdentity(ADSessionSettings adSettings, WindowsIdentity windowsIdentity)
		{
			return this.FromWindowsIdentity(adSettings, windowsIdentity, RemotingOptions.LocalConnectionsOnly);
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x00079F63 File Offset: 0x00078163
		public ExchangePrincipal FromWindowsIdentity(ADSessionSettings adSettings, WindowsIdentity windowsIdentity, RemotingOptions remotingOptions)
		{
			EnumValidator.ThrowIfInvalid<RemotingOptions>(remotingOptions, "remotingOptions");
			Util.ThrowOnNullArgument(adSettings, "adSettings");
			Util.ThrowOnNullArgument(windowsIdentity, "windowsIdentity");
			return this.FromUserSid(adSettings, windowsIdentity.User, remotingOptions);
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x00079F94 File Offset: 0x00078194
		public ExchangePrincipal FromWindowsIdentity(IRecipientSession recipientSession, WindowsIdentity windowsIdentity)
		{
			return this.FromWindowsIdentity(recipientSession, windowsIdentity, RemotingOptions.LocalConnectionsOnly);
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x00079F9F File Offset: 0x0007819F
		public ExchangePrincipal FromWindowsIdentity(IRecipientSession recipientSession, WindowsIdentity windowsIdentity, RemotingOptions remotingOptions)
		{
			EnumValidator.ThrowIfInvalid<RemotingOptions>(remotingOptions, "remotingOptions");
			Util.ThrowOnNullArgument(windowsIdentity, "windowsIdentity");
			return this.FromUserSid(recipientSession, windowsIdentity.User, remotingOptions);
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x00079FC5 File Offset: 0x000781C5
		public ExchangePrincipal FromUserSid(ADSessionSettings adSettings, SecurityIdentifier userSid)
		{
			return this.FromUserSid(adSettings, userSid, RemotingOptions.LocalConnectionsOnly);
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x00079FD0 File Offset: 0x000781D0
		public ExchangePrincipal FromUserSid(ADSessionSettings adSettings, SecurityIdentifier userSid, RemotingOptions remotingOptions)
		{
			Util.ThrowOnNullArgument(adSettings, "adSettings");
			EnumValidator.ThrowIfInvalid<RemotingOptions>(remotingOptions, "remotingOptions");
			return this.FromUserSid(adSettings.CreateRecipientSession(null), userSid, remotingOptions);
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x0007A004 File Offset: 0x00078204
		public ExchangePrincipal FromUserSid(IRecipientSession recipientSession, SecurityIdentifier userSid)
		{
			return this.FromUserSid(recipientSession, userSid, RemotingOptions.LocalConnectionsOnly);
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x0007A010 File Offset: 0x00078210
		public ExchangePrincipal FromUserSid(IRecipientSession recipientSession, SecurityIdentifier userSid, RemotingOptions remotingOptions)
		{
			Util.ThrowOnNullArgument(recipientSession, "recipientSession");
			Util.ThrowOnNullArgument(userSid, "userSid");
			EnumValidator.ThrowIfInvalid<RemotingOptions>(remotingOptions, "remotingOptions");
			this.CheckNoCrossPremiseAccess(remotingOptions);
			IGenericADUser genericADUser = this.directoryAccessor.FindBySid(recipientSession, userSid);
			if (genericADUser == null)
			{
				throw new ObjectNotFoundException(ServerStrings.ADUserNotFound);
			}
			return this.InternalFromADUser(genericADUser, remotingOptions);
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x0007A06B File Offset: 0x0007826B
		public ExchangePrincipal FromProxyAddress(ADSessionSettings adSettings, string proxyAddress)
		{
			return this.FromProxyAddress(adSettings, proxyAddress, RemotingOptions.LocalConnectionsOnly);
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x0007A078 File Offset: 0x00078278
		public ExchangePrincipal FromProxyAddress(ADSessionSettings adSettings, string proxyAddress, RemotingOptions remotingOptions)
		{
			Util.ThrowOnNullArgument(adSettings, "adSettings");
			EnumValidator.ThrowIfInvalid<RemotingOptions>(remotingOptions, "remotingOptions");
			return this.FromProxyAddress(adSettings.CreateRecipientSession(null), proxyAddress, remotingOptions);
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x0007A0AC File Offset: 0x000782AC
		public ExchangePrincipal FromProxyAddress(IRecipientSession session, string proxyAddress)
		{
			return this.FromProxyAddress(session, proxyAddress, RemotingOptions.LocalConnectionsOnly);
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x0007A0B8 File Offset: 0x000782B8
		public ExchangePrincipal FromProxyAddress(IRecipientSession session, string proxyAddress, RemotingOptions remotingOptions)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(proxyAddress, "proxyAddress");
			EnumValidator.ThrowIfInvalid<RemotingOptions>(remotingOptions, "remotingOptions");
			if (proxyAddress.Length == 0)
			{
				throw new ObjectNotFoundException(ServerStrings.ADUserNotFound);
			}
			this.CheckNoCrossPremiseAccess(remotingOptions);
			ProxyAddress proxyAddress2 = ProxyAddress.Parse(proxyAddress);
			IGenericADUser genericADUser = this.directoryAccessor.FindByProxyAddress(session, proxyAddress2);
			if (genericADUser == null)
			{
				throw new ObjectNotFoundException(ServerStrings.ADUserNotFound);
			}
			ADObjectId mdb;
			bool asArchive = this.UpdateArchiveStatus(genericADUser.MailboxGuid, genericADUser, out mdb);
			return this.InternalFromADUser(genericADUser, mdb, null, remotingOptions, asArchive, false, null);
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x0007A14D File Offset: 0x0007834D
		public ExchangePrincipal FromLegacyDN(ADSessionSettings adSettings, string legacyDN)
		{
			return this.FromLegacyDN(adSettings, legacyDN, RemotingOptions.LocalConnectionsOnly);
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x0007A158 File Offset: 0x00078358
		public ExchangePrincipal FromLegacyDN(ADSessionSettings adSettings, string legacyDN, RemotingOptions remotingOptions)
		{
			Util.ThrowOnNullArgument(adSettings, "adSettings");
			return this.FromLegacyDN(adSettings.CreateRecipientSession(null), legacyDN, remotingOptions);
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x0007A1BC File Offset: 0x000783BC
		public ExchangePrincipal FromLegacyDN(IRecipientSession recipientSession, string legacyDN, RemotingOptions remotingOptions)
		{
			Util.ThrowOnNullArgument(recipientSession, "recipientSession");
			Util.ThrowOnNullArgument(legacyDN, "legacyDN");
			EnumValidator.ThrowIfInvalid<RemotingOptions>(remotingOptions, "remotingOptions");
			if (legacyDN.Length == 0)
			{
				throw new ObjectNotFoundException(ServerStrings.ADUserNotFound);
			}
			Guid mbxGuid;
			legacyDN = this.TryToExtractArchiveOrAggregatedMailboxGuid(legacyDN, out mbxGuid);
			IGenericADUser genericADUser = this.directoryAccessor.FindByLegacyExchangeDn(recipientSession, legacyDN);
			if (genericADUser == null)
			{
				throw new ObjectNotFoundException(ServerStrings.ADUserNotFound);
			}
			ADObjectId mdb;
			bool asArchive = this.UpdateArchiveStatus(mbxGuid, genericADUser, out mdb);
			Guid? aggregatedMailboxGuid = null;
			if (mbxGuid != Guid.Empty && genericADUser.ArchiveGuid != mbxGuid)
			{
				if (genericADUser.AggregatedMailboxGuids != null)
				{
					aggregatedMailboxGuid = (genericADUser.AggregatedMailboxGuids.Any((Guid mailbox) => mailbox == mbxGuid) ? new Guid?(mbxGuid) : null);
				}
				if (aggregatedMailboxGuid == null && genericADUser.MailboxLocations != null)
				{
					aggregatedMailboxGuid = (genericADUser.MailboxLocations.Any((IMailboxLocationInfo mailbox) => mailbox.MailboxGuid.Equals(mbxGuid)) ? new Guid?(mbxGuid) : null);
				}
			}
			ExchangePrincipal exchangePrincipal = this.InternalFromADUser(genericADUser, mdb, null, remotingOptions, asArchive, false, aggregatedMailboxGuid);
			if (mbxGuid != Guid.Empty && !exchangePrincipal.MailboxInfo.MailboxGuid.Equals(mbxGuid))
			{
				throw new ObjectNotFoundException(ServerStrings.AggregatedMailboxNotFound(mbxGuid.ToString()));
			}
			return exchangePrincipal;
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x0007A36A File Offset: 0x0007856A
		public ExchangePrincipal FromLegacyDNByMiniRecipient(ADSessionSettings adSettings, string legacyDN, RemotingOptions remotingOptions, PropertyDefinition[] miniRecipientProperties, out StorageMiniRecipient miniRecipient)
		{
			Util.ThrowOnNullArgument(adSettings, "adSettings");
			return this.FromLegacyDNByMiniRecipient(adSettings.CreateRecipientSession(null), legacyDN, remotingOptions, miniRecipientProperties, out miniRecipient);
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x0007A3D0 File Offset: 0x000785D0
		public ExchangePrincipal FromLegacyDNByMiniRecipient(IRecipientSession recipientSession, string legacyDN, RemotingOptions remotingOptions, PropertyDefinition[] miniRecipientProperties, out StorageMiniRecipient miniRecipient)
		{
			Util.ThrowOnNullArgument(recipientSession, "recipientSession");
			Util.ThrowOnNullArgument(legacyDN, "legacyDN");
			EnumValidator.ThrowIfInvalid<RemotingOptions>(remotingOptions, "remotingOptions");
			if (legacyDN.Length == 0)
			{
				throw new ArgumentException("legacyDN has zero length", "legacyDN");
			}
			Guid mbxGuid;
			legacyDN = this.TryToExtractArchiveOrAggregatedMailboxGuid(legacyDN, out mbxGuid);
			IGenericADUser genericADUser = this.directoryAccessor.FindMiniRecipientByProxyAddress(recipientSession, ProxyAddressPrefix.LegacyDN.GetProxyAddress(legacyDN, true), miniRecipientProperties, out miniRecipient);
			if (genericADUser == null)
			{
				throw new ObjectNotFoundException(ServerStrings.ADUserNotFound);
			}
			ADObjectId mdb;
			bool asArchive = this.UpdateArchiveStatus(mbxGuid, genericADUser, out mdb);
			Guid? aggregatedMailboxGuid = null;
			if (genericADUser.AggregatedMailboxGuids != null)
			{
				aggregatedMailboxGuid = (genericADUser.AggregatedMailboxGuids.Any((Guid mailbox) => mailbox == mbxGuid) ? new Guid?(mbxGuid) : null);
			}
			IMailboxLocation mailboxLocation = new OnDemandMailboxLocation(() => new MailboxDatabaseLocation(this.databaseLocationProvider.GetLocationInfo(mdb.ObjectGuid, false, (remotingOptions & RemotingOptions.AllowCrossSite) == RemotingOptions.AllowCrossSite)));
			ExchangePrincipal exchangePrincipal = this.InternalFromMiniRecipient(genericADUser, mdb, mailboxLocation, remotingOptions, asArchive, aggregatedMailboxGuid);
			if (mbxGuid != Guid.Empty && !exchangePrincipal.MailboxInfo.IsAggregated && !exchangePrincipal.MailboxInfo.IsArchive)
			{
				throw new ObjectNotFoundException(ServerStrings.AggregatedMailboxNotFound(mbxGuid.ToString()));
			}
			return exchangePrincipal;
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x0007A550 File Offset: 0x00078750
		public ExchangePrincipal FromADSystemMailbox(ADSessionSettings adSettings, ADSystemMailbox adSystemMailbox, Server server)
		{
			Util.ThrowOnNullArgument(adSettings, "adSettings");
			Util.ThrowOnNullArgument(adSystemMailbox, "adSystemMailbox");
			Util.ThrowOnNullArgument(server, "server");
			if (!server.IsMailboxServer)
			{
				throw new ArgumentException("Needs to be a Mailbox server", "server");
			}
			return this.FromADSystemMailbox(adSettings.CreateRecipientSession(null), new ADSystemMailboxGenericWrapper(adSystemMailbox), server.Fqdn, server.ExchangeLegacyDN);
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x0007A5B8 File Offset: 0x000787B8
		public ExchangePrincipal FromADSystemMailbox(IRecipientSession recipientSession, IGenericADUser adSystemMailbox, string serverFqdn, string serverLegacyDn)
		{
			Util.ThrowOnNullArgument(recipientSession, "recipientSession");
			Util.ThrowOnNullArgument(adSystemMailbox, "adSystemMailbox");
			ArgumentValidator.ThrowIfNullOrEmpty("serverFqdn", serverFqdn);
			ArgumentValidator.ThrowIfNullOrEmpty("serverLegacyDn", serverLegacyDn);
			if (adSystemMailbox.RecipientType != RecipientType.SystemMailbox)
			{
				throw new ArgumentException("User object doesn't represent SystemMailbox", "adSystemMailbox");
			}
			if (adSystemMailbox.MailboxDatabase == null || adSystemMailbox.MailboxDatabase.ObjectGuid == Guid.Empty)
			{
				throw new UserHasNoMailboxException();
			}
			return this.FromMailboxData(recipientSession.SessionSettings, Util.NullIf<string>(adSystemMailbox.DisplayName, string.Empty) ?? "Microsoft System Attendant", serverFqdn, serverLegacyDn, adSystemMailbox.LegacyDn, adSystemMailbox.MailboxGuid, adSystemMailbox.MailboxDatabase.ObjectGuid, adSystemMailbox.PrimarySmtpAddress.ToString(), adSystemMailbox.ObjectId, new List<CultureInfo>(), Array<Guid>.Empty, RecipientType.SystemMailbox, RemotingOptions.AllowCrossSite);
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x0007A699 File Offset: 0x00078899
		public ExchangePrincipal FromMailboxGuid(ADSessionSettings adSettings, Guid mailboxGuid, string domainController = null)
		{
			return this.FromMailboxGuid(adSettings, mailboxGuid, RemotingOptions.LocalConnectionsOnly, domainController);
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x0007A6A5 File Offset: 0x000788A5
		public ExchangePrincipal FromMailboxGuid(ADSessionSettings adSettings, Guid mailboxGuid, RemotingOptions remotingOptions, string domainController = null)
		{
			return this.FromMailboxGuid(adSettings, mailboxGuid, Guid.Empty, remotingOptions, domainController, false);
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x0007A6B8 File Offset: 0x000788B8
		public ExchangePrincipal FromMailboxGuid(ADSessionSettings adSettings, Guid mailboxGuid, Guid mdbGuid, RemotingOptions remotingOptions, string domainController = null, bool isContentIndexing = false)
		{
			Util.ThrowOnNullArgument(adSettings, "adSettings");
			return this.FromMailboxGuid(adSettings.CreateRecipientSession(domainController), mailboxGuid, mdbGuid, remotingOptions, isContentIndexing);
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x0007A6E8 File Offset: 0x000788E8
		public ExchangePrincipal FromMailboxGuid(IRecipientSession recipientSession, Guid mailboxGuid, Guid mdbGuid, RemotingOptions remotingOptions, bool isContentIndexing = false)
		{
			Util.ThrowOnNullArgument(recipientSession, "recipientSession");
			EnumValidator.ThrowIfInvalid<RemotingOptions>(remotingOptions, "remotingOptions");
			if (mailboxGuid == Guid.Empty)
			{
				throw new ArgumentException("Guid-less mailboxes are not supported by this factory method", "mailboxGuid");
			}
			IGenericADUser genericADUser = this.directoryAccessor.FindByExchangeGuid(recipientSession, mailboxGuid, false);
			if (genericADUser == null)
			{
				throw new ObjectNotFoundException(ServerStrings.ADUserNotFound);
			}
			ADObjectId mdb;
			bool asArchive = this.UpdateArchiveStatus(mailboxGuid, genericADUser, out mdb);
			if (mdbGuid != Guid.Empty)
			{
				mdb = new ADObjectId(mdbGuid);
			}
			return this.InternalFromADUser(genericADUser, mdb, null, remotingOptions, asArchive, isContentIndexing, new Guid?(mailboxGuid));
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x0007A779 File Offset: 0x00078979
		public ExchangePrincipal FromLocalServerMailboxGuid(ADSessionSettings adSettings, Guid mdbGuid, Guid mailboxGuid)
		{
			return this.FromLocalServerMailboxGuid(adSettings, mdbGuid, mailboxGuid, false);
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x0007A788 File Offset: 0x00078988
		public ExchangePrincipal FromLocalServerMailboxGuid(ADSessionSettings adSettings, Guid mdbGuid, Guid mailboxGuid, bool isContentIndexing)
		{
			Util.ThrowOnNullArgument(adSettings, "adSettings");
			Server localServer = this.directoryAccessor.GetLocalServer();
			if (!localServer.IsMailboxServer)
			{
				throw new InvalidOperationException("This method can only be called on a Mailbox server");
			}
			return this.FromLocalServerMailboxGuid(adSettings.CreateRecipientSession(null), mdbGuid, mailboxGuid, new DatabaseLocationInfo(localServer, false), isContentIndexing);
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x0007A7D9 File Offset: 0x000789D9
		public ExchangePrincipal FromLocalServerMailboxGuid(IRecipientSession recipientSession, Guid mdbGuid, Guid mailboxGuid, DatabaseLocationInfo databaseLocationInfo)
		{
			return this.FromLocalServerMailboxGuid(recipientSession, mdbGuid, mailboxGuid, databaseLocationInfo, false);
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x0007A7E8 File Offset: 0x000789E8
		public ExchangePrincipal FromLocalServerMailboxGuid(IRecipientSession recipientSession, Guid mdbGuid, Guid mailboxGuid, DatabaseLocationInfo databaseLocationInfo, bool isContentIndexing)
		{
			Util.ThrowOnNullArgument(recipientSession, "recipientSession");
			Util.ThrowOnNullArgument(databaseLocationInfo, "databaseLocationInfo");
			if (mdbGuid == Guid.Empty)
			{
				throw new ArgumentException("Should not be Empty", "mdbGuid");
			}
			if (mailboxGuid == Guid.Empty)
			{
				throw new ArgumentException("Guid-less mailboxes are not supported by this factory method", "mailboxGuid");
			}
			IGenericADUser genericADUser = this.directoryAccessor.FindByExchangeGuid(recipientSession, mailboxGuid, true);
			if (genericADUser == null)
			{
				throw new ObjectNotFoundException(ServerStrings.ADUserNotFound);
			}
			if (genericADUser.RecipientType == RecipientType.SystemMailbox)
			{
				return this.FromADSystemMailbox(recipientSession, genericADUser, databaseLocationInfo.ServerFqdn, databaseLocationInfo.ServerLegacyDN);
			}
			ADObjectId mdb;
			bool asArchive = this.UpdateArchiveStatus(genericADUser.RecipientType, mailboxGuid, genericADUser.ArchiveGuid, new ADObjectId(mdbGuid), genericADUser.ArchiveDatabase, out mdb);
			return this.InternalFromADUser(genericADUser, mdb, databaseLocationInfo, RemotingOptions.LocalConnectionsOnly, asArchive, isContentIndexing, new Guid?(mailboxGuid));
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x0007A8BC File Offset: 0x00078ABC
		public ExchangePrincipal FromDirectoryObjectId(IRecipientSession session, ADObjectId directoryEntry, RemotingOptions remoteOptions = RemotingOptions.LocalConnectionsOnly)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(directoryEntry, "directoryEntry");
			IGenericADUser genericADUser = this.directoryAccessor.FindByObjectId(session, directoryEntry);
			if (genericADUser == null)
			{
				throw new ObjectNotFoundException(ServerStrings.ADUserNotFound);
			}
			return this.InternalFromADUser(genericADUser, remoteOptions);
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x0007A908 File Offset: 0x00078B08
		private ExchangePrincipal InternalFromADUser(IGenericADUser user, RemotingOptions remotingOptions)
		{
			ADObjectId mdb;
			bool asArchive = this.UpdateArchiveStatus(user.MailboxGuid, user, out mdb);
			return this.InternalFromADUser(user, mdb, null, remotingOptions, asArchive, false, null);
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x0007A93C File Offset: 0x00078B3C
		private ExchangePrincipal InternalFromADUser(IGenericADUser user, ADObjectId mdb, DatabaseLocationInfo databaseLocationInfo, RemotingOptions remotingOptions)
		{
			return this.InternalFromADUser(user, mdb, databaseLocationInfo, remotingOptions, false, false, null);
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x0007A960 File Offset: 0x00078B60
		private ExchangePrincipal InternalFromADUser(IGenericADUser user, ADObjectId mdb, DatabaseLocationInfo databaseLocationInfo, RemotingOptions remotingOptions, bool asArchive, bool isContentIndexing = false, Guid? aggregatedMailboxGuid = null)
		{
			if (databaseLocationInfo == null && mdb != null)
			{
				databaseLocationInfo = this.databaseLocationProvider.GetLocationInfo(mdb.ObjectGuid, false, (remotingOptions & RemotingOptions.AllowCrossSite) == RemotingOptions.AllowCrossSite);
			}
			return this.CreateExchangePrincipal(user, mdb, this.CreateMailboxLocation(databaseLocationInfo), remotingOptions, asArchive, aggregatedMailboxGuid, this.databaseLocationProvider, isContentIndexing);
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x0007A9AC File Offset: 0x00078BAC
		public ExchangePrincipal FromMailboxData(ADSessionSettings adSettings, Guid mdbGuid, Guid mailboxGuid, string mailboxLegacyDN, string primarySmtpAddress, ICollection<CultureInfo> preferredCultures, bool bypassRemoteCheck = false)
		{
			return this.FromMailboxData(mailboxLegacyDN, adSettings, mdbGuid, mailboxGuid, mailboxLegacyDN, primarySmtpAddress, preferredCultures, bypassRemoteCheck);
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x0007A9CC File Offset: 0x00078BCC
		public ExchangePrincipal FromMailboxData(string displayName, ADSessionSettings adSettings, Guid mdbGuid, Guid mailboxGuid, string mailboxLegacyDN, string primarySmtpAddress, ICollection<CultureInfo> preferredCultures, bool bypassRemoteCheck = false)
		{
			return this.FromMailboxData(displayName, adSettings, mdbGuid, mailboxGuid, mailboxLegacyDN, primarySmtpAddress, preferredCultures, bypassRemoteCheck, RecipientType.Invalid, RecipientTypeDetails.None);
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x0007A9F0 File Offset: 0x00078BF0
		public ExchangePrincipal FromMailboxData(string displayName, ADSessionSettings adSettings, Guid mdbGuid, Guid mailboxGuid, string mailboxLegacyDN, string primarySmtpAddress, ICollection<CultureInfo> preferredCultures, bool bypassRemoteCheck, RecipientType recipientType, RecipientTypeDetails recipientTypeDetails)
		{
			Util.ThrowOnNullArgument(adSettings, "adSettings");
			Util.ThrowOnNullArgument(mailboxLegacyDN, "mailboxLegacyDN");
			Util.ThrowOnNullArgument(preferredCultures, "preferredCultures");
			if (mailboxLegacyDN.Length == 0)
			{
				throw new ArgumentException("Should not be empty", "mailboxLegacyDN");
			}
			if (mdbGuid == Guid.Empty)
			{
				throw new ArgumentException("Should not be Empty", "mdbGuid");
			}
			if (mailboxGuid == Guid.Empty)
			{
				throw new ArgumentException("Should not be Empty", "mailboxGuid");
			}
			return this.CreateExchangePrincipal(displayName, this.CreateMailboxLocation(this.databaseLocationProvider.GetLocationInfo(mdbGuid, false, false)), RemotingOptions.LocalConnectionsOnly, mailboxLegacyDN, mailboxGuid, mdbGuid, primarySmtpAddress, null, adSettings.CurrentOrganizationId, preferredCultures, bypassRemoteCheck, recipientType, recipientTypeDetails, null, false, null);
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x0007AAB4 File Offset: 0x00078CB4
		public ExchangePrincipal FromAnyVersionMailboxData(string displayName, Guid mailboxGuid, Guid mdbGuid, string primarySmtpAddress, string legacyExchangeDN, ADObjectId id, RecipientType recipientType, SecurityIdentifier masterAccountSid, OrganizationId organizationId)
		{
			return this.FromAnyVersionMailboxData(displayName, mailboxGuid, mdbGuid, primarySmtpAddress, legacyExchangeDN, id, recipientType, masterAccountSid, organizationId, false);
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x0007AAD8 File Offset: 0x00078CD8
		public ExchangePrincipal FromAnyVersionMailboxData(string displayName, Guid mailboxGuid, Guid mdbGuid, string primarySmtpAddress, string legacyExchangeDN, ADObjectId id, RecipientType recipientType, SecurityIdentifier masterAccountSid, OrganizationId organizationId, bool isArchive)
		{
			return this.FromAnyVersionMailboxData(displayName, mailboxGuid, mdbGuid, primarySmtpAddress, legacyExchangeDN, id, recipientType, masterAccountSid, organizationId, RemotingOptions.LocalConnectionsOnly, isArchive);
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x0007AB00 File Offset: 0x00078D00
		public ExchangePrincipal FromAnyVersionMailboxData(string displayName, Guid mailboxGuid, Guid mdbGuid, string primarySmtpAddress, string legacyExchangeDN, ADObjectId id, RecipientType recipientType, SecurityIdentifier masterAccountSid, OrganizationId organizationId, RemotingOptions remotingOptions, bool isArchive)
		{
			EnumValidator.ThrowIfInvalid<RecipientType>(recipientType, "recipientType");
			try
			{
				DatabaseLocationInfo locationInfo = this.databaseLocationProvider.GetLocationInfo(mdbGuid, false, (remotingOptions & RemotingOptions.AllowCrossSite) == RemotingOptions.AllowCrossSite);
				return this.CreateExchangePrincipal(displayName, this.CreateMailboxLocation(locationInfo), remotingOptions, legacyExchangeDN, mailboxGuid, mdbGuid, primarySmtpAddress, id, Array<CultureInfo>.Empty, recipientType, masterAccountSid, organizationId, isArchive);
			}
			catch (DatabaseNotFoundException)
			{
				ExTraceGlobals.StorageTracer.TraceError<Guid>(0L, "Database was not found for mailbox {0}.", mailboxGuid);
			}
			catch (UnableToFindServerForDatabaseException)
			{
				ExTraceGlobals.SessionTracer.TraceError<Guid>(0L, "Server was not found for Database {0}.", mdbGuid);
			}
			return null;
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x0007ABA4 File Offset: 0x00078DA4
		public ExchangePrincipal FromMailboxData(ADSessionSettings adSettings, string displayName, string serverFqdn, string serverLegacyDN, string mailboxLegacyDN, Guid mailboxGuid, Guid mdbGuid, string primarySmtpAddress, ICollection<CultureInfo> preferredCultures, IEnumerable<Guid> aggregatedMailboxGuids)
		{
			Util.ThrowOnNullArgument(adSettings, "adSettings");
			return this.FromMailboxData(adSettings, displayName, serverFqdn, serverLegacyDN, mailboxLegacyDN, mailboxGuid, mdbGuid, primarySmtpAddress, null, preferredCultures, aggregatedMailboxGuids, RecipientType.Invalid, RemotingOptions.AllowCrossSite);
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x0007ABEC File Offset: 0x00078DEC
		public ExchangePrincipal FromMailboxData(ADSessionSettings adSessionSettings, string displayName, string serverFqdn, string serverLegacyDN, string mailboxLegacyDN, Guid mailboxGuid, Guid mdbGuid, string primarySmtpAddress, ADObjectId id, ICollection<CultureInfo> preferredCultures, IEnumerable<Guid> aggregatedMailboxGuids, RecipientType userRecipientType = RecipientType.Invalid, RemotingOptions remotingOptions = RemotingOptions.AllowCrossSite)
		{
			Util.ThrowOnNullArgument(displayName, "displayName");
			Util.ThrowOnNullArgument(serverFqdn, "serverFqdn");
			Util.ThrowOnNullArgument(serverLegacyDN, "serverLegacyDN");
			Util.ThrowOnNullArgument(preferredCultures, "preferredCultures");
			Util.ThrowOnNullArgument(aggregatedMailboxGuids, "aggregatedMailboxGuids");
			if (displayName.Length == 0)
			{
				throw new ArgumentException("displayName has zero length", "displayName");
			}
			if (serverFqdn.Length == 0)
			{
				throw new ArgumentException("serverFqdn has zero length", "serverFqdn");
			}
			if (serverLegacyDN.Length == 0)
			{
				throw new ArgumentException("serverLegacyDN has zero length", "serverLegacyDN");
			}
			if (mdbGuid == Guid.Empty)
			{
				throw new ArgumentException("Should not be Empty", "mdbGuid");
			}
			if (string.IsNullOrEmpty(mailboxLegacyDN) && mailboxGuid == Guid.Empty)
			{
				throw new ArgumentException(ServerStrings.ExchangePrincipalFromMailboxDataError);
			}
			Guid? aggregatedMailboxGuid = aggregatedMailboxGuids.Any((Guid mailbox) => mailbox == mailboxGuid) ? new Guid?(mailboxGuid) : null;
			return this.CreateExchangePrincipal(displayName, this.CreateMailboxLocation(new DatabaseLocationInfo(serverFqdn, serverLegacyDN, null, null, false)), remotingOptions, mailboxLegacyDN, mailboxGuid, mdbGuid, primarySmtpAddress, id, (adSessionSettings != null) ? adSessionSettings.CurrentOrganizationId : null, preferredCultures, false, userRecipientType, aggregatedMailboxGuid);
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x0007AD39 File Offset: 0x00078F39
		public ExchangePrincipal FromMailboxData(Guid mailboxGuid, Guid mdbGuid, ICollection<CultureInfo> preferredCultures)
		{
			return this.FromMailboxData(mailboxGuid, mdbGuid, null, preferredCultures, RemotingOptions.LocalConnectionsOnly);
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x0007AD46 File Offset: 0x00078F46
		public ExchangePrincipal FromMailboxData(Guid mailboxGuid, Guid mdbGuid, OrganizationId organizationId, ICollection<CultureInfo> preferredCultures)
		{
			return this.FromMailboxData(mailboxGuid, mdbGuid, organizationId, preferredCultures, RemotingOptions.LocalConnectionsOnly);
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x0007AD54 File Offset: 0x00078F54
		public ExchangePrincipal FromMailboxData(Guid mailboxGuid, Guid mdbGuid, ICollection<CultureInfo> preferredCultures, RemotingOptions remotingOptions)
		{
			return this.FromMailboxData(mailboxGuid, mdbGuid, null, preferredCultures, remotingOptions);
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x0007AD62 File Offset: 0x00078F62
		public ExchangePrincipal FromMailboxData(Guid mailboxGuid, Guid mdbGuid, OrganizationId organizationId, ICollection<CultureInfo> preferredCultures, RemotingOptions remotingOptions)
		{
			return this.FromMailboxData(mailboxGuid, mdbGuid, organizationId, preferredCultures, remotingOptions, null);
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x0007AD74 File Offset: 0x00078F74
		public ExchangePrincipal FromMailboxData(Guid mailboxGuid, Guid mdbGuid, OrganizationId organizationId, ICollection<CultureInfo> preferredCultures, RemotingOptions remotingOptions, DatabaseLocationInfo databaseLocationInfo)
		{
			EnumValidator.ThrowIfInvalid<RemotingOptions>(remotingOptions, "remotingOptions");
			if (mailboxGuid == Guid.Empty)
			{
				throw new ArgumentException("Should not be empty", "mailboxGuid");
			}
			if (mdbGuid == Guid.Empty)
			{
				throw new ArgumentException("Should not be empty", "mdbGuid");
			}
			return this.CreateExchangePrincipal(mailboxGuid.ToString(), this.CreateMailboxLocation(databaseLocationInfo ?? this.databaseLocationProvider.GetLocationInfo(mdbGuid, false, (remotingOptions & RemotingOptions.AllowCrossSite) == RemotingOptions.AllowCrossSite)), remotingOptions, string.Empty, mailboxGuid, mdbGuid, string.Empty, null, organizationId, preferredCultures, false, RecipientType.Invalid, null);
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x0007AE19 File Offset: 0x00079019
		public ExchangePrincipal FromMiniRecipient(StorageMiniRecipient miniRecipient)
		{
			return this.FromMiniRecipient(miniRecipient, RemotingOptions.LocalConnectionsOnly);
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x0007AE23 File Offset: 0x00079023
		public ExchangePrincipal FromMiniRecipient(StorageMiniRecipient miniRecipient, RemotingOptions remotingOptions)
		{
			return this.FromMiniRecipient(new MiniRecipientGenericWrapper(miniRecipient), remotingOptions);
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x0007AE34 File Offset: 0x00079034
		public ExchangePrincipal FromMiniRecipient(IGenericADUser miniRecipient, RemotingOptions remotingOptions)
		{
			EnumValidator.ThrowIfInvalid<RemotingOptions>(remotingOptions, "remotingOptions");
			Util.ThrowOnNullArgument(miniRecipient, "miniRecipient");
			ADObjectId mdb;
			bool asArchive = this.UpdateArchiveStatus(miniRecipient.MailboxGuid, miniRecipient, out mdb);
			return this.InternalFromMiniRecipient(miniRecipient, mdb, null, remotingOptions, asArchive, null);
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x0007AE7C File Offset: 0x0007907C
		private ExchangePrincipal InternalFromMiniRecipient(IGenericADUser adUser, ADObjectId mdb, IMailboxLocation mailboxLocation, RemotingOptions remotingOptions, bool asArchive, Guid? aggregatedMailboxGuid = null)
		{
			if (adUser == null)
			{
				throw new ObjectNotFoundException(ServerStrings.ADUserNotFound);
			}
			if (mdb == null)
			{
				mdb = adUser.MailboxDatabase;
			}
			if (mailboxLocation == null && mdb != null)
			{
				mailboxLocation = this.CreateMailboxLocation(this.databaseLocationProvider.GetLocationInfo(mdb.ObjectGuid, false, (remotingOptions & RemotingOptions.AllowCrossSite) == RemotingOptions.AllowCrossSite));
			}
			return this.CreateExchangePrincipal(adUser, mdb, mailboxLocation, remotingOptions, asArchive, aggregatedMailboxGuid, this.databaseLocationProvider, false);
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x0007AEE4 File Offset: 0x000790E4
		private ExchangePrincipal CreateExchangePrincipal(IGenericADUser user, ADObjectId mdb, IMailboxLocation mailboxLocation, RemotingOptions remotingOptions, bool asArchive, Guid? aggregatedMailboxGuid, IDatabaseLocationProvider databaseLocationProvider, bool isContentIndexing = false)
		{
			ExchangePrincipalBuilder exchangePrincipalBuilder = ((remotingOptions & RemotingOptions.AllowHybridAccess) == RemotingOptions.AllowHybridAccess) ? new RemoteUserMailboxPrincipalBuilder(user) : this.GetExchangePrincipalBuilder(user);
			exchangePrincipalBuilder.SetRemotingOptions(remotingOptions);
			exchangePrincipalBuilder.SetDatabaseLocationProvider(databaseLocationProvider);
			if (!mdb.IsNullOrEmpty())
			{
				exchangePrincipalBuilder.SetSelectedMailboxDatabase(mdb.ObjectGuid);
			}
			if (mailboxLocation != null)
			{
				exchangePrincipalBuilder.SetSelectedMailboxLocation(mailboxLocation);
			}
			if (asArchive)
			{
				exchangePrincipalBuilder.SelectArchiveMailbox();
			}
			exchangePrincipalBuilder.BypassRecipientTypeValidation(isContentIndexing);
			if (aggregatedMailboxGuid != null && aggregatedMailboxGuid != Guid.Empty)
			{
				exchangePrincipalBuilder.SelectMailbox(aggregatedMailboxGuid.Value);
			}
			return exchangePrincipalBuilder.Build();
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x0007AF88 File Offset: 0x00079188
		private ExchangePrincipal CreateExchangePrincipal(string displayName, IMailboxLocation mailboxLocation, RemotingOptions remotingOptions, string mailboxLegacyDN, Guid mailboxGuid, Guid mdbGuid, string primarySmtpAddress, ADObjectId id, OrganizationId orgId, IEnumerable<CultureInfo> preferredCultures, bool bypassRemoteCheck = false, RecipientType userRecipientType = RecipientType.Invalid, Guid? aggregatedMailboxGuid = null)
		{
			return this.CreateExchangePrincipal(displayName, mailboxLocation, remotingOptions, mailboxLegacyDN, mailboxGuid, mdbGuid, primarySmtpAddress, id, orgId, preferredCultures, bypassRemoteCheck, userRecipientType, RecipientTypeDetails.None, null, false, aggregatedMailboxGuid);
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x0007AFB8 File Offset: 0x000791B8
		private ExchangePrincipal CreateExchangePrincipal(string displayName, IMailboxLocation mailboxLocation, RemotingOptions remotingOptions, string mailboxLegacyDN, Guid mailboxGuid, Guid mdbGuid, string primarySmtpAddress, ADObjectId id, OrganizationId orgId, IEnumerable<CultureInfo> preferredCultures, bool bypassRemoteCheck, RecipientType recipientType, RecipientTypeDetails recipientTypeDetails, SecurityIdentifier masterAccountSid = null, bool isArchive = false, Guid? aggregatedMailboxGuid = null)
		{
			ADObjectId adobjectId = new ADObjectId(mdbGuid);
			IGenericADUser recipient = new GenericADUser
			{
				MailboxDatabase = (isArchive ? null : adobjectId),
				ArchiveDatabase = (isArchive ? adobjectId : null),
				LegacyDn = mailboxLegacyDN,
				OrganizationId = orgId,
				DisplayName = displayName,
				PrimarySmtpAddress = new SmtpAddress(primarySmtpAddress),
				MailboxGuid = (isArchive ? Guid.Empty : mailboxGuid),
				ArchiveGuid = (isArchive ? mailboxGuid : Guid.Empty),
				Languages = preferredCultures,
				RecipientType = recipientType,
				RecipientTypeDetails = recipientTypeDetails,
				ObjectId = id,
				MasterAccountSid = masterAccountSid,
				AggregatedMailboxGuids = ((aggregatedMailboxGuid != null) ? new Guid[]
				{
					aggregatedMailboxGuid.Value
				} : Array<Guid>.Empty)
			};
			ExchangePrincipalBuilder exchangePrincipalBuilder = this.GetExchangePrincipalBuilder(recipient);
			exchangePrincipalBuilder.SetRemotingOptions(remotingOptions);
			exchangePrincipalBuilder.BypassRecipientTypeValidation(true);
			if (mailboxLocation != null)
			{
				exchangePrincipalBuilder.SetSelectedMailboxLocation(mailboxLocation);
			}
			if (isArchive)
			{
				exchangePrincipalBuilder.SelectArchiveMailbox();
			}
			if (aggregatedMailboxGuid != null && aggregatedMailboxGuid != Guid.Empty)
			{
				exchangePrincipalBuilder.SelectMailbox(aggregatedMailboxGuid.Value);
			}
			return exchangePrincipalBuilder.Build();
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x0007B104 File Offset: 0x00079304
		private ExchangePrincipal CreateExchangePrincipal(string displayName, IMailboxLocation mailboxLocation, RemotingOptions remotingOptions, string mailboxLegacyDN, Guid mailboxGuid, Guid mdbGuid, string primarySmtpAddress, ADObjectId id, IEnumerable<CultureInfo> preferredCultures, RecipientType recipientType, SecurityIdentifier masterAccountSid, OrganizationId organizationId, bool isArchive = false)
		{
			return this.CreateExchangePrincipal(displayName, mailboxLocation, remotingOptions, mailboxLegacyDN, mailboxGuid, mdbGuid, primarySmtpAddress, id, organizationId, preferredCultures, false, recipientType, RecipientTypeDetails.None, masterAccountSid, isArchive, null);
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x0007B13C File Offset: 0x0007933C
		private IMailboxLocation CreateMailboxLocation(DatabaseLocationInfo databaseLocationInfo)
		{
			IMailboxLocation result = null;
			if (databaseLocationInfo != null)
			{
				result = new MailboxDatabaseLocation(databaseLocationInfo);
			}
			return result;
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x0007B158 File Offset: 0x00079358
		private string TryToExtractArchiveOrAggregatedMailboxGuid(string legacyDN, out Guid mbxGuid)
		{
			mbxGuid = Guid.Empty;
			int num = legacyDN.LastIndexOf("/guid=", StringComparison.OrdinalIgnoreCase);
			if (num != -1 && num + 6 + 1 < legacyDN.Length)
			{
				string g = legacyDN.Substring(num + 6);
				try
				{
					mbxGuid = new Guid(g);
				}
				catch (FormatException)
				{
					return legacyDN;
				}
				catch (OverflowException)
				{
					return legacyDN;
				}
				return legacyDN.Remove(num);
			}
			return legacyDN;
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x0007B1D8 File Offset: 0x000793D8
		private bool UpdateArchiveStatus(Guid mailboxToUse, IGenericADUser user, out ADObjectId databaseToUse)
		{
			databaseToUse = null;
			bool flag = this.UpdateArchiveStatus(user.RecipientType, mailboxToUse, user.ArchiveGuid, user.MailboxDatabase, user.ArchiveDatabase, out databaseToUse);
			if (!flag)
			{
				databaseToUse = user.MailboxDatabase;
				if (user.MailboxLocations != null)
				{
					foreach (IMailboxLocationInfo mailboxLocationInfo in user.MailboxLocations)
					{
						if (mailboxLocationInfo.MailboxGuid.Equals(mailboxToUse))
						{
							databaseToUse = mailboxLocationInfo.DatabaseLocation;
							break;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x0007B274 File Offset: 0x00079474
		private bool UpdateArchiveStatus(RecipientType recipientType, Guid mailboxToUse, Guid archiveGuid, ADObjectId primaryDatabase, ADObjectId archiveDatabase, out ADObjectId databaseToUse)
		{
			databaseToUse = primaryDatabase;
			bool flag = this.IsArchiveMailUser(recipientType, archiveGuid, archiveDatabase);
			if (flag)
			{
				ExTraceGlobals.SessionTracer.TraceDebug<Guid>(0L, "ExchangePrincipal::UpdateArchiveStatus. Recipient is an archive mail user. ArchiveGuid: {0}", archiveGuid);
			}
			else
			{
				flag = (mailboxToUse != Guid.Empty && archiveGuid != Guid.Empty && archiveGuid.Equals(mailboxToUse));
			}
			if (flag && archiveDatabase != null)
			{
				databaseToUse = archiveDatabase;
			}
			return flag;
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x0007B2DB File Offset: 0x000794DB
		private void CheckNoCrossPremiseAccess(RemotingOptions options)
		{
			if ((options & RemotingOptions.AllowCrossPremise) == RemotingOptions.AllowCrossPremise)
			{
				throw new NotSupportedException("RemotingOptions.AllowCrossPremise is not supported for this overload of ExchangePrincipal.From().");
			}
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x0007B2F0 File Offset: 0x000794F0
		private bool IsArchiveMailUser(RecipientType recipientType, Guid archiveGuid, ADObjectId archiveDatabase)
		{
			return recipientType == RecipientType.MailUser && !archiveGuid.Equals(Guid.Empty) && archiveDatabase != null && !archiveDatabase.ObjectGuid.Equals(Guid.Empty);
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x0007B32C File Offset: 0x0007952C
		private ExchangePrincipalBuilder GetExchangePrincipalBuilder(IGenericADUser recipient)
		{
			if (recipient is ADGroupGenericWrapper)
			{
				return new GroupPrincipalBuilder(recipient);
			}
			return new UserPrincipalBuilder(recipient);
		}

		// Token: 0x04001269 RID: 4713
		private readonly IDirectoryAccessor directoryAccessor;

		// Token: 0x0400126A RID: 4714
		private readonly IDatabaseLocationProvider databaseLocationProvider;
	}
}
