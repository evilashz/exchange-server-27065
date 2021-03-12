using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000A9 RID: 169
	[DataContract]
	public class SetRegionalSettingsConfiguration : SetObjectProperties
	{
		// Token: 0x170018DB RID: 6363
		// (get) Token: 0x06001C2D RID: 7213 RVA: 0x0005835C File Offset: 0x0005655C
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-MailboxRegionalConfiguration";
			}
		}

		// Token: 0x170018DC RID: 6364
		// (get) Token: 0x06001C2E RID: 7214 RVA: 0x00058363 File Offset: 0x00056563
		public override string RbacScope
		{
			get
			{
				return "@W:Self";
			}
		}

		// Token: 0x170018DD RID: 6365
		// (get) Token: 0x06001C2F RID: 7215 RVA: 0x0005836A File Offset: 0x0005656A
		// (set) Token: 0x06001C30 RID: 7216 RVA: 0x0005837C File Offset: 0x0005657C
		[DataMember]
		public string DateFormat
		{
			get
			{
				return (string)base["DateFormat"];
			}
			set
			{
				base["DateFormat"] = value;
			}
		}

		// Token: 0x170018DE RID: 6366
		// (get) Token: 0x06001C31 RID: 7217 RVA: 0x0005838A File Offset: 0x0005658A
		// (set) Token: 0x06001C32 RID: 7218 RVA: 0x000583A6 File Offset: 0x000565A6
		[DataMember]
		public int Language
		{
			get
			{
				return (int)(base["Language"] ?? 0);
			}
			set
			{
				base["Language"] = value;
			}
		}

		// Token: 0x170018DF RID: 6367
		// (get) Token: 0x06001C33 RID: 7219 RVA: 0x000583B9 File Offset: 0x000565B9
		// (set) Token: 0x06001C34 RID: 7220 RVA: 0x000583D5 File Offset: 0x000565D5
		[DataMember]
		public bool LocalizeDefaultFolderName
		{
			get
			{
				return (bool)(base["LocalizeDefaultFolderName"] ?? false);
			}
			set
			{
				base["LocalizeDefaultFolderName"] = value;
			}
		}

		// Token: 0x170018E0 RID: 6368
		// (get) Token: 0x06001C35 RID: 7221 RVA: 0x000583E8 File Offset: 0x000565E8
		// (set) Token: 0x06001C36 RID: 7222 RVA: 0x000583FA File Offset: 0x000565FA
		[DataMember]
		public string TimeFormat
		{
			get
			{
				return (string)base["TimeFormat"];
			}
			set
			{
				base["TimeFormat"] = value;
			}
		}

		// Token: 0x170018E1 RID: 6369
		// (get) Token: 0x06001C37 RID: 7223 RVA: 0x00058408 File Offset: 0x00056608
		// (set) Token: 0x06001C38 RID: 7224 RVA: 0x00058410 File Offset: 0x00056610
		[DataMember]
		public string TimeZone { get; set; }

		// Token: 0x06001C39 RID: 7225 RVA: 0x00058419 File Offset: 0x00056619
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if (!string.IsNullOrEmpty(this.TimeZone))
			{
				base["TimeZone"] = this.TimeZone;
			}
		}
	}
}
