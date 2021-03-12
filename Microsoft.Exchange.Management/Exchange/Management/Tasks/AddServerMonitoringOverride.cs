using System;
using System.IO;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000584 RID: 1412
	[Cmdlet("Add", "ServerMonitoringOverride", SupportsShouldProcess = true, DefaultParameterSetName = "Duration")]
	public sealed class AddServerMonitoringOverride : ServerMonitoringOverrideBase
	{
		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x060031C0 RID: 12736 RVA: 0x000CA3E0 File Offset: 0x000C85E0
		// (set) Token: 0x060031C1 RID: 12737 RVA: 0x000CA3F7 File Offset: 0x000C85F7
		[Parameter(Mandatory = true, Position = 0)]
		[ValidateNotNullOrEmpty]
		public string Identity
		{
			get
			{
				return (string)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x060031C2 RID: 12738 RVA: 0x000CA40A File Offset: 0x000C860A
		// (set) Token: 0x060031C3 RID: 12739 RVA: 0x000CA421 File Offset: 0x000C8621
		[Parameter(Mandatory = true)]
		public MonitoringItemTypeEnum ItemType
		{
			get
			{
				return (MonitoringItemTypeEnum)base.Fields["ItemType"];
			}
			set
			{
				base.Fields["ItemType"] = value;
			}
		}

		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x060031C4 RID: 12740 RVA: 0x000CA439 File Offset: 0x000C8639
		// (set) Token: 0x060031C5 RID: 12741 RVA: 0x000CA450 File Offset: 0x000C8650
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true)]
		public string PropertyName
		{
			get
			{
				return (string)base.Fields["PropertyName"];
			}
			set
			{
				base.Fields["PropertyName"] = value;
			}
		}

		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x060031C6 RID: 12742 RVA: 0x000CA463 File Offset: 0x000C8663
		// (set) Token: 0x060031C7 RID: 12743 RVA: 0x000CA47A File Offset: 0x000C867A
		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public string PropertyValue
		{
			get
			{
				return (string)base.Fields["PropertyValue"];
			}
			set
			{
				base.Fields["PropertyValue"] = value;
			}
		}

		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x060031C8 RID: 12744 RVA: 0x000CA490 File Offset: 0x000C8690
		// (set) Token: 0x060031C9 RID: 12745 RVA: 0x000CA4CE File Offset: 0x000C86CE
		[Parameter(Mandatory = false, ParameterSetName = "Duration")]
		public EnhancedTimeSpan? Duration
		{
			get
			{
				if (!base.Fields.Contains("Duration"))
				{
					return null;
				}
				return (EnhancedTimeSpan?)base.Fields["Duration"];
			}
			set
			{
				base.Fields["Duration"] = value;
			}
		}

		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x060031CA RID: 12746 RVA: 0x000CA4E6 File Offset: 0x000C86E6
		// (set) Token: 0x060031CB RID: 12747 RVA: 0x000CA4FD File Offset: 0x000C86FD
		[Parameter(Mandatory = true, ParameterSetName = "ApplyVersion")]
		public Version ApplyVersion
		{
			get
			{
				return (Version)base.Fields["ApplyVersion"];
			}
			set
			{
				base.Fields["ApplyVersion"] = value;
			}
		}

		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x060031CC RID: 12748 RVA: 0x000CA510 File Offset: 0x000C8710
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageAddMonitoringOverride(this.PropertyName, this.helper.MonitoringItemIdentity, this.ItemType.ToString());
			}
		}

		// Token: 0x060031CD RID: 12749 RVA: 0x000CA538 File Offset: 0x000C8738
		protected override void InternalValidate()
		{
			base.InternalValidate();
			this.helper.ParseAndValidateIdentity(this.Identity, false);
			if (base.Fields.IsModified("ApplyVersion"))
			{
				MonitoringOverrideHelpers.ValidateApplyVersion(this.ApplyVersion);
			}
			if (base.Fields.IsModified("Duration"))
			{
				MonitoringOverrideHelpers.ValidateOverrideDuration(this.Duration);
				return;
			}
			this.Duration = new EnhancedTimeSpan?(EnhancedTimeSpan.FromDays(365.0));
		}

		// Token: 0x060031CE RID: 12750 RVA: 0x000CA5B4 File Offset: 0x000C87B4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (!base.ValidateMonitoringItemExist(this.helper.HealthSet, this.ItemType) && !base.ShouldProcess(Strings.ConfirmationMonitoringItemNotFound(this.helper.MonitoringItemIdentity, this.ItemType.ToString())))
			{
				TaskLogger.LogExit();
				return;
			}
			try
			{
				using (RegistryKey registryKey = base.RegistryKeyHive.CreateSubKey(ServerMonitoringOverrideBase.OverridesBaseRegistryKey, RegistryKeyPermissionCheck.ReadWriteSubTree))
				{
					using (RegistryKey registryKey2 = registryKey.CreateSubKey(this.ItemType.ToString(), RegistryKeyPermissionCheck.ReadWriteSubTree))
					{
						string[] subKeyNames = registryKey2.GetSubKeyNames();
						string text = base.GenerateOverrideString(this.helper.MonitoringItemIdentity, this.PropertyName);
						base.ValidateGlobalLocalConflict(text, subKeyNames, this.PropertyName, this.ItemType);
						using (RegistryKey registryKey3 = registryKey2.CreateSubKey(text))
						{
							registryKey3.SetValue("PropertyValue", this.PropertyValue, RegistryValueKind.String);
							registryKey3.SetValue("TimeUpdated", DateTime.UtcNow.ToString("u"), RegistryValueKind.String);
							registryKey3.SetValue("CreatedBy", base.ExecutingUserIdentityName, RegistryValueKind.String);
							if (base.Fields.IsModified("Duration"))
							{
								registryKey3.SetValue("ExpirationTime", DateTime.UtcNow.AddSeconds(this.Duration.Value.TotalSeconds).ToString("u"), RegistryValueKind.String);
							}
							if (base.Fields.IsModified("ApplyVersion"))
							{
								registryKey3.SetValue("ApplyVersion", this.ApplyVersion.ToString(true), RegistryValueKind.String);
							}
							registryKey.SetValue("Watermark", Guid.NewGuid().ToString(), RegistryValueKind.String);
							registryKey.SetValue("TimeUpdated", DateTime.UtcNow, RegistryValueKind.String);
						}
					}
				}
				if (base.IsVerboseOn)
				{
					base.WriteVerbose(Strings.SuccessAddServerMonitoringOverride(this.helper.MonitoringItemName, base.ServerName));
				}
			}
			catch (IOException ex)
			{
				base.WriteError(new FailedToRunServerMonitoringOverrideException(base.ServerName, ex.ToString()), ErrorCategory.ObjectNotFound, null);
			}
			catch (SecurityException ex2)
			{
				base.WriteError(new FailedToRunServerMonitoringOverrideException(base.ServerName, ex2.ToString()), ExchangeErrorCategory.Authorization, null);
			}
			catch (UnauthorizedAccessException ex3)
			{
				base.WriteError(new FailedToRunServerMonitoringOverrideException(base.ServerName, ex3.ToString()), ExchangeErrorCategory.Authorization, null);
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}
	}
}
