using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000285 RID: 645
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class SearchPreviewItemType
	{
		// Token: 0x04001079 RID: 4217
		public ItemIdType Id;

		// Token: 0x0400107A RID: 4218
		public PreviewItemMailboxType Mailbox;

		// Token: 0x0400107B RID: 4219
		public ItemIdType ParentId;

		// Token: 0x0400107C RID: 4220
		public string ItemClass;

		// Token: 0x0400107D RID: 4221
		public string UniqueHash;

		// Token: 0x0400107E RID: 4222
		public string SortValue;

		// Token: 0x0400107F RID: 4223
		public string OwaLink;

		// Token: 0x04001080 RID: 4224
		public string Sender;

		// Token: 0x04001081 RID: 4225
		[XmlArrayItem("SmtpAddress", IsNullable = false)]
		public string[] ToRecipients;

		// Token: 0x04001082 RID: 4226
		[XmlArrayItem("SmtpAddress", IsNullable = false)]
		public string[] CcRecipients;

		// Token: 0x04001083 RID: 4227
		[XmlArrayItem("SmtpAddress", IsNullable = false)]
		public string[] BccRecipients;

		// Token: 0x04001084 RID: 4228
		public DateTime CreatedTime;

		// Token: 0x04001085 RID: 4229
		[XmlIgnore]
		public bool CreatedTimeSpecified;

		// Token: 0x04001086 RID: 4230
		public DateTime ReceivedTime;

		// Token: 0x04001087 RID: 4231
		[XmlIgnore]
		public bool ReceivedTimeSpecified;

		// Token: 0x04001088 RID: 4232
		public DateTime SentTime;

		// Token: 0x04001089 RID: 4233
		[XmlIgnore]
		public bool SentTimeSpecified;

		// Token: 0x0400108A RID: 4234
		public string Subject;

		// Token: 0x0400108B RID: 4235
		public long Size;

		// Token: 0x0400108C RID: 4236
		[XmlIgnore]
		public bool SizeSpecified;

		// Token: 0x0400108D RID: 4237
		public string Preview;

		// Token: 0x0400108E RID: 4238
		public ImportanceChoicesType Importance;

		// Token: 0x0400108F RID: 4239
		[XmlIgnore]
		public bool ImportanceSpecified;

		// Token: 0x04001090 RID: 4240
		public bool Read;

		// Token: 0x04001091 RID: 4241
		[XmlIgnore]
		public bool ReadSpecified;

		// Token: 0x04001092 RID: 4242
		public bool HasAttachment;

		// Token: 0x04001093 RID: 4243
		[XmlIgnore]
		public bool HasAttachmentSpecified;

		// Token: 0x04001094 RID: 4244
		public NonEmptyArrayOfExtendedPropertyType ExtendedProperties;
	}
}
