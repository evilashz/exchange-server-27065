using System;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005C4 RID: 1476
	public class EcpNonBlankValidator : EcpCustomValidator
	{
		// Token: 0x06004308 RID: 17160 RVA: 0x000CB671 File Offset: 0x000C9871
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			base.ClientValidationFunction = "EvaluateIsNonBlank";
			base.DefaultErrorMessage = Strings.NonBlankValidatorMessage;
			base.ValidateEmptyText = true;
		}

		// Token: 0x17002603 RID: 9731
		// (get) Token: 0x06004309 RID: 17161 RVA: 0x000CB69C File Offset: 0x000C989C
		public override string TypeId
		{
			get
			{
				return "nonBlank";
			}
		}
	}
}
