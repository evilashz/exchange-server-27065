using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200017D RID: 381
	[Serializable]
	public class RoleGroupIdParameter : GroupIdParameter
	{
		// Token: 0x06000DC3 RID: 3523 RVA: 0x0002937B File Offset: 0x0002757B
		public RoleGroupIdParameter(string identity) : base(identity)
		{
			GuidHelper.TryParseGuid(identity, out this.guid);
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x0002939C File Offset: 0x0002759C
		public RoleGroupIdParameter()
		{
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x000293AF File Offset: 0x000275AF
		public RoleGroupIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x000293C3 File Offset: 0x000275C3
		public RoleGroupIdParameter(RoleGroup group) : base(group.Id)
		{
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x000293DC File Offset: 0x000275DC
		public RoleGroupIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
			GuidHelper.TryParseGuid(namedIdentity.Identity, out this.guid);
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x00029402 File Offset: 0x00027602
		public RoleGroupIdParameter(Guid guid) : base(guid.ToString())
		{
			this.guid = guid;
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x00029429 File Offset: 0x00027629
		internal override RecipientType[] RecipientTypes
		{
			get
			{
				return RoleGroupIdParameter.AllowedRecipientTypes;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x00029430 File Offset: 0x00027630
		protected override QueryFilter AdditionalQueryFilter
		{
			get
			{
				return RoleGroupIdParameter.GetRoleGroupFilter(base.AdditionalQueryFilter);
			}
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x0002943D File Offset: 0x0002763D
		public new static RoleGroupIdParameter Parse(string identity)
		{
			return new RoleGroupIdParameter(identity);
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x00029448 File Offset: 0x00027648
		private bool HasEmptyGuid
		{
			get
			{
				return Guid.Empty.Equals(this.guid);
			}
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x00029468 File Offset: 0x00027668
		internal override IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			notFoundReason = null;
			EnumerableWrapper<T> wrapper = EnumerableWrapper<T>.GetWrapper(base.GetObjects<T>(rootId, session, subTreeSession, optionalData, out notFoundReason));
			if (!wrapper.HasElements())
			{
				wrapper = EnumerableWrapper<T>.GetWrapper(this.TryGetObjectsFromDC<T>(rootId, subTreeSession, optionalData));
			}
			if (!wrapper.HasElements() && !Guid.Empty.Equals(this.guid) && typeof(T).IsAssignableFrom(typeof(ADGroup)))
			{
				ADObjectId containerId;
				if (session.SessionSettings.CurrentOrganizationId.Equals(OrganizationId.ForestWideOrgId))
				{
					containerId = session.GetConfigurationNamingContext();
				}
				else
				{
					containerId = session.SessionSettings.CurrentOrganizationId.ConfigurationUnit;
				}
				bool useGlobalCatalog = session.UseGlobalCatalog;
				bool useConfigNC = session.UseConfigNC;
				ADGroup adgroup = null;
				try
				{
					session.UseGlobalCatalog = true;
					session.UseConfigNC = false;
					adgroup = session.ResolveWellKnownGuid<ADGroup>(this.guid, containerId);
				}
				finally
				{
					session.UseGlobalCatalog = useGlobalCatalog;
					session.UseConfigNC = useConfigNC;
				}
				if (adgroup != null)
				{
					wrapper = EnumerableWrapper<T>.GetWrapper(new List<ADGroup>(1)
					{
						adgroup
					}.Cast<T>());
				}
			}
			return wrapper;
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x00029590 File Offset: 0x00027790
		private IEnumerable<T> TryGetObjectsFromDC<T>(ADObjectId rootId, IDirectorySession subTreeSession, OptionalIdentityData optionalData) where T : IConfigurable, new()
		{
			if (rootId != null || this.HasEmptyGuid || Datacenter.GetExchangeSku() != Datacenter.ExchangeSku.Enterprise)
			{
				return EnumerableWrapper<T>.Empty;
			}
			rootId = subTreeSession.GetRootDomainNamingContext();
			if (rootId != null && optionalData != null && optionalData.RootOrgDomainContainerId != null)
			{
				optionalData.RootOrgDomainContainerId = null;
			}
			bool useGlobalCatalog = subTreeSession.UseGlobalCatalog;
			IEnumerable<T> exactMatchObjects;
			try
			{
				subTreeSession.UseGlobalCatalog = false;
				exactMatchObjects = base.GetExactMatchObjects<T>(rootId, subTreeSession, optionalData);
			}
			finally
			{
				subTreeSession.UseGlobalCatalog = useGlobalCatalog;
			}
			return exactMatchObjects;
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x00029608 File Offset: 0x00027808
		internal static QueryFilter GetRoleGroupFilter(QueryFilter additionalFilter)
		{
			return QueryFilter.AndTogether(new QueryFilter[]
			{
				additionalFilter,
				Filters.GetRecipientTypeDetailsFilterOptimization(RecipientTypeDetails.RoleGroup)
			});
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x00029634 File Offset: 0x00027834
		protected override LocalizedString GetErrorMessageForWrongType(string id)
		{
			return Strings.WrongTypeRoleGroup(id);
		}

		// Token: 0x040002FD RID: 765
		private Guid guid = Guid.Empty;

		// Token: 0x040002FE RID: 766
		private new static readonly RecipientType[] AllowedRecipientTypes = new RecipientType[]
		{
			RecipientType.Group
		};
	}
}
