using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x0200023C RID: 572
	internal static class HygieneDataStrings
	{
		// Token: 0x060016FC RID: 5884 RVA: 0x00047364 File Offset: 0x00045564
		static HygieneDataStrings()
		{
			HygieneDataStrings.stringIDs.Add(2445616851U, "ErrorEmptyList");
			HygieneDataStrings.stringIDs.Add(590305096U, "ErrorPermanentDALException");
			HygieneDataStrings.stringIDs.Add(1719343280U, "ErrorEmptyGuid");
			HygieneDataStrings.stringIDs.Add(3093538361U, "ErrorDataStoreUnavailable");
			HygieneDataStrings.stringIDs.Add(797725276U, "ErrorTransientDALExceptionMaxRetries");
			HygieneDataStrings.stringIDs.Add(2833073812U, "ErrorTransientDALExceptionAmbientTransaction");
			HygieneDataStrings.stringIDs.Add(3127795643U, "ErrorTransactionNotSupported");
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x0004742C File Offset: 0x0004562C
		public static LocalizedString ErrorPropertyValueTypeMismatch(string propertyName, string propertyType, string valueType)
		{
			return new LocalizedString("ErrorPropertyValueTypeMismatch", "", false, false, HygieneDataStrings.ResourceManager, new object[]
			{
				propertyName,
				propertyType,
				valueType
			});
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x060016FE RID: 5886 RVA: 0x00047463 File Offset: 0x00045663
		public static LocalizedString ErrorEmptyList
		{
			get
			{
				return new LocalizedString("ErrorEmptyList", "", false, false, HygieneDataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x060016FF RID: 5887 RVA: 0x00047481 File Offset: 0x00045681
		public static LocalizedString ErrorPermanentDALException
		{
			get
			{
				return new LocalizedString("ErrorPermanentDALException", "", false, false, HygieneDataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x000474A0 File Offset: 0x000456A0
		public static LocalizedString ErrorInvalidArgumentTenantIdMismatch(Guid domainTargetEnvironmentTenantId, Guid targetServiceTenantId)
		{
			return new LocalizedString("ErrorInvalidArgumentTenantIdMismatch", "", false, false, HygieneDataStrings.ResourceManager, new object[]
			{
				domainTargetEnvironmentTenantId,
				targetServiceTenantId
			});
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x000474E0 File Offset: 0x000456E0
		public static LocalizedString ErrorQueryFilterType(string filter)
		{
			return new LocalizedString("ErrorQueryFilterType", "", false, false, HygieneDataStrings.ResourceManager, new object[]
			{
				filter
			});
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06001702 RID: 5890 RVA: 0x0004750F File Offset: 0x0004570F
		public static LocalizedString ErrorEmptyGuid
		{
			get
			{
				return new LocalizedString("ErrorEmptyGuid", "", false, false, HygieneDataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x00047530 File Offset: 0x00045730
		public static LocalizedString ErrorInvalidArgumentDomainKeyMismatch(string domainTargetEnvironmentDomainKey, string targetServiceDomainKey)
		{
			return new LocalizedString("ErrorInvalidArgumentDomainKeyMismatch", "", false, false, HygieneDataStrings.ResourceManager, new object[]
			{
				domainTargetEnvironmentDomainKey,
				targetServiceDomainKey
			});
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x00047564 File Offset: 0x00045764
		public static LocalizedString ErrorInvalidArgumentDomainNameMismatch(string domainTargetEnvironmentDomainName, string targetServiceDomainName)
		{
			return new LocalizedString("ErrorInvalidArgumentDomainNameMismatch", "", false, false, HygieneDataStrings.ResourceManager, new object[]
			{
				domainTargetEnvironmentDomainName,
				targetServiceDomainName
			});
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06001705 RID: 5893 RVA: 0x00047597 File Offset: 0x00045797
		public static LocalizedString ErrorDataStoreUnavailable
		{
			get
			{
				return new LocalizedString("ErrorDataStoreUnavailable", "", false, false, HygieneDataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06001706 RID: 5894 RVA: 0x000475B5 File Offset: 0x000457B5
		public static LocalizedString ErrorTransientDALExceptionMaxRetries
		{
			get
			{
				return new LocalizedString("ErrorTransientDALExceptionMaxRetries", "", false, false, HygieneDataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x000475D4 File Offset: 0x000457D4
		public static LocalizedString ErrorInvalidDataStoreType(string storeType)
		{
			return new LocalizedString("ErrorInvalidDataStoreType", "", false, false, HygieneDataStrings.ResourceManager, new object[]
			{
				storeType
			});
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x00047604 File Offset: 0x00045804
		public static LocalizedString ErrorMultipleMatchForUserProxy(string proxyAddress, string matchingIds)
		{
			return new LocalizedString("ErrorMultipleMatchForUserProxy", "", false, false, HygieneDataStrings.ResourceManager, new object[]
			{
				proxyAddress,
				matchingIds
			});
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x00047638 File Offset: 0x00045838
		public static LocalizedString ErrorCannotFindClientCertificate(string certificateName)
		{
			return new LocalizedString("ErrorCannotFindClientCertificate", "", false, false, HygieneDataStrings.ResourceManager, new object[]
			{
				certificateName
			});
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x00047668 File Offset: 0x00045868
		public static LocalizedString ErrorUnsupportedInterface(string typeName, string interfaceName)
		{
			return new LocalizedString("ErrorUnsupportedInterface", "", false, false, HygieneDataStrings.ResourceManager, new object[]
			{
				typeName,
				interfaceName
			});
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x0600170B RID: 5899 RVA: 0x0004769B File Offset: 0x0004589B
		public static LocalizedString ErrorTransientDALExceptionAmbientTransaction
		{
			get
			{
				return new LocalizedString("ErrorTransientDALExceptionAmbientTransaction", "", false, false, HygieneDataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x000476BC File Offset: 0x000458BC
		public static LocalizedString ErrorDataProviderIsTooBusy(string store, string partition, string copyId)
		{
			return new LocalizedString("ErrorDataProviderIsTooBusy", "", false, false, HygieneDataStrings.ResourceManager, new object[]
			{
				store,
				partition,
				copyId
			});
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x000476F4 File Offset: 0x000458F4
		public static LocalizedString ErrorInvalidInstanceType(string typeName)
		{
			return new LocalizedString("ErrorInvalidInstanceType", "", false, false, HygieneDataStrings.ResourceManager, new object[]
			{
				typeName
			});
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x00047724 File Offset: 0x00045924
		public static LocalizedString ErrorInvalidDataOperationException(string dataOperationType)
		{
			return new LocalizedString("ErrorInvalidDataOperationException", "", false, false, HygieneDataStrings.ResourceManager, new object[]
			{
				dataOperationType
			});
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x00047754 File Offset: 0x00045954
		public static LocalizedString ErrorInvalidObjectTypeForSession(string session, string type)
		{
			return new LocalizedString("ErrorInvalidObjectTypeForSession", "", false, false, HygieneDataStrings.ResourceManager, new object[]
			{
				session,
				type
			});
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06001710 RID: 5904 RVA: 0x00047787 File Offset: 0x00045987
		public static LocalizedString ErrorTransactionNotSupported
		{
			get
			{
				return new LocalizedString("ErrorTransactionNotSupported", "", false, false, HygieneDataStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x000477A8 File Offset: 0x000459A8
		public static LocalizedString ErrorInvalidColumnType(string propName, string typeName)
		{
			return new LocalizedString("ErrorInvalidColumnType", "", false, false, HygieneDataStrings.ResourceManager, new object[]
			{
				propName,
				typeName
			});
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x000477DC File Offset: 0x000459DC
		public static LocalizedString ErrorObjectIdTypeNotSupported(string objectIdType)
		{
			return new LocalizedString("ErrorObjectIdTypeNotSupported", "", false, false, HygieneDataStrings.ResourceManager, new object[]
			{
				objectIdType
			});
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x0004780B File Offset: 0x00045A0B
		public static LocalizedString GetLocalizedString(HygieneDataStrings.IDs key)
		{
			return new LocalizedString(HygieneDataStrings.stringIDs[(uint)key], HygieneDataStrings.ResourceManager, new object[0]);
		}

		// Token: 0x04000C29 RID: 3113
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(7);

		// Token: 0x04000C2A RID: 3114
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Hygiene.Data.DataStrings", typeof(HygieneDataStrings).GetTypeInfo().Assembly);

		// Token: 0x0200023D RID: 573
		public enum IDs : uint
		{
			// Token: 0x04000C2C RID: 3116
			ErrorEmptyList = 2445616851U,
			// Token: 0x04000C2D RID: 3117
			ErrorPermanentDALException = 590305096U,
			// Token: 0x04000C2E RID: 3118
			ErrorEmptyGuid = 1719343280U,
			// Token: 0x04000C2F RID: 3119
			ErrorDataStoreUnavailable = 3093538361U,
			// Token: 0x04000C30 RID: 3120
			ErrorTransientDALExceptionMaxRetries = 797725276U,
			// Token: 0x04000C31 RID: 3121
			ErrorTransientDALExceptionAmbientTransaction = 2833073812U,
			// Token: 0x04000C32 RID: 3122
			ErrorTransactionNotSupported = 3127795643U
		}

		// Token: 0x0200023E RID: 574
		private enum ParamIDs
		{
			// Token: 0x04000C34 RID: 3124
			ErrorPropertyValueTypeMismatch,
			// Token: 0x04000C35 RID: 3125
			ErrorInvalidArgumentTenantIdMismatch,
			// Token: 0x04000C36 RID: 3126
			ErrorQueryFilterType,
			// Token: 0x04000C37 RID: 3127
			ErrorInvalidArgumentDomainKeyMismatch,
			// Token: 0x04000C38 RID: 3128
			ErrorInvalidArgumentDomainNameMismatch,
			// Token: 0x04000C39 RID: 3129
			ErrorInvalidDataStoreType,
			// Token: 0x04000C3A RID: 3130
			ErrorMultipleMatchForUserProxy,
			// Token: 0x04000C3B RID: 3131
			ErrorCannotFindClientCertificate,
			// Token: 0x04000C3C RID: 3132
			ErrorUnsupportedInterface,
			// Token: 0x04000C3D RID: 3133
			ErrorDataProviderIsTooBusy,
			// Token: 0x04000C3E RID: 3134
			ErrorInvalidInstanceType,
			// Token: 0x04000C3F RID: 3135
			ErrorInvalidDataOperationException,
			// Token: 0x04000C40 RID: 3136
			ErrorInvalidObjectTypeForSession,
			// Token: 0x04000C41 RID: 3137
			ErrorInvalidColumnType,
			// Token: 0x04000C42 RID: 3138
			ErrorObjectIdTypeNotSupported
		}
	}
}
