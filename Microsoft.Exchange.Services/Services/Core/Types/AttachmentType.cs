using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003FA RID: 1018
	[XmlInclude(typeof(FileAttachmentType))]
	[XmlInclude(typeof(ItemAttachmentType))]
	[XmlInclude(typeof(ReferenceAttachmentType))]
	[KnownType(typeof(ReferenceAttachmentType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[KnownType(typeof(FileAttachmentType))]
	[KnownType(typeof(ItemAttachmentType))]
	[KnownType(typeof(ItemIdAttachmentType))]
	[Serializable]
	public class AttachmentType
	{
		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001CBA RID: 7354 RVA: 0x0009E769 File Offset: 0x0009C969
		// (set) Token: 0x06001CBB RID: 7355 RVA: 0x0009E771 File Offset: 0x0009C971
		[DataMember(EmitDefaultValue = false, IsRequired = false)]
		[XmlElement("AttachmentId")]
		public AttachmentIdType AttachmentId { get; set; }

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06001CBC RID: 7356 RVA: 0x0009E77A File Offset: 0x0009C97A
		// (set) Token: 0x06001CBD RID: 7357 RVA: 0x0009E782 File Offset: 0x0009C982
		[DataMember(EmitDefaultValue = false, IsRequired = false)]
		public string Name { get; set; }

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06001CBE RID: 7358 RVA: 0x0009E78B File Offset: 0x0009C98B
		// (set) Token: 0x06001CBF RID: 7359 RVA: 0x0009E793 File Offset: 0x0009C993
		[DataMember(EmitDefaultValue = false, IsRequired = false)]
		public string ContentType { get; set; }

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001CC0 RID: 7360 RVA: 0x0009E79C File Offset: 0x0009C99C
		// (set) Token: 0x06001CC1 RID: 7361 RVA: 0x0009E7A4 File Offset: 0x0009C9A4
		[DataMember(EmitDefaultValue = false, IsRequired = false)]
		public string ContentId { get; set; }

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001CC2 RID: 7362 RVA: 0x0009E7AD File Offset: 0x0009C9AD
		// (set) Token: 0x06001CC3 RID: 7363 RVA: 0x0009E7B5 File Offset: 0x0009C9B5
		[DataMember(EmitDefaultValue = false, IsRequired = false)]
		public string ContentLocation { get; set; }

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06001CC4 RID: 7364 RVA: 0x0009E7BE File Offset: 0x0009C9BE
		// (set) Token: 0x06001CC5 RID: 7365 RVA: 0x0009E7C6 File Offset: 0x0009C9C6
		[DataMember(EmitDefaultValue = false, IsRequired = false)]
		public int Size
		{
			get
			{
				return this.size;
			}
			set
			{
				this.SizeSpecified = true;
				this.size = value;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06001CC6 RID: 7366 RVA: 0x0009E7D6 File Offset: 0x0009C9D6
		// (set) Token: 0x06001CC7 RID: 7367 RVA: 0x0009E7DE File Offset: 0x0009C9DE
		[IgnoreDataMember]
		[XmlIgnore]
		public bool SizeSpecified { get; set; }

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06001CC8 RID: 7368 RVA: 0x0009E7E7 File Offset: 0x0009C9E7
		// (set) Token: 0x06001CC9 RID: 7369 RVA: 0x0009E7EF File Offset: 0x0009C9EF
		[DateTimeString]
		[DataMember(EmitDefaultValue = false, IsRequired = false)]
		public string LastModifiedTime
		{
			get
			{
				return this.lastModifiedTime;
			}
			set
			{
				this.LastModifiedTimeSpecified = true;
				this.lastModifiedTime = value;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06001CCA RID: 7370 RVA: 0x0009E7FF File Offset: 0x0009C9FF
		// (set) Token: 0x06001CCB RID: 7371 RVA: 0x0009E807 File Offset: 0x0009CA07
		[IgnoreDataMember]
		[XmlIgnore]
		public bool LastModifiedTimeSpecified { get; set; }

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06001CCC RID: 7372 RVA: 0x0009E810 File Offset: 0x0009CA10
		// (set) Token: 0x06001CCD RID: 7373 RVA: 0x0009E818 File Offset: 0x0009CA18
		[DataMember(EmitDefaultValue = false, IsRequired = false)]
		public bool IsInline
		{
			get
			{
				return this.isInline;
			}
			set
			{
				this.IsInlineSpecified = true;
				this.isInline = value;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06001CCE RID: 7374 RVA: 0x0009E828 File Offset: 0x0009CA28
		// (set) Token: 0x06001CCF RID: 7375 RVA: 0x0009E830 File Offset: 0x0009CA30
		[IgnoreDataMember]
		[XmlIgnore]
		public bool IsInlineSpecified { get; set; }

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06001CD0 RID: 7376 RVA: 0x0009E839 File Offset: 0x0009CA39
		// (set) Token: 0x06001CD1 RID: 7377 RVA: 0x0009E841 File Offset: 0x0009CA41
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false, IsRequired = false)]
		public bool IsInlineToUniqueBody { get; set; }

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06001CD2 RID: 7378 RVA: 0x0009E84A File Offset: 0x0009CA4A
		// (set) Token: 0x06001CD3 RID: 7379 RVA: 0x0009E852 File Offset: 0x0009CA52
		[DataMember(EmitDefaultValue = false, IsRequired = false)]
		[XmlIgnore]
		public bool IsInlineToNormalBody { get; set; }

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06001CD4 RID: 7380 RVA: 0x0009E85B File Offset: 0x0009CA5B
		// (set) Token: 0x06001CD5 RID: 7381 RVA: 0x0009E863 File Offset: 0x0009CA63
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false, IsRequired = false)]
		public string ThumbnailMimeType { get; set; }

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06001CD6 RID: 7382 RVA: 0x0009E86C File Offset: 0x0009CA6C
		// (set) Token: 0x06001CD7 RID: 7383 RVA: 0x0009E874 File Offset: 0x0009CA74
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false, IsRequired = false)]
		public string Thumbnail { get; set; }

		// Token: 0x040012E1 RID: 4833
		private int size;

		// Token: 0x040012E2 RID: 4834
		private string lastModifiedTime;

		// Token: 0x040012E3 RID: 4835
		private bool isInline;
	}
}
