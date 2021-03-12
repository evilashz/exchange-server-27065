using System;
using System.Management.Automation;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000998 RID: 2456
	[Cmdlet("Update", "DatabaseSchema", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public class UpdateDatabaseSchema : SetTopologySystemConfigurationObjectTask<DatabaseIdParameter, Database>
	{
		// Token: 0x17001A2C RID: 6700
		// (get) Token: 0x060057DD RID: 22493 RVA: 0x0016F070 File Offset: 0x0016D270
		// (set) Token: 0x060057DE RID: 22494 RVA: 0x0016F087 File Offset: 0x0016D287
		[Parameter(Mandatory = true, ParameterSetName = "Versions", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		[Parameter(Mandatory = true, ParameterSetName = "Identity", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public override DatabaseIdParameter Identity
		{
			get
			{
				return (DatabaseIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17001A2D RID: 6701
		// (get) Token: 0x060057DF RID: 22495 RVA: 0x0016F09A File Offset: 0x0016D29A
		// (set) Token: 0x060057E0 RID: 22496 RVA: 0x0016F0B1 File Offset: 0x0016D2B1
		[Parameter(Mandatory = true, ParameterSetName = "Versions")]
		public ushort MajorVersion
		{
			get
			{
				return (ushort)base.Fields["MajorVersion"];
			}
			set
			{
				base.Fields["MajorVersion"] = value;
			}
		}

		// Token: 0x17001A2E RID: 6702
		// (get) Token: 0x060057E1 RID: 22497 RVA: 0x0016F0C9 File Offset: 0x0016D2C9
		// (set) Token: 0x060057E2 RID: 22498 RVA: 0x0016F0E0 File Offset: 0x0016D2E0
		[Parameter(Mandatory = true, ParameterSetName = "Versions")]
		public ushort MinorVersion
		{
			get
			{
				return (ushort)base.Fields["MinorVersion"];
			}
			set
			{
				base.Fields["MinorVersion"] = value;
			}
		}

		// Token: 0x060057E3 RID: 22499 RVA: 0x0016F0F8 File Offset: 0x0016D2F8
		private static int VersionFromComponents(ushort major, ushort minor)
		{
			return (int)major << 16 | (int)minor;
		}

		// Token: 0x060057E4 RID: 22500 RVA: 0x0016F100 File Offset: 0x0016D300
		private static string VersionString(int version)
		{
			return string.Format("{0}.{1}", (ushort)(version >> 16), (ushort)(version & 65535));
		}

		// Token: 0x17001A2F RID: 6703
		// (get) Token: 0x060057E5 RID: 22501 RVA: 0x0016F123 File Offset: 0x0016D323
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetMailboxDatabase(this.Identity.ToString());
			}
		}

		// Token: 0x060057E6 RID: 22502 RVA: 0x0016F138 File Offset: 0x0016D338
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			Server server = this.DataObject.GetServer();
			bool flag = false;
			if (server == null)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorDBOwningServerNotFound(this.DataObject.Identity.ToString())), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			else
			{
				flag = server.IsE15OrLater;
			}
			if (!flag)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorModifyE14DatabaseNotAllowed), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			DatabaseAvailabilityGroup databaseAvailabilityGroup;
			using (IClusterDB clusterDB = DatabaseTasksHelper.OpenClusterDatabase((ITopologyConfigurationSession)base.DataSession, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskErrorLoggingDelegate(base.WriteError), this.DataObject, false, out databaseAvailabilityGroup))
			{
				if (clusterDB == null || !clusterDB.IsInstalled)
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorSchemaVersionDoesntApply(this.DataObject.Name)), ErrorCategory.InvalidOperation, this.DataObject.Identity);
				}
			}
			if (base.Fields.IsModified("MajorVersion"))
			{
				this.version = UpdateDatabaseSchema.VersionFromComponents(this.MajorVersion, this.MinorVersion);
				int num;
				int num2;
				int num3;
				DatabaseTasksHelper.GetSupporableDatabaseSchemaVersionRange((ITopologyConfigurationSession)base.DataSession, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskErrorLoggingDelegate(base.WriteError), this.DataObject, out num, out num2, out num3);
				if (this.version < num || this.version > num2)
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorSchemaVersionOutOfRange(UpdateDatabaseSchema.VersionString(num), UpdateDatabaseSchema.VersionString(num2))), ErrorCategory.InvalidOperation, this.DataObject.Identity);
				}
				if (num3 > this.version)
				{
					this.WriteWarning(Strings.RequestedVersionIsLowerThanCurrentVersion(num3));
				}
			}
			else
			{
				this.version = DatabaseTasksHelper.GetMaximumSupportedDatabaseSchemaVersion((ITopologyConfigurationSession)base.DataSession, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskErrorLoggingDelegate(base.WriteError), this.DataObject);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060057E7 RID: 22503 RVA: 0x0016F360 File Offset: 0x0016D560
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject
			});
			DatabaseTasksHelper.SetRequestedDatabaseSchemaVersion((ITopologyConfigurationSession)base.DataSession, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskErrorLoggingDelegate(base.WriteError), this.DataObject, this.version);
			TaskLogger.LogExit();
		}

		// Token: 0x040032A7 RID: 12967
		internal const string paramMajorVersion = "MajorVersion";

		// Token: 0x040032A8 RID: 12968
		internal const string paramMinorVersion = "MinorVersion";

		// Token: 0x040032A9 RID: 12969
		internal const string versionParameterSetName = "Versions";

		// Token: 0x040032AA RID: 12970
		private int version;
	}
}
