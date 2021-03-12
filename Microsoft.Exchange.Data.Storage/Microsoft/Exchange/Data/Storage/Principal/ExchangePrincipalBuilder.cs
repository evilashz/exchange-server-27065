using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.Principal
{
	// Token: 0x02000269 RID: 617
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ExchangePrincipalBuilder
	{
		// Token: 0x06001949 RID: 6473 RVA: 0x000791A4 File Offset: 0x000773A4
		public ExchangePrincipalBuilder(Func<IADUserFinder, IRecipientSession, IGenericADUser> findADUser)
		{
			ArgumentValidator.ThrowIfNull("findADUser", findADUser);
			this.findADUser = findADUser;
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x000791C5 File Offset: 0x000773C5
		public ExchangePrincipalBuilder(IGenericADUser adUser)
		{
			ArgumentValidator.ThrowIfNull("adUser", adUser);
			this.adUser = adUser;
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x000791E6 File Offset: 0x000773E6
		public void SetADRecipientSession(IRecipientSession recipientSession)
		{
			ArgumentValidator.ThrowIfNull("recipientSession", recipientSession);
			this.adRecipientSession = recipientSession;
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x000791FA File Offset: 0x000773FA
		public void SetADUserFinder(IADUserFinder adUserFinder)
		{
			ArgumentValidator.ThrowIfNull("adUserFinder", adUserFinder);
			this.adUserFinder = adUserFinder;
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x0007920E File Offset: 0x0007740E
		public void SetDatabaseLocationProvider(IDatabaseLocationProvider databaseLocationProvider)
		{
			ArgumentValidator.ThrowIfNull("databaseLocationProvider", databaseLocationProvider);
			this.databaseLocationProvider = databaseLocationProvider;
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x00079222 File Offset: 0x00077422
		public void SetRemotingOptions(RemotingOptions remotingOptions)
		{
			EnumValidator<RemotingOptions>.ThrowIfInvalid(remotingOptions);
			this.remotingOptions = remotingOptions;
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x00079231 File Offset: 0x00077431
		public void SelectArchiveMailbox()
		{
			this.isArchiveMailboxSelected = true;
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x0007923A File Offset: 0x0007743A
		public void SelectMailbox(Guid mailboxGuid)
		{
			ArgumentValidator.ThrowIfEmpty("mailboxGuid", mailboxGuid);
			this.selectedMailboxGuid = new Guid?(mailboxGuid);
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x00079253 File Offset: 0x00077453
		[Obsolete("Use SelectMailboxGuid")]
		public void SelectAggregatedMailbox(Guid aggregatedMailboxGuid)
		{
			ArgumentValidator.ThrowIfEmpty("aggregatedMailboxGuid", aggregatedMailboxGuid);
			this.selectedMailboxGuid = new Guid?(aggregatedMailboxGuid);
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x0007926C File Offset: 0x0007746C
		public void SetSelectedMailboxDatabase(Guid mailboxDatabaseGuid)
		{
			ArgumentValidator.ThrowIfEmpty("mailboxDatabaseGuid", mailboxDatabaseGuid);
			this.selectedMailboxDatabaseGuid = mailboxDatabaseGuid;
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x00079280 File Offset: 0x00077480
		public void SetSelectedMailboxLocation(IMailboxLocation mailboxLocation)
		{
			ArgumentValidator.ThrowIfNull("mailboxLocation", mailboxLocation);
			this.selectedMailboxLocation = mailboxLocation;
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x00079294 File Offset: 0x00077494
		public void UseOnDemandLocation(bool useOnDemandLocation)
		{
			this.useOnDemandLocation = useOnDemandLocation;
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x0007929D File Offset: 0x0007749D
		public void BypassRecipientTypeValidation(bool bypassRecipientTypeValidation)
		{
			this.bypassRecipientTypeValidation = bypassRecipientTypeValidation;
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x000792BC File Offset: 0x000774BC
		public ExchangePrincipal Build()
		{
			IGenericADUser aduser = this.GetADUser();
			ExchangePrincipal exchangePrincipal = this.BuildExchangePrincipal(aduser, this.isArchiveMailboxSelected, new ADObjectId(this.selectedMailboxDatabaseGuid), this.selectedMailboxGuid, new MailboxConfiguration(aduser), this.selectedMailboxLocation, (ADObjectId database) => this.BuildMailboxLocation(this.GetDatabaseLocationProvider(), database, this.remotingOptions), this.remotingOptions);
			if ((this.remotingOptions & RemotingOptions.AllowHybridAccess) != RemotingOptions.AllowHybridAccess && exchangePrincipal.MailboxInfo != null && exchangePrincipal.MailboxInfo.IsRemote)
			{
				this.ThrowIfRemoteMailboxNotAllowed();
			}
			else if (!this.bypassRecipientTypeValidation && !this.IsRecipientTypeSupported(aduser))
			{
				throw new AdUserNotFoundException(ServerStrings.ADUserNotFound);
			}
			return exchangePrincipal;
		}

		// Token: 0x06001957 RID: 6487
		protected abstract ExchangePrincipal BuildPrincipal(IGenericADUser recipient, IEnumerable<IMailboxInfo> allMailboxes, Func<IMailboxInfo, bool> mailboxSelector, RemotingOptions remotingOptions);

		// Token: 0x06001958 RID: 6488 RVA: 0x00079357 File Offset: 0x00077557
		private IGenericADUser GetADUser()
		{
			if (this.adUser == null)
			{
				this.adUser = this.findADUser(this.GetADUserFinder(), this.GetADRecipientSession());
			}
			if (this.adUser == null)
			{
				throw new ObjectNotFoundException(ServerStrings.ADUserNotFound);
			}
			return this.adUser;
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x00079397 File Offset: 0x00077597
		private IADUserFinder GetADUserFinder()
		{
			if (this.adUserFinder != null)
			{
				return this.adUserFinder;
			}
			return this.GetDefaultADUserFinder();
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x000793AE File Offset: 0x000775AE
		private IADUserFinder GetDefaultADUserFinder()
		{
			if (ExchangePrincipalBuilder.DefaultADUserFinder == null)
			{
				ExchangePrincipalBuilder.DefaultADUserFinder = new DirectoryAccessor();
			}
			return ExchangePrincipalBuilder.DefaultADUserFinder;
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x000793C6 File Offset: 0x000775C6
		private IDatabaseLocationProvider GetDatabaseLocationProvider()
		{
			if (this.databaseLocationProvider != null)
			{
				return this.databaseLocationProvider;
			}
			return this.GetDefaultDatabaseLocationProvider();
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x000793DD File Offset: 0x000775DD
		private IDatabaseLocationProvider GetDefaultDatabaseLocationProvider()
		{
			if (ExchangePrincipalBuilder.DefaultDatabaseLocationProvider == null)
			{
				ExchangePrincipalBuilder.DefaultDatabaseLocationProvider = new DatabaseLocationProvider();
			}
			return ExchangePrincipalBuilder.DefaultDatabaseLocationProvider;
		}

		// Token: 0x0600195D RID: 6493
		protected abstract bool IsRecipientTypeSupported(IGenericADUser user);

		// Token: 0x0600195E RID: 6494 RVA: 0x0007948C File Offset: 0x0007768C
		private ExchangePrincipal BuildExchangePrincipal(IGenericADUser user, bool isSelectedMailboxArchive, ADObjectId selectedDatabaseId, Guid? selectedMailboxGuid, MailboxConfiguration mailboxConfiguration, IMailboxLocation selectedMailboxLocation, Func<ADObjectId, IMailboxLocation> locationFactory, RemotingOptions remotingOptions)
		{
			List<IMailboxInfo> list = new List<IMailboxInfo>();
			if (user.ArchiveGuid != Guid.Empty)
			{
				IMailboxInfo mailboxInfo5 = this.BuildMailboxInfo(user.ArchiveGuid, user.ArchiveDatabase, isSelectedMailboxArchive, selectedDatabaseId, selectedMailboxLocation, user, mailboxConfiguration, locationFactory);
				if (mailboxInfo5 != null)
				{
					list.Add(mailboxInfo5);
				}
			}
			bool flag = false;
			if (selectedMailboxGuid != null && selectedMailboxGuid.Value != Guid.Empty)
			{
				if (user.MailboxLocations != null)
				{
					flag = user.MailboxLocations.Any((IMailboxLocationInfo mailboxInfo) => mailboxInfo.MailboxGuid == selectedMailboxGuid.Value);
				}
				if (!flag && user.AggregatedMailboxGuids != null)
				{
					flag = user.AggregatedMailboxGuids.Any((Guid mailbox) => mailbox == selectedMailboxGuid.Value);
				}
			}
			if (user.MailboxGuid != Guid.Empty)
			{
				bool isSelected = (selectedMailboxGuid != null && user.MailboxGuid.Equals(selectedMailboxGuid.Value)) || !flag || !isSelectedMailboxArchive;
				IMailboxInfo mailboxInfo2 = this.BuildMailboxInfo(user.MailboxGuid, user.MailboxDatabase, isSelected, selectedDatabaseId, selectedMailboxLocation, user, mailboxConfiguration, locationFactory);
				if (mailboxInfo2 != null)
				{
					list.Add(mailboxInfo2);
				}
			}
			if (user.MailboxLocations != null)
			{
				foreach (IMailboxLocationInfo mailboxLocationInfo in user.MailboxLocations)
				{
					IMailboxInfo mailboxInfo3 = this.BuildMailboxInfo(mailboxLocationInfo.MailboxGuid, mailboxLocationInfo.DatabaseLocation, mailboxLocationInfo.MailboxLocationType.Equals(MailboxLocationType.MainArchive) ? isSelectedMailboxArchive : (mailboxLocationInfo.MailboxGuid == selectedMailboxGuid), mailboxLocationInfo.DatabaseLocation, selectedMailboxLocation, user, mailboxConfiguration, locationFactory);
					if (mailboxInfo3 != null)
					{
						list.Add(mailboxInfo3);
					}
				}
			}
			if (user.AggregatedMailboxGuids != null)
			{
				foreach (Guid guid in user.AggregatedMailboxGuids)
				{
					IMailboxInfo mailboxInfo4 = this.BuildMailboxInfo(guid, user.MailboxDatabase, selectedMailboxGuid == guid, selectedDatabaseId, selectedMailboxLocation, user, mailboxConfiguration, locationFactory);
					if (mailboxInfo4 != null)
					{
						list.Add(mailboxInfo4);
					}
				}
			}
			Func<IMailboxInfo, bool> mailboxSelector;
			if (isSelectedMailboxArchive)
			{
				mailboxSelector = ((IMailboxInfo mailbox) => mailbox.MailboxType == MailboxLocationType.MainArchive);
			}
			else if (selectedMailboxGuid != null && selectedMailboxGuid != Guid.Empty)
			{
				mailboxSelector = ((IMailboxInfo mailbox) => mailbox.MailboxGuid == selectedMailboxGuid);
			}
			else
			{
				mailboxSelector = ((IMailboxInfo mailbox) => mailbox.MailboxGuid == user.MailboxGuid && mailbox.MailboxType == MailboxLocationType.Primary);
			}
			return this.BuildPrincipal(user, list, mailboxSelector, remotingOptions);
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x00079860 File Offset: 0x00077A60
		private IMailboxInfo BuildMailboxInfo(Guid mailboxGuid, ADObjectId mailboxDatabase, bool isSelected, ADObjectId selectedMailboxDatabase, IMailboxLocation selectedMailboxLocation, IGenericADUser adUser, IMailboxConfiguration configuration, Func<ADObjectId, IMailboxLocation> locationFactory)
		{
			ADObjectId adobjectId = mailboxDatabase;
			IMailboxLocation mailboxLocation = null;
			if (isSelected)
			{
				if (!selectedMailboxDatabase.IsNullOrEmpty())
				{
					adobjectId = selectedMailboxDatabase;
				}
				mailboxLocation = selectedMailboxLocation;
			}
			try
			{
				return new MailboxInfo(mailboxGuid, adobjectId, adUser, configuration, mailboxLocation ?? locationFactory(adobjectId));
			}
			catch (ObjectNotFoundException)
			{
			}
			return null;
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x000798E8 File Offset: 0x00077AE8
		private IMailboxLocation BuildMailboxLocation(IDatabaseLocationProvider databaseLocationProvider, ADObjectId databaseId, RemotingOptions remotingOptions)
		{
			if (databaseId.IsNullOrEmpty())
			{
				return MailboxDatabaseLocation.Unknown;
			}
			Func<IMailboxLocation> func = () => new MailboxDatabaseLocation(databaseLocationProvider.GetLocationInfo(databaseId.ObjectGuid, false, (remotingOptions & RemotingOptions.AllowCrossSite) == RemotingOptions.AllowCrossSite));
			if (this.useOnDemandLocation)
			{
				return new OnDemandMailboxLocation(func);
			}
			return func();
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x0007994C File Offset: 0x00077B4C
		private IRecipientSession GetADRecipientSession()
		{
			if (this.adRecipientSession == null)
			{
				this.adRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, true, ConsistencyMode.IgnoreInvalid, null, ADSessionSettings.FromRootOrgScopeSet(), 588, "GetADRecipientSession", "f:\\15.00.1497\\sources\\dev\\data\\src\\storage\\ExchangePrincipal\\ExchangePrincipalBuilder.cs");
			}
			return this.adRecipientSession;
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x0007998F File Offset: 0x00077B8F
		private void ThrowIfRemoteMailboxNotAllowed()
		{
			if ((this.remotingOptions & RemotingOptions.AllowCrossPremise) != RemotingOptions.AllowCrossPremise)
			{
				throw new UserHasNoMailboxException();
			}
		}

		// Token: 0x04001253 RID: 4691
		private static IADUserFinder DefaultADUserFinder;

		// Token: 0x04001254 RID: 4692
		private static IDatabaseLocationProvider DefaultDatabaseLocationProvider;

		// Token: 0x04001255 RID: 4693
		private IGenericADUser adUser;

		// Token: 0x04001256 RID: 4694
		private IADUserFinder adUserFinder;

		// Token: 0x04001257 RID: 4695
		private IDatabaseLocationProvider databaseLocationProvider;

		// Token: 0x04001258 RID: 4696
		private IRecipientSession adRecipientSession;

		// Token: 0x04001259 RID: 4697
		private Guid selectedMailboxDatabaseGuid;

		// Token: 0x0400125A RID: 4698
		private IMailboxLocation selectedMailboxLocation;

		// Token: 0x0400125B RID: 4699
		private RemotingOptions remotingOptions;

		// Token: 0x0400125C RID: 4700
		private bool useOnDemandLocation = true;

		// Token: 0x0400125D RID: 4701
		private bool bypassRecipientTypeValidation;

		// Token: 0x0400125E RID: 4702
		private bool isArchiveMailboxSelected;

		// Token: 0x0400125F RID: 4703
		private Guid? selectedMailboxGuid;

		// Token: 0x04001260 RID: 4704
		private readonly Func<IADUserFinder, IRecipientSession, IGenericADUser> findADUser;
	}
}
