using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Extensibility
{
	// Token: 0x02000045 RID: 69
	internal class Registry
	{
		// Token: 0x0600019E RID: 414 RVA: 0x0000A361 File Offset: 0x00008561
		private Registry()
		{
			this.registryTable = new object[this.GetEnumTableSize(RegistryComponent.Application)][];
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000A380 File Offset: 0x00008580
		public static Registry Instance
		{
			get
			{
				return Registry.instance;
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000A387 File Offset: 0x00008587
		public void RegisterInstance<T>(RegistryComponent component, Enum subComponent, T instance) where T : class
		{
			this.AddTableEntry(component, subComponent, instance);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000A397 File Offset: 0x00008597
		public void RegisterFactory<T, P>(RegistryComponent component, Enum subComponent, Func<P, T> factory) where T : class where P : class
		{
			this.AddTableEntry(component, subComponent, factory);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000A3A4 File Offset: 0x000085A4
		public bool TryGetInstance<T>(RegistryComponent component, Enum subComponent, out T instance, out FaultDefinition faultDefinition, [CallerMemberName] string callerMember = null, [CallerFilePath] string callerFilePath = null, [CallerLineNumber] int callerLineNumber = 0) where T : class
		{
			return this.TryGetInstance<T, object>(component, subComponent, out instance, null, out faultDefinition, callerMember, callerFilePath, callerLineNumber);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000A3C4 File Offset: 0x000085C4
		public bool TryGetInstance<T, P>(RegistryComponent component, Enum subComponent, out T instance, P factoryParam, out FaultDefinition faultDefinition, [CallerMemberName] string callerMember = null, [CallerFilePath] string callerFilePath = null, [CallerLineNumber] int callerLineNumber = 0) where T : class where P : class
		{
			faultDefinition = null;
			int num = Convert.ToInt32(subComponent);
			if (this.registryTable[(int)component] != null && this.registryTable[(int)component].Length > num)
			{
				object obj = this.registryTable[(int)component][num];
				if (obj != null)
				{
					if (!(obj is Delegate))
					{
						instance = (obj as T);
						return instance != null;
					}
					Func<P, T> func = obj as Func<P, T>;
					if (func != null)
					{
						instance = func(factoryParam);
						return instance != null;
					}
				}
			}
			faultDefinition = FaultDefinition.FromErrorString(string.Format("REGISTRY: Could not find a type {0}, for component {1} and sub component {2}", typeof(T), component, subComponent), "TryGetInstance", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\TaskDistributionSystem\\TaskDistributionCommon\\Extensibility\\Registry.cs", 157);
			instance = default(T);
			return false;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000A494 File Offset: 0x00008694
		private void AddTableEntry(RegistryComponent component, Enum subComponent, object entry)
		{
			int num = Convert.ToInt32(subComponent);
			if (this.registryTable[(int)component] == null)
			{
				this.registryTable[(int)component] = new object[this.GetEnumTableSize(subComponent)];
			}
			this.registryTable[(int)component][num] = entry;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000A4DB File Offset: 0x000086DB
		private int GetEnumTableSize(Enum enumeration)
		{
			return (from object t in Enum.GetValues(enumeration.GetType())
			select Convert.ToInt32(t)).Max() + 1;
		}

		// Token: 0x0400012C RID: 300
		private static Registry instance = new Registry();

		// Token: 0x0400012D RID: 301
		private object[][] registryTable;
	}
}
