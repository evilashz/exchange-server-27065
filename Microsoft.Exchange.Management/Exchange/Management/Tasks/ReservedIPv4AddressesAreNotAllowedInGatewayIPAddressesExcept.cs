using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200101F RID: 4127
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReservedIPv4AddressesAreNotAllowedInGatewayIPAddressesException : LocalizedException
	{
		// Token: 0x0600AF55 RID: 44885 RVA: 0x0029437F File Offset: 0x0029257F
		public ReservedIPv4AddressesAreNotAllowedInGatewayIPAddressesException(string ip) : base(Strings.ReservedIPv4AddressesAreNotAllowedInGatewayIPAddressesId(ip))
		{
			this.ip = ip;
		}

		// Token: 0x0600AF56 RID: 44886 RVA: 0x00294394 File Offset: 0x00292594
		public ReservedIPv4AddressesAreNotAllowedInGatewayIPAddressesException(string ip, Exception innerException) : base(Strings.ReservedIPv4AddressesAreNotAllowedInGatewayIPAddressesId(ip), innerException)
		{
			this.ip = ip;
		}

		// Token: 0x0600AF57 RID: 44887 RVA: 0x002943AA File Offset: 0x002925AA
		protected ReservedIPv4AddressesAreNotAllowedInGatewayIPAddressesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ip = (string)info.GetValue("ip", typeof(string));
		}

		// Token: 0x0600AF58 RID: 44888 RVA: 0x002943D4 File Offset: 0x002925D4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ip", this.ip);
		}

		// Token: 0x170037F6 RID: 14326
		// (get) Token: 0x0600AF59 RID: 44889 RVA: 0x002943EF File Offset: 0x002925EF
		public string Ip
		{
			get
			{
				return this.ip;
			}
		}

		// Token: 0x0400615C RID: 24924
		private readonly string ip;
	}
}
