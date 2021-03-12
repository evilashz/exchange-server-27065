using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI;

namespace AjaxControlToolkit
{
	// Token: 0x02000021 RID: 33
	[RequiredScript(typeof(CommonToolkitScripts))]
	[TargetControlType(typeof(Control))]
	[Designer("AjaxControlToolkit.ModalPopupDesigner, AjaxControlToolkit")]
	[ClientScriptResource("AjaxControlToolkit.ModalPopupBehavior", "AjaxControlToolkit.ModalPopup.ModalPopupBehavior.js")]
	public class ModalPopupExtender : ExtenderControlBase
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x00004384 File Offset: 0x00002584
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddProperty("PopupControlID", this.PopupControlID, true);
			descriptor.AddProperty("BackgroundCssClass", this.BackgroundCssClass, true);
			if (this.X != -1)
			{
				descriptor.AddProperty("X", this.X);
			}
			if (this.Y != -1)
			{
				descriptor.AddProperty("Y", this.Y);
			}
			descriptor.AddProperty("ButtonIDs", this.ButtonIDs, true);
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000FA RID: 250 RVA: 0x0000440C File Offset: 0x0000260C
		// (set) Token: 0x060000FB RID: 251 RVA: 0x0000441E File Offset: 0x0000261E
		[RequiredProperty]
		public string PopupControlID
		{
			get
			{
				return base.GetPropertyValue<string>("PopupControlID", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("PopupControlID", value);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000FC RID: 252 RVA: 0x0000442C File Offset: 0x0000262C
		// (set) Token: 0x060000FD RID: 253 RVA: 0x0000443E File Offset: 0x0000263E
		public string BackgroundCssClass
		{
			get
			{
				return base.GetPropertyValue<string>("BackgroundCssClass", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("BackgroundCssClass", value);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000FE RID: 254 RVA: 0x0000444C File Offset: 0x0000264C
		// (set) Token: 0x060000FF RID: 255 RVA: 0x0000445A File Offset: 0x0000265A
		public int X
		{
			get
			{
				return base.GetPropertyValue<int>("X", -1);
			}
			set
			{
				base.SetPropertyValue<int>("X", value);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00004468 File Offset: 0x00002668
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00004476 File Offset: 0x00002676
		public int Y
		{
			get
			{
				return base.GetPropertyValue<int>("Y", -1);
			}
			set
			{
				base.SetPropertyValue<int>("Y", value);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00004484 File Offset: 0x00002684
		// (set) Token: 0x06000103 RID: 259 RVA: 0x00004496 File Offset: 0x00002696
		public string ButtonIDs
		{
			get
			{
				return base.GetPropertyValue<string>("ButtonIDs", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("ButtonIDs", value);
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000044A4 File Offset: 0x000026A4
		public void Show()
		{
			this.ChangeVisibility(true);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000044AD File Offset: 0x000026AD
		public void Hide()
		{
			this.ChangeVisibility(false);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000044B8 File Offset: 0x000026B8
		private void ChangeVisibility(bool show)
		{
			if (base.TargetControl == null)
			{
				throw new ArgumentNullException("TargetControl", "TargetControl property cannot be null");
			}
			string text = show ? "show" : "hide";
			if (ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
			{
				ScriptManager.GetCurrent(this.Page).RegisterDataItem(base.TargetControl, text);
				return;
			}
			string script = string.Format(CultureInfo.InvariantCulture, "(function() {{var fn = function() {{AjaxControlToolkit.ModalPopupBehavior.invokeViaServer('{0}', {1}); Sys.Application.remove_load(fn);}};Sys.Application.add_load(fn);}})();", new object[]
			{
				base.BehaviorID,
				show ? "true" : "false"
			});
			ScriptManager.RegisterStartupScript(this, typeof(ModalPopupExtender), text + base.BehaviorID, script, true);
		}

		// Token: 0x0400003E RID: 62
		private const string StringPopupControlID = "PopupControlID";

		// Token: 0x0400003F RID: 63
		private const string StringBackgroundCssClass = "BackgroundCssClass";

		// Token: 0x04000040 RID: 64
		private const string StringX = "X";

		// Token: 0x04000041 RID: 65
		private const string StringY = "Y";

		// Token: 0x04000042 RID: 66
		private const string StringButtonIDs = "ButtonIDs";
	}
}
