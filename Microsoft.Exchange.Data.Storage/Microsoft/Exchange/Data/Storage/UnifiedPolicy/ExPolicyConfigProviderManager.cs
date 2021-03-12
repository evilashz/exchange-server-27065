using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.CompliancePolicy;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Exchange.Data.Storage.UnifiedPolicy
{
	// Token: 0x02000E85 RID: 3717
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ExPolicyConfigProviderManager : PolicyConfigProviderManager<ExPolicyConfigProviderManager>
	{
		// Token: 0x06008163 RID: 33123 RVA: 0x00234FC8 File Offset: 0x002331C8
		public override PolicyConfigProvider CreateForSyncEngine(Guid externalDirectoryOrganizationId, string auxiliaryStore, bool enablePolicyApplication = true, ExecutionLog logProvider = null)
		{
			return this.WrapKnownException(delegate
			{
				ExPolicyConfigProvider exPolicyConfigProvider = new ExPolicyConfigProvider(externalDirectoryOrganizationId, false, string.Empty, logProvider);
				if (enablePolicyApplication)
				{
					exPolicyConfigProvider.PolicyConfigChanged += ExPolicyConfigChangeHandler.Current.EventHandler;
				}
				return exPolicyConfigProvider;
			});
		}

		// Token: 0x06008164 RID: 33124 RVA: 0x0023502C File Offset: 0x0023322C
		public IConfigurationSession CreateForCmdlet(IConfigurationSession configurationSession, ExecutionLog logProvider)
		{
			return (IConfigurationSession)this.WrapKnownException(() => new ExPolicyConfigProvider(configurationSession, logProvider));
		}

		// Token: 0x06008165 RID: 33125 RVA: 0x00235094 File Offset: 0x00233294
		public PolicyConfigProvider CreateForProcessingEngine(OrganizationId organizationId, ExecutionLog logProvider, string preferredDomainController)
		{
			return this.WrapKnownException(delegate
			{
				OrganizationId organizationId2 = organizationId;
				ExecutionLog logProvider2 = logProvider;
				return new ExPolicyConfigProvider(organizationId2, true, preferredDomainController, logProvider2);
			});
		}

		// Token: 0x06008166 RID: 33126 RVA: 0x002350EC File Offset: 0x002332EC
		public PolicyConfigProvider CreateForProcessingEngine(OrganizationId organizationId)
		{
			return this.WrapKnownException(() => new ExPolicyConfigProvider(organizationId, true, "", null));
		}

		// Token: 0x06008167 RID: 33127 RVA: 0x00235144 File Offset: 0x00233344
		public PolicyConfigProvider CreateForTest(Guid externalDirectoryOrganizationId, string auxiliaryStore)
		{
			return this.WrapKnownException(() => new ExPolicyConfigProvider(externalDirectoryOrganizationId, false, "", null));
		}

		// Token: 0x06008168 RID: 33128 RVA: 0x002351A0 File Offset: 0x002333A0
		public PolicyConfigProvider CreateForTest(OrganizationId organizationId, ExecutionLog logger)
		{
			return this.WrapKnownException(() => new ExPolicyConfigProvider(organizationId, false, "", logger));
		}

		// Token: 0x06008169 RID: 33129 RVA: 0x002351D4 File Offset: 0x002333D4
		private PolicyConfigProvider WrapKnownException(Func<PolicyConfigProvider> createProviderDelegate)
		{
			PolicyConfigProvider result;
			try
			{
				result = createProviderDelegate();
			}
			catch (DataSourceOperationException ex)
			{
				throw new PolicyConfigProviderPermanentException(ex.Message, ex);
			}
			catch (DataValidationException ex2)
			{
				throw new PolicyConfigProviderPermanentException(ex2.Message, ex2);
			}
			catch (DataSourceTransientException ex3)
			{
				throw new PolicyConfigProviderTransientException(ex3.Message, ex3);
			}
			return result;
		}
	}
}
