using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Deployment;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000007 RID: 7
	internal abstract class ConfigurationDataHandler : SetupSingleTaskDataHandler, IPrecheckEnabled
	{
		// Token: 0x06000071 RID: 113 RVA: 0x00003703 File Offset: 0x00001903
		public ConfigurationDataHandler(ISetupContext context, string installableUnitName, string commandText, MonadConnection connection) : base(context, commandText, connection)
		{
			this.InstallableUnitName = installableUnitName;
			this.isADDependentRole = true;
			base.ImplementsDatacenterMode = true;
			base.ImplementsDatacenterDedicatedMode = true;
			base.ImplementsPartnerHostedMode = true;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000072 RID: 114 RVA: 0x0000373D File Offset: 0x0000193D
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00003745 File Offset: 0x00001945
		public string InstallableUnitName
		{
			get
			{
				return this.installableUnitName;
			}
			protected set
			{
				if (this.InstallableUnitName != value)
				{
					this.installableUnitName = value;
					if (!string.IsNullOrEmpty(this.InstallableUnitName))
					{
						this.InstallableUnitConfigurationInfo = InstallableUnitConfigurationInfoManager.GetInstallableUnitConfigurationInfoByName(this.InstallableUnitName);
						return;
					}
					this.InstallableUnitConfigurationInfo = null;
				}
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003782 File Offset: 0x00001982
		// (set) Token: 0x06000075 RID: 117 RVA: 0x0000378C File Offset: 0x0000198C
		public InstallableUnitConfigurationInfo InstallableUnitConfigurationInfo
		{
			get
			{
				return this.installableUnitConfigurationInfo;
			}
			protected set
			{
				if (this.InstallableUnitConfigurationInfo != value)
				{
					this.installableUnitConfigurationInfo = value;
					if (this.InstallableUnitConfigurationInfo != null)
					{
						this.InstallableUnitName = this.InstallableUnitConfigurationInfo.Name;
						base.WorkUnit.Text = this.InstallableUnitConfigurationInfo.DisplayName;
					}
					else
					{
						this.InstallableUnitName = string.Empty;
						base.WorkUnit.Text = string.Empty;
					}
					base.WorkUnit.Icon = null;
				}
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003806 File Offset: 0x00001A06
		// (set) Token: 0x06000077 RID: 119 RVA: 0x0000380E File Offset: 0x00001A0E
		public List<string> SelectedInstallableUnits { get; set; }

		// Token: 0x06000078 RID: 120 RVA: 0x00003818 File Offset: 0x00001A18
		protected override void AddParameters()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.AddParameters();
			string domainController = base.SetupContext.DomainController;
			if (this.isADDependentRole && !string.IsNullOrEmpty(domainController))
			{
				base.Parameters.AddWithValue("DomainController", new Fqdn(domainController));
			}
			SetupLogger.TraceExit();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003870 File Offset: 0x00001A70
		public virtual void UpdatePreCheckTaskDataHandler()
		{
			if (this.InstallableUnitName != null)
			{
				PrerequisiteAnalysisTaskDataHandler instance = PrerequisiteAnalysisTaskDataHandler.GetInstance(base.SetupContext, base.Connection);
				instance.AddRoleByUnitName(this.InstallableUnitName);
				instance.TargetDir = base.SetupContext.TargetDir;
				instance.AddSelectedInstallableUnits(this.SelectedInstallableUnits);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000038C0 File Offset: 0x00001AC0
		// (set) Token: 0x0600007B RID: 123 RVA: 0x000038C8 File Offset: 0x00001AC8
		public bool ValidationDelayed
		{
			get
			{
				return this.validationDelayed;
			}
			set
			{
				this.validationDelayed = value;
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000038D4 File Offset: 0x00001AD4
		public sealed override ValidationError[] Validate()
		{
			List<ValidationError> list = new List<ValidationError>();
			if (!this.ValidationDelayed)
			{
				list.AddRange(this.ValidateConfiguration());
				list.AddRange(base.Validate());
			}
			return list.ToArray();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003910 File Offset: 0x00001B10
		public virtual ValidationError[] ValidateConfiguration()
		{
			List<ValidationError> list = new List<ValidationError>();
			if (!string.IsNullOrEmpty(this.InstallableUnitName))
			{
				ConfigurationStatus configurationStatus = new ConfigurationStatus(this.InstallableUnitName);
				RolesUtility.GetConfiguringStatus(ref configurationStatus);
				InstallationModes action = configurationStatus.Action;
				if (action != InstallationModes.Unknown && action != base.SetupContext.InstallationMode && (action != InstallationModes.Install || base.SetupContext.InstallationMode != InstallationModes.Uninstall))
				{
					list.Add(new SetupValidationError(Strings.IllegalResumptionException(action.ToString(), base.SetupContext.InstallationMode.ToString())));
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000039AA File Offset: 0x00001BAA
		protected override void OnSaveData()
		{
			this.LogSetupConfigurationStartEvent();
			base.OnSaveData();
			if (base.WorkUnits.Count > 0 && !this.IsSucceeded)
			{
				this.LogSetupConfigurationFailureEvent();
				return;
			}
			this.LogSetupConfigurationSuccessEvent();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000039DB File Offset: 0x00001BDB
		protected string GetMsiSourcePath()
		{
			return base.SetupContext.SourceDir.PathName + Path.DirectorySeparatorChar;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000039FC File Offset: 0x00001BFC
		private void LogSetupConfigurationStartEvent()
		{
			if (!string.IsNullOrEmpty(this.InstallableUnitName))
			{
				SetupEventLog.LogStartEvent(this.InstallableUnitName);
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003A16 File Offset: 0x00001C16
		private void LogSetupConfigurationSuccessEvent()
		{
			if (!string.IsNullOrEmpty(this.InstallableUnitName))
			{
				SetupEventLog.LogSuccessEvent(this.InstallableUnitName);
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003A30 File Offset: 0x00001C30
		private void LogSetupConfigurationFailureEvent()
		{
			if (!string.IsNullOrEmpty(this.InstallableUnitName))
			{
				string text = base.WorkUnits[0].ErrorsDescription;
				if (text.Length > 32766)
				{
					text = text.Substring(0, 32766);
				}
				SetupEventLog.LogFailureEvent(this.InstallableUnitName, text);
			}
		}

		// Token: 0x0400001A RID: 26
		private string installableUnitName;

		// Token: 0x0400001B RID: 27
		private InstallableUnitConfigurationInfo installableUnitConfigurationInfo;

		// Token: 0x0400001C RID: 28
		protected List<ConfigurationDataHandler> nestedConfigurationDataHandlers = new List<ConfigurationDataHandler>();

		// Token: 0x0400001D RID: 29
		private bool validationDelayed;

		// Token: 0x0400001E RID: 30
		protected bool isADDependentRole;
	}
}
