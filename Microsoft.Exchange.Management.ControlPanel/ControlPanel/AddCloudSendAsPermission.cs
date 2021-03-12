using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000F0 RID: 240
	internal class AddCloudSendAsPermission : UpdateCloudSendAsPermission
	{
		// Token: 0x170019CB RID: 6603
		// (get) Token: 0x06001E8E RID: 7822 RVA: 0x0005C113 File Offset: 0x0005A313
		public override string AssociatedCmdlet
		{
			get
			{
				return "Add-RecipientPermission";
			}
		}
	}
}
