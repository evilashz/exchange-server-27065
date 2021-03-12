using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200101A RID: 4122
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IPv6AddressesAreNotAllowedInGatewayIPAddressesException : LocalizedException
	{
		// Token: 0x0600AF3F RID: 44863 RVA: 0x00294202 File Offset: 0x00292402
		public IPv6AddressesAreNotAllowedInGatewayIPAddressesException(string ip) : base(Strings.IPv6AddressesAreNotAllowedInGatewayIPAddressesId(ip))
		{
			this.ip = ip;
		}

		// Token: 0x0600AF40 RID: 44864 RVA: 0x00294217 File Offset: 0x00292417
		public IPv6AddressesAreNotAllowedInGatewayIPAddressesException(string ip, Exception innerException) : base(Strings.IPv6AddressesAreNotAllowedInGatewayIPAddressesId(ip), innerException)
		{
			this.ip = ip;
		}

		// Token: 0x0600AF41 RID: 44865 RVA: 0x0029422D File Offset: 0x0029242D
		protected IPv6AddressesAreNotAllowedInGatewayIPAddressesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ip = (string)info.GetValue("ip", typeof(string));
		}

		// Token: 0x0600AF42 RID: 44866 RVA: 0x00294257 File Offset: 0x00292457
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ip", this.ip);
		}

		// Token: 0x170037F4 RID: 14324
		// (get) Token: 0x0600AF43 RID: 44867 RVA: 0x00294272 File Offset: 0x00292472
		public string Ip
		{
			get
			{
				return this.ip;
			}
		}

		// Token: 0x0400615A RID: 24922
		private readonly string ip;
	}
}
