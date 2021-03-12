using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000198 RID: 408
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidUMProxyAddressException : LocalizedException
	{
		// Token: 0x06000E2E RID: 3630 RVA: 0x00034C68 File Offset: 0x00032E68
		public InvalidUMProxyAddressException(string proxyAddress) : base(Strings.InvalidUMProxyAddressException(proxyAddress))
		{
			this.proxyAddress = proxyAddress;
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x00034C7D File Offset: 0x00032E7D
		public InvalidUMProxyAddressException(string proxyAddress, Exception innerException) : base(Strings.InvalidUMProxyAddressException(proxyAddress), innerException)
		{
			this.proxyAddress = proxyAddress;
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x00034C93 File Offset: 0x00032E93
		protected InvalidUMProxyAddressException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.proxyAddress = (string)info.GetValue("proxyAddress", typeof(string));
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x00034CBD File Offset: 0x00032EBD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("proxyAddress", this.proxyAddress);
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000E32 RID: 3634 RVA: 0x00034CD8 File Offset: 0x00032ED8
		public string ProxyAddress
		{
			get
			{
				return this.proxyAddress;
			}
		}

		// Token: 0x0400077E RID: 1918
		private readonly string proxyAddress;
	}
}
