using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000D0 RID: 208
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[Serializable]
	public class RetentionPolicyTagType
	{
		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x00020420 File Offset: 0x0001E620
		// (set) Token: 0x06000A03 RID: 2563 RVA: 0x00020428 File Offset: 0x0001E628
		public string DisplayName
		{
			get
			{
				return this.displayNameField;
			}
			set
			{
				this.displayNameField = value;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x00020431 File Offset: 0x0001E631
		// (set) Token: 0x06000A05 RID: 2565 RVA: 0x00020439 File Offset: 0x0001E639
		public string RetentionId
		{
			get
			{
				return this.retentionIdField;
			}
			set
			{
				this.retentionIdField = value;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x00020442 File Offset: 0x0001E642
		// (set) Token: 0x06000A07 RID: 2567 RVA: 0x0002044A File Offset: 0x0001E64A
		public int RetentionPeriod
		{
			get
			{
				return this.retentionPeriodField;
			}
			set
			{
				this.retentionPeriodField = value;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x00020453 File Offset: 0x0001E653
		// (set) Token: 0x06000A09 RID: 2569 RVA: 0x0002045B File Offset: 0x0001E65B
		public ElcFolderType Type
		{
			get
			{
				return this.typeField;
			}
			set
			{
				this.typeField = value;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x00020464 File Offset: 0x0001E664
		// (set) Token: 0x06000A0B RID: 2571 RVA: 0x0002046C File Offset: 0x0001E66C
		public RetentionActionType RetentionAction
		{
			get
			{
				return this.retentionActionField;
			}
			set
			{
				this.retentionActionField = value;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x00020475 File Offset: 0x0001E675
		// (set) Token: 0x06000A0D RID: 2573 RVA: 0x0002047D File Offset: 0x0001E67D
		public string Description
		{
			get
			{
				return this.descriptionField;
			}
			set
			{
				this.descriptionField = value;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x00020486 File Offset: 0x0001E686
		// (set) Token: 0x06000A0F RID: 2575 RVA: 0x0002048E File Offset: 0x0001E68E
		public bool IsVisible
		{
			get
			{
				return this.isVisibleField;
			}
			set
			{
				this.isVisibleField = value;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x00020497 File Offset: 0x0001E697
		// (set) Token: 0x06000A11 RID: 2577 RVA: 0x0002049F File Offset: 0x0001E69F
		public bool OptedInto
		{
			get
			{
				return this.optedIntoField;
			}
			set
			{
				this.optedIntoField = value;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x000204A8 File Offset: 0x0001E6A8
		// (set) Token: 0x06000A13 RID: 2579 RVA: 0x000204B0 File Offset: 0x0001E6B0
		public bool IsArchive
		{
			get
			{
				return this.isArchiveField;
			}
			set
			{
				this.isArchiveField = value;
			}
		}

		// Token: 0x040005B6 RID: 1462
		private string displayNameField;

		// Token: 0x040005B7 RID: 1463
		private string retentionIdField;

		// Token: 0x040005B8 RID: 1464
		private int retentionPeriodField;

		// Token: 0x040005B9 RID: 1465
		private ElcFolderType typeField;

		// Token: 0x040005BA RID: 1466
		private RetentionActionType retentionActionField;

		// Token: 0x040005BB RID: 1467
		private string descriptionField;

		// Token: 0x040005BC RID: 1468
		private bool isVisibleField;

		// Token: 0x040005BD RID: 1469
		private bool optedIntoField;

		// Token: 0x040005BE RID: 1470
		private bool isArchiveField;
	}
}
