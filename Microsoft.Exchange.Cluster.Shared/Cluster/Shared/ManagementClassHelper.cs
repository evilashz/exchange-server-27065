using System;
using System.Management;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x02000010 RID: 16
	internal class ManagementClassHelper : IManagementClassHelper
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000030FC File Offset: 0x000012FC
		public DateTime LocalBootTime
		{
			get
			{
				if (this.localBootTime == null)
				{
					lock (this.objectForLock)
					{
						if (this.localBootTime == null)
						{
							TimeSpan timeSpan;
							this.localBootTime = new DateTime?(this.GetLocalBootTime(out timeSpan));
						}
					}
				}
				return this.localBootTime.Value;
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003170 File Offset: 0x00001370
		public DateTime GetLocalBootTime(out TimeSpan systemUptime)
		{
			long tickCount = NativeMethods.GetTickCount64();
			systemUptime = TimeSpan.FromMilliseconds((double)tickCount);
			return DateTime.UtcNow.Subtract(systemUptime);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000031A4 File Offset: 0x000013A4
		public DateTime GetBootTime(AmServerName machineName)
		{
			ManagementScope managementScope = this.GetManagementScope(machineName);
			ManagementPath path = new ManagementPath("Win32_OperatingSystem");
			ObjectGetOptions options = null;
			DateTime bootTimeWithWmi;
			using (ManagementClass managementClass = new ManagementClass(managementScope, path, options))
			{
				bootTimeWithWmi = this.GetBootTimeWithWmi(managementClass, machineName);
			}
			return bootTimeWithWmi;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000031F8 File Offset: 0x000013F8
		private DateTime GetBootTimeWithWmi(ManagementClass mgmtClass, AmServerName machineName)
		{
			DateTime dateTime = ExDateTime.Now.UniversalTime;
			Exception ex = null;
			try
			{
				using (ManagementObjectCollection instances = mgmtClass.GetInstances())
				{
					if (instances != null)
					{
						using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = instances.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								ManagementBaseObject managementBaseObject = enumerator.Current;
								ManagementObject managementObject = (ManagementObject)managementBaseObject;
								using (managementObject)
								{
									string dmtfDate = (string)managementObject["LastBootupTime"];
									dateTime = ManagementDateTimeConverter.ToDateTime(dmtfDate).ToUniversalTime();
									AmTrace.Debug("GetBootTimeWithWmi: WMI says that the boot time for {0} is {1}.", new object[]
									{
										machineName,
										dateTime
									});
								}
							}
							goto IL_102;
						}
					}
					AmTrace.Error("GetBootTimeWithWmi: WMI could not query the boot time on server {0}: No instances found for management path {1}.", new object[]
					{
						machineName,
						mgmtClass.ClassPath.Path
					});
					ReplayEventLogConstants.Tuple_GetBootTimeWithWmiFailure.LogEvent(string.Empty, new object[]
					{
						machineName,
						Strings.NoInstancesFoundForManagementPath(mgmtClass.ClassPath.Path)
					});
					IL_102:;
				}
			}
			catch (COMException ex2)
			{
				ex = ex2;
			}
			catch (UnauthorizedAccessException ex3)
			{
				ex = ex3;
			}
			catch (ManagementException ex4)
			{
				ex = ex4;
			}
			catch (OutOfMemoryException ex5)
			{
				ex = ex5;
			}
			if (ex != null)
			{
				AmTrace.Error("GetBootTimeWithWmi: WMI could not query the boot time on server {0}: {1}", new object[]
				{
					machineName,
					ex
				});
				ReplayEventLogConstants.Tuple_GetBootTimeWithWmiFailure.LogEvent(string.Empty, new object[]
				{
					machineName,
					ex.Message
				});
			}
			return dateTime;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000033D4 File Offset: 0x000015D4
		private ManagementScope GetManagementScope(AmServerName machineName)
		{
			ManagementPath path = new ManagementPath(string.Format("\\\\{0}\\root\\cimv2", machineName.Fqdn));
			AmServerName amServerName = new AmServerName(Environment.MachineName);
			ConnectionOptions connectionOptions = new ConnectionOptions();
			if (!amServerName.Equals(machineName))
			{
				connectionOptions.Authority = string.Format("Kerberos:host/{0}", machineName.Fqdn);
			}
			return new ManagementScope(path, connectionOptions);
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003430 File Offset: 0x00001630
		public string LocalComputerFqdn
		{
			get
			{
				if (this.localComputerFqdn == null)
				{
					lock (this.fqdnLock)
					{
						if (this.localComputerFqdn == null)
						{
							this.localComputerFqdn = NativeHelpers.GetLocalComputerFqdn(true);
						}
					}
				}
				return this.localComputerFqdn;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000079 RID: 121 RVA: 0x0000348C File Offset: 0x0000168C
		public string LocalDomainName
		{
			get
			{
				if (this.localDomainName == null)
				{
					lock (this.fqdnLock)
					{
						if (this.localDomainName == null)
						{
							this.localDomainName = NativeHelpers.GetDomainName();
						}
					}
				}
				return this.localDomainName;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000034E8 File Offset: 0x000016E8
		string IManagementClassHelper.LocalMachineName
		{
			get
			{
				if (this.localMachineName == null)
				{
					lock (this.objectForLock)
					{
						if (this.localMachineName == null)
						{
							this.localMachineName = Environment.MachineName;
						}
					}
				}
				return this.localMachineName;
			}
		}

		// Token: 0x04000019 RID: 25
		private string localComputerFqdn;

		// Token: 0x0400001A RID: 26
		private string localDomainName;

		// Token: 0x0400001B RID: 27
		private DateTime? localBootTime = null;

		// Token: 0x0400001C RID: 28
		private string localMachineName;

		// Token: 0x0400001D RID: 29
		private object objectForLock = new object();

		// Token: 0x0400001E RID: 30
		private object fqdnLock = new object();
	}
}
