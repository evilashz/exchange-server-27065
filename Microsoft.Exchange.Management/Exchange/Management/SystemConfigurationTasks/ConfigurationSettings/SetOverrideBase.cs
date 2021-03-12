using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks.ConfigurationSettings
{
	// Token: 0x02000945 RID: 2373
	public abstract class SetOverrideBase : SetTopologySystemConfigurationObjectTask<SettingOverrideIdParameter, SettingOverride>
	{
		// Token: 0x17001957 RID: 6487
		// (get) Token: 0x060054C2 RID: 21698
		protected abstract bool IsFlight { get; }

		// Token: 0x17001958 RID: 6488
		// (get) Token: 0x060054C3 RID: 21699 RVA: 0x0015E4CF File Offset: 0x0015C6CF
		// (set) Token: 0x060054C4 RID: 21700 RVA: 0x0015E4E6 File Offset: 0x0015C6E6
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

		// Token: 0x17001959 RID: 6489
		// (get) Token: 0x060054C5 RID: 21701 RVA: 0x0015E4F9 File Offset: 0x0015C6F9
		// (set) Token: 0x060054C6 RID: 21702 RVA: 0x0015E510 File Offset: 0x0015C710
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

		// Token: 0x1700195A RID: 6490
		// (get) Token: 0x060054C7 RID: 21703 RVA: 0x0015E523 File Offset: 0x0015C723
		// (set) Token: 0x060054C8 RID: 21704 RVA: 0x0015E53A File Offset: 0x0015C73A
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

		// Token: 0x1700195B RID: 6491
		// (get) Token: 0x060054C9 RID: 21705 RVA: 0x0015E54D File Offset: 0x0015C74D
		// (set) Token: 0x060054CA RID: 21706 RVA: 0x0015E564 File Offset: 0x0015C764
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

		// Token: 0x1700195C RID: 6492
		// (get) Token: 0x060054CB RID: 21707 RVA: 0x0015E577 File Offset: 0x0015C777
		// (set) Token: 0x060054CC RID: 21708 RVA: 0x0015E58E File Offset: 0x0015C78E
		[Parameter(Mandatory = false)]
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

		// Token: 0x1700195D RID: 6493
		// (get) Token: 0x060054CD RID: 21709 RVA: 0x0015E5A1 File Offset: 0x0015C7A1
		// (set) Token: 0x060054CE RID: 21710 RVA: 0x0015E5B8 File Offset: 0x0015C7B8
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
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

		// Token: 0x1700195E RID: 6494
		// (get) Token: 0x060054CF RID: 21711 RVA: 0x0015E5CB File Offset: 0x0015C7CB
		protected override ObjectId RootId
		{
			get
			{
				return SettingOverride.GetContainerId(this.IsFlight);
			}
		}

		// Token: 0x1700195F RID: 6495
		// (get) Token: 0x060054D0 RID: 21712 RVA: 0x0015E5D8 File Offset: 0x0015C7D8
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetExchangeSettings(this.DataObject.Name);
			}
		}

		// Token: 0x060054D1 RID: 21713 RVA: 0x0015E5EA File Offset: 0x0015C7EA
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || typeof(ConfigurationSettingsException).IsInstanceOfType(exception);
		}

		// Token: 0x060054D2 RID: 21714 RVA: 0x0015E608 File Offset: 0x0015C808
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (this.MinVersion != null)
			{
				if (this.MaxVersion != null && this.MinVersion > this.MaxVersion)
				{
					base.WriteError(new SettingOverrideMinVersionGreaterThanMaxVersionException(this.MinVersion.ToString(), this.MaxVersion.ToString()), ErrorCategory.InvalidOperation, null);
				}
				if (this.FixVersion != null && this.MinVersion > this.FixVersion)
				{
					base.WriteError(new SettingOverrideMinVersionGreaterThanMaxVersionException(this.MinVersion.ToString(), this.FixVersion.ToString()), ErrorCategory.InvalidOperation, null);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060054D3 RID: 21715 RVA: 0x0015E6C0 File Offset: 0x0015C8C0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			SettingOverrideXml xml = this.DataObject.Xml;
			if (base.Fields.IsModified("MinVersion"))
			{
				xml.MinVersion = this.MinVersion;
			}
			if (base.Fields.IsModified("MaxVersion"))
			{
				xml.MaxVersion = this.MaxVersion;
				if (this.MaxVersion != null)
				{
					xml.FixVersion = null;
				}
			}
			if (base.Fields.IsModified("FixVersion"))
			{
				xml.FixVersion = this.FixVersion;
				if (this.FixVersion != null)
				{
					xml.MaxVersion = null;
				}
			}
			if (base.Fields.IsModified("Server"))
			{
				xml.Server = this.Server;
			}
			if (base.Fields.IsModified("Parameters"))
			{
				xml.Parameters = this.Parameters;
			}
			if (base.Fields.IsModified("Reason"))
			{
				xml.Reason = this.Reason;
			}
			xml.ModifiedBy = base.ExecutingUserIdentityName;
			this.DataObject.Xml = xml;
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}
	}
}
