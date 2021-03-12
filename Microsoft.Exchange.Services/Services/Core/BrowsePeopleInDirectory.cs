using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002A8 RID: 680
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class BrowsePeopleInDirectory : FindPeopleImplementation
	{
		// Token: 0x06001243 RID: 4675 RVA: 0x000597EC File Offset: 0x000579EC
		public BrowsePeopleInDirectory(FindPeopleParameters parameters, OrganizationId organizationId, ADObjectId addressListId, MailboxSession mailboxSession) : base(parameters, null, true)
		{
			ServiceCommandBase.ThrowIfNull(organizationId, "organizationId", "BrowsePeopleInDirectory::BrowsePeopleInDirectory");
			ServiceCommandBase.ThrowIfNull(addressListId, "addressListId", "BrowsePeopleInDirectory::BrowsePeopleInDirectory");
			ServiceCommandBase.ThrowIfNull(mailboxSession, "mailboxSession", "BrowsePeopleInMailbox::BrowsePeopleInMailbox");
			this.organizationId = organizationId;
			this.addressListId = addressListId;
			this.mailboxSession = mailboxSession;
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x00059849 File Offset: 0x00057A49
		public override void Validate()
		{
			base.Validate();
			if (base.AggregationRestriction != null || base.Restriction != null)
			{
				throw new ServiceArgumentException((CoreResources.IDs)3784063568U);
			}
			if (base.SortResults != null)
			{
				throw new ServiceArgumentException((CoreResources.IDs)2841035169U);
			}
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x00059889 File Offset: 0x00057A89
		protected override void ValidatePaging()
		{
			base.ValidatePaging();
			if (!(base.Paging is IndexedPageView))
			{
				throw new ServiceArgumentException(CoreResources.IDs.ErrorInvalidIndexedPagingParameters);
			}
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x000598B0 File Offset: 0x00057AB0
		public override FindPeopleResult Execute()
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			FindPeopleResult findPeopleResult = this.ExecuteInternal();
			stopwatch.Stop();
			base.Log(FindPeopleMetadata.GalSearchTime, stopwatch.ElapsedMilliseconds);
			base.Log(FindPeopleMetadata.GalCount, findPeopleResult.PersonaList.Length);
			base.Log(FindPeopleMetadata.TotalNumberOfPeopleInView, findPeopleResult.TotalNumberOfPeopleInView);
			return findPeopleResult;
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x0005990C File Offset: 0x00057B0C
		private FindPeopleResult ExecuteInternal()
		{
			ToServiceObjectForPropertyBagPropertyList propertyListForPersonaResponseShape = Persona.GetPropertyListForPersonaResponseShape(base.PersonaShape);
			List<PropertyDefinition> list = new List<PropertyDefinition>(propertyListForPersonaResponseShape.GetPropertyDefinitions());
			ADPersonToContactConverterSet adpersonToContactConverterSet = ADPersonToContactConverterSet.FromPersonProperties(list.ToArray(), null);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(null, true, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.FromAccountPartitionWideScopeSet(this.organizationId.PartitionId), 47, "ExecuteInternal", "f:\\15.00.1497\\sources\\dev\\services\\src\\Services\\Server\\ServiceCommands\\BrowsePeopleInDirectory.cs");
			NspiVirtualListView nspiVirtualListView = new NspiVirtualListView(tenantOrTopologyConfigurationSession, base.CultureInfo.TextInfo.ANSICodePage, this.addressListId, adpersonToContactConverterSet.ADProperties);
			ADRawEntry[] propertyBags = nspiVirtualListView.GetPropertyBags(((IndexedPageView)base.Paging).Offset, base.Paging.MaxRows);
			int estimatedRowCount = nspiVirtualListView.EstimatedRowCount;
			int offset = ((IndexedPageView)base.Paging).Offset;
			Stopwatch stopwatch = Stopwatch.StartNew();
			Persona[] array = new Persona[propertyBags.Length];
			for (int i = 0; i < propertyBags.Length; i++)
			{
				array[i] = Persona.LoadFromADRawEntry(propertyBags[i], this.mailboxSession, adpersonToContactConverterSet, propertyListForPersonaResponseShape);
			}
			stopwatch.Stop();
			base.Log(FindPeopleMetadata.GalDataConversion, stopwatch.ElapsedMilliseconds);
			return FindPeopleResult.CreateDirectoryBrowseResult(array, estimatedRowCount, offset);
		}

		// Token: 0x04000CF7 RID: 3319
		private readonly OrganizationId organizationId;

		// Token: 0x04000CF8 RID: 3320
		private readonly ADObjectId addressListId;

		// Token: 0x04000CF9 RID: 3321
		private readonly MailboxSession mailboxSession;
	}
}
