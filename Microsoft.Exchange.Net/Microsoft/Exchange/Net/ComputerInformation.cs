using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Exchange.Win32;
using Microsoft.Win32;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000BE0 RID: 3040
	internal static class ComputerInformation
	{
		// Token: 0x17001082 RID: 4226
		// (get) Token: 0x06004250 RID: 16976 RVA: 0x000B0BAE File Offset: 0x000AEDAE
		public static string NetbiosName
		{
			get
			{
				return ComputerInformation.GetComputerName(NativeMethods.ComputerNameFormat.NetBios);
			}
		}

		// Token: 0x17001083 RID: 4227
		// (get) Token: 0x06004251 RID: 16977 RVA: 0x000B0BB6 File Offset: 0x000AEDB6
		public static string DnsDomainName
		{
			get
			{
				return ComputerInformation.GetComputerName(NativeMethods.ComputerNameFormat.DnsDomain);
			}
		}

		// Token: 0x17001084 RID: 4228
		// (get) Token: 0x06004252 RID: 16978 RVA: 0x000B0BC0 File Offset: 0x000AEDC0
		public static string DnsFullyQualifiedDomainName
		{
			get
			{
				string result;
				if (ComputerInformation.runningOnNonCluster)
				{
					result = ComputerInformation.GetComputerName(NativeMethods.ComputerNameFormat.DnsFullyQualified);
				}
				else
				{
					result = (ComputerInformation.GetClusterDnsFullyQualifiedDomainName() ?? ComputerInformation.GetComputerName(NativeMethods.ComputerNameFormat.DnsFullyQualified));
				}
				return result;
			}
		}

		// Token: 0x17001085 RID: 4229
		// (get) Token: 0x06004253 RID: 16979 RVA: 0x000B0BF0 File Offset: 0x000AEDF0
		public static string DnsHostName
		{
			get
			{
				return ComputerInformation.GetComputerName(NativeMethods.ComputerNameFormat.DnsHostname);
			}
		}

		// Token: 0x17001086 RID: 4230
		// (get) Token: 0x06004254 RID: 16980 RVA: 0x000B0BF8 File Offset: 0x000AEDF8
		public static string NetbiosPhysicalName
		{
			get
			{
				return ComputerInformation.GetComputerName(NativeMethods.ComputerNameFormat.PhysicalNetBios);
			}
		}

		// Token: 0x17001087 RID: 4231
		// (get) Token: 0x06004255 RID: 16981 RVA: 0x000B0C00 File Offset: 0x000AEE00
		public static string DnsPhysicalDomainName
		{
			get
			{
				return ComputerInformation.GetComputerName(NativeMethods.ComputerNameFormat.PhysicalDnsDomain);
			}
		}

		// Token: 0x17001088 RID: 4232
		// (get) Token: 0x06004256 RID: 16982 RVA: 0x000B0C08 File Offset: 0x000AEE08
		public static string DnsPhysicalFullyQualifiedDomainName
		{
			get
			{
				return ComputerInformation.GetComputerName(NativeMethods.ComputerNameFormat.PhysicalDnsFullyQualified);
			}
		}

		// Token: 0x17001089 RID: 4233
		// (get) Token: 0x06004257 RID: 16983 RVA: 0x000B0C10 File Offset: 0x000AEE10
		public static string DnsPhysicalHostName
		{
			get
			{
				return ComputerInformation.GetComputerName(NativeMethods.ComputerNameFormat.PhysicalDnsHostname);
			}
		}

		// Token: 0x06004258 RID: 16984 RVA: 0x000B0C18 File Offset: 0x000AEE18
		public static List<IPAddress> GetLocalIPAddresses()
		{
			NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
			List<IPAddress> list = new List<IPAddress>(allNetworkInterfaces.Length);
			foreach (NetworkInterface networkInterface in allNetworkInterfaces)
			{
				IPInterfaceProperties ipproperties = networkInterface.GetIPProperties();
				UnicastIPAddressInformationCollection unicastAddresses = ipproperties.UnicastAddresses;
				foreach (IPAddressInformation ipaddressInformation in unicastAddresses)
				{
					if (!IPAddress.IsLoopback(ipaddressInformation.Address))
					{
						list.Add(ipaddressInformation.Address);
					}
				}
			}
			return list;
		}

		// Token: 0x06004259 RID: 16985 RVA: 0x000B0CB8 File Offset: 0x000AEEB8
		private static string GetComputerName(NativeMethods.ComputerNameFormat type)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			uint capacity = (uint)stringBuilder.Capacity;
			if (!NativeMethods.GetComputerNameEx(type, stringBuilder, ref capacity))
			{
				Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
			}
			return stringBuilder.ToString().Trim();
		}

		// Token: 0x0600425A RID: 16986 RVA: 0x000B0CF8 File Offset: 0x000AEEF8
		private static string GetClusterDnsFullyQualifiedDomainName()
		{
			string result;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Services\\WLBS\\Enum"))
			{
				if (registryKey == null)
				{
					ComputerInformation.runningOnNonCluster = true;
					result = null;
				}
				else
				{
					object value = registryKey.GetValue("Count");
					if (value == null || (int)value != 1)
					{
						ComputerInformation.runningOnNonCluster = true;
						result = null;
					}
					else
					{
						using (RegistryKey registryKey2 = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Services\\WLBS\\Parameters\\Interface"))
						{
							if (registryKey2 == null || registryKey2.SubKeyCount == 0)
							{
								ComputerInformation.runningOnNonCluster = true;
								result = null;
							}
							else
							{
								using (RegistryKey registryKey3 = registryKey2.OpenSubKey(registryKey2.GetSubKeyNames()[0]))
								{
									if (registryKey3 == null)
									{
										ComputerInformation.runningOnNonCluster = true;
										result = null;
									}
									else
									{
										string text = (string)registryKey3.GetValue("ClusterName");
										if (!string.IsNullOrEmpty(text))
										{
											text = text.Trim();
											if (!string.IsNullOrEmpty(text))
											{
												result = text;
											}
											else
											{
												result = null;
											}
										}
										else
										{
											result = null;
										}
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x040038D1 RID: 14545
		private const string DefaultNLBParentKeyName = "System\\CurrentControlSet\\Services\\WLBS\\Parameters\\Interface";

		// Token: 0x040038D2 RID: 14546
		private const string DefaultNLBAdaptorKeyName = "System\\CurrentControlSet\\Services\\WLBS\\Enum";

		// Token: 0x040038D3 RID: 14547
		private const string ClusterName = "ClusterName";

		// Token: 0x040038D4 RID: 14548
		private static bool runningOnNonCluster;
	}
}
