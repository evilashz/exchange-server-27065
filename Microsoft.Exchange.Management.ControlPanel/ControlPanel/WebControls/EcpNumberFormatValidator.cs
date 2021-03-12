using System;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005C5 RID: 1477
	public class EcpNumberFormatValidator : EcpRegularExpressionValidator
	{
		// Token: 0x0600430B RID: 17163 RVA: 0x000CB6AB File Offset: 0x000C98AB
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			base.ValidationExpression = "^\\+?(\\*$|\\d+\\*$|\\d+x+$|x+$|\\d+$)";
		}

		// Token: 0x17002604 RID: 9732
		// (get) Token: 0x0600430C RID: 17164 RVA: 0x000CB6BF File Offset: 0x000C98BF
		public override string DefaultErrorMessage
		{
			get
			{
				return Strings.NumberFormatValidatorMessage;
			}
		}

		// Token: 0x17002605 RID: 9733
		// (get) Token: 0x0600430D RID: 17165 RVA: 0x000CB6CB File Offset: 0x000C98CB
		public override string TypeId
		{
			get
			{
				return "NumberFormat";
			}
		}
	}
}
