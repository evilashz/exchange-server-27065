using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel.DataContracts
{
	// Token: 0x020000D9 RID: 217
	[DataContract]
	public class GetMailboxPermissionParameters : MailboxPermissionParameters
	{
		// Token: 0x1700195A RID: 6490
		// (get) Token: 0x06001D88 RID: 7560 RVA: 0x0005A6A2 File Offset: 0x000588A2
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-MailboxPermission";
			}
		}

		// Token: 0x1700195B RID: 6491
		// (get) Token: 0x06001D89 RID: 7561 RVA: 0x0005A6A9 File Offset: 0x000588A9
		public override string RbacScope
		{
			get
			{
				return "@R:Self";
			}
		}

		// Token: 0x1700195C RID: 6492
		// (get) Token: 0x06001D8A RID: 7562 RVA: 0x0005A6B0 File Offset: 0x000588B0
		// (set) Token: 0x06001D8B RID: 7563 RVA: 0x0005A6C2 File Offset: 0x000588C2
		[DataMember]
		public Identity User
		{
			get
			{
				return (Identity)base["User"];
			}
			set
			{
				base["User"] = value;
			}
		}

		// Token: 0x04001BE5 RID: 7141
		private const string GetMailboxPermission = "Get-MailboxPermission";
	}
}
