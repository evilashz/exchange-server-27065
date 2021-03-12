using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x0200004A RID: 74
	internal static class OwsLogRegistry
	{
		// Token: 0x06000200 RID: 512 RVA: 0x0000A7BC File Offset: 0x000089BC
		public static void Register(string action, Type metadataEnumType, params Type[] additionalTypes)
		{
			if (OwsLogRegistry.IsRegistered(action))
			{
				return;
			}
			lock (OwsLogRegistry.RegisterLock)
			{
				if (!OwsLogRegistry.IsRegistered(action))
				{
					OwsLogRegistry.RegisterType(action, metadataEnumType);
					foreach (Type type in additionalTypes)
					{
						OwsLogRegistry.RegisterType(action, type);
					}
				}
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000A830 File Offset: 0x00008A30
		public static IEnumerable<Enum> GetRegisteredValues(string action)
		{
			HashSet<Enum> result;
			if (OwsLogRegistry.actionPropertyMappings.TryGetValue(action, out result))
			{
				return result;
			}
			return OwsLogRegistry.EmptySet;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000A853 File Offset: 0x00008A53
		private static bool IsRegistered(string action)
		{
			return OwsLogRegistry.actionPropertyMappings.ContainsKey(action);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000A860 File Offset: 0x00008A60
		private static void RegisterType(string action, Type type)
		{
			if (!type.GetTypeInfo().IsEnum)
			{
				throw new ArgumentException("Only enum types are allowed: " + type, "type");
			}
			HashSet<Enum> hashSet;
			if (!OwsLogRegistry.actionPropertyMappings.TryGetValue(action, out hashSet))
			{
				hashSet = new HashSet<Enum>();
				OwsLogRegistry.actionPropertyMappings[action] = hashSet;
			}
			foreach (object obj in Enum.GetValues(type))
			{
				Enum item = (Enum)obj;
				hashSet.Add(item);
			}
			ActivityContext.RegisterMetadata(type);
		}

		// Token: 0x040003D6 RID: 982
		private static readonly object RegisterLock = new object();

		// Token: 0x040003D7 RID: 983
		private static readonly IDictionary<string, HashSet<Enum>> actionPropertyMappings = new Dictionary<string, HashSet<Enum>>(64);

		// Token: 0x040003D8 RID: 984
		private static readonly HashSet<Enum> EmptySet = new HashSet<Enum>();
	}
}
