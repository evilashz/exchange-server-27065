using System;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005BE RID: 1470
	public class EcpInvalidMailboxSearchNameValidator : RegularExpressionValidator, IEcpValidator
	{
		// Token: 0x060042DD RID: 17117 RVA: 0x000CB2DD File Offset: 0x000C94DD
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			base.ValidationExpression = "^[^\\?\\*]+$";
		}

		// Token: 0x170025F3 RID: 9715
		// (get) Token: 0x060042DE RID: 17118 RVA: 0x000CB2F1 File Offset: 0x000C94F1
		public string DefaultErrorMessage
		{
			get
			{
				return Strings.MailboxSearchNameValidatorErrorMessage;
			}
		}

		// Token: 0x170025F4 RID: 9716
		// (get) Token: 0x060042DF RID: 17119 RVA: 0x000CB2FD File Offset: 0x000C94FD
		public string TypeId
		{
			get
			{
				return "MailboxSearchName";
			}
		}
	}
}
