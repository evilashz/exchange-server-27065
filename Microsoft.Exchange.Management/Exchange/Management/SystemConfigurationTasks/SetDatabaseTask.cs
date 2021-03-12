using System;
using System.Management.Automation;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000992 RID: 2450
	public abstract class SetDatabaseTask<TDataObject> : SetTopologySystemConfigurationObjectTask<DatabaseIdParameter, TDataObject> where TDataObject : Database, new()
	{
		// Token: 0x06005788 RID: 22408
		internal abstract ADPropertyDefinition[,] GetPropertiesCannotBeSet();

		// Token: 0x06005789 RID: 22409
		internal abstract ADPropertyDefinition[] GetDeprecatedProperties();

		// Token: 0x0600578A RID: 22410 RVA: 0x0016D184 File Offset: 0x0016B384
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			Database database = (Database)this.GetDynamicParameters();
			TaskLogger.LogExit();
		}

		// Token: 0x0600578B RID: 22411 RVA: 0x0016D1A4 File Offset: 0x0016B3A4
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
			Database database = this.DataObject;
			if (database.Servers != null && database.Servers.Length > 0)
			{
				foreach (ADObjectId adobjectId in database.Servers)
				{
					DatabaseTasksHelper.RunConfigurationUpdaterRpcAsync(amServerName.Fqdn, database, ReplayConfigChangeHints.DbCopyAdded, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600578C RID: 22412 RVA: 0x0016D2B8 File Offset: 0x0016B4B8
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			ADPropertyDefinition[,] propertiesCannotBeSet = this.GetPropertiesCannotBeSet();
			for (int i = 0; i < propertiesCannotBeSet.GetLength(0); i++)
			{
				TDataObject instance = this.Instance;
				if (instance.IsModified(propertiesCannotBeSet[i, 0]))
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorSpecifiedPropertyCannotBeSet((propertiesCannotBeSet[i, 1] ?? propertiesCannotBeSet[i, 0]).ToString())), ErrorCategory.InvalidOperation, this.Identity);
				}
			}
			if (this.Identity != null)
			{
				this.Identity.AllowInvalid = true;
			}
			Database result = (Database)base.PrepareDataObject();
			TaskLogger.LogExit();
			return result;
		}

		// Token: 0x0600578D RID: 22413 RVA: 0x0016D35C File Offset: 0x0016B55C
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			Database database = dataObject as Database;
			database.InvalidDatabaseCopiesAllowed = true;
			base.StampChangesOn(dataObject);
		}

		// Token: 0x0600578E RID: 22414 RVA: 0x0016D380 File Offset: 0x0016B580
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			MapiTaskHelper.VerifyDatabaseIsWithinScope(base.SessionSettings, this.DataObject, new Task.ErrorLoggerDelegate(base.WriteError));
			TDataObject dataObject = this.DataObject;
			if (dataObject.Servers != null)
			{
				TDataObject dataObject2 = this.DataObject;
				if (dataObject2.Servers.Length > 0)
				{
					MapiTaskHelper.VerifyServerIsWithinScope(this.DataObject, new Task.ErrorLoggerDelegate(base.WriteError), (ITopologyConfigurationSession)this.ConfigurationSession);
				}
			}
			ADPropertyDefinition[] deprecatedProperties = this.GetDeprecatedProperties();
			for (int i = 0; i < deprecatedProperties.Length; i++)
			{
				TDataObject dataObject3 = this.DataObject;
				if (dataObject3.IsModified(deprecatedProperties[i]))
				{
					this.WriteWarning(Strings.WarnAboutDeprecatedParameter(deprecatedProperties[i].Name));
				}
			}
			TDataObject dataObject4 = this.DataObject;
			if (dataObject4.IsChanged(DatabaseSchema.DataMoveReplicationConstraintDefinition))
			{
				TDataObject dataObject5 = this.DataObject;
				DataMoveReplicationConstraintParameter dataMoveReplicationConstraint = dataObject5.DataMoveReplicationConstraint;
				ITopologyConfigurationSession taskSession = (ITopologyConfigurationSession)base.DataSession;
				Database database = this.DataObject;
				TDataObject dataObject6 = this.DataObject;
				DatabaseTasksHelper.DataMoveReplicationConstraintFallBack(taskSession, database, dataObject6.DataMoveReplicationConstraint, out dataMoveReplicationConstraint);
				DataMoveReplicationConstraintParameter dataMoveReplicationConstraintParameter = dataMoveReplicationConstraint;
				TDataObject dataObject7 = this.DataObject;
				if (dataMoveReplicationConstraintParameter != dataObject7.DataMoveReplicationConstraint)
				{
					TDataObject dataObject8 = this.DataObject;
					DataMoveReplicationConstraintParameter dataMoveReplicationConstraint2 = dataObject8.DataMoveReplicationConstraint;
					TDataObject dataObject9 = this.DataObject;
					base.WriteError(new ConstraintErrorException(dataMoveReplicationConstraint2, dataObject9.Identity.ToString()), ErrorCategory.InvalidOperation, this.Identity);
				}
			}
			TDataObject dataObject10 = this.DataObject;
			if (dataObject10.IsChanged(DatabaseSchema.CircularLoggingEnabled))
			{
				TDataObject dataObject11 = this.DataObject;
				DatabaseCopy[] databaseCopies = dataObject11.GetDatabaseCopies();
				if (databaseCopies != null && databaseCopies.Length == 1)
				{
					TDataObject dataObject12 = this.DataObject;
					this.WriteWarning(Strings.WarningOperationOnDBWithJetCircularLogging(dataObject12.Identity.ToString()));
				}
			}
			TaskLogger.LogExit();
		}
	}
}
