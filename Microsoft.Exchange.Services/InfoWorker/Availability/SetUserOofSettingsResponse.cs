using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.InfoWorker.Availability
{
	// Token: 0x02000011 RID: 17
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class SetUserOofSettingsResponse : IExchangeWebMethodResponse
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000039DB File Offset: 0x00001BDB
		// (set) Token: 0x06000087 RID: 135 RVA: 0x000039E3 File Offset: 0x00001BE3
		[XmlElement(IsNullable = false)]
		public ResponseMessage ResponseMessage
		{
			get
			{
				return this.responseMessage;
			}
			set
			{
				this.responseMessage = value;
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000039EC File Offset: 0x00001BEC
		public ResponseType GetResponseType()
		{
			return ResponseType.SetUserOofSettingsResponseMessage;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000039F0 File Offset: 0x00001BF0
		public ResponseCodeType GetErrorCodeToLog()
		{
			if (this.ResponseMessage == null)
			{
				return ResponseCodeType.NoError;
			}
			return this.ResponseMessage.ResponseCode;
		}

		// Token: 0x04000029 RID: 41
		private ResponseMessage responseMessage;
	}
}
