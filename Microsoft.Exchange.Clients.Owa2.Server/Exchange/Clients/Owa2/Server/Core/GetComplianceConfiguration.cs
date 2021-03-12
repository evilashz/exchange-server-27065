using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Clients.EventLogs;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Common.Cache;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.MessageSecurity.MessageClassifications;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000310 RID: 784
	internal class GetComplianceConfiguration : ServiceCommand<ComplianceConfiguration>
	{
		// Token: 0x060019FC RID: 6652 RVA: 0x0005DC7F File Offset: 0x0005BE7F
		public GetComplianceConfiguration(CallContext callContext) : base(callContext)
		{
			OwsLogRegistry.Register("GetComplianceConfiguration", typeof(OwaUserConfigurationLogMetadata), new Type[0]);
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x0005DCA4 File Offset: 0x0005BEA4
		protected override ComplianceConfiguration InternalExecute()
		{
			ComplianceConfiguration complianceConfiguration = new ComplianceConfiguration();
			UserContext userContext = UserContextManager.GetUserContext(CallContext.Current.HttpContext, CallContext.Current.EffectiveCaller, true);
			OrganizationId organizationId = base.MailboxIdentityMailboxSession.MailboxOwner.MailboxInfo.OrganizationId;
			complianceConfiguration.RmsTemplates = this.GetRmsTemplates(organizationId, userContext.UserCulture);
			complianceConfiguration.MessageClassifications = this.GetMessageClassifications(organizationId, userContext.UserCulture);
			return complianceConfiguration;
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x0005DD54 File Offset: 0x0005BF54
		protected MessageClassificationType[] GetMessageClassifications(OrganizationId organizationId, CultureInfo userCulture)
		{
			IEnumerable<MessageClassificationType> source = from classificationSummary in this.GetRawClassifications(organizationId, userCulture)
			where classificationSummary.PermissionMenuVisible
			select new MessageClassificationType(classificationSummary.ClassificationID.ToString(), classificationSummary.DisplayName, classificationSummary.SenderDescription) into classification
			orderby classification.Name
			select classification;
			return source.ToArray<MessageClassificationType>();
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x0005DDD6 File Offset: 0x0005BFD6
		protected virtual IEnumerable<ClassificationSummary> GetRawClassifications(OrganizationId organizationId, CultureInfo userCulture)
		{
			GetComplianceConfiguration.classificationConfig = (GetComplianceConfiguration.classificationConfig ?? new ClassificationConfig());
			return GetComplianceConfiguration.classificationConfig.GetClassifications(organizationId, userCulture);
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x0005DE80 File Offset: 0x0005C080
		protected RmsTemplateType[] GetRmsTemplates(OrganizationId organizationId, CultureInfo userCulture)
		{
			try
			{
				ExTraceGlobals.IrmTracer.TraceDebug<OrganizationId, CultureInfo>((long)this.GetHashCode(), "Getting rms templates for organization {0} with culture {1}", organizationId, userCulture);
				IEnumerable<RmsTemplate> source = this.AcquireRmsTemplates(organizationId);
				IEnumerable<RmsTemplateType> source2 = from mesrTemplate in source
				select new RmsTemplateType(mesrTemplate.Id.ToString(), mesrTemplate.GetName(userCulture), mesrTemplate.GetDescription(userCulture)) into template
				orderby template.Name
				select template;
				string arg = string.Join(",", from template in source2
				select string.Format(userCulture, "{0}:{1}", new object[]
				{
					template.Id,
					template.Name
				}));
				ExTraceGlobals.IrmTracer.TraceDebug<string>((long)this.GetHashCode(), "Loaded templates= {0}", arg);
				return source2.ToArray<RmsTemplateType>();
			}
			catch (OwaThrottlingException)
			{
				throw;
			}
			catch (Exception ex)
			{
				OwaDiagnostics.LogEvent(ClientsEventLogConstants.Tuple_RmsTemplateLoadFailure, organizationId.ToString(), new object[]
				{
					ex
				});
				OwaServerTraceLogger.AppendToLog(new RmsLoadingLogEvent(organizationId, ex));
			}
			return null;
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x0005DFA0 File Offset: 0x0005C1A0
		protected virtual IEnumerable<RmsTemplate> AcquireRmsTemplates(OrganizationId organizationId)
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
				if (GetComplianceConfiguration.organizationsDoingExpensiveRetrivals.Contains(organizationId))
				{
					lock (GetComplianceConfiguration.organizationsDoingExpensiveRetrivals)
					{
						GetComplianceConfiguration.organizationsDoingExpensiveRetrivals.Remove(organizationId);
					}
				}
			}
			return result;
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x0005E028 File Offset: 0x0005C228
		private void HandleSimultaneousExpensiveRmsTemplateCalls(OrganizationId organizationId)
		{
			lock (GetComplianceConfiguration.organizationsDoingExpensiveRetrivals)
			{
				if (GetComplianceConfiguration.organizationsDoingExpensiveRetrivals.Contains(organizationId))
				{
					throw new OwaThrottlingException();
				}
				if (!RmsClientManager.Initialized)
				{
					GetComplianceConfiguration.organizationsDoingExpensiveRetrivals.Add(organizationId);
				}
				else if (organizationId == OrganizationId.ForestWideOrgId)
				{
					Cache<Guid, RmsTemplate> templateCacheForFirstOrg = RmsClientManager.TemplateCacheForFirstOrg;
					ICollection<RmsTemplate> collection;
					if (templateCacheForFirstOrg == null || templateCacheForFirstOrg.GetAllValues(out collection) || collection.Count == 0)
					{
						GetComplianceConfiguration.organizationsDoingExpensiveRetrivals.Add(organizationId);
					}
				}
				else if (!RmsClientManager.IRMConfig.AreRmsTemplatesInCache(organizationId))
				{
					GetComplianceConfiguration.organizationsDoingExpensiveRetrivals.Add(organizationId);
				}
			}
		}

		// Token: 0x04000E66 RID: 3686
		private static ClassificationConfig classificationConfig;

		// Token: 0x04000E67 RID: 3687
		private static HashSet<OrganizationId> organizationsDoingExpensiveRetrivals = new HashSet<OrganizationId>();
	}
}
