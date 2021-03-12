using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000160 RID: 352
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class PostItemType : ItemType
	{
		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x000235F2 File Offset: 0x000217F2
		// (set) Token: 0x06000FE6 RID: 4070 RVA: 0x000235FA File Offset: 0x000217FA
		[XmlElement(DataType = "base64Binary")]
		public byte[] ConversationIndex
		{
			get
			{
				return this.conversationIndexField;
			}
			set
			{
				this.conversationIndexField = value;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x00023603 File Offset: 0x00021803
		// (set) Token: 0x06000FE8 RID: 4072 RVA: 0x0002360B File Offset: 0x0002180B
		public string ConversationTopic
		{
			get
			{
				return this.conversationTopicField;
			}
			set
			{
				this.conversationTopicField = value;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x00023614 File Offset: 0x00021814
		// (set) Token: 0x06000FEA RID: 4074 RVA: 0x0002361C File Offset: 0x0002181C
		public SingleRecipientType From
		{
			get
			{
				return this.fromField;
			}
			set
			{
				this.fromField = value;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06000FEB RID: 4075 RVA: 0x00023625 File Offset: 0x00021825
		// (set) Token: 0x06000FEC RID: 4076 RVA: 0x0002362D File Offset: 0x0002182D
		public string InternetMessageId
		{
			get
			{
				return this.internetMessageIdField;
			}
			set
			{
				this.internetMessageIdField = value;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06000FED RID: 4077 RVA: 0x00023636 File Offset: 0x00021836
		// (set) Token: 0x06000FEE RID: 4078 RVA: 0x0002363E File Offset: 0x0002183E
		public bool IsRead
		{
			get
			{
				return this.isReadField;
			}
			set
			{
				this.isReadField = value;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06000FEF RID: 4079 RVA: 0x00023647 File Offset: 0x00021847
		// (set) Token: 0x06000FF0 RID: 4080 RVA: 0x0002364F File Offset: 0x0002184F
		[XmlIgnore]
		public bool IsReadSpecified
		{
			get
			{
				return this.isReadFieldSpecified;
			}
			set
			{
				this.isReadFieldSpecified = value;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06000FF1 RID: 4081 RVA: 0x00023658 File Offset: 0x00021858
		// (set) Token: 0x06000FF2 RID: 4082 RVA: 0x00023660 File Offset: 0x00021860
		public DateTime PostedTime
		{
			get
			{
				return this.postedTimeField;
			}
			set
			{
				this.postedTimeField = value;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x00023669 File Offset: 0x00021869
		// (set) Token: 0x06000FF4 RID: 4084 RVA: 0x00023671 File Offset: 0x00021871
		[XmlIgnore]
		public bool PostedTimeSpecified
		{
			get
			{
				return this.postedTimeFieldSpecified;
			}
			set
			{
				this.postedTimeFieldSpecified = value;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06000FF5 RID: 4085 RVA: 0x0002367A File Offset: 0x0002187A
		// (set) Token: 0x06000FF6 RID: 4086 RVA: 0x00023682 File Offset: 0x00021882
		public string References
		{
			get
			{
				return this.referencesField;
			}
			set
			{
				this.referencesField = value;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x0002368B File Offset: 0x0002188B
		// (set) Token: 0x06000FF8 RID: 4088 RVA: 0x00023693 File Offset: 0x00021893
		public SingleRecipientType Sender
		{
			get
			{
				return this.senderField;
			}
			set
			{
				this.senderField = value;
			}
		}

		// Token: 0x04000AEE RID: 2798
		private byte[] conversationIndexField;

		// Token: 0x04000AEF RID: 2799
		private string conversationTopicField;

		// Token: 0x04000AF0 RID: 2800
		private SingleRecipientType fromField;

		// Token: 0x04000AF1 RID: 2801
		private string internetMessageIdField;

		// Token: 0x04000AF2 RID: 2802
		private bool isReadField;

		// Token: 0x04000AF3 RID: 2803
		private bool isReadFieldSpecified;

		// Token: 0x04000AF4 RID: 2804
		private DateTime postedTimeField;

		// Token: 0x04000AF5 RID: 2805
		private bool postedTimeFieldSpecified;

		// Token: 0x04000AF6 RID: 2806
		private string referencesField;

		// Token: 0x04000AF7 RID: 2807
		private SingleRecipientType senderField;
	}
}
