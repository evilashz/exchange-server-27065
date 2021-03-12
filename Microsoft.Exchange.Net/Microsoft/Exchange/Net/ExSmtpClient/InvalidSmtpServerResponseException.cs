using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net.ExSmtpClient
{
	// Token: 0x020000E7 RID: 231
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidSmtpServerResponseException : LocalizedException
	{
		// Token: 0x06000613 RID: 1555 RVA: 0x00015E60 File Offset: 0x00014060
		public InvalidSmtpServerResponseException(string response) : base(NetException.InvalidSmtpServerResponseException(response))
		{
			this.response = response;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00015E75 File Offset: 0x00014075
		public InvalidSmtpServerResponseException(string response, Exception innerException) : base(NetException.InvalidSmtpServerResponseException(response), innerException)
		{
			this.response = response;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00015E8B File Offset: 0x0001408B
		protected InvalidSmtpServerResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.response = (string)info.GetValue("response", typeof(string));
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00015EB5 File Offset: 0x000140B5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("response", this.response);
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x00015ED0 File Offset: 0x000140D0
		public string Response
		{
			get
			{
				return this.response;
			}
		}

		// Token: 0x040004F8 RID: 1272
		private readonly string response;
	}
}
