using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessResponse
{
	// Token: 0x02000195 RID: 405
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true, Namespace = "DeltaSyncV2:")]
	[DebuggerStepThrough]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[Serializable]
	public class StatelessCollectionCommands
	{
		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x0001DDE6 File Offset: 0x0001BFE6
		// (set) Token: 0x06000B5B RID: 2907 RVA: 0x0001DDEE File Offset: 0x0001BFEE
		[XmlElement(Namespace = "HMSYNC:")]
		public int Status
		{
			get
			{
				return this.statusField;
			}
			set
			{
				this.statusField = value;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x0001DDF7 File Offset: 0x0001BFF7
		// (set) Token: 0x06000B5D RID: 2909 RVA: 0x0001DDFF File Offset: 0x0001BFFF
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

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x0001DE08 File Offset: 0x0001C008
		// (set) Token: 0x06000B5F RID: 2911 RVA: 0x0001DE10 File Offset: 0x0001C010
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

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x0001DE19 File Offset: 0x0001C019
		// (set) Token: 0x06000B61 RID: 2913 RVA: 0x0001DE21 File Offset: 0x0001C021
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

		// Token: 0x0400068A RID: 1674
		private int statusField;

		// Token: 0x0400068B RID: 1675
		private StatelessCollectionCommandsDelete[] deleteField;

		// Token: 0x0400068C RID: 1676
		private StatelessCollectionCommandsChange[] changeField;

		// Token: 0x0400068D RID: 1677
		private StatelessCollectionCommandsAdd[] addField;
	}
}
