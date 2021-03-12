using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.ProvisioningAgent;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.EventMessages;
using Microsoft.Exchange.ProvisioningAgent;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.DefaultProvisioningAgent.Rus
{
	// Token: 0x0200004C RID: 76
	internal class ProxyDLL : IDisposable
	{
		// Token: 0x060001F1 RID: 497
		[DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr GetProcAddress([In] SafeLibraryHandle libHandle, [MarshalAs(UnmanagedType.LPStr)] [In] string lpProcName);

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000CC70 File Offset: 0x0000AE70
		public bool IsInitialized
		{
			get
			{
				return this.isInitialized;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000CC78 File Offset: 0x0000AE78
		public string AddrType
		{
			get
			{
				return this.addrType;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000CC80 File Offset: 0x0000AE80
		public string ProxyDLLPath
		{
			get
			{
				return this.proxyDLLPath;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000CC88 File Offset: 0x0000AE88
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000CC90 File Offset: 0x0000AE90
		public Version ProxyDLLVersion
		{
			get
			{
				return this.proxyDLLVersion;
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000CC98 File Offset: 0x0000AE98
		public ProxyDLL(ITopologyConfigurationSession cfgSession, string addrType, string proxyDLLPath, Version proxyDLLVersion, string serverName)
		{
			this.isInitialized = false;
			this.configurationSession = cfgSession;
			this.addrType = addrType;
			this.proxyDLLPath = proxyDLLPath;
			this.proxyDLLVersion = proxyDLLVersion;
			this.serverName = serverName;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000CCCC File Offset: 0x0000AECC
		~ProxyDLL()
		{
			this.Dispose(false);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000CCFC File Offset: 0x0000AEFC
		protected void Dispose(bool disposing)
		{
			if (disposing && this.proxyDLLModule != null && !this.proxyDLLModule.IsInvalid)
			{
				this.proxyDLLModule.Dispose();
				this.proxyDLLModule = null;
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000CD28 File Offset: 0x0000AF28
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000CD38 File Offset: 0x0000AF38
		public void ScInitialize()
		{
			lock (this)
			{
				if (!this.IsInitialized)
				{
					if (VariantConfiguration.InvariantNoFlightingSnapshot.CmdletInfra.ProxyDllUpdate.Enabled)
					{
						try
						{
							this.UpdateDLL();
							goto IL_9E;
						}
						catch (RusException ex)
						{
							ExTraceGlobals.RusTracer.TraceDebug<string, string>((long)this.GetHashCode(), "An error happened while update dll '{0}' on local machine, error message '{1}'", this.ProxyDLLPath, ex.Message);
							ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_UpdateProxyGenerationDllFailed, new string[]
							{
								this.ProxyDLLPath,
								ex.Message
							});
							goto IL_9E;
						}
					}
					ExTraceGlobals.RusTracer.TraceDebug((long)this.GetHashCode(), "Update DLL is skipped b/c Flighting.ProxyDllUpdate is false.");
					IL_9E:
					this.proxyDLLModule = NativeMethods.LoadLibrary(this.proxyDLLPath);
					if (this.proxyDLLModule.IsInvalid)
					{
						ExTraceGlobals.RusTracer.TraceDebug((long)this.GetHashCode(), "proxyDllModule.IsInvalid = true.");
						int lastWin32Error = Marshal.GetLastWin32Error();
						throw new RusException(Strings.ErrorProxyLoadDll(this.proxyDLLPath, this.AddrType, new Win32Exception(lastWin32Error).Message));
					}
					try
					{
						string text = "RcInitProxies";
						IntPtr procAddress = ProxyDLL.GetProcAddress(this.proxyDLLModule, text);
						if (procAddress == IntPtr.Zero)
						{
							throw new RusException(Strings.ErrorProxyGenErrorEntryPoint(this.proxyDLLPath, this.AddrType, text));
						}
						this.RcInitProxies = (ProxyDLL.RcInitProxiesDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(ProxyDLL.RcInitProxiesDelegate));
						text = "RcGenerateProxy";
						IntPtr procAddress2 = ProxyDLL.GetProcAddress(this.proxyDLLModule, text);
						if (procAddress2 == IntPtr.Zero)
						{
							throw new RusException(Strings.ErrorProxyGenErrorEntryPoint(this.proxyDLLPath, this.AddrType, text));
						}
						this.RcGenerateProxy = (ProxyDLL.RcGenerateProxyDelegate)Marshal.GetDelegateForFunctionPointer(procAddress2, typeof(ProxyDLL.RcGenerateProxyDelegate));
						text = "RcCheckProxy";
						IntPtr procAddress3 = ProxyDLL.GetProcAddress(this.proxyDLLModule, text);
						if (procAddress3 != IntPtr.Zero)
						{
							this.RcCheckProxy = (ProxyDLL.RcCheckProxyDelegate)Marshal.GetDelegateForFunctionPointer(procAddress3, typeof(ProxyDLL.RcCheckProxyDelegate));
						}
						text = "RcValidateProxy";
						IntPtr procAddress4 = ProxyDLL.GetProcAddress(this.proxyDLLModule, text);
						if (procAddress4 == IntPtr.Zero)
						{
							throw new RusException(Strings.ErrorProxyGenErrorEntryPoint(this.proxyDLLPath, this.AddrType, text));
						}
						this.RcValidateProxy = (ProxyDLL.RcValidateProxyDelegate)Marshal.GetDelegateForFunctionPointer(procAddress4, typeof(ProxyDLL.RcValidateProxyDelegate));
						text = "RcValidateSiteProxy";
						IntPtr procAddress5 = ProxyDLL.GetProcAddress(this.proxyDLLModule, text);
						if (procAddress5 == IntPtr.Zero)
						{
							throw new RusException(Strings.ErrorProxyGenErrorEntryPoint(this.proxyDLLPath, this.AddrType, text));
						}
						this.RcValidateSiteProxy = (ProxyDLL.RcValidateSiteProxyDelegate)Marshal.GetDelegateForFunctionPointer(procAddress5, typeof(ProxyDLL.RcValidateSiteProxyDelegate));
						text = "RcUpdateProxy";
						IntPtr procAddress6 = ProxyDLL.GetProcAddress(this.proxyDLLModule, text);
						if (procAddress6 == IntPtr.Zero)
						{
							throw new RusException(Strings.ErrorProxyGenErrorEntryPoint(this.proxyDLLPath, this.AddrType, text));
						}
						this.RcUpdateProxy = (ProxyDLL.RcUpdateProxyDelegate)Marshal.GetDelegateForFunctionPointer(procAddress6, typeof(ProxyDLL.RcUpdateProxyDelegate));
						text = "FreeProxy";
						IntPtr procAddress7 = ProxyDLL.GetProcAddress(this.proxyDLLModule, text);
						if (procAddress7 == IntPtr.Zero)
						{
							throw new RusException(Strings.ErrorProxyGenErrorEntryPoint(this.proxyDLLPath, this.AddrType, text));
						}
						this.FreeProxy = (ProxyDLL.FreeProxyDelegate)Marshal.GetDelegateForFunctionPointer(procAddress7, typeof(ProxyDLL.FreeProxyDelegate));
						text = "CloseProxies";
						IntPtr procAddress8 = ProxyDLL.GetProcAddress(this.proxyDLLModule, text);
						if (procAddress8 == IntPtr.Zero)
						{
							throw new RusException(Strings.ErrorProxyGenErrorEntryPoint(this.proxyDLLPath, this.AddrType, text));
						}
						this.CloseProxies = (ProxyDLL.CloseProxiesDelegate)Marshal.GetDelegateForFunctionPointer(procAddress8, typeof(ProxyDLL.CloseProxiesDelegate));
						this.isInitialized = true;
					}
					finally
					{
						if (!this.isInitialized && this.proxyDLLModule != null && !this.proxyDLLModule.IsInvalid)
						{
							this.proxyDLLModule.Dispose();
							this.proxyDLLModule = null;
						}
					}
				}
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000D168 File Offset: 0x0000B368
		private void UpdateDLL()
		{
			if (File.Exists(this.ProxyDLLPath))
			{
				FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(this.ProxyDLLPath);
				if (versionInfo.FileVersion == null || new Version(versionInfo.FileVersion) >= this.ProxyDLLVersion)
				{
					ExTraceGlobals.RusTracer.TraceDebug<string, Version>((long)this.GetHashCode(), "info.FileVersion = {0}, this.ProxyDllVersion = {1}.", versionInfo.FileVersion, this.proxyDLLVersion);
					return;
				}
			}
			QueryFilter[] array = new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ServerSchema.VersionNumber, Server.E2007MinVersion),
				new BitMaskOrFilter(ServerSchema.CurrentServerRole, 2UL)
			};
			bool flag = false;
			ADSite localSite = this.configurationSession.GetLocalSite();
			if (localSite != null)
			{
				QueryFilter queryFilter = new AndFilter(new QueryFilter[]
				{
					array[0],
					array[1],
					new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.ServerSite, localSite.Id)
				});
				ExTraceGlobals.RusTracer.TraceDebug<string, QueryFilter>((long)this.GetHashCode(), "Update from local site {0}, Filter = {1}.", localSite.AdminDisplayName, queryFilter);
				Server[] servers = this.configurationSession.Find<Server>(null, QueryScope.SubTree, queryFilter, null, 0);
				flag = this.UpdateDLL(servers);
			}
			if (!flag)
			{
				QueryFilter queryFilter2;
				if (localSite != null)
				{
					queryFilter2 = new AndFilter(new QueryFilter[]
					{
						array[0],
						array[1],
						new ComparisonFilter(ComparisonOperator.NotEqual, ServerSchema.ServerSite, localSite.Id)
					});
				}
				else
				{
					queryFilter2 = new AndFilter(array);
				}
				ExTraceGlobals.RusTracer.TraceDebug<QueryFilter>((long)this.GetHashCode(), "Find in all the org, Filter = {0}.", queryFilter2);
				Server[] servers2 = this.configurationSession.Find<Server>(null, QueryScope.SubTree, queryFilter2, null, 0);
				flag = this.UpdateDLL(servers2);
			}
			if (!flag)
			{
				throw new RusException(Strings.ErrorProxyDllNotFound(this.proxyDLLPath.Substring(this.proxyDLLPath.LastIndexOf('\\') + 1)));
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000D320 File Offset: 0x0000B520
		private bool UpdateDLL(Server[] servers)
		{
			if (servers == null)
			{
				return false;
			}
			bool result = false;
			string cpuType = ProxySession.GetCpuType();
			string text = Path.Combine("address", this.AddrType);
			text = Path.Combine(text, cpuType);
			text = Path.Combine(text, this.proxyDLLPath.Substring(this.proxyDLLPath.LastIndexOf('\\') + 1));
			ExTraceGlobals.RusTracer.TraceDebug<int>((long)this.GetHashCode(), "Servers.Length = {0}.", servers.Length);
			foreach (Server server in servers)
			{
				string text2 = string.Format("\\\\{0}\\{1}", server.Name, text);
				ExTraceGlobals.RusTracer.TraceDebug<string>((long)this.GetHashCode(), "Searching path {0}.", text2);
				if (File.Exists(text2))
				{
					FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(text2);
					if (versionInfo.FileVersion != null && new Version(versionInfo.FileVersion).Equals(this.ProxyDLLVersion))
					{
						ExTraceGlobals.RusTracer.TraceDebug<string, string>((long)this.GetHashCode(), "The proxy generation dll '{0}' on local machine doesn't exist or have the right version, copying from '{1}'", this.ProxyDLLPath, text2);
						Exception ex = null;
						try
						{
							File.Copy(text2, this.proxyDLLPath, true);
							result = true;
							break;
						}
						catch (UnauthorizedAccessException ex2)
						{
							ex = ex2;
						}
						catch (DirectoryNotFoundException ex3)
						{
							ex = ex3;
						}
						catch (IOException ex4)
						{
							ex = ex4;
						}
						if (ex != null)
						{
							ExTraceGlobals.RusTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "An error happened while Copy file from '{0}' to '{1}', error message '{2}'", text2, this.proxyDLLPath, ex.Message);
							ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_CopyProxyGeneratinDllFailed, new string[]
							{
								text2,
								this.ProxyDLLPath,
								ex.Message
							});
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0400010E RID: 270
		private SafeLibraryHandle proxyDLLModule;

		// Token: 0x0400010F RID: 271
		private bool isInitialized;

		// Token: 0x04000110 RID: 272
		private ITopologyConfigurationSession configurationSession;

		// Token: 0x04000111 RID: 273
		private Version proxyDLLVersion;

		// Token: 0x04000112 RID: 274
		private string addrType;

		// Token: 0x04000113 RID: 275
		private string proxyDLLPath;

		// Token: 0x04000114 RID: 276
		private string serverName;

		// Token: 0x04000115 RID: 277
		public ProxyDLL.RcInitProxiesDelegate RcInitProxies;

		// Token: 0x04000116 RID: 278
		public ProxyDLL.CloseProxiesDelegate CloseProxies;

		// Token: 0x04000117 RID: 279
		public ProxyDLL.RcGenerateProxyDelegate RcGenerateProxy;

		// Token: 0x04000118 RID: 280
		public ProxyDLL.FreeProxyDelegate FreeProxy;

		// Token: 0x04000119 RID: 281
		public ProxyDLL.RcValidateProxyDelegate RcValidateProxy;

		// Token: 0x0400011A RID: 282
		public ProxyDLL.RcUpdateProxyDelegate RcUpdateProxy;

		// Token: 0x0400011B RID: 283
		public ProxyDLL.RcValidateSiteProxyDelegate RcValidateSiteProxy;

		// Token: 0x0400011C RID: 284
		public ProxyDLL.RcCheckProxyDelegate RcCheckProxy;

		// Token: 0x0200004D RID: 77
		// (Invoke) Token: 0x060001FF RID: 511
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal delegate ReturnCode RcInitProxiesDelegate([In] string pstrSiteProxy, [In] string pszServer, out IntPtr phProxySession);

		// Token: 0x0200004E RID: 78
		// (Invoke) Token: 0x06000203 RID: 515
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void CloseProxiesDelegate([In] IntPtr hProxySession);

		// Token: 0x0200004F RID: 79
		// (Invoke) Token: 0x06000207 RID: 519
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal delegate ReturnCode RcGenerateProxyDelegate([In] IntPtr hProxySession, [In] ref RecipientInfo pRecipientInfo, [In] int nRetries, out IntPtr ppwstrProxyAddr);

		// Token: 0x02000050 RID: 80
		// (Invoke) Token: 0x0600020B RID: 523
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal delegate void FreeProxyDelegate([In] IntPtr pszProxy);

		// Token: 0x02000051 RID: 81
		// (Invoke) Token: 0x0600020F RID: 527
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal delegate ReturnCode RcCheckProxyDelegate([In] IntPtr hProxySession, [In] ref RecipientInfo pRecipientInfo, [In] string pwszOldProxy, out bool pfValid);

		// Token: 0x02000052 RID: 82
		// (Invoke) Token: 0x06000213 RID: 531
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal delegate ReturnCode RcValidateSiteProxyDelegate([In] IntPtr hProxySession, StringBuilder pwstrSiteProxy, out bool pfValid);

		// Token: 0x02000053 RID: 83
		// (Invoke) Token: 0x06000217 RID: 535
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal delegate ReturnCode RcValidateProxyDelegate([In] IntPtr hProxySession, StringBuilder pwstrProxyAddr, out bool pfValid);

		// Token: 0x02000054 RID: 84
		// (Invoke) Token: 0x0600021B RID: 539
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
		internal delegate ReturnCode RcUpdateProxyDelegate([In] IntPtr hProxySession, [In] ref RecipientInfo pRecipientInfo, [In] string pwstrOldSiteProxy, StringBuilder pwstrNewSiteProxy, StringBuilder pwstrProxy, out bool pfNoDomainMatch);
	}
}
