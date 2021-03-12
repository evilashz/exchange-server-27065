using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000F1 RID: 241
	internal class RemoveCloudSendAsPermission : UpdateCloudSendAsPermission
	{
		// Token: 0x170019CC RID: 6604
		// (get) Token: 0x06001E90 RID: 7824 RVA: 0x0005C122 File Offset: 0x0005A322
		public override string AssociatedCmdlet
		{
			get
			{
				return "Remove-RecipientPermission";
			}
		}
	}
}
