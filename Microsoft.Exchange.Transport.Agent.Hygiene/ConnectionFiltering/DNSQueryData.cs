using System;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Transport.Agent.ConnectionFiltering
{
	// Token: 0x02000009 RID: 9
	internal class DNSQueryData
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00003728 File Offset: 0x00001928
		public DNSQueryData(IPAddress hostIP, object source, ReceiveEventArgs eventArgs, bool safeListEnabled, bool blockListEnabled)
		{
			this.HostIP = hostIP;
			this.ReverseIP = DNSQueryData.ConvertToReverseIP(hostIP);
			this.Source = source;
			this.EventArgs = eventArgs;
			this.SafeListEnabled = safeListEnabled;
			this.BlockListEnabled = blockListEnabled;
			if (source is ReceiveCommandEventSource)
			{
				this.CurrentEvent = SMTPEvent.RcptTo;
				return;
			}
			this.CurrentEvent = SMTPEvent.EOH;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003784 File Offset: 0x00001984
		public static string ConvertToReverseIP(IPAddress address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (address.AddressFamily != AddressFamily.InterNetwork)
			{
				throw new ArgumentException("address must be IPv4.", "address");
			}
			byte[] addressBytes = address.GetAddressBytes();
			return string.Format(CultureInfo.InvariantCulture, "{0}.{1}.{2}.{3}", new object[]
			{
				addressBytes[3],
				addressBytes[2],
				addressBytes[1],
				addressBytes[0]
			});
		}

		// Token: 0x04000028 RID: 40
		public IPAddress HostIP;

		// Token: 0x04000029 RID: 41
		public string ReverseIP;

		// Token: 0x0400002A RID: 42
		public SMTPEvent CurrentEvent;

		// Token: 0x0400002B RID: 43
		public object Source;

		// Token: 0x0400002C RID: 44
		public ReceiveEventArgs EventArgs;

		// Token: 0x0400002D RID: 45
		public IPListProvider Provider;

		// Token: 0x0400002E RID: 46
		public bool SafeListEnabled;

		// Token: 0x0400002F RID: 47
		public bool BlockListEnabled;
	}
}
