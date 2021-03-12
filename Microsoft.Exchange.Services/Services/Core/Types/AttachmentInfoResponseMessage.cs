using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004B3 RID: 1203
	[XmlType("AttachmentInfoResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AttachmentInfoResponseMessage : ResponseMessage
	{
		// Token: 0x060023D2 RID: 9170 RVA: 0x000A43D3 File Offset: 0x000A25D3
		public AttachmentInfoResponseMessage()
		{
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x000A43DB File Offset: 0x000A25DB
		internal AttachmentInfoResponseMessage(ServiceResultCode code, ServiceError error, AttachmentType attachment) : base(code, error)
		{
			this.attachments = new List<AttachmentType>();
			this.attachments.Add(attachment);
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x000A43FC File Offset: 0x000A25FC
		internal AttachmentInfoResponseMessage(int itemIndex, ServiceCommandBase serviceCommand)
		{
			this.itemIndex = itemIndex;
			this.serviceCommand = serviceCommand;
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x060023D5 RID: 9173 RVA: 0x000A4412 File Offset: 0x000A2612
		// (set) Token: 0x060023D6 RID: 9174 RVA: 0x000A4425 File Offset: 0x000A2625
		[XmlArray("Attachments", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(EmitDefaultValue = false, Name = "Attachments")]
		[XmlArrayItem(ElementName = "ItemAttachment", Type = typeof(ItemAttachmentType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[XmlArrayItem(ElementName = "FileAttachment", Type = typeof(FileAttachmentType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[XmlArrayItem(ElementName = "ReferenceAttachment", Type = typeof(ReferenceAttachmentType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		public AttachmentType[] Attachments
		{
			get
			{
				base.ExecuteServiceCommandIfRequired();
				return this.attachments.ToArray();
			}
			set
			{
				this.attachments = new List<AttachmentType>(value);
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x060023D7 RID: 9175 RVA: 0x000A4433 File Offset: 0x000A2633
		// (set) Token: 0x060023D8 RID: 9176 RVA: 0x000A443B File Offset: 0x000A263B
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false)]
		public bool IsCancelled
		{
			get
			{
				return this.isCancelled;
			}
			set
			{
				this.isCancelled = value;
			}
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x000A4444 File Offset: 0x000A2644
		protected override void InternalExecuteServiceCommand()
		{
			if (this.serviceCommand != null)
			{
				ServiceResult<AttachmentType> serviceResult = this.CheckBatchProcessingErrorAndExecute();
				this.PopulateServiceResult(serviceResult);
			}
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x000A4468 File Offset: 0x000A2668
		private ServiceResult<AttachmentType> CheckBatchProcessingErrorAndExecute()
		{
			GetAttachment getAttachment = (GetAttachment)this.serviceCommand;
			bool flag = getAttachment.IsPreviousResultStopBatchProcessingError(this.itemIndex);
			ServiceResult<AttachmentType> result;
			if (flag)
			{
				result = new ServiceResult<AttachmentType>(ServiceResultCode.Warning, null, ServiceError.CreateBatchProcessingStoppedError());
			}
			else
			{
				result = getAttachment.GetAttachmentResult(this.itemIndex);
			}
			return result;
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x000A44B0 File Offset: 0x000A26B0
		private void PopulateServiceResult(ServiceResult<AttachmentType> serviceResult)
		{
			base.Initialize(serviceResult.Code, serviceResult.Error);
			this.attachments = new List<AttachmentType>();
			this.attachments.Add(serviceResult.Value);
		}

		// Token: 0x0400155D RID: 5469
		private int itemIndex;

		// Token: 0x0400155E RID: 5470
		private bool isCancelled;

		// Token: 0x0400155F RID: 5471
		private ServiceCommandBase serviceCommand;

		// Token: 0x04001560 RID: 5472
		private List<AttachmentType> attachments;
	}
}
