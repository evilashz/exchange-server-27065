using System;
using System.Globalization;
using System.Threading;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.Win32;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000085 RID: 133
	internal static class DefinitionIdGenerator<TDefinition> where TDefinition : WorkDefinition, new()
	{
		// Token: 0x060006E9 RID: 1769 RVA: 0x0001CFB8 File Offset: 0x0001B1B8
		public static void Initialize()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(DefinitionIdGenerator<TDefinition>.RegistryPath))
			{
				string[] valueNames = registryKey.GetValueNames();
				foreach (string name in valueNames)
				{
					object value = registryKey.GetValue(name);
					if (value is int && (int)value > DefinitionIdGenerator<TDefinition>.maxId)
					{
						DefinitionIdGenerator<TDefinition>.maxId = (int)value;
					}
				}
			}
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0001D03C File Offset: 0x0001B23C
		public static void AssignId(TDefinition definition)
		{
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(DefinitionIdGenerator<TDefinition>.RegistryPath))
			{
				string name = DefinitionIdGenerator<TDefinition>.ConstructValueName(definition);
				object value = registryKey.GetValue(name);
				if (value != null && value is int)
				{
					definition.Id = (int)value;
				}
				else
				{
					definition.Id = (Interlocked.Increment(ref DefinitionIdGenerator<TDefinition>.maxId) & int.MaxValue);
					registryKey.SetValue(name, definition.Id);
				}
			}
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0001D0DC File Offset: 0x0001B2DC
		public static int GetIdForNotification(string resultName)
		{
			return resultName.GetHashCode() | int.MinValue;
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0001D0EC File Offset: 0x0001B2EC
		private static string ConstructValueName(TDefinition definition)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}~{1}~{2}~{3}~{4}~{5}", new object[]
			{
				definition.ServiceName,
				definition.Name,
				definition.TargetResource,
				definition.TargetPartition,
				definition.TargetGroup,
				definition.TargetExtension
			});
		}

		// Token: 0x0400045F RID: 1119
		private static readonly string RegistryPath = string.Format("SOFTWARE\\Microsoft\\ExchangeServer\\{0}\\WorkerTaskFramework\\IdStore\\{1}", "v15", typeof(TDefinition).Name);

		// Token: 0x04000460 RID: 1120
		private static int maxId = 0;
	}
}
