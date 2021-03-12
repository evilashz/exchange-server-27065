using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000618 RID: 1560
	[ClientScriptResource("MultiLineLabel", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	[RequiredScript(typeof(ExtenderControlBase))]
	public class MultiLineLabel : WebControl, IScriptControl, INamingContainer
	{
		// Token: 0x0600455E RID: 17758 RVA: 0x000D1A84 File Offset: 0x000CFC84
		public MultiLineLabel() : base(HtmlTextWriterTag.Table)
		{
			this.label = new DivLabel();
		}

		// Token: 0x170026C1 RID: 9921
		// (get) Token: 0x0600455F RID: 17759 RVA: 0x000D1A99 File Offset: 0x000CFC99
		// (set) Token: 0x06004560 RID: 17760 RVA: 0x000D1AA6 File Offset: 0x000CFCA6
		public string Text
		{
			get
			{
				return this.label.Text;
			}
			set
			{
				this.label.Text = value;
			}
		}

		// Token: 0x170026C2 RID: 9922
		// (get) Token: 0x06004561 RID: 17761 RVA: 0x000D1AB4 File Offset: 0x000CFCB4
		// (set) Token: 0x06004562 RID: 17762 RVA: 0x000D1ABC File Offset: 0x000CFCBC
		public Unit MaxHeight { get; set; }

		// Token: 0x170026C3 RID: 9923
		// (get) Token: 0x06004563 RID: 17763 RVA: 0x000D1AC5 File Offset: 0x000CFCC5
		// (set) Token: 0x06004564 RID: 17764 RVA: 0x000D1ACD File Offset: 0x000CFCCD
		public Unit SafariMaxHeight { get; set; }

		// Token: 0x170026C4 RID: 9924
		// (get) Token: 0x06004565 RID: 17765 RVA: 0x000D1AD6 File Offset: 0x000CFCD6
		// (set) Token: 0x06004566 RID: 17766 RVA: 0x000D1ADE File Offset: 0x000CFCDE
		public Unit MinHeight { get; set; }

		// Token: 0x170026C5 RID: 9925
		// (get) Token: 0x06004567 RID: 17767 RVA: 0x000D1AE7 File Offset: 0x000CFCE7
		// (set) Token: 0x06004568 RID: 17768 RVA: 0x000D1AEF File Offset: 0x000CFCEF
		public bool ReserveSpace { get; set; }

		// Token: 0x06004569 RID: 17769 RVA: 0x000D1AF8 File Offset: 0x000CFCF8
		IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
		{
			ScriptControlDescriptor scriptControlDescriptor = new ScriptControlDescriptor("MultiLineLabel", this.ClientID);
			if (Util.IsSafari())
			{
				Unit safariMaxHeight = this.SafariMaxHeight;
				scriptControlDescriptor.AddProperty("SafariMaxHeight", (this.SafariMaxHeight.IsEmpty ? MultiLineLabel.defaultSafariMaxHeight : this.SafariMaxHeight).ToString());
			}
			scriptControlDescriptor.AddProperty("ReserveSpace", this.ReserveSpace, true);
			return new ScriptDescriptor[]
			{
				scriptControlDescriptor
			};
		}

		// Token: 0x0600456A RID: 17770 RVA: 0x000D1B78 File Offset: 0x000CFD78
		IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
		{
			return ScriptObjectBuilder.GetScriptReferences(typeof(MultiLineLabel));
		}

		// Token: 0x0600456B RID: 17771 RVA: 0x000D1B8C File Offset: 0x000CFD8C
		protected override void OnPreRender(EventArgs e)
		{
			base.Attributes.Add("cellspacing", "0");
			base.Attributes.Add("cellpadding", "0");
			this.CssClass = "multiLineLabel";
			base.OnPreRender(e);
			if (this.Page != null)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptControl<MultiLineLabel>(this);
			}
		}

		// Token: 0x0600456C RID: 17772 RVA: 0x000D1BEE File Offset: 0x000CFDEE
		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
			if (!base.DesignMode)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);
			}
		}

		// Token: 0x0600456D RID: 17773 RVA: 0x000D1C10 File Offset: 0x000CFE10
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			this.label.ID = "textContainer";
			this.label.Style.Add("max-height", (this.MaxHeight.IsEmpty ? MultiLineLabel.defaultMaxHeight : this.MaxHeight).ToString());
			this.label.Attributes.Add("tabindex", "0");
			this.label.Height = this.Height;
			if (!this.MinHeight.IsEmpty)
			{
				this.label.Style.Add("min-height", this.MinHeight.ToString());
			}
			tableCell.Controls.Add(this.label);
			tableRow.Controls.Add(tableCell);
			this.Controls.Add(tableRow);
		}

		// Token: 0x04002E83 RID: 11907
		private static Unit defaultMaxHeight = new Unit(7.0, UnitType.Em);

		// Token: 0x04002E84 RID: 11908
		private static Unit defaultSafariMaxHeight = new Unit(6.5, UnitType.Em);

		// Token: 0x04002E85 RID: 11909
		private DivLabel label;
	}
}
