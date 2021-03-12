using System;
using System.ComponentModel;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000612 RID: 1554
	public class MicroEditColumnHeader : ColumnHeader
	{
		// Token: 0x0600452B RID: 17707 RVA: 0x000D12E4 File Offset: 0x000CF4E4
		public MicroEditColumnHeader()
		{
			this.Width = 25;
			base.IsSortable = false;
			base.EnableColumnResize = false;
			base.EnableExport = false;
		}

		// Token: 0x170026AF RID: 9903
		// (get) Token: 0x0600452C RID: 17708 RVA: 0x000D130E File Offset: 0x000CF50E
		// (set) Token: 0x0600452D RID: 17709 RVA: 0x000D1316 File Offset: 0x000CF516
		public string Condition { get; set; }

		// Token: 0x170026B0 RID: 9904
		// (get) Token: 0x0600452E RID: 17710 RVA: 0x000D131F File Offset: 0x000CF51F
		// (set) Token: 0x0600452F RID: 17711 RVA: 0x000D1322 File Offset: 0x000CF522
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

		// Token: 0x06004530 RID: 17712 RVA: 0x000D132C File Offset: 0x000CF52C
		public override string ToJavaScript()
		{
			return string.Format("new MicroEditColumnHeader(\"{0}\",\"{1}\",\"{2}\",\"{3}\",{4},\"{5}\",{6},\"{7}\",{8})", new object[]
			{
				base.Name,
				base.SortExpression,
				base.FormatString,
				base.TextAlign,
				string.IsNullOrEmpty(this.Condition) ? "null" : ("function($_) { return " + this.Condition + "}"),
				base.Text,
				this.Width.Value,
				this.Width.Type.ToJavaScript(),
				base.Features
			});
		}
	}
}
