using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000082 RID: 130
	[DataContract]
	public class SetUserMailboxFolderPermission : SetObjectProperties
	{
		// Token: 0x1700189F RID: 6303
		// (get) Token: 0x06001B85 RID: 7045 RVA: 0x0005732A File Offset: 0x0005552A
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-MailboxFolderPermission";
			}
		}

		// Token: 0x170018A0 RID: 6304
		// (get) Token: 0x06001B86 RID: 7046 RVA: 0x00057331 File Offset: 0x00055531
		public override string RbacScope
		{
			get
			{
				return "@W:Self";
			}
		}

		// Token: 0x170018A1 RID: 6305
		// (get) Token: 0x06001B87 RID: 7047 RVA: 0x00057338 File Offset: 0x00055538
		// (set) Token: 0x06001B88 RID: 7048 RVA: 0x0005734A File Offset: 0x0005554A
		public string User
		{
			get
			{
				return (string)base["User"];
			}
			set
			{
				base["User"] = value;
			}
		}

		// Token: 0x170018A2 RID: 6306
		// (get) Token: 0x06001B89 RID: 7049 RVA: 0x00057358 File Offset: 0x00055558
		// (set) Token: 0x06001B8A RID: 7050 RVA: 0x0005736A File Offset: 0x0005556A
		[DataMember]
		public string ReadAccessRights
		{
			get
			{
				return (string)base["AccessRights"];
			}
			set
			{
				base["AccessRights"] = value;
			}
		}
	}
}
