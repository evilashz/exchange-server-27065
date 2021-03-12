using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004BA RID: 1210
	internal class SmtpInConnectorMap<TData> where TData : class
	{
		// Token: 0x060036A9 RID: 13993 RVA: 0x000E06A0 File Offset: 0x000DE8A0
		public void AddEntry(IPBinding[] bindings, IPRange[] ranges, TData data)
		{
			if (bindings == null || ranges == null)
			{
				return;
			}
			foreach (IPBinding ipbinding in bindings)
			{
				if (ipbinding.Address.AddressFamily != AddressFamily.InterNetwork && ipbinding.Address.AddressFamily != AddressFamily.InterNetworkV6)
				{
					throw new ArgumentException("Unsupported address type or family");
				}
				IPRangeRemote[] array = Util.FilterIpRangesByAddressFamily(ranges, ipbinding.Address.AddressFamily);
				if (array.Length != 0)
				{
					Dictionary<int, SmtpInConnectorMap<TData>.PortEntry<TData>> dictionary;
					if (this.localIPTable.TryGetValue(ipbinding.Address, out dictionary))
					{
						SmtpInConnectorMap<TData>.PortEntry<TData> portEntry;
						if (dictionary.TryGetValue(ipbinding.Port, out portEntry))
						{
							portEntry.Add(array, data);
						}
						else
						{
							dictionary.Add(ipbinding.Port, new SmtpInConnectorMap<TData>.PortEntry<TData>(ipbinding.Port, array, data));
						}
					}
					else
					{
						dictionary = new Dictionary<int, SmtpInConnectorMap<TData>.PortEntry<TData>>
						{
							{
								ipbinding.Port,
								new SmtpInConnectorMap<TData>.PortEntry<TData>(ipbinding.Port, array, data)
							}
						};
						this.localIPTable.Add(ipbinding.Address, dictionary);
					}
				}
			}
		}

		// Token: 0x060036AA RID: 13994 RVA: 0x000E0798 File Offset: 0x000DE998
		public TData Lookup(IPAddress localIp, int localPort, IPAddress remoteIpAddress)
		{
			IPRangeRemote v = null;
			TData tdata = default(TData);
			IPvxAddress remoteIP = new IPvxAddress(remoteIpAddress);
			Dictionary<int, SmtpInConnectorMap<TData>.PortEntry<TData>> dictionary;
			SmtpInConnectorMap<TData>.PortEntry<TData> portEntry;
			if (this.localIPTable.TryGetValue(localIp, out dictionary) && dictionary.TryGetValue(localPort, out portEntry))
			{
				tdata = portEntry.Lookup(remoteIP, out v);
			}
			if (localIp.AddressFamily == AddressFamily.InterNetwork)
			{
				this.localIPTable.TryGetValue(IPAddress.Any, out dictionary);
			}
			else
			{
				if (localIp.AddressFamily != AddressFamily.InterNetworkV6)
				{
					return default(TData);
				}
				this.localIPTable.TryGetValue(IPAddress.IPv6Any, out dictionary);
			}
			if (dictionary == null)
			{
				return tdata;
			}
			if (!dictionary.TryGetValue(localPort, out portEntry))
			{
				return tdata;
			}
			if (tdata == null)
			{
				return portEntry.Lookup(remoteIP);
			}
			IPRangeRemote v2;
			TData tdata2 = portEntry.Lookup(remoteIP, out v2);
			if (tdata2 == null || IPRangeRemote.Compare(v, v2) <= 0)
			{
				return tdata;
			}
			return tdata2;
		}

		// Token: 0x04001BE3 RID: 7139
		private readonly Dictionary<IPAddress, Dictionary<int, SmtpInConnectorMap<TData>.PortEntry<TData>>> localIPTable = new Dictionary<IPAddress, Dictionary<int, SmtpInConnectorMap<TData>.PortEntry<TData>>>();

		// Token: 0x020004BB RID: 1211
		internal class PortEntry<TPortData> where TPortData : class
		{
			// Token: 0x060036AC RID: 13996 RVA: 0x000E0880 File Offset: 0x000DEA80
			public PortEntry(int port, IPRangeRemote[] ranges, TPortData data)
			{
				this.Port = port;
				SmtpInConnectorMap<TData>.RangesEntry<TPortData> item = new SmtpInConnectorMap<TData>.RangesEntry<TPortData>(ranges, data);
				this.RangeEntries.Add(item);
			}

			// Token: 0x060036AD RID: 13997 RVA: 0x000E08BC File Offset: 0x000DEABC
			public void Add(IPRangeRemote[] ranges, TPortData data)
			{
				SmtpInConnectorMap<TData>.RangesEntry<TPortData> item = new SmtpInConnectorMap<TData>.RangesEntry<TPortData>(ranges, data);
				this.RangeEntries.Add(item);
			}

			// Token: 0x060036AE RID: 13998 RVA: 0x000E08E0 File Offset: 0x000DEAE0
			public TPortData Lookup(IPvxAddress remoteIP)
			{
				IPRangeRemote iprangeRemote = null;
				TPortData result = default(TPortData);
				foreach (SmtpInConnectorMap<TData>.RangesEntry<TPortData> rangesEntry in this.RangeEntries)
				{
					IPRangeRemote iprangeRemote2 = rangesEntry.Match(remoteIP);
					if (iprangeRemote2 != null && (iprangeRemote == null || IPRangeRemote.Compare(iprangeRemote2, iprangeRemote) < 0))
					{
						iprangeRemote = iprangeRemote2;
						result = rangesEntry.Data;
					}
				}
				return result;
			}

			// Token: 0x060036AF RID: 13999 RVA: 0x000E0968 File Offset: 0x000DEB68
			public TPortData Lookup(IPvxAddress remoteIP, out IPRangeRemote bestMatchRange)
			{
				bool flag = false;
				TPortData result = default(TPortData);
				bestMatchRange = null;
				foreach (SmtpInConnectorMap<TData>.RangesEntry<TPortData> rangesEntry in this.RangeEntries)
				{
					IPRangeRemote iprangeRemote = rangesEntry.Match(remoteIP);
					if (iprangeRemote != null && (!flag || IPRangeRemote.Compare(iprangeRemote, bestMatchRange) < 0))
					{
						bestMatchRange = iprangeRemote;
						result = rangesEntry.Data;
						flag = true;
					}
				}
				return result;
			}

			// Token: 0x060036B0 RID: 14000 RVA: 0x000E09F0 File Offset: 0x000DEBF0
			public override int GetHashCode()
			{
				return this.Port;
			}

			// Token: 0x04001BE4 RID: 7140
			public readonly int Port;

			// Token: 0x04001BE5 RID: 7141
			public List<SmtpInConnectorMap<TData>.RangesEntry<TPortData>> RangeEntries = new List<SmtpInConnectorMap<TData>.RangesEntry<TPortData>>();
		}

		// Token: 0x020004BC RID: 1212
		internal class RangesEntry<TPortData> where TPortData : class
		{
			// Token: 0x060036B1 RID: 14001 RVA: 0x000E09F8 File Offset: 0x000DEBF8
			public RangesEntry(IPRangeRemote[] ranges, TPortData data)
			{
				this.Ranges = ranges;
				this.Data = data;
			}

			// Token: 0x060036B2 RID: 14002 RVA: 0x000E0A10 File Offset: 0x000DEC10
			public IPRangeRemote Match(IPvxAddress ipAddress)
			{
				IPRangeRemote iprangeRemote = null;
				foreach (IPRangeRemote iprangeRemote2 in this.Ranges)
				{
					if (iprangeRemote2.Contains(ipAddress) && (iprangeRemote == null || IPRangeRemote.Compare(iprangeRemote2, iprangeRemote) < 0))
					{
						iprangeRemote = iprangeRemote2;
					}
				}
				return iprangeRemote;
			}

			// Token: 0x04001BE6 RID: 7142
			public IPRangeRemote[] Ranges;

			// Token: 0x04001BE7 RID: 7143
			public TPortData Data;
		}
	}
}
