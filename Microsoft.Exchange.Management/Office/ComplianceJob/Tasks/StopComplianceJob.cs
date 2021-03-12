using System;
using System.Management.Automation;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.ObjectModel;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Office.ComplianceJob.Tasks
{
	// Token: 0x02000146 RID: 326
	public abstract class StopComplianceJob<TDataObject> : ObjectActionTenantADTask<ComplianceJobIdParameter, TDataObject> where TDataObject : ComplianceJob, new()
	{
		// Token: 0x06000BC8 RID: 3016 RVA: 0x00036DAC File Offset: 0x00034FAC
		protected override IConfigDataProvider CreateSession()
		{
			if (base.ExchangeRunspaceConfig == null)
			{
				base.ThrowTerminatingError(new ComplianceJobTaskException(Strings.UnableToDetermineExecutingUser), ErrorCategory.InvalidOperation, null);
			}
			return new ComplianceJobProvider(base.ExchangeRunspaceConfig.OrganizationId);
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x00036DE0 File Offset: 0x00034FE0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (base.ExchangeRunspaceConfig == null)
			{
				base.WriteError(new ComplianceJobTaskException(Strings.UnableToDetermineExecutingUser), ErrorCategory.InvalidOperation, null);
			}
			TDataObject dataObject = this.DataObject;
			switch (dataObject.JobStatus)
			{
			case ComplianceJobStatus.Starting:
			case ComplianceJobStatus.InProgress:
				break;
			default:
			{
				TDataObject dataObject2 = this.DataObject;
				base.WriteError(new ComplianceJobTaskException(Strings.CannotStopNonRunningJob(dataObject2.Name)), ErrorCategory.InvalidOperation, this.DataObject);
				break;
			}
			}
			TDataObject dataObject3 = this.DataObject;
			dataObject3.JobEndTime = DateTime.UtcNow;
			TDataObject dataObject4 = this.DataObject;
			dataObject4.JobStatus = ComplianceJobStatus.Stopped;
			base.DataSession.Save(this.DataObject);
			TaskLogger.LogExit();
		}
	}
}
