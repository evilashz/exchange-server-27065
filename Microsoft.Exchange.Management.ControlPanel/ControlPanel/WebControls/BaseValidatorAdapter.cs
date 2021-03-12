using System;
using System.Web.UI;
using System.Web.UI.Adapters;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x0200056C RID: 1388
	public class BaseValidatorAdapter : ControlAdapter
	{
		// Token: 0x06004098 RID: 16536 RVA: 0x000C5348 File Offset: 0x000C3548
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.Control.Display = ValidatorDisplay.Dynamic;
			this.Control.Text = (this.Control.ErrorMessage = null);
			this.Control.Attributes["TypeId"] = ((IEcpValidator)this.Control).TypeId;
			if (this.Control is CompareValidator)
			{
				CompareValidator compareValidator = this.Control as CompareValidator;
				Control control = this.Control.NamingContainer.FindControl(compareValidator.ControlToCompare);
				this.Control.Attributes["ControlToCompare"] = control.ClientID;
			}
		}

		// Token: 0x06004099 RID: 16537 RVA: 0x000C53F2 File Offset: 0x000C35F2
		protected override void Render(HtmlTextWriter writer)
		{
			this.TrySetEcpErrorMessage(((IEcpValidator)this.Control).DefaultErrorMessage);
			base.Render(writer);
		}

		// Token: 0x0600409A RID: 16538 RVA: 0x000C5411 File Offset: 0x000C3611
		private void TrySetEcpErrorMessage(string mesage)
		{
			if (string.IsNullOrEmpty(this.Control.Attributes["EcpErrorMessage"]))
			{
				this.Control.Attributes["EcpErrorMessage"] = mesage;
			}
		}

		// Token: 0x17002509 RID: 9481
		// (get) Token: 0x0600409B RID: 16539 RVA: 0x000C5445 File Offset: 0x000C3645
		private new BaseValidator Control
		{
			get
			{
				return (BaseValidator)base.Control;
			}
		}

		// Token: 0x04002AF2 RID: 10994
		private const string EcpErrorMessageAttributeName = "EcpErrorMessage";

		// Token: 0x04002AF3 RID: 10995
		private const string TypeIdAttributeName = "TypeId";

		// Token: 0x04002AF4 RID: 10996
		private const string ControlToCompare = "ControlToCompare";
	}
}
