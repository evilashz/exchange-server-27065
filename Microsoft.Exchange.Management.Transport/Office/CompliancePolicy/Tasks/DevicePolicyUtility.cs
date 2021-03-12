using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000E9 RID: 233
	internal static class DevicePolicyUtility
	{
		// Token: 0x06000955 RID: 2389 RVA: 0x000266C8 File Offset: 0x000248C8
		static DevicePolicyUtility()
		{
			DevicePolicyUtility.WorkloadToConfigurationRuleGuidMap = new Dictionary<Workload, Guid>();
			DevicePolicyUtility.WorkloadToConfigurationRuleGuidMap.Add(Workload.Exchange, DevicePolicyUtility.ExchangeDeviceConfigurationRuleGuid);
			DevicePolicyUtility.WorkloadToConfigurationRuleGuidMap.Add(Workload.Intune, DevicePolicyUtility.IntuneDeviceConfigurationRuleGuid);
			DevicePolicyUtility.WorkloadToConditionalAccessRuleGuidMap = new Dictionary<Workload, Guid>();
			DevicePolicyUtility.WorkloadToConditionalAccessRuleGuidMap.Add(Workload.Exchange, DevicePolicyUtility.ExchangeDeviceConditionalAccessRuleGuid);
			DevicePolicyUtility.WorkloadToConditionalAccessRuleGuidMap.Add(Workload.Intune, DevicePolicyUtility.IntuneDeviceConditionalAccessRuleGuid);
			DevicePolicyUtility.WorkloadToTenantConditionalAccessRuleGuidMap = new Dictionary<Workload, Guid>();
			DevicePolicyUtility.WorkloadToTenantConditionalAccessRuleGuidMap.Add(Workload.Intune, DevicePolicyUtility.IntuneDeviceTenantConditionalAccessRuleGuid);
			DevicePolicyUtility.WorkloadToTenantConditionalAccessPolicyGuidMap = new Dictionary<Workload, Guid>();
			DevicePolicyUtility.WorkloadToTenantConditionalAccessPolicyGuidMap.Add(Workload.Intune, DevicePolicyUtility.IntuneDeviceTenantConditionalAccessPolicyGuid);
			DevicePolicyUtility.ConfigurationRuleGuidSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			DevicePolicyUtility.ConfigurationRuleGuidSet.Add(DevicePolicyUtility.ExchangeDeviceConfigurationRuleGuid.ToString());
			DevicePolicyUtility.ConfigurationRuleGuidSet.Add(DevicePolicyUtility.IntuneDeviceConfigurationRuleGuid.ToString());
			DevicePolicyUtility.ConditionalAccessRuleGuidSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			DevicePolicyUtility.ConditionalAccessRuleGuidSet.Add(DevicePolicyUtility.ExchangeDeviceConditionalAccessRuleGuid.ToString());
			DevicePolicyUtility.ConditionalAccessRuleGuidSet.Add(DevicePolicyUtility.IntuneDeviceConditionalAccessRuleGuid.ToString());
			DevicePolicyUtility.TenantConditionalAccessRuleGuidSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			DevicePolicyUtility.TenantConditionalAccessRuleGuidSet.Add(DevicePolicyUtility.IntuneDeviceTenantConditionalAccessRuleGuid.ToString());
			DevicePolicyUtility.TenantConditionalAccessPolicyGuidSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			DevicePolicyUtility.TenantConditionalAccessPolicyGuidSet.Add(DevicePolicyUtility.IntuneDeviceTenantConditionalAccessPolicyGuid.ToString());
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x000268DE File Offset: 0x00024ADE
		internal static bool GetConfigurationRuleGuidFromWorkload(Workload workload, out Guid ruleGuid)
		{
			return DevicePolicyUtility.WorkloadToConfigurationRuleGuidMap.TryGetValue(workload, out ruleGuid);
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x000268EC File Offset: 0x00024AEC
		internal static bool GetConditionalAccessRuleGuidFromWorkload(Workload workload, out Guid ruleGuid)
		{
			return DevicePolicyUtility.WorkloadToConditionalAccessRuleGuidMap.TryGetValue(workload, out ruleGuid);
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x000268FA File Offset: 0x00024AFA
		internal static bool GetTenantRuleGuidFromWorkload(Workload workload, out Guid ruleGuid)
		{
			return DevicePolicyUtility.WorkloadToTenantConditionalAccessRuleGuidMap.TryGetValue(workload, out ruleGuid);
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00026908 File Offset: 0x00024B08
		internal static bool GetTenantPolicyGuidFromWorkload(Workload workload, out Guid ruleGuid)
		{
			return DevicePolicyUtility.WorkloadToTenantConditionalAccessPolicyGuidMap.TryGetValue(workload, out ruleGuid);
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x00026918 File Offset: 0x00024B18
		internal static bool IsDeviceConditionalAccessRule(string identity)
		{
			Guid ruleGuid;
			return Guid.TryParse(identity.Substring((identity.LastIndexOf('{') == -1) ? 0 : identity.LastIndexOf('{')), out ruleGuid) && DevicePolicyUtility.IsDeviceConditionalAccessRule(ruleGuid);
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00026954 File Offset: 0x00024B54
		internal static bool IsDeviceConfigurationRule(string identity)
		{
			Guid ruleGuid;
			return Guid.TryParse(identity.Substring((identity.LastIndexOf('{') == -1) ? 0 : identity.LastIndexOf('{')), out ruleGuid) && DevicePolicyUtility.IsDeviceConfigurationRule(ruleGuid);
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x00026990 File Offset: 0x00024B90
		internal static bool IsDeviceTenantRule(string identity)
		{
			Guid ruleGuid;
			return Guid.TryParse(identity.Substring((identity.LastIndexOf('{') == -1) ? 0 : identity.LastIndexOf('{')), out ruleGuid) && DevicePolicyUtility.IsDeviceTenantRule(ruleGuid);
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x000269CA File Offset: 0x00024BCA
		internal static bool IsDeviceTenantRule(Guid ruleGuid)
		{
			return DevicePolicyUtility.TenantConditionalAccessRuleGuidSet.Contains(ruleGuid.ToString());
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x000269E3 File Offset: 0x00024BE3
		internal static bool IsDeviceConfigurationRule(Guid ruleGuid)
		{
			return DevicePolicyUtility.ConfigurationRuleGuidSet.Contains(ruleGuid.ToString());
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x000269FC File Offset: 0x00024BFC
		internal static bool IsDeviceConditionalAccessRule(Guid ruleGuid)
		{
			return DevicePolicyUtility.ConditionalAccessRuleGuidSet.Contains(ruleGuid.ToString());
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x00026A18 File Offset: 0x00024C18
		internal static bool IsPropertySpecified(ADPresentationObject presentationObject, ADPropertyDefinition property)
		{
			object obj;
			return presentationObject != null && property != null && presentationObject.TryGetValueWithoutDefault(property, out obj);
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x00026A36 File Offset: 0x00024C36
		internal static void ValidateDeviceScenarioArgument(PolicyScenario scenario)
		{
			if (!DevicePolicyUtility.DevicePolicyScenarios.Contains(scenario))
			{
				throw new ArgumentException("Invalid Policy Scenario Argument");
			}
		}

		// Token: 0x040003FA RID: 1018
		private static readonly Guid ExchangeDeviceConfigurationRuleGuid = new Guid("4CD01950-43F9-47A1-AF0C-EA4E1BE47CBB");

		// Token: 0x040003FB RID: 1019
		private static readonly Guid IntuneDeviceConfigurationRuleGuid = new Guid("58B50D1C-2B18-461C-8893-3E20C648B136");

		// Token: 0x040003FC RID: 1020
		private static readonly Guid ExchangeDeviceConditionalAccessRuleGuid = new Guid("3CB6EC45-68E8-4758-935B-FCEFD71E234C");

		// Token: 0x040003FD RID: 1021
		private static readonly Guid IntuneDeviceConditionalAccessRuleGuid = new Guid("914F151C-394B-4DA9-9422-F5A2F65DEC30");

		// Token: 0x040003FE RID: 1022
		private static readonly Guid IntuneDeviceTenantConditionalAccessRuleGuid = new Guid("7577C5F3-05A4-4F55-A0A3-82AAB5E98C84");

		// Token: 0x040003FF RID: 1023
		private static readonly Guid IntuneDeviceTenantConditionalAccessPolicyGuid = new Guid("A6958701-C82C-4064-AC11-64E40E7F4032");

		// Token: 0x04000400 RID: 1024
		private static readonly Dictionary<Workload, Guid> WorkloadToConfigurationRuleGuidMap = null;

		// Token: 0x04000401 RID: 1025
		private static readonly Dictionary<Workload, Guid> WorkloadToConditionalAccessRuleGuidMap = null;

		// Token: 0x04000402 RID: 1026
		private static readonly Dictionary<Workload, Guid> WorkloadToTenantConditionalAccessRuleGuidMap = null;

		// Token: 0x04000403 RID: 1027
		private static readonly Dictionary<Workload, Guid> WorkloadToTenantConditionalAccessPolicyGuidMap = null;

		// Token: 0x04000404 RID: 1028
		private static readonly HashSet<string> ConfigurationRuleGuidSet = null;

		// Token: 0x04000405 RID: 1029
		private static readonly HashSet<string> ConditionalAccessRuleGuidSet = null;

		// Token: 0x04000406 RID: 1030
		private static readonly HashSet<string> TenantConditionalAccessRuleGuidSet = null;

		// Token: 0x04000407 RID: 1031
		private static readonly HashSet<string> TenantConditionalAccessPolicyGuidSet = null;

		// Token: 0x04000408 RID: 1032
		private static readonly IEnumerable<PolicyScenario> DevicePolicyScenarios = new PolicyScenario[]
		{
			PolicyScenario.DeviceConditionalAccess,
			PolicyScenario.DeviceSettings,
			PolicyScenario.DeviceTenantConditionalAccess
		};
	}
}
