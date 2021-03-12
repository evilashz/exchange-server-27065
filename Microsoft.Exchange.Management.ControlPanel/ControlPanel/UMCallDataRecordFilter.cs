using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004B1 RID: 1201
	[DataContract]
	public class UMCallDataRecordFilter : WebServiceParameters
	{
		// Token: 0x1700237D RID: 9085
		// (get) Token: 0x06003B77 RID: 15223 RVA: 0x000B3924 File Offset: 0x000B1B24
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-UMCallDataRecord";
			}
		}

		// Token: 0x1700237E RID: 9086
		// (get) Token: 0x06003B78 RID: 15224 RVA: 0x000B392B File Offset: 0x000B1B2B
		public override string RbacScope
		{
			get
			{
				return "@R:Organization";
			}
		}

		// Token: 0x1700237F RID: 9087
		// (get) Token: 0x06003B79 RID: 15225 RVA: 0x000B3932 File Offset: 0x000B1B32
		// (set) Token: 0x06003B7A RID: 15226 RVA: 0x000B3944 File Offset: 0x000B1B44
		[DataMember]
		public Identity Mailbox
		{
			get
			{
				return (Identity)base["Mailbox"];
			}
			set
			{
				base["Mailbox"] = value;
			}
		}

		// Token: 0x0400276A RID: 10090
		public const string RbacParameters = "?Mailbox";
	}
}
