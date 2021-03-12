using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020005AE RID: 1454
	[ClientScriptResource("DockPanel2", "Microsoft.Exchange.Management.ControlPanel.Client.Common.js")]
	public class DockPanel2 : ScriptControlBase
	{
		// Token: 0x06004283 RID: 17027 RVA: 0x000CA70D File Offset: 0x000C890D
		public DockPanel2() : base(HtmlTextWriterTag.Div)
		{
			this.Panels = new List<DockElement>();
		}

		// Token: 0x170025D0 RID: 9680
		// (get) Token: 0x06004284 RID: 17028 RVA: 0x000CA722 File Offset: 0x000C8922
		// (set) Token: 0x06004285 RID: 17029 RVA: 0x000CA72A File Offset: 0x000C892A
		public List<DockElement> Panels { get; private set; }

		// Token: 0x06004286 RID: 17030 RVA: 0x000CA734 File Offset: 0x000C8934
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			DockElement dockElement = (this.Controls.Count > 0) ? (this.Controls[0] as DockElement) : null;
			if (dockElement != null)
			{
				descriptor.AddComponentProperty("ChildDockPanel", dockElement);
			}
		}

		// Token: 0x06004287 RID: 17031 RVA: 0x000CA77C File Offset: 0x000C897C
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			WebControl webControl = this;
			bool flag = false;
			this.CssClass += " abs0 DockPanel2 DockElement";
			foreach (DockElement dockElement in this.Panels)
			{
				if (flag)
				{
					throw new ArgumentException("Filled panel must be the last panel in one DockPanel2.");
				}
				webControl.Controls.Add(dockElement);
				if (dockElement.Dock == DockStyle.Fill)
				{
					flag = true;
				}
				else
				{
					webControl = this.PutDockPanelInBuddyPanel(webControl, dockElement);
				}
			}
		}

		// Token: 0x06004288 RID: 17032 RVA: 0x000CA818 File Offset: 0x000C8A18
		private DockElement PutDockPanelInBuddyPanel(WebControl currentContainer, DockElement panel)
		{
			string key = string.Empty;
			Unit unit = Unit.Empty;
			HtmlTextWriterStyle key2 = HtmlTextWriterStyle.MarginLeft;
			int num = 2;
			switch (panel.Dock)
			{
			case DockStyle.Top:
				key = "top";
				unit = panel.Height;
				key2 = HtmlTextWriterStyle.MarginTop;
				num += panel.BorderWidths.VerticalWidth;
				break;
			case DockStyle.Left:
				key = (RtlUtil.IsRtl ? "right" : "left");
				unit = panel.Width;
				key2 = (RtlUtil.IsRtl ? HtmlTextWriterStyle.MarginRight : HtmlTextWriterStyle.MarginLeft);
				num += panel.BorderWidths.HorizontalWidth;
				break;
			case DockStyle.Right:
				key = (RtlUtil.IsRtl ? "left" : "right");
				unit = panel.Width;
				key2 = (RtlUtil.IsRtl ? HtmlTextWriterStyle.MarginLeft : HtmlTextWriterStyle.MarginRight);
				num += panel.BorderWidths.HorizontalWidth;
				break;
			case DockStyle.Bottom:
				key = "bottom";
				unit = panel.Height;
				key2 = HtmlTextWriterStyle.MarginBottom;
				num += panel.BorderWidths.VerticalWidth;
				break;
			}
			DockElement dockElement = new DockElement();
			currentContainer.Controls.Add(dockElement);
			panel.FillPanel = dockElement;
			dockElement.Style.Add(key, unit.ToString());
			dockElement.Style.Add(key2, num + "px");
			return dockElement;
		}

		// Token: 0x06004289 RID: 17033 RVA: 0x000CA964 File Offset: 0x000C8B64
		public override Control FindControl(string id)
		{
			Control control = null;
			foreach (DockElement dockElement in this.Panels)
			{
				control = dockElement.InternalFindControl(id);
				if (control != null)
				{
					break;
				}
			}
			return control;
		}

		// Token: 0x0600428A RID: 17034 RVA: 0x000CA9C0 File Offset: 0x000C8BC0
		public T FindFirstControl<T>() where T : Control
		{
			T t = default(T);
			foreach (DockElement dockElement in this.Panels)
			{
				foreach (object obj in dockElement.Controls)
				{
					Control control = (Control)obj;
					t = (control as T);
					if (t != null)
					{
						break;
					}
				}
				if (t != null)
				{
					break;
				}
			}
			return t;
		}
	}
}
