using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000362 RID: 866
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ConvertIdType : BaseRequestType
	{
		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06001BB8 RID: 7096 RVA: 0x00029999 File Offset: 0x00027B99
		// (set) Token: 0x06001BB9 RID: 7097 RVA: 0x000299A1 File Offset: 0x00027BA1
		[XmlArrayItem("AlternatePublicFolderId", typeof(AlternatePublicFolderIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("AlternateId", typeof(AlternateIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("AlternatePublicFolderItemId", typeof(AlternatePublicFolderItemIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public AlternateIdBaseType[] SourceIds
		{
			get
			{
				return this.sourceIdsField;
			}
			set
			{
				this.sourceIdsField = value;
			}
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06001BBA RID: 7098 RVA: 0x000299AA File Offset: 0x00027BAA
		// (set) Token: 0x06001BBB RID: 7099 RVA: 0x000299B2 File Offset: 0x00027BB2
		[XmlAttribute]
		public IdFormatType DestinationFormat
		{
			get
			{
				return this.destinationFormatField;
			}
			set
			{
				this.destinationFormatField = value;
			}
		}

		// Token: 0x04001278 RID: 4728
		private AlternateIdBaseType[] sourceIdsField;

		// Token: 0x04001279 RID: 4729
		private IdFormatType destinationFormatField;
	}
}
