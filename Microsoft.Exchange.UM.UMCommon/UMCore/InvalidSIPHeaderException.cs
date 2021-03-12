using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001EE RID: 494
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidSIPHeaderException : LocalizedException
	{
		// Token: 0x0600106C RID: 4204 RVA: 0x00038C0D File Offset: 0x00036E0D
		public InvalidSIPHeaderException(string request, string header, string value) : base(Strings.InvalidSIPHeader(request, header, value))
		{
			this.request = request;
			this.header = header;
			this.value = value;
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x00038C32 File Offset: 0x00036E32
		public InvalidSIPHeaderException(string request, string header, string value, Exception innerException) : base(Strings.InvalidSIPHeader(request, header, value), innerException)
		{
			this.request = request;
			this.header = header;
			this.value = value;
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x00038C5C File Offset: 0x00036E5C
		protected InvalidSIPHeaderException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.request = (string)info.GetValue("request", typeof(string));
			this.header = (string)info.GetValue("header", typeof(string));
			this.value = (string)info.GetValue("value", typeof(string));
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x00038CD1 File Offset: 0x00036ED1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("request", this.request);
			info.AddValue("header", this.header);
			info.AddValue("value", this.value);
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x00038D0E File Offset: 0x00036F0E
		public string Request
		{
			get
			{
				return this.request;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x00038D16 File Offset: 0x00036F16
		public string Header
		{
			get
			{
				return this.header;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x00038D1E File Offset: 0x00036F1E
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x04000872 RID: 2162
		private readonly string request;

		// Token: 0x04000873 RID: 2163
		private readonly string header;

		// Token: 0x04000874 RID: 2164
		private readonly string value;
	}
}
