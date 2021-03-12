using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch
{
	// Token: 0x02000D11 RID: 3345
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DiscoverySearchDataProvider : TenantStoreDataProvider, IDiscoverySearchDataProvider
	{
		// Token: 0x06007327 RID: 29479 RVA: 0x001FE994 File Offset: 0x001FCB94
		static DiscoverySearchDataProvider()
		{
			DiscoverySearchDataProvider.minimalProperties = (from SimplePropertyDefinition x in new MailboxDiscoverySearch().ObjectSchema.AllProperties
			where (x.PropertyDefinitionFlags & PropertyDefinitionFlags.ReturnOnBind) != PropertyDefinitionFlags.ReturnOnBind && (!(x is EwsStoreObjectPropertyDefinition) || ((EwsStoreObjectPropertyDefinition)x).StorePropertyDefinition != ItemSchema.Attachments)
			select x).ToArray<SimplePropertyDefinition>();
		}

		// Token: 0x06007328 RID: 29480 RVA: 0x001FEA00 File Offset: 0x001FCC00
		public DiscoverySearchDataProvider(OrganizationId organizationId) : base(organizationId)
		{
		}

		// Token: 0x17001EA5 RID: 7845
		// (get) Token: 0x06007329 RID: 29481 RVA: 0x001FEA30 File Offset: 0x001FCC30
		public OrganizationId OrganizationId
		{
			get
			{
				return base.Mailbox.MailboxInfo.OrganizationId;
			}
		}

		// Token: 0x17001EA6 RID: 7846
		// (get) Token: 0x0600732A RID: 29482 RVA: 0x001FEA44 File Offset: 0x001FCC44
		public string PrimarySmtpAddress
		{
			get
			{
				return base.Mailbox.MailboxInfo.PrimarySmtpAddress.ToString();
			}
		}

		// Token: 0x17001EA7 RID: 7847
		// (get) Token: 0x0600732B RID: 29483 RVA: 0x001FEA6F File Offset: 0x001FCC6F
		public string DisplayName
		{
			get
			{
				return base.Mailbox.MailboxInfo.DisplayName;
			}
		}

		// Token: 0x17001EA8 RID: 7848
		// (get) Token: 0x0600732C RID: 29484 RVA: 0x001FEA81 File Offset: 0x001FCC81
		public string DistinguishedName
		{
			get
			{
				return base.Mailbox.ObjectId.DistinguishedName;
			}
		}

		// Token: 0x17001EA9 RID: 7849
		// (get) Token: 0x0600732D RID: 29485 RVA: 0x001FEA93 File Offset: 0x001FCC93
		public string LegacyDistinguishedName
		{
			get
			{
				return base.Mailbox.LegacyDn;
			}
		}

		// Token: 0x17001EAA RID: 7850
		// (get) Token: 0x0600732E RID: 29486 RVA: 0x001FEAA0 File Offset: 0x001FCCA0
		public Guid ObjectGuid
		{
			get
			{
				return base.Mailbox.ObjectId.ObjectGuid;
			}
		}

		// Token: 0x0600732F RID: 29487 RVA: 0x001FEAB2 File Offset: 0x001FCCB2
		public IEnumerable<T> GetAll<T>() where T : DiscoverySearchBase, new()
		{
			return base.InternalFindPaged<T>(null, null, false, this.sortBy, 1000, new ProviderPropertyDefinition[0]);
		}

		// Token: 0x06007330 RID: 29488 RVA: 0x001FEAD0 File Offset: 0x001FCCD0
		public MailboxDiscoverySearch FindByInPlaceHoldIdentity(string inPlaceHoldIdentity)
		{
			Util.ThrowOnNullOrEmptyArgument(inPlaceHoldIdentity, "inPlaceHoldIdentity");
			SearchFilter filter = new SearchFilter.IsEqualTo(MailboxDiscoverySearchSchema.InPlaceHoldIdentity.StorePropertyDefinition, inPlaceHoldIdentity);
			IEnumerable<MailboxDiscoverySearch> source = this.InternalFindPaged<MailboxDiscoverySearch>(filter, null, false, null, 1, new ProviderPropertyDefinition[0]);
			return source.FirstOrDefault<MailboxDiscoverySearch>();
		}

		// Token: 0x06007331 RID: 29489 RVA: 0x001FEB14 File Offset: 0x001FCD14
		public MailboxDiscoverySearch FindByLegacySearchObjectIdentity(string legacySearchObjectIdentity)
		{
			Util.ThrowOnNullOrEmptyArgument(legacySearchObjectIdentity, "legacySearchObjectIdentity");
			SearchFilter filter = new SearchFilter.IsEqualTo(MailboxDiscoverySearchSchema.LegacySearchObjectIdentity.StorePropertyDefinition, legacySearchObjectIdentity);
			IEnumerable<MailboxDiscoverySearch> source = this.InternalFindPaged<MailboxDiscoverySearch>(filter, null, false, null, 1, new ProviderPropertyDefinition[0]);
			return source.FirstOrDefault<MailboxDiscoverySearch>();
		}

		// Token: 0x06007332 RID: 29490 RVA: 0x001FEB55 File Offset: 0x001FCD55
		public T Find<T>(string searchId) where T : DiscoverySearchBase, new()
		{
			Util.ThrowOnNullOrEmptyArgument(searchId, "searchId");
			return this.FindByAlternativeId<T>(searchId);
		}

		// Token: 0x06007333 RID: 29491 RVA: 0x001FEB6C File Offset: 0x001FCD6C
		public override T FindByAlternativeId<T>(string alternativeId)
		{
			if (string.IsNullOrEmpty(alternativeId))
			{
				throw new ArgumentNullException("alternativeId");
			}
			SearchFilter filter = new SearchFilter.IsEqualTo(EwsStoreObjectSchema.AlternativeId.StorePropertyDefinition, alternativeId);
			IEnumerable<T> source = this.InternalFindPaged<T>(filter, null, false, null, 1, new ProviderPropertyDefinition[0]);
			return source.FirstOrDefault<T>();
		}

		// Token: 0x06007334 RID: 29492 RVA: 0x001FEBB7 File Offset: 0x001FCDB7
		public void CreateOrUpdate<T>(T discoverySearch) where T : DiscoverySearchBase
		{
			Util.ThrowOnNullArgument(discoverySearch, "discoverySearch");
			this.Save(discoverySearch);
		}

		// Token: 0x06007335 RID: 29493 RVA: 0x001FEBD8 File Offset: 0x001FCDD8
		public void Delete<T>(string searchId) where T : DiscoverySearchBase, new()
		{
			Util.ThrowOnNullOrEmptyArgument(searchId, "searchId");
			T t = this.Find<T>(searchId);
			if (t == null)
			{
				throw new StoragePermanentException(ServerStrings.MailboxSearchObjectNotExist(searchId));
			}
			base.Delete(t);
		}

		// Token: 0x06007336 RID: 29494 RVA: 0x001FEC18 File Offset: 0x001FCE18
		protected override IEnumerable<T> InternalFindPaged<T>(SearchFilter filter, FolderId rootId, bool deepSearch, SortBy[] sortBy, int pageSize, params ProviderPropertyDefinition[] properties)
		{
			Stopwatch stopwatch = new Stopwatch();
			IEnumerable<T> result;
			try
			{
				if (filter == null && typeof(T) == typeof(MailboxDiscoverySearch))
				{
					properties = DiscoverySearchDataProvider.minimalProperties;
					deepSearch = false;
					pageSize = 1000;
				}
				stopwatch.Start();
				result = base.InternalFindPaged<T>(filter, rootId, deepSearch, sortBy, pageSize, properties).ToList<T>();
			}
			finally
			{
				stopwatch.Stop();
				long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
			}
			return result;
		}

		// Token: 0x06007337 RID: 29495 RVA: 0x001FEC98 File Offset: 0x001FCE98
		protected override void OnExchangeServiceCreated(ExchangeService service)
		{
			service.UserAgent = string.Format("Exchange\\{0}\\EDiscovery\\EWS\\SID={1}&S=CFG&BI=0&R=0&RT={2}", DiscoverySearchDataProvider.version, Guid.NewGuid(), DateTime.UtcNow.Ticks);
			base.OnExchangeServiceCreated(service);
		}

		// Token: 0x06007338 RID: 29496 RVA: 0x001FECE0 File Offset: 0x001FCEE0
		protected override FolderId GetDefaultFolder()
		{
			if (this.mailboxSearchFolderId == null)
			{
				this.mailboxSearchFolderId = base.GetOrCreateFolder("Discovery", new FolderId(10, new Mailbox(base.Mailbox.MailboxInfo.PrimarySmtpAddress.ToString()))).Id;
			}
			return this.mailboxSearchFolderId;
		}

		// Token: 0x0400505A RID: 20570
		internal const string MailboxSearchFolderName = "Discovery";

		// Token: 0x0400505B RID: 20571
		private static ProviderPropertyDefinition[] minimalProperties = null;

		// Token: 0x0400505C RID: 20572
		private static string version = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;

		// Token: 0x0400505D RID: 20573
		private readonly SortBy[] sortBy = new SortBy[]
		{
			new SortBy(MailboxDiscoverySearchSchema.LastModifiedTime, SortOrder.Descending)
		};

		// Token: 0x0400505E RID: 20574
		private FolderId mailboxSearchFolderId;
	}
}
