using System;
using System.Globalization;
using Microsoft.Exchange.Clients.Owa.Premium;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002AD RID: 685
	internal class CalendarAdapter : CalendarAdapterBase
	{
		// Token: 0x06001A9B RID: 6811 RVA: 0x0009A163 File Offset: 0x00098363
		public CalendarAdapter(UserContext userContext, StoreObjectId storeObjectId) : this(userContext, OwaStoreObjectId.CreateFromSessionFolderId(userContext, userContext.MailboxSession, storeObjectId))
		{
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x0009A17C File Offset: 0x0009837C
		public CalendarAdapter(UserContext userContext, OwaStoreObjectId folderId)
		{
			this.ownFolder = true;
			base..ctor();
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (folderId == null)
			{
				throw new ArgumentNullException("folderId");
			}
			switch (folderId.OwaStoreObjectIdType)
			{
			case OwaStoreObjectIdType.MailBoxObject:
			case OwaStoreObjectIdType.PublicStoreFolder:
			case OwaStoreObjectIdType.ArchiveMailboxObject:
				goto IL_A8;
			case OwaStoreObjectIdType.OtherUserMailboxObject:
			case OwaStoreObjectIdType.GSCalendar:
				if (folderId.MailboxOwnerLegacyDN == null)
				{
					ExTraceGlobals.CalendarCallTracer.TraceDebug((long)this.GetHashCode(), "MailboxOwnerLegacyDN cannot be null");
					throw new ArgumentException("MailboxOwnerLegacyDN cannot be null");
				}
				goto IL_A8;
			}
			ExTraceGlobals.CalendarCallTracer.TraceDebug<OwaStoreObjectIdType>((long)this.GetHashCode(), "Does not support this type of OwaStoreObjectId: {0}", folderId.OwaStoreObjectIdType);
			throw new ArgumentException("Does not support this type of OwaStoreObjectId");
			IL_A8:
			this.UserContext = userContext;
			this.FolderId = folderId;
			this.ownFolder = true;
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x0009A248 File Offset: 0x00098448
		public CalendarAdapter(UserContext userContext, CalendarFolder folder)
		{
			this.ownFolder = true;
			base..ctor();
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (folder == null)
			{
				throw new ArgumentNullException("folder");
			}
			this.UserContext = userContext;
			this.folder = folder;
			this.FolderId = OwaStoreObjectId.CreateFromStoreObject(folder);
			this.ownFolder = false;
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x0009A29F File Offset: 0x0009849F
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.folder != null && this.ownFolder)
			{
				this.folder.Dispose();
				this.folder = null;
			}
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x0009A2C8 File Offset: 0x000984C8
		public void LoadData(PropertyDefinition[] queryProperties, ExDateTime[] days, bool addOwaConditionAdvisor, bool throwIfFolderNotFound)
		{
			if (queryProperties == null || queryProperties.Length == 0)
			{
				throw new ArgumentNullException("queryProperties");
			}
			base.DateRanges = CalendarAdapterBase.ConvertDateTimeArrayToDateRangeArray(days);
			if (this.folder == null)
			{
				try
				{
					this.folder = this.OpenFolder(throwIfFolderNotFound);
				}
				catch (OwaSharedFromOlderVersionException)
				{
				}
				catch (OwaLoadSharedCalendarFailedException)
				{
					return;
				}
			}
			this.GetDataAndUpdateCommonViewIfNecessary(false);
			if (this.folder != null && CalendarUtilities.UserHasRightToLoad(this.folder))
			{
				base.DataSource = new CalendarDataSource(this.UserContext, this.folder, base.DateRanges, queryProperties);
				if (addOwaConditionAdvisor)
				{
					this.AddOwaConditionAdvisorIfNecessary(this.folder);
				}
			}
			else if (this.IsGSCalendar || (this.isFromOlderVersion && this.olderExchangeCalendarTypeInNode != NavigationNodeFolder.OlderExchangeCalendarType.Secondary))
			{
				base.DataSource = new AvailabilityDataSource(this.UserContext, this.SmtpAddress, (!this.IsGSCalendar && this.olderExchangeCalendarTypeInNode == NavigationNodeFolder.OlderExchangeCalendarType.Unknown) ? this.FolderId.StoreObjectId : null, base.DateRanges, true);
			}
			this.dataLoaded = true;
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x0009A3D8 File Offset: 0x000985D8
		public void LoadData(PropertyDefinition[] queryProperties, ExDateTime[] days, bool addOwaConditionAdvisor, ref CalendarViewType viewType, out int viewWidth, out ReadingPanePosition readingPanePosition)
		{
			this.LoadData(queryProperties, days, addOwaConditionAdvisor, 0, 24, ref viewType, out viewWidth, out readingPanePosition);
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x0009A3F8 File Offset: 0x000985F8
		public void LoadData(PropertyDefinition[] queryProperties, ExDateTime[] days, bool addOwaConditionAdvisor, int startHour, int endHour, ref CalendarViewType viewType, out int viewWidth, out ReadingPanePosition readingPanePosition)
		{
			if (queryProperties == null)
			{
				throw new ArgumentNullException("queryProperties");
			}
			if (queryProperties.Length == 0)
			{
				throw new ArgumentException("Length of queryProperties cannot be 0");
			}
			viewWidth = 0;
			readingPanePosition = ReadingPanePosition.Min;
			if (this.folder == null)
			{
				try
				{
					this.folder = this.OpenFolder(false);
				}
				catch (OwaSharedFromOlderVersionException)
				{
				}
				catch (OwaLoadSharedCalendarFailedException)
				{
					return;
				}
			}
			this.GetDataAndUpdateCommonViewIfNecessary(true);
			if (this.folder != null && CalendarUtilities.UserHasRightToLoad(this.folder))
			{
				this.LoadFolderViewStates(this.folder, ref days, ref viewType, out viewWidth, out readingPanePosition);
				base.DateRanges = CalendarAdapterBase.ConvertDateTimeArrayToDateRangeArray(days, startHour, endHour);
				base.DataSource = new CalendarDataSource(this.UserContext, this.folder, base.DateRanges, queryProperties);
				if (addOwaConditionAdvisor)
				{
					this.AddOwaConditionAdvisorIfNecessary(this.folder);
				}
			}
			else if (this.IsGSCalendar || (this.isFromOlderVersion && this.olderExchangeCalendarTypeInNode != NavigationNodeFolder.OlderExchangeCalendarType.Secondary))
			{
				this.LoadFolderViewStates(null, ref days, ref viewType, out viewWidth, out readingPanePosition);
				base.DateRanges = CalendarAdapterBase.ConvertDateTimeArrayToDateRangeArray(days, startHour, endHour);
				base.DataSource = new AvailabilityDataSource(this.UserContext, this.SmtpAddress, (!this.IsGSCalendar && this.olderExchangeCalendarTypeInNode == NavigationNodeFolder.OlderExchangeCalendarType.Unknown) ? this.FolderId.StoreObjectId : null, base.DateRanges, false);
			}
			this.dataLoaded = true;
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x0009A554 File Offset: 0x00098754
		private static void InternalGetFolderViewStates(UserContext userContext, CalendarFolder folder, ref ExDateTime[] days, ref CalendarViewType viewType, out int viewWidth, out ReadingPanePosition readingPanePosition)
		{
			FolderViewStates folderViewStates = userContext.GetFolderViewStates(folder);
			CalendarUtilities.GetCalendarViewParamsFromViewStates(folderViewStates, out viewWidth, ref viewType, out readingPanePosition);
			days = CalendarUtilities.GetViewDays(userContext, days, viewType, OwaStoreObjectId.CreateFromStoreObject(folder), folderViewStates);
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x0009A588 File Offset: 0x00098788
		private void GetDataAndUpdateCommonViewIfNecessary(bool needGetColor)
		{
			NavigationNodeCollection navigationNodeCollection = null;
			NavigationNodeFolder[] array = null;
			if (Utilities.IsWebPartDelegateAccessRequest(OwaContext.Current) || (!needGetColor && this.FolderId.Equals(this.UserContext.CalendarFolderOwaId)) || !this.TryGetNodeFoldersFromNavigationTree(out array, out navigationNodeCollection))
			{
				base.CalendarTitle = ((this.folder != null) ? this.folder.DisplayName : string.Empty);
				this.CalendarColor = -2;
				return;
			}
			this.CalendarColor = CalendarColorManager.GetCalendarFolderColor(this.UserContext, navigationNodeCollection, array);
			OwaStoreObjectId owaStoreObjectId = OwaStoreObjectId.CreateFromNavigationNodeFolder(this.UserContext, array[0]);
			if (owaStoreObjectId.IsArchive)
			{
				base.CalendarTitle = string.Format(LocalizedStrings.GetNonEncoded(-83764036), array[0].Subject, Utilities.GetMailboxOwnerDisplayName((MailboxSession)owaStoreObjectId.GetSession(this.UserContext)));
			}
			else
			{
				base.CalendarTitle = array[0].Subject;
			}
			foreach (NavigationNodeFolder navigationNodeFolder in array)
			{
				if (!navigationNodeFolder.IsGSCalendar && navigationNodeFolder.IsPrimarySharedCalendar)
				{
					navigationNodeFolder.UpgradeToGSCalendar();
				}
				if (this.olderExchangeCalendarTypeInNode == NavigationNodeFolder.OlderExchangeCalendarType.Unknown)
				{
					this.olderExchangeCalendarTypeInNode = navigationNodeFolder.OlderExchangeSharedCalendarType;
				}
			}
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x0009A6A8 File Offset: 0x000988A8
		public void SaveCalendarTypeFromOlderExchangeAsNeeded()
		{
			if (this.isFromOlderVersion)
			{
				NavigationNodeCollection navigationNodeCollection = null;
				NavigationNodeFolder[] array = null;
				if (this.TryGetNodeFoldersFromNavigationTree(out array, out navigationNodeCollection))
				{
					foreach (NavigationNodeFolder navigationNodeFolder in array)
					{
						if (this.olderExchangeCalendarTypeInNode == NavigationNodeFolder.OlderExchangeCalendarType.Unknown)
						{
							navigationNodeFolder.OlderExchangeSharedCalendarType = this.OlderExchangeSharedCalendarType;
						}
					}
					navigationNodeCollection.Save(this.UserContext.MailboxSession);
				}
			}
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x0009A70C File Offset: 0x0009890C
		public static void KeepMapiNotification(UserContext userContext, OwaStoreObjectId folderId)
		{
			using (CalendarAdapter calendarAdapter = new CalendarAdapter(userContext, folderId))
			{
				calendarAdapter.KeepMapiNotification();
			}
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x0009A744 File Offset: 0x00098944
		private void KeepMapiNotification()
		{
			try
			{
				this.folder = this.OpenFolder(false);
			}
			catch (OwaSharedFromOlderVersionException)
			{
				return;
			}
			catch (OwaLoadSharedCalendarFailedException)
			{
				return;
			}
			if (this.folder != null)
			{
				this.AddOwaConditionAdvisorIfNecessary(this.folder);
				if (this.PromotedFolderId != null && this.PromotedFolderId.IsOtherMailbox)
				{
					MailboxSession session = this.folder.Session as MailboxSession;
					this.UserContext.MapiNotificationManager.RenewDelegateHandler(session);
				}
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06001AA7 RID: 6823 RVA: 0x0009A7D0 File Offset: 0x000989D0
		public bool IsGSCalendar
		{
			get
			{
				return this.FolderId.IsGSCalendar;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06001AA8 RID: 6824 RVA: 0x0009A7DD File Offset: 0x000989DD
		public bool IsSecondaryCalendarFromOldExchange
		{
			get
			{
				return this.OlderExchangeSharedCalendarType == NavigationNodeFolder.OlderExchangeCalendarType.Secondary;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06001AA9 RID: 6825 RVA: 0x0009A7E8 File Offset: 0x000989E8
		public NavigationNodeFolder.OlderExchangeCalendarType OlderExchangeSharedCalendarType
		{
			get
			{
				if (!this.dataLoaded)
				{
					return NavigationNodeFolder.OlderExchangeCalendarType.Unknown;
				}
				if (!this.isFromOlderVersion)
				{
					return NavigationNodeFolder.OlderExchangeCalendarType.NotOlderVersion;
				}
				if (this.olderExchangeCalendarTypeInNode != NavigationNodeFolder.OlderExchangeCalendarType.Unknown)
				{
					return this.olderExchangeCalendarTypeInNode;
				}
				switch (((AvailabilityDataSource)base.DataSource).AssociatedCalendarType)
				{
				case AvailabilityDataSource.CalendarType.Primary:
					return NavigationNodeFolder.OlderExchangeCalendarType.Primary;
				case AvailabilityDataSource.CalendarType.Secondary:
					return NavigationNodeFolder.OlderExchangeCalendarType.Secondary;
				}
				return NavigationNodeFolder.OlderExchangeCalendarType.Unknown;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06001AAA RID: 6826 RVA: 0x0009A844 File Offset: 0x00098A44
		public CalendarFolder Folder
		{
			get
			{
				return this.folder;
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06001AAB RID: 6827 RVA: 0x0009A84C File Offset: 0x00098A4C
		// (set) Token: 0x06001AAC RID: 6828 RVA: 0x0009A854 File Offset: 0x00098A54
		public UserContext UserContext { get; private set; }

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06001AAD RID: 6829 RVA: 0x0009A85D File Offset: 0x00098A5D
		// (set) Token: 0x06001AAE RID: 6830 RVA: 0x0009A865 File Offset: 0x00098A65
		public OwaStoreObjectId FolderId { get; private set; }

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06001AAF RID: 6831 RVA: 0x0009A86E File Offset: 0x00098A6E
		// (set) Token: 0x06001AB0 RID: 6832 RVA: 0x0009A876 File Offset: 0x00098A76
		public OwaStoreObjectId PromotedFolderId { get; private set; }

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06001AB1 RID: 6833 RVA: 0x0009A87F File Offset: 0x00098A7F
		// (set) Token: 0x06001AB2 RID: 6834 RVA: 0x0009A887 File Offset: 0x00098A87
		public int CalendarColor { get; protected set; }

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06001AB3 RID: 6835 RVA: 0x0009A890 File Offset: 0x00098A90
		public bool IsPublic
		{
			get
			{
				return this.FolderId.IsPublic;
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001AB4 RID: 6836 RVA: 0x0009A8A0 File Offset: 0x00098AA0
		public string SmtpAddress
		{
			get
			{
				if (this.FolderId.IsPublic)
				{
					return null;
				}
				if (this.exchangePrincipal != null)
				{
					return this.exchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString();
				}
				return this.UserContext.ExchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString();
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001AB5 RID: 6837 RVA: 0x0009A906 File Offset: 0x00098B06
		public string LegacyDN
		{
			get
			{
				return this.FolderId.MailboxOwnerLegacyDN;
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001AB6 RID: 6838 RVA: 0x0009A914 File Offset: 0x00098B14
		public override string CalendarOwnerDisplayName
		{
			get
			{
				if (base.DataSource.SharedType == SharedType.CrossOrg || base.DataSource.SharedType == SharedType.WebCalendar)
				{
					throw new NotSupportedException("CalendarOwnerDisplayName is not support for external shared calendar.");
				}
				if (this.FolderId.IsOtherMailbox || this.FolderId.IsGSCalendar)
				{
					return this.exchangePrincipal.MailboxInfo.DisplayName;
				}
				return this.UserContext.MailboxSession.DisplayName;
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06001AB7 RID: 6839 RVA: 0x0009A983 File Offset: 0x00098B83
		public bool IsInArchiveMailbox
		{
			get
			{
				return this.folder != null && Utilities.IsInArchiveMailbox(this.folder);
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06001AB8 RID: 6840 RVA: 0x0009A99A File Offset: 0x00098B9A
		public bool IsPublishedOut
		{
			get
			{
				return this.folder != null && Utilities.IsPublishedOutFolder(this.folder);
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06001AB9 RID: 6841 RVA: 0x0009A9B1 File Offset: 0x00098BB1
		public bool IsExternalSharedInFolder
		{
			get
			{
				return this.folder != null && Utilities.IsExternalSharedInFolder(this.folder);
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06001ABA RID: 6842 RVA: 0x0009A9C8 File Offset: 0x00098BC8
		public override string IdentityString
		{
			get
			{
				return this.FolderId.ToBase64String();
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06001ABB RID: 6843 RVA: 0x0009A9D5 File Offset: 0x00098BD5
		public ExDateTime LastAttemptTime
		{
			get
			{
				if (!this.IsExternalSharedInFolder)
				{
					return ExDateTime.MinValue;
				}
				return this.folder.LastAttemptedSyncTime;
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06001ABC RID: 6844 RVA: 0x0009A9F0 File Offset: 0x00098BF0
		public ExDateTime LastSuccessSyncTime
		{
			get
			{
				if (!this.IsExternalSharedInFolder)
				{
					return ExDateTime.MinValue;
				}
				return this.folder.LastSuccessfulSyncTime;
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06001ABD RID: 6845 RVA: 0x0009AA0C File Offset: 0x00098C0C
		public string WebCalendarUrl
		{
			get
			{
				if (this.webCalendarUrl == null && base.DataSource.SharedType == SharedType.WebCalendar)
				{
					this.webCalendarUrl = CalendarUtilities.GetWebCalendarUrl((MailboxSession)this.folder.Session, this.FolderId.StoreObjectId);
				}
				return this.webCalendarUrl;
			}
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x0009AA5C File Offset: 0x00098C5C
		private bool TryGetNodeFoldersFromNavigationTree(out NavigationNodeFolder[] navigationNodeFolders, out NavigationNodeCollection navigationNodeCollection)
		{
			navigationNodeFolders = null;
			navigationNodeCollection = NavigationNodeCollection.TryCreateNavigationNodeCollection(this.UserContext, this.UserContext.MailboxSession, NavigationNodeGroupSection.Calendar);
			if (navigationNodeCollection != null)
			{
				try
				{
					if (this.IsGSCalendar)
					{
						navigationNodeFolders = navigationNodeCollection.FindGSCalendarsByLegacyDN(this.FolderId.MailboxOwnerLegacyDN);
					}
					else
					{
						navigationNodeFolders = navigationNodeCollection.FindFoldersById(this.FolderId.StoreObjectId);
					}
					if (navigationNodeFolders != null && navigationNodeFolders.Length > 0)
					{
						return true;
					}
				}
				catch (StoragePermanentException ex)
				{
					string message = string.Format(CultureInfo.InvariantCulture, "CalendarColorManager.GetCalendarFolderColor. Unable to find tree node related to the given calendar. Exception: {0}.", new object[]
					{
						ex.Message
					});
					ExTraceGlobals.CoreCallTracer.TraceDebug(0L, message);
				}
				catch (StorageTransientException ex2)
				{
					string message2 = string.Format(CultureInfo.InvariantCulture, "CalendarColorManager.GetCalendarFolderColor. Unable to find tree node related to the given calendar. Exception: {0}.", new object[]
					{
						ex2.Message
					});
					ExTraceGlobals.CoreCallTracer.TraceDebug(0L, message2);
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x0009AB58 File Offset: 0x00098D58
		private CalendarFolder OpenFolder(bool throwIfFolderNotFound)
		{
			CalendarFolder calendarFolder = null;
			this.isFromOlderVersion = false;
			if (this.exchangePrincipal == null && (this.FolderId.IsOtherMailbox || this.FolderId.IsGSCalendar) && !this.UserContext.DelegateSessionManager.TryGetExchangePrincipal(this.FolderId.MailboxOwnerLegacyDN, out this.exchangePrincipal))
			{
				throw new OwaLoadSharedCalendarFailedException("cannot get exchange principal");
			}
			try
			{
				if (this.IsGSCalendar)
				{
					StoreObjectId storeObjectId = Utilities.TryGetDefaultFolderId(this.UserContext, this.exchangePrincipal, DefaultFolderType.Calendar);
					if (storeObjectId != null)
					{
						this.PromotedFolderId = OwaStoreObjectId.CreateFromOtherUserMailboxFolderId(storeObjectId, this.LegacyDN);
					}
				}
				else
				{
					this.PromotedFolderId = this.FolderId;
				}
				if (this.PromotedFolderId != null)
				{
					try
					{
						calendarFolder = Utilities.GetFolderForContent<CalendarFolder>(this.UserContext, this.PromotedFolderId, CalendarUtilities.FolderViewProperties);
						if (!this.IsGSCalendar)
						{
							this.FolderId = OwaStoreObjectId.CreateFromSessionFolderId(this.FolderId.OwaStoreObjectIdType, this.FolderId.MailboxOwnerLegacyDN, calendarFolder.StoreObjectId);
						}
					}
					catch (ObjectNotFoundException)
					{
						if (throwIfFolderNotFound)
						{
							throw;
						}
					}
					catch (WrongObjectTypeException innerException)
					{
						throw new OwaInvalidRequestException("The folder is not a calendar folder", innerException, this);
					}
				}
			}
			catch (OwaSharedFromOlderVersionException)
			{
				this.isFromOlderVersion = true;
				throw;
			}
			return calendarFolder;
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x0009AC98 File Offset: 0x00098E98
		private void AddOwaConditionAdvisorIfNecessary(CalendarFolder folder)
		{
			Utilities.AddOwaConditionAdvisorIfNecessary(this.UserContext, folder, EventObjectType.None, EventType.None);
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x0009ACA8 File Offset: 0x00098EA8
		private void LoadFolderViewStates(CalendarFolder advicedFolder, ref ExDateTime[] days, ref CalendarViewType viewType, out int viewWidth, out ReadingPanePosition readingPanePosition)
		{
			OwaStoreObjectId owaStoreObjectId = null;
			if (advicedFolder != null)
			{
				owaStoreObjectId = OwaStoreObjectId.CreateFromStoreObject(advicedFolder);
			}
			if (owaStoreObjectId != null && (owaStoreObjectId.Equals(this.UserContext.CalendarFolderOwaId) || owaStoreObjectId.IsPublic))
			{
				CalendarAdapter.InternalGetFolderViewStates(this.UserContext, advicedFolder, ref days, ref viewType, out viewWidth, out readingPanePosition);
				return;
			}
			using (CalendarFolder folderForContent = Utilities.GetFolderForContent<CalendarFolder>(this.UserContext, this.UserContext.CalendarFolderOwaId, CalendarUtilities.FolderViewProperties))
			{
				CalendarAdapter.InternalGetFolderViewStates(this.UserContext, folderForContent, ref days, ref viewType, out viewWidth, out readingPanePosition);
			}
		}

		// Token: 0x0400130B RID: 4875
		private ExchangePrincipal exchangePrincipal;

		// Token: 0x0400130C RID: 4876
		private CalendarFolder folder;

		// Token: 0x0400130D RID: 4877
		private bool isFromOlderVersion;

		// Token: 0x0400130E RID: 4878
		private NavigationNodeFolder.OlderExchangeCalendarType olderExchangeCalendarTypeInNode;

		// Token: 0x0400130F RID: 4879
		private bool ownFolder;

		// Token: 0x04001310 RID: 4880
		private bool dataLoaded;

		// Token: 0x04001311 RID: 4881
		private string webCalendarUrl;
	}
}
