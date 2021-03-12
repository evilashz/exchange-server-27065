using System;
using System.Collections;
using System.Net;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000125 RID: 293
	public class ExchangeHostnameResolver
	{
		// Token: 0x060008AF RID: 2223 RVA: 0x00032E24 File Offset: 0x00031024
		public ExchangeHostnameResolver() : this("pilot.outlook.com")
		{
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00032E31 File Offset: 0x00031031
		public ExchangeHostnameResolver(string hostname) : this(hostname, true)
		{
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00032E3B File Offset: 0x0003103B
		public ExchangeHostnameResolver(string hostname, bool flush)
		{
			this.hostname = hostname;
			this.ResolveInternal(flush);
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00032E51 File Offset: 0x00031051
		public void Resolve()
		{
			this.ResolveInternal(true);
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00032E5A File Offset: 0x0003105A
		public IEnumerator GetEnumerator()
		{
			return new ExchangeHostnameResolver.ExchangeHostnameIPEnumerator(this.ipAddresses);
		}

		// Token: 0x060008B4 RID: 2228
		[DllImport("dnsapi.dll")]
		private static extern uint DnsFlushResolverCache();

		// Token: 0x060008B5 RID: 2229 RVA: 0x00032E67 File Offset: 0x00031067
		private void ResolveInternal(bool flush)
		{
			if (flush)
			{
				ExchangeHostnameResolver.DnsFlushResolverCache();
			}
			this.ipAddresses = Dns.GetHostAddresses(this.hostname);
		}

		// Token: 0x040005FD RID: 1533
		private readonly string hostname;

		// Token: 0x040005FE RID: 1534
		private IPAddress[] ipAddresses;

		// Token: 0x02000126 RID: 294
		public class ExchangeHostnameIPEnumerator : IEnumerator
		{
			// Token: 0x060008B6 RID: 2230 RVA: 0x00032E83 File Offset: 0x00031083
			public ExchangeHostnameIPEnumerator(IPAddress[] list)
			{
				this.ipAddresses = list;
			}

			// Token: 0x170001F9 RID: 505
			// (get) Token: 0x060008B7 RID: 2231 RVA: 0x00032E99 File Offset: 0x00031099
			object IEnumerator.Current
			{
				get
				{
					return this.ipAddresses[this.position];
				}
			}

			// Token: 0x060008B8 RID: 2232 RVA: 0x00032EA8 File Offset: 0x000310A8
			public bool MoveNext()
			{
				this.position++;
				return this.position < this.ipAddresses.Length;
			}

			// Token: 0x060008B9 RID: 2233 RVA: 0x00032EC8 File Offset: 0x000310C8
			public void Reset()
			{
				this.position = -1;
			}

			// Token: 0x040005FF RID: 1535
			private IPAddress[] ipAddresses;

			// Token: 0x04000600 RID: 1536
			private int position = -1;
		}
	}
}
