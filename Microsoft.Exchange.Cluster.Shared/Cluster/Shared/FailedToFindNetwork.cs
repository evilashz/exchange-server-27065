using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000D8 RID: 216
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToFindNetwork : ClusCommonValidationFailedException
	{
		// Token: 0x06000771 RID: 1905 RVA: 0x0001C2CA File Offset: 0x0001A4CA
		public FailedToFindNetwork(string network) : base(Strings.FailedToFindNetwork(network))
		{
			this.network = network;
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0001C2E4 File Offset: 0x0001A4E4
		public FailedToFindNetwork(string network, Exception innerException) : base(Strings.FailedToFindNetwork(network), innerException)
		{
			this.network = network;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0001C2FF File Offset: 0x0001A4FF
		protected FailedToFindNetwork(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.network = (string)info.GetValue("network", typeof(string));
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0001C329 File Offset: 0x0001A529
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("network", this.network);
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x0001C344 File Offset: 0x0001A544
		public string Network
		{
			get
			{
				return this.network;
			}
		}

		// Token: 0x04000731 RID: 1841
		private readonly string network;
	}
}
