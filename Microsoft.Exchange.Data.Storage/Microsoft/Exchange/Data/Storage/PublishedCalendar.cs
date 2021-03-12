using System;
using System.IO;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DAD RID: 3501
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PublishedCalendar : PublishedFolder
	{
		// Token: 0x0600784A RID: 30794 RVA: 0x00212F5D File Offset: 0x0021115D
		internal PublishedCalendar(string domain, SecurityIdentifier sid, StoreObjectId folderId, ObscureKind? obscureKind, SecurityIdentifier reachUserSid) : base(domain, sid, folderId)
		{
			this.obscureKind = obscureKind;
			this.reachUserSid = reachUserSid;
		}

		// Token: 0x0600784B RID: 30795 RVA: 0x00212F78 File Offset: 0x00211178
		internal PublishedCalendar(MailboxSession mailboxSession, StoreObjectId folderId) : base(mailboxSession, folderId)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.LoadPublishedOptions();
				disposeGuard.Success();
			}
		}

		// Token: 0x0600784C RID: 30796 RVA: 0x00212FC4 File Offset: 0x002111C4
		public static bool TryGetPublishedCalendar(MailboxSession mailboxSession, StoreObjectId folderId, ObscureKind? obscureKind, out PublishedCalendar calendar)
		{
			calendar = null;
			try
			{
				calendar = new PublishedCalendar(mailboxSession, folderId);
				calendar.obscureKind = obscureKind;
			}
			catch (FolderNotPublishedException)
			{
			}
			return calendar != null;
		}

		// Token: 0x0600784D RID: 30797 RVA: 0x00213004 File Offset: 0x00211204
		public static bool IsCalendarPublished(MailboxSession mailboxSession, StoreObjectId folderId)
		{
			bool result = false;
			try
			{
				new PublishedCalendar(mailboxSession, folderId).Dispose();
				result = true;
			}
			catch (FolderNotPublishedException)
			{
			}
			return result;
		}

		// Token: 0x0600784E RID: 30798 RVA: 0x00213038 File Offset: 0x00211238
		internal bool TrySetObscureKind(ObscureKind obscureKind)
		{
			if (this.PublishedOptions.SearchableUrlEnabled)
			{
				return false;
			}
			this.obscureKind = new ObscureKind?(obscureKind);
			return true;
		}

		// Token: 0x1700202D RID: 8237
		// (get) Token: 0x0600784F RID: 30799 RVA: 0x00213056 File Offset: 0x00211256
		public MailboxCalendarFolder PublishedOptions
		{
			get
			{
				if (this.publishedOptions == null)
				{
					this.LoadPublishedOptions();
				}
				return this.publishedOptions;
			}
		}

		// Token: 0x1700202E RID: 8238
		// (get) Token: 0x06007850 RID: 30800 RVA: 0x0021306C File Offset: 0x0021126C
		public override string BrowseUrl
		{
			get
			{
				base.CheckDisposed("BrowseUrl::get");
				if (this.obscureKind == ObscureKind.Normal)
				{
					return this.PublishedOptions.PublishedCalendarUrl;
				}
				return this.PublishedOptions.PublishedCalendarUrlRaw;
			}
		}

		// Token: 0x1700202F RID: 8239
		// (get) Token: 0x06007851 RID: 30801 RVA: 0x002130B8 File Offset: 0x002112B8
		public string ICalUrl
		{
			get
			{
				base.CheckDisposed("ICalUrl::get");
				if (this.obscureKind == ObscureKind.Normal)
				{
					return this.PublishedOptions.PublishedICalUrl;
				}
				return this.PublishedOptions.PublishedICalUrlRaw;
			}
		}

		// Token: 0x17002030 RID: 8240
		// (get) Token: 0x06007852 RID: 30802 RVA: 0x00213104 File Offset: 0x00211304
		public DetailLevelEnumType DetailLevel
		{
			get
			{
				base.CheckDisposed("DetailLevel::get");
				if (this.obscureKind == ObscureKind.Restricted)
				{
					return DetailLevelEnumType.AvailabilityOnly;
				}
				return this.PublishedOptions.DetailLevel;
			}
		}

		// Token: 0x17002031 RID: 8241
		// (get) Token: 0x06007853 RID: 30803 RVA: 0x00213146 File Offset: 0x00211346
		public ExDateTime PublishedFromDateTime
		{
			get
			{
				base.CheckDisposed("PublishedFromDateTime::get");
				return this.CalculateRelativeDateTime(this.PublishedOptions.PublishDateRangeFrom, false);
			}
		}

		// Token: 0x17002032 RID: 8242
		// (get) Token: 0x06007854 RID: 30804 RVA: 0x00213168 File Offset: 0x00211368
		public ExDateTime PublishedToDateTime
		{
			get
			{
				base.CheckDisposed("PublishedToDateTime::get");
				return this.CalculateRelativeDateTime(this.PublishedOptions.PublishDateRangeTo, true).IncrementDays(1);
			}
		}

		// Token: 0x17002033 RID: 8243
		// (get) Token: 0x06007855 RID: 30805 RVA: 0x0021319B File Offset: 0x0021139B
		// (set) Token: 0x06007856 RID: 30806 RVA: 0x002131B3 File Offset: 0x002113B3
		public ExTimeZone TimeZone
		{
			get
			{
				base.CheckDisposed("TimeZone::get");
				return base.MailboxSession.ExTimeZone;
			}
			set
			{
				base.CheckDisposed("TimeZone::set");
				base.MailboxSession.ExTimeZone = value;
			}
		}

		// Token: 0x06007857 RID: 30807 RVA: 0x002131CC File Offset: 0x002113CC
		public Folder GetCalendarFolder()
		{
			return Folder.Bind(base.MailboxSession, base.FolderId);
		}

		// Token: 0x06007858 RID: 30808 RVA: 0x002131E0 File Offset: 0x002113E0
		public void SavePublishedOptions()
		{
			using (MailboxCalendarFolderDataProvider mailboxCalendarFolderDataProvider = new MailboxCalendarFolderDataProvider(base.MailboxSession.GetADSessionSettings(), DirectoryHelper.ReadADRecipient(base.MailboxSession.MailboxOwner.MailboxInfo.MailboxGuid, base.MailboxSession.MailboxOwner.MailboxInfo.IsArchive, base.MailboxSession.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid)) as ADUser, "SavePublishedOptions"))
			{
				mailboxCalendarFolderDataProvider.Save(this.PublishedOptions);
			}
			this.publishedOptions = null;
		}

		// Token: 0x06007859 RID: 30809 RVA: 0x00213274 File Offset: 0x00211474
		public object[][] GetCalendarView(ExDateTime startTime, ExDateTime endTime, params PropertyDefinition[] dataColumns)
		{
			base.CheckDisposed("GetCalendarView");
			if (startTime > endTime)
			{
				throw new ArgumentException(ServerStrings.ExInvalidDateTimeRange((DateTime)startTime, (DateTime)endTime));
			}
			ExDateTime calendarViewFromDateTime = this.GetCalendarViewFromDateTime(startTime);
			ExDateTime calendarViewToDateTime = this.GetCalendarViewToDateTime(endTime);
			if (calendarViewFromDateTime > calendarViewToDateTime)
			{
				ExTraceGlobals.SharingTracer.TraceError<string, ExDateTime, ExDateTime>((long)this.GetHashCode(), "{0}: GetCalendarView: the request window is out of range of published window. CalendarViewFromDateTime = {1}; CalendarViewToDateTime = {2}.", base.OwnerDisplayName, calendarViewFromDateTime, calendarViewToDateTime);
				return Array<object[]>.Empty;
			}
			ExTraceGlobals.SharingTracer.TraceDebug((long)this.GetHashCode(), "{0}: GetCalendarView: FolderId = {1}; DetailLevel = {2}; CalendarViewFromDateTime = {3}; CalendarViewToDateTime = {4}.", new object[]
			{
				base.OwnerDisplayName,
				base.FolderId,
				this.DetailLevel,
				calendarViewFromDateTime,
				calendarViewToDateTime
			});
			object[][] result;
			using (CalendarFolder calendarFolder = CalendarFolder.Bind(base.MailboxSession, base.FolderId))
			{
				result = calendarFolder.InternalGetCalendarView(calendarViewFromDateTime, calendarViewToDateTime, this.DetailLevel == DetailLevelEnumType.AvailabilityOnly, true, true, RecurrenceExpansionOption.IncludeRegularOccurrences, dataColumns);
			}
			return result;
		}

		// Token: 0x0600785A RID: 30810 RVA: 0x00213394 File Offset: 0x00211594
		public CalendarItemBase GetItem(StoreObjectId itemId, params PropertyDefinition[] dataColumns)
		{
			base.CheckDisposed("GetItem");
			Util.ThrowOnNullArgument(itemId, "itemId");
			if (this.DetailLevel == DetailLevelEnumType.AvailabilityOnly)
			{
				throw new InvalidOperationException();
			}
			CalendarItemBase calendarItemBase = CalendarItemBase.Bind(base.MailboxSession, itemId, dataColumns);
			using (DisposeGuard disposeGuard = calendarItemBase.Guard())
			{
				if (!calendarItemBase.ParentId.Equals(base.FolderId))
				{
					throw new ObjectNotFoundException(ServerStrings.ExItemNotFound);
				}
				disposeGuard.Success();
			}
			return calendarItemBase;
		}

		// Token: 0x0600785B RID: 30811 RVA: 0x00213424 File Offset: 0x00211624
		public PublishedCalendarItemData GetItemData(StoreObjectId itemId)
		{
			base.CheckDisposed("GetItemData");
			PublishedCalendarItemData result;
			using (CalendarItemBase item = this.GetItem(itemId, null))
			{
				string bodyText = string.Empty;
				if (this.DetailLevel == DetailLevelEnumType.FullDetails)
				{
					using (TextReader textReader = item.Body.OpenTextReader(BodyFormat.TextPlain))
					{
						bodyText = textReader.ReadToEnd();
					}
				}
				result = new PublishedCalendarItemData
				{
					Subject = item.Subject,
					Location = item.Location,
					When = item.GenerateWhen(),
					BodyText = bodyText
				};
			}
			return result;
		}

		// Token: 0x0600785C RID: 30812 RVA: 0x002134D8 File Offset: 0x002116D8
		public void WriteInternetCalendar(Stream output, string charset)
		{
			base.CheckDisposed("WriteInternetCalendar");
			Util.ThrowOnNullArgument(output, "output");
			Util.ThrowOnNullOrEmptyArgument(charset, "charset");
			ExTraceGlobals.SharingTracer.TraceDebug((long)this.GetHashCode(), "{0}: WriteInternetCalendar: FolderId = {1}; DetailLevel = {2}; PublishedFromDateTime = {3}; PublishedToDateTime = {4}.", new object[]
			{
				base.OwnerDisplayName,
				base.FolderId,
				this.DetailLevel,
				this.PublishedFromDateTime,
				this.PublishedToDateTime
			});
			ICalSharingHelper.ExportCalendar(output, charset, new OutboundConversionOptions(base.MailboxSession.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid), base.MailboxSession.ServerFullyQualifiedDomainName), base.MailboxSession, base.FolderId, this.PublishedFromDateTime, this.PublishedToDateTime, this.DetailLevel);
		}

		// Token: 0x0600785D RID: 30813 RVA: 0x002135A4 File Offset: 0x002117A4
		private ExDateTime CalculateRelativeDateTime(DateRangeEnumType dateRange, bool forward)
		{
			int num = forward ? 1 : -1;
			switch (dateRange)
			{
			case DateRangeEnumType.OneDay:
				return ExDateTime.GetNow(this.TimeZone).Date.IncrementDays(num);
			case DateRangeEnumType.ThreeDays:
				return ExDateTime.GetNow(this.TimeZone).Date.IncrementDays(3 * num);
			case DateRangeEnumType.OneWeek:
				return ExDateTime.GetNow(this.TimeZone).Date.IncrementDays(7 * num);
			case DateRangeEnumType.OneMonth:
				return ExDateTime.GetNow(this.TimeZone).Date.IncrementMonths(num);
			case DateRangeEnumType.ThreeMonths:
				return ExDateTime.GetNow(this.TimeZone).Date.IncrementMonths(3 * num);
			case DateRangeEnumType.SixMonths:
				return ExDateTime.GetNow(this.TimeZone).Date.IncrementMonths(6 * num);
			case DateRangeEnumType.OneYear:
				return ExDateTime.GetNow(this.TimeZone).Date.IncrementYears(num);
			default:
				throw new ArgumentOutOfRangeException("dateRange");
			}
		}

		// Token: 0x0600785E RID: 30814 RVA: 0x002136CB File Offset: 0x002118CB
		private ExDateTime GetCalendarViewFromDateTime(ExDateTime startTime)
		{
			if (startTime < this.PublishedFromDateTime)
			{
				ExTraceGlobals.SharingTracer.TraceDebug<string, ExDateTime, ExDateTime>((long)this.GetHashCode(), "{0}: The requested start time {1} has been moved to published-from time {2}.", base.OwnerDisplayName, startTime, this.PublishedFromDateTime);
				return this.PublishedFromDateTime;
			}
			return startTime;
		}

		// Token: 0x0600785F RID: 30815 RVA: 0x00213706 File Offset: 0x00211906
		private ExDateTime GetCalendarViewToDateTime(ExDateTime endTime)
		{
			if (endTime > this.PublishedToDateTime)
			{
				ExTraceGlobals.SharingTracer.TraceDebug<string, ExDateTime, ExDateTime>((long)this.GetHashCode(), "{0}: The requested end time {1} has been moved to published-to time {2}.", base.OwnerDisplayName, endTime, this.PublishedToDateTime);
				return this.PublishedToDateTime;
			}
			return endTime;
		}

		// Token: 0x06007860 RID: 30816 RVA: 0x00213741 File Offset: 0x00211941
		private void LoadPublishedOptions()
		{
			if (this.reachUserSid == null)
			{
				this.LoadPublishedCalendarOptions();
				return;
			}
			this.LoadPublishedCalendarOptionsForReachUser();
		}

		// Token: 0x06007861 RID: 30817 RVA: 0x00213760 File Offset: 0x00211960
		private void LoadPublishedCalendarOptionsForReachUser()
		{
			this.publishedOptions = new MailboxCalendarFolder();
			PermissionSecurityPrincipal targetPrincipal = null;
			using (ExternalUserCollection externalUsers = base.MailboxSession.GetExternalUsers())
			{
				ExternalUser externalUser = externalUsers.FindExternalUser(this.reachUserSid);
				if (externalUser == null || !externalUser.IsReachUser)
				{
					ExTraceGlobals.SharingTracer.TraceDebug<SecurityIdentifier, bool, bool>((long)this.GetHashCode(), "ReachAccessSid={0}, Found ExternalUser:{1}, IsReachUser:{2}", this.reachUserSid, externalUser != null, externalUser != null && externalUser.IsReachUser);
					throw new PublishedFolderAccessDeniedException();
				}
				targetPrincipal = new PermissionSecurityPrincipal(externalUser);
			}
			DetailLevelEnumType? freeBusyAccessLevel = this.GetFreeBusyAccessLevel(targetPrincipal);
			if (freeBusyAccessLevel == null)
			{
				throw new PublishedFolderAccessDeniedException();
			}
			this.publishedOptions.DetailLevel = freeBusyAccessLevel.Value;
			this.publishedOptions.PublishEnabled = true;
			this.publishedOptions.PublishDateRangeFrom = DateRangeEnumType.OneMonth;
			this.publishedOptions.PublishDateRangeTo = DateRangeEnumType.SixMonths;
		}

		// Token: 0x06007862 RID: 30818 RVA: 0x00213848 File Offset: 0x00211A48
		private DetailLevelEnumType? GetFreeBusyAccessLevel(PermissionSecurityPrincipal targetPrincipal)
		{
			DetailLevelEnumType? result = null;
			CalendarFolderPermission calendarFolderPermission = null;
			using (CalendarFolder calendarFolder = (CalendarFolder)this.GetCalendarFolder())
			{
				CalendarFolderPermissionSet permissionSet = calendarFolder.GetPermissionSet();
				calendarFolderPermission = permissionSet.GetEntry(targetPrincipal);
				if (calendarFolderPermission != null)
				{
					if ((calendarFolderPermission.MemberRights & MemberRights.ReadAny) == MemberRights.ReadAny)
					{
						result = new DetailLevelEnumType?(DetailLevelEnumType.FullDetails);
					}
					else if (calendarFolderPermission.FreeBusyAccess == FreeBusyAccess.Details)
					{
						result = new DetailLevelEnumType?(DetailLevelEnumType.LimitedDetails);
					}
					else if (calendarFolderPermission.FreeBusyAccess == FreeBusyAccess.Basic)
					{
						result = new DetailLevelEnumType?(DetailLevelEnumType.AvailabilityOnly);
					}
				}
			}
			if (result == null)
			{
				ExTraceGlobals.SharingTracer.TraceDebug<SmtpAddress, string>((long)this.GetHashCode(), "ReachUser ={0}, Not authroized to view published calendar.  Permissions:{1}", targetPrincipal.ExternalUser.OriginalSmtpAddress, (calendarFolderPermission == null) ? "No permission" : calendarFolderPermission.MemberRights.ToString());
			}
			return result;
		}

		// Token: 0x06007863 RID: 30819 RVA: 0x00213928 File Offset: 0x00211B28
		private void LoadPublishedCalendarOptions()
		{
			this.publishedOptions = (MailboxCalendarFolder)UserConfigurationDictionaryHelper.Fill(new MailboxCalendarFolder(), MailboxCalendarFolder.CalendarFolderConfigurationProperties, (bool createIfNonexisting) => UserConfigurationHelper.GetPublishingConfiguration(base.MailboxSession, base.FolderId, createIfNonexisting));
			if (this.publishedOptions == null)
			{
				ExTraceGlobals.SharingTracer.TraceError<string, StoreObjectId>((long)this.GetHashCode(), "{0}: Cannot find published options on folder {1}.", base.OwnerDisplayName, base.FolderId);
				throw new FolderNotPublishedException();
			}
			if (!this.publishedOptions.PublishEnabled)
			{
				ExTraceGlobals.SharingTracer.TraceError<string, StoreObjectId>((long)this.GetHashCode(), "{0}: Published options indicates disabled on folder {1}.", base.OwnerDisplayName, base.FolderId);
				throw new FolderNotPublishedException();
			}
		}

		// Token: 0x04005340 RID: 21312
		private MailboxCalendarFolder publishedOptions;

		// Token: 0x04005341 RID: 21313
		private ObscureKind? obscureKind;

		// Token: 0x04005342 RID: 21314
		private readonly SecurityIdentifier reachUserSid;
	}
}
