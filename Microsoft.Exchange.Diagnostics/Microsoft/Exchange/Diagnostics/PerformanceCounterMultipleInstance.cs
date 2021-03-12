using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200014B RID: 331
	public class PerformanceCounterMultipleInstance
	{
		// Token: 0x06000961 RID: 2401 RVA: 0x000235E1 File Offset: 0x000217E1
		public PerformanceCounterMultipleInstance(string categoryName, CreateInstanceDelegate instanceCreator)
		{
			this.instanceCreator = instanceCreator;
			this.category = PerformanceCounterFactory.CreatePerformanceCounterCategory(categoryName);
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0002360C File Offset: 0x0002180C
		public virtual PerformanceCounterInstance GetInstance(string instanceName)
		{
			return this.GetInstance(instanceName, null);
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x00023618 File Offset: 0x00021818
		public virtual void ResetInstance(string instanceName)
		{
			lock (this)
			{
				PerformanceCounterInstance instance;
				if (!this.cachedInstances.TryGetValue(instanceName, out instance))
				{
					if (!this.InstanceExists(instanceName))
					{
						return;
					}
					instance = this.GetInstance(instanceName);
				}
				instance.Reset();
			}
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x00023678 File Offset: 0x00021878
		public virtual void CloseInstance(string instanceName)
		{
			lock (this)
			{
				PerformanceCounterInstance performanceCounterInstance;
				if (this.cachedInstances.TryGetValue(instanceName, out performanceCounterInstance))
				{
					performanceCounterInstance.Close();
					this.cachedInstances.Remove(instanceName);
				}
			}
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x000236D0 File Offset: 0x000218D0
		public bool InstanceExists(string instanceName)
		{
			bool result;
			try
			{
				lock (this)
				{
					result = this.category.InstanceExists(instanceName);
				}
			}
			catch (InvalidOperationException)
			{
				result = false;
			}
			catch (Win32Exception)
			{
				result = false;
			}
			catch (FormatException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00023748 File Offset: 0x00021948
		public virtual string[] GetInstanceNames()
		{
			string[] instanceNames;
			try
			{
				lock (this)
				{
					instanceNames = this.category.GetInstanceNames();
				}
			}
			catch (InvalidOperationException)
			{
				instanceNames = PerformanceCounterMultipleInstance.zeroInstances;
			}
			catch (Win32Exception)
			{
				instanceNames = PerformanceCounterMultipleInstance.zeroInstances;
			}
			catch (FormatException)
			{
				instanceNames = PerformanceCounterMultipleInstance.zeroInstances;
			}
			return instanceNames;
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x000237CC File Offset: 0x000219CC
		public virtual void RemoveInstance(string instanceName)
		{
			lock (this)
			{
				this.RemoveInstanceInternal(instanceName);
			}
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x00023808 File Offset: 0x00021A08
		public virtual void RemoveAllInstances()
		{
			lock (this)
			{
				this.RemoveAllInstancesInternal();
			}
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00023844 File Offset: 0x00021A44
		public void GetPerfCounterDiagnosticsInfo(XElement element)
		{
			foreach (string instanceName in this.GetInstanceNames())
			{
				PerformanceCounterInstance instance = this.GetInstance(instanceName);
				if (instance != null)
				{
					instance.GetPerfCounterDiagnosticsInfo(element);
				}
			}
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0002387C File Offset: 0x00021A7C
		protected PerformanceCounterInstance GetInstance(string instanceName, PerformanceCounterInstance totalInstance)
		{
			PerformanceCounterInstance result;
			lock (this)
			{
				PerformanceCounterInstance performanceCounterInstance;
				if (!this.cachedInstances.TryGetValue(instanceName, out performanceCounterInstance))
				{
					performanceCounterInstance = this.InstanceCreator(instanceName, totalInstance);
					this.cachedInstances.Add(instanceName, performanceCounterInstance);
				}
				result = performanceCounterInstance;
			}
			return result;
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x000238E0 File Offset: 0x00021AE0
		protected virtual void RemoveInstanceInternal(string instanceName)
		{
			PerformanceCounterInstance instance;
			if (!this.cachedInstances.TryGetValue(instanceName, out instance))
			{
				if (!this.InstanceExists(instanceName))
				{
					return;
				}
				instance = this.GetInstance(instanceName);
			}
			this.cachedInstances.Remove(instanceName);
			instance.Reset();
			instance.Remove();
			instance.Close();
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x00023930 File Offset: 0x00021B30
		protected void RemoveAllInstancesInternal()
		{
			string[] instanceNames = this.GetInstanceNames();
			foreach (string instanceName in instanceNames)
			{
				this.RemoveInstanceInternal(instanceName);
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x0002395F File Offset: 0x00021B5F
		protected CreateInstanceDelegate InstanceCreator
		{
			get
			{
				return this.instanceCreator;
			}
		}

		// Token: 0x04000666 RID: 1638
		private CreateInstanceDelegate instanceCreator;

		// Token: 0x04000667 RID: 1639
		private IPerformanceCounterCategory category;

		// Token: 0x04000668 RID: 1640
		private static readonly string[] zeroInstances = new string[0];

		// Token: 0x04000669 RID: 1641
		private Dictionary<string, PerformanceCounterInstance> cachedInstances = new Dictionary<string, PerformanceCounterInstance>(StringComparer.OrdinalIgnoreCase);
	}
}
