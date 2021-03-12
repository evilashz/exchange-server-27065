using System;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000DA9 RID: 3497
	internal class UmMessageFaultDetail : MessageFault
	{
		// Token: 0x060058BE RID: 22718 RVA: 0x001144CC File Offset: 0x001126CC
		internal UmMessageFaultDetail(LocalizedException exception, FaultParty faultParty)
		{
			if (exception is ObjectNotFoundException)
			{
				this.ExitFast();
				return;
			}
			this.exceptionTypeName = exception.GetType().Name;
			this.faultCode = FaultCode.CreateSenderFaultCode(new FaultCode(this.exceptionTypeName, "http://schemas.microsoft.com/exchange/services/2006/errors"));
			ServiceError serviceError = new ServiceError(exception.Message, ResponseCodeType.ErrorInternalServerError, 0, ExchangeVersion.Exchange2007);
			this.faultReason = new FaultReason(serviceError.MessageText);
		}

		// Token: 0x1700145E RID: 5214
		// (get) Token: 0x060058BF RID: 22719 RVA: 0x00114542 File Offset: 0x00112742
		public override FaultCode Code
		{
			get
			{
				return this.faultCode;
			}
		}

		// Token: 0x1700145F RID: 5215
		// (get) Token: 0x060058C0 RID: 22720 RVA: 0x0011454A File Offset: 0x0011274A
		public override bool HasDetail
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001460 RID: 5216
		// (get) Token: 0x060058C1 RID: 22721 RVA: 0x0011454D File Offset: 0x0011274D
		public override FaultReason Reason
		{
			get
			{
				return this.faultReason;
			}
		}

		// Token: 0x060058C2 RID: 22722 RVA: 0x00114555 File Offset: 0x00112755
		private void ExitFast()
		{
			BailOut.SetHTTPStatusAndClose(HttpStatusCode.Unauthorized);
		}

		// Token: 0x060058C3 RID: 22723 RVA: 0x00114561 File Offset: 0x00112761
		protected override void OnWriteDetailContents(XmlDictionaryWriter writer)
		{
			if (this.HasDetail)
			{
				writer.WriteRaw(this.GetXmlDetailString());
			}
		}

		// Token: 0x060058C4 RID: 22724 RVA: 0x00114578 File Offset: 0x00112778
		private string GetXmlDetailString()
		{
			SafeXmlDocument xmlDocument = new SafeXmlDocument();
			XmlElement xmlElement = ServiceXml.CreateElement(xmlDocument, "DummyNode", "http://schemas.microsoft.com/exchange/services/2006/messages");
			ServiceXml.CreateTextElement(xmlElement, "ExceptionType", this.exceptionTypeName);
			return xmlElement.InnerXml;
		}

		// Token: 0x0400313F RID: 12607
		private FaultCode faultCode;

		// Token: 0x04003140 RID: 12608
		private FaultReason faultReason;

		// Token: 0x04003141 RID: 12609
		private string exceptionTypeName;
	}
}
