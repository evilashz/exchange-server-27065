using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Xml;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B76 RID: 2934
	internal class EwsMessageFaultDetail : MessageFault
	{
		// Token: 0x0600531D RID: 21277 RVA: 0x0010D820 File Offset: 0x0010BA20
		internal EwsMessageFaultDetail(LocalizedException exception, FaultParty faultParty, ExchangeVersion currentExchangeVersion)
		{
			this.serviceError = ServiceErrors.GetServiceError(exception, currentExchangeVersion);
			this.faultCode = this.GetFaultCode(faultParty);
			string text = this.serviceError.MessageText;
			if (this.serviceError.MessageKey == ResponseCodeType.ErrorSchemaValidation)
			{
				text = CoreResources.GetLocalizedString((CoreResources.IDs)4281412187U);
				if (exception.InnerException != null)
				{
					text += exception.InnerException.Message;
				}
			}
			this.faultReason = new FaultReason(text);
		}

		// Token: 0x17001424 RID: 5156
		// (get) Token: 0x0600531E RID: 21278 RVA: 0x0010D8A1 File Offset: 0x0010BAA1
		public override FaultCode Code
		{
			get
			{
				return this.faultCode;
			}
		}

		// Token: 0x17001425 RID: 5157
		// (get) Token: 0x0600531F RID: 21279 RVA: 0x0010D8A9 File Offset: 0x0010BAA9
		public override bool HasDetail
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001426 RID: 5158
		// (get) Token: 0x06005320 RID: 21280 RVA: 0x0010D8AC File Offset: 0x0010BAAC
		public override FaultReason Reason
		{
			get
			{
				return this.faultReason;
			}
		}

		// Token: 0x06005321 RID: 21281 RVA: 0x0010D8B4 File Offset: 0x0010BAB4
		private FaultCode GetFaultCode(FaultParty faultParty)
		{
			return (faultParty == FaultParty.Sender) ? FaultCode.CreateSenderFaultCode(this.serviceError.MessageKey.ToString(), "http://schemas.microsoft.com/exchange/services/2006/types") : FaultCode.CreateReceiverFaultCode(this.serviceError.MessageKey.ToString(), "http://schemas.microsoft.com/exchange/services/2006/types");
		}

		// Token: 0x06005322 RID: 21282 RVA: 0x0010D906 File Offset: 0x0010BB06
		protected override void OnWriteDetailContents(XmlDictionaryWriter writer)
		{
			if (this.HasDetail)
			{
				writer.WriteRaw(this.serviceError.GetAsXmlString());
			}
		}

		// Token: 0x04002E45 RID: 11845
		private FaultCode faultCode;

		// Token: 0x04002E46 RID: 11846
		private ServiceError serviceError;

		// Token: 0x04002E47 RID: 11847
		private FaultReason faultReason;
	}
}
