using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000DB RID: 219
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IpResourceCreationOnWrongTypeOfNetworkException : ClusCommonValidationFailedException
	{
		// Token: 0x06000780 RID: 1920 RVA: 0x0001C450 File Offset: 0x0001A650
		public IpResourceCreationOnWrongTypeOfNetworkException(string network) : base(Strings.IpResourceCreationOnWrongTypeOfNetworkException(network))
		{
			this.network = network;
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0001C46A File Offset: 0x0001A66A
		public IpResourceCreationOnWrongTypeOfNetworkException(string network, Exception innerException) : base(Strings.IpResourceCreationOnWrongTypeOfNetworkException(network), innerException)
		{
			this.network = network;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0001C485 File Offset: 0x0001A685
		protected IpResourceCreationOnWrongTypeOfNetworkException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.network = (string)info.GetValue("network", typeof(string));
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0001C4AF File Offset: 0x0001A6AF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("network", this.network);
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x0001C4CA File Offset: 0x0001A6CA
		public string Network
		{
			get
			{
				return this.network;
			}
		}

		// Token: 0x04000734 RID: 1844
		private readonly string network;
	}
}
