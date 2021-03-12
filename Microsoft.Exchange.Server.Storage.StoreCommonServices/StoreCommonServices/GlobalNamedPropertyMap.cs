using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000071 RID: 113
	public static class GlobalNamedPropertyMap
	{
		// Token: 0x0600046D RID: 1133 RVA: 0x0001C9E8 File Offset: 0x0001ABE8
		private static GlobalNamedPropertyMap._CriticalConsistencyBlock Critical()
		{
			return new GlobalNamedPropertyMap._CriticalConsistencyBlock
			{
				CriticalBlockActive = true
			};
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0001CA06 File Offset: 0x0001AC06
		public static void Initialize()
		{
			GlobalNamedPropertyMap.nameToDefinitionMap = new Dictionary<StorePropName, StoreNamedPropInfo>(4096);
			GlobalNamedPropertyMap.lockObject = new object();
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0001CA24 File Offset: 0x0001AC24
		internal static void AddPropDefinition(Context context, ref StoreNamedPropInfo namedPropertyInfo)
		{
			StorePropName propName = namedPropertyInfo.PropName;
			using (GlobalNamedPropertyMap._CriticalConsistencyBlock criticalConsistencyBlock = GlobalNamedPropertyMap.Critical())
			{
				using (LockManager.Lock(GlobalNamedPropertyMap.lockObject, context.Diagnostics))
				{
					if (GlobalNamedPropertyMap.nameToDefinitionMap.Count > 50000)
					{
						GlobalNamedPropertyMap.nameToDefinitionMap.Clear();
					}
					StoreNamedPropInfo storeNamedPropInfo;
					if (!GlobalNamedPropertyMap.nameToDefinitionMap.TryGetValue(propName, out storeNamedPropInfo))
					{
						GlobalNamedPropertyMap.nameToDefinitionMap[propName] = namedPropertyInfo;
					}
					else
					{
						namedPropertyInfo = storeNamedPropInfo;
					}
				}
				criticalConsistencyBlock.Success();
			}
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0001CAD0 File Offset: 0x0001ACD0
		internal static bool GetDefinitionFromName(Context context, StorePropName propName, out StoreNamedPropInfo namedPropertyInfo)
		{
			bool result = false;
			namedPropertyInfo = null;
			using (GlobalNamedPropertyMap._CriticalConsistencyBlock criticalConsistencyBlock = GlobalNamedPropertyMap.Critical())
			{
				using (LockManager.Lock(GlobalNamedPropertyMap.lockObject, context.Diagnostics))
				{
					if (GlobalNamedPropertyMap.nameToDefinitionMap.TryGetValue(propName, out namedPropertyInfo))
					{
						result = true;
					}
				}
				criticalConsistencyBlock.Success();
			}
			return result;
		}

		// Token: 0x040003AA RID: 938
		private static Dictionary<StorePropName, StoreNamedPropInfo> nameToDefinitionMap;

		// Token: 0x040003AB RID: 939
		private static object lockObject;

		// Token: 0x02000072 RID: 114
		private struct _CriticalConsistencyBlock : IDisposable
		{
			// Token: 0x06000471 RID: 1137 RVA: 0x0001CB50 File Offset: 0x0001AD50
			public void Dispose()
			{
				if (this.CriticalBlockActive)
				{
					Globals.AssertRetail(false, "forced crash");
				}
			}

			// Token: 0x06000472 RID: 1138 RVA: 0x0001CB65 File Offset: 0x0001AD65
			public void Success()
			{
				this.CriticalBlockActive = false;
			}

			// Token: 0x040003AC RID: 940
			public bool CriticalBlockActive;
		}
	}
}
