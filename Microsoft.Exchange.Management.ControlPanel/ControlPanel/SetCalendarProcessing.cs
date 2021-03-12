using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000295 RID: 661
	public class SetCalendarProcessing : SetObjectProperties
	{
		// Token: 0x17001D5D RID: 7517
		// (get) Token: 0x06002B2D RID: 11053 RVA: 0x000868F3 File Offset: 0x00084AF3
		// (set) Token: 0x06002B2E RID: 11054 RVA: 0x000868FB File Offset: 0x00084AFB
		[DataMember]
		public string AutomaticBooking { get; set; }

		// Token: 0x17001D5E RID: 7518
		// (get) Token: 0x06002B2F RID: 11055 RVA: 0x00086904 File Offset: 0x00084B04
		// (set) Token: 0x06002B30 RID: 11056 RVA: 0x0008690C File Offset: 0x00084B0C
		[DataMember]
		public Identity[] ResourceDelegates { get; set; }

		// Token: 0x17001D5F RID: 7519
		// (get) Token: 0x06002B31 RID: 11057 RVA: 0x00086915 File Offset: 0x00084B15
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-CalendarProcessing";
			}
		}

		// Token: 0x17001D60 RID: 7520
		// (get) Token: 0x06002B32 RID: 11058 RVA: 0x0008691C File Offset: 0x00084B1C
		public override string RbacScope
		{
			get
			{
				return "@W:Self|Organization";
			}
		}

		// Token: 0x06002B33 RID: 11059 RVA: 0x00086924 File Offset: 0x00084B24
		public void UpdateResourceObjects()
		{
			bool flag;
			if (this.AutomaticBooking != null && bool.TryParse(this.AutomaticBooking, out flag))
			{
				base["AutomateProcessing"] = CalendarProcessingFlags.AutoAccept;
				base["AllBookInPolicy"] = flag;
				base["AllRequestInPolicy"] = !flag;
			}
			if (this.ResourceDelegates != null)
			{
				base["ResourceDelegates"] = this.ResourceDelegates.ToIdParameters();
			}
		}
	}
}
