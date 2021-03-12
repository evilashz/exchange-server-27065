using System;
using System.Xml.Serialization;
using Microsoft.Exchange.InfoWorker.Common.OOF;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.InfoWorker.Availability
{
	// Token: 0x0200000D RID: 13
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetUserOofSettingsResponse : IExchangeWebMethodResponse
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000033BF File Offset: 0x000015BF
		// (set) Token: 0x0600006B RID: 107 RVA: 0x000033C7 File Offset: 0x000015C7
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

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000033D0 File Offset: 0x000015D0
		// (set) Token: 0x0600006D RID: 109 RVA: 0x000033D8 File Offset: 0x000015D8
		[XmlElement(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public UserOofSettings OofSettings
		{
			get
			{
				return this.oofSettings;
			}
			set
			{
				if (value != null)
				{
					this.oofSettings = value;
				}
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000033E4 File Offset: 0x000015E4
		// (set) Token: 0x0600006F RID: 111 RVA: 0x000033F6 File Offset: 0x000015F6
		public string AllowExternalOof
		{
			get
			{
				if (!this.EmitAllowExternalOof)
				{
					return null;
				}
				return this.allowExternalOof;
			}
			set
			{
				this.allowExternalOof = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000033FF File Offset: 0x000015FF
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00003407 File Offset: 0x00001607
		[XmlIgnore]
		internal bool EmitAllowExternalOof
		{
			get
			{
				return this.emitAllowExternalOof;
			}
			set
			{
				this.emitAllowExternalOof = value;
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003410 File Offset: 0x00001610
		public ResponseType GetResponseType()
		{
			return ResponseType.GetUserOofSettingsResponseMessage;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003414 File Offset: 0x00001614
		public ResponseCodeType GetErrorCodeToLog()
		{
			if (this.ResponseMessage == null)
			{
				return ResponseCodeType.NoError;
			}
			return this.ResponseMessage.ResponseCode;
		}

		// Token: 0x04000020 RID: 32
		private ResponseMessage responseMessage;

		// Token: 0x04000021 RID: 33
		private UserOofSettings oofSettings;

		// Token: 0x04000022 RID: 34
		private string allowExternalOof = ExternalAudience.None.ToString();

		// Token: 0x04000023 RID: 35
		private bool emitAllowExternalOof = true;
	}
}
