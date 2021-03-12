using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200016F RID: 367
	[ImmutableObject(true)]
	[Serializable]
	public sealed class OrganizationId : IEquatable<OrganizationId>, IOrganizationIdForEventLog
	{
		// Token: 0x06000F73 RID: 3955 RVA: 0x00049B76 File Offset: 0x00047D76
		private OrganizationId()
		{
			this.orgUnit = null;
			this.configUnit = null;
			this.partitionId = null;
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x00049B93 File Offset: 0x00047D93
		internal OrganizationId(ADObjectId orgUnit, ADObjectId configUnit)
		{
			this.Initialize(orgUnit, configUnit);
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000F75 RID: 3957 RVA: 0x00049BA3 File Offset: 0x00047DA3
		public ADObjectId OrganizationalUnit
		{
			get
			{
				return this.orgUnit;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000F76 RID: 3958 RVA: 0x00049BAB File Offset: 0x00047DAB
		public ADObjectId ConfigurationUnit
		{
			get
			{
				return this.configUnit;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000F77 RID: 3959 RVA: 0x00049BB3 File Offset: 0x00047DB3
		internal PartitionId PartitionId
		{
			get
			{
				if (this.partitionId == null)
				{
					this.partitionId = PartitionId.LocalForest;
				}
				return this.partitionId;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x00049BD4 File Offset: 0x00047DD4
		string IOrganizationIdForEventLog.IdForEventLog
		{
			get
			{
				if (this.Equals(OrganizationId.ForestWideOrgId))
				{
					return string.Empty;
				}
				if (this.ConfigurationUnit != null)
				{
					return this.ConfigurationUnit.DistinguishedName ?? this.ConfigurationUnit.ObjectGuid.ToString();
				}
				if (this.OrganizationalUnit != null)
				{
					return this.OrganizationalUnit.DistinguishedName ?? this.OrganizationalUnit.ObjectGuid.ToString();
				}
				return string.Empty;
			}
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x00049C5B File Offset: 0x00047E5B
		public static bool operator ==(OrganizationId a, OrganizationId b)
		{
			return object.ReferenceEquals(a, b) || (a != null && b != null && a.Equals(b));
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x00049C77 File Offset: 0x00047E77
		public static bool operator !=(OrganizationId a, OrganizationId b)
		{
			return !(a == b);
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x00049C83 File Offset: 0x00047E83
		public override bool Equals(object obj)
		{
			return this.Equals(obj as OrganizationId);
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x00049C94 File Offset: 0x00047E94
		public bool Equals(OrganizationId other)
		{
			return !(other == null) && ((this.OrganizationalUnit == null && other.OrganizationalUnit == null) || (this.OrganizationalUnit != null && other.OrganizationalUnit != null && this.OrganizationalUnit.DistinguishedName.Equals(other.OrganizationalUnit.DistinguishedName, StringComparison.OrdinalIgnoreCase)));
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x00049CEF File Offset: 0x00047EEF
		public override int GetHashCode()
		{
			if (this.OrganizationalUnit == null)
			{
				return 0;
			}
			return this.OrganizationalUnit.DistinguishedName.ToLower().GetHashCode();
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x00049D10 File Offset: 0x00047F10
		public override string ToString()
		{
			if (this.orgUnit != null && this.configUnit != null)
			{
				return string.Format("{0} - {1}", this.orgUnit.ToCanonicalName(), this.configUnit.ToCanonicalName());
			}
			return string.Empty;
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x00049D48 File Offset: 0x00047F48
		internal static object Getter(IPropertyBag propertyBag)
		{
			OrganizationId organizationId = OrganizationId.ForestWideOrgId;
			ADObjectId adobjectId = (ADObjectId)propertyBag[ADObjectSchema.OrganizationalUnitRoot];
			ADObjectId adobjectId2 = (ADObjectId)propertyBag[ADObjectSchema.ConfigurationUnit];
			if (adobjectId != null && adobjectId2 != null)
			{
				organizationId = new OrganizationId();
				organizationId.Initialize(adobjectId, adobjectId2);
			}
			else if (adobjectId != null || adobjectId2 != null)
			{
				ADObjectId adobjectId3 = (ADObjectId)propertyBag[ADObjectSchema.Id];
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.ErrorInvalidOrganizationId((adobjectId3 != null) ? adobjectId3.ToDNString() : "<null>", (adobjectId != null) ? adobjectId.ToDNString() : "<null>", (adobjectId2 != null) ? adobjectId2.ToDNString() : "<null>"), ADObjectSchema.OrganizationId, null), null);
			}
			return organizationId;
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x00049DF4 File Offset: 0x00047FF4
		internal static void Setter(object value, IPropertyBag propertyBag)
		{
			OrganizationId organizationId = value as OrganizationId;
			propertyBag[ADObjectSchema.OrganizationalUnitRoot] = organizationId.OrganizationalUnit;
			propertyBag[ADObjectSchema.ConfigurationUnit] = organizationId.ConfigurationUnit;
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x00049E2C File Offset: 0x0004802C
		internal static bool TryCreateFromBytes(byte[] bytes, Encoding encoding, out OrganizationId orgId)
		{
			orgId = null;
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (bytes.Length < 6)
			{
				return false;
			}
			byte b = bytes[0];
			if (b <= 0)
			{
				return false;
			}
			int num = 1;
			int num2 = (int)(1 + 5 * b);
			ADObjectId adobjectId = null;
			ADObjectId adobjectId2 = null;
			for (int i = 0; i < (int)b; i++)
			{
				if (bytes.Length < num + 4 + 1)
				{
					return false;
				}
				OrganizationId.ByteRepresentationTag byteRepresentationTag = (OrganizationId.ByteRepresentationTag)bytes[num++];
				int num3 = BitConverter.ToInt32(bytes, num);
				num += 4;
				if (num3 < 0)
				{
					return false;
				}
				if (num3 != 0 && bytes.Length < num2 + num3)
				{
					return false;
				}
				switch (byteRepresentationTag)
				{
				case OrganizationId.ByteRepresentationTag.ForestWideOrgIdTag:
					if (num3 != 0)
					{
						return false;
					}
					orgId = OrganizationId.ForestWideOrgId;
					return true;
				case OrganizationId.ByteRepresentationTag.OrgUnitTag:
					if (adobjectId != null || !ADObjectId.TryCreateFromBytes(bytes, num2, num3, encoding, out adobjectId))
					{
						return false;
					}
					break;
				case OrganizationId.ByteRepresentationTag.ConfigUnitTag:
					if (adobjectId2 != null || !ADObjectId.TryCreateFromBytes(bytes, num2, num3, encoding, out adobjectId2))
					{
						return false;
					}
					if (!ADSession.IsTenantConfigObjectInCorrectNC(adobjectId2))
					{
						return false;
					}
					break;
				}
				num2 += num3;
			}
			if (adobjectId == null || adobjectId2 == null)
			{
				return false;
			}
			orgId = new OrganizationId();
			orgId.Initialize(adobjectId, adobjectId2);
			return true;
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x00049F34 File Offset: 0x00048134
		internal byte[] GetBytes(Encoding encoding)
		{
			byte[] array;
			if (this.orgUnit == null)
			{
				array = new byte[6];
				array[0] = 1;
				array[1] = 0;
				ExBitConverter.Write(0, array, 2);
			}
			else
			{
				if (this.configUnit.ObjectGuid == Guid.Empty || this.orgUnit.ObjectGuid == Guid.Empty)
				{
					throw new InvalidOperationException("OrganizationId is not fully populated and cannot be serialized");
				}
				int byteCount = this.orgUnit.GetByteCount(encoding);
				int byteCount2 = this.configUnit.GetByteCount(encoding);
				array = new byte[byteCount + byteCount2 + 8 + 3];
				int num = 0;
				array[num++] = 2;
				array[num++] = 1;
				num += ExBitConverter.Write(byteCount, array, num);
				array[num++] = 2;
				num += ExBitConverter.Write(byteCount2, array, num);
				this.orgUnit.GetBytes(encoding, array, num);
				num += byteCount;
				this.configUnit.GetBytes(encoding, array, num);
			}
			return array;
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x0004A04C File Offset: 0x0004824C
		internal static OrganizationId FromExternalDirectoryOrganizationId(Guid externalDirectoryOrganizationId)
		{
			string tenantContainerCN;
			PartitionId partitionIdByExternalDirectoryOrganizationId = ADAccountPartitionLocator.GetPartitionIdByExternalDirectoryOrganizationId(externalDirectoryOrganizationId, out tenantContainerCN);
			return OrganizationId.FromPartition<Guid>(externalDirectoryOrganizationId, externalDirectoryOrganizationId, tenantContainerCN, partitionIdByExternalDirectoryOrganizationId, (ITenantConfigurationSession session) => session.GetOrganizationIdFromExternalDirectoryOrgId(externalDirectoryOrganizationId), () => new CannotResolveExternalDirectoryOrganizationIdException(DirectoryStrings.CannotFindTenantCUByExternalDirectoryId(externalDirectoryOrganizationId.ToString())));
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x0004A0A0 File Offset: 0x000482A0
		internal static OrganizationId FromTenantForestAndCN(string exoAccountForest, string exoTenantContainer)
		{
			PartitionId partitionId = new PartitionId(exoAccountForest);
			ADSessionSettings sessionSettings = ADSessionSettings.FromAllTenantsPartitionId(partitionId);
			ITenantConfigurationSession session = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 505, "FromTenantForestAndCN", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\OrganizationId.cs");
			return OrganizationId.FromTenantContainerCN(exoTenantContainer, session);
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x0004A108 File Offset: 0x00048308
		internal static OrganizationId FromAcceptedDomain(string acceptedDomain)
		{
			string tenantContainerCN;
			Guid guid;
			PartitionId partitionIdByAcceptedDomainName = ADAccountPartitionLocator.GetPartitionIdByAcceptedDomainName(acceptedDomain, out tenantContainerCN, out guid);
			return OrganizationId.FromPartition<string>(acceptedDomain, guid, tenantContainerCN, partitionIdByAcceptedDomainName, (ITenantConfigurationSession session) => session.GetOrganizationIdFromOrgNameOrAcceptedDomain(acceptedDomain), () => new CannotResolveTenantNameException(DirectoryStrings.CannotFindTenantCUByAcceptedDomain(acceptedDomain)));
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x0004A18C File Offset: 0x0004838C
		internal static OrganizationId FromMSAUserNetID(string msaUserNetID)
		{
			Guid externalDirectoryOrganizationId;
			string tenantContainerCN;
			PartitionId partitionIdByMSAUserNetID = ADAccountPartitionLocator.GetPartitionIdByMSAUserNetID(msaUserNetID, out tenantContainerCN, out externalDirectoryOrganizationId);
			return OrganizationId.FromPartition<Guid>(externalDirectoryOrganizationId, externalDirectoryOrganizationId, tenantContainerCN, partitionIdByMSAUserNetID, (ITenantConfigurationSession session) => session.GetOrganizationIdFromExternalDirectoryOrgId(externalDirectoryOrganizationId), () => new CannotResolveExternalDirectoryOrganizationIdException(DirectoryStrings.CannotFindTenantCUByExternalDirectoryId(externalDirectoryOrganizationId.ToString())));
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x0004A1DC File Offset: 0x000483DC
		private static OrganizationId FromPartition<T>(T lookupKey, Guid externalDirectoryOrganizationId, string tenantContainerCN, PartitionId partitionId, Func<ITenantConfigurationSession, OrganizationId> GetOrgFromAD, Func<Exception> CreateNotFoundException)
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromAllTenantsPartitionId(partitionId);
			ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 561, "FromPartition", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\OrganizationId.cs");
			if (!string.IsNullOrEmpty(tenantContainerCN))
			{
				OrganizationId organizationId = OrganizationId.FromTenantContainerCN(tenantContainerCN, tenantConfigurationSession);
				organizationId.externalDirectoryOrganizationId = new Guid?(externalDirectoryOrganizationId);
				return organizationId;
			}
			OrganizationId organizationId2 = GetOrgFromAD(tenantConfigurationSession);
			if (organizationId2 != null)
			{
				ExTraceGlobals.GLSTracer.TraceDebug<OrganizationId, T>(0L, "[OrganizationIdConvertor.FromExternalDirectoryOrganizationId]: Found OrganizationId {0} for {1}", organizationId2, lookupKey);
				Guid value = Guid.Empty;
				if (externalDirectoryOrganizationId == Guid.Empty)
				{
					ADRawEntry adrawEntry = tenantConfigurationSession.ReadADRawEntry(organizationId2.ConfigurationUnit, new PropertyDefinition[]
					{
						ExchangeConfigurationUnitSchema.ExternalDirectoryOrganizationId
					});
					if (adrawEntry != null)
					{
						Guid.TryParse(adrawEntry[ExchangeConfigurationUnitSchema.ExternalDirectoryOrganizationId].ToString(), out value);
					}
				}
				else
				{
					value = externalDirectoryOrganizationId;
				}
				organizationId2.externalDirectoryOrganizationId = new Guid?(value);
				return organizationId2;
			}
			throw CreateNotFoundException();
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x0004A2C0 File Offset: 0x000484C0
		private static OrganizationId FromTenantContainerCN(string tenantContainerCN, ITenantConfigurationSession session)
		{
			ADObjectId exchangeConfigurationUnitIdByName = session.GetExchangeConfigurationUnitIdByName(tenantContainerCN);
			ADObjectId childId = session.GetHostedOrganizationsRoot().GetChildId("OU", tenantContainerCN);
			OrganizationId organizationId = new OrganizationId();
			organizationId.Initialize(childId, exchangeConfigurationUnitIdByName);
			return organizationId;
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x0004A2F6 File Offset: 0x000484F6
		internal void EnsureFullyPopulated()
		{
			if (this.configUnit.ObjectGuid == Guid.Empty || this.orgUnit.ObjectGuid == Guid.Empty)
			{
				this.PopulateGuidsAndExternalDirectoryOrganizationId();
			}
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x0004A32C File Offset: 0x0004852C
		private void PopulateGuidsAndExternalDirectoryOrganizationId()
		{
			ExchangeConfigurationUnit exchangeConfigurationUnit = this.ReadCU();
			this.configUnit = exchangeConfigurationUnit.OrganizationId.configUnit;
			this.orgUnit = exchangeConfigurationUnit.OrganizationId.OrganizationalUnit;
			Guid value;
			if (Guid.TryParse(exchangeConfigurationUnit.ExternalDirectoryOrganizationId, out value))
			{
				this.externalDirectoryOrganizationId = new Guid?(value);
			}
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0004A380 File Offset: 0x00048580
		internal string ToExternalDirectoryOrganizationId()
		{
			if (!Datacenter.IsMultiTenancyEnabled() || this == OrganizationId.ForestWideOrgId)
			{
				ExTraceGlobals.GLSTracer.TraceDebug<string>(0L, "[OrganizationIdConvertor.ToExternalDirectoryOrganizationId]: Returning string.Empty because {0}.", Datacenter.IsMultiTenancyEnabled() ? "orgId is ForestWideOrgId" : "multitenancy is not enabled");
				return string.Empty;
			}
			if (this.externalDirectoryOrganizationId == null)
			{
				this.PopulateGuidsAndExternalDirectoryOrganizationId();
			}
			return this.externalDirectoryOrganizationId.ToString();
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0004A3F0 File Offset: 0x000485F0
		private ExchangeConfigurationUnit ReadCU()
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 671, "ReadCU", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\OrganizationId.cs");
			ExchangeConfigurationUnit exchangeConfigurationUnit = tenantOrTopologyConfigurationSession.Read<ExchangeConfigurationUnit>(this.ConfigurationUnit);
			if (exchangeConfigurationUnit == null)
			{
				throw new CannotResolveTenantNameException(DirectoryStrings.TenantOrgContainerNotFoundException(this.configUnit.DistinguishedName));
			}
			return exchangeConfigurationUnit;
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x0004A448 File Offset: 0x00048648
		[Conditional("DEBUG")]
		private static void Dbg_CheckCallStack()
		{
			string stackTrace = Environment.StackTrace;
			if (!stackTrace.Contains("OrganizationIdGetter") && !stackTrace.Contains("FromTenantContainerCN"))
			{
				throw new NotSupportedException("OrganizationId's constructor can only be called from OrganizationIdGetter");
			}
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x0004A480 File Offset: 0x00048680
		private void Initialize(ADObjectId orgUnit, ADObjectId configUnit)
		{
			if (orgUnit == null)
			{
				throw new ArgumentNullException("orgUnit");
			}
			if (configUnit == null)
			{
				throw new ArgumentNullException("configUnit");
			}
			this.orgUnit = orgUnit;
			this.configUnit = configUnit;
			this.partitionId = ((orgUnit.DomainId != null && !PartitionId.IsLocalForestPartition(orgUnit.PartitionFQDN)) ? orgUnit.GetPartitionId() : PartitionId.LocalForest);
		}

		// Token: 0x04000917 RID: 2327
		private ADObjectId orgUnit;

		// Token: 0x04000918 RID: 2328
		private ADObjectId configUnit;

		// Token: 0x04000919 RID: 2329
		private PartitionId partitionId;

		// Token: 0x0400091A RID: 2330
		private Guid? externalDirectoryOrganizationId;

		// Token: 0x0400091B RID: 2331
		internal static OrganizationId ForestWideOrgId = new OrganizationId();

		// Token: 0x02000170 RID: 368
		private enum ByteRepresentationTag : byte
		{
			// Token: 0x0400091D RID: 2333
			ForestWideOrgIdTag,
			// Token: 0x0400091E RID: 2334
			OrgUnitTag,
			// Token: 0x0400091F RID: 2335
			ConfigUnitTag
		}
	}
}
