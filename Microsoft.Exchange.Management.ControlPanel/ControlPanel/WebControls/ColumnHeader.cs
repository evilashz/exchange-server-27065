using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020001CA RID: 458
	public class ColumnHeader
	{
		// Token: 0x060024F0 RID: 9456 RVA: 0x00071AD1 File Offset: 0x0006FCD1
		public ColumnHeader()
		{
			this.IsSortable = true;
			this.EnableColumnResize = true;
			this.EnableColumnSelect = true;
			this.EnableExport = true;
		}

		// Token: 0x17001B56 RID: 6998
		// (get) Token: 0x060024F1 RID: 9457 RVA: 0x00071B0C File Offset: 0x0006FD0C
		// (set) Token: 0x060024F2 RID: 9458 RVA: 0x00071B14 File Offset: 0x0006FD14
		[DefaultValue(false)]
		public virtual bool AllowHTML { get; set; }

		// Token: 0x17001B57 RID: 6999
		// (get) Token: 0x060024F3 RID: 9459 RVA: 0x00071B1D File Offset: 0x0006FD1D
		// (set) Token: 0x060024F4 RID: 9460 RVA: 0x00071B25 File Offset: 0x0006FD25
		public string CssClass { get; set; }

		// Token: 0x17001B58 RID: 7000
		// (get) Token: 0x060024F5 RID: 9461 RVA: 0x00071B2E File Offset: 0x0006FD2E
		// (set) Token: 0x060024F6 RID: 9462 RVA: 0x00071B36 File Offset: 0x0006FD36
		[DefaultValue(true)]
		public bool IsSortable { get; set; }

		// Token: 0x17001B59 RID: 7001
		// (get) Token: 0x060024F7 RID: 9463 RVA: 0x00071B3F File Offset: 0x0006FD3F
		// (set) Token: 0x060024F8 RID: 9464 RVA: 0x00071B47 File Offset: 0x0006FD47
		[DefaultValue(false)]
		public bool DefaultOff { get; set; }

		// Token: 0x17001B5A RID: 7002
		// (get) Token: 0x060024F9 RID: 9465 RVA: 0x00071B50 File Offset: 0x0006FD50
		// (set) Token: 0x060024FA RID: 9466 RVA: 0x00071B58 File Offset: 0x0006FD58
		[DefaultValue(null)]
		public string EmptyText { get; set; }

		// Token: 0x17001B5B RID: 7003
		// (get) Token: 0x060024FB RID: 9467 RVA: 0x00071B61 File Offset: 0x0006FD61
		// (set) Token: 0x060024FC RID: 9468 RVA: 0x00071B69 File Offset: 0x0006FD69
		[DefaultValue(null)]
		public string Name { get; set; }

		// Token: 0x17001B5C RID: 7004
		// (get) Token: 0x060024FD RID: 9469 RVA: 0x00071B72 File Offset: 0x0006FD72
		// (set) Token: 0x060024FE RID: 9470 RVA: 0x00071B9C File Offset: 0x0006FD9C
		public string SortExpression
		{
			get
			{
				if (!this.IsSortable)
				{
					return string.Empty;
				}
				if (!string.IsNullOrEmpty(this.sortExpression))
				{
					return this.sortExpression;
				}
				return this.Name;
			}
			set
			{
				this.sortExpression = value;
			}
		}

		// Token: 0x17001B5D RID: 7005
		// (get) Token: 0x060024FF RID: 9471 RVA: 0x00071BA5 File Offset: 0x0006FDA5
		// (set) Token: 0x06002500 RID: 9472 RVA: 0x00071BAD File Offset: 0x0006FDAD
		[Localizable(true)]
		[DefaultValue(null)]
		public string Text { get; set; }

		// Token: 0x17001B5E RID: 7006
		// (get) Token: 0x06002501 RID: 9473 RVA: 0x00071BB6 File Offset: 0x0006FDB6
		// (set) Token: 0x06002502 RID: 9474 RVA: 0x00071BBE File Offset: 0x0006FDBE
		[DefaultValue("")]
		public virtual Unit Width { get; set; }

		// Token: 0x17001B5F RID: 7007
		// (get) Token: 0x06002503 RID: 9475 RVA: 0x00071BC7 File Offset: 0x0006FDC7
		// (set) Token: 0x06002504 RID: 9476 RVA: 0x00071BCF File Offset: 0x0006FDCF
		[DefaultValue(null)]
		public string FormatString { get; set; }

		// Token: 0x17001B60 RID: 7008
		// (get) Token: 0x06002505 RID: 9477 RVA: 0x00071BD8 File Offset: 0x0006FDD8
		// (set) Token: 0x06002506 RID: 9478 RVA: 0x00071BE0 File Offset: 0x0006FDE0
		[DefaultValue(true)]
		public bool EnableExport { get; set; }

		// Token: 0x17001B61 RID: 7009
		// (get) Token: 0x06002507 RID: 9479 RVA: 0x00071BE9 File Offset: 0x0006FDE9
		// (set) Token: 0x06002508 RID: 9480 RVA: 0x00071BF1 File Offset: 0x0006FDF1
		[DefaultValue(true)]
		public bool EnableColumnSelect { get; set; }

		// Token: 0x17001B62 RID: 7010
		// (get) Token: 0x06002509 RID: 9481 RVA: 0x00071BFA File Offset: 0x0006FDFA
		// (set) Token: 0x0600250A RID: 9482 RVA: 0x00071C02 File Offset: 0x0006FE02
		[DefaultValue(true)]
		public bool EnableColumnResize { get; set; }

		// Token: 0x17001B63 RID: 7011
		// (get) Token: 0x0600250B RID: 9483 RVA: 0x00071C0C File Offset: 0x0006FE0C
		public int Features
		{
			get
			{
				ColumnHeaderFlags columnHeaderFlags = (ColumnHeaderFlags)0;
				if (this.EnableExport)
				{
					columnHeaderFlags |= ColumnHeaderFlags.EnableExport;
				}
				if (this.EnableColumnSelect)
				{
					columnHeaderFlags |= ColumnHeaderFlags.EnableColumnSelect;
				}
				if (this.EnableColumnResize)
				{
					columnHeaderFlags |= ColumnHeaderFlags.EnableColumnResize;
				}
				if (this.DefaultOff)
				{
					columnHeaderFlags |= ColumnHeaderFlags.Defaultoff;
				}
				if (this.AllowHTML)
				{
					columnHeaderFlags |= ColumnHeaderFlags.AllowHTML;
				}
				return (int)columnHeaderFlags;
			}
		}

		// Token: 0x17001B64 RID: 7012
		// (get) Token: 0x0600250C RID: 9484 RVA: 0x00071C59 File Offset: 0x0006FE59
		// (set) Token: 0x0600250D RID: 9485 RVA: 0x00071C61 File Offset: 0x0006FE61
		public HorizontalAlign TextAlign
		{
			get
			{
				return this.textAlign;
			}
			set
			{
				this.textAlign = RtlUtil.GetHorizontalAlign(value);
			}
		}

		// Token: 0x17001B65 RID: 7013
		// (get) Token: 0x0600250E RID: 9486 RVA: 0x00071C6F File Offset: 0x0006FE6F
		// (set) Token: 0x0600250F RID: 9487 RVA: 0x00071C77 File Offset: 0x0006FE77
		public string Description { get; set; }

		// Token: 0x17001B66 RID: 7014
		// (get) Token: 0x06002510 RID: 9488 RVA: 0x00071C80 File Offset: 0x0006FE80
		// (set) Token: 0x06002511 RID: 9489 RVA: 0x00071C88 File Offset: 0x0006FE88
		public string Role { get; set; }

		// Token: 0x17001B67 RID: 7015
		// (get) Token: 0x06002512 RID: 9490 RVA: 0x00071C91 File Offset: 0x0006FE91
		// (set) Token: 0x06002513 RID: 9491 RVA: 0x00071C99 File Offset: 0x0006FE99
		[DefaultValue("")]
		protected virtual string SpanCssClass { get; set; }

		// Token: 0x06002514 RID: 9492 RVA: 0x00071CA4 File Offset: 0x0006FEA4
		public virtual string ToJavaScript()
		{
			return string.Format("new ColumnHeader(\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",{6},\"{7}\",{8})", new object[]
			{
				this.Name,
				this.SortExpression,
				this.FormatString,
				this.TextAlign.ToJavaScript(),
				HttpUtility.JavaScriptStringEncode(this.EmptyText),
				HttpUtility.JavaScriptStringEncode(this.Text),
				(this.Width == Unit.Empty) ? "\"auto\"" : this.Width.Value.ToString(),
				(this.Width == Unit.Empty) ? string.Empty : this.Width.Type.ToJavaScript(),
				this.Features
			});
		}

		// Token: 0x04001EB4 RID: 7860
		private string sortExpression = string.Empty;

		// Token: 0x04001EB5 RID: 7861
		private HorizontalAlign textAlign = RtlUtil.GetHorizontalAlign(HorizontalAlign.Left);
	}
}
