using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000C06 RID: 3078
	internal class DnsServerList : IDisposable
	{
		// Token: 0x170010DD RID: 4317
		// (get) Token: 0x06004366 RID: 17254 RVA: 0x000B5141 File Offset: 0x000B3341
		public int Count
		{
			get
			{
				if (this.addresses != null)
				{
					return this.addresses.Count;
				}
				return 0;
			}
		}

		// Token: 0x170010DE RID: 4318
		// (get) Token: 0x06004367 RID: 17255 RVA: 0x000B5158 File Offset: 0x000B3358
		public IList<IPAddress> Addresses
		{
			get
			{
				return this.addresses;
			}
		}

		// Token: 0x170010DF RID: 4319
		// (get) Token: 0x06004368 RID: 17256 RVA: 0x000B5160 File Offset: 0x000B3360
		public DnsCache Cache
		{
			get
			{
				return this.cache;
			}
		}

		// Token: 0x06004369 RID: 17257 RVA: 0x000B5168 File Offset: 0x000B3368
		public static IPAddress[] GetAdapterDnsServerList(Guid adapterGuid, bool excludeServersFromLoopbackAdapters, bool excludeIPv6SiteLocalAddresses)
		{
			IPAddress[] result;
			try
			{
				result = DnsServerList.GetMachineDnsServerList(adapterGuid, 100, excludeServersFromLoopbackAdapters, excludeIPv6SiteLocalAddresses);
			}
			catch (NetworkInformationException ex)
			{
				Dns.EventLogger.LogEvent(CommonEventLogConstants.Tuple_DnsServerConfigurationFetchFailed, null, new object[]
				{
					ex
				});
				DnsLog.Log(adapterGuid, "Getting DNS server information failed. Exception {0}", new object[]
				{
					ex
				});
				result = null;
			}
			return result;
		}

		// Token: 0x0600436A RID: 17258 RVA: 0x000B51D4 File Offset: 0x000B33D4
		public static IPAddress[] GetMachineDnsServerList()
		{
			return DnsServerList.GetMachineDnsServerList(Guid.Empty, int.MaxValue, false, false);
		}

		// Token: 0x0600436B RID: 17259 RVA: 0x000B51E8 File Offset: 0x000B33E8
		private static IPAddress[] GetMachineDnsServerList(Guid adapterGuid, int maxTargets, bool excludeServersFromLoopbackAdapters, bool excludeIPv6SiteLocalAddresses)
		{
			HashSet<IPAddress> hashSet = new HashSet<IPAddress>();
			string text = (Guid.Empty == adapterGuid) ? null : adapterGuid.ToString("B").ToUpperInvariant();
			NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
			foreach (NetworkInterface networkInterface in allNetworkInterfaces)
			{
				if ((!(adapterGuid != Guid.Empty) || text.Equals(networkInterface.Id, StringComparison.OrdinalIgnoreCase)) && (!excludeServersFromLoopbackAdapters || networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback))
				{
					IPInterfaceProperties ipproperties = networkInterface.GetIPProperties();
					bool flag = ipproperties.IsDnsEnabled || ipproperties.IsDynamicDnsEnabled;
					if (flag)
					{
						foreach (IPAddress ipaddress in ipproperties.DnsAddresses)
						{
							if (hashSet.Count >= maxTargets)
							{
								return hashSet.ToArray<IPAddress>();
							}
							if (!excludeIPv6SiteLocalAddresses || !ipaddress.IsIPv6SiteLocal)
							{
								hashSet.Add(ipaddress);
							}
						}
					}
				}
			}
			return hashSet.ToArray<IPAddress>();
		}

		// Token: 0x0600436C RID: 17260 RVA: 0x000B5304 File Offset: 0x000B3504
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600436D RID: 17261 RVA: 0x000B5313 File Offset: 0x000B3513
		public bool Initialize(IPAddress[] addresses)
		{
			return this.Initialize(addresses, null);
		}

		// Token: 0x0600436E RID: 17262 RVA: 0x000B531D File Offset: 0x000B351D
		public bool Initialize(IPAddress[] addresses, string hostsFileName)
		{
			this.addresses = ((addresses == null) ? Array.AsReadOnly<IPAddress>(new IPAddress[0]) : Array.AsReadOnly<IPAddress>(addresses));
			this.cache = (string.IsNullOrEmpty(hostsFileName) ? DnsCache.CreateFromSystem() : DnsCache.CreateFromFile(hostsFileName));
			return true;
		}

		// Token: 0x0600436F RID: 17263 RVA: 0x000B5357 File Offset: 0x000B3557
		public void FlushCache()
		{
			if (this.cache != null)
			{
				this.cache.FlushCache();
			}
		}

		// Token: 0x06004370 RID: 17264 RVA: 0x000B536C File Offset: 0x000B356C
		public bool IsAddressListIdentical(IPAddress[] addresses)
		{
			ReadOnlyCollection<IPAddress> readOnlyCollection = this.addresses;
			if (readOnlyCollection == null && addresses == null)
			{
				return true;
			}
			if (readOnlyCollection == null || addresses == null)
			{
				return false;
			}
			if (readOnlyCollection.Count != addresses.Length)
			{
				return false;
			}
			for (int i = 0; i < addresses.Length; i++)
			{
				if (!addresses[i].Equals(readOnlyCollection[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004371 RID: 17265 RVA: 0x000B53C0 File Offset: 0x000B35C0
		public override string ToString()
		{
			IList<IPAddress> list = this.addresses;
			if (list.Count == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(list.Count * 40);
			foreach (IPAddress ipaddress in list)
			{
				if (stringBuilder.Length != 0)
				{
					stringBuilder.Append(';');
				}
				stringBuilder.Append(ipaddress.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004372 RID: 17266 RVA: 0x000B544C File Offset: 0x000B364C
		protected void Dispose(bool disposing)
		{
			if (disposing && this.cache != null)
			{
				this.cache.Close();
			}
		}

		// Token: 0x04003961 RID: 14689
		private DnsCache cache;

		// Token: 0x04003962 RID: 14690
		private ReadOnlyCollection<IPAddress> addresses;
	}
}
