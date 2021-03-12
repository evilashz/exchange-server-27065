using System;
using Microsoft.Exchange.Clients.Owa.Premium;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002B1 RID: 689
	internal sealed class CalendarDataSource : CalendarFolderDataSourceBase
	{
		// Token: 0x06001AFD RID: 6909 RVA: 0x0009B2D0 File Offset: 0x000994D0
		public CalendarDataSource(UserContext userContext, CalendarFolder folder, DateRange[] dateRanges, PropertyDefinition[] properties) : base(dateRanges, properties)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (folder == null)
			{
				throw new ArgumentNullException("folder");
			}
			this.userContext = userContext;
			this.folder = folder;
			this.folderId = OwaStoreObjectId.CreateFromStoreObject(folder);
			base.Load((ExDateTime start, ExDateTime end) => folder.GetCalendarView(start, end, properties));
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x0009B360 File Offset: 0x00099560
		public override OwaStoreObjectId GetItemId(int index)
		{
			VersionedId versionedId;
			if (base.TryGetPropertyValue<VersionedId>(index, ItemSchema.Id, out versionedId))
			{
				return OwaStoreObjectId.CreateFromItemId(versionedId.ObjectId, this.folder);
			}
			ExTraceGlobals.CalendarDataTracer.TraceDebug(0L, "Couldn't get id from the item, skipping...");
			return null;
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x0009B3A4 File Offset: 0x000995A4
		public override string GetChangeKey(int index)
		{
			VersionedId versionedId;
			if (base.TryGetPropertyValue<VersionedId>(index, ItemSchema.Id, out versionedId))
			{
				return versionedId.ChangeKeyAsBase64String();
			}
			ExTraceGlobals.CalendarDataTracer.TraceDebug(0L, "Couldn't get id from the item, skipping...");
			return null;
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x0009B3DC File Offset: 0x000995DC
		public override string GetCssClassName(int index)
		{
			string text = "noClrCal";
			bool flag = false;
			string[] categories = base.GetCategories(index);
			if (categories != null && 0 < categories.Length)
			{
				MasterCategoryList masterCategoryList = this.userContext.GetMasterCategoryList(this.folderId);
				if (masterCategoryList != null)
				{
					for (int i = 0; i < categories.Length; i++)
					{
						Category category = masterCategoryList[categories[i]];
						if (category != null && category.Color != -1)
						{
							flag = true;
							text = CategorySwatch.GetCategoryClassName(category);
							break;
						}
					}
				}
			}
			int num;
			if (!flag && base.TryGetPropertyValue<int>(index, CalendarItemBaseSchema.AppointmentColor, out num))
			{
				if (num < 0 || num >= CalendarDataSource.categoryColorOldLabelsToColors.Length)
				{
					num = 0;
				}
				text = CategorySwatch.GetCategoryClassNameFromColor(CalendarDataSource.categoryColorOldLabelsToColors[num]);
			}
			if (StringComparer.OrdinalIgnoreCase.Compare(text, "noClr") == 0)
			{
				text = "noClrCal";
			}
			return text;
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06001B01 RID: 6913 RVA: 0x0009B49C File Offset: 0x0009969C
		public override SharedType SharedType
		{
			get
			{
				if (this.sharedType == null)
				{
					if (this.userContext.IsInOtherMailbox(this.folder))
					{
						this.sharedType = new SharedType?(SharedType.InternalFullDetail);
					}
					else if (Utilities.IsCrossOrgFolder(this.folder))
					{
						this.sharedType = new SharedType?(SharedType.CrossOrg);
					}
					else if (Utilities.IsWebCalendarFolder(this.folder))
					{
						this.sharedType = new SharedType?(SharedType.WebCalendar);
					}
					else
					{
						this.sharedType = new SharedType?(SharedType.None);
					}
				}
				return this.sharedType.Value;
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06001B02 RID: 6914 RVA: 0x0009B524 File Offset: 0x00099724
		public override WorkingHours WorkingHours
		{
			get
			{
				if (this.workingHours == null)
				{
					if (Utilities.IsOtherMailbox(this.folder))
					{
						this.workingHours = this.userContext.GetOthersWorkingHours(this.folderId);
					}
					else
					{
						this.workingHours = this.userContext.WorkingHours;
					}
				}
				return this.workingHours;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06001B03 RID: 6915 RVA: 0x0009B576 File Offset: 0x00099776
		public override bool UserCanReadItem
		{
			get
			{
				return CalendarUtilities.UserHasRightToLoad(this.folder);
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06001B04 RID: 6916 RVA: 0x0009B583 File Offset: 0x00099783
		public override bool UserCanCreateItem
		{
			get
			{
				return Utilities.CanCreateItemInFolder(this.folder) && this.SharedType != SharedType.CrossOrg && this.SharedType != SharedType.WebCalendar;
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06001B05 RID: 6917 RVA: 0x0009B5A9 File Offset: 0x000997A9
		public override string FolderClassName
		{
			get
			{
				return this.folder.ClassName;
			}
		}

		// Token: 0x0400131C RID: 4892
		private static readonly int[] categoryColorOldLabelsToColors = new int[]
		{
			-1,
			0,
			7,
			4,
			12,
			1,
			22,
			21,
			8,
			5,
			3
		};

		// Token: 0x0400131D RID: 4893
		private CalendarFolder folder;

		// Token: 0x0400131E RID: 4894
		private UserContext userContext;

		// Token: 0x0400131F RID: 4895
		private WorkingHours workingHours;

		// Token: 0x04001320 RID: 4896
		private SharedType? sharedType;

		// Token: 0x04001321 RID: 4897
		private OwaStoreObjectId folderId;
	}
}
