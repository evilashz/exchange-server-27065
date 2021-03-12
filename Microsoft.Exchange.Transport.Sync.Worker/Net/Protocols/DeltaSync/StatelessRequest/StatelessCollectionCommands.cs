using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessRequest
{
	// Token: 0x0200017D RID: 381
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[XmlType(AnonymousType = true, Namespace = "DeltaSyncV2:")]
	[Serializable]
	public class StatelessCollectionCommands
	{
		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x0001D9C5 File Offset: 0x0001BBC5
		// (set) Token: 0x06000AE3 RID: 2787 RVA: 0x0001D9CD File Offset: 0x0001BBCD
		[XmlElement("Change")]
		public StatelessCollectionCommandsChange[] Change
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

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x0001D9D6 File Offset: 0x0001BBD6
		// (set) Token: 0x06000AE5 RID: 2789 RVA: 0x0001D9DE File Offset: 0x0001BBDE
		[XmlElement("Delete")]
		public StatelessCollectionCommandsDelete[] Delete
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

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x0001D9E7 File Offset: 0x0001BBE7
		// (set) Token: 0x06000AE7 RID: 2791 RVA: 0x0001D9EF File Offset: 0x0001BBEF
		[XmlElement("Add")]
		public StatelessCollectionCommandsAdd[] Add
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

		// Token: 0x0400062C RID: 1580
		private StatelessCollectionCommandsChange[] changeField;

		// Token: 0x0400062D RID: 1581
		private StatelessCollectionCommandsDelete[] deleteField;

		// Token: 0x0400062E RID: 1582
		private StatelessCollectionCommandsAdd[] addField;
	}
}
