using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200008A RID: 138
	[DataContract]
	public class SetMessagingConfigurationBase : SetObjectProperties
	{
		// Token: 0x170018A7 RID: 6311
		// (get) Token: 0x06001B9E RID: 7070 RVA: 0x000575A0 File Offset: 0x000557A0
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-MailboxMessageConfiguration";
			}
		}

		// Token: 0x170018A8 RID: 6312
		// (get) Token: 0x06001B9F RID: 7071 RVA: 0x000575A7 File Offset: 0x000557A7
		public override string RbacScope
		{
			get
			{
				return "@W:Self";
			}
		}
	}
}
