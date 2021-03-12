using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005F4 RID: 1524
	public class ImageColumnHeader : ColumnHeader
	{
		// Token: 0x06004454 RID: 17492 RVA: 0x000CE9AC File Offset: 0x000CCBAC
		public ImageColumnHeader()
		{
			this.SpanCssClass = "ImgColumnSpan";
		}

		// Token: 0x17002666 RID: 9830
		// (get) Token: 0x06004455 RID: 17493 RVA: 0x000CE9BF File Offset: 0x000CCBBF
		// (set) Token: 0x06004456 RID: 17494 RVA: 0x000CE9C2 File Offset: 0x000CCBC2
		[DefaultValue(true)]
		public override bool AllowHTML
		{
			get
			{
				return true;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17002667 RID: 9831
		// (get) Token: 0x06004457 RID: 17495 RVA: 0x000CE9C9 File Offset: 0x000CCBC9
		// (set) Token: 0x06004458 RID: 17496 RVA: 0x000CE9D1 File Offset: 0x000CCBD1
		[DefaultValue(null)]
		public virtual string AlternateTextProperty { get; set; }

		// Token: 0x17002668 RID: 9832
		// (get) Token: 0x06004459 RID: 17497 RVA: 0x000CE9DA File Offset: 0x000CCBDA
		// (set) Token: 0x0600445A RID: 17498 RVA: 0x000CE9E2 File Offset: 0x000CCBE2
		[DefaultValue(null)]
		public virtual string DescriptionProperty { get; set; }

		// Token: 0x17002669 RID: 9833
		// (get) Token: 0x0600445B RID: 17499 RVA: 0x000CE9EB File Offset: 0x000CCBEB
		// (set) Token: 0x0600445C RID: 17500 RVA: 0x000CE9F3 File Offset: 0x000CCBF3
		[DefaultValue("")]
		public virtual Unit ImageHeight { get; set; }

		// Token: 0x1700266A RID: 9834
		// (get) Token: 0x0600445D RID: 17501 RVA: 0x000CE9FC File Offset: 0x000CCBFC
		// (set) Token: 0x0600445E RID: 17502 RVA: 0x000CEA04 File Offset: 0x000CCC04
		[DefaultValue("")]
		public virtual Unit ImageWidth { get; set; }

		// Token: 0x0600445F RID: 17503 RVA: 0x000CEA10 File Offset: 0x000CCC10
		public override string ToJavaScript()
		{
			return string.Format("new ImageColumnHeader(\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",{10},\"{11}\",{12})", new object[]
			{
				base.Name,
				base.SortExpression,
				base.FormatString,
				base.TextAlign.ToJavaScript(),
				HttpUtility.JavaScriptStringEncode(base.EmptyText),
				HttpUtility.JavaScriptStringEncode(this.AlternateTextProperty),
				HttpUtility.JavaScriptStringEncode(this.DescriptionProperty),
				this.ImageHeight.ToString(),
				this.ImageWidth.ToString(),
				base.Text,
				this.Width.Value,
				this.Width.Type.ToJavaScript(),
				base.Features
			});
		}
	}
}
