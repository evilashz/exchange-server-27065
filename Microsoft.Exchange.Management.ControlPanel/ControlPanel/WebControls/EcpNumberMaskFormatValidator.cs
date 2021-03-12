using System;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005C6 RID: 1478
	public class EcpNumberMaskFormatValidator : EcpRegularExpressionValidator
	{
		// Token: 0x0600430F RID: 17167 RVA: 0x000CB6DA File Offset: 0x000C98DA
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			base.ValidationExpression = "^\\*$|^\\d+\\*$|^\\d+x+$|^x+$|^\\d+$";
		}

		// Token: 0x17002606 RID: 9734
		// (get) Token: 0x06004310 RID: 17168 RVA: 0x000CB6EE File Offset: 0x000C98EE
		public override string DefaultErrorMessage
		{
			get
			{
				return Strings.NumberMaskFormatValidatorMessage;
			}
		}

		// Token: 0x17002607 RID: 9735
		// (get) Token: 0x06004311 RID: 17169 RVA: 0x000CB6FA File Offset: 0x000C98FA
		public override string TypeId
		{
			get
			{
				return "NumberMaskFormat";
			}
		}
	}
}
