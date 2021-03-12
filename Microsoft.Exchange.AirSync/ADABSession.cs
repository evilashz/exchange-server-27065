using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ABProviderFramework;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory.ABProviderFramework;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000AD RID: 173
	internal sealed class ADABSession : ABSession
	{
		// Token: 0x06000967 RID: 2407 RVA: 0x00037223 File Offset: 0x00035423
		public ADABSession(OrganizationId organizationId, ADObjectId searchRoot, int lcid, ConsistencyMode consistencyMode, ClientSecurityContext clientSecurityContext) : base(ExTraceGlobals.ActiveDirectoryTracer)
		{
			this.organizationId = organizationId;
			this.searchRoot = searchRoot;
			this.lcid = lcid;
			this.consistencyMode = consistencyMode;
			this.clientSecurityContext = clientSecurityContext;
			this.recipientSession = null;
			this.configurationSession = null;
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x00037264 File Offset: 0x00035464
		public static ABSession Create(IABSessionSettings sessionSettings)
		{
			if (sessionSettings == null)
			{
				throw new ArgumentNullException("sessionSettings");
			}
			ADABSession adabsession = null;
			bool flag = false;
			try
			{
				adabsession = new ADABSession(sessionSettings.Get<OrganizationId>("OrganizationId"), sessionSettings.Get<ADObjectId>("SearchRoot"), sessionSettings.Get<int>("Lcid"), sessionSettings.Get<ConsistencyMode>("ConsistencyMode"), sessionSettings.Get<ClientSecurityContext>("ClientSecurityContext"));
				flag = true;
			}
			finally
			{
				if (!flag && adabsession != null)
				{
					adabsession.Dispose();
					adabsession = null;
				}
			}
			return adabsession;
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x000372E4 File Offset: 0x000354E4
		public override ABProviderCapabilities ProviderCapabilities
		{
			get
			{
				return ADABSession.providerCapabilities;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x000372EB File Offset: 0x000354EB
		protected override string InternalProviderName
		{
			get
			{
				return "AD";
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x000372F2 File Offset: 0x000354F2
		private IConfigurationSession ConfigurationSession
		{
			get
			{
				if (this.configurationSession == null)
				{
					this.configurationSession = this.CreateConfigurationSession();
				}
				return this.configurationSession;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x0003730E File Offset: 0x0003550E
		private IRecipientSession RecipientSession
		{
			get
			{
				if (this.recipientSession == null)
				{
					if (this.searchRoot != null)
					{
						this.recipientSession = this.CreateRecipientSession();
					}
					else
					{
						this.recipientSession = this.CreateGalScopedRecipientSession(this.FindGlobalAddressList());
					}
				}
				return this.recipientSession;
			}
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x00037346 File Offset: 0x00035546
		internal void SetRecipientSessionForTesting(IRecipientSession recipientSession)
		{
			this.recipientSession = recipientSession;
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x00037350 File Offset: 0x00035550
		protected override ABObject InternalFindById(ABObjectId id)
		{
			ADABObjectId adabobjectId = (ADABObjectId)id;
			ADRecipient recipient;
			try
			{
				recipient = this.RecipientSession.Read(adabobjectId.NativeId);
			}
			catch (DataSourceOperationException ex)
			{
				throw new ABOperationException(ex.LocalizedString, ex);
			}
			catch (DataSourceTransientException ex2)
			{
				throw new ABTransientException(ex2.LocalizedString, ex2);
			}
			return this.RecipientToABObject(recipient);
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x000373B8 File Offset: 0x000355B8
		protected override ABRawEntry InternalFindById(ABObjectId id, ABPropertyDefinitionCollection properties)
		{
			ADABObjectId adabobjectId = (ADABObjectId)id;
			ADRawEntry rawEntry;
			try
			{
				rawEntry = this.recipientSession.ReadADRawEntry(adabobjectId.NativeId, ADABPropertyMapper.ConvertToADProperties(properties));
			}
			catch (DataSourceOperationException ex)
			{
				throw new ABOperationException(ex.LocalizedString, ex);
			}
			catch (DataSourceTransientException ex2)
			{
				throw new ABTransientException(ex2.LocalizedString, ex2);
			}
			return new ADABRawEntry(this, properties, rawEntry);
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00037428 File Offset: 0x00035628
		protected override IList<ABRawEntry> InternalFindByIds(ICollection<ABObjectId> ids, ABPropertyDefinitionCollection properties)
		{
			ADObjectId[] array = new ADObjectId[ids.Count];
			int num = 0;
			foreach (ABObjectId abobjectId in ids)
			{
				ADABObjectId adabobjectId = (ADABObjectId)abobjectId;
				array[num++] = adabobjectId.NativeId;
			}
			Result<ADRawEntry>[] activeDirectoryRawEntries;
			try
			{
				activeDirectoryRawEntries = this.recipientSession.ReadMultiple(array, ADABPropertyMapper.ConvertToADProperties(properties));
			}
			catch (DataSourceOperationException ex)
			{
				throw new ABOperationException(ex.LocalizedString, ex);
			}
			catch (DataSourceTransientException ex2)
			{
				throw new ABTransientException(ex2.LocalizedString, ex2);
			}
			return this.ADRawEntryResultsToABRawEntries(properties, activeDirectoryRawEntries);
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x000374E8 File Offset: 0x000356E8
		protected override ABObject InternalFindByProxyAddress(ProxyAddress proxyAddress)
		{
			ADRecipient recipient;
			try
			{
				recipient = this.RecipientSession.FindByProxyAddress(proxyAddress);
			}
			catch (DataSourceOperationException ex)
			{
				throw new ABOperationException(ex.LocalizedString, ex);
			}
			catch (DataSourceTransientException ex2)
			{
				throw new ABTransientException(ex2.LocalizedString, ex2);
			}
			return this.RecipientToABObject(recipient);
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00037544 File Offset: 0x00035744
		protected override ABObject InternalFindByLegacyExchangeDN(string legacyExchangeDN)
		{
			ADRecipient recipient;
			try
			{
				recipient = this.RecipientSession.FindByLegacyExchangeDN(legacyExchangeDN);
			}
			catch (DataSourceOperationException ex)
			{
				throw new ABOperationException(ex.LocalizedString, ex);
			}
			catch (DataSourceTransientException ex2)
			{
				throw new ABTransientException(ex2.LocalizedString, ex2);
			}
			return this.RecipientToABObject(recipient);
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x000375A0 File Offset: 0x000357A0
		protected override List<ABObject> InternalFindByANR(string anrMatch, int maxResults)
		{
			ADRecipient[] recipients;
			try
			{
				recipients = this.RecipientSession.FindByANR(anrMatch, maxResults, ADABSession.sortByDisplayName);
			}
			catch (DataSourceOperationException ex)
			{
				throw new ABOperationException(ex.LocalizedString, ex);
			}
			catch (DataSourceTransientException ex2)
			{
				throw new ABTransientException(ex2.LocalizedString, ex2);
			}
			return this.RecipientsToABObjects(recipients);
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00037604 File Offset: 0x00035804
		protected override List<ABRawEntry> InternalFindByANR(string anrMatch, int maxResults, ABPropertyDefinitionCollection properties)
		{
			ADRecipient[] recipients;
			try
			{
				recipients = this.recipientSession.FindByANR(anrMatch, maxResults, ADABSession.sortByDisplayName);
			}
			catch (DataSourceOperationException ex)
			{
				throw new ABOperationException(ex.LocalizedString, ex);
			}
			catch (DataSourceTransientException ex2)
			{
				throw new ABTransientException(ex2.LocalizedString, ex2);
			}
			return this.RecipientsToABRawEntries(recipients, properties);
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x00037668 File Offset: 0x00035868
		private ABRawEntry RecipientToABRawEntry(ADRecipient recipient, ABPropertyDefinitionCollection properties)
		{
			return new ADABRawEntry(this, properties, recipient);
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00037674 File Offset: 0x00035874
		private List<ABObject> RecipientsToABObjects(ICollection<ADRecipient> recipients)
		{
			List<ABObject> list = new List<ABObject>(recipients.Count);
			foreach (ADRecipient recipient in recipients)
			{
				list.Add(this.RecipientToABObject(recipient));
			}
			return list;
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x000376D0 File Offset: 0x000358D0
		private List<ABRawEntry> RecipientsToABRawEntries(ICollection<ADRecipient> recipients, ABPropertyDefinitionCollection properties)
		{
			List<ABRawEntry> list = new List<ABRawEntry>(recipients.Count);
			foreach (ADRecipient recipient in recipients)
			{
				list.Add(this.RecipientToABRawEntry(recipient, properties));
			}
			return list;
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0003772C File Offset: 0x0003592C
		private ABObject RecipientToABObject(ADRecipient recipient)
		{
			if (recipient == null)
			{
				return null;
			}
			switch (recipient.RecipientType)
			{
			default:
				return null;
			case RecipientType.User:
			case RecipientType.UserMailbox:
			case RecipientType.MailUser:
			case RecipientType.Contact:
			case RecipientType.MailContact:
			case RecipientType.PublicFolder:
			case RecipientType.SystemAttendantMailbox:
			case RecipientType.SystemMailbox:
			case RecipientType.MicrosoftExchange:
				return new ADABContact(this, recipient);
			case RecipientType.Group:
			case RecipientType.MailUniversalDistributionGroup:
			case RecipientType.MailUniversalSecurityGroup:
			case RecipientType.MailNonUniversalGroup:
				return new ADABGroup(this, (ADGroup)recipient);
			case RecipientType.DynamicDistributionGroup:
				return new ADABGroup(this, (ADDynamicGroup)recipient);
			}
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x000377B0 File Offset: 0x000359B0
		private List<ABRawEntry> ADRawEntryResultsToABRawEntries(ABPropertyDefinitionCollection properties, Result<ADRawEntry>[] activeDirectoryRawEntries)
		{
			List<ABRawEntry> list = new List<ABRawEntry>(activeDirectoryRawEntries.Length);
			foreach (Result<ADRawEntry> result in activeDirectoryRawEntries)
			{
				list.Add(this.ADRawEntryResultToABRawEntry(properties, result));
			}
			return list;
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x000377F4 File Offset: 0x000359F4
		private ADABRawEntry ADRawEntryResultToABRawEntry(ABPropertyDefinitionCollection properties, Result<ADRawEntry> result)
		{
			if (result.Data == null || result.Error != null)
			{
				if (result.Error == ProviderError.NotFound)
				{
					AirSyncDiagnostics.TraceDebug(base.Tracer, null, "Map Result<ADRawEntry> to null since result indicates entry not found.");
				}
				else
				{
					AirSyncDiagnostics.TraceError(base.Tracer, null, "Map Result<ADRawEntry> to null since result indicates unknown error or data is null.");
				}
				return null;
			}
			return new ADABRawEntry(this, properties, result.Data);
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00037858 File Offset: 0x00035A58
		private IRecipientSession CreateRecipientSession()
		{
			ADSessionSettings adsessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.organizationId);
			adsessionSettings.AccountingObject = null;
			return DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, this.searchRoot, this.lcid, true, this.consistencyMode, null, adsessionSettings, 614, "CreateRecipientSession", "f:\\15.00.1497\\sources\\dev\\AirSync\\src\\AirSync\\GalSearch\\ADABSession.cs");
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x000378A8 File Offset: 0x00035AA8
		private IRecipientSession CreateGalScopedRecipientSession(AddressBookBase globalAddressList)
		{
			ADSessionSettings adsessionSettings = ADSessionSettings.FromOrganizationIdWithAddressListScopeServiceOnly(this.organizationId, (globalAddressList != null) ? globalAddressList.Id : null);
			adsessionSettings.AccountingObject = null;
			return DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, this.searchRoot, this.lcid, true, this.consistencyMode, null, adsessionSettings, 641, "CreateGalScopedRecipientSession", "f:\\15.00.1497\\sources\\dev\\AirSync\\src\\AirSync\\GalSearch\\ADABSession.cs");
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00037904 File Offset: 0x00035B04
		private IConfigurationSession CreateConfigurationSession()
		{
			ADSessionSettings adsessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.organizationId);
			adsessionSettings.AccountingObject = null;
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(this.consistencyMode, adsessionSettings, 667, "CreateConfigurationSession", "f:\\15.00.1497\\sources\\dev\\AirSync\\src\\AirSync\\GalSearch\\ADABSession.cs");
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x00037944 File Offset: 0x00035B44
		private AddressBookBase FindGlobalAddressList()
		{
			IConfigurationSession configurationSession = this.ConfigurationSession;
			return AddressBookBase.GetGlobalAddressList(this.clientSecurityContext, this.ConfigurationSession, this.CreateRecipientSession());
		}

		// Token: 0x040005E3 RID: 1507
		private static ABProviderCapabilities providerCapabilities = new ABProviderCapabilities(ABProviderFlags.HasGal | ABProviderFlags.CanBrowse);

		// Token: 0x040005E4 RID: 1508
		private static readonly SortBy sortByDisplayName = new SortBy(ADRecipientSchema.DisplayName, SortOrder.Ascending);

		// Token: 0x040005E5 RID: 1509
		private readonly int lcid;

		// Token: 0x040005E6 RID: 1510
		private readonly ConsistencyMode consistencyMode;

		// Token: 0x040005E7 RID: 1511
		private IRecipientSession recipientSession;

		// Token: 0x040005E8 RID: 1512
		private IConfigurationSession configurationSession;

		// Token: 0x040005E9 RID: 1513
		private OrganizationId organizationId;

		// Token: 0x040005EA RID: 1514
		private ADObjectId searchRoot;

		// Token: 0x040005EB RID: 1515
		private ClientSecurityContext clientSecurityContext;
	}
}
