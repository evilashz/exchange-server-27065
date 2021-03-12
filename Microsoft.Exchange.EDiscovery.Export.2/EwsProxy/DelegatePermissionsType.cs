using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001ED RID: 493
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class DelegatePermissionsType
	{
		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x0600141E RID: 5150 RVA: 0x0002599B File Offset: 0x00023B9B
		// (set) Token: 0x0600141F RID: 5151 RVA: 0x000259A3 File Offset: 0x00023BA3
		public DelegateFolderPermissionLevelType CalendarFolderPermissionLevel
		{
			get
			{
				return this.calendarFolderPermissionLevelField;
			}
			set
			{
				this.calendarFolderPermissionLevelField = value;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001420 RID: 5152 RVA: 0x000259AC File Offset: 0x00023BAC
		// (set) Token: 0x06001421 RID: 5153 RVA: 0x000259B4 File Offset: 0x00023BB4
		[XmlIgnore]
		public bool CalendarFolderPermissionLevelSpecified
		{
			get
			{
				return this.calendarFolderPermissionLevelFieldSpecified;
			}
			set
			{
				this.calendarFolderPermissionLevelFieldSpecified = value;
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06001422 RID: 5154 RVA: 0x000259BD File Offset: 0x00023BBD
		// (set) Token: 0x06001423 RID: 5155 RVA: 0x000259C5 File Offset: 0x00023BC5
		public DelegateFolderPermissionLevelType TasksFolderPermissionLevel
		{
			get
			{
				return this.tasksFolderPermissionLevelField;
			}
			set
			{
				this.tasksFolderPermissionLevelField = value;
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001424 RID: 5156 RVA: 0x000259CE File Offset: 0x00023BCE
		// (set) Token: 0x06001425 RID: 5157 RVA: 0x000259D6 File Offset: 0x00023BD6
		[XmlIgnore]
		public bool TasksFolderPermissionLevelSpecified
		{
			get
			{
				return this.tasksFolderPermissionLevelFieldSpecified;
			}
			set
			{
				this.tasksFolderPermissionLevelFieldSpecified = value;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x000259DF File Offset: 0x00023BDF
		// (set) Token: 0x06001427 RID: 5159 RVA: 0x000259E7 File Offset: 0x00023BE7
		public DelegateFolderPermissionLevelType InboxFolderPermissionLevel
		{
			get
			{
				return this.inboxFolderPermissionLevelField;
			}
			set
			{
				this.inboxFolderPermissionLevelField = value;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x000259F0 File Offset: 0x00023BF0
		// (set) Token: 0x06001429 RID: 5161 RVA: 0x000259F8 File Offset: 0x00023BF8
		[XmlIgnore]
		public bool InboxFolderPermissionLevelSpecified
		{
			get
			{
				return this.inboxFolderPermissionLevelFieldSpecified;
			}
			set
			{
				this.inboxFolderPermissionLevelFieldSpecified = value;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x00025A01 File Offset: 0x00023C01
		// (set) Token: 0x0600142B RID: 5163 RVA: 0x00025A09 File Offset: 0x00023C09
		public DelegateFolderPermissionLevelType ContactsFolderPermissionLevel
		{
			get
			{
				return this.contactsFolderPermissionLevelField;
			}
			set
			{
				this.contactsFolderPermissionLevelField = value;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x00025A12 File Offset: 0x00023C12
		// (set) Token: 0x0600142D RID: 5165 RVA: 0x00025A1A File Offset: 0x00023C1A
		[XmlIgnore]
		public bool ContactsFolderPermissionLevelSpecified
		{
			get
			{
				return this.contactsFolderPermissionLevelFieldSpecified;
			}
			set
			{
				this.contactsFolderPermissionLevelFieldSpecified = value;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x00025A23 File Offset: 0x00023C23
		// (set) Token: 0x0600142F RID: 5167 RVA: 0x00025A2B File Offset: 0x00023C2B
		public DelegateFolderPermissionLevelType NotesFolderPermissionLevel
		{
			get
			{
				return this.notesFolderPermissionLevelField;
			}
			set
			{
				this.notesFolderPermissionLevelField = value;
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001430 RID: 5168 RVA: 0x00025A34 File Offset: 0x00023C34
		// (set) Token: 0x06001431 RID: 5169 RVA: 0x00025A3C File Offset: 0x00023C3C
		[XmlIgnore]
		public bool NotesFolderPermissionLevelSpecified
		{
			get
			{
				return this.notesFolderPermissionLevelFieldSpecified;
			}
			set
			{
				this.notesFolderPermissionLevelFieldSpecified = value;
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06001432 RID: 5170 RVA: 0x00025A45 File Offset: 0x00023C45
		// (set) Token: 0x06001433 RID: 5171 RVA: 0x00025A4D File Offset: 0x00023C4D
		public DelegateFolderPermissionLevelType JournalFolderPermissionLevel
		{
			get
			{
				return this.journalFolderPermissionLevelField;
			}
			set
			{
				this.journalFolderPermissionLevelField = value;
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06001434 RID: 5172 RVA: 0x00025A56 File Offset: 0x00023C56
		// (set) Token: 0x06001435 RID: 5173 RVA: 0x00025A5E File Offset: 0x00023C5E
		[XmlIgnore]
		public bool JournalFolderPermissionLevelSpecified
		{
			get
			{
				return this.journalFolderPermissionLevelFieldSpecified;
			}
			set
			{
				this.journalFolderPermissionLevelFieldSpecified = value;
			}
		}

		// Token: 0x04000DDF RID: 3551
		private DelegateFolderPermissionLevelType calendarFolderPermissionLevelField;

		// Token: 0x04000DE0 RID: 3552
		private bool calendarFolderPermissionLevelFieldSpecified;

		// Token: 0x04000DE1 RID: 3553
		private DelegateFolderPermissionLevelType tasksFolderPermissionLevelField;

		// Token: 0x04000DE2 RID: 3554
		private bool tasksFolderPermissionLevelFieldSpecified;

		// Token: 0x04000DE3 RID: 3555
		private DelegateFolderPermissionLevelType inboxFolderPermissionLevelField;

		// Token: 0x04000DE4 RID: 3556
		private bool inboxFolderPermissionLevelFieldSpecified;

		// Token: 0x04000DE5 RID: 3557
		private DelegateFolderPermissionLevelType contactsFolderPermissionLevelField;

		// Token: 0x04000DE6 RID: 3558
		private bool contactsFolderPermissionLevelFieldSpecified;

		// Token: 0x04000DE7 RID: 3559
		private DelegateFolderPermissionLevelType notesFolderPermissionLevelField;

		// Token: 0x04000DE8 RID: 3560
		private bool notesFolderPermissionLevelFieldSpecified;

		// Token: 0x04000DE9 RID: 3561
		private DelegateFolderPermissionLevelType journalFolderPermissionLevelField;

		// Token: 0x04000DEA RID: 3562
		private bool journalFolderPermissionLevelFieldSpecified;
	}
}
