using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.RecipientDLExpansion;
using Microsoft.Win32;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.RecipientDLExpansion
{
	// Token: 0x0200025E RID: 606
	internal sealed class RecipientDLExpansionEventBasedAssistantHelper
	{
		// Token: 0x06001687 RID: 5767 RVA: 0x000800A0 File Offset: 0x0007E2A0
		internal static void GetComplianceMaxExpansionDGRecipientsAndNestedDGs(OrganizationId organizationId, out uint complianceMaxExpansionDGRecipients, out uint complianceMaxExpansionNestedDGs)
		{
			IThrottlingPolicy throttlingPolicy = RecipientDLExpansionEventBasedAssistantHelper.GetThrottlingPolicy(organizationId);
			complianceMaxExpansionDGRecipients = (throttlingPolicy.ComplianceMaxExpansionDGRecipients.IsUnlimited ? uint.MaxValue : throttlingPolicy.ComplianceMaxExpansionDGRecipients.Value);
			ExTraceGlobals.FaultInjectionTracer.TraceTest<uint>(3162910013U, ref complianceMaxExpansionDGRecipients);
			complianceMaxExpansionNestedDGs = (throttlingPolicy.ComplianceMaxExpansionNestedDGs.IsUnlimited ? uint.MaxValue : throttlingPolicy.ComplianceMaxExpansionNestedDGs.Value);
			ExTraceGlobals.FaultInjectionTracer.TraceTest<uint>(4236651837U, ref complianceMaxExpansionNestedDGs);
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x0008011C File Offset: 0x0007E31C
		internal static bool IsRecipientDLExpansionTestHookEnabled()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Services\\MSExchangeMailboxAssistants\\Parameters"))
			{
				if (registryKey != null)
				{
					object value = registryKey.GetValue("RecipientDLExpansionTestHookEnabled");
					if (value != null && (int)value == 1)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x00080178 File Offset: 0x0007E378
		private static IThrottlingPolicy GetThrottlingPolicy(OrganizationId organizationId)
		{
			TenantBudgetKey tenantBudgetKey = new TenantBudgetKey(organizationId, BudgetType.PowerShell);
			return tenantBudgetKey.Lookup();
		}

		// Token: 0x04000D3A RID: 3386
		internal const string ParameterRegistryKeyPath = "System\\CurrentControlSet\\Services\\MSExchangeMailboxAssistants\\Parameters";

		// Token: 0x04000D3B RID: 3387
		internal const string RecipientDLExpansionTestHookEnabled = "RecipientDLExpansionTestHookEnabled";

		// Token: 0x04000D3C RID: 3388
		private const uint LIDComplianceMaxExpansionDGRecipients = 3162910013U;

		// Token: 0x04000D3D RID: 3389
		private const uint LIDComplianceMaxExpansionNestedDGs = 4236651837U;
	}
}
