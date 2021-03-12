using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.InfoWorker.Common.ELC
{
	// Token: 0x02000190 RID: 400
	internal class ContentSetting
	{
		// Token: 0x06000ABB RID: 2747 RVA: 0x0002DD69 File Offset: 0x0002BF69
		internal ContentSetting()
		{
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0002DD74 File Offset: 0x0002BF74
		internal ContentSetting(ElcContentSettings elcContentSettings)
		{
			this.guid = elcContentSettings.Guid;
			this.name = elcContentSettings.Name;
			this.messageClass = elcContentSettings.MessageClass;
			this.ageLimitForRetention = elcContentSettings.AgeLimitForRetention;
			this.retentionEnabled = elcContentSettings.RetentionEnabled;
			this.retentionAction = elcContentSettings.RetentionAction;
			this.journalingEnabled = elcContentSettings.JournalingEnabled;
			this.triggerForRetention = elcContentSettings.TriggerForRetention;
			if (elcContentSettings.MoveToDestinationFolder != null)
			{
				this.moveToDestinationFolder = new Guid?(elcContentSettings.MoveToDestinationFolder.ObjectGuid);
				this.moveToDestinationFolderName = elcContentSettings.MoveToDestinationFolder.Name;
			}
			this.managedFolderName = elcContentSettings.ManagedFolderName;
			this.addressForJournaling = elcContentSettings.AddressForJournaling;
			this.labelForJournaling = elcContentSettings.LabelForJournaling;
			this.messageFormatForJournaling = elcContentSettings.MessageFormatForJournaling;
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x0002DE46 File Offset: 0x0002C046
		// (set) Token: 0x06000ABE RID: 2750 RVA: 0x0002DE4E File Offset: 0x0002C04E
		internal Guid Guid
		{
			get
			{
				return this.guid;
			}
			set
			{
				this.guid = value;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x0002DE57 File Offset: 0x0002C057
		// (set) Token: 0x06000AC0 RID: 2752 RVA: 0x0002DE5F File Offset: 0x0002C05F
		internal string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x0002DE68 File Offset: 0x0002C068
		// (set) Token: 0x06000AC2 RID: 2754 RVA: 0x0002DE70 File Offset: 0x0002C070
		internal string MessageClass
		{
			get
			{
				return this.messageClass;
			}
			set
			{
				this.messageClass = value;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x0002DE79 File Offset: 0x0002C079
		// (set) Token: 0x06000AC4 RID: 2756 RVA: 0x0002DE81 File Offset: 0x0002C081
		internal EnhancedTimeSpan? AgeLimitForRetention
		{
			get
			{
				return this.ageLimitForRetention;
			}
			set
			{
				this.ageLimitForRetention = value;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x0002DE8A File Offset: 0x0002C08A
		// (set) Token: 0x06000AC6 RID: 2758 RVA: 0x0002DE92 File Offset: 0x0002C092
		internal bool RetentionEnabled
		{
			get
			{
				return this.retentionEnabled;
			}
			set
			{
				this.retentionEnabled = value;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0002DE9B File Offset: 0x0002C09B
		// (set) Token: 0x06000AC8 RID: 2760 RVA: 0x0002DEA3 File Offset: 0x0002C0A3
		internal RetentionActionType RetentionAction
		{
			get
			{
				return this.retentionAction;
			}
			set
			{
				this.retentionAction = value;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x0002DEAC File Offset: 0x0002C0AC
		// (set) Token: 0x06000ACA RID: 2762 RVA: 0x0002DEB4 File Offset: 0x0002C0B4
		internal bool JournalingEnabled
		{
			get
			{
				return this.journalingEnabled;
			}
			set
			{
				this.journalingEnabled = value;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x0002DEBD File Offset: 0x0002C0BD
		// (set) Token: 0x06000ACC RID: 2764 RVA: 0x0002DEC5 File Offset: 0x0002C0C5
		internal RetentionDateType TriggerForRetention
		{
			get
			{
				return this.triggerForRetention;
			}
			set
			{
				this.triggerForRetention = value;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x0002DECE File Offset: 0x0002C0CE
		// (set) Token: 0x06000ACE RID: 2766 RVA: 0x0002DED6 File Offset: 0x0002C0D6
		internal Guid? MoveToDestinationFolder
		{
			get
			{
				return this.moveToDestinationFolder;
			}
			set
			{
				this.moveToDestinationFolder = value;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x0002DEDF File Offset: 0x0002C0DF
		// (set) Token: 0x06000AD0 RID: 2768 RVA: 0x0002DEE7 File Offset: 0x0002C0E7
		internal string MoveToDestinationFolderName
		{
			get
			{
				return this.moveToDestinationFolderName;
			}
			set
			{
				this.moveToDestinationFolderName = value;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x0002DEF0 File Offset: 0x0002C0F0
		// (set) Token: 0x06000AD2 RID: 2770 RVA: 0x0002DEF8 File Offset: 0x0002C0F8
		internal string ManagedFolderName
		{
			get
			{
				return this.managedFolderName;
			}
			set
			{
				this.managedFolderName = value;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x0002DF01 File Offset: 0x0002C101
		// (set) Token: 0x06000AD4 RID: 2772 RVA: 0x0002DF09 File Offset: 0x0002C109
		internal ADObjectId AddressForJournaling
		{
			get
			{
				return this.addressForJournaling;
			}
			set
			{
				this.addressForJournaling = value;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x0002DF12 File Offset: 0x0002C112
		// (set) Token: 0x06000AD6 RID: 2774 RVA: 0x0002DF1A File Offset: 0x0002C11A
		internal string LabelForJournaling
		{
			get
			{
				return this.labelForJournaling;
			}
			set
			{
				this.labelForJournaling = value;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x0002DF23 File Offset: 0x0002C123
		// (set) Token: 0x06000AD8 RID: 2776 RVA: 0x0002DF2B File Offset: 0x0002C12B
		internal JournalingFormat MessageFormatForJournaling
		{
			get
			{
				return this.messageFormatForJournaling;
			}
			set
			{
				this.messageFormatForJournaling = value;
			}
		}

		// Token: 0x04000813 RID: 2067
		private Guid guid;

		// Token: 0x04000814 RID: 2068
		private string name;

		// Token: 0x04000815 RID: 2069
		private string messageClass;

		// Token: 0x04000816 RID: 2070
		private EnhancedTimeSpan? ageLimitForRetention;

		// Token: 0x04000817 RID: 2071
		private bool retentionEnabled;

		// Token: 0x04000818 RID: 2072
		private RetentionActionType retentionAction;

		// Token: 0x04000819 RID: 2073
		private bool journalingEnabled;

		// Token: 0x0400081A RID: 2074
		private RetentionDateType triggerForRetention;

		// Token: 0x0400081B RID: 2075
		private Guid? moveToDestinationFolder;

		// Token: 0x0400081C RID: 2076
		private string moveToDestinationFolderName;

		// Token: 0x0400081D RID: 2077
		private string managedFolderName;

		// Token: 0x0400081E RID: 2078
		private ADObjectId addressForJournaling;

		// Token: 0x0400081F RID: 2079
		private string labelForJournaling;

		// Token: 0x04000820 RID: 2080
		private JournalingFormat messageFormatForJournaling;
	}
}
