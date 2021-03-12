using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000C9 RID: 201
	[DataContract]
	public class SetVoiceMailSettings : SetVoiceMailBase
	{
		// Token: 0x17001947 RID: 6471
		// (get) Token: 0x06001D35 RID: 7477 RVA: 0x00059A55 File Offset: 0x00057C55
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-UMMailbox";
			}
		}

		// Token: 0x17001948 RID: 6472
		// (get) Token: 0x06001D36 RID: 7478 RVA: 0x00059A5C File Offset: 0x00057C5C
		public override string RbacScope
		{
			get
			{
				return "@W:Self";
			}
		}
	}
}
