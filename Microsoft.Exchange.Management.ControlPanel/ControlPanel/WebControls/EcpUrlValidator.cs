using System;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005CE RID: 1486
	public class EcpUrlValidator : EcpRegularExpressionValidator
	{
		// Token: 0x17002614 RID: 9748
		// (get) Token: 0x0600432F RID: 17199 RVA: 0x000CBAE0 File Offset: 0x000C9CE0
		public override string DefaultErrorMessage
		{
			get
			{
				return Strings.UrlValidatorErrorMessage;
			}
		}

		// Token: 0x17002615 RID: 9749
		// (get) Token: 0x06004330 RID: 17200 RVA: 0x000CBAEC File Offset: 0x000C9CEC
		public override string TypeId
		{
			get
			{
				return "Url";
			}
		}

		// Token: 0x06004331 RID: 17201 RVA: 0x000CBAF3 File Offset: 0x000C9CF3
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			base.ValidationExpression = CommonRegex.Url.ToString();
		}
	}
}
