using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200101E RID: 4126
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidIPv4AddressesAreNotAllowedInGatewayIPAddressesException : LocalizedException
	{
		// Token: 0x0600AF50 RID: 44880 RVA: 0x00294307 File Offset: 0x00292507
		public InvalidIPv4AddressesAreNotAllowedInGatewayIPAddressesException(string ip) : base(Strings.InvalidIPv4AddressesAreNotAllowedInGatewayIPAddressesId(ip))
		{
			this.ip = ip;
		}

		// Token: 0x0600AF51 RID: 44881 RVA: 0x0029431C File Offset: 0x0029251C
		public InvalidIPv4AddressesAreNotAllowedInGatewayIPAddressesException(string ip, Exception innerException) : base(Strings.InvalidIPv4AddressesAreNotAllowedInGatewayIPAddressesId(ip), innerException)
		{
			this.ip = ip;
		}

		// Token: 0x0600AF52 RID: 44882 RVA: 0x00294332 File Offset: 0x00292532
		protected InvalidIPv4AddressesAreNotAllowedInGatewayIPAddressesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ip = (string)info.GetValue("ip", typeof(string));
		}

		// Token: 0x0600AF53 RID: 44883 RVA: 0x0029435C File Offset: 0x0029255C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ip", this.ip);
		}

		// Token: 0x170037F5 RID: 14325
		// (get) Token: 0x0600AF54 RID: 44884 RVA: 0x00294377 File Offset: 0x00292577
		public string Ip
		{
			get
			{
				return this.ip;
			}
		}

		// Token: 0x0400615B RID: 24923
		private readonly string ip;
	}
}
