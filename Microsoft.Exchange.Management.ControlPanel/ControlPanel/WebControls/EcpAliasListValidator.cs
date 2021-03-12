using System;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005B7 RID: 1463
	public class EcpAliasListValidator : EcpRegularExpressionValidator
	{
		// Token: 0x060042B6 RID: 17078 RVA: 0x000CB048 File Offset: 0x000C9248
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			base.ValidationExpression = "^[^=)(;\"><\\*]*$";
		}

		// Token: 0x170025E3 RID: 9699
		// (get) Token: 0x060042B7 RID: 17079 RVA: 0x000CB05C File Offset: 0x000C925C
		public override string DefaultErrorMessage
		{
			get
			{
				return Strings.AliasListValidatorMessage;
			}
		}

		// Token: 0x170025E4 RID: 9700
		// (get) Token: 0x060042B8 RID: 17080 RVA: 0x000CB068 File Offset: 0x000C9268
		public override string TypeId
		{
			get
			{
				return "AliasList";
			}
		}
	}
}
