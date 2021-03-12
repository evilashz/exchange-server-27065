using System;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.Win32;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000039 RID: 57
	public abstract class MaintenanceWorkItem : WorkItem
	{
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x000118F0 File Offset: 0x0000FAF0
		public new MaintenanceDefinition Definition
		{
			get
			{
				return (MaintenanceDefinition)base.Definition;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x000118FD File Offset: 0x0000FAFD
		public new MaintenanceResult Result
		{
			get
			{
				return (MaintenanceResult)base.Result;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x0001190A File Offset: 0x0000FB0A
		public new IMaintenanceWorkBroker Broker
		{
			get
			{
				return (IMaintenanceWorkBroker)base.Broker;
			}
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00011918 File Offset: 0x0000FB18
		protected override bool ShouldTakeWatsonOnTimeout()
		{
			int maintenanceTimeoutWatsonHours = Settings.MaintenanceTimeoutWatsonHours;
			if (maintenanceTimeoutWatsonHours <= 0)
			{
				return false;
			}
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(MaintenanceWorkItem.registryPath))
			{
				if (registryKey == null)
				{
					return false;
				}
				DateTime utcNow = DateTime.UtcNow;
				try
				{
					long num = Convert.ToInt64(registryKey.GetValue("LastMaintenanceTimeoutWatson", 0));
					if (num > 0L)
					{
						DateTime value = DateTime.FromBinary(num);
						if (utcNow.Subtract(value) < TimeSpan.FromHours((double)maintenanceTimeoutWatsonHours))
						{
							return false;
						}
					}
				}
				catch (InvalidCastException)
				{
					return false;
				}
				registryKey.SetValue("LastMaintenanceTimeoutWatson", utcNow.ToBinary(), RegistryValueKind.QWord);
			}
			return true;
		}

		// Token: 0x0400033D RID: 829
		private static readonly string registryPath = string.Format("SOFTWARE\\Microsoft\\ExchangeServer\\{0}\\WorkerTaskFramework", "v15");
	}
}
