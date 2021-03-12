using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000ED RID: 237
	internal class AddSendAsPermission : UpdateSendAsPermission
	{
		// Token: 0x06001E84 RID: 7812 RVA: 0x0005C085 File Offset: 0x0005A285
		public AddSendAsPermission()
		{
			base["AccessRights"] = "ExtendedRight";
		}

		// Token: 0x170019C6 RID: 6598
		// (get) Token: 0x06001E85 RID: 7813 RVA: 0x0005C09D File Offset: 0x0005A29D
		public override string AssociatedCmdlet
		{
			get
			{
				return "Add-ADPermission";
			}
		}
	}
}
