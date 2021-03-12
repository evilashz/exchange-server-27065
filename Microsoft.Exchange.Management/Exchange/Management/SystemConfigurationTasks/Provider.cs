using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A34 RID: 2612
	internal class Provider
	{
		// Token: 0x06005D4F RID: 23887 RVA: 0x001893A2 File Offset: 0x001875A2
		public Provider(IPListProvider provider, Server server)
		{
			this.provider = provider;
			this.dns = Provider.GetAndInitializeDns(server);
		}

		// Token: 0x06005D50 RID: 23888 RVA: 0x001893C0 File Offset: 0x001875C0
		public static bool Query(Server server, IPListProvider provider, IPAddress ipAddress, out IPAddress[] providerResult)
		{
			Provider provider2 = new Provider(provider, server);
			IAsyncResult asyncResult = provider2.BeginQuery(ipAddress, null, null);
			asyncResult.AsyncWaitHandle.WaitOne();
			return provider2.EndQuery(asyncResult, out providerResult);
		}

		// Token: 0x06005D51 RID: 23889 RVA: 0x001893F4 File Offset: 0x001875F4
		public IAsyncResult BeginQuery(IPAddress ipAddress, AsyncCallback asyncCallback, object asyncState)
		{
			Provider.QueryAsyncResult queryAsyncResult = new Provider.QueryAsyncResult(asyncCallback, asyncState);
			queryAsyncResult.SetAsync();
			string domainName = string.Format("{0}.{1}", this.ReverseIP(ipAddress), this.provider.LookupDomain);
			IAsyncResult asyncResult = this.dns.BeginResolveToAddresses(domainName, AddressFamily.InterNetwork, new AsyncCallback(this.DnsQueryCallback), queryAsyncResult);
			if (asyncResult.CompletedSynchronously)
			{
				queryAsyncResult.ResetAsync();
			}
			return queryAsyncResult;
		}

		// Token: 0x06005D52 RID: 23890 RVA: 0x00189458 File Offset: 0x00187658
		public bool EndQuery(IAsyncResult ar, out IPAddress[] providerResult)
		{
			Provider.QueryAsyncResult queryAsyncResult = ar as Provider.QueryAsyncResult;
			if (queryAsyncResult != null && queryAsyncResult.IsCompleted)
			{
				providerResult = queryAsyncResult.ProviderResult;
				return queryAsyncResult.IsMatch;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06005D53 RID: 23891 RVA: 0x0018948C File Offset: 0x0018768C
		internal static Dns GetAndInitializeDns(Server server)
		{
			Dns dns = new Dns();
			dns.Options = Provider.GetDnsOptions(server.ExternalDNSProtocolOption);
			MultiValuedProperty<IPAddress> externalDNSServers = server.ExternalDNSServers;
			if (server.ExternalDNSAdapterEnabled || MultiValuedPropertyBase.IsNullOrEmpty(externalDNSServers))
			{
				dns.AdapterServerList(server.ExternalDNSAdapterGuid);
			}
			else
			{
				IPAddress[] array = new IPAddress[externalDNSServers.Count];
				externalDNSServers.CopyTo(array, 0);
				dns.ServerList.Initialize(array);
			}
			return dns;
		}

		// Token: 0x06005D54 RID: 23892 RVA: 0x001894F8 File Offset: 0x001876F8
		private static DnsQueryOptions GetDnsOptions(ProtocolOption protocolOption)
		{
			DnsQueryOptions dnsQueryOptions = DnsQueryOptions.None;
			switch (protocolOption)
			{
			case ProtocolOption.UseUdpOnly:
				dnsQueryOptions |= DnsQueryOptions.AcceptTruncatedResponse;
				break;
			case ProtocolOption.UseTcpOnly:
				dnsQueryOptions |= DnsQueryOptions.UseTcpOnly;
				break;
			}
			return dnsQueryOptions;
		}

		// Token: 0x06005D55 RID: 23893 RVA: 0x00189528 File Offset: 0x00187728
		private void DnsQueryCallback(IAsyncResult ar)
		{
			IPAddress[] array;
			DnsStatus dnsStatus = Dns.EndResolveToAddresses(ar, out array);
			bool isMatch = false;
			if (dnsStatus == DnsStatus.Success)
			{
				isMatch = this.MatchResult(array);
			}
			Provider.QueryAsyncResult queryAsyncResult = (Provider.QueryAsyncResult)ar.AsyncState;
			queryAsyncResult.QueryCompleted(isMatch, array);
		}

		// Token: 0x06005D56 RID: 23894 RVA: 0x00189560 File Offset: 0x00187760
		private string ReverseIP(IPAddress address)
		{
			byte[] addressBytes = address.GetAddressBytes();
			return string.Format("{0}.{1}.{2}.{3}", new object[]
			{
				addressBytes[3],
				addressBytes[2],
				addressBytes[1],
				addressBytes[0]
			});
		}

		// Token: 0x06005D57 RID: 23895 RVA: 0x001895B4 File Offset: 0x001877B4
		private bool MatchResult(IPAddress[] addresses)
		{
			int i = 0;
			while (i < addresses.Length)
			{
				IPAddress ipaddress = addresses[i];
				if (!this.provider.AnyMatch)
				{
					if (this.provider.BitmaskMatch != null)
					{
						byte[] addressBytes = this.provider.BitmaskMatch.GetAddressBytes();
						byte[] addressBytes2 = ipaddress.GetAddressBytes();
						for (int j = 0; j < addressBytes.Length; j++)
						{
							if ((addressBytes[j] & addressBytes2[j]) != 0)
							{
								return true;
							}
						}
					}
					if (this.provider.IPAddressesMatch != null)
					{
						foreach (IPAddress obj in this.provider.IPAddressesMatch)
						{
							if (ipaddress.Equals(obj))
							{
								return true;
							}
						}
					}
					i++;
					continue;
				}
				return true;
			}
			return false;
		}

		// Token: 0x040034A9 RID: 13481
		private IPListProvider provider;

		// Token: 0x040034AA RID: 13482
		private Dns dns;

		// Token: 0x02000A35 RID: 2613
		private class QueryAsyncResult : IAsyncResult
		{
			// Token: 0x06005D58 RID: 23896 RVA: 0x001896A0 File Offset: 0x001878A0
			public QueryAsyncResult(AsyncCallback asyncCallback, object asyncState)
			{
				this.asyncCallback = asyncCallback;
				this.asyncState = asyncState;
				this.isCompleted = false;
				this.isMatch = false;
				this.providerResult = null;
				if (asyncCallback == null)
				{
					this.manualResetEvent = new ManualResetEvent(false);
					return;
				}
				this.manualResetEvent = null;
			}

			// Token: 0x17001BFA RID: 7162
			// (get) Token: 0x06005D59 RID: 23897 RVA: 0x001896ED File Offset: 0x001878ED
			public bool IsMatch
			{
				get
				{
					this.ThrowIfNotCompleted();
					return this.isMatch;
				}
			}

			// Token: 0x17001BFB RID: 7163
			// (get) Token: 0x06005D5A RID: 23898 RVA: 0x001896FB File Offset: 0x001878FB
			public IPAddress[] ProviderResult
			{
				get
				{
					this.ThrowIfNotCompleted();
					return this.providerResult;
				}
			}

			// Token: 0x06005D5B RID: 23899 RVA: 0x00189709 File Offset: 0x00187909
			private void ThrowIfNotCompleted()
			{
				if (!this.isCompleted)
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x06005D5C RID: 23900 RVA: 0x00189719 File Offset: 0x00187919
			public void QueryCompleted(bool isMatch, IPAddress[] providerResult)
			{
				this.isMatch = isMatch;
				this.providerResult = providerResult;
				this.isCompleted = true;
				if (this.manualResetEvent != null)
				{
					this.manualResetEvent.Set();
				}
				if (this.asyncCallback != null)
				{
					this.asyncCallback(this);
				}
			}

			// Token: 0x06005D5D RID: 23901 RVA: 0x00189758 File Offset: 0x00187958
			public void SetAsync()
			{
				this.completedSynchronously = false;
			}

			// Token: 0x06005D5E RID: 23902 RVA: 0x00189761 File Offset: 0x00187961
			public void ResetAsync()
			{
				this.completedSynchronously = true;
			}

			// Token: 0x17001BFC RID: 7164
			// (get) Token: 0x06005D5F RID: 23903 RVA: 0x0018976A File Offset: 0x0018796A
			public object AsyncState
			{
				get
				{
					return this.asyncState;
				}
			}

			// Token: 0x17001BFD RID: 7165
			// (get) Token: 0x06005D60 RID: 23904 RVA: 0x00189772 File Offset: 0x00187972
			public bool CompletedSynchronously
			{
				get
				{
					return this.completedSynchronously;
				}
			}

			// Token: 0x17001BFE RID: 7166
			// (get) Token: 0x06005D61 RID: 23905 RVA: 0x0018977A File Offset: 0x0018797A
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					if (this.manualResetEvent != null)
					{
						return this.manualResetEvent;
					}
					throw new InvalidOperationException();
				}
			}

			// Token: 0x17001BFF RID: 7167
			// (get) Token: 0x06005D62 RID: 23906 RVA: 0x00189790 File Offset: 0x00187990
			public bool IsCompleted
			{
				get
				{
					return this.isCompleted;
				}
			}

			// Token: 0x040034AB RID: 13483
			private AsyncCallback asyncCallback;

			// Token: 0x040034AC RID: 13484
			private object asyncState;

			// Token: 0x040034AD RID: 13485
			private bool isCompleted;

			// Token: 0x040034AE RID: 13486
			private bool completedSynchronously;

			// Token: 0x040034AF RID: 13487
			private bool isMatch;

			// Token: 0x040034B0 RID: 13488
			private IPAddress[] providerResult;

			// Token: 0x040034B1 RID: 13489
			private ManualResetEvent manualResetEvent;
		}
	}
}
