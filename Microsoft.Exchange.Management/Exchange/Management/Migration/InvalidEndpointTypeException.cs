using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x02001124 RID: 4388
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidEndpointTypeException : LocalizedException
	{
		// Token: 0x0600B4A2 RID: 46242 RVA: 0x0029D15D File Offset: 0x0029B35D
		public InvalidEndpointTypeException(string endpointId, string type) : base(Strings.ErrorInvalidEndpointType(endpointId, type))
		{
			this.endpointId = endpointId;
			this.type = type;
		}

		// Token: 0x0600B4A3 RID: 46243 RVA: 0x0029D17A File Offset: 0x0029B37A
		public InvalidEndpointTypeException(string endpointId, string type, Exception innerException) : base(Strings.ErrorInvalidEndpointType(endpointId, type), innerException)
		{
			this.endpointId = endpointId;
			this.type = type;
		}

		// Token: 0x0600B4A4 RID: 46244 RVA: 0x0029D198 File Offset: 0x0029B398
		protected InvalidEndpointTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.endpointId = (string)info.GetValue("endpointId", typeof(string));
			this.type = (string)info.GetValue("type", typeof(string));
		}

		// Token: 0x0600B4A5 RID: 46245 RVA: 0x0029D1ED File Offset: 0x0029B3ED
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("endpointId", this.endpointId);
			info.AddValue("type", this.type);
		}

		// Token: 0x1700392F RID: 14639
		// (get) Token: 0x0600B4A6 RID: 46246 RVA: 0x0029D219 File Offset: 0x0029B419
		public string EndpointId
		{
			get
			{
				return this.endpointId;
			}
		}

		// Token: 0x17003930 RID: 14640
		// (get) Token: 0x0600B4A7 RID: 46247 RVA: 0x0029D221 File Offset: 0x0029B421
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x04006295 RID: 25237
		private readonly string endpointId;

		// Token: 0x04006296 RID: 25238
		private readonly string type;
	}
}
