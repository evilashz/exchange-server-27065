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
	// Token: 0x020005BB RID: 1467
	[Cmdlet("Remove", "ServerMonitoringOverride", SupportsShouldProcess = true)]
	public sealed class RemoveServerMonitoringOverride : ServerMonitoringOverrideBase
	{
		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x06003378 RID: 13176 RVA: 0x000D1225 File Offset: 0x000CF425
		// (set) Token: 0x06003379 RID: 13177 RVA: 0x000D123C File Offset: 0x000CF43C
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

		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x0600337A RID: 13178 RVA: 0x000D124F File Offset: 0x000CF44F
		// (set) Token: 0x0600337B RID: 13179 RVA: 0x000D1266 File Offset: 0x000CF466
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

		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x0600337C RID: 13180 RVA: 0x000D127E File Offset: 0x000CF47E
		// (set) Token: 0x0600337D RID: 13181 RVA: 0x000D1295 File Offset: 0x000CF495
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

		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x0600337E RID: 13182 RVA: 0x000D12A8 File Offset: 0x000CF4A8
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveMonitoringOverride(this.PropertyName, this.helper.MonitoringItemIdentity, this.ItemType.ToString());
			}
		}

		// Token: 0x0600337F RID: 13183 RVA: 0x000D12D0 File Offset: 0x000CF4D0
		protected override void InternalValidate()
		{
			base.InternalValidate();
			this.helper.ParseAndValidateIdentity(this.Identity, false);
		}

		// Token: 0x06003380 RID: 13184 RVA: 0x000D12EC File Offset: 0x000CF4EC
		protected override void InternalProcessRecord()
		{
			bool flag = false;
			TaskLogger.LogEnter();
			try
			{
				using (RegistryKey registryKey = base.RegistryKeyHive.OpenSubKey(ServerMonitoringOverrideBase.OverridesBaseRegistryKey, true))
				{
					if (registryKey != null)
					{
						using (RegistryKey registryKey2 = registryKey.OpenSubKey(this.ItemType.ToString(), true))
						{
							if (registryKey2 != null)
							{
								string subkey = base.GenerateOverrideString(this.helper.MonitoringItemIdentity, this.PropertyName);
								try
								{
									registryKey2.DeleteSubKey(subkey, true);
									registryKey.SetValue("Watermark", Guid.NewGuid().ToString(), RegistryValueKind.String);
									registryKey.SetValue("TimeUpdated", DateTime.UtcNow, RegistryValueKind.String);
									flag = true;
								}
								catch (ArgumentException)
								{
								}
							}
						}
					}
				}
				if (!flag)
				{
					base.WriteError(new OverrideNotFoundException(this.helper.MonitoringItemIdentity, this.ItemType.ToString(), this.PropertyName), ErrorCategory.ObjectNotFound, null);
				}
				if (base.IsVerboseOn)
				{
					base.WriteVerbose(Strings.SuccessRemoveServerMonitoringOverride(this.helper.MonitoringItemName, base.ServerName));
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
