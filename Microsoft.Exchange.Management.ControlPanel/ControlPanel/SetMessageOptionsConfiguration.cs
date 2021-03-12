using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200009A RID: 154
	[DataContract]
	public class SetMessageOptionsConfiguration : SetMessagingConfigurationBase
	{
		// Token: 0x170018C3 RID: 6339
		// (get) Token: 0x06001BE8 RID: 7144 RVA: 0x00057ADE File Offset: 0x00055CDE
		// (set) Token: 0x06001BE9 RID: 7145 RVA: 0x00057AF0 File Offset: 0x00055CF0
		[DataMember]
		public string AfterMoveOrDeleteBehavior
		{
			get
			{
				return (string)base["AfterMoveOrDeleteBehavior"];
			}
			set
			{
				base["AfterMoveOrDeleteBehavior"] = value;
			}
		}

		// Token: 0x170018C4 RID: 6340
		// (get) Token: 0x06001BEA RID: 7146 RVA: 0x00057AFE File Offset: 0x00055CFE
		// (set) Token: 0x06001BEB RID: 7147 RVA: 0x00057B1B File Offset: 0x00055D1B
		[DataMember]
		public int NewItemNotification
		{
			get
			{
				return (int)(base["NewItemNotification"] ?? 15);
			}
			set
			{
				base["NewItemNotification"] = value;
			}
		}

		// Token: 0x170018C5 RID: 6341
		// (get) Token: 0x06001BEC RID: 7148 RVA: 0x00057B2E File Offset: 0x00055D2E
		// (set) Token: 0x06001BED RID: 7149 RVA: 0x00057B4A File Offset: 0x00055D4A
		[DataMember]
		public bool EmptyDeletedItemsOnLogoff
		{
			get
			{
				return (bool)(base["EmptyDeletedItemsOnLogoff"] ?? false);
			}
			set
			{
				base["EmptyDeletedItemsOnLogoff"] = value;
			}
		}

		// Token: 0x170018C6 RID: 6342
		// (get) Token: 0x06001BEE RID: 7150 RVA: 0x00057B5D File Offset: 0x00055D5D
		// (set) Token: 0x06001BEF RID: 7151 RVA: 0x00057B79 File Offset: 0x00055D79
		[DataMember]
		public bool CheckForForgottenAttachments
		{
			get
			{
				return (bool)(base["CheckForForgottenAttachments"] ?? false);
			}
			set
			{
				base["CheckForForgottenAttachments"] = value;
			}
		}
	}
}
