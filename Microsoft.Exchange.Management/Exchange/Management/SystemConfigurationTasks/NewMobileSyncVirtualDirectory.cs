using System;
using System.Collections;
using System.DirectoryServices;
using System.IO;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.ServicesServerTasks;
using Microsoft.Exchange.Management.Metabase;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C25 RID: 3109
	[Cmdlet("New", "ActiveSyncVirtualDirectory", SupportsShouldProcess = true)]
	public sealed class NewMobileSyncVirtualDirectory : NewExchangeVirtualDirectory<ADMobileVirtualDirectory>
	{
		// Token: 0x06007546 RID: 30022 RVA: 0x001DEE97 File Offset: 0x001DD097
		public NewMobileSyncVirtualDirectory()
		{
			base.AppPoolId = "MSExchangeSyncAppPool";
			base.AppPoolManagedPipelineMode = MetabasePropertyTypes.ManagedPipelineMode.Integrated;
			this.Name = "Microsoft-Server-ActiveSync";
		}

		// Token: 0x17002405 RID: 9221
		// (get) Token: 0x06007547 RID: 30023 RVA: 0x001DEEBC File Offset: 0x001DD0BC
		// (set) Token: 0x06007548 RID: 30024 RVA: 0x001DEEE7 File Offset: 0x001DD0E7
		[Parameter]
		public bool InstallProxySubDirectory
		{
			get
			{
				return !base.Fields.Contains("InstallProxySubDirectory") || (bool)base.Fields["InstallProxySubDirectory"];
			}
			set
			{
				base.Fields["InstallProxySubDirectory"] = value;
			}
		}

		// Token: 0x17002406 RID: 9222
		// (get) Token: 0x06007549 RID: 30025 RVA: 0x001DEEFF File Offset: 0x001DD0FF
		internal override MetabasePropertyTypes.AppPoolIdentityType AppPoolIdentityType
		{
			get
			{
				return MetabasePropertyTypes.AppPoolIdentityType.LocalSystem;
			}
		}

		// Token: 0x17002407 RID: 9223
		// (get) Token: 0x0600754A RID: 30026 RVA: 0x001DEF02 File Offset: 0x001DD102
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewMobileSyncVirtualDirectory(base.WebSiteName.ToString(), base.Server.ToString());
			}
		}

		// Token: 0x17002408 RID: 9224
		// (get) Token: 0x0600754B RID: 30027 RVA: 0x001DEF20 File Offset: 0x001DD120
		protected override ArrayList CustomizedVDirProperties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ArrayList();
					this.properties.Add(new MetabaseProperty("AppFriendlyName", this.Name));
					this.properties.Add(new MetabaseProperty("AppRoot", base.RetrieveVDirAppRootValue(base.GetAbsolutePath(IisUtility.AbsolutePathType.WebSiteRoot), this.Name)));
					this.properties.Add(new MetabaseProperty("DefaultDoc", "default.aspx"));
					this.properties.Add(new MetabaseProperty("AuthFlags", MetabasePropertyTypes.AuthFlags.Basic));
					this.properties.Add(new MetabaseProperty("AccessFlags", MetabasePropertyTypes.AccessFlags.Script));
					this.properties.Add(new MetabaseProperty("AppIsolated", MetabasePropertyTypes.AppIsolated.Pooled));
					this.properties.Add(new MetabaseProperty("HttpExpires", "D, 0x278d00"));
					this.properties.Add(new MetabaseProperty("DoDynamicCompression", true));
				}
				ExTraceGlobals.TaskTracer.Information<ArrayList>((long)this.GetHashCode(), "Got CustomizedVDirProperties: {0}", this.properties);
				return this.properties;
			}
		}

		// Token: 0x17002409 RID: 9225
		// (get) Token: 0x0600754C RID: 30028 RVA: 0x001DF054 File Offset: 0x001DD254
		// (set) Token: 0x0600754D RID: 30029 RVA: 0x001DF05C File Offset: 0x001DD25C
		private new string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x0600754E RID: 30030 RVA: 0x001DF068 File Offset: 0x001DD268
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			IConfigurable result = base.PrepareDataObject();
			if (base.Role == VirtualDirectoryRole.ClientAccess && this.DataObject.InternalUrl == null)
			{
				this.DataObject.InternalUrl = new Uri(string.Format("https://{0}/{1}", base.OwningServer.Fqdn, "Microsoft-Server-ActiveSync"));
			}
			return result;
		}

		// Token: 0x0600754F RID: 30031 RVA: 0x001DF0C8 File Offset: 0x001DD2C8
		protected override void InternalProcessMetabase()
		{
			TaskLogger.LogEnter();
			base.InternalProcessMetabase();
			if (base.Role == VirtualDirectoryRole.Mailbox)
			{
				this.UpdateVDirScriptMaps();
				this.UpdateCompressionLevel();
			}
			if (DirectoryEntry.Exists(this.DataObject.MetabasePath))
			{
				if (base.Role != VirtualDirectoryRole.Mailbox)
				{
					try
					{
						MobileSyncVirtualDirectoryHelper.InstallIsapiFilter(this.DataObject, true);
					}
					catch (Exception ex)
					{
						TaskLogger.Trace("Exception occurred in InstallIsapiFilter(): {0}", new object[]
						{
							ex.Message
						});
						this.WriteWarning(Strings.ActiveSyncMetabaseIsapiInstallFailure);
						throw;
					}
				}
				try
				{
					if (this.InstallProxySubDirectory)
					{
						MobileSyncVirtualDirectoryHelper.InstallProxySubDirectory(this.DataObject, this);
					}
				}
				catch (Exception ex2)
				{
					TaskLogger.Trace("Exception occurred in InstallProxySubDirectory(): {0}", new object[]
					{
						ex2.Message
					});
					this.WriteWarning(Strings.ActiveSyncMetabaseProxyInstallFailure);
					throw;
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06007550 RID: 30032 RVA: 0x001DF1A8 File Offset: 0x001DD3A8
		protected override void WriteResultMetabaseFixup(ExchangeVirtualDirectory dataObject)
		{
			TaskLogger.LogEnter();
			ADMobileVirtualDirectory admobileVirtualDirectory = dataObject as ADMobileVirtualDirectory;
			admobileVirtualDirectory.BasicAuthEnabled = true;
			admobileVirtualDirectory.CompressionEnabled = true;
			string metabasePath = this.DataObject.MetabasePath;
			using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(metabasePath, new Task.TaskErrorLoggingReThrowDelegate(this.WriteError), this.DataObject.Identity))
			{
				string virtualDirectoryName = (string)directoryEntry.Properties["AppFriendlyName"].Value;
				admobileVirtualDirectory.VirtualDirectoryName = virtualDirectoryName;
				admobileVirtualDirectory.WebSiteSSLEnabled = false;
				admobileVirtualDirectory.ClientCertAuth = new ClientCertAuthTypes?(ClientCertAuthTypes.Ignore);
				int? num = (int?)directoryEntry.Properties["AccessSSLFlags"].Value;
				if (num != null)
				{
					if ((num.Value & 8) == 8)
					{
						admobileVirtualDirectory.WebSiteSSLEnabled = true;
					}
					if ((num.Value & 104) == 104)
					{
						admobileVirtualDirectory.ClientCertAuth = new ClientCertAuthTypes?(ClientCertAuthTypes.Required);
					}
					else if ((num.Value & 32) == 32)
					{
						admobileVirtualDirectory.ClientCertAuth = new ClientCertAuthTypes?(ClientCertAuthTypes.Accepted);
					}
				}
				else
				{
					int startIndex = metabasePath.LastIndexOf('/');
					string iisDirectoryEntryPath = metabasePath.Remove(startIndex);
					using (DirectoryEntry directoryEntry2 = IisUtility.CreateIISDirectoryEntry(iisDirectoryEntryPath, new Task.TaskErrorLoggingReThrowDelegate(this.WriteError), this.DataObject.Identity))
					{
						num = (int?)directoryEntry2.Properties["AccessSSLFlags"].Value;
						if (num != null)
						{
							if ((num.Value & 8) == 8)
							{
								admobileVirtualDirectory.WebSiteSSLEnabled = true;
							}
							if ((num.Value & 104) == 104)
							{
								admobileVirtualDirectory.ClientCertAuth = new ClientCertAuthTypes?(ClientCertAuthTypes.Required);
							}
							else if ((num.Value & 32) == 32)
							{
								admobileVirtualDirectory.ClientCertAuth = new ClientCertAuthTypes?(ClientCertAuthTypes.Accepted);
							}
						}
					}
				}
			}
			char[] separator = new char[]
			{
				'/'
			};
			string[] array = metabasePath.Split(separator);
			int num2 = array.Length - 2;
			if (num2 <= 0)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder(47);
			for (int i = 0; i < num2; i++)
			{
				stringBuilder.Append(array[i]);
				if (i < num2 - 1)
				{
					stringBuilder.Append('/');
				}
			}
			using (DirectoryEntry directoryEntry3 = IisUtility.CreateIISDirectoryEntry(stringBuilder.ToString(), new Task.TaskErrorLoggingReThrowDelegate(this.WriteError), this.DataObject.Identity))
			{
				string websiteName = (string)directoryEntry3.Properties["ServerComment"].Value;
				admobileVirtualDirectory.WebsiteName = websiteName;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06007551 RID: 30033 RVA: 0x001DF474 File Offset: 0x001DD674
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject.Identity
			});
			if (!base.Fields.IsModified("Path"))
			{
				base.Path = System.IO.Path.Combine(ConfigurationContext.Setup.InstallPath, (base.Role == VirtualDirectoryRole.ClientAccess) ? "FrontEnd\\HttpProxy\\sync" : "ClientAccess\\sync");
			}
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06007552 RID: 30034 RVA: 0x001DF4E8 File Offset: 0x001DD6E8
		private void UpdateCompressionLevel()
		{
			string metabasePath = this.DataObject.MetabasePath;
			Gzip.SetIisGzipLevel(IisUtility.WebSiteFromMetabasePath(metabasePath), GzipLevel.High);
			Gzip.SetVirtualDirectoryGzipLevel(this.DataObject.MetabasePath, GzipLevel.High);
			if (Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major >= 6)
			{
				try
				{
					Gzip.SetIisGzipMimeTypes();
				}
				catch (Exception ex)
				{
					TaskLogger.Trace("Exception occurred in SetIisGzipMimeTypes(): {0}", new object[]
					{
						ex.Message
					});
					this.WriteWarning(Strings.SetIISGzipMimeTypesFailure);
					throw;
				}
			}
		}

		// Token: 0x06007553 RID: 30035 RVA: 0x001DF580 File Offset: 0x001DD780
		private void UpdateVDirScriptMaps()
		{
			TaskLogger.LogEnter();
			using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(this.DataObject.MetabasePath, new Task.TaskErrorLoggingReThrowDelegate(this.WriteError), this.DataObject.Identity))
			{
				string[] array = new string[directoryEntry.Properties["ScriptMaps"].Count];
				directoryEntry.Properties["ScriptMaps"].CopyTo(array, 0);
				ExTraceGlobals.TaskTracer.Information((long)this.GetHashCode(), "UpdateVDirScriptMaps got ScriptMaps property");
				int num = 0;
				string[] array2 = array;
				int i = 0;
				while (i < array2.Length)
				{
					string text = array2[i];
					if (text.StartsWith(".aspx", StringComparison.OrdinalIgnoreCase))
					{
						ExTraceGlobals.TaskTracer.Information<string>((long)this.GetHashCode(), "UpdateVDirScriptMaps found .aspx mapping: {0}", text);
						if (text.IndexOf(",options", StringComparison.OrdinalIgnoreCase) >= 0)
						{
							ExTraceGlobals.TaskTracer.Information((long)this.GetHashCode(), "Leaving UpdateVDirScriptMaps without updating .aspx mapping.");
							return;
						}
						string text2 = text + ",OPTIONS";
						directoryEntry.Properties["ScriptMaps"][num] = text2;
						ExTraceGlobals.TaskTracer.Information<string>((long)this.GetHashCode(), "UpdateVDirScriptMaps updated .aspx mapping to: {0}", text2);
						break;
					}
					else
					{
						num++;
						i++;
					}
				}
				directoryEntry.CommitChanges();
				IisUtility.CommitMetabaseChanges((base.OwningServer == null) ? null : base.OwningServer.Name);
				ExTraceGlobals.TaskTracer.Information((long)this.GetHashCode(), "UpdateVDirScriptMaps committed mapping edit to vDir object.");
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04003B7A RID: 15226
		private const string InstallProxySubDirectoryKey = "InstallProxySubDirectory";

		// Token: 0x04003B7B RID: 15227
		private ArrayList properties;
	}
}
