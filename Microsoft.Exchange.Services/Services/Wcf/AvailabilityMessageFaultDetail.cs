using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B6A RID: 2922
	internal class AvailabilityMessageFaultDetail : MessageFault
	{
		// Token: 0x060052BF RID: 21183 RVA: 0x0010BD74 File Offset: 0x00109F74
		internal AvailabilityMessageFaultDetail(LocalizedException exception, FaultParty faultParty)
		{
			this.serviceError = new ServiceError(exception.Message, (ResponseCodeType)exception.ErrorCode, 0, ExchangeVersion.Exchange2007);
			this.faultCode = this.GetFaultCode(faultParty);
			this.faultReason = new FaultReason(this.serviceError.MessageText);
		}

		// Token: 0x1700141C RID: 5148
		// (get) Token: 0x060052C0 RID: 21184 RVA: 0x0010BDC7 File Offset: 0x00109FC7
		public override FaultCode Code
		{
			get
			{
				return this.faultCode;
			}
		}

		// Token: 0x1700141D RID: 5149
		// (get) Token: 0x060052C1 RID: 21185 RVA: 0x0010BDCF File Offset: 0x00109FCF
		public override bool HasDetail
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700141E RID: 5150
		// (get) Token: 0x060052C2 RID: 21186 RVA: 0x0010BDD2 File Offset: 0x00109FD2
		public override FaultReason Reason
		{
			get
			{
				return this.faultReason;
			}
		}

		// Token: 0x060052C3 RID: 21187 RVA: 0x0010BDDC File Offset: 0x00109FDC
		private FaultCode GetFaultCode(FaultParty faultParty)
		{
			return (faultParty == FaultParty.Sender) ? FaultCode.CreateSenderFaultCode(this.serviceError.MessageKey.ToString(), "http://schemas.microsoft.com/exchange/services/2006/types") : FaultCode.CreateReceiverFaultCode(this.serviceError.MessageKey.ToString(), "http://schemas.microsoft.com/exchange/services/2006/types");
		}

		// Token: 0x060052C4 RID: 21188 RVA: 0x0010BE2E File Offset: 0x0010A02E
		protected override void OnWriteDetailContents(XmlDictionaryWriter writer)
		{
			if (this.HasDetail)
			{
				writer.WriteRaw(this.GetXmlDetailString());
			}
		}

		// Token: 0x060052C5 RID: 21189 RVA: 0x0010BE44 File Offset: 0x0010A044
		private string GetXmlDetailString()
		{
			SafeXmlDocument xmlDocument = new SafeXmlDocument();
			XmlElement xmlElement = ServiceXml.CreateElement(xmlDocument, "DummyNode", "http://schemas.microsoft.com/exchange/services/2006/messages");
			ServiceXml.CreateTextElement(xmlElement, "ErrorCode", ((int)this.serviceError.MessageKey).ToString());
			return xmlElement.InnerXml;
		}

		// Token: 0x04002E13 RID: 11795
		private FaultCode faultCode;

		// Token: 0x04002E14 RID: 11796
		private ServiceError serviceError;

		// Token: 0x04002E15 RID: 11797
		private FaultReason faultReason;
	}
}
