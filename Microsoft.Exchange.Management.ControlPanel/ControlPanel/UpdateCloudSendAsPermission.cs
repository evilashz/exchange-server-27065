using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000EF RID: 239
	internal abstract class UpdateCloudSendAsPermission : WebServiceParameters
	{
		// Token: 0x06001E88 RID: 7816 RVA: 0x0005C0B3 File Offset: 0x0005A2B3
		public UpdateCloudSendAsPermission()
		{
			base["AccessRights"] = RecipientAccessRight.SendAs;
		}

		// Token: 0x170019C8 RID: 6600
		// (get) Token: 0x06001E89 RID: 7817 RVA: 0x0005C0CC File Offset: 0x0005A2CC
		public override string RbacScope
		{
			get
			{
				return "@W:Organization";
			}
		}

		// Token: 0x170019C9 RID: 6601
		// (get) Token: 0x06001E8A RID: 7818 RVA: 0x0005C0D3 File Offset: 0x0005A2D3
		// (set) Token: 0x06001E8B RID: 7819 RVA: 0x0005C0E5 File Offset: 0x0005A2E5
		public string Trustee
		{
			get
			{
				return (string)base["Trustee"];
			}
			set
			{
				base["Trustee"] = value;
			}
		}

		// Token: 0x170019CA RID: 6602
		// (get) Token: 0x06001E8C RID: 7820 RVA: 0x0005C0F3 File Offset: 0x0005A2F3
		// (set) Token: 0x06001E8D RID: 7821 RVA: 0x0005C105 File Offset: 0x0005A305
		public string Identity
		{
			get
			{
				return (string)base["Identity"];
			}
			set
			{
				base["Identity"] = value;
			}
		}
	}
}
