using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011B6 RID: 4534
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IPGatewayAlreadyIPAddressExistsException : LocalizedException
	{
		// Token: 0x0600B877 RID: 47223 RVA: 0x002A49C3 File Offset: 0x002A2BC3
		public IPGatewayAlreadyIPAddressExistsException(string ipaddress) : base(Strings.ExceptionIPGatewayIPAddressAlreadyExists(ipaddress))
		{
			this.ipaddress = ipaddress;
		}

		// Token: 0x0600B878 RID: 47224 RVA: 0x002A49D8 File Offset: 0x002A2BD8
		public IPGatewayAlreadyIPAddressExistsException(string ipaddress, Exception innerException) : base(Strings.ExceptionIPGatewayIPAddressAlreadyExists(ipaddress), innerException)
		{
			this.ipaddress = ipaddress;
		}

		// Token: 0x0600B879 RID: 47225 RVA: 0x002A49EE File Offset: 0x002A2BEE
		protected IPGatewayAlreadyIPAddressExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ipaddress = (string)info.GetValue("ipaddress", typeof(string));
		}

		// Token: 0x0600B87A RID: 47226 RVA: 0x002A4A18 File Offset: 0x002A2C18
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ipaddress", this.ipaddress);
		}

		// Token: 0x17003A1C RID: 14876
		// (get) Token: 0x0600B87B RID: 47227 RVA: 0x002A4A33 File Offset: 0x002A2C33
		public string Ipaddress
		{
			get
			{
				return this.ipaddress;
			}
		}

		// Token: 0x04006437 RID: 25655
		private readonly string ipaddress;
	}
}
