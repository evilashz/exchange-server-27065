using System;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005CC RID: 1484
	public class EcpSelectOneValidator : RequiredFieldValidator, IEcpValidator
	{
		// Token: 0x17002612 RID: 9746
		// (get) Token: 0x0600432B RID: 17195 RVA: 0x000CBABD File Offset: 0x000C9CBD
		public string DefaultErrorMessage
		{
			get
			{
				return Strings.SelectOneValidatorErrorMessage;
			}
		}

		// Token: 0x17002613 RID: 9747
		// (get) Token: 0x0600432C RID: 17196 RVA: 0x000CBAC9 File Offset: 0x000C9CC9
		public string TypeId
		{
			get
			{
				return "SelOne";
			}
		}
	}
}
