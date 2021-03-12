using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessRequest
{
	// Token: 0x02000176 RID: 374
	[DesignerCategory("code")]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[XmlType(AnonymousType = true, Namespace = "DeltaSyncV2:")]
	[Serializable]
	public class StatelessCollectionGetFilter
	{
		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x0001D926 File Offset: 0x0001BB26
		// (set) Token: 0x06000AD0 RID: 2768 RVA: 0x0001D92E File Offset: 0x0001BB2E
		[XmlElement("And", typeof(StatelessCollectionGetFilterAnd))]
		[XmlChoiceIdentifier("ItemElementName")]
		[XmlElement("Clause", typeof(Clause))]
		[XmlElement("Not", typeof(StatelessCollectionGetFilterNot))]
		[XmlElement("Or", typeof(StatelessCollectionGetFilterOR))]
		public object Item
		{
			get
			{
				return this.itemField;
			}
			set
			{
				this.itemField = value;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x0001D937 File Offset: 0x0001BB37
		// (set) Token: 0x06000AD2 RID: 2770 RVA: 0x0001D93F File Offset: 0x0001BB3F
		[XmlIgnore]
		public ItemChoiceType ItemElementName
		{
			get
			{
				return this.itemElementNameField;
			}
			set
			{
				this.itemElementNameField = value;
			}
		}

		// Token: 0x0400061D RID: 1565
		private object itemField;

		// Token: 0x0400061E RID: 1566
		private ItemChoiceType itemElementNameField;
	}
}
