using System;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005CB RID: 1483
	public class EcpRequiredFieldValidator : RequiredFieldValidator, IEcpValidator
	{
		// Token: 0x17002610 RID: 9744
		// (get) Token: 0x06004328 RID: 17192 RVA: 0x000CBAA2 File Offset: 0x000C9CA2
		public string DefaultErrorMessage
		{
			get
			{
				return Strings.RequiredFieldValidatorErrorMessage;
			}
		}

		// Token: 0x17002611 RID: 9745
		// (get) Token: 0x06004329 RID: 17193 RVA: 0x000CBAAE File Offset: 0x000C9CAE
		public string TypeId
		{
			get
			{
				return "ReqFld";
			}
		}
	}
}
