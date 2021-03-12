using System;
using System.IO;
using System.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management;
using Microsoft.Web.Administration;

namespace Microsoft.Exchange.Servicelets.RPCHTTP
{
	// Token: 0x0200000B RID: 11
	internal sealed class VirtualDirectoryConfiguration : DisposeTrackableBase
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00003C18 File Offset: 0x00001E18
		public VirtualDirectoryConfiguration(RpcHandlerMode rpcHandlerMode)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.serverManager = new ServerManager();
				this.rpcHandlerMode = rpcHandlerMode;
				if (this.serverManager.Sites.Count == 0)
				{
					throw new WebSitesNotConfiguredException();
				}
				this.defaultWebSiteName = this.serverManager.Sites[0].Name;
				this.applicationHostConfig = this.serverManager.GetApplicationHostConfiguration();
				disposeGuard.Success();
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00003CB0 File Offset: 0x00001EB0
		public static string BackEndWebSiteName
		{
			get
			{
				return "Exchange Back End";
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00003CB7 File Offset: 0x00001EB7
		public string DefaultWebSiteName
		{
			get
			{
				return this.defaultWebSiteName;
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003CBF File Offset: 0x00001EBF
		private bool ShouldUseCertificateSettings(string webSiteName, RpcVirtualDirectoryName rpcVirtualDirectoryName)
		{
			return string.Equals(webSiteName, this.DefaultWebSiteName, StringComparison.OrdinalIgnoreCase) && rpcVirtualDirectoryName == RpcVirtualDirectoryName.RpcWithCert;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003CD8 File Offset: 0x00001ED8
		public void ConfigureRpc(string webSiteName, RpcVirtualDirectoryName rpcVirtualDirectoryName)
		{
			string virtualDirectoryPath = VirtualDirectoryConfiguration.GetVirtualDirectoryPath(rpcVirtualDirectoryName);
			bool useCertificateSettings = this.ShouldUseCertificateSettings(webSiteName, rpcVirtualDirectoryName);
			this.FixRpcAppPool();
			this.ConfigureSiteApplication(webSiteName, virtualDirectoryPath);
			this.ConfigureLocationSettings(webSiteName, virtualDirectoryPath, useCertificateSettings);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003D0C File Offset: 0x00001F0C
		public void ConfigureRpcSecurity(string webSiteName, RpcVirtualDirectoryName rpcVirtualDirectoryName, VirtualDirectorySecuritySettings securitySettings)
		{
			this.ConfigureSecuritySettings(webSiteName, VirtualDirectoryConfiguration.GetVirtualDirectoryPath(rpcVirtualDirectoryName), securitySettings);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003D1C File Offset: 0x00001F1C
		public void RemoveRpc(string webSiteName, RpcVirtualDirectoryName rpcVirtualDirectoryName)
		{
			this.RemoveSiteApplication(webSiteName, VirtualDirectoryConfiguration.GetVirtualDirectoryPath(rpcVirtualDirectoryName));
			this.RemoveLocationSettings(webSiteName, VirtualDirectoryConfiguration.GetVirtualDirectoryPath(rpcVirtualDirectoryName));
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003D38 File Offset: 0x00001F38
		public bool Commit()
		{
			bool result = this.isDirty;
			if (this.isDirty)
			{
				this.serverManager.CommitChanges();
				this.isDirty = false;
			}
			if (!this.isRpcAppPoolNew && this.recycleRpcAppPool)
			{
				string applicationPoolName = this.GetApplicationPoolName();
				ApplicationPoolCollection applicationPools = this.serverManager.ApplicationPools;
				ApplicationPool applicationPool = applicationPools[applicationPoolName];
				if (applicationPool == null)
				{
					throw new InvalidOperationException(applicationPoolName + " not found!");
				}
				applicationPool.Recycle();
				this.isRpcAppPoolNew = false;
				this.recycleRpcAppPool = false;
			}
			return result;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003DBB File Offset: 0x00001FBB
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				DisposeGuard.DisposeIfPresent(this.serverManager);
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003DCB File Offset: 0x00001FCB
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<VirtualDirectoryConfiguration>(this);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003DD4 File Offset: 0x00001FD4
		private string GetApplicationPhysicalPath()
		{
			switch (this.rpcHandlerMode)
			{
			case RpcHandlerMode.RpcProxy:
				return "%windir%\\System32\\RpcProxy";
			case RpcHandlerMode.HttpProxy:
				return Path.Combine(ConfigurationContext.Setup.InstallPath, "FrontEnd\\HttpProxy\\rpc");
			default:
				throw new InvalidOperationException("Unrecognized handler mode: " + this.rpcHandlerMode);
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003E28 File Offset: 0x00002028
		private string GetClrConfigFilePath()
		{
			switch (this.rpcHandlerMode)
			{
			case RpcHandlerMode.RpcProxy:
				return Path.Combine(ConfigurationContext.Setup.InstallPath, "bin\\MSExchangeRpcProxyAppPool_CLRConfig.config");
			case RpcHandlerMode.HttpProxy:
				return Path.Combine(ConfigurationContext.Setup.InstallPath, "bin\\MSExchangeRpcProxyFrontEndAppPool_CLRConfig.config");
			default:
				throw new InvalidOperationException("Unrecognized handler mode: " + this.rpcHandlerMode);
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003E88 File Offset: 0x00002088
		private string GetApplicationPoolName()
		{
			switch (this.rpcHandlerMode)
			{
			case RpcHandlerMode.RpcProxy:
				return "MSExchangeRpcProxyAppPool";
			case RpcHandlerMode.HttpProxy:
				return "MSExchangeRpcProxyFrontEndAppPool";
			default:
				throw new InvalidOperationException("Unrecognized handler mode: " + this.rpcHandlerMode);
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003ED4 File Offset: 0x000020D4
		private static string GetVirtualDirectoryPath(RpcVirtualDirectoryName rpcVirtualDirectoryName)
		{
			switch (rpcVirtualDirectoryName)
			{
			case RpcVirtualDirectoryName.Rpc:
				return "/Rpc";
			case RpcVirtualDirectoryName.RpcWithCert:
				return "/RpcWithCert";
			default:
				throw new InvalidOperationException("Unrecognized virtual directory type: " + rpcVirtualDirectoryName);
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003F14 File Offset: 0x00002114
		private ConfigurationElement GetSiteElement(string siteName)
		{
			ConfigurationSection section = this.applicationHostConfig.GetSection("system.applicationHost/sites");
			ConfigurationElementCollection collection = section.GetCollection();
			ConfigurationElement configurationElement = IISConfigurationUtilities.FindElement(collection, "site", "name", siteName);
			if (configurationElement == null)
			{
				throw new WebSiteNotFoundException(siteName);
			}
			return configurationElement;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003F58 File Offset: 0x00002158
		private void FixRpcAppPool()
		{
			string applicationPoolName = this.GetApplicationPoolName();
			string clrConfigFilePath = this.GetClrConfigFilePath();
			bool flag = IISConfigurationUtilities.CreateOrUpdateApplicationPool(this.serverManager, applicationPoolName, clrConfigFilePath, out this.isRpcAppPoolNew);
			this.recycleRpcAppPool = (this.recycleRpcAppPool || flag);
			this.isDirty = (this.isDirty || flag);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003FA8 File Offset: 0x000021A8
		private void ConfigureSiteApplication(string webSiteName, string path)
		{
			ConfigurationElement siteElement = this.GetSiteElement(webSiteName);
			ConfigurationElementCollection collection = siteElement.GetCollection();
			ConfigurationElement configurationElement = IISConfigurationUtilities.FindElement(collection, "application", "path", path);
			if (configurationElement == null)
			{
				configurationElement = collection.CreateElement("application");
				configurationElement["path"] = path;
				collection.Add(configurationElement);
				this.isDirty = true;
			}
			string applicationPoolName = this.GetApplicationPoolName();
			this.recycleRpcAppPool |= IISConfigurationUtilities.UpdateElementAttribute(configurationElement, "applicationPool", applicationPoolName);
			this.isDirty |= this.recycleRpcAppPool;
			ConfigurationElementCollection collection2 = configurationElement.GetCollection();
			ConfigurationElement configurationElement2 = IISConfigurationUtilities.FindElement(collection2, "virtualDirectory", "path", "/");
			if (configurationElement2 == null)
			{
				configurationElement2 = collection2.CreateElement("virtualDirectory");
				configurationElement2["path"] = "/";
				collection2.Add(configurationElement2);
				this.isDirty = true;
			}
			this.isDirty |= IISConfigurationUtilities.UpdateElementAttribute(configurationElement2, "physicalPath", this.GetApplicationPhysicalPath());
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000040A8 File Offset: 0x000022A8
		private void ConfigureLocationSettings(string webSiteName, string path, bool useCertificateSettings)
		{
			string text = webSiteName + path;
			ConfigurationSection section = this.applicationHostConfig.GetSection("system.webServer/serverRuntime", text);
			if (string.Compare(webSiteName, this.DefaultWebSiteName, true) == 0)
			{
				this.isDirty |= IISConfigurationUtilities.UpdateSectionAttribute(section, "appConcurrentRequestLimit", 120000);
			}
			else
			{
				this.isDirty |= IISConfigurationUtilities.UpdateSectionAttribute(section, "appConcurrentRequestLimit", 65535);
			}
			this.isDirty |= IISConfigurationUtilities.UpdateSectionAttribute(section, "uploadReadAheadSize", 0);
			ConfigurationSection section2 = this.applicationHostConfig.GetSection("system.webServer/directoryBrowse", text);
			this.isDirty |= IISConfigurationUtilities.UpdateSectionAttribute(section2, "enabled", false);
			this.isDirty |= IISConfigurationUtilities.UpdateSectionAttribute(section2, "showFlags", "Date, Time, Size, Extension");
			ConfigurationSection section3 = this.applicationHostConfig.GetSection("system.webServer/defaultDocument", text);
			this.isDirty |= IISConfigurationUtilities.UpdateSectionAttribute(section3, "enabled", true);
			ConfigurationSection section4 = this.applicationHostConfig.GetSection("system.webServer/security/requestFiltering", text);
			ConfigurationElement childElement = section4.GetChildElement("requestLimits");
			this.isDirty |= IISConfigurationUtilities.UpdateElementAttribute(childElement, "maxAllowedContentLength", 2147483648U);
			string text2 = null;
			if (this.rpcHandlerMode == RpcHandlerMode.RpcProxy)
			{
				text2 = "None";
			}
			if (useCertificateSettings)
			{
				text2 = "Ssl, SslNegotiateCert, SslRequireCert";
			}
			if (text2 != null)
			{
				ConfigurationSection section5 = this.applicationHostConfig.GetSection("system.webServer/security/access", text);
				this.isDirty |= IISConfigurationUtilities.UpdateSectionAttribute(section5, "sslFlags", text2);
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00004254 File Offset: 0x00002454
		private void ConfigureSecuritySettings(string webSiteName, string path, VirtualDirectorySecuritySettings securitySettings)
		{
			string text = webSiteName + path;
			ConfigurationSection section = this.applicationHostConfig.GetSection("system.webServer/security/authentication/anonymousAuthentication", text);
			this.isDirty |= IISConfigurationUtilities.UpdateSectionAttribute(section, "enabled", securitySettings.Anonymous);
			ConfigurationSection section2 = this.applicationHostConfig.GetSection("system.webServer/security/authentication/basicAuthentication", text);
			this.isDirty |= IISConfigurationUtilities.UpdateSectionAttribute(section2, "enabled", securitySettings.Basic);
			ConfigurationSection section3 = this.applicationHostConfig.GetSection("system.webServer/security/authentication/clientCertificateMappingAuthentication", text);
			this.isDirty |= IISConfigurationUtilities.UpdateSectionAttribute(section3, "enabled", securitySettings.ClientCertificateMapping);
			ConfigurationSection section4 = this.applicationHostConfig.GetSection("system.webServer/security/authentication/digestAuthentication", text);
			this.isDirty |= IISConfigurationUtilities.UpdateSectionAttribute(section4, "enabled", securitySettings.Digest);
			ConfigurationSection section5 = this.applicationHostConfig.GetSection("system.webServer/security/authentication/iisClientCertificateMappingAuthentication", text);
			this.isDirty |= IISConfigurationUtilities.UpdateSectionAttribute(section5, "enabled", securitySettings.IisClientCertificateMapping);
			ConfigurationSection section6 = this.applicationHostConfig.GetSection("system.webServer/security/authentication/windowsAuthentication", text);
			this.isDirty |= IISConfigurationUtilities.UpdateSectionAttribute(section6, "enabled", securitySettings.Windows);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000043B0 File Offset: 0x000025B0
		private void RemoveSiteApplication(string webSiteName, string path)
		{
			ConfigurationElement siteElement = this.GetSiteElement(webSiteName);
			ConfigurationElementCollection collection = siteElement.GetCollection();
			ConfigurationElement configurationElement = IISConfigurationUtilities.FindElement(collection, "application", "path", path);
			if (configurationElement != null)
			{
				collection.Remove(configurationElement);
				this.isDirty = true;
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000043F0 File Offset: 0x000025F0
		private void RemoveLocationSettings(string webSiteName, string path)
		{
			string text = webSiteName + path;
			string[] locationPaths = this.applicationHostConfig.GetLocationPaths();
			if (locationPaths.Contains(text))
			{
				this.applicationHostConfig.RemoveLocationPath(text);
				this.isDirty = true;
			}
		}

		// Token: 0x04000038 RID: 56
		public const string RpcProxyFrontEndApplicationPoolName = "MSExchangeRpcProxyFrontEndAppPool";

		// Token: 0x04000039 RID: 57
		public const string RpcProxyApplicationPoolName = "MSExchangeRpcProxyAppPool";

		// Token: 0x0400003A RID: 58
		public const string RpcProxyClrConfigFilePath_Cafe = "bin\\MSExchangeRpcProxyFrontEndAppPool_CLRConfig.config";

		// Token: 0x0400003B RID: 59
		public const string RpcProxyClrConfigFilePath_Mailbox = "bin\\MSExchangeRpcProxyAppPool_CLRConfig.config";

		// Token: 0x0400003C RID: 60
		public const string RpcVirtualDirectoryPath = "/Rpc";

		// Token: 0x0400003D RID: 61
		public const string RpcWithCertVirtualDirectoryPath = "/RpcWithCert";

		// Token: 0x0400003E RID: 62
		public const string ExchangeBackEndWebSiteName = "Exchange Back End";

		// Token: 0x0400003F RID: 63
		private readonly string defaultWebSiteName;

		// Token: 0x04000040 RID: 64
		private readonly ServerManager serverManager;

		// Token: 0x04000041 RID: 65
		private readonly Configuration applicationHostConfig;

		// Token: 0x04000042 RID: 66
		private bool isDirty;

		// Token: 0x04000043 RID: 67
		private bool isRpcAppPoolNew;

		// Token: 0x04000044 RID: 68
		private bool recycleRpcAppPool;

		// Token: 0x04000045 RID: 69
		private RpcHandlerMode rpcHandlerMode;
	}
}
