using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200002E RID: 46
	[Serializable]
	public abstract class ADIdParameter : IIdentityParameter
	{
		// Token: 0x060001BA RID: 442 RVA: 0x000071B6 File Offset: 0x000053B6
		protected ADIdParameter()
		{
		}

		// Token: 0x060001BB RID: 443 RVA: 0x000071BE File Offset: 0x000053BE
		protected ADIdParameter(ADObjectId adObjectId)
		{
			this.Initialize(adObjectId);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000071CD File Offset: 0x000053CD
		protected ADIdParameter(INamedIdentity namedIdentity) : this(namedIdentity.Identity)
		{
			this.displayName = this.TryResolveRedactedPii(namedIdentity.DisplayName);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000071F0 File Offset: 0x000053F0
		protected ADIdParameter(string identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			if (identity.Length == 0)
			{
				throw new ArgumentException(Strings.ErrorEmptyParameter(base.GetType().ToString()), "identity");
			}
			identity = this.TryResolveRedactedPii(identity);
			ADObjectId adobjectId;
			if (ADObjectId.TryParseDnOrGuid(identity, out adobjectId) && !adobjectId.IsRelativeDn)
			{
				this.Initialize(adobjectId);
			}
			this.rawIdentity = identity;
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00007262 File Offset: 0x00005462
		string IIdentityParameter.RawIdentity
		{
			get
			{
				return this.RawIdentity;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001BF RID: 447 RVA: 0x0000726A File Offset: 0x0000546A
		internal ADObjectId InternalADObjectId
		{
			get
			{
				return this.adObjectId;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00007272 File Offset: 0x00005472
		internal string RawIdentity
		{
			get
			{
				return this.rawIdentity;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000727A File Offset: 0x0000547A
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x00007282 File Offset: 0x00005482
		internal bool HasRedactedPiiData
		{
			get
			{
				return this.hasRedactedPiiData;
			}
			private set
			{
				this.hasRedactedPiiData = value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000728B File Offset: 0x0000548B
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x00007293 File Offset: 0x00005493
		internal bool IsRedactedPiiResolved
		{
			get
			{
				return this.isRedactedPiiResolved;
			}
			private set
			{
				this.isRedactedPiiResolved = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000729C File Offset: 0x0000549C
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x000072A4 File Offset: 0x000054A4
		internal string OriginalRedactedPiiData
		{
			get
			{
				return this.originalRedactedPiiData;
			}
			private set
			{
				this.originalRedactedPiiData = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x000072AD File Offset: 0x000054AD
		protected virtual QueryFilter AdditionalQueryFilter
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x000072B0 File Offset: 0x000054B0
		internal virtual ADPropertyDefinition[] AdditionalMatchingProperties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x000072B3 File Offset: 0x000054B3
		protected virtual SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.NotShared;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001CA RID: 458 RVA: 0x000072B8 File Offset: 0x000054B8
		private static string LocalForestDomainNamingContext
		{
			get
			{
				if (ADIdParameter.localForestDomainNamingContext == null)
				{
					ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 243, "LocalForestDomainNamingContext", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\ADIdParameter.cs");
					ADIdParameter.localForestDomainNamingContext = topologyConfigurationSession.GetRootDomainNamingContextFromCurrentReadConnection();
				}
				return ADIdParameter.localForestDomainNamingContext;
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000072FC File Offset: 0x000054FC
		public static explicit operator string(ADIdParameter adIdParameter)
		{
			if (adIdParameter != null)
			{
				return adIdParameter.ToString();
			}
			return null;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00007309 File Offset: 0x00005509
		void IIdentityParameter.Initialize(ObjectId objectId)
		{
			this.Initialize(objectId);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00007314 File Offset: 0x00005514
		public override string ToString()
		{
			if (!string.IsNullOrEmpty(this.OriginalRedactedPiiData))
			{
				return this.OriginalRedactedPiiData;
			}
			if (!string.IsNullOrEmpty(this.displayName))
			{
				return this.displayName;
			}
			if (this.InternalADObjectId != null && !string.IsNullOrEmpty(this.InternalADObjectId.DistinguishedName))
			{
				return this.InternalADObjectId.ToString();
			}
			return this.RawIdentity ?? string.Empty;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00007380 File Offset: 0x00005580
		protected OrganizationId GetOrganizationId(OrganizationId currentOrgId, string orgName)
		{
			if (OrganizationId.ForestWideOrgId.Equals(currentOrgId) && !string.IsNullOrEmpty(orgName))
			{
				if (TemplateTenantConfiguration.IsTemplateTenantName(orgName) && TemplateTenantConfiguration.GetLocalTemplateTenant() != null)
				{
					return TemplateTenantConfiguration.GetLocalTemplateTenant().OrganizationId;
				}
				ExchangeConfigurationUnit configurationUnit = this.GetConfigurationUnit(orgName);
				if (configurationUnit != null)
				{
					if (this.MustScopeToSharedConfiguration(configurationUnit))
					{
						SharedConfiguration sharedConfiguration = SharedConfiguration.GetSharedConfiguration(configurationUnit.OrganizationId);
						if (sharedConfiguration != null)
						{
							return sharedConfiguration.SharedConfigurationCU.OrganizationId;
						}
					}
					return configurationUnit.OrganizationId;
				}
			}
			return currentOrgId;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x000073F4 File Offset: 0x000055F4
		protected OrganizationId GetOrganizationId(OrganizationId currentOrgId, ADObjectId id)
		{
			if (OrganizationId.ForestWideOrgId.Equals(currentOrgId) && id != null)
			{
				if (id.Parent != null && TemplateTenantConfiguration.IsTemplateTenantName(id.Parent.Name) && TemplateTenantConfiguration.GetLocalTemplateTenant() != null)
				{
					return TemplateTenantConfiguration.GetLocalTemplateTenant().OrganizationId;
				}
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromAllTenantsOrRootOrgAutoDetect(id), 350, "GetOrganizationId", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\ADIdParameter.cs");
				ADRawEntry adrawEntry = tenantOrTopologyConfigurationSession.ReadADRawEntry(id, new ADPropertyDefinition[]
				{
					ADObjectSchema.OrganizationId
				});
				if (adrawEntry != null)
				{
					return (OrganizationId)adrawEntry[ADObjectSchema.OrganizationId];
				}
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromAllTenantsOrRootOrgAutoDetect(id), 365, "GetOrganizationId", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\ADIdParameter.cs");
				adrawEntry = tenantOrRootOrgRecipientSession.ReadADRawEntry(id, new ADPropertyDefinition[]
				{
					ADObjectSchema.OrganizationId
				});
				if (adrawEntry != null)
				{
					return (OrganizationId)adrawEntry[ADObjectSchema.OrganizationId];
				}
			}
			return currentOrgId;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000074E4 File Offset: 0x000056E4
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session) where T : IConfigurable, new()
		{
			LocalizedString? localizedString;
			return this.GetObjects<T>(rootId, session, null, out localizedString);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000074FC File Offset: 0x000056FC
		public IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason) where T : IConfigurable, new()
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (!(session is IDirectorySession))
			{
				throw new ArgumentException("Session should be an IDirectorySession", "session");
			}
			if (rootId != null && !(rootId is ADObjectId))
			{
				throw new ArgumentException("RootId must be an ADObjectId", "rootId");
			}
			IDirectorySession directorySession = (IDirectorySession)session;
			IDirectorySession directorySession2 = null;
			if (!(this is OrganizationIdParameter) && VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled && rootId == null && ADSessionSettings.GetProcessServerSettings() == null && directorySession is IConfigurationSession)
			{
				IConfigurationSession configurationSession = directorySession as IConfigurationSession;
				ADObjectId configurationUnitsRoot = directorySession.GetConfigurationUnitsRoot();
				bool flag = !string.IsNullOrEmpty(this.rawIdentity) && this.rawIdentity.IndexOf("\\") != -1;
				if (this.InternalADObjectId != null)
				{
					flag = !string.IsNullOrEmpty(this.InternalADObjectId.DistinguishedName);
				}
				if (!flag && configurationSession.UseConfigNC && !configurationUnitsRoot.IsDescendantOf(directorySession.GetConfigurationNamingContext()) && typeof(ADConfigurationObject).IsAssignableFrom(typeof(T)) && !typeof(ADNonExchangeObject).IsAssignableFrom(typeof(T)))
				{
					T t = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
					ADObject adobject = t as ADObject;
					ObjectScopeAttribute objectScopeAttribute;
					bool flag2 = adobject.IsApplicableToTenant(out objectScopeAttribute);
					if (directorySession.SessionSettings.ExecutingUserOrganizationId.Equals(OrganizationId.ForestWideOrgId) && directorySession.SessionSettings.CurrentOrganizationId.Equals(directorySession.SessionSettings.ExecutingUserOrganizationId) && flag2)
					{
						directorySession2 = directorySession;
					}
				}
			}
			if (directorySession2 == null)
			{
				directorySession2 = ADSession.RescopeSessionToTenantSubTree(directorySession);
			}
			return this.GetObjects<T>((ADObjectId)rootId, directorySession, directorySession2, optionalData, out notFoundReason);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000076D8 File Offset: 0x000058D8
		internal virtual void Initialize(ObjectId objectId)
		{
			if (objectId == null)
			{
				throw new ArgumentNullException("objectId");
			}
			ADObjectId adobjectId = objectId as ADObjectId;
			if (adobjectId == null)
			{
				throw new ArgumentException(Strings.ErrorInvalidParameterType("objectId", typeof(ADObjectId).Name), "objectId");
			}
			if (this.InternalADObjectId != null)
			{
				throw new InvalidOperationException(Strings.ErrorChangeImmutableType);
			}
			if (string.IsNullOrEmpty(adobjectId.DistinguishedName) && adobjectId.ObjectGuid == Guid.Empty)
			{
				throw new ArgumentException(Strings.ErrorInvalidParameterFormat("objectId"), "objectId");
			}
			if (adobjectId.IsRelativeDn)
			{
				throw new ArgumentException(Strings.ErrorRelativeDn(adobjectId.ToDNString()), "objectId");
			}
			if (PiiMapManager.ContainsRedactedPiiValue(adobjectId.DistinguishedName))
			{
				string distinguishedName = this.TryResolveRedactedPii(adobjectId.DistinguishedName);
				if (this.IsRedactedPiiResolved)
				{
					adobjectId = new ADObjectId(distinguishedName, adobjectId.ObjectGuid, adobjectId.PartitionGuid);
				}
			}
			this.adObjectId = adobjectId;
			this.rawIdentity = adobjectId.ToDNString();
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000077E2 File Offset: 0x000059E2
		internal virtual IEnumerableFilter<T> GetEnumerableFilter<T>() where T : IConfigurable, new()
		{
			return null;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000077E8 File Offset: 0x000059E8
		internal virtual IEnumerable<T> GetObjects<T>(ADObjectId rootId, IDirectorySession session, IDirectorySession subTreeSession, OptionalIdentityData optionalData, out LocalizedString? notFoundReason) where T : IConfigurable, new()
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (subTreeSession == null)
			{
				throw new ArgumentNullException("subTreeSession");
			}
			notFoundReason = null;
			EnumerableWrapper<T> enumerableWrapper = null;
			if (string.IsNullOrEmpty(this.RawIdentity))
			{
				throw new InvalidOperationException(Strings.ErrorOperationOnInvalidObject);
			}
			enumerableWrapper = this.GetEnumerableWrapper<T>(enumerableWrapper, this.GetExactMatchObjects<T>(rootId, subTreeSession, optionalData));
			if (!enumerableWrapper.HasElements() && VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled)
			{
				enumerableWrapper = this.GetEnumerableWrapper<T>(enumerableWrapper, this.GetMultitenancyObjects<T>(this.RawIdentity, rootId, session, optionalData, out notFoundReason));
			}
			if (!enumerableWrapper.HasElements())
			{
				enumerableWrapper = this.GetEnumerableWrapper<T>(enumerableWrapper, this.GetObjectsInOrganization<T>(this.RawIdentity, rootId, session, optionalData));
			}
			if (!enumerableWrapper.HasElements() && !string.IsNullOrEmpty(this.displayName) && this.displayName != this.RawIdentity)
			{
				enumerableWrapper = this.GetEnumerableWrapper<T>(enumerableWrapper, this.GetObjectsInOrganization<T>(this.displayName, rootId, session, optionalData));
			}
			return enumerableWrapper;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000078E8 File Offset: 0x00005AE8
		internal EnumerableWrapper<T> GetEnumerableWrapper<T>(EnumerableWrapper<T> noElementsValue, IEnumerable<T> collection) where T : IConfigurable, new()
		{
			EnumerableWrapper<T> result = noElementsValue ?? EnumerableWrapper<T>.Empty;
			EnumerableWrapper<T> wrapper = EnumerableWrapper<T>.GetWrapper(collection, this.GetEnumerableFilter<T>());
			if (wrapper.HasUnfilteredElements())
			{
				result = wrapper;
			}
			return result;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00007918 File Offset: 0x00005B18
		internal IEnumerable<T> GetExactMatchObjects<T>(ADObjectId rootId, IDirectorySession session, OptionalIdentityData optionalData) where T : IConfigurable, new()
		{
			EnumerableWrapper<T> enumerableWrapper = EnumerableWrapper<T>.Empty;
			ADObjectId identity;
			if (this.InternalADObjectId != null)
			{
				enumerableWrapper = EnumerableWrapper<T>.GetWrapper(this.GetADObjectIdObjects<T>(this.InternalADObjectId, rootId, session, optionalData));
			}
			else if (ADIdParameter.TryResolveCanonicalName(this.RawIdentity, out identity))
			{
				enumerableWrapper = EnumerableWrapper<T>.GetWrapper(this.GetADObjectIdObjects<T>(identity, rootId, session, optionalData));
				if (enumerableWrapper.HasElements())
				{
					this.UpdateInternalADObjectId(identity);
				}
			}
			return enumerableWrapper;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000797C File Offset: 0x00005B7C
		internal IEnumerable<T> GetADObjectIdObjects<T>(ADObjectId identity, ADObjectId rootId, IDirectorySession session, OptionalIdentityData optionalData) where T : IConfigurable, new()
		{
			if (identity != null)
			{
				OrganizationId organizationId;
				if (this.InternalADObjectId != null && this.InternalADObjectId.Equals(identity) && this.orgIdResolved)
				{
					organizationId = this.resolvedOrganizationId;
				}
				else
				{
					organizationId = this.GetOrganizationId(session.SessionSettings.CurrentOrganizationId, identity);
				}
				IDirectorySession directorySession = session;
				if (organizationId != null)
				{
					directorySession = TaskHelper.UnderscopeSessionToOrganization(session, organizationId, true);
				}
				if (session.ConfigScope == ConfigScopes.TenantSubTree)
				{
					directorySession = ADSession.RescopeSessionToTenantSubTree(directorySession);
				}
				if (directorySession.IsRootIdWithinScope<T>(rootId))
				{
					if (ADObjectId.Equals(identity, identity.DomainId) && !typeof(OrganizationalUnitIdParameterBase).IsAssignableFrom(base.GetType()))
					{
						if (!typeof(ADRawEntryIdParameter).IsAssignableFrom(base.GetType()))
						{
							goto IL_15F;
						}
					}
					try
					{
						ADObjectId rootId2 = rootId;
						bool enforceContainerizedScoping = directorySession.EnforceContainerizedScoping;
						bool flag = directorySession is IRecipientSession;
						if (rootId == null && !string.IsNullOrEmpty(identity.DistinguishedName))
						{
							if (!ADObjectId.Equals(identity, identity.DomainId) && directorySession.IsRootIdWithinScope<T>(identity.Parent))
							{
								rootId2 = identity.Parent;
							}
							else if (directorySession.IsRootIdWithinScope<T>(identity))
							{
								rootId2 = identity;
								if (flag)
								{
									directorySession.EnforceContainerizedScoping = false;
								}
							}
						}
						try
						{
							EnumerableWrapper<T> wrapper = EnumerableWrapper<T>.GetWrapper(this.PerformPrimarySearch<T>(new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Id, identity), rootId2, directorySession, true, optionalData));
							if (wrapper.HasElements())
							{
								return wrapper;
							}
						}
						finally
						{
							if (flag)
							{
								directorySession.EnforceContainerizedScoping = enforceContainerizedScoping;
							}
						}
					}
					catch (LocalizedException exception)
					{
						if (!TaskHelper.IsTaskKnownException(exception))
						{
							throw;
						}
					}
					IL_15F:
					if (identity.ObjectGuid != Guid.Empty)
					{
						return this.PerformPrimarySearch<T>(new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Guid, identity.ObjectGuid), rootId, directorySession, true, optionalData);
					}
				}
			}
			return EnumerableWrapper<T>.Empty;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00007B40 File Offset: 0x00005D40
		internal ExchangeConfigurationUnit GetConfigurationUnit(string orgName)
		{
			if (string.IsNullOrEmpty(orgName))
			{
				throw new ArgumentException("OrgName must contain a non-empty value", "orgName");
			}
			ExchangeConfigurationUnit exchangeConfigurationUnit = null;
			try
			{
				ADSessionSettings adsessionSettings = ADSessionSettings.FromTenantCUName(orgName);
				ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.PartiallyConsistent, adsessionSettings, 866, "GetConfigurationUnit", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\ADIdParameter.cs");
				adsessionSettings.TenantConsistencyMode = TenantConsistencyMode.IncludeRetiredTenants;
				exchangeConfigurationUnit = tenantConfigurationSession.GetExchangeConfigurationUnitByName(orgName);
			}
			catch (CannotResolveTenantNameException)
			{
			}
			SmtpDomain smtpDomain = null;
			if (exchangeConfigurationUnit == null && SmtpDomain.TryParse(orgName, out smtpDomain))
			{
				try
				{
					ADSessionSettings sessionSettings = ADSessionSettings.FromTenantAcceptedDomain(orgName);
					ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.PartiallyConsistent, sessionSettings, 890, "GetConfigurationUnit", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\ADIdParameter.cs");
					exchangeConfigurationUnit = tenantConfigurationSession.GetExchangeConfigurationUnitByNameOrAcceptedDomain(orgName);
				}
				catch (CannotResolveTenantNameException)
				{
				}
			}
			Guid externalDirectoryOrganizationId;
			if (exchangeConfigurationUnit == null && GuidHelper.TryParseGuid(orgName, out externalDirectoryOrganizationId))
			{
				try
				{
					PartitionId partitionIdByExternalDirectoryOrganizationId = ADAccountPartitionLocator.GetPartitionIdByExternalDirectoryOrganizationId(externalDirectoryOrganizationId);
					ADSessionSettings sessionSettings2 = ADSessionSettings.FromAllTenantsPartitionId(partitionIdByExternalDirectoryOrganizationId);
					ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.PartiallyConsistent, sessionSettings2, 911, "GetConfigurationUnit", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\ADIdParameter.cs");
					QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ExchangeConfigurationUnitSchema.ExternalDirectoryOrganizationId, externalDirectoryOrganizationId.ToString());
					ExchangeConfigurationUnit[] array = tenantConfigurationSession.Find<ExchangeConfigurationUnit>(ADSession.GetConfigurationUnitsRoot(partitionIdByExternalDirectoryOrganizationId.ForestFQDN), QueryScope.SubTree, filter, null, 0);
					if (array.Length == 1)
					{
						exchangeConfigurationUnit = array[0];
					}
				}
				catch (CannotResolveExternalDirectoryOrganizationIdException)
				{
				}
			}
			return exchangeConfigurationUnit;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00007C90 File Offset: 0x00005E90
		internal virtual IEnumerable<T> GetObjectsInOrganization<T>(string identityString, ADObjectId rootId, IDirectorySession session, OptionalIdentityData optionalData) where T : IConfigurable, new()
		{
			if (string.IsNullOrEmpty(identityString))
			{
				throw new ArgumentException("IdentityString must contain a non-empty value", "identityString");
			}
			EnumerableWrapper<T> wrapper = EnumerableWrapper<T>.GetWrapper(this.PerformPrimarySearch<T>(this.GetNameMatchingFilter(identityString, false), rootId, session, true, optionalData));
			if (!wrapper.HasElements() && this.IsWildcardDefined(identityString))
			{
				wrapper = EnumerableWrapper<T>.GetWrapper(this.PerformPrimarySearch<T>(this.GetNameMatchingFilter(identityString, true), rootId, session, true, optionalData));
			}
			return wrapper;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00007CF9 File Offset: 0x00005EF9
		internal QueryFilter CreateWildcardOrEqualFilter(ADPropertyDefinition schemaProperty, string name)
		{
			if (this.IsWildcardDefined(name))
			{
				return this.CreateWildcardFilter(schemaProperty, name);
			}
			return new ComparisonFilter(ComparisonOperator.Equal, schemaProperty, name);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00007D18 File Offset: 0x00005F18
		internal QueryFilter CreateWildcardFilter(ADPropertyDefinition schemaProperty, string identityString)
		{
			string text = identityString;
			MatchOptions matchOptions = MatchOptions.FullString;
			if (text.StartsWith("*") && text.EndsWith("*"))
			{
				if (text.Length <= 2)
				{
					return null;
				}
				text = text.Substring(1, text.Length - 2);
				matchOptions = MatchOptions.SubString;
			}
			else if (text.EndsWith("*"))
			{
				text = text.Substring(0, text.Length - 1);
				matchOptions = MatchOptions.Prefix;
			}
			else if (text.StartsWith("*"))
			{
				text = text.Substring(1);
				matchOptions = MatchOptions.Suffix;
			}
			return new TextFilter(schemaProperty, text, matchOptions, MatchFlags.IgnoreCase);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00007DA4 File Offset: 0x00005FA4
		internal bool IsNullScopedSession(IDirectorySession session)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (session.SessionSettings == null)
			{
				throw new ArgumentException("session.SessionSettings should not be null", "session");
			}
			return session.SessionSettings.IsGlobal || session.SessionSettings.RecipientReadScope == null;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00007DF4 File Offset: 0x00005FF4
		internal IEnumerable<T> PerformPrimarySearch<T>(QueryFilter filter, ADObjectId rootId, IDirectorySession session, bool deepSearch, OptionalIdentityData optionalData) where T : IConfigurable, new()
		{
			if (rootId != null && rootId.IsRelativeDn)
			{
				throw new ArgumentException("RootId cannot be a relative DN", "rootId");
			}
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (optionalData == null)
			{
				optionalData = new OptionalIdentityData();
			}
			if (session.ConfigScope == ConfigScopes.TenantLocal && rootId == null && !this.IsNullScopedSession(session))
			{
				if (optionalData.ConfigurationContainerRdn != null)
				{
					rootId = this.CreateContainerRootId(session, optionalData.ConfigurationContainerRdn);
				}
				else if (optionalData.RootOrgDomainContainerId != null && this.IsForestWideScopedSession(session))
				{
					rootId = optionalData.RootOrgDomainContainerId;
				}
			}
			QueryFilter filter2 = QueryFilter.AndTogether(new QueryFilter[]
			{
				filter,
				this.AdditionalQueryFilter,
				optionalData.AdditionalFilter
			});
			IEnumerable<T> enumerable = this.PerformSearch<T>(filter2, rootId, session, deepSearch);
			return EnumerableWrapper<T>.GetWrapper(enumerable, this.GetEnumerableFilter<T>());
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00007EBD File Offset: 0x000060BD
		internal virtual IEnumerable<T> PerformSearch<T>(QueryFilter filter, ADObjectId rootId, IDirectorySession session, bool deepSearch) where T : IConfigurable, new()
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			return ((IConfigDataProvider)session).FindPaged<T>(filter, rootId, deepSearch, null, 0);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00007EE0 File Offset: 0x000060E0
		internal virtual OrganizationId ResolveOrganizationIdBasedOnIdentity(OrganizationId executingUserOrgId)
		{
			if (this.orgIdResolved)
			{
				return this.resolvedOrganizationId;
			}
			if (!this.IsMultitenancyEnabled())
			{
				return OrganizationId.ForestWideOrgId;
			}
			if (executingUserOrgId != null && !executingUserOrgId.Equals(OrganizationId.ForestWideOrgId))
			{
				return executingUserOrgId;
			}
			ADObjectId id = null;
			string text;
			string text2;
			if (this.adObjectId != null)
			{
				this.resolvedOrganizationId = this.GetOrganizationId(executingUserOrgId, this.adObjectId);
			}
			else if (ADIdParameter.TryResolveCanonicalName(this.RawIdentity, out id))
			{
				this.resolvedOrganizationId = this.GetOrganizationId(executingUserOrgId, id);
			}
			else if (this.TryParseOrganizationName(out text, out text2))
			{
				if (this.IsWildcardDefined(text))
				{
					this.resolvedOrganizationId = OrganizationId.ForestWideOrgId;
				}
				else
				{
					this.resolvedOrganizationId = this.GetOrganizationId(executingUserOrgId, text);
				}
			}
			else
			{
				this.resolvedOrganizationId = null;
			}
			this.orgIdResolved = true;
			return this.resolvedOrganizationId;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00007FA8 File Offset: 0x000061A8
		protected static bool TryResolveCanonicalName(string canonicalName, out ADObjectId adObjectId)
		{
			adObjectId = null;
			SmtpDomain smtpDomain;
			if (!string.IsNullOrEmpty(canonicalName) && SmtpDomain.TryParse(canonicalName.Split(new char[]
			{
				'/'
			})[0], out smtpDomain))
			{
				try
				{
					string distinguishedName = NativeHelpers.DistinguishedNameFromCanonicalName(canonicalName);
					adObjectId = new ADObjectId(distinguishedName);
					return true;
				}
				catch (NameConversionException)
				{
				}
				return false;
			}
			return false;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00008008 File Offset: 0x00006208
		protected void UpdateInternalADObjectId(ADObjectId identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			ADObjectId adobjectId = this.adObjectId;
			this.adObjectId = identity;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00008026 File Offset: 0x00006226
		protected bool IsMultitenancyEnabled()
		{
			return ADSession.GetRootDomainNamingContextForLocalForest().Equals(ADSession.GetDomainNamingContextForLocalForest()) && !ADSession.IsBoundToAdam;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00008043 File Offset: 0x00006243
		protected bool MustScopeToSharedConfiguration(ExchangeConfigurationUnit configUnit)
		{
			return this.SharedTenantConfigurationMode == SharedTenantConfigurationMode.Static || (this.SharedTenantConfigurationMode == SharedTenantConfigurationMode.Dehydrateable && configUnit.IsDehydrated);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00008061 File Offset: 0x00006261
		protected virtual bool IsWildcardDefined(string name)
		{
			return name != null && (name.StartsWith("*") || name.EndsWith("*"));
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00008084 File Offset: 0x00006284
		private bool TryParseOrganizationName(out string organizationName, out string friendlyName)
		{
			string text = this.RawIdentity;
			int num = text.IndexOf('\\');
			organizationName = null;
			friendlyName = null;
			if (num >= 0)
			{
				if (num > 0 && num < text.Length - 1)
				{
					organizationName = text.Substring(0, num);
					friendlyName = text.Substring(num + 1);
					return true;
				}
				return false;
			}
			else
			{
				num = text.IndexOf('@');
				if (num > 0 && num < text.Length - 1)
				{
					organizationName = text.Substring(num + 1);
					friendlyName = text;
					return true;
				}
				return false;
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x000080FC File Offset: 0x000062FC
		private IEnumerable<T> GetMultitenancyObjects<T>(string identityString, ADObjectId rootId, IDirectorySession session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason) where T : IConfigurable, new()
		{
			if (identityString == null)
			{
				throw new ArgumentNullException("rawIdentity");
			}
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			notFoundReason = null;
			string text;
			string identityString2;
			if (!this.IsMultitenancyEnabled() || !this.TryParseOrganizationName(out text, out identityString2))
			{
				return EnumerableWrapper<T>.Empty;
			}
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(session.DomainController, true, session.ConsistencyMode, session.NetworkCredential, session.SessionSettings, 1403, "GetMultitenancyObjects", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\ADIdParameter.cs");
			tenantOrTopologyConfigurationSession.UseConfigNC = true;
			tenantOrTopologyConfigurationSession.LinkResolutionServer = session.LinkResolutionServer;
			if (!string.IsNullOrEmpty(session.DomainController) && tenantOrTopologyConfigurationSession.SessionSettings.CurrentOrganizationId.Equals(OrganizationId.ForestWideOrgId) && tenantOrTopologyConfigurationSession.SessionSettings.PartitionId.ForestFQDN != TopologyProvider.LocalForestFqdn)
			{
				return EnumerableWrapper<T>.Empty;
			}
			if (this.IsWildcardDefined(text))
			{
				notFoundReason = new LocalizedString?(Strings.ErrorOrganizationWildcard);
				return EnumerableWrapper<T>.Empty;
			}
			OrganizationId organizationId;
			if (this.orgIdResolved)
			{
				organizationId = this.resolvedOrganizationId;
			}
			else
			{
				organizationId = this.GetOrganizationId(session.SessionSettings.CurrentOrganizationId, text);
			}
			IDirectorySession directorySession = session;
			if (organizationId != null && !OrganizationId.ForestWideOrgId.Equals(organizationId) && session.SessionSettings != null && session.SessionSettings.CurrentOrganizationId.Equals(OrganizationId.ForestWideOrgId))
			{
				directorySession = TaskHelper.UnderscopeSessionToOrganization(session, organizationId, true);
			}
			if (organizationId != null && !OrganizationId.ForestWideOrgId.Equals(organizationId) && organizationId.Equals(directorySession.SessionSettings.CurrentOrganizationId))
			{
				IEnumerable<T> objectsInOrganization = this.GetObjectsInOrganization<T>(identityString2, rootId, directorySession, optionalData);
				return EnumerableWrapper<T>.GetWrapper(objectsInOrganization);
			}
			return EnumerableWrapper<T>.Empty;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000082A0 File Offset: 0x000064A0
		private ADObjectId CreateContainerRootId(IDirectorySession session, ADObjectId configRdn)
		{
			if (this.IsForestWideScopedSession(session))
			{
				return session.SessionSettings.RootOrgId.GetDescendantId(configRdn);
			}
			return session.SessionSettings.CurrentOrganizationId.ConfigurationUnit.GetDescendantId(configRdn);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000082D4 File Offset: 0x000064D4
		private bool IsForestWideScopedSession(IDirectorySession session)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (session.SessionSettings == null)
			{
				throw new ArgumentException("session.SessionSettings should not be null", "session");
			}
			return OrganizationId.ForestWideOrgId.Equals(session.SessionSettings.CurrentOrganizationId);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00008324 File Offset: 0x00006524
		private QueryFilter GetNameMatchingFilter(string identityString, bool wildcard)
		{
			QueryFilter queryFilter;
			if (wildcard)
			{
				queryFilter = this.CreateWildcardFilter(ADObjectSchema.Name, identityString);
			}
			else
			{
				queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, identityString);
			}
			if (this.AdditionalMatchingProperties == null || this.AdditionalMatchingProperties.Length == 0)
			{
				return queryFilter;
			}
			QueryFilter[] array = new QueryFilter[this.AdditionalMatchingProperties.Length];
			for (int i = 0; i < this.AdditionalMatchingProperties.Length; i++)
			{
				array[i] = (wildcard ? this.CreateWildcardFilter(this.AdditionalMatchingProperties[i], identityString) : new ComparisonFilter(ComparisonOperator.Equal, this.AdditionalMatchingProperties[i], identityString));
			}
			QueryFilter queryFilter2 = QueryFilter.OrTogether(array);
			return QueryFilter.OrTogether(new QueryFilter[]
			{
				queryFilter2,
				queryFilter
			});
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000083D0 File Offset: 0x000065D0
		private string TryResolveRedactedPii(string value)
		{
			if (PiiMapManager.ContainsRedactedPiiValue(value))
			{
				string text = PiiMapManager.Instance.ResolveRedactedValue(value);
				this.HasRedactedPiiData = true;
				if (text != value)
				{
					this.IsRedactedPiiResolved = true;
					this.OriginalRedactedPiiData = value;
				}
				return text;
			}
			return value;
		}

		// Token: 0x04000090 RID: 144
		internal const char ElementSeparatorChar = '\\';

		// Token: 0x04000091 RID: 145
		private static string localForestDomainNamingContext;

		// Token: 0x04000092 RID: 146
		private ADObjectId adObjectId;

		// Token: 0x04000093 RID: 147
		private string rawIdentity;

		// Token: 0x04000094 RID: 148
		private string displayName;

		// Token: 0x04000095 RID: 149
		private OrganizationId resolvedOrganizationId;

		// Token: 0x04000096 RID: 150
		private bool orgIdResolved;

		// Token: 0x04000097 RID: 151
		[NonSerialized]
		private bool hasRedactedPiiData;

		// Token: 0x04000098 RID: 152
		[NonSerialized]
		private bool isRedactedPiiResolved;

		// Token: 0x04000099 RID: 153
		[NonSerialized]
		private string originalRedactedPiiData;
	}
}
