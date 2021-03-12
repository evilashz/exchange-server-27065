using System;
using System.Collections.Generic;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x0200006D RID: 109
	public static class ExtensionMethods
	{
		// Token: 0x0600064B RID: 1611 RVA: 0x0001ABE0 File Offset: 0x00018DE0
		public static T GetValueOrDefault<K, T>(this IDictionary<K, T> dictionary, K key, T defaultValue)
		{
			T result;
			if (!dictionary.TryGetValue(key, out result))
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0001ABFC File Offset: 0x00018DFC
		public static int ReadAttribute(this WorkItem workItem, string name, int defaultValue)
		{
			int result;
			if (workItem.Definition.Attributes.ContainsKey(name) && int.TryParse(workItem.Definition.Attributes[name], out result))
			{
				return result;
			}
			return defaultValue;
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0001AC3C File Offset: 0x00018E3C
		public static double ReadAttribute(this WorkItem workItem, string name, double defaultValue)
		{
			double result;
			if (workItem.Definition.Attributes.ContainsKey(name) && double.TryParse(workItem.Definition.Attributes[name], out result))
			{
				return result;
			}
			return defaultValue;
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0001AC79 File Offset: 0x00018E79
		public static string ReadAttribute(this WorkItem workItem, string name, string defaultValue)
		{
			if (workItem.Definition.Attributes.ContainsKey(name) && !string.IsNullOrEmpty(workItem.Definition.Attributes[name]))
			{
				return workItem.Definition.Attributes[name];
			}
			return defaultValue;
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0001ACBC File Offset: 0x00018EBC
		public static TimeSpan ReadAttribute(this WorkItem workItem, string name, TimeSpan defaultValue)
		{
			TimeSpan result;
			if (workItem.Definition.Attributes.ContainsKey(name) && TimeSpan.TryParse(workItem.Definition.Attributes[name], out result))
			{
				return result;
			}
			return defaultValue;
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0001ACFC File Offset: 0x00018EFC
		public static bool ReadAttribute(this WorkItem workItem, string name, bool defaultValue)
		{
			bool result;
			if (workItem.Definition.Attributes.ContainsKey(name) && bool.TryParse(workItem.Definition.Attributes[name], out result))
			{
				return result;
			}
			return defaultValue;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0001AD3C File Offset: 0x00018F3C
		public static void CopyAttributes(this WorkItem workItem, IList<string> names, WorkDefinition destination)
		{
			foreach (string key in names)
			{
				if (workItem.Definition.Attributes.ContainsKey(key))
				{
					destination.Attributes[key] = workItem.Definition.Attributes[key];
				}
			}
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0001ADB0 File Offset: 0x00018FB0
		public static TDefinition ApplyModifier<TDefinition>(this TDefinition definition, Func<TDefinition, TDefinition> modifier) where TDefinition : WorkDefinition
		{
			return modifier(definition);
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0001ADB9 File Offset: 0x00018FB9
		public static TDefinition ApplyModifier<TDefinition, TArg>(this TDefinition definition, Func<TDefinition, TArg, TDefinition> modifier, TArg arg) where TDefinition : WorkDefinition
		{
			return modifier(definition, arg);
		}
	}
}
