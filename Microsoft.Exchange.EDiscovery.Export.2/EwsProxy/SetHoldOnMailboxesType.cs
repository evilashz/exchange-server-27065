using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200033D RID: 829
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[Serializable]
	public class SetHoldOnMailboxesType : BaseRequestType
	{
		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06001AB3 RID: 6835 RVA: 0x000290FD File Offset: 0x000272FD
		// (set) Token: 0x06001AB4 RID: 6836 RVA: 0x00029105 File Offset: 0x00027305
		public HoldActionType ActionType
		{
			get
			{
				return this.actionTypeField;
			}
			set
			{
				this.actionTypeField = value;
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06001AB5 RID: 6837 RVA: 0x0002910E File Offset: 0x0002730E
		// (set) Token: 0x06001AB6 RID: 6838 RVA: 0x00029116 File Offset: 0x00027316
		public string HoldId
		{
			get
			{
				return this.holdIdField;
			}
			set
			{
				this.holdIdField = value;
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06001AB7 RID: 6839 RVA: 0x0002911F File Offset: 0x0002731F
		// (set) Token: 0x06001AB8 RID: 6840 RVA: 0x00029127 File Offset: 0x00027327
		public string Query
		{
			get
			{
				return this.queryField;
			}
			set
			{
				this.queryField = value;
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06001AB9 RID: 6841 RVA: 0x00029130 File Offset: 0x00027330
		// (set) Token: 0x06001ABA RID: 6842 RVA: 0x00029138 File Offset: 0x00027338
		[XmlArrayItem("String", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public string[] Mailboxes
		{
			get
			{
				return this.mailboxesField;
			}
			set
			{
				this.mailboxesField = value;
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06001ABB RID: 6843 RVA: 0x00029141 File Offset: 0x00027341
		// (set) Token: 0x06001ABC RID: 6844 RVA: 0x00029149 File Offset: 0x00027349
		public string Language
		{
			get
			{
				return this.languageField;
			}
			set
			{
				this.languageField = value;
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06001ABD RID: 6845 RVA: 0x00029152 File Offset: 0x00027352
		// (set) Token: 0x06001ABE RID: 6846 RVA: 0x0002915A File Offset: 0x0002735A
		public bool IncludeNonIndexableItems
		{
			get
			{
				return this.includeNonIndexableItemsField;
			}
			set
			{
				this.includeNonIndexableItemsField = value;
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06001ABF RID: 6847 RVA: 0x00029163 File Offset: 0x00027363
		// (set) Token: 0x06001AC0 RID: 6848 RVA: 0x0002916B File Offset: 0x0002736B
		[XmlIgnore]
		public bool IncludeNonIndexableItemsSpecified
		{
			get
			{
				return this.includeNonIndexableItemsFieldSpecified;
			}
			set
			{
				this.includeNonIndexableItemsFieldSpecified = value;
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06001AC1 RID: 6849 RVA: 0x00029174 File Offset: 0x00027374
		// (set) Token: 0x06001AC2 RID: 6850 RVA: 0x0002917C File Offset: 0x0002737C
		public bool Deduplication
		{
			get
			{
				return this.deduplicationField;
			}
			set
			{
				this.deduplicationField = value;
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06001AC3 RID: 6851 RVA: 0x00029185 File Offset: 0x00027385
		// (set) Token: 0x06001AC4 RID: 6852 RVA: 0x0002918D File Offset: 0x0002738D
		[XmlIgnore]
		public bool DeduplicationSpecified
		{
			get
			{
				return this.deduplicationFieldSpecified;
			}
			set
			{
				this.deduplicationFieldSpecified = value;
			}
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06001AC5 RID: 6853 RVA: 0x00029196 File Offset: 0x00027396
		// (set) Token: 0x06001AC6 RID: 6854 RVA: 0x0002919E File Offset: 0x0002739E
		public string InPlaceHoldIdentity
		{
			get
			{
				return this.inPlaceHoldIdentityField;
			}
			set
			{
				this.inPlaceHoldIdentityField = value;
			}
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06001AC7 RID: 6855 RVA: 0x000291A7 File Offset: 0x000273A7
		// (set) Token: 0x06001AC8 RID: 6856 RVA: 0x000291AF File Offset: 0x000273AF
		public string ItemHoldPeriod
		{
			get
			{
				return this.itemHoldPeriodField;
			}
			set
			{
				this.itemHoldPeriodField = value;
			}
		}

		// Token: 0x040011D9 RID: 4569
		private HoldActionType actionTypeField;

		// Token: 0x040011DA RID: 4570
		private string holdIdField;

		// Token: 0x040011DB RID: 4571
		private string queryField;

		// Token: 0x040011DC RID: 4572
		private string[] mailboxesField;

		// Token: 0x040011DD RID: 4573
		private string languageField;

		// Token: 0x040011DE RID: 4574
		private bool includeNonIndexableItemsField;

		// Token: 0x040011DF RID: 4575
		private bool includeNonIndexableItemsFieldSpecified;

		// Token: 0x040011E0 RID: 4576
		private bool deduplicationField;

		// Token: 0x040011E1 RID: 4577
		private bool deduplicationFieldSpecified;

		// Token: 0x040011E2 RID: 4578
		private string inPlaceHoldIdentityField;

		// Token: 0x040011E3 RID: 4579
		private string itemHoldPeriodField;
	}
}
