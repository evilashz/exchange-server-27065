using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.Web;
using System.Web.Configuration;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.DispatchPipe.Ews;
using Microsoft.Exchange.Services.Wcf;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Win32;

namespace Microsoft.Exchange.Services
{
	// Token: 0x02000018 RID: 24
	internal static class EWSSettings
	{
		// Token: 0x06000125 RID: 293 RVA: 0x00006760 File Offset: 0x00004960
		private static T SafeGetMessageProperty<T>(string keyName, T defaultValue)
		{
			EwsOperationContextBase operationContext = EWSSettings.GetOperationContext();
			if (operationContext == null || operationContext.RequestMessage == null || operationContext.RequestMessage.State == MessageState.Closed)
			{
				EWSSettings.TraceSafeGetSetMessagePropertyFailure("EWSSettings::SafeGetMessageProperty", operationContext);
				return defaultValue;
			}
			object obj;
			if (operationContext.RequestMessage.Properties.TryGetValue(keyName, out obj) && obj is T)
			{
				return (T)((object)obj);
			}
			return defaultValue;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000067C0 File Offset: 0x000049C0
		private static void SafeSetMessageProperty<T>(string keyName, T value)
		{
			EwsOperationContextBase operationContext = EWSSettings.GetOperationContext();
			if (operationContext == null || operationContext.RequestMessage == null || operationContext.RequestMessage.State == MessageState.Closed)
			{
				EWSSettings.TraceSafeGetSetMessagePropertyFailure("EWSSettings::SafeSetMessageProperty", operationContext);
				return;
			}
			operationContext.RequestMessage.Properties[keyName] = value;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00006810 File Offset: 0x00004A10
		private static void TraceSafeGetSetMessagePropertyFailure(string functionName, EwsOperationContextBase operationContext)
		{
			string text;
			string text2;
			string text3;
			if (operationContext == null)
			{
				text = "null";
				text2 = "n/a";
				text3 = "n/a";
			}
			else if (operationContext.RequestMessage == null)
			{
				text = "non-null";
				if (operationContext is WrappedWcfOperationContext)
				{
					text2 = ((operationContext.BackingOperationContext.RequestContext != null) ? "non-null" : "null");
				}
				else
				{
					text2 = "n/a";
				}
				text3 = "null";
			}
			else
			{
				text = "non-null";
				text2 = "non-null";
				text3 = "null";
			}
			ExTraceGlobals.CommonAlgorithmTracer.TraceDebug(0L, "[{0}] Could not get/set message property because one of the following was null: OperationContext.Current({1}); OperationContext.Current.RequestContext({2}); OperationContext.Current.RequestContext.RequestMessage({3})", new object[]
			{
				functionName,
				text,
				text2,
				text3
			});
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000068B0 File Offset: 0x00004AB0
		private static T SafeGet<T>(string keyName, T defaultValue)
		{
			HttpContext httpContext = EWSSettings.GetHttpContext();
			if (httpContext == null)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug(0L, "[EWSSettings::SafeGet] Could not get HttpContext.Current property because HttpContext.Current was null");
				return defaultValue;
			}
			object obj = httpContext.Items[keyName];
			if (obj != null)
			{
				return (T)((object)obj);
			}
			return defaultValue;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000068F4 File Offset: 0x00004AF4
		private static void SafeSet<T>(string keyName, T value)
		{
			HttpContext httpContext = EWSSettings.GetHttpContext();
			if (httpContext == null)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug(0L, "[EWSSettings::SafeSet] Could not get HttpContext.Current property because HttpContext.Current was null");
				return;
			}
			httpContext.Items[keyName] = value;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600012A RID: 298 RVA: 0x0000692E File Offset: 0x00004B2E
		// (set) Token: 0x0600012B RID: 299 RVA: 0x0000693B File Offset: 0x00004B3B
		internal static HttpWebResponse ProxyResponse
		{
			get
			{
				return EWSSettings.SafeGet<HttpWebResponse>("WS_ProxyResponse", null);
			}
			set
			{
				EWSSettings.SafeSet<HttpWebResponse>("WS_ProxyResponse", value);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00006948 File Offset: 0x00004B48
		// (set) Token: 0x0600012D RID: 301 RVA: 0x00006955 File Offset: 0x00004B55
		internal static Dictionary<string, string> ProxyHopHeaders
		{
			get
			{
				return EWSSettings.SafeGet<Dictionary<string, string>>("WS_ProxyHopHeaders", null);
			}
			set
			{
				EWSSettings.SafeSet<Dictionary<string, string>>("WS_ProxyHopHeaders", value);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00006962 File Offset: 0x00004B62
		// (set) Token: 0x0600012F RID: 303 RVA: 0x0000696F File Offset: 0x00004B6F
		internal static bool InWCFChannelLayer
		{
			get
			{
				return EWSSettings.SafeGet<bool>("WS_InWCFChannelLayer", false);
			}
			set
			{
				EWSSettings.SafeSet<bool>("WS_InWCFChannelLayer", value);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000130 RID: 304 RVA: 0x0000697C File Offset: 0x00004B7C
		// (set) Token: 0x06000131 RID: 305 RVA: 0x00006989 File Offset: 0x00004B89
		internal static string FailoverType
		{
			get
			{
				return EWSSettings.SafeGet<string>("WS_FailoverType", null);
			}
			set
			{
				EWSSettings.SafeSet<string>("WS_FailoverType", value);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00006996 File Offset: 0x00004B96
		// (set) Token: 0x06000133 RID: 307 RVA: 0x000069A3 File Offset: 0x00004BA3
		internal static string ExceptionType
		{
			get
			{
				return EWSSettings.SafeGet<string>("WS_ExceptionType", null);
			}
			set
			{
				EWSSettings.SafeSet<string>("WS_ExceptionType", value);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000134 RID: 308 RVA: 0x000069B0 File Offset: 0x00004BB0
		// (set) Token: 0x06000135 RID: 309 RVA: 0x000069BD File Offset: 0x00004BBD
		internal static bool WritingToWire
		{
			get
			{
				return EWSSettings.SafeGetMessageProperty<bool>(EWSSettings.WritingToWireKey, false);
			}
			set
			{
				EWSSettings.SafeSetMessageProperty<bool>(EWSSettings.WritingToWireKey, value);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000136 RID: 310 RVA: 0x000069CA File Offset: 0x00004BCA
		// (set) Token: 0x06000137 RID: 311 RVA: 0x000069D7 File Offset: 0x00004BD7
		internal static Message MessageCopyForProxyOnly
		{
			get
			{
				return EWSSettings.SafeGet<Message>("WS_ProxyMessageCopy", null);
			}
			set
			{
				EWSSettings.SafeSet<Message>("WS_ProxyMessageCopy", value);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000138 RID: 312 RVA: 0x000069E4 File Offset: 0x00004BE4
		// (set) Token: 0x06000139 RID: 313 RVA: 0x000069F1 File Offset: 0x00004BF1
		internal static string UpnFromClaimSets
		{
			get
			{
				return EWSSettings.SafeGet<string>("WS_UpnFromClaimSets", null);
			}
			set
			{
				EWSSettings.SafeSet<string>("WS_UpnFromClaimSets", value);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600013A RID: 314 RVA: 0x000069FE File Offset: 0x00004BFE
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00006A0B File Offset: 0x00004C0B
		internal static bool FaultExceptionDueToAuthorizationManager
		{
			get
			{
				return EWSSettings.SafeGet<bool>("WS_FaultExceptionAuthZMgr", false);
			}
			set
			{
				EWSSettings.SafeSet<bool>("WS_FaultExceptionAuthZMgr", value);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00006A18 File Offset: 0x00004C18
		public static Guid RequestCorrelation
		{
			get
			{
				return EWSSettings.SafeGetMessageProperty<Guid>("WS_RequestCorrelationKey", Guid.Empty);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00006A29 File Offset: 0x00004C29
		public static int RequestThreadId
		{
			get
			{
				return EWSSettings.SafeGetMessageProperty<int>("WS_RequestThreadIdKey", -1);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00006A36 File Offset: 0x00004C36
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00006A43 File Offset: 0x00004C43
		public static Exception WcfDelayedException
		{
			get
			{
				return EWSSettings.SafeGetMessageProperty<Exception>("WS_WcfDelayedExceptionKey", null);
			}
			set
			{
				EWSSettings.SafeSetMessageProperty<Exception>("WS_WcfDelayedExceptionKey", value);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00006A50 File Offset: 0x00004C50
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00006A70 File Offset: 0x00004C70
		internal static bool? ItemHasBlockedImages
		{
			get
			{
				return EWSSettings.SafeGet<bool?>("WS_ItemHasBlockedImages", null);
			}
			set
			{
				EWSSettings.SafeSet<bool?>("WS_ItemHasBlockedImages", value);
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00006A7D File Offset: 0x00004C7D
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00006A8A File Offset: 0x00004C8A
		internal static JunkEmailRule JunkEmailRule
		{
			get
			{
				return EWSSettings.SafeGet<JunkEmailRule>("WS_JunkEmailRuleKey", null);
			}
			set
			{
				EWSSettings.SafeSet<JunkEmailRule>("WS_JunkEmailRuleKey", value);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00006A98 File Offset: 0x00004C98
		internal static IDictionary<string, bool> InlineImagesInUniqueBody
		{
			get
			{
				IDictionary<string, bool> dictionary = EWSSettings.SafeGet<IDictionary<string, bool>>("WS_InlineImageIdsToUniqueBody", null);
				if (dictionary == null)
				{
					dictionary = new Dictionary<string, bool>();
					EWSSettings.SafeSet<IDictionary<string, bool>>("WS_InlineImageIdsToUniqueBody", dictionary);
				}
				return dictionary;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00006AC8 File Offset: 0x00004CC8
		internal static IDictionary<string, bool> InlineImagesInNormalizedBody
		{
			get
			{
				IDictionary<string, bool> dictionary = EWSSettings.SafeGet<IDictionary<string, bool>>("WS_InlineImageIdsToNormalizedBody", null);
				if (dictionary == null)
				{
					dictionary = new Dictionary<string, bool>();
					EWSSettings.SafeSet<IDictionary<string, bool>>("WS_InlineImageIdsToNormalizedBody", dictionary);
				}
				return dictionary;
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00006AF8 File Offset: 0x00004CF8
		internal static void SetInlineAttachmentFlags(ItemType item)
		{
			if (item != null && item.Attachments != null)
			{
				foreach (Microsoft.Exchange.Services.Core.Types.AttachmentType attachmentType in item.Attachments)
				{
					attachmentType.IsInlineToUniqueBody = EWSSettings.InlineImagesInUniqueBody.ContainsKey(attachmentType.AttachmentId.Id);
					attachmentType.IsInlineToNormalBody = EWSSettings.InlineImagesInNormalizedBody.ContainsKey(attachmentType.AttachmentId.Id);
					if ((!string.IsNullOrEmpty(attachmentType.ContentType) && "audio/wav".Contains(attachmentType.ContentType.ToLowerInvariant())) || attachmentType is ReferenceAttachmentType)
					{
						attachmentType.IsInline = false;
					}
					else
					{
						attachmentType.IsInline = (attachmentType.IsInlineToUniqueBody || attachmentType.IsInlineToNormalBody || attachmentType.IsInline);
					}
					item.HasAttachments |= !attachmentType.IsInline;
				}
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00006BE4 File Offset: 0x00004DE4
		private static bool GetOWARegistryValue(string valueName, bool defaultValue)
		{
			bool result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(EWSSettings.MSExchangeOWARegistryPath, false))
				{
					object value = registryKey.GetValue(valueName);
					if (value == null || !(value is int))
					{
						result = defaultValue;
					}
					else
					{
						result = ((int)value != 0);
					}
				}
			}
			catch (SecurityException)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<string, bool>(0L, "[EWSSettings::GetOWARegistryValue] Security exception encountered while retrieving {0} registry value.  Defaulting to {1}", valueName, defaultValue);
				result = defaultValue;
			}
			catch (UnauthorizedAccessException)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<string, bool>(0L, "[EWSSettings::allowInternalUntrustedCerts delegate] Security exception encountered while retrieving {0} registry value.  Defaulting to {1}", valueName, defaultValue);
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00006C8C File Offset: 0x00004E8C
		internal static EwsOperationContextBase GetOperationContext()
		{
			EwsOperationContextBase ewsOperationContextBase = null;
			if (EwsOperationContextBase.Current != null)
			{
				ewsOperationContextBase = EwsOperationContextBase.Current;
			}
			else
			{
				CallContext callContext = CallContext.Current;
				if (callContext != null)
				{
					ewsOperationContextBase = callContext.OperationContext;
				}
			}
			if (ewsOperationContextBase == null)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug(0L, "EWSSettings.GetOperationContext() OperationContext is null.");
			}
			return ewsOperationContextBase;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00006CD0 File Offset: 0x00004ED0
		internal static HttpContext GetHttpContext()
		{
			HttpContext httpContext = null;
			if (HttpContext.Current != null)
			{
				httpContext = HttpContext.Current;
			}
			else
			{
				CallContext callContext = CallContext.Current;
				if (callContext != null)
				{
					httpContext = callContext.HttpContext;
				}
			}
			if (httpContext == null)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug(0L, "EWSSettings.GetHttpContext() HttpContext is null.");
			}
			return httpContext;
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00006D13 File Offset: 0x00004F13
		public static bool AllowInternalUntrustedCerts
		{
			get
			{
				return EWSSettings.allowInternalUntrustedCerts.Member;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00006D1F File Offset: 0x00004F1F
		public static bool AllowProxyingWithoutSSL
		{
			get
			{
				return EWSSettings.allowProxyingWithoutSSL.Member;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00006D2B File Offset: 0x00004F2B
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00006D38 File Offset: 0x00004F38
		public static CultureInfo ClientCulture
		{
			get
			{
				return EWSSettings.SafeGet<CultureInfo>("WS_ClientCultureKey", null);
			}
			set
			{
				EWSSettings.SafeSet<CultureInfo>("WS_ClientCultureKey", value);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00006D45 File Offset: 0x00004F45
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00006D52 File Offset: 0x00004F52
		public static CultureInfo ServerCulture
		{
			get
			{
				return EWSSettings.SafeGet<CultureInfo>("WS_ServerCultureKey", null);
			}
			set
			{
				EWSSettings.SafeSet<CultureInfo>("WS_ServerCultureKey", value);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00006D60 File Offset: 0x00004F60
		internal static ParticipantInformationDictionary ParticipantInformation
		{
			get
			{
				ParticipantInformationDictionary participantInformationDictionary = EWSSettings.SafeGet<ParticipantInformationDictionary>("WS_ParticipantInformation", null);
				if (participantInformationDictionary == null)
				{
					participantInformationDictionary = new ParticipantInformationDictionary();
					EWSSettings.SafeSet<ParticipantInformationDictionary>("WS_ParticipantInformation", participantInformationDictionary);
				}
				return participantInformationDictionary;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00006D8E File Offset: 0x00004F8E
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00006D9B File Offset: 0x00004F9B
		internal static DistinguishedFolderIdNameDictionary DistinguishedFolderIdNameDictionary
		{
			get
			{
				return EWSSettings.SafeGet<DistinguishedFolderIdNameDictionary>("WS_DistinguishedFolderIdNameDictionary", null);
			}
			set
			{
				EWSSettings.SafeSet<DistinguishedFolderIdNameDictionary>("WS_DistinguishedFolderIdNameDictionary", value);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00006DA8 File Offset: 0x00004FA8
		// (set) Token: 0x06000154 RID: 340 RVA: 0x00006DB5 File Offset: 0x00004FB5
		internal static Dictionary<AttachmentId, Microsoft.Exchange.Services.Core.Types.AttachmentType> AttachmentInformation
		{
			get
			{
				return EWSSettings.SafeGet<Dictionary<AttachmentId, Microsoft.Exchange.Services.Core.Types.AttachmentType>>("WS_AttachmentInformation", null);
			}
			set
			{
				EWSSettings.SafeSet<Dictionary<AttachmentId, Microsoft.Exchange.Services.Core.Types.AttachmentType>>("WS_AttachmentInformation", value);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00006DC2 File Offset: 0x00004FC2
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00006DCF File Offset: 0x00004FCF
		internal static ICoreConversation CurrentConversation
		{
			get
			{
				return EWSSettings.SafeGet<ICoreConversation>("WS_CurrentConversationKey", null);
			}
			set
			{
				EWSSettings.SafeSet<ICoreConversation>("WS_CurrentConversationKey", value);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00006DDC File Offset: 0x00004FDC
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00006DE9 File Offset: 0x00004FE9
		internal static bool CreateItemWithAttachments
		{
			get
			{
				return EWSSettings.SafeGet<bool>("WS_CreateItemWithAttachments", false);
			}
			set
			{
				EWSSettings.SafeSet<bool>("WS_CreateItemWithAttachments", value);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00006DF6 File Offset: 0x00004FF6
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00006E03 File Offset: 0x00005003
		internal static int AttachmentNestLevel
		{
			get
			{
				return EWSSettings.SafeGet<int>("WS_AttachmentNestLevelKey", 0);
			}
			set
			{
				EWSSettings.SafeSet<int>("WS_AttachmentNestLevelKey", value);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00006E10 File Offset: 0x00005010
		public static PostSavePropertyCollection PostSavePropertyCommands
		{
			get
			{
				PostSavePropertyCollection postSavePropertyCollection = EWSSettings.SafeGet<PostSavePropertyCollection>("WS_PostSaveProperties", null);
				if (postSavePropertyCollection == null)
				{
					postSavePropertyCollection = new PostSavePropertyCollection();
					EWSSettings.SafeSet<PostSavePropertyCollection>("WS_PostSaveProperties", postSavePropertyCollection);
				}
				return postSavePropertyCollection;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00006E3E File Offset: 0x0000503E
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00006E4F File Offset: 0x0000504F
		public static ExTimeZone RequestTimeZone
		{
			get
			{
				return EWSSettings.SafeGet<ExTimeZone>("WS_RequestTimeZoneKey", ExTimeZone.UtcTimeZone);
			}
			set
			{
				EWSSettings.SafeSet<ExTimeZone>("WS_RequestTimeZoneKey", value);
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00006E5C File Offset: 0x0000505C
		public static bool UpdateSessionTimeZoneFromRequestSoapHeader(StoreSession session)
		{
			ExTimeZone exTimeZone = EWSSettings.SafeGet<ExTimeZone>("WS_RequestTimeZoneKey", null);
			if (exTimeZone != null)
			{
				session.ExTimeZone = exTimeZone;
				return true;
			}
			return false;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00006E84 File Offset: 0x00005084
		public static ExTimeZone DefaultGmtTimeZone
		{
			get
			{
				if (EWSSettings.gmtTimeZone == null && !ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName("Greenwich Standard Time", out EWSSettings.gmtTimeZone))
				{
					ExTimeZoneInformation exTimeZoneInformation = new ExTimeZoneInformation("Greenwich Standard Time", "Greenwich Standard Time");
					ExTimeZoneRuleGroup exTimeZoneRuleGroup = new ExTimeZoneRuleGroup(null);
					ExTimeZoneRule ruleInfo = new ExTimeZoneRule("Standard", "Standard", new TimeSpan(0L), null);
					exTimeZoneRuleGroup.AddRule(ruleInfo);
					exTimeZoneInformation.AddGroup(exTimeZoneRuleGroup);
					EWSSettings.gmtTimeZone = new ExTimeZone(exTimeZoneInformation);
				}
				return EWSSettings.gmtTimeZone;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00006F03 File Offset: 0x00005103
		// (set) Token: 0x06000161 RID: 353 RVA: 0x00006F10 File Offset: 0x00005110
		public static DateTimePrecision DateTimePrecision
		{
			get
			{
				return EWSSettings.SafeGet<DateTimePrecision>("WS_DateTimePrecisionKey", DateTimePrecision.Seconds);
			}
			set
			{
				EWSSettings.SafeSet<DateTimePrecision>("WS_DateTimePrecisionKey", value);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00006F1D File Offset: 0x0000511D
		internal static string SimpleAssemblyName
		{
			get
			{
				return EWSSettings.simpleAssemblyName.Member;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00006F29 File Offset: 0x00005129
		internal static bool IsPartnerHostedOnly
		{
			get
			{
				return EWSSettings.isPartnerHostedOnly.Member;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00006F35 File Offset: 0x00005135
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00006F3C File Offset: 0x0000513C
		internal static List<string> OtherSimpleAssemblyNames { get; set; }

		// Token: 0x06000166 RID: 358 RVA: 0x00006F44 File Offset: 0x00005144
		internal static bool IsFromEwsAssemblies(string source)
		{
			return source == EWSSettings.SimpleAssemblyName || (EWSSettings.OtherSimpleAssemblyNames != null && EWSSettings.OtherSimpleAssemblyNames.Contains(source));
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00006F69 File Offset: 0x00005169
		internal static bool IsWsSecurityAddress(Uri uri)
		{
			return uri.LocalPath.EndsWith("wssecurity", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00006F7C File Offset: 0x0000517C
		internal static bool IsWsSecuritySymmetricKeyAddress(Uri uri)
		{
			return uri.LocalPath.EndsWith("wssecurity/symmetrickey", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00006F8F File Offset: 0x0000518F
		internal static bool IsWsSecurityX509CertAddress(Uri uri)
		{
			return uri.LocalPath.EndsWith("wssecurity/x509cert", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00006FA2 File Offset: 0x000051A2
		internal static double WcfDispatchLatency
		{
			get
			{
				return EWSSettings.SafeGetMessageProperty<double>("WcfLatency", 0.0);
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00006FB8 File Offset: 0x000051B8
		private static void CheckWsSecurityEndpointsStatus()
		{
			EWSSettings.isWsSecurityEndpointEnabled = new bool?(false);
			EWSSettings.isWsSecuritySymmetricKeyEndpointEnabled = new bool?(false);
			EWSSettings.isWsSecurityX509CertEndpointEnabled = new bool?(false);
			EWSSettings.isOAuthEndpointEnabled = new bool?(OAuthHttpModule.IsModuleLoaded.Value);
			Configuration config = WebConfigurationManager.OpenWebConfiguration("~/web.config");
			ServiceElementCollection services = ServiceModelSectionGroup.GetSectionGroup(config).Services.Services;
			foreach (object obj in services)
			{
				ServiceElement serviceElement = (ServiceElement)obj;
				foreach (object obj2 in serviceElement.Endpoints)
				{
					ServiceEndpointElement serviceEndpointElement = (ServiceEndpointElement)obj2;
					if (!string.IsNullOrEmpty(serviceEndpointElement.BindingConfiguration) && (serviceEndpointElement.Contract.Equals("Microsoft.Exchange.Services.Wcf.IEWSContract", StringComparison.OrdinalIgnoreCase) || serviceEndpointElement.Contract.Equals("Microsoft.Exchange.Services.Wcf.IEWSStreamingContract", StringComparison.OrdinalIgnoreCase)))
					{
						if (serviceEndpointElement.Address.OriginalString.Equals("wssecurity", StringComparison.OrdinalIgnoreCase))
						{
							EWSSettings.isWsSecurityEndpointEnabled = new bool?(true);
						}
						else if (serviceEndpointElement.Address.OriginalString.Equals("wssecurity/symmetrickey", StringComparison.OrdinalIgnoreCase))
						{
							EWSSettings.isWsSecuritySymmetricKeyEndpointEnabled = new bool?(true);
						}
						else if (serviceEndpointElement.Address.OriginalString.Equals("wssecurity/x509cert", StringComparison.OrdinalIgnoreCase))
						{
							EWSSettings.isWsSecurityX509CertEndpointEnabled = new bool?(true);
						}
					}
				}
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000716C File Offset: 0x0000536C
		private static ServiceEndpointElement TryFindEndpointByNameAndContract(ServiceEndpointElementCollection endpoints, string name, string contract)
		{
			if (endpoints != null)
			{
				foreach (object obj in endpoints)
				{
					ServiceEndpointElement serviceEndpointElement = (ServiceEndpointElement)obj;
					if (serviceEndpointElement.Name == name && serviceEndpointElement.Contract == contract)
					{
						return serviceEndpointElement;
					}
				}
			}
			return null;
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600016D RID: 365 RVA: 0x000071E0 File Offset: 0x000053E0
		// (set) Token: 0x0600016E RID: 366 RVA: 0x000071F1 File Offset: 0x000053F1
		internal static BaseResponseRenderer ResponseRenderer
		{
			get
			{
				return EWSSettings.SafeGetMessageProperty<BaseResponseRenderer>("WS_ResponseRenderer", SoapWcfResponseRenderer.Singleton);
			}
			set
			{
				EWSSettings.SafeSetMessageProperty<BaseResponseRenderer>("WS_ResponseRenderer", value ?? SoapWcfResponseRenderer.Singleton);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00007207 File Offset: 0x00005407
		// (set) Token: 0x06000170 RID: 368 RVA: 0x00007214 File Offset: 0x00005414
		internal static Stream MessageStream
		{
			get
			{
				return EWSSettings.SafeGetMessageProperty<Stream>("MessageStream", null);
			}
			set
			{
				EWSSettings.SafeSetMessageProperty<Stream>("MessageStream", value);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00007221 File Offset: 0x00005421
		internal static bool IsMultiTenancyEnabled
		{
			get
			{
				return EWSSettings.isMultiTenancyEnabled.Member;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000722D File Offset: 0x0000542D
		internal static bool IsLinkedAccountTokenMungingEnabled
		{
			get
			{
				return EWSSettings.isLinkedAccountTokenMungingEnabled.Member;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00007239 File Offset: 0x00005439
		internal static bool IsWsPerformanceCountersEnabled
		{
			get
			{
				return EWSSettings.isWsPerformanceCountersEnabled.Member;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00007245 File Offset: 0x00005445
		public static Guid SelfSiteGuid
		{
			get
			{
				return EWSSettings.selfSiteGuid.Member;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00007251 File Offset: 0x00005451
		internal static bool IsWsSecurityEndpointEnabled
		{
			get
			{
				if (EWSSettings.isWsSecurityEndpointEnabled == null)
				{
					EWSSettings.CheckWsSecurityEndpointsStatus();
				}
				return EWSSettings.isWsSecurityEndpointEnabled.Value;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000176 RID: 374 RVA: 0x0000726E File Offset: 0x0000546E
		internal static bool IsWsSecuritySymmetricKeyEndpointEnabled
		{
			get
			{
				if (EWSSettings.isWsSecuritySymmetricKeyEndpointEnabled == null)
				{
					EWSSettings.CheckWsSecurityEndpointsStatus();
				}
				return EWSSettings.isWsSecuritySymmetricKeyEndpointEnabled.Value;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000177 RID: 375 RVA: 0x0000728B File Offset: 0x0000548B
		internal static bool IsWsSecurityX509CertEndpointEnabled
		{
			get
			{
				if (EWSSettings.isWsSecurityX509CertEndpointEnabled == null)
				{
					EWSSettings.CheckWsSecurityEndpointsStatus();
				}
				return EWSSettings.isWsSecurityX509CertEndpointEnabled.Value;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000178 RID: 376 RVA: 0x000072A8 File Offset: 0x000054A8
		internal static bool IsOAuthEndpointEnabled
		{
			get
			{
				if (EWSSettings.isOAuthEndpointEnabled == null)
				{
					EWSSettings.CheckWsSecurityEndpointsStatus();
				}
				return EWSSettings.isOAuthEndpointEnabled.Value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000179 RID: 377 RVA: 0x000072C5 File Offset: 0x000054C5
		internal static bool AreGccStoredSecretKeysValid
		{
			get
			{
				return EWSSettings.areGccStoredSecretKeysValid.Member;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600017A RID: 378 RVA: 0x000072D1 File Offset: 0x000054D1
		internal static bool DisableReferenceAttachment
		{
			get
			{
				if (EWSSettings.disableReferenceAttachment == null)
				{
					EWSSettings.disableReferenceAttachment = new bool?(Global.GetAppSettingAsBool("DisableReferenceAttachment", false));
				}
				return EWSSettings.disableReferenceAttachment.Value;
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000072FE File Offset: 0x000054FE
		internal static void SetOutgoingHttpStatusCode(HttpStatusCode statusCode)
		{
			EWSSettings.ResponseRenderer = SoapWcfResponseRenderer.Create(statusCode);
		}

		// Token: 0x040000F2 RID: 242
		private const string ClientCultureKey = "WS_ClientCultureKey";

		// Token: 0x040000F3 RID: 243
		private const string ServerCultureKey = "WS_ServerCultureKey";

		// Token: 0x040000F4 RID: 244
		private const string RequestTimeZoneKey = "WS_RequestTimeZoneKey";

		// Token: 0x040000F5 RID: 245
		private const string DateTimePrecisionKey = "WS_DateTimePrecisionKey";

		// Token: 0x040000F6 RID: 246
		private const string ProxyMessageCopyKey = "WS_ProxyMessageCopy";

		// Token: 0x040000F7 RID: 247
		private const string UpnFromClaimSetsKey = "WS_UpnFromClaimSets";

		// Token: 0x040000F8 RID: 248
		private const string FaultExceptionDueToAuthorizationManagerKey = "WS_FaultExceptionAuthZMgr";

		// Token: 0x040000F9 RID: 249
		private const string ProxyHopHeadersKey = "WS_ProxyHopHeaders";

		// Token: 0x040000FA RID: 250
		private const string ProxyResponseKey = "WS_ProxyResponse";

		// Token: 0x040000FB RID: 251
		private const string FailoverTypeKey = "WS_FailoverType";

		// Token: 0x040000FC RID: 252
		private const string ExceptionTypeKey = "WS_ExceptionType";

		// Token: 0x040000FD RID: 253
		private const string ParticipantInformationKey = "WS_ParticipantInformation";

		// Token: 0x040000FE RID: 254
		private const string DistinguishedFolderIdNameDictionaryKey = "WS_DistinguishedFolderIdNameDictionary";

		// Token: 0x040000FF RID: 255
		private const string AttachmentInformationKey = "WS_AttachmentInformation";

		// Token: 0x04000100 RID: 256
		private const string CurrentConversationKey = "WS_CurrentConversationKey";

		// Token: 0x04000101 RID: 257
		private const string CreateItemWithAttachmentsKey = "WS_CreateItemWithAttachments";

		// Token: 0x04000102 RID: 258
		private const string AttachmentNestLevelKey = "WS_AttachmentNestLevelKey";

		// Token: 0x04000103 RID: 259
		private const string InWCFChannelLayerKey = "WS_InWCFChannelLayer";

		// Token: 0x04000104 RID: 260
		private const string PostSavePropertiesKey = "WS_PostSaveProperties";

		// Token: 0x04000105 RID: 261
		private const string WebMethodEntryKey = "WS_WebMethodEntry";

		// Token: 0x04000106 RID: 262
		private const string ItemHasBlockedImagesKey = "WS_ItemHasBlockedImages";

		// Token: 0x04000107 RID: 263
		private const string JunkEmailRuleKey = "WS_JunkEmailRuleKey";

		// Token: 0x04000108 RID: 264
		private const string InlineImageIdsToUniqueBodyKey = "WS_InlineImageIdsToUniqueBody";

		// Token: 0x04000109 RID: 265
		private const string InlineImageIdsToNormalizedBodyKey = "WS_InlineImageIdsToNormalizedBody";

		// Token: 0x0400010A RID: 266
		public const string EWSAnonymousHttpsBindingName = "EWSAnonymousHttpsBinding";

		// Token: 0x0400010B RID: 267
		public const string EWSAnonymousHttpBindingName = "EWSAnonymousHttpBinding";

		// Token: 0x0400010C RID: 268
		public const string EWSBasicHttpsBindingName = "EWSBasicHttpsBinding";

		// Token: 0x0400010D RID: 269
		public const string EWSBasicHttpBindingName = "EWSBasicHttpBinding";

		// Token: 0x0400010E RID: 270
		public const string EWSNegotiateHttpsBindingName = "EWSNegotiateHttpsBinding";

		// Token: 0x0400010F RID: 271
		public const string EWSNegotiateHttpBindingName = "EWSNegotiateHttpBinding";

		// Token: 0x04000110 RID: 272
		public const string EWSWSSecurityHttpsBindingName = "EWSWSSecurityHttpsBinding";

		// Token: 0x04000111 RID: 273
		public const string EWSWSSecurityHttpBindingName = "EWSWSSecurityHttpBinding";

		// Token: 0x04000112 RID: 274
		public const string EWSStreamingNegotiateHttpsBindingName = "EWSStreamingNegotiateHttpsBinding";

		// Token: 0x04000113 RID: 275
		public const string EWSStreamingNegotiateHttpBindingName = "EWSStreamingNegotiateHttpBinding";

		// Token: 0x04000114 RID: 276
		public const string ServiceName = "Microsoft.Exchange.Services.Wcf.EWSService";

		// Token: 0x04000115 RID: 277
		public const string Contract = "Microsoft.Exchange.Services.Wcf.IEWSContract";

		// Token: 0x04000116 RID: 278
		public const string StreamingContract = "Microsoft.Exchange.Services.Wcf.IEWSStreamingContract";

		// Token: 0x04000117 RID: 279
		public const string WsSecurityAddress = "wssecurity";

		// Token: 0x04000118 RID: 280
		public const string WsSecuritySymmetricKeyAddress = "wssecurity/symmetrickey";

		// Token: 0x04000119 RID: 281
		public const string WsSecurityX509CertAddress = "wssecurity/x509cert";

		// Token: 0x0400011A RID: 282
		public const string EndpointNameHttps = "Https";

		// Token: 0x0400011B RID: 283
		public const string RequestCorrelationKey = "WS_RequestCorrelationKey";

		// Token: 0x0400011C RID: 284
		public const string RequestThreadIdKey = "WS_RequestThreadIdKey";

		// Token: 0x0400011D RID: 285
		public const string WcfDelayedExceptionKey = "WS_WcfDelayedExceptionKey";

		// Token: 0x0400011E RID: 286
		private const string inlineAsAttachedContentTypes = "audio/wav";

		// Token: 0x0400011F RID: 287
		private const string ResponseRendererKey = "WS_ResponseRenderer";

		// Token: 0x04000120 RID: 288
		private const string AppSettingDisableReferenceAttachment = "DisableReferenceAttachment";

		// Token: 0x04000121 RID: 289
		internal const string SelfSiteIdKey = "WS_SetSiteIdKey";

		// Token: 0x04000122 RID: 290
		private static readonly string MSExchangeOWARegistryPath = "SYSTEM\\CurrentControlSet\\Services\\MSExchange OWA";

		// Token: 0x04000123 RID: 291
		private static readonly bool AllowInternalUntrustedCertsDefault = true;

		// Token: 0x04000124 RID: 292
		private static readonly string AllowInternalUntrustedCertsValueName = "AllowInternalUntrustedCerts";

		// Token: 0x04000125 RID: 293
		private static readonly bool AllowProxyingWithoutSSLDefault = false;

		// Token: 0x04000126 RID: 294
		private static readonly string AllowProxyingWithoutSSLValueName = "AllowProxyingWithoutSSL";

		// Token: 0x04000127 RID: 295
		private static readonly string WritingToWireKey = "WS_WritingToWireKey";

		// Token: 0x04000128 RID: 296
		private static LazyMember<bool> allowInternalUntrustedCerts = new LazyMember<bool>(() => EWSSettings.GetOWARegistryValue(EWSSettings.AllowInternalUntrustedCertsValueName, EWSSettings.AllowInternalUntrustedCertsDefault));

		// Token: 0x04000129 RID: 297
		private static LazyMember<bool> allowProxyingWithoutSSL = new LazyMember<bool>(() => EWSSettings.GetOWARegistryValue(EWSSettings.AllowProxyingWithoutSSLValueName, EWSSettings.AllowProxyingWithoutSSLDefault));

		// Token: 0x0400012A RID: 298
		private static LazyMember<string> simpleAssemblyName = new LazyMember<string>(delegate()
		{
			AssemblyName assemblyName = new AssemblyName(typeof(EWSSettings).GetTypeInfo().Assembly.FullName);
			return assemblyName.Name;
		});

		// Token: 0x0400012B RID: 299
		private static LazyMember<bool> isPartnerHostedOnly = new LazyMember<bool>(delegate()
		{
			try
			{
				if (Datacenter.IsPartnerHostedOnly(true))
				{
					return true;
				}
			}
			catch (CannotDetermineExchangeModeException)
			{
			}
			return false;
		});

		// Token: 0x0400012C RID: 300
		private static ExTimeZone gmtTimeZone = null;

		// Token: 0x0400012D RID: 301
		private static LazyMember<Guid> selfSiteGuid = new LazyMember<Guid>(() => LocalServer.GetServer().ServerSite.ObjectGuid);

		// Token: 0x0400012E RID: 302
		private static LazyMember<bool> isLinkedAccountTokenMungingEnabled = new LazyMember<bool>(() => VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Ews.LinkedAccountTokenMunging.Enabled);

		// Token: 0x0400012F RID: 303
		private static LazyMember<bool> isMultiTenancyEnabled = new LazyMember<bool>(() => VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled);

		// Token: 0x04000130 RID: 304
		private static LazyMember<bool> isWsPerformanceCountersEnabled = new LazyMember<bool>(() => VariantConfiguration.InvariantNoFlightingSnapshot.Ews.WsPerformanceCounters.Enabled);

		// Token: 0x04000131 RID: 305
		private static bool? isWsSecurityEndpointEnabled;

		// Token: 0x04000132 RID: 306
		private static bool? isWsSecuritySymmetricKeyEndpointEnabled;

		// Token: 0x04000133 RID: 307
		private static bool? isWsSecurityX509CertEndpointEnabled;

		// Token: 0x04000134 RID: 308
		private static bool? isOAuthEndpointEnabled;

		// Token: 0x04000135 RID: 309
		private static bool? disableReferenceAttachment;

		// Token: 0x04000136 RID: 310
		private static LazyMember<bool> areGccStoredSecretKeysValid = new LazyMember<bool>(() => GccUtils.AreStoredSecretKeysValid());
	}
}
