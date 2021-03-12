using System;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005BB RID: 1467
	public class EcpCustomValidator : CustomValidator, IEcpValidator
	{
		// Token: 0x170025EF RID: 9711
		// (get) Token: 0x060042D2 RID: 17106 RVA: 0x000CB241 File Offset: 0x000C9441
		// (set) Token: 0x060042D3 RID: 17107 RVA: 0x000CB249 File Offset: 0x000C9449
		public string DefaultErrorMessage { get; set; }

		// Token: 0x170025F0 RID: 9712
		// (get) Token: 0x060042D4 RID: 17108 RVA: 0x000CB252 File Offset: 0x000C9452
		public virtual string TypeId
		{
			get
			{
				return "custom";
			}
		}
	}
}
