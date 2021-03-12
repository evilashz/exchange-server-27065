using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200041E RID: 1054
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "FileAttachment")]
	[Serializable]
	public class FileAttachmentType : AttachmentType
	{
		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001E47 RID: 7751 RVA: 0x0009FE72 File Offset: 0x0009E072
		// (set) Token: 0x06001E48 RID: 7752 RVA: 0x0009FE88 File Offset: 0x0009E088
		[XmlElement(DataType = "base64Binary")]
		[IgnoreDataMember]
		public byte[] Content
		{
			get
			{
				if (this.ContentStream != null)
				{
					this.GetContentStreamBytes();
				}
				return this.contentBytes;
			}
			set
			{
				this.contentBytes = value;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001E49 RID: 7753 RVA: 0x0009FE91 File Offset: 0x0009E091
		// (set) Token: 0x06001E4A RID: 7754 RVA: 0x0009FEA8 File Offset: 0x0009E0A8
		[DataMember(Name = "Content", EmitDefaultValue = false)]
		[XmlIgnore]
		public string ContentString
		{
			get
			{
				if (this.Content != null)
				{
					return Convert.ToBase64String(this.Content);
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this.Content = Convert.FromBase64String(value);
					return;
				}
				this.Content = null;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06001E4B RID: 7755 RVA: 0x0009FEC1 File Offset: 0x0009E0C1
		// (set) Token: 0x06001E4C RID: 7756 RVA: 0x0009FEC9 File Offset: 0x0009E0C9
		internal Stream ContentStream { private get; set; }

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06001E4D RID: 7757 RVA: 0x0009FED2 File Offset: 0x0009E0D2
		// (set) Token: 0x06001E4E RID: 7758 RVA: 0x0009FEDA File Offset: 0x0009E0DA
		[DataMember(EmitDefaultValue = false)]
		public bool IsContactPhoto
		{
			get
			{
				return this.isContactPhoto;
			}
			set
			{
				this.IsContactPhotoSpecified = true;
				this.isContactPhoto = value;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06001E4F RID: 7759 RVA: 0x0009FEEA File Offset: 0x0009E0EA
		// (set) Token: 0x06001E50 RID: 7760 RVA: 0x0009FEF2 File Offset: 0x0009E0F2
		[XmlIgnore]
		[IgnoreDataMember]
		public bool IsContactPhotoSpecified { get; set; }

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06001E51 RID: 7761 RVA: 0x0009FEFB File Offset: 0x0009E0FB
		// (set) Token: 0x06001E52 RID: 7762 RVA: 0x0009FF03 File Offset: 0x0009E103
		[XmlArrayItem("byte", IsNullable = false)]
		[IgnoreDataMember]
		public byte[] ImageThumbnailSalientRegions
		{
			get
			{
				return this.imageThumbnailSalientRegions;
			}
			set
			{
				this.imageThumbnailSalientRegions = value;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06001E53 RID: 7763 RVA: 0x0009FF0C File Offset: 0x0009E10C
		// (set) Token: 0x06001E54 RID: 7764 RVA: 0x0009FF14 File Offset: 0x0009E114
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false)]
		public int ImageThumbnailHeight
		{
			get
			{
				return this.imageThumbnailHeight;
			}
			set
			{
				this.imageThumbnailHeight = value;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001E55 RID: 7765 RVA: 0x0009FF1D File Offset: 0x0009E11D
		// (set) Token: 0x06001E56 RID: 7766 RVA: 0x0009FF25 File Offset: 0x0009E125
		[DataMember(EmitDefaultValue = false)]
		[XmlIgnore]
		public int ImageThumbnailWidth
		{
			get
			{
				return this.imageThumbnailWidth;
			}
			set
			{
				this.imageThumbnailWidth = value;
			}
		}

		// Token: 0x06001E57 RID: 7767 RVA: 0x0009FF30 File Offset: 0x0009E130
		private void GetContentStreamBytes()
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				byte[] array = new byte[4096];
				int count;
				while ((count = this.ContentStream.Read(array, 0, array.Length)) != 0)
				{
					memoryStream.Write(array, 0, count);
				}
				this.contentBytes = memoryStream.ToArray();
				this.ContentStream = null;
			}
		}

		// Token: 0x0400136F RID: 4975
		private byte[] contentBytes;

		// Token: 0x04001370 RID: 4976
		private bool isContactPhoto;

		// Token: 0x04001371 RID: 4977
		private byte[] imageThumbnailSalientRegions;

		// Token: 0x04001372 RID: 4978
		private int imageThumbnailHeight;

		// Token: 0x04001373 RID: 4979
		private int imageThumbnailWidth;
	}
}
