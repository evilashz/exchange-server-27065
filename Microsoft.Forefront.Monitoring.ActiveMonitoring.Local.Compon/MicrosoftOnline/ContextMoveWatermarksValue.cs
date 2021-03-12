using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000FB RID: 251
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[Serializable]
	public class ContextMoveWatermarksValue
	{
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x0001FE1E File Offset: 0x0001E01E
		// (set) Token: 0x060007A2 RID: 1954 RVA: 0x0001FE26 File Offset: 0x0001E026
		[XmlElement(DataType = "base64Binary")]
		public byte[] Source
		{
			get
			{
				return this.sourceField;
			}
			set
			{
				this.sourceField = value;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x0001FE2F File Offset: 0x0001E02F
		// (set) Token: 0x060007A4 RID: 1956 RVA: 0x0001FE37 File Offset: 0x0001E037
		[XmlElement(DataType = "base64Binary")]
		public byte[] Target
		{
			get
			{
				return this.targetField;
			}
			set
			{
				this.targetField = value;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x0001FE40 File Offset: 0x0001E040
		// (set) Token: 0x060007A6 RID: 1958 RVA: 0x0001FE48 File Offset: 0x0001E048
		[XmlArrayItem("SourceSubscriberFilterVersion", IsNullable = false)]
		public ContextMoveWatermarksValueSourceSubscriberFilterVersion[] SourceSubscriberFilterVersions
		{
			get
			{
				return this.sourceSubscriberFilterVersionsField;
			}
			set
			{
				this.sourceSubscriberFilterVersionsField = value;
			}
		}

		// Token: 0x040003EC RID: 1004
		private byte[] sourceField;

		// Token: 0x040003ED RID: 1005
		private byte[] targetField;

		// Token: 0x040003EE RID: 1006
		private ContextMoveWatermarksValueSourceSubscriberFilterVersion[] sourceSubscriberFilterVersionsField;
	}
}
