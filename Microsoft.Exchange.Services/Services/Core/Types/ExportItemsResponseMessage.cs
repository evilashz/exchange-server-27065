using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004D7 RID: 1239
	[XmlType("ExportItemsResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ExportItemsResponseMessage : ResponseMessage
	{
		// Token: 0x0600243A RID: 9274 RVA: 0x000A4949 File Offset: 0x000A2B49
		public ExportItemsResponseMessage()
		{
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x000A4951 File Offset: 0x000A2B51
		internal ExportItemsResponseMessage(ServiceResultCode code, ServiceError error, XmlNode item) : base(code, error)
		{
			this.itemXML = item;
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x000A4962 File Offset: 0x000A2B62
		internal ExportItemsResponseMessage(int itemIndex, ServiceCommandBase serviceCommand)
		{
			this.itemIndex = itemIndex;
			this.serviceCommand = serviceCommand;
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x0600243D RID: 9277 RVA: 0x000A4978 File Offset: 0x000A2B78
		// (set) Token: 0x0600243E RID: 9278 RVA: 0x000A4986 File Offset: 0x000A2B86
		[XmlAnyElement("ItemId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public XmlNode ItemId
		{
			get
			{
				base.ExecuteServiceCommandIfRequired();
				return this.itemId;
			}
			set
			{
				this.itemId = value;
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x0600243F RID: 9279 RVA: 0x000A498F File Offset: 0x000A2B8F
		// (set) Token: 0x06002440 RID: 9280 RVA: 0x000A499D File Offset: 0x000A2B9D
		[XmlAnyElement("Data", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public XmlNode Data
		{
			get
			{
				base.ExecuteServiceCommandIfRequired();
				return this.data;
			}
			set
			{
				this.data = value;
			}
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x000A49A8 File Offset: 0x000A2BA8
		protected override void InternalExecuteServiceCommand()
		{
			if (this.serviceCommand != null)
			{
				ServiceResult<XmlNode> serviceResult = this.CheckBatchProcessingErrorAndExecute();
				this.PopulateServiceResult(serviceResult);
			}
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x000A49CC File Offset: 0x000A2BCC
		private ServiceResult<XmlNode> CheckBatchProcessingErrorAndExecute()
		{
			ExportItems exportItems = (ExportItems)this.serviceCommand;
			bool flag = exportItems.IsPreviousResultStopBatchProcessingError(this.itemIndex);
			ServiceResult<XmlNode> result;
			if (flag)
			{
				result = new ServiceResult<XmlNode>(ServiceResultCode.Warning, null, ServiceError.CreateBatchProcessingStoppedError());
			}
			else
			{
				result = exportItems.ExportItemsResult(this.itemIndex);
			}
			return result;
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x000A4A14 File Offset: 0x000A2C14
		private void PopulateServiceResult(ServiceResult<XmlNode> serviceResult)
		{
			base.Initialize(serviceResult.Code, serviceResult.Error);
			this.itemXML = serviceResult.Value;
			if (this.itemXML != null)
			{
				foreach (object obj in this.itemXML.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (xmlNode.LocalName == "ItemId")
					{
						this.itemId = xmlNode;
					}
					else if (xmlNode.LocalName == "Data")
					{
						this.data = xmlNode;
					}
				}
			}
		}

		// Token: 0x0400156C RID: 5484
		private int itemIndex;

		// Token: 0x0400156D RID: 5485
		private ServiceCommandBase serviceCommand;

		// Token: 0x0400156E RID: 5486
		private XmlNode itemXML;

		// Token: 0x0400156F RID: 5487
		private XmlNode itemId;

		// Token: 0x04001570 RID: 5488
		private XmlNode data;
	}
}
