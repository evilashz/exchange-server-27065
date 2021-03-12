using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI;

namespace AjaxControlToolkit
{
	// Token: 0x02000024 RID: 36
	[TargetControlType(typeof(IEditableTextControl))]
	[Designer("AjaxControlToolkit.TextBoxWatermarkExtenderDesigner, AjaxControlToolkit")]
	[ClientScriptResource("AjaxControlToolkit.TextBoxWatermarkBehavior", "AjaxControlToolkit.TextboxWatermark.TextboxWatermark.js")]
	public class TextBoxWatermarkExtender : ExtenderControlBase
	{
		// Token: 0x0600014B RID: 331 RVA: 0x00004B22 File Offset: 0x00002D22
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddProperty("WatermarkText", this.WatermarkText, true);
			descriptor.AddProperty("WatermarkCssClass", this.WatermarkCssClass, true);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00004B50 File Offset: 0x00002D50
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			string key = string.Format(CultureInfo.InvariantCulture, "{0}_onSubmit", new object[]
			{
				this.ID
			});
			string script = string.Format(CultureInfo.InvariantCulture, "var o = $find('{0}'); if(o) {{ o._onSubmit(); }}", new object[]
			{
				base.BehaviorID
			});
			ScriptManager.RegisterOnSubmitStatement(this, typeof(TextBoxWatermarkExtender), key, script);
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00004BB8 File Offset: 0x00002DB8
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00004BCA File Offset: 0x00002DCA
		[RequiredProperty]
		public string WatermarkText
		{
			get
			{
				return base.GetPropertyValue<string>("WatermarkText", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("WatermarkText", value);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00004BD8 File Offset: 0x00002DD8
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00004BEA File Offset: 0x00002DEA
		public string WatermarkCssClass
		{
			get
			{
				return base.GetPropertyValue<string>("WatermarkCssClass", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("WatermarkCssClass", value);
			}
		}

		// Token: 0x04000047 RID: 71
		private const string StringWatermarkText = "WatermarkText";

		// Token: 0x04000048 RID: 72
		private const string StringWatermarkCssClass = "WatermarkCssClass";
	}
}
