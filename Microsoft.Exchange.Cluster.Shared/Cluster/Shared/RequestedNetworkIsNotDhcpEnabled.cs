using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000D9 RID: 217
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RequestedNetworkIsNotDhcpEnabled : ClusCommonValidationFailedException
	{
		// Token: 0x06000776 RID: 1910 RVA: 0x0001C34C File Offset: 0x0001A54C
		public RequestedNetworkIsNotDhcpEnabled(string network) : base(Strings.RequestedNetworkIsNotDhcpEnabled(network))
		{
			this.network = network;
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0001C366 File Offset: 0x0001A566
		public RequestedNetworkIsNotDhcpEnabled(string network, Exception innerException) : base(Strings.RequestedNetworkIsNotDhcpEnabled(network), innerException)
		{
			this.network = network;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0001C381 File Offset: 0x0001A581
		protected RequestedNetworkIsNotDhcpEnabled(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.network = (string)info.GetValue("network", typeof(string));
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0001C3AB File Offset: 0x0001A5AB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("network", this.network);
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x0001C3C6 File Offset: 0x0001A5C6
		public string Network
		{
			get
			{
				return this.network;
			}
		}

		// Token: 0x04000732 RID: 1842
		private readonly string network;
	}
}
