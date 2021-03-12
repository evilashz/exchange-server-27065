using System;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005B6 RID: 1462
	public class EcpRegularExpressionValidator : RegularExpressionValidator, IEcpValidator
	{
		// Token: 0x060042B2 RID: 17074 RVA: 0x000CB018 File Offset: 0x000C9218
		public EcpRegularExpressionValidator()
		{
			this.DefaultErrorMessage = Strings.RegexValidatorErrorMessage;
		}

		// Token: 0x170025E1 RID: 9697
		// (get) Token: 0x060042B3 RID: 17075 RVA: 0x000CB030 File Offset: 0x000C9230
		// (set) Token: 0x060042B4 RID: 17076 RVA: 0x000CB038 File Offset: 0x000C9238
		public virtual string DefaultErrorMessage { get; set; }

		// Token: 0x170025E2 RID: 9698
		// (get) Token: 0x060042B5 RID: 17077 RVA: 0x000CB041 File Offset: 0x000C9241
		public virtual string TypeId
		{
			get
			{
				return "Regex";
			}
		}
	}
}
