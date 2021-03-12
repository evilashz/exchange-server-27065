using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000663 RID: 1635
	public class SpriteColumnHeader : ColumnHeader
	{
		// Token: 0x06004727 RID: 18215 RVA: 0x000D7ECC File Offset: 0x000D60CC
		public SpriteColumnHeader()
		{
			this.SpanCssClass = "ImgColumnSpan";
			this.Width = Unit.Pixel(22);
		}

		// Token: 0x1700275C RID: 10076
		// (get) Token: 0x06004728 RID: 18216 RVA: 0x000D7EEC File Offset: 0x000D60EC
		// (set) Token: 0x06004729 RID: 18217 RVA: 0x000D7EEF File Offset: 0x000D60EF
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

		// Token: 0x1700275D RID: 10077
		// (get) Token: 0x0600472A RID: 18218 RVA: 0x000D7EF6 File Offset: 0x000D60F6
		// (set) Token: 0x0600472B RID: 18219 RVA: 0x000D7EFE File Offset: 0x000D60FE
		[DefaultValue(null)]
		public virtual string AlternateTextProperty { get; set; }

		// Token: 0x1700275E RID: 10078
		// (get) Token: 0x0600472C RID: 18220 RVA: 0x000D7F07 File Offset: 0x000D6107
		// (set) Token: 0x0600472D RID: 18221 RVA: 0x000D7F0F File Offset: 0x000D610F
		[DefaultValue(null)]
		public virtual string DefaultSprite { get; set; }

		// Token: 0x0600472E RID: 18222 RVA: 0x000D7F18 File Offset: 0x000D6118
		public override string ToJavaScript()
		{
			return string.Format("new SpriteColumnHeader(\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",{7},\"{8}\",{9})", new object[]
			{
				base.Name,
				base.SortExpression,
				base.FormatString,
				base.TextAlign.ToJavaScript(),
				HttpUtility.JavaScriptStringEncode(base.EmptyText),
				HttpUtility.JavaScriptStringEncode(this.AlternateTextProperty),
				base.Text,
				this.Width.Value,
				this.Width.Type.ToJavaScript(),
				base.Features
			});
		}
	}
}
