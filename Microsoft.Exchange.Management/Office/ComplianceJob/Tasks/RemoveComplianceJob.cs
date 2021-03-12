using System;
using System.Management.Automation;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.ObjectModel;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Office.ComplianceJob.Tasks
{
	// Token: 0x02000144 RID: 324
	public abstract class RemoveComplianceJob<TDataObject> : RemoveTenantADTaskBase<ComplianceJobIdParameter, TDataObject> where TDataObject : ComplianceJob, new()
	{
		// Token: 0x06000BBB RID: 3003 RVA: 0x0003653F File Offset: 0x0003473F
		protected override IConfigDataProvider CreateSession()
		{
			if (base.ExchangeRunspaceConfig == null)
			{
				base.ThrowTerminatingError(new ComplianceJobTaskException(Strings.UnableToDetermineExecutingUser), ErrorCategory.InvalidOperation, null);
			}
			return new ComplianceJobProvider(base.ExchangeRunspaceConfig.OrganizationId);
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x00036570 File Offset: 0x00034770
		protected override void InternalProcessRecord()
		{
			if (base.ExchangeRunspaceConfig == null)
			{
				base.WriteError(new ComplianceJobTaskException(Strings.UnableToDetermineExecutingUser), ErrorCategory.InvalidOperation, null);
				return;
			}
			if (base.DataObject != null)
			{
				TDataObject dataObject = base.DataObject;
				if (dataObject.IsRunning())
				{
					TDataObject dataObject2 = base.DataObject;
					base.WriteError(new ComplianceJobTaskException(Strings.ComplianceSearchIsInProgress(dataObject2.Name)), ErrorCategory.InvalidOperation, null);
					return;
				}
				base.InternalProcessRecord();
			}
		}
	}
}
