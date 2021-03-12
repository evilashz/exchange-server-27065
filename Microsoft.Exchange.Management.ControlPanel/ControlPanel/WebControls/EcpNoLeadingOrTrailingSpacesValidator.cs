using System;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005C3 RID: 1475
	public class EcpNoLeadingOrTrailingSpacesValidator : EcpRegularExpressionValidator
	{
		// Token: 0x06004304 RID: 17156 RVA: 0x000CB642 File Offset: 0x000C9842
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			base.ValidationExpression = "^[^\\s]+(.*[^\\s]+)?$";
		}

		// Token: 0x17002601 RID: 9729
		// (get) Token: 0x06004305 RID: 17157 RVA: 0x000CB656 File Offset: 0x000C9856
		public override string DefaultErrorMessage
		{
			get
			{
				return Strings.NoLeadingOrTrailingSpacesValidatorMessage;
			}
		}

		// Token: 0x17002602 RID: 9730
		// (get) Token: 0x06004306 RID: 17158 RVA: 0x000CB662 File Offset: 0x000C9862
		public override string TypeId
		{
			get
			{
				return "Trlspc";
			}
		}
	}
}
