using System;
using System.Management.Automation;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Common.Cluster;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200092B RID: 2347
	public abstract class MoveStoreFilesTask<TIdentity, TDataObject> : SystemConfigurationObjectActionTask<TIdentity, TDataObject> where TIdentity : IIdentityParameter, new() where TDataObject : ADConfigurationObject, new()
	{
		// Token: 0x170018FB RID: 6395
		// (get) Token: 0x060053C4 RID: 21444 RVA: 0x0015A173 File Offset: 0x00158373
		// (set) Token: 0x060053C5 RID: 21445 RVA: 0x0015A199 File Offset: 0x00158399
		[Parameter]
		public SwitchParameter ConfigurationOnly
		{
			get
			{
				return (SwitchParameter)(base.Fields["ConfigurationOnly"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ConfigurationOnly"] = value;
			}
		}

		// Token: 0x170018FC RID: 6396
		// (get) Token: 0x060053C6 RID: 21446 RVA: 0x0015A1B1 File Offset: 0x001583B1
		protected string OwnerServerName
		{
			get
			{
				return this.OwnerServer.Name;
			}
		}

		// Token: 0x170018FD RID: 6397
		// (get) Token: 0x060053C7 RID: 21447
		protected abstract Server OwnerServer { get; }

		// Token: 0x060053C8 RID: 21448 RVA: 0x0015A1BE File Offset: 0x001583BE
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || SystemConfigurationTasksHelper.IsKnownWmiException(exception) || SystemConfigurationTasksHelper.IsKnownMapiDotNETException(exception) || SystemConfigurationTasksHelper.IsKnownClusterUpdateDatabaseResourceException(exception);
		}

		// Token: 0x060053C9 RID: 21449 RVA: 0x0015A1EC File Offset: 0x001583EC
		protected override void TranslateException(ref Exception e, out ErrorCategory category)
		{
			base.TranslateException(ref e, out category);
			category = (ErrorCategory)1001;
			if (SystemConfigurationTasksHelper.IsKnownMapiDotNETException(e))
			{
				TIdentity identity = this.Identity;
				e = new InvalidOperationException(Strings.ErrorFailedToGetDatabaseStatus(identity.ToString()), e);
			}
		}

		// Token: 0x060053CA RID: 21450 RVA: 0x0015A238 File Offset: 0x00158438
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (!this.ConfigurationOnly && !Cluster.StringIEquals(this.OwnerServerName, Environment.MachineName))
			{
				Exception exception = new ArgumentException(Strings.ErrorConfigurationOnly, "ConfigurationOnly");
				ErrorCategory category = ErrorCategory.InvalidArgument;
				TDataObject dataObject = this.DataObject;
				base.WriteError(exception, category, dataObject.Identity);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060053CB RID: 21451 RVA: 0x0015A2A4 File Offset: 0x001584A4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject
			});
			TDataObject scratchPad = Activator.CreateInstance<TDataObject>();
			scratchPad.CopyChangesFrom(this.DataObject);
			base.InternalProcessRecord();
			AmServerName amServerName = new AmServerName(this.OwnerServerName);
			TDataObject dataObject = this.DataObject;
			SystemConfigurationTasksHelper.DoubleWrite<TDataObject>(dataObject.Identity, scratchPad, amServerName.Fqdn, base.DomainController, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
			TaskLogger.LogExit();
		}

		// Token: 0x04003104 RID: 12548
		internal const string paramConfigurationOnly = "ConfigurationOnly";
	}
}
