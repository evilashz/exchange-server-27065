using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000080 RID: 128
	[DataContract]
	public class MailboxFolderPermissionFilter : WebServiceParameters
	{
		// Token: 0x1700189A RID: 6298
		// (get) Token: 0x06001B7B RID: 7035 RVA: 0x00057280 File Offset: 0x00055480
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-MailboxFolderPermission";
			}
		}

		// Token: 0x1700189B RID: 6299
		// (get) Token: 0x06001B7C RID: 7036 RVA: 0x00057287 File Offset: 0x00055487
		public override string RbacScope
		{
			get
			{
				return "@R:Self";
			}
		}

		// Token: 0x1700189C RID: 6300
		// (get) Token: 0x06001B7D RID: 7037 RVA: 0x0005728E File Offset: 0x0005548E
		// (set) Token: 0x06001B7E RID: 7038 RVA: 0x000572A0 File Offset: 0x000554A0
		[DataMember]
		public Identity Identity
		{
			get
			{
				return (Identity)base["Identity"];
			}
			set
			{
				base["Identity"] = value;
			}
		}

		// Token: 0x04001B5E RID: 7006
		public const string RbacParameters = "?Identity";
	}
}
