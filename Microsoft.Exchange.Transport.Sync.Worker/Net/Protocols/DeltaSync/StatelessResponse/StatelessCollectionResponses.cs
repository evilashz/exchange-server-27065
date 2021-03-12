using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessResponse
{
	// Token: 0x02000199 RID: 409
	[XmlType(AnonymousType = true, Namespace = "DeltaSyncV2:")]
	[DesignerCategory("code")]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[Serializable]
	public class StatelessCollectionResponses
	{
		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x0001DE9F File Offset: 0x0001C09F
		// (set) Token: 0x06000B71 RID: 2929 RVA: 0x0001DEA7 File Offset: 0x0001C0A7
		[XmlElement("Change")]
		public StatelessCollectionResponsesChange[] Change
		{
			get
			{
				return this.changeField;
			}
			set
			{
				this.changeField = value;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x0001DEB0 File Offset: 0x0001C0B0
		// (set) Token: 0x06000B73 RID: 2931 RVA: 0x0001DEB8 File Offset: 0x0001C0B8
		[XmlElement("Delete")]
		public StatelessCollectionResponsesDelete[] Delete
		{
			get
			{
				return this.deleteField;
			}
			set
			{
				this.deleteField = value;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x0001DEC1 File Offset: 0x0001C0C1
		// (set) Token: 0x06000B75 RID: 2933 RVA: 0x0001DEC9 File Offset: 0x0001C0C9
		[XmlElement("Add")]
		public StatelessCollectionResponsesAdd[] Add
		{
			get
			{
				return this.addField;
			}
			set
			{
				this.addField = value;
			}
		}

		// Token: 0x04000693 RID: 1683
		private StatelessCollectionResponsesChange[] changeField;

		// Token: 0x04000694 RID: 1684
		private StatelessCollectionResponsesDelete[] deleteField;

		// Token: 0x04000695 RID: 1685
		private StatelessCollectionResponsesAdd[] addField;
	}
}
