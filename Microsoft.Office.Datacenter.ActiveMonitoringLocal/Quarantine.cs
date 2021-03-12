using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.Win32;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x0200009B RID: 155
	internal static class Quarantine<TDefinition> where TDefinition : WorkDefinition
	{
		// Token: 0x060007A2 RID: 1954 RVA: 0x0001FD00 File Offset: 0x0001DF00
		static Quarantine()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(Quarantine<TDefinition>.RegistryPath))
			{
				string[] valueNames = registryKey.GetValueNames();
				DateTime utcNow = DateTime.UtcNow;
				foreach (string text in valueNames)
				{
					WTFDiagnostics.TraceInformation<string, string>(WTFLog.DataAccess, Quarantine<TDefinition>.traceContext, "Checking quarantine information for {0} at {1}", text, Quarantine<TDefinition>.RegistryPath, null, ".cctor", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\Quarantine.cs", 58);
					int key;
					if (int.TryParse(text, out key))
					{
						string text2 = registryKey.GetValue(text) as string;
						WTFDiagnostics.TraceInformation<string, string, string>(WTFLog.DataAccess, Quarantine<TDefinition>.traceContext, "Quarantine end date for {0} at {1} is {2}", text, Quarantine<TDefinition>.RegistryPath, text2, null, ".cctor", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\Quarantine.cs", 64);
						DateTime value;
						if (text2 != null && DateTime.TryParse(text2, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out value))
						{
							WTFDiagnostics.TraceInformation<string>(WTFLog.DataAccess, Quarantine<TDefinition>.traceContext, "Add quarantine information for {0} to cache", text, null, ".cctor", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\Quarantine.cs", 68);
							Quarantine<TDefinition>.quarantineInfo[key] = value;
						}
					}
				}
			}
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0001FE4C File Offset: 0x0001E04C
		internal static DateTime? GetQuarantineEndTime(TDefinition definition)
		{
			if (Quarantine<TDefinition>.quarantineInfo.ContainsKey(definition.Id))
			{
				return new DateTime?(Quarantine<TDefinition>.quarantineInfo[definition.Id]);
			}
			return null;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0001FE98 File Offset: 0x0001E098
		internal static DateTime SetQuarantine(int id)
		{
			DateTime dateTime = DateTime.UtcNow.AddHours((double)Settings.QuarantineHours);
			Quarantine<TDefinition>.quarantineInfo[id] = dateTime;
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(Quarantine<TDefinition>.RegistryPath))
			{
				registryKey.SetValue(id.ToString(), dateTime.ToString(CultureInfo.InvariantCulture));
			}
			return dateTime;
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0001FF0C File Offset: 0x0001E10C
		internal static void RemoveQuarantine(int id)
		{
			Quarantine<TDefinition>.quarantineInfo.Remove(id);
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(Quarantine<TDefinition>.RegistryPath))
			{
				registryKey.DeleteValue(id.ToString(), false);
			}
		}

		// Token: 0x040005EA RID: 1514
		private static readonly string RegistryPath = string.Format("SOFTWARE\\Microsoft\\ExchangeServer\\{0}\\WorkerTaskFramework\\Quarantine\\{1}", "v15", typeof(TDefinition).Name);

		// Token: 0x040005EB RID: 1515
		private static Dictionary<int, DateTime> quarantineInfo = new Dictionary<int, DateTime>();

		// Token: 0x040005EC RID: 1516
		private static TracingContext traceContext = TracingContext.Default;
	}
}
