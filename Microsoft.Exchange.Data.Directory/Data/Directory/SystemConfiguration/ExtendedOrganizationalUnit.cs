using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000622 RID: 1570
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class ExtendedOrganizationalUnit : ADConfigurationObject
	{
		// Token: 0x170018A7 RID: 6311
		// (get) Token: 0x06004A49 RID: 19017 RVA: 0x0011296C File Offset: 0x00110B6C
		internal override ADObjectSchema Schema
		{
			get
			{
				return ExtendedOrganizationalUnit.schema;
			}
		}

		// Token: 0x170018A8 RID: 6312
		// (get) Token: 0x06004A4A RID: 19018 RVA: 0x00112973 File Offset: 0x00110B73
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ExtendedOrganizationalUnit.mostDerivedClass;
			}
		}

		// Token: 0x170018A9 RID: 6313
		// (get) Token: 0x06004A4C RID: 19020 RVA: 0x00112982 File Offset: 0x00110B82
		public OrganizationalUnitType Type
		{
			get
			{
				return this.organizationalUnitType;
			}
		}

		// Token: 0x170018AA RID: 6314
		// (get) Token: 0x06004A4D RID: 19021 RVA: 0x0011298A File Offset: 0x00110B8A
		public string CanonicalName
		{
			get
			{
				return (string)this[ADObjectSchema.CanonicalName];
			}
		}

		// Token: 0x170018AB RID: 6315
		// (get) Token: 0x06004A4E RID: 19022 RVA: 0x0011299C File Offset: 0x00110B9C
		public bool IsWellKnownContainer
		{
			get
			{
				return this.isWellKnowContainer;
			}
		}

		// Token: 0x170018AC RID: 6316
		// (get) Token: 0x06004A4F RID: 19023 RVA: 0x001129A4 File Offset: 0x00110BA4
		// (set) Token: 0x06004A50 RID: 19024 RVA: 0x001129B6 File Offset: 0x00110BB6
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> DirSyncStatusAck
		{
			get
			{
				return (MultiValuedProperty<string>)this[ExtendedOrganizationalUnitSchema.DirSyncStatusAck];
			}
			set
			{
				this[ExtendedOrganizationalUnitSchema.DirSyncStatusAck] = value;
			}
		}

		// Token: 0x06004A51 RID: 19025 RVA: 0x001129C4 File Offset: 0x00110BC4
		protected override void ValidateRead(List<ValidationError> errors)
		{
			base.ValidateRead(errors);
			if (((MultiValuedProperty<string>)this[ADObjectSchema.ObjectClass]).Contains(ADDomain.MostDerivedClass))
			{
				this.organizationalUnitType = OrganizationalUnitType.Domain;
				this.isWellKnowContainer = false;
				return;
			}
			if (((MultiValuedProperty<string>)this[ADObjectSchema.ObjectClass]).Contains(ADContainer.MostDerivedClass))
			{
				this.organizationalUnitType = OrganizationalUnitType.Container;
				this.isWellKnowContainer = true;
				return;
			}
			if (((MultiValuedProperty<string>)this[ADObjectSchema.ObjectClass]).Contains(ADOrganizationalUnit.MostDerivedClass))
			{
				this.organizationalUnitType = OrganizationalUnitType.OrganizationalUnit;
				this.isWellKnowContainer = false;
				return;
			}
			errors.Add(new PropertyValidationError(DataStrings.BadEnumValue(typeof(OrganizationalUnitType)), ADObjectSchema.ObjectClass, null));
		}

		// Token: 0x170018AD RID: 6317
		// (get) Token: 0x06004A52 RID: 19026 RVA: 0x00112A79 File Offset: 0x00110C79
		internal MultiValuedProperty<string> UPNSuffixes
		{
			get
			{
				return (MultiValuedProperty<string>)this[ExtendedOrganizationalUnitSchema.UPNSuffixes];
			}
		}

		// Token: 0x170018AE RID: 6318
		// (get) Token: 0x06004A53 RID: 19027 RVA: 0x00112A8C File Offset: 0x00110C8C
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new OrFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, ADOrganizationalUnit.MostDerivedClass),
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, ADDomain.MostDerivedClass),
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, ADContainer.MostDerivedClass)
				});
			}
		}

		// Token: 0x06004A54 RID: 19028 RVA: 0x00112AE1 File Offset: 0x00110CE1
		internal static bool IsTenant(IDirectorySession session)
		{
			return !session.SessionSettings.CurrentOrganizationId.Equals(OrganizationId.ForestWideOrgId);
		}

		// Token: 0x06004A55 RID: 19029 RVA: 0x00112AFC File Offset: 0x00110CFC
		internal static IEnumerable<ExtendedOrganizationalUnit> FindFirstLevelChildOrganizationalUnit(bool includeContainers, IConfigurationSession session, ADObjectId rootId, QueryFilter preFilter, SortBy sortBy, int pageSize)
		{
			QueryFilter queryFilter = new OrFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, ADOrganizationalUnit.MostDerivedClass),
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, ADDomain.MostDerivedClass)
			});
			if (!ExtendedOrganizationalUnit.IsTenant(session))
			{
				ExchangeOrganizationalUnit exchangeOrganizationalUnit = session.ResolveWellKnownGuid<ExchangeOrganizationalUnit>(WellKnownGuid.DomainControllersWkGuid, rootId);
				ExchangeOrganizationalUnit exchangeOrganizationalUnit2 = session.ResolveWellKnownGuid<ExchangeOrganizationalUnit>(WellKnownGuid.UsersWkGuid, rootId);
				if (exchangeOrganizationalUnit != null)
				{
					queryFilter = new AndFilter(new QueryFilter[]
					{
						queryFilter,
						new ComparisonFilter(ComparisonOperator.NotEqual, ADObjectSchema.Id, exchangeOrganizationalUnit.Id)
					});
				}
				if (includeContainers && exchangeOrganizationalUnit2 != null)
				{
					queryFilter = new OrFilter(new QueryFilter[]
					{
						queryFilter,
						new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Id, exchangeOrganizationalUnit2.Id)
					});
				}
			}
			if (preFilter != null)
			{
				queryFilter = new AndFilter(new QueryFilter[]
				{
					preFilter,
					queryFilter
				});
			}
			return session.FindPaged<ExtendedOrganizationalUnit>(rootId, QueryScope.OneLevel, queryFilter, sortBy, pageSize);
		}

		// Token: 0x06004A56 RID: 19030 RVA: 0x00112BE8 File Offset: 0x00110DE8
		internal static IEnumerable<ExtendedOrganizationalUnit> FindSubTreeChildOrganizationalUnit(bool includeContainers, IConfigurationSession session, ADObjectId rootId, QueryFilter preFilter)
		{
			QueryFilter queryFilter = new OrFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, ADOrganizationalUnit.MostDerivedClass),
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, ADDomain.MostDerivedClass)
			});
			if (!ExtendedOrganizationalUnit.IsTenant(session))
			{
				IList<QueryFilter> list = new List<QueryFilter>();
				IList<QueryFilter> list2 = new List<QueryFilter>();
				IEnumerable<ADDomain> enumerable = ADForest.GetLocalForest(session.DomainController).FindDomains();
				foreach (ADDomain addomain in enumerable)
				{
					ExchangeOrganizationalUnit exchangeOrganizationalUnit = session.ResolveWellKnownGuid<ExchangeOrganizationalUnit>(WellKnownGuid.DomainControllersWkGuid, addomain.Id);
					if (exchangeOrganizationalUnit != null)
					{
						list.Add(new ComparisonFilter(ComparisonOperator.NotEqual, ADObjectSchema.Id, exchangeOrganizationalUnit.Id));
					}
					ExchangeOrganizationalUnit exchangeOrganizationalUnit2 = session.ResolveWellKnownGuid<ExchangeOrganizationalUnit>(WellKnownGuid.UsersWkGuid, addomain.Id);
					if (exchangeOrganizationalUnit2 != null)
					{
						list2.Add(new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Id, exchangeOrganizationalUnit2.Id));
					}
				}
				foreach (QueryFilter queryFilter2 in list)
				{
					queryFilter = new AndFilter(new QueryFilter[]
					{
						queryFilter,
						queryFilter2
					});
				}
				if (includeContainers)
				{
					foreach (QueryFilter queryFilter3 in list2)
					{
						queryFilter = new OrFilter(new QueryFilter[]
						{
							queryFilter,
							queryFilter3
						});
					}
				}
			}
			if (preFilter != null)
			{
				queryFilter = new AndFilter(new QueryFilter[]
				{
					preFilter,
					queryFilter
				});
			}
			return session.FindPaged<ExtendedOrganizationalUnit>(rootId, QueryScope.SubTree, queryFilter, null, 0);
		}

		// Token: 0x04003368 RID: 13160
		private static ExtendedOrganizationalUnitSchema schema = ObjectSchema.GetInstance<ExtendedOrganizationalUnitSchema>();

		// Token: 0x04003369 RID: 13161
		private static string mostDerivedClass = "organizationalUnit";

		// Token: 0x0400336A RID: 13162
		private OrganizationalUnitType organizationalUnitType;

		// Token: 0x0400336B RID: 13163
		private bool isWellKnowContainer;
	}
}
