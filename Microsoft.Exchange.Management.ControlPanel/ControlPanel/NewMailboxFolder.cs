using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200060D RID: 1549
	[DataContract]
	public class NewMailboxFolder : WebServiceParameters
	{
		// Token: 0x170026AA RID: 9898
		// (get) Token: 0x06004514 RID: 17684 RVA: 0x000D0EBE File Offset: 0x000CF0BE
		public override string AssociatedCmdlet
		{
			get
			{
				return "New-MailboxFolder";
			}
		}

		// Token: 0x170026AB RID: 9899
		// (get) Token: 0x06004515 RID: 17685 RVA: 0x000D0EC5 File Offset: 0x000CF0C5
		public override string RbacScope
		{
			get
			{
				return "@W:Self";
			}
		}

		// Token: 0x170026AC RID: 9900
		// (get) Token: 0x06004516 RID: 17686 RVA: 0x000D0ECC File Offset: 0x000CF0CC
		// (set) Token: 0x06004517 RID: 17687 RVA: 0x000D0EDE File Offset: 0x000CF0DE
		[DataMember]
		public string Name
		{
			get
			{
				return (string)base[MailboxFolderSchema.Name];
			}
			set
			{
				base[MailboxFolderSchema.Name] = value;
			}
		}

		// Token: 0x170026AD RID: 9901
		// (get) Token: 0x06004518 RID: 17688 RVA: 0x000D0EEC File Offset: 0x000CF0EC
		// (set) Token: 0x06004519 RID: 17689 RVA: 0x000D0EFE File Offset: 0x000CF0FE
		[DataMember]
		public string Parent
		{
			get
			{
				return (string)base["Parent"];
			}
			set
			{
				base["Parent"] = value;
			}
		}

		// Token: 0x04002E56 RID: 11862
		public const string RbacParameters = "?Name&Parent";

		// Token: 0x04002E57 RID: 11863
		private const string StringParent = "Parent";
	}
}
