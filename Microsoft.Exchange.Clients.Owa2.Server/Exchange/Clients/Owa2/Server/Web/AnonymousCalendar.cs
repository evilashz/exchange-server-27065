using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Security.AntiXss;
using Microsoft.Exchange.Clients.Owa2.Server.Core;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x0200046D RID: 1133
	public class AnonymousCalendar : DefaultPageBase
	{
		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x0600260E RID: 9742 RVA: 0x0008976F File Offset: 0x0008796F
		public ResourceBase[] Resources
		{
			get
			{
				return this.UserDataEmbeddedLinks;
			}
		}

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x0600260F RID: 9743 RVA: 0x00089777 File Offset: 0x00087977
		public override string TenantId
		{
			get
			{
				return "AnonymousCalendar";
			}
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06002610 RID: 9744 RVA: 0x0008977E File Offset: 0x0008797E
		public override string MdbGuid
		{
			get
			{
				return "AnonymousCalendar";
			}
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06002611 RID: 9745 RVA: 0x00089785 File Offset: 0x00087985
		public override string SlabsManifest
		{
			get
			{
				return SlabManifestCollectionFactory.GetInstance(this.VersionString).GetSlabsJson(SlabManifestType.Anonymous, new string[0], this.UserAgent.Layout);
			}
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06002612 RID: 9746 RVA: 0x000897AD File Offset: 0x000879AD
		public string CurrentUICultureName
		{
			get
			{
				return Thread.CurrentThread.CurrentUICulture.Name;
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06002613 RID: 9747 RVA: 0x000897CC File Offset: 0x000879CC
		public string CurrentResourceLocalizedCultureName
		{
			get
			{
				ResourceBase[] nonThemedUserDataEmbededLinks = UserResourcesFinder.GetNonThemedUserDataEmbededLinks(base.GetBootSlab(), this.VersionString);
				return ((LocalizedStringsScriptResource)nonThemedUserDataEmbededLinks.First((ResourceBase t) => t is LocalizedStringsScriptResource)).GetLocalizedCultureName();
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06002614 RID: 9748 RVA: 0x00089818 File Offset: 0x00087A18
		public string DefaultStylesFolder
		{
			get
			{
				return string.Format(base.BootStylesFolderFormat, base.Theme).Replace("#LCL", DefaultPageBase.ThemeStyleCultureDirectory);
			}
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06002615 RID: 9749 RVA: 0x0008983A File Offset: 0x00087A3A
		public string OwaTitle
		{
			get
			{
				return AntiXssEncoder.HtmlEncode(Strings.AnonymousCalendarTitle(AnonymousUserContext.Current.ExchangePrincipal.MailboxInfo.DisplayName), false);
			}
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06002616 RID: 9750 RVA: 0x0008985C File Offset: 0x00087A5C
		public override string ServerSettings
		{
			get
			{
				return base.ServerSettings + ",'bootType': 'AnonymousCalendar','disableCalendarDetails': '" + (AnonymousUserContext.Current.SharingDetail == DetailLevelEnumType.AvailabilityOnly).ToString().ToLower() + "'";
			}
		}

		// Token: 0x06002617 RID: 9751 RVA: 0x00089898 File Offset: 0x00087A98
		public override string FormatURIForCDN(string relativeUri, bool isBootResourceUri)
		{
			return relativeUri;
		}

		// Token: 0x06002618 RID: 9752 RVA: 0x0008989B File Offset: 0x00087A9B
		public override string GetCdnEndpointForResources(bool bootResources)
		{
			return string.Empty;
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06002619 RID: 9753 RVA: 0x000898A2 File Offset: 0x00087AA2
		public override SlabManifestType ManifestType
		{
			get
			{
				return SlabManifestType.Anonymous;
			}
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x0600261A RID: 9754 RVA: 0x000898AC File Offset: 0x00087AAC
		// (set) Token: 0x0600261B RID: 9755 RVA: 0x000898FA File Offset: 0x00087AFA
		public override string VersionString
		{
			get
			{
				if (this.buildVersion == null)
				{
					string value = OwaVersionId.Current;
					if (string.IsNullOrEmpty(value))
					{
						ExTraceGlobals.AppcacheManifestHandlerTracer.TraceError(0L, "DefaultPageHandler.VersionString: Could not retrieve OwaVersion from registry.");
						throw new ArgumentException("Could not retrieve OwaVersion from registry.");
					}
					this.buildVersion = value;
				}
				return this.buildVersion;
			}
			set
			{
				this.buildVersion = value;
			}
		}

		// Token: 0x0600261C RID: 9756 RVA: 0x00089904 File Offset: 0x00087B04
		public string GetCalendarFolder()
		{
			CalendarGroup[] array = new CalendarGroup[]
			{
				new CalendarGroup()
			};
			array[0].GroupId = Guid.Empty.ToString();
			array[0].GroupType = CalendarGroupType.MyCalendars;
			array[0].GroupName = string.Empty;
			array[0].ItemId = new Microsoft.Exchange.Services.Core.Types.ItemId(array[0].GroupId, array[0].GroupId);
			array[0].Calendars = new CalendarEntry[1];
			LocalCalendarEntry localCalendarEntry = new LocalCalendarEntry();
			localCalendarEntry.CalendarColor = CalendarColor.Auto;
			localCalendarEntry.IsDefaultCalendar = true;
			array[0].Calendars[0] = localCalendarEntry;
			CalendarFolderType calendarFolderType = new CalendarFolderType();
			calendarFolderType.FolderId = IdConverter.GetFolderIdFromStoreId(AnonymousUserContext.Current.PublishedCalendarId, new MailboxId(AnonymousUserContext.Current.ExchangePrincipal.MailboxInfo.MailboxGuid));
			localCalendarEntry.ItemId = new Microsoft.Exchange.Services.Core.Types.ItemId("calendarEntryFor" + calendarFolderType.FolderId.Id, "calendarEntryChangeKeyFor" + calendarFolderType.FolderId.ChangeKey);
			calendarFolderType.DisplayName = AntiXssEncoder.HtmlEncode(AnonymousUserContext.Current.PublishedCalendarName, false);
			calendarFolderType.ChildFolderCount = new int?(0);
			calendarFolderType.ChildFolderCountSpecified = true;
			calendarFolderType.ExtendedProperty = null;
			calendarFolderType.FolderClass = "IPF.Appointment";
			calendarFolderType.EffectiveRights = new EffectiveRightsType();
			calendarFolderType.EffectiveRights.CreateAssociated = false;
			calendarFolderType.EffectiveRights.CreateContents = false;
			calendarFolderType.EffectiveRights.CreateHierarchy = false;
			calendarFolderType.EffectiveRights.Delete = false;
			calendarFolderType.EffectiveRights.Modify = false;
			calendarFolderType.EffectiveRights.Read = true;
			calendarFolderType.EffectiveRights.ViewPrivateItemsSpecified = false;
			localCalendarEntry.CalendarFolderId = calendarFolderType.FolderId;
			GetCalendarFoldersResponse instance = new GetCalendarFoldersResponse(array, new CalendarFolderType[]
			{
				calendarFolderType
			});
			return SessionDataHandler.EmitPayload("calendarFolders", JsonConverter.ToJSON(instance));
		}

		// Token: 0x0600261D RID: 9757 RVA: 0x00089AD4 File Offset: 0x00087CD4
		public string GetUserConfiguration()
		{
			OwaUserConfiguration owaUserConfiguration = new OwaUserConfiguration();
			owaUserConfiguration.UserOptions = new UserOptionsType();
			owaUserConfiguration.UserOptions.TimeZone = AnonymousUserContext.Current.TimeZone.AlternativeId;
			owaUserConfiguration.UserOptions.TimeFormat = MailboxRegionalConfiguration.GetDefaultTimeFormat(CultureInfo.CurrentUICulture);
			owaUserConfiguration.UserOptions.DateFormat = MailboxRegionalConfiguration.GetDefaultDateFormat(CultureInfo.CurrentUICulture);
			owaUserConfiguration.UserOptions.WorkingHours = new WorkingHoursType(0, 1440, 127, AnonymousUserContext.Current.TimeZone, AnonymousUserContext.Current.TimeZone);
			owaUserConfiguration.ApplicationSettings = new ApplicationSettingsType();
			owaUserConfiguration.ApplicationSettings.AnalyticsEnabled = false;
			owaUserConfiguration.ApplicationSettings.InferenceEnabled = false;
			owaUserConfiguration.ApplicationSettings.WatsonEnabled = false;
			owaUserConfiguration.ApplicationSettings.DefaultTraceLevel = TraceLevel.Off;
			owaUserConfiguration.ApplicationSettings.InstrumentationSendIntervalSeconds = 0;
			owaUserConfiguration.SessionSettings = new SessionSettingsType();
			owaUserConfiguration.SessionSettings.ArchiveDisplayName = string.Empty;
			owaUserConfiguration.SessionSettings.CanActAsOwner = false;
			owaUserConfiguration.SessionSettings.IsExplicitLogon = false;
			owaUserConfiguration.SessionSettings.IsExplicitLogonOthersMailbox = false;
			owaUserConfiguration.SessionSettings.UserCulture = AnonymousUserContext.Current.Culture.Name;
			owaUserConfiguration.SessionSettings.UserDisplayName = AntiXssEncoder.HtmlEncode(AnonymousUserContext.Current.ExchangePrincipal.MailboxInfo.DisplayName, false);
			owaUserConfiguration.SessionSettings.MaxMessageSizeInKb = 0;
			owaUserConfiguration.SessionSettings.DefaultFolderIds = new FolderId[1];
			owaUserConfiguration.SessionSettings.DefaultFolderIds[0] = IdConverter.GetFolderIdFromStoreId(AnonymousUserContext.Current.PublishedCalendarId, new MailboxId(AnonymousUserContext.Current.ExchangePrincipal.MailboxInfo.MailboxGuid));
			owaUserConfiguration.SessionSettings.DefaultFolderNames = new string[]
			{
				DefaultFolderType.Calendar.ToString()
			};
			owaUserConfiguration.PolicySettings = new PolicySettingsType();
			owaUserConfiguration.PolicySettings.PlacesEnabled = false;
			owaUserConfiguration.PolicySettings.WeatherEnabled = false;
			owaUserConfiguration.ViewStateConfiguration = new OwaViewStateConfiguration();
			owaUserConfiguration.ViewStateConfiguration.CalendarViewTypeDesktop = 4;
			owaUserConfiguration.ViewStateConfiguration.CalendarViewTypeNarrow = 1;
			owaUserConfiguration.ViewStateConfiguration.CalendarViewTypeWide = 4;
			owaUserConfiguration.SegmentationSettings = new SegmentationSettingsType(0UL);
			owaUserConfiguration.SegmentationSettings.Calendar = true;
			return SessionDataHandler.EmitPayload("owaUserConfig", JsonConverter.ToJSON(owaUserConfiguration));
		}

		// Token: 0x0600261E RID: 9758 RVA: 0x00089D15 File Offset: 0x00087F15
		protected override string GetFeaturesSupportedJsonArray(FlightedFeatureScope scope)
		{
			return "[]";
		}

		// Token: 0x0600261F RID: 9759 RVA: 0x00089D1C File Offset: 0x00087F1C
		protected override bool ShouldSkipThemeFolder()
		{
			return ThemeManagerFactory.GetInstance(this.VersionString).ShouldSkipThemeFolder;
		}

		// Token: 0x06002620 RID: 9760 RVA: 0x00089D2E File Offset: 0x00087F2E
		protected override void OnPreInit(EventArgs e)
		{
			if (!Globals.IsAnonymousCalendarApp)
			{
				HttpUtilities.EndResponse(this.Context, HttpStatusCode.BadRequest);
			}
			base.OnPreInit(e);
		}

		// Token: 0x06002621 RID: 9761 RVA: 0x00089D4E File Offset: 0x00087F4E
		protected override bool GetIsClientAppCacheEnabled(HttpContext context)
		{
			return false;
		}

		// Token: 0x04001659 RID: 5721
		protected const string CalendarFolderPayLoadName = "calendarFolders";

		// Token: 0x0400165A RID: 5722
		private const int AnonymousDefaultWorkDayStartTime = 0;

		// Token: 0x0400165B RID: 5723
		private const int AnonymousDefaultWorkDayEndTime = 1440;

		// Token: 0x0400165C RID: 5724
		private const int AnonymousDefaultWorkDays = 127;

		// Token: 0x0400165D RID: 5725
		private string buildVersion;
	}
}
