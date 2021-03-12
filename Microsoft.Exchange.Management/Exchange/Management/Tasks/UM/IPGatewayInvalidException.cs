using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011B7 RID: 4535
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IPGatewayInvalidException : LocalizedException
	{
		// Token: 0x0600B87C RID: 47228 RVA: 0x002A4A3B File Offset: 0x002A2C3B
		public IPGatewayInvalidException(string ipaddress) : base(Strings.ExceptionIPGatewayInvalid(ipaddress))
		{
			this.ipaddress = ipaddress;
		}

		// Token: 0x0600B87D RID: 47229 RVA: 0x002A4A50 File Offset: 0x002A2C50
		public IPGatewayInvalidException(string ipaddress, Exception innerException) : base(Strings.ExceptionIPGatewayInvalid(ipaddress), innerException)
		{
			this.ipaddress = ipaddress;
		}

		// Token: 0x0600B87E RID: 47230 RVA: 0x002A4A66 File Offset: 0x002A2C66
		protected IPGatewayInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ipaddress = (string)info.GetValue("ipaddress", typeof(string));
		}

		// Token: 0x0600B87F RID: 47231 RVA: 0x002A4A90 File Offset: 0x002A2C90
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ipaddress", this.ipaddress);
		}

		// Token: 0x17003A1D RID: 14877
		// (get) Token: 0x0600B880 RID: 47232 RVA: 0x002A4AAB File Offset: 0x002A2CAB
		public string Ipaddress
		{
			get
			{
				return this.ipaddress;
			}
		}

		// Token: 0x04006438 RID: 25656
		private readonly string ipaddress;
	}
}
