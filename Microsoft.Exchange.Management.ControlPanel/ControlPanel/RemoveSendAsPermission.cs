using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000EE RID: 238
	internal class RemoveSendAsPermission : UpdateSendAsPermission
	{
		// Token: 0x170019C7 RID: 6599
		// (get) Token: 0x06001E87 RID: 7815 RVA: 0x0005C0AC File Offset: 0x0005A2AC
		public override string AssociatedCmdlet
		{
			get
			{
				return "Remove-ADPermission";
			}
		}
	}
}
