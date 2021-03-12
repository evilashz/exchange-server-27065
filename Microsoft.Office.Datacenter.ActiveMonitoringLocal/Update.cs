using System;
using System.Collections.Generic;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.Win32;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x0200009C RID: 156
	internal static class Update<TDefinition> where TDefinition : WorkDefinition, new()
	{
		// Token: 0x060007A6 RID: 1958 RVA: 0x0001FF60 File Offset: 0x0001E160
		internal static void ApplyUpdates(TDefinition definition)
		{
			WTFDiagnostics.TraceFunction<string, string, int>(WTFLog.DataAccess, Update<TDefinition>.traceContext, "Apply updates for {0} with name {1}, ID {2}", typeof(TDefinition).Name, definition.Name, definition.Id, null, "ApplyUpdates", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\Update.cs", 44);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			string registryPathForDefinition = Update<TDefinition>.GetRegistryPathForDefinition(definition);
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(registryPathForDefinition))
			{
				foreach (string text in registryKey.GetValueNames())
				{
					dictionary.Add(text, registryKey.GetValue(text) as string);
				}
			}
			((IPersistence)((object)definition)).SetProperties(dictionary);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00020034 File Offset: 0x0001E234
		internal static void CreateUpdateNoCheck(TDefinition definition, string property, object value)
		{
			WTFDiagnostics.TraceFunction<string, string, int, string, object>(WTFLog.DataAccess, Update<TDefinition>.traceContext, "Create update with no validation for {0} with name {1}, ID {2}, property {3}, value {4}", typeof(TDefinition).Name, definition.Name, definition.Id, property, value, null, "CreateUpdateNoCheck", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\Update.cs", 67);
			string registryPathForDefinition = Update<TDefinition>.GetRegistryPathForDefinition(definition);
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(registryPathForDefinition))
			{
				registryKey.SetValue(property, value.ToString());
			}
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x000200CC File Offset: 0x0001E2CC
		private static string GetRegistryPathForDefinition(TDefinition definition)
		{
			return string.Format("{0}\\{1}", Update<TDefinition>.RegistryPathBase, definition.Id);
		}

		// Token: 0x040005ED RID: 1517
		private static readonly string RegistryPathBase = string.Format("SOFTWARE\\Microsoft\\ExchangeServer\\{0}\\WorkerTaskFramework\\Update\\{1}", "v15", typeof(TDefinition).Name);

		// Token: 0x040005EE RID: 1518
		private static TracingContext traceContext = TracingContext.Default;
	}
}
