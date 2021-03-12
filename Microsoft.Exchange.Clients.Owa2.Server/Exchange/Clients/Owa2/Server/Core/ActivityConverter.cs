using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Owa2.Server.Web;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActivityLog;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200008E RID: 142
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ActivityConverter
	{
		// Token: 0x06000539 RID: 1337 RVA: 0x0000F2B4 File Offset: 0x0000D4B4
		public ActivityConverter(UserContext userContext, IMailboxSession mailboxSession, string ipAddress, string userAgent, string clientVersion)
		{
			ArgumentValidator.ThrowIfNull("userContext", userContext);
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			ArgumentValidator.ThrowIfNullOrEmpty("clientVersion", clientVersion);
			this.userContext = userContext;
			this.mailboxSession = mailboxSession;
			this.ipAddress = ipAddress;
			this.userAgent = userAgent;
			this.clientVersion = clientVersion;
			if (this.userContext.Key != null && UserContextUtilities.IsValidGuid(this.userContext.Key.UserContextId))
			{
				this.clientSessionId = new Guid(this.userContext.Key.UserContextId);
				return;
			}
			this.clientSessionId = Guid.NewGuid();
			ActivityConverter.tracer.TraceError<string>(0L, "[ActivityConverter.ctor] UserContext.Key.UserContextId is not valid: {0}.", (this.userContext.Key != null) ? this.userContext.Key.UserContextId : "UserContext.Key is null");
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x0000F38E File Offset: 0x0000D58E
		internal static Dictionary<string, string> ReportingLabelsForKeys
		{
			get
			{
				return ActivityConverter.reportingLabelsForKeys;
			}
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0000F398 File Offset: 0x0000D598
		public IList<Activity> GetActivities(ICollection<ClientLogEvent> events)
		{
			List<Activity> list = new List<Activity>(events.Count);
			foreach (ClientLogEvent clientLogEvent in events)
			{
				ActivityConverter.ConversionState conversionState = new ActivityConverter.ConversionState();
				ActivityId activityId;
				if (!ActivityConverter.TryGetActivityId(clientLogEvent.EventId, out activityId))
				{
					conversionState.Errors |= ActivityConverter.ConversionErrors.BadActivityId;
					ActivityConverter.tracer.TraceError<string>(0L, "[ActivityConverter.GetActivities] ActivityId cannot be resolved from client datapoint EventId: '{0}'.", clientLogEvent.EventId);
				}
				ExDateTime minValue;
				if (!ExDateTime.TryParse(ExTimeZone.UtcTimeZone, clientLogEvent.Time, out minValue))
				{
					conversionState.Errors |= ActivityConverter.ConversionErrors.BadTimestamp;
					minValue = ExDateTime.MinValue;
					ActivityConverter.tracer.TraceError<string>(0L, "[ActivityConverter.GetActivities] timestamp cannot be parsed: '{0}'", clientLogEvent.Time);
				}
				ActivityConverter.PropertySet requiredProperties;
				if (!ActivityConverter.propertiesForActivities.TryGetValue(activityId, out requiredProperties))
				{
					ActivityConverter.tracer.TraceDebug<string>(0L, "[ActivityConverter.GetActivities] activity id {0} does not have any required properties mapped. This may be expected.", activityId.ToString());
				}
				if (ActivityConverter.IsItemlessActivity(activityId))
				{
					this.AddItemlessActivity(list, clientLogEvent, activityId, minValue, requiredProperties, conversionState);
				}
				else if (ActivityConverter.IsMultipleItemActivity(activityId))
				{
					this.AddMultipleItemActivities(list, clientLogEvent, activityId, minValue, requiredProperties, conversionState);
				}
				else
				{
					this.AddSingleItemActivity(list, clientLogEvent, activityId, minValue, requiredProperties, conversionState);
				}
			}
			if (list.Count < events.Count)
			{
				string text = string.Format("[ActivityConverter.GetActivities] activity count {0} is less than datapoint count {1}. This indicates possible data loss due to conversion failures.", list.Count, events.Count);
				ActivityConverter.tracer.TraceError(0L, text);
				list.Add(this.CreateErrorActivity(text));
			}
			return list;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0000F520 File Offset: 0x0000D720
		private static bool IsItemlessActivity(ActivityId activityId)
		{
			return ActivityConverter.itemlessActivities.Contains(activityId);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0000F52D File Offset: 0x0000D72D
		private static bool IsMultipleItemActivity(ActivityId activityId)
		{
			return ActivityConverter.multipleItemActivities.Contains(activityId);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0000F53C File Offset: 0x0000D73C
		private void AddSingleItemActivity(IList<Activity> results, ClientLogEvent logEvent, ActivityId activityId, ExDateTime time, ActivityConverter.PropertySet requiredProperties, ActivityConverter.ConversionState conversionState)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			StoreObjectId itemId = null;
			if (requiredProperties != ActivityConverter.PropertySet.GroupMailbox)
			{
				itemId = ActivityConverter.GetItemId(logEvent, dictionary, conversionState);
			}
			ActivityConverter.GetRequiredItemActivityProperties(logEvent, requiredProperties, dictionary, conversionState);
			Activity item = new Activity(activityId, ClientId.Web, time, this.clientSessionId, this.clientVersion, this.GetNextActivitySequenceNumber(), this.mailboxSession, itemId, null, dictionary);
			results.Add(item);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0000F5A0 File Offset: 0x0000D7A0
		private static bool TryGetActivityId(string name, out ActivityId activityId)
		{
			if (string.IsNullOrEmpty(name))
			{
				ActivityConverter.tracer.TraceError(0L, "[ActivityConverter.TryGetActivityId] Activity name was null or empty.");
				activityId = ActivityId.Error;
				return false;
			}
			if (ActivityConverter.TryGetActivityIdFromSpecialCase(name, out activityId))
			{
				return true;
			}
			if (ActivityConverter.TryGetActivityIdFromEnum(name, out activityId))
			{
				return true;
			}
			ActivityConverter.tracer.TraceError<string>(0L, "[ActivityConverter.TryGetActivityId] Activity name not recognized: {0}.", name);
			activityId = ActivityId.Error;
			return false;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0000F5F9 File Offset: 0x0000D7F9
		private static bool TryGetActivityIdFromSpecialCase(string name, out ActivityId activityId)
		{
			if (string.Compare(name, "SessionInfo") == 0)
			{
				activityId = ActivityId.Logon;
				return true;
			}
			activityId = ActivityId.Min;
			return false;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0000F614 File Offset: 0x0000D814
		private static bool TryGetActivityIdFromEnum(string name, out ActivityId activityId)
		{
			int num = name.LastIndexOf('.');
			if (num == -1 || num + 1 >= name.Length)
			{
				ActivityConverter.tracer.TraceError<string>(0L, "[ActivityConverter.TryGetItemActivityIdFromEnum] Error converting {0} to ActivityId", name);
				activityId = ActivityId.Min;
				return false;
			}
			string value = name.Substring(num + 1);
			if (!Enum.TryParse<ActivityId>(value, out activityId))
			{
				ActivityConverter.tracer.TraceError<string>(0L, "[ActivityConverter.TryGetItemActivityIdFromEnum] Error converting {0} to ActivityId", name);
				activityId = ActivityId.Min;
				return false;
			}
			return true;
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0000F67C File Offset: 0x0000D87C
		private static bool TryGetStoreId(string idString, BasicTypes basicType, out StoreObjectId storeId)
		{
			if (string.IsNullOrEmpty(idString))
			{
				ActivityConverter.tracer.TraceError<string>(0L, "[ActivityConverter.TryGetStoreId] Invalid id \"{0}.\"", (idString == null) ? "(null)" : "(empty)");
				storeId = null;
				return false;
			}
			try
			{
				ItemId itemId = new ItemId(idString, null);
				storeId = (StoreObjectId)IdConverter.ConvertItemIdToStoreId(itemId, basicType);
				return true;
			}
			catch (LocalizedException arg)
			{
				ActivityConverter.tracer.TraceError<LocalizedException, string>(0L, "[ActivityConverter.TryGetStoreId] Exception {0} occurred converting {1} to StoreId", arg, idString);
			}
			storeId = null;
			return false;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0000F6FC File Offset: 0x0000D8FC
		private static bool TryGetStoreConversationId(string idString, out ConversationId conversationId)
		{
			if (string.IsNullOrEmpty(idString))
			{
				ActivityConverter.tracer.TraceError<string>(0L, "[ActivityConverter.TryGetStoreConversationId] Invalid id \"{0}.\"", (idString == null) ? "(null)" : "(empty)");
				conversationId = null;
				return false;
			}
			try
			{
				conversationId = IdConverter.EwsIdToConversationId(idString);
				return true;
			}
			catch (LocalizedException arg)
			{
				ActivityConverter.tracer.TraceError<LocalizedException, string>(0L, "[ActivityConverter.TryGetStoreConversationId] Exception {0} occurred converting {1} to ConversationId", arg, idString);
			}
			conversationId = null;
			return false;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0000F770 File Offset: 0x0000D970
		private static StoreObjectId GetItemId(ClientLogEvent logEvent, Dictionary<string, string> activityCustomProperties, ActivityConverter.ConversionState conversionState)
		{
			StoreObjectId result = null;
			string text;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "id", true, conversionState, out text) && !ActivityConverter.TryGetStoreId(text, BasicTypes.Item, out result))
			{
				activityCustomProperties.Add("BadItemIds", text);
				conversionState.Errors |= ActivityConverter.ConversionErrors.BadItemIds;
			}
			return result;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0000F7B8 File Offset: 0x0000D9B8
		private static StoreObjectId[] GetItemIds(ClientLogEvent logEvent, Dictionary<string, string> activityCustomProperties, ActivityConverter.ConversionState conversionState)
		{
			StoreObjectId[] array = new StoreObjectId[1];
			StoreObjectId[] array2 = array;
			string text;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "ids", true, conversionState, out text))
			{
				string[] array3 = text.Split(new char[]
				{
					','
				});
				array2 = new StoreObjectId[array3.Length];
				for (int i = 0; i < array3.Length; i++)
				{
					if (!ActivityConverter.TryGetStoreId(array3[i], BasicTypes.Item, out array2[i]))
					{
						activityCustomProperties.Add("BadItemIds", array3[i]);
						conversionState.Errors |= ActivityConverter.ConversionErrors.BadItemIds;
						break;
					}
				}
			}
			return array2;
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0000F840 File Offset: 0x0000DA40
		private static bool TryGetClientLogEventProperty(ClientLogEvent clientLogEvent, string key, bool isRequiredProperty, ActivityConverter.ConversionState conversionState, out string value)
		{
			value = null;
			if (!clientLogEvent.TryGetValue<string>(key, out value))
			{
				ActivityConverter.tracer.TraceDebug<string>(0L, "[ActivityConverter.TryGetClientLogEventProperty] {0} is missing.", key);
			}
			if (string.IsNullOrEmpty(value))
			{
				if (isRequiredProperty)
				{
					ActivityConverter.tracer.TraceError<string>(0L, "[ActivityConverter.TryGetClientLogEventProperty] {0} property is null or empty.", key);
					conversionState.AddMissingProperty(key);
				}
				return false;
			}
			return true;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0000F898 File Offset: 0x0000DA98
		private void AddMultipleItemActivities(IList<Activity> results, ClientLogEvent logEvent, ActivityId activityId, ExDateTime time, ActivityConverter.PropertySet requiredProperties, ActivityConverter.ConversionState conversionState)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			StoreObjectId[] itemIds = ActivityConverter.GetItemIds(logEvent, dictionary, conversionState);
			ActivityConverter.GetRequiredItemActivityProperties(logEvent, requiredProperties, dictionary, conversionState);
			foreach (StoreObjectId itemId in itemIds)
			{
				Dictionary<string, string> customProperties = new Dictionary<string, string>(dictionary);
				Activity item = new Activity(activityId, ClientId.Web, time, this.clientSessionId, this.clientVersion, this.GetNextActivitySequenceNumber(), this.mailboxSession, itemId, null, customProperties);
				results.Add(item);
			}
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0000F918 File Offset: 0x0000DB18
		private void AddItemlessActivity(IList<Activity> results, ClientLogEvent logEvent, ActivityId activityId, ExDateTime time, ActivityConverter.PropertySet requiredProperties, ActivityConverter.ConversionState conversionState)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			this.GetRequiredItemlessActivityProperties(logEvent, requiredProperties, dictionary, conversionState);
			Activity item = new Activity(activityId, ClientId.Web, time, this.clientSessionId, this.clientVersion, this.GetNextActivitySequenceNumber(), this.mailboxSession, null, null, dictionary);
			results.Add(item);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0000F968 File Offset: 0x0000DB68
		private static void GetDeleteProperties(ClientLogEvent logEvent, Dictionary<string, string> activityCustomProperties, ActivityConverter.ConversionState conversionState)
		{
			string value;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "dt", true, conversionState, out value))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["dt"], value);
			}
			int num = -1;
			if (ActivityConverter.TryGetIntegerProperty(logEvent, "dm", false, conversionState, out num))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["dm"], num.ToString());
			}
			int num2 = -1;
			if (ActivityConverter.TryGetIntegerProperty(logEvent, "cda", false, conversionState, out num2))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["cda"], num2.ToString());
			}
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0000F9FC File Offset: 0x0000DBFC
		private static void GetConversationProperties(ClientLogEvent logEvent, Dictionary<string, string> activityCustomProperties, ActivityConverter.ConversionState conversionState)
		{
			int num = -1;
			if (ActivityConverter.TryGetIntegerProperty(logEvent, "co", true, conversionState, out num))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["co"], num.ToString());
				if (num == 1)
				{
					ActivityConverter.GetConversationId(logEvent, true, activityCustomProperties, conversionState);
				}
			}
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0000FA48 File Offset: 0x0000DC48
		private static void GetConversationId(ClientLogEvent logEvent, bool isRequiredProperty, Dictionary<string, string> activityCustomProperties, ActivityConverter.ConversionState conversionState)
		{
			string text;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "cid", isRequiredProperty, conversionState, out text))
			{
				ConversationId conversationId = null;
				if (ActivityConverter.TryGetStoreConversationId(text, out conversationId) && conversationId != null)
				{
					activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["cid"], conversationId.ToString());
					return;
				}
				activityCustomProperties.Add("BadConversationId", text);
				conversionState.Errors |= ActivityConverter.ConversionErrors.BadConversationId;
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0000FAAC File Offset: 0x0000DCAC
		private static bool TryGetIntegerProperty(ClientLogEvent logEvent, string keyName, bool isRequiredProperty, ActivityConverter.ConversionState conversionState, out int propertyValue)
		{
			propertyValue = 0;
			string text;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, keyName, isRequiredProperty, conversionState, out text) && int.TryParse(text, out propertyValue))
			{
				return true;
			}
			ActivityConverter.tracer.TraceError<string, string>(0L, "[ActivityConverter.TryGetIntegerProperty] failed to get integer property for '{0}'; value '{1}' cannot be converted to an integer", keyName, text);
			return false;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0000FAEC File Offset: 0x0000DCEC
		private void AddSessionStartProperties(ClientLogEvent clientLogEvent, Dictionary<string, string> activityCustomProperties, ActivityConverter.ConversionState conversionState)
		{
			string value;
			if (ActivityConverter.TryGetClientLogEventProperty(clientLogEvent, "l", true, conversionState, out value))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["l"], value);
			}
			string value2;
			if (ActivityConverter.TryGetClientLogEventProperty(clientLogEvent, "tz", true, conversionState, out value2))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["tz"], value2);
			}
			activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["ip"], this.ipAddress);
			activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["brn"], this.userAgent);
			if (this.mailboxSession.Mailbox != null)
			{
				bool valueOrDefault = this.mailboxSession.Mailbox.GetValueOrDefault<bool>(MailboxSchema.InferenceUserUIReady, false);
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["uir"], valueOrDefault ? "1" : "0");
			}
			if (this.userContext.FeaturesManager != null)
			{
				HashSet<string> enabledFlightedFeatures = this.userContext.FeaturesManager.GetEnabledFlightedFeatures(FlightedFeatureScope.Any);
				string value3 = string.Join<string>(",", enabledFlightedFeatures);
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["fl"], value3);
			}
			int num;
			if (ActivityConverter.TryGetIntegerProperty(clientLogEvent, "uio", true, conversionState, out num))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["uio"], num.ToString());
			}
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0000FC38 File Offset: 0x0000DE38
		private static void GetDestinationFolderProperties(ClientLogEvent logEvent, Dictionary<string, string> activityCustomProperties, ActivityConverter.ConversionState conversionState)
		{
			string idString;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "df", true, conversionState, out idString))
			{
				StoreObjectId storeObjectId = null;
				if (ActivityConverter.TryGetStoreId(idString, BasicTypes.Folder, out storeObjectId))
				{
					activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["df"], storeObjectId.ToString());
				}
			}
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0000FC80 File Offset: 0x0000DE80
		private static void GetSelectedFolderProperties(ClientLogEvent logEvent, Dictionary<string, string> activityCustomProperties, ActivityConverter.ConversionState conversionState)
		{
			string value;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "sfn", false, conversionState, out value))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["sfn"], value);
			}
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0000FCB4 File Offset: 0x0000DEB4
		private static void GetGroupMailboxProperties(ClientLogEvent logEvent, Dictionary<string, string> activityCustomProperties, ActivityConverter.ConversionState conversionState)
		{
			string value;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "gms", false, conversionState, out value))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["gms"], value);
			}
			string value2;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "gmt", false, conversionState, out value2))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["gmt"], value2);
			}
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0000FD10 File Offset: 0x0000DF10
		private static void GetIsClutterProperties(ClientLogEvent logEvent, Dictionary<string, string> activityCustomProperties, ActivityConverter.ConversionState conversionState)
		{
			int num;
			if (ActivityConverter.TryGetIntegerProperty(logEvent, "cl", true, conversionState, out num))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["cl"], num.ToString());
			}
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0000FD4C File Offset: 0x0000DF4C
		private static void AddClutterExpansionProperties(ClientLogEvent logEvent, Dictionary<string, string> activityCustomProperties, ActivityConverter.ConversionState conversionState)
		{
			string value;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "ct", true, conversionState, out value))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["ct"], value);
			}
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0000FD80 File Offset: 0x0000DF80
		private static void AddFeatureSurveyProperties(ClientLogEvent logEvent, Dictionary<string, string> activityCustomProperties, ActivityConverter.ConversionState conversionState)
		{
			string value;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "fvr", true, conversionState, out value))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["fvr"], value);
			}
			string value2;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "fvc", false, conversionState, out value2))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["fvc"], value2);
			}
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0000FDDC File Offset: 0x0000DFDC
		private static void GetSurveyResponseProperties(ClientLogEvent logEvent, Dictionary<string, string> activityCustomProperties, ActivityConverter.ConversionState conversionState, UserContext userContext)
		{
			string value;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "sc", true, conversionState, out value))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["sc"], value);
			}
			string value2;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "sfq", true, conversionState, out value2))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["sfq"], value2);
			}
			string value3;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "ssq", true, conversionState, out value3))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["ssq"], value3);
			}
			string value4;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "sfm", true, conversionState, out value4))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["sfm"], value4);
			}
			string value5;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "ssd", true, conversionState, out value5))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["ssd"], value5);
			}
			string value6;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "ssz", true, conversionState, out value6))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["ssz"], value6);
			}
			string value7;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "sdm", true, conversionState, out value7))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["sdm"], value7);
			}
			string value8;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "sds", true, conversionState, out value8))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["sds"], value8);
			}
			string value9;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "scd", true, conversionState, out value9))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["scd"], value9);
			}
			string value10;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "Bld", true, conversionState, out value10))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["Bld"], value10);
			}
			if (userContext.FeaturesManager != null)
			{
				HashSet<string> enabledFlightedFeatures = userContext.FeaturesManager.GetEnabledFlightedFeatures(FlightedFeatureScope.Any);
				string value11 = string.Join<string>(",", enabledFlightedFeatures);
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["fl"], value11);
			}
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0000FFB0 File Offset: 0x0000E1B0
		private static void AddInferenceUiDisabledProperties(ClientLogEvent logEvent, Dictionary<string, string> activityCustomProperties, ActivityConverter.ConversionState conversionState)
		{
			string value;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "uds", true, conversionState, out value))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["uds"], value);
			}
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0000FFE4 File Offset: 0x0000E1E4
		private static void AddPivotNavigationProperties(ClientLogEvent logEvent, Dictionary<string, string> activityCustomProperties, ActivityConverter.ConversionState conversionState)
		{
			string value;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "mp", true, conversionState, out value))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["mp"], value);
			}
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00010018 File Offset: 0x0000E218
		private static void AddConversionErrorProperties(Dictionary<string, string> activityCustomProperties, ActivityConverter.ConversionState conversionState)
		{
			string text = conversionState.Errors.ToString();
			activityCustomProperties.Add("ConversionErrors", text);
			ActivityConverter.tracer.TraceError<string>(0L, "[ActivityConverter.AddConversionStateProperties] There were errors during conversion: {0}", text);
			if ((conversionState.Errors & ActivityConverter.ConversionErrors.MissingRequiredProperties) == ActivityConverter.ConversionErrors.MissingRequiredProperties)
			{
				string text2 = string.Join(",", conversionState.MissingPropertyList.ToArray());
				activityCustomProperties.Add("MissingProperties", text2);
				ActivityConverter.tracer.TraceError<string>(0L, "[ActivityConverter.AddConversionStateProperties] Missing required properties: {0}", text2);
			}
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00010094 File Offset: 0x0000E294
		private static void GetRequiredItemActivityProperties(ClientLogEvent logEvent, ActivityConverter.PropertySet requiredProperties, Dictionary<string, string> activityCustomProperties, ActivityConverter.ConversionState conversionState)
		{
			if ((requiredProperties & ActivityConverter.PropertySet.Delete) != ActivityConverter.PropertySet.None)
			{
				ActivityConverter.GetDeleteProperties(logEvent, activityCustomProperties, conversionState);
			}
			if ((requiredProperties & ActivityConverter.PropertySet.DestinationFolder) != ActivityConverter.PropertySet.None)
			{
				ActivityConverter.GetDestinationFolderProperties(logEvent, activityCustomProperties, conversionState);
			}
			if ((requiredProperties & ActivityConverter.PropertySet.IsClutter) != ActivityConverter.PropertySet.None)
			{
				ActivityConverter.GetIsClutterProperties(logEvent, activityCustomProperties, conversionState);
			}
			if ((requiredProperties & ActivityConverter.PropertySet.SelectedFolder) != ActivityConverter.PropertySet.None)
			{
				ActivityConverter.GetSelectedFolderProperties(logEvent, activityCustomProperties, conversionState);
			}
			if ((requiredProperties & ActivityConverter.PropertySet.GroupMailbox) != ActivityConverter.PropertySet.None)
			{
				ActivityConverter.GetGroupMailboxProperties(logEvent, activityCustomProperties, conversionState);
			}
			if ((requiredProperties & ActivityConverter.PropertySet.ConversationInfo) != ActivityConverter.PropertySet.None)
			{
				ActivityConverter.GetConversationProperties(logEvent, activityCustomProperties, conversionState);
			}
			else
			{
				ActivityConverter.GetConversationId(logEvent, false, activityCustomProperties, conversionState);
			}
			if (conversionState.Errors != ActivityConverter.ConversionErrors.None)
			{
				ActivityConverter.AddConversionErrorProperties(activityCustomProperties, conversionState);
			}
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00010114 File Offset: 0x0000E314
		private static void AddSearchSessionCommonProperties(ClientLogEvent logEvent, Dictionary<string, string> activityCustomProperties, ActivityConverter.ConversionState conversionState)
		{
			string value;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "issi", true, conversionState, out value))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["issi"], value);
			}
			string value2;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "isss", true, conversionState, out value2))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["isss"], value2);
			}
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00010170 File Offset: 0x0000E370
		private static void AddSearchRequestCommonProperties(ClientLogEvent logEvent, Dictionary<string, string> activityCustomProperties, ActivityConverter.ConversionState conversionState)
		{
			string value;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "issi", true, conversionState, out value))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["issi"], value);
			}
			string value2;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "isrid", false, conversionState, out value2))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["isrid"], value2);
			}
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x000101CC File Offset: 0x0000E3CC
		private static void AddSearchResultsProperties(ClientLogEvent logEvent, Dictionary<string, string> activityCustomProperties, ActivityConverter.ConversionState conversionState)
		{
			string value;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "isrc", true, conversionState, out value))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["isrc"], value);
			}
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00010200 File Offset: 0x0000E400
		private static void AddSearchRefinersReceivedProperties(ClientLogEvent logEvent, Dictionary<string, string> activityCustomProperties, ActivityConverter.ConversionState conversionState)
		{
			string value;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "isrd", true, conversionState, out value))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["isrd"], value);
			}
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00010234 File Offset: 0x0000E434
		private static void AddSearchRequestEndProperties(ClientLogEvent logEvent, Dictionary<string, string> activityCustomProperties, ActivityConverter.ConversionState conversionState)
		{
			string value;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "issu", true, conversionState, out value))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["issu"], value);
			}
			string value2;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "isqs", true, conversionState, out value2))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["isqs"], value2);
			}
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00010290 File Offset: 0x0000E490
		private static void AddSearchSessionEndProperties(ClientLogEvent logEvent, Dictionary<string, string> activityCustomProperties, ActivityConverter.ConversionState conversionState)
		{
			string value;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "issa", true, conversionState, out value))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["issa"], value);
			}
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "istsa", true, conversionState, out value))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["istsa"], value);
			}
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x000102EC File Offset: 0x0000E4EC
		private static void AddIntroductionPeekProperties(ClientLogEvent logEvent, Dictionary<string, string> activityCustomProperties, ActivityConverter.ConversionState conversionState)
		{
			string value;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "ipks", true, conversionState, out value))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["ipks"], value);
			}
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00010320 File Offset: 0x0000E520
		private static void AddHelpPanelProperties(ClientLogEvent logEvent, Dictionary<string, string> activityCustomProperties, ActivityConverter.ConversionState conversionState)
		{
			string value;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "hlpm", true, conversionState, out value))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["hlpm"], value);
			}
			string value2;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "hlpa", true, conversionState, out value2))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["hlpa"], value2);
			}
			string value3;
			if (ActivityConverter.TryGetClientLogEventProperty(logEvent, "hlpc", true, conversionState, out value3))
			{
				activityCustomProperties.Add(ActivityConverter.reportingLabelsForKeys["hlpc"], value3);
			}
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x000103A4 File Offset: 0x0000E5A4
		private void GetRequiredItemlessActivityProperties(ClientLogEvent logEvent, ActivityConverter.PropertySet requiredProperties, Dictionary<string, string> activityCustomProperties, ActivityConverter.ConversionState conversionState)
		{
			if (requiredProperties == ActivityConverter.PropertySet.Logon)
			{
				this.AddSessionStartProperties(logEvent, activityCustomProperties, conversionState);
			}
			if (requiredProperties == ActivityConverter.PropertySet.ClutterExpansion)
			{
				ActivityConverter.AddClutterExpansionProperties(logEvent, activityCustomProperties, conversionState);
			}
			if (requiredProperties == ActivityConverter.PropertySet.FeatureSurvey)
			{
				ActivityConverter.AddFeatureSurveyProperties(logEvent, activityCustomProperties, conversionState);
			}
			if (requiredProperties == ActivityConverter.PropertySet.SurveyResponse)
			{
				ActivityConverter.GetSurveyResponseProperties(logEvent, activityCustomProperties, conversionState, this.userContext);
			}
			if (requiredProperties == ActivityConverter.PropertySet.InferenceUiDisabled)
			{
				ActivityConverter.AddInferenceUiDisabledProperties(logEvent, activityCustomProperties, conversionState);
			}
			if (requiredProperties == ActivityConverter.PropertySet.PivotNavigation)
			{
				ActivityConverter.AddPivotNavigationProperties(logEvent, activityCustomProperties, conversionState);
			}
			if (requiredProperties == ActivityConverter.PropertySet.IntroductionPeek)
			{
				ActivityConverter.AddIntroductionPeekProperties(logEvent, activityCustomProperties, conversionState);
			}
			if (requiredProperties == ActivityConverter.PropertySet.HelpPanel)
			{
				ActivityConverter.AddHelpPanelProperties(logEvent, activityCustomProperties, conversionState);
			}
			if ((requiredProperties & ActivityConverter.PropertySet.SearchSessionCommon) != ActivityConverter.PropertySet.None)
			{
				ActivityConverter.AddSearchSessionCommonProperties(logEvent, activityCustomProperties, conversionState);
			}
			if ((requiredProperties & ActivityConverter.PropertySet.SearchSessionEnd) != ActivityConverter.PropertySet.None)
			{
				ActivityConverter.AddSearchSessionEndProperties(logEvent, activityCustomProperties, conversionState);
			}
			if ((requiredProperties & ActivityConverter.PropertySet.SearchRequestCommon) != ActivityConverter.PropertySet.None)
			{
				ActivityConverter.AddSearchRequestCommonProperties(logEvent, activityCustomProperties, conversionState);
			}
			if ((requiredProperties & ActivityConverter.PropertySet.SearchResultsReceived) != ActivityConverter.PropertySet.None)
			{
				ActivityConverter.AddSearchResultsProperties(logEvent, activityCustomProperties, conversionState);
			}
			if ((requiredProperties & ActivityConverter.PropertySet.SearchRefinersReceived) != ActivityConverter.PropertySet.None)
			{
				ActivityConverter.AddSearchRefinersReceivedProperties(logEvent, activityCustomProperties, conversionState);
			}
			if ((requiredProperties & ActivityConverter.PropertySet.SearchRequestEnd) != ActivityConverter.PropertySet.None)
			{
				ActivityConverter.AddSearchRequestEndProperties(logEvent, activityCustomProperties, conversionState);
			}
			if ((requiredProperties & ActivityConverter.PropertySet.SelectedFolder) != ActivityConverter.PropertySet.None)
			{
				ActivityConverter.GetSelectedFolderProperties(logEvent, activityCustomProperties, conversionState);
			}
			if (conversionState.Errors != ActivityConverter.ConversionErrors.None)
			{
				ActivityConverter.AddConversionErrorProperties(activityCustomProperties, conversionState);
			}
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x000104C8 File Offset: 0x0000E6C8
		private long GetNextActivitySequenceNumber()
		{
			return this.userContext.GetNextClientActivitySequenceNumber();
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x000104D8 File Offset: 0x0000E6D8
		private Activity CreateErrorActivity(string errorMessage)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("ConversionErrors", errorMessage);
			return new Activity(ActivityId.Error, ClientId.Web, ExDateTime.UtcNow, this.clientSessionId, this.clientVersion, this.GetNextActivitySequenceNumber(), this.mailboxSession, null, null, dictionary);
		}

		// Token: 0x040002B7 RID: 695
		internal const string SessionInfoDatapointName = "SessionInfo";

		// Token: 0x040002B8 RID: 696
		internal const string MoveToDeletedItems = "MoveToDeletedItems";

		// Token: 0x040002B9 RID: 697
		internal const string ConversionErrorsReportingLabel = "ConversionErrors";

		// Token: 0x040002BA RID: 698
		internal const string MissingPropertiesReportingLabel = "MissingProperties";

		// Token: 0x040002BB RID: 699
		internal const string BadItemIdsReportingLabel = "BadItemIds";

		// Token: 0x040002BC RID: 700
		internal const string BadConversationIdReportingLabel = "BadConversationId";

		// Token: 0x040002BD RID: 701
		internal const string ItemIdKeyName = "id";

		// Token: 0x040002BE RID: 702
		internal const string ItemIdArrayKeyName = "ids";

		// Token: 0x040002BF RID: 703
		internal const string DeleteTypeKeyName = "dt";

		// Token: 0x040002C0 RID: 704
		public const string SelectedFolderNameKeyName = "sfn";

		// Token: 0x040002C1 RID: 705
		public const string GroupMailboxSmtpAddressKeyName = "gms";

		// Token: 0x040002C2 RID: 706
		public const string GroupMailboxTypeKeyName = "gmt";

		// Token: 0x040002C3 RID: 707
		internal const string DestinationFolderIdKeyName = "df";

		// Token: 0x040002C4 RID: 708
		internal const string SessionInfoIPAddressKeyName = "ip";

		// Token: 0x040002C5 RID: 709
		internal const string IsClutterKeyName = "cl";

		// Token: 0x040002C6 RID: 710
		internal const string IsConversationKeyName = "co";

		// Token: 0x040002C7 RID: 711
		internal const string ConversationIdKeyName = "cid";

		// Token: 0x040002C8 RID: 712
		internal const string UnreadCountKeyName = "ct";

		// Token: 0x040002C9 RID: 713
		internal const string InferenceUiReadyKeyName = "uir";

		// Token: 0x040002CA RID: 714
		internal const string InferenceUiOnKeyName = "uio";

		// Token: 0x040002CB RID: 715
		internal const string FlightsListKeyName = "fl";

		// Token: 0x040002CC RID: 716
		internal const string DeletedByMultiSelectKeyName = "dm";

		// Token: 0x040002CD RID: 717
		internal const string DeletedByDeleteAllKeyName = "cda";

		// Token: 0x040002CE RID: 718
		internal const string FeatureValueResponseKeyName = "fvr";

		// Token: 0x040002CF RID: 719
		public const string FeatureValueCommentsKeyName = "fvc";

		// Token: 0x040002D0 RID: 720
		public const string SurveyComponentKeyName = "sc";

		// Token: 0x040002D1 RID: 721
		public const string SurveyFirstQuestionKeyName = "sfq";

		// Token: 0x040002D2 RID: 722
		public const string SurveySecondQuestionKeyName = "ssq";

		// Token: 0x040002D3 RID: 723
		public const string SurveyFeedbackMessageKeyName = "sfm";

		// Token: 0x040002D4 RID: 724
		public const string SurveySendKeyName = "ssd";

		// Token: 0x040002D5 RID: 725
		public const string SurveySnoozeKeyName = "ssz";

		// Token: 0x040002D6 RID: 726
		public const string SurveyDismissKeyName = "sdm";

		// Token: 0x040002D7 RID: 727
		public const string SurveyDontShowAgainKeyName = "sds";

		// Token: 0x040002D8 RID: 728
		public const string SurveyCustomDataKeyName = "scd";

		// Token: 0x040002D9 RID: 729
		public const string ServerVersionKeyName = "Bld";

		// Token: 0x040002DA RID: 730
		internal const string InferenceUiDisabledSourceKeyName = "uds";

		// Token: 0x040002DB RID: 731
		internal const string PivotNavigationDestinationKeyName = "mp";

		// Token: 0x040002DC RID: 732
		private const string SearchSessionId = "issi";

		// Token: 0x040002DD RID: 733
		private const string SearchSessionStatistics = "isss";

		// Token: 0x040002DE RID: 734
		private const string SearchSuccessActivity = "issa";

		// Token: 0x040002DF RID: 735
		private const string SearchTentativeSuccessActivity = "istsa";

		// Token: 0x040002E0 RID: 736
		private const string SearchRequestId = "isrid";

		// Token: 0x040002E1 RID: 737
		private const string SearchResultsCount = "isrc";

		// Token: 0x040002E2 RID: 738
		private const string SearchTriggerType = "istt";

		// Token: 0x040002E3 RID: 739
		private const string SearchRefinersData = "isrd";

		// Token: 0x040002E4 RID: 740
		private const string IsSearchSuccessful = "issu";

		// Token: 0x040002E5 RID: 741
		private const string InstantSearchQueryStatistics = "isqs";

		// Token: 0x040002E6 RID: 742
		private const string IntroductionPeekKeyName = "ipks";

		// Token: 0x040002E7 RID: 743
		private const string HelpPanelModuleKeyName = "hlpm";

		// Token: 0x040002E8 RID: 744
		private const string HelpArticleIdsKeyName = "hlpa";

		// Token: 0x040002E9 RID: 745
		private const string HelpArticleClickedKeyName = "hlpc";

		// Token: 0x040002EA RID: 746
		private readonly IMailboxSession mailboxSession;

		// Token: 0x040002EB RID: 747
		private readonly UserContext userContext;

		// Token: 0x040002EC RID: 748
		private readonly string ipAddress;

		// Token: 0x040002ED RID: 749
		private readonly string userAgent;

		// Token: 0x040002EE RID: 750
		private readonly string clientVersion;

		// Token: 0x040002EF RID: 751
		private readonly Guid clientSessionId;

		// Token: 0x040002F0 RID: 752
		private static Trace tracer = ExTraceGlobals.ActivityConverterTracer;

		// Token: 0x040002F1 RID: 753
		private static Dictionary<ActivityId, ActivityConverter.PropertySet> propertiesForActivities = new Dictionary<ActivityId, ActivityConverter.PropertySet>
		{
			{
				ActivityId.Categorize,
				ActivityConverter.PropertySet.Category
			},
			{
				ActivityId.ClutterGroupOpened,
				ActivityConverter.PropertySet.ClutterExpansion
			},
			{
				ActivityId.Delete,
				ActivityConverter.PropertySet.Delete | ActivityConverter.PropertySet.ConversationInfo | ActivityConverter.PropertySet.SelectedFolder
			},
			{
				ActivityId.FeatureValueResponse,
				ActivityConverter.PropertySet.FeatureSurvey
			},
			{
				ActivityId.Flag,
				ActivityConverter.PropertySet.ConversationInfo | ActivityConverter.PropertySet.SelectedFolder
			},
			{
				ActivityId.FlagCleared,
				ActivityConverter.PropertySet.ConversationInfo | ActivityConverter.PropertySet.SelectedFolder
			},
			{
				ActivityId.FlagComplete,
				ActivityConverter.PropertySet.ConversationInfo | ActivityConverter.PropertySet.SelectedFolder
			},
			{
				ActivityId.InspectorDisplayStart,
				ActivityConverter.PropertySet.IsClutter
			},
			{
				ActivityId.Logon,
				ActivityConverter.PropertySet.Logon
			},
			{
				ActivityId.Move,
				ActivityConverter.PropertySet.DestinationFolder | ActivityConverter.PropertySet.SelectedFolder
			},
			{
				ActivityId.NewMessage,
				ActivityConverter.PropertySet.SelectedFolder
			},
			{
				ActivityId.Reply,
				ActivityConverter.PropertySet.SelectedFolder
			},
			{
				ActivityId.ReplyAll,
				ActivityConverter.PropertySet.SelectedFolder
			},
			{
				ActivityId.Forward,
				ActivityConverter.PropertySet.SelectedFolder
			},
			{
				ActivityId.ModernGroupsQuickCompose,
				ActivityConverter.PropertySet.GroupMailbox
			},
			{
				ActivityId.ModernGroupsQuickReply,
				ActivityConverter.PropertySet.GroupMailbox
			},
			{
				ActivityId.ModernGroupsConversationSelected,
				ActivityConverter.PropertySet.GroupMailbox
			},
			{
				ActivityId.PivotChange,
				ActivityConverter.PropertySet.PivotNavigation
			},
			{
				ActivityId.ReadingPaneDisplayStart,
				ActivityConverter.PropertySet.ConversationInfo | ActivityConverter.PropertySet.IsClutter | ActivityConverter.PropertySet.SelectedFolder
			},
			{
				ActivityId.ReadingPaneDisplayEnd,
				ActivityConverter.PropertySet.ConversationInfo
			},
			{
				ActivityId.TurnInferenceOff,
				ActivityConverter.PropertySet.InferenceUiDisabled
			},
			{
				ActivityId.MessageSelected,
				ActivityConverter.PropertySet.ConversationInfo | ActivityConverter.PropertySet.SelectedFolder
			},
			{
				ActivityId.MarkAsRead,
				ActivityConverter.PropertySet.ConversationInfo | ActivityConverter.PropertySet.SelectedFolder
			},
			{
				ActivityId.MarkAsUnread,
				ActivityConverter.PropertySet.ConversationInfo | ActivityConverter.PropertySet.SelectedFolder
			},
			{
				ActivityId.MarkAllItemsAsRead,
				ActivityConverter.PropertySet.SelectedFolder
			},
			{
				ActivityId.MarkMessageAsClutter,
				ActivityConverter.PropertySet.ConversationInfo
			},
			{
				ActivityId.MarkMessageAsNotClutter,
				ActivityConverter.PropertySet.ConversationInfo
			},
			{
				ActivityId.IgnoreConversation,
				ActivityConverter.PropertySet.ConversationInfo
			},
			{
				ActivityId.SearchSessionStart,
				ActivityConverter.PropertySet.SearchSessionCommon
			},
			{
				ActivityId.SearchSessionEnd,
				ActivityConverter.PropertySet.SearchSessionCommon | ActivityConverter.PropertySet.SearchSessionEnd
			},
			{
				ActivityId.SearchRequestStart,
				ActivityConverter.PropertySet.SearchRequestCommon | ActivityConverter.PropertySet.SearchRequestStart
			},
			{
				ActivityId.SearchResultsReceived,
				ActivityConverter.PropertySet.SearchRequestCommon | ActivityConverter.PropertySet.SearchResultsReceived
			},
			{
				ActivityId.SearchRefinersReceived,
				ActivityConverter.PropertySet.SearchRequestCommon | ActivityConverter.PropertySet.SearchRefinersReceived
			},
			{
				ActivityId.SearchRequestEnd,
				ActivityConverter.PropertySet.SearchRequestCommon | ActivityConverter.PropertySet.SearchRequestEnd
			},
			{
				ActivityId.IntroductionPeekControllerCreated,
				ActivityConverter.PropertySet.IntroductionPeek
			},
			{
				ActivityId.IntroductionPeekShown,
				ActivityConverter.PropertySet.IntroductionPeek
			},
			{
				ActivityId.IntroductionPeekDismissed,
				ActivityConverter.PropertySet.IntroductionPeek
			},
			{
				ActivityId.IntroductionLearnMoreClicked,
				ActivityConverter.PropertySet.IntroductionPeek
			},
			{
				ActivityId.IntroductionTryFeatureClicked,
				ActivityConverter.PropertySet.IntroductionPeek
			},
			{
				ActivityId.SurveyResponse,
				ActivityConverter.PropertySet.SurveyResponse
			},
			{
				ActivityId.HelpPanelShown,
				ActivityConverter.PropertySet.HelpPanel
			},
			{
				ActivityId.HelpPanelClosed,
				ActivityConverter.PropertySet.HelpPanel
			},
			{
				ActivityId.HelpArticleShown,
				ActivityConverter.PropertySet.HelpPanel
			},
			{
				ActivityId.HelpArticleLinkClicked,
				ActivityConverter.PropertySet.HelpPanel
			}
		};

		// Token: 0x040002F2 RID: 754
		private static HashSet<ActivityId> multipleItemActivities = new HashSet<ActivityId>
		{
			ActivityId.Categorize,
			ActivityId.Delete,
			ActivityId.Flag,
			ActivityId.FlagCleared,
			ActivityId.FlagComplete,
			ActivityId.IgnoreConversation,
			ActivityId.MarkAsUnread,
			ActivityId.MarkAsRead,
			ActivityId.MarkMessageAsClutter,
			ActivityId.MarkMessageAsNotClutter,
			ActivityId.Move,
			ActivityId.ReadingPaneDisplayStart,
			ActivityId.ReadingPaneDisplayEnd,
			ActivityId.MessageSelected
		};

		// Token: 0x040002F3 RID: 755
		private static HashSet<ActivityId> itemlessActivities = new HashSet<ActivityId>
		{
			ActivityId.ClutterGroupOpened,
			ActivityId.ClutterGroupClosed,
			ActivityId.FeatureValueResponse,
			ActivityId.Logon,
			ActivityId.NewMessage,
			ActivityId.PivotChange,
			ActivityId.TurnInferenceOff,
			ActivityId.TurnInferenceOn,
			ActivityId.SearchSessionStart,
			ActivityId.SearchSessionEnd,
			ActivityId.SearchRequestStart,
			ActivityId.SearchResultsReceived,
			ActivityId.SearchRefinersReceived,
			ActivityId.SearchRequestEnd,
			ActivityId.IntroductionPeekControllerCreated,
			ActivityId.IntroductionPeekShown,
			ActivityId.IntroductionPeekDismissed,
			ActivityId.IntroductionLearnMoreClicked,
			ActivityId.IntroductionTryFeatureClicked,
			ActivityId.SurveyResponse,
			ActivityId.MarkAllItemsAsRead,
			ActivityId.HelpCenterShown,
			ActivityId.HelpPanelCreated,
			ActivityId.HelpPanelShown,
			ActivityId.HelpPanelClosed,
			ActivityId.HelpArticleShown,
			ActivityId.HelpArticleLinkClicked,
			ActivityId.UserPhotoUploaded
		};

		// Token: 0x040002F4 RID: 756
		private static Dictionary<string, string> reportingLabelsForKeys = new Dictionary<string, string>
		{
			{
				"cda",
				"DeletedByDeleteAll"
			},
			{
				"dm",
				"DeletedByMultiSelect"
			},
			{
				"dt",
				"DeleteType"
			},
			{
				"df",
				"DestinationFolder"
			},
			{
				"fvc",
				"FeatureComments"
			},
			{
				"fvr",
				"FeatureValue"
			},
			{
				"fl",
				"Flights"
			},
			{
				"uds",
				"InferenceUiDisabled"
			},
			{
				"uio",
				"InferenceUiOnKeyName"
			},
			{
				"uir",
				"InferenceUiReady"
			},
			{
				"cl",
				"IsClutter"
			},
			{
				"co",
				"IsConversation"
			},
			{
				"mp",
				"PivotDestination"
			},
			{
				"brn",
				"Browser"
			},
			{
				"ip",
				"IPAddress"
			},
			{
				"l",
				"Layout"
			},
			{
				"tz",
				"Timezone"
			},
			{
				"ct",
				"UnreadCount"
			},
			{
				"cid",
				"ConversationId"
			},
			{
				"sfn",
				"SelectedFolderName"
			},
			{
				"gms",
				"GroupMailboxSmtpAddress"
			},
			{
				"gmt",
				"GroupMailboxType"
			},
			{
				"issi",
				"SearchSessionId"
			},
			{
				"isrid",
				"SearchRequestId"
			},
			{
				"isrc",
				"SearchResultsCount"
			},
			{
				"istt",
				"SearchTriggerType"
			},
			{
				"isrd",
				"SearchRefinersData"
			},
			{
				"issu",
				"SearchSucessful"
			},
			{
				"isqs",
				"SearchQueryStatistics"
			},
			{
				"ipks",
				"IntroductionPeekSource"
			},
			{
				"sc",
				"SurveyComponent"
			},
			{
				"sfq",
				"FirstQuestion"
			},
			{
				"ssq",
				"SecondQuestion"
			},
			{
				"sfm",
				"FeedbackMessage"
			},
			{
				"ssd",
				"Send"
			},
			{
				"ssz",
				"Snooze"
			},
			{
				"sdm",
				"Dismiss"
			},
			{
				"sds",
				"DontShowAgain"
			},
			{
				"scd",
				"SurveyCustomData"
			},
			{
				"hlpm",
				"HelpPanelModule"
			},
			{
				"hlpa",
				"HelpArticleIds"
			},
			{
				"hlpc",
				"HelpArticleClicked"
			},
			{
				"Bld",
				"ServerVersion"
			},
			{
				"isss",
				"SearchSessionStatistics"
			},
			{
				"issa",
				"SearchSuccess"
			},
			{
				"istsa",
				"SearchTentativeSuccess"
			}
		};

		// Token: 0x0200008F RID: 143
		[Flags]
		private enum PropertySet
		{
			// Token: 0x040002F6 RID: 758
			None = 0,
			// Token: 0x040002F7 RID: 759
			Logon = 1,
			// Token: 0x040002F8 RID: 760
			Delete = 2,
			// Token: 0x040002F9 RID: 761
			ConversationInfo = 4,
			// Token: 0x040002FA RID: 762
			DestinationFolder = 8,
			// Token: 0x040002FB RID: 763
			Category = 16,
			// Token: 0x040002FC RID: 764
			IsClutter = 32,
			// Token: 0x040002FD RID: 765
			ClutterExpansion = 64,
			// Token: 0x040002FE RID: 766
			PivotNavigation = 128,
			// Token: 0x040002FF RID: 767
			InferenceUiDisabled = 256,
			// Token: 0x04000300 RID: 768
			FeatureSurvey = 512,
			// Token: 0x04000301 RID: 769
			SelectedFolder = 1024,
			// Token: 0x04000302 RID: 770
			SearchSessionCommon = 2048,
			// Token: 0x04000303 RID: 771
			SearchRequestCommon = 4096,
			// Token: 0x04000304 RID: 772
			SearchRequestStart = 8192,
			// Token: 0x04000305 RID: 773
			SearchResultsReceived = 16384,
			// Token: 0x04000306 RID: 774
			SearchRefinersReceived = 32768,
			// Token: 0x04000307 RID: 775
			SearchRequestEnd = 65536,
			// Token: 0x04000308 RID: 776
			IntroductionPeek = 131072,
			// Token: 0x04000309 RID: 777
			SurveyResponse = 262144,
			// Token: 0x0400030A RID: 778
			GroupMailbox = 524288,
			// Token: 0x0400030B RID: 779
			HelpPanel = 1048576,
			// Token: 0x0400030C RID: 780
			SearchSessionEnd = 2097152
		}

		// Token: 0x02000090 RID: 144
		[Flags]
		private enum ConversionErrors
		{
			// Token: 0x0400030E RID: 782
			None = 0,
			// Token: 0x0400030F RID: 783
			BadActivityId = 1,
			// Token: 0x04000310 RID: 784
			BadTimestamp = 2,
			// Token: 0x04000311 RID: 785
			BadItemIds = 4,
			// Token: 0x04000312 RID: 786
			MissingRequiredProperties = 8,
			// Token: 0x04000313 RID: 787
			BadConversationId = 16
		}

		// Token: 0x02000091 RID: 145
		private class ConversionState
		{
			// Token: 0x170001A2 RID: 418
			// (get) Token: 0x06000564 RID: 1380 RVA: 0x00010525 File Offset: 0x0000E725
			// (set) Token: 0x06000565 RID: 1381 RVA: 0x0001052D File Offset: 0x0000E72D
			public ActivityConverter.ConversionErrors Errors { get; set; }

			// Token: 0x170001A3 RID: 419
			// (get) Token: 0x06000566 RID: 1382 RVA: 0x00010536 File Offset: 0x0000E736
			// (set) Token: 0x06000567 RID: 1383 RVA: 0x0001053E File Offset: 0x0000E73E
			public List<string> MissingPropertyList { get; private set; }

			// Token: 0x06000568 RID: 1384 RVA: 0x00010547 File Offset: 0x0000E747
			public void AddMissingProperty(string propertyName)
			{
				if (this.MissingPropertyList == null)
				{
					this.MissingPropertyList = new List<string>();
				}
				this.MissingPropertyList.Add(propertyName);
				this.Errors |= ActivityConverter.ConversionErrors.MissingRequiredProperties;
			}
		}
	}
}
