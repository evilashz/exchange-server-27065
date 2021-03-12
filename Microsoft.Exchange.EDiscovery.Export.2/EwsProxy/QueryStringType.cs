using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002E6 RID: 742
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class QueryStringType
	{
		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06001912 RID: 6418 RVA: 0x0002834A File Offset: 0x0002654A
		// (set) Token: 0x06001913 RID: 6419 RVA: 0x00028352 File Offset: 0x00026552
		[XmlAttribute]
		public bool ResetCache
		{
			get
			{
				return this.resetCacheField;
			}
			set
			{
				this.resetCacheField = value;
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06001914 RID: 6420 RVA: 0x0002835B File Offset: 0x0002655B
		// (set) Token: 0x06001915 RID: 6421 RVA: 0x00028363 File Offset: 0x00026563
		[XmlIgnore]
		public bool ResetCacheSpecified
		{
			get
			{
				return this.resetCacheFieldSpecified;
			}
			set
			{
				this.resetCacheFieldSpecified = value;
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06001916 RID: 6422 RVA: 0x0002836C File Offset: 0x0002656C
		// (set) Token: 0x06001917 RID: 6423 RVA: 0x00028374 File Offset: 0x00026574
		[XmlAttribute]
		public bool ReturnHighlightTerms
		{
			get
			{
				return this.returnHighlightTermsField;
			}
			set
			{
				this.returnHighlightTermsField = value;
			}
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06001918 RID: 6424 RVA: 0x0002837D File Offset: 0x0002657D
		// (set) Token: 0x06001919 RID: 6425 RVA: 0x00028385 File Offset: 0x00026585
		[XmlIgnore]
		public bool ReturnHighlightTermsSpecified
		{
			get
			{
				return this.returnHighlightTermsFieldSpecified;
			}
			set
			{
				this.returnHighlightTermsFieldSpecified = value;
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x0600191A RID: 6426 RVA: 0x0002838E File Offset: 0x0002658E
		// (set) Token: 0x0600191B RID: 6427 RVA: 0x00028396 File Offset: 0x00026596
		[XmlAttribute]
		public bool ReturnDeletedItems
		{
			get
			{
				return this.returnDeletedItemsField;
			}
			set
			{
				this.returnDeletedItemsField = value;
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x0600191C RID: 6428 RVA: 0x0002839F File Offset: 0x0002659F
		// (set) Token: 0x0600191D RID: 6429 RVA: 0x000283A7 File Offset: 0x000265A7
		[XmlIgnore]
		public bool ReturnDeletedItemsSpecified
		{
			get
			{
				return this.returnDeletedItemsFieldSpecified;
			}
			set
			{
				this.returnDeletedItemsFieldSpecified = value;
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x0600191E RID: 6430 RVA: 0x000283B0 File Offset: 0x000265B0
		// (set) Token: 0x0600191F RID: 6431 RVA: 0x000283B8 File Offset: 0x000265B8
		[XmlText]
		public string Value
		{
			get
			{
				return this.valueField;
			}
			set
			{
				this.valueField = value;
			}
		}

		// Token: 0x040010FE RID: 4350
		private bool resetCacheField;

		// Token: 0x040010FF RID: 4351
		private bool resetCacheFieldSpecified;

		// Token: 0x04001100 RID: 4352
		private bool returnHighlightTermsField;

		// Token: 0x04001101 RID: 4353
		private bool returnHighlightTermsFieldSpecified;

		// Token: 0x04001102 RID: 4354
		private bool returnDeletedItemsField;

		// Token: 0x04001103 RID: 4355
		private bool returnDeletedItemsFieldSpecified;

		// Token: 0x04001104 RID: 4356
		private string valueField;
	}
}
