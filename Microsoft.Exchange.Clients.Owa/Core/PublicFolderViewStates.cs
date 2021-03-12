using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000225 RID: 549
	internal class PublicFolderViewStates : FolderViewStates
	{
		// Token: 0x06001270 RID: 4720 RVA: 0x000707AC File Offset: 0x0006E9AC
		internal PublicFolderViewStates(UserContext userContext, Folder folder)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (folder == null)
			{
				throw new ArgumentNullException("folder");
			}
			this.cache = PublicFolderViewStatesCache.GetInstance(userContext);
			OwaStoreObjectId owaStoreObjectId = OwaStoreObjectId.CreateFromStoreObject(folder);
			this.folderId = owaStoreObjectId.ToString();
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001271 RID: 4721 RVA: 0x00070805 File Offset: 0x0006EA05
		private bool ExistsInCache
		{
			get
			{
				return this.cache.CacheEntryExists(this.folderId);
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x00070818 File Offset: 0x0006EA18
		private PublicFolderViewStatesEntry CacheEntryForGet
		{
			get
			{
				if (this.ExistsInCache)
				{
					PublicFolderViewStatesEntry publicFolderViewStatesEntry = this.cache[this.folderId];
					if (publicFolderViewStatesEntry != null)
					{
						publicFolderViewStatesEntry.UpdateLastAccessedDateTimeTicks();
						return publicFolderViewStatesEntry;
					}
				}
				return this.dummyEntry;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001273 RID: 4723 RVA: 0x00070850 File Offset: 0x0006EA50
		private PublicFolderViewStatesEntry CacheEntryForSet
		{
			get
			{
				if (this.ExistsInCache)
				{
					PublicFolderViewStatesEntry publicFolderViewStatesEntry = this.cache[this.folderId];
					if (publicFolderViewStatesEntry != null)
					{
						publicFolderViewStatesEntry.UpdateLastAccessedDateTimeTicks();
						return publicFolderViewStatesEntry;
					}
				}
				PublicFolderViewStatesEntry publicFolderViewStatesEntry2 = new PublicFolderViewStatesEntry(this.folderId);
				this.cache.AddEntry(this.folderId, publicFolderViewStatesEntry2);
				publicFolderViewStatesEntry2.UpdateLastAccessedDateTimeTicks();
				return publicFolderViewStatesEntry2;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x000708A7 File Offset: 0x0006EAA7
		// (set) Token: 0x06001275 RID: 4725 RVA: 0x000708B4 File Offset: 0x0006EAB4
		internal override CalendarViewType CalendarViewType
		{
			get
			{
				return (CalendarViewType)this.CacheEntryForGet.CalendarViewType;
			}
			set
			{
				if (!FolderViewStates.ValidateCalendarViewType(value))
				{
					throw new ArgumentOutOfRangeException("value", "Cannot set CalendarViewType to invalid value.");
				}
				this.CacheEntryForSet.CalendarViewType = (int)value;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001276 RID: 4726 RVA: 0x000708DA File Offset: 0x0006EADA
		// (set) Token: 0x06001277 RID: 4727 RVA: 0x000708E7 File Offset: 0x0006EAE7
		internal override int DailyViewDays
		{
			get
			{
				return this.CacheEntryForGet.DailyViewDays;
			}
			set
			{
				if (!FolderViewStates.ValidateDailyViewDays(value))
				{
					throw new ArgumentOutOfRangeException("value", "Cannot set DailyViewDays to invalid value.");
				}
				this.CacheEntryForSet.DailyViewDays = value;
			}
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x00070910 File Offset: 0x0006EB10
		internal override bool GetMultiLine(bool defaultValue)
		{
			if (this.ExistsInCache && this.CacheEntryForGet.MultiLine != null)
			{
				return this.CacheEntryForGet.MultiLine.Value;
			}
			return defaultValue;
		}

		// Token: 0x1700050C RID: 1292
		// (set) Token: 0x06001279 RID: 4729 RVA: 0x0007094F File Offset: 0x0006EB4F
		internal override bool MultiLine
		{
			set
			{
				this.CacheEntryForSet.MultiLine = new bool?(value);
			}
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x00070964 File Offset: 0x0006EB64
		internal override ReadingPanePosition GetReadingPanePosition(ReadingPanePosition defaultValue)
		{
			if (this.ExistsInCache)
			{
				int? readingPanePosition = this.CacheEntryForGet.ReadingPanePosition;
				if (readingPanePosition != null)
				{
					return (ReadingPanePosition)readingPanePosition.Value;
				}
			}
			return defaultValue;
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x0600127B RID: 4731 RVA: 0x00070997 File Offset: 0x0006EB97
		// (set) Token: 0x0600127C RID: 4732 RVA: 0x000709A0 File Offset: 0x0006EBA0
		internal override ReadingPanePosition ReadingPanePosition
		{
			get
			{
				return this.GetReadingPanePosition(ReadingPanePosition.Right);
			}
			set
			{
				if (!FolderViewStates.ValidateReadingPanePosition(value))
				{
					throw new ArgumentOutOfRangeException("value", "Cannot set ReadingPanePosition to invalid value.");
				}
				this.CacheEntryForSet.ReadingPanePosition = new int?((int)value);
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x0600127D RID: 4733 RVA: 0x000709CB File Offset: 0x0006EBCB
		// (set) Token: 0x0600127E RID: 4734 RVA: 0x000709D8 File Offset: 0x0006EBD8
		internal override ReadingPanePosition ReadingPanePositionMultiDay
		{
			get
			{
				return (ReadingPanePosition)this.CacheEntryForGet.ReadingPanePositionMultiDay;
			}
			set
			{
				if (!FolderViewStates.ValidateReadingPanePosition(value))
				{
					throw new ArgumentOutOfRangeException("value", "Cannot set ReadingPanePositionMultiDay to invalid value.");
				}
				this.CacheEntryForSet.ReadingPanePositionMultiDay = (int)value;
			}
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x000709FE File Offset: 0x0006EBFE
		internal override string GetSortColumn(string defaultValue)
		{
			if (this.ExistsInCache && this.CacheEntryForGet.SortColumn != null)
			{
				return this.CacheEntryForGet.SortColumn;
			}
			return defaultValue;
		}

		// Token: 0x1700050F RID: 1295
		// (set) Token: 0x06001280 RID: 4736 RVA: 0x00070A22 File Offset: 0x0006EC22
		internal override string SortColumn
		{
			set
			{
				this.CacheEntryForSet.SortColumn = value;
			}
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x00070A30 File Offset: 0x0006EC30
		internal override SortOrder GetSortOrder(SortOrder defaultValue)
		{
			if (this.ExistsInCache && this.CacheEntryForGet.SortOrder != null)
			{
				return (SortOrder)this.CacheEntryForGet.SortOrder.Value;
			}
			return defaultValue;
		}

		// Token: 0x17000510 RID: 1296
		// (set) Token: 0x06001282 RID: 4738 RVA: 0x00070A6F File Offset: 0x0006EC6F
		internal override SortOrder SortOrder
		{
			set
			{
				if (!FolderViewStates.ValidateSortOrder(value))
				{
					throw new ArgumentOutOfRangeException("value", "Cannot set SortOrder to invalid value.");
				}
				this.CacheEntryForSet.SortOrder = new int?((int)value);
			}
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x00070A9C File Offset: 0x0006EC9C
		internal override int GetViewHeight(int defaultViewHeight)
		{
			if (this.CacheEntryForGet.ViewHeight != null)
			{
				return this.CacheEntryForGet.ViewHeight.Value;
			}
			return defaultViewHeight;
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x00070AD3 File Offset: 0x0006ECD3
		// (set) Token: 0x06001285 RID: 4741 RVA: 0x00070AE0 File Offset: 0x0006ECE0
		internal override int ViewHeight
		{
			get
			{
				return this.GetViewHeight(250);
			}
			set
			{
				if (!FolderViewStates.ValidateWidthOrHeight(value))
				{
					throw new ArgumentOutOfRangeException("value", "Cannot set ViewHeight to invalid value: " + value);
				}
				this.CacheEntryForSet.ViewHeight = new int?(value);
			}
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x00070B18 File Offset: 0x0006ED18
		internal override int GetViewWidth(int defaultViewWidth)
		{
			if (this.CacheEntryForGet.ViewWidth != null)
			{
				return this.CacheEntryForGet.ViewWidth.Value;
			}
			return defaultViewWidth;
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001287 RID: 4743 RVA: 0x00070B4F File Offset: 0x0006ED4F
		// (set) Token: 0x06001288 RID: 4744 RVA: 0x00070B5C File Offset: 0x0006ED5C
		internal override int ViewWidth
		{
			get
			{
				return this.GetViewWidth(450);
			}
			set
			{
				if (!FolderViewStates.ValidateWidthOrHeight(value))
				{
					throw new ArgumentOutOfRangeException("value", "Cannot set ViewWidth to invalid value: " + value);
				}
				this.CacheEntryForSet.ViewWidth = new int?(value);
			}
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x00070B92 File Offset: 0x0006ED92
		internal override void Save()
		{
			this.cache.Commit();
		}

		// Token: 0x04000CAC RID: 3244
		private string folderId;

		// Token: 0x04000CAD RID: 3245
		private PublicFolderViewStatesCache cache;

		// Token: 0x04000CAE RID: 3246
		private PublicFolderViewStatesEntry dummyEntry = new PublicFolderViewStatesEntry();
	}
}
