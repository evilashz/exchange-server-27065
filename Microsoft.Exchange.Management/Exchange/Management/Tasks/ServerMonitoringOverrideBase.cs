using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000583 RID: 1411
	public abstract class ServerMonitoringOverrideBase : Task
	{
		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x060031AD RID: 12717 RVA: 0x000C9E69 File Offset: 0x000C8069
		internal string ServerName
		{
			get
			{
				return this.Server.ToString();
			}
		}

		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x060031AE RID: 12718 RVA: 0x000C9E76 File Offset: 0x000C8076
		// (set) Token: 0x060031AF RID: 12719 RVA: 0x000C9E7E File Offset: 0x000C807E
		internal RegistryKey RegistryKeyHive
		{
			get
			{
				return this.registryKeyHive;
			}
			set
			{
				this.registryKeyHive = value;
			}
		}

		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x060031B0 RID: 12720 RVA: 0x000C9E87 File Offset: 0x000C8087
		// (set) Token: 0x060031B1 RID: 12721 RVA: 0x000C9E9E File Offset: 0x000C809E
		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public ServerIdParameter Server
		{
			get
			{
				return (ServerIdParameter)base.Fields["Server"];
			}
			set
			{
				base.Fields["Server"] = value;
			}
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x000C9EB4 File Offset: 0x000C80B4
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				if (this.registryKeyHive == null)
				{
					this.registryKeyHive = this.OpenHive(this.Server.Fqdn);
				}
			}
			catch (IOException ex)
			{
				base.WriteError(new FailedToRunServerMonitoringOverrideException(this.ServerName, ex.ToString()), ErrorCategory.ObjectNotFound, null);
			}
			catch (SecurityException ex2)
			{
				base.WriteError(new FailedToRunServerMonitoringOverrideException(this.ServerName, ex2.ToString()), ExchangeErrorCategory.Authorization, null);
			}
			catch (UnauthorizedAccessException ex3)
			{
				base.WriteError(new FailedToRunServerMonitoringOverrideException(this.ServerName, ex3.ToString()), ExchangeErrorCategory.Authorization, null);
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x060031B3 RID: 12723 RVA: 0x000C9F80 File Offset: 0x000C8180
		internal RegistryKey OpenHive(string serverName)
		{
			if (string.IsNullOrWhiteSpace(serverName))
			{
				throw new ArgumentNullException("serverName");
			}
			return RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, serverName);
		}

		// Token: 0x060031B4 RID: 12724 RVA: 0x000C9FA0 File Offset: 0x000C81A0
		internal bool ValidateMonitoringItemExist(string healthsetName, MonitoringItemTypeEnum itemType)
		{
			List<RpcGetMonitoringItemIdentity.RpcMonitorItemIdentity> monitoringItemsForHealthSet = this.GetMonitoringItemsForHealthSet(healthsetName);
			if (monitoringItemsForHealthSet != null)
			{
				foreach (RpcGetMonitoringItemIdentity.RpcMonitorItemIdentity rpcMonitorItemIdentity in monitoringItemsForHealthSet)
				{
					if (string.Compare(itemType.ToString(), rpcMonitorItemIdentity.ItemType, true) == 0 && string.Compare(rpcMonitorItemIdentity.Name, this.helper.MonitoringItemName, true) == 0 && (string.IsNullOrWhiteSpace(this.helper.TargetResource) || string.Compare(rpcMonitorItemIdentity.TargetResource, this.helper.TargetResource, true) == 0))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060031B5 RID: 12725 RVA: 0x000CA05C File Offset: 0x000C825C
		internal string GetConvertedString(string stringToReplace, char oldChar, char newChar)
		{
			return stringToReplace.Replace(oldChar, newChar);
		}

		// Token: 0x060031B6 RID: 12726 RVA: 0x000CA068 File Offset: 0x000C8268
		internal string GetPropertyName(string registryKeyName)
		{
			string[] array = MonitoringOverrideHelpers.SplitMonitoringItemIdentity(registryKeyName, '~');
			if (array != null && array.Length >= 3)
			{
				return array[2];
			}
			return string.Empty;
		}

		// Token: 0x060031B7 RID: 12727 RVA: 0x000CA090 File Offset: 0x000C8290
		internal string GenerateMonitoringItemIdentityString(string fullString)
		{
			string[] array = MonitoringOverrideHelpers.SplitMonitoringItemIdentity(fullString, '~');
			if (array != null)
			{
				if (array.Length == 4)
				{
					return string.Format("{0}{1}{2}{1}{3}", new object[]
					{
						array[0],
						'\\',
						array[1],
						array[3]
					});
				}
				if (array.Length == 3)
				{
					return string.Format("{0}{1}{2}", array[0], '\\', array[1]);
				}
			}
			return fullString;
		}

		// Token: 0x060031B8 RID: 12728 RVA: 0x000CA100 File Offset: 0x000C8300
		internal string GenerateOverrideString(string monitoringItemIdentity, string propertyName)
		{
			if (string.IsNullOrWhiteSpace(this.helper.TargetResource))
			{
				return string.Format("{0}{1}{2}{1}{3}", new object[]
				{
					this.helper.HealthSet,
					'~',
					this.helper.MonitoringItemName,
					propertyName
				});
			}
			return string.Format("{0}{1}{2}{1}{3}{1}{4}", new object[]
			{
				this.helper.HealthSet,
				'~',
				this.helper.MonitoringItemName,
				propertyName,
				this.helper.TargetResource
			});
		}

		// Token: 0x060031B9 RID: 12729 RVA: 0x000CA1A8 File Offset: 0x000C83A8
		internal void ValidateGlobalLocalConflict(string identity, string[] registryKeys, string propertyName, MonitoringItemTypeEnum monitoringItemType)
		{
			if (registryKeys != null)
			{
				foreach (string text in registryKeys)
				{
					if (string.Compare(text, identity, true) == 0)
					{
						base.WriteError(new PropertyAlreadyHasAnOverrideException(propertyName, this.helper.MonitoringItemIdentity, monitoringItemType.ToString()), ErrorCategory.ResourceExists, null);
					}
					else
					{
						string[] array = MonitoringOverrideHelpers.SplitMonitoringItemIdentity(text, '~');
						if (array.Length > 1 && string.Compare(array[0], this.helper.HealthSet, true) == 0 && string.Compare(array[1], this.helper.MonitoringItemName, true) == 0)
						{
							if (array.Length == 4 && !string.IsNullOrWhiteSpace(this.helper.TargetResource))
							{
								return;
							}
							if (array.Length == 4 && string.IsNullOrWhiteSpace(this.helper.TargetResource))
							{
								base.WriteError(new MonitoringItemAlreadyHasLocalOverrideException(this.helper.MonitoringItemIdentity, monitoringItemType.ToString(), this.GenerateMonitoringItemIdentityString(text)), ErrorCategory.ResourceExists, null);
							}
							else if (array.Length == 3 && !string.IsNullOrWhiteSpace(this.helper.TargetResource))
							{
								base.WriteError(new MonitoringItemAlreadyHasGlobalOverrideException(this.helper.MonitoringItemIdentity, monitoringItemType.ToString(), this.GenerateMonitoringItemIdentityString(text)), ErrorCategory.ResourceExists, null);
							}
						}
					}
				}
			}
		}

		// Token: 0x060031BA RID: 12730 RVA: 0x000CA2F4 File Offset: 0x000C84F4
		private List<RpcGetMonitoringItemIdentity.RpcMonitorItemIdentity> GetMonitoringItemsForHealthSet(string healthSetName)
		{
			List<RpcGetMonitoringItemIdentity.RpcMonitorItemIdentity> result = null;
			try
			{
				result = RpcGetMonitoringItemIdentity.Invoke(this.Server.Fqdn, healthSetName, 900000);
			}
			catch (ActiveMonitoringServerException)
			{
			}
			catch (ActiveMonitoringServerTransientException)
			{
			}
			return result;
		}

		// Token: 0x060031BB RID: 12731 RVA: 0x000CA340 File Offset: 0x000C8540
		protected override bool IsKnownException(Exception exception)
		{
			return exception is InvalidVersionException || exception is InvalidIdentityException || exception is InvalidDurationException || exception is ExAssertException || DataAccessHelper.IsDataAccessKnownException(exception) || base.IsKnownException(exception);
		}

		// Token: 0x060031BC RID: 12732 RVA: 0x000CA373 File Offset: 0x000C8573
		protected override void InternalEndProcessing()
		{
			base.InternalEndProcessing();
			if (this.registryKeyHive != null)
			{
				this.registryKeyHive.Close();
				this.registryKeyHive = null;
			}
		}

		// Token: 0x060031BD RID: 12733 RVA: 0x000CA395 File Offset: 0x000C8595
		protected override void InternalStopProcessing()
		{
			base.InternalStopProcessing();
			if (this.registryKeyHive != null)
			{
				this.registryKeyHive.Close();
				this.registryKeyHive = null;
			}
		}

		// Token: 0x04002331 RID: 9009
		private RegistryKey registryKeyHive;

		// Token: 0x04002332 RID: 9010
		protected MonitoringOverrideHelpers helper = new MonitoringOverrideHelpers();

		// Token: 0x04002333 RID: 9011
		internal static readonly string OverridesBaseRegistryKey = string.Format("SOFTWARE\\Microsoft\\ExchangeServer\\{0}\\ActiveMonitoring\\Overrides", "v15");
	}
}
