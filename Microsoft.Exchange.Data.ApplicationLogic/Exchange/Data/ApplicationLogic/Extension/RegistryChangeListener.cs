using System;
using System.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.ApplicationLogic;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x0200010E RID: 270
	internal class RegistryChangeListener : DisposeTrackableBase
	{
		// Token: 0x06000B5E RID: 2910 RVA: 0x0002E2DC File Offset: 0x0002C4DC
		public RegistryChangeListener(string registryKey, EventArrivedEventHandler eventArrivedEventHandler)
		{
			registryKey = registryKey.Replace("\\", "\\\\");
			string text = string.Format("SELECT * FROM RegistryKeyChangeEvent WHERE Hive = 'HKEY_LOCAL_MACHINE' AND KeyPath = '{0}'", registryKey);
			try
			{
				WqlEventQuery query = new WqlEventQuery(text);
				this.watcher = new ManagementEventWatcher(query);
				this.watcher.EventArrived += eventArrivedEventHandler;
				this.watcher.Start();
				RegistryChangeListener.Tracer.TraceDebug<string>(0L, "Registry Watcher Started With Query: {0}", text);
			}
			catch (ManagementException ex)
			{
				RegistryChangeListener.Tracer.TraceDebug<string, string>(0L, "An error occurred when setting registry listener. Query: {0}, Message: {1} ", text, ex.Message);
			}
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0002E378 File Offset: 0x0002C578
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<RegistryChangeListener>(this);
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0002E380 File Offset: 0x0002C580
		protected override void InternalDispose(bool disposing)
		{
			if (!disposing)
			{
				return;
			}
			if (this.watcher == null)
			{
				return;
			}
			this.watcher.Stop();
			this.watcher.Dispose();
			this.watcher = null;
		}

		// Token: 0x040005C0 RID: 1472
		private static readonly Trace Tracer = ExTraceGlobals.ExtensionTracer;

		// Token: 0x040005C1 RID: 1473
		private ManagementEventWatcher watcher;
	}
}
