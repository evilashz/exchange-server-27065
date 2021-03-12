using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000100 RID: 256
	internal sealed class CalendarPermissionInformation : PermissionInformationBase<CalendarPermissionType>
	{
		// Token: 0x0600072E RID: 1838 RVA: 0x00023AB1 File Offset: 0x00021CB1
		public CalendarPermissionInformation()
		{
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00023AB9 File Offset: 0x00021CB9
		public CalendarPermissionInformation(CalendarPermissionType permissionElement) : base(permissionElement)
		{
			this.permissionLevel = permissionElement.CalendarPermissionLevel;
			this.freeBusyAccess = this.ComputeFreeBusyAccess();
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x00023ADA File Offset: 0x00021CDA
		// (set) Token: 0x06000731 RID: 1841 RVA: 0x00023AE2 File Offset: 0x00021CE2
		internal FreeBusyAccess? FreeBusyAccess
		{
			get
			{
				return this.freeBusyAccess;
			}
			set
			{
				this.freeBusyAccess = value;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x00023AEB File Offset: 0x00021CEB
		// (set) Token: 0x06000733 RID: 1843 RVA: 0x00023AF3 File Offset: 0x00021CF3
		internal CalendarPermissionLevelType CalendarPermissionLevel
		{
			get
			{
				return this.permissionLevel;
			}
			set
			{
				this.permissionLevel = value;
			}
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00023AFC File Offset: 0x00021CFC
		internal override bool DoAnyNonPermissionLevelFieldsHaveValue()
		{
			return base.DoAnyNonPermissionLevelFieldsHaveValue() || this.FreeBusyAccess != null;
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00023B21 File Offset: 0x00021D21
		private static PermissionLevel ConvertCalendarPermissionLevel(CalendarPermissionLevelType calendarPermissionLevel)
		{
			if (calendarPermissionLevel == CalendarPermissionLevelType.FreeBusyTimeOnly || calendarPermissionLevel == CalendarPermissionLevelType.FreeBusyTimeAndSubjectAndLocation)
			{
				return PermissionLevel.Custom;
			}
			return (PermissionLevel)calendarPermissionLevel;
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00023B31 File Offset: 0x00021D31
		protected override PermissionLevel GetPermissionLevelToSet()
		{
			return CalendarPermissionInformation.ConvertCalendarPermissionLevel(this.CalendarPermissionLevel);
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00023B3E File Offset: 0x00021D3E
		internal override bool IsNonCustomPermissionLevelSet()
		{
			return this.CalendarPermissionLevel != CalendarPermissionLevelType.Custom;
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00023B50 File Offset: 0x00021D50
		protected override void SetByTypePermissionFieldsOntoPermission(Permission permission)
		{
			CalendarFolderPermission calendarFolderPermission = (CalendarFolderPermission)permission;
			if (this.FreeBusyAccess != null)
			{
				calendarFolderPermission.FreeBusyAccess = this.FreeBusyAccess.Value;
			}
			else
			{
				switch (this.CalendarPermissionLevel)
				{
				case CalendarPermissionLevelType.Owner:
				case CalendarPermissionLevelType.PublishingEditor:
				case CalendarPermissionLevelType.Editor:
				case CalendarPermissionLevelType.PublishingAuthor:
				case CalendarPermissionLevelType.Author:
				case CalendarPermissionLevelType.NoneditingAuthor:
				case CalendarPermissionLevelType.Reviewer:
					calendarFolderPermission.FreeBusyAccess = Microsoft.Exchange.Data.Storage.FreeBusyAccess.Details;
					goto IL_7A;
				case CalendarPermissionLevelType.FreeBusyTimeOnly:
				case CalendarPermissionLevelType.FreeBusyTimeAndSubjectAndLocation:
					goto IL_7A;
				}
				calendarFolderPermission.FreeBusyAccess = Microsoft.Exchange.Data.Storage.FreeBusyAccess.None;
			}
			IL_7A:
			if (this.CalendarPermissionLevel == CalendarPermissionLevelType.FreeBusyTimeOnly)
			{
				calendarFolderPermission.CanReadItems = false;
				calendarFolderPermission.CanCreateItems = false;
				calendarFolderPermission.CanCreateSubfolders = false;
				calendarFolderPermission.IsFolderOwner = false;
				calendarFolderPermission.IsFolderVisible = false;
				calendarFolderPermission.IsFolderContact = false;
				calendarFolderPermission.EditItems = ItemPermissionScope.None;
				calendarFolderPermission.DeleteItems = ItemPermissionScope.None;
				calendarFolderPermission.FreeBusyAccess = Microsoft.Exchange.Data.Storage.FreeBusyAccess.Basic;
				return;
			}
			if (this.CalendarPermissionLevel == CalendarPermissionLevelType.FreeBusyTimeAndSubjectAndLocation)
			{
				calendarFolderPermission.CanReadItems = false;
				calendarFolderPermission.CanCreateItems = false;
				calendarFolderPermission.CanCreateSubfolders = false;
				calendarFolderPermission.IsFolderOwner = false;
				calendarFolderPermission.IsFolderVisible = false;
				calendarFolderPermission.IsFolderContact = false;
				calendarFolderPermission.EditItems = ItemPermissionScope.None;
				calendarFolderPermission.DeleteItems = ItemPermissionScope.None;
				calendarFolderPermission.FreeBusyAccess = Microsoft.Exchange.Data.Storage.FreeBusyAccess.Details;
			}
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00023C6A File Offset: 0x00021E6A
		protected override CalendarPermissionType CreateDefaultBasePermissionType()
		{
			return new CalendarPermissionType();
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x00023C74 File Offset: 0x00021E74
		// (set) Token: 0x0600073B RID: 1851 RVA: 0x00023CC4 File Offset: 0x00021EC4
		internal override bool? CanReadItems
		{
			get
			{
				base.EnsurePermissionElementIsNotNull();
				if (base.PermissionElement.ReadItems != null)
				{
					return new bool?(base.PermissionElement.ReadItems.Value == CalendarPermissionReadAccess.FullDetails);
				}
				return null;
			}
			set
			{
				base.EnsurePermissionElementIsNotNull();
				if (value == null)
				{
					base.PermissionElement.ReadItems = null;
					return;
				}
				if (value.Value)
				{
					base.PermissionElement.ReadItems = new CalendarPermissionReadAccess?(CalendarPermissionReadAccess.FullDetails);
					return;
				}
				base.PermissionElement.ReadItems = new CalendarPermissionReadAccess?(CalendarPermissionReadAccess.None);
			}
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00023D24 File Offset: 0x00021F24
		private FreeBusyAccess? ComputeFreeBusyAccess()
		{
			if (base.PermissionElement.ReadItems != null)
			{
				switch (base.PermissionElement.ReadItems.Value)
				{
				case CalendarPermissionReadAccess.TimeOnly:
					return new FreeBusyAccess?(Microsoft.Exchange.Data.Storage.FreeBusyAccess.Basic);
				case CalendarPermissionReadAccess.TimeAndSubjectAndLocation:
					return new FreeBusyAccess?(Microsoft.Exchange.Data.Storage.FreeBusyAccess.Details);
				case CalendarPermissionReadAccess.FullDetails:
					return new FreeBusyAccess?(Microsoft.Exchange.Data.Storage.FreeBusyAccess.Details);
				}
				return new FreeBusyAccess?(Microsoft.Exchange.Data.Storage.FreeBusyAccess.None);
			}
			return null;
		}

		// Token: 0x040006EB RID: 1771
		private FreeBusyAccess? freeBusyAccess;

		// Token: 0x040006EC RID: 1772
		private CalendarPermissionLevelType permissionLevel;
	}
}
