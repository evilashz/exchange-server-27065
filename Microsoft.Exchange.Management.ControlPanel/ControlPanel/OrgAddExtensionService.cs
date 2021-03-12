using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001D9 RID: 473
	public class OrgAddExtensionService : AddExtensionService
	{
		// Token: 0x17001B99 RID: 7065
		// (get) Token: 0x0600258B RID: 9611 RVA: 0x000735AC File Offset: 0x000717AC
		public override Type SetParameterType
		{
			get
			{
				return typeof(OrgUploadExtensionParameter);
			}
		}

		// Token: 0x0600258C RID: 9612 RVA: 0x000735B8 File Offset: 0x000717B8
		protected override void AddParameters(PSCommand installCommand, WebServiceParameters param)
		{
			base.AddParameters(installCommand, param);
			installCommand.AddParameter("OrganizationApp");
		}
	}
}
