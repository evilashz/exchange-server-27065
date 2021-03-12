using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Transport.Agent.InterceptorAgent;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B2C RID: 2860
	[Cmdlet("Get", "InterceptorRule", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class GetInterceptorRule : GetSystemConfigurationObjectTask<InterceptorRuleIdParameter, InterceptorRule>
	{
		// Token: 0x060066E2 RID: 26338 RVA: 0x001A92EF File Offset: 0x001A74EF
		public GetInterceptorRule()
		{
			base.Fields.ResetChangeTracking();
		}

		// Token: 0x17001F9D RID: 8093
		// (get) Token: 0x060066E3 RID: 26339 RVA: 0x001A9302 File Offset: 0x001A7502
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001F9E RID: 8094
		// (get) Token: 0x060066E4 RID: 26340 RVA: 0x001A9305 File Offset: 0x001A7505
		protected override ObjectId RootId
		{
			get
			{
				return base.RootOrgContainerId.GetDescendantId(InterceptorRule.InterceptorRulesContainer);
			}
		}

		// Token: 0x060066E5 RID: 26341 RVA: 0x001A9318 File Offset: 0x001A7518
		protected override void WriteResult(IConfigurable dataObject)
		{
			InterceptorRule interceptorRule = dataObject as InterceptorRule;
			InterceptorAgentRule interceptorAgentRule = null;
			try
			{
				interceptorAgentRule = InterceptorAgentRule.CreateRuleFromXml(interceptorRule.Xml);
			}
			catch (InvalidOperationException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidOperation, this.Identity);
				TaskLogger.LogExit();
				return;
			}
			catch (FormatException exception2)
			{
				base.WriteError(exception2, ErrorCategory.InvalidOperation, this.Identity);
				TaskLogger.LogExit();
				return;
			}
			interceptorAgentRule.SetPropertiesFromAdObjet(interceptorRule);
			base.WriteResult(interceptorAgentRule);
		}
	}
}
