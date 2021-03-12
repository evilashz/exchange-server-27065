using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000085 RID: 133
	public static class ExtensionMethods
	{
		// Token: 0x0600051B RID: 1307 RVA: 0x0001150C File Offset: 0x0000F70C
		public static InstanceGroupMemberConfig GetMemberConfig(this InstanceGroupConfig cfg, string memberName)
		{
			InstanceGroupMemberConfig result = null;
			if (memberName == null)
			{
				memberName = cfg.Self;
			}
			if (cfg.Members != null)
			{
				result = cfg.Members.FirstOrDefault((InstanceGroupMemberConfig mc) => Utils.IsEqual(mc.Name, memberName, StringComparison.OrdinalIgnoreCase));
			}
			return result;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00011564 File Offset: 0x0000F764
		public static string GetMemberNetworkAddress(this InstanceGroupConfig cfg, string target)
		{
			InstanceGroupMemberConfig memberConfig = cfg.GetMemberConfig(target);
			return EndpointBuilder.GetNetworkAddress(cfg.Self, target, (memberConfig != null) ? memberConfig.NetworkAddress : null, cfg.NameResolver, false);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00011598 File Offset: 0x0000F798
		public static string GetGroupEndPointAddress(this InstanceGroupConfig cfg, string interfaceName, string target, int portNumber, string protocolName, bool isUseDefaultGroupIdentifier)
		{
			return EndpointBuilder.ConstructEndpointAddress(interfaceName, cfg.ComponentName, cfg.Self, target, cfg.GetMemberNetworkAddress(target), isUseDefaultGroupIdentifier ? "B1563499-EA40-4101-A9E6-59A8EB26FF1E" : cfg.Name, cfg.IsZeroboxMode, portNumber, protocolName);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x000115DC File Offset: 0x0000F7DC
		public static string GetEndPointAddress(this InstanceManagerConfig cfg, string target)
		{
			string networkAddress = EndpointBuilder.GetNetworkAddress(cfg.Self, target, cfg.NetworkAddress, cfg.NameResolver, false);
			return EndpointBuilder.ConstructEndpointAddress("Manager", cfg.ComponentName, cfg.Self, target, networkAddress, null, cfg.IsZeroboxMode, cfg.EndpointPortNumber, cfg.EndpointProtocolName);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0001162E File Offset: 0x0000F82E
		public static string GetStoreAccessEndPointAddress(this InstanceGroupConfig cfg, string target, bool isUseDefaultGroupIdentifier)
		{
			return cfg.GetGroupEndPointAddress("Access", target, cfg.Settings.AccessEndpointPortNumber, cfg.Settings.AccessEndpointProtocolName, isUseDefaultGroupIdentifier);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00011653 File Offset: 0x0000F853
		public static string GetStoreInstanceEndPointAddress(this InstanceGroupConfig cfg, string target, bool isUseDefaultGroupIdentifier)
		{
			return cfg.GetGroupEndPointAddress("Instance", target, cfg.Settings.InstanceEndpointPortNumber, cfg.Settings.InstanceEndpointProtocolName, isUseDefaultGroupIdentifier);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00011678 File Offset: 0x0000F878
		public static string ConstructUniqueAccessBindingName(this InstanceGroupConfig cfg, string target, bool isUseDefaultGroup)
		{
			target = ExtensionMethods.ResolveTarget(cfg.Self, target);
			return EndpointBuilder.ConstructUniqueBindingName(target, cfg.ComponentName, cfg.Settings.AccessEndpointProtocolName, "Access", isUseDefaultGroup ? "B1563499-EA40-4101-A9E6-59A8EB26FF1E" : cfg.Name, cfg.IsZeroboxMode);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x000116C8 File Offset: 0x0000F8C8
		public static string ConstructUniqueInstanceBindingName(this InstanceGroupConfig cfg, string target, bool isUseDefaultGroup)
		{
			target = ExtensionMethods.ResolveTarget(cfg.Self, target);
			return EndpointBuilder.ConstructUniqueBindingName(target, cfg.ComponentName, cfg.Settings.InstanceEndpointProtocolName, "Instance", isUseDefaultGroup ? "B1563499-EA40-4101-A9E6-59A8EB26FF1E" : cfg.Name, cfg.IsZeroboxMode);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00011715 File Offset: 0x0000F915
		public static string ConstructUniqueBindingName(this InstanceManagerConfig cfg, string target)
		{
			target = ExtensionMethods.ResolveTarget(cfg.Self, target);
			return EndpointBuilder.ConstructUniqueBindingName(target, cfg.ComponentName, cfg.EndpointProtocolName, "Manager", null, cfg.IsZeroboxMode);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x000117AC File Offset: 0x0000F9AC
		public static bool IsMembersChanged(this InstanceGroupConfig groupCfg, InstanceGroupMemberConfig[] members)
		{
			int num = (members != null) ? members.Length : 0;
			int num2 = (groupCfg != null) ? groupCfg.Members.Length : 0;
			if (num != num2)
			{
				return true;
			}
			if (groupCfg == null || members == null)
			{
				return false;
			}
			int num3 = members.Count((InstanceGroupMemberConfig member) => groupCfg.Members.Any((InstanceGroupMemberConfig gm) => Utils.IsEqual(gm.Name, member.Name, StringComparison.OrdinalIgnoreCase)));
			return num3 != num;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00011816 File Offset: 0x0000FA16
		public static void Log(this IDxStoreEventLogger logger, string periodicKey, TimeSpan? periodicDuration, DxEventSeverity severity, int id, string formatString, params object[] args)
		{
			if (periodicKey != null)
			{
				logger.LogPeriodic(periodicKey, periodicDuration.Value, severity, id, formatString, args);
				return;
			}
			logger.Log(severity, id, formatString, args);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00011840 File Offset: 0x0000FA40
		public static Dictionary<string, ServiceEndpoint> GetAllMemberAccessClientEndPoints(this InstanceGroupConfig cfg, bool isUseDefaultGroup = false, WcfTimeout timeout = null)
		{
			Dictionary<string, ServiceEndpoint> dictionary = new Dictionary<string, ServiceEndpoint>();
			foreach (InstanceGroupMemberConfig instanceGroupMemberConfig in cfg.Members)
			{
				ServiceEndpoint storeAccessEndpoint = cfg.GetStoreAccessEndpoint(instanceGroupMemberConfig.Name, false, false, timeout ?? cfg.Settings.StoreAccessWcfTimeout);
				dictionary[instanceGroupMemberConfig.Name] = storeAccessEndpoint;
			}
			return dictionary;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x000118A0 File Offset: 0x0000FAA0
		public static string ResolveNameBestEffort(this IServerNameResolver resolver, string shortName)
		{
			string result = null;
			try
			{
				if (resolver != null)
				{
					result = resolver.ResolveName(shortName);
				}
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x000118D0 File Offset: 0x0000FAD0
		public static void AppendSpaces(this StringBuilder sb, int count)
		{
			if (count > 0)
			{
				sb.Append(new string(' ', count));
			}
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x000118E5 File Offset: 0x0000FAE5
		public static string JoinWithComma(this IEnumerable<string> strArray, string defaultValue = "<null>")
		{
			if (strArray != null)
			{
				return string.Join(",", strArray);
			}
			return defaultValue;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x000118F7 File Offset: 0x0000FAF7
		public static string ToShortString(this DateTimeOffset dt)
		{
			return dt.ToString("yy/MM/dd hh:mm:ss.fff");
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00011905 File Offset: 0x0000FB05
		private static string ResolveTarget(string self, string target)
		{
			if (string.IsNullOrEmpty(self))
			{
				self = Environment.MachineName;
			}
			if (!string.IsNullOrEmpty(target))
			{
				return target;
			}
			return self;
		}
	}
}
