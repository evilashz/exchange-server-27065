using System;
using System.ComponentModel;
using System.Web;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020001CB RID: 459
	public class TrendColumnHeader : ColumnHeader
	{
		// Token: 0x06002515 RID: 9493 RVA: 0x00071D78 File Offset: 0x0006FF78
		public TrendColumnHeader()
		{
			this.AllowHTML = true;
		}

		// Token: 0x17001B68 RID: 7016
		// (get) Token: 0x06002516 RID: 9494 RVA: 0x00071D87 File Offset: 0x0006FF87
		// (set) Token: 0x06002517 RID: 9495 RVA: 0x00071D8F File Offset: 0x0006FF8F
		[DefaultValue(null)]
		public virtual string TrendProperty { get; set; }

		// Token: 0x17001B69 RID: 7017
		// (get) Token: 0x06002518 RID: 9496 RVA: 0x00071D98 File Offset: 0x0006FF98
		// (set) Token: 0x06002519 RID: 9497 RVA: 0x00071DA0 File Offset: 0x0006FFA0
		[DefaultValue(null)]
		public virtual string AlternateTextProperty { get; set; }

		// Token: 0x0600251A RID: 9498 RVA: 0x00071DAC File Offset: 0x0006FFAC
		public override string ToJavaScript()
		{
			return string.Format("new TrendColumnHeader(\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",{8},\"{9}\",{10})", new object[]
			{
				base.Name,
				base.SortExpression,
				base.FormatString,
				base.TextAlign.ToJavaScript(),
				HttpUtility.JavaScriptStringEncode(base.EmptyText),
				HttpUtility.JavaScriptStringEncode(this.TrendProperty),
				HttpUtility.JavaScriptStringEncode(this.AlternateTextProperty),
				base.Text,
				this.Width.Value,
				this.Width.Type.ToJavaScript(),
				base.Features
			});
		}
	}
}
