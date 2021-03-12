using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000DA RID: 218
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RequestedNetworkIsNotIPv6Enabled : ClusCommonValidationFailedException
	{
		// Token: 0x0600077B RID: 1915 RVA: 0x0001C3CE File Offset: 0x0001A5CE
		public RequestedNetworkIsNotIPv6Enabled(string network) : base(Strings.RequestedNetworkIsNotIPv6Enabled(network))
		{
			this.network = network;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0001C3E8 File Offset: 0x0001A5E8
		public RequestedNetworkIsNotIPv6Enabled(string network, Exception innerException) : base(Strings.RequestedNetworkIsNotIPv6Enabled(network), innerException)
		{
			this.network = network;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0001C403 File Offset: 0x0001A603
		protected RequestedNetworkIsNotIPv6Enabled(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.network = (string)info.GetValue("network", typeof(string));
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0001C42D File Offset: 0x0001A62D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("network", this.network);
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x0001C448 File Offset: 0x0001A648
		public string Network
		{
			get
			{
				return this.network;
			}
		}

		// Token: 0x04000733 RID: 1843
		private readonly string network;
	}
}
