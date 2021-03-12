using System;
using System.Collections.Generic;
using Microsoft.Exchange.DxStore.Common;
using Microsoft.Win32;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000A0 RID: 160
	public static class ExtensionMethods
	{
		// Token: 0x060005F3 RID: 1523 RVA: 0x000167C0 File Offset: 0x000149C0
		public static IDistributedStoreKey OpenOrCreateKey(this IDistributedStoreKey key, string keyName, bool isIgnoreIfNotExist = false, ReadWriteConstraints constraints = null)
		{
			return key.OpenKey(keyName, DxStoreKeyAccessMode.CreateIfNotExist, isIgnoreIfNotExist, constraints);
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x000167CC File Offset: 0x000149CC
		public static bool SetValue<T>(this IDistributedStoreKey key, string propertyName, T propertyValue, bool isBestEffort = false, ReadWriteConstraints constraints = null)
		{
			RegistryValueKind valueKind = Utils.GetValueKind(propertyValue);
			return key.SetValue(propertyName, propertyValue, valueKind, isBestEffort, constraints);
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x000167F8 File Offset: 0x000149F8
		public static T GetValue<T>(this IDistributedStoreKey key, string propertyName, T defaultValue = default(T), ReadWriteConstraints constraints = null)
		{
			bool flag;
			return key.GetValue(propertyName, defaultValue, out flag, constraints);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00016810 File Offset: 0x00014A10
		public static T GetValue<T>(this IDistributedStoreKey key, string propertyName, T defaultValue, out bool isValueExist, ReadWriteConstraints constriants = null)
		{
			RegistryValueKind registryValueKind;
			object value = key.GetValue(propertyName, out isValueExist, out registryValueKind, constriants);
			if (isValueExist)
			{
				return (T)((object)value);
			}
			return defaultValue;
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00016838 File Offset: 0x00014A38
		public static IEnumerable<string> GetSubkeyNames(this IDistributedStoreKey parentKey, string subkeyName, ReadWriteConstraints constraints)
		{
			using (IDistributedStoreKey distributedStoreKey = parentKey.OpenKey(subkeyName, DxStoreKeyAccessMode.Read, true, constraints))
			{
				if (distributedStoreKey != null)
				{
					return distributedStoreKey.GetSubkeyNames(constraints);
				}
			}
			return null;
		}
	}
}
