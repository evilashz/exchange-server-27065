using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks.ConfigurationSettings
{
	// Token: 0x02000943 RID: 2371
	public abstract class NewOverrideBase : NewSystemConfigurationObjectTask<SettingOverride>
	{
		// Token: 0x1700194A RID: 6474
		// (get) Token: 0x060054A7 RID: 21671
		protected abstract bool IsFlight { get; }

		// Token: 0x1700194B RID: 6475
		// (get) Token: 0x060054A8 RID: 21672 RVA: 0x0015E137 File Offset: 0x0015C337
		// (set) Token: 0x060054A9 RID: 21673 RVA: 0x0015E14E File Offset: 0x0015C34E
		[Parameter(Mandatory = true, Position = 0)]
		[ValidateNotNullOrEmpty]
		public new string Name
		{
			get
			{
				return base.Fields["Name"] as string;
			}
			set
			{
				base.Fields["Name"] = value;
			}
		}

		// Token: 0x1700194C RID: 6476
		// (get) Token: 0x060054AA RID: 21674 RVA: 0x0015E161 File Offset: 0x0015C361
		// (set) Token: 0x060054AB RID: 21675 RVA: 0x0015E178 File Offset: 0x0015C378
		[Parameter(Mandatory = false)]
		public Version MinVersion
		{
			get
			{
				return base.Fields["MinVersion"] as Version;
			}
			set
			{
				base.Fields["MinVersion"] = value;
			}
		}

		// Token: 0x1700194D RID: 6477
		// (get) Token: 0x060054AC RID: 21676 RVA: 0x0015E18B File Offset: 0x0015C38B
		// (set) Token: 0x060054AD RID: 21677 RVA: 0x0015E1A2 File Offset: 0x0015C3A2
		[Parameter(Mandatory = false)]
		public Version MaxVersion
		{
			get
			{
				return base.Fields["MaxVersion"] as Version;
			}
			set
			{
				base.Fields["MaxVersion"] = value;
			}
		}

		// Token: 0x1700194E RID: 6478
		// (get) Token: 0x060054AE RID: 21678 RVA: 0x0015E1B5 File Offset: 0x0015C3B5
		// (set) Token: 0x060054AF RID: 21679 RVA: 0x0015E1CC File Offset: 0x0015C3CC
		[Parameter(Mandatory = false)]
		public Version FixVersion
		{
			get
			{
				return base.Fields["FixVersion"] as Version;
			}
			set
			{
				base.Fields["FixVersion"] = value;
			}
		}

		// Token: 0x1700194F RID: 6479
		// (get) Token: 0x060054B0 RID: 21680 RVA: 0x0015E1DF File Offset: 0x0015C3DF
		// (set) Token: 0x060054B1 RID: 21681 RVA: 0x0015E1F6 File Offset: 0x0015C3F6
		[Parameter(Mandatory = false)]
		public string[] Server
		{
			get
			{
				return base.Fields["Server"] as string[];
			}
			set
			{
				base.Fields["Server"] = value;
			}
		}

		// Token: 0x17001950 RID: 6480
		// (get) Token: 0x060054B2 RID: 21682 RVA: 0x0015E209 File Offset: 0x0015C409
		// (set) Token: 0x060054B3 RID: 21683 RVA: 0x0015E220 File Offset: 0x0015C420
		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public MultiValuedProperty<string> Parameters
		{
			get
			{
				return base.Fields["Parameters"] as MultiValuedProperty<string>;
			}
			set
			{
				base.Fields["Parameters"] = value;
			}
		}

		// Token: 0x17001951 RID: 6481
		// (get) Token: 0x060054B4 RID: 21684 RVA: 0x0015E233 File Offset: 0x0015C433
		// (set) Token: 0x060054B5 RID: 21685 RVA: 0x0015E24A File Offset: 0x0015C44A
		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public string Reason
		{
			get
			{
				return base.Fields["Reason"] as string;
			}
			set
			{
				base.Fields["Reason"] = value;
			}
		}

		// Token: 0x17001952 RID: 6482
		// (get) Token: 0x060054B6 RID: 21686 RVA: 0x0015E260 File Offset: 0x0015C460
		// (set) Token: 0x060054B7 RID: 21687 RVA: 0x0015E2A2 File Offset: 0x0015C4A2
		[Parameter(Mandatory = false)]
		public SwitchParameter Force
		{
			get
			{
				SwitchParameter? switchParameter = base.Fields["Force"] as SwitchParameter?;
				if (switchParameter == null)
				{
					return default(SwitchParameter);
				}
				return switchParameter.GetValueOrDefault();
			}
			set
			{
				base.Fields["Force"] = value;
			}
		}

		// Token: 0x17001953 RID: 6483
		// (get) Token: 0x060054B8 RID: 21688 RVA: 0x0015E2BA File Offset: 0x0015C4BA
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewExchangeSettings(this.Name);
			}
		}

		// Token: 0x060054B9 RID: 21689 RVA: 0x0015E2C8 File Offset: 0x0015C4C8
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (this.MaxVersion != null && this.FixVersion != null)
			{
				base.WriteError(new SettingOverrideMaxVersionAndFixVersionSpecifiedException(), ErrorCategory.InvalidOperation, null);
			}
			if (this.MinVersion != null)
			{
				if (this.MaxVersion != null && this.MinVersion > this.MaxVersion)
				{
					base.WriteError(new SettingOverrideMinVersionGreaterThanMaxVersionException(this.MinVersion.ToString(), this.MaxVersion.ToString()), ErrorCategory.InvalidOperation, null);
				}
				if (this.FixVersion != null && this.MinVersion >= this.FixVersion)
				{
					base.WriteError(new SettingOverrideMinVersionGreaterThanMaxVersionException(this.MinVersion.ToString(), this.FixVersion.ToString()), ErrorCategory.InvalidOperation, null);
				}
			}
			try
			{
				SettingOverride.Validate(this.GetOverride());
			}
			catch (SettingOverrideException ex)
			{
				if (this.Force)
				{
					base.WriteWarning(ex.Message);
				}
				else
				{
					base.WriteError(ex, ErrorCategory.InvalidOperation, null);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060054BA RID: 21690 RVA: 0x0015E3EC File Offset: 0x0015C5EC
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			SettingOverride settingOverride = (SettingOverride)base.PrepareDataObject();
			if (base.HasErrors)
			{
				return null;
			}
			settingOverride.SetName(this.Name, this.IsFlight);
			settingOverride.Xml = this.GetXml();
			TaskLogger.LogExit();
			return settingOverride;
		}

		// Token: 0x060054BB RID: 21691 RVA: 0x0015E438 File Offset: 0x0015C638
		protected virtual SettingOverrideXml GetXml()
		{
			return new SettingOverrideXml
			{
				MinVersion = this.MinVersion,
				MaxVersion = this.MaxVersion,
				FixVersion = this.FixVersion,
				Server = this.Server,
				Parameters = this.Parameters,
				Reason = this.Reason,
				ModifiedBy = base.ExecutingUserIdentityName
			};
		}

		// Token: 0x060054BC RID: 21692
		protected abstract VariantConfigurationOverride GetOverride();
	}
}
