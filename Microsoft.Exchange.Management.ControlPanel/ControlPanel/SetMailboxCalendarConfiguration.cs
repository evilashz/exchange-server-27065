using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000B6 RID: 182
	[DataContract]
	public class SetMailboxCalendarConfiguration : SetResourceConfigurationBase
	{
		// Token: 0x17001901 RID: 6401
		// (get) Token: 0x06001C89 RID: 7305 RVA: 0x00058B92 File Offset: 0x00056D92
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-MailboxCalendarConfiguration";
			}
		}

		// Token: 0x17001902 RID: 6402
		// (get) Token: 0x06001C8A RID: 7306 RVA: 0x00058B99 File Offset: 0x00056D99
		// (set) Token: 0x06001C8B RID: 7307 RVA: 0x00058BB5 File Offset: 0x00056DB5
		[DataMember]
		public bool DisableReminders
		{
			get
			{
				return (bool)(base["RemindersEnabled"] ?? true);
			}
			set
			{
				base["RemindersEnabled"] = !value;
			}
		}
	}
}
