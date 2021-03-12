using System;
using System.Management.Automation;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000975 RID: 2421
	public abstract class DatabaseActionTaskBase<TDataObject> : SystemConfigurationObjectActionTask<DatabaseIdParameter, TDataObject> where TDataObject : Database, new()
	{
		// Token: 0x0600567B RID: 22139 RVA: 0x00163719 File Offset: 0x00161919
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || SystemConfigurationTasksHelper.IsKnownWmiException(exception) || SystemConfigurationTasksHelper.IsKnownMapiDotNETException(exception) || SystemConfigurationTasksHelper.IsKnownClusterUpdateDatabaseResourceException(exception);
		}

		// Token: 0x0600567C RID: 22140 RVA: 0x00163748 File Offset: 0x00161948
		protected override void TranslateException(ref Exception e, out ErrorCategory category)
		{
			base.TranslateException(ref e, out category);
			category = (ErrorCategory)1001;
			if (SystemConfigurationTasksHelper.IsKnownMapiDotNETException(e))
			{
				TDataObject dataObject = this.DataObject;
				e = new InvalidOperationException(Strings.ErrorFailedToConnectToStore(dataObject.ServerName), e);
			}
		}

		// Token: 0x0600567D RID: 22141 RVA: 0x00163794 File Offset: 0x00161994
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			TDataObject dataObject = this.DataObject;
			if (!dataObject.IsExchange2009OrLater)
			{
				Exception exception = new InvalidOperationException(Strings.ErrorModifyE12DatabaseNotAllowed);
				ErrorCategory category = ErrorCategory.InvalidOperation;
				TDataObject dataObject2 = this.DataObject;
				base.WriteError(exception, category, dataObject2.Identity);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600567E RID: 22142 RVA: 0x001637F4 File Offset: 0x001619F4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject
			});
			TDataObject scratchPad = Activator.CreateInstance<TDataObject>();
			scratchPad.CopyChangesFrom(this.DataObject);
			base.InternalProcessRecord();
			TDataObject dataObject = this.DataObject;
			AmServerName amServerName = new AmServerName(dataObject.ServerName);
			TDataObject dataObject2 = this.DataObject;
			SystemConfigurationTasksHelper.DoubleWrite<TDataObject>(dataObject2.Identity, scratchPad, amServerName.Fqdn, base.DomainController, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
			TaskLogger.LogExit();
		}
	}
}
