using System;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005BC RID: 1468
	public class EcpEmailAddressValidator : EcpRegularExpressionValidator
	{
		// Token: 0x060042D6 RID: 17110 RVA: 0x000CB261 File Offset: 0x000C9461
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			base.ValidationExpression = "^[a-zA-Z0-9-_.!#$%&*+-/=?^_|~]+@[a-zA-Z0-9-_.]+$";
		}

		// Token: 0x170025F1 RID: 9713
		// (get) Token: 0x060042D7 RID: 17111 RVA: 0x000CB275 File Offset: 0x000C9475
		public override string TypeId
		{
			get
			{
				return "EmailAddress";
			}
		}
	}
}
