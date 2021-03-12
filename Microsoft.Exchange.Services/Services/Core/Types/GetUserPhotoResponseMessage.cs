using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000524 RID: 1316
	[DataContract(Name = "GetUserPhotoResponseMessage", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("GetUserPhotoResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public sealed class GetUserPhotoResponseMessage : ResponseMessage
	{
		// Token: 0x060025B7 RID: 9655 RVA: 0x000A5DF9 File Offset: 0x000A3FF9
		public GetUserPhotoResponseMessage()
		{
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x000A5E01 File Offset: 0x000A4001
		internal GetUserPhotoResponseMessage(ServiceResultCode code, ServiceError error, Stream result, bool hasChanged, string contentType) : base(code, error)
		{
			this.UserPhotoStream = result;
			this.pictureData = null;
			this.hasChanged = hasChanged;
			this.contentType = contentType;
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x060025B9 RID: 9657 RVA: 0x000A5E29 File Offset: 0x000A4029
		// (set) Token: 0x060025BA RID: 9658 RVA: 0x000A5E31 File Offset: 0x000A4031
		internal Stream UserPhotoStream { get; set; }

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x060025BB RID: 9659 RVA: 0x000A5E3A File Offset: 0x000A403A
		// (set) Token: 0x060025BC RID: 9660 RVA: 0x000A5E42 File Offset: 0x000A4042
		[DataMember(Name = "HasChanged", IsRequired = true)]
		[XmlElement("HasChanged")]
		public bool HasChanged
		{
			get
			{
				return this.hasChanged;
			}
			set
			{
				this.hasChanged = value;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x060025BD RID: 9661 RVA: 0x000A5E4C File Offset: 0x000A404C
		// (set) Token: 0x060025BE RID: 9662 RVA: 0x000A5E9C File Offset: 0x000A409C
		[XmlElement("PictureData")]
		[DataMember(Name = "PictureData", IsRequired = false)]
		public byte[] PictureData
		{
			get
			{
				if (this.pictureData == null && this.UserPhotoStream != null)
				{
					int num = (int)this.UserPhotoStream.Length;
					this.pictureData = new byte[num];
					this.UserPhotoStream.Read(this.pictureData, 0, num);
				}
				return this.pictureData;
			}
			set
			{
				this.pictureData = value;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x060025BF RID: 9663 RVA: 0x000A5EA5 File Offset: 0x000A40A5
		// (set) Token: 0x060025C0 RID: 9664 RVA: 0x000A5EAD File Offset: 0x000A40AD
		[DataMember(Name = "ContentType", IsRequired = false)]
		[XmlElement("ContentType")]
		public string ContentType
		{
			get
			{
				return this.contentType;
			}
			set
			{
				this.contentType = value;
			}
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x000A5EB6 File Offset: 0x000A40B6
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetUserPhotoResponseMessage;
		}

		// Token: 0x040015D3 RID: 5587
		private byte[] pictureData;

		// Token: 0x040015D4 RID: 5588
		private bool hasChanged;

		// Token: 0x040015D5 RID: 5589
		private string contentType;
	}
}
