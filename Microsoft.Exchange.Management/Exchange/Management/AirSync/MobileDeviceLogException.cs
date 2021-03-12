using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.AirSync
{
	// Token: 0x02000E2D RID: 3629
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MobileDeviceLogException : LocalizedException
	{
		// Token: 0x0600A5E7 RID: 42471 RVA: 0x00286CAD File Offset: 0x00284EAD
		public MobileDeviceLogException(string msg) : base(Strings.MobileDeviceLogException(msg))
		{
			this.msg = msg;
		}

		// Token: 0x0600A5E8 RID: 42472 RVA: 0x00286CC2 File Offset: 0x00284EC2
		public MobileDeviceLogException(string msg, Exception innerException) : base(Strings.MobileDeviceLogException(msg), innerException)
		{
			this.msg = msg;
		}

		// Token: 0x0600A5E9 RID: 42473 RVA: 0x00286CD8 File Offset: 0x00284ED8
		protected MobileDeviceLogException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x0600A5EA RID: 42474 RVA: 0x00286D02 File Offset: 0x00284F02
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x17003650 RID: 13904
		// (get) Token: 0x0600A5EB RID: 42475 RVA: 0x00286D1D File Offset: 0x00284F1D
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x04005FB6 RID: 24502
		private readonly string msg;
	}
}
