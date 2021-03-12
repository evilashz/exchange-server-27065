using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000062 RID: 98
	[DataContract]
	public class SetCalendarConfigurationBase : SetObjectProperties
	{
		// Token: 0x17001847 RID: 6215
		// (get) Token: 0x06001A6A RID: 6762 RVA: 0x00054490 File Offset: 0x00052690
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-MailboxCalendarConfiguration";
			}
		}

		// Token: 0x17001848 RID: 6216
		// (get) Token: 0x06001A6B RID: 6763 RVA: 0x00054497 File Offset: 0x00052697
		public override string RbacScope
		{
			get
			{
				return "@W:Self";
			}
		}
	}
}
