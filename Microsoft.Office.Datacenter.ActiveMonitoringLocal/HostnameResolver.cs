using System;
using System.Collections;
using System.Net;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000069 RID: 105
	public class HostnameResolver
	{
		// Token: 0x06000633 RID: 1587 RVA: 0x0001A232 File Offset: 0x00018432
		public HostnameResolver(string hostname) : this(hostname, true)
		{
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0001A23C File Offset: 0x0001843C
		public HostnameResolver(string hostname, bool flush)
		{
			this.hostname = hostname;
			this.ResolveInternal(flush);
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0001A252 File Offset: 0x00018452
		public void Resolve()
		{
			this.ResolveInternal(true);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001A25B File Offset: 0x0001845B
		public IEnumerator GetEnumerator()
		{
			return new HostnameResolver.HostnameIPEnumerator(this.ipAddresses);
		}

		// Token: 0x06000637 RID: 1591
		[DllImport("dnsapi.dll")]
		private static extern uint DnsFlushResolverCache();

		// Token: 0x06000638 RID: 1592 RVA: 0x0001A268 File Offset: 0x00018468
		private void ResolveInternal(bool flush)
		{
			if (flush)
			{
				HostnameResolver.DnsFlushResolverCache();
			}
			this.ipAddresses = Dns.GetHostAddresses(this.hostname);
		}

		// Token: 0x0400041B RID: 1051
		private readonly string hostname;

		// Token: 0x0400041C RID: 1052
		private IPAddress[] ipAddresses;

		// Token: 0x0200006A RID: 106
		public class HostnameIPEnumerator : IEnumerator
		{
			// Token: 0x06000639 RID: 1593 RVA: 0x0001A284 File Offset: 0x00018484
			public HostnameIPEnumerator(IPAddress[] list)
			{
				this.ipAddresses = list;
			}

			// Token: 0x170001FC RID: 508
			// (get) Token: 0x0600063A RID: 1594 RVA: 0x0001A29A File Offset: 0x0001849A
			object IEnumerator.Current
			{
				get
				{
					return this.ipAddresses[this.position];
				}
			}

			// Token: 0x0600063B RID: 1595 RVA: 0x0001A2A9 File Offset: 0x000184A9
			public bool MoveNext()
			{
				this.position++;
				return this.position < this.ipAddresses.Length;
			}

			// Token: 0x0600063C RID: 1596 RVA: 0x0001A2C9 File Offset: 0x000184C9
			public void Reset()
			{
				this.position = -1;
			}

			// Token: 0x0400041D RID: 1053
			private IPAddress[] ipAddresses;

			// Token: 0x0400041E RID: 1054
			private int position = -1;
		}
	}
}
