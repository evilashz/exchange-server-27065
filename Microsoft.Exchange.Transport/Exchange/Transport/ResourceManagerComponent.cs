using System;
using System.Globalization;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Metering.ResourceMonitoring;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.ResourceMonitoring;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000043 RID: 67
	internal sealed class ResourceManagerComponent : ITransportComponent, IDiagnosable
	{
		// Token: 0x06000187 RID: 391 RVA: 0x00007F05 File Offset: 0x00006105
		public ResourceManagerComponent(ResourceManagerResources resourcesToMonitor)
		{
			this.resourcesToMonitor = resourcesToMonitor;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00007F20 File Offset: 0x00006120
		public ResourceManager ResourceManager
		{
			get
			{
				if (this.resourceManager == null)
				{
					lock (this.syncLoad)
					{
						if (this.resourceManager == null)
						{
							ResourceManagerConfiguration resourceManagerConfiguration = new ResourceManagerConfiguration();
							resourceManagerConfiguration.Load();
							ResourceLog resourceLog = this.CreateResourceLog();
							this.resourceManager = new ResourceManager(resourceManagerConfiguration, new ResourceMonitorFactory(resourceManagerConfiguration), new ResourceManagerEventLogger(), new ResourceManagerComponentsAdapter(), this.resourcesToMonitor, resourceLog);
						}
					}
				}
				return this.resourceManager;
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00007FA8 File Offset: 0x000061A8
		void ITransportComponent.Load()
		{
			this.ResourceManager.Load();
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00007FB5 File Offset: 0x000061B5
		void ITransportComponent.Unload()
		{
			this.ThrowIfNotLoaded();
			this.resourceManager = null;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00007FC4 File Offset: 0x000061C4
		string ITransportComponent.OnUnhandledException(Exception e)
		{
			if (this.resourceManager != null)
			{
				return this.resourceManager.ToString();
			}
			return "ResourceManagerComponent is not loaded.";
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00007FDF File Offset: 0x000061DF
		private void ThrowIfNotLoaded()
		{
			if (this.resourceManager == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Attempt to retrieve ResourceManager instance before ResourceManagerComponent is loaded.", new object[0]));
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00008004 File Offset: 0x00006204
		private ResourceLog CreateResourceLog()
		{
			Server transportServer = Components.Configuration.LocalServer.TransportServer;
			bool enabled = transportServer.ResourceLogEnabled;
			string logDirectory = string.Empty;
			if (transportServer.ResourceLogPath == null || string.IsNullOrEmpty(transportServer.ResourceLogPath.PathName))
			{
				enabled = false;
			}
			else
			{
				logDirectory = transportServer.ResourceLogPath.PathName;
			}
			ResourceMeteringConfig resourceMeteringConfig = new ResourceMeteringConfig(8000, null);
			return new ResourceLog(enabled, "ResourceManager", logDirectory, transportServer.ResourceLogMaxAge, resourceMeteringConfig.ResourceLogFlushInterval, resourceMeteringConfig.ResourceLogBackgroundWriteInterval, (long)(transportServer.ResourceLogMaxDirectorySize.IsUnlimited ? 0UL : transportServer.ResourceLogMaxDirectorySize.Value.ToBytes()), (long)(transportServer.ResourceLogMaxFileSize.IsUnlimited ? 0UL : transportServer.ResourceLogMaxDirectorySize.Value.ToBytes()), resourceMeteringConfig.ResourceLogBufferSize);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000080ED File Offset: 0x000062ED
		string IDiagnosable.GetDiagnosticComponentName()
		{
			return "ResourceManager";
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000080F4 File Offset: 0x000062F4
		XElement IDiagnosable.GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			bool flag = parameters.Argument.IndexOf("verbose", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag2 = flag || parameters.Argument.IndexOf("basic", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag3 = flag2 || parameters.Argument.IndexOf("config", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag4 = !flag3 || parameters.Argument.IndexOf("help", StringComparison.OrdinalIgnoreCase) != -1;
			string diagnosticComponentName = ((IDiagnosable)this).GetDiagnosticComponentName();
			XElement xelement = new XElement(diagnosticComponentName);
			if (flag4)
			{
				xelement.Add(new XElement("help", "Supported arguments: config, basic, verbose, help."));
			}
			if (flag3)
			{
				this.ResourceManager.AddDiagnosticInfoTo(xelement, flag2, flag);
			}
			return xelement;
		}

		// Token: 0x040000F2 RID: 242
		private ResourceManager resourceManager;

		// Token: 0x040000F3 RID: 243
		private object syncLoad = new object();

		// Token: 0x040000F4 RID: 244
		private ResourceManagerResources resourcesToMonitor;
	}
}
