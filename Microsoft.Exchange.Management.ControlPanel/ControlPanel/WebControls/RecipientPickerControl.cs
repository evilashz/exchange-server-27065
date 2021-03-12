using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000640 RID: 1600
	[ToolboxData("<{0}:RecipientPickerControl runat=server></{0}:RecipientPickerControl>")]
	[ClientScriptResource("RecipientPickerControl", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	public class RecipientPickerControl : PickerControl
	{
		// Token: 0x06004620 RID: 17952 RVA: 0x000D40C9 File Offset: 0x000D22C9
		public RecipientPickerControl()
		{
			this.AllowTyping = true;
			this.PreferOwaPicker = true;
			base.ValueProperty = "PrimarySmtpAddress";
			if (string.IsNullOrEmpty(base.PickerFormUrl))
			{
				base.PickerFormUrl = "~/Pickers/MemberPicker.aspx";
			}
		}

		// Token: 0x17002708 RID: 9992
		// (get) Token: 0x06004621 RID: 17953 RVA: 0x000D4102 File Offset: 0x000D2302
		// (set) Token: 0x06004622 RID: 17954 RVA: 0x000D410A File Offset: 0x000D230A
		public bool SupportPostback { get; set; }

		// Token: 0x17002709 RID: 9993
		// (get) Token: 0x06004623 RID: 17955 RVA: 0x000D4113 File Offset: 0x000D2313
		// (set) Token: 0x06004624 RID: 17956 RVA: 0x000D411B File Offset: 0x000D231B
		public bool SingleSelect { get; set; }

		// Token: 0x1700270A RID: 9994
		// (get) Token: 0x06004625 RID: 17957 RVA: 0x000D4124 File Offset: 0x000D2324
		// (set) Token: 0x06004626 RID: 17958 RVA: 0x000D412C File Offset: 0x000D232C
		public bool UseVoicemailPicker { get; set; }

		// Token: 0x1700270B RID: 9995
		// (get) Token: 0x06004627 RID: 17959 RVA: 0x000D4135 File Offset: 0x000D2335
		// (set) Token: 0x06004628 RID: 17960 RVA: 0x000D413D File Offset: 0x000D233D
		public bool AllowTyping { get; set; }

		// Token: 0x1700270C RID: 9996
		// (get) Token: 0x06004629 RID: 17961 RVA: 0x000D4146 File Offset: 0x000D2346
		// (set) Token: 0x0600462A RID: 17962 RVA: 0x000D414E File Offset: 0x000D234E
		public bool PreferOwaPicker { get; set; }

		// Token: 0x1700270D RID: 9997
		// (get) Token: 0x0600462B RID: 17963 RVA: 0x000D4157 File Offset: 0x000D2357
		private bool UseOwaPicker
		{
			get
			{
				return this.PreferOwaPicker && OwaPickerUtil.CanUseOwaPicker;
			}
		}

		// Token: 0x0600462C RID: 17964 RVA: 0x000D4168 File Offset: 0x000D2368
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			if (this.UseOwaPicker)
			{
				this.owaPickerUtil = new OwaPickerUtil();
				this.Controls.Add(this.owaPickerUtil);
			}
			if (this.SupportPostback)
			{
				this.hiddenField = new HiddenField();
				this.hiddenField.ID = "pbValue";
				this.Controls.Add(this.hiddenField);
			}
		}

		// Token: 0x1700270E RID: 9998
		// (get) Token: 0x0600462D RID: 17965 RVA: 0x000D41D3 File Offset: 0x000D23D3
		public string ValuePostBack
		{
			get
			{
				if (this.hiddenField != null)
				{
					return this.hiddenField.Value;
				}
				return null;
			}
		}

		// Token: 0x0600462E RID: 17966 RVA: 0x000D41EC File Offset: 0x000D23EC
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddProperty("SingleSelect", this.SingleSelect, true);
			descriptor.AddProperty("AllowTyping", this.AllowTyping, true);
			descriptor.AddProperty("UseVoicemailPicker", this.UseVoicemailPicker, false);
			if (this.SupportPostback)
			{
				descriptor.AddElementProperty("HiddenField", this.hiddenField.ClientID, true);
			}
			if (this.UseOwaPicker)
			{
				descriptor.AddComponentProperty("OwaPickerUtil", this.owaPickerUtil.ClientID, this);
			}
		}

		// Token: 0x04002F68 RID: 12136
		private OwaPickerUtil owaPickerUtil;

		// Token: 0x04002F69 RID: 12137
		private HiddenField hiddenField;
	}
}
