using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000664 RID: 1636
	[ToolboxData("<{0}:StatisticsBar runat=server></{0}:StatisticsBar>")]
	[ControlValueProperty("Value")]
	[ClientScriptResource("StatisticsBar", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	public class StatisticsBar : ScriptControlBase
	{
		// Token: 0x0600472F RID: 18223 RVA: 0x000D7FC1 File Offset: 0x000D61C1
		public StatisticsBar() : base(HtmlTextWriterTag.Div)
		{
		}

		// Token: 0x1700275F RID: 10079
		// (get) Token: 0x06004730 RID: 18224 RVA: 0x000D7FCB File Offset: 0x000D61CB
		// (set) Token: 0x06004731 RID: 18225 RVA: 0x000D7FD3 File Offset: 0x000D61D3
		[DefaultValue(typeof(Unit), "100%")]
		public Unit BarWidth
		{
			get
			{
				return this.barWidth;
			}
			set
			{
				this.barWidth = value;
			}
		}

		// Token: 0x17002760 RID: 10080
		// (get) Token: 0x06004732 RID: 18226 RVA: 0x000D7FDC File Offset: 0x000D61DC
		// (set) Token: 0x06004733 RID: 18227 RVA: 0x000D7FE4 File Offset: 0x000D61E4
		[DefaultValue(typeof(Unit), "16px")]
		public Unit BarHeight
		{
			get
			{
				return this.barHeight;
			}
			set
			{
				if (value.Type == UnitType.Pixel && value.Value >= 1.0 && value.Value <= 16.0)
				{
					this.barHeight = value;
					return;
				}
				throw new InvalidOperationException("Height for the bar can only be specified in Pixels and ranging from 1px to 16px.");
			}
		}

		// Token: 0x06004734 RID: 18228 RVA: 0x000D8034 File Offset: 0x000D6234
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			if (!this.BarWidth.IsEmpty)
			{
				descriptor.AddProperty("BarWidth", this.BarWidth.ToString(CultureInfo.InvariantCulture));
			}
			if (!this.BarHeight.IsEmpty)
			{
				descriptor.AddProperty("BarHeight", this.BarHeight.ToString(CultureInfo.InvariantCulture));
			}
		}

		// Token: 0x04002FF5 RID: 12277
		private Unit barWidth;

		// Token: 0x04002FF6 RID: 12278
		private Unit barHeight;
	}
}
