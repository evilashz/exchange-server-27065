using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011B5 RID: 4533
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IPGatewayAlreadyExistsException : LocalizedException
	{
		// Token: 0x0600B872 RID: 47218 RVA: 0x002A494B File Offset: 0x002A2B4B
		public IPGatewayAlreadyExistsException(string ipaddress) : base(Strings.ExceptionIPGatewayAlreadyExists(ipaddress))
		{
			this.ipaddress = ipaddress;
		}

		// Token: 0x0600B873 RID: 47219 RVA: 0x002A4960 File Offset: 0x002A2B60
		public IPGatewayAlreadyExistsException(string ipaddress, Exception innerException) : base(Strings.ExceptionIPGatewayAlreadyExists(ipaddress), innerException)
		{
			this.ipaddress = ipaddress;
		}

		// Token: 0x0600B874 RID: 47220 RVA: 0x002A4976 File Offset: 0x002A2B76
		protected IPGatewayAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ipaddress = (string)info.GetValue("ipaddress", typeof(string));
		}

		// Token: 0x0600B875 RID: 47221 RVA: 0x002A49A0 File Offset: 0x002A2BA0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ipaddress", this.ipaddress);
		}

		// Token: 0x17003A1B RID: 14875
		// (get) Token: 0x0600B876 RID: 47222 RVA: 0x002A49BB File Offset: 0x002A2BBB
		public string Ipaddress
		{
			get
			{
				return this.ipaddress;
			}
		}

		// Token: 0x04006436 RID: 25654
		private readonly string ipaddress;
	}
}
