using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E82 RID: 3714
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidUMProxyAddressException : LocalizedException
	{
		// Token: 0x0600A75C RID: 42844 RVA: 0x00288440 File Offset: 0x00286640
		public InvalidUMProxyAddressException(string proxyAddress) : base(Strings.InvalidUMProxyAddressException(proxyAddress))
		{
			this.proxyAddress = proxyAddress;
		}

		// Token: 0x0600A75D RID: 42845 RVA: 0x00288455 File Offset: 0x00286655
		public InvalidUMProxyAddressException(string proxyAddress, Exception innerException) : base(Strings.InvalidUMProxyAddressException(proxyAddress), innerException)
		{
			this.proxyAddress = proxyAddress;
		}

		// Token: 0x0600A75E RID: 42846 RVA: 0x0028846B File Offset: 0x0028666B
		protected InvalidUMProxyAddressException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.proxyAddress = (string)info.GetValue("proxyAddress", typeof(string));
		}

		// Token: 0x0600A75F RID: 42847 RVA: 0x00288495 File Offset: 0x00286695
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("proxyAddress", this.proxyAddress);
		}

		// Token: 0x17003671 RID: 13937
		// (get) Token: 0x0600A760 RID: 42848 RVA: 0x002884B0 File Offset: 0x002866B0
		public string ProxyAddress
		{
			get
			{
				return this.proxyAddress;
			}
		}

		// Token: 0x04005FD7 RID: 24535
		private readonly string proxyAddress;
	}
}
