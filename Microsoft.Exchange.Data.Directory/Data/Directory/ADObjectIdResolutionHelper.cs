using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000054 RID: 84
	internal static class ADObjectIdResolutionHelper
	{
		// Token: 0x06000429 RID: 1065 RVA: 0x00017DA0 File Offset: 0x00015FA0
		private static IDirectorySession GetSessionForPartition(string partitionFQDN)
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromAccountPartitionRootOrgScopeSet(new PartitionId(partitionFQDN));
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.PartiallyConsistent, sessionSettings, 28, "GetSessionForPartition", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\ADObjectIdResolutionHelper.cs");
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00017DD2 File Offset: 0x00015FD2
		internal static ADObjectId ResolveDN(ADObjectId obj)
		{
			if (!ADGlobalConfigSettings.SoftLinkEnabled)
			{
				return obj;
			}
			if (obj == null || !string.IsNullOrEmpty(obj.DistinguishedName) || obj.ObjectGuid == Guid.Empty)
			{
				return obj;
			}
			return ADObjectIdResolutionHelper.ResolveADObject(obj);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00017E07 File Offset: 0x00016007
		internal static ADObjectId ResolveDNIfNecessary(ADObjectId obj)
		{
			if (ADSessionSettings.IsRunningOnCmdlet() || ConfigBase<AdDriverConfigSchema>.GetConfig<bool>("IsSoftLinkResolutionEnabledForAllProcesses"))
			{
				return ADObjectIdResolutionHelper.ResolveDN(obj);
			}
			return obj;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00017E24 File Offset: 0x00016024
		internal static ADObjectId ResolveADObjectIdWithoutCache(ADObjectId obj)
		{
			IDirectorySession sessionForPartition = ADObjectIdResolutionHelper.GetSessionForPartition(obj.PartitionFQDN);
			ADRawEntry adrawEntry = sessionForPartition.ReadADRawEntry(obj, ADObjectIdResolutionHelper.propertiesToRetrieve);
			if (adrawEntry != null)
			{
				return adrawEntry.Id;
			}
			return obj;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00017E58 File Offset: 0x00016058
		internal static ADObjectId ResolveSoftLink(ADObjectId obj)
		{
			if (!ADGlobalConfigSettings.SoftLinkEnabled)
			{
				return obj;
			}
			if (obj == null || string.IsNullOrEmpty(obj.DistinguishedName) || (obj.PartitionGuid != Guid.Empty && obj.ObjectGuid != Guid.Empty))
			{
				return obj;
			}
			return ADObjectIdResolutionHelper.ResolveADObjectIdWithoutCache(obj);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00017EAC File Offset: 0x000160AC
		private static ADObjectId ResolveADObject(ADObjectId obj)
		{
			bool config = ConfigBase<AdDriverConfigSchema>.GetConfig<bool>("IsSoftLinkResolutionCacheEnabled");
			if (config)
			{
				return ADObjectIdResolutionCache.Default.GetEntry(obj);
			}
			return ADObjectIdResolutionHelper.ResolveADObjectIdWithoutCache(obj);
		}

		// Token: 0x04000178 RID: 376
		private static readonly ADPropertyDefinition[] propertiesToRetrieve = new ADPropertyDefinition[]
		{
			ADObjectSchema.Id
		};
	}
}
