using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.EventLogs;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000300 RID: 768
	internal sealed class GetComplianceConfiguration : SingleStepServiceCommand<GetComplianceConfigurationRequest, GetComplianceConfigurationResponseMessage>
	{
		// Token: 0x060015AA RID: 5546 RVA: 0x0007082B File Offset: 0x0006EA2B
		public GetComplianceConfiguration(CallContext callContext, GetComplianceConfigurationRequest request) : base(callContext, request)
		{
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x00070835 File Offset: 0x0006EA35
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return new GetComplianceConfigurationResponseMessage(this.rmsTemplates, base.Result.Code, base.Result.Error);
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x00070858 File Offset: 0x0006EA58
		internal override ServiceResult<GetComplianceConfigurationResponseMessage> Execute()
		{
			OrganizationId organizationId = base.MailboxIdentityMailboxSession.MailboxOwner.MailboxInfo.OrganizationId;
			this.rmsTemplates = this.GetRmsTemplates(organizationId, base.CallContext.ClientCulture);
			return new ServiceResult<GetComplianceConfigurationResponseMessage>(new GetComplianceConfigurationResponseMessage(this.rmsTemplates, ServiceResultCode.Success, null));
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x000708EC File Offset: 0x0006EAEC
		private RmsTemplate[] GetRmsTemplates(OrganizationId organizationId, CultureInfo userCulture)
		{
			RmsTemplate[] result;
			try
			{
				ExTraceGlobals.IrmTracer.TraceDebug<OrganizationId, CultureInfo>((long)this.GetHashCode(), "Getting rms templates for organization {0} with culture {1}", organizationId, userCulture);
				IEnumerable<RmsTemplate> enumerable = this.AcquireRmsTemplates(organizationId);
				RmsTemplate[] array = (enumerable as RmsTemplate[]) ?? enumerable.ToArray<RmsTemplate>();
				string arg = string.Join(",", from template in array
				select string.Format(userCulture, "{0}:{1}", new object[]
				{
					template.Id,
					template.Name
				}));
				ExTraceGlobals.IrmTracer.TraceDebug<string>((long)this.GetHashCode(), "Loaded templates= {0}", arg);
				result = array;
			}
			catch (RightsManagementTransientException ex)
			{
				ExTraceGlobals.IrmTracer.TraceDebug<string>((long)this.GetHashCode(), "exception {0}", ex.ToString());
				throw;
			}
			catch (Exception ex2)
			{
				ServiceDiagnostics.Logger.LogEvent(ServicesEventLogConstants.Tuple_InternalServerError, organizationId.ToString(), new object[]
				{
					ex2
				});
				ExTraceGlobals.IrmTracer.TraceError<string>((long)this.GetHashCode(), "exception {0}", ex2.ToString());
				throw;
			}
			return result;
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x00070A08 File Offset: 0x0006EC08
		private IEnumerable<RmsTemplate> AcquireRmsTemplates(OrganizationId organizationId)
		{
			this.HandleSimultaneousExpensiveRmsTemplateCalls(organizationId);
			IEnumerable<RmsTemplate> result;
			try
			{
				IEnumerable<RmsTemplate> enumerable;
				if (RmsClientManager.IRMConfig.IsClientAccessServerEnabledForTenant(organizationId))
				{
					enumerable = RmsClientManager.AcquireRmsTemplates(organizationId, false);
				}
				else
				{
					enumerable = DrmEmailConstants.EmptyTemplateArray;
				}
				result = enumerable;
			}
			finally
			{
				if (GetComplianceConfiguration.OrganizationsDoingExpensiveRetrievals.Contains(organizationId))
				{
					lock (GetComplianceConfiguration.OrganizationsDoingExpensiveRetrievals)
					{
						GetComplianceConfiguration.OrganizationsDoingExpensiveRetrievals.Remove(organizationId);
					}
				}
			}
			return result;
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x00070A90 File Offset: 0x0006EC90
		private void HandleSimultaneousExpensiveRmsTemplateCalls(OrganizationId organizationId)
		{
			lock (GetComplianceConfiguration.OrganizationsDoingExpensiveRetrievals)
			{
				if (GetComplianceConfiguration.OrganizationsDoingExpensiveRetrievals.Contains(organizationId))
				{
					throw new RightsManagementTransientException(ServerStrings.RmExceptionGenericMessage, null);
				}
				if (!RmsClientManager.Initialized)
				{
					GetComplianceConfiguration.OrganizationsDoingExpensiveRetrievals.Add(organizationId);
				}
				else if (organizationId == OrganizationId.ForestWideOrgId)
				{
					ICollection<RmsTemplate> collection;
					if (RmsClientManager.TemplateCacheForFirstOrg == null || RmsClientManager.TemplateCacheForFirstOrg.GetAllValues(out collection) || collection.Count == 0)
					{
						GetComplianceConfiguration.OrganizationsDoingExpensiveRetrievals.Add(organizationId);
					}
				}
				else if (!RmsClientManager.IRMConfig.AreRmsTemplatesInCache(organizationId))
				{
					GetComplianceConfiguration.OrganizationsDoingExpensiveRetrievals.Add(organizationId);
				}
			}
		}

		// Token: 0x04000EB4 RID: 3764
		private static readonly HashSet<OrganizationId> OrganizationsDoingExpensiveRetrievals = new HashSet<OrganizationId>();

		// Token: 0x04000EB5 RID: 3765
		private RmsTemplate[] rmsTemplates;
	}
}
