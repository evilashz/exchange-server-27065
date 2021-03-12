using System;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004AC RID: 1196
	[ClientScriptResource("UMBasePopupProperties", "Microsoft.Exchange.Management.ControlPanel.Client.UnifiedMessaging.js")]
	public abstract class UMBasePopupProperties : Properties
	{
		// Token: 0x17002363 RID: 9059
		// (get) Token: 0x06003B35 RID: 15157 RVA: 0x000B31ED File Offset: 0x000B13ED
		protected bool IsNewRequest
		{
			get
			{
				return this.newRequest;
			}
		}

		// Token: 0x06003B36 RID: 15158 RVA: 0x000B31F8 File Offset: 0x000B13F8
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.ValidateQueryStrings();
			base.CaptionTextField = string.Empty;
			PopupForm popupForm = (PopupForm)this.Page;
			popupForm.Title = string.Empty;
			this.SetTitleAndCaption(popupForm);
		}

		// Token: 0x06003B37 RID: 15159 RVA: 0x000B323C File Offset: 0x000B143C
		protected override ScriptControlDescriptor GetScriptDescriptor()
		{
			ScriptControlDescriptor scriptDescriptor = base.GetScriptDescriptor();
			scriptDescriptor.AddProperty("IsNewRequest", this.IsNewRequest);
			scriptDescriptor.Type = "UMBasePopupProperties";
			return scriptDescriptor;
		}

		// Token: 0x06003B38 RID: 15160 RVA: 0x000B3272 File Offset: 0x000B1472
		protected virtual void ValidateQueryStrings()
		{
			if (!bool.TryParse(this.Page.Request["new"], out this.newRequest))
			{
				throw new BadQueryParameterException("new");
			}
		}

		// Token: 0x06003B39 RID: 15161
		protected abstract void SetTitleAndCaption(PopupForm form);

		// Token: 0x04002762 RID: 10082
		private bool newRequest;
	}
}
