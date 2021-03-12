using System;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Management.Supervision;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009C2 RID: 2498
	internal class ClosedCampusQueryProcessor : RbacQuery.RbacQueryProcessor
	{
		// Token: 0x060046CE RID: 18126 RVA: 0x000FBC10 File Offset: 0x000F9E10
		public override bool? TryIsInRole(ExchangeRunspaceConfiguration rbacConfiguration)
		{
			bool? result;
			try
			{
				PSLocalTask<GetSupervisionPolicy, SupervisionPolicy> taskWrapper = CmdletTaskFactory.Instance.CreateGetSupervisionPolicyTask(CallContext.Current.AccessingPrincipal);
				CmdletRunner<GetSupervisionPolicy, SupervisionPolicy> cmdletRunner = new CmdletRunner<GetSupervisionPolicy, SupervisionPolicy>(CallContext.Current, "Get-SupervisionPolicy", ScopeLocation.RecipientRead, taskWrapper);
				cmdletRunner.Execute();
				foreach (SupervisionPolicy supervisionPolicy in cmdletRunner.TaskAllResults)
				{
					if (supervisionPolicy.ClosedCampusInboundPolicyEnabled && supervisionPolicy.ClosedCampusOutboundPolicyEnabled)
					{
						return new bool?(true);
					}
				}
				result = new bool?(false);
			}
			catch (CmdletException ex)
			{
				if (ex.ErrorCode == OptionsActionError.PermissionDenied)
				{
					result = new bool?(false);
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x040028AE RID: 10414
		internal const string RoleName = "ClosedCampus";
	}
}
