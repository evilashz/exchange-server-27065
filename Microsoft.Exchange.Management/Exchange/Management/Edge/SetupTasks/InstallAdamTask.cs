using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x020002F3 RID: 755
	[Cmdlet("Install", "Adam")]
	[LocDescription(Strings.IDs.InstallAdamTask)]
	public sealed class InstallAdamTask : Task
	{
		// Token: 0x060019E4 RID: 6628 RVA: 0x00073212 File Offset: 0x00071412
		public InstallAdamTask()
		{
			this.InstanceName = "MSExchange";
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x060019E5 RID: 6629 RVA: 0x00073225 File Offset: 0x00071425
		// (set) Token: 0x060019E6 RID: 6630 RVA: 0x0007323C File Offset: 0x0007143C
		[Parameter(Mandatory = false)]
		public string InstanceName
		{
			get
			{
				return base.Fields["InstanceName"] as string;
			}
			set
			{
				base.Fields["InstanceName"] = value;
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x060019E7 RID: 6631 RVA: 0x0007324F File Offset: 0x0007144F
		// (set) Token: 0x060019E8 RID: 6632 RVA: 0x00073266 File Offset: 0x00071466
		[Parameter(Mandatory = false)]
		public string DataFilesPath
		{
			get
			{
				return base.Fields["DataFilesPath"] as string;
			}
			set
			{
				base.Fields["DataFilesPath"] = value;
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x060019E9 RID: 6633 RVA: 0x00073279 File Offset: 0x00071479
		// (set) Token: 0x060019EA RID: 6634 RVA: 0x00073290 File Offset: 0x00071490
		[Parameter(Mandatory = false)]
		public string LogFilesPath
		{
			get
			{
				return base.Fields["LogFilesPath"] as string;
			}
			set
			{
				base.Fields["LogFilesPath"] = value;
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x060019EB RID: 6635 RVA: 0x000732A3 File Offset: 0x000714A3
		// (set) Token: 0x060019EC RID: 6636 RVA: 0x000732BA File Offset: 0x000714BA
		[Parameter(Mandatory = false)]
		public int Port
		{
			get
			{
				return (int)base.Fields["Port"];
			}
			set
			{
				base.Fields["Port"] = value;
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x060019ED RID: 6637 RVA: 0x000732D2 File Offset: 0x000714D2
		// (set) Token: 0x060019EE RID: 6638 RVA: 0x000732E9 File Offset: 0x000714E9
		[Parameter(Mandatory = false)]
		public int SslPort
		{
			get
			{
				return (int)base.Fields["SslPort"];
			}
			set
			{
				base.Fields["SslPort"] = value;
			}
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x00073304 File Offset: 0x00071504
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (!new GatewayRole().IsUnpacked)
			{
				base.WriteError(new TaskException(Strings.GatewayRoleIsNotUnpacked), ErrorCategory.InvalidOperation, null);
			}
			if (this.DataFilesPath == null)
			{
				this.DataFilesPath = ConfigurationContext.Setup.TransportDataPath;
			}
			else
			{
				try
				{
					Utils.ValidateDirectory(this.DataFilesPath, "DataFilesPath");
				}
				catch (InvalidCharsInPathException exception)
				{
					base.WriteError(exception, ErrorCategory.InvalidArgument, null);
				}
				catch (InvalidDriveInPathException exception2)
				{
					base.WriteError(exception2, ErrorCategory.InvalidArgument, null);
				}
				catch (NoPermissionsForPathException exception3)
				{
					base.WriteError(exception3, ErrorCategory.InvalidArgument, null);
				}
				catch (PathIsTooLongException exception4)
				{
					base.WriteError(exception4, ErrorCategory.InvalidArgument, null);
				}
				catch (ReadOnlyPathException exception5)
				{
					base.WriteError(exception5, ErrorCategory.InvalidArgument, null);
				}
			}
			if (this.LogFilesPath == null)
			{
				this.LogFilesPath = ConfigurationContext.Setup.TransportDataPath;
			}
			else if (this.LogFilesPath != this.DataFilesPath)
			{
				try
				{
					Utils.ValidateDirectory(this.LogFilesPath, "LogFilesPath");
				}
				catch (InvalidCharsInPathException exception6)
				{
					base.WriteError(exception6, ErrorCategory.InvalidArgument, null);
				}
				catch (InvalidDriveInPathException exception7)
				{
					base.WriteError(exception7, ErrorCategory.InvalidArgument, null);
				}
				catch (NoPermissionsForPathException exception8)
				{
					base.WriteError(exception8, ErrorCategory.InvalidArgument, null);
				}
				catch (PathIsTooLongException exception9)
				{
					base.WriteError(exception9, ErrorCategory.InvalidArgument, null);
				}
				catch (ReadOnlyPathException exception10)
				{
					base.WriteError(exception10, ErrorCategory.InvalidArgument, null);
				}
			}
			this.Port = this.ValidatePort(base.Fields["Port"], "Port", null);
			this.SslPort = this.ValidatePort(base.Fields["SslPort"], "SslPort", base.Fields["Port"]);
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x000734EC File Offset: 0x000716EC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				ManageAdamService.InstallAdam(this.InstanceName, this.DataFilesPath, this.LogFilesPath, this.Port, this.SslPort, new WriteVerboseDelegate(base.WriteVerbose));
			}
			catch (AdamInstallFailureDataOrLogFolderNotEmptyException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidOperation, null);
			}
			catch (AdamInstallProcessFailureException exception2)
			{
				base.WriteError(exception2, ErrorCategory.InvalidOperation, null);
			}
			catch (AdamInstallGeneralFailureWithResultException exception3)
			{
				base.WriteError(exception3, ErrorCategory.InvalidOperation, null);
			}
			catch (AdamInstallErrorException exception4)
			{
				base.WriteError(exception4, ErrorCategory.InvalidOperation, null);
			}
			catch (AdamSetAclsProcessFailureException exception5)
			{
				base.WriteError(exception5, ErrorCategory.InvalidOperation, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x000735B4 File Offset: 0x000717B4
		private int ValidatePort(object portToValidateObj, string parameterName, object ldapPortObj)
		{
			if (Convert.ToInt32(portToValidateObj) == 0)
			{
				return Utils.GetAvailablePort(ldapPortObj);
			}
			int num = (int)portToValidateObj;
			if (!Utils.IsPortValid(num))
			{
				base.WriteError(new InvalidPortNumberException(num), ErrorCategory.InvalidArgument, null);
			}
			if (ldapPortObj != null)
			{
				int num2 = (int)ldapPortObj;
				if (num == num2)
				{
					base.WriteError(new SslPortSameAsLdapPortException(), ErrorCategory.InvalidArgument, null);
				}
			}
			if (!Utils.IsPortAvailable(num))
			{
				base.WriteError(new PortIsBusyException(num), ErrorCategory.InvalidArgument, null);
			}
			return num;
		}

		// Token: 0x04000B41 RID: 2881
		public const string InstanceParamName = "InstanceName";

		// Token: 0x04000B42 RID: 2882
		public const string DataFilesPathParamName = "DataFilesPath";

		// Token: 0x04000B43 RID: 2883
		public const string LogFilesPathParamName = "LogFilesPath";

		// Token: 0x04000B44 RID: 2884
		public const string LdapPortParamName = "Port";

		// Token: 0x04000B45 RID: 2885
		public const string SslPortParamName = "SslPort";
	}
}
