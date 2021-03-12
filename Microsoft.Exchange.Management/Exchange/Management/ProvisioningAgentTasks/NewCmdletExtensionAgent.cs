using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Provisioning;

namespace Microsoft.Exchange.Management.ProvisioningAgentTasks
{
	// Token: 0x02000CD8 RID: 3288
	[Cmdlet("New", "CmdletExtensionAgent", SupportsShouldProcess = true, DefaultParameterSetName = "NonSystemAgent")]
	public sealed class NewCmdletExtensionAgent : NewFixedNameSystemConfigurationObjectTask<CmdletExtensionAgent>
	{
		// Token: 0x17002760 RID: 10080
		// (get) Token: 0x06007EC5 RID: 32453 RVA: 0x00205E19 File Offset: 0x00204019
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewCmdletExtensionAgent(this.Assembly, this.ClassFactory);
			}
		}

		// Token: 0x17002761 RID: 10081
		// (get) Token: 0x06007EC6 RID: 32454 RVA: 0x00205E2C File Offset: 0x0020402C
		// (set) Token: 0x06007EC7 RID: 32455 RVA: 0x00205E39 File Offset: 0x00204039
		[Parameter(Mandatory = false, Position = 0)]
		public string Name
		{
			get
			{
				return this.DataObject.Name;
			}
			set
			{
				this.DataObject.Name = value;
			}
		}

		// Token: 0x17002762 RID: 10082
		// (get) Token: 0x06007EC8 RID: 32456 RVA: 0x00205E47 File Offset: 0x00204047
		// (set) Token: 0x06007EC9 RID: 32457 RVA: 0x00205E54 File Offset: 0x00204054
		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public string Assembly
		{
			get
			{
				return this.DataObject.Assembly;
			}
			set
			{
				this.DataObject.Assembly = value;
			}
		}

		// Token: 0x17002763 RID: 10083
		// (get) Token: 0x06007ECA RID: 32458 RVA: 0x00205E62 File Offset: 0x00204062
		// (set) Token: 0x06007ECB RID: 32459 RVA: 0x00205E6F File Offset: 0x0020406F
		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public string ClassFactory
		{
			get
			{
				return this.DataObject.ClassFactory;
			}
			set
			{
				this.DataObject.ClassFactory = value;
			}
		}

		// Token: 0x17002764 RID: 10084
		// (get) Token: 0x06007ECC RID: 32460 RVA: 0x00205E7D File Offset: 0x0020407D
		// (set) Token: 0x06007ECD RID: 32461 RVA: 0x00205EA8 File Offset: 0x002040A8
		[Parameter(Mandatory = false)]
		public byte Priority
		{
			get
			{
				if (base.Fields["Priority"] != null)
				{
					return (byte)base.Fields["Priority"];
				}
				return 0;
			}
			set
			{
				base.Fields["Priority"] = value;
			}
		}

		// Token: 0x17002765 RID: 10085
		// (get) Token: 0x06007ECE RID: 32462 RVA: 0x00205EC0 File Offset: 0x002040C0
		// (set) Token: 0x06007ECF RID: 32463 RVA: 0x00205EE1 File Offset: 0x002040E1
		[Parameter(Mandatory = false)]
		public bool Enabled
		{
			get
			{
				return (bool)(base.Fields["Enabled"] ?? true);
			}
			set
			{
				base.Fields["Enabled"] = value;
			}
		}

		// Token: 0x17002766 RID: 10086
		// (get) Token: 0x06007ED0 RID: 32464 RVA: 0x00205EF9 File Offset: 0x002040F9
		// (set) Token: 0x06007ED1 RID: 32465 RVA: 0x00205F1A File Offset: 0x0020411A
		[Parameter(Mandatory = false)]
		public bool IsSystem
		{
			get
			{
				return (bool)(base.Fields["IsSystem"] ?? false);
			}
			set
			{
				base.Fields["IsSystem"] = value;
			}
		}

		// Token: 0x06007ED2 RID: 32466 RVA: 0x00205F34 File Offset: 0x00204134
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter();
			base.InternalStateReset();
			this.agentsGlobalConfig = new CmdletExtensionAgentsGlobalConfig((ITopologyConfigurationSession)base.DataSession);
			if (this.agentsGlobalConfig.ConfigurationIssues.Length > 0)
			{
				base.WriteError(new InvalidOperationException(this.agentsGlobalConfig.ConfigurationIssues[0]), ErrorCategory.InvalidOperation, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06007ED3 RID: 32467 RVA: 0x00205FA0 File Offset: 0x002041A0
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			this.DataObject = (CmdletExtensionAgent)base.PrepareDataObject();
			if (base.HasErrors)
			{
				return null;
			}
			if (this.Assembly.IndexOf("\\") != -1)
			{
				base.WriteError(new ArgumentException(Strings.ErrorAssemblyIsPath(this.Assembly)), ErrorCategory.InvalidArgument, null);
			}
			if (string.IsNullOrEmpty(this.DataObject.Name))
			{
				string[] array = this.ClassFactory.Split(new char[]
				{
					'.'
				});
				string text = array[array.Length - 1];
				if (text.Length > 64)
				{
					this.DataObject.Name = text.Substring(0, 64);
				}
				else
				{
					this.DataObject.Name = text;
				}
			}
			if (!this.agentsGlobalConfig.IsPriorityAvailable(this.Priority, null) && !this.agentsGlobalConfig.FreeUpPriorityValue(this.Priority))
			{
				base.WriteError(new ArgumentException(Strings.NotEnoughFreePrioritiesAvailable(this.Priority.ToString())), ErrorCategory.InvalidArgument, null);
			}
			this.DataObject.Priority = this.Priority;
			this.DataObject.Enabled = this.Enabled;
			this.DataObject.IsSystem = this.IsSystem;
			if (this.IsSystem && !this.Enabled)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorAgentCannotBeDisabled), ErrorCategory.InvalidOperation, null);
			}
			ADObjectId descendantId = base.RootOrgContainerId.GetDescendantId(new ADObjectId("CN=Global Settings"));
			ADObjectId descendantId2 = descendantId.GetDescendantId(new ADObjectId("CN=CmdletExtensionAgent Settings"));
			this.DataObject.SetId(descendantId2.GetChildId(this.DataObject.Name));
			TaskLogger.LogExit();
			return this.DataObject;
		}

		// Token: 0x06007ED4 RID: 32468 RVA: 0x00206154 File Offset: 0x00204354
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			if (this.agentsGlobalConfig.IsFactoryIdentityInUse(this.DataObject.Assembly, this.DataObject.ClassFactory))
			{
				base.WriteError(new ArgumentException(Strings.FactoryIdentityInUse(this.DataObject.Assembly, this.DataObject.ClassFactory)), ErrorCategory.InvalidArgument, null);
			}
			Exception exception;
			if (!this.AssemblyAndClassFactoryFound(this.DataObject.Assembly, this.DataObject.ClassFactory, out exception))
			{
				base.WriteError(exception, ErrorCategory.InvalidArgument, null);
			}
			base.InternalValidate();
			TaskLogger.LogExit();
		}

		// Token: 0x06007ED5 RID: 32469 RVA: 0x002061EC File Offset: 0x002043EC
		protected override void InternalProcessRecord()
		{
			if (this.agentsGlobalConfig.ObjectsToSave != null)
			{
				foreach (CmdletExtensionAgent instance in this.agentsGlobalConfig.ObjectsToSave)
				{
					base.DataSession.Save(instance);
				}
			}
			base.InternalProcessRecord();
			ProvisioningLayer.RefreshProvisioningBroker(this);
		}

		// Token: 0x06007ED6 RID: 32470 RVA: 0x00206264 File Offset: 0x00204464
		private bool AssemblyAndClassFactoryFound(string assemblyName, string classFactory, out Exception ex)
		{
			ProvisioningBroker.GetClassFactoryInstance(assemblyName, classFactory, out ex);
			return ex == null;
		}

		// Token: 0x04003E40 RID: 15936
		private CmdletExtensionAgentsGlobalConfig agentsGlobalConfig;
	}
}
