using System;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005BA RID: 1466
	public class EcpCompareValidator : CompareValidator, IEcpValidator
	{
		// Token: 0x170025ED RID: 9709
		// (get) Token: 0x060042CE RID: 17102 RVA: 0x000CB211 File Offset: 0x000C9411
		// (set) Token: 0x060042CF RID: 17103 RVA: 0x000CB219 File Offset: 0x000C9419
		public string DefaultErrorMessage
		{
			get
			{
				return this.defaultErrorMessage;
			}
			set
			{
				this.defaultErrorMessage = value;
			}
		}

		// Token: 0x170025EE RID: 9710
		// (get) Token: 0x060042D0 RID: 17104 RVA: 0x000CB222 File Offset: 0x000C9422
		public string TypeId
		{
			get
			{
				return "CompVal";
			}
		}

		// Token: 0x04002D84 RID: 11652
		private string defaultErrorMessage = Strings.CompareValidatorMessage;
	}
}
