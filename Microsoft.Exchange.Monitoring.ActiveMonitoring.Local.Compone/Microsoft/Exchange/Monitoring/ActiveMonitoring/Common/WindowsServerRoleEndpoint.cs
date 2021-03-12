using System;
using System.Management;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000551 RID: 1361
	internal class WindowsServerRoleEndpoint : IEndpoint
	{
		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x060021E1 RID: 8673 RVA: 0x000CD62F File Offset: 0x000CB82F
		public bool IsDirectoryServiceRoleInstalled
		{
			get
			{
				return this.isDirectoryServiceRoleInstalled;
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x060021E2 RID: 8674 RVA: 0x000CD637 File Offset: 0x000CB837
		public bool IsDhcpServerRoleInstalled
		{
			get
			{
				return this.isDhcpServerRoleInstalled;
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x060021E3 RID: 8675 RVA: 0x000CD63F File Offset: 0x000CB83F
		public bool IsDnsServerRoleInstalled
		{
			get
			{
				return this.isDnsServerRoleInstalled;
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x060021E4 RID: 8676 RVA: 0x000CD647 File Offset: 0x000CB847
		public bool IsNatServerRoleInstalled
		{
			get
			{
				return this.isNatServerRoleInstalled;
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x060021E5 RID: 8677 RVA: 0x000CD64F File Offset: 0x000CB84F
		public bool RestartOnChange
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x060021E6 RID: 8678 RVA: 0x000CD652 File Offset: 0x000CB852
		// (set) Token: 0x060021E7 RID: 8679 RVA: 0x000CD65A File Offset: 0x000CB85A
		public Exception Exception { get; set; }

		// Token: 0x060021E8 RID: 8680 RVA: 0x000CD664 File Offset: 0x000CB864
		public void Initialize()
		{
			WTFDiagnostics.TraceFunction(ExTraceGlobals.WindowsServerRoleEndpointTracer, this.traceContext, "Checking Windows server role configuration", null, "Initialize", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\WindowsServerRoleEndpoint.cs", 147);
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_ServerFeature WHERE ParentID = 0"))
			{
				using (ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get())
				{
					foreach (ManagementBaseObject managementBaseObject in managementObjectCollection)
					{
						ManagementObject managementObject = (ManagementObject)managementBaseObject;
						using (managementObject)
						{
							switch ((uint)managementObject["ID"])
							{
							case 10U:
								this.isDirectoryServiceRoleInstalled = true;
								break;
							case 12U:
								this.isDhcpServerRoleInstalled = true;
								break;
							case 13U:
								this.isDnsServerRoleInstalled = true;
								break;
							case 14U:
								this.isNatServerRoleInstalled = true;
								break;
							}
						}
					}
				}
			}
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x000CD78C File Offset: 0x000CB98C
		public bool DetectChange()
		{
			WTFDiagnostics.TraceFunction(ExTraceGlobals.WindowsServerRoleEndpointTracer, this.traceContext, "Detecting Windows server role configuration change", null, "DetectChange", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Discovery\\WindowsServerRoleEndpoint.cs", 189);
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_ServerFeature WHERE ParentID = 0"))
			{
				using (ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get())
				{
					foreach (ManagementBaseObject managementBaseObject in managementObjectCollection)
					{
						ManagementObject managementObject = (ManagementObject)managementBaseObject;
						switch ((uint)managementObject["ID"])
						{
						case 10U:
							if (!this.isDirectoryServiceRoleInstalled)
							{
								return true;
							}
							break;
						case 12U:
							if (!this.isDhcpServerRoleInstalled)
							{
								return true;
							}
							break;
						case 13U:
							if (!this.isDnsServerRoleInstalled)
							{
								return true;
							}
							break;
						case 14U:
							if (!this.isNatServerRoleInstalled)
							{
								return true;
							}
							break;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x040018A6 RID: 6310
		private const string WmiQueryString = "SELECT * FROM Win32_ServerFeature WHERE ParentID = 0";

		// Token: 0x040018A7 RID: 6311
		private const uint DirectoryServiceFeatureId = 10U;

		// Token: 0x040018A8 RID: 6312
		private const uint DhcpFeatureId = 12U;

		// Token: 0x040018A9 RID: 6313
		private const uint DnsFeatureId = 13U;

		// Token: 0x040018AA RID: 6314
		private const uint NatFeatureId = 14U;

		// Token: 0x040018AB RID: 6315
		private bool isDirectoryServiceRoleInstalled;

		// Token: 0x040018AC RID: 6316
		private bool isDhcpServerRoleInstalled;

		// Token: 0x040018AD RID: 6317
		private bool isDnsServerRoleInstalled;

		// Token: 0x040018AE RID: 6318
		private bool isNatServerRoleInstalled;

		// Token: 0x040018AF RID: 6319
		private TracingContext traceContext = TracingContext.Default;
	}
}
