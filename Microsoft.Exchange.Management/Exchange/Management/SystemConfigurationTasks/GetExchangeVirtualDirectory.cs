using System;
using System.Collections;
using System.DirectoryServices;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Metabase;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C09 RID: 3081
	public abstract class GetExchangeVirtualDirectory<T> : GetVirtualDirectory<T> where T : ExchangeVirtualDirectory, new()
	{
		// Token: 0x170023D1 RID: 9169
		// (get) Token: 0x06007478 RID: 29816 RVA: 0x001DB827 File Offset: 0x001D9A27
		// (set) Token: 0x06007479 RID: 29817 RVA: 0x001DB84D File Offset: 0x001D9A4D
		[Parameter]
		public SwitchParameter ADPropertiesOnly
		{
			get
			{
				return (SwitchParameter)(base.Fields["ADPropertiesOnly"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ADPropertiesOnly"] = value;
			}
		}

		// Token: 0x170023D2 RID: 9170
		// (get) Token: 0x0600747A RID: 29818 RVA: 0x001DB865 File Offset: 0x001D9A65
		// (set) Token: 0x0600747B RID: 29819 RVA: 0x001DB88B File Offset: 0x001D9A8B
		[Parameter]
		public SwitchParameter ShowMailboxVirtualDirectories
		{
			get
			{
				return (SwitchParameter)(base.Fields["ShowMailboxVirtualDirectories"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ShowMailboxVirtualDirectories"] = value;
			}
		}

		// Token: 0x170023D3 RID: 9171
		// (get) Token: 0x0600747C RID: 29820 RVA: 0x001DB8A3 File Offset: 0x001D9AA3
		// (set) Token: 0x0600747D RID: 29821 RVA: 0x001DB8C9 File Offset: 0x001D9AC9
		[Parameter]
		public SwitchParameter ShowBackEndVirtualDirectories
		{
			get
			{
				return (SwitchParameter)(base.Fields["ShowBackEndVirtualDirectories"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ShowBackEndVirtualDirectories"] = value;
			}
		}

		// Token: 0x0600747E RID: 29822 RVA: 0x001DB8E4 File Offset: 0x001D9AE4
		protected override void WriteResult(IConfigurable dataObject)
		{
			ExchangeVirtualDirectory exchangeVirtualDirectory = (ExchangeVirtualDirectory)dataObject;
			if (!this.FilterBackendVdir(exchangeVirtualDirectory))
			{
				exchangeVirtualDirectory.ADPropertiesOnly = this.ADPropertiesOnly;
				if (!exchangeVirtualDirectory.IsReadOnly && !this.ADPropertiesOnly)
				{
					bool flag = true;
					try
					{
						using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(exchangeVirtualDirectory.MetabasePath, new Task.TaskErrorLoggingReThrowDelegate(this.WriteError), dataObject.Identity, false))
						{
							if (directoryEntry != null)
							{
								this.MetabaseProperties = IisUtility.GetProperties(directoryEntry);
								this.ProcessMetabaseProperties(exchangeVirtualDirectory);
								flag = false;
							}
						}
					}
					catch (IISNotInstalledException exception)
					{
						this.WriteError(exception, ErrorCategory.ReadError, null, false);
					}
					catch (IISNotReachableException exception2)
					{
						this.WriteError(exception2, ErrorCategory.ReadError, null, false);
					}
					catch (IISGeneralCOMException ex)
					{
						if (ex.Code == -2147023174)
						{
							this.WriteError(new IISNotReachableException(IisUtility.GetHostName(exchangeVirtualDirectory.MetabasePath), ex.Message), ErrorCategory.ResourceUnavailable, null, false);
						}
						else
						{
							if (ex.Code != -2147024893)
							{
								throw;
							}
							if (!this.CanIgnoreMissingMetabaseEntry())
							{
								this.WriteError(ex, ErrorCategory.ReadError, null, false);
							}
							else
							{
								this.WriteWarning(this.GetMissingMetabaseEntryWarning(exchangeVirtualDirectory));
								flag = false;
							}
						}
					}
					catch (UnauthorizedAccessException exception3)
					{
						this.WriteError(exception3, ErrorCategory.PermissionDenied, null, false);
					}
					if (flag)
					{
						return;
					}
				}
				base.WriteResult(dataObject);
			}
		}

		// Token: 0x0600747F RID: 29823 RVA: 0x001DBA64 File Offset: 0x001D9C64
		protected virtual void ProcessMetabaseProperties(ExchangeVirtualDirectory dataObject)
		{
			dataObject.Path = (string)IisUtility.GetIisPropertyValue("Path", this.MetabaseProperties);
			ExtendedProtection.LoadFromMetabase(dataObject, this);
		}

		// Token: 0x06007480 RID: 29824 RVA: 0x001DBA88 File Offset: 0x001D9C88
		protected virtual bool CanIgnoreMissingMetabaseEntry()
		{
			return false;
		}

		// Token: 0x06007481 RID: 29825 RVA: 0x001DBA8B File Offset: 0x001D9C8B
		protected virtual LocalizedString GetMissingMetabaseEntryWarning(ExchangeVirtualDirectory vdir)
		{
			return Strings.ErrorObjectNotFound(vdir.MetabasePath);
		}

		// Token: 0x06007482 RID: 29826 RVA: 0x001DBA98 File Offset: 0x001D9C98
		protected bool FilterBackendVdir(ExchangeVirtualDirectory vdir)
		{
			if (this.ShowMailboxVirtualDirectories || this.ShowBackEndVirtualDirectories)
			{
				return false;
			}
			if (ExchangeServiceVDirHelper.IsBackEndVirtualDirectory(vdir))
			{
				return true;
			}
			Server server = (Server)base.GetDataObject<Server>(new ServerIdParameter(vdir.Server), base.DataSession, null, new LocalizedString?(Strings.ErrorServerNotFound(vdir.Server.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(vdir.Server.ToString())));
			return server.IsE15OrLater && !server.IsCafeServer && server.IsMailboxServer;
		}

		// Token: 0x04003B56 RID: 15190
		private ICollection MetabaseProperties;
	}
}
