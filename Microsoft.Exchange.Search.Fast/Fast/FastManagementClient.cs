using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using Microsoft.Ceres.Common.Utils.Net;
using Microsoft.Ceres.Common.WcfUtils;
using Microsoft.Ceres.CoreServices.Tools.Management.Client;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.EventLog;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Win32;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x02000008 RID: 8
	internal abstract class FastManagementClient : IDisposeTrackable, IDisposable
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00003420 File Offset: 0x00001620
		static FastManagementClient()
		{
			if (string.IsNullOrEmpty(FastManagementClient.fsisInstallPath) && !ExEnvironment.IsTestProcess)
			{
				throw new InvalidOperationException("Failure to detect Fast installation.");
			}
			Environment.SetEnvironmentVariable("CERES_REGISTRY_PRODUCT_NAME", "Search Foundation for Exchange", EnvironmentVariableTarget.Process);
			AppDomain.CurrentDomain.AssemblyResolve += FastManagementClient.OnAssemblyResolveEvent;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000034BA File Offset: 0x000016BA
		protected FastManagementClient()
		{
			this.diagnosticsSession = Microsoft.Exchange.Search.Core.Diagnostics.DiagnosticsSession.CreateComponentDiagnosticsSession("FastManagementClient", ComponentInstance.Globals.Search.ServiceName, ExTraceGlobals.IndexManagementTracer, (long)this.GetHashCode());
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004B RID: 75 RVA: 0x000034F4 File Offset: 0x000016F4
		protected static SearchConfig Config
		{
			get
			{
				if (FastManagementClient.config == null)
				{
					lock (FastManagementClient.lockObject)
					{
						if (FastManagementClient.config == null)
						{
							FlightingSearchConfig flightingSearchConfig = new FlightingSearchConfig();
							Thread.MemoryBarrier();
							FastManagementClient.config = flightingSearchConfig;
						}
					}
				}
				return FastManagementClient.config;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00003554 File Offset: 0x00001754
		protected static int FsisInstallBasePort
		{
			get
			{
				return FastManagementClient.fsisInstallBasePort;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600004D RID: 77 RVA: 0x0000355B File Offset: 0x0000175B
		protected IDiagnosticsSession DiagnosticsSession
		{
			get
			{
				return this.diagnosticsSession;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600004E RID: 78
		protected abstract int ManagementPortOffset { get; }

		// Token: 0x0600004F RID: 79
		public abstract DisposeTracker GetDisposeTracker();

		// Token: 0x06000050 RID: 80 RVA: 0x00003563 File Offset: 0x00001763
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003572 File Offset: 0x00001772
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06000052 RID: 82
		protected abstract void InternalConnectManagementAgents(WcfManagementClient client);

		// Token: 0x06000053 RID: 83 RVA: 0x00003587 File Offset: 0x00001787
		protected virtual void Dispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
					this.disposeTracker = null;
				}
				if (this.client != null)
				{
					this.client.Dispose();
					this.client = null;
				}
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000035D8 File Offset: 0x000017D8
		protected void PerformFastOperation(Action function, string eventLogKey)
		{
			this.PerformFastOperation<object>(delegate()
			{
				function();
				return null;
			}, eventLogKey);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003608 File Offset: 0x00001808
		protected virtual T PerformFastOperation<T>(Func<T> function, string eventLogKey)
		{
			int num = 0;
			Exception ex;
			for (;;)
			{
				ex = null;
				try
				{
					if (this.client == null)
					{
						this.ConnectManagementAgents();
					}
					return function();
				}
				catch (Exception ex2)
				{
					if (Util.ShouldRethrowException(ex2))
					{
						throw;
					}
					ex = ex2;
				}
				num++;
				if (num >= 3)
				{
					break;
				}
				Thread.Sleep(FastManagementClient.OperationRetryWait);
				this.LogExceptionAndReconnectManagementAgents(ex, eventLogKey);
			}
			throw new PerformingFastOperationException(ex);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003674 File Offset: 0x00001874
		protected void ConnectManagementAgents()
		{
			this.ConnectManagementAgents("localhost");
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003684 File Offset: 0x00001884
		protected void ConnectManagementAgents(string serverName)
		{
			int num = 0;
			for (;;)
			{
				try
				{
					this.CreateWcfClient(serverName);
					this.InternalConnectManagementAgents(this.client);
					this.diagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, "Successfully connected the Management Agents.", new object[0]);
					break;
				}
				catch (Exception ex)
				{
					if (Util.ShouldRethrowException(ex))
					{
						throw;
					}
					num++;
					if (num >= 3)
					{
						throw new PerformingFastOperationException(ex);
					}
					this.diagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Warnings, "Retry connecting management agents due to error:  {0}", new object[]
					{
						ex
					});
				}
				Thread.Sleep(FastManagementClient.ConnectionRetryWait);
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003714 File Offset: 0x00001914
		protected SystemClient ConnectSystem()
		{
			Uri uri = new Uri(string.Format("net.tcp://{0}:{1}/Management", "localhost", FastManagementClient.FsisInstallBasePort + this.ManagementPortOffset));
			Binding binding = ClientFactory.CreateBinding(uri, true, true);
			SystemClient systemClient = new SystemClient(binding, EndpointIdentity.CreateUpnIdentity("*"), null)
			{
				SystemManagerLocations = new List<Uri>
				{
					uri
				}
			};
			try
			{
				if (!systemClient.Connect())
				{
					throw systemClient.ConnectionException;
				}
			}
			catch
			{
				systemClient.Dispose();
				throw;
			}
			return systemClient;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000037A8 File Offset: 0x000019A8
		protected TManagementAgent GetManagementAgent<TManagementAgent>(string agentName)
		{
			return this.client.GetManagementAgent<TManagementAgent>(agentName);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000037B8 File Offset: 0x000019B8
		private static string GetFastInstallPath()
		{
			string value = RegistryReader.Instance.GetValue<string>(Registry.LocalMachine, "SOFTWARE\\Microsoft\\Search Foundation for Exchange", "InstallationPath", string.Empty);
			if (string.IsNullOrEmpty(value))
			{
				return null;
			}
			return new DirectoryInfo(value).FullName;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000037FC File Offset: 0x000019FC
		private static Assembly OnAssemblyResolveEvent(object sender, ResolveEventArgs args)
		{
			string text = args.Name.Split(new char[]
			{
				','
			})[0];
			string text2 = Path.Combine(FastManagementClient.fsisInstallPath, "Installer\\Bin");
			string text3 = Path.Combine(FastManagementClient.fsisInstallPath, "HostController");
			foreach (string text4 in new string[]
			{
				text2,
				text3
			})
			{
				string text5 = string.Concat(new object[]
				{
					text4,
					Path.DirectorySeparatorChar,
					text,
					".dll"
				});
				if (File.Exists(text5))
				{
					return Assembly.LoadFrom(text5);
				}
			}
			return null;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000038C0 File Offset: 0x00001AC0
		private void CreateWcfClient(string serverName)
		{
			if (this.client != null)
			{
				this.client.Dispose();
				this.client = null;
			}
			string uriString = string.Format("net.tcp://{0}:{1}/Management", serverName, FastManagementClient.fsisInstallBasePort + this.ManagementPortOffset);
			ClientConnectionSettings clientConnectionSettings = ClientUtils.CreateConnectionSettings(new Uri(uriString), true, "*", 1);
			this.client = new WcfManagementClient(clientConnectionSettings);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003924 File Offset: 0x00001B24
		private void LogExceptionAndReconnectManagementAgents(Exception ex, string eventLogKey)
		{
			this.diagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Warnings, "Retry due to error: {0}", new object[]
			{
				ex
			});
			PerformingFastOperationException ex2 = new PerformingFastOperationException(ex);
			this.diagnosticsSession.LogPeriodicEvent(MSExchangeFastSearchEventLogConstants.Tuple_FASTConnectionIssue, eventLogKey, new object[]
			{
				ex2
			});
			this.ConnectManagementAgents();
		}

		// Token: 0x04000021 RID: 33
		private const int MaxRetries = 3;

		// Token: 0x04000022 RID: 34
		private const string ProductNameEnvironmentVariable = "CERES_REGISTRY_PRODUCT_NAME";

		// Token: 0x04000023 RID: 35
		private const string ProductName = "Search Foundation for Exchange";

		// Token: 0x04000024 RID: 36
		private const string FsisProductKeyPath = "SOFTWARE\\Microsoft\\Search Foundation for Exchange";

		// Token: 0x04000025 RID: 37
		private const string FsisProductInstallPathParameterName = "InstallationPath";

		// Token: 0x04000026 RID: 38
		private const string FsisProductInstallBasePortParameterName = "BasePort";

		// Token: 0x04000027 RID: 39
		private static readonly TimeSpan OperationRetryWait = TimeSpan.FromSeconds(1.0);

		// Token: 0x04000028 RID: 40
		private static readonly object lockObject = new object();

		// Token: 0x04000029 RID: 41
		private static readonly TimeSpan ConnectionRetryWait = TimeSpan.FromSeconds(10.0);

		// Token: 0x0400002A RID: 42
		private static readonly string fsisInstallPath = FastManagementClient.GetFastInstallPath();

		// Token: 0x0400002B RID: 43
		private static readonly int fsisInstallBasePort = FastManagementClient.Config.BasePort;

		// Token: 0x0400002C RID: 44
		private static SearchConfig config;

		// Token: 0x0400002D RID: 45
		private readonly IDiagnosticsSession diagnosticsSession;

		// Token: 0x0400002E RID: 46
		private WcfManagementClient client;

		// Token: 0x0400002F RID: 47
		private DisposeTracker disposeTracker;
	}
}
