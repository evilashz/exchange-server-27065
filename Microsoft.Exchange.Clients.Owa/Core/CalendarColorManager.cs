using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020000DD RID: 221
	internal sealed class CalendarColorManager
	{
		// Token: 0x06000787 RID: 1927 RVA: 0x000395F9 File Offset: 0x000377F9
		private CalendarColorManager(UserContext userContext)
		{
			this.Load(userContext);
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x00039608 File Offset: 0x00037808
		private UserConfiguration Configuration
		{
			get
			{
				return this.config;
			}
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00039610 File Offset: 0x00037810
		public static bool IsColorIndexValid(int colorIndex)
		{
			return colorIndex >= -2 && colorIndex < 15;
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00039620 File Offset: 0x00037820
		public static int GetCalendarFolderColor(UserContext userContext, StoreObjectId calendarFolderId)
		{
			CalendarColorManager.ThrowIfCannotActAsOwner(userContext);
			NavigationNodeCollection navigationNodeCollection = NavigationNodeCollection.TryCreateNavigationNodeCollection(userContext, userContext.MailboxSession, NavigationNodeGroupSection.Calendar);
			return CalendarColorManager.GetCalendarFolderColor(userContext, calendarFolderId, navigationNodeCollection);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0003964C File Offset: 0x0003784C
		public static int GetCalendarFolderColor(UserContext userContext, StoreObjectId calendarFolderId, NavigationNodeCollection navigationNodeCollection)
		{
			CalendarColorManager.ThrowIfCannotActAsOwner(userContext);
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (calendarFolderId == null)
			{
				throw new ArgumentNullException("calendarFolderId");
			}
			int result = -2;
			if (navigationNodeCollection != null)
			{
				try
				{
					NavigationNodeFolder[] navigationNodeFolders = navigationNodeCollection.FindFoldersById(calendarFolderId);
					result = CalendarColorManager.GetCalendarFolderColor(userContext, navigationNodeCollection, navigationNodeFolders);
				}
				catch (StoragePermanentException ex)
				{
					string message = string.Format(CultureInfo.InvariantCulture, "CalendarColorManager.GetCalendarFolderColor. Unable to get calendar folder color. Exception: {0}.", new object[]
					{
						ex.Message
					});
					ExTraceGlobals.CoreCallTracer.TraceDebug(0L, message);
				}
				catch (StorageTransientException ex2)
				{
					string message2 = string.Format(CultureInfo.InvariantCulture, "CalendarColorManager.GetCalendarFolderColor. Unable to get calendar folder color. Exception: {0}.", new object[]
					{
						ex2.Message
					});
					ExTraceGlobals.CoreCallTracer.TraceDebug(0L, message2);
				}
			}
			return result;
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00039720 File Offset: 0x00037920
		public static int GetCalendarFolderColor(UserContext userContext, NavigationNodeCollection navigationNodeCollection, NavigationNodeFolder[] navigationNodeFolders)
		{
			CalendarColorManager.ThrowIfCannotActAsOwner(userContext);
			int num = -2;
			if (navigationNodeFolders != null && navigationNodeFolders.Length > 0)
			{
				num = navigationNodeFolders[0].NavigationNodeCalendarColor;
			}
			if (!CalendarColorManager.IsColorIndexValid(num))
			{
				num = -2;
				foreach (NavigationNodeFolder navigationNodeFolder in navigationNodeFolders)
				{
					navigationNodeFolder.NavigationNodeCalendarColor = num;
				}
				navigationNodeCollection.Save(userContext.MailboxSession);
			}
			return num;
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0003977C File Offset: 0x0003797C
		public static int ParseColorIndexString(string sColor, bool isClientColorIndex)
		{
			if (sColor == null)
			{
				throw new ArgumentNullException("sColor");
			}
			int num;
			if (!int.TryParse(sColor, out num))
			{
				num = -2;
			}
			else if (isClientColorIndex)
			{
				num = CalendarColorManager.GetServerColorIndex(num);
			}
			if (!CalendarColorManager.IsColorIndexValid(num))
			{
				num = -2;
			}
			return num;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x000397BC File Offset: 0x000379BC
		public static int GetClientColorIndex(int serverColorIndex)
		{
			if (serverColorIndex < 0)
			{
				return serverColorIndex;
			}
			return serverColorIndex + 1;
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x000397C7 File Offset: 0x000379C7
		public static int GetServerColorIndex(int clientColorIndex)
		{
			if (clientColorIndex <= 0)
			{
				return clientColorIndex;
			}
			return clientColorIndex - 1;
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x000397D4 File Offset: 0x000379D4
		public static void ChangeColorName(UserContext userContext, int calendarColorIndex, string calendarColorName)
		{
			CalendarColorManager.ThrowIfCannotActAsOwner(userContext);
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (-2 > calendarColorIndex || calendarColorIndex >= 15)
			{
				throw new ArgumentOutOfRangeException("calendarColorIndex");
			}
			if (string.IsNullOrEmpty(calendarColorName))
			{
				throw new ArgumentNullException("calendarColorName");
			}
			if (calendarColorName.Length > 255)
			{
				throw new ArgumentException("calendarColorName may not exceed 255 characters in length");
			}
			CalendarColorManager calendarColorManager = new CalendarColorManager(userContext);
			IDictionary dictionary = calendarColorManager.Configuration.GetDictionary();
			if (!dictionary.Contains(calendarColorIndex))
			{
				throw new OwaInvalidInputException(string.Format(CultureInfo.InvariantCulture, "Value {0} is not a valid calendar color index", new object[]
				{
					calendarColorIndex
				}));
			}
			dictionary[calendarColorIndex] = calendarColorName;
			calendarColorManager.Configuration.Save();
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00039894 File Offset: 0x00037A94
		private static Dictionary<DefaultCalendarColor, string> MapColorNames()
		{
			Dictionary<DefaultCalendarColor, string> dictionary = new Dictionary<DefaultCalendarColor, string>();
			dictionary[DefaultCalendarColor.NoneSet] = LocalizedStrings.GetNonEncoded(-789554764);
			dictionary[DefaultCalendarColor.Auto] = LocalizedStrings.GetNonEncoded(185042824);
			dictionary[DefaultCalendarColor.Brown] = LocalizedStrings.GetNonEncoded(1091142534);
			dictionary[DefaultCalendarColor.BrightGreen] = LocalizedStrings.GetNonEncoded(1509769609);
			dictionary[DefaultCalendarColor.Purple] = LocalizedStrings.GetNonEncoded(-415545442);
			dictionary[DefaultCalendarColor.TaupeDarkGrey] = LocalizedStrings.GetNonEncoded(-2088323323);
			dictionary[DefaultCalendarColor.KhakiGreen] = LocalizedStrings.GetNonEncoded(-40076381);
			dictionary[DefaultCalendarColor.CoralPink] = LocalizedStrings.GetNonEncoded(173435619);
			dictionary[DefaultCalendarColor.GrassGreen] = LocalizedStrings.GetNonEncoded(-242192433);
			dictionary[DefaultCalendarColor.PeriwinkleBlue] = LocalizedStrings.GetNonEncoded(16181640);
			dictionary[DefaultCalendarColor.TealGreen] = LocalizedStrings.GetNonEncoded(-1942954909);
			dictionary[DefaultCalendarColor.Magenta] = LocalizedStrings.GetNonEncoded(-195962327);
			dictionary[DefaultCalendarColor.DarkBlue] = LocalizedStrings.GetNonEncoded(-1300826558);
			dictionary[DefaultCalendarColor.SageGreen] = LocalizedStrings.GetNonEncoded(-2009804245);
			dictionary[DefaultCalendarColor.CamelBrown] = LocalizedStrings.GetNonEncoded(66432428);
			dictionary[DefaultCalendarColor.ElectricBlue] = LocalizedStrings.GetNonEncoded(916435529);
			dictionary[DefaultCalendarColor.Cinnamon] = LocalizedStrings.GetNonEncoded(1001882251);
			return dictionary;
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x000399D0 File Offset: 0x00037BD0
		private static bool InitializeConfiguration(IDictionary source, IDictionary target)
		{
			bool result = false;
			foreach (object obj in source)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (!target.Contains((int)dictionaryEntry.Key))
				{
					target.Add((int)dictionaryEntry.Key, (string)dictionaryEntry.Value);
					result = true;
				}
				else if (string.IsNullOrEmpty(target[(int)dictionaryEntry.Key] as string))
				{
					target[(int)dictionaryEntry.Key] = (string)source[(int)dictionaryEntry.Key];
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x00039AC0 File Offset: 0x00037CC0
		private static void ThrowIfCannotActAsOwner(UserContext userContext)
		{
			if (!userContext.CanActAsOwner)
			{
				throw new OwaAccessDeniedException(LocalizedStrings.GetNonEncoded(1622692336), true);
			}
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00039ADC File Offset: 0x00037CDC
		private void Load(UserContext userContext)
		{
			try
			{
				this.config = userContext.MailboxSession.UserConfigurationManager.GetFolderConfiguration("IPM.Configuration.CalendarColorList", UserConfigurationTypes.Dictionary, userContext.CalendarFolderId);
			}
			catch (ObjectNotFoundException)
			{
				ExTraceGlobals.CalendarCallTracer.TraceDebug((long)this.GetHashCode(), "CalendarColorManager::Load. No existing Calendar colors list configuration was found. We are creating new one.");
				if (userContext.CanActAsOwner)
				{
					this.config = userContext.MailboxSession.UserConfigurationManager.CreateFolderConfiguration("IPM.Configuration.CalendarColorList", UserConfigurationTypes.Dictionary, userContext.CalendarFolderId);
				}
			}
			if (userContext.CanActAsOwner && CalendarColorManager.InitializeConfiguration(CalendarColorManager.colorNamesMap, this.config.GetDictionary()))
			{
				this.config.Save();
			}
		}

		// Token: 0x04000548 RID: 1352
		private const string CalendarColorConfigurationName = "IPM.Configuration.CalendarColorList";

		// Token: 0x04000549 RID: 1353
		private static readonly Dictionary<DefaultCalendarColor, string> colorNamesMap = CalendarColorManager.MapColorNames();

		// Token: 0x0400054A RID: 1354
		private UserConfiguration config;
	}
}
