using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessRequest
{
	// Token: 0x0200017E RID: 382
	[DesignerCategory("code")]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[XmlType(AnonymousType = true, Namespace = "DeltaSyncV2:")]
	[Serializable]
	public class StatelessCollectionCommandsChange
	{
		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x0001DA00 File Offset: 0x0001BC00
		// (set) Token: 0x06000AEA RID: 2794 RVA: 0x0001DA08 File Offset: 0x0001BC08
		public string ServerId
		{
			get
			{
				return this.serverIdField;
			}
			set
			{
				this.serverIdField = value;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x0001DA11 File Offset: 0x0001BC11
		// (set) Token: 0x06000AEC RID: 2796 RVA: 0x0001DA19 File Offset: 0x0001BC19
		[XmlElement(Namespace = "HMMAIL:")]
		public string SourceFolderId
		{
			get
			{
				return this.sourceFolderIdField;
			}
			set
			{
				this.sourceFolderIdField = value;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x0001DA22 File Offset: 0x0001BC22
		// (set) Token: 0x06000AEE RID: 2798 RVA: 0x0001DA2A File Offset: 0x0001BC2A
		public ApplicationDataTypeRequest ApplicationData
		{
			get
			{
				return this.applicationDataField;
			}
			set
			{
				this.applicationDataField = value;
			}
		}

		// Token: 0x0400062F RID: 1583
		private string serverIdField;

		// Token: 0x04000630 RID: 1584
		private string sourceFolderIdField;

		// Token: 0x04000631 RID: 1585
		private ApplicationDataTypeRequest applicationDataField;
	}
}
