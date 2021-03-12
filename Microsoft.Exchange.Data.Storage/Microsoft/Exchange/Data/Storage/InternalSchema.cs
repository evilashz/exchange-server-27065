using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.NaturalLanguage;
using Microsoft.Exchange.Data.Storage.ReliableActions;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C6B RID: 3179
	[ClassAccessLevel(Microsoft.Exchange.Diagnostics.AccessLevel.Implementation)]
	internal static class InternalSchema
	{
		// Token: 0x06006FD6 RID: 28630 RVA: 0x001E1750 File Offset: 0x001DF950
		internal static string ToPropertyDefinitionString(PropertyDefinition propertyDefinition)
		{
			if (propertyDefinition == null)
			{
				return "<unknown>:<unknown>";
			}
			return propertyDefinition.Name + ":" + propertyDefinition.Type.ToString();
		}

		// Token: 0x06006FD7 RID: 28631 RVA: 0x001E1778 File Offset: 0x001DF978
		internal static StorePropertyDefinition ToStorePropertyDefinition(PropertyDefinition propertyDefinition)
		{
			if (propertyDefinition == null)
			{
				throw new ArgumentNullException("propertyDefinition");
			}
			StorePropertyDefinition storePropertyDefinition = propertyDefinition as StorePropertyDefinition;
			if (storePropertyDefinition != null)
			{
				return storePropertyDefinition;
			}
			throw new InvalidOperationException(ServerStrings.ExNoMatchingStorePropertyDefinition(InternalSchema.ToPropertyDefinitionString(propertyDefinition)));
		}

		// Token: 0x06006FD8 RID: 28632 RVA: 0x001E17B4 File Offset: 0x001DF9B4
		internal static StorePropertyDefinition[] ToStorePropertyDefinitions(ICollection<PropertyDefinition> propertyDefinitions)
		{
			int count = propertyDefinitions.Count;
			StorePropertyDefinition[] array = new StorePropertyDefinition[count];
			int num = 0;
			foreach (PropertyDefinition propertyDefinition in propertyDefinitions)
			{
				array[num++] = InternalSchema.ToStorePropertyDefinition(propertyDefinition);
			}
			return array;
		}

		// Token: 0x06006FD9 RID: 28633 RVA: 0x001E1818 File Offset: 0x001DFA18
		internal static NativeStorePropertyDefinition[] RemoveNullPropertyDefinions(NativeStorePropertyDefinition[] definitions, out bool changed)
		{
			List<NativeStorePropertyDefinition> list = new List<NativeStorePropertyDefinition>(definitions.Length);
			foreach (NativeStorePropertyDefinition nativeStorePropertyDefinition in definitions)
			{
				if (nativeStorePropertyDefinition != null)
				{
					list.Add(nativeStorePropertyDefinition);
				}
			}
			changed = (list.Count != definitions.Length);
			return list.ToArray();
		}

		// Token: 0x06006FDA RID: 28634 RVA: 0x001E1864 File Offset: 0x001DFA64
		internal static PropType PropTagTypeFromClrType(Type type)
		{
			if (typeof(short) == type)
			{
				return PropType.Short;
			}
			if (typeof(int) == type)
			{
				return PropType.Int;
			}
			if (typeof(long) == type)
			{
				return PropType.Long;
			}
			if (typeof(float) == type)
			{
				return PropType.Float;
			}
			if (typeof(double) == type)
			{
				return PropType.Double;
			}
			if (typeof(ExDateTime) == type)
			{
				return PropType.SysTime;
			}
			if (typeof(bool) == type)
			{
				return PropType.Boolean;
			}
			if (typeof(string) == type)
			{
				return PropType.String;
			}
			if (typeof(Guid) == type)
			{
				return PropType.Guid;
			}
			if (typeof(byte[]) == type)
			{
				return PropType.Binary;
			}
			if (typeof(string[]) == type)
			{
				return PropType.StringArray;
			}
			if (typeof(byte[][]) == type)
			{
				return PropType.BinaryArray;
			}
			if (typeof(short[]) == type)
			{
				return PropType.ShortArray;
			}
			if (typeof(int[]) == type)
			{
				return PropType.IntArray;
			}
			if (typeof(float[]) == type)
			{
				return PropType.FloatArray;
			}
			if (typeof(double[]) == type)
			{
				return PropType.DoubleArray;
			}
			if (typeof(long[]) == type)
			{
				return PropType.LongArray;
			}
			if (typeof(ExDateTime[]) == type)
			{
				return PropType.SysTimeArray;
			}
			if (typeof(Guid[]) == type)
			{
				return PropType.GuidArray;
			}
			throw new ArgumentException(ServerStrings.ExUnsupportedMapiType(type));
		}

		// Token: 0x06006FDB RID: 28635 RVA: 0x001E1A2A File Offset: 0x001DFC2A
		internal static Type ClrTypeFromPropTag(PropTag propTag)
		{
			return InternalSchema.ClrTypeFromPropTagType(propTag.ValueType());
		}

		// Token: 0x06006FDC RID: 28636 RVA: 0x001E1A38 File Offset: 0x001DFC38
		internal static Type ClrTypeFromPropTagType(PropType mapiType)
		{
			if (mapiType <= PropType.Binary)
			{
				if (mapiType <= PropType.SysTime)
				{
					switch (mapiType)
					{
					case PropType.Null:
						return typeof(int);
					case PropType.Short:
						return typeof(short);
					case PropType.Int:
						return typeof(int);
					case PropType.Float:
						return typeof(float);
					case PropType.Double:
						return typeof(double);
					case PropType.Currency:
						return typeof(long);
					case PropType.AppTime:
						return typeof(double);
					case (PropType)8:
					case (PropType)9:
					case (PropType)12:
					case (PropType)14:
					case (PropType)15:
					case (PropType)16:
					case (PropType)17:
					case (PropType)18:
					case (PropType)19:
						break;
					case PropType.Error:
						return typeof(int);
					case PropType.Boolean:
						return typeof(bool);
					case PropType.Object:
						return typeof(object);
					case PropType.Long:
						return typeof(long);
					default:
						switch (mapiType)
						{
						case PropType.AnsiString:
							return typeof(string);
						case PropType.String:
							return typeof(string);
						default:
							if (mapiType == PropType.SysTime)
							{
								return typeof(ExDateTime);
							}
							break;
						}
						break;
					}
				}
				else
				{
					if (mapiType == PropType.Guid)
					{
						return typeof(Guid);
					}
					switch (mapiType)
					{
					case PropType.Restriction:
						return typeof(QueryFilter);
					case PropType.Actions:
						return typeof(RuleAction[]);
					default:
						if (mapiType == PropType.Binary)
						{
							return typeof(byte[]);
						}
						break;
					}
				}
			}
			else if (mapiType <= PropType.StringArray)
			{
				switch (mapiType)
				{
				case PropType.ShortArray:
					return typeof(short[]);
				case PropType.IntArray:
					return typeof(int[]);
				case PropType.FloatArray:
					return typeof(float[]);
				case PropType.DoubleArray:
					return typeof(double[]);
				case PropType.CurrencyArray:
					return typeof(long[]);
				case PropType.AppTimeArray:
					return typeof(double[]);
				case (PropType)4104:
				case (PropType)4105:
				case (PropType)4106:
				case (PropType)4107:
				case (PropType)4108:
					break;
				case PropType.ObjectArray:
					return typeof(object[]);
				default:
					if (mapiType == PropType.LongArray)
					{
						return typeof(long[]);
					}
					switch (mapiType)
					{
					case PropType.AnsiStringArray:
						return typeof(string[]);
					case PropType.StringArray:
						return typeof(string[]);
					}
					break;
				}
			}
			else
			{
				if (mapiType == PropType.SysTimeArray)
				{
					return typeof(ExDateTime[]);
				}
				if (mapiType == PropType.GuidArray)
				{
					return typeof(Guid[]);
				}
				if (mapiType == PropType.BinaryArray)
				{
					return typeof(byte[][]);
				}
			}
			LocalizedString localizedString = ServerStrings.ExInvalidMAPIType((uint)mapiType);
			ExTraceGlobals.StorageTracer.TraceError(0L, localizedString);
			throw new InvalidPropertyTypeException(localizedString);
		}

		// Token: 0x06006FDD RID: 28637 RVA: 0x001E1CFC File Offset: 0x001DFEFC
		internal static void CheckPropertyValueType(PropertyDefinition propertyDefinition, object value)
		{
			try
			{
				if (!(value.GetType() == propertyDefinition.Type))
				{
					if (typeof(short) == propertyDefinition.Type)
					{
						InternalSchema.CheckValueType<short>(value);
					}
					else if (typeof(int) == propertyDefinition.Type)
					{
						InternalSchema.CheckValueType<int>(value);
					}
					else if (typeof(long) == propertyDefinition.Type)
					{
						InternalSchema.CheckValueType<long>(value);
					}
					else if (typeof(float) == propertyDefinition.Type)
					{
						InternalSchema.CheckValueType<float>(value);
					}
					else if (typeof(double) == propertyDefinition.Type)
					{
						InternalSchema.CheckValueType<double>(value);
					}
					else if (typeof(ExDateTime) == propertyDefinition.Type)
					{
						InternalSchema.CheckValueType<ExDateTime>(value);
					}
					else if (typeof(bool) == propertyDefinition.Type)
					{
						InternalSchema.CheckValueType<bool>(value);
					}
					else if (typeof(string) == propertyDefinition.Type)
					{
						InternalSchema.CheckValueType<string>(value);
					}
					else if (typeof(Guid) == propertyDefinition.Type)
					{
						InternalSchema.CheckValueType<Guid>(value);
					}
					else if (typeof(byte[]) == propertyDefinition.Type)
					{
						InternalSchema.CheckValueType<byte[]>(value);
					}
					else if (typeof(string[]) == propertyDefinition.Type)
					{
						InternalSchema.CheckValueType<string[]>(value);
					}
					else if (typeof(byte[][]) == propertyDefinition.Type)
					{
						InternalSchema.CheckValueType<byte[][]>(value);
					}
					else if (typeof(short[]) == propertyDefinition.Type)
					{
						InternalSchema.CheckValueType<short[]>(value);
					}
					else if (typeof(int[]) == propertyDefinition.Type)
					{
						InternalSchema.CheckValueType<int[]>(value);
					}
					else if (typeof(float[]) == propertyDefinition.Type)
					{
						InternalSchema.CheckValueType<float[]>(value);
					}
					else if (typeof(double[]) == propertyDefinition.Type)
					{
						InternalSchema.CheckValueType<double[]>(value);
					}
					else if (typeof(long[]) == propertyDefinition.Type)
					{
						InternalSchema.CheckValueType<long[]>(value);
					}
					else if (typeof(ExDateTime[]) == propertyDefinition.Type)
					{
						InternalSchema.CheckValueType<ExDateTime[]>(value);
					}
					else if (typeof(Guid[]) == propertyDefinition.Type)
					{
						InternalSchema.CheckValueType<Guid[]>(value);
					}
					else
					{
						if (!(typeof(QueryFilter) == propertyDefinition.Type))
						{
							string message = string.Format("Value of type {0} was passed for property {1}; should be type {2}.", value.GetType(), propertyDefinition, propertyDefinition.Type);
							throw new ArgumentException(message);
						}
						InternalSchema.CheckValueType<QueryFilter>(value);
					}
				}
			}
			catch (InvalidCastException innerException)
			{
				string message2 = string.Format("Value of type {0} was passed for property {1}; should be type {2}.", value.GetType(), propertyDefinition, propertyDefinition.Type);
				throw new ArgumentException(message2, innerException);
			}
		}

		// Token: 0x06006FDE RID: 28638 RVA: 0x001E201C File Offset: 0x001E021C
		private static void CheckValueType<T>(object value)
		{
			T t = (T)((object)value);
		}

		// Token: 0x06006FDF RID: 28639 RVA: 0x001E2025 File Offset: 0x001E0225
		internal static ICollection<T> Combine<T>(ICollection<T> first, ICollection<T> second) where T : PropertyDefinition
		{
			if (first == InternalSchema.ContentConversionPropertiesAsICollection)
			{
				return first;
			}
			if (second == InternalSchema.ContentConversionPropertiesAsICollection)
			{
				return second;
			}
			return first.Union(second);
		}

		// Token: 0x06006FE0 RID: 28640 RVA: 0x001E2042 File Offset: 0x001E0242
		private static GuidNamePropertyDefinition CreateGuidNameProperty(string displayName, Type propertyType, Guid propertyGuid, string propertyName = null)
		{
			return InternalSchema.CreateGuidNameProperty(displayName, propertyType, propertyGuid, propertyName ?? displayName, PropertyFlags.None, PropertyDefinitionConstraint.None);
		}

		// Token: 0x06006FE1 RID: 28641 RVA: 0x001E2058 File Offset: 0x001E0258
		private static GuidNamePropertyDefinition CreateGuidNameProperty(string displayName, Type propertyType, Guid propertyGuid, string propertyName, PropertyFlags propertyFlag, params PropertyDefinitionConstraint[] constraints)
		{
			PropType mapiPropType = InternalSchema.PropTagTypeFromClrType(propertyType);
			return GuidNamePropertyDefinition.InternalCreate(displayName, propertyType, mapiPropType, propertyGuid, propertyName, propertyFlag, NativeStorePropertyDefinition.TypeCheckingFlag.DisableTypeCheck, false, constraints);
		}

		// Token: 0x06006FE2 RID: 28642 RVA: 0x001E207C File Offset: 0x001E027C
		private static GuidIdPropertyDefinition CreateGuidIdProperty(string displayName, Type propertyType, Guid propertyGuid, int dispId)
		{
			return InternalSchema.CreateGuidIdProperty(displayName, propertyType, propertyGuid, dispId, PropertyFlags.None, PropertyDefinitionConstraint.None);
		}

		// Token: 0x06006FE3 RID: 28643 RVA: 0x001E2090 File Offset: 0x001E0290
		private static GuidIdPropertyDefinition CreateGuidIdProperty(string displayName, Type propertyType, Guid propertyGuid, int dispId, PropertyFlags propertyFlag, params PropertyDefinitionConstraint[] constraints)
		{
			PropType mapiPropType = InternalSchema.PropTagTypeFromClrType(propertyType);
			return GuidIdPropertyDefinition.InternalCreate(displayName, propertyType, mapiPropType, propertyGuid, dispId, propertyFlag, NativeStorePropertyDefinition.TypeCheckingFlag.DisableTypeCheck, false, constraints);
		}

		// Token: 0x040043CE RID: 17358
		private const int TaskWorkLimit = 1525252319;

		// Token: 0x040043CF RID: 17359
		internal const int MaxSubjectLength = 255;

		// Token: 0x040043D0 RID: 17360
		internal static readonly StorePropertyDefinition[] ContentConversionProperties = new StorePropertyDefinition[0];

		// Token: 0x040043D1 RID: 17361
		private static readonly ICollection<PropertyDefinition> ContentConversionPropertiesAsICollection = (ICollection<PropertyDefinition>)InternalSchema.ContentConversionProperties;

		// Token: 0x040043D2 RID: 17362
		public static readonly PropertyTagPropertyDefinition PropertyGroupChangeMask = PropertyTagPropertyDefinition.InternalCreate("PropertyGroupChangeMask", PropTag.PropertyGroupChangeMask);

		// Token: 0x040043D3 RID: 17363
		public static readonly PropertyTagPropertyDefinition PropertyGroupMappingId = PropertyTagPropertyDefinition.InternalCreate("PropertyGroupMappingId ", PropTag.LdapReads);

		// Token: 0x040043D4 RID: 17364
		public static readonly GuidNamePropertyDefinition PropertyExistenceTracker = InternalSchema.CreateGuidNameProperty("PropertyExistenceTracker", typeof(long), WellKnownPropertySet.Common, "PropertyExistenceTracker");

		// Token: 0x040043D5 RID: 17365
		public static readonly PropertyTagPropertyDefinition MailEnabled = PropertyTagPropertyDefinition.InternalCreate("MailEnabled", PropTag.PfProxyRequired);

		// Token: 0x040043D6 RID: 17366
		public static readonly PropertyTagPropertyDefinition ProxyGuid = PropertyTagPropertyDefinition.InternalCreate("ProxyGuid", PropTag.PfProxy);

		// Token: 0x040043D7 RID: 17367
		public static readonly PropertyTagPropertyDefinition LocalDirectory = PropertyTagPropertyDefinition.InternalCreate("LocalDirectory", PropTag.LocalDirectory);

		// Token: 0x040043D8 RID: 17368
		public static readonly PropertyTagPropertyDefinition MemberEmailLocalDirectory = PropertyTagPropertyDefinition.InternalCreate("MemberEmail", PropTag.MemberEmail);

		// Token: 0x040043D9 RID: 17369
		public static readonly PropertyTagPropertyDefinition MemberExternalIdLocalDirectory = PropertyTagPropertyDefinition.InternalCreate("MemberExternalId", PropTag.MemberExternalId);

		// Token: 0x040043DA RID: 17370
		public static readonly PropertyTagPropertyDefinition MemberSIDLocalDirectory = PropertyTagPropertyDefinition.InternalCreate("MemberSID", PropTag.MemberSID);

		// Token: 0x040043DB RID: 17371
		public static readonly PropertyTagPropertyDefinition SenderTelephoneNumber = PropertyTagPropertyDefinition.InternalCreate("SenderTelephoneNumber", (PropTag)1744961567U);

		// Token: 0x040043DC RID: 17372
		public static readonly PropertyTagPropertyDefinition RetentionAgeLimit = PropertyTagPropertyDefinition.InternalCreate("RetentionAgeLimit", PropTag.RetentionAgeLimit);

		// Token: 0x040043DD RID: 17373
		public static readonly PropertyTagPropertyDefinition OverallAgeLimit = PropertyTagPropertyDefinition.InternalCreate("OverallAgeLimit", PropTag.OverallAgeLimit);

		// Token: 0x040043DE RID: 17374
		public static readonly PropertyTagPropertyDefinition PfQuotaStyle = PropertyTagPropertyDefinition.InternalCreate("PfQuotaStyle", PropTag.PfQuotaStyle);

		// Token: 0x040043DF RID: 17375
		public static readonly PropertyTagPropertyDefinition PfOverHardQuotaLimit = PropertyTagPropertyDefinition.InternalCreate("PfOverHardQuotaLimit", PropTag.PfOverHardQuotaLimit);

		// Token: 0x040043E0 RID: 17376
		public static readonly PropertyTagPropertyDefinition PfStorageQuota = PropertyTagPropertyDefinition.InternalCreate("PfStorageQuota", PropTag.PfStorageQuota);

		// Token: 0x040043E1 RID: 17377
		public static readonly PropertyTagPropertyDefinition PfMsgSizeLimit = PropertyTagPropertyDefinition.InternalCreate("PfMsgSizeLimit", PropTag.PfMsgSizeLimit);

		// Token: 0x040043E2 RID: 17378
		public static readonly PropertyTagPropertyDefinition DisablePerUserRead = PropertyTagPropertyDefinition.InternalCreate("DisablePerUserRead", PropTag.DisablePeruserRead);

		// Token: 0x040043E3 RID: 17379
		public static readonly PropertyTagPropertyDefinition EformsLocaleId = PropertyTagPropertyDefinition.InternalCreate("EformsLocaleId", PropTag.EformsLocaleId);

		// Token: 0x040043E4 RID: 17380
		public static readonly PropertyTagPropertyDefinition PublishInAddressBook = PropertyTagPropertyDefinition.InternalCreate("PublishInAddressBook", PropTag.PublishInAddressBook);

		// Token: 0x040043E5 RID: 17381
		public static readonly PropertyTagPropertyDefinition HasRules = PropertyTagPropertyDefinition.InternalCreate("HasRules", PropTag.HasRules);

		// Token: 0x040043E6 RID: 17382
		internal static readonly PropertyTagPropertyDefinition MapiRulesTable = PropertyTagPropertyDefinition.InternalCreate("MapiRulesTable", PropTag.RulesTable, PropertyFlags.Streamable);

		// Token: 0x040043E7 RID: 17383
		internal static readonly PropertyTagPropertyDefinition MapiRulesData = PropertyTagPropertyDefinition.InternalCreate("MapiRulesData", (PropTag)1071710466U);

		// Token: 0x040043E8 RID: 17384
		internal static readonly PropertyTagPropertyDefinition ExtendedRuleCondition = PropertyTagPropertyDefinition.InternalCreate("ExtendedRuleCondition", (PropTag)244973826U);

		// Token: 0x040043E9 RID: 17385
		internal static readonly PropertyTagPropertyDefinition ExtendedRuleSizeLimit = PropertyTagPropertyDefinition.InternalCreate("ExtendedRuleSizeLimit", (PropTag)245039107U);

		// Token: 0x040043EA RID: 17386
		internal static readonly PropertyTagPropertyDefinition MapiAclTable = PropertyTagPropertyDefinition.InternalCreate("MapiAclTable", PropTag.AclTable);

		// Token: 0x040043EB RID: 17387
		internal static readonly PropertyTagPropertyDefinition RawSecurityDescriptor = PropertyTagPropertyDefinition.InternalCreate("NTSD", PropTag.NTSD);

		// Token: 0x040043EC RID: 17388
		internal static readonly PropertyTagPropertyDefinition RawSecurityDescriptorAsXml = PropertyTagPropertyDefinition.InternalCreate("NTSD", PropTag.NTSDAsXML, PropertyFlags.Streamable);

		// Token: 0x040043ED RID: 17389
		internal static readonly PropertyTagPropertyDefinition AclTableAndSecurityDescriptor = PropertyTagPropertyDefinition.InternalCreate("AclTableAndNTSD", PropTag.AclTableAndNTSD);

		// Token: 0x040043EE RID: 17390
		public static readonly StorePropertyDefinition SecurityDescriptor = new SecurityDescriptorProperty(InternalSchema.RawSecurityDescriptor);

		// Token: 0x040043EF RID: 17391
		public static readonly GuidNamePropertyDefinition XSenderTelephoneNumber = InternalSchema.CreateGuidNameProperty("X-CallingTelephoneNumber", typeof(string), WellKnownPropertySet.InternetHeaders, "X-CallingTelephoneNumber");

		// Token: 0x040043F0 RID: 17392
		public static readonly GuidNamePropertyDefinition XTnefCorrelator = InternalSchema.CreateGuidNameProperty("X-MS-TNEF-Correlator", typeof(string), WellKnownPropertySet.InternetHeaders, "X-MS-TNEF-Correlator");

		// Token: 0x040043F1 RID: 17393
		public static readonly GuidNamePropertyDefinition XMsExchOrganizationAuthDomain = InternalSchema.CreateGuidNameProperty("X-MS-Exchange-Organization-AuthDomain", typeof(string), WellKnownPropertySet.InternetHeaders, "X-MS-Exchange-Organization-AuthDomain");

		// Token: 0x040043F2 RID: 17394
		public static readonly GuidNamePropertyDefinition XMsExchOrganizationAuthAs = InternalSchema.CreateGuidNameProperty("X-MS-Exchange-Organization-AuthAs", typeof(string), WellKnownPropertySet.InternetHeaders, "X-MS-Exchange-Organization-AuthAs");

		// Token: 0x040043F3 RID: 17395
		public static readonly GuidNamePropertyDefinition XMsExchOrganizationAuthMechanism = InternalSchema.CreateGuidNameProperty("X-MS-Exchange-Organization-AuthMechanism", typeof(string), WellKnownPropertySet.InternetHeaders, "X-MS-Exchange-Organization-AuthMechanism");

		// Token: 0x040043F4 RID: 17396
		public static readonly GuidNamePropertyDefinition XMsExchOrganizationAuthSource = InternalSchema.CreateGuidNameProperty("X-MS-Exchange-Organization-AuthSource", typeof(string), WellKnownPropertySet.InternetHeaders, "X-MS-Exchange-Organization-AuthSource");

		// Token: 0x040043F5 RID: 17397
		public static readonly GuidNamePropertyDefinition XMsExchImapAppendStamp = InternalSchema.CreateGuidNameProperty("X-MS-Exchange-ImapAppendStamp", typeof(string), WellKnownPropertySet.InternetHeaders, "X-MS-Exchange-ImapAppendStamp");

		// Token: 0x040043F6 RID: 17398
		public static readonly GuidNamePropertyDefinition XMsExchOrganizationOriginalClientIPAddress = InternalSchema.CreateGuidNameProperty("X-MS-Exchange-Organization-OriginalClientIPAddress", typeof(string), WellKnownPropertySet.InternetHeaders, "X-MS-Exchange-Organization-OriginalClientIPAddress");

		// Token: 0x040043F7 RID: 17399
		public static readonly GuidNamePropertyDefinition XMsExchOrganizationOriginalServerIPAddress = InternalSchema.CreateGuidNameProperty("X-MS-Exchange-Organization-OriginalServerIPAddress", typeof(string), WellKnownPropertySet.InternetHeaders, "X-MS-Exchange-Organization-OriginalServerIPAddress");

		// Token: 0x040043F8 RID: 17400
		public static readonly GuidNamePropertyDefinition LastMovedTimeStamp = InternalSchema.CreateGuidNameProperty("LastMovedTimeStamp", typeof(ExDateTime), WellKnownPropertySet.Elc, "LastMovedTimeStamp");

		// Token: 0x040043F9 RID: 17401
		public static readonly GuidNamePropertyDefinition OriginalScl = InternalSchema.CreateGuidNameProperty("OriginalScl", typeof(int), WellKnownPropertySet.Messaging, "OriginalScl");

		// Token: 0x040043FA RID: 17402
		public static readonly PropertyTagPropertyDefinition SecureSubmitFlags = PropertyTagPropertyDefinition.InternalCreate("SecureSubmitFlags", PropTag.SecureSubmitFlags);

		// Token: 0x040043FB RID: 17403
		public static readonly PropertyTagPropertyDefinition SubmitFlags = PropertyTagPropertyDefinition.InternalCreate("SubmitFlags", PropTag.SubmitFlags);

		// Token: 0x040043FC RID: 17404
		public static readonly PropertyTagPropertyDefinition XMsExchOrganizationAVStampMailbox = PropertyTagPropertyDefinition.InternalCreate("XMsExchOrganizationAVStampMailbox", PropTag.TransportAntiVirusStamp);

		// Token: 0x040043FD RID: 17405
		public static readonly PropertyTagPropertyDefinition VoiceMessageDuration = PropertyTagPropertyDefinition.InternalCreate("VoiceMessageDuration", (PropTag)1744896003U);

		// Token: 0x040043FE RID: 17406
		public static readonly GuidNamePropertyDefinition XVoiceMessageDuration = InternalSchema.CreateGuidNameProperty("X-VoiceMessageDuration", typeof(string), WellKnownPropertySet.InternetHeaders, "X-VoiceMessageDuration");

		// Token: 0x040043FF RID: 17407
		public static readonly GuidNamePropertyDefinition XMsExchangeOrganizationRightsProtectMessage = InternalSchema.CreateGuidNameProperty("X-MS-Exchange-Organization-RightsProtectMessage", typeof(string), WellKnownPropertySet.InternetHeaders, "X-MS-Exchange-Organization-RightsProtectMessage");

		// Token: 0x04004400 RID: 17408
		public static readonly PropertyTagPropertyDefinition VoiceMessageSenderName = PropertyTagPropertyDefinition.InternalCreate("VoiceMessageSenderName", (PropTag)1745027103U);

		// Token: 0x04004401 RID: 17409
		public static readonly GuidNamePropertyDefinition XVoiceMessageSenderName = InternalSchema.CreateGuidNameProperty("X-VoiceMessageSenderName", typeof(string), WellKnownPropertySet.InternetHeaders, "X-VoiceMessageSenderName");

		// Token: 0x04004402 RID: 17410
		public static readonly PropertyTagPropertyDefinition FaxNumberOfPages = PropertyTagPropertyDefinition.InternalCreate("FaxNumberOfPages", (PropTag)1745092611U);

		// Token: 0x04004403 RID: 17411
		public static readonly GuidNamePropertyDefinition XFaxNumberOfPages = InternalSchema.CreateGuidNameProperty("X-FaxNumberOfPages", typeof(string), WellKnownPropertySet.InternetHeaders, "X-FaxNumberOfPages");

		// Token: 0x04004404 RID: 17412
		public static readonly PropertyTagPropertyDefinition VoiceMessageAttachmentOrder = PropertyTagPropertyDefinition.InternalCreate("VoiceMessageAttachmentOrder", (PropTag)1745158175U);

		// Token: 0x04004405 RID: 17413
		public static readonly GuidNamePropertyDefinition XVoiceMessageAttachmentOrder = InternalSchema.CreateGuidNameProperty("X-AttachmentOrder", typeof(string), WellKnownPropertySet.InternetHeaders, "X-AttachmentOrder");

		// Token: 0x04004406 RID: 17414
		public static readonly PropertyTagPropertyDefinition CallId = PropertyTagPropertyDefinition.InternalCreate("CallId", (PropTag)1745223711U);

		// Token: 0x04004407 RID: 17415
		public static readonly GuidNamePropertyDefinition XCallId = InternalSchema.CreateGuidNameProperty("X-CallID", typeof(string), WellKnownPropertySet.InternetHeaders, "X-CallID");

		// Token: 0x04004408 RID: 17416
		public static readonly GuidNamePropertyDefinition XRequireProtectedPlayOnPhone = InternalSchema.CreateGuidNameProperty("X-RequireProtectedPlayOnPhone", typeof(string), WellKnownPropertySet.InternetHeaders, "X-RequireProtectedPlayOnPhone");

		// Token: 0x04004409 RID: 17417
		public static readonly GuidNamePropertyDefinition XMsExchangeUMPartnerAssignedID = InternalSchema.CreateGuidNameProperty("X-MS-Exchange-UM-PartnerAssignedID", typeof(string), WellKnownPropertySet.UnifiedMessaging, "X-MS-Exchange-UM-PartnerAssignedID");

		// Token: 0x0400440A RID: 17418
		public static readonly GuidNamePropertyDefinition XMsExchangeUMPartnerContent = InternalSchema.CreateGuidNameProperty("X-MS-Exchange-UM-PartnerContent", typeof(string), WellKnownPropertySet.UnifiedMessaging, "X-MS-Exchange-UM-PartnerContent");

		// Token: 0x0400440B RID: 17419
		public static readonly GuidNamePropertyDefinition XMsExchangeUMPartnerContext = InternalSchema.CreateGuidNameProperty("X-MS-Exchange-UM-PartnerContext", typeof(string), WellKnownPropertySet.UnifiedMessaging, "X-MS-Exchange-UM-PartnerContext");

		// Token: 0x0400440C RID: 17420
		public static readonly GuidNamePropertyDefinition XMsExchangeUMPartnerStatus = InternalSchema.CreateGuidNameProperty("X-MS-Exchange-UM-PartnerStatus", typeof(string), WellKnownPropertySet.UnifiedMessaging, "X-MS-Exchange-UM-PartnerStatus");

		// Token: 0x0400440D RID: 17421
		public static readonly GuidNamePropertyDefinition XMsExchangeUMDialPlanLanguage = InternalSchema.CreateGuidNameProperty("X-MS-Exchange-UM-DialPlanLanguage", typeof(string), WellKnownPropertySet.UnifiedMessaging, "X-MS-Exchange-UM-DialPlanLanguage");

		// Token: 0x0400440E RID: 17422
		public static readonly GuidNamePropertyDefinition XMsExchangeUMCallerInformedOfAnalysis = InternalSchema.CreateGuidNameProperty("X-MS-Exchange-UM-CallerInformedOfAnalysis", typeof(string), WellKnownPropertySet.UnifiedMessaging, "X-MS-Exchange-UM-CallerInformedOfAnalysis");

		// Token: 0x0400440F RID: 17423
		public static readonly GuidNamePropertyDefinition PstnCallbackTelephoneNumber = InternalSchema.CreateGuidNameProperty("PstnCallbackTelephoneNumber", typeof(string), WellKnownPropertySet.UnifiedMessaging, "PstnCallbackTelephoneNumber");

		// Token: 0x04004410 RID: 17424
		public static readonly GuidNamePropertyDefinition UcSubject = InternalSchema.CreateGuidNameProperty("UcSubject", typeof(string), WellKnownPropertySet.UnifiedMessaging, "UcSubject");

		// Token: 0x04004411 RID: 17425
		public static readonly GuidNamePropertyDefinition ReceivedSPF = InternalSchema.CreateGuidNameProperty("Received-SPF", typeof(string), WellKnownPropertySet.InternetHeaders, "Received-SPF");

		// Token: 0x04004412 RID: 17426
		public static readonly GuidNamePropertyDefinition XCDRDataCallStartTime = InternalSchema.CreateGuidNameProperty("CallStartTime", typeof(ExDateTime), WellKnownPropertySet.UnifiedMessaging, "CallStartTime");

		// Token: 0x04004413 RID: 17427
		public static readonly GuidNamePropertyDefinition XCDRDataCallType = InternalSchema.CreateGuidNameProperty("CallType", typeof(string), WellKnownPropertySet.UnifiedMessaging, "CallType");

		// Token: 0x04004414 RID: 17428
		public static readonly GuidNamePropertyDefinition XCDRDataCallIdentity = InternalSchema.CreateGuidNameProperty("CallIdentity", typeof(string), WellKnownPropertySet.UnifiedMessaging, "CallIdentity");

		// Token: 0x04004415 RID: 17429
		public static readonly GuidNamePropertyDefinition XCDRDataParentCallIdentity = InternalSchema.CreateGuidNameProperty("ParentCallIdentity", typeof(string), WellKnownPropertySet.UnifiedMessaging, "ParentCallIdentity");

		// Token: 0x04004416 RID: 17430
		public static readonly GuidNamePropertyDefinition XCDRDataUMServerName = InternalSchema.CreateGuidNameProperty("UMServerName", typeof(string), WellKnownPropertySet.UnifiedMessaging, "UMServerName");

		// Token: 0x04004417 RID: 17431
		public static readonly GuidNamePropertyDefinition XCDRDataDialPlanGuid = InternalSchema.CreateGuidNameProperty("DialPlanGuid", typeof(Guid), WellKnownPropertySet.UnifiedMessaging, "DialPlanGuid");

		// Token: 0x04004418 RID: 17432
		public static readonly GuidNamePropertyDefinition XCDRDataDialPlanName = InternalSchema.CreateGuidNameProperty("DialPlanName", typeof(string), WellKnownPropertySet.UnifiedMessaging, "DialPlanName");

		// Token: 0x04004419 RID: 17433
		public static readonly GuidNamePropertyDefinition XCDRDataCallDuration = InternalSchema.CreateGuidNameProperty("CallDuration", typeof(int), WellKnownPropertySet.UnifiedMessaging, "CallDuration");

		// Token: 0x0400441A RID: 17434
		public static readonly GuidNamePropertyDefinition XCDRDataIPGatewayAddress = InternalSchema.CreateGuidNameProperty("IPGatewayAddress", typeof(string), WellKnownPropertySet.UnifiedMessaging, "IPGatewayAddress");

		// Token: 0x0400441B RID: 17435
		public static readonly GuidNamePropertyDefinition XCDRDataIPGatewayName = InternalSchema.CreateGuidNameProperty("IPGatewayName", typeof(string), WellKnownPropertySet.UnifiedMessaging, "IPGatewayName");

		// Token: 0x0400441C RID: 17436
		public static readonly GuidNamePropertyDefinition XCDRDataGatewayGuid = InternalSchema.CreateGuidNameProperty("GatewayGuid", typeof(Guid), WellKnownPropertySet.UnifiedMessaging, "GatewayGuid");

		// Token: 0x0400441D RID: 17437
		public static readonly GuidNamePropertyDefinition XCDRDataCalledPhoneNumber = InternalSchema.CreateGuidNameProperty("CalledPhoneNumber", typeof(string), WellKnownPropertySet.UnifiedMessaging, "CalledPhoneNumber");

		// Token: 0x0400441E RID: 17438
		public static readonly GuidNamePropertyDefinition XCDRDataCallerPhoneNumber = InternalSchema.CreateGuidNameProperty("CallerPhoneNumber", typeof(string), WellKnownPropertySet.UnifiedMessaging, "CallerPhoneNumber");

		// Token: 0x0400441F RID: 17439
		public static readonly GuidNamePropertyDefinition XCDRDataOfferResult = InternalSchema.CreateGuidNameProperty("OfferResult", typeof(string), WellKnownPropertySet.UnifiedMessaging, "OfferResult");

		// Token: 0x04004420 RID: 17440
		public static readonly GuidNamePropertyDefinition XCDRDataDropCallReason = InternalSchema.CreateGuidNameProperty("DropCallReason", typeof(string), WellKnownPropertySet.UnifiedMessaging, "DropCallReason");

		// Token: 0x04004421 RID: 17441
		public static readonly GuidNamePropertyDefinition XCDRDataReasonForCall = InternalSchema.CreateGuidNameProperty("ReasonForCall", typeof(string), WellKnownPropertySet.UnifiedMessaging, "ReasonForCall");

		// Token: 0x04004422 RID: 17442
		public static readonly GuidNamePropertyDefinition XCDRDataTransferredNumber = InternalSchema.CreateGuidNameProperty("TransferredNumber", typeof(string), WellKnownPropertySet.UnifiedMessaging, "TransferredNumber");

		// Token: 0x04004423 RID: 17443
		public static readonly GuidNamePropertyDefinition XCDRDataDialedString = InternalSchema.CreateGuidNameProperty("DialedString", typeof(string), WellKnownPropertySet.UnifiedMessaging, "DialedString");

		// Token: 0x04004424 RID: 17444
		public static readonly GuidNamePropertyDefinition XCDRDataCallerMailboxAlias = InternalSchema.CreateGuidNameProperty("CallerMailboxAlias", typeof(string), WellKnownPropertySet.UnifiedMessaging, "CallerMailboxAlias");

		// Token: 0x04004425 RID: 17445
		public static readonly GuidNamePropertyDefinition XCDRDataCalleeMailboxAlias = InternalSchema.CreateGuidNameProperty("CalleeMailboxAlias", typeof(string), WellKnownPropertySet.UnifiedMessaging, "CalleeMailboxAlias");

		// Token: 0x04004426 RID: 17446
		public static readonly GuidNamePropertyDefinition XCDRDataAutoAttendantName = InternalSchema.CreateGuidNameProperty("AutoAttendantName", typeof(string), WellKnownPropertySet.UnifiedMessaging, "AutoAttendantName");

		// Token: 0x04004427 RID: 17447
		public static readonly GuidNamePropertyDefinition XCDRDataAudioCodec = InternalSchema.CreateGuidNameProperty("AudioCodec", typeof(string), WellKnownPropertySet.UnifiedMessaging, "AudioCodec");

		// Token: 0x04004428 RID: 17448
		public static readonly GuidNamePropertyDefinition XCDRDataBurstDensity = InternalSchema.CreateGuidNameProperty("BurstDensity", typeof(float), WellKnownPropertySet.UnifiedMessaging, "BurstDensity");

		// Token: 0x04004429 RID: 17449
		public static readonly GuidNamePropertyDefinition XCDRDataBurstDuration = InternalSchema.CreateGuidNameProperty("BurstDuration", typeof(float), WellKnownPropertySet.UnifiedMessaging, "BurstDuration");

		// Token: 0x0400442A RID: 17450
		public static readonly GuidNamePropertyDefinition XCDRDataJitter = InternalSchema.CreateGuidNameProperty("Jitter", typeof(float), WellKnownPropertySet.UnifiedMessaging, "Jitter");

		// Token: 0x0400442B RID: 17451
		public static readonly GuidNamePropertyDefinition XCDRDataNMOS = InternalSchema.CreateGuidNameProperty("NMOS", typeof(float), WellKnownPropertySet.UnifiedMessaging, "NMOS");

		// Token: 0x0400442C RID: 17452
		public static readonly GuidNamePropertyDefinition XCDRDataNMOSDegradation = InternalSchema.CreateGuidNameProperty("NMOSDegradation", typeof(float), WellKnownPropertySet.UnifiedMessaging, "NMOSDegradation");

		// Token: 0x0400442D RID: 17453
		public static readonly GuidNamePropertyDefinition XCDRDataNMOSDegradationJitter = InternalSchema.CreateGuidNameProperty("NMOSDegradationJitter", typeof(float), WellKnownPropertySet.UnifiedMessaging, "NMOSDegradationJitter");

		// Token: 0x0400442E RID: 17454
		public static readonly GuidNamePropertyDefinition XCDRDataNMOSDegradationPacketLoss = InternalSchema.CreateGuidNameProperty("NMOSDegradationPacketLoss", typeof(float), WellKnownPropertySet.UnifiedMessaging, "NMOSDegradationPacketLoss");

		// Token: 0x0400442F RID: 17455
		public static readonly GuidNamePropertyDefinition XCDRDataPacketLoss = InternalSchema.CreateGuidNameProperty("PacketLoss", typeof(float), WellKnownPropertySet.UnifiedMessaging, "PacketLoss");

		// Token: 0x04004430 RID: 17456
		public static readonly GuidNamePropertyDefinition XCDRDataRoundTrip = InternalSchema.CreateGuidNameProperty("RoundTrip", typeof(float), WellKnownPropertySet.UnifiedMessaging, "RoundTrip");

		// Token: 0x04004431 RID: 17457
		public static readonly GuidNamePropertyDefinition AsrData = InternalSchema.CreateGuidNameProperty("AsrData", typeof(byte[]), WellKnownPropertySet.UnifiedMessaging, "AsrData");

		// Token: 0x04004432 RID: 17458
		public static readonly PropertyTagPropertyDefinition Flags = PropertyTagPropertyDefinition.InternalCreate("Flags", PropTag.MessageFlags);

		// Token: 0x04004433 RID: 17459
		public static readonly PropertyTagPropertyDefinition MessageStatus = PropertyTagPropertyDefinition.InternalCreate("MessageStatus", PropTag.MsgStatus);

		// Token: 0x04004434 RID: 17460
		public static readonly PropertyTagPropertyDefinition ItemTemporaryFlag = PropertyTagPropertyDefinition.InternalCreate("PR_ITEM_TMPFLAGS", PropTag.ItemTemporaryFlag);

		// Token: 0x04004435 RID: 17461
		public static readonly PropertyTagPropertyDefinition DeliverAsRead = PropertyTagPropertyDefinition.InternalCreate("DeliverAsRead", PropTag.DeliverAsRead);

		// Token: 0x04004436 RID: 17462
		public static readonly GuidNamePropertyDefinition ContentClass = InternalSchema.CreateGuidNameProperty("ContentClass", typeof(string), WellKnownPropertySet.InternetHeaders, "content-class");

		// Token: 0x04004437 RID: 17463
		public static readonly GuidNamePropertyDefinition AttachmentMacInfo = InternalSchema.CreateGuidNameProperty("AttachmentMacInfo", typeof(byte[]), WellKnownPropertySet.Attachment, "AttachmentMacInfo", PropertyFlags.Streamable, PropertyDefinitionConstraint.None);

		// Token: 0x04004438 RID: 17464
		public static readonly GuidNamePropertyDefinition AttachmentMacContentType = InternalSchema.CreateGuidNameProperty("AttachmentMacContentType", typeof(string), WellKnownPropertySet.Attachment, "AttachmentMacContentType");

		// Token: 0x04004439 RID: 17465
		public static readonly GuidNamePropertyDefinition AttachmentProviderEndpointUrl = InternalSchema.CreateGuidNameProperty("AttachmentProviderEndpointUrl", typeof(string), WellKnownPropertySet.Attachment, "AttachmentProviderEndpointUrl");

		// Token: 0x0400443A RID: 17466
		public static readonly GuidNamePropertyDefinition AttachmentProviderType = InternalSchema.CreateGuidNameProperty("AttachmentProviderType", typeof(string), WellKnownPropertySet.Attachment, "AttachmentProviderType");

		// Token: 0x0400443B RID: 17467
		public static readonly PropertyTagPropertyDefinition ConversationIndex = PropertyTagPropertyDefinition.InternalCreate("ConversationIndex", PropTag.ConversationIndex);

		// Token: 0x0400443C RID: 17468
		public static readonly PropertyTagPropertyDefinition ConversationTopic = PropertyTagPropertyDefinition.InternalCreate("ConversationTopic", PropTag.ConversationTopic);

		// Token: 0x0400443D RID: 17469
		public static readonly PropertyTagPropertyDefinition ConversationTopicHash = PropertyTagPropertyDefinition.InternalCreate("ConversationTopicHash", PropTag.ConversationTopicHash);

		// Token: 0x0400443E RID: 17470
		public static readonly PropertyTagPropertyDefinition IsDeliveryReceiptRequestedInternal = PropertyTagPropertyDefinition.InternalCreate("IsDeliveryReceiptRequestedInternal", PropTag.OriginatorDeliveryReportRequested);

		// Token: 0x0400443F RID: 17471
		public static readonly PropertyTagPropertyDefinition IsNonDeliveryReceiptRequestedInternal = PropertyTagPropertyDefinition.InternalCreate("IsNonDeliveryReceiptRequestedInternal", PropTag.OriginatorNonDeliveryReportRequested);

		// Token: 0x04004440 RID: 17472
		public static readonly PropertyTagPropertyDefinition DisplayCcInternal = PropertyTagPropertyDefinition.InternalCreate("DisplayCc", PropTag.DisplayCc, PropertyFlags.ReadOnly | PropertyFlags.Streamable);

		// Token: 0x04004441 RID: 17473
		public static readonly PropertyTagPropertyDefinition DisplayToInternal = PropertyTagPropertyDefinition.InternalCreate("DisplayToInternal", PropTag.DisplayTo, PropertyFlags.ReadOnly | PropertyFlags.Streamable);

		// Token: 0x04004442 RID: 17474
		public static readonly PropertyTagPropertyDefinition MessageToMe = PropertyTagPropertyDefinition.InternalCreate("MessageToMe", PropTag.MessageToMe);

		// Token: 0x04004443 RID: 17475
		public static readonly PropertyTagPropertyDefinition MessageCcMe = PropertyTagPropertyDefinition.InternalCreate("MessageCcMe", PropTag.MessageCcMe);

		// Token: 0x04004444 RID: 17476
		public static readonly PropertyTagPropertyDefinition DisplayBccInternal = PropertyTagPropertyDefinition.InternalCreate("DisplayBccInternal", PropTag.DisplayBcc, PropertyFlags.ReadOnly | PropertyFlags.Streamable);

		// Token: 0x04004445 RID: 17477
		public static readonly GuidIdPropertyDefinition DisplayAttendeesAll = InternalSchema.CreateGuidIdProperty("DisplayAttendeesAll", typeof(string), WellKnownPropertySet.Appointment, 33336, PropertyFlags.Streamable, PropertyDefinitionConstraint.None);

		// Token: 0x04004446 RID: 17478
		public static readonly GuidIdPropertyDefinition DisplayAttendeesTo = InternalSchema.CreateGuidIdProperty("DisplayAttendeesTo", typeof(string), WellKnownPropertySet.Appointment, 33339, PropertyFlags.Streamable, PropertyDefinitionConstraint.None);

		// Token: 0x04004447 RID: 17479
		public static readonly GuidIdPropertyDefinition DisplayAttendeesCc = InternalSchema.CreateGuidIdProperty("DisplayAttendeesCc", typeof(string), WellKnownPropertySet.Appointment, 33340, PropertyFlags.Streamable, new PropertyDefinitionConstraint[0]);

		// Token: 0x04004448 RID: 17480
		public static readonly PropertyTagPropertyDefinition ParentDisplayName = PropertyTagPropertyDefinition.InternalCreate("ParentDisplay", PropTag.ParentDisplay);

		// Token: 0x04004449 RID: 17481
		public static readonly GuidIdPropertyDefinition FlagRequest = InternalSchema.CreateGuidIdProperty("FlagRequest", typeof(string), WellKnownPropertySet.Common, 34096);

		// Token: 0x0400444A RID: 17482
		public static readonly GuidIdPropertyDefinition RequestedAction = InternalSchema.CreateGuidIdProperty("FlagStringEnum", typeof(int), WellKnownPropertySet.Common, 34240);

		// Token: 0x0400444B RID: 17483
		public static readonly GuidIdPropertyDefinition InfoPathFormName = InternalSchema.CreateGuidIdProperty("InfoPathFormName", typeof(string), WellKnownPropertySet.Common, 34225);

		// Token: 0x0400444C RID: 17484
		public static readonly PropertyTagPropertyDefinition InReplyTo = PropertyTagPropertyDefinition.InternalCreate("PR_IN_REPLY_TO_ID", (PropTag)272760863U);

		// Token: 0x0400444D RID: 17485
		public static readonly PropertyTagPropertyDefinition MapiInternetCpid = PropertyTagPropertyDefinition.InternalCreate("MapiInternetCpid", PropTag.InternetCPID);

		// Token: 0x0400444E RID: 17486
		internal static readonly PropertyTagPropertyDefinition TextAttachmentCharset = PropertyTagPropertyDefinition.InternalCreate("TextAttachmentCharset", (PropTag)924516383U);

		// Token: 0x0400444F RID: 17487
		public static readonly PropertyTagPropertyDefinition InternetMessageId = PropertyTagPropertyDefinition.InternalCreate("InternetMessageId", PropTag.InternetMessageId);

		// Token: 0x04004450 RID: 17488
		public static readonly PropertyTagPropertyDefinition InternetMessageIdHash = PropertyTagPropertyDefinition.InternalCreate("InternetMessageIdHash", PropTag.InternetMessageIdHash);

		// Token: 0x04004451 RID: 17489
		public static readonly PropertyTagPropertyDefinition Preview = PropertyTagPropertyDefinition.InternalCreate("Preview", PropTag.Preview);

		// Token: 0x04004452 RID: 17490
		public static readonly PropertyTagPropertyDefinition InternetReferences = PropertyTagPropertyDefinition.InternalCreate("PR_INTERNET_REFERENCES", (PropTag)272171039U);

		// Token: 0x04004453 RID: 17491
		public static readonly PropertyTagPropertyDefinition IsAutoForwarded = PropertyTagPropertyDefinition.InternalCreate("IsAutoForwarded", PropTag.AutoForwarded);

		// Token: 0x04004454 RID: 17492
		public static readonly PropertyTagPropertyDefinition IsReplyRequested = PropertyTagPropertyDefinition.InternalCreate("IsReplyRequested", PropTag.ReplyRequested);

		// Token: 0x04004455 RID: 17493
		public static readonly PropertyTagPropertyDefinition IsResponseRequested = PropertyTagPropertyDefinition.InternalCreate("IsResponseRequested", PropTag.ResponseRequested);

		// Token: 0x04004456 RID: 17494
		public static readonly PropertyTagPropertyDefinition IsReadReceiptRequestedInternal = PropertyTagPropertyDefinition.InternalCreate("IsReadReceiptRequestedInternal", PropTag.ReadReceiptRequested);

		// Token: 0x04004457 RID: 17495
		public static readonly PropertyTagPropertyDefinition IsNotReadReceiptRequestedInternal = PropertyTagPropertyDefinition.InternalCreate("IsNotReadReceiptRequestedInternal", PropTag.NonReceiptNotificationRequested);

		// Token: 0x04004458 RID: 17496
		public static readonly PropertyTagPropertyDefinition ListHelp = PropertyTagPropertyDefinition.InternalCreate("ListHelp", (PropTag)272826399U);

		// Token: 0x04004459 RID: 17497
		public static readonly PropertyTagPropertyDefinition ListSubscribe = PropertyTagPropertyDefinition.InternalCreate("ListSubscribe", (PropTag)272891935U);

		// Token: 0x0400445A RID: 17498
		public static readonly PropertyTagPropertyDefinition ListUnsubscribe = PropertyTagPropertyDefinition.InternalCreate("ListUnsubscribe", (PropTag)272957471U);

		// Token: 0x0400445B RID: 17499
		public static readonly PropertyTagPropertyDefinition MapiHasAttachment = PropertyTagPropertyDefinition.InternalCreate("MapiHasAttachment", PropTag.Hasattach, PropertyFlags.ReadOnly);

		// Token: 0x0400445C RID: 17500
		public static readonly PropertyTagPropertyDefinition MapiPriority = PropertyTagPropertyDefinition.InternalCreate("MapiPriority", PropTag.Priority);

		// Token: 0x0400445D RID: 17501
		public static readonly PropertyTagPropertyDefinition MapiReplyToBlob = PropertyTagPropertyDefinition.InternalCreate("ReplyTo", PropTag.ReplyRecipientEntries, new PropertyDefinitionConstraint[]
		{
			new StoreByteArrayLengthConstraint(32767)
		});

		// Token: 0x0400445E RID: 17502
		public static readonly PropertyTagPropertyDefinition MapiReplyToNames = PropertyTagPropertyDefinition.InternalCreate("ReplyToNames", PropTag.ReplyRecipientNames);

		// Token: 0x0400445F RID: 17503
		public static readonly GuidNamePropertyDefinition MapiLikersBlob = InternalSchema.CreateGuidNameProperty("Likers", typeof(byte[]), WellKnownPropertySet.Messaging, "Likers");

		// Token: 0x04004460 RID: 17504
		public static readonly PropertyTagPropertyDefinition MapiLikeCount = PropertyTagPropertyDefinition.InternalCreate("LikeCount", PropTag.LikeCount);

		// Token: 0x04004461 RID: 17505
		public static readonly PropertyTagPropertyDefinition PeopleCentricConversationId = PropertyTagPropertyDefinition.InternalCreate("PeopleCentricConversationId", PropTag.PeopleCentricConversationId);

		// Token: 0x04004462 RID: 17506
		public static readonly PropertyTagPropertyDefinition MessageRecipients = PropertyTagPropertyDefinition.InternalCreate("MessageRecipients", PropTag.MessageRecipients);

		// Token: 0x04004463 RID: 17507
		public static readonly PropertyTagPropertyDefinition MID = PropertyTagPropertyDefinition.InternalCreate("MID", PropTag.Mid);

		// Token: 0x04004464 RID: 17508
		public static readonly PropertyTagPropertyDefinition LTID = PropertyTagPropertyDefinition.InternalCreate("LTID", PropTag.LTID);

		// Token: 0x04004465 RID: 17509
		public static readonly PropertyTagPropertyDefinition MappingSignature = PropertyTagPropertyDefinition.InternalCreate("MappingSignature", PropTag.MappingSignature);

		// Token: 0x04004466 RID: 17510
		public static readonly PropertyTagPropertyDefinition MdbProvider = PropertyTagPropertyDefinition.InternalCreate("MdbProvider", PropTag.MdbProvider);

		// Token: 0x04004467 RID: 17511
		public static readonly PropertyTagPropertyDefinition RuleTriggerHistory = PropertyTagPropertyDefinition.InternalCreate("RuleTriggerHistory", (PropTag)1072824578U);

		// Token: 0x04004468 RID: 17512
		public static readonly PropertyTagPropertyDefinition RuleError = PropertyTagPropertyDefinition.InternalCreate("RuleError", PropTag.RuleError);

		// Token: 0x04004469 RID: 17513
		public static readonly PropertyTagPropertyDefinition RuleActionType = PropertyTagPropertyDefinition.InternalCreate("RuleActionType", PropTag.RuleActionType);

		// Token: 0x0400446A RID: 17514
		public static readonly PropertyTagPropertyDefinition RuleActionNumber = PropertyTagPropertyDefinition.InternalCreate("RuleActionNumber", PropTag.RuleActionNumber);

		// Token: 0x0400446B RID: 17515
		public static readonly PropertyTagPropertyDefinition RuleId = PropertyTagPropertyDefinition.InternalCreate("RuleId", PropTag.RuleID);

		// Token: 0x0400446C RID: 17516
		public static readonly PropertyTagPropertyDefinition RuleIds = PropertyTagPropertyDefinition.InternalCreate("RuleIds", PropTag.RuleIDs);

		// Token: 0x0400446D RID: 17517
		public static readonly PropertyTagPropertyDefinition DelegatedByRule = PropertyTagPropertyDefinition.InternalCreate("DelegatedByRule", (PropTag)1071841291U);

		// Token: 0x0400446E RID: 17518
		public static readonly PropertyTagPropertyDefinition RuleFolderEntryId = PropertyTagPropertyDefinition.InternalCreate("RuleFolderEntryId", PropTag.RuleFolderEntryID);

		// Token: 0x0400446F RID: 17519
		public static readonly PropertyTagPropertyDefinition RuleProvider = PropertyTagPropertyDefinition.InternalCreate("RuleProvider", PropTag.RuleProvider);

		// Token: 0x04004470 RID: 17520
		public static readonly PropertyTagPropertyDefinition ClientActions = PropertyTagPropertyDefinition.InternalCreate("ClientActions", PropTag.PromotedProperties);

		// Token: 0x04004471 RID: 17521
		public static readonly PropertyTagPropertyDefinition DeferredActionMessageBackPatched = PropertyTagPropertyDefinition.InternalCreate("DeferredActionMessageBackPatched", PropTag.DeferredActionMessageBackPatched);

		// Token: 0x04004472 RID: 17522
		public static readonly PropertyTagPropertyDefinition HasDeferredActionMessage = PropertyTagPropertyDefinition.InternalCreate("HasDeferredActionMessage", PropTag.HasDeferredActionMessage);

		// Token: 0x04004473 RID: 17523
		public static readonly PropertyTagPropertyDefinition MoveToFolderEntryId = PropertyTagPropertyDefinition.InternalCreate("MoveToFolderEntryId", (PropTag)1072955650U);

		// Token: 0x04004474 RID: 17524
		public static readonly PropertyTagPropertyDefinition MoveToStoreEntryId = PropertyTagPropertyDefinition.InternalCreate("MoveToStoreEntryId", (PropTag)1072890114U);

		// Token: 0x04004475 RID: 17525
		public static readonly PropertyTagPropertyDefinition OriginalMessageEntryId = PropertyTagPropertyDefinition.InternalCreate("OriginalMessageEntryId", (PropTag)1715863810U);

		// Token: 0x04004476 RID: 17526
		public static readonly PropertyTagPropertyDefinition OriginalMessageSvrEId = PropertyTagPropertyDefinition.InternalCreate("OriginalMessageSvrEId", (PropTag)1732313346U);

		// Token: 0x04004477 RID: 17527
		public static readonly PropertyTagPropertyDefinition NormalizedSubjectInternal = PropertyTagPropertyDefinition.InternalCreate("NormalizedSubjectInternal", PropTag.NormalizedSubject);

		// Token: 0x04004478 RID: 17528
		public static readonly PropertyTagPropertyDefinition ReceivedTime = PropertyTagPropertyDefinition.InternalCreate("ReceivedTime", PropTag.MessageDeliveryTime);

		// Token: 0x04004479 RID: 17529
		public static readonly PropertyTagPropertyDefinition RenewTime = PropertyTagPropertyDefinition.InternalCreate("RenewTime", PropTag.RenewTime);

		// Token: 0x0400447A RID: 17530
		public static readonly PropertyTagPropertyDefinition ReceivedOrRenewTime = PropertyTagPropertyDefinition.InternalCreate("ReceivedOrRenewTime", PropTag.DeliveryOrRenewTime);

		// Token: 0x0400447B RID: 17531
		public static readonly PropertyTagPropertyDefinition MailboxGuidGuid = PropertyTagPropertyDefinition.InternalCreate("MailboxGuidGuid", PropTag.MailboxDSGuidGuid);

		// Token: 0x0400447C RID: 17532
		public static readonly PropertyTagPropertyDefinition MailboxPartitionMailboxGuids = PropertyTagPropertyDefinition.InternalCreate("MailboxPartitionMailboxGuids", PropTag.MailboxPartitionMailboxGuids);

		// Token: 0x0400447D RID: 17533
		public static readonly PropertyTagPropertyDefinition DeferredDeliveryTime = PropertyTagPropertyDefinition.InternalCreate("DeferredDeliveryTime", PropTag.DeferredDeliveryTime);

		// Token: 0x0400447E RID: 17534
		public static readonly PropertyTagPropertyDefinition DeferredSendTime = PropertyTagPropertyDefinition.InternalCreate("DeferredSendTime", PropTag.DeferredSendTime);

		// Token: 0x0400447F RID: 17535
		public static readonly PropertyTagPropertyDefinition ReplyTime = PropertyTagPropertyDefinition.InternalCreate("PR_REPLY_TIME", PropTag.ReplyTime);

		// Token: 0x04004480 RID: 17536
		public static readonly PropertyTagPropertyDefinition SenderAddressType = PropertyTagPropertyDefinition.InternalCreate("SenderAddressType", PropTag.SenderAddrType, new PropertyDefinitionConstraint[]
		{
			new NonMoveMailboxPropertyConstraint(new StringLengthConstraint(0, 9))
		});

		// Token: 0x04004481 RID: 17537
		public static readonly PropertyTagPropertyDefinition SenderDisplayName = PropertyTagPropertyDefinition.InternalCreate("SenderDisplayName", PropTag.SenderName);

		// Token: 0x04004482 RID: 17538
		public static readonly PropertyTagPropertyDefinition SenderEmailAddress = PropertyTagPropertyDefinition.InternalCreate("SenderEmailAddress", PropTag.SenderEmailAddress);

		// Token: 0x04004483 RID: 17539
		public static readonly PropertyTagPropertyDefinition SenderEntryId = PropertyTagPropertyDefinition.InternalCreate("SenderEntryId", PropTag.SenderEntryId);

		// Token: 0x04004484 RID: 17540
		public static readonly PropertyTagPropertyDefinition SenderSearchKey = PropertyTagPropertyDefinition.InternalCreate("SenderSearchKey", PropTag.SenderSearchKey);

		// Token: 0x04004485 RID: 17541
		public static readonly PropertyTagPropertyDefinition SenderIdStatus = PropertyTagPropertyDefinition.InternalCreate("SenderIdStatus", PropTag.SenderIdStatus);

		// Token: 0x04004486 RID: 17542
		public static readonly PropertyTagPropertyDefinition SentMailEntryId = PropertyTagPropertyDefinition.InternalCreate("SentMailEntryId", PropTag.SentMailEntryId);

		// Token: 0x04004487 RID: 17543
		public static readonly PropertyTagPropertyDefinition SenderSID = PropertyTagPropertyDefinition.InternalCreate("SenderSID", PropTag.SenderSID);

		// Token: 0x04004488 RID: 17544
		public static readonly PropertyTagPropertyDefinition ConversationCreatorSID = PropertyTagPropertyDefinition.InternalCreate("ConversationCreatorSID", (PropTag)240845058U);

		// Token: 0x04004489 RID: 17545
		public static readonly PropertyTagPropertyDefinition SenderGuid = PropertyTagPropertyDefinition.InternalCreate("SenderGuid", PropTag.SenderGuid);

		// Token: 0x0400448A RID: 17546
		public static readonly PropertyTagPropertyDefinition SentRepresentingDisplayName = PropertyTagPropertyDefinition.InternalCreate("SentRepresentingDisplayName", PropTag.SentRepresentingName);

		// Token: 0x0400448B RID: 17547
		public static readonly PropertyTagPropertyDefinition SentRepresentingEmailAddress = PropertyTagPropertyDefinition.InternalCreate("SentRepresentingEmailAddress", PropTag.SentRepresentingEmailAddress);

		// Token: 0x0400448C RID: 17548
		public static readonly PropertyTagPropertyDefinition SentRepresentingEntryId = PropertyTagPropertyDefinition.InternalCreate("SentRepresentingEntryId", PropTag.SentRepresentingEntryId);

		// Token: 0x0400448D RID: 17549
		public static readonly PropertyTagPropertyDefinition SentRepresentingSearchKey = PropertyTagPropertyDefinition.InternalCreate("SentRepresentingSearchKey", PropTag.SentRepresentingSearchKey);

		// Token: 0x0400448E RID: 17550
		public static readonly PropertyTagPropertyDefinition SentRepresentingSimpleDisplayName = PropertyTagPropertyDefinition.InternalCreate("SentRepresentingSimpleDisplayName", (PropTag)1076953119U);

		// Token: 0x0400448F RID: 17551
		public static readonly PropertyTagPropertyDefinition SentRepresentingOrgAddressType = PropertyTagPropertyDefinition.InternalCreate("SentRepresentingOrgAddressType", (PropTag)1078001695U);

		// Token: 0x04004490 RID: 17552
		public static readonly PropertyTagPropertyDefinition SentRepresentingOrgEmailAddr = PropertyTagPropertyDefinition.InternalCreate("SentRepresentingOrgEmailAddr", (PropTag)1078067231U);

		// Token: 0x04004491 RID: 17553
		public static readonly PropertyTagPropertyDefinition SentRepresentingSID = PropertyTagPropertyDefinition.InternalCreate("SentRepresentingSID", (PropTag)239993090U);

		// Token: 0x04004492 RID: 17554
		public static readonly PropertyTagPropertyDefinition SentRepresentingGuid = PropertyTagPropertyDefinition.InternalCreate("SentRepresentingGuid", (PropTag)239141122U);

		// Token: 0x04004493 RID: 17555
		public static readonly PropertyTagPropertyDefinition SenderFlags = PropertyTagPropertyDefinition.InternalCreate("SenderFlags", (PropTag)1075380227U);

		// Token: 0x04004494 RID: 17556
		public static readonly PropertyTagPropertyDefinition SentRepresentingFlags = PropertyTagPropertyDefinition.InternalCreate("SentRepresentingFlags", (PropTag)1075445763U);

		// Token: 0x04004495 RID: 17557
		public static readonly PropertyTagPropertyDefinition SentRepresentingType = PropertyTagPropertyDefinition.InternalCreate("SentRepresentingType", PropTag.SentRepresentingAddrType, new PropertyDefinitionConstraint[]
		{
			new NonMoveMailboxPropertyConstraint(new StringLengthConstraint(0, 9))
		});

		// Token: 0x04004496 RID: 17558
		public static readonly PropertyTagPropertyDefinition MessageSubmissionId = PropertyTagPropertyDefinition.InternalCreate("MessageSubmissionId", PropTag.MessageSubmissionId);

		// Token: 0x04004497 RID: 17559
		public static readonly PropertyTagPropertyDefinition ProviderSubmitTime = PropertyTagPropertyDefinition.InternalCreate("ProviderSubmitTime", PropTag.ProviderSubmitTime);

		// Token: 0x04004498 RID: 17560
		public static readonly PropertyTagPropertyDefinition SentTime = PropertyTagPropertyDefinition.InternalCreate("SentTime", PropTag.ClientSubmitTime);

		// Token: 0x04004499 RID: 17561
		public static readonly PropertyTagPropertyDefinition SentMailSvrEId = PropertyTagPropertyDefinition.InternalCreate("SentMailSvrEId", (PropTag)1732247810U);

		// Token: 0x0400449A RID: 17562
		public static readonly PropertyTagPropertyDefinition SubjectPrefixInternal = PropertyTagPropertyDefinition.InternalCreate("SubjectPrefixInternal", PropTag.SubjectPrefix);

		// Token: 0x0400449B RID: 17563
		public static readonly PropertyTagPropertyDefinition MapiSubject = PropertyTagPropertyDefinition.InternalCreate("MapiSubject", PropTag.Subject);

		// Token: 0x0400449C RID: 17564
		public static readonly PropertyTagPropertyDefinition TnefCorrelationKey = PropertyTagPropertyDefinition.InternalCreate("TnefCorrelationKey", PropTag.TnefCorrelationKey);

		// Token: 0x0400449D RID: 17565
		public static readonly PropertyTagPropertyDefinition TransportMessageHeaders = PropertyTagPropertyDefinition.InternalCreate("TransportMessageHeaders", PropTag.TransportMessageHeaders, PropertyFlags.Streamable);

		// Token: 0x0400449E RID: 17566
		public static readonly PropertyTagPropertyDefinition ParticipantSID = PropertyTagPropertyDefinition.InternalCreate("ParticipantSID", PropTag.ParticipantSID);

		// Token: 0x0400449F RID: 17567
		public static readonly PropertyTagPropertyDefinition ParticipantGuid = PropertyTagPropertyDefinition.InternalCreate("ParticipantGuid", PropTag.ParticipantGuid);

		// Token: 0x040044A0 RID: 17568
		public static readonly PropertyTagPropertyDefinition DeleteAfterSubmit = PropertyTagPropertyDefinition.InternalCreate("DeleteAfterSubmit", PropTag.DeleteAfterSubmit);

		// Token: 0x040044A1 RID: 17569
		public static readonly PropertyTagPropertyDefinition TargetEntryId = PropertyTagPropertyDefinition.InternalCreate("TargetEntryId", PropTag.TargetEntryId);

		// Token: 0x040044A2 RID: 17570
		public static readonly PropertyTagPropertyDefinition MapiFlagStatus = PropertyTagPropertyDefinition.InternalCreate("PR_FLAG_STATUS", (PropTag)277872643U);

		// Token: 0x040044A3 RID: 17571
		public static readonly PropertyTagPropertyDefinition Associated = PropertyTagPropertyDefinition.InternalCreate("Associated", PropTag.Associated);

		// Token: 0x040044A4 RID: 17572
		public static readonly PropertyTagPropertyDefinition IconIndex = PropertyTagPropertyDefinition.InternalCreate("IconIndex", (PropTag)276824067U);

		// Token: 0x040044A5 RID: 17573
		public static readonly PropertyTagPropertyDefinition LastVerbExecuted = PropertyTagPropertyDefinition.InternalCreate("PR_LAST_VERB_EXECUTED", (PropTag)276889603U);

		// Token: 0x040044A6 RID: 17574
		public static readonly PropertyTagPropertyDefinition LastVerbExecutionTime = PropertyTagPropertyDefinition.InternalCreate("PR_LAST_VERB_EXECUTIONTIME", (PropTag)276955200U);

		// Token: 0x040044A7 RID: 17575
		public static readonly PropertyTagPropertyDefinition ReplyForwardStatus = PropertyTagPropertyDefinition.InternalCreate("ReplyForwardStatus", (PropTag)2081095711U);

		// Token: 0x040044A8 RID: 17576
		public static readonly PropertyTagPropertyDefinition PopImapPoisonMessageStamp = PropertyTagPropertyDefinition.InternalCreate("PopImapPoisonMessageStamp", (PropTag)2081161247U);

		// Token: 0x040044A9 RID: 17577
		public static readonly GuidNamePropertyDefinition PopImapConversionVersion = InternalSchema.CreateGuidNameProperty("PopImapConversionVersion", typeof(string), WellKnownPropertySet.IMAPMsg, "PopImap:PopImapConversionVersion");

		// Token: 0x040044AA RID: 17578
		public static readonly GuidNamePropertyDefinition PopMIMESize = InternalSchema.CreateGuidNameProperty("PopMIMESize", typeof(long), WellKnownPropertySet.IMAPMsg, "PopImap:PopMIMESize");

		// Token: 0x040044AB RID: 17579
		public static readonly GuidNamePropertyDefinition PopMIMEOptions = InternalSchema.CreateGuidNameProperty("PopMIMEOptions", typeof(long), WellKnownPropertySet.IMAPMsg, "PopImap:PopMIMEOptions");

		// Token: 0x040044AC RID: 17580
		public static readonly GuidNamePropertyDefinition ImapMIMESize = InternalSchema.CreateGuidNameProperty("ImapMIMESize", typeof(long), WellKnownPropertySet.IMAPMsg, "PopImap:ImapMIMESize");

		// Token: 0x040044AD RID: 17581
		public static readonly GuidNamePropertyDefinition ImapMIMEOptions = InternalSchema.CreateGuidNameProperty("ImapMIMEOptions", typeof(long), WellKnownPropertySet.IMAPMsg, "PopImap:ImapMIMEOptions");

		// Token: 0x040044AE RID: 17582
		public static readonly GuidNamePropertyDefinition ImapLastUidFixTime = InternalSchema.CreateGuidNameProperty("ImapLastUidFixTime", typeof(ExDateTime), WellKnownPropertySet.IMAPMsg, "PopImap:ImapLastUidFixTime");

		// Token: 0x040044AF RID: 17583
		public static readonly PropertyTagPropertyDefinition ExpiryTime = PropertyTagPropertyDefinition.InternalCreate("ExpiryTime", PropTag.ExpiryTime);

		// Token: 0x040044B0 RID: 17584
		public static readonly PropertyTagPropertyDefinition IsHidden = PropertyTagPropertyDefinition.InternalCreate("IsHidden", (PropTag)284426251U);

		// Token: 0x040044B1 RID: 17585
		public static readonly PropertyTagPropertyDefinition SystemFolderFlags = PropertyTagPropertyDefinition.InternalCreate("SystemFolderFlags", PropTag.SystemFolderFlags);

		// Token: 0x040044B2 RID: 17586
		public static readonly PropertyTagPropertyDefinition LocallyDelivered = PropertyTagPropertyDefinition.InternalCreate("LocallyDelivered", (PropTag)1732575490U);

		// Token: 0x040044B3 RID: 17587
		public static readonly GuidNamePropertyDefinition XLoop = InternalSchema.CreateGuidNameProperty("XLoop", typeof(string[]), WellKnownPropertySet.Messaging, "XLoop");

		// Token: 0x040044B4 RID: 17588
		public static readonly GuidNamePropertyDefinition DoNotDeliver = InternalSchema.CreateGuidNameProperty("DoNotDeliver", typeof(string), WellKnownPropertySet.InternetHeaders, "X-MS-Exchange-Organization-Test-Message");

		// Token: 0x040044B5 RID: 17589
		public static readonly GuidNamePropertyDefinition DropMessageInHub = InternalSchema.CreateGuidNameProperty("DropMessageInHub", typeof(string), WellKnownPropertySet.InternetHeaders, "X-Exchange-Probe-Drop-Message");

		// Token: 0x040044B6 RID: 17590
		public static readonly GuidNamePropertyDefinition SystemProbeDrop = InternalSchema.CreateGuidNameProperty("SystemProbeDrop", typeof(string), WellKnownPropertySet.InternetHeaders, "X-Exchange-System-Probe-Drop");

		// Token: 0x040044B7 RID: 17591
		public static readonly GuidNamePropertyDefinition XLAMNotificationId = InternalSchema.CreateGuidNameProperty("XLAMNotificationId", typeof(string), WellKnownPropertySet.InternetHeaders, "X-LAMNotificationId");

		// Token: 0x040044B8 RID: 17592
		public static readonly GuidNamePropertyDefinition MapiSubmitLamNotificationId = InternalSchema.CreateGuidNameProperty("MapiSubmitLamNotificationId", typeof(string), WellKnownPropertySet.Messaging, "MapiSubmitLamNotificationId");

		// Token: 0x040044B9 RID: 17593
		public static readonly GuidNamePropertyDefinition MapiSubmitSystemProbeActivityId = InternalSchema.CreateGuidNameProperty("MapiSubmitSystemProbeActivityId", typeof(Guid), WellKnownPropertySet.Messaging, "MapiSubmitSystemProbeActivityId");

		// Token: 0x040044BA RID: 17594
		public static readonly GuidNamePropertyDefinition ResentFrom = InternalSchema.CreateGuidNameProperty("ResentFrom", typeof(string), WellKnownPropertySet.InternetHeaders, "resent-from");

		// Token: 0x040044BB RID: 17595
		public static readonly GuidNamePropertyDefinition OriginalSentTimeForEscalation = InternalSchema.CreateGuidNameProperty("OriginalSentTimeForEscalation", typeof(ExDateTime), WellKnownPropertySet.Messaging, "OriginalSentTimeForEscalation");

		// Token: 0x040044BC RID: 17596
		public static readonly PropertyTagPropertyDefinition MessageAnnotation = PropertyTagPropertyDefinition.InternalCreate("MessageAnnotation", PropTag.MessageAnnotation);

		// Token: 0x040044BD RID: 17597
		public static readonly PropertyTagPropertyDefinition OriginalAuthorName = PropertyTagPropertyDefinition.InternalCreate("OriginalAuthorName", PropTag.OriginalAuthorName);

		// Token: 0x040044BE RID: 17598
		public static readonly PropertyTagPropertyDefinition OriginalSensitivity = PropertyTagPropertyDefinition.InternalCreate("OriginalSensitivity", PropTag.OriginalSensitivity);

		// Token: 0x040044BF RID: 17599
		public static readonly PropertyTagPropertyDefinition ReceivedRepresentingAddressType = PropertyTagPropertyDefinition.InternalCreate("ReceivedRepresentingAddressType", PropTag.RcvdRepresentingAddrType, new PropertyDefinitionConstraint[]
		{
			new NonMoveMailboxPropertyConstraint(new StringLengthConstraint(0, 9))
		});

		// Token: 0x040044C0 RID: 17600
		public static readonly PropertyTagPropertyDefinition ReceivedRepresentingDisplayName = PropertyTagPropertyDefinition.InternalCreate("ReceivedRepresentingDisplayName", PropTag.RcvdRepresentingName);

		// Token: 0x040044C1 RID: 17601
		public static readonly PropertyTagPropertyDefinition ReceivedRepresentingEmailAddress = PropertyTagPropertyDefinition.InternalCreate("ReceivedRepresentingEmailAddress", PropTag.RcvdRepresentingEmailAddress);

		// Token: 0x040044C2 RID: 17602
		public static readonly PropertyTagPropertyDefinition ReceivedRepresentingSearchKey = PropertyTagPropertyDefinition.InternalCreate("ReceivedRepresentingSearchKey", PropTag.RcvdRepresentingSearchKey);

		// Token: 0x040044C3 RID: 17603
		public static readonly PropertyTagPropertyDefinition ReceivedRepresentingEntryId = PropertyTagPropertyDefinition.InternalCreate("ReceivedRepresentingEntryId", PropTag.RcvdRepresentingEntryId);

		// Token: 0x040044C4 RID: 17604
		public static readonly PropertyTagPropertyDefinition EventEmailReminderTimer = PropertyTagPropertyDefinition.InternalCreate("EventEmailReminderTimer", PropTag.EventEmailReminderTimer);

		// Token: 0x040044C5 RID: 17605
		public static readonly PropertyTagPropertyDefinition AttachCalendarFlags = PropertyTagPropertyDefinition.InternalCreate("AttachFlags", (PropTag)2147287043U);

		// Token: 0x040044C6 RID: 17606
		public static readonly PropertyTagPropertyDefinition AttachCalendarHidden = PropertyTagPropertyDefinition.InternalCreate("AttachHidden", (PropTag)2147352587U);

		// Token: 0x040044C7 RID: 17607
		public static readonly PropertyTagPropertyDefinition AttachCalendarLinkId = PropertyTagPropertyDefinition.InternalCreate("AttachLinkId", (PropTag)2147090435U);

		// Token: 0x040044C8 RID: 17608
		public static readonly PropertyTagPropertyDefinition AttachContentBase = PropertyTagPropertyDefinition.InternalCreate("AttachContentBase", (PropTag)923861023U);

		// Token: 0x040044C9 RID: 17609
		public static readonly PropertyTagPropertyDefinition AttachContentId = PropertyTagPropertyDefinition.InternalCreate("AttachContentId", (PropTag)923926559U);

		// Token: 0x040044CA RID: 17610
		public static readonly PropertyTagPropertyDefinition AttachContentLocation = PropertyTagPropertyDefinition.InternalCreate("AttachContentLocation", PropTag.AttachContentLocation);

		// Token: 0x040044CB RID: 17611
		public static readonly PropertyTagPropertyDefinition AttachEncoding = PropertyTagPropertyDefinition.InternalCreate("AttachEncoding", PropTag.AttachEncoding);

		// Token: 0x040044CC RID: 17612
		public static readonly PropertyTagPropertyDefinition AttachExtension = PropertyTagPropertyDefinition.InternalCreate("AttachExtension", PropTag.AttachExtension);

		// Token: 0x040044CD RID: 17613
		public static readonly PropertyTagPropertyDefinition AttachFileName = PropertyTagPropertyDefinition.InternalCreate("AttachFileName", PropTag.AttachFileName);

		// Token: 0x040044CE RID: 17614
		public static readonly PropertyTagPropertyDefinition AttachLongFileName = PropertyTagPropertyDefinition.InternalCreate("AttachLongFileName", PropTag.AttachLongFileName);

		// Token: 0x040044CF RID: 17615
		public static readonly PropertyTagPropertyDefinition AttachLongPathName = PropertyTagPropertyDefinition.InternalCreate("AttachLongPathName", PropTag.AttachLongPathName);

		// Token: 0x040044D0 RID: 17616
		public static readonly PropertyTagPropertyDefinition AttachAdditionalInfo = PropertyTagPropertyDefinition.InternalCreate("AttachAdditionalInfo", PropTag.AttachAdditionalInfo);

		// Token: 0x040044D1 RID: 17617
		public static readonly PropertyTagPropertyDefinition AttachMethod = PropertyTagPropertyDefinition.InternalCreate("AttachMethod", PropTag.AttachMethod);

		// Token: 0x040044D2 RID: 17618
		public static readonly PropertyTagPropertyDefinition AttachMhtmlFlags = PropertyTagPropertyDefinition.InternalCreate("AttachMhtmlFlags", PropTag.AttachFlags);

		// Token: 0x040044D3 RID: 17619
		public static readonly PropertyTagPropertyDefinition AttachMimeTag = PropertyTagPropertyDefinition.InternalCreate("AttachMimeTag", PropTag.AttachMimeTag);

		// Token: 0x040044D4 RID: 17620
		public static readonly PropertyTagPropertyDefinition AttachNum = PropertyTagPropertyDefinition.InternalCreate("AttachNum", PropTag.AttachNum);

		// Token: 0x040044D5 RID: 17621
		public static readonly PropertyTagPropertyDefinition AttachInConflict = PropertyTagPropertyDefinition.InternalCreate("AttachInConflict", (PropTag)1718353931U);

		// Token: 0x040044D6 RID: 17622
		public static readonly GuidIdPropertyDefinition SideEffects = InternalSchema.CreateGuidIdProperty("SideEffects", typeof(int), WellKnownPropertySet.Common, 34064);

		// Token: 0x040044D7 RID: 17623
		public static readonly PropertyTagPropertyDefinition AttachSize = PropertyTagPropertyDefinition.InternalCreate("AttachSize", PropTag.AttachSize);

		// Token: 0x040044D8 RID: 17624
		public static readonly PropertyTagPropertyDefinition AttachTransportName = PropertyTagPropertyDefinition.InternalCreate("AttachTransportName", PropTag.AttachTransportName);

		// Token: 0x040044D9 RID: 17625
		[MessageClassSpecific("FixMe")]
		public static readonly PropertyTagPropertyDefinition AppointmentTombstones = PropertyTagPropertyDefinition.InternalCreate("PR_SCHDINFO_APPT_TOMBSTONE", (PropTag)1751777538U);

		// Token: 0x040044DA RID: 17626
		public static readonly GuidIdPropertyDefinition AllAttachmentsHidden = InternalSchema.CreateGuidIdProperty("AllAttachmentsHidden", typeof(bool), WellKnownPropertySet.Common, 34068);

		// Token: 0x040044DB RID: 17627
		public static readonly PropertyTagPropertyDefinition Not822Renderable = PropertyTagPropertyDefinition.InternalCreate("Not822Renderable", (PropTag)1733492747U);

		// Token: 0x040044DC RID: 17628
		public static readonly PropertyTagPropertyDefinition RenderingPosition = PropertyTagPropertyDefinition.InternalCreate("RenderingPosition", PropTag.RenderingPosition);

		// Token: 0x040044DD RID: 17629
		public static readonly GuidNamePropertyDefinition NamedContentType = InternalSchema.CreateGuidNameProperty("NamedContentType", typeof(string), WellKnownPropertySet.InternetHeaders, "content-type");

		// Token: 0x040044DE RID: 17630
		public static readonly PropertyTagPropertyDefinition FailedInboundICalAsAttachment = PropertyTagPropertyDefinition.InternalCreate("FailedInboundICalAsAttachment", (PropTag)924581899U);

		// Token: 0x040044DF RID: 17631
		public static readonly GuidIdPropertyDefinition FreeBusyStatus = InternalSchema.CreateGuidIdProperty("FreeBusyStatus", typeof(int), WellKnownPropertySet.Appointment, 33285);

		// Token: 0x040044E0 RID: 17632
		public static readonly GuidIdPropertyDefinition ResponseState = InternalSchema.CreateGuidIdProperty("ResponseState", typeof(int), WellKnownPropertySet.Meeting, 33);

		// Token: 0x040044E1 RID: 17633
		public static readonly GuidIdPropertyDefinition MapiResponseType = InternalSchema.CreateGuidIdProperty("ResponseType", typeof(int), WellKnownPropertySet.Appointment, 33304);

		// Token: 0x040044E2 RID: 17634
		public static readonly GuidNamePropertyDefinition BodyContentBase = InternalSchema.CreateGuidNameProperty("BodyContentBase", typeof(string), WellKnownPropertySet.InternetHeaders, "Content-Base");

		// Token: 0x040044E3 RID: 17635
		public static readonly PropertyTagPropertyDefinition BodyContentId = PropertyTagPropertyDefinition.InternalCreate("BodyContentId", (PropTag)269811743U);

		// Token: 0x040044E4 RID: 17636
		public static readonly PropertyTagPropertyDefinition BodyContentLocation = PropertyTagPropertyDefinition.InternalCreate("BodyContentLocation", (PropTag)269746207U);

		// Token: 0x040044E5 RID: 17637
		public static readonly PropertyTagPropertyDefinition Codepage = PropertyTagPropertyDefinition.InternalCreate("Codepage", (PropTag)1073545219U, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 65535)
		});

		// Token: 0x040044E6 RID: 17638
		public static readonly PropertyTagPropertyDefinition HtmlBody = PropertyTagPropertyDefinition.InternalCreate("HtmlBody", PropTag.Html, PropertyFlags.Streamable);

		// Token: 0x040044E7 RID: 17639
		public static readonly PropertyTagPropertyDefinition RtfBody = PropertyTagPropertyDefinition.InternalCreate("RtfBody", PropTag.RtfCompressed, PropertyFlags.Streamable);

		// Token: 0x040044E8 RID: 17640
		public static readonly PropertyTagPropertyDefinition RtfInSync = PropertyTagPropertyDefinition.InternalCreate("RtfInSync", PropTag.RtfInSync);

		// Token: 0x040044E9 RID: 17641
		public static readonly PropertyTagPropertyDefinition RtfSyncBodyCount = PropertyTagPropertyDefinition.InternalCreate("RtfSyncBodyCount", PropTag.RtfSyncBodyCount);

		// Token: 0x040044EA RID: 17642
		public static readonly PropertyTagPropertyDefinition RtfSyncBodyCrc = PropertyTagPropertyDefinition.InternalCreate("RtfSyncBodyCrc", PropTag.RtfSyncBodyCrc);

		// Token: 0x040044EB RID: 17643
		public static readonly PropertyTagPropertyDefinition RtfSyncBodyTag = PropertyTagPropertyDefinition.InternalCreate("RtfSyncBodyTag", PropTag.RtfSyncBodyTag);

		// Token: 0x040044EC RID: 17644
		public static readonly PropertyTagPropertyDefinition RtfSyncPrefixCount = PropertyTagPropertyDefinition.InternalCreate("RtfSyncPrefixCount", PropTag.RtfSyncPrefixCount);

		// Token: 0x040044ED RID: 17645
		public static readonly PropertyTagPropertyDefinition RtfSyncTrailingCount = PropertyTagPropertyDefinition.InternalCreate("RtfSyncTrailingCount", PropTag.RtfSyncTrailingCount);

		// Token: 0x040044EE RID: 17646
		public static readonly PropertyTagPropertyDefinition TextBody = PropertyTagPropertyDefinition.InternalCreate("TextBody", PropTag.Body, PropertyFlags.Streamable);

		// Token: 0x040044EF RID: 17647
		public static readonly PropertyTagPropertyDefinition NativeBodyInfo = PropertyTagPropertyDefinition.InternalCreate("NativeBodyInfo", PropTag.NativeBodyInfo, PropertyFlags.ReadOnly);

		// Token: 0x040044F0 RID: 17648
		public static readonly PropertyTagPropertyDefinition SendOutlookRecallReport = PropertyTagPropertyDefinition.InternalCreate("SendOutlookRecallReport", (PropTag)1745027083U);

		// Token: 0x040044F1 RID: 17649
		internal static readonly PropertyTagPropertyDefinition InternalAccess = PropertyTagPropertyDefinition.InternalCreate("InternalAccess", PropTag.InternalAccess);

		// Token: 0x040044F2 RID: 17650
		public static readonly PropertyTagPropertyDefinition RawFreeBusySecurityDescriptor = PropertyTagPropertyDefinition.InternalCreate("FreeBusySecurityDescriptor", PropTag.FreeBusyNTSD, PropertyFlags.Streamable);

		// Token: 0x040044F3 RID: 17651
		public static readonly StorePropertyDefinition FreeBusySecurityDescriptor = new SecurityDescriptorProperty(InternalSchema.RawFreeBusySecurityDescriptor);

		// Token: 0x040044F4 RID: 17652
		public static readonly GuidNamePropertyDefinition BirthdayCalendarFolderEntryId = InternalSchema.CreateGuidNameProperty("BirthdayCalendarFolderEntryId", typeof(byte[]), WellKnownPropertySet.Common, "BirthdayCalendarFolderEntryId");

		// Token: 0x040044F5 RID: 17653
		public static readonly GuidIdPropertyDefinition ReminderDueByInternal = InternalSchema.CreateGuidIdProperty("ReminderDueByInternal", typeof(ExDateTime), WellKnownPropertySet.Common, 34050);

		// Token: 0x040044F6 RID: 17654
		public static readonly GuidIdPropertyDefinition ReminderIsSetInternal = InternalSchema.CreateGuidIdProperty("ReminderIsSetInternal", typeof(bool), WellKnownPropertySet.Common, 34051);

		// Token: 0x040044F7 RID: 17655
		public static readonly GuidIdPropertyDefinition NonSendableTo = InternalSchema.CreateGuidIdProperty("NonSendableTo", typeof(string), WellKnownPropertySet.Common, 34102);

		// Token: 0x040044F8 RID: 17656
		public static readonly GuidIdPropertyDefinition NonSendableCC = InternalSchema.CreateGuidIdProperty("NonSendableCC", typeof(string), WellKnownPropertySet.Common, 34103);

		// Token: 0x040044F9 RID: 17657
		public static readonly GuidIdPropertyDefinition NonSendableBCC = InternalSchema.CreateGuidIdProperty("NonSendableBCC", typeof(string), WellKnownPropertySet.Common, 34104);

		// Token: 0x040044FA RID: 17658
		public static readonly GuidIdPropertyDefinition NonSendToTrackStatus = InternalSchema.CreateGuidIdProperty("NonSendToTrackStatus", typeof(int[]), WellKnownPropertySet.Common, 34115);

		// Token: 0x040044FB RID: 17659
		public static readonly GuidIdPropertyDefinition NonSendCCTrackStatus = InternalSchema.CreateGuidIdProperty("NonSendCCTrackStatus", typeof(int[]), WellKnownPropertySet.Common, 34116);

		// Token: 0x040044FC RID: 17660
		public static readonly GuidIdPropertyDefinition NonSendBCCTrackStatus = InternalSchema.CreateGuidIdProperty("NonSendBCCTrackStatus", typeof(int[]), WellKnownPropertySet.Common, 34117);

		// Token: 0x040044FD RID: 17661
		public static readonly GuidIdPropertyDefinition ReminderNextTime = InternalSchema.CreateGuidIdProperty("ReminderNextTime", typeof(ExDateTime), WellKnownPropertySet.Common, 34144);

		// Token: 0x040044FE RID: 17662
		public static readonly GuidIdPropertyDefinition ReminderMinutesBeforeStartInternal = InternalSchema.CreateGuidIdProperty("ReminderMinutesBeforeStartInternal", typeof(int), WellKnownPropertySet.Common, 34049);

		// Token: 0x040044FF RID: 17663
		public static readonly GuidIdPropertyDefinition AppointmentClass = InternalSchema.CreateGuidIdProperty("AppointmentClass", typeof(string), WellKnownPropertySet.Meeting, 36);

		// Token: 0x04004500 RID: 17664
		public static readonly GuidIdPropertyDefinition AppointmentColor = InternalSchema.CreateGuidIdProperty("AppointmentColor", typeof(int), WellKnownPropertySet.Appointment, 33300);

		// Token: 0x04004501 RID: 17665
		public static readonly PropertyTagPropertyDefinition AppointmentExceptionEndTime = PropertyTagPropertyDefinition.InternalCreate("AppointmentExceptionEndTime", (PropTag)2147221568U);

		// Token: 0x04004502 RID: 17666
		public static readonly PropertyTagPropertyDefinition AppointmentExceptionStartTime = PropertyTagPropertyDefinition.InternalCreate("AppointmentExceptionStartTime", (PropTag)2147156032U);

		// Token: 0x04004503 RID: 17667
		public static readonly GuidIdPropertyDefinition AppointmentStateInternal = InternalSchema.CreateGuidIdProperty("AppointmentStateInternal", typeof(int), WellKnownPropertySet.Appointment, 33303, PropertyFlags.TrackChange, new PropertyDefinitionConstraint[0]);

		// Token: 0x04004504 RID: 17668
		public static readonly GuidIdPropertyDefinition AppointmentAuxiliaryFlags = InternalSchema.CreateGuidIdProperty("AppointmentAuxiliaryFlags", typeof(int), WellKnownPropertySet.Appointment, 33287);

		// Token: 0x04004505 RID: 17669
		public static readonly GuidIdPropertyDefinition AppointmentLastSequenceNumber = InternalSchema.CreateGuidIdProperty("AppointmentLastSequenceNumber", typeof(int), WellKnownPropertySet.Appointment, 33283);

		// Token: 0x04004506 RID: 17670
		public static readonly GuidNamePropertyDefinition CdoSequenceNumber = InternalSchema.CreateGuidNameProperty("CdoSequenceNumber", typeof(int), WellKnownPropertySet.PublicStrings, "urn:schemas:calendar:sequence");

		// Token: 0x04004507 RID: 17671
		public static readonly GuidIdPropertyDefinition AppointmentRecurrenceBlob = InternalSchema.CreateGuidIdProperty("AppointmentRecurrenceBlob", typeof(byte[]), WellKnownPropertySet.Appointment, 33302, PropertyFlags.Streamable, new PropertyDefinitionConstraint[0]);

		// Token: 0x04004508 RID: 17672
		public static readonly GuidIdPropertyDefinition AppointmentRecurring = InternalSchema.CreateGuidIdProperty("AppointmentRecurring", typeof(bool), WellKnownPropertySet.Appointment, 33315);

		// Token: 0x04004509 RID: 17673
		public static readonly GuidIdPropertyDefinition AppointmentReplyTime = InternalSchema.CreateGuidIdProperty("AppointmentReplyTime", typeof(ExDateTime), WellKnownPropertySet.Appointment, 33312);

		// Token: 0x0400450A RID: 17674
		public static readonly GuidIdPropertyDefinition AppointmentSequenceNumber = InternalSchema.CreateGuidIdProperty("AppointmentSequenceNumber", typeof(int), WellKnownPropertySet.Appointment, 33281);

		// Token: 0x0400450B RID: 17675
		public static readonly GuidIdPropertyDefinition AppointmentSequenceTime = InternalSchema.CreateGuidIdProperty("AppointmentSequenceTime", typeof(ExDateTime), WellKnownPropertySet.Appointment, 33282);

		// Token: 0x0400450C RID: 17676
		public static readonly GuidIdPropertyDefinition AppointmentExtractVersion = InternalSchema.CreateGuidIdProperty("AppointmentExtractVersion", typeof(long), WellKnownPropertySet.Appointment, 33324);

		// Token: 0x0400450D RID: 17677
		public static readonly GuidIdPropertyDefinition AppointmentExtractTime = InternalSchema.CreateGuidIdProperty("AppointmentExtractTime", typeof(ExDateTime), WellKnownPropertySet.Appointment, 33325);

		// Token: 0x0400450E RID: 17678
		public static readonly GuidIdPropertyDefinition ConferenceType = InternalSchema.CreateGuidIdProperty("ConferenceType", typeof(int), WellKnownPropertySet.Appointment, 33345);

		// Token: 0x0400450F RID: 17679
		public static readonly GuidIdPropertyDefinition DisallowNewTimeProposal = InternalSchema.CreateGuidIdProperty("DisallowNewTimeProposal", typeof(bool), WellKnownPropertySet.Appointment, 33370);

		// Token: 0x04004510 RID: 17680
		public static readonly GuidIdPropertyDefinition Duration = InternalSchema.CreateGuidIdProperty("Duration", typeof(int), WellKnownPropertySet.Appointment, 33299);

		// Token: 0x04004511 RID: 17681
		public static readonly GuidIdPropertyDefinition IsOnlineMeeting = InternalSchema.CreateGuidIdProperty("IsOnlineMeeting", typeof(bool), WellKnownPropertySet.Appointment, 33344);

		// Token: 0x04004512 RID: 17682
		public static readonly GuidIdPropertyDefinition LidWhere = InternalSchema.CreateGuidIdProperty("LID_WHERE", typeof(string), WellKnownPropertySet.Meeting, 2);

		// Token: 0x04004513 RID: 17683
		public static readonly GuidIdPropertyDefinition SeriesCreationHash = InternalSchema.CreateGuidIdProperty("SeriesCreationHash", typeof(int), WellKnownPropertySet.CalendarAssistant, 34146);

		// Token: 0x04004514 RID: 17684
		public static readonly GuidNamePropertyDefinition SeriesMasterId = InternalSchema.CreateGuidNameProperty("SeriesMasterId", typeof(string), WellKnownPropertySet.Appointment, "SeriesMasterId");

		// Token: 0x04004515 RID: 17685
		public static readonly GuidNamePropertyDefinition SeriesId = InternalSchema.CreateGuidNameProperty("SeriesId", typeof(string), WellKnownPropertySet.Appointment, "SeriesId");

		// Token: 0x04004516 RID: 17686
		public static readonly GuidNamePropertyDefinition EventClientId = InternalSchema.CreateGuidNameProperty("EventClientId", typeof(string), WellKnownPropertySet.Appointment, "EventClientId");

		// Token: 0x04004517 RID: 17687
		public static readonly GuidNamePropertyDefinition SeriesReminderIsSet = InternalSchema.CreateGuidNameProperty("SeriesReminderIsSet", typeof(bool), WellKnownPropertySet.Appointment, "SeriesReminderIsSet");

		// Token: 0x04004518 RID: 17688
		public static readonly GuidNamePropertyDefinition InstanceCreationIndex = InternalSchema.CreateGuidNameProperty("InstanceCreationIndex", typeof(int), WellKnownPropertySet.Appointment, "InstanceCreationIndex");

		// Token: 0x04004519 RID: 17689
		public static readonly GuidNamePropertyDefinition IsHiddenFromLegacyClients = InternalSchema.CreateGuidNameProperty("IsHiddenFromLegacyClients", typeof(bool), WellKnownPropertySet.Appointment, "IsHiddenFromLegacyClients");

		// Token: 0x0400451A RID: 17690
		public static readonly GuidNamePropertyDefinition OccurrencesExceptionalViewProperties = InternalSchema.CreateGuidNameProperty("OccurrencesExceptionalViewProperties", typeof(string), WellKnownPropertySet.CalendarAssistant, "OccurrencesExceptionalViewProperties", PropertyFlags.Streamable, new PropertyDefinitionConstraint[0]);

		// Token: 0x0400451B RID: 17691
		public static readonly GuidNamePropertyDefinition SeriesSequenceNumber = InternalSchema.CreateGuidNameProperty("SeriesSequenceNumber", typeof(int), WellKnownPropertySet.CalendarAssistant, "SeriesSequenceNumber");

		// Token: 0x0400451C RID: 17692
		public static readonly GuidNamePropertyDefinition PropertyChangeMetadataProcessingFlags = InternalSchema.CreateGuidNameProperty("PropertyChangeMetadataProcessingFlags", typeof(int), WellKnownPropertySet.CalendarAssistant, "PropertyChangeMetadataProcessingFlags");

		// Token: 0x0400451D RID: 17693
		public static readonly GuidNamePropertyDefinition MasterGlobalObjectId = InternalSchema.CreateGuidNameProperty("MasterGlobalObjectId", typeof(byte[]), WellKnownPropertySet.CalendarAssistant, "MasterGlobalObjectId");

		// Token: 0x0400451E RID: 17694
		public static readonly GuidNamePropertyDefinition ParkedCorrelationId = InternalSchema.CreateGuidNameProperty("ParkedCorrelationId", typeof(string), WellKnownPropertySet.CalendarAssistant, "ParkedCorrelationId");

		// Token: 0x0400451F RID: 17695
		public static readonly GuidIdPropertyDefinition GlobalObjectId = InternalSchema.CreateGuidIdProperty("GlobalObjectId", typeof(byte[]), WellKnownPropertySet.Meeting, 3);

		// Token: 0x04004520 RID: 17696
		public static readonly GuidIdPropertyDefinition IsSilent = InternalSchema.CreateGuidIdProperty("IsSilent", typeof(bool), WellKnownPropertySet.Meeting, 4);

		// Token: 0x04004521 RID: 17697
		public static readonly GuidIdPropertyDefinition IsRecurring = InternalSchema.CreateGuidIdProperty("IsRecurring", typeof(bool), WellKnownPropertySet.Meeting, 5);

		// Token: 0x04004522 RID: 17698
		public static readonly GuidIdPropertyDefinition CleanGlobalObjectId = InternalSchema.CreateGuidIdProperty("CleanGlobalObjectId", typeof(byte[]), WellKnownPropertySet.Meeting, 35);

		// Token: 0x04004523 RID: 17699
		internal static readonly GuidIdPropertyDefinition MeetingUniqueId = InternalSchema.CreateGuidIdProperty("MeetingUniqueId", typeof(byte[]), WellKnownPropertySet.Meeting, 46);

		// Token: 0x04004524 RID: 17700
		public static readonly GuidIdPropertyDefinition Location = InternalSchema.CreateGuidIdProperty("Location", typeof(string), WellKnownPropertySet.Appointment, 33288);

		// Token: 0x04004525 RID: 17701
		public static readonly GuidIdPropertyDefinition MapiEndTime = InternalSchema.CreateGuidIdProperty("MapiEndTime", typeof(ExDateTime), WellKnownPropertySet.Appointment, 33294);

		// Token: 0x04004526 RID: 17702
		public static readonly GuidIdPropertyDefinition MapiStartTime = InternalSchema.CreateGuidIdProperty("MapiStartTime", typeof(ExDateTime), WellKnownPropertySet.Appointment, 33293);

		// Token: 0x04004527 RID: 17703
		public static readonly PropertyTagPropertyDefinition MapiPRStartDate = PropertyTagPropertyDefinition.InternalCreate("PR_START_DATE", PropTag.StartDate);

		// Token: 0x04004528 RID: 17704
		public static readonly PropertyTagPropertyDefinition MapiPREndDate = PropertyTagPropertyDefinition.InternalCreate("PR_END_DATE", PropTag.EndDate);

		// Token: 0x04004529 RID: 17705
		public static readonly GuidIdPropertyDefinition MapiIsAllDayEvent = InternalSchema.CreateGuidIdProperty("MapiIsAllDayEvent", typeof(bool), WellKnownPropertySet.Appointment, 33301);

		// Token: 0x0400452A RID: 17706
		public static readonly GuidIdPropertyDefinition MeetingRequestWasSent = InternalSchema.CreateGuidIdProperty("MeetingRequestWasSent", typeof(bool), WellKnownPropertySet.Appointment, 33321);

		// Token: 0x0400452B RID: 17707
		public static readonly GuidIdPropertyDefinition MeetingWorkspaceUrl = InternalSchema.CreateGuidIdProperty("MeetingWorkspaceUrl", typeof(string), WellKnownPropertySet.Appointment, 33289);

		// Token: 0x0400452C RID: 17708
		public static readonly GuidIdPropertyDefinition NetShowURL = InternalSchema.CreateGuidIdProperty("NetShowURL", typeof(string), WellKnownPropertySet.Appointment, 33352);

		// Token: 0x0400452D RID: 17709
		public static readonly GuidIdPropertyDefinition OnlineMeetingChanged = InternalSchema.CreateGuidIdProperty("OnlinePropertyChanged", typeof(bool), WellKnownPropertySet.Appointment, 33343);

		// Token: 0x0400452E RID: 17710
		public static readonly GuidIdPropertyDefinition RecurrencePattern = InternalSchema.CreateGuidIdProperty("RecurrencePattern", typeof(string), WellKnownPropertySet.Appointment, 33330);

		// Token: 0x0400452F RID: 17711
		public static readonly GuidIdPropertyDefinition MapiRecurrenceType = InternalSchema.CreateGuidIdProperty("MapiRecurrenceType", typeof(int), WellKnownPropertySet.Appointment, 33329);

		// Token: 0x04004530 RID: 17712
		public static readonly GuidIdPropertyDefinition TimeZone = InternalSchema.CreateGuidIdProperty("TimeZone", typeof(string), WellKnownPropertySet.Appointment, 33332);

		// Token: 0x04004531 RID: 17713
		public static readonly GuidIdPropertyDefinition TimeZoneBlob = InternalSchema.CreateGuidIdProperty("TimeZoneBlob", typeof(byte[]), WellKnownPropertySet.Appointment, 33331);

		// Token: 0x04004532 RID: 17714
		public static readonly GuidIdPropertyDefinition TimeZoneDefinitionStart = InternalSchema.CreateGuidIdProperty("TimeZoneDefinitionStart", typeof(byte[]), WellKnownPropertySet.Appointment, 33374);

		// Token: 0x04004533 RID: 17715
		public static readonly GuidIdPropertyDefinition TimeZoneDefinitionEnd = InternalSchema.CreateGuidIdProperty("TimeZoneDefinitionEnd", typeof(byte[]), WellKnownPropertySet.Appointment, 33375);

		// Token: 0x04004534 RID: 17716
		public static readonly GuidIdPropertyDefinition TimeZoneDefinitionRecurring = InternalSchema.CreateGuidIdProperty("TimeZoneDefinitionRecurring", typeof(byte[]), WellKnownPropertySet.Appointment, 33376);

		// Token: 0x04004535 RID: 17717
		internal static readonly GuidIdPropertyDefinition ClipStartTime = InternalSchema.CreateGuidIdProperty("ClipStartTime", typeof(ExDateTime), WellKnownPropertySet.Appointment, 33333);

		// Token: 0x04004536 RID: 17718
		internal static readonly GuidIdPropertyDefinition ClipEndTime = InternalSchema.CreateGuidIdProperty("ClipEndTime", typeof(ExDateTime), WellKnownPropertySet.Appointment, 33334);

		// Token: 0x04004537 RID: 17719
		internal static readonly GuidIdPropertyDefinition OriginalStoreEntryId = InternalSchema.CreateGuidIdProperty("OriginalStoreEntryId", typeof(byte[]), WellKnownPropertySet.Appointment, 33335);

		// Token: 0x04004538 RID: 17720
		public static readonly GuidIdPropertyDefinition When = InternalSchema.CreateGuidIdProperty("When", typeof(string), WellKnownPropertySet.Meeting, 34);

		// Token: 0x04004539 RID: 17721
		public static readonly GuidIdPropertyDefinition ExceptionReplaceTime = InternalSchema.CreateGuidIdProperty("ExceptionReplaceTime", typeof(ExDateTime), WellKnownPropertySet.Appointment, 33320);

		// Token: 0x0400453A RID: 17722
		public static readonly GuidNamePropertyDefinition UCOpenedConferenceID = InternalSchema.CreateGuidNameProperty("UCOpenedConferenceID", typeof(string), WellKnownPropertySet.PublicStrings, "UCOpenedConferenceID");

		// Token: 0x0400453B RID: 17723
		public static readonly GuidNamePropertyDefinition OnlineMeetingExternalLink = InternalSchema.CreateGuidNameProperty("OnlineMeetingExternalLink", typeof(string), WellKnownPropertySet.PublicStrings, "OnlineMeetingExternalLink");

		// Token: 0x0400453C RID: 17724
		public static readonly GuidNamePropertyDefinition OnlineMeetingInternalLink = InternalSchema.CreateGuidNameProperty("OnlineMeetingInternalLink", typeof(string), WellKnownPropertySet.PublicStrings, "OnlineMeetingInternalLink");

		// Token: 0x0400453D RID: 17725
		public static readonly GuidNamePropertyDefinition OnlineMeetingConfLink = InternalSchema.CreateGuidNameProperty("OnlineMeetingConfLink", typeof(string), WellKnownPropertySet.PublicStrings, "OnlineMeetingConfLink");

		// Token: 0x0400453E RID: 17726
		public static readonly GuidNamePropertyDefinition UCCapabilities = InternalSchema.CreateGuidNameProperty("UCCapabilities", typeof(string), WellKnownPropertySet.PublicStrings, "UCCapabilities", PropertyFlags.Streamable, new PropertyDefinitionConstraint[0]);

		// Token: 0x0400453F RID: 17727
		public static readonly GuidNamePropertyDefinition UCInband = InternalSchema.CreateGuidNameProperty("UCInband", typeof(string), WellKnownPropertySet.PublicStrings, "UCInband", PropertyFlags.Streamable, new PropertyDefinitionConstraint[0]);

		// Token: 0x04004540 RID: 17728
		public static readonly GuidNamePropertyDefinition UCMeetingSetting = InternalSchema.CreateGuidNameProperty("UCMeetingSetting", typeof(string), WellKnownPropertySet.PublicStrings, "UCMeetingSetting", PropertyFlags.Streamable, new PropertyDefinitionConstraint[0]);

		// Token: 0x04004541 RID: 17729
		public static readonly GuidNamePropertyDefinition UCMeetingSettingSent = InternalSchema.CreateGuidNameProperty("UCMeetingSettingSent", typeof(string), WellKnownPropertySet.PublicStrings, "UCMeetingSettingSent", PropertyFlags.Streamable, new PropertyDefinitionConstraint[0]);

		// Token: 0x04004542 RID: 17730
		public static readonly GuidNamePropertyDefinition ConferenceTelURI = InternalSchema.CreateGuidNameProperty("ConferenceTelURI", typeof(string), WellKnownPropertySet.PublicStrings, "ConferenceTelURI");

		// Token: 0x04004543 RID: 17731
		public static readonly GuidNamePropertyDefinition ConferenceInfo = InternalSchema.CreateGuidNameProperty("ConferenceInfo", typeof(string), WellKnownPropertySet.PublicStrings, "ConferenceInfo");

		// Token: 0x04004544 RID: 17732
		public static readonly PropertyTagPropertyDefinition AttachPayloadProviderGuidString = PropertyTagPropertyDefinition.InternalCreate("AttachPayloadProviderGuidString", (PropTag)924385311U);

		// Token: 0x04004545 RID: 17733
		public static readonly PropertyTagPropertyDefinition AttachPayloadClass = PropertyTagPropertyDefinition.InternalCreate("AttachPayloadProviderClass", (PropTag)924450847U);

		// Token: 0x04004546 RID: 17734
		public static readonly GuidIdPropertyDefinition IsException = InternalSchema.CreateGuidIdProperty("IsException", typeof(bool), WellKnownPropertySet.Meeting, 10);

		// Token: 0x04004547 RID: 17735
		public static readonly GuidNamePropertyDefinition AllContactsFolderEntryId = InternalSchema.CreateGuidNameProperty("AllContactsFolderEntryId", typeof(byte[]), WellKnownPropertySet.Address, "AllContactsFolderEntryId");

		// Token: 0x04004548 RID: 17736
		public static readonly GuidNamePropertyDefinition MyContactsFolderEntryId = InternalSchema.CreateGuidNameProperty("MyContactsFolderEntryId", typeof(byte[]), WellKnownPropertySet.Address, "MyContactsFolderEntryId");

		// Token: 0x04004549 RID: 17737
		public static readonly GuidNamePropertyDefinition MyContactsExtendedFolderEntryId = InternalSchema.CreateGuidNameProperty("MyContactsExtendedFolderEntryId", typeof(byte[]), WellKnownPropertySet.Address, "MyContactsExtendedFolderEntryId");

		// Token: 0x0400454A RID: 17738
		public static readonly GuidNamePropertyDefinition MyContactsFoldersInternal = InternalSchema.CreateGuidNameProperty("MyContactsFoldersInternal", typeof(byte[][]), WellKnownPropertySet.Address, "MyContactsFolders");

		// Token: 0x0400454B RID: 17739
		public static readonly StoreObjectIdCollectionProperty MyContactsFolders = new StoreObjectIdCollectionProperty(InternalSchema.MyContactsFoldersInternal, PropertyFlags.None, "MyContactsFolders");

		// Token: 0x0400454C RID: 17740
		public static readonly GuidNamePropertyDefinition PeopleConnectFolderEntryId = InternalSchema.CreateGuidNameProperty("PeopleConnectFolderEntryId", typeof(byte[]), WellKnownPropertySet.Address, "PeopleConnectFolderEntryId");

		// Token: 0x0400454D RID: 17741
		public static readonly GuidNamePropertyDefinition FavoritesFolderEntryId = InternalSchema.CreateGuidNameProperty("FavoritesFolderEntryId", typeof(byte[]), WellKnownPropertySet.Address, "FavoritesFolderEntryId");

		// Token: 0x0400454E RID: 17742
		public static readonly GuidNamePropertyDefinition FromFavoriteSendersFolderEntryId = InternalSchema.CreateGuidNameProperty("FromFavoriteSendersFolderEntryId", typeof(byte[]), WellKnownPropertySet.Address, "FromFavoriteSendersFolderEntryId");

		// Token: 0x0400454F RID: 17743
		public static readonly GuidNamePropertyDefinition FromPeopleFolderEntryId = InternalSchema.CreateGuidNameProperty("FromPeopleFolderEntryId", typeof(byte[]), WellKnownPropertySet.Address, "FromPeopleFolderEntryId");

		// Token: 0x04004550 RID: 17744
		public static readonly GuidNamePropertyDefinition MailboxAssociationFolderEntryId = InternalSchema.CreateGuidNameProperty("MailboxAssociationFolderEntryId", typeof(byte[]), WellKnownPropertySet.Common, "MailboxAssociationFolderEntryId");

		// Token: 0x04004551 RID: 17745
		public static readonly PropertyTagPropertyDefinition AssistantName = PropertyTagPropertyDefinition.InternalCreate("AssistantName", PropTag.Assistant);

		// Token: 0x04004552 RID: 17746
		public static readonly PropertyTagPropertyDefinition AssistantPhoneNumber = PropertyTagPropertyDefinition.InternalCreate("AssistantPhoneNumber", PropTag.AssistantTelephoneNumber);

		// Token: 0x04004553 RID: 17747
		public static readonly GuidIdPropertyDefinition BillingInformation = InternalSchema.CreateGuidIdProperty("BillingInformation", typeof(string), WellKnownPropertySet.Common, 34101);

		// Token: 0x04004554 RID: 17748
		public static readonly GuidIdPropertyDefinition BirthdayLocal = InternalSchema.CreateGuidIdProperty("BirthdayLocal", typeof(ExDateTime), WellKnownPropertySet.Address, 32990);

		// Token: 0x04004555 RID: 17749
		public static readonly PropertyTagPropertyDefinition Birthday = PropertyTagPropertyDefinition.InternalCreate("Birthday", PropTag.Birthday);

		// Token: 0x04004556 RID: 17750
		public static readonly GuidNamePropertyDefinition NotInBirthdayCalendar = InternalSchema.CreateGuidNameProperty("NotInBirthdayCalendar", typeof(bool), WellKnownPropertySet.Address, "ContactNotInBirthdayCalendar");

		// Token: 0x04004557 RID: 17751
		public static readonly GuidNamePropertyDefinition IsBirthdayContactWritable = InternalSchema.CreateGuidNameProperty("IsBirthdayContactWritable", typeof(bool), WellKnownPropertySet.Address, "IsBirthdayContactWritable");

		// Token: 0x04004558 RID: 17752
		public static readonly GuidNamePropertyDefinition BirthdayContactEntryId = InternalSchema.CreateGuidNameProperty("BirthdayContactEntryId", typeof(byte[]), WellKnownPropertySet.Address, "BirthdayContactEntryId");

		// Token: 0x04004559 RID: 17753
		public static readonly GuidNamePropertyDefinition BirthdayContactAttributionDisplayName = InternalSchema.CreateGuidNameProperty("BirthdayContactAttributionDisplayName", typeof(string), WellKnownPropertySet.Address, "BirthdayContactAttributionDisplayName");

		// Token: 0x0400455A RID: 17754
		public static readonly PropertyTagPropertyDefinition BusinessHomePage = PropertyTagPropertyDefinition.InternalCreate("BusinessHomePage", PropTag.BusinessHomePage);

		// Token: 0x0400455B RID: 17755
		public static readonly PropertyTagPropertyDefinition BusinessPhoneNumber = PropertyTagPropertyDefinition.InternalCreate("BusinessPhoneNumber", PropTag.BusinessTelephoneNumber);

		// Token: 0x0400455C RID: 17756
		public static readonly PropertyTagPropertyDefinition BusinessPhoneNumber2 = PropertyTagPropertyDefinition.InternalCreate("BusinessPhoneNumber2", PropTag.Business2TelephoneNumber);

		// Token: 0x0400455D RID: 17757
		public static readonly PropertyTagPropertyDefinition CallbackPhone = PropertyTagPropertyDefinition.InternalCreate("CallbackPhone", PropTag.CallbackTelephoneNumber);

		// Token: 0x0400455E RID: 17758
		public static readonly PropertyTagPropertyDefinition CarPhone = PropertyTagPropertyDefinition.InternalCreate("CarPhone", PropTag.CarTelephoneNumber);

		// Token: 0x0400455F RID: 17759
		public static readonly PropertyTagPropertyDefinition Children = PropertyTagPropertyDefinition.InternalCreate("Children", PropTag.ChildrensNames);

		// Token: 0x04004560 RID: 17760
		public static readonly GuidIdPropertyDefinition Companies = InternalSchema.CreateGuidIdProperty("Companies", typeof(string[]), WellKnownPropertySet.Common, 34105);

		// Token: 0x04004561 RID: 17761
		public static readonly PropertyTagPropertyDefinition CompanyName = PropertyTagPropertyDefinition.InternalCreate("CompanyName", PropTag.CompanyName);

		// Token: 0x04004562 RID: 17762
		public static readonly PropertyTagPropertyDefinition Department = PropertyTagPropertyDefinition.InternalCreate("Department", PropTag.DepartmentName);

		// Token: 0x04004563 RID: 17763
		public static readonly GuidIdPropertyDefinition Email1AddrType = InternalSchema.CreateGuidIdProperty("Email1AddrType", typeof(string), WellKnownPropertySet.Address, 32898, PropertyFlags.None, new PropertyDefinitionConstraint[]
		{
			new NonMoveMailboxPropertyConstraint(new StringLengthConstraint(0, 9))
		});

		// Token: 0x04004564 RID: 17764
		public static readonly GuidIdPropertyDefinition Email1DisplayName = InternalSchema.CreateGuidIdProperty("Email1DisplayName", typeof(string), WellKnownPropertySet.Address, 32896);

		// Token: 0x04004565 RID: 17765
		public static readonly GuidIdPropertyDefinition Email1EmailAddress = InternalSchema.CreateGuidIdProperty("Email1EmailAddress", typeof(string), WellKnownPropertySet.Address, 32899);

		// Token: 0x04004566 RID: 17766
		public static readonly GuidIdPropertyDefinition Email1OriginalDisplayName = InternalSchema.CreateGuidIdProperty("Email1OriginalDisplayName", typeof(string), WellKnownPropertySet.Address, 32900);

		// Token: 0x04004567 RID: 17767
		public static readonly GuidIdPropertyDefinition Email1OriginalEntryID = InternalSchema.CreateGuidIdProperty("Email1OriginalEntryID", typeof(byte[]), WellKnownPropertySet.Address, 32901);

		// Token: 0x04004568 RID: 17768
		public static readonly GuidIdPropertyDefinition Email2AddrType = InternalSchema.CreateGuidIdProperty("Email2AddrType", typeof(string), WellKnownPropertySet.Address, 32914, PropertyFlags.None, new PropertyDefinitionConstraint[]
		{
			new NonMoveMailboxPropertyConstraint(new StringLengthConstraint(0, 9))
		});

		// Token: 0x04004569 RID: 17769
		public static readonly GuidIdPropertyDefinition Email2DisplayName = InternalSchema.CreateGuidIdProperty("Email2DisplayName", typeof(string), WellKnownPropertySet.Address, 32912);

		// Token: 0x0400456A RID: 17770
		public static readonly GuidIdPropertyDefinition Email2EmailAddress = InternalSchema.CreateGuidIdProperty("Email2EmailAddress", typeof(string), WellKnownPropertySet.Address, 32915);

		// Token: 0x0400456B RID: 17771
		public static readonly GuidIdPropertyDefinition Email2OriginalDisplayName = InternalSchema.CreateGuidIdProperty("Email2OriginalDisplayName", typeof(string), WellKnownPropertySet.Address, 32916);

		// Token: 0x0400456C RID: 17772
		public static readonly GuidIdPropertyDefinition Email2OriginalEntryID = InternalSchema.CreateGuidIdProperty("Email2OriginalEntryID", typeof(byte[]), WellKnownPropertySet.Address, 32917);

		// Token: 0x0400456D RID: 17773
		public static readonly GuidIdPropertyDefinition Email3AddrType = InternalSchema.CreateGuidIdProperty("Email3AddrType", typeof(string), WellKnownPropertySet.Address, 32930, PropertyFlags.None, new PropertyDefinitionConstraint[]
		{
			new NonMoveMailboxPropertyConstraint(new StringLengthConstraint(0, 9))
		});

		// Token: 0x0400456E RID: 17774
		public static readonly GuidIdPropertyDefinition Email3DisplayName = InternalSchema.CreateGuidIdProperty("Email3DisplayName", typeof(string), WellKnownPropertySet.Address, 32928);

		// Token: 0x0400456F RID: 17775
		public static readonly GuidIdPropertyDefinition Email3EmailAddress = InternalSchema.CreateGuidIdProperty("Email3EmailAddress", typeof(string), WellKnownPropertySet.Address, 32931);

		// Token: 0x04004570 RID: 17776
		public static readonly GuidIdPropertyDefinition Email3OriginalDisplayName = InternalSchema.CreateGuidIdProperty("Email3OriginalDisplayName", typeof(string), WellKnownPropertySet.Address, 32932);

		// Token: 0x04004571 RID: 17777
		public static readonly GuidIdPropertyDefinition Email3OriginalEntryID = InternalSchema.CreateGuidIdProperty("Email3OriginalEntryID", typeof(byte[]), WellKnownPropertySet.Address, 32933);

		// Token: 0x04004572 RID: 17778
		public static readonly GuidIdPropertyDefinition Fax1AddrType = InternalSchema.CreateGuidIdProperty("Fax1AddrType", typeof(string), WellKnownPropertySet.Address, 32946, PropertyFlags.None, new PropertyDefinitionConstraint[]
		{
			new NonMoveMailboxPropertyConstraint(new StringLengthConstraint(0, 9))
		});

		// Token: 0x04004573 RID: 17779
		public static readonly GuidIdPropertyDefinition Fax1EmailAddress = InternalSchema.CreateGuidIdProperty("Fax1EmailAddress", typeof(string), WellKnownPropertySet.Address, 32947);

		// Token: 0x04004574 RID: 17780
		public static readonly GuidIdPropertyDefinition Fax1OriginalDisplayName = InternalSchema.CreateGuidIdProperty("Fax1OriginalDisplayName", typeof(string), WellKnownPropertySet.Address, 32948);

		// Token: 0x04004575 RID: 17781
		public static readonly GuidIdPropertyDefinition Fax1OriginalEntryID = InternalSchema.CreateGuidIdProperty("Fax1OriginalEntryID", typeof(byte[]), WellKnownPropertySet.Address, 32949);

		// Token: 0x04004576 RID: 17782
		public static readonly GuidIdPropertyDefinition Fax2AddrType = InternalSchema.CreateGuidIdProperty("Fax2AddrType", typeof(string), WellKnownPropertySet.Address, 32962, PropertyFlags.None, new PropertyDefinitionConstraint[]
		{
			new NonMoveMailboxPropertyConstraint(new StringLengthConstraint(0, 9))
		});

		// Token: 0x04004577 RID: 17783
		public static readonly GuidIdPropertyDefinition Fax2EmailAddress = InternalSchema.CreateGuidIdProperty("Fax2EmailAddress", typeof(string), WellKnownPropertySet.Address, 32963);

		// Token: 0x04004578 RID: 17784
		public static readonly GuidIdPropertyDefinition Fax2OriginalDisplayName = InternalSchema.CreateGuidIdProperty("Fax2OriginalDisplayName", typeof(string), WellKnownPropertySet.Address, 32964);

		// Token: 0x04004579 RID: 17785
		public static readonly GuidIdPropertyDefinition Fax2OriginalEntryID = InternalSchema.CreateGuidIdProperty("Fax2OriginalEntryID", typeof(byte[]), WellKnownPropertySet.Address, 32965);

		// Token: 0x0400457A RID: 17786
		public static readonly GuidIdPropertyDefinition Fax3AddrType = InternalSchema.CreateGuidIdProperty("Fax3AddrType", typeof(string), WellKnownPropertySet.Address, 32978, PropertyFlags.None, new PropertyDefinitionConstraint[]
		{
			new NonMoveMailboxPropertyConstraint(new StringLengthConstraint(0, 9))
		});

		// Token: 0x0400457B RID: 17787
		public static readonly GuidIdPropertyDefinition Fax3EmailAddress = InternalSchema.CreateGuidIdProperty("Fax3EmailAddress", typeof(string), WellKnownPropertySet.Address, 32979);

		// Token: 0x0400457C RID: 17788
		public static readonly GuidIdPropertyDefinition Fax3OriginalDisplayName = InternalSchema.CreateGuidIdProperty("Fax3OriginalDisplayName", typeof(string), WellKnownPropertySet.Address, 32980);

		// Token: 0x0400457D RID: 17789
		public static readonly GuidIdPropertyDefinition Fax3OriginalEntryID = InternalSchema.CreateGuidIdProperty("Fax3OriginalEntryID", typeof(byte[]), WellKnownPropertySet.Address, 32981);

		// Token: 0x0400457E RID: 17790
		public static readonly PropertyTagPropertyDefinition EmailAddress = PropertyTagPropertyDefinition.InternalCreate("EmailAddress", PropTag.EmailAddress);

		// Token: 0x0400457F RID: 17791
		public static readonly PropertyTagPropertyDefinition FaxNumber = PropertyTagPropertyDefinition.InternalCreate("FaxNumber", PropTag.BusinessFaxNumber);

		// Token: 0x04004580 RID: 17792
		public static readonly GuidNamePropertyDefinition Transparent = InternalSchema.CreateGuidNameProperty("Transparent", typeof(string), WellKnownPropertySet.PublicStrings, "urn:schemas:calendar:transparent");

		// Token: 0x04004581 RID: 17793
		public static readonly GuidIdPropertyDefinition FileAsStringInternal = InternalSchema.CreateGuidIdProperty("FileAsInternal", typeof(string), WellKnownPropertySet.Address, 32773);

		// Token: 0x04004582 RID: 17794
		public static readonly GuidIdPropertyDefinition FileAsId = InternalSchema.CreateGuidIdProperty("FileAsId", typeof(int), WellKnownPropertySet.Address, 32774);

		// Token: 0x04004583 RID: 17795
		public static readonly PropertyTagPropertyDefinition GivenName = PropertyTagPropertyDefinition.InternalCreate("GivenName", PropTag.GivenName, new PropertyDefinitionConstraint[]
		{
			new NonMoveMailboxPropertyConstraint(new StringLengthConstraint(0, 256))
		});

		// Token: 0x04004584 RID: 17796
		public static readonly PropertyTagPropertyDefinition HomePostOfficeBox = PropertyTagPropertyDefinition.InternalCreate("HomePostOfficeBox", PropTag.HomeAddressPostOfficeBox);

		// Token: 0x04004585 RID: 17797
		public static readonly PropertyTagPropertyDefinition HomeCity = PropertyTagPropertyDefinition.InternalCreate("HomeCity", PropTag.HomeAddressCity);

		// Token: 0x04004586 RID: 17798
		public static readonly PropertyTagPropertyDefinition HomeCountry = PropertyTagPropertyDefinition.InternalCreate("HomeCountry", PropTag.HomeAddressCountry);

		// Token: 0x04004587 RID: 17799
		public static readonly PropertyTagPropertyDefinition HomeFax = PropertyTagPropertyDefinition.InternalCreate("HomeFax", PropTag.HomeFaxNumber);

		// Token: 0x04004588 RID: 17800
		public static readonly PropertyTagPropertyDefinition HomePhone = PropertyTagPropertyDefinition.InternalCreate("HomePhone", PropTag.HomeTelephoneNumber);

		// Token: 0x04004589 RID: 17801
		public static readonly PropertyTagPropertyDefinition HomePhone2 = PropertyTagPropertyDefinition.InternalCreate("HomePhone2", PropTag.Home2TelephoneNumber);

		// Token: 0x0400458A RID: 17802
		public static readonly PropertyTagPropertyDefinition HomePostalCode = PropertyTagPropertyDefinition.InternalCreate("HomePostalCode", PropTag.HomeAddressPostalCode);

		// Token: 0x0400458B RID: 17803
		public static readonly PropertyTagPropertyDefinition HomeState = PropertyTagPropertyDefinition.InternalCreate("HomeState", PropTag.HomeAddressStateOrProvince);

		// Token: 0x0400458C RID: 17804
		public static readonly PropertyTagPropertyDefinition HomeStreet = PropertyTagPropertyDefinition.InternalCreate("HomeStreet", PropTag.HomeAddressStreet);

		// Token: 0x0400458D RID: 17805
		public static readonly GuidIdPropertyDefinition IMAddress = InternalSchema.CreateGuidIdProperty("IMAddress", typeof(string), WellKnownPropertySet.Address, 32866);

		// Token: 0x0400458E RID: 17806
		public static GuidNamePropertyDefinition IMAddress2 = InternalSchema.CreateGuidNameProperty("IMAddress2", typeof(string), WellKnownPropertySet.AirSync, "AirSync:IMAddress2");

		// Token: 0x0400458F RID: 17807
		public static GuidNamePropertyDefinition IMAddress3 = InternalSchema.CreateGuidNameProperty("IMAddress3", typeof(string), WellKnownPropertySet.AirSync, "AirSync:IMAddress3");

		// Token: 0x04004590 RID: 17808
		public static readonly GuidNamePropertyDefinition ClientCategoryList = InternalSchema.CreateGuidNameProperty("ClientCategoryList", typeof(int[]), WellKnownPropertySet.AirSync, "AirSync:ClientCategoryList");

		// Token: 0x04004591 RID: 17809
		public static readonly GuidNamePropertyDefinition LastSeenClientIds = InternalSchema.CreateGuidNameProperty("LastSeenClientIds", typeof(string[]), WellKnownPropertySet.AirSync, "AirSync:LastSeenClientIds");

		// Token: 0x04004592 RID: 17810
		public static readonly GuidNamePropertyDefinition AirSyncSyncKey = InternalSchema.CreateGuidNameProperty("AirSyncSynckey", typeof(int), WellKnownPropertySet.AirSync, "AirSync:AirSyncSynckey");

		// Token: 0x04004593 RID: 17811
		public static readonly GuidNamePropertyDefinition AirSyncFilter = InternalSchema.CreateGuidNameProperty("AirSyncFilter", typeof(int), WellKnownPropertySet.AirSync, "AirSync:AirSyncFilter");

		// Token: 0x04004594 RID: 17812
		public static readonly GuidNamePropertyDefinition AirSyncConversationMode = InternalSchema.CreateGuidNameProperty("AirSyncConversationMode", typeof(bool), WellKnownPropertySet.AirSync, "AirSync:AirSyncConversationMode");

		// Token: 0x04004595 RID: 17813
		public static readonly GuidNamePropertyDefinition AirSyncSettingsHash = InternalSchema.CreateGuidNameProperty("AirSyncSettingsHash", typeof(int), WellKnownPropertySet.AirSync, "AirSync:AirSyncSettingsHash");

		// Token: 0x04004596 RID: 17814
		public static readonly GuidNamePropertyDefinition AirSyncMaxItems = InternalSchema.CreateGuidNameProperty("AirSyncMaxItems", typeof(int), WellKnownPropertySet.AirSync, "AirSync:AirSyncMaxItems");

		// Token: 0x04004597 RID: 17815
		public static readonly GuidNamePropertyDefinition AirSyncDeletedCountTotal = InternalSchema.CreateGuidNameProperty("AirSyncDeletedCountTotal", typeof(int), WellKnownPropertySet.AirSync, "AirSync:AirSyncDeletedCountTotal");

		// Token: 0x04004598 RID: 17816
		public static readonly GuidNamePropertyDefinition AirSyncLastSyncTime = InternalSchema.CreateGuidNameProperty("AirSyncLastSyncTime", typeof(long), WellKnownPropertySet.AirSync, "AirSync:AirSyncLastSyncTime");

		// Token: 0x04004599 RID: 17817
		public static readonly GuidNamePropertyDefinition AirSyncLocalCommitTimeMax = InternalSchema.CreateGuidNameProperty("AirSyncLocalCommitTimeMax", typeof(long), WellKnownPropertySet.AirSync, "AirSync:AirSyncLocalCommitTimeMax");

		// Token: 0x0400459A RID: 17818
		public static readonly GuidNamePropertyDefinition LastSyncAttemptTime = InternalSchema.CreateGuidNameProperty("LastSyncAttemptTime", typeof(ExDateTime), WellKnownPropertySet.AirSync, "AirSync:LastSyncAttemptTime");

		// Token: 0x0400459B RID: 17819
		public static readonly GuidNamePropertyDefinition LastSyncSuccessTime = InternalSchema.CreateGuidNameProperty("LastSyncSuccessTime", typeof(ExDateTime), WellKnownPropertySet.AirSync, "AirSync:LastSyncSuccessTime");

		// Token: 0x0400459C RID: 17820
		public static readonly GuidNamePropertyDefinition LastSyncUserAgent = InternalSchema.CreateGuidNameProperty("LastSyncUserAgent", typeof(string), WellKnownPropertySet.AirSync, "AirSync:LastSyncUserAgent");

		// Token: 0x0400459D RID: 17821
		public static readonly GuidNamePropertyDefinition LastPingHeartbeatInterval = InternalSchema.CreateGuidNameProperty("LastPingHeartbeatInterval", typeof(int), WellKnownPropertySet.AirSync, "AirSync:LastPingHeartbeatInterval");

		// Token: 0x0400459E RID: 17822
		public static readonly GuidNamePropertyDefinition DeviceBlockedUntil = InternalSchema.CreateGuidNameProperty("DeviceBlockedUntil", typeof(ExDateTime), WellKnownPropertySet.AirSync, "AirSync:DeviceBlockedUntil");

		// Token: 0x0400459F RID: 17823
		public static readonly GuidNamePropertyDefinition DeviceBlockedAt = InternalSchema.CreateGuidNameProperty("DeviceBlockedAt", typeof(ExDateTime), WellKnownPropertySet.AirSync, "AirSync:DeviceBlockedAt");

		// Token: 0x040045A0 RID: 17824
		public static readonly GuidNamePropertyDefinition DeviceBlockedReason = InternalSchema.CreateGuidNameProperty("DeviceBlockedReason", typeof(string), WellKnownPropertySet.AirSync, "AirSync:DeviceBlockedReason");

		// Token: 0x040045A1 RID: 17825
		public static readonly GuidIdPropertyDefinition Linked = InternalSchema.CreateGuidIdProperty("Linked", typeof(bool), WellKnownPropertySet.Address, 32992);

		// Token: 0x040045A2 RID: 17826
		public static readonly GuidIdPropertyDefinition AddressBookEntryId = InternalSchema.CreateGuidIdProperty("AddressBookEntryId", typeof(byte[]), WellKnownPropertySet.Address, 32994);

		// Token: 0x040045A3 RID: 17827
		public static readonly GuidIdPropertyDefinition SmtpAddressCache = InternalSchema.CreateGuidIdProperty("SmtpAddressCache", typeof(string[]), WellKnownPropertySet.Address, 32995);

		// Token: 0x040045A4 RID: 17828
		public static readonly GuidIdPropertyDefinition LinkADID = InternalSchema.CreateGuidIdProperty("LinkADID", typeof(Guid), WellKnownPropertySet.Address, 32996);

		// Token: 0x040045A5 RID: 17829
		public static readonly GuidIdPropertyDefinition LinkRejectHistoryRaw = InternalSchema.CreateGuidIdProperty("LinkRejectHistoryRaw", typeof(byte[][]), WellKnownPropertySet.Address, 32997);

		// Token: 0x040045A6 RID: 17830
		public static readonly GuidIdPropertyDefinition GALLinkState = InternalSchema.CreateGuidIdProperty("GALLinkState", typeof(int), WellKnownPropertySet.Address, 32998);

		// Token: 0x040045A7 RID: 17831
		public static readonly GuidIdPropertyDefinition GALLinkID = InternalSchema.CreateGuidIdProperty("GALLinkID", typeof(Guid), WellKnownPropertySet.Address, 33000);

		// Token: 0x040045A8 RID: 17832
		public static readonly GuidIdPropertyDefinition UserApprovedLink = InternalSchema.CreateGuidIdProperty("UserApprovedLink", typeof(bool), WellKnownPropertySet.Address, 33003);

		// Token: 0x040045A9 RID: 17833
		public static readonly GuidNamePropertyDefinition LinkChangeHistory = InternalSchema.CreateGuidNameProperty("LinkChangeHistory", typeof(string), WellKnownPropertySet.Address, "LinkChangeHistory");

		// Token: 0x040045AA RID: 17834
		public static readonly GuidNamePropertyDefinition TelUri = InternalSchema.CreateGuidNameProperty("TelURI", typeof(string), WellKnownPropertySet.PublicStrings, "TelURI");

		// Token: 0x040045AB RID: 17835
		public static readonly GuidNamePropertyDefinition ImContactSipUriAddress = InternalSchema.CreateGuidNameProperty("ImContactSipUriAddress", typeof(string), WellKnownPropertySet.Address, "ImContactSipUriAddress");

		// Token: 0x040045AC RID: 17836
		public static readonly GuidIdPropertyDefinition OutlookContactLinkDateTime = InternalSchema.CreateGuidIdProperty("OutlookContactLinkDateTime", typeof(ExDateTime), WellKnownPropertySet.PublicStrings, 32993);

		// Token: 0x040045AD RID: 17837
		public static readonly GuidIdPropertyDefinition OutlookContactLinkVersion = InternalSchema.CreateGuidIdProperty("OutlookContactLinkVersion", typeof(long), WellKnownPropertySet.PublicStrings, 32999);

		// Token: 0x040045AE RID: 17838
		public static readonly GuidNamePropertyDefinition MailboxAssociationExternalId = InternalSchema.CreateGuidNameProperty("MailboxAssociationExternalId", typeof(string), WellKnownPropertySet.Common, "MailboxAssociationExternalId");

		// Token: 0x040045AF RID: 17839
		public static readonly GuidNamePropertyDefinition MailboxAssociationLegacyDN = InternalSchema.CreateGuidNameProperty("MailboxAssociationLegacyDN", typeof(string), WellKnownPropertySet.Common, "MailboxAssociationLegacyDN");

		// Token: 0x040045B0 RID: 17840
		public static readonly GuidNamePropertyDefinition MailboxAssociationIsMember = InternalSchema.CreateGuidNameProperty("MailboxAssociationIsMember", typeof(bool), WellKnownPropertySet.Common, "MailboxAssociationIsMember");

		// Token: 0x040045B1 RID: 17841
		public static readonly GuidNamePropertyDefinition MailboxAssociationShouldEscalate = InternalSchema.CreateGuidNameProperty("MailboxAssociationShouldEscalate", typeof(bool), WellKnownPropertySet.Common, "MailboxAssociationShouldEscalate");

		// Token: 0x040045B2 RID: 17842
		public static readonly GuidNamePropertyDefinition MailboxAssociationIsAutoSubscribed = InternalSchema.CreateGuidNameProperty("MailboxAssociationIsAutoSubscribed", typeof(bool), WellKnownPropertySet.Common, "MailboxAssociationIsAutoSubscribed");

		// Token: 0x040045B3 RID: 17843
		public static readonly GuidNamePropertyDefinition MailboxAssociationSmtpAddress = InternalSchema.CreateGuidNameProperty("MailboxAssociationSmtpAddress", typeof(string), WellKnownPropertySet.Common, "MailboxAssociationSmtpAddress");

		// Token: 0x040045B4 RID: 17844
		public static readonly GuidNamePropertyDefinition MailboxAssociationJoinedBy = InternalSchema.CreateGuidNameProperty("MailboxAssociationJoinedBy", typeof(string), WellKnownPropertySet.Common, "MailboxAssociationJoinedBy");

		// Token: 0x040045B5 RID: 17845
		public static readonly GuidNamePropertyDefinition MailboxAssociationIsPin = InternalSchema.CreateGuidNameProperty("MailboxAssociationIsPin", typeof(bool), WellKnownPropertySet.Common, "MailboxAssociationIsPin");

		// Token: 0x040045B6 RID: 17846
		public static readonly GuidNamePropertyDefinition MailboxAssociationJoinDate = InternalSchema.CreateGuidNameProperty("MailboxAssociationJoinDate", typeof(ExDateTime), WellKnownPropertySet.Common, "MailboxAssociationJoinDate");

		// Token: 0x040045B7 RID: 17847
		public static readonly GuidNamePropertyDefinition MailboxAssociationLastVisitedDate = InternalSchema.CreateGuidNameProperty("MailboxAssociationLastVisitedDate", typeof(ExDateTime), WellKnownPropertySet.Common, "MailboxAssociationLastVisitedDate");

		// Token: 0x040045B8 RID: 17848
		public static readonly GuidNamePropertyDefinition MailboxAssociationPinDate = InternalSchema.CreateGuidNameProperty("MailboxAssociationPinDate", typeof(ExDateTime), WellKnownPropertySet.Common, "MailboxAssociationPinDate");

		// Token: 0x040045B9 RID: 17849
		public static readonly GuidNamePropertyDefinition MailboxAssociationSyncedIdentityHash = InternalSchema.CreateGuidNameProperty("MailboxAssociationSyncedIdentityHash", typeof(string), WellKnownPropertySet.Common, "MailboxAssociationSyncedIdentityHash");

		// Token: 0x040045BA RID: 17850
		public static readonly GuidNamePropertyDefinition MailboxAssociationCurrentVersion = InternalSchema.CreateGuidNameProperty("MailboxAssociationCurrentVersion", typeof(int), WellKnownPropertySet.Common, "MailboxAssociationCurrentVersion");

		// Token: 0x040045BB RID: 17851
		public static readonly GuidNamePropertyDefinition MailboxAssociationSyncedVersion = InternalSchema.CreateGuidNameProperty("MailboxAssociationSyncedVersion", typeof(int), WellKnownPropertySet.Common, "MailboxAssociationSyncedVersion");

		// Token: 0x040045BC RID: 17852
		public static readonly GuidNamePropertyDefinition MailboxAssociationLastSyncError = InternalSchema.CreateGuidNameProperty("MailboxAssociationLastSyncError", typeof(string), WellKnownPropertySet.Common, "MailboxAssociationLastSyncError");

		// Token: 0x040045BD RID: 17853
		public static readonly GuidNamePropertyDefinition MailboxAssociationSyncAttempts = InternalSchema.CreateGuidNameProperty("MailboxAssociationSyncAttempts", typeof(int), WellKnownPropertySet.Common, "MailboxAssociationSyncAttempts");

		// Token: 0x040045BE RID: 17854
		public static readonly GuidNamePropertyDefinition MailboxAssociationSyncedSchemaVersion = InternalSchema.CreateGuidNameProperty("MailboxAssociationSyncedSchemaVersion", typeof(string), WellKnownPropertySet.Common, "MailboxAssociationSyncedSchemaVersion");

		// Token: 0x040045BF RID: 17855
		public static readonly PropertyTagPropertyDefinition ControlDataForSearchIndexRepairAssistant = PropertyTagPropertyDefinition.InternalCreate("ControlDataForSearchIndexRepairAssistant", PropTag.ControlDataForSearchIndexRepairAssistant);

		// Token: 0x040045C0 RID: 17856
		public static readonly PropertyTagPropertyDefinition ControlDataForGroupMailboxAssistant = PropertyTagPropertyDefinition.InternalCreate("ControlDataForGroupMailboxAssistant", PropTag.ControlDataForGroupMailboxAssistant);

		// Token: 0x040045C1 RID: 17857
		public static readonly PropertyTagPropertyDefinition ControlDataForMailboxAssociationReplicationAssistant = PropertyTagPropertyDefinition.InternalCreate("ControlDataForMailboxAssociationReplicationAssistant", PropTag.ControlDataForMailboxAssociationReplicationAssistant);

		// Token: 0x040045C2 RID: 17858
		public static readonly PropertyTagPropertyDefinition MailboxAssociationNextReplicationTime = PropertyTagPropertyDefinition.InternalCreate("MailboxAssociationNextReplicationTime", PropTag.MailboxAssociationNextReplicationTime);

		// Token: 0x040045C3 RID: 17859
		public static readonly PropertyTagPropertyDefinition MailboxAssociationProcessingFlags = PropertyTagPropertyDefinition.InternalCreate("MailboxAssociationProcessingFlags", PropTag.MailboxAssociationProcessingFlags);

		// Token: 0x040045C4 RID: 17860
		public static readonly PropertyTagPropertyDefinition GroupMailboxPermissionsVersion = PropertyTagPropertyDefinition.InternalCreate("GroupMailboxPermissionsVersion", PropTag.GroupMailboxPermissionsVersion);

		// Token: 0x040045C5 RID: 17861
		public static readonly PropertyTagPropertyDefinition GroupMailboxGeneratedPhotoSignature = PropertyTagPropertyDefinition.InternalCreate("GroupMailboxGeneratedPhotoSignature", PropTag.GroupMailboxGeneratedPhotoSignature);

		// Token: 0x040045C6 RID: 17862
		public static readonly PropertyTagPropertyDefinition GroupMailboxGeneratedPhotoVersion = PropertyTagPropertyDefinition.InternalCreate("GroupMailboxGeneratedPhotoVersion", PropTag.GroupMailboxGeneratedPhotoVersion);

		// Token: 0x040045C7 RID: 17863
		public static readonly PropertyTagPropertyDefinition GroupMailboxExchangeResourcesPublishedVersion = PropertyTagPropertyDefinition.InternalCreate("GroupMailboxExchangeResourcesPublishedVersion", PropTag.GroupMailboxExchangeResourcesPublishedVersion);

		// Token: 0x040045C8 RID: 17864
		public static readonly GuidNamePropertyDefinition HierarchySyncLastAttemptedSyncTime = InternalSchema.CreateGuidNameProperty("HierarchySyncLastAttemptedSyncTime", typeof(ExDateTime), WellKnownPropertySet.Common, "HierarchySyncLastAttemptedSyncTime");

		// Token: 0x040045C9 RID: 17865
		public static readonly GuidNamePropertyDefinition HierarchySyncLastFailedSyncTime = InternalSchema.CreateGuidNameProperty("HierarchySyncLastFailedSyncTime", typeof(ExDateTime), WellKnownPropertySet.Common, "HierarchySyncLastFailedSyncTime");

		// Token: 0x040045CA RID: 17866
		public static readonly GuidNamePropertyDefinition HierarchySyncLastSuccessfulSyncTime = InternalSchema.CreateGuidNameProperty("HierarchySyncLastSuccessfulSyncTime", typeof(ExDateTime), WellKnownPropertySet.Common, "HierarchySyncLastSuccessfulSyncTime");

		// Token: 0x040045CB RID: 17867
		public static readonly GuidNamePropertyDefinition HierarchySyncFirstFailedSyncTimeAfterLastSuccess = InternalSchema.CreateGuidNameProperty("HierarchySyncFirstFailedSyncTimeAfterLastSuccess", typeof(ExDateTime), WellKnownPropertySet.Common, "HierarchySyncFirstFailedSyncTimeAfterLastSuccess");

		// Token: 0x040045CC RID: 17868
		public static readonly GuidNamePropertyDefinition HierarchySyncLastSyncFailure = InternalSchema.CreateGuidNameProperty("HierarchySyncLastSyncFailure", typeof(string), WellKnownPropertySet.Common, "HierarchySyncLastSyncFailure");

		// Token: 0x040045CD RID: 17869
		public static readonly GuidNamePropertyDefinition HierarchySyncNumberOfAttemptsAfterLastSuccess = InternalSchema.CreateGuidNameProperty("HierarchySyncNumberOfAttemptsAfterLastSuccess", typeof(int), WellKnownPropertySet.Common, "HierarchySyncNumberOfAttemptsAfterLastSuccess");

		// Token: 0x040045CE RID: 17870
		public static readonly GuidNamePropertyDefinition HierarchySyncNumberOfBatchesExecuted = InternalSchema.CreateGuidNameProperty("HierarchySyncNumberOfBatchesExecuted", typeof(int), WellKnownPropertySet.Common, "HierarchySyncNumberOfBatchesExecuted");

		// Token: 0x040045CF RID: 17871
		public static readonly GuidNamePropertyDefinition HierarchySyncNumberOfFoldersSynced = InternalSchema.CreateGuidNameProperty("HierarchySyncNumberOfFoldersSynced", typeof(int), WellKnownPropertySet.Common, "HierarchySyncNumberOfFoldersSynced");

		// Token: 0x040045D0 RID: 17872
		public static readonly GuidNamePropertyDefinition HierarchySyncNumberOfFoldersToBeSynced = InternalSchema.CreateGuidNameProperty("HierarchySyncNumberOfFoldersToBeSynced", typeof(int), WellKnownPropertySet.Common, "HierarchySyncNumberOfFoldersToBeSynced");

		// Token: 0x040045D1 RID: 17873
		public static readonly GuidNamePropertyDefinition HierarchySyncBatchSize = InternalSchema.CreateGuidNameProperty("HierarchySyncBatchSize", typeof(int), WellKnownPropertySet.Common, "HierarchySyncBatchSize");

		// Token: 0x040045D2 RID: 17874
		public static readonly GuidIdPropertyDefinition Schools = InternalSchema.CreateGuidIdProperty("Schools", typeof(string), WellKnownPropertySet.Address, 33001);

		// Token: 0x040045D3 RID: 17875
		public static readonly GuidIdPropertyDefinition InternalPersonType = InternalSchema.CreateGuidIdProperty("InternalPersonType", typeof(int), WellKnownPropertySet.Address, 33002);

		// Token: 0x040045D4 RID: 17876
		public static GuidNamePropertyDefinition MMS = InternalSchema.CreateGuidNameProperty("MMS", typeof(string), WellKnownPropertySet.AirSync, "AirSync:MMS");

		// Token: 0x040045D5 RID: 17877
		public static readonly PropertyTagPropertyDefinition Initials = PropertyTagPropertyDefinition.InternalCreate("Initials", PropTag.Initials);

		// Token: 0x040045D6 RID: 17878
		public static readonly PropertyTagPropertyDefinition InternationalIsdnNumber = PropertyTagPropertyDefinition.InternalCreate("InternationalIsdnNumber", PropTag.IsdnNumber);

		// Token: 0x040045D7 RID: 17879
		public static readonly PropertyTagPropertyDefinition Manager = PropertyTagPropertyDefinition.InternalCreate("Manager", PropTag.ManagerName);

		// Token: 0x040045D8 RID: 17880
		public static readonly PropertyTagPropertyDefinition MiddleName = PropertyTagPropertyDefinition.InternalCreate("MiddleName", PropTag.MiddleName, new PropertyDefinitionConstraint[]
		{
			new NonMoveMailboxPropertyConstraint(new StringLengthConstraint(0, 256))
		});

		// Token: 0x040045D9 RID: 17881
		public static readonly GuidIdPropertyDefinition Mileage = InternalSchema.CreateGuidIdProperty("Mileage", typeof(string), WellKnownPropertySet.Common, 34100);

		// Token: 0x040045DA RID: 17882
		public static readonly PropertyTagPropertyDefinition MobilePhone = PropertyTagPropertyDefinition.InternalCreate("MobilePhone", PropTag.MobileTelephoneNumber);

		// Token: 0x040045DB RID: 17883
		public static readonly PropertyTagPropertyDefinition Nickname = PropertyTagPropertyDefinition.InternalCreate("Nickname", PropTag.Nickname);

		// Token: 0x040045DC RID: 17884
		public static readonly PropertyTagPropertyDefinition OfficeLocation = PropertyTagPropertyDefinition.InternalCreate("OfficeLocation", PropTag.OfficeLocation);

		// Token: 0x040045DD RID: 17885
		public static readonly PropertyTagPropertyDefinition OrganizationMainPhone = PropertyTagPropertyDefinition.InternalCreate("OrganizationMainPhone", PropTag.CompanyMainPhoneNumber);

		// Token: 0x040045DE RID: 17886
		public static readonly PropertyTagPropertyDefinition OtherPostOfficeBox = PropertyTagPropertyDefinition.InternalCreate("OtherPostOfficeBox", PropTag.OtherAddressPostOfficeBox);

		// Token: 0x040045DF RID: 17887
		public static readonly PropertyTagPropertyDefinition OtherCity = PropertyTagPropertyDefinition.InternalCreate("OtherCity", PropTag.OtherAddressCity);

		// Token: 0x040045E0 RID: 17888
		public static readonly PropertyTagPropertyDefinition OtherCountry = PropertyTagPropertyDefinition.InternalCreate("OtherCountry", PropTag.OtherAddressCountry);

		// Token: 0x040045E1 RID: 17889
		public static readonly PropertyTagPropertyDefinition OtherFax = PropertyTagPropertyDefinition.InternalCreate("OtherFax", PropTag.PrimaryFaxNumber);

		// Token: 0x040045E2 RID: 17890
		public static readonly PropertyTagPropertyDefinition OtherPostalCode = PropertyTagPropertyDefinition.InternalCreate("OtherPostalCode", PropTag.OtherAddressPostalCode);

		// Token: 0x040045E3 RID: 17891
		public static readonly PropertyTagPropertyDefinition OtherState = PropertyTagPropertyDefinition.InternalCreate("OtherState", PropTag.OtherAddressStateOrProvince);

		// Token: 0x040045E4 RID: 17892
		public static readonly PropertyTagPropertyDefinition OtherStreet = PropertyTagPropertyDefinition.InternalCreate("OtherStreet", PropTag.OtherAddressStreet);

		// Token: 0x040045E5 RID: 17893
		public static readonly PropertyTagPropertyDefinition OtherTelephone = PropertyTagPropertyDefinition.InternalCreate("OtherTelephone", PropTag.OtherTelephoneNumber);

		// Token: 0x040045E6 RID: 17894
		public static readonly PropertyTagPropertyDefinition Pager = PropertyTagPropertyDefinition.InternalCreate("Pager", PropTag.PagerTelephoneNumber);

		// Token: 0x040045E7 RID: 17895
		public static readonly PropertyTagPropertyDefinition PartnerNetworkId = PropertyTagPropertyDefinition.InternalCreate("PartnerNetworkId", PropTag.PartnerNetworkId);

		// Token: 0x040045E8 RID: 17896
		public static readonly PropertyTagPropertyDefinition PartnerNetworkUserId = PropertyTagPropertyDefinition.InternalCreate("PartnerNetworkUserId", PropTag.PartnerNetworkUserId);

		// Token: 0x040045E9 RID: 17897
		public static readonly PropertyTagPropertyDefinition PartnerNetworkThumbnailPhotoUrl = PropertyTagPropertyDefinition.InternalCreate("PartnerNetworkThumbnailPhotoUrl", PropTag.PartnerNetworkThumbnailPhotoUrl);

		// Token: 0x040045EA RID: 17898
		public static readonly PropertyTagPropertyDefinition PartnerNetworkProfilePhotoUrl = PropertyTagPropertyDefinition.InternalCreate("PartnerNetworkProfilePhotoUrl", PropTag.PartnerNetworkProfilePhotoUrl);

		// Token: 0x040045EB RID: 17899
		public static readonly PropertyTagPropertyDefinition PartnerNetworkContactType = PropertyTagPropertyDefinition.InternalCreate("PartnerNetworkContactType", PropTag.PartnerNetworkContactType);

		// Token: 0x040045EC RID: 17900
		public static readonly GuidNamePropertyDefinition GALContactType = InternalSchema.CreateGuidNameProperty("GALContactType", typeof(int), WellKnownPropertySet.PublicStrings, "GALContactType");

		// Token: 0x040045ED RID: 17901
		public static readonly GuidNamePropertyDefinition GALObjectId = InternalSchema.CreateGuidNameProperty("GALObjectId", typeof(byte[]), WellKnownPropertySet.PublicStrings, "GALObjectId");

		// Token: 0x040045EE RID: 17902
		public static readonly GuidNamePropertyDefinition GALRecipientType = InternalSchema.CreateGuidNameProperty("GALRecipientType", typeof(int), WellKnownPropertySet.PublicStrings, "GALRecipientType");

		// Token: 0x040045EF RID: 17903
		public static readonly GuidNamePropertyDefinition GALHiddenFromAddressListsEnabled = InternalSchema.CreateGuidNameProperty("GALHiddenFromAddressListsEnabled", typeof(bool), WellKnownPropertySet.PublicStrings, "GALHiddenFromAddressListsEnabled");

		// Token: 0x040045F0 RID: 17904
		public static readonly GuidNamePropertyDefinition GALSpeechNormalizedNamesForDisplayName = InternalSchema.CreateGuidNameProperty("GALSpeechNormalizedNamesForDisplayName", typeof(byte[]), WellKnownPropertySet.PublicStrings, "GALSpeechNormalizedNamesForDisplayName", PropertyFlags.Streamable, new PropertyDefinitionConstraint[0]);

		// Token: 0x040045F1 RID: 17905
		public static readonly GuidNamePropertyDefinition GALSpeechNormalizedNamesForPhoneticDisplayName = InternalSchema.CreateGuidNameProperty("GALSpeechNormalizedNamesForPhoneticDisplayName", typeof(byte[]), WellKnownPropertySet.PublicStrings, "GALSpeechNormalizedNamesForPhoneticDisplayName", PropertyFlags.Streamable, new PropertyDefinitionConstraint[0]);

		// Token: 0x040045F2 RID: 17906
		public static readonly GuidNamePropertyDefinition GALUMDialplanId = InternalSchema.CreateGuidNameProperty("GALUMDialplanId", typeof(Guid), WellKnownPropertySet.PublicStrings, "GALUMDialplanId");

		// Token: 0x040045F3 RID: 17907
		public static readonly GuidNamePropertyDefinition GALCurrentSpeechGrammarVersion = InternalSchema.CreateGuidNameProperty("GALCurrentSpeechGrammarVersion", typeof(int), WellKnownPropertySet.PublicStrings, "GALCurrentSpeechGrammarVersion");

		// Token: 0x040045F4 RID: 17908
		public static readonly GuidNamePropertyDefinition GALPreviousSpeechGrammarVersion = InternalSchema.CreateGuidNameProperty("GALPreviousSpeechGrammarVersion", typeof(int), WellKnownPropertySet.PublicStrings, "GALPreviousSpeechGrammarVersion");

		// Token: 0x040045F5 RID: 17909
		public static readonly GuidNamePropertyDefinition GALCurrentUMDtmfMapVersion = InternalSchema.CreateGuidNameProperty("GALCurrentUMDtmfMapVersion", typeof(int), WellKnownPropertySet.PublicStrings, "GALCurrentUMDtmfMapVersion");

		// Token: 0x040045F6 RID: 17910
		public static readonly GuidNamePropertyDefinition GALPreviousUMDtmfMapVersion = InternalSchema.CreateGuidNameProperty("GALPreviousUMDtmfMapVersion", typeof(int), WellKnownPropertySet.PublicStrings, "GALPreviousUMDtmfMapVersion");

		// Token: 0x040045F7 RID: 17911
		public static readonly GuidIdPropertyDefinition PostalAddressId = InternalSchema.CreateGuidIdProperty("PostalAddressId", typeof(int), WellKnownPropertySet.Address, 32802);

		// Token: 0x040045F8 RID: 17912
		public static readonly PropertyTagPropertyDefinition Profession = PropertyTagPropertyDefinition.InternalCreate("Profession", PropTag.Profession);

		// Token: 0x040045F9 RID: 17913
		public static readonly PropertyTagPropertyDefinition RadioPhone = PropertyTagPropertyDefinition.InternalCreate("RadioPhone", PropTag.RadioTelephoneNumber);

		// Token: 0x040045FA RID: 17914
		public static readonly PropertyTagPropertyDefinition SpouseName = PropertyTagPropertyDefinition.InternalCreate("SpouseName", PropTag.SpouseName);

		// Token: 0x040045FB RID: 17915
		public static readonly PropertyTagPropertyDefinition Surname = PropertyTagPropertyDefinition.InternalCreate("Surname", PropTag.Surname, new PropertyDefinitionConstraint[]
		{
			new NonMoveMailboxPropertyConstraint(new StringLengthConstraint(0, 256))
		});

		// Token: 0x040045FC RID: 17916
		public static readonly PropertyTagPropertyDefinition Title = PropertyTagPropertyDefinition.InternalCreate("Title", PropTag.Title);

		// Token: 0x040045FD RID: 17917
		public static readonly PropertyTagPropertyDefinition WeddingAnniversary = PropertyTagPropertyDefinition.InternalCreate("WeddingAnniversary", PropTag.WeddingAnniversary);

		// Token: 0x040045FE RID: 17918
		public static readonly GuidIdPropertyDefinition WeddingAnniversaryLocal = InternalSchema.CreateGuidIdProperty("WeddingAnniversaryLocal", typeof(ExDateTime), WellKnownPropertySet.Address, 32991);

		// Token: 0x040045FF RID: 17919
		public static readonly PropertyTagPropertyDefinition WorkPostOfficeBox = PropertyTagPropertyDefinition.InternalCreate("WorkPostOfficeBox", PropTag.PostOfficeBox);

		// Token: 0x04004600 RID: 17920
		public static readonly GuidIdPropertyDefinition WorkAddressCity = InternalSchema.CreateGuidIdProperty("WorkAddressCity", typeof(string), WellKnownPropertySet.Address, 32838);

		// Token: 0x04004601 RID: 17921
		public static readonly GuidIdPropertyDefinition WorkAddressCountry = InternalSchema.CreateGuidIdProperty("WorkAddressCountry", typeof(string), WellKnownPropertySet.Address, 32841);

		// Token: 0x04004602 RID: 17922
		public static readonly GuidIdPropertyDefinition WorkAddressPostalCode = InternalSchema.CreateGuidIdProperty("WorkAddressPostalCode", typeof(string), WellKnownPropertySet.Address, 32840);

		// Token: 0x04004603 RID: 17923
		public static readonly GuidIdPropertyDefinition WorkAddressState = InternalSchema.CreateGuidIdProperty("WorkAddressState", typeof(string), WellKnownPropertySet.Address, 32839);

		// Token: 0x04004604 RID: 17924
		public static readonly GuidIdPropertyDefinition WorkAddressStreet = InternalSchema.CreateGuidIdProperty("WorkAddressStreet", typeof(string), WellKnownPropertySet.Address, 32837);

		// Token: 0x04004605 RID: 17925
		public static readonly PropertyTagPropertyDefinition Generation = PropertyTagPropertyDefinition.InternalCreate("Generation", PropTag.Generation);

		// Token: 0x04004606 RID: 17926
		public static readonly GuidNamePropertyDefinition DisplayNameFirstLast = InternalSchema.CreateGuidNameProperty("DisplayNameFirstLast", typeof(string), WellKnownPropertySet.Address, "DisplayNameFirstLast");

		// Token: 0x04004607 RID: 17927
		public static readonly GuidNamePropertyDefinition DisplayNameLastFirst = InternalSchema.CreateGuidNameProperty("DisplayNameLastFirst", typeof(string), WellKnownPropertySet.Address, "DisplayNameLastFirst");

		// Token: 0x04004608 RID: 17928
		public static readonly GuidNamePropertyDefinition DisplayNamePriority = InternalSchema.CreateGuidNameProperty("DisplayNamePriority", typeof(int), WellKnownPropertySet.Address, "DisplayNamePriority");

		// Token: 0x04004609 RID: 17929
		public static readonly GuidIdPropertyDefinition ProtectedEmailAddress = InternalSchema.CreateGuidIdProperty("ProtectedEmailAddress", typeof(string), WellKnownPropertySet.Address, 33008);

		// Token: 0x0400460A RID: 17930
		public static readonly GuidIdPropertyDefinition ProtectedPhoneNumber = InternalSchema.CreateGuidIdProperty("ProtectedPhoneNumber", typeof(string), WellKnownPropertySet.Address, 33010);

		// Token: 0x0400460B RID: 17931
		public static readonly GuidIdPropertyDefinition DLChecksum = InternalSchema.CreateGuidIdProperty("DLChecksum", typeof(int), WellKnownPropertySet.Address, 32844);

		// Token: 0x0400460C RID: 17932
		public static readonly GuidIdPropertyDefinition Members = InternalSchema.CreateGuidIdProperty("Members", typeof(byte[][]), WellKnownPropertySet.Address, 32853);

		// Token: 0x0400460D RID: 17933
		public static readonly GuidIdPropertyDefinition OneOffMembers = InternalSchema.CreateGuidIdProperty("OneOffMembers", typeof(byte[][]), WellKnownPropertySet.Address, 32852);

		// Token: 0x0400460E RID: 17934
		public static readonly GuidIdPropertyDefinition DLName = InternalSchema.CreateGuidIdProperty("DLName", typeof(string), WellKnownPropertySet.Address, 32851);

		// Token: 0x0400460F RID: 17935
		public static readonly GuidIdPropertyDefinition DLStream = InternalSchema.CreateGuidIdProperty("DLStream", typeof(byte[]), WellKnownPropertySet.Address, 32868, PropertyFlags.Streamable, PropertyDefinitionConstraint.None);

		// Token: 0x04004610 RID: 17936
		public static readonly GuidNamePropertyDefinition DLAlias = InternalSchema.CreateGuidNameProperty("DLAlias", typeof(string), WellKnownPropertySet.PublicStrings, "DLAlias");

		// Token: 0x04004611 RID: 17937
		public static readonly PropertyTagPropertyDefinition FolderQuota = PropertyTagPropertyDefinition.InternalCreate("FolderQuota", PropTag.PagePreread);

		// Token: 0x04004612 RID: 17938
		public static readonly PropertyTagPropertyDefinition FolderSize = PropertyTagPropertyDefinition.InternalCreate("FolderSize", PropTag.PageRead);

		// Token: 0x04004613 RID: 17939
		public static readonly PropertyTagPropertyDefinition FolderSizeExtended = PropertyTagPropertyDefinition.InternalCreate("FolderSizeExtended", PropTag.FolderSizeExtended);

		// Token: 0x04004614 RID: 17940
		public static readonly PropertyTagPropertyDefinition FolderRulesSize = PropertyTagPropertyDefinition.InternalCreate("FolderRulesSize", PropTag.RulesSize);

		// Token: 0x04004615 RID: 17941
		public static readonly PropertyTagPropertyDefinition FolderWebViewInfo = PropertyTagPropertyDefinition.InternalCreate("FolderWebViewInfo", (PropTag)920584450U);

		// Token: 0x04004616 RID: 17942
		public static readonly PropertyTagPropertyDefinition HasChildren = PropertyTagPropertyDefinition.InternalCreate("HasSubFolders", PropTag.SubFolders, PropertyFlags.ReadOnly);

		// Token: 0x04004617 RID: 17943
		public static readonly PropertyTagPropertyDefinition ItemCount = PropertyTagPropertyDefinition.InternalCreate("ItemCount", PropTag.ContentCount, PropertyFlags.ReadOnly);

		// Token: 0x04004618 RID: 17944
		public static readonly PropertyTagPropertyDefinition AssociatedItemCount = PropertyTagPropertyDefinition.InternalCreate("AssociatedItemCount", PropTag.AssocContentCount);

		// Token: 0x04004619 RID: 17945
		public static readonly PropertyTagPropertyDefinition SearchFolderItemCount = PropertyTagPropertyDefinition.InternalCreate("SearchFolderItemCount", PropTag.SearchFolderMsgCount);

		// Token: 0x0400461A RID: 17946
		public static readonly PropertyTagPropertyDefinition SearchBacklinkNames = PropertyTagPropertyDefinition.InternalCreate("SearchBacklinkNames", PropTag.SearchBacklinkNames);

		// Token: 0x0400461B RID: 17947
		[Obsolete("Use InternalSchema.SearchFolderItemCount instead.")]
		public static readonly PropertyTagPropertyDefinition SearchFolderMessageCount = InternalSchema.SearchFolderItemCount;

		// Token: 0x0400461C RID: 17948
		public static readonly PropertyTagPropertyDefinition SearchFolderAllowAgeout = PropertyTagPropertyDefinition.InternalCreate("SearchFolderAllowAgeout", PropTag.AllowAgeout);

		// Token: 0x0400461D RID: 17949
		public static readonly PropertyTagPropertyDefinition SearchFolderAgeOutTimeout = PropertyTagPropertyDefinition.InternalCreate("SearchFolderAgeOutTimeout", PropTag.SearchFolderAgeOutTimeout);

		// Token: 0x0400461E RID: 17950
		public static readonly PropertyTagPropertyDefinition IPMFolder = PropertyTagPropertyDefinition.InternalCreate("ptagIPMFolder", PropTag.IPMFolder);

		// Token: 0x0400461F RID: 17951
		public static readonly PropertyTagPropertyDefinition MapiFolderType = PropertyTagPropertyDefinition.InternalCreate("MapiFolderType", PropTag.FolderType);

		// Token: 0x04004620 RID: 17952
		public static readonly PropertyTagPropertyDefinition ChildCount = PropertyTagPropertyDefinition.InternalCreate("SubFolderCount", (PropTag)1714946051U, PropertyFlags.ReadOnly);

		// Token: 0x04004621 RID: 17953
		public static readonly PropertyTagPropertyDefinition PublicFolderDumpsterHolderEntryId = PropertyTagPropertyDefinition.InternalCreate("PublicFolderDumpsterHolderEntryId", PropTag.CurrentIPMWasteBasketContainerEntryId);

		// Token: 0x04004622 RID: 17954
		public static readonly PropertyTagPropertyDefinition UnreadCount = PropertyTagPropertyDefinition.InternalCreate("UnreadCount", PropTag.ContentUnread, PropertyFlags.ReadOnly);

		// Token: 0x04004623 RID: 17955
		public static readonly PropertyTagPropertyDefinition ContainerClass = PropertyTagPropertyDefinition.InternalCreate("ContainerClass", PropTag.ContainerClass, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 255)
		});

		// Token: 0x04004624 RID: 17956
		public static readonly PropertyTagPropertyDefinition ELCFolderComment = PropertyTagPropertyDefinition.InternalCreate("ELCFolderComment", PropTag.ELCPolicyComment);

		// Token: 0x04004625 RID: 17957
		public static readonly PropertyTagPropertyDefinition ELCPolicyIds = PropertyTagPropertyDefinition.InternalCreate("ELCPolicyIds", (PropTag)1731330079U);

		// Token: 0x04004626 RID: 17958
		public static readonly PropertyTagPropertyDefinition NextArticleId = PropertyTagPropertyDefinition.InternalCreate("NextArticleId", PropTag.NextArticleId);

		// Token: 0x04004627 RID: 17959
		public static readonly PropertyTagPropertyDefinition ExtendedFolderFlagsInternal = PropertyTagPropertyDefinition.InternalCreate("ExtendedFolderFlagsInternal", (PropTag)920256770U);

		// Token: 0x04004628 RID: 17960
		public static readonly PropertyTagPropertyDefinition PartOfContentIndexing = PropertyTagPropertyDefinition.InternalCreate("PartOfContentIndexing", (PropTag)910491659U);

		// Token: 0x04004629 RID: 17961
		public static readonly PropertyTagPropertyDefinition UrlName = PropertyTagPropertyDefinition.InternalCreate("UrlName", PropTag.UrlName);

		// Token: 0x0400462A RID: 17962
		public static readonly PropertyTagPropertyDefinition FolderPathName = PropertyTagPropertyDefinition.InternalCreate("FolderPathName", PropTag.FolderPathName);

		// Token: 0x0400462B RID: 17963
		public static readonly PropertyTagPropertyDefinition UrlCompName = PropertyTagPropertyDefinition.InternalCreate("UrlCompName", (PropTag)284360735U);

		// Token: 0x0400462C RID: 17964
		public static readonly PropertyTagPropertyDefinition UrlCompNamePostfix = PropertyTagPropertyDefinition.InternalCreate("UrlCompNamePostfix", (PropTag)241238019U);

		// Token: 0x0400462D RID: 17965
		public static readonly PropertyTagPropertyDefinition OutOfOfficeHistory = PropertyTagPropertyDefinition.InternalCreate("OutOfOfficeHistory", PropTag.OofHistory, PropertyFlags.Streamable);

		// Token: 0x0400462E RID: 17966
		public static readonly PropertyTagPropertyDefinition FolderFlags = PropertyTagPropertyDefinition.InternalCreate("FolderFlags", (PropTag)1722286083U);

		// Token: 0x0400462F RID: 17967
		public static readonly PropertyTagPropertyDefinition ContentAggregationFlags = PropertyTagPropertyDefinition.InternalCreate("ContentAggregationFlags", PropTag.ContentAggregationFlags);

		// Token: 0x04004630 RID: 17968
		public static readonly GuidNamePropertyDefinition PeopleIKnowEmailAddressCollection = InternalSchema.CreateGuidNameProperty("PeopleIKnowEmailAddressCollection", typeof(byte[]), WellKnownPropertySet.Address, "PeopleIKnowEmailAddressCollection");

		// Token: 0x04004631 RID: 17969
		public static readonly GuidNamePropertyDefinition PeopleIKnowEmailAddressRelevanceScoreCollection = InternalSchema.CreateGuidNameProperty("PeopleIKnowEmailAddressRelevanceScoreCollection", typeof(byte[]), WellKnownPropertySet.Address, "PeopleIKnowEmailAddressRelevanceScoreCollection");

		// Token: 0x04004632 RID: 17970
		public static readonly GuidNamePropertyDefinition OfficeGraphLocation = InternalSchema.CreateGuidNameProperty("OfficeGraphLocation", typeof(string), WellKnownPropertySet.Common, "OfficeGraphLocation");

		// Token: 0x04004633 RID: 17971
		public static readonly GuidNamePropertyDefinition GalContactsFolderState = InternalSchema.CreateGuidNameProperty("GALContactsFolderState", typeof(int), WellKnownPropertySet.Common, "GALContactsFolderState");

		// Token: 0x04004634 RID: 17972
		public static readonly GuidNamePropertyDefinition PermissionChangeBlocked = InternalSchema.CreateGuidNameProperty("PermissionChangeBlocked", typeof(bool), WellKnownPropertySet.ExternalSharing, "PermissionChangeBlocked");

		// Token: 0x04004635 RID: 17973
		public static readonly GuidNamePropertyDefinition RecentBindingHistory = InternalSchema.CreateGuidNameProperty("RecentBindingHistory", typeof(string[]), WellKnownPropertySet.Elc, "RecentBindingHistory");

		// Token: 0x04004636 RID: 17974
		public static readonly GuidNamePropertyDefinition PeopleHubSortGroupPriority = InternalSchema.CreateGuidNameProperty("PeopleHubSortGroupPriority", typeof(int), WellKnownPropertySet.Common, "PeopleHubSortGroupPriority");

		// Token: 0x04004637 RID: 17975
		public static readonly GuidNamePropertyDefinition PeopleHubSortGroupPriorityVersion = InternalSchema.CreateGuidNameProperty("PeopleHubSortGroupPriorityVersion", typeof(int), WellKnownPropertySet.Common, "PeopleHubSortGroupPriorityVersion");

		// Token: 0x04004638 RID: 17976
		public static readonly GuidNamePropertyDefinition IsPeopleConnectSyncFolder = InternalSchema.CreateGuidNameProperty("IsPeopleConnectSyncFolder", typeof(bool), WellKnownPropertySet.Common, "IsPeopleConnectSyncFolder");

		// Token: 0x04004639 RID: 17977
		public static readonly GuidNamePropertyDefinition LocationFolderEntryId = InternalSchema.CreateGuidNameProperty("LocationFolderEntryId", typeof(byte[]), WellKnownPropertySet.Location, "LocationFolderEntryId");

		// Token: 0x0400463A RID: 17978
		public static readonly GuidNamePropertyDefinition LocationRelevanceRank = InternalSchema.CreateGuidNameProperty("LocationRelevanceRank", typeof(int), WellKnownPropertySet.Location, "LocationRelevanceRank");

		// Token: 0x0400463B RID: 17979
		public static readonly GuidNamePropertyDefinition LocationDisplayName = InternalSchema.CreateGuidNameProperty("LocationDisplayName", typeof(string), WellKnownPropertySet.Location, "LocationDisplayName");

		// Token: 0x0400463C RID: 17980
		public static readonly GuidNamePropertyDefinition LocationAnnotation = InternalSchema.CreateGuidNameProperty("LocationAnnotation", typeof(string), WellKnownPropertySet.Location, "LocationAnnotation");

		// Token: 0x0400463D RID: 17981
		public static readonly GuidNamePropertyDefinition LocationSource = InternalSchema.CreateGuidNameProperty("LocationSource", typeof(int), WellKnownPropertySet.Location, "LocationSource");

		// Token: 0x0400463E RID: 17982
		public static readonly GuidNamePropertyDefinition LocationUri = InternalSchema.CreateGuidNameProperty("LocationUri", typeof(string), WellKnownPropertySet.Location, "LocationUri");

		// Token: 0x0400463F RID: 17983
		public static readonly GuidNamePropertyDefinition Latitude = InternalSchema.CreateGuidNameProperty("Latitude", typeof(double), WellKnownPropertySet.Location, "Latitude");

		// Token: 0x04004640 RID: 17984
		public static readonly GuidNamePropertyDefinition Longitude = InternalSchema.CreateGuidNameProperty("Longitude", typeof(double), WellKnownPropertySet.Location, "Longitude");

		// Token: 0x04004641 RID: 17985
		public static readonly GuidNamePropertyDefinition Accuracy = InternalSchema.CreateGuidNameProperty("Accuracy", typeof(double), WellKnownPropertySet.Location, "Accuracy");

		// Token: 0x04004642 RID: 17986
		public static readonly GuidNamePropertyDefinition Altitude = InternalSchema.CreateGuidNameProperty("Altitude", typeof(double), WellKnownPropertySet.Location, "Altitude");

		// Token: 0x04004643 RID: 17987
		public static readonly GuidNamePropertyDefinition AltitudeAccuracy = InternalSchema.CreateGuidNameProperty("AltitudeAccuracy", typeof(double), WellKnownPropertySet.Location, "AltitudeAccuracy");

		// Token: 0x04004644 RID: 17988
		public static readonly GuidNamePropertyDefinition LocationAddressInternal = InternalSchema.CreateGuidNameProperty("LocationAddressInternal", typeof(string), WellKnownPropertySet.Location, "LocationAddressInternal");

		// Token: 0x04004645 RID: 17989
		public static readonly GuidNamePropertyDefinition LocationStreet = InternalSchema.CreateGuidNameProperty("StreetAddress", typeof(string), WellKnownPropertySet.Location, "StreetAddress");

		// Token: 0x04004646 RID: 17990
		public static readonly GuidNamePropertyDefinition LocationCity = InternalSchema.CreateGuidNameProperty("LocationCity", typeof(string), WellKnownPropertySet.Location, "LocationCity");

		// Token: 0x04004647 RID: 17991
		public static readonly GuidNamePropertyDefinition LocationState = InternalSchema.CreateGuidNameProperty("LocationState", typeof(string), WellKnownPropertySet.Location, "LocationState");

		// Token: 0x04004648 RID: 17992
		public static readonly GuidNamePropertyDefinition LocationCountry = InternalSchema.CreateGuidNameProperty("LocationCountry", typeof(string), WellKnownPropertySet.Location, "LocationCountry");

		// Token: 0x04004649 RID: 17993
		public static readonly GuidNamePropertyDefinition LocationPostalCode = InternalSchema.CreateGuidNameProperty("LocationPostalCode", typeof(string), WellKnownPropertySet.Location, "LocationPostalCode");

		// Token: 0x0400464A RID: 17994
		public static readonly PhysicalAddressProperty LocationAddress = new PhysicalAddressProperty("LocationAddress", InternalSchema.LocationAddressInternal, InternalSchema.LocationStreet, InternalSchema.LocationCity, InternalSchema.LocationState, InternalSchema.LocationPostalCode, InternalSchema.LocationCountry);

		// Token: 0x0400464B RID: 17995
		public static readonly GuidNamePropertyDefinition HomeLocationSource = InternalSchema.CreateGuidNameProperty("HomeLocationSource", typeof(int), WellKnownPropertySet.Location, "HomeLocationSource");

		// Token: 0x0400464C RID: 17996
		public static readonly GuidNamePropertyDefinition HomeLocationUri = InternalSchema.CreateGuidNameProperty("HomeLocationUri", typeof(string), WellKnownPropertySet.Location, "HomeLocationUri");

		// Token: 0x0400464D RID: 17997
		public static readonly GuidNamePropertyDefinition HomeLatitude = InternalSchema.CreateGuidNameProperty("HomeLatitude", typeof(double), WellKnownPropertySet.Location, "HomeLatitude", PropertyFlags.SetIfNotChanged, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<double>(-90.0, 90.0)
		});

		// Token: 0x0400464E RID: 17998
		public static readonly GuidNamePropertyDefinition HomeLongitude = InternalSchema.CreateGuidNameProperty("HomeLongitude", typeof(double), WellKnownPropertySet.Location, "HomeLongitude", PropertyFlags.SetIfNotChanged, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<double>(-180.0, 180.0)
		});

		// Token: 0x0400464F RID: 17999
		public static readonly GuidNamePropertyDefinition HomeAccuracy = InternalSchema.CreateGuidNameProperty("HomeAccuracy", typeof(double), WellKnownPropertySet.Location, "HomeAccuracy");

		// Token: 0x04004650 RID: 18000
		public static readonly GuidNamePropertyDefinition HomeAltitude = InternalSchema.CreateGuidNameProperty("HomeAltitude", typeof(double), WellKnownPropertySet.Location, "HomeAltitude");

		// Token: 0x04004651 RID: 18001
		public static readonly GuidNamePropertyDefinition HomeAltitudeAccuracy = InternalSchema.CreateGuidNameProperty("HomeAltitudeAccuracy", typeof(double), WellKnownPropertySet.Location, "HomeAltitudeAccuracy");

		// Token: 0x04004652 RID: 18002
		public static readonly GuidNamePropertyDefinition WorkLocationSource = InternalSchema.CreateGuidNameProperty("WorkLocationSource", typeof(int), WellKnownPropertySet.Location, "WorkLocationSource");

		// Token: 0x04004653 RID: 18003
		public static readonly GuidNamePropertyDefinition WorkLocationUri = InternalSchema.CreateGuidNameProperty("WorkLocationUri", typeof(string), WellKnownPropertySet.Location, "WorkLocationUri");

		// Token: 0x04004654 RID: 18004
		public static readonly GuidNamePropertyDefinition WorkLatitude = InternalSchema.CreateGuidNameProperty("WorkLatitude", typeof(double), WellKnownPropertySet.Location, "WorkLatitude", PropertyFlags.SetIfNotChanged, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<double>(-90.0, 90.0)
		});

		// Token: 0x04004655 RID: 18005
		public static readonly GuidNamePropertyDefinition WorkLongitude = InternalSchema.CreateGuidNameProperty("WorkLongitude", typeof(double), WellKnownPropertySet.Location, "WorkLongitude", PropertyFlags.SetIfNotChanged, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<double>(-180.0, 180.0)
		});

		// Token: 0x04004656 RID: 18006
		public static readonly GuidNamePropertyDefinition WorkAccuracy = InternalSchema.CreateGuidNameProperty("WorkAccuracy", typeof(double), WellKnownPropertySet.Location, "WorkAccuracy");

		// Token: 0x04004657 RID: 18007
		public static readonly GuidNamePropertyDefinition WorkAltitude = InternalSchema.CreateGuidNameProperty("WorkAltitude", typeof(double), WellKnownPropertySet.Location, "WorkAltitude");

		// Token: 0x04004658 RID: 18008
		public static readonly GuidNamePropertyDefinition WorkAltitudeAccuracy = InternalSchema.CreateGuidNameProperty("WorkAltitudeAccuracy", typeof(double), WellKnownPropertySet.Location, "WorkAltitudeAccuracy");

		// Token: 0x04004659 RID: 18009
		public static readonly GuidNamePropertyDefinition OtherLocationSource = InternalSchema.CreateGuidNameProperty("OtherLocationSource", typeof(int), WellKnownPropertySet.Location, "OtherLocationSource");

		// Token: 0x0400465A RID: 18010
		public static readonly GuidNamePropertyDefinition OtherLocationUri = InternalSchema.CreateGuidNameProperty("OtherLocationUri", typeof(string), WellKnownPropertySet.Location, "OtherLocationUri");

		// Token: 0x0400465B RID: 18011
		public static readonly GuidNamePropertyDefinition OtherLatitude = InternalSchema.CreateGuidNameProperty("OtherLatitude", typeof(double), WellKnownPropertySet.Location, "OtherLatitude", PropertyFlags.SetIfNotChanged, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<double>(-90.0, 90.0)
		});

		// Token: 0x0400465C RID: 18012
		public static readonly GuidNamePropertyDefinition OtherLongitude = InternalSchema.CreateGuidNameProperty("OtherLongitude", typeof(double), WellKnownPropertySet.Location, "OtherLongitude", PropertyFlags.SetIfNotChanged, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<double>(-180.0, 180.0)
		});

		// Token: 0x0400465D RID: 18013
		public static readonly GuidNamePropertyDefinition OtherAccuracy = InternalSchema.CreateGuidNameProperty("OtherAccuracy", typeof(double), WellKnownPropertySet.Location, "OtherAccuracy");

		// Token: 0x0400465E RID: 18014
		public static readonly GuidNamePropertyDefinition OtherAltitude = InternalSchema.CreateGuidNameProperty("OtherAltitude", typeof(double), WellKnownPropertySet.Location, "OtherAltitude");

		// Token: 0x0400465F RID: 18015
		public static readonly GuidNamePropertyDefinition OtherAltitudeAccuracy = InternalSchema.CreateGuidNameProperty("OtherAltitudeAccuracy", typeof(double), WellKnownPropertySet.Location, "OtherAltitudeAccuracy");

		// Token: 0x04004660 RID: 18016
		public static readonly GuidNamePropertyDefinition Categories = InternalSchema.CreateGuidNameProperty("Categories", typeof(string[]), WellKnownPropertySet.PublicStrings, "Keywords", PropertyFlags.None, new PropertyDefinitionConstraint[]
		{
			new NonMoveMailboxPropertyConstraint(new StringLengthConstraint(0, 255)),
			new NonMoveMailboxPropertyConstraint(new CharacterConstraint(Category.ProhibitedCharacters, false))
		});

		// Token: 0x04004661 RID: 18017
		public static readonly GuidNamePropertyDefinition LinkedId = InternalSchema.CreateGuidNameProperty("LinkedId", typeof(Guid), WellKnownPropertySet.LinkedFolder, "LinkedId");

		// Token: 0x04004662 RID: 18018
		public static readonly GuidNamePropertyDefinition LinkedUrl = InternalSchema.CreateGuidNameProperty("LinkedUrl", typeof(string), WellKnownPropertySet.LinkedFolder, "LinkedUrl");

		// Token: 0x04004663 RID: 18019
		public static readonly GuidNamePropertyDefinition LinkedObjectVersion = InternalSchema.CreateGuidNameProperty("LinkedObjectVersion", typeof(string), WellKnownPropertySet.LinkedFolder, "LinkedObjectVersion");

		// Token: 0x04004664 RID: 18020
		public static readonly GuidNamePropertyDefinition LinkedSiteUrl = InternalSchema.CreateGuidNameProperty("LinkedSiteUrl", typeof(string), WellKnownPropertySet.LinkedFolder, "LinkedSiteUrl");

		// Token: 0x04004665 RID: 18021
		public static readonly GuidNamePropertyDefinition LinkedListId = InternalSchema.CreateGuidNameProperty("LinkedListId", typeof(Guid), WellKnownPropertySet.LinkedFolder, "LinkedListId");

		// Token: 0x04004666 RID: 18022
		public static readonly GuidNamePropertyDefinition IsDocumentLibraryFolder = InternalSchema.CreateGuidNameProperty("IsDocumentLibraryFolder", typeof(bool), WellKnownPropertySet.LinkedFolder, "IsDocumentLibraryFolder");

		// Token: 0x04004667 RID: 18023
		public static readonly GuidNamePropertyDefinition SharePointChangeToken = InternalSchema.CreateGuidNameProperty("SharePointChangeToken", typeof(string), WellKnownPropertySet.LinkedFolder, "SharePointChangeToken");

		// Token: 0x04004668 RID: 18024
		public static readonly GuidNamePropertyDefinition LinkedItemUpdateHistory = InternalSchema.CreateGuidNameProperty("LinkedItemUpdateHistory", typeof(string[]), WellKnownPropertySet.LinkedFolder, "LinkedItemUpdateHistory");

		// Token: 0x04004669 RID: 18025
		public static readonly GuidIdPropertyDefinition LinkedDocumentCheckOutTo = InternalSchema.CreateGuidIdProperty("LinkedDocumentCheckOutTo", typeof(string), WellKnownPropertySet.Common, 34241);

		// Token: 0x0400466A RID: 18026
		public static readonly GuidIdPropertyDefinition LinkedDocumentSize = InternalSchema.CreateGuidIdProperty("LinkedDocumentSize", typeof(int), WellKnownPropertySet.Remote, 36613);

		// Token: 0x0400466B RID: 18027
		public static readonly GuidIdPropertyDefinition LinkedPendingState = InternalSchema.CreateGuidIdProperty("LinkedPendingState", typeof(int), WellKnownPropertySet.Common, 34272);

		// Token: 0x0400466C RID: 18028
		public static readonly GuidNamePropertyDefinition LinkedLastFullSyncTime = InternalSchema.CreateGuidNameProperty("LinkedLastFullSyncTime", typeof(ExDateTime), WellKnownPropertySet.LinkedFolder, "LinkedLastFullSyncTime");

		// Token: 0x0400466D RID: 18029
		public static readonly PropertyTagPropertyDefinition LinkedSiteAuthorityUrl = PropertyTagPropertyDefinition.InternalCreate("LinkedSiteAuthorityUrl", PropTag.LinkedSiteAuthorityUrl);

		// Token: 0x0400466E RID: 18030
		public static readonly GuidNamePropertyDefinition UnifiedPolicyNotificationId = InternalSchema.CreateGuidNameProperty("UnifiedPolicyNotificationId", typeof(string), WellKnownPropertySet.UnifiedPolicy, "UnifiedPolicyNotificationId");

		// Token: 0x0400466F RID: 18031
		public static readonly GuidNamePropertyDefinition UnifiedPolicyNotificationData = InternalSchema.CreateGuidNameProperty("UnifiedPolicyNotificationData", typeof(byte[]), WellKnownPropertySet.UnifiedPolicy, "UnifiedPolicyNotificationData", PropertyFlags.Streamable, new PropertyDefinitionConstraint[0]);

		// Token: 0x04004670 RID: 18032
		public static readonly PropertyTagPropertyDefinition ControlDataForSiteMailboxAssistant = PropertyTagPropertyDefinition.InternalCreate("ControlDataForSiteMailboxAssistant", PropTag.ControlDataForSiteMailboxAssistant);

		// Token: 0x04004671 RID: 18033
		public static readonly PropertyTagPropertyDefinition Description = PropertyTagPropertyDefinition.InternalCreate("Description", PropTag.Comment);

		// Token: 0x04004672 RID: 18034
		public static readonly PropertyTagPropertyDefinition MapiImportance = PropertyTagPropertyDefinition.InternalCreate("MapiImportance", PropTag.Importance);

		// Token: 0x04004673 RID: 18035
		public static readonly PropertyTagPropertyDefinition MapiSensitivity = PropertyTagPropertyDefinition.InternalCreate("MapiSensitivity", PropTag.Sensitivity);

		// Token: 0x04004674 RID: 18036
		public static readonly PropertyTagPropertyDefinition MessageAttachments = PropertyTagPropertyDefinition.InternalCreate("MessageAttachments", PropTag.MessageAttachments);

		// Token: 0x04004675 RID: 18037
		public static readonly PropertyTagPropertyDefinition MessageDeepAttachments = PropertyTagPropertyDefinition.InternalCreate("MessageDeepAttachments", PropTag.MessageDeepAttachments);

		// Token: 0x04004676 RID: 18038
		public static readonly PropertyTagPropertyDefinition Size = PropertyTagPropertyDefinition.InternalCreate("Size", PropTag.MessageSize, PropertyFlags.ReadOnly);

		// Token: 0x04004677 RID: 18039
		public static readonly PropertyTagPropertyDefinition ExtendedSize = PropertyTagPropertyDefinition.InternalCreate("ExtendedSize", PropTag.MessageSizeExtended, PropertyFlags.ReadOnly);

		// Token: 0x04004678 RID: 18040
		public static readonly PropertyTagPropertyDefinition ExtendedDumpsterSize = PropertyTagPropertyDefinition.InternalCreate("ExtendedDumpsterSize", PropTag.DeletedMessageSizeExtended, PropertyFlags.ReadOnly);

		// Token: 0x04004679 RID: 18041
		public static readonly PropertyTagPropertyDefinition ExtendedAssociatedItemSize = PropertyTagPropertyDefinition.InternalCreate("ExtendedAssociatedItemSize", PropTag.AssocMessageSizeExtended, PropertyFlags.ReadOnly);

		// Token: 0x0400467A RID: 18042
		public static readonly GuidIdPropertyDefinition Privacy = InternalSchema.CreateGuidIdProperty("Privacy", typeof(bool), WellKnownPropertySet.Common, 34054);

		// Token: 0x0400467B RID: 18043
		public static readonly PropertyTagPropertyDefinition AccessRights = PropertyTagPropertyDefinition.InternalCreate("AccessRights", (PropTag)1715011587U);

		// Token: 0x0400467C RID: 18044
		public static readonly PropertyTagPropertyDefinition AccessLevel = PropertyTagPropertyDefinition.InternalCreate("AccessLevel", PropTag.AccessLevel, PropertyFlags.ReadOnly);

		// Token: 0x0400467D RID: 18045
		public static readonly PropertyTagPropertyDefinition ChangeKey = PropertyTagPropertyDefinition.InternalCreate("ChangeKey", PropTag.ChangeKey, PropertyFlags.ReadOnly);

		// Token: 0x0400467E RID: 18046
		public static readonly PropertyTagPropertyDefinition ArticleId = PropertyTagPropertyDefinition.InternalCreate("ArticleId", PropTag.InternetArticleNumber);

		// Token: 0x0400467F RID: 18047
		public static readonly PropertyTagPropertyDefinition ImapId = PropertyTagPropertyDefinition.InternalCreate("ImapId", PropTag.ImapId);

		// Token: 0x04004680 RID: 18048
		public static readonly PropertyTagPropertyDefinition OriginalSourceServerVersion = PropertyTagPropertyDefinition.InternalCreate("OriginalSourceServerVersion", PropTag.OriginalSourceServerVersion);

		// Token: 0x04004681 RID: 18049
		public static readonly GuidIdPropertyDefinition MarkedForDownload = InternalSchema.CreateGuidIdProperty("MarkedForDownload", typeof(int), WellKnownPropertySet.Common, 34161);

		// Token: 0x04004682 RID: 18050
		public static readonly PropertyTagPropertyDefinition ObjectType = PropertyTagPropertyDefinition.InternalCreate("ObjectType", PropTag.ObjectType);

		// Token: 0x04004683 RID: 18051
		public static readonly PropertyTagPropertyDefinition RowId = PropertyTagPropertyDefinition.InternalCreate("RowId", PropTag.RowId);

		// Token: 0x04004684 RID: 18052
		public static readonly PropertyTagPropertyDefinition RowType = PropertyTagPropertyDefinition.InternalCreate("RowType", PropTag.RowType);

		// Token: 0x04004685 RID: 18053
		public static readonly PropertyTagPropertyDefinition SyncCustomState = PropertyTagPropertyDefinition.InternalCreate("SyncCustomState", PropTag.FavPublicSourceKey, PropertyFlags.Streamable);

		// Token: 0x04004686 RID: 18054
		public static readonly PropertyTagPropertyDefinition SyncFolderChangeKey = PropertyTagPropertyDefinition.InternalCreate("SyncFolderChangeKey", (PropTag)2080637186U);

		// Token: 0x04004687 RID: 18055
		public static readonly PropertyTagPropertyDefinition SyncFolderLastModificationTime = PropertyTagPropertyDefinition.InternalCreate("SyncFolderLastModificationTime", PropTag.DeletedMessageSizeExtendedLastModificationTime);

		// Token: 0x04004688 RID: 18056
		public static readonly PropertyTagPropertyDefinition SyncFolderSourceKey = PropertyTagPropertyDefinition.InternalCreate("SyncFolderSourceId", (PropTag)2080571650U);

		// Token: 0x04004689 RID: 18057
		public static readonly PropertyTagPropertyDefinition SyncState = PropertyTagPropertyDefinition.InternalCreate("SyncState", (PropTag)2081030402U, PropertyFlags.Streamable);

		// Token: 0x0400468A RID: 18058
		public static readonly PropertyTagPropertyDefinition ImapInternalDate = PropertyTagPropertyDefinition.InternalCreate("ImapInternalDate", PropTag.ImapInternalDate);

		// Token: 0x0400468B RID: 18059
		public static readonly PropertyTagPropertyDefinition ImapLastSeenArticleId = PropertyTagPropertyDefinition.InternalCreate("ImapLastSeenArticleId", PropTag.ImapLastArticleId);

		// Token: 0x0400468C RID: 18060
		public static readonly PropertyTagPropertyDefinition ImapSubscribeList = PropertyTagPropertyDefinition.InternalCreate("ImapSubscribeList", PropTag.ImapSubscribeList);

		// Token: 0x0400468D RID: 18061
		public static readonly GuidNamePropertyDefinition ProtocolLog = InternalSchema.CreateGuidNameProperty("ProtocolLog", typeof(byte[]), WellKnownPropertySet.IMAPMsg, "ProtocolLog", PropertyFlags.Streamable, PropertyDefinitionConstraint.None);

		// Token: 0x0400468E RID: 18062
		public static readonly PropertyTagPropertyDefinition MailboxOofStateInternal = PropertyTagPropertyDefinition.InternalCreate("PR_OOF_STATE", PropTag.OofState);

		// Token: 0x0400468F RID: 18063
		public static readonly PropertyTagPropertyDefinition UserName = PropertyTagPropertyDefinition.InternalCreate("PR_USER_NAME", PropTag.UserDisplayName);

		// Token: 0x04004690 RID: 18064
		public static readonly PropertyTagPropertyDefinition MailboxOofStateEx = PropertyTagPropertyDefinition.InternalCreate("PR_OOF_STATE_EX", PropTag.OofStateEx);

		// Token: 0x04004691 RID: 18065
		public static readonly PropertyTagPropertyDefinition UserOofSettingsItemId = PropertyTagPropertyDefinition.InternalCreate("PR_OOF_SETTINGS_ITEM_ID", PropTag.UserOofSettingsItemId);

		// Token: 0x04004692 RID: 18066
		public static readonly PropertyTagPropertyDefinition MailboxOofStateUserChangeTime = PropertyTagPropertyDefinition.InternalCreate("PR_OOF_STATE_USER_CHANGE_TIME", PropTag.OofStateUserChangeTime);

		// Token: 0x04004693 RID: 18067
		public static readonly PropertyTagPropertyDefinition IsContentIndexingEnabled = PropertyTagPropertyDefinition.InternalCreate("IsContentIndexingEnabled", PropTag.CISearchEnabled);

		// Token: 0x04004694 RID: 18068
		public static readonly PropertyTagPropertyDefinition IsMailboxLocalized = PropertyTagPropertyDefinition.InternalCreate("IsMailboxLocalized", PropTag.Localized);

		// Token: 0x04004695 RID: 18069
		public static readonly GuidIdPropertyDefinition UnifiedMessagingOptions = InternalSchema.CreateGuidIdProperty("UnifiedMessagingOptions", typeof(int), WellKnownPropertySet.UnifiedMessaging, 1);

		// Token: 0x04004696 RID: 18070
		public static readonly GuidIdPropertyDefinition OfficeCommunicatorOptions = InternalSchema.CreateGuidIdProperty("OfficeCommunicatorOptions", typeof(int), WellKnownPropertySet.UnifiedMessaging, 2);

		// Token: 0x04004697 RID: 18071
		public static readonly PropertyTagPropertyDefinition DefaultFoldersLocaleId = PropertyTagPropertyDefinition.InternalCreate("DefaultFolderLocaleId", PropTag.DefaultFoldersLocaleId);

		// Token: 0x04004698 RID: 18072
		public static readonly PropertyTagPropertyDefinition SendReadNotifications = PropertyTagPropertyDefinition.InternalCreate("SendReadNotifications", PropTag.InternetMdns);

		// Token: 0x04004699 RID: 18073
		public static readonly PropertyTagPropertyDefinition DraftsFolderEntryId = PropertyTagPropertyDefinition.InternalCreate("DraftsFolderEntryId", PropTag.DraftsFolderEntryId);

		// Token: 0x0400469A RID: 18074
		public static readonly PropertyTagPropertyDefinition AdditionalRenEntryIds = PropertyTagPropertyDefinition.InternalCreate("AdditionalRenEntryIds", (PropTag)920129794U);

		// Token: 0x0400469B RID: 18075
		public static readonly PropertyTagPropertyDefinition AdditionalRenEntryIdsEx = PropertyTagPropertyDefinition.InternalCreate("AdditionalRenEntryIdsEx", (PropTag)920191234U);

		// Token: 0x0400469C RID: 18076
		public static readonly PropertyTagPropertyDefinition RemindersSearchFolderEntryId = PropertyTagPropertyDefinition.InternalCreate("RemindersSearchFolderEntryId", (PropTag)919929090U);

		// Token: 0x0400469D RID: 18077
		public static readonly PropertyTagPropertyDefinition DeferredActionFolderEntryId = PropertyTagPropertyDefinition.InternalCreate("DeferredActionFolderEntryId", PropTag.DeferredActionFolderEntryID);

		// Token: 0x0400469E RID: 18078
		public static readonly PropertyTagPropertyDefinition LegacyScheduleFolderEntryId = PropertyTagPropertyDefinition.InternalCreate("LegacyScheduleFolderEntryId", (PropTag)1713242370U);

		// Token: 0x0400469F RID: 18079
		public static readonly PropertyTagPropertyDefinition LegacyShortcutsFolderEntryId = PropertyTagPropertyDefinition.InternalCreate("LegacyShortcutsFolderEntryId", PropTag.LegacyShortcutsFolderEntryId);

		// Token: 0x040046A0 RID: 18080
		public static readonly PropertyTagPropertyDefinition LegacyViewsFolderEntryId = PropertyTagPropertyDefinition.InternalCreate("LegacyViewsFolderEntryId", PropTag.ViewsEntryId);

		// Token: 0x040046A1 RID: 18081
		public static readonly PropertyTagPropertyDefinition DeletedItemsEntryId = PropertyTagPropertyDefinition.InternalCreate("DeletedItemsEntryId", PropTag.IpmWasteBasketEntryId);

		// Token: 0x040046A2 RID: 18082
		public static readonly PropertyTagPropertyDefinition SentItemsEntryId = PropertyTagPropertyDefinition.InternalCreate("SentItemsEntryId", PropTag.IpmSentMailEntryId);

		// Token: 0x040046A3 RID: 18083
		public static readonly PropertyTagPropertyDefinition OutboxEntryId = PropertyTagPropertyDefinition.InternalCreate("OutboxEntryId", PropTag.IpmOutboxEntryId);

		// Token: 0x040046A4 RID: 18084
		public static readonly PropertyTagPropertyDefinition CalendarFolderEntryId = PropertyTagPropertyDefinition.InternalCreate("CalendarFolderId", PropTag.CalendarFolderEntryId);

		// Token: 0x040046A5 RID: 18085
		public static readonly PropertyTagPropertyDefinition ContactsFolderEntryId = PropertyTagPropertyDefinition.InternalCreate("ContactsFolderEntryId", PropTag.ContactsFolderEntryId);

		// Token: 0x040046A6 RID: 18086
		public static readonly PropertyTagPropertyDefinition JournalFolderEntryId = PropertyTagPropertyDefinition.InternalCreate("JournalFolderEntryId", PropTag.JournalFolderEntryId);

		// Token: 0x040046A7 RID: 18087
		public static readonly PropertyTagPropertyDefinition NotesFolderEntryId = PropertyTagPropertyDefinition.InternalCreate("NotesFolderEntryId", PropTag.NotesFolderEntryId);

		// Token: 0x040046A8 RID: 18088
		public static readonly PropertyTagPropertyDefinition TasksFolderEntryId = PropertyTagPropertyDefinition.InternalCreate("TasksFolderEntryId", PropTag.TasksFolderEntryId);

		// Token: 0x040046A9 RID: 18089
		public static readonly PropertyTagPropertyDefinition FinderEntryId = PropertyTagPropertyDefinition.InternalCreate("FinderEntryId", PropTag.FinderEntryId);

		// Token: 0x040046AA RID: 18090
		public static readonly PropertyTagPropertyDefinition CommonViewsEntryId = PropertyTagPropertyDefinition.InternalCreate("CommonViewsEntryId", PropTag.CommonViewsEntryId);

		// Token: 0x040046AB RID: 18091
		public static readonly PropertyTagPropertyDefinition ElcRootFolderEntryId = PropertyTagPropertyDefinition.InternalCreate("ElcRootFolderEntryId", PropTag.SpoolerQueueEntryId);

		// Token: 0x040046AC RID: 18092
		public static readonly GuidNamePropertyDefinition RetentionTagEntryId = InternalSchema.CreateGuidNameProperty("RetentionTagEntryId", typeof(byte[]), WellKnownPropertySet.Elc, "RetentionTagEntryId");

		// Token: 0x040046AD RID: 18093
		public static readonly PropertyTagPropertyDefinition CommunicatorHistoryFolderEntryId = PropertyTagPropertyDefinition.InternalCreate("CommunicatorHistoryFolderEntryId", (PropTag)904462594U);

		// Token: 0x040046AE RID: 18094
		public static readonly PropertyTagPropertyDefinition SyncRootFolderEntryId = PropertyTagPropertyDefinition.InternalCreate("SyncRootFolderEntryId", (PropTag)904528130U);

		// Token: 0x040046AF RID: 18095
		public static readonly PropertyTagPropertyDefinition UMVoicemailFolderEntryId = PropertyTagPropertyDefinition.InternalCreate("UMVoicemailFolderEntryId", (PropTag)904593666U);

		// Token: 0x040046B0 RID: 18096
		public static readonly PropertyTagPropertyDefinition UMFaxFolderEntryId = PropertyTagPropertyDefinition.InternalCreate("UMFaxFolderEntryId", (PropTag)918487298U);

		// Token: 0x040046B1 RID: 18097
		public static readonly PropertyTagPropertyDefinition SharingFolderEntryId = PropertyTagPropertyDefinition.InternalCreate("SharingFolderEntryId", PropTag.SharingFolderEntryId);

		// Token: 0x040046B2 RID: 18098
		public static readonly PropertyTagPropertyDefinition AllItemsFolderEntryId = PropertyTagPropertyDefinition.InternalCreate("AllItemsFolderEntryId", PropTag.AllItemsEntryId);

		// Token: 0x040046B3 RID: 18099
		public static readonly GuidNamePropertyDefinition RecoverableItemsRootFolderEntryId = InternalSchema.CreateGuidNameProperty("DumpsterEntryId", typeof(byte[]), WellKnownPropertySet.Elc, "DumpsterEntryId");

		// Token: 0x040046B4 RID: 18100
		public static readonly GuidNamePropertyDefinition RecoverableItemsDeletionsFolderEntryId = InternalSchema.CreateGuidNameProperty("RecoverableItemsDeletionsEntryId", typeof(byte[]), WellKnownPropertySet.Elc, "RecoverableItemsDeletionsEntryId");

		// Token: 0x040046B5 RID: 18101
		public static readonly GuidNamePropertyDefinition RecoverableItemsVersionsFolderEntryId = InternalSchema.CreateGuidNameProperty("RecoverableItemsVersionsEntryId", typeof(byte[]), WellKnownPropertySet.Elc, "RecoverableItemsVersionsEntryId");

		// Token: 0x040046B6 RID: 18102
		public static readonly GuidNamePropertyDefinition RecoverableItemsPurgesFolderEntryId = InternalSchema.CreateGuidNameProperty("RecoverableItemsPurgesEntryId", typeof(byte[]), WellKnownPropertySet.Elc, "RecoverableItemsPurgesEntryId");

		// Token: 0x040046B7 RID: 18103
		public static readonly GuidNamePropertyDefinition RecoverableItemsDiscoveryHoldsFolderEntryId = InternalSchema.CreateGuidNameProperty("RecoverableItemsDiscoveryHoldsEntryId", typeof(byte[]), WellKnownPropertySet.Elc, "RecoverableItemsDiscoveryHoldsEntryId");

		// Token: 0x040046B8 RID: 18104
		public static readonly GuidNamePropertyDefinition RecoverableItemsMigratedMessagesFolderEntryId = InternalSchema.CreateGuidNameProperty("RecoverableItemsMigratedMessagesEntryId", typeof(byte[]), WellKnownPropertySet.Elc, "RecoverableItemsMigratedMessagesEntryId");

		// Token: 0x040046B9 RID: 18105
		public static readonly GuidNamePropertyDefinition CalendarLoggingFolderEntryId = InternalSchema.CreateGuidNameProperty("CalendarLoggingEntryId", typeof(byte[]), WellKnownPropertySet.Elc, "CalendarLoggingEntryId");

		// Token: 0x040046BA RID: 18106
		public static readonly PropertyTagPropertyDefinition SystemFolderEntryId = PropertyTagPropertyDefinition.InternalCreate("SystemFolderEntryId", (PropTag)905773314U);

		// Token: 0x040046BB RID: 18107
		public static readonly GuidNamePropertyDefinition CalendarVersionStoreFolderEntryId = InternalSchema.CreateGuidNameProperty("CalendarVersionStoreEntryId", typeof(byte[]), WellKnownPropertySet.CalendarAssistant, "CalendarVersionStoreEntryId");

		// Token: 0x040046BC RID: 18108
		public static readonly GuidNamePropertyDefinition AdminAuditLogsFolderEntryId = InternalSchema.CreateGuidNameProperty("AdminAuditLogsFolderEntryId", typeof(byte[]), WellKnownPropertySet.Elc, "AdminAuditLogsFolderEntryId");

		// Token: 0x040046BD RID: 18109
		public static readonly GuidNamePropertyDefinition AuditsFolderEntryId = InternalSchema.CreateGuidNameProperty("AuditsFolderEntryId", typeof(byte[]), WellKnownPropertySet.Elc, "AuditsFolderEntryId");

		// Token: 0x040046BE RID: 18110
		public static readonly GuidNamePropertyDefinition RecipientCacheFolderEntryId = InternalSchema.CreateGuidNameProperty("RecipientCacheFolderEntryId", typeof(byte[]), WellKnownPropertySet.Common, "RecipientCacheFolderEntryId");

		// Token: 0x040046BF RID: 18111
		public static readonly GuidNamePropertyDefinition SmsAndChatsSyncFolderEntryId = InternalSchema.CreateGuidNameProperty("SmsAndChatsSyncFolderEntryId", typeof(byte[]), WellKnownPropertySet.Common, "SmsAndChatsSyncFolderEntryId");

		// Token: 0x040046C0 RID: 18112
		public static readonly GuidNamePropertyDefinition GalContactsFolderEntryId = InternalSchema.CreateGuidNameProperty("GALContactsFolderEntryId", typeof(byte[]), WellKnownPropertySet.Common, "GALContactsFolderEntryId");

		// Token: 0x040046C1 RID: 18113
		public static readonly GuidNamePropertyDefinition QuickContactsFolderEntryId = InternalSchema.CreateGuidNameProperty("QuickContactsFolderEntryId", typeof(byte[]), WellKnownPropertySet.UnifiedContactStore, "QuickContactsFolderEntryId");

		// Token: 0x040046C2 RID: 18114
		public static readonly GuidNamePropertyDefinition ImContactListFolderEntryId = InternalSchema.CreateGuidNameProperty("ImContactListFolderEntryId", typeof(byte[]), WellKnownPropertySet.UnifiedContactStore, "ImContactListFolderEntryId");

		// Token: 0x040046C3 RID: 18115
		public static readonly GuidNamePropertyDefinition OrganizationalContactsFolderEntryId = InternalSchema.CreateGuidNameProperty("OrganizationalContactsFolderEntryId", typeof(byte[]), WellKnownPropertySet.UnifiedContactStore, "OrganizationalContactsFolderEntryId");

		// Token: 0x040046C4 RID: 18116
		public static readonly GuidNamePropertyDefinition LegacyArchiveJournalsFolderEntryId = InternalSchema.CreateGuidNameProperty("LegacyArchiveJournalsFolderEntryId", typeof(byte[]), WellKnownPropertySet.Messaging, "LegacyArchiveJournalsFolderEntryId");

		// Token: 0x040046C5 RID: 18117
		public static readonly GuidNamePropertyDefinition DocumentSyncIssuesFolderEntryId = InternalSchema.CreateGuidNameProperty("DocumentSyncIssuesFolderEntryId", typeof(byte[]), WellKnownPropertySet.LinkedFolder, "DocumentSyncIssuesFolderEntryId");

		// Token: 0x040046C6 RID: 18118
		public static readonly GuidIdPropertyDefinition AttendeeCriticalChangeTime = InternalSchema.CreateGuidIdProperty("AttendeeCriticalChangeTime", typeof(ExDateTime), WellKnownPropertySet.Meeting, 1);

		// Token: 0x040046C7 RID: 18119
		public static readonly GuidIdPropertyDefinition CalendarProcessed = InternalSchema.CreateGuidIdProperty("CalendarProcessed", typeof(bool), WellKnownPropertySet.CalendarAssistant, 1);

		// Token: 0x040046C8 RID: 18120
		public static readonly GuidIdPropertyDefinition CalendarProcessingSteps = InternalSchema.CreateGuidIdProperty("CalendarProcessingSteps", typeof(int), WellKnownPropertySet.CalendarAssistant, 3);

		// Token: 0x040046C9 RID: 18121
		public static readonly GuidIdPropertyDefinition OriginalMeetingType = InternalSchema.CreateGuidIdProperty("OriginalMeetingType", typeof(int), WellKnownPropertySet.CalendarAssistant, 4);

		// Token: 0x040046CA RID: 18122
		public static readonly GuidIdPropertyDefinition ChangeList = InternalSchema.CreateGuidIdProperty("ChangeList", typeof(byte[]), WellKnownPropertySet.CalendarAssistant, 5);

		// Token: 0x040046CB RID: 18123
		public static readonly GuidIdPropertyDefinition CalendarLogTriggerAction = InternalSchema.CreateGuidIdProperty("CalendarLogTriggerAction", typeof(string), WellKnownPropertySet.CalendarAssistant, 6);

		// Token: 0x040046CC RID: 18124
		public static readonly GuidIdPropertyDefinition OriginalFolderId = InternalSchema.CreateGuidIdProperty("OriginalFolderId", typeof(byte[]), WellKnownPropertySet.CalendarAssistant, 7);

		// Token: 0x040046CD RID: 18125
		public static readonly GuidIdPropertyDefinition OriginalCreationTime = InternalSchema.CreateGuidIdProperty("OriginalCreationTime", typeof(ExDateTime), WellKnownPropertySet.CalendarAssistant, 8);

		// Token: 0x040046CE RID: 18126
		public static readonly GuidIdPropertyDefinition OriginalLastModifiedTime = InternalSchema.CreateGuidIdProperty("OriginalLastModifiedTime", typeof(ExDateTime), WellKnownPropertySet.CalendarAssistant, 9);

		// Token: 0x040046CF RID: 18127
		public static readonly GuidIdPropertyDefinition ResponsibleUserName = InternalSchema.CreateGuidIdProperty("ResponsibleUserName", typeof(string), WellKnownPropertySet.CalendarAssistant, 10);

		// Token: 0x040046D0 RID: 18128
		public static readonly GuidIdPropertyDefinition ClientInfoString = InternalSchema.CreateGuidIdProperty("ClientInfoString", typeof(string), WellKnownPropertySet.CalendarAssistant, 11);

		// Token: 0x040046D1 RID: 18129
		public static readonly GuidIdPropertyDefinition ClientProcessName = InternalSchema.CreateGuidIdProperty("ClientProcessName", typeof(string), WellKnownPropertySet.CalendarAssistant, 12);

		// Token: 0x040046D2 RID: 18130
		public static readonly GuidIdPropertyDefinition ClientMachineName = InternalSchema.CreateGuidIdProperty("ClientMachineName", typeof(string), WellKnownPropertySet.CalendarAssistant, 13);

		// Token: 0x040046D3 RID: 18131
		public static readonly GuidIdPropertyDefinition ClientBuildVersion = InternalSchema.CreateGuidIdProperty("ClientBuildVersion", typeof(string), WellKnownPropertySet.CalendarAssistant, 14);

		// Token: 0x040046D4 RID: 18132
		public static readonly GuidIdPropertyDefinition MiddleTierProcessName = InternalSchema.CreateGuidIdProperty("MiddleTierProcessName", typeof(string), WellKnownPropertySet.CalendarAssistant, 15);

		// Token: 0x040046D5 RID: 18133
		public static readonly GuidIdPropertyDefinition MiddleTierServerName = InternalSchema.CreateGuidIdProperty("MiddleTierServerName", typeof(string), WellKnownPropertySet.CalendarAssistant, 16);

		// Token: 0x040046D6 RID: 18134
		public static readonly GuidIdPropertyDefinition MiddleTierServerBuildVersion = InternalSchema.CreateGuidIdProperty("MiddleTierServerBuildVersion", typeof(string), WellKnownPropertySet.CalendarAssistant, 17);

		// Token: 0x040046D7 RID: 18135
		public static readonly GuidIdPropertyDefinition MailboxServerName = InternalSchema.CreateGuidIdProperty("ServerName", typeof(string), WellKnownPropertySet.CalendarAssistant, 18);

		// Token: 0x040046D8 RID: 18136
		public static readonly GuidIdPropertyDefinition MailboxServerBuildVersion = InternalSchema.CreateGuidIdProperty("ServerBuildVersion", typeof(string), WellKnownPropertySet.CalendarAssistant, 19);

		// Token: 0x040046D9 RID: 18137
		public static readonly GuidIdPropertyDefinition MailboxDatabaseName = InternalSchema.CreateGuidIdProperty("MailboxDatabaseName", typeof(string), WellKnownPropertySet.CalendarAssistant, 20);

		// Token: 0x040046DA RID: 18138
		public static readonly GuidIdPropertyDefinition ClientIntent = InternalSchema.CreateGuidIdProperty("ClientIntent", typeof(int), WellKnownPropertySet.CalendarAssistant, 21, PropertyFlags.SetIfNotChanged, new PropertyDefinitionConstraint[0]);

		// Token: 0x040046DB RID: 18139
		public static readonly GuidIdPropertyDefinition ItemVersion = InternalSchema.CreateGuidIdProperty("ItemVersion", typeof(int), WellKnownPropertySet.CalendarAssistant, 22);

		// Token: 0x040046DC RID: 18140
		public static readonly GuidIdPropertyDefinition OriginalEntryId = InternalSchema.CreateGuidIdProperty("OriginalEntryId", typeof(byte[]), WellKnownPropertySet.CalendarAssistant, 23);

		// Token: 0x040046DD RID: 18141
		public static readonly GuidIdPropertyDefinition CalendarOriginatorId = InternalSchema.CreateGuidIdProperty("CalendarOriginatorId", typeof(string), WellKnownPropertySet.CalendarAssistant, 24, PropertyFlags.TrackChange, new PropertyDefinitionConstraint[0]);

		// Token: 0x040046DE RID: 18142
		public static readonly GuidIdPropertyDefinition HijackedMeeting = InternalSchema.CreateGuidIdProperty("HijackedMeeting", typeof(bool), WellKnownPropertySet.CalendarAssistant, 25);

		// Token: 0x040046DF RID: 18143
		public static readonly GuidIdPropertyDefinition MFNAddedRecipients = InternalSchema.CreateGuidIdProperty("MFNAddedRecipients", typeof(byte[]), WellKnownPropertySet.CalendarAssistant, 32, PropertyFlags.Streamable, new PropertyDefinitionConstraint[0]);

		// Token: 0x040046E0 RID: 18144
		public static readonly GuidIdPropertyDefinition ViewStartTime = InternalSchema.CreateGuidIdProperty("ViewStartTime", typeof(ExDateTime), WellKnownPropertySet.CalendarAssistant, 33);

		// Token: 0x040046E1 RID: 18145
		public static readonly GuidIdPropertyDefinition ViewEndTime = InternalSchema.CreateGuidIdProperty("ViewEndTime", typeof(ExDateTime), WellKnownPropertySet.CalendarAssistant, 34);

		// Token: 0x040046E2 RID: 18146
		public static readonly GuidIdPropertyDefinition CalendarFolderVersion = InternalSchema.CreateGuidIdProperty("CalendarFolderVersion", typeof(int), WellKnownPropertySet.CalendarAssistant, 35);

		// Token: 0x040046E3 RID: 18147
		public static readonly GuidIdPropertyDefinition HasAttendees = InternalSchema.CreateGuidIdProperty("HasAttendees", typeof(bool), WellKnownPropertySet.CalendarAssistant, 36);

		// Token: 0x040046E4 RID: 18148
		public static readonly GuidIdPropertyDefinition CharmId = InternalSchema.CreateGuidIdProperty("CharmId", typeof(string), WellKnownPropertySet.CalendarAssistant, 37);

		// Token: 0x040046E5 RID: 18149
		public static readonly GuidNamePropertyDefinition CalendarInteropActionQueueInternal = InternalSchema.CreateGuidNameProperty("CalendarInteropActionQueueInternal", typeof(byte[]), WellKnownPropertySet.CalendarAssistant, "CalendarInteropActionQueueInternal", PropertyFlags.Streamable, new PropertyDefinitionConstraint[0]);

		// Token: 0x040046E6 RID: 18150
		public static readonly GuidNamePropertyDefinition CalendarInteropActionQueueHasDataInternal = InternalSchema.CreateGuidNameProperty("CalendarInteropActionQueueHasDataInternal", typeof(bool), WellKnownPropertySet.CalendarAssistant, "CalendarInteropActionQueueHasDataInternal");

		// Token: 0x040046E7 RID: 18151
		public static readonly GuidNamePropertyDefinition LastExecutedCalendarInteropAction = InternalSchema.CreateGuidNameProperty("LastExecutedCalendarInteropAction", typeof(Guid), WellKnownPropertySet.CalendarAssistant, "LastExecutedCalendarInteropAction");

		// Token: 0x040046E8 RID: 18152
		public static readonly GuidIdPropertyDefinition ChangeHighlight = InternalSchema.CreateGuidIdProperty("ChangeHighlight", typeof(int), WellKnownPropertySet.Appointment, 33284);

		// Token: 0x040046E9 RID: 18153
		public static readonly GuidIdPropertyDefinition IntendedFreeBusyStatus = InternalSchema.CreateGuidIdProperty("IntendedFreeBusyStatus", typeof(int), WellKnownPropertySet.Appointment, 33316);

		// Token: 0x040046EA RID: 18154
		public static readonly PropertyTagPropertyDefinition IsProcessed = PropertyTagPropertyDefinition.InternalCreate("IsProcessed", (PropTag)2097217547U);

		// Token: 0x040046EB RID: 18155
		public static readonly GuidIdPropertyDefinition MeetingRequestType = InternalSchema.CreateGuidIdProperty("MeetingRequestType", typeof(int), WellKnownPropertySet.Meeting, 38);

		// Token: 0x040046EC RID: 18156
		public static readonly GuidIdPropertyDefinition OldLocation = InternalSchema.CreateGuidIdProperty("OldLocation", typeof(string), WellKnownPropertySet.Meeting, 40);

		// Token: 0x040046ED RID: 18157
		public static readonly GuidIdPropertyDefinition OldStartWhole = InternalSchema.CreateGuidIdProperty("OldStartWhole", typeof(ExDateTime), WellKnownPropertySet.Meeting, 41);

		// Token: 0x040046EE RID: 18158
		public static readonly GuidIdPropertyDefinition OldEndWhole = InternalSchema.CreateGuidIdProperty("OldEndWhole", typeof(ExDateTime), WellKnownPropertySet.Meeting, 42);

		// Token: 0x040046EF RID: 18159
		public static readonly PropertyTagPropertyDefinition OwnerAppointmentID = PropertyTagPropertyDefinition.InternalCreate("OwnerAppointmentID", PropTag.OwnerApptId);

		// Token: 0x040046F0 RID: 18160
		public static readonly GuidIdPropertyDefinition OwnerCriticalChangeTime = InternalSchema.CreateGuidIdProperty("OwnerCriticalChangeTime", typeof(ExDateTime), WellKnownPropertySet.Meeting, 26);

		// Token: 0x040046F1 RID: 18161
		public static readonly GuidIdPropertyDefinition LidSingleInvite = InternalSchema.CreateGuidIdProperty("LID_SINGLE_INVITE", typeof(bool), WellKnownPropertySet.Meeting, 11);

		// Token: 0x040046F2 RID: 18162
		public static readonly GuidIdPropertyDefinition LidTimeZone = InternalSchema.CreateGuidIdProperty("LID_TIME_ZONE", typeof(int), WellKnownPropertySet.Meeting, 12);

		// Token: 0x040046F3 RID: 18163
		public static readonly GuidIdPropertyDefinition StartRecurDate = InternalSchema.CreateGuidIdProperty("LID_START_RECUR_DATE", typeof(int), WellKnownPropertySet.Meeting, 13);

		// Token: 0x040046F4 RID: 18164
		public static readonly GuidIdPropertyDefinition StartRecurTime = InternalSchema.CreateGuidIdProperty("LID_START_RECUR_TIME", typeof(int), WellKnownPropertySet.Meeting, 14);

		// Token: 0x040046F5 RID: 18165
		public static readonly GuidIdPropertyDefinition EndRecurDate = InternalSchema.CreateGuidIdProperty("LID_END_RECUR_DATE", typeof(int), WellKnownPropertySet.Meeting, 15);

		// Token: 0x040046F6 RID: 18166
		public static readonly GuidIdPropertyDefinition EndRecurTime = InternalSchema.CreateGuidIdProperty("LID_END_RECUR_TIME", typeof(int), WellKnownPropertySet.Meeting, 16);

		// Token: 0x040046F7 RID: 18167
		public static readonly GuidIdPropertyDefinition LidDayInterval = InternalSchema.CreateGuidIdProperty("LID_DAY_INTERVAL", typeof(short), WellKnownPropertySet.Meeting, 17);

		// Token: 0x040046F8 RID: 18168
		public static readonly GuidIdPropertyDefinition LidWeekInterval = InternalSchema.CreateGuidIdProperty("LID_WEEK_INTERVAL", typeof(short), WellKnownPropertySet.Meeting, 18);

		// Token: 0x040046F9 RID: 18169
		public static readonly GuidIdPropertyDefinition LidMonthInterval = InternalSchema.CreateGuidIdProperty("LID_MONTH_INTERVAL", typeof(short), WellKnownPropertySet.Meeting, 19);

		// Token: 0x040046FA RID: 18170
		public static readonly GuidIdPropertyDefinition LidYearInterval = InternalSchema.CreateGuidIdProperty("LID_YEAR_INTERVAL", typeof(short), WellKnownPropertySet.Meeting, 20);

		// Token: 0x040046FB RID: 18171
		public static readonly GuidIdPropertyDefinition LidDayOfWeekMask = InternalSchema.CreateGuidIdProperty("LID_DOW_MASK", typeof(int), WellKnownPropertySet.Meeting, 21);

		// Token: 0x040046FC RID: 18172
		public static readonly GuidIdPropertyDefinition LidDayOfMonthMask = InternalSchema.CreateGuidIdProperty("LID_DOM_MASK", typeof(int), WellKnownPropertySet.Meeting, 22);

		// Token: 0x040046FD RID: 18173
		public static readonly GuidIdPropertyDefinition LidMonthOfYearMask = InternalSchema.CreateGuidIdProperty("LID_MOY_MASK", typeof(int), WellKnownPropertySet.Meeting, 23);

		// Token: 0x040046FE RID: 18174
		public static readonly GuidIdPropertyDefinition LidRecurType = InternalSchema.CreateGuidIdProperty("LID_RECUR_TYPE", typeof(short), WellKnownPropertySet.Meeting, 24);

		// Token: 0x040046FF RID: 18175
		public static readonly GuidIdPropertyDefinition LidFirstDayOfWeek = InternalSchema.CreateGuidIdProperty("LID_DOW_PREF", typeof(short), WellKnownPropertySet.Meeting, 25);

		// Token: 0x04004700 RID: 18176
		public static readonly PropertyTagPropertyDefinition AddrType = PropertyTagPropertyDefinition.InternalCreate("AddrType", PropTag.AddrType, new PropertyDefinitionConstraint[]
		{
			new NonMoveMailboxPropertyConstraint(new StringLengthConstraint(0, 64))
		});

		// Token: 0x04004701 RID: 18177
		public static readonly PropertyTagPropertyDefinition RecipientFlags = PropertyTagPropertyDefinition.InternalCreate("RecipientFlags", PropTag.RecipientFlags);

		// Token: 0x04004702 RID: 18178
		public static readonly PropertyTagPropertyDefinition RecipientTrackStatus = PropertyTagPropertyDefinition.InternalCreate("RecipientTrackStatus", PropTag.RecipientTrackStatus);

		// Token: 0x04004703 RID: 18179
		public static readonly PropertyTagPropertyDefinition RecipientTrackStatusTime = PropertyTagPropertyDefinition.InternalCreate("RecipientTrackStatusTime", PropTag.RecipientTrackStatusTime);

		// Token: 0x04004704 RID: 18180
		public static readonly PropertyTagPropertyDefinition RecipientType = PropertyTagPropertyDefinition.InternalCreate("RecipientType", PropTag.RecipientType);

		// Token: 0x04004705 RID: 18181
		public static readonly PropertyTagPropertyDefinition SmtpAddress = PropertyTagPropertyDefinition.InternalCreate("SmtpAddress", PropTag.SmtpAddress);

		// Token: 0x04004706 RID: 18182
		public static readonly PropertyTagPropertyDefinition SenderSmtpAddress = PropertyTagPropertyDefinition.InternalCreate("SenderSmtpAddress", PropTag.SenderSmtpAddress);

		// Token: 0x04004707 RID: 18183
		public static readonly PropertyTagPropertyDefinition SentRepresentingSmtpAddress = PropertyTagPropertyDefinition.InternalCreate("SentRepresentingSmtpAddress", PropTag.SentRepresentingSmtpAddress);

		// Token: 0x04004708 RID: 18184
		public static readonly PropertyTagPropertyDefinition ReceivedBySmtpAddress = PropertyTagPropertyDefinition.InternalCreate("ReceivedBySmtpAddress", PropTag.ReceivedBySmtpAddress);

		// Token: 0x04004709 RID: 18185
		public static readonly PropertyTagPropertyDefinition ReceivedRepresentingSmtpAddress = PropertyTagPropertyDefinition.InternalCreate("ReceivedRepresentingSmtpAddress", PropTag.RcvdRepresentingSmtpAddress);

		// Token: 0x0400470A RID: 18186
		public static readonly PropertyTagPropertyDefinition OriginalSentRepresentingSmtpAddress = PropertyTagPropertyDefinition.InternalCreate("OriginalSentRepresentingSmtpAddress", (PropTag)1560543263U);

		// Token: 0x0400470B RID: 18187
		public static readonly PropertyTagPropertyDefinition OriginalSenderSmtpAddress = PropertyTagPropertyDefinition.InternalCreate("OriginalSenderSmtpAddress", (PropTag)1560477727U);

		// Token: 0x0400470C RID: 18188
		public static readonly PropertyTagPropertyDefinition OriginalAuthorSmtpAddress = PropertyTagPropertyDefinition.InternalCreate("OriginalAuthorSmtpAddress", (PropTag)1560674335U);

		// Token: 0x0400470D RID: 18189
		public static readonly PropertyTagPropertyDefinition ReadReceiptSmtpAddress = PropertyTagPropertyDefinition.InternalCreate("ReadReceiptSmtpAddress", (PropTag)1560608799U);

		// Token: 0x0400470E RID: 18190
		public static readonly PropertyTagPropertyDefinition CreationTime = PropertyTagPropertyDefinition.InternalCreate("CreationTime", PropTag.CreationTime);

		// Token: 0x0400470F RID: 18191
		public static readonly PropertyTagPropertyDefinition DisplayName = PropertyTagPropertyDefinition.InternalCreate("DisplayName", PropTag.DisplayName);

		// Token: 0x04004710 RID: 18192
		public static readonly GuidNamePropertyDefinition PhoneticDisplayName = InternalSchema.CreateGuidNameProperty("PhoneticDisplayName", typeof(string), WellKnownPropertySet.PublicStrings, "PhoneticDisplayName");

		// Token: 0x04004711 RID: 18193
		public static readonly PropertyTagPropertyDefinition EntryId = PropertyTagPropertyDefinition.InternalCreate("EntryId", PropTag.EntryId);

		// Token: 0x04004712 RID: 18194
		public static readonly PropertyTagPropertyDefinition FavoriteDisplayAlias = PropertyTagPropertyDefinition.InternalCreate("FavoriteDisplayAlias", PropTag.FavoriteDisplayAlias);

		// Token: 0x04004713 RID: 18195
		public static readonly PropertyTagPropertyDefinition FavoriteDisplayName = PropertyTagPropertyDefinition.InternalCreate("FavoriteDisplayName", PropTag.FavoriteDisplayName);

		// Token: 0x04004714 RID: 18196
		public static readonly PropertyTagPropertyDefinition FavLevelMask = PropertyTagPropertyDefinition.InternalCreate("FavLevelMask", PropTag.FavLevelMask);

		// Token: 0x04004715 RID: 18197
		public static readonly PropertyTagPropertyDefinition FavPublicSourceKey = PropertyTagPropertyDefinition.InternalCreate("FavPublicSourceKey", PropTag.FavPublicSourceKey);

		// Token: 0x04004716 RID: 18198
		public static readonly PropertyTagPropertyDefinition DocumentId = PropertyTagPropertyDefinition.InternalCreate("DocumentId", PropTag.DocumentId);

		// Token: 0x04004717 RID: 18199
		public static readonly PropertyTagPropertyDefinition ConversationDocumentId = PropertyTagPropertyDefinition.InternalCreate("ConversationDocumentId", PropTag.ConversationDocumentId);

		// Token: 0x04004718 RID: 18200
		public static readonly PropertyTagPropertyDefinition ParentSourceKey = PropertyTagPropertyDefinition.InternalCreate("ParentSourceKey", PropTag.ParentSourceKey);

		// Token: 0x04004719 RID: 18201
		public static readonly PropertyTagPropertyDefinition SourceKey = PropertyTagPropertyDefinition.InternalCreate("SourceKey", PropTag.SourceKey);

		// Token: 0x0400471A RID: 18202
		public static readonly PropertyTagPropertyDefinition PredecessorChangeList = PropertyTagPropertyDefinition.InternalCreate("PredecessorChangeList", PropTag.PredecessorChangeList);

		// Token: 0x0400471B RID: 18203
		public static readonly PropertyTagPropertyDefinition StoreEntryId = PropertyTagPropertyDefinition.InternalCreate("StoreEntryId", PropTag.StoreEntryid);

		// Token: 0x0400471C RID: 18204
		public static readonly PropertyTagPropertyDefinition StoreRecordKey = PropertyTagPropertyDefinition.InternalCreate("StoreRecordKey", PropTag.StoreRecordKey);

		// Token: 0x0400471D RID: 18205
		public static readonly PropertyTagPropertyDefinition StoreSupportMask = PropertyTagPropertyDefinition.InternalCreate("StoreSupportMask", PropTag.StoreSupportMask);

		// Token: 0x0400471E RID: 18206
		public static readonly PropertyTagPropertyDefinition ItemClass = PropertyTagPropertyDefinition.InternalCreate("ItemClass", PropTag.MessageClass, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 255)
		});

		// Token: 0x0400471F RID: 18207
		public static readonly GuidNamePropertyDefinition OriginalMimeReadTime = InternalSchema.CreateGuidNameProperty("OriginalMimeReadTime", typeof(ExDateTime), WellKnownPropertySet.Attachment, "OriginalMimeReadTime");

		// Token: 0x04004720 RID: 18208
		public static readonly PropertyTagPropertyDefinition LastModifiedTime = PropertyTagPropertyDefinition.InternalCreate("LastModifiedTime", PropTag.LastModificationTime);

		// Token: 0x04004721 RID: 18209
		public static readonly PropertyTagPropertyDefinition LastModifierName = PropertyTagPropertyDefinition.InternalCreate("LastModifierName", (PropTag)1073348639U);

		// Token: 0x04004722 RID: 18210
		public static readonly PropertyTagPropertyDefinition LastModifierEntryId = PropertyTagPropertyDefinition.InternalCreate("LastModifierEntryId", (PropTag)1073414402U);

		// Token: 0x04004723 RID: 18211
		public static readonly PropertyTagPropertyDefinition CreatorEntryId = PropertyTagPropertyDefinition.InternalCreate("CreatorEntryId", (PropTag)1073283330U);

		// Token: 0x04004724 RID: 18212
		public static readonly PropertyTagPropertyDefinition ParentEntryId = PropertyTagPropertyDefinition.InternalCreate("ParentEntryId", PropTag.ParentEntryId);

		// Token: 0x04004725 RID: 18213
		public static readonly PropertyTagPropertyDefinition SearchKey = PropertyTagPropertyDefinition.InternalCreate("SearchKey", PropTag.SearchKey);

		// Token: 0x04004726 RID: 18214
		public static readonly PropertyTagPropertyDefinition RecordKey = PropertyTagPropertyDefinition.InternalCreate("RecordKey", PropTag.RecordKey);

		// Token: 0x04004727 RID: 18215
		public static readonly PropertyTagPropertyDefinition UserConfigurationDictionary = PropertyTagPropertyDefinition.InternalCreate("UserConfigurationDictionary", (PropTag)2080833794U, PropertyFlags.Streamable);

		// Token: 0x04004728 RID: 18216
		public static readonly PropertyTagPropertyDefinition UserConfigurationStream = PropertyTagPropertyDefinition.InternalCreate("UserConfigurationStream", (PropTag)2080964866U, PropertyFlags.Streamable);

		// Token: 0x04004729 RID: 18217
		public static readonly PropertyTagPropertyDefinition OwnerLogonUserConfigurationCache = PropertyTagPropertyDefinition.InternalCreate("OwnerLogonUserConfigurationCacheEntryId", PropTag.OwnerLogonUserConfigurationCache);

		// Token: 0x0400472A RID: 18218
		public static readonly PropertyTagPropertyDefinition UserConfigurationType = PropertyTagPropertyDefinition.InternalCreate("UserConfigurationDataType", (PropTag)2080768003U);

		// Token: 0x0400472B RID: 18219
		public static readonly PropertyTagPropertyDefinition UserConfigurationXml = PropertyTagPropertyDefinition.InternalCreate("UserConfigurationXmlStream", (PropTag)2080899330U, PropertyFlags.Streamable);

		// Token: 0x0400472C RID: 18220
		public static readonly PropertyTagPropertyDefinition AdminFolderFlags = PropertyTagPropertyDefinition.InternalCreate("AdminFolderFlags", PropTag.TimeInServer);

		// Token: 0x0400472D RID: 18221
		public static readonly PropertyTagPropertyDefinition DeletedCountTotal = PropertyTagPropertyDefinition.InternalCreate("DeletedCount", PropTag.DeletedCountTotal, PropertyFlags.ReadOnly);

		// Token: 0x0400472E RID: 18222
		public static readonly PropertyTagPropertyDefinition DeletedMsgCount = PropertyTagPropertyDefinition.InternalCreate("DeletedCount", PropTag.DeletedMsgCount, PropertyFlags.ReadOnly);

		// Token: 0x0400472F RID: 18223
		public static readonly PropertyTagPropertyDefinition DeletedAssocMsgCount = PropertyTagPropertyDefinition.InternalCreate("DeletedCount", PropTag.DeletedAssocMsgCount, PropertyFlags.ReadOnly);

		// Token: 0x04004730 RID: 18224
		public static readonly PropertyTagPropertyDefinition LocalCommitTimeMax = PropertyTagPropertyDefinition.InternalCreate("LocalCommitTimeMax", PropTag.LocalCommitTimeMax, PropertyFlags.ReadOnly);

		// Token: 0x04004731 RID: 18225
		public static readonly GuidIdPropertyDefinition NetMeetingOrganizerAlias = InternalSchema.CreateGuidIdProperty("NetMeetingOrganizerAlias", typeof(string), WellKnownPropertySet.Appointment, 33347);

		// Token: 0x04004732 RID: 18226
		public static readonly GuidIdPropertyDefinition NetMeetingDocPathName = InternalSchema.CreateGuidIdProperty("NetMeetingDocPathName", typeof(string), WellKnownPropertySet.Meeting, 33345);

		// Token: 0x04004733 RID: 18227
		public static readonly GuidIdPropertyDefinition NetMeetingConferenceServerAllowExternal = InternalSchema.CreateGuidIdProperty("NetMeetingConferenceServerAllowExternal", typeof(bool), WellKnownPropertySet.Meeting, 33350);

		// Token: 0x04004734 RID: 18228
		public static readonly GuidIdPropertyDefinition NetMeetingConferenceSerPassword = InternalSchema.CreateGuidIdProperty("NetMeetingConferenceSerPassword", typeof(string), WellKnownPropertySet.Meeting, 33353);

		// Token: 0x04004735 RID: 18229
		public static readonly GuidIdPropertyDefinition NetMeetingServer = InternalSchema.CreateGuidIdProperty("NetMeetingServer", typeof(string), WellKnownPropertySet.Appointment, 33346);

		// Token: 0x04004736 RID: 18230
		public static readonly PropertyTagPropertyDefinition DisplayNamePrefix = PropertyTagPropertyDefinition.InternalCreate("DisplayNamePrefix", PropTag.DisplayNamePrefix);

		// Token: 0x04004737 RID: 18231
		public static readonly PropertyTagPropertyDefinition InstanceKey = PropertyTagPropertyDefinition.InternalCreate("InstanceKey", PropTag.InstanceKey);

		// Token: 0x04004738 RID: 18232
		public static readonly PropertyTagPropertyDefinition AttachmentContent = PropertyTagPropertyDefinition.InternalCreate("AttachmentContent", PropTag.SearchAttachments);

		// Token: 0x04004739 RID: 18233
		public static readonly GuidNamePropertyDefinition MessageAudioNotes = InternalSchema.CreateGuidNameProperty("UMAudioNotes", typeof(string), WellKnownPropertySet.UnifiedMessaging, "UMAudioNotes");

		// Token: 0x0400473A RID: 18234
		public static readonly GuidNamePropertyDefinition MessageAudioNotesIncorrectType = InternalSchema.CreateGuidNameProperty("UMAudioNotesIncorrectType", typeof(string[]), WellKnownPropertySet.UnifiedMessaging, "UMAudioNotes");

		// Token: 0x0400473B RID: 18235
		public static readonly PropertyTagPropertyDefinition PersonalHomePage = PropertyTagPropertyDefinition.InternalCreate("PersonalHomePage", PropTag.PersonalHomePage);

		// Token: 0x0400473C RID: 18236
		public static readonly GuidIdPropertyDefinition LegacyWebPage = InternalSchema.CreateGuidIdProperty("LegacyWebPage", typeof(string), WellKnownPropertySet.Address, 32811);

		// Token: 0x0400473D RID: 18237
		public static readonly GuidIdPropertyDefinition OutlookCardDesign = InternalSchema.CreateGuidIdProperty("OutlookCardDesign", typeof(byte[]), WellKnownPropertySet.Address, 32832);

		// Token: 0x0400473E RID: 18238
		public static readonly GuidIdPropertyDefinition UserText1 = InternalSchema.CreateGuidIdProperty("UserText1", typeof(string), WellKnownPropertySet.Address, 32847);

		// Token: 0x0400473F RID: 18239
		public static readonly GuidIdPropertyDefinition UserText2 = InternalSchema.CreateGuidIdProperty("UserText2", typeof(string), WellKnownPropertySet.Address, 32848);

		// Token: 0x04004740 RID: 18240
		public static readonly GuidIdPropertyDefinition UserText3 = InternalSchema.CreateGuidIdProperty("UserText3", typeof(string), WellKnownPropertySet.Address, 32849);

		// Token: 0x04004741 RID: 18241
		public static readonly GuidIdPropertyDefinition UserText4 = InternalSchema.CreateGuidIdProperty("UserText4", typeof(string), WellKnownPropertySet.Address, 32850);

		// Token: 0x04004742 RID: 18242
		public static readonly GuidIdPropertyDefinition FreeBusyUrl = InternalSchema.CreateGuidIdProperty("FreeBusyUrl", typeof(string), WellKnownPropertySet.Address, 32984);

		// Token: 0x04004743 RID: 18243
		public static readonly PropertyTagPropertyDefinition Hobbies = PropertyTagPropertyDefinition.InternalCreate("Hobbies", PropTag.Hobbies);

		// Token: 0x04004744 RID: 18244
		public static readonly GuidIdPropertyDefinition YomiFirstName = InternalSchema.CreateGuidIdProperty("YomiFirstName", typeof(string), WellKnownPropertySet.Address, 32812);

		// Token: 0x04004745 RID: 18245
		public static readonly GuidIdPropertyDefinition YomiLastName = InternalSchema.CreateGuidIdProperty("YomiLastName", typeof(string), WellKnownPropertySet.Address, 32813);

		// Token: 0x04004746 RID: 18246
		public static readonly GuidIdPropertyDefinition YomiCompany = InternalSchema.CreateGuidIdProperty("YomiCompany", typeof(string), WellKnownPropertySet.Address, 32814);

		// Token: 0x04004747 RID: 18247
		public static readonly GuidIdPropertyDefinition HomeAddressInternal = InternalSchema.CreateGuidIdProperty("HomeAddressInternal", typeof(string), WellKnownPropertySet.Address, 32794);

		// Token: 0x04004748 RID: 18248
		public static readonly GuidIdPropertyDefinition BusinessAddressInternal = InternalSchema.CreateGuidIdProperty("BusinessAddressInternal", typeof(string), WellKnownPropertySet.Address, 32795);

		// Token: 0x04004749 RID: 18249
		public static readonly GuidIdPropertyDefinition OtherAddressInternal = InternalSchema.CreateGuidIdProperty("OtherAddressInternal", typeof(string), WellKnownPropertySet.Address, 32796);

		// Token: 0x0400474A RID: 18250
		public static readonly PropertyTagPropertyDefinition TtyTddPhoneNumber = PropertyTagPropertyDefinition.InternalCreate("TtyTddPhoneNumber", PropTag.TtytddPhoneNumber);

		// Token: 0x0400474B RID: 18251
		public static readonly PropertyTagPropertyDefinition PrimaryTelephoneNumber = PropertyTagPropertyDefinition.InternalCreate("PrimaryTelephoneNumber", PropTag.PrimaryTelephoneNumber);

		// Token: 0x0400474C RID: 18252
		public static readonly PropertyTagPropertyDefinition TelexNumber = PropertyTagPropertyDefinition.InternalCreate("TelexNumber", PropTag.TelexNumber);

		// Token: 0x0400474D RID: 18253
		public static readonly PropertyTagPropertyDefinition ItemColor = PropertyTagPropertyDefinition.InternalCreate("ItemColor", (PropTag)278200323U);

		// Token: 0x0400474E RID: 18254
		public static readonly PropertyTagPropertyDefinition FlagCompleteTime = PropertyTagPropertyDefinition.InternalCreate("FlagCompleteTime", (PropTag)277938240U);

		// Token: 0x0400474F RID: 18255
		public static readonly PropertyTagPropertyDefinition AttachDataBin = PropertyTagPropertyDefinition.InternalCreate("AttachDataBin", PropTag.AttachDataBin, PropertyFlags.Streamable);

		// Token: 0x04004750 RID: 18256
		public static readonly PropertyTagPropertyDefinition AttachDataObj = PropertyTagPropertyDefinition.InternalCreate("AttachDataObj", PropTag.AttachDataObj, PropertyFlags.Streamable);

		// Token: 0x04004751 RID: 18257
		public static readonly GuidNamePropertyDefinition AttachHash = InternalSchema.CreateGuidNameProperty("AttachHash", typeof(byte[]), WellKnownPropertySet.Attachment, "AttachHash");

		// Token: 0x04004752 RID: 18258
		public static readonly GuidNamePropertyDefinition ImageThumbnail = InternalSchema.CreateGuidNameProperty("ImageThumbnail", typeof(byte[]), WellKnownPropertySet.Attachment, "ImageThumbnail", PropertyFlags.Streamable, new PropertyDefinitionConstraint[0]);

		// Token: 0x04004753 RID: 18259
		public static readonly GuidNamePropertyDefinition ImageThumbnailSalientRegions = InternalSchema.CreateGuidNameProperty("ImageThumbnailSalientRegions", typeof(byte[]), WellKnownPropertySet.Attachment, "ImageThumbnailSalientRegions");

		// Token: 0x04004754 RID: 18260
		public static readonly GuidNamePropertyDefinition ImageThumbnailHeight = InternalSchema.CreateGuidNameProperty("ImageThumbnailHeight", typeof(int), WellKnownPropertySet.Attachment, "ImageThumbnailHeight");

		// Token: 0x04004755 RID: 18261
		public static readonly GuidNamePropertyDefinition ImageThumbnailWidth = InternalSchema.CreateGuidNameProperty("ImageThumbnailWidth", typeof(int), WellKnownPropertySet.Attachment, "ImageThumbnailWidth");

		// Token: 0x04004756 RID: 18262
		public static readonly PropertyTagPropertyDefinition UserPhotoCacheId = PropertyTagPropertyDefinition.InternalCreate("UserPhotoCacheId", PropTag.UserPhotoCacheId);

		// Token: 0x04004757 RID: 18263
		public static readonly PropertyTagPropertyDefinition UserPhotoPreviewCacheId = PropertyTagPropertyDefinition.InternalCreate("UserPhotoPreviewCacheId", PropTag.UserPhotoPreviewCacheId);

		// Token: 0x04004758 RID: 18264
		public static readonly PropertyTagPropertyDefinition InferenceTrainedModelVersionBreadCrumb = PropertyTagPropertyDefinition.InternalCreate("InferenceTrainedModelVersionBreadCrumb", PropTag.InferenceTrainedModelVersionBreadCrumb);

		// Token: 0x04004759 RID: 18265
		public static readonly GuidNamePropertyDefinition UserPhotoHR648x648 = InternalSchema.CreateGuidNameProperty("UserPhotoHR648x648", typeof(byte[]), WellKnownPropertySet.Common, "UserPhotoHR648x648", PropertyFlags.Streamable, new PropertyDefinitionConstraint[0]);

		// Token: 0x0400475A RID: 18266
		public static readonly GuidNamePropertyDefinition UserPhotoHR504x504 = InternalSchema.CreateGuidNameProperty("UserPhotoHR504x504", typeof(byte[]), WellKnownPropertySet.Common, "UserPhotoHR504x504", PropertyFlags.Streamable, new PropertyDefinitionConstraint[0]);

		// Token: 0x0400475B RID: 18267
		public static readonly GuidNamePropertyDefinition UserPhotoHR432x432 = InternalSchema.CreateGuidNameProperty("UserPhotoHR432x432", typeof(byte[]), WellKnownPropertySet.Common, "UserPhotoHR432x432", PropertyFlags.Streamable, new PropertyDefinitionConstraint[0]);

		// Token: 0x0400475C RID: 18268
		public static readonly GuidNamePropertyDefinition UserPhotoHR360x360 = InternalSchema.CreateGuidNameProperty("UserPhotoHR360x360", typeof(byte[]), WellKnownPropertySet.Common, "UserPhotoHR360x360", PropertyFlags.Streamable, new PropertyDefinitionConstraint[0]);

		// Token: 0x0400475D RID: 18269
		public static readonly GuidNamePropertyDefinition UserPhotoHR240x240 = InternalSchema.CreateGuidNameProperty("UserPhotoHR240x240", typeof(byte[]), WellKnownPropertySet.Common, "UserPhotoHR240x240", PropertyFlags.Streamable, new PropertyDefinitionConstraint[0]);

		// Token: 0x0400475E RID: 18270
		public static readonly GuidNamePropertyDefinition UserPhotoHR120x120 = InternalSchema.CreateGuidNameProperty("UserPhotoHR120x120", typeof(byte[]), WellKnownPropertySet.Common, "UserPhotoHR120x120", PropertyFlags.Streamable, new PropertyDefinitionConstraint[0]);

		// Token: 0x0400475F RID: 18271
		public static readonly GuidNamePropertyDefinition UserPhotoHR96x96 = InternalSchema.CreateGuidNameProperty("UserPhotoHR96x96", typeof(byte[]), WellKnownPropertySet.Common, "UserPhotoHR96x96", PropertyFlags.Streamable, new PropertyDefinitionConstraint[0]);

		// Token: 0x04004760 RID: 18272
		public static readonly GuidNamePropertyDefinition UserPhotoHR64x64 = InternalSchema.CreateGuidNameProperty("UserPhotoHR64x64", typeof(byte[]), WellKnownPropertySet.Common, "UserPhotoHR64x64", PropertyFlags.Streamable, new PropertyDefinitionConstraint[0]);

		// Token: 0x04004761 RID: 18273
		public static readonly GuidNamePropertyDefinition UserPhotoHR48x48 = InternalSchema.CreateGuidNameProperty("UserPhotoHR48x48", typeof(byte[]), WellKnownPropertySet.Common, "UserPhotoHR48x48", PropertyFlags.Streamable, new PropertyDefinitionConstraint[0]);

		// Token: 0x04004762 RID: 18274
		public static readonly PropertyTagPropertyDefinition FolderHierarchyDepth = PropertyTagPropertyDefinition.InternalCreate("FolderHierarchyDepth", PropTag.Depth, PropertyFlags.ReadOnly);

		// Token: 0x04004763 RID: 18275
		public static readonly PropertyTagPropertyDefinition ProhibitReceiveQuota = PropertyTagPropertyDefinition.InternalCreate("ProhibitReceiveQuota", PropTag.ProhibitReceiveQuota);

		// Token: 0x04004764 RID: 18276
		public static readonly PropertyTagPropertyDefinition MaxSubmitMessageSize = PropertyTagPropertyDefinition.InternalCreate("MaxSubmitMessageSize", PropTag.MaxSubmitMessageSize);

		// Token: 0x04004765 RID: 18277
		public static readonly PropertyTagPropertyDefinition MaxMessageSize = PropertyTagPropertyDefinition.InternalCreate("MaxMessageSize", PropTag.MaxMessageSize);

		// Token: 0x04004766 RID: 18278
		public static readonly PropertyTagPropertyDefinition ProhibitSendQuota = PropertyTagPropertyDefinition.InternalCreate("ProhibitSendQuota", PropTag.ProhibitSendQuota);

		// Token: 0x04004767 RID: 18279
		public static readonly PropertyTagPropertyDefinition SubmittedByAdmin = PropertyTagPropertyDefinition.InternalCreate("SubmittedByAdmin", PropTag.SubmittedByAdmin);

		// Token: 0x04004768 RID: 18280
		public static readonly PropertyTagPropertyDefinition OofReplyType = PropertyTagPropertyDefinition.InternalCreate("OofReplyType", PropTag.OofReplyType);

		// Token: 0x04004769 RID: 18281
		public static readonly PropertyTagPropertyDefinition MemberId = PropertyTagPropertyDefinition.InternalCreate("MemberId", PropTag.MemberId);

		// Token: 0x0400476A RID: 18282
		public static readonly PropertyTagPropertyDefinition MemberEntryId = InternalSchema.EntryId;

		// Token: 0x0400476B RID: 18283
		public static readonly PropertyTagPropertyDefinition MemberName = PropertyTagPropertyDefinition.InternalCreate("MemberName", PropTag.MemberName);

		// Token: 0x0400476C RID: 18284
		[Obsolete("Use InternalSchema.MemberName instead.")]
		public static readonly PropertyTagPropertyDefinition MemberNameLocalDirectory = InternalSchema.MemberName;

		// Token: 0x0400476D RID: 18285
		public static readonly PropertyTagPropertyDefinition ShortTermEntryIdFromObject = PropertyTagPropertyDefinition.InternalCreate("ShortTermEntryIdFromObject", (PropTag)1718747394U);

		// Token: 0x0400476E RID: 18286
		public static readonly PropertyTagPropertyDefinition MemberRights = PropertyTagPropertyDefinition.InternalCreate("MemberRights", PropTag.MemberRights);

		// Token: 0x0400476F RID: 18287
		public static readonly PropertyTagPropertyDefinition MemberSecurityIdentifier = PropertyTagPropertyDefinition.InternalCreate("MemberSecurityIdentifier", PropTag.MemberSecurityIdentifier);

		// Token: 0x04004770 RID: 18288
		public static readonly PropertyTagPropertyDefinition MemberIsGroup = PropertyTagPropertyDefinition.InternalCreate("MemberIsGroup", PropTag.MemberIsGroup);

		// Token: 0x04004771 RID: 18289
		public static readonly PropertyTagPropertyDefinition EffectiveRights = PropertyTagPropertyDefinition.InternalCreate("EffectiveRights", PropTag.Access, PropertyFlags.ReadOnly);

		// Token: 0x04004772 RID: 18290
		public static readonly PropertyTagPropertyDefinition ConversationKey = PropertyTagPropertyDefinition.InternalCreate("ConversationKey", PropTag.ConversationKey);

		// Token: 0x04004773 RID: 18291
		public static readonly PropertyTagPropertyDefinition AttachRendering = PropertyTagPropertyDefinition.InternalCreate("AttachRendering", PropTag.AttachRendering);

		// Token: 0x04004774 RID: 18292
		public static readonly PropertyTagPropertyDefinition OrigMessageClass = PropertyTagPropertyDefinition.InternalCreate("OrigMessageClass", PropTag.OrigMessageClass);

		// Token: 0x04004775 RID: 18293
		public static readonly PropertyTagPropertyDefinition TransmitableDisplayName = PropertyTagPropertyDefinition.InternalCreate("TransmitableDisplayName", PropTag.TransmitableDisplayName);

		// Token: 0x04004776 RID: 18294
		public static readonly PropertyTagPropertyDefinition DisplayName7Bit = PropertyTagPropertyDefinition.InternalCreate("DisplayName7Bit", PropTag.DisplayNamePrintable);

		// Token: 0x04004777 RID: 18295
		public static readonly PropertyTagPropertyDefinition DisplayType = PropertyTagPropertyDefinition.InternalCreate("DisplayType", PropTag.DisplayType);

		// Token: 0x04004778 RID: 18296
		public static readonly PropertyTagPropertyDefinition AutoResponseSuppressInternal = PropertyTagPropertyDefinition.InternalCreate("AutoResponseSuppressInternal", PropTag.AutoResponseSuppress);

		// Token: 0x04004779 RID: 18297
		public static readonly PropertyTagPropertyDefinition MessageLocaleId = PropertyTagPropertyDefinition.InternalCreate("MessageLocaleId", PropTag.MessageLocaleId);

		// Token: 0x0400477A RID: 18298
		public static readonly PropertyTagPropertyDefinition LocaleId = PropertyTagPropertyDefinition.InternalCreate("LocaleId", PropTag.LocaleId);

		// Token: 0x0400477B RID: 18299
		public static readonly PropertyTagPropertyDefinition InternetMdns = PropertyTagPropertyDefinition.InternalCreate("InternetMdns", PropTag.InternetMdns);

		// Token: 0x0400477C RID: 18300
		public static readonly PropertyTagPropertyDefinition NonReceiptReason = PropertyTagPropertyDefinition.InternalCreate("NonReceiptReason", PropTag.NonReceiptReason);

		// Token: 0x0400477D RID: 18301
		public static readonly PropertyTagPropertyDefinition DiscardReason = PropertyTagPropertyDefinition.InternalCreate("DiscardReason", PropTag.DiscardReason);

		// Token: 0x0400477E RID: 18302
		public static readonly PropertyTagPropertyDefinition OriginallyIntendedRecipientName = PropertyTagPropertyDefinition.InternalCreate("OriginallyIntendedRecipientName", PropTag.OriginallyIntendedRecipientName);

		// Token: 0x0400477F RID: 18303
		public static readonly PropertyTagPropertyDefinition OriginallyIntendedRecipEntryId = PropertyTagPropertyDefinition.InternalCreate("OriginallyIntendedRecipEntryId", PropTag.OriginallyIntendedRecipEntryId);

		// Token: 0x04004780 RID: 18304
		public static readonly PropertyTagPropertyDefinition OriginalSearchKey = PropertyTagPropertyDefinition.InternalCreate("OriginalSearchKey", PropTag.OriginalSearchKey);

		// Token: 0x04004781 RID: 18305
		public static readonly PropertyTagPropertyDefinition ContentIdentifier = PropertyTagPropertyDefinition.InternalCreate("ContentIdentifier", PropTag.ContentIdentifier);

		// Token: 0x04004782 RID: 18306
		public static readonly PropertyTagPropertyDefinition DeliveryTime = PropertyTagPropertyDefinition.InternalCreate("DeliverTime", PropTag.DeliverTime);

		// Token: 0x04004783 RID: 18307
		public static readonly PropertyTagPropertyDefinition ReportText = PropertyTagPropertyDefinition.InternalCreate("ReportText", PropTag.ReportText);

		// Token: 0x04004784 RID: 18308
		public static readonly PropertyTagPropertyDefinition ReportTime = PropertyTagPropertyDefinition.InternalCreate("ReportTime", PropTag.ReportTime);

		// Token: 0x04004785 RID: 18309
		public static readonly PropertyTagPropertyDefinition NdrDiagnosticCode = PropertyTagPropertyDefinition.InternalCreate("NdrDiagnosticCode", PropTag.NdrDiagCode);

		// Token: 0x04004786 RID: 18310
		public static readonly PropertyTagPropertyDefinition NdrReasonCode = PropertyTagPropertyDefinition.InternalCreate("NdrReasonCode", PropTag.NdrReasonCode);

		// Token: 0x04004787 RID: 18311
		public static readonly PropertyTagPropertyDefinition NdrStatusCode = PropertyTagPropertyDefinition.InternalCreate("NdrStatusCode", (PropTag)203423747U);

		// Token: 0x04004788 RID: 18312
		public static readonly PropertyTagPropertyDefinition OriginalDisplayTo = PropertyTagPropertyDefinition.InternalCreate("OriginalDisplayTo", PropTag.OriginalDisplayTo, PropertyFlags.Streamable);

		// Token: 0x04004789 RID: 18313
		public static readonly PropertyTagPropertyDefinition OriginalDisplayCc = PropertyTagPropertyDefinition.InternalCreate("OriginalDisplayCc", PropTag.OriginalDisplayCc, PropertyFlags.Streamable);

		// Token: 0x0400478A RID: 18314
		public static readonly PropertyTagPropertyDefinition OriginalDisplayBcc = PropertyTagPropertyDefinition.InternalCreate("OriginalDisplayBcc", PropTag.OriginalDisplayBcc, PropertyFlags.Streamable);

		// Token: 0x0400478B RID: 18315
		public static readonly PropertyTagPropertyDefinition OriginalDeliveryTime = PropertyTagPropertyDefinition.InternalCreate("OriginalDeliveryTime", PropTag.OriginalDeliveryTime);

		// Token: 0x0400478C RID: 18316
		public static readonly PropertyTagPropertyDefinition OriginalAuthorAddressType = PropertyTagPropertyDefinition.InternalCreate("OriginalAuthorAddressType", PropTag.OriginalAuthorAddrType, new PropertyDefinitionConstraint[]
		{
			new NonMoveMailboxPropertyConstraint(new StringLengthConstraint(0, 9))
		});

		// Token: 0x0400478D RID: 18317
		public static readonly PropertyTagPropertyDefinition OriginalAuthorEmailAddress = PropertyTagPropertyDefinition.InternalCreate("OriginalAuthorEmailAddress", PropTag.OriginalAuthorEmailAddress);

		// Token: 0x0400478E RID: 18318
		public static readonly PropertyTagPropertyDefinition OriginalAuthorEntryId = PropertyTagPropertyDefinition.InternalCreate("OriginalAuthorEntryId", PropTag.OriginalAuthorEntryId);

		// Token: 0x0400478F RID: 18319
		public static readonly PropertyTagPropertyDefinition OriginalAuthorSearchKey = PropertyTagPropertyDefinition.InternalCreate("OriginalAuthorSearchKey", PropTag.OriginalAuthorSearchKey);

		// Token: 0x04004790 RID: 18320
		public static readonly PropertyTagPropertyDefinition OriginalSenderAddressType = PropertyTagPropertyDefinition.InternalCreate("OriginalSenderAddressType", PropTag.OriginalSenderAddrType, new PropertyDefinitionConstraint[]
		{
			new NonMoveMailboxPropertyConstraint(new StringLengthConstraint(0, 9))
		});

		// Token: 0x04004791 RID: 18321
		public static readonly PropertyTagPropertyDefinition OriginalSenderDisplayName = PropertyTagPropertyDefinition.InternalCreate("OriginalSenderDisplayName", PropTag.OriginalSenderName);

		// Token: 0x04004792 RID: 18322
		public static readonly PropertyTagPropertyDefinition OriginalSenderEmailAddress = PropertyTagPropertyDefinition.InternalCreate("OriginalSenderEmailAddress", PropTag.OriginalSenderEmailAddress);

		// Token: 0x04004793 RID: 18323
		public static readonly PropertyTagPropertyDefinition OriginalSenderEntryId = PropertyTagPropertyDefinition.InternalCreate("OriginalSenderEntryId", PropTag.OriginalSenderEntryId);

		// Token: 0x04004794 RID: 18324
		public static readonly PropertyTagPropertyDefinition OriginalSenderSearchKey = PropertyTagPropertyDefinition.InternalCreate("OriginalSenderSearchKey", PropTag.OriginalSenderSearchKey);

		// Token: 0x04004795 RID: 18325
		public static readonly PropertyTagPropertyDefinition OriginalSentRepresentingAddressType = PropertyTagPropertyDefinition.InternalCreate("OriginalSentRepresentingAddressType", PropTag.OriginalSentRepresentingAddrType, new PropertyDefinitionConstraint[]
		{
			new NonMoveMailboxPropertyConstraint(new StringLengthConstraint(0, 9))
		});

		// Token: 0x04004796 RID: 18326
		public static readonly PropertyTagPropertyDefinition OriginalSentRepresentingDisplayName = PropertyTagPropertyDefinition.InternalCreate("OriginalSentRepresentingDisplayName", PropTag.OriginalSentRepresentingName);

		// Token: 0x04004797 RID: 18327
		public static readonly PropertyTagPropertyDefinition OriginalSentRepresentingEmailAddress = PropertyTagPropertyDefinition.InternalCreate("OriginalSentRepresentingEmailAddress", PropTag.OriginalSentRepresentingEmailAddress);

		// Token: 0x04004798 RID: 18328
		public static readonly PropertyTagPropertyDefinition OriginalSentRepresentingEntryId = PropertyTagPropertyDefinition.InternalCreate("OriginalSentRepresentingEntryId", PropTag.OriginalSentRepresentingEntryId);

		// Token: 0x04004799 RID: 18329
		public static readonly PropertyTagPropertyDefinition OriginalSentRepresentingSearchKey = PropertyTagPropertyDefinition.InternalCreate("OriginalSentRepresentingSearchKey", PropTag.OriginalSentRepresentingSearchKey);

		// Token: 0x0400479A RID: 18330
		public static readonly PropertyTagPropertyDefinition OriginalSubject = PropertyTagPropertyDefinition.InternalCreate("OriginalSubject", PropTag.OriginalSubject);

		// Token: 0x0400479B RID: 18331
		public static readonly PropertyTagPropertyDefinition OriginalSentTime = PropertyTagPropertyDefinition.InternalCreate("OriginalSentTime", PropTag.OriginalSubmitTime);

		// Token: 0x0400479C RID: 18332
		public static readonly PropertyTagPropertyDefinition ReceiptTime = PropertyTagPropertyDefinition.InternalCreate("ReceiptTime", PropTag.ReceiptTime);

		// Token: 0x0400479D RID: 18333
		public static readonly PropertyTagPropertyDefinition SupplementaryInfo = PropertyTagPropertyDefinition.InternalCreate("SupplementaryInfo", PropTag.SupplementaryInfo);

		// Token: 0x0400479E RID: 18334
		public static readonly PropertyTagPropertyDefinition ReceivedByAddrType = PropertyTagPropertyDefinition.InternalCreate("ReceivedByAddrType", PropTag.ReceivedByAddrType, new PropertyDefinitionConstraint[]
		{
			new NonMoveMailboxPropertyConstraint(new StringLengthConstraint(0, 9))
		});

		// Token: 0x0400479F RID: 18335
		public static readonly PropertyTagPropertyDefinition ReceivedByEmailAddress = PropertyTagPropertyDefinition.InternalCreate("ReceivedByEmailAddress", PropTag.ReceivedByEmailAddress);

		// Token: 0x040047A0 RID: 18336
		public static readonly PropertyTagPropertyDefinition ReceivedByEntryId = PropertyTagPropertyDefinition.InternalCreate("ReceivedByEntryId", PropTag.ReceivedByEntryId);

		// Token: 0x040047A1 RID: 18337
		public static readonly PropertyTagPropertyDefinition ReceivedByName = PropertyTagPropertyDefinition.InternalCreate("ReceivedByName", PropTag.ReceivedByName);

		// Token: 0x040047A2 RID: 18338
		public static readonly PropertyTagPropertyDefinition ReceivedBySearchKey = PropertyTagPropertyDefinition.InternalCreate("ReceivedBySearchKey", PropTag.ReceivedBySearchKey);

		// Token: 0x040047A3 RID: 18339
		public static readonly PropertyTagPropertyDefinition ElcAutoCopyLabel = PropertyTagPropertyDefinition.InternalCreate("ElcAutoCopyLabel", PropTag.ElcAutoCopyLabel);

		// Token: 0x040047A4 RID: 18340
		public static readonly PropertyTagPropertyDefinition ElcAutoCopyTag = PropertyTagPropertyDefinition.InternalCreate("ElcAutoCopyTag", PropTag.ElcAutoCopyTag);

		// Token: 0x040047A5 RID: 18341
		public static readonly PropertyTagPropertyDefinition ElcMoveDate = PropertyTagPropertyDefinition.InternalCreate("ElcMoveDate", PropTag.ElcMoveDate);

		// Token: 0x040047A6 RID: 18342
		public static readonly PropertyTagPropertyDefinition ControlDataForCalendarRepairAssistant = PropertyTagPropertyDefinition.InternalCreate("ControlDataForCalendarRepairAssistant", PropTag.ControlDataForCalendarRepairAssistant);

		// Token: 0x040047A7 RID: 18343
		public static readonly PropertyTagPropertyDefinition ControlDataForSharingPolicyAssistant = PropertyTagPropertyDefinition.InternalCreate("ControlDataForSharingPolicyAssistant", PropTag.ControlDataForSharingPolicyAssistant);

		// Token: 0x040047A8 RID: 18344
		public static readonly PropertyTagPropertyDefinition ControlDataForElcAssistant = PropertyTagPropertyDefinition.InternalCreate("ControlDataForElcAssistant", PropTag.ControlDataForElcAssistant);

		// Token: 0x040047A9 RID: 18345
		public static readonly PropertyTagPropertyDefinition ElcLastRunTotalProcessingTime = PropertyTagPropertyDefinition.InternalCreate("ElcLastRunTotalProcessingTime", PropTag.ElcLastRunTotalProcessingTime);

		// Token: 0x040047AA RID: 18346
		public static readonly PropertyTagPropertyDefinition ElcLastRunSubAssistantProcessingTime = PropertyTagPropertyDefinition.InternalCreate("ElcLastRunSubAssistantProcessingTime", PropTag.ElcLastRunSubAssistantProcessingTime);

		// Token: 0x040047AB RID: 18347
		public static readonly PropertyTagPropertyDefinition ElcLastRunUpdatedFolderCount = PropertyTagPropertyDefinition.InternalCreate("ElcLastRunUpdatedFolderCount", PropTag.ElcLastRunUpdatedFolderCount);

		// Token: 0x040047AC RID: 18348
		public static readonly PropertyTagPropertyDefinition ElcLastRunTaggedFolderCount = PropertyTagPropertyDefinition.InternalCreate("ElcLastRunTaggedFolderCount", PropTag.ElcLastRunTaggedFolderCount);

		// Token: 0x040047AD RID: 18349
		public static readonly PropertyTagPropertyDefinition ElcLastRunUpdatedItemCount = PropertyTagPropertyDefinition.InternalCreate("ElcLastRunUpdatedItemCount", PropTag.ElcLastRunUpdatedItemCount);

		// Token: 0x040047AE RID: 18350
		public static readonly PropertyTagPropertyDefinition ElcLastRunTaggedWithArchiveItemCount = PropertyTagPropertyDefinition.InternalCreate("ElcLastRunTaggedWithArchiveItemCount", PropTag.ElcLastRunTaggedWithArchiveItemCount);

		// Token: 0x040047AF RID: 18351
		public static readonly PropertyTagPropertyDefinition ElcLastRunTaggedWithExpiryItemCount = PropertyTagPropertyDefinition.InternalCreate("ElcLastRunTaggedWithExpiryItemCount", PropTag.ElcLastRunTaggedWithExpiryItemCount);

		// Token: 0x040047B0 RID: 18352
		public static readonly PropertyTagPropertyDefinition ElcLastRunDeletedFromRootItemCount = PropertyTagPropertyDefinition.InternalCreate("ElcLastRunDeletedFromRootItemCount", PropTag.ElcLastRunDeletedFromRootItemCount);

		// Token: 0x040047B1 RID: 18353
		public static readonly PropertyTagPropertyDefinition ElcLastRunDeletedFromDumpsterItemCount = PropertyTagPropertyDefinition.InternalCreate("ElcLastRunDeletedFromDumpsterItemCount", PropTag.ElcLastRunDeletedFromDumpsterItemCount);

		// Token: 0x040047B2 RID: 18354
		public static readonly PropertyTagPropertyDefinition ElcLastRunArchivedFromRootItemCount = PropertyTagPropertyDefinition.InternalCreate("ElcLastRunArchivedFromRootItemCount", PropTag.ElcLastRunArchivedFromRootItemCount);

		// Token: 0x040047B3 RID: 18355
		public static readonly PropertyTagPropertyDefinition ElcLastRunArchivedFromDumpsterItemCount = PropertyTagPropertyDefinition.InternalCreate("ElcLastRunArchivedFromDumpsterItemCount", PropTag.ElcLastRunArchivedFromDumpsterItemCount);

		// Token: 0x040047B4 RID: 18356
		public static readonly PropertyTagPropertyDefinition ELCLastSuccessTimestamp = PropertyTagPropertyDefinition.InternalCreate("ELCLastSuccessTimestamp", PropTag.ELCLastSuccessTimestamp);

		// Token: 0x040047B5 RID: 18357
		public static readonly PropertyTagPropertyDefinition ControlDataForTopNWordsAssistant = PropertyTagPropertyDefinition.InternalCreate("ControlDataForTopNWordsAssistant", PropTag.ControlDataForTopNWordsAssistant);

		// Token: 0x040047B6 RID: 18358
		public static readonly PropertyTagPropertyDefinition IsTopNEnabled = PropertyTagPropertyDefinition.InternalCreate("IsTopNEnabled", PropTag.IsTopNEnabled);

		// Token: 0x040047B7 RID: 18359
		public static readonly PropertyTagPropertyDefinition ControlDataForJunkEmailAssistant = PropertyTagPropertyDefinition.InternalCreate("ControlDataForJunkEmailAssistant", PropTag.ControlDataForJunkEmailAssistant);

		// Token: 0x040047B8 RID: 18360
		public static readonly PropertyTagPropertyDefinition ControlDataForCalendarSyncAssistant = PropertyTagPropertyDefinition.InternalCreate("ControlDataForCalendarSyncAssistant", PropTag.ControlDataForCalendarSyncAssistant);

		// Token: 0x040047B9 RID: 18361
		public static readonly PropertyTagPropertyDefinition ExternalSharingCalendarSubscriptionCount = PropertyTagPropertyDefinition.InternalCreate("ExternalSharingCalendarSubscriptionCount", PropTag.ExternalSharingCalendarSubscriptionCount);

		// Token: 0x040047BA RID: 18362
		public static readonly PropertyTagPropertyDefinition ControlDataForUMReportingAssistant = PropertyTagPropertyDefinition.InternalCreate("ControlDataForUMReportingAssistant", PropTag.ControlDataForUMReportingAssistant);

		// Token: 0x040047BB RID: 18363
		public static readonly PropertyTagPropertyDefinition HasUMReportData = PropertyTagPropertyDefinition.InternalCreate("HasUMReportData", PropTag.HasUMReportData);

		// Token: 0x040047BC RID: 18364
		public static readonly PropertyTagPropertyDefinition PredictedActionsSummaryDeprecated = PropertyTagPropertyDefinition.InternalCreate("PredictedActionsSummary", PropTag.PredictedActionsSummary, PropertyFlags.ReadOnly);

		// Token: 0x040047BD RID: 18365
		public static readonly PropertyTagPropertyDefinition GroupingActionsDeprecated = PropertyTagPropertyDefinition.InternalCreate("GroupingActions", PropTag.GroupingActions, PropertyFlags.ReadOnly);

		// Token: 0x040047BE RID: 18366
		public static readonly PropertyTagPropertyDefinition ControlDataForInferenceTrainingAssistant = PropertyTagPropertyDefinition.InternalCreate("ControlDataForInferenceTrainingAssistant", PropTag.ControlDataForInferenceTrainingAssistant);

		// Token: 0x040047BF RID: 18367
		public static readonly PropertyTagPropertyDefinition InferenceEnabled = PropertyTagPropertyDefinition.InternalCreate("InferenceEnabled", PropTag.InferenceEnabled);

		// Token: 0x040047C0 RID: 18368
		public static readonly PropertyTagPropertyDefinition InferenceClientActivityFlags = PropertyTagPropertyDefinition.InternalCreate("InferenceClientActivityFlags", PropTag.InferenceClientActivityFlags);

		// Token: 0x040047C1 RID: 18369
		public static readonly GuidNamePropertyDefinition IsVoiceReminderEnabled = InternalSchema.CreateGuidNameProperty("IsVoiceReminderEnabled", typeof(bool), WellKnownPropertySet.UnifiedMessaging, "IsVoiceReminderEnabled");

		// Token: 0x040047C2 RID: 18370
		public static readonly GuidNamePropertyDefinition VoiceReminderPhoneNumber = InternalSchema.CreateGuidNameProperty("VoiceReminderPhoneNumber", typeof(string), WellKnownPropertySet.UnifiedMessaging, "VoiceReminderPhoneNumber");

		// Token: 0x040047C3 RID: 18371
		public static readonly PropertyTagPropertyDefinition ControlDataForDirectoryProcessorAssistant = PropertyTagPropertyDefinition.InternalCreate("ControlDataForDirectoryProcessorAssistant", PropTag.ControlDataForDirectoryProcessorAssistant);

		// Token: 0x040047C4 RID: 18372
		public static readonly PropertyTagPropertyDefinition NeedsDirectoryProcessor = PropertyTagPropertyDefinition.InternalCreate("NeedsDirectoryProcessor", PropTag.NeedsDirectoryProcessor);

		// Token: 0x040047C5 RID: 18373
		public static readonly PropertyTagPropertyDefinition ControlDataForOABGeneratorAssistant = PropertyTagPropertyDefinition.InternalCreate("ControlDataForOABGeneratorAssistant", PropTag.ControlDataForOABGeneratorAssistant);

		// Token: 0x040047C6 RID: 18374
		public static readonly PropertyTagPropertyDefinition InternetCalendarSubscriptionCount = PropertyTagPropertyDefinition.InternalCreate("InternetCalendarSubscriptionCount", PropTag.InternetCalendarSubscriptionCount);

		// Token: 0x040047C7 RID: 18375
		public static readonly PropertyTagPropertyDefinition ExternalSharingContactSubscriptionCount = PropertyTagPropertyDefinition.InternalCreate("ExternalSharingContactSubscriptionCount", PropTag.ExternalSharingContactSubscriptionCount);

		// Token: 0x040047C8 RID: 18376
		public static readonly PropertyTagPropertyDefinition JunkEmailSafeListDirty = PropertyTagPropertyDefinition.InternalCreate("JunkEmailSafeListDirty", PropTag.JunkEmailSafeListDirty);

		// Token: 0x040047C9 RID: 18377
		public static readonly PropertyTagPropertyDefinition LastSharingPolicyAppliedId = PropertyTagPropertyDefinition.InternalCreate("LastSharingPolicyAppliedId", PropTag.LastSharingPolicyAppliedId);

		// Token: 0x040047CA RID: 18378
		public static readonly PropertyTagPropertyDefinition LastSharingPolicyAppliedHash = PropertyTagPropertyDefinition.InternalCreate("LastSharingPolicyAppliedHash", PropTag.LastSharingPolicyAppliedHash);

		// Token: 0x040047CB RID: 18379
		public static readonly PropertyTagPropertyDefinition LastSharingPolicyAppliedTime = PropertyTagPropertyDefinition.InternalCreate("LastSharingPolicyAppliedTime", PropTag.LastSharingPolicyAppliedTime);

		// Token: 0x040047CC RID: 18380
		public static readonly PropertyTagPropertyDefinition OofScheduleStart = PropertyTagPropertyDefinition.InternalCreate("OofScheduleStart", PropTag.OofScheduleStart);

		// Token: 0x040047CD RID: 18381
		public static readonly PropertyTagPropertyDefinition OofScheduleEnd = PropertyTagPropertyDefinition.InternalCreate("OofScheduleEnd", PropTag.OofScheduleEnd);

		// Token: 0x040047CE RID: 18382
		public static readonly PropertyTagPropertyDefinition RetentionQueryInfo = PropertyTagPropertyDefinition.InternalCreate("RetentionQueryInfo", PropTag.RetentionQueryInfo);

		// Token: 0x040047CF RID: 18383
		public static readonly PropertyTagPropertyDefinition ControlDataForPublicFolderAssistant = PropertyTagPropertyDefinition.InternalCreate("ControlDataForPublicFolderAssistant", PropTag.ControlDataForPublicFolderAssistant);

		// Token: 0x040047D0 RID: 18384
		public static readonly PropertyTagPropertyDefinition IsMarkedMailbox = PropertyTagPropertyDefinition.InternalCreate("IsMarkedMailbox", PropTag.MailboxMarked);

		// Token: 0x040047D1 RID: 18385
		public static readonly PropertyTagPropertyDefinition MailboxLastProcessedTimestamp = PropertyTagPropertyDefinition.InternalCreate("MailboxLastProcessedTimestamp", PropTag.MailboxLastProcessedTimestamp);

		// Token: 0x040047D2 RID: 18386
		public static readonly PropertyTagPropertyDefinition MailboxType = PropertyTagPropertyDefinition.InternalCreate("MailboxType", PropTag.MailboxType);

		// Token: 0x040047D3 RID: 18387
		public static readonly PropertyTagPropertyDefinition MailboxTypeDetail = PropertyTagPropertyDefinition.InternalCreate("MailboxTypeDetail", PropTag.MailboxTypeDetail);

		// Token: 0x040047D4 RID: 18388
		public static readonly PropertyTagPropertyDefinition ContactLinking = PropertyTagPropertyDefinition.InternalCreate("ContactLinking", PropTag.ContactLinking);

		// Token: 0x040047D5 RID: 18389
		public static readonly PropertyTagPropertyDefinition ContactSaveVersion = PropertyTagPropertyDefinition.InternalCreate("ContactSaveVersion", PropTag.ContactSaveVersion);

		// Token: 0x040047D6 RID: 18390
		public static readonly PropertyTagPropertyDefinition PushNotificationSubscriptionType = PropertyTagPropertyDefinition.InternalCreate("PushNotificationSubscriptionType", PropTag.PushNotificationSubscriptionType);

		// Token: 0x040047D7 RID: 18391
		public static readonly PropertyTagPropertyDefinition NotificationBrokerSubscriptions = PropertyTagPropertyDefinition.InternalCreate("NotificationBrokerSubscriptions", PropTag.NotificationBrokerSubscriptions);

		// Token: 0x040047D8 RID: 18392
		public static readonly PropertyTagPropertyDefinition ControlDataForInferenceDataCollectionAssistant = PropertyTagPropertyDefinition.InternalCreate("ControlDataForInferenceDataCollectionAssistant", PropTag.ControlDataForInferenceDataCollectionAssistant);

		// Token: 0x040047D9 RID: 18393
		public static readonly PropertyTagPropertyDefinition InferenceDataCollectionProcessingState = PropertyTagPropertyDefinition.InternalCreate("InferenceDataCollectionProcessingState", PropTag.InferenceDataCollectionProcessingState);

		// Token: 0x040047DA RID: 18394
		public static readonly PropertyTagPropertyDefinition SiteMailboxInternalState = PropertyTagPropertyDefinition.InternalCreate("SiteMailboxInternalState", PropTag.SiteMailboxInternalState);

		// Token: 0x040047DB RID: 18395
		public static readonly PropertyTagPropertyDefinition ControlDataForPeopleRelevanceAssistant = PropertyTagPropertyDefinition.InternalCreate("ControlDataForPeopleRelevanceAssistant", PropTag.ControlDataForPeopleRelevanceAssistant);

		// Token: 0x040047DC RID: 18396
		public static readonly PropertyTagPropertyDefinition ControlDataForSharePointSignalStoreAssistant = PropertyTagPropertyDefinition.InternalCreate("ControlDataForSharePointSignalStoreAssistant", PropTag.ControlDataForSharePointSignalStoreAssistant);

		// Token: 0x040047DD RID: 18397
		public static readonly PropertyTagPropertyDefinition ControlDataForPeopleCentricTriageAssistant = PropertyTagPropertyDefinition.InternalCreate("ControlDataForPeopleCentricTriageAssistant", PropTag.ControlDataForPeopleCentricTriageAssistant);

		// Token: 0x040047DE RID: 18398
		public static readonly PropertyTagPropertyDefinition InferenceTrainingLastContentCount = PropertyTagPropertyDefinition.InternalCreate("InferenceTrainingLastContentCount", PropTag.InferenceTrainingLastContentCount);

		// Token: 0x040047DF RID: 18399
		public static readonly PropertyTagPropertyDefinition InferenceTrainingLastAttemptTimestamp = PropertyTagPropertyDefinition.InternalCreate("InferenceTrainingLastAttemptTimestamp", PropTag.InferenceTrainingLastAttemptTimestamp);

		// Token: 0x040047E0 RID: 18400
		public static readonly PropertyTagPropertyDefinition InferenceTrainingLastSuccessTimestamp = PropertyTagPropertyDefinition.InternalCreate("InferenceTrainingLastSuccessTimestamp", PropTag.InferenceTrainingLastSuccessTimestamp);

		// Token: 0x040047E1 RID: 18401
		public static readonly PropertyTagPropertyDefinition InferenceTruthLoggingLastAttemptTimestamp = PropertyTagPropertyDefinition.InternalCreate("InferenceTruthLoggingLastAttemptTimestamp", PropTag.InferenceTruthLoggingLastAttemptTimestamp);

		// Token: 0x040047E2 RID: 18402
		public static readonly PropertyTagPropertyDefinition InferenceTruthLoggingLastSuccessTimestamp = PropertyTagPropertyDefinition.InternalCreate("InferenceTruthLoggingLastSuccessTimestamp", PropTag.InferenceTruthLoggingLastSuccessTimestamp);

		// Token: 0x040047E3 RID: 18403
		public static readonly PropertyTagPropertyDefinition InferenceUserCapabilityFlags = PropertyTagPropertyDefinition.InternalCreate("InferenceUserCapabilityFlags", PropTag.InferenceUserCapabilityFlags);

		// Token: 0x040047E4 RID: 18404
		public static readonly StorePropertyDefinition InferenceUserClassificationReady = new InferenceUserCapabilityFlagsProperty(Microsoft.Exchange.Data.Storage.InferenceUserCapabilityFlags.ClassificationReady);

		// Token: 0x040047E5 RID: 18405
		public static readonly StorePropertyDefinition InferenceUserUIReady = new InferenceUserCapabilityFlagsProperty(Microsoft.Exchange.Data.Storage.InferenceUserCapabilityFlags.UIReady);

		// Token: 0x040047E6 RID: 18406
		public static readonly StorePropertyDefinition InferenceClassificationEnabled = new InferenceUserCapabilityFlagsProperty(Microsoft.Exchange.Data.Storage.InferenceUserCapabilityFlags.ClassificationEnabled);

		// Token: 0x040047E7 RID: 18407
		public static readonly StorePropertyDefinition InferenceClutterEnabled = new InferenceUserCapabilityFlagsProperty(Microsoft.Exchange.Data.Storage.InferenceUserCapabilityFlags.ClutterEnabled);

		// Token: 0x040047E8 RID: 18408
		public static readonly StorePropertyDefinition InferenceHasBeenClutterInvited = new InferenceUserCapabilityFlagsProperty(Microsoft.Exchange.Data.Storage.InferenceUserCapabilityFlags.HasBeenClutterInvited);

		// Token: 0x040047E9 RID: 18409
		public static readonly PropertyTagPropertyDefinition PolicyTag = PropertyTagPropertyDefinition.InternalCreate("PolicyTag", PropTag.PolicyTag);

		// Token: 0x040047EA RID: 18410
		public static readonly GuidNamePropertyDefinition ExplicitPolicyTag = InternalSchema.CreateGuidNameProperty("ExplicitPolicyTag", typeof(byte[]), WellKnownPropertySet.Elc, "ExplicitPolicyTag");

		// Token: 0x040047EB RID: 18411
		public static readonly PropertyTagPropertyDefinition RetentionPeriod = PropertyTagPropertyDefinition.InternalCreate("RetentionPeriod", PropTag.RetentionPeriod);

		// Token: 0x040047EC RID: 18412
		public static readonly PropertyTagPropertyDefinition StartDateEtc = PropertyTagPropertyDefinition.InternalCreate("StartDateEtc", PropTag.StartDateEtc);

		// Token: 0x040047ED RID: 18413
		public static readonly PropertyTagPropertyDefinition RetentionDate = PropertyTagPropertyDefinition.InternalCreate("RetentionDate", PropTag.RetentionDate);

		// Token: 0x040047EE RID: 18414
		public static readonly GuidNamePropertyDefinition EHAMigrationExpirationDate = InternalSchema.CreateGuidNameProperty("EHAMigrationExpirationDate", typeof(ExDateTime), WellKnownPropertySet.Elc, "EHAMigrationExpirationDate");

		// Token: 0x040047EF RID: 18415
		public static readonly GuidNamePropertyDefinition EHAMigrationMessageCount = InternalSchema.CreateGuidNameProperty("EHAMigrationMessageCount", typeof(long), WellKnownPropertySet.Elc, "EHAMigrationMessageCount");

		// Token: 0x040047F0 RID: 18416
		public static readonly PropertyTagPropertyDefinition RetentionFlags = PropertyTagPropertyDefinition.InternalCreate("RetentionFlags", PropTag.RetentionFlags);

		// Token: 0x040047F1 RID: 18417
		public static readonly PropertyTagPropertyDefinition ArchiveTag = PropertyTagPropertyDefinition.InternalCreate("ArchiveTag", PropTag.ArchiveTag);

		// Token: 0x040047F2 RID: 18418
		public static readonly GuidNamePropertyDefinition ExplicitArchiveTag = InternalSchema.CreateGuidNameProperty("ExplicitArchiveTag", typeof(byte[]), WellKnownPropertySet.Elc, "ExplicitArchiveTag");

		// Token: 0x040047F3 RID: 18419
		public static readonly PropertyTagPropertyDefinition ArchiveDate = PropertyTagPropertyDefinition.InternalCreate("ArchiveDate", PropTag.ArchiveDate);

		// Token: 0x040047F4 RID: 18420
		public static readonly PropertyTagPropertyDefinition ArchivePeriod = PropertyTagPropertyDefinition.InternalCreate("ArchivePeriod", PropTag.ArchivePeriod);

		// Token: 0x040047F5 RID: 18421
		public static readonly GuidIdPropertyDefinition IsClassified = InternalSchema.CreateGuidIdProperty("IsClassified", typeof(bool), WellKnownPropertySet.Common, 34229);

		// Token: 0x040047F6 RID: 18422
		public static readonly PropertyTagPropertyDefinition SwappedToDoData = PropertyTagPropertyDefinition.InternalCreate("SwappedToDoData", (PropTag)237830402U);

		// Token: 0x040047F7 RID: 18423
		public static readonly PropertyTagPropertyDefinition SwappedToDoStore = PropertyTagPropertyDefinition.InternalCreate("SwappedToDoStore", (PropTag)237764866U);

		// Token: 0x040047F8 RID: 18424
		public static readonly GuidIdPropertyDefinition Classification = InternalSchema.CreateGuidIdProperty("Classification", typeof(string), WellKnownPropertySet.Common, 34230);

		// Token: 0x040047F9 RID: 18425
		public static readonly GuidIdPropertyDefinition ClassificationDescription = InternalSchema.CreateGuidIdProperty("ClassificationDescription", typeof(string), WellKnownPropertySet.Common, 34231);

		// Token: 0x040047FA RID: 18426
		public static readonly GuidIdPropertyDefinition ClassificationGuid = InternalSchema.CreateGuidIdProperty("ClassificationGuid", typeof(string), WellKnownPropertySet.Common, 34232);

		// Token: 0x040047FB RID: 18427
		public static readonly GuidIdPropertyDefinition ClassificationKeep = InternalSchema.CreateGuidIdProperty("ClassificationKeep", typeof(bool), WellKnownPropertySet.Common, 34234);

		// Token: 0x040047FC RID: 18428
		public static readonly GuidNamePropertyDefinition QuarantineOriginalSender = InternalSchema.CreateGuidNameProperty("QuarantineOriginalSender", typeof(string), WellKnownPropertySet.PublicStrings, "quarantine-original-sender");

		// Token: 0x040047FD RID: 18429
		public static readonly GuidNamePropertyDefinition JournalingRemoteAccounts = InternalSchema.CreateGuidNameProperty("JournalingRemoteAccounts", typeof(string[]), WellKnownPropertySet.PublicStrings, "journal-remote-accounts");

		// Token: 0x040047FE RID: 18430
		public static readonly GuidIdPropertyDefinition EmailListType = InternalSchema.CreateGuidIdProperty("EmailListType", typeof(int), WellKnownPropertySet.Address, 32809);

		// Token: 0x040047FF RID: 18431
		public static readonly GuidIdPropertyDefinition EmailList = InternalSchema.CreateGuidIdProperty("EmailList", typeof(int[]), WellKnownPropertySet.Address, 32808);

		// Token: 0x04004800 RID: 18432
		public static readonly PropertyTagPropertyDefinition PurportedSenderDomain = PropertyTagPropertyDefinition.InternalCreate("PurportedSenderDomain", PropTag.PurportedSenderDomain);

		// Token: 0x04004801 RID: 18433
		public static readonly PropertyTagPropertyDefinition ParentKey = PropertyTagPropertyDefinition.InternalCreate("ParentKey", PropTag.ParentKey);

		// Token: 0x04004802 RID: 18434
		public static readonly PropertyTagPropertyDefinition OriginalMessageId = PropertyTagPropertyDefinition.InternalCreate("OriginalMessageId", (PropTag)273023007U);

		// Token: 0x04004803 RID: 18435
		public static readonly GuidIdPropertyDefinition AppointmentCounterStartWhole = InternalSchema.CreateGuidIdProperty("AppointmentCounterStartWhole", typeof(ExDateTime), WellKnownPropertySet.Appointment, 33360);

		// Token: 0x04004804 RID: 18436
		public static readonly GuidIdPropertyDefinition AppointmentCounterEndWhole = InternalSchema.CreateGuidIdProperty("AppointmentCounterEndWhole", typeof(ExDateTime), WellKnownPropertySet.Appointment, 33361);

		// Token: 0x04004805 RID: 18437
		public static readonly GuidIdPropertyDefinition AppointmentProposedDuration = InternalSchema.CreateGuidIdProperty("AppointmentProposedDuration", typeof(int), WellKnownPropertySet.Appointment, 33366);

		// Token: 0x04004806 RID: 18438
		public static readonly GuidIdPropertyDefinition AppointmentCounterProposal = InternalSchema.CreateGuidIdProperty("AppointmentCounterProposal", typeof(bool), WellKnownPropertySet.Appointment, 33367);

		// Token: 0x04004807 RID: 18439
		public static readonly GuidIdPropertyDefinition UnsendableRecipients = InternalSchema.CreateGuidIdProperty("UnsendableRecipients", typeof(byte[]), WellKnownPropertySet.Appointment, 33373, PropertyFlags.Streamable, new PropertyDefinitionConstraint[0]);

		// Token: 0x04004808 RID: 18440
		public static readonly GuidIdPropertyDefinition ForwardNotificationRecipients = InternalSchema.CreateGuidIdProperty("ForwardNotificationRecipients", typeof(byte[]), WellKnownPropertySet.Appointment, 33377, PropertyFlags.Streamable, new PropertyDefinitionConstraint[0]);

		// Token: 0x04004809 RID: 18441
		public static readonly GuidIdPropertyDefinition AppointmentCounterProposalCount = InternalSchema.CreateGuidIdProperty("AppointmentCounterProposalCount", typeof(int), WellKnownPropertySet.Appointment, 33369);

		// Token: 0x0400480A RID: 18442
		public static readonly GuidNamePropertyDefinition PropertyChangeMetadataRaw = InternalSchema.CreateGuidNameProperty("PropertyChangeMetadataRaw", typeof(byte[]), WellKnownPropertySet.Appointment, "PropertyChangeMetadataRaw");

		// Token: 0x0400480B RID: 18443
		public static readonly PropertyTagPropertyDefinition RecipientProposed = PropertyTagPropertyDefinition.InternalCreate("RecipientProposed", (PropTag)1608581131U);

		// Token: 0x0400480C RID: 18444
		public static readonly PropertyTagPropertyDefinition RecipientProposedStartTime = PropertyTagPropertyDefinition.InternalCreate("RecipientProposedStartTime", (PropTag)1608712256U);

		// Token: 0x0400480D RID: 18445
		public static readonly PropertyTagPropertyDefinition RecipientProposedEndTime = PropertyTagPropertyDefinition.InternalCreate("RecipientProposedEndTime", (PropTag)1608777792U);

		// Token: 0x0400480E RID: 18446
		public static readonly PropertyTagPropertyDefinition RecipientOrder = PropertyTagPropertyDefinition.InternalCreate("RecipientOrder", (PropTag)1608450051U);

		// Token: 0x0400480F RID: 18447
		public static readonly GuidIdPropertyDefinition OutlookVersion = InternalSchema.CreateGuidIdProperty("OutlookVersion", typeof(string), WellKnownPropertySet.Common, 34132);

		// Token: 0x04004810 RID: 18448
		public static readonly GuidIdPropertyDefinition OutlookInternalVersion = InternalSchema.CreateGuidIdProperty("OutlookInternalVersion", typeof(int), WellKnownPropertySet.Common, 34130);

		// Token: 0x04004811 RID: 18449
		public static readonly PropertyTagPropertyDefinition NativeBlockStatus = PropertyTagPropertyDefinition.InternalCreate("NativeBlockStatus", (PropTag)278265859U);

		// Token: 0x04004812 RID: 18450
		public static readonly PropertyTagPropertyDefinition SelectedPreferredPhoneNumber = InternalSchema.UserConfigurationType;

		// Token: 0x04004813 RID: 18451
		public static readonly PropertyTagPropertyDefinition ReadReceiptDisplayName = PropertyTagPropertyDefinition.InternalCreate("ReadReceiptDisplayName", (PropTag)1076559903U);

		// Token: 0x04004814 RID: 18452
		public static readonly PropertyTagPropertyDefinition ReadReceiptEmailAddress = PropertyTagPropertyDefinition.InternalCreate("ReadReceiptEmailAddress", (PropTag)1076494367U);

		// Token: 0x04004815 RID: 18453
		public static readonly PropertyTagPropertyDefinition ReadReceiptAddrType = PropertyTagPropertyDefinition.InternalCreate("ReadReceiptAddrType", (PropTag)1076428831U, new PropertyDefinitionConstraint[]
		{
			new NonMoveMailboxPropertyConstraint(new StringLengthConstraint(0, 9))
		});

		// Token: 0x04004816 RID: 18454
		public static readonly PropertyTagPropertyDefinition ReportEntryId = PropertyTagPropertyDefinition.InternalCreate("ReportEntryId", PropTag.ReportEntryId);

		// Token: 0x04004817 RID: 18455
		public static readonly PropertyTagPropertyDefinition ReadReceiptEntryId = PropertyTagPropertyDefinition.InternalCreate("ReadReceiptEntryId", PropTag.ReadReceiptEntryId);

		// Token: 0x04004818 RID: 18456
		public static readonly PropertyTagPropertyDefinition ReplyTemplateId = PropertyTagPropertyDefinition.InternalCreate("REPLY_TEMPLATE_ID", PropTag.ReplyTemplateID);

		// Token: 0x04004819 RID: 18457
		[MessageClassSpecific("IPM.Microsoft.WunderBar.SFInfo")]
		public static readonly PropertyTagPropertyDefinition AssociatedSearchFolderId = PropertyTagPropertyDefinition.InternalCreate("AssociatedSearchFolderId", (PropTag)1749156098U);

		// Token: 0x0400481A RID: 18458
		public static readonly PropertyTagPropertyDefinition AssociatedSearchFolderFlags = PropertyTagPropertyDefinition.InternalCreate("AssociatedSearchFolderFlags", (PropTag)1749549059U);

		// Token: 0x0400481B RID: 18459
		public static readonly PropertyTagPropertyDefinition AssociatedSearchFolderExpiration = PropertyTagPropertyDefinition.InternalCreate("AssociatedSearchFolderExpiration", (PropTag)1748631555U);

		// Token: 0x0400481C RID: 18460
		public static readonly PropertyTagPropertyDefinition AssociatedSearchFolderLastUsedTime = PropertyTagPropertyDefinition.InternalCreate("AssociatedSearchFolderLastUsedTime", PropTag.InferenceClientActivityFlags);

		// Token: 0x0400481D RID: 18461
		public static readonly PropertyTagPropertyDefinition AssociatedSearchFolderTemplateId = PropertyTagPropertyDefinition.InternalCreate("AssociatedSearchFolderTemplateId", (PropTag)1749090307U);

		// Token: 0x0400481E RID: 18462
		[MessageClassSpecific("IPM.Microsoft.WunderBar.SFInfo")]
		public static readonly PropertyTagPropertyDefinition AssociatedSearchFolderTag = PropertyTagPropertyDefinition.InternalCreate("AssociatedSearchFolderTag", (PropTag)1749483523U);

		// Token: 0x0400481F RID: 18463
		public static readonly PropertyTagPropertyDefinition AssociatedSearchFolderStorageType = PropertyTagPropertyDefinition.InternalCreate("AssociatedSearchFolderStorageType", (PropTag)1749417987U);

		// Token: 0x04004820 RID: 18464
		public static readonly PropertyTagPropertyDefinition AssociatedSearchFolderDefinition = PropertyTagPropertyDefinition.InternalCreate("AssociatedSearchFolderDefinition", (PropTag)1749352706U, PropertyFlags.Streamable);

		// Token: 0x04004821 RID: 18465
		[MessageClassSpecific("IPM.Microsoft.WunderBar.Link")]
		public static readonly PropertyTagPropertyDefinition NavigationNodeGroupClassId = PropertyTagPropertyDefinition.InternalCreate("NavigationNodeGroupClassId", (PropTag)1749156098U);

		// Token: 0x04004822 RID: 18466
		[MessageClassSpecific("IPM.Microsoft.WunderBar.Link")]
		public static readonly PropertyTagPropertyDefinition NavigationNodeOutlookTagId = PropertyTagPropertyDefinition.InternalCreate("NavigationNodeOutlookTagId", (PropTag)1749483523U);

		// Token: 0x04004823 RID: 18467
		public static readonly PropertyTagPropertyDefinition NavigationNodeType = PropertyTagPropertyDefinition.InternalCreate("NavigationNodeType", (PropTag)1749614595U);

		// Token: 0x04004824 RID: 18468
		public static readonly PropertyTagPropertyDefinition NavigationNodeFlags = PropertyTagPropertyDefinition.InternalCreate("NavigationNodeFlags", (PropTag)1749680131U);

		// Token: 0x04004825 RID: 18469
		public static readonly PropertyTagPropertyDefinition NavigationNodeOrdinal = PropertyTagPropertyDefinition.InternalCreate("NavigationNodeOrdinal", (PropTag)1749745922U);

		// Token: 0x04004826 RID: 18470
		public static readonly PropertyTagPropertyDefinition NavigationNodeEntryId = PropertyTagPropertyDefinition.InternalCreate("NavigationNodeEntryId", (PropTag)1749811458U);

		// Token: 0x04004827 RID: 18471
		public static readonly PropertyTagPropertyDefinition NavigationNodeRecordKey = PropertyTagPropertyDefinition.InternalCreate("NavigationNodeRecordKey", (PropTag)1749876994U);

		// Token: 0x04004828 RID: 18472
		public static readonly PropertyTagPropertyDefinition NavigationNodeStoreEntryId = PropertyTagPropertyDefinition.InternalCreate("NavigationNodeStoreEntryId", (PropTag)1749942530U);

		// Token: 0x04004829 RID: 18473
		public static readonly PropertyTagPropertyDefinition NavigationNodeClassId = PropertyTagPropertyDefinition.InternalCreate("NavigationNodeClassId", (PropTag)1750008066U);

		// Token: 0x0400482A RID: 18474
		public static readonly PropertyTagPropertyDefinition NavigationNodeParentGroupClassId = PropertyTagPropertyDefinition.InternalCreate("NavigationNodeParentGroupClassId", (PropTag)1750073602U);

		// Token: 0x0400482B RID: 18475
		public static readonly PropertyTagPropertyDefinition NavigationNodeGroupName = PropertyTagPropertyDefinition.InternalCreate("NavigationNodeGroupName", (PropTag)1750138911U);

		// Token: 0x0400482C RID: 18476
		public static readonly PropertyTagPropertyDefinition NavigationNodeGroupSection = PropertyTagPropertyDefinition.InternalCreate("NavigationNodeGroupSection", (PropTag)1750204419U);

		// Token: 0x0400482D RID: 18477
		public static readonly PropertyTagPropertyDefinition NavigationNodeCalendarColor = PropertyTagPropertyDefinition.InternalCreate("NavigationNodeCalendarColor", (PropTag)1750269955U);

		// Token: 0x0400482E RID: 18478
		public static readonly PropertyTagPropertyDefinition NavigationNodeAddressBookEntryId = PropertyTagPropertyDefinition.InternalCreate("NavigationNodeAddressBookEntryId", (PropTag)1750335746U);

		// Token: 0x0400482F RID: 18479
		public static readonly GuidNamePropertyDefinition NavigationNodeCalendarTypeFromOlderExchange = InternalSchema.CreateGuidNameProperty("OWA-NavigationNodeCalendarTypeFromOlderExchange", typeof(int), WellKnownPropertySet.Sharing, "OWA-NavigationNodeCalendarTypeFromOlderExchange");

		// Token: 0x04004830 RID: 18480
		public static readonly PropertyTagPropertyDefinition NavigationNodeAddressBookStoreEntryId = PropertyTagPropertyDefinition.InternalCreate("NavigationNodeAddressBookStoreEntryId", (PropTag)1754333442U);

		// Token: 0x04004831 RID: 18481
		public static readonly PropertyTagPropertyDefinition SearchAllIndexedProps = PropertyTagPropertyDefinition.InternalCreate("SearchAllIndexedProps", PropTag.SearchAllIndexedProps, PropertyFlags.None);

		// Token: 0x04004832 RID: 18482
		public static readonly PropertyTagPropertyDefinition SearchIsPartiallyIndexed = PropertyTagPropertyDefinition.InternalCreate("SearchIsPartiallyIndexed", (PropTag)248381451U, PropertyFlags.None);

		// Token: 0x04004833 RID: 18483
		public static readonly PropertyTagPropertyDefinition SearchSender = PropertyTagPropertyDefinition.InternalCreate("SearchSender", PropTag.SearchSender, PropertyFlags.None);

		// Token: 0x04004834 RID: 18484
		public static readonly PropertyTagPropertyDefinition SearchRecipients = PropertyTagPropertyDefinition.InternalCreate("SearchRecipients", PropTag.SearchRecipients, PropertyFlags.None);

		// Token: 0x04004835 RID: 18485
		public static readonly PropertyTagPropertyDefinition SearchRecipientsTo = PropertyTagPropertyDefinition.InternalCreate("SearchRecipientsTo", PropTag.SearchRecipientsTo, PropertyFlags.None);

		// Token: 0x04004836 RID: 18486
		public static readonly PropertyTagPropertyDefinition SearchRecipientsCc = PropertyTagPropertyDefinition.InternalCreate("SearchRecipientsCc", PropTag.SearchRecipientsCc, PropertyFlags.None);

		// Token: 0x04004837 RID: 18487
		public static readonly PropertyTagPropertyDefinition SearchRecipientsBcc = PropertyTagPropertyDefinition.InternalCreate("SearchRecipientsBcc", PropTag.SearchRecipientsBcc, PropertyFlags.None);

		// Token: 0x04004838 RID: 18488
		public static readonly PropertyTagPropertyDefinition SearchFullText = PropertyTagPropertyDefinition.InternalCreate("SearchFullText", PropTag.SearchFullText, PropertyFlags.None);

		// Token: 0x04004839 RID: 18489
		public static readonly PropertyTagPropertyDefinition SearchFullTextSubject = PropertyTagPropertyDefinition.InternalCreate("SearchFullTextSubject", PropTag.SearchFullTextSubject, PropertyFlags.None);

		// Token: 0x0400483A RID: 18490
		public static readonly PropertyTagPropertyDefinition SearchFullTextBody = PropertyTagPropertyDefinition.InternalCreate("SearchFullTextBody", PropTag.SearchFullTextBody, PropertyFlags.None);

		// Token: 0x0400483B RID: 18491
		public static readonly PropertyTagPropertyDefinition QuotaStorageWarning = PropertyTagPropertyDefinition.InternalCreate("ptagStorageQuota", PropTag.PfStorageQuota);

		// Token: 0x0400483C RID: 18492
		public static PropertyTagPropertyDefinition CustomerId = PropertyTagPropertyDefinition.InternalCreate("CustomerId", PropTag.CustomerId);

		// Token: 0x0400483D RID: 18493
		public static PropertyTagPropertyDefinition GovernmentIdNumber = PropertyTagPropertyDefinition.InternalCreate("GovernmentIdNumber", PropTag.GovernmentIdNumber);

		// Token: 0x0400483E RID: 18494
		public static PropertyTagPropertyDefinition Account = PropertyTagPropertyDefinition.InternalCreate("Account", PropTag.Account);

		// Token: 0x0400483F RID: 18495
		public static readonly PropertyTagPropertyDefinition DelegateNames = PropertyTagPropertyDefinition.InternalCreate("DelegateNames", (PropTag)1749684255U);

		// Token: 0x04004840 RID: 18496
		public static readonly PropertyTagPropertyDefinition DelegateEntryIds = PropertyTagPropertyDefinition.InternalCreate("DelegateEntryIds", (PropTag)1749356802U);

		// Token: 0x04004841 RID: 18497
		public static readonly PropertyTagPropertyDefinition DelegateFlags = PropertyTagPropertyDefinition.InternalCreate("DelegateFlags", (PropTag)1751846915U);

		// Token: 0x04004842 RID: 18498
		public static readonly PropertyTagPropertyDefinition DelegateEntryIds2 = PropertyTagPropertyDefinition.InternalCreate("DelegateEntryIds2", (PropTag)1752174850U);

		// Token: 0x04004843 RID: 18499
		public static readonly PropertyTagPropertyDefinition DelegateFlags2 = PropertyTagPropertyDefinition.InternalCreate("DelegateFlags2", (PropTag)1752240131U);

		// Token: 0x04004844 RID: 18500
		public static readonly PropertyTagPropertyDefinition DelegateBossWantsCopy = PropertyTagPropertyDefinition.InternalCreate("DelegateBossWantsCopy", (PropTag)1749155851U);

		// Token: 0x04004845 RID: 18501
		public static readonly PropertyTagPropertyDefinition DelegateBossWantsInfo = PropertyTagPropertyDefinition.InternalCreate("DelegateBossWantsInfo", (PropTag)1749745675U);

		// Token: 0x04004846 RID: 18502
		public static readonly PropertyTagPropertyDefinition DelegateDontMail = PropertyTagPropertyDefinition.InternalCreate("DelegateDontMail", (PropTag)1749221387U);

		// Token: 0x04004847 RID: 18503
		public static readonly PropertyTagPropertyDefinition FreeBusyEntryIds = PropertyTagPropertyDefinition.InternalCreate("FreeBusyEntryIds", (PropTag)920916226U);

		// Token: 0x04004848 RID: 18504
		public static readonly PropertyTagPropertyDefinition ScheduleInfoMonthsTentative = PropertyTagPropertyDefinition.InternalCreate("ScheduleInfoMonthsTentative", (PropTag)1750142979U);

		// Token: 0x04004849 RID: 18505
		public static readonly PropertyTagPropertyDefinition ScheduleInfoFreeBusyTentative = PropertyTagPropertyDefinition.InternalCreate("ScheduleInfoFreeBusyTentative", (PropTag)1750208770U);

		// Token: 0x0400484A RID: 18506
		public static readonly PropertyTagPropertyDefinition ScheduleInfoMonthsBusy = PropertyTagPropertyDefinition.InternalCreate("ScheduleInfoMonthsBusy", (PropTag)1750274051U);

		// Token: 0x0400484B RID: 18507
		public static readonly PropertyTagPropertyDefinition ScheduleInfoFreeBusyBusy = PropertyTagPropertyDefinition.InternalCreate("ScheduleInfoFreeBusyBusy", (PropTag)1750339842U);

		// Token: 0x0400484C RID: 18508
		public static readonly PropertyTagPropertyDefinition ScheduleInfoMonthsOof = PropertyTagPropertyDefinition.InternalCreate("ScheduleInfoMonthsOof", (PropTag)1750405123U);

		// Token: 0x0400484D RID: 18509
		public static readonly PropertyTagPropertyDefinition ScheduleInfoFreeBusyOof = PropertyTagPropertyDefinition.InternalCreate("ScheduleInfoFreeBusyOof", (PropTag)1750470914U);

		// Token: 0x0400484E RID: 18510
		public static readonly PropertyTagPropertyDefinition ScheduleInfoMonthsMerged = PropertyTagPropertyDefinition.InternalCreate("ScheduleInfoMonthsMerged", (PropTag)1750011907U);

		// Token: 0x0400484F RID: 18511
		public static readonly PropertyTagPropertyDefinition ScheduleInfoFreeBusyMerged = PropertyTagPropertyDefinition.InternalCreate("ScheduleInfoFreeBusyMerged", (PropTag)1750077698U);

		// Token: 0x04004850 RID: 18512
		public static readonly PropertyTagPropertyDefinition ScheduleInfoRecipientLegacyDn = PropertyTagPropertyDefinition.InternalCreate("ScheduleInfoRecipientLegacyDn", (PropTag)1749614622U);

		// Token: 0x04004851 RID: 18513
		public static readonly PropertyTagPropertyDefinition OutlookFreeBusyMonthCount = PropertyTagPropertyDefinition.InternalCreate("OutlookFreeBusyMonthCount", (PropTag)1751711747U);

		// Token: 0x04004852 RID: 18514
		public static readonly GuidNamePropertyDefinition DRMLicense = InternalSchema.CreateGuidNameProperty("DRMLicense", typeof(byte[][]), WellKnownPropertySet.PublicStrings, "DRMLicense");

		// Token: 0x04004853 RID: 18515
		public static readonly GuidNamePropertyDefinition DRMServerLicense = InternalSchema.CreateGuidNameProperty("DRMServerLicense", typeof(string), WellKnownPropertySet.PublicStrings, "DRMServerLicense");

		// Token: 0x04004854 RID: 18516
		public static readonly GuidNamePropertyDefinition DRMServerLicenseCompressed = InternalSchema.CreateGuidNameProperty("DRMServerLicenseCompressed", typeof(byte[]), WellKnownPropertySet.PublicStrings, "DRMServerLicenseCompressed", PropertyFlags.Streamable, new PropertyDefinitionConstraint[0]);

		// Token: 0x04004855 RID: 18517
		public static readonly GuidNamePropertyDefinition DRMRights = InternalSchema.CreateGuidNameProperty("DRMRights", typeof(int), WellKnownPropertySet.PublicStrings, "DRMRights");

		// Token: 0x04004856 RID: 18518
		public static readonly GuidNamePropertyDefinition DRMExpiryTime = InternalSchema.CreateGuidNameProperty("DRMExpiryTime", typeof(ExDateTime), WellKnownPropertySet.PublicStrings, "DRMExpiryTime");

		// Token: 0x04004857 RID: 18519
		public static readonly GuidNamePropertyDefinition DRMPropsSignature = InternalSchema.CreateGuidNameProperty("DRMPropsSignature", typeof(byte[]), WellKnownPropertySet.PublicStrings, "DRMPropsSignature");

		// Token: 0x04004858 RID: 18520
		public static readonly GuidNamePropertyDefinition DrmPublishLicense = InternalSchema.CreateGuidNameProperty("DrmPublishLicense", typeof(string), WellKnownPropertySet.PublicStrings, "DrmPublishLicense");

		// Token: 0x04004859 RID: 18521
		public static readonly GuidNamePropertyDefinition DRMPrelicenseFailure = InternalSchema.CreateGuidNameProperty("DRMPrelicenseFailure", typeof(int), WellKnownPropertySet.PublicStrings, "DRMPrelicenseFailure");

		// Token: 0x0400485A RID: 18522
		public static readonly GuidNamePropertyDefinition AcceptLanguage = InternalSchema.CreateGuidNameProperty("AcceptLanguage", typeof(string), WellKnownPropertySet.InternetHeaders, "AcceptLanguage");

		// Token: 0x0400485B RID: 18523
		public static readonly GuidNamePropertyDefinition OutlookSpoofingStamp = InternalSchema.CreateGuidNameProperty("SpoofingStamp", typeof(int), WellKnownPropertySet.PublicStrings, "http://schemas.microsoft.com/outlook/spoofingstamp");

		// Token: 0x0400485C RID: 18524
		public static readonly GuidNamePropertyDefinition OutlookPhishingStamp = InternalSchema.CreateGuidNameProperty("PhishingStamp", typeof(int), WellKnownPropertySet.PublicStrings, "http://schemas.microsoft.com/outlook/phishingstamp");

		// Token: 0x0400485D RID: 18525
		public static readonly GuidNamePropertyDefinition OwaViewStateSortColumn = InternalSchema.CreateGuidNameProperty("OwaViewStateSortColumn", typeof(string), WellKnownPropertySet.PublicStrings, "http://schemas.microsoft.com/exchange/wcsortcolumn");

		// Token: 0x0400485E RID: 18526
		public static readonly GuidNamePropertyDefinition OwaViewStateSortOrder = InternalSchema.CreateGuidNameProperty("OwaViewStateSortOrder", typeof(int), WellKnownPropertySet.PublicStrings, "http://schemas.microsoft.com/exchange/wcsortorder");

		// Token: 0x0400485F RID: 18527
		public static readonly PropertyTagPropertyDefinition ContentFilterPcl = PropertyTagPropertyDefinition.InternalCreate("ContenFilterPcl", (PropTag)1082392579U);

		// Token: 0x04004860 RID: 18528
		public static readonly GuidIdPropertyDefinition ProviderGuidBinary = InternalSchema.CreateGuidIdProperty("OutlookProviderGuidBinary", typeof(byte[]), WellKnownPropertySet.Sharing, 35329);

		// Token: 0x04004861 RID: 18529
		public static readonly PropertyTagPropertyDefinition RecipientEntryId = PropertyTagPropertyDefinition.InternalCreate("RecipientEntryId", (PropTag)1610023170U);

		// Token: 0x04004862 RID: 18530
		public static readonly PropertyTagPropertyDefinition DisplayTypeExInternal = PropertyTagPropertyDefinition.InternalCreate("DisplayTypeExInternal", PropTag.DisplayTypeEx);

		// Token: 0x04004863 RID: 18531
		public static readonly PropertyTagPropertyDefinition RemoteMta = PropertyTagPropertyDefinition.InternalCreate("ReportingMta", (PropTag)203489311U);

		// Token: 0x04004864 RID: 18532
		public static readonly PropertyTagPropertyDefinition Responsibility = PropertyTagPropertyDefinition.InternalCreate("PR_RESPONSIBILITY", PropTag.Responsibility);

		// Token: 0x04004865 RID: 18533
		public static readonly PropertyTagPropertyDefinition DavSubmitData = PropertyTagPropertyDefinition.InternalCreate("ptagDAVSubmitData", PropTag.DavSubmitData);

		// Token: 0x04004866 RID: 18534
		public static readonly PropertyTagPropertyDefinition SendRichInfo = PropertyTagPropertyDefinition.InternalCreate("SendRichInfo", PropTag.SendRichInfo);

		// Token: 0x04004867 RID: 18535
		public static readonly PropertyTagPropertyDefinition SendInternetEncoding = PropertyTagPropertyDefinition.InternalCreate("SendInternetEncoding", PropTag.SendInternetEncoding);

		// Token: 0x04004868 RID: 18536
		public static readonly PropertyTagPropertyDefinition SpamConfidenceLevel = PropertyTagPropertyDefinition.InternalCreate("SpamConfidenceLevel", PropTag.SpamConfidenceLevel);

		// Token: 0x04004869 RID: 18537
		public static readonly PropertyTagPropertyDefinition StorageQuotaLimit = PropertyTagPropertyDefinition.InternalCreate("StorageQuotaLimit", PropTag.StorageQuotaLimit);

		// Token: 0x0400486A RID: 18538
		public static readonly PropertyTagPropertyDefinition PersistableTenantPartitionHint = PropertyTagPropertyDefinition.InternalCreate("PersistableTenantPartitionHint", PropTag.PersistableTenantPartitionHint);

		// Token: 0x0400486B RID: 18539
		public static readonly PropertyTagPropertyDefinition ExcessStorageUsed = PropertyTagPropertyDefinition.InternalCreate("ExcessStorageUsed", PropTag.ExcessStorageUsed);

		// Token: 0x0400486C RID: 18540
		public static readonly PropertyTagPropertyDefinition SvrGeneratingQuotaMsg = PropertyTagPropertyDefinition.InternalCreate("SvrGeneratingQuotaMsg", PropTag.SvrGeneratingQuotaMsg);

		// Token: 0x0400486D RID: 18541
		public static readonly PropertyTagPropertyDefinition PrimaryMbxOverQuota = PropertyTagPropertyDefinition.InternalCreate("PrimaryMbxOverQuota", PropTag.PrimaryMbxOverQuota);

		// Token: 0x0400486E RID: 18542
		public static readonly PropertyTagPropertyDefinition QuotaType = PropertyTagPropertyDefinition.InternalCreate("QuotaType", PropTag.QuotaType);

		// Token: 0x0400486F RID: 18543
		public static readonly PropertyTagPropertyDefinition IsPublicFolderQuotaMessage = PropertyTagPropertyDefinition.InternalCreate("IsPublicFolderQuotaMessage", PropTag.IsPublicFolderQuotaMessage);

		// Token: 0x04004870 RID: 18544
		public static readonly GuidNamePropertyDefinition Contact = InternalSchema.CreateGuidNameProperty("Contact", typeof(string), WellKnownPropertySet.PublicStrings, "urn:schemas:calendar:contact");

		// Token: 0x04004871 RID: 18545
		public static readonly GuidNamePropertyDefinition ContactURL = InternalSchema.CreateGuidNameProperty("ContactURL", typeof(string), WellKnownPropertySet.PublicStrings, "urn:schemas:calendar:contacturl");

		// Token: 0x04004872 RID: 18546
		public static readonly GuidNamePropertyDefinition MobilePhone2 = InternalSchema.CreateGuidNameProperty("MobilePhone2", typeof(string), WellKnownPropertySet.Address, "ContactMobilePhone2");

		// Token: 0x04004873 RID: 18547
		public static readonly GuidNamePropertyDefinition OtherPhone2 = InternalSchema.CreateGuidNameProperty("OtherPhone2", typeof(string), WellKnownPropertySet.Address, "ContactOtherPhone2");

		// Token: 0x04004874 RID: 18548
		public static readonly GuidNamePropertyDefinition HomePhoneAttributes = InternalSchema.CreateGuidNameProperty("HomePhoneAttributes", typeof(string), WellKnownPropertySet.Address, "ContactHomePhoneAttributes");

		// Token: 0x04004875 RID: 18549
		public static readonly GuidNamePropertyDefinition WorkPhoneAttributes = InternalSchema.CreateGuidNameProperty("WorkPhoneAttributes", typeof(string), WellKnownPropertySet.Address, "ContactWorkPhoneAttributes");

		// Token: 0x04004876 RID: 18550
		public static readonly GuidNamePropertyDefinition MobilePhoneAttributes = InternalSchema.CreateGuidNameProperty("MobilePhoneAttributes", typeof(string), WellKnownPropertySet.Address, "ContactMobilePhoneAttributes");

		// Token: 0x04004877 RID: 18551
		public static readonly GuidNamePropertyDefinition OtherPhoneAttributes = InternalSchema.CreateGuidNameProperty("OtherPhoneAttributes", typeof(string), WellKnownPropertySet.Address, "ContactOtherPhoneAttributes");

		// Token: 0x04004878 RID: 18552
		public static readonly GuidNamePropertyDefinition LocationURL = InternalSchema.CreateGuidNameProperty("LocationURL", typeof(string), WellKnownPropertySet.PublicStrings, "urn:schemas:calendar:locationurl");

		// Token: 0x04004879 RID: 18553
		public static readonly GuidNamePropertyDefinition Keywords = InternalSchema.Categories;

		// Token: 0x0400487A RID: 18554
		public static readonly PropertyTagPropertyDefinition UserX509Certificates = PropertyTagPropertyDefinition.InternalCreate("UserX509Certificates", PropTag.UserSMimeCertificate);

		// Token: 0x0400487B RID: 18555
		public static readonly PropertyTagPropertyDefinition DeletedOnTime = PropertyTagPropertyDefinition.InternalCreate("DeletedOnTime", (PropTag)1720647744U);

		// Token: 0x0400487C RID: 18556
		public static readonly PropertyTagPropertyDefinition IsSoftDeleted = PropertyTagPropertyDefinition.InternalCreate("IsSoftDeleted", (PropTag)1735393291U);

		// Token: 0x0400487D RID: 18557
		public static readonly GuidIdPropertyDefinition ExceptionalBody = InternalSchema.CreateGuidIdProperty("ExceptionalBody", typeof(bool), WellKnownPropertySet.Appointment, 33286);

		// Token: 0x0400487E RID: 18558
		public static readonly GuidIdPropertyDefinition ExceptionalAttendees = InternalSchema.CreateGuidIdProperty("ExceptionalAttendees", typeof(bool), WellKnownPropertySet.Appointment, 33323);

		// Token: 0x0400487F RID: 18559
		public static readonly GuidIdPropertyDefinition AppointmentReplyName = InternalSchema.CreateGuidIdProperty("AppointmentReplyName", typeof(string), WellKnownPropertySet.Appointment, 33328);

		// Token: 0x04004880 RID: 18560
		public static readonly GuidIdPropertyDefinition InboundICalStream = InternalSchema.CreateGuidIdProperty("InboundICalStream", typeof(byte[]), WellKnownPropertySet.Appointment, 33402, PropertyFlags.Streamable, new PropertyDefinitionConstraint[0]);

		// Token: 0x04004881 RID: 18561
		public static readonly GuidIdPropertyDefinition IsSingleBodyICal = InternalSchema.CreateGuidIdProperty("IsSingleBodyICal", typeof(bool), WellKnownPropertySet.Appointment, 33403);

		// Token: 0x04004882 RID: 18562
		public static readonly GuidNamePropertyDefinition ElcFolderLocalizedName = InternalSchema.CreateGuidNameProperty("ElcFolderLocalizedName", typeof(string), WellKnownPropertySet.Elc, "ElcFolderLocalizedName");

		// Token: 0x04004883 RID: 18563
		public static readonly GuidIdPropertyDefinition OutlookUserPropsFormStorage = InternalSchema.CreateGuidIdProperty("dispidFormStorage", typeof(byte[]), WellKnownPropertySet.Common, 34063);

		// Token: 0x04004884 RID: 18564
		public static readonly GuidIdPropertyDefinition OutlookUserPropsScriptStream = InternalSchema.CreateGuidIdProperty("dispidScriptStream", typeof(byte[]), WellKnownPropertySet.Common, 34113);

		// Token: 0x04004885 RID: 18565
		public static readonly GuidIdPropertyDefinition OutlookUserPropsFormPropStream = InternalSchema.CreateGuidIdProperty("dispidFormPropStream", typeof(byte[]), WellKnownPropertySet.Common, 34075);

		// Token: 0x04004886 RID: 18566
		public static readonly GuidIdPropertyDefinition OutlookUserPropsPageDirStream = InternalSchema.CreateGuidIdProperty("dispidPageDirStream", typeof(byte[]), WellKnownPropertySet.Common, 34067);

		// Token: 0x04004887 RID: 18567
		public static readonly GuidIdPropertyDefinition OutlookUserPropsPropDefStream = InternalSchema.CreateGuidIdProperty("dispidPropDefStream", typeof(byte[]), WellKnownPropertySet.Common, 34112);

		// Token: 0x04004888 RID: 18568
		public static readonly GuidIdPropertyDefinition OutlookUserPropsCustomFlag = InternalSchema.CreateGuidIdProperty("dispidCustomFlag", typeof(int), WellKnownPropertySet.Common, 34114);

		// Token: 0x04004889 RID: 18569
		public static readonly PropertyTagPropertyDefinition ConversationIndexTracking = PropertyTagPropertyDefinition.InternalCreate("ConversationIndexTracking", PropTag.ConversationIndexTracking);

		// Token: 0x0400488A RID: 18570
		public static readonly GuidNamePropertyDefinition ConversationIndexTrackingEx = InternalSchema.CreateGuidNameProperty("ConversationIndexTrackingEx", typeof(string), WellKnownPropertySet.Conversations, "ConversationIndexTrackingEx");

		// Token: 0x0400488B RID: 18571
		public static readonly PropertyTagPropertyDefinition ConversationIdHash = PropertyTagPropertyDefinition.InternalCreate("ConversationIdHash", PropTag.ConversationIdHash);

		// Token: 0x0400488C RID: 18572
		public static readonly PropertyTagPropertyDefinition MapiConversationId = PropertyTagPropertyDefinition.InternalCreate("Conversation Id", PropTag.ConversationId);

		// Token: 0x0400488D RID: 18573
		public static readonly PropertyTagPropertyDefinition InternalConversationMVFrom = PropertyTagPropertyDefinition.InternalCreate("Conversation MVFrom", PropTag.ConversationMvFrom);

		// Token: 0x0400488E RID: 18574
		public static readonly PropertyTagPropertyDefinition InternalConversationGlobalMVFrom = PropertyTagPropertyDefinition.InternalCreate("Conversation Global MVFrom", PropTag.ConversationMvFromMailboxWide);

		// Token: 0x0400488F RID: 18575
		public static readonly PropertyTagPropertyDefinition InternalConversationMVTo = PropertyTagPropertyDefinition.InternalCreate("Conversation MVTo", PropTag.ConversationMvTo);

		// Token: 0x04004890 RID: 18576
		public static readonly PropertyTagPropertyDefinition InternalConversationGlobalMVTo = PropertyTagPropertyDefinition.InternalCreate("Conversation MailboxWide MVTo", PropTag.ConversationMvToMailboxWide);

		// Token: 0x04004891 RID: 18577
		public static readonly PropertyTagPropertyDefinition InternalConversationLastDeliveryTime = PropertyTagPropertyDefinition.InternalCreate("Conversation LastDeliveryTime", PropTag.ConversationMsgDeliveryTime);

		// Token: 0x04004892 RID: 18578
		public static readonly PropertyTagPropertyDefinition InternalConversationGlobalLastDeliveryTime = PropertyTagPropertyDefinition.InternalCreate("Conversation MailboxWide LastDeliveryTime", PropTag.ConversationMsgDeliveryTimeMailboxWide);

		// Token: 0x04004893 RID: 18579
		public static readonly PropertyTagPropertyDefinition InternalConversationLastDeliveryOrRenewTime = PropertyTagPropertyDefinition.InternalCreate("Conversation LastDeliveryOrRenewTime", PropTag.ConversationMessageDeliveryOrRenewTime);

		// Token: 0x04004894 RID: 18580
		public static readonly PropertyTagPropertyDefinition InternalConversationGlobalLastDeliveryOrRenewTime = PropertyTagPropertyDefinition.InternalCreate("Conversation MailboxWide LastDeliveryOrRenewTime", PropTag.ConversationMessageDeliveryOrRenewTimeMailboxWide);

		// Token: 0x04004895 RID: 18581
		public static readonly PropertyTagPropertyDefinition InternalConversationMailboxGuid = PropertyTagPropertyDefinition.InternalCreate("Conversation MailboxGuid", PropTag.MailboxDSGuidGuid);

		// Token: 0x04004896 RID: 18582
		public static readonly PropertyTagPropertyDefinition InternalConversationCategories = PropertyTagPropertyDefinition.InternalCreate("Conversation Categories", PropTag.ConversationCategories);

		// Token: 0x04004897 RID: 18583
		public static readonly PropertyTagPropertyDefinition InternalConversationGlobalCategories = PropertyTagPropertyDefinition.InternalCreate("Conversation MailboxWide Categories", PropTag.ConversationCategoriesMailboxWide);

		// Token: 0x04004898 RID: 18584
		public static readonly PropertyTagPropertyDefinition InternalConversationFlagStatus = PropertyTagPropertyDefinition.InternalCreate("Conversation FlagStatus", PropTag.ConversationFlagStatus);

		// Token: 0x04004899 RID: 18585
		public static readonly PropertyTagPropertyDefinition InternalConversationGlobalFlagStatus = PropertyTagPropertyDefinition.InternalCreate("Conversation MailboxWide FlagStatus", PropTag.ConversationFlagStatusMailboxWide);

		// Token: 0x0400489A RID: 18586
		public static readonly PropertyTagPropertyDefinition InternalConversationFlagCompleteTime = PropertyTagPropertyDefinition.InternalCreate("Conversation FlagCompleteTime", PropTag.ConversationFlagCompleteTime);

		// Token: 0x0400489B RID: 18587
		public static readonly PropertyTagPropertyDefinition InternalConversationGlobalFlagCompleteTime = PropertyTagPropertyDefinition.InternalCreate("Conversation MailboxWide FlagCompleteTime", PropTag.ConversationFlagCompleteTimeMailboxWide);

		// Token: 0x0400489C RID: 18588
		public static readonly PropertyTagPropertyDefinition InternalConversationHasAttach = PropertyTagPropertyDefinition.InternalCreate("Conversation HasAttach", PropTag.ConversationHasAttach);

		// Token: 0x0400489D RID: 18589
		public static readonly PropertyTagPropertyDefinition InternalConversationGlobalHasAttach = PropertyTagPropertyDefinition.InternalCreate("Conversation MailboxWide HasAttach", PropTag.ConversationHasAttachMailboxWide);

		// Token: 0x0400489E RID: 18590
		public static readonly PropertyTagPropertyDefinition InternalConversationHasIrm = PropertyTagPropertyDefinition.InternalCreate("Conversation HasIrm", PropTag.ConversationHasIrm);

		// Token: 0x0400489F RID: 18591
		public static readonly PropertyTagPropertyDefinition InternalConversationGlobalHasIrm = PropertyTagPropertyDefinition.InternalCreate("Conversation MailboxWide HasIrm", PropTag.ConversationHasIrmMailboxWide);

		// Token: 0x040048A0 RID: 18592
		public static readonly PropertyTagPropertyDefinition InternalConversationMessageCount = PropertyTagPropertyDefinition.InternalCreate("Conversation Message Count", PropTag.ConversationContentCount);

		// Token: 0x040048A1 RID: 18593
		public static readonly PropertyTagPropertyDefinition InternalConversationGlobalMessageCount = PropertyTagPropertyDefinition.InternalCreate("Conversation MailboxWide Message Count", PropTag.ConversationContentCountMailboxWide);

		// Token: 0x040048A2 RID: 18594
		public static readonly PropertyTagPropertyDefinition InternalConversationUnreadMessageCount = PropertyTagPropertyDefinition.InternalCreate("Conversation Unread Message Count", PropTag.ConversationContentUnread);

		// Token: 0x040048A3 RID: 18595
		public static readonly PropertyTagPropertyDefinition InternalConversationGlobalUnreadMessageCount = PropertyTagPropertyDefinition.InternalCreate("Conversation MailboxWide Unread Message Count", PropTag.ConversationContentUnreadMailboxWide);

		// Token: 0x040048A4 RID: 18596
		public static readonly PropertyTagPropertyDefinition InternalConversationMessageSize = PropertyTagPropertyDefinition.InternalCreate("Conversation MessageSize", PropTag.ConversationMessageSize);

		// Token: 0x040048A5 RID: 18597
		public static readonly PropertyTagPropertyDefinition InternalConversationGlobalMessageSize = PropertyTagPropertyDefinition.InternalCreate("Conversation MailboxWide MessageSize", PropTag.ConversationMessageSizeMailboxWide);

		// Token: 0x040048A6 RID: 18598
		public static readonly PropertyTagPropertyDefinition InternalConversationMessageClasses = PropertyTagPropertyDefinition.InternalCreate("Conversation MessageClasses", PropTag.ConversationMessageClasses);

		// Token: 0x040048A7 RID: 18599
		public static readonly PropertyTagPropertyDefinition InternalConversationGlobalMessageClasses = PropertyTagPropertyDefinition.InternalCreate("Conversation MailboxWide MessageClasses", PropTag.ConversationMessageClassesMailboxWide);

		// Token: 0x040048A8 RID: 18600
		public static readonly PropertyTagPropertyDefinition InternalConversationReplyForwardState = PropertyTagPropertyDefinition.InternalCreate("Conversation ReplyForwardState", PropTag.ConversationReplyForwardState);

		// Token: 0x040048A9 RID: 18601
		public static readonly PropertyTagPropertyDefinition InternalConversationGlobalReplyForwardState = PropertyTagPropertyDefinition.InternalCreate("Conversation MailboxWide ReplyForwardState", PropTag.ConversationReplyForwardStateMailboxWide);

		// Token: 0x040048AA RID: 18602
		public static readonly PropertyTagPropertyDefinition InternalConversationImportance = PropertyTagPropertyDefinition.InternalCreate("Conversation Importance", PropTag.ConversationImportance);

		// Token: 0x040048AB RID: 18603
		public static readonly PropertyTagPropertyDefinition InternalConversationGlobalImportance = PropertyTagPropertyDefinition.InternalCreate("Conversation MailboxWide Importance", PropTag.ConversationImportanceMailboxWide);

		// Token: 0x040048AC RID: 18604
		public static readonly PropertyTagPropertyDefinition InternalFamilyId = PropertyTagPropertyDefinition.InternalCreate("Conversation level Family id", PropTag.FamilyId);

		// Token: 0x040048AD RID: 18605
		public static readonly PropertyTagPropertyDefinition InternalConversationMVUnreadFrom = PropertyTagPropertyDefinition.InternalCreate("Conversation Unread MVFrom", PropTag.ConversationMvFromUnread);

		// Token: 0x040048AE RID: 18606
		public static readonly PropertyTagPropertyDefinition InternalConversationGlobalMVUnreadFrom = PropertyTagPropertyDefinition.InternalCreate("Conversation MailboxWide Unread MVFrom", PropTag.ConversationMvFromUnreadMailboxWide);

		// Token: 0x040048AF RID: 18607
		public static readonly PropertyTagPropertyDefinition InternalConversationLastMemberDocumentId = PropertyTagPropertyDefinition.InternalCreate("Conversation Last Member DocumentId", PropTag.ConversationLastMemberDocumentId);

		// Token: 0x040048B0 RID: 18608
		public static readonly PropertyTagPropertyDefinition InternalConversationGlobalLastMemberDocumentId = PropertyTagPropertyDefinition.InternalCreate("Conversation MailboxWide Last Member DocumentId", PropTag.ConversationLastMemberDocumentIdMailboxWide);

		// Token: 0x040048B1 RID: 18609
		public static readonly PropertyTagPropertyDefinition InternalConversationPreview = PropertyTagPropertyDefinition.InternalCreate("Conversation Preview", PropTag.ConversationPreview);

		// Token: 0x040048B2 RID: 18610
		public static readonly PropertyTagPropertyDefinition InternalConversationGlobalPreview = PropertyTagPropertyDefinition.InternalCreate("Conversation MailboxWide Preview", PropTag.ConversationPreviewMailboxWide);

		// Token: 0x040048B3 RID: 18611
		public static readonly PropertyTagPropertyDefinition InternalConversationWorkingSetSourcePartition = PropertyTagPropertyDefinition.InternalCreate("Conversation WorkingSetSourcePartition", PropTag.ConversationWorkingSetSourcePartition);

		// Token: 0x040048B4 RID: 18612
		public static readonly PropertyTagPropertyDefinition InternalConversationMVItemIds = PropertyTagPropertyDefinition.InternalCreate("Conversation ItemIds", PropTag.ConversationMvItemIds);

		// Token: 0x040048B5 RID: 18613
		public static readonly PropertyTagPropertyDefinition InternalConversationGlobalMVItemIds = PropertyTagPropertyDefinition.InternalCreate("Conversation MailboxWide ItemIds", PropTag.ConversationMvItemIdsMailboxWide);

		// Token: 0x040048B6 RID: 18614
		public static readonly PropertyTagPropertyDefinition InternalConversationGlobalRichContent = PropertyTagPropertyDefinition.InternalCreate("Conversation MailboxWide RichContent", PropTag.ConversationMessageRichContentMailboxWide);

		// Token: 0x040048B7 RID: 18615
		public static readonly PropertyTagPropertyDefinition InternalPersonCompanyName = PropertyTagPropertyDefinition.InternalCreate("Person MailboxWide CompanyName", PropTag.PersonCompanyNameMailboxWide);

		// Token: 0x040048B8 RID: 18616
		public static readonly PropertyTagPropertyDefinition InternalPersonDisplayName = PropertyTagPropertyDefinition.InternalCreate("Person MailboxWide DisplayName", PropTag.PersonDisplayNameMailboxWide);

		// Token: 0x040048B9 RID: 18617
		public static readonly PropertyTagPropertyDefinition InternalPersonGivenName = PropertyTagPropertyDefinition.InternalCreate("Person MailboxWide GivenName", PropTag.PersonGivenNameMailboxWide);

		// Token: 0x040048BA RID: 18618
		public static readonly PropertyTagPropertyDefinition InternalPersonSurname = PropertyTagPropertyDefinition.InternalCreate("Person MailboxWide Surname", PropTag.PersonSurnameMailboxWide);

		// Token: 0x040048BB RID: 18619
		public static readonly PropertyTagPropertyDefinition InternalPersonFileAs = PropertyTagPropertyDefinition.InternalCreate("Person MailboxWide FileAs", PropTag.PersonFileAsMailboxWide);

		// Token: 0x040048BC RID: 18620
		public static readonly PropertyTagPropertyDefinition InternalPersonHomeCity = PropertyTagPropertyDefinition.InternalCreate("Person MailboxWide HomeCity", PropTag.PersonHomeCityMailboxWide);

		// Token: 0x040048BD RID: 18621
		public static readonly PropertyTagPropertyDefinition InternalPersonCreationTime = PropertyTagPropertyDefinition.InternalCreate("Person MailboxWide CreationTime", PropTag.PersonCreationTimeMailboxWide);

		// Token: 0x040048BE RID: 18622
		public static readonly PropertyTagPropertyDefinition InternalPersonRelevanceScore = PropertyTagPropertyDefinition.InternalCreate("Person MailboxWide RelevanceScore", PropTag.PersonRelevanceScoreMailboxWide);

		// Token: 0x040048BF RID: 18623
		public static readonly PropertyTagPropertyDefinition InternalPersonWorkCity = PropertyTagPropertyDefinition.InternalCreate("Person MailboxWide WorkCity", PropTag.PersonWorkCityMailboxWide);

		// Token: 0x040048C0 RID: 18624
		public static readonly PropertyTagPropertyDefinition InternalPersonDisplayNameFirstLast = PropertyTagPropertyDefinition.InternalCreate("Person MailboxWide DisplayNameFirstLast", PropTag.PersonDisplayNameFirstLastMailboxWide);

		// Token: 0x040048C1 RID: 18625
		public static readonly PropertyTagPropertyDefinition InternalPersonDisplayNameLastFirst = PropertyTagPropertyDefinition.InternalCreate("Person MailboxWide DisplayNameLastFirst", PropTag.PersonDisplayNameLastFirstMailboxWide);

		// Token: 0x040048C2 RID: 18626
		public static readonly PropertyTagPropertyDefinition InternalConversationHasClutter = PropertyTagPropertyDefinition.InternalCreate("Conversation HasClutter", PropTag.ConversationHasClutter);

		// Token: 0x040048C3 RID: 18627
		public static readonly PropertyTagPropertyDefinition InternalConversationGlobalHasClutter = PropertyTagPropertyDefinition.InternalCreate("Conversation MailboxWide HasClutter", PropTag.ConversationHasClutterMailboxWide);

		// Token: 0x040048C4 RID: 18628
		public static readonly PropertyTagPropertyDefinition InternalConversationInitialMemberDocumentId = PropertyTagPropertyDefinition.InternalCreate("Conversation Initial Member DocumentId", PropTag.ConversationInitialMemberDocumentId);

		// Token: 0x040048C5 RID: 18629
		public static readonly PropertyTagPropertyDefinition InternalConversationMemberDocumentIds = PropertyTagPropertyDefinition.InternalCreate("Conversation Member DocumentIds", PropTag.ConversationMemberDocumentIds);

		// Token: 0x040048C6 RID: 18630
		public static readonly GuidIdPropertyDefinition HasWrittenTracking = InternalSchema.CreateGuidIdProperty("HasWrittenTracking", typeof(bool), WellKnownPropertySet.Tracking, 34824);

		// Token: 0x040048C7 RID: 18631
		public static readonly PropertyTagPropertyDefinition ReportTag = PropertyTagPropertyDefinition.InternalCreate("ReportTag", PropTag.ReportTag);

		// Token: 0x040048C8 RID: 18632
		public static readonly GuidIdPropertyDefinition VotingResponse = InternalSchema.CreateGuidIdProperty("VotingResponse", typeof(string), WellKnownPropertySet.Common, 34084);

		// Token: 0x040048C9 RID: 18633
		public static readonly GuidIdPropertyDefinition OutlookUserPropsVerbStream = InternalSchema.CreateGuidIdProperty("dispidVerbStream", typeof(byte[]), WellKnownPropertySet.Common, 34080);

		// Token: 0x040048CA RID: 18634
		[Obsolete("Use InternalSchema.OutlookUserPropsVerbStream instead.")]
		public static readonly GuidIdPropertyDefinition VotingBlob = InternalSchema.OutlookUserPropsVerbStream;

		// Token: 0x040048CB RID: 18635
		public static readonly GuidIdPropertyDefinition IsVotingResponse = InternalSchema.CreateGuidIdProperty("IsVotingResponse", typeof(int), WellKnownPropertySet.Common, 34074);

		// Token: 0x040048CC RID: 18636
		public static readonly PropertyTagPropertyDefinition BodyTag = PropertyTagPropertyDefinition.InternalCreate("BodyTag", PropTag.BodyTag);

		// Token: 0x040048CD RID: 18637
		public static readonly PropertyTagPropertyDefinition RuleActions = PropertyTagPropertyDefinition.InternalCreate("RuleActions", PropTag.RuleActions);

		// Token: 0x040048CE RID: 18638
		public static readonly PropertyTagPropertyDefinition RuleCondition = PropertyTagPropertyDefinition.InternalCreate("RuleCondition", PropTag.RuleCondition);

		// Token: 0x040048CF RID: 18639
		public static readonly GuidNamePropertyDefinition XSharingBrowseUrl = InternalSchema.CreateGuidNameProperty("x-sharing-browse-url", typeof(string), WellKnownPropertySet.InternetHeaders, "x-sharing-browse-url");

		// Token: 0x040048D0 RID: 18640
		public static readonly GuidNamePropertyDefinition XSharingCapabilities = InternalSchema.CreateGuidNameProperty("x-sharing-capabilities", typeof(string), WellKnownPropertySet.InternetHeaders, "x-sharing-capabilities");

		// Token: 0x040048D1 RID: 18641
		public static readonly GuidNamePropertyDefinition XSharingFlavor = InternalSchema.CreateGuidNameProperty("x-sharing-flavor", typeof(string), WellKnownPropertySet.InternetHeaders, "x-sharing-flavor");

		// Token: 0x040048D2 RID: 18642
		public static readonly GuidNamePropertyDefinition XSharingInstanceGuid = InternalSchema.CreateGuidNameProperty("x-sharing-instance-guid", typeof(string), WellKnownPropertySet.InternetHeaders, "x-sharing-instance-guid");

		// Token: 0x040048D3 RID: 18643
		public static readonly GuidNamePropertyDefinition XSharingLocalType = InternalSchema.CreateGuidNameProperty("x-sharing-local-type", typeof(string), WellKnownPropertySet.InternetHeaders, "x-sharing-local-type");

		// Token: 0x040048D4 RID: 18644
		public static readonly GuidNamePropertyDefinition XSharingProviderGuid = InternalSchema.CreateGuidNameProperty("x-sharing-provider-guid", typeof(string), WellKnownPropertySet.InternetHeaders, "x-sharing-provider-guid");

		// Token: 0x040048D5 RID: 18645
		public static readonly GuidNamePropertyDefinition XSharingProviderName = InternalSchema.CreateGuidNameProperty("x-sharing-provider-name", typeof(string), WellKnownPropertySet.InternetHeaders, "x-sharing-provider-name");

		// Token: 0x040048D6 RID: 18646
		public static readonly GuidNamePropertyDefinition XSharingProviderUrl = InternalSchema.CreateGuidNameProperty("x-sharing-provider-url", typeof(string), WellKnownPropertySet.InternetHeaders, "x-sharing-provider-url");

		// Token: 0x040048D7 RID: 18647
		public static readonly GuidNamePropertyDefinition XSharingRemoteName = InternalSchema.CreateGuidNameProperty("x-sharing-remote-name", typeof(string), WellKnownPropertySet.InternetHeaders, "x-sharing-remote-name");

		// Token: 0x040048D8 RID: 18648
		public static readonly GuidNamePropertyDefinition XSharingRemotePath = InternalSchema.CreateGuidNameProperty("x-sharing-remote-path", typeof(string), WellKnownPropertySet.InternetHeaders, "x-sharing-remote-path");

		// Token: 0x040048D9 RID: 18649
		public static readonly GuidNamePropertyDefinition XSharingRemoteType = InternalSchema.CreateGuidNameProperty("x-sharing-remote-type", typeof(string), WellKnownPropertySet.InternetHeaders, "x-sharing-remote-type");

		// Token: 0x040048DA RID: 18650
		public static readonly GuidNamePropertyDefinition XGroupMailboxSmtpAddressId = InternalSchema.CreateGuidNameProperty("X-MS-Exchange-GroupMailbox-Id", typeof(string), WellKnownPropertySet.InternetHeaders, "X-MS-Exchange-GroupMailbox-Id");

		// Token: 0x040048DB RID: 18651
		public static readonly GuidNamePropertyDefinition TextMessageDeliveryStatus = InternalSchema.CreateGuidNameProperty("TextMessageDeliveryStatus", typeof(int), WellKnownPropertySet.Messaging, "TextMessaging:DeliveryStatus");

		// Token: 0x040048DC RID: 18652
		public static readonly PropertyTagPropertyDefinition ParentFid = PropertyTagPropertyDefinition.InternalCreate("Parent FID", (PropTag)1732837396U, PropertyFlags.ReadOnly);

		// Token: 0x040048DD RID: 18653
		public static readonly PropertyTagPropertyDefinition Fid = PropertyTagPropertyDefinition.InternalCreate("FID", PropTag.Fid, PropertyFlags.ReadOnly);

		// Token: 0x040048DE RID: 18654
		public static readonly GuidNamePropertyDefinition PushNotificationFolderEntryId = InternalSchema.CreateGuidNameProperty("PushNotificationFolderEntryId", typeof(byte[]), WellKnownPropertySet.PushNotificationSubscription, "PushNotificationFolderEntryId");

		// Token: 0x040048DF RID: 18655
		public static readonly GuidNamePropertyDefinition PushNotificationSubscriptionId = InternalSchema.CreateGuidNameProperty("PushNotificationSubscriptionId", typeof(string), WellKnownPropertySet.PushNotificationSubscription, "PushNotificationSubscriptionId");

		// Token: 0x040048E0 RID: 18656
		public static readonly GuidNamePropertyDefinition PushNotificationSubscriptionLastUpdateTimeUTC = InternalSchema.CreateGuidNameProperty("PushNotificationSubscriptionLastUpdateTimeUTC", typeof(ExDateTime), WellKnownPropertySet.PushNotificationSubscription, "PushNotificationSubscriptionLastUpdateTimeUTC");

		// Token: 0x040048E1 RID: 18657
		public static readonly GuidNamePropertyDefinition PushNotificationSubscription = InternalSchema.CreateGuidNameProperty("SerializedPushNotificationSubscription", typeof(string), WellKnownPropertySet.PushNotificationSubscription, "SerializedPushNotificationSubscription");

		// Token: 0x040048E2 RID: 18658
		public static readonly GuidNamePropertyDefinition GroupNotificationsFolderEntryId = InternalSchema.CreateGuidNameProperty("GroupNotificationsFolderEntryId", typeof(byte[]), WellKnownPropertySet.GroupNotifications, "GroupNotificationsFolderEntryId");

		// Token: 0x040048E3 RID: 18659
		public static readonly GuidNamePropertyDefinition OutlookServiceFolderEntryId = InternalSchema.CreateGuidNameProperty("OutlookServiceFolderEntryId", typeof(byte[]), WellKnownPropertySet.OutlookService, "OutlookServiceFolderEntryId");

		// Token: 0x040048E4 RID: 18660
		public static readonly GuidNamePropertyDefinition OutlookServiceSubscriptionId = InternalSchema.CreateGuidNameProperty("OutlookServiceSubscriptionId", typeof(string), WellKnownPropertySet.OutlookService, "OutlookServiceSubscriptionId");

		// Token: 0x040048E5 RID: 18661
		public static readonly GuidNamePropertyDefinition OutlookServiceSubscriptionLastUpdateTimeUTC = InternalSchema.CreateGuidNameProperty("OutlookServiceSubscriptionLastUpdateTimeUTC", typeof(ExDateTime), WellKnownPropertySet.OutlookService, "OutlookServiceSubscriptionLastUpdateTimeUTC");

		// Token: 0x040048E6 RID: 18662
		public static readonly GuidNamePropertyDefinition OutlookServiceAppId = InternalSchema.CreateGuidNameProperty("OutlookServiceAppId", typeof(string), WellKnownPropertySet.OutlookService, "OutlookServiceAppId");

		// Token: 0x040048E7 RID: 18663
		public static readonly GuidNamePropertyDefinition OutlookServicePackageId = InternalSchema.CreateGuidNameProperty("OutlookServicePackageId", typeof(string), WellKnownPropertySet.OutlookService, "OutlookServicePackageId");

		// Token: 0x040048E8 RID: 18664
		public static readonly GuidNamePropertyDefinition OutlookServiceDeviceNotificationId = InternalSchema.CreateGuidNameProperty("OutlookServiceDeviceNotificationId", typeof(string), WellKnownPropertySet.OutlookService, "OutlookServiceDeviceNotificationId");

		// Token: 0x040048E9 RID: 18665
		public static readonly GuidNamePropertyDefinition OutlookServiceExpirationTime = InternalSchema.CreateGuidNameProperty("OutlookServiceExpirationTime", typeof(ExDateTime), WellKnownPropertySet.OutlookService, "OutlookServiceExpirationTime");

		// Token: 0x040048EA RID: 18666
		public static readonly GuidNamePropertyDefinition OutlookServiceLockScreen = InternalSchema.CreateGuidNameProperty("OutlookServiceLockScreen", typeof(bool), WellKnownPropertySet.OutlookService, "OutlookServiceLockScreen");

		// Token: 0x040048EB RID: 18667
		public static readonly GuidNamePropertyDefinition SnackyAppsFolderEntryId = InternalSchema.CreateGuidNameProperty("SnackyAppsFolderEntryId", typeof(byte[]), WellKnownPropertySet.Common, "SnackyAppsFolderEntryId");

		// Token: 0x040048EC RID: 18668
		public static readonly GuidNamePropertyDefinition EventTimeBasedInboxReminders = InternalSchema.CreateGuidNameProperty("EventTimeBasedInboxReminders", typeof(byte[]), WellKnownPropertySet.Reminders, "EventTimeBasedInboxReminders", PropertyFlags.Streamable, new PropertyDefinitionConstraint[0]);

		// Token: 0x040048ED RID: 18669
		public static readonly GuidNamePropertyDefinition HasExceptionalInboxReminders = InternalSchema.CreateGuidNameProperty("HasExceptionalInboxReminders", typeof(bool), WellKnownPropertySet.Reminders, "HasExceptionalInboxReminders");

		// Token: 0x040048EE RID: 18670
		public static readonly GuidNamePropertyDefinition EventTimeBasedInboxRemindersState = InternalSchema.CreateGuidNameProperty("EventTimeBasedInboxRemindersState", typeof(byte[]), WellKnownPropertySet.Reminders, "EventTimeBasedInboxRemindersState", PropertyFlags.Streamable, new PropertyDefinitionConstraint[0]);

		// Token: 0x040048EF RID: 18671
		public static readonly GuidNamePropertyDefinition ModernReminders = InternalSchema.CreateGuidNameProperty("ModernReminders", typeof(byte[]), WellKnownPropertySet.Reminders, "ModernReminders", PropertyFlags.Streamable, new PropertyDefinitionConstraint[0]);

		// Token: 0x040048F0 RID: 18672
		public static readonly GuidNamePropertyDefinition ModernRemindersState = InternalSchema.CreateGuidNameProperty("ModernRemindersState", typeof(byte[]), WellKnownPropertySet.Reminders, "ModernRemindersState", PropertyFlags.Streamable, new PropertyDefinitionConstraint[0]);

		// Token: 0x040048F1 RID: 18673
		public static readonly GuidNamePropertyDefinition ReminderId = InternalSchema.CreateGuidNameProperty("ReminderId", typeof(Guid), WellKnownPropertySet.Reminders, "ReminderId");

		// Token: 0x040048F2 RID: 18674
		public static readonly GuidNamePropertyDefinition ReminderItemGlobalObjectId = InternalSchema.CreateGuidNameProperty("ReminderItemGlobalObjectId", typeof(byte[]), WellKnownPropertySet.Reminders, "ReminderItemGlobalObjectId");

		// Token: 0x040048F3 RID: 18675
		public static readonly GuidNamePropertyDefinition ReminderOccurrenceGlobalObjectId = InternalSchema.CreateGuidNameProperty("ReminderOccurrenceGlobalObjectId", typeof(byte[]), WellKnownPropertySet.Reminders, "ReminderOccurrenceGlobalObjectId");

		// Token: 0x040048F4 RID: 18676
		public static readonly GuidNamePropertyDefinition ReminderText = InternalSchema.CreateGuidNameProperty("ReminderText", typeof(string), WellKnownPropertySet.Reminders, "ReminderText");

		// Token: 0x040048F5 RID: 18677
		public static readonly GuidNamePropertyDefinition ReminderStartTime = InternalSchema.CreateGuidNameProperty("ReminderStartTime", typeof(ExDateTime), WellKnownPropertySet.Reminders, "ReminderStartTime");

		// Token: 0x040048F6 RID: 18678
		public static readonly GuidNamePropertyDefinition ReminderEndTime = InternalSchema.CreateGuidNameProperty("ReminderEndTime", typeof(ExDateTime), WellKnownPropertySet.Reminders, "ReminderEndTime");

		// Token: 0x040048F7 RID: 18679
		public static readonly GuidNamePropertyDefinition ScheduledReminderTime = InternalSchema.CreateGuidNameProperty("ScheduledReminderTime", typeof(ExDateTime), WellKnownPropertySet.Reminders, "ScheduledReminderTime");

		// Token: 0x040048F8 RID: 18680
		public static readonly PropertyTagPropertyDefinition IsContactPhoto = PropertyTagPropertyDefinition.InternalCreate("IsContactPhoto", (PropTag)2147418123U);

		// Token: 0x040048F9 RID: 18681
		public static readonly GuidIdPropertyDefinition HasPicture = InternalSchema.CreateGuidIdProperty("HasPicture", typeof(bool), WellKnownPropertySet.Address, 32789);

		// Token: 0x040048FA RID: 18682
		public static readonly PropertyTagPropertyDefinition RelevanceScore = PropertyTagPropertyDefinition.InternalCreate("RelevanceScore", PropTag.RelevanceScore);

		// Token: 0x040048FB RID: 18683
		public static readonly PropertyTagPropertyDefinition ReportingMta = PropertyTagPropertyDefinition.InternalCreate("ReportingMta", (PropTag)1746927647U);

		// Token: 0x040048FC RID: 18684
		public static readonly GuidNamePropertyDefinition XMSJournalReport = InternalSchema.CreateGuidNameProperty("XMSJournalReport", typeof(string), WellKnownPropertySet.InternetHeaders, "X-MS-Journal-Report");

		// Token: 0x040048FD RID: 18685
		public static readonly GuidNamePropertyDefinition ApprovalAllowedDecisionMakers = InternalSchema.CreateGuidNameProperty("X-MS-Exchange-Organization-Approval-Allowed-Decision-Makers", typeof(string), WellKnownPropertySet.InternetHeaders, "X-MS-Exchange-Organization-Approval-Allowed-Decision-Makers");

		// Token: 0x040048FE RID: 18686
		public static readonly GuidNamePropertyDefinition ApprovalRequestor = InternalSchema.CreateGuidNameProperty("X-MS-Exchange-Organization-Approval-Requestor", typeof(string), WellKnownPropertySet.InternetHeaders, "X-MS-Exchange-Organization-Approval-Requestor");

		// Token: 0x040048FF RID: 18687
		public static readonly GuidNamePropertyDefinition ApprovalDecisionMaker = InternalSchema.CreateGuidNameProperty("ApprovalDecisionMaker", typeof(string), WellKnownPropertySet.Messaging, "ApprovalDecisionMaker");

		// Token: 0x04004900 RID: 18688
		public static readonly GuidNamePropertyDefinition ApprovalDecision = InternalSchema.CreateGuidNameProperty("ApprovalDecision", typeof(int), WellKnownPropertySet.Messaging, "ApprovalDecision");

		// Token: 0x04004901 RID: 18689
		public static readonly GuidNamePropertyDefinition ApprovalDecisionTime = InternalSchema.CreateGuidNameProperty("ApprovalDecisionTime", typeof(ExDateTime), WellKnownPropertySet.Messaging, "ApprovalDecisionTime");

		// Token: 0x04004902 RID: 18690
		public static readonly GuidNamePropertyDefinition ApprovalRequestMessageId = InternalSchema.CreateGuidNameProperty("ApprovalRequestMessageId", typeof(string), WellKnownPropertySet.Messaging, "ApprovalRequestMessageId");

		// Token: 0x04004903 RID: 18691
		public static readonly GuidNamePropertyDefinition ApprovalStatus = InternalSchema.CreateGuidNameProperty("ApprovalStatus", typeof(int), WellKnownPropertySet.Messaging, "ApprovalStatus");

		// Token: 0x04004904 RID: 18692
		public static readonly GuidNamePropertyDefinition ApprovalDecisionMakersNdred = InternalSchema.CreateGuidNameProperty("ApprovalDecisionMakersNdred", typeof(string), WellKnownPropertySet.Messaging, "ApprovalDecisionMakersNdred");

		// Token: 0x04004905 RID: 18693
		public static readonly GuidNamePropertyDefinition ApprovalApplicationId = InternalSchema.CreateGuidNameProperty("ApprovalApplicationId", typeof(int), WellKnownPropertySet.Messaging, "ApprovalApplicationId");

		// Token: 0x04004906 RID: 18694
		public static readonly GuidNamePropertyDefinition ApprovalApplicationData = InternalSchema.CreateGuidNameProperty("ApprovalApplicationData", typeof(string), WellKnownPropertySet.Messaging, "ApprovalApplicationData");

		// Token: 0x04004907 RID: 18695
		public static readonly GuidNamePropertyDefinition MessageBccMe = InternalSchema.CreateGuidNameProperty("MessageBccMe", typeof(bool), WellKnownPropertySet.Messaging, "MessageBccMe");

		// Token: 0x04004908 RID: 18696
		public static readonly GuidNamePropertyDefinition IsSigned = InternalSchema.CreateGuidNameProperty("IsSigned", typeof(bool), WellKnownPropertySet.Messaging, "IsSigned");

		// Token: 0x04004909 RID: 18697
		public static readonly GuidNamePropertyDefinition IsReadReceipt = InternalSchema.CreateGuidNameProperty("IsReadReceipt", typeof(bool), WellKnownPropertySet.Messaging, "IsReadReceipt");

		// Token: 0x0400490A RID: 18698
		public static readonly GuidNamePropertyDefinition XMSExchangeOutlookProtectionRuleVersion = InternalSchema.CreateGuidNameProperty("OutlookProtectionRuleVersion", typeof(string), WellKnownPropertySet.UnifiedMessaging, "X-MS-Exchange-Organization-Outlook-Protection-Rule-Addin-Version");

		// Token: 0x0400490B RID: 18699
		public static readonly GuidNamePropertyDefinition XMSExchangeOutlookProtectionRuleConfigTimestamp = InternalSchema.CreateGuidNameProperty("OutlookProtectionRuleConfigTimestamp", typeof(string), WellKnownPropertySet.UnifiedMessaging, "X-MS-Exchange-Organization-Outlook-Protection-Rule-Config-Timestamp");

		// Token: 0x0400490C RID: 18700
		public static readonly GuidNamePropertyDefinition XMSExchangeOutlookProtectionRuleOverridden = InternalSchema.CreateGuidNameProperty("OutlookProtectionRuleOverridden", typeof(string), WellKnownPropertySet.UnifiedMessaging, "X-MS-Exchange-Organization-Outlook-Protection-Rule-Overridden");

		// Token: 0x0400490D RID: 18701
		public static readonly GuidNamePropertyDefinition OscContactSources = InternalSchema.CreateGuidNameProperty("OscContactSources", typeof(string[]), WellKnownPropertySet.PublicStrings, "OscContactSources");

		// Token: 0x0400490E RID: 18702
		public static readonly GuidNamePropertyDefinition OscContactSourcesForContact = InternalSchema.CreateGuidNameProperty("OscContactSourcesForContact", typeof(byte[]), WellKnownPropertySet.PublicStrings, "OscContactSources");

		// Token: 0x0400490F RID: 18703
		public static readonly PropertyTagPropertyDefinition OscSyncEnabledOnServer = PropertyTagPropertyDefinition.InternalCreate("OscSyncEnabledOnServer", PropTag.OscSyncEnabledOnServer);

		// Token: 0x04004910 RID: 18704
		public static readonly GuidNamePropertyDefinition PeopleConnectionCreationTime = InternalSchema.CreateGuidNameProperty("PeopleConnectionCreationTime", typeof(ExDateTime), WellKnownPropertySet.PublicStrings, "PeopleConnectionCreationTime");

		// Token: 0x04004911 RID: 18705
		public static readonly GuidNamePropertyDefinition DlpSenderOverride = InternalSchema.CreateGuidNameProperty("DlpSenderOverride", typeof(string), WellKnownPropertySet.InternetHeaders, "X-Ms-Exchange-Organization-Dlp-SenderOverrideJustification");

		// Token: 0x04004912 RID: 18706
		public static readonly GuidNamePropertyDefinition DlpFalsePositive = InternalSchema.CreateGuidNameProperty("DlpFalsePositive", typeof(string), WellKnownPropertySet.InternetHeaders, "X-Ms-Exchange-Organization-Dlp-FalsePositive");

		// Token: 0x04004913 RID: 18707
		public static readonly GuidNamePropertyDefinition DlpDetectedClassifications = InternalSchema.CreateGuidNameProperty("DlpDetectedClassifications", typeof(string), WellKnownPropertySet.InternetHeaders, "X-Ms-Exchange-Organization-Dlp-DetectedClassifications");

		// Token: 0x04004914 RID: 18708
		public static readonly GuidNamePropertyDefinition DlpDetectedClassificationObjects = InternalSchema.CreateGuidNameProperty("DlpDetectedClassificationObjects", typeof(byte[]), WellKnownPropertySet.Messaging, "DlpDetectedClassificationObjects", PropertyFlags.Streamable, new PropertyDefinitionConstraint[0]);

		// Token: 0x04004915 RID: 18709
		public static readonly GuidNamePropertyDefinition HasDlpDetectedClassifications = InternalSchema.CreateGuidNameProperty("HasDlpDetectedClassifications", typeof(string), WellKnownPropertySet.Messaging, "HasDlpDetectedClassifications");

		// Token: 0x04004916 RID: 18710
		public static readonly GuidNamePropertyDefinition RecoveryOptions = InternalSchema.CreateGuidNameProperty("RecoveryOptions", typeof(int), WellKnownPropertySet.Messaging, "RecoveryOptions");

		// Token: 0x04004917 RID: 18711
		public static readonly PropertyTagPropertyDefinition RichContent = PropertyTagPropertyDefinition.InternalCreate("RichContent", PropTag.RichContent);

		// Token: 0x04004918 RID: 18712
		public static readonly GuidIdPropertyDefinition TaskResetReminder = InternalSchema.CreateGuidIdProperty("TaskResetReminder", typeof(bool), WellKnownPropertySet.Task, 33031);

		// Token: 0x04004919 RID: 18713
		public static readonly GuidIdPropertyDefinition IsOneOff = InternalSchema.CreateGuidIdProperty("IsOneOff", typeof(bool), WellKnownPropertySet.Task, 33033);

		// Token: 0x0400491A RID: 18714
		public static readonly GuidIdPropertyDefinition TaskDueDate = InternalSchema.CreateGuidIdProperty("TaskDueDate", typeof(string), WellKnownPropertySet.Common, 33029);

		// Token: 0x0400491B RID: 18715
		public static readonly GuidIdPropertyDefinition CompleteDate = InternalSchema.CreateGuidIdProperty("DateCompleted", typeof(ExDateTime), WellKnownPropertySet.Task, 33039);

		// Token: 0x0400491C RID: 18716
		public static readonly GuidIdPropertyDefinition FlagSubject = InternalSchema.CreateGuidIdProperty("FlagSubject", typeof(string), WellKnownPropertySet.Common, 34212);

		// Token: 0x0400491D RID: 18717
		public static readonly GuidIdPropertyDefinition LocalDueDate = InternalSchema.CreateGuidIdProperty("LocalDueDate", typeof(ExDateTime), WellKnownPropertySet.Task, 33029);

		// Token: 0x0400491E RID: 18718
		public static readonly GuidIdPropertyDefinition LocalStartDate = InternalSchema.CreateGuidIdProperty("LocalStartDate", typeof(ExDateTime), WellKnownPropertySet.Task, 33028);

		// Token: 0x0400491F RID: 18719
		public static readonly GuidIdPropertyDefinition UtcStartDate = InternalSchema.CreateGuidIdProperty("CommonStart", typeof(ExDateTime), WellKnownPropertySet.Common, 34070);

		// Token: 0x04004920 RID: 18720
		public static readonly GuidNamePropertyDefinition DoItTime = InternalSchema.CreateGuidNameProperty("DoItTime", typeof(ExDateTime), WellKnownPropertySet.Task, "DoItTime", PropertyFlags.None, new PropertyDefinitionConstraint[0]);

		// Token: 0x04004921 RID: 18721
		public static readonly GuidIdPropertyDefinition UtcDueDate = InternalSchema.CreateGuidIdProperty("CommonEnd", typeof(ExDateTime), WellKnownPropertySet.Common, 34071);

		// Token: 0x04004922 RID: 18722
		public static readonly GuidIdPropertyDefinition TaskStatus = InternalSchema.CreateGuidIdProperty("Status", typeof(int), WellKnownPropertySet.Task, 33025);

		// Token: 0x04004923 RID: 18723
		public static readonly GuidIdPropertyDefinition StatusDescription = InternalSchema.CreateGuidIdProperty("StatusDescription", typeof(string), WellKnownPropertySet.Task, 33079);

		// Token: 0x04004924 RID: 18724
		public static readonly GuidIdPropertyDefinition PercentComplete = InternalSchema.CreateGuidIdProperty("PercentComplete", typeof(double), WellKnownPropertySet.Task, 33026, PropertyFlags.None, new PropertyDefinitionConstraint[]
		{
			new NonMoveMailboxPropertyConstraint(new RangedValueConstraint<double>(0.0, 1.0))
		});

		// Token: 0x04004925 RID: 18725
		public static readonly GuidIdPropertyDefinition TaskRecurrence = InternalSchema.CreateGuidIdProperty("TaskRecurrence", typeof(byte[]), WellKnownPropertySet.Task, 33046);

		// Token: 0x04004926 RID: 18726
		public static readonly GuidIdPropertyDefinition MapiIsTaskRecurring = InternalSchema.CreateGuidIdProperty("MapiIsTaskRecurring", typeof(bool), WellKnownPropertySet.Task, 33062);

		// Token: 0x04004927 RID: 18727
		public static readonly GuidIdPropertyDefinition IsComplete = InternalSchema.CreateGuidIdProperty("IsComplete", typeof(bool), WellKnownPropertySet.Task, 33052);

		// Token: 0x04004928 RID: 18728
		public static readonly GuidIdPropertyDefinition TotalWork = InternalSchema.CreateGuidIdProperty("TotalWork", typeof(int), WellKnownPropertySet.Task, 33041, PropertyFlags.None, new PropertyDefinitionConstraint[]
		{
			new NonMoveMailboxPropertyConstraint(new RangedValueConstraint<int>(0, 1525252319))
		});

		// Token: 0x04004929 RID: 18729
		public static readonly GuidIdPropertyDefinition ActualWork = InternalSchema.CreateGuidIdProperty("ActualWork", typeof(int), WellKnownPropertySet.Task, 33040, PropertyFlags.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 1525252319)
		});

		// Token: 0x0400492A RID: 18730
		public static readonly GuidIdPropertyDefinition Contacts = InternalSchema.CreateGuidIdProperty("Contacts", typeof(string[]), WellKnownPropertySet.Common, 34106);

		// Token: 0x0400492B RID: 18731
		public static readonly GuidIdPropertyDefinition TaskOwner = InternalSchema.CreateGuidIdProperty("Owner", typeof(string), WellKnownPropertySet.Task, 33055);

		// Token: 0x0400492C RID: 18732
		public static readonly GuidIdPropertyDefinition LastModifiedBy = InternalSchema.CreateGuidIdProperty("LastModifiedBy", typeof(string), WellKnownPropertySet.Task, 33058);

		// Token: 0x0400492D RID: 18733
		public static readonly GuidIdPropertyDefinition TaskDelegator = InternalSchema.CreateGuidIdProperty("Delegator", typeof(string), WellKnownPropertySet.Task, 33057);

		// Token: 0x0400492E RID: 18734
		public static readonly GuidIdPropertyDefinition AssignedTime = InternalSchema.CreateGuidIdProperty("AssignedTime", typeof(ExDateTime), WellKnownPropertySet.Task, 33045);

		// Token: 0x0400492F RID: 18735
		public static readonly GuidIdPropertyDefinition OwnershipState = InternalSchema.CreateGuidIdProperty("Ownership", typeof(int), WellKnownPropertySet.Task, 33065);

		// Token: 0x04004930 RID: 18736
		public static readonly GuidIdPropertyDefinition DelegationState = InternalSchema.CreateGuidIdProperty("DelegationState", typeof(int), WellKnownPropertySet.Task, 33066);

		// Token: 0x04004931 RID: 18737
		public static readonly GuidIdPropertyDefinition IsAssignmentEditable = InternalSchema.CreateGuidIdProperty("IsAssignmentEditable", typeof(int), WellKnownPropertySet.Common, 34072);

		// Token: 0x04004932 RID: 18738
		public static readonly GuidIdPropertyDefinition TaskType = InternalSchema.CreateGuidIdProperty("TaskType", typeof(int), WellKnownPropertySet.Task, 33043);

		// Token: 0x04004933 RID: 18739
		public static readonly GuidIdPropertyDefinition IsTeamTask = InternalSchema.CreateGuidIdProperty("TeamTask", typeof(bool), WellKnownPropertySet.Task, 33027);

		// Token: 0x04004934 RID: 18740
		public static readonly GuidIdPropertyDefinition TaskChangeCount = InternalSchema.CreateGuidIdProperty("TaskChangeCount", typeof(int), WellKnownPropertySet.Task, 33042);

		// Token: 0x04004935 RID: 18741
		public static readonly GuidIdPropertyDefinition LastUpdateType = InternalSchema.CreateGuidIdProperty("LastUpdateType", typeof(int), WellKnownPropertySet.Task, 33050);

		// Token: 0x04004936 RID: 18742
		public static readonly GuidIdPropertyDefinition TaskAccepted = InternalSchema.CreateGuidIdProperty("TaskAccepted", typeof(bool), WellKnownPropertySet.Task, 33032);

		// Token: 0x04004937 RID: 18743
		public static readonly PropertyTagPropertyDefinition MapiToDoItemFlag = PropertyTagPropertyDefinition.InternalCreate("MapiToDoItemFlag", (PropTag)237699075U);

		// Token: 0x04004938 RID: 18744
		public static readonly GuidIdPropertyDefinition ToDoSubOrdinal = InternalSchema.CreateGuidIdProperty("dispidToDoSubOrdinal", typeof(string), WellKnownPropertySet.Common, 34209);

		// Token: 0x04004939 RID: 18745
		public static readonly GuidIdPropertyDefinition ToDoOrdinalDate = InternalSchema.CreateGuidIdProperty("dispidToDoOrdinalDate", typeof(ExDateTime), WellKnownPropertySet.Common, 34208);

		// Token: 0x0400493A RID: 18746
		public static readonly GuidIdPropertyDefinition FlagStringAllowed = InternalSchema.CreateGuidIdProperty("dispidAllowedFlagString", typeof(int), WellKnownPropertySet.Common, 61624);

		// Token: 0x0400493B RID: 18747
		public static readonly GuidIdPropertyDefinition ValidFlagStringProof = InternalSchema.CreateGuidIdProperty("dispidValidFlagStringProof", typeof(ExDateTime), WellKnownPropertySet.Common, 34239);

		// Token: 0x0400493C RID: 18748
		public static readonly XmlAttributePropertyDefinition CategoryListDefaultCategory = new XmlAttributePropertyDefinition("CategoryListDefaultCategory", typeof(string), "default", new PropertyDefinitionConstraint[0]);

		// Token: 0x0400493D RID: 18749
		public static readonly XmlAttributePropertyDefinition CategoryListLastSavedSession = new XmlAttributePropertyDefinition("CategoryListLastSaveSession", typeof(int), "lastSavedSession", new PropertyDefinitionConstraint[0]);

		// Token: 0x0400493E RID: 18750
		public static readonly XmlAttributePropertyDefinition CategoryListLastSavedTime = new XmlAttributePropertyDefinition("CategoryListLastSaveTime", typeof(ExDateTime), "lastSavedTime", new PropertyDefinitionConstraint[0]);

		// Token: 0x0400493F RID: 18751
		public static readonly XmlAttributePropertyDefinition CategoryAllowRenameOnFirstUse = new XmlAttributePropertyDefinition("CategoryAllowRenameOnFirstUse", typeof(bool), "renameOnFirstUse", () => false, new PropertyDefinitionConstraint[0]);

		// Token: 0x04004940 RID: 18752
		public static readonly XmlAttributePropertyDefinition CategoryName = new XmlAttributePropertyDefinition("CategoryName", typeof(string), "name", new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 255),
			new CharacterConstraint(Category.ProhibitedCharacters, false)
		});

		// Token: 0x04004941 RID: 18753
		public static readonly XmlAttributePropertyDefinition CategoryColor = new XmlAttributePropertyDefinition("CategoryColor", typeof(int), "color", () => -1, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(-1, 24)
		});

		// Token: 0x04004942 RID: 18754
		public static readonly XmlAttributePropertyDefinition CategoryKeyboardShortcut = new XmlAttributePropertyDefinition("CategoryKeyboardShortcut", typeof(int), "keyboardShortcut", () => 0, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 11)
		});

		// Token: 0x04004943 RID: 18755
		public static readonly XmlAttributePropertyDefinition CategoryLastTimeUsedNotes = new XmlAttributePropertyDefinition("CategoryLastTimeUsedNotes", typeof(ExDateTime), "lastTimeUsedNotes", new PropertyDefinitionConstraint[0]);

		// Token: 0x04004944 RID: 18756
		public static readonly XmlAttributePropertyDefinition CategoryLastTimeUsedJournal = new XmlAttributePropertyDefinition("CategoryLastTimeUsedJournal", typeof(ExDateTime), "lastTimeUsedJournal", new PropertyDefinitionConstraint[0]);

		// Token: 0x04004945 RID: 18757
		public static readonly XmlAttributePropertyDefinition CategoryLastTimeUsedContacts = new XmlAttributePropertyDefinition("CategoryLastTimeUsedContacts", typeof(ExDateTime), "lastTimeUsedContacts", new PropertyDefinitionConstraint[0]);

		// Token: 0x04004946 RID: 18758
		public static readonly XmlAttributePropertyDefinition CategoryLastTimeUsedTasks = new XmlAttributePropertyDefinition("CategoryLastTimeUsedTasks", typeof(ExDateTime), "lastTimeUsedTasks", new PropertyDefinitionConstraint[0]);

		// Token: 0x04004947 RID: 18759
		public static readonly XmlAttributePropertyDefinition CategoryLastTimeUsedCalendar = new XmlAttributePropertyDefinition("CategoryLastTimeUsedCalendar", typeof(ExDateTime), "lastTimeUsedCalendar", new PropertyDefinitionConstraint[0]);

		// Token: 0x04004948 RID: 18760
		public static readonly XmlAttributePropertyDefinition CategoryLastTimeUsedMail = new XmlAttributePropertyDefinition("CategoryLastTimeUsedMail", typeof(ExDateTime), "lastTimeUsedMail", new PropertyDefinitionConstraint[0]);

		// Token: 0x04004949 RID: 18761
		public static readonly XmlAttributePropertyDefinition CategoryLastTimeUsed = new XmlAttributePropertyDefinition("CategoryLastTimeUsed", typeof(ExDateTime), "lastTimeUsed", new PropertyDefinitionConstraint[0]);

		// Token: 0x0400494A RID: 18762
		public static readonly XmlAttributePropertyDefinition CategoryLastSessionUsed = new XmlAttributePropertyDefinition("CategoryLastSessionUsed", typeof(int), "lastSessionUsed", new PropertyDefinitionConstraint[0]);

		// Token: 0x0400494B RID: 18763
		public static readonly XmlAttributePropertyDefinition CategoryGuid = new XmlAttributePropertyDefinition("CategoryGuid", typeof(Guid), "guid", () => Guid.NewGuid(), new PropertyDefinitionConstraint[0]);

		// Token: 0x0400494C RID: 18764
		public static readonly PropertyTagPropertyDefinition OriginatorRequestedAlternateRecipientEntryId = PropertyTagPropertyDefinition.InternalCreate("OriginatorRequestedAlternateRecipientEntryId", PropTag.OriginatorRequestedAlternateRecipient);

		// Token: 0x0400494D RID: 18765
		public static readonly PropertyTagPropertyDefinition RedirectionHistory = PropertyTagPropertyDefinition.InternalCreate("RedirectionHistory", PropTag.RedirectionHistory);

		// Token: 0x0400494E RID: 18766
		public static readonly PropertyTagPropertyDefinition DlExpansionProhibited = PropertyTagPropertyDefinition.InternalCreate("DlExpansionProhibited", PropTag.DlExpansionProhibited);

		// Token: 0x0400494F RID: 18767
		public static readonly PropertyTagPropertyDefinition RecipientReassignmentProhibited = PropertyTagPropertyDefinition.InternalCreate("RecipientReassignmentProhibited", PropTag.RecipientReassignmentProhibited);

		// Token: 0x04004950 RID: 18768
		public static readonly GuidIdPropertyDefinition SharingStatus = InternalSchema.CreateGuidIdProperty("SharingStatus", typeof(int), WellKnownPropertySet.Sharing, 35328);

		// Token: 0x04004951 RID: 18769
		public static readonly GuidIdPropertyDefinition SharingProviderGuid = InternalSchema.CreateGuidIdProperty("SharingProviderGuid", typeof(Guid), WellKnownPropertySet.Sharing, 35329);

		// Token: 0x04004952 RID: 18770
		[Obsolete("Use InternalSchema.SharingProviderGuid instead.")]
		public static readonly GuidIdPropertyDefinition ProviderGuid = InternalSchema.SharingProviderGuid;

		// Token: 0x04004953 RID: 18771
		public static readonly GuidIdPropertyDefinition SharingProviderName = InternalSchema.CreateGuidIdProperty("SharingProviderName", typeof(string), WellKnownPropertySet.Sharing, 35330);

		// Token: 0x04004954 RID: 18772
		public static readonly GuidIdPropertyDefinition SharingProviderUrl = InternalSchema.CreateGuidIdProperty("SharingProviderUrl", typeof(string), WellKnownPropertySet.Sharing, 35331);

		// Token: 0x04004955 RID: 18773
		public static readonly GuidIdPropertyDefinition SharingRemotePath = InternalSchema.CreateGuidIdProperty("SharingRemotePath", typeof(string), WellKnownPropertySet.Sharing, 35332);

		// Token: 0x04004956 RID: 18774
		[Obsolete("Use InternalSchema.SharingRemotePath instead.")]
		public static readonly GuidIdPropertyDefinition SharepointFolder = InternalSchema.SharingRemotePath;

		// Token: 0x04004957 RID: 18775
		public static readonly GuidIdPropertyDefinition SharingRemoteName = InternalSchema.CreateGuidIdProperty("SharingRemoteName", typeof(string), WellKnownPropertySet.Sharing, 35333);

		// Token: 0x04004958 RID: 18776
		[Obsolete("Use InternalSchema.SharingRemoteName instead.")]
		public static readonly GuidIdPropertyDefinition SharepointFolderDisplayName = InternalSchema.SharingRemoteName;

		// Token: 0x04004959 RID: 18777
		public static readonly GuidIdPropertyDefinition SharingRemoteUid = InternalSchema.CreateGuidIdProperty("SharingRemoteUid", typeof(string), WellKnownPropertySet.Sharing, 35334);

		// Token: 0x0400495A RID: 18778
		public static readonly GuidIdPropertyDefinition SharingInitiatorName = InternalSchema.CreateGuidIdProperty("SharingInitiatorName", typeof(string), WellKnownPropertySet.Sharing, 35335);

		// Token: 0x0400495B RID: 18779
		public static readonly GuidIdPropertyDefinition SharingInitiatorSmtp = InternalSchema.CreateGuidIdProperty("SharingInitiatorSmtp", typeof(string), WellKnownPropertySet.Sharing, 35336);

		// Token: 0x0400495C RID: 18780
		public static readonly GuidIdPropertyDefinition SharingInitiatorEntryId = InternalSchema.CreateGuidIdProperty("SharingInitiatorEntryId", typeof(byte[]), WellKnownPropertySet.Sharing, 35337);

		// Token: 0x0400495D RID: 18781
		public static readonly GuidIdPropertyDefinition SharingLocalName = InternalSchema.CreateGuidIdProperty("SharingLocalName", typeof(string), WellKnownPropertySet.Sharing, 35343);

		// Token: 0x0400495E RID: 18782
		public static readonly GuidIdPropertyDefinition SharingLocalUid = InternalSchema.CreateGuidIdProperty("SharingLocalUid", typeof(string), WellKnownPropertySet.Sharing, 35344);

		// Token: 0x0400495F RID: 18783
		public static readonly GuidIdPropertyDefinition SharingRemoteUser = InternalSchema.CreateGuidIdProperty("SharingRemoteUser", typeof(string), WellKnownPropertySet.Sharing, 35340);

		// Token: 0x04004960 RID: 18784
		public static readonly GuidIdPropertyDefinition SharingRemotePass = InternalSchema.CreateGuidIdProperty("SharingRemotePass", typeof(string), WellKnownPropertySet.Sharing, 35341);

		// Token: 0x04004961 RID: 18785
		public static readonly GuidIdPropertyDefinition SharingLocalType = InternalSchema.CreateGuidIdProperty("SharingLocalType", typeof(string), WellKnownPropertySet.Sharing, 35348);

		// Token: 0x04004962 RID: 18786
		public static readonly GuidIdPropertyDefinition SharingCapabilities = InternalSchema.CreateGuidIdProperty("SharingCapabilities", typeof(int), WellKnownPropertySet.Sharing, 35351);

		// Token: 0x04004963 RID: 18787
		public static readonly GuidIdPropertyDefinition SharingFlavor = InternalSchema.CreateGuidIdProperty("SharingFlavor", typeof(int), WellKnownPropertySet.Sharing, 35352);

		// Token: 0x04004964 RID: 18788
		public static readonly GuidIdPropertyDefinition SharingPermissions = InternalSchema.CreateGuidIdProperty("SharingPermissions", typeof(int), WellKnownPropertySet.Sharing, 35355);

		// Token: 0x04004965 RID: 18789
		public static readonly GuidIdPropertyDefinition SharingInstanceGuid = InternalSchema.CreateGuidIdProperty("SharingInstanceGuid", typeof(Guid), WellKnownPropertySet.Sharing, 35356);

		// Token: 0x04004966 RID: 18790
		public static readonly GuidIdPropertyDefinition SharingRemoteType = InternalSchema.CreateGuidIdProperty("SharingRemoteType", typeof(string), WellKnownPropertySet.Sharing, 35357);

		// Token: 0x04004967 RID: 18791
		public static readonly GuidIdPropertyDefinition SharingLastSync = InternalSchema.CreateGuidIdProperty("SharingLastSync", typeof(ExDateTime), WellKnownPropertySet.Sharing, 35359);

		// Token: 0x04004968 RID: 18792
		public static readonly GuidIdPropertyDefinition SharingRssHash = InternalSchema.CreateGuidIdProperty("SharingRssHash", typeof(byte[]), WellKnownPropertySet.Sharing, 35360);

		// Token: 0x04004969 RID: 18793
		public static readonly GuidIdPropertyDefinition SharingRemoteLastMod = InternalSchema.CreateGuidIdProperty("SharingRemoteLastMod", typeof(ExDateTime), WellKnownPropertySet.Sharing, 35362);

		// Token: 0x0400496A RID: 18794
		public static readonly GuidIdPropertyDefinition SharingConfigUrl = InternalSchema.CreateGuidIdProperty("SharingConfigUrl", typeof(string), WellKnownPropertySet.Sharing, 35364);

		// Token: 0x0400496B RID: 18795
		public static readonly GuidIdPropertyDefinition SharingResponseType = InternalSchema.CreateGuidIdProperty("SharingResponseType", typeof(int), WellKnownPropertySet.Sharing, 35367);

		// Token: 0x0400496C RID: 18796
		public static readonly GuidIdPropertyDefinition SharingResponseTime = InternalSchema.CreateGuidIdProperty("SharingResponseTime", typeof(ExDateTime), WellKnownPropertySet.Sharing, 35368);

		// Token: 0x0400496D RID: 18797
		public static readonly GuidIdPropertyDefinition SharingOriginalMessageEntryId = InternalSchema.CreateGuidIdProperty("SharingOriginalMessageEntryId", typeof(byte[]), WellKnownPropertySet.Sharing, 35369);

		// Token: 0x0400496E RID: 18798
		public static readonly GuidIdPropertyDefinition SharingDetail = InternalSchema.CreateGuidIdProperty("SharingDetail", typeof(int), WellKnownPropertySet.Sharing, 35371);

		// Token: 0x0400496F RID: 18799
		public static readonly GuidIdPropertyDefinition SharingTimeToLive = InternalSchema.CreateGuidIdProperty("SharingTimeToLive", typeof(int), WellKnownPropertySet.Sharing, 35372);

		// Token: 0x04004970 RID: 18800
		public static readonly GuidIdPropertyDefinition SharingBindingEid = InternalSchema.CreateGuidIdProperty("SharingBindingEid", typeof(byte[]), WellKnownPropertySet.Sharing, 35373);

		// Token: 0x04004971 RID: 18801
		public static readonly GuidIdPropertyDefinition SharingIndexEid = InternalSchema.CreateGuidIdProperty("SharingIndexEid", typeof(byte[]), WellKnownPropertySet.Sharing, 35374);

		// Token: 0x04004972 RID: 18802
		public static readonly GuidIdPropertyDefinition SharingRemoteComment = InternalSchema.CreateGuidIdProperty("SharingRemoteComment", typeof(string), WellKnownPropertySet.Sharing, 35375);

		// Token: 0x04004973 RID: 18803
		public static readonly GuidIdPropertyDefinition SharingRemoteStoreUid = InternalSchema.CreateGuidIdProperty("SharingRemoteStoreUid", typeof(string), WellKnownPropertySet.Sharing, 35400);

		// Token: 0x04004974 RID: 18804
		public static readonly GuidIdPropertyDefinition SharingLocalStoreUid = InternalSchema.CreateGuidIdProperty("SharingLocalStoreUid", typeof(string), WellKnownPropertySet.Sharing, 35401);

		// Token: 0x04004975 RID: 18805
		public static readonly GuidIdPropertyDefinition SharingRemoteByteSize = InternalSchema.CreateGuidIdProperty("SharingRemoteByteSize", typeof(int), WellKnownPropertySet.Sharing, 35403);

		// Token: 0x04004976 RID: 18806
		public static readonly GuidIdPropertyDefinition SharingRemoteCrc = InternalSchema.CreateGuidIdProperty("SharingRemoteCrc", typeof(int), WellKnownPropertySet.Sharing, 35404);

		// Token: 0x04004977 RID: 18807
		public static readonly GuidIdPropertyDefinition SharingRoamLog = InternalSchema.CreateGuidIdProperty("SharingRoamLog", typeof(int), WellKnownPropertySet.Sharing, 35406);

		// Token: 0x04004978 RID: 18808
		public static readonly GuidIdPropertyDefinition SharingBrowseUrl = InternalSchema.CreateGuidIdProperty("SharingBrowseUrl", typeof(string), WellKnownPropertySet.Sharing, 35409);

		// Token: 0x04004979 RID: 18809
		public static readonly GuidIdPropertyDefinition SharingLastAutoSync = InternalSchema.CreateGuidIdProperty("SharingLastAutoSync", typeof(ExDateTime), WellKnownPropertySet.Sharing, 35413);

		// Token: 0x0400497A RID: 18810
		public static readonly GuidIdPropertyDefinition SharingSavedSession = InternalSchema.CreateGuidIdProperty("SharingSavedSession", typeof(Guid), WellKnownPropertySet.Sharing, 35422);

		// Token: 0x0400497B RID: 18811
		public static readonly GuidIdPropertyDefinition SharingRemoteFolderId = InternalSchema.CreateGuidIdProperty("SharingRemoteFolderId", typeof(string), WellKnownPropertySet.Sharing, 35429);

		// Token: 0x0400497C RID: 18812
		public static readonly GuidIdPropertyDefinition SharingLocalFolderEwsId = InternalSchema.CreateGuidIdProperty("SharingLocalFolderEwsId", typeof(string), WellKnownPropertySet.Sharing, 35430);

		// Token: 0x0400497D RID: 18813
		public static readonly GuidNamePropertyDefinition SharingLastSubscribeTime = InternalSchema.CreateGuidNameProperty("SharingLastSubscribeTime", typeof(ExDateTime), WellKnownPropertySet.Sharing, "SharingLastSubscribeTime");

		// Token: 0x0400497E RID: 18814
		public static readonly GuidIdPropertyDefinition SharingDetailedStatus = InternalSchema.CreateGuidIdProperty("SharingDetailedStatus", typeof(int), WellKnownPropertySet.Sharing, 35488);

		// Token: 0x0400497F RID: 18815
		public static readonly GuidIdPropertyDefinition SharingLastSuccessSyncTime = InternalSchema.CreateGuidIdProperty("SharingLastSuccessSyncTime", typeof(ExDateTime), WellKnownPropertySet.Sharing, 35492);

		// Token: 0x04004980 RID: 18816
		public static readonly GuidIdPropertyDefinition SharingSyncRange = InternalSchema.CreateGuidIdProperty("SharingSyncRange", typeof(int), WellKnownPropertySet.Sharing, 35493);

		// Token: 0x04004981 RID: 18817
		public static readonly GuidIdPropertyDefinition SharingAggregationStatus = InternalSchema.CreateGuidIdProperty("SharingAggregationStatus", typeof(int), WellKnownPropertySet.Sharing, 35494);

		// Token: 0x04004982 RID: 18818
		public static readonly GuidIdPropertyDefinition SharingWlidAuthPolicy = InternalSchema.CreateGuidIdProperty("SharingWlidAuthPolicy", typeof(string), WellKnownPropertySet.Sharing, 35504);

		// Token: 0x04004983 RID: 18819
		public static readonly GuidIdPropertyDefinition SharingWlidUserPuid = InternalSchema.CreateGuidIdProperty("SharingWlidUserPuid", typeof(string), WellKnownPropertySet.Sharing, 35505);

		// Token: 0x04004984 RID: 18820
		public static readonly GuidIdPropertyDefinition SharingWlidAuthToken = InternalSchema.CreateGuidIdProperty("SharingWlidAuthToken", typeof(string), WellKnownPropertySet.Sharing, 35506);

		// Token: 0x04004985 RID: 18821
		public static readonly GuidIdPropertyDefinition SharingWlidAuthTokenExpireTime = InternalSchema.CreateGuidIdProperty("SharingWlidAuthTokenExpireTime", typeof(ExDateTime), WellKnownPropertySet.Sharing, 35507);

		// Token: 0x04004986 RID: 18822
		public static readonly GuidIdPropertyDefinition SharingMinSyncPollInterval = InternalSchema.CreateGuidIdProperty("SharingMinSyncPollInterval", typeof(int), WellKnownPropertySet.Sharing, 35520);

		// Token: 0x04004987 RID: 18823
		public static readonly GuidIdPropertyDefinition SharingMinSettingPollInterval = InternalSchema.CreateGuidIdProperty("SharingMinSettingPollInterval", typeof(int), WellKnownPropertySet.Sharing, 35521);

		// Token: 0x04004988 RID: 18824
		public static readonly GuidIdPropertyDefinition SharingSyncMultiplier = InternalSchema.CreateGuidIdProperty("SharingSyncMultiplier", typeof(double), WellKnownPropertySet.Sharing, 35522);

		// Token: 0x04004989 RID: 18825
		public static readonly GuidIdPropertyDefinition SharingMaxObjectsInSync = InternalSchema.CreateGuidIdProperty("SharingMaxObjectsInSync", typeof(int), WellKnownPropertySet.Sharing, 35523);

		// Token: 0x0400498A RID: 18826
		public static readonly GuidIdPropertyDefinition SharingMaxNumberOfEmails = InternalSchema.CreateGuidIdProperty("SharingMaxNumberOfEmails", typeof(int), WellKnownPropertySet.Sharing, 35524);

		// Token: 0x0400498B RID: 18827
		public static readonly GuidIdPropertyDefinition SharingMaxNumberOfFolders = InternalSchema.CreateGuidIdProperty("SharingMaxNumberOfFolders", typeof(int), WellKnownPropertySet.Sharing, 35525);

		// Token: 0x0400498C RID: 18828
		public static readonly GuidIdPropertyDefinition SharingMaxAttachments = InternalSchema.CreateGuidIdProperty("SharingMaxAttachments", typeof(int), WellKnownPropertySet.Sharing, 35526);

		// Token: 0x0400498D RID: 18829
		public static readonly GuidIdPropertyDefinition SharingMaxMessageSize = InternalSchema.CreateGuidIdProperty("SharingMaxMessageSize", typeof(long), WellKnownPropertySet.Sharing, 35527);

		// Token: 0x0400498E RID: 18830
		public static readonly GuidIdPropertyDefinition SharingMaxRecipients = InternalSchema.CreateGuidIdProperty("SharingMaxRecipients", typeof(int), WellKnownPropertySet.Sharing, 35528);

		// Token: 0x0400498F RID: 18831
		public static readonly GuidIdPropertyDefinition SharingMigrationState = InternalSchema.CreateGuidIdProperty("SharingMigrationState", typeof(int), WellKnownPropertySet.Sharing, 35529);

		// Token: 0x04004990 RID: 18832
		public static readonly GuidIdPropertyDefinition SharingDiagnostics = InternalSchema.CreateGuidIdProperty("SharingDiagnostics", typeof(string), WellKnownPropertySet.Sharing, 35530);

		// Token: 0x04004991 RID: 18833
		public static readonly GuidIdPropertyDefinition SharingPoisonCallstack = InternalSchema.CreateGuidIdProperty("SharingPoisonCallstack", typeof(string), WellKnownPropertySet.Sharing, 35531);

		// Token: 0x04004992 RID: 18834
		public static readonly GuidIdPropertyDefinition SharingAggregationType = InternalSchema.CreateGuidIdProperty("SharingAggregationType", typeof(int), WellKnownPropertySet.Sharing, 35532);

		// Token: 0x04004993 RID: 18835
		public static readonly GuidIdPropertyDefinition SharingSubscriptionConfiguration = InternalSchema.CreateGuidIdProperty("SharingSubscriptionConfiguration", typeof(int), WellKnownPropertySet.Sharing, 35584);

		// Token: 0x04004994 RID: 18836
		public static readonly GuidIdPropertyDefinition SharingAggregationProtocolVersion = InternalSchema.CreateGuidIdProperty("SharingSharingAggregationProtocolVersion", typeof(int), WellKnownPropertySet.Sharing, 35585);

		// Token: 0x04004995 RID: 18837
		public static readonly GuidIdPropertyDefinition SharingAggregationProtocolName = InternalSchema.CreateGuidIdProperty("SharingAggregationProtocolName", typeof(string), WellKnownPropertySet.Sharing, 35586);

		// Token: 0x04004996 RID: 18838
		public static readonly GuidIdPropertyDefinition SharingSubscriptionName = InternalSchema.CreateGuidIdProperty("SharingSubscriptionName", typeof(string), WellKnownPropertySet.Sharing, 35587);

		// Token: 0x04004997 RID: 18839
		public static readonly GuidNamePropertyDefinition SharingSubscriptionVersion = InternalSchema.CreateGuidNameProperty("SharingSubscriptionVersion", typeof(long), WellKnownPropertySet.Sharing, "SharingSubscriptionVersion");

		// Token: 0x04004998 RID: 18840
		public static readonly GuidIdPropertyDefinition SharingSubscriptionsCache = InternalSchema.CreateGuidIdProperty("SharingSubscriptions", typeof(byte[]), WellKnownPropertySet.Sharing, 35840, PropertyFlags.Streamable, new PropertyDefinitionConstraint[0]);

		// Token: 0x04004999 RID: 18841
		public static readonly GuidIdPropertyDefinition SharingAdjustedLastSuccessfulSyncTime = InternalSchema.CreateGuidIdProperty("SharingAdjustedLastSuccessfulSyncTime", typeof(ExDateTime), WellKnownPropertySet.Sharing, 35841);

		// Token: 0x0400499A RID: 18842
		public static readonly GuidIdPropertyDefinition SharingOutageDetectionDiagnostics = InternalSchema.CreateGuidIdProperty("SharingOutageDetectionDiagnostics", typeof(string), WellKnownPropertySet.Sharing, 35842);

		// Token: 0x0400499B RID: 18843
		public static readonly GuidNamePropertyDefinition SharingSendAsState = InternalSchema.CreateGuidNameProperty("SharingSendAsState", typeof(int), WellKnownPropertySet.Sharing, "SharingSendAsState");

		// Token: 0x0400499C RID: 18844
		public static readonly GuidNamePropertyDefinition SharingSendAsValidatedEmail = InternalSchema.CreateGuidNameProperty("SharingSendAsValidatedEmail", typeof(string), WellKnownPropertySet.Sharing, "SharingSendAsValidatedEmail");

		// Token: 0x0400499D RID: 18845
		public static readonly GuidNamePropertyDefinition SharingSendAsSubmissionUrl = InternalSchema.CreateGuidNameProperty("SharingSendAsSubmissionUrl", typeof(string), WellKnownPropertySet.Sharing, "SharingSendAsSubmissionUrl");

		// Token: 0x0400499E RID: 18846
		public static readonly GuidNamePropertyDefinition SharingEwsUri = InternalSchema.CreateGuidNameProperty("SharingEwsUri", typeof(string), WellKnownPropertySet.Sharing, "SharingEwsUri");

		// Token: 0x0400499F RID: 18847
		public static readonly GuidNamePropertyDefinition SharingRemoteExchangeVersion = InternalSchema.CreateGuidNameProperty("SharingRemoteExchangeVersion", typeof(string), WellKnownPropertySet.Sharing, "SharingRemoteExchangeVersion");

		// Token: 0x040049A0 RID: 18848
		public static readonly GuidNamePropertyDefinition SharingSubscriptionCreationType = InternalSchema.CreateGuidNameProperty("SharingSubscriptionCreationType", typeof(int), WellKnownPropertySet.Sharing, "SharingSubscriptionCreationType");

		// Token: 0x040049A1 RID: 18849
		public static readonly GuidNamePropertyDefinition SharingSubscriptionSyncPhase = InternalSchema.CreateGuidNameProperty("SharingSubscriptionSyncPhase", typeof(int), WellKnownPropertySet.Sharing, "SharingSubscriptionSyncPhase");

		// Token: 0x040049A2 RID: 18850
		public static readonly GuidNamePropertyDefinition SharingSendAsVerificationEmailState = InternalSchema.CreateGuidNameProperty("SharingSendAsVerificationEmailState", typeof(int), WellKnownPropertySet.Sharing, "SharingSendAsVerificationEmailState");

		// Token: 0x040049A3 RID: 18851
		public static readonly GuidNamePropertyDefinition SharingSendAsVerificationMessageId = InternalSchema.CreateGuidNameProperty("SharingSendAsVerificationMessageId", typeof(string), WellKnownPropertySet.Sharing, "SharingSendAsVerificationMessageId");

		// Token: 0x040049A4 RID: 18852
		public static readonly GuidNamePropertyDefinition SharingSendAsVerificationTimestamp = InternalSchema.CreateGuidNameProperty("SharingSendAsVerificationTimestamp", typeof(ExDateTime), WellKnownPropertySet.Sharing, "SharingSendAsVerificationTimestamp");

		// Token: 0x040049A5 RID: 18853
		public static readonly GuidNamePropertyDefinition SharingSubscriptionEvents = InternalSchema.CreateGuidNameProperty("SharingSubscriptionEvents", typeof(int), WellKnownPropertySet.Sharing, "SharingSubscriptionEvents");

		// Token: 0x040049A6 RID: 18854
		public static readonly GuidNamePropertyDefinition SharingSubscriptionItemsSynced = InternalSchema.CreateGuidNameProperty("SharingSubscriptionItemsSynced", typeof(long), WellKnownPropertySet.Sharing, "SharingSubscriptionItemsSynced");

		// Token: 0x040049A7 RID: 18855
		public static readonly GuidNamePropertyDefinition SharingSubscriptionItemsSkipped = InternalSchema.CreateGuidNameProperty("SharingSubscriptionItemsSkipped", typeof(long), WellKnownPropertySet.Sharing, "SharingSubscriptionItemsSkipped");

		// Token: 0x040049A8 RID: 18856
		public static readonly GuidNamePropertyDefinition SharingSubscriptionTotalItemsInSourceMailbox = InternalSchema.CreateGuidNameProperty("SharingSubscriptionTotalItemsInSourceMailbox", typeof(long), WellKnownPropertySet.Sharing, "SharingSubscriptionTotalItemsInSourceMailbox");

		// Token: 0x040049A9 RID: 18857
		public static readonly GuidNamePropertyDefinition SharingSubscriptionTotalSizeOfSourceMailbox = InternalSchema.CreateGuidNameProperty("SharingSubscriptionTotalSizeOfSourceMailbox", typeof(long), WellKnownPropertySet.Sharing, "SharingSubscriptionTotalSizeOfSourceMailbox");

		// Token: 0x040049AA RID: 18858
		public static readonly GuidNamePropertyDefinition SharingImapPathPrefix = InternalSchema.CreateGuidNameProperty("SharingImapPathPrefix", typeof(string), WellKnownPropertySet.Sharing, "SharingImapPathPrefix");

		// Token: 0x040049AB RID: 18859
		public static readonly GuidNamePropertyDefinition SharingRemoteUserDomain = InternalSchema.CreateGuidNameProperty("SharingRemoteUserDomain", typeof(string), WellKnownPropertySet.Sharing, "SharingRemoteUserDomain");

		// Token: 0x040049AC RID: 18860
		public static readonly GuidNamePropertyDefinition SharingSubscriptionExclusionFolders = InternalSchema.CreateGuidNameProperty("SharingSubscriptionExclusionFolders", typeof(string), WellKnownPropertySet.Sharing, "SharingSubscriptionExclusionFolders");

		// Token: 0x040049AD RID: 18861
		public static readonly GuidNamePropertyDefinition SharingLastSyncNowRequest = InternalSchema.CreateGuidNameProperty("SharingLastSyncNowRequest", typeof(ExDateTime), WellKnownPropertySet.Sharing, "SharingLastSyncNowRequest");

		// Token: 0x040049AE RID: 18862
		public static readonly PropertyTagPropertyDefinition RssServerLockStartTime = PropertyTagPropertyDefinition.InternalCreate("RssServerLockStartTime", (PropTag)1610612739U);

		// Token: 0x040049AF RID: 18863
		public static readonly PropertyTagPropertyDefinition RssServerLockTimeout = PropertyTagPropertyDefinition.InternalCreate("RssServerLockTimeout", (PropTag)1610678275U);

		// Token: 0x040049B0 RID: 18864
		public static readonly PropertyTagPropertyDefinition RssServerLockClientName = PropertyTagPropertyDefinition.InternalCreate("RssServerLockClientName", (PropTag)1610743839U);

		// Token: 0x040049B1 RID: 18865
		public static readonly GuidNamePropertyDefinition SharingEncryptedAccessToken = InternalSchema.CreateGuidNameProperty("SharingEncryptedAccessToken", typeof(string), WellKnownPropertySet.Sharing, "SharingEncryptedAccessToken");

		// Token: 0x040049B2 RID: 18866
		public static readonly GuidNamePropertyDefinition SharingConnectState = InternalSchema.CreateGuidNameProperty("SharingConnectState", typeof(int), WellKnownPropertySet.Sharing, "SharingConnectState");

		// Token: 0x040049B3 RID: 18867
		public static readonly GuidNamePropertyDefinition SharingAppId = InternalSchema.CreateGuidNameProperty("SharingAppId", typeof(string), WellKnownPropertySet.Sharing, "SharingAppId");

		// Token: 0x040049B4 RID: 18868
		public static readonly GuidNamePropertyDefinition SharingUserId = InternalSchema.CreateGuidNameProperty("SharingUserId", typeof(string), WellKnownPropertySet.Sharing, "SharingUserId");

		// Token: 0x040049B5 RID: 18869
		public static readonly GuidNamePropertyDefinition SharingEncryptedAccessTokenSecret = InternalSchema.CreateGuidNameProperty("SharingEncryptedAccessTokenSecret", typeof(string), WellKnownPropertySet.Sharing, "SharingEncryptedAccessTokenSecret");

		// Token: 0x040049B6 RID: 18870
		public static readonly GuidNamePropertyDefinition SharingInitialSyncInRecoveryMode = InternalSchema.CreateGuidNameProperty("SharingInitialSyncInRecoveryMode", typeof(bool), WellKnownPropertySet.Sharing, "SharingInitialSyncInRecoveryMode");

		// Token: 0x040049B7 RID: 18871
		public static readonly GuidNamePropertyDefinition CloudId = InternalSchema.CreateGuidNameProperty("CloudId", typeof(string), WellKnownPropertySet.Messaging, "CloudId");

		// Token: 0x040049B8 RID: 18872
		public static readonly GuidNamePropertyDefinition CloudVersion = InternalSchema.CreateGuidNameProperty("CloudVersion", typeof(string), WellKnownPropertySet.Messaging, "CloudVersion");

		// Token: 0x040049B9 RID: 18873
		public static readonly GuidNamePropertyDefinition AggregationSyncProgress = InternalSchema.CreateGuidNameProperty("AggregationSyncProgress", typeof(int), WellKnownPropertySet.Messaging, "AggregationSyncProgress");

		// Token: 0x040049BA RID: 18874
		public static readonly GuidIdPropertyDefinition MigrationVersion = InternalSchema.CreateGuidIdProperty("MigrationVersion", typeof(long), WellKnownPropertySet.Sharing, 35600);

		// Token: 0x040049BB RID: 18875
		public static readonly GuidIdPropertyDefinition MigrationJobId = InternalSchema.CreateGuidIdProperty("MigrationJobId", typeof(Guid), WellKnownPropertySet.Sharing, 35601);

		// Token: 0x040049BC RID: 18876
		public static readonly GuidIdPropertyDefinition MigrationJobName = InternalSchema.CreateGuidIdProperty("MigrationJobName", typeof(string), WellKnownPropertySet.Sharing, 35602);

		// Token: 0x040049BD RID: 18877
		public static readonly GuidIdPropertyDefinition MigrationJobSubmittedBy = InternalSchema.CreateGuidIdProperty("MigrationJobSubmittedBy", typeof(string), WellKnownPropertySet.Sharing, 35603);

		// Token: 0x040049BE RID: 18878
		public static readonly GuidIdPropertyDefinition MigrationJobItemStatus = InternalSchema.CreateGuidIdProperty("MigrationJobItemStatus", typeof(int), WellKnownPropertySet.Sharing, 35604);

		// Token: 0x040049BF RID: 18879
		public static readonly GuidIdPropertyDefinition MigrationJobTotalRowCount = InternalSchema.CreateGuidIdProperty("MigrationJobTotalItemCount", typeof(int), WellKnownPropertySet.Sharing, 35605);

		// Token: 0x040049C0 RID: 18880
		public static readonly GuidIdPropertyDefinition MigrationJobExcludedFolders = InternalSchema.CreateGuidIdProperty("MigrationJobExcludedFolders", typeof(string), WellKnownPropertySet.Sharing, 35606);

		// Token: 0x040049C1 RID: 18881
		public static readonly GuidIdPropertyDefinition MigrationJobNotificationEmails = InternalSchema.CreateGuidIdProperty("MigrationJobNotificationEmails", typeof(string), WellKnownPropertySet.Sharing, 35607);

		// Token: 0x040049C2 RID: 18882
		public static readonly GuidIdPropertyDefinition MigrationJobStartTime = InternalSchema.CreateGuidIdProperty("MigrationJobStartTime", typeof(ExDateTime), WellKnownPropertySet.Sharing, 35609);

		// Token: 0x040049C3 RID: 18883
		public static readonly GuidIdPropertyDefinition MigrationJobUserTimeZone = InternalSchema.CreateGuidIdProperty("MigrationJobUserTimeZone", typeof(string), WellKnownPropertySet.Sharing, 35610);

		// Token: 0x040049C4 RID: 18884
		public static readonly GuidIdPropertyDefinition MigrationJobCancelledFlag = InternalSchema.CreateGuidIdProperty("MigrationJobCancelledFlag", typeof(bool), WellKnownPropertySet.Sharing, 35611);

		// Token: 0x040049C5 RID: 18885
		public static readonly GuidIdPropertyDefinition MigrationJobItemIdentifier = InternalSchema.CreateGuidIdProperty("MigrationJobItemEmailAddress", typeof(string), WellKnownPropertySet.Sharing, 35612);

		// Token: 0x040049C6 RID: 18886
		public static readonly GuidIdPropertyDefinition MigrationJobItemEncryptedIncomingPassword = InternalSchema.CreateGuidIdProperty("MigrationJobItemEncryptedIncomingPassword", typeof(string), WellKnownPropertySet.Sharing, 35613);

		// Token: 0x040049C7 RID: 18887
		public static readonly GuidIdPropertyDefinition MigrationJobItemIncomingUsername = InternalSchema.CreateGuidIdProperty("MigrationJobItemIncomingUsername", typeof(string), WellKnownPropertySet.Sharing, 35614);

		// Token: 0x040049C8 RID: 18888
		public static readonly GuidIdPropertyDefinition MigrationJobItemSubscriptionMessageId = InternalSchema.CreateGuidIdProperty("MigrationJobItemSubscriptionMessageId", typeof(byte[]), WellKnownPropertySet.Sharing, 35615);

		// Token: 0x040049C9 RID: 18889
		public static readonly GuidIdPropertyDefinition MigrationJobItemMailboxServer = InternalSchema.CreateGuidIdProperty("MigrationJobItemMailboxServer", typeof(string), WellKnownPropertySet.Sharing, 35616);

		// Token: 0x040049CA RID: 18890
		public static readonly GuidIdPropertyDefinition MigrationJobItemMailboxId = InternalSchema.CreateGuidIdProperty("MigrationJobItemMailboxId", typeof(Guid), WellKnownPropertySet.Sharing, 35617);

		// Token: 0x040049CB RID: 18891
		public static readonly GuidIdPropertyDefinition MigrationJobItemStateLastUpdated = InternalSchema.CreateGuidIdProperty("MigrationJobItemStateLastUpdated", typeof(ExDateTime), WellKnownPropertySet.Sharing, 35618);

		// Token: 0x040049CC RID: 18892
		public static readonly GuidIdPropertyDefinition MigrationJobItemMailboxLegacyDN = InternalSchema.CreateGuidIdProperty("MigrationJobItemMailboxLegacyDN", typeof(string), WellKnownPropertySet.Sharing, 35619);

		// Token: 0x040049CD RID: 18893
		public static readonly GuidIdPropertyDefinition MigrationJobRemoteServerHostName = InternalSchema.CreateGuidIdProperty("MigrationJobRemoteServerHostName", typeof(string), WellKnownPropertySet.Sharing, 35620);

		// Token: 0x040049CE RID: 18894
		public static readonly GuidIdPropertyDefinition MigrationJobRemoteServerPortNumber = InternalSchema.CreateGuidIdProperty("MigrationJobRemoteServerPortNumber", typeof(int), WellKnownPropertySet.Sharing, 35621);

		// Token: 0x040049CF RID: 18895
		public static readonly GuidIdPropertyDefinition MigrationJobRemoteServerAuth = InternalSchema.CreateGuidIdProperty("MigrationJobRemoteServerAuth", typeof(int), WellKnownPropertySet.Sharing, 35622);

		// Token: 0x040049D0 RID: 18896
		public static readonly GuidIdPropertyDefinition MigrationJobRemoteServerSecurity = InternalSchema.CreateGuidIdProperty("MigrationJobRemoteServerSecurity", typeof(int), WellKnownPropertySet.Sharing, 35623);

		// Token: 0x040049D1 RID: 18897
		public static readonly GuidIdPropertyDefinition MigrationJobMaxConcurrentMigrations = InternalSchema.CreateGuidIdProperty("MigrationJobMaxConcurrentMigrations", typeof(int), WellKnownPropertySet.Sharing, 35624);

		// Token: 0x040049D2 RID: 18898
		public static readonly GuidIdPropertyDefinition MigrationCacheEntryMailboxLegacyDN = InternalSchema.CreateGuidIdProperty("MigrationCacheEntryMailboxLegacyDN", typeof(string), WellKnownPropertySet.Sharing, 35625);

		// Token: 0x040049D3 RID: 18899
		public static readonly GuidIdPropertyDefinition MigrationCacheEntryTenantPartitionHint = InternalSchema.CreateGuidIdProperty("MigrationCacheEntryTenantPartitionHint", typeof(byte[]), WellKnownPropertySet.Sharing, 35772);

		// Token: 0x040049D4 RID: 18900
		public static readonly GuidIdPropertyDefinition MigrationJobItemRowIndex = InternalSchema.CreateGuidIdProperty("MigrationJobItemRowIndex", typeof(int), WellKnownPropertySet.Sharing, 35627);

		// Token: 0x040049D5 RID: 18901
		public static readonly GuidIdPropertyDefinition MigrationJobItemLocalizedError = InternalSchema.CreateGuidIdProperty("MigrationJobItemLocalizedError", typeof(string), WellKnownPropertySet.Sharing, 35628);

		// Token: 0x040049D6 RID: 18902
		public static readonly GuidIdPropertyDefinition MigrationJobItemSubscriptionCreated = InternalSchema.CreateGuidIdProperty("MigrationJobItemSubscriptionCreated", typeof(ExDateTime), WellKnownPropertySet.Sharing, 35629);

		// Token: 0x040049D7 RID: 18903
		public static readonly GuidIdPropertyDefinition MigrationJobItemSubscriptionLastChecked = InternalSchema.CreateGuidIdProperty("MigrationJobItemSubscriptionStateLastChecked", typeof(ExDateTime), WellKnownPropertySet.Sharing, 35630);

		// Token: 0x040049D8 RID: 18904
		public static readonly GuidIdPropertyDefinition MigrationJobInternalError = InternalSchema.CreateGuidIdProperty("MigrationJobInternalError", typeof(string), WellKnownPropertySet.Sharing, 35631);

		// Token: 0x040049D9 RID: 18905
		public static readonly GuidIdPropertyDefinition MigrationCacheEntryLastUpdated = InternalSchema.CreateGuidIdProperty("MigrationCacheEntryLastUpdated", typeof(ExDateTime), WellKnownPropertySet.Sharing, 35632);

		// Token: 0x040049DA RID: 18906
		public static readonly GuidIdPropertyDefinition MigrationJobOriginalCreationTime = InternalSchema.CreateGuidIdProperty("MigrationJobOriginalCreationTime", typeof(ExDateTime), WellKnownPropertySet.Sharing, 35633);

		// Token: 0x040049DB RID: 18907
		public static readonly GuidIdPropertyDefinition MigrationJobAdminCulture = InternalSchema.CreateGuidIdProperty("MigrationJobAdminCulture", typeof(string), WellKnownPropertySet.Sharing, 35634);

		// Token: 0x040049DC RID: 18908
		public static readonly GuidIdPropertyDefinition MigrationUserRootFolder = InternalSchema.CreateGuidIdProperty("MigrationUserRootFolder", typeof(string), WellKnownPropertySet.Sharing, 35635);

		// Token: 0x040049DD RID: 18909
		public static readonly GuidIdPropertyDefinition MigrationType = InternalSchema.CreateGuidIdProperty("MigrationType", typeof(int), WellKnownPropertySet.Sharing, 35636);

		// Token: 0x040049DE RID: 18910
		public static readonly GuidIdPropertyDefinition MigrationJobWindowsLiveNetId = InternalSchema.CreateGuidIdProperty("MigrationJobWindowsLiveNetId", typeof(string), WellKnownPropertySet.Sharing, 35637);

		// Token: 0x040049DF RID: 18911
		public static readonly GuidIdPropertyDefinition MigrationJobCursorPosition = InternalSchema.CreateGuidIdProperty("MigrationJobCursorPosition", typeof(string), WellKnownPropertySet.Sharing, 35638);

		// Token: 0x040049E0 RID: 18912
		public static readonly GuidIdPropertyDefinition MigrationJobItemWLSASigned = InternalSchema.CreateGuidIdProperty("MigrationJobItemWLSASigned", typeof(bool), WellKnownPropertySet.Sharing, 35639);

		// Token: 0x040049E1 RID: 18913
		public static readonly GuidIdPropertyDefinition MigrationHotmailSubscriptionStatus = InternalSchema.CreateGuidIdProperty("MigrationHotmailSubscriptionStatus", typeof(int), WellKnownPropertySet.Sharing, 35641);

		// Token: 0x040049E2 RID: 18914
		public static readonly GuidIdPropertyDefinition MigrationHotmailSubscriptionMessageId = InternalSchema.CreateGuidIdProperty("MigrationHotmailSubscriptionMessageId", typeof(byte[]), WellKnownPropertySet.Sharing, 35642);

		// Token: 0x040049E3 RID: 18915
		public static readonly GuidIdPropertyDefinition MigrationJobHasAdminPrivilege = InternalSchema.CreateGuidIdProperty("MigrationJobHasAdminPrivilege", typeof(bool), WellKnownPropertySet.Sharing, 35644);

		// Token: 0x040049E4 RID: 18916
		public static readonly GuidIdPropertyDefinition MigrationJobHasAutodiscovery = InternalSchema.CreateGuidIdProperty("MigrationJobHasAutodiscovery", typeof(bool), WellKnownPropertySet.Sharing, 35645);

		// Token: 0x040049E5 RID: 18917
		public static readonly GuidIdPropertyDefinition MigrationJobRemoteAutodiscoverUrl = InternalSchema.CreateGuidIdProperty("MigrationJobRemoteAutodiscoverUrl", typeof(string), WellKnownPropertySet.Sharing, 35646);

		// Token: 0x040049E6 RID: 18918
		public static readonly GuidIdPropertyDefinition MigrationJobEmailAddress = InternalSchema.CreateGuidIdProperty("MigrationJobEmailAddress", typeof(string), WellKnownPropertySet.Sharing, 35647);

		// Token: 0x040049E7 RID: 18919
		public static readonly GuidIdPropertyDefinition MigrationJobProxyServerHostName = InternalSchema.CreateGuidIdProperty("MigrationJobProxyServerHostName", typeof(string), WellKnownPropertySet.Sharing, 35648);

		// Token: 0x040049E8 RID: 18920
		public static readonly GuidIdPropertyDefinition MigrationJobExchangeRemoteServerHostName = InternalSchema.CreateGuidIdProperty("MigrationJobExchangeRemoteServerHostName", typeof(string), WellKnownPropertySet.Sharing, 35649);

		// Token: 0x040049E9 RID: 18921
		public static readonly GuidIdPropertyDefinition MigrationJobRemoteNSPIServerHostName = InternalSchema.CreateGuidIdProperty("MigrationJobRemoteNSPIServerHostName", typeof(string), WellKnownPropertySet.Sharing, 35650);

		// Token: 0x040049EA RID: 18922
		public static readonly GuidIdPropertyDefinition MigrationJobRemoteDomain = InternalSchema.CreateGuidIdProperty("MigrationJobRemoteDomain", typeof(string), WellKnownPropertySet.Sharing, 35651);

		// Token: 0x040049EB RID: 18923
		public static readonly GuidIdPropertyDefinition MigrationJobRemoteServerVersion = InternalSchema.CreateGuidIdProperty("MigrationJobRemoteServerVersion", typeof(string), WellKnownPropertySet.Sharing, 35652);

		// Token: 0x040049EC RID: 18924
		public static readonly GuidIdPropertyDefinition MigrationJobItemRemoteMailboxLegacyDN = InternalSchema.CreateGuidIdProperty("MigrationJobItemRemoteMailboxLegacyDN", typeof(string), WellKnownPropertySet.Sharing, 35653);

		// Token: 0x040049ED RID: 18925
		public static readonly GuidIdPropertyDefinition MigrationJobItemRemoteServerLegacyDN = InternalSchema.CreateGuidIdProperty("MigrationJobItemRemoteServerLegacyDN", typeof(string), WellKnownPropertySet.Sharing, 35654);

		// Token: 0x040049EE RID: 18926
		public static readonly GuidIdPropertyDefinition MigrationJobItemProxyServerHostName = InternalSchema.CreateGuidIdProperty("MigrationJobItemProxyServerHostName", typeof(string), WellKnownPropertySet.Sharing, 35655);

		// Token: 0x040049EF RID: 18927
		public static readonly GuidIdPropertyDefinition MigrationJobItemRemoteAutodiscoverUrl = InternalSchema.CreateGuidIdProperty("MigrationJobItemRemoteAutodiscoverUrl", typeof(string), WellKnownPropertySet.Sharing, 35656);

		// Token: 0x040049F0 RID: 18928
		public static readonly GuidIdPropertyDefinition MigrationJobExchangeRemoteServerAuth = InternalSchema.CreateGuidIdProperty("MigrationJobExchangeRemoteServerAuth", typeof(int), WellKnownPropertySet.Sharing, 35657);

		// Token: 0x040049F1 RID: 18929
		public static readonly GuidIdPropertyDefinition MigrationJobItemHotmailLocalizedError = InternalSchema.CreateGuidIdProperty("MigrationJobItemHotmailLocalizedError", typeof(string), WellKnownPropertySet.Sharing, 35659);

		// Token: 0x040049F2 RID: 18930
		public static readonly GuidIdPropertyDefinition MigrationJobItemExchangeRemoteServerHostName = InternalSchema.CreateGuidIdProperty("MigrationJobItemExchangeRemoteServerHostName", typeof(string), WellKnownPropertySet.Sharing, 35660);

		// Token: 0x040049F3 RID: 18931
		public static readonly GuidIdPropertyDefinition MigrationJobOwnerId = InternalSchema.CreateGuidIdProperty("MigrationJobOwnerId", typeof(byte[]), WellKnownPropertySet.Sharing, 35661);

		// Token: 0x040049F4 RID: 18932
		public static readonly GuidIdPropertyDefinition MigrationJobSuppressErrors = InternalSchema.CreateGuidIdProperty("MigrationJobSuppressErrors", typeof(bool), WellKnownPropertySet.Sharing, 35662);

		// Token: 0x040049F5 RID: 18933
		public static readonly GuidIdPropertyDefinition MigrationJobFinalizeTime = InternalSchema.CreateGuidIdProperty("MigrationJobFinalizeTime", typeof(ExDateTime), WellKnownPropertySet.Sharing, 35663);

		// Token: 0x040049F6 RID: 18934
		public static readonly GuidIdPropertyDefinition MigrationSubmittedByUserAdminType = InternalSchema.CreateGuidIdProperty("MigrationSubmittedByUserAdminType", typeof(int), WellKnownPropertySet.Sharing, 35664);

		// Token: 0x040049F7 RID: 18935
		public static readonly GuidIdPropertyDefinition MigrationJobItemExchangeRecipientIndex = InternalSchema.CreateGuidIdProperty("MigrationJobItemExchangeRecipientIndex", typeof(int), WellKnownPropertySet.Sharing, 35665);

		// Token: 0x040049F8 RID: 18936
		public static readonly GuidIdPropertyDefinition MigrationJobItemExchangeRecipientProperties = InternalSchema.CreateGuidIdProperty("MigrationJobItemExchangeRecipientProperties", typeof(string), WellKnownPropertySet.Sharing, 35667);

		// Token: 0x040049F9 RID: 18937
		public static readonly GuidIdPropertyDefinition MigrationJobItemMRSId = InternalSchema.CreateGuidIdProperty("MigrationJobItemMRSId", typeof(Guid), WellKnownPropertySet.Sharing, 35668);

		// Token: 0x040049FA RID: 18938
		public static readonly GuidIdPropertyDefinition MigrationJobItemExchangeMsExchHomeServerName = InternalSchema.CreateGuidIdProperty("MigrationJobItemExchangeMsExchHomeServerName", typeof(string), WellKnownPropertySet.Sharing, 35669);

		// Token: 0x040049FB RID: 18939
		public static readonly GuidIdPropertyDefinition MigrationJobItemProvisioningData = InternalSchema.CreateGuidIdProperty("MigrationJobItemProvisioningData", typeof(string), WellKnownPropertySet.Sharing, 35670);

		// Token: 0x040049FC RID: 18940
		public static readonly GuidIdPropertyDefinition MigrationJobStatisticsEnabled = InternalSchema.CreateGuidIdProperty("MigrationJobStatisticsEnabled", typeof(bool), WellKnownPropertySet.Sharing, 35671);

		// Token: 0x040049FD RID: 18941
		public static readonly GuidIdPropertyDefinition MigrationJobItemItemsSynced = InternalSchema.CreateGuidIdProperty("MigrationJobItemItemsSynced", typeof(long), WellKnownPropertySet.Sharing, 35672);

		// Token: 0x040049FE RID: 18942
		public static readonly GuidIdPropertyDefinition MigrationJobItemItemsSkipped = InternalSchema.CreateGuidIdProperty("MigrationJobItemItemsSkipped", typeof(long), WellKnownPropertySet.Sharing, 35673);

		// Token: 0x040049FF RID: 18943
		public static readonly GuidIdPropertyDefinition MigrationJobItemLastProvisionedMemberIndex = InternalSchema.CreateGuidIdProperty("MigrationJobItemLastProvisionedMemberIndex", typeof(int), WellKnownPropertySet.Sharing, 35674);

		// Token: 0x04004A00 RID: 18944
		public static readonly GuidIdPropertyDefinition MigrationJobItemMailboxDatabaseId = InternalSchema.CreateGuidIdProperty("MigrationJobItemMailboxDatabaseId", typeof(Guid), WellKnownPropertySet.Sharing, 35675);

		// Token: 0x04004A01 RID: 18945
		public static readonly GuidIdPropertyDefinition MigrationReportName = InternalSchema.CreateGuidIdProperty("MigrationReportName", typeof(string), WellKnownPropertySet.Sharing, 35676);

		// Token: 0x04004A02 RID: 18946
		public static readonly GuidIdPropertyDefinition MigrationJobItemOwnerId = InternalSchema.CreateGuidIdProperty("MigrationJobItemOwnerId", typeof(byte[]), WellKnownPropertySet.Sharing, 35677);

		// Token: 0x04004A03 RID: 18947
		public static readonly GuidIdPropertyDefinition MigrationJobTotalItemCountLegacy = InternalSchema.CreateGuidIdProperty("MigrationJobTotalItemCountR5", typeof(string), WellKnownPropertySet.Sharing, 35678);

		// Token: 0x04004A04 RID: 18948
		public static readonly GuidIdPropertyDefinition MigrationJobItemADObjectExists = InternalSchema.CreateGuidIdProperty("MigrationJobItemADObjectExists", typeof(bool), WellKnownPropertySet.Sharing, 35679);

		// Token: 0x04004A05 RID: 18949
		public static readonly GuidIdPropertyDefinition MigrationJobCancellationReason = InternalSchema.CreateGuidIdProperty("MigrationJobCancellationReason", typeof(int), WellKnownPropertySet.Sharing, 35680);

		// Token: 0x04004A06 RID: 18950
		public static readonly GuidIdPropertyDefinition MigrationJobItemExchangeMbxEncryptedPassword = InternalSchema.CreateGuidIdProperty("MigrationJobItemExchangeMbxEncryptedPassword", typeof(string), WellKnownPropertySet.Sharing, 35681);

		// Token: 0x04004A07 RID: 18951
		public static readonly GuidIdPropertyDefinition MigrationJobItemRecipientType = InternalSchema.CreateGuidIdProperty("MigrationJobItemRecipientType", typeof(int), WellKnownPropertySet.Sharing, 35682);

		// Token: 0x04004A08 RID: 18952
		public static readonly GuidIdPropertyDefinition MigrationJobDelegatedAdminOwnerId = InternalSchema.CreateGuidIdProperty("MigrationJobDelegatedAdminOwnerId", typeof(string), WellKnownPropertySet.Sharing, 35683);

		// Token: 0x04004A09 RID: 18953
		public static readonly GuidIdPropertyDefinition MigrationJobCheckWLSA = InternalSchema.CreateGuidIdProperty("MigrationJobCheckWLSA", typeof(bool), WellKnownPropertySet.Sharing, 35684);

		// Token: 0x04004A0A RID: 18954
		public static readonly GuidIdPropertyDefinition MigrationJobItemHotmailPodID = InternalSchema.CreateGuidIdProperty("MigrationJobItemHotmailPodID", typeof(int), WellKnownPropertySet.Sharing, 35685);

		// Token: 0x04004A0B RID: 18955
		public static readonly GuidIdPropertyDefinition MigrationJobItemTransientErrorCount = InternalSchema.CreateGuidIdProperty("MigrationJobItemTransientErrorCount", typeof(int), WellKnownPropertySet.Sharing, 35686);

		// Token: 0x04004A0C RID: 18956
		public static readonly GuidIdPropertyDefinition MigrationJobItemPreviousStatus = InternalSchema.CreateGuidIdProperty("MigrationJobItemPreviousStatus", typeof(int), WellKnownPropertySet.Sharing, 35687);

		// Token: 0x04004A0D RID: 18957
		public static readonly GuidIdPropertyDefinition MigrationJobItemSubscriptionId = InternalSchema.CreateGuidIdProperty("MigrationJobItemSubscriptionId", typeof(Guid), WellKnownPropertySet.Sharing, 35688);

		// Token: 0x04004A0E RID: 18958
		public static readonly GuidIdPropertyDefinition MigrationHotmailSubscriptionId = InternalSchema.CreateGuidIdProperty("MigrationHotmailSubscriptionId", typeof(Guid), WellKnownPropertySet.Sharing, 35689);

		// Token: 0x04004A0F RID: 18959
		public static readonly GuidIdPropertyDefinition MigrationJobInternalErrorTime = InternalSchema.CreateGuidIdProperty("JobInternalErrorTime", typeof(ExDateTime), WellKnownPropertySet.Sharing, 35691);

		// Token: 0x04004A10 RID: 18960
		public static readonly GuidIdPropertyDefinition MigrationJobPoisonCount = InternalSchema.CreateGuidIdProperty("MigrationJobPoisonCount", typeof(int), WellKnownPropertySet.Sharing, 35692);

		// Token: 0x04004A11 RID: 18961
		public static readonly GuidIdPropertyDefinition MigrationReportType = InternalSchema.CreateGuidIdProperty("MigrationReportType", typeof(int), WellKnownPropertySet.Sharing, 35693);

		// Token: 0x04004A12 RID: 18962
		public static readonly GuidIdPropertyDefinition MigrationSuccessReportUrl = InternalSchema.CreateGuidIdProperty("MigrationSuccessReportUrl", typeof(string), WellKnownPropertySet.Sharing, 35694);

		// Token: 0x04004A13 RID: 18963
		public static readonly GuidIdPropertyDefinition MigrationErrorReportUrl = InternalSchema.CreateGuidIdProperty("MigrationErrorReportUrl", typeof(string), WellKnownPropertySet.Sharing, 35695);

		// Token: 0x04004A14 RID: 18964
		public static readonly GuidIdPropertyDefinition MigrationJobTargetDomainName = InternalSchema.CreateGuidIdProperty("MigrationJobTargetDomainName", typeof(string), WellKnownPropertySet.Sharing, 35696);

		// Token: 0x04004A15 RID: 18965
		public static readonly GuidIdPropertyDefinition MigrationJobIsStaged = InternalSchema.CreateGuidIdProperty("MigrationJobIsStaged", typeof(bool), WellKnownPropertySet.Sharing, 35697);

		// Token: 0x04004A16 RID: 18966
		public static readonly GuidIdPropertyDefinition MigrationJobItemForceChangePassword = InternalSchema.CreateGuidIdProperty("MigrationJobItemForceChangePassword", typeof(bool), WellKnownPropertySet.Sharing, 35698);

		// Token: 0x04004A17 RID: 18967
		public static readonly GuidIdPropertyDefinition MigrationJobItemLocalizedErrorID = InternalSchema.CreateGuidIdProperty("MigrationJobItemLocalizedErrorID", typeof(string), WellKnownPropertySet.Sharing, 35699);

		// Token: 0x04004A18 RID: 18968
		public static readonly GuidIdPropertyDefinition MigrationJobItemHotmailLocalizedErrorID = InternalSchema.CreateGuidIdProperty("MigrationJobItemHotmailLocalizedErrorID", typeof(string), WellKnownPropertySet.Sharing, 35701);

		// Token: 0x04004A19 RID: 18969
		public static readonly GuidIdPropertyDefinition MigrationJobItemGroupMemberSkipped = InternalSchema.CreateGuidIdProperty("MigrationJobItemGroupMemberSkipped", typeof(int), WellKnownPropertySet.Sharing, 35702);

		// Token: 0x04004A1A RID: 18970
		public static readonly GuidIdPropertyDefinition MigrationJobItemGroupMemberProvisioned = InternalSchema.CreateGuidIdProperty("MigrationJobItemGroupMemberProvisioned", typeof(int), WellKnownPropertySet.Sharing, 35703);

		// Token: 0x04004A1B RID: 18971
		public static readonly GuidIdPropertyDefinition MigrationJobItemGroupMemberProvisioningState = InternalSchema.CreateGuidIdProperty("MigrationJobItemGroupMemberProvisioningState", typeof(int), WellKnownPropertySet.Sharing, 35704);

		// Token: 0x04004A1C RID: 18972
		public static readonly GuidIdPropertyDefinition MigrationJobItemStatusHistory = InternalSchema.CreateGuidIdProperty("MigrationJobItemStatusHistory", typeof(string), WellKnownPropertySet.Sharing, 35705);

		// Token: 0x04004A1D RID: 18973
		public static readonly GuidIdPropertyDefinition MigrationJobItemIDSIdentityFlags = InternalSchema.CreateGuidIdProperty("MigrationJobItemIDSIdentityFlags", typeof(int), WellKnownPropertySet.Sharing, 35706);

		// Token: 0x04004A1E RID: 18974
		public static readonly GuidIdPropertyDefinition MigrationJobItemLocalizedMessage = InternalSchema.CreateGuidIdProperty("MigrationJobItemLocalizedMessage", typeof(string), WellKnownPropertySet.Sharing, 35707);

		// Token: 0x04004A1F RID: 18975
		public static readonly GuidIdPropertyDefinition MigrationJobItemLocalizedMessageID = InternalSchema.CreateGuidIdProperty("MigrationJobItemLocalizedMessageID", typeof(string), WellKnownPropertySet.Sharing, 35708);

		// Token: 0x04004A20 RID: 18976
		public static readonly GuidIdPropertyDefinition MigrationLastSuccessfulSyncTime = InternalSchema.CreateGuidIdProperty("MigrationLastSuccessfulSyncTime", typeof(ExDateTime), WellKnownPropertySet.Sharing, 35709);

		// Token: 0x04004A21 RID: 18977
		public static readonly GuidIdPropertyDefinition MigrationPersistableDictionary = InternalSchema.CreateGuidIdProperty("MigrationPersistableDictionary", typeof(string), WellKnownPropertySet.Sharing, 35710);

		// Token: 0x04004A22 RID: 18978
		public static readonly GuidIdPropertyDefinition MigrationReportSets = InternalSchema.CreateGuidIdProperty("MigrationReportSets", typeof(string), WellKnownPropertySet.Sharing, 35711);

		// Token: 0x04004A23 RID: 18979
		public static readonly GuidIdPropertyDefinition MigrationDisableTime = InternalSchema.CreateGuidIdProperty("MigrationDisableTime", typeof(ExDateTime), WellKnownPropertySet.Sharing, 35712);

		// Token: 0x04004A24 RID: 18980
		public static readonly GuidIdPropertyDefinition MigrationProvisionedTime = InternalSchema.CreateGuidIdProperty("MigrationProvisionedTime", typeof(ExDateTime), WellKnownPropertySet.Sharing, 35713);

		// Token: 0x04004A25 RID: 18981
		public static readonly GuidIdPropertyDefinition MigrationSameStatusCount = InternalSchema.CreateGuidIdProperty("MigrationSameStatusCount", typeof(int), WellKnownPropertySet.Sharing, 35715);

		// Token: 0x04004A26 RID: 18982
		public static readonly GuidIdPropertyDefinition MigrationTransitionTime = InternalSchema.CreateGuidIdProperty("MigrationTransitionTime", typeof(ExDateTime), WellKnownPropertySet.Sharing, 35716);

		// Token: 0x04004A27 RID: 18983
		public static readonly GuidIdPropertyDefinition MigrationDeltaSyncShouldSync = InternalSchema.CreateGuidIdProperty("MigrationDeltaSyncShouldSync", typeof(bool), WellKnownPropertySet.Sharing, 35718);

		// Token: 0x04004A28 RID: 18984
		public static readonly GuidIdPropertyDefinition MigrationJobItemId = InternalSchema.CreateGuidIdProperty("MigrationJobItemId", typeof(Guid), WellKnownPropertySet.Sharing, 35719);

		// Token: 0x04004A29 RID: 18985
		public static readonly GuidIdPropertyDefinition MigrationEndpointType = InternalSchema.CreateGuidIdProperty("MigrationEndpointType", typeof(int), WellKnownPropertySet.Sharing, 35751);

		// Token: 0x04004A2A RID: 18986
		public static readonly GuidIdPropertyDefinition MigrationEndpointName = InternalSchema.CreateGuidIdProperty("MigrationEndpointName", typeof(string), WellKnownPropertySet.Sharing, 35752);

		// Token: 0x04004A2B RID: 18987
		public static readonly GuidIdPropertyDefinition MigrationEndpointGuid = InternalSchema.CreateGuidIdProperty("MigrationEndpointGuid", typeof(Guid), WellKnownPropertySet.Sharing, 35753);

		// Token: 0x04004A2C RID: 18988
		public static readonly GuidIdPropertyDefinition MigrationEndpointRemoteHostName = InternalSchema.CreateGuidIdProperty("MigrationEndpointRemoteHostName", typeof(string), WellKnownPropertySet.Sharing, 35754);

		// Token: 0x04004A2D RID: 18989
		public static readonly GuidIdPropertyDefinition MigrationEndpointExchangeServer = InternalSchema.CreateGuidIdProperty("MigrationEndpointExchangeServer", typeof(string), WellKnownPropertySet.Sharing, 35755);

		// Token: 0x04004A2E RID: 18990
		public static readonly GuidIdPropertyDefinition MigrationJobSourceEndpoint = InternalSchema.CreateGuidIdProperty("MigrationJobSourceEndpoint", typeof(Guid), WellKnownPropertySet.Sharing, 35756);

		// Token: 0x04004A2F RID: 18991
		public static readonly GuidIdPropertyDefinition MigrationJobTargetEndpoint = InternalSchema.CreateGuidIdProperty("MigrationJobTargetEndpoint", typeof(Guid), WellKnownPropertySet.Sharing, 35757);

		// Token: 0x04004A30 RID: 18992
		public static readonly GuidIdPropertyDefinition MigrationRuntimeJobData = InternalSchema.CreateGuidIdProperty("MigrationRuntimeJobData", typeof(string), WellKnownPropertySet.Sharing, 35758);

		// Token: 0x04004A31 RID: 18993
		public static readonly GuidIdPropertyDefinition MigrationJobDirection = InternalSchema.CreateGuidIdProperty("MigrationJobDirection", typeof(int), WellKnownPropertySet.Sharing, 35759);

		// Token: 0x04004A32 RID: 18994
		public static readonly GuidIdPropertyDefinition MigrationJobTargetDatabase = InternalSchema.CreateGuidIdProperty("MigrationJobTargetDatabase", typeof(string[]), WellKnownPropertySet.Sharing, 35760);

		// Token: 0x04004A33 RID: 18995
		public static readonly GuidIdPropertyDefinition MigrationJobTargetArchiveDatabase = InternalSchema.CreateGuidIdProperty("MigrationJobTargetArchiveDatabase", typeof(string[]), WellKnownPropertySet.Sharing, 35761);

		// Token: 0x04004A34 RID: 18996
		public static readonly GuidIdPropertyDefinition MigrationJobBadItemLimit = InternalSchema.CreateGuidIdProperty("MigrationJobBadItemLimit", typeof(string), WellKnownPropertySet.Sharing, 35762);

		// Token: 0x04004A35 RID: 18997
		public static readonly GuidIdPropertyDefinition MigrationJobLargeItemLimit = InternalSchema.CreateGuidIdProperty("MigrationJobLargeItemLimit", typeof(string), WellKnownPropertySet.Sharing, 35763);

		// Token: 0x04004A36 RID: 18998
		public static readonly GuidIdPropertyDefinition MigrationJobPrimaryOnly = InternalSchema.CreateGuidIdProperty("MigrationJobPrimaryOnly", typeof(bool), WellKnownPropertySet.Sharing, 35764);

		// Token: 0x04004A37 RID: 18999
		public static readonly GuidIdPropertyDefinition MigrationJobArchiveOnly = InternalSchema.CreateGuidIdProperty("MigrationJobArchiveOnly", typeof(bool), WellKnownPropertySet.Sharing, 35765);

		// Token: 0x04004A38 RID: 19000
		public static readonly GuidIdPropertyDefinition MigrationSlotMaximumInitialSeedings = InternalSchema.CreateGuidIdProperty("MigrationSlotMaximumInitialSeedings", typeof(string), WellKnownPropertySet.Sharing, 35766);

		// Token: 0x04004A39 RID: 19001
		public static readonly GuidIdPropertyDefinition MigrationSlotMaximumIncrementalSeedings = InternalSchema.CreateGuidIdProperty("MigrationSlotMaximumIncrementalSeedings", typeof(string), WellKnownPropertySet.Sharing, 35767);

		// Token: 0x04004A3A RID: 19002
		public static readonly GuidIdPropertyDefinition MigrationJobTargetDeliveryDomain = InternalSchema.CreateGuidIdProperty("MigrationJobTargetDeliveryDomain", typeof(string), WellKnownPropertySet.Sharing, 35768);

		// Token: 0x04004A3B RID: 19003
		public static readonly GuidIdPropertyDefinition MigrationJobSkipSteps = InternalSchema.CreateGuidIdProperty("MigrationJobSkipSteps", typeof(int), WellKnownPropertySet.Sharing, 35769);

		// Token: 0x04004A3C RID: 19004
		public static readonly GuidIdPropertyDefinition MigrationJobItemSlotType = InternalSchema.CreateGuidIdProperty("MigrationJobItemSlotType", typeof(int), WellKnownPropertySet.Sharing, 35770);

		// Token: 0x04004A3D RID: 19005
		public static readonly GuidIdPropertyDefinition MigrationJobItemSlotProviderId = InternalSchema.CreateGuidIdProperty("MigrationJobItemSlotProviderId", typeof(Guid), WellKnownPropertySet.Sharing, 35771);

		// Token: 0x04004A3E RID: 19006
		public static readonly GuidIdPropertyDefinition MigrationJobCountCache = InternalSchema.CreateGuidIdProperty("MigrationJobCountCache", typeof(string), WellKnownPropertySet.Sharing, 35772);

		// Token: 0x04004A3F RID: 19007
		public static readonly GuidIdPropertyDefinition MigrationJobCountCacheFullScanTime = InternalSchema.CreateGuidIdProperty("MigrationJobCountCacheFullScanTime", typeof(ExDateTime), WellKnownPropertySet.Sharing, 35773);

		// Token: 0x04004A40 RID: 19008
		public static readonly GuidIdPropertyDefinition MigrationJobLastRestartTime = InternalSchema.CreateGuidIdProperty("MigrationJobLastRestartTime", typeof(ExDateTime), WellKnownPropertySet.Sharing, 35774);

		// Token: 0x04004A41 RID: 19009
		public static readonly GuidIdPropertyDefinition MigrationFailureRecord = InternalSchema.CreateGuidIdProperty("MigrationFailureRecord", typeof(string), WellKnownPropertySet.Sharing, 35775);

		// Token: 0x04004A42 RID: 19010
		public static readonly GuidIdPropertyDefinition MigrationJobIsRunning = InternalSchema.CreateGuidIdProperty("MigrationJobIsRunning", typeof(bool), WellKnownPropertySet.Sharing, 35776);

		// Token: 0x04004A43 RID: 19011
		public static readonly GuidIdPropertyDefinition MigrationSubscriptionSettingsLastModifiedTime = InternalSchema.CreateGuidIdProperty("MigrationSubscriptionSettingsLastModifiedTime", typeof(ExDateTime), WellKnownPropertySet.Sharing, 35777);

		// Token: 0x04004A44 RID: 19012
		public static readonly GuidIdPropertyDefinition MigrationJobItemSubscriptionSettingsLastUpdatedTime = InternalSchema.CreateGuidIdProperty("MigrationJobItemSubscriptionSettingsLastUpdatedTime", typeof(ExDateTime), WellKnownPropertySet.Sharing, 35778);

		// Token: 0x04004A45 RID: 19013
		public static readonly GuidIdPropertyDefinition MigrationJobStartAfter = InternalSchema.CreateGuidIdProperty("MigrationJobStartAfter", typeof(ExDateTime), WellKnownPropertySet.Sharing, 35779);

		// Token: 0x04004A46 RID: 19014
		public static readonly GuidIdPropertyDefinition MigrationJobCompleteAfter = InternalSchema.CreateGuidIdProperty("MigrationJobCompleteAfter", typeof(ExDateTime), WellKnownPropertySet.Sharing, 35780);

		// Token: 0x04004A47 RID: 19015
		public static readonly GuidIdPropertyDefinition MigrationNextProcessTime = InternalSchema.CreateGuidIdProperty("MigrationNextProcessTime", typeof(ExDateTime), WellKnownPropertySet.Sharing, 35781);

		// Token: 0x04004A48 RID: 19016
		public static readonly GuidIdPropertyDefinition MigrationStatusDataFailureWatsonHash = InternalSchema.CreateGuidIdProperty("MigrationStatusDataFailureWatsonHash", typeof(string), WellKnownPropertySet.Sharing, 35782);

		// Token: 0x04004A49 RID: 19017
		public static readonly GuidIdPropertyDefinition MigrationState = InternalSchema.CreateGuidIdProperty("MigrationState", typeof(int), WellKnownPropertySet.Sharing, 35783);

		// Token: 0x04004A4A RID: 19018
		public static readonly GuidIdPropertyDefinition MigrationFlags = InternalSchema.CreateGuidIdProperty("MigrationFlags", typeof(int), WellKnownPropertySet.Sharing, 35784);

		// Token: 0x04004A4B RID: 19019
		public static readonly GuidIdPropertyDefinition MigrationStage = InternalSchema.CreateGuidIdProperty("MigrationStage", typeof(int), WellKnownPropertySet.Sharing, 35785);

		// Token: 0x04004A4C RID: 19020
		public static readonly GuidIdPropertyDefinition MigrationStep = InternalSchema.CreateGuidIdProperty("MigrationStep", typeof(int), WellKnownPropertySet.Sharing, 35786);

		// Token: 0x04004A4D RID: 19021
		public static readonly GuidIdPropertyDefinition MigrationWorkflow = InternalSchema.CreateGuidIdProperty("MigrationWorkflow", typeof(string), WellKnownPropertySet.Sharing, 35787);

		// Token: 0x04004A4E RID: 19022
		public static readonly GuidIdPropertyDefinition MigrationPSTFilePath = InternalSchema.CreateGuidIdProperty("MigrationPSTFilePath", typeof(string), WellKnownPropertySet.Sharing, 35788);

		// Token: 0x04004A4F RID: 19023
		public static readonly GuidIdPropertyDefinition MigrationSourceRootFolder = InternalSchema.CreateGuidIdProperty("MigrationSourceRootFolder", typeof(string), WellKnownPropertySet.Sharing, 35789);

		// Token: 0x04004A50 RID: 19024
		public static readonly GuidIdPropertyDefinition MigrationTargetRootFolder = InternalSchema.CreateGuidIdProperty("MigrationTargetRootFolder", typeof(string), WellKnownPropertySet.Sharing, 35790);

		// Token: 0x04004A51 RID: 19025
		public static readonly GuidIdPropertyDefinition MigrationJobItemLocalMailboxIdentifier = InternalSchema.CreateGuidIdProperty("MigrationJobItemLocalMailboxIdentifier", typeof(string), WellKnownPropertySet.Sharing, 35791);

		// Token: 0x04004A52 RID: 19026
		public static readonly GuidIdPropertyDefinition MigrationExchangeObjectId = InternalSchema.CreateGuidIdProperty("MigrationJobExchangeObjectGuid", typeof(Guid), WellKnownPropertySet.Sharing, 35792);

		// Token: 0x04004A53 RID: 19027
		public static readonly GuidIdPropertyDefinition MigrationJobItemSubscriptionQueuedTime = InternalSchema.CreateGuidIdProperty("MigrationJobItemSubscriptionQueuedTime", typeof(ExDateTime), WellKnownPropertySet.Sharing, 35793);

		// Token: 0x04004A54 RID: 19028
		public static readonly GuidIdPropertyDefinition MigrationJobSourcePublicFolderDatabase = InternalSchema.CreateGuidIdProperty("MigrationJobSourcePublicFolderDatabase", typeof(string), WellKnownPropertySet.Sharing, 35794);

		// Token: 0x04004A55 RID: 19029
		public static readonly GuidIdPropertyDefinition MigrationJobItemPuid = InternalSchema.CreateGuidIdProperty("MigrationJobItemPuid", typeof(long), WellKnownPropertySet.Sharing, 35795);

		// Token: 0x04004A56 RID: 19030
		public static readonly GuidIdPropertyDefinition MigrationJobItemFirstName = InternalSchema.CreateGuidIdProperty("MigrationJobItemFirstName", typeof(string), WellKnownPropertySet.Sharing, 35796);

		// Token: 0x04004A57 RID: 19031
		public static readonly GuidIdPropertyDefinition MigrationJobItemLastName = InternalSchema.CreateGuidIdProperty("MigrationJobItemLastName", typeof(string), WellKnownPropertySet.Sharing, 35797);

		// Token: 0x04004A58 RID: 19032
		public static readonly GuidIdPropertyDefinition MigrationJobItemTimeZone = InternalSchema.CreateGuidIdProperty("MigrationJobItemTimeZone", typeof(string), WellKnownPropertySet.Sharing, 35798);

		// Token: 0x04004A59 RID: 19033
		public static readonly GuidIdPropertyDefinition MigrationJobItemLocaleId = InternalSchema.CreateGuidIdProperty("MigrationJobItemLocaleId", typeof(int), WellKnownPropertySet.Sharing, 35799);

		// Token: 0x04004A5A RID: 19034
		public static readonly GuidIdPropertyDefinition MigrationJobItemAliases = InternalSchema.CreateGuidIdProperty("MigrationJobItemAliases", typeof(string), WellKnownPropertySet.Sharing, 35800);

		// Token: 0x04004A5B RID: 19035
		public static readonly GuidIdPropertyDefinition MigrationJobItemAccountSize = InternalSchema.CreateGuidIdProperty("MigrationJobItemAccountSize", typeof(long), WellKnownPropertySet.Sharing, 35801);

		// Token: 0x04004A5C RID: 19036
		public static readonly GuidIdPropertyDefinition MigrationJobLastFinalizationAttempt = InternalSchema.CreateGuidIdProperty("MigrationJobLastFinalizationAttempt", typeof(int), WellKnownPropertySet.Sharing, 35805);

		// Token: 0x04004A5D RID: 19037
		public static readonly GuidNamePropertyDefinition MonitoringEventInstanceId = InternalSchema.CreateGuidNameProperty("MonitoringEventInstanceId", typeof(int), WellKnownPropertySet.PublicStrings, "MonitoringEventInstanceId");

		// Token: 0x04004A5E RID: 19038
		public static readonly GuidNamePropertyDefinition MonitoringEventSource = InternalSchema.CreateGuidNameProperty("MonitoringEventSource", typeof(string), WellKnownPropertySet.PublicStrings, "MonitoringEventSource");

		// Token: 0x04004A5F RID: 19039
		public static readonly GuidNamePropertyDefinition MonitoringEventCategoryId = InternalSchema.CreateGuidNameProperty("MonitoringEventCategoryId", typeof(int), WellKnownPropertySet.PublicStrings, "MonitoringEventCategoryId");

		// Token: 0x04004A60 RID: 19040
		public static readonly GuidNamePropertyDefinition MonitoringEventTimeUtc = InternalSchema.CreateGuidNameProperty("MonitoringEventTimeUtc", typeof(ExDateTime), WellKnownPropertySet.PublicStrings, "MonitoringEventTimeUtc");

		// Token: 0x04004A61 RID: 19041
		public static readonly GuidNamePropertyDefinition MonitoringEventEntryType = InternalSchema.CreateGuidNameProperty("MonitoringEventEntryType", typeof(int), WellKnownPropertySet.PublicStrings, "MonitoringEventEntryType");

		// Token: 0x04004A62 RID: 19042
		public static readonly GuidNamePropertyDefinition MonitoringInsertionStrings = InternalSchema.CreateGuidNameProperty("MonitoringInsertionStrings", typeof(string[]), WellKnownPropertySet.PublicStrings, "MonitoringInsertionStrings");

		// Token: 0x04004A63 RID: 19043
		public static readonly GuidNamePropertyDefinition MonitoringUniqueId = InternalSchema.CreateGuidNameProperty("MonitoringUniqueId", typeof(byte[]), WellKnownPropertySet.PublicStrings, "MonitoringUniqueId");

		// Token: 0x04004A64 RID: 19044
		public static readonly GuidNamePropertyDefinition MonitoringNotificationEmailSent = InternalSchema.CreateGuidNameProperty("MonitoringNotificationEmailSent", typeof(bool), WellKnownPropertySet.PublicStrings, "MonitoringNotificationEmailSent");

		// Token: 0x04004A65 RID: 19045
		public static readonly GuidNamePropertyDefinition MonitoringCreationTimeUtc = InternalSchema.CreateGuidNameProperty("MonitoringCreationTimeUtc", typeof(ExDateTime), WellKnownPropertySet.PublicStrings, "MonitoringCreationTimeUtc");

		// Token: 0x04004A66 RID: 19046
		public static readonly GuidNamePropertyDefinition MonitoringCountOfNotificationsSentInPast24Hours = InternalSchema.CreateGuidNameProperty("MonitoringCountOfNotificationsSentInPast24Hours", typeof(long), WellKnownPropertySet.PublicStrings, "MonitoringCountOfNotificationsSentInPast24Hours");

		// Token: 0x04004A67 RID: 19047
		public static readonly GuidNamePropertyDefinition MonitoringNotificationRecipients = InternalSchema.CreateGuidNameProperty("MonitoringNotificationRecipients", typeof(string[]), WellKnownPropertySet.PublicStrings, "MonitoringNotificationRecipients");

		// Token: 0x04004A68 RID: 19048
		public static readonly GuidNamePropertyDefinition MonitoringHashCodeForDuplicateDetection = InternalSchema.CreateGuidNameProperty("MonitoringHashCodeForDuplicateDetection", typeof(long), WellKnownPropertySet.PublicStrings, "MonitoringHashCodeForDuplicateDetection");

		// Token: 0x04004A69 RID: 19049
		public static readonly GuidNamePropertyDefinition MonitoringNotificationMessageIds = InternalSchema.CreateGuidNameProperty("MonitoringNotificationMessageIds", typeof(string[]), WellKnownPropertySet.PublicStrings, "MonitoringNotificationMessageIds");

		// Token: 0x04004A6A RID: 19050
		public static readonly GuidNamePropertyDefinition MonitoringEventPeriodicKey = InternalSchema.CreateGuidNameProperty("MonitoringEventPeriodicKey", typeof(string), WellKnownPropertySet.PublicStrings, "MonitoringEventPeriodicKey");

		// Token: 0x04004A6B RID: 19051
		public static readonly StorePropertyDefinition IsTaskRecurring = new IsTaskRecurringProperty();

		// Token: 0x04004A6C RID: 19052
		public static readonly StorePropertyDefinition CalculatedRecurrenceType = new RecurrenceTypeProperty();

		// Token: 0x04004A6D RID: 19053
		public static readonly StorePropertyDefinition LinkEnabled = new LinkEnabled();

		// Token: 0x04004A6E RID: 19054
		public static readonly StorePropertyDefinition StartDate = new StartDate();

		// Token: 0x04004A6F RID: 19055
		public static readonly StorePropertyDefinition DueDate = new DueDate();

		// Token: 0x04004A70 RID: 19056
		public static readonly StorePropertyDefinition IsToDoItem = new FlagsProperty("IsToDoItem", InternalSchema.MapiToDoItemFlag, 1, PropertyDefinitionConstraint.None);

		// Token: 0x04004A71 RID: 19057
		public static readonly StorePropertyDefinition IsFlagSetForRecipient = new FlagsProperty("IsFlagSetForRecipient", InternalSchema.MapiToDoItemFlag, 8, PropertyDefinitionConstraint.None);

		// Token: 0x04004A72 RID: 19058
		public static readonly StorePropertyDefinition FlagStatus = new FlagStatusProperty();

		// Token: 0x04004A73 RID: 19059
		public static readonly StorePropertyDefinition Subject = new SubjectProperty();

		// Token: 0x04004A74 RID: 19060
		public static readonly StorePropertyDefinition SubjectPrefix = new SubjectPortionProperty("SubjectPrefix", InternalSchema.SubjectPrefixInternal);

		// Token: 0x04004A75 RID: 19061
		public static readonly StorePropertyDefinition NormalizedSubject = new SubjectPortionProperty("NormalizedSubject", InternalSchema.NormalizedSubjectInternal);

		// Token: 0x04004A76 RID: 19062
		public static readonly StorePropertyDefinition HasAttachment = new HasAttachmentProperty();

		// Token: 0x04004A77 RID: 19063
		public static readonly StorePropertyDefinition ClientSubmittedSecurely = new FlagsProperty("ClientSubmittedSecurely", InternalSchema.SecureSubmitFlags, 1, PropertyDefinitionConstraint.None);

		// Token: 0x04004A78 RID: 19064
		public static readonly StorePropertyDefinition ServerSubmittedSecurely = new FlagsProperty("ServerSubmittedSecurely", InternalSchema.SecureSubmitFlags, 2, PropertyDefinitionConstraint.None);

		// Token: 0x04004A79 RID: 19065
		public static readonly StorePropertyDefinition HasBeenSubmitted = new FlagsProperty("HasBeenSubmitted", InternalSchema.Flags, 4, PropertyDefinitionConstraint.None);

		// Token: 0x04004A7A RID: 19066
		public static readonly StorePropertyDefinition IsAssociated = new FlagsProperty("IsAssociated", InternalSchema.Flags, 64, PropertyDefinitionConstraint.None);

		// Token: 0x04004A7B RID: 19067
		public static readonly StorePropertyDefinition IsResend = new FlagsProperty("IsResend", InternalSchema.Flags, 128, PropertyDefinitionConstraint.None);

		// Token: 0x04004A7C RID: 19068
		public static readonly StorePropertyDefinition NeedSpecialRecipientProcessing = new FlagsProperty("NeedSpecialRecipientProcessing", InternalSchema.Flags, 131072, PropertyDefinitionConstraint.None);

		// Token: 0x04004A7D RID: 19069
		public static readonly StorePropertyDefinition IsReadReceiptPendingInternal = new FlagsProperty("IsReadReceiptPendingInternal", InternalSchema.Flags, 256, PropertyDefinitionConstraint.None);

		// Token: 0x04004A7E RID: 19070
		public static readonly StorePropertyDefinition IsNotReadReceiptPendingInternal = new FlagsProperty("IsNotReadReceiptPendingInternal", InternalSchema.Flags, 512, PropertyDefinitionConstraint.None);

		// Token: 0x04004A7F RID: 19071
		public static readonly StorePropertyDefinition IsDraft = new MessageFlagsProperty("IsDraft", InternalSchema.Flags, 8);

		// Token: 0x04004A80 RID: 19072
		public static readonly StorePropertyDefinition SuppressReadReceipt = new MessageFlagsProperty("SuppressReadReceipt", InternalSchema.Flags, 512);

		// Token: 0x04004A81 RID: 19073
		public static readonly StorePropertyDefinition IsRestricted = new MessageFlagsProperty("IsRestricted", InternalSchema.Flags, 2048);

		// Token: 0x04004A82 RID: 19074
		public static readonly StorePropertyDefinition MessageDeliveryNotificationSent = new MessageFlagsProperty("MessageDeliveryNotificationSent", InternalSchema.MessageStatus, 16384);

		// Token: 0x04004A83 RID: 19075
		public static readonly StorePropertyDefinition MimeConversionFailed = new MessageFlagsProperty("MimeConversionFailed", InternalSchema.MessageStatus, 32768);

		// Token: 0x04004A84 RID: 19076
		public static readonly StorePropertyDefinition AttachmentIsInline = new AttachmentIsInlineProperty();

		// Token: 0x04004A85 RID: 19077
		public static readonly StorePropertyDefinition ReplyToBlob = new ReplyToBlobProperty();

		// Token: 0x04004A86 RID: 19078
		public static readonly StorePropertyDefinition ReplyToNames = new ReplyToNamesProperty();

		// Token: 0x04004A87 RID: 19079
		public static readonly StorePropertyDefinition LikersBlob = new LikersBlobProperty();

		// Token: 0x04004A88 RID: 19080
		public static readonly StorePropertyDefinition LikeCount = new LikeCountProperty();

		// Token: 0x04004A89 RID: 19081
		public static readonly StorePropertyDefinition IsRead = new MessageFlagsProperty("IsRead", InternalSchema.Flags, 1);

		// Token: 0x04004A8A RID: 19082
		public static readonly StorePropertyDefinition WasEverRead = new MessageFlagsProperty("WasEverRead", InternalSchema.Flags, 1024);

		// Token: 0x04004A8B RID: 19083
		public static readonly StorePropertyDefinition MessageAnswered = new MessageFlagsProperty("MessageAnswered", InternalSchema.MessageStatus, 512);

		// Token: 0x04004A8C RID: 19084
		public static readonly StorePropertyDefinition MessageDelMarked = new MessageFlagsProperty("MessageDelMarked", InternalSchema.MessageStatus, 8);

		// Token: 0x04004A8D RID: 19085
		public static readonly StorePropertyDefinition MessageDraft = new MessageFlagsProperty("MessageDraft", InternalSchema.MessageStatus, 256);

		// Token: 0x04004A8E RID: 19086
		public static readonly StorePropertyDefinition MessageHidden = new MessageFlagsProperty("MessageHidden", InternalSchema.MessageStatus, 4);

		// Token: 0x04004A8F RID: 19087
		public static readonly StorePropertyDefinition MessageHighlighted = new MessageFlagsProperty("MessageHighlighted", InternalSchema.MessageStatus, 1);

		// Token: 0x04004A90 RID: 19088
		public static readonly StorePropertyDefinition MessageInConflict = new MessageFlagsProperty("MessageInConflict", InternalSchema.MessageStatus, 2048);

		// Token: 0x04004A91 RID: 19089
		public static readonly StorePropertyDefinition MessageRemoteDelete = new MessageFlagsProperty("MessageRemoteDelete", InternalSchema.MessageStatus, 8192);

		// Token: 0x04004A92 RID: 19090
		public static readonly StorePropertyDefinition MessageRemoteDownload = new MessageFlagsProperty("MessageRemoteDownload", InternalSchema.MessageStatus, 4096);

		// Token: 0x04004A93 RID: 19091
		public static readonly StorePropertyDefinition MessageTagged = new MessageFlagsProperty("MessageTagged", InternalSchema.MessageStatus, 2);

		// Token: 0x04004A94 RID: 19092
		public static readonly StorePropertyDefinition ReminderDueBy = new ReminderDueByProperty();

		// Token: 0x04004A95 RID: 19093
		public static readonly ReminderAdjustmentProperty ReminderMinutesBeforeStart = new ReminderAdjustmentProperty("ReminderMinutesBeforeStartProperty", InternalSchema.ReminderMinutesBeforeStartInternal);

		// Token: 0x04004A96 RID: 19094
		public static readonly ReminderAdjustmentProperty ReminderIsSet = new ReminderAdjustmentProperty("ReminderIsSetProperty", InternalSchema.ReminderIsSetInternal);

		// Token: 0x04004A97 RID: 19095
		public static readonly AutoResponseSuppressProperty AutoResponseSuppress = new AutoResponseSuppressProperty();

		// Token: 0x04004A98 RID: 19096
		public static readonly AutoResponseRequestProperty IsDeliveryReceiptRequested = new AutoResponseRequestProperty("IsDeliveryReceiptRequested", InternalSchema.IsDeliveryReceiptRequestedInternal);

		// Token: 0x04004A99 RID: 19097
		public static readonly AutoResponseRequestProperty IsNonDeliveryReceiptRequested = new AutoResponseRequestProperty("IsNonDeliveryReceiptRequested", InternalSchema.IsNonDeliveryReceiptRequestedInternal);

		// Token: 0x04004A9A RID: 19098
		public static readonly AutoResponseRequestProperty IsReadReceiptRequested = new AutoResponseRequestProperty("IsReadReceiptRequested", InternalSchema.IsReadReceiptRequestedInternal);

		// Token: 0x04004A9B RID: 19099
		public static readonly AutoResponseRequestProperty IsNotReadReceiptRequested = new AutoResponseRequestProperty("IsNotReadReceiptRequested", InternalSchema.IsNotReadReceiptRequestedInternal);

		// Token: 0x04004A9C RID: 19100
		public static readonly AutoResponseFlagProperty IsReadReceiptPending = new AutoResponseFlagProperty("IsReadReceiptPending", MessageFlags.IsReadReceiptPending, Microsoft.Exchange.Data.Storage.AutoResponseSuppress.RN);

		// Token: 0x04004A9D RID: 19101
		public static readonly AutoResponseFlagProperty IsNotReadReceiptPending = new AutoResponseFlagProperty("IsNotReadReceiptPending", MessageFlags.IsNotReadReceiptPending, Microsoft.Exchange.Data.Storage.AutoResponseSuppress.NRN);

		// Token: 0x04004A9E RID: 19102
		public static readonly StorePropertyDefinition AppointmentState = new AppointmentStateProperty();

		// Token: 0x04004A9F RID: 19103
		public static readonly StorePropertyDefinition IsAllDayEvent = new IsAllDayEventProperty();

		// Token: 0x04004AA0 RID: 19104
		public static readonly StorePropertyDefinition IsEvent = new IsEventProperty();

		// Token: 0x04004AA1 RID: 19105
		public static readonly StorePropertyDefinition CalendarItemType = new CalendarItemTypeProperty();

		// Token: 0x04004AA2 RID: 19106
		public static readonly StorePropertyDefinition StartTime = new StartTimeProperty();

		// Token: 0x04004AA3 RID: 19107
		public static readonly StorePropertyDefinition EndTime = new EndTimeProperty();

		// Token: 0x04004AA4 RID: 19108
		public static readonly StorePropertyDefinition StartTimeZone = new StartTimeZoneProperty();

		// Token: 0x04004AA5 RID: 19109
		public static readonly StorePropertyDefinition EndTimeZone = new EndTimeZoneProperty();

		// Token: 0x04004AA6 RID: 19110
		public static readonly StorePropertyDefinition StartTimeZoneId = new StartTimeZoneIdProperty();

		// Token: 0x04004AA7 RID: 19111
		public static readonly StorePropertyDefinition EndTimeZoneId = new EndTimeZoneIdProperty();

		// Token: 0x04004AA8 RID: 19112
		public static readonly StorePropertyDefinition StartWallClock = new StartWallClockProperty();

		// Token: 0x04004AA9 RID: 19113
		public static readonly StorePropertyDefinition EndWallClock = new EndWallClockProperty();

		// Token: 0x04004AAA RID: 19114
		public static readonly StorePropertyDefinition IsMeeting = new IsMeetingProperty();

		// Token: 0x04004AAB RID: 19115
		public static readonly StorePropertyDefinition IsSeriesCancelled = new IsSeriesCancelledProperty();

		// Token: 0x04004AAC RID: 19116
		public static readonly StorePropertyDefinition PropertyChangeMetadata = new PropertyChangeMetadataProperty();

		// Token: 0x04004AAD RID: 19117
		public static readonly StorePropertyDefinition CalendarInteropActionQueue = new ActionQueueProperty(InternalSchema.CalendarInteropActionQueueInternal, InternalSchema.CalendarInteropActionQueueHasDataInternal);

		// Token: 0x04004AAE RID: 19118
		public static readonly StorePropertyDefinition CalendarInteropActionQueueHasData = new ActionQueueHasDataProperty(InternalSchema.CalendarInteropActionQueueHasDataInternal);

		// Token: 0x04004AAF RID: 19119
		public static readonly StorePropertyDefinition ExtendedFolderFlags = new ExtendedFolderFlagsProperty(ExtendedFolderFlagsProperty.FlagTag.Flags);

		// Token: 0x04004AB0 RID: 19120
		public static readonly StorePropertyDefinition ExtendedFolderToDoVersion = new ExtendedFolderFlagsProperty(ExtendedFolderFlagsProperty.FlagTag.ToDoVersion);

		// Token: 0x04004AB1 RID: 19121
		public static readonly StorePropertyDefinition InternetCpid = new InternetCpidProperty();

		// Token: 0x04004AB2 RID: 19122
		public static readonly StorePropertyDefinition Importance = new ImportanceProperty();

		// Token: 0x04004AB3 RID: 19123
		public static readonly StorePropertyDefinition Sensitivity = new SensitivityProperty();

		// Token: 0x04004AB4 RID: 19124
		public static readonly StorePropertyDefinition IsUnmodified = new FlagsProperty("IsUnmodified", InternalSchema.Flags, 2, PropertyDefinitionConstraint.None);

		// Token: 0x04004AB5 RID: 19125
		public static readonly StorePropertyDefinition IsOutOfDate = new IsOutOfDateProperty();

		// Token: 0x04004AB6 RID: 19126
		public static readonly StorePropertyDefinition EmailRoutingType = new SimpleVirtualPropertyDefinition("EmailAddrType", typeof(string), PropertyFlags.None, new PropertyDefinitionConstraint[0]);

		// Token: 0x04004AB7 RID: 19127
		public static readonly StorePropertyDefinition EmailDisplayName = new SimpleVirtualPropertyDefinition("EmailDisplayName", typeof(string), PropertyFlags.None, new PropertyDefinitionConstraint[0]);

		// Token: 0x04004AB8 RID: 19128
		public static readonly StorePropertyDefinition EmailOriginalDisplayName = new SimpleVirtualPropertyDefinition("EmailOriginalDisplayName", typeof(string), PropertyFlags.None, new PropertyDefinitionConstraint[0]);

		// Token: 0x04004AB9 RID: 19129
		public static readonly StorePropertyDefinition EmailAddressForDisplay = new SimpleVirtualPropertyDefinition("EmailAddressForDisplay", typeof(string), PropertyFlags.None, new PropertyDefinitionConstraint[0]);

		// Token: 0x04004ABA RID: 19130
		public static readonly StorePropertyDefinition ParticipantOriginItemId = new SimpleVirtualPropertyDefinition("ParticipantOriginItemId", typeof(StoreObjectId), PropertyFlags.None, new PropertyDefinitionConstraint[0]);

		// Token: 0x04004ABB RID: 19131
		public static readonly StorePropertyDefinition LegacyExchangeDN = new SimpleVirtualPropertyDefinition("LegacyExchangeDN", typeof(string), PropertyFlags.None, new PropertyDefinitionConstraint[0]);

		// Token: 0x04004ABC RID: 19132
		public static readonly StorePropertyDefinition Alias = new SimpleVirtualPropertyDefinition("Alias", typeof(string), PropertyFlags.None, new PropertyDefinitionConstraint[0]);

		// Token: 0x04004ABD RID: 19133
		public static readonly PropertyTagPropertyDefinition ReadCnNew = PropertyTagPropertyDefinition.InternalCreate("ReadCnNew", PropTag.ReadCnNew);

		// Token: 0x04004ABE RID: 19134
		public static readonly PropertyTagPropertyDefinition SipUri = PropertyTagPropertyDefinition.InternalCreate("SipUri", (PropTag)1608843295U);

		// Token: 0x04004ABF RID: 19135
		public static readonly StorePropertyDefinition DisplayTypeEx = new DisplayTypeExProperty();

		// Token: 0x04004AC0 RID: 19136
		public static readonly StorePropertyDefinition IsDistributionList = new IsDistributionListProperty();

		// Token: 0x04004AC1 RID: 19137
		public static readonly PropertyTagPropertyDefinition IsDistributionListContact = PropertyTagPropertyDefinition.InternalCreate("IsDistributionListContact", PropTag.IsDistributionListContact);

		// Token: 0x04004AC2 RID: 19138
		public static readonly GuidNamePropertyDefinition IsFavorite = InternalSchema.CreateGuidNameProperty("IsFavorite", typeof(bool), WellKnownPropertySet.Address, "IsFavorite");

		// Token: 0x04004AC3 RID: 19139
		public static readonly PropertyTagPropertyDefinition IsPromotedContact = PropertyTagPropertyDefinition.InternalCreate("IsPromotedContact", PropTag.IsPromotedContact);

		// Token: 0x04004AC4 RID: 19140
		public static readonly StorePropertyDefinition IsRoom = new IsRoomProperty();

		// Token: 0x04004AC5 RID: 19141
		public static readonly StorePropertyDefinition IsResource = new IsResourceProperty();

		// Token: 0x04004AC6 RID: 19142
		public static readonly StorePropertyDefinition IsGroupMailbox = new IsGroupMailboxProperty();

		// Token: 0x04004AC7 RID: 19143
		public static readonly StorePropertyDefinition IsMailboxUser = new IsMailboxUserProperty();

		// Token: 0x04004AC8 RID: 19144
		public static readonly IdProperty ItemId = new ItemIdProperty();

		// Token: 0x04004AC9 RID: 19145
		public static readonly IdProperty FolderId = new FolderIdProperty();

		// Token: 0x04004ACA RID: 19146
		public static readonly IdProperty MailboxId = new MailboxIdProperty();

		// Token: 0x04004ACB RID: 19147
		public static readonly StorePropertyDefinition ParentItemId = new FolderItemIdProperty(InternalSchema.ParentEntryId, "ParentUniqueItemId");

		// Token: 0x04004ACC RID: 19148
		public static readonly StorePropertyDefinition IsOrganizer = new IsOrganizerProperty();

		// Token: 0x04004ACD RID: 19149
		public static readonly StorePropertyDefinition BlockStatus = new OutlookBlockStatusProperty();

		// Token: 0x04004ACE RID: 19150
		public static readonly StorePropertyDefinition MeetingMessageResponseType = new MeetingResponseType();

		// Token: 0x04004ACF RID: 19151
		public static readonly SmartPropertyDefinition IsOutlookSearchFolder = new IsOutlookSearchFolderProperty();

		// Token: 0x04004AD0 RID: 19152
		public static readonly SmartPropertyDefinition FolderHomePageUrl = new FolderHomePageUrlProperty();

		// Token: 0x04004AD1 RID: 19153
		public static readonly SmartPropertyDefinition OutlookSearchFolderClsId = new OutlookSearchFolderClsIdProperty();

		// Token: 0x04004AD2 RID: 19154
		public static readonly StorePropertyDefinition DisplayAll = new DisplayXXProperty("DisplayAll");

		// Token: 0x04004AD3 RID: 19155
		public static readonly StorePropertyDefinition DisplayTo = new DisplayXXProperty("DisplayTo", InternalSchema.DisplayToInternal, new RecipientItemType?(RecipientItemType.To));

		// Token: 0x04004AD4 RID: 19156
		public static readonly StorePropertyDefinition DisplayCc = new DisplayXXProperty("DisplayCc", InternalSchema.DisplayCcInternal, new RecipientItemType?(RecipientItemType.Cc));

		// Token: 0x04004AD5 RID: 19157
		public static readonly StorePropertyDefinition DisplayBcc = new DisplayXXProperty("DisplayBcc", InternalSchema.DisplayBccInternal, new RecipientItemType?(RecipientItemType.Bcc));

		// Token: 0x04004AD6 RID: 19158
		public static readonly FileAsStringProperty FileAsString = new FileAsStringProperty();

		// Token: 0x04004AD7 RID: 19159
		public static readonly PhysicalAddressProperty HomeAddress = new PhysicalAddressProperty("HomeAddress", InternalSchema.HomeAddressInternal, InternalSchema.HomeStreet, InternalSchema.HomeCity, InternalSchema.HomeState, InternalSchema.HomePostalCode, InternalSchema.HomeCountry);

		// Token: 0x04004AD8 RID: 19160
		public static readonly PhysicalAddressProperty BusinessAddress = new PhysicalAddressProperty("BusinessAddress", InternalSchema.BusinessAddressInternal, InternalSchema.WorkAddressStreet, InternalSchema.WorkAddressCity, InternalSchema.WorkAddressState, InternalSchema.WorkAddressPostalCode, InternalSchema.WorkAddressCountry);

		// Token: 0x04004AD9 RID: 19161
		public static readonly PhysicalAddressProperty OtherAddress = new PhysicalAddressProperty("OtherAddress", InternalSchema.OtherAddressInternal, InternalSchema.OtherStreet, InternalSchema.OtherCity, InternalSchema.OtherState, InternalSchema.OtherPostalCode, InternalSchema.OtherCountry);

		// Token: 0x04004ADA RID: 19162
		public static readonly PropertyTagPropertyDefinition PredictedActionsInternal = PropertyTagPropertyDefinition.InternalCreate("PredictedActionsInternal", PropTag.PredictedActions);

		// Token: 0x04004ADB RID: 19163
		public static readonly PredictedActionsProperty PredictedActions = new PredictedActionsProperty("PredictedActions", InternalSchema.PredictedActionsInternal, PropertyFlags.None);

		// Token: 0x04004ADC RID: 19164
		public static readonly PropertyTagPropertyDefinition InferencePredictedReplyForwardReasons = PropertyTagPropertyDefinition.InternalCreate("InferencePredictedReplyForwardReasons", PropTag.InferencePredictedReplyForwardReasons);

		// Token: 0x04004ADD RID: 19165
		public static readonly PropertyTagPropertyDefinition InferencePredictedDeleteReasons = PropertyTagPropertyDefinition.InternalCreate("InferencePredictedDeleteReasons", PropTag.InferencePredictedDeleteReasons);

		// Token: 0x04004ADE RID: 19166
		public static readonly PropertyTagPropertyDefinition InferencePredictedIgnoreReasons = PropertyTagPropertyDefinition.InternalCreate("InferencePredictedIgnoreReasons", PropTag.InferencePredictedIgnoreReasons);

		// Token: 0x04004ADF RID: 19167
		public static readonly PropertyTagPropertyDefinition IsClutter = PropertyTagPropertyDefinition.InternalCreate("IsClutter", PropTag.IsClutter);

		// Token: 0x04004AE0 RID: 19168
		public static readonly PropertyTagPropertyDefinition OriginalDeliveryFolderInfo = PropertyTagPropertyDefinition.InternalCreate("OriginalDeliveryFolderInfo", PropTag.OriginalDeliveryFolderInfo);

		// Token: 0x04004AE1 RID: 19169
		public static readonly GuidNamePropertyDefinition XmlExtractedMeetings = InternalSchema.CreateGuidNameProperty("XmlExtractedMeetings", typeof(string), WellKnownPropertySet.Inference, "XmlExtractedMeetings");

		// Token: 0x04004AE2 RID: 19170
		public static readonly GuidNamePropertyDefinition XmlExtractedTasks = InternalSchema.CreateGuidNameProperty("XmlExtractedTasks", typeof(string), WellKnownPropertySet.Inference, "XmlExtractedTasks");

		// Token: 0x04004AE3 RID: 19171
		public static readonly GuidNamePropertyDefinition XmlExtractedKeywords = InternalSchema.CreateGuidNameProperty("XmlExtractedKeywords", typeof(string), WellKnownPropertySet.Inference, "XmlExtractedKeywords");

		// Token: 0x04004AE4 RID: 19172
		public static readonly GuidNamePropertyDefinition XmlExtractedAddresses = InternalSchema.CreateGuidNameProperty("XmlExtractedAddresses", typeof(string), WellKnownPropertySet.Inference, "XmlExtractedAddresses");

		// Token: 0x04004AE5 RID: 19173
		public static readonly GuidNamePropertyDefinition XmlExtractedPhones = InternalSchema.CreateGuidNameProperty("XmlExtractedPhones", typeof(string), WellKnownPropertySet.Inference, "XmlExtractedPhones");

		// Token: 0x04004AE6 RID: 19174
		public static readonly GuidNamePropertyDefinition XmlExtractedEmails = InternalSchema.CreateGuidNameProperty("XmlExtractedEmails", typeof(string), WellKnownPropertySet.Inference, "XmlExtractedEmails");

		// Token: 0x04004AE7 RID: 19175
		public static readonly GuidNamePropertyDefinition XmlExtractedUrls = InternalSchema.CreateGuidNameProperty("XmlExtractedUrls", typeof(string), WellKnownPropertySet.Inference, "XmlExtractedUrls");

		// Token: 0x04004AE8 RID: 19176
		public static readonly GuidNamePropertyDefinition XmlExtractedContacts = InternalSchema.CreateGuidNameProperty("XmlExtractedContacts", typeof(string), WellKnownPropertySet.Inference, "XmlExtractedContacts");

		// Token: 0x04004AE9 RID: 19177
		public static readonly GuidNamePropertyDefinition SenderRelevanceScore = InternalSchema.CreateGuidNameProperty("SenderRelevanceScore", typeof(int), WellKnownPropertySet.Address, "SenderRelevanceScore");

		// Token: 0x04004AEA RID: 19178
		public static readonly GuidNamePropertyDefinition DetectedLanguage = InternalSchema.CreateGuidNameProperty("DetectedLanguage", typeof(string), WellKnownPropertySet.Search, "DetectedLanguage");

		// Token: 0x04004AEB RID: 19179
		public static readonly GuidNamePropertyDefinition IndexingErrorCode = InternalSchema.CreateGuidNameProperty("IndexingErrorCode", typeof(int), WellKnownPropertySet.Search, "IndexingErrorCode");

		// Token: 0x04004AEC RID: 19180
		public static readonly GuidNamePropertyDefinition IsPartiallyIndexed = InternalSchema.CreateGuidNameProperty("IsPartiallyIndexed", typeof(bool), WellKnownPropertySet.Search, "IsPartiallyIndexed");

		// Token: 0x04004AED RID: 19181
		public static readonly GuidNamePropertyDefinition LastIndexingAttemptTime = InternalSchema.CreateGuidNameProperty("LastIndexingAttemptTime", typeof(ExDateTime), WellKnownPropertySet.Search, "LastIndexingAttemptTime");

		// Token: 0x04004AEE RID: 19182
		public static readonly ExtractedNaturalLanguageProperty<Meeting, MeetingSet> ExtractedMeetings = new ExtractedNaturalLanguageProperty<Meeting, MeetingSet>("ExtractedMeetings", InternalSchema.XmlExtractedMeetings);

		// Token: 0x04004AEF RID: 19183
		public static readonly ExtractedNaturalLanguageProperty<Task, TaskSet> ExtractedTasks = new ExtractedNaturalLanguageProperty<Task, TaskSet>("ExtractedTasks", InternalSchema.XmlExtractedTasks);

		// Token: 0x04004AF0 RID: 19184
		public static readonly ExtractedNaturalLanguageProperty<Address, AddressSet> ExtractedAddresses = new ExtractedNaturalLanguageProperty<Address, AddressSet>("ExtractedAddresses", InternalSchema.XmlExtractedAddresses);

		// Token: 0x04004AF1 RID: 19185
		public static readonly ExtractedNaturalLanguageProperty<Keyword, KeywordSet> ExtractedKeywords = new ExtractedNaturalLanguageProperty<Keyword, KeywordSet>("ExtractedKeywords", InternalSchema.XmlExtractedKeywords);

		// Token: 0x04004AF2 RID: 19186
		public static readonly ExtractedNaturalLanguageProperty<Url, UrlSet> ExtractedUrls = new ExtractedNaturalLanguageProperty<Url, UrlSet>("ExtractedUrls", InternalSchema.XmlExtractedUrls);

		// Token: 0x04004AF3 RID: 19187
		public static readonly ExtractedNaturalLanguageProperty<Phone, PhoneSet> ExtractedPhones = new ExtractedNaturalLanguageProperty<Phone, PhoneSet>("ExtractedPhones", InternalSchema.XmlExtractedPhones);

		// Token: 0x04004AF4 RID: 19188
		public static readonly ExtractedNaturalLanguageProperty<Email, EmailSet> ExtractedEmails = new ExtractedNaturalLanguageProperty<Email, EmailSet>("ExtractedEmails", InternalSchema.XmlExtractedEmails);

		// Token: 0x04004AF5 RID: 19189
		public static readonly ExtractedNaturalLanguageProperty<Contact, ContactSet> ExtractedContacts = new ExtractedNaturalLanguageProperty<Contact, ContactSet>("ExtractedContacts", InternalSchema.XmlExtractedContacts);

		// Token: 0x04004AF6 RID: 19190
		public static readonly GuidNamePropertyDefinition XSimSlotNumber = InternalSchema.CreateGuidNameProperty("X-SimSlotNumber", typeof(string), WellKnownPropertySet.InternetHeaders, "X-SimSlotNumber");

		// Token: 0x04004AF7 RID: 19191
		public static readonly GuidNamePropertyDefinition XMmsMessageId = InternalSchema.CreateGuidNameProperty("X-MmsMessageId", typeof(string), WellKnownPropertySet.InternetHeaders, "X-MmsMessageId");

		// Token: 0x04004AF8 RID: 19192
		public static readonly GuidNamePropertyDefinition XSentItem = InternalSchema.CreateGuidNameProperty("X-SentItem", typeof(string), WellKnownPropertySet.InternetHeaders, "X-SentItem");

		// Token: 0x04004AF9 RID: 19193
		public static readonly GuidNamePropertyDefinition XSentTime = InternalSchema.CreateGuidNameProperty("X-SentTime", typeof(string), WellKnownPropertySet.InternetHeaders, "X-SentTime");

		// Token: 0x04004AFA RID: 19194
		public static readonly GuidNamePropertyDefinition ExternalSharingSharerIdentity = InternalSchema.CreateGuidNameProperty("ExternalSharingSharerIdentity", typeof(string), WellKnownPropertySet.ExternalSharing, "ExternalSharingSharerIdentity");

		// Token: 0x04004AFB RID: 19195
		public static readonly GuidNamePropertyDefinition ExternalSharingSharerName = InternalSchema.CreateGuidNameProperty("ExternalSharingSharerName", typeof(string), WellKnownPropertySet.ExternalSharing, "ExternalSharingSharerName");

		// Token: 0x04004AFC RID: 19196
		public static readonly GuidNamePropertyDefinition ExternalSharingRemoteFolderId = InternalSchema.CreateGuidNameProperty("ExternalSharingRemoteFolderId", typeof(string), WellKnownPropertySet.ExternalSharing, "ExternalSharingRemoteFolderId");

		// Token: 0x04004AFD RID: 19197
		public static readonly GuidNamePropertyDefinition ExternalSharingRemoteFolderName = InternalSchema.CreateGuidNameProperty("ExternalSharingRemoteFolderName", typeof(string), WellKnownPropertySet.ExternalSharing, "ExternalSharingRemoteFolderName");

		// Token: 0x04004AFE RID: 19198
		public static readonly GuidNamePropertyDefinition ExternalSharingLevelOfDetails = InternalSchema.CreateGuidNameProperty("ExternalSharingLevelOfDetails", typeof(int), WellKnownPropertySet.ExternalSharing, "ExternalSharingLevelOfDetails");

		// Token: 0x04004AFF RID: 19199
		public static readonly GuidNamePropertyDefinition ExternalSharingIsPrimary = InternalSchema.CreateGuidNameProperty("ExternalSharingIsPrimary", typeof(bool), WellKnownPropertySet.ExternalSharing, "ExternalSharingIsPrimary");

		// Token: 0x04004B00 RID: 19200
		public static readonly GuidNamePropertyDefinition ExternalSharingSharerIdentityFederationUri = InternalSchema.CreateGuidNameProperty("ExternalSharingSharerIdentityFederationUri", typeof(string), WellKnownPropertySet.ExternalSharing, "ExternalSharingSharerIdentityFederationUri");

		// Token: 0x04004B01 RID: 19201
		public static readonly GuidNamePropertyDefinition ExternalSharingUrl = InternalSchema.CreateGuidNameProperty("ExternalSharingUrl", typeof(string), WellKnownPropertySet.ExternalSharing, "ExternalSharingUrl");

		// Token: 0x04004B02 RID: 19202
		public static readonly GuidNamePropertyDefinition ExternalSharingLocalFolderId = InternalSchema.CreateGuidNameProperty("ExternalSharingLocalFolderId", typeof(byte[]), WellKnownPropertySet.ExternalSharing, "ExternalSharingLocalFolderId");

		// Token: 0x04004B03 RID: 19203
		public static readonly GuidNamePropertyDefinition ExternalSharingDataType = InternalSchema.CreateGuidNameProperty("ExternalSharingDataType", typeof(string), WellKnownPropertySet.ExternalSharing, "ExternalSharingDataType");

		// Token: 0x04004B04 RID: 19204
		public static readonly GuidNamePropertyDefinition ExternalSharingSharingKey = InternalSchema.CreateGuidNameProperty("ExternalSharingSharingKey", typeof(string), WellKnownPropertySet.ExternalSharing, "ExternalSharingSharingKey");

		// Token: 0x04004B05 RID: 19205
		public static readonly GuidNamePropertyDefinition ExternalSharingSubscriberIdentity = InternalSchema.CreateGuidNameProperty("ExternalSharingSubscriberIdentity", typeof(string), WellKnownPropertySet.ExternalSharing, "ExternalSharingSubscriberIdentity");

		// Token: 0x04004B06 RID: 19206
		public static readonly GuidNamePropertyDefinition ExternalSharingMasterId = InternalSchema.CreateGuidNameProperty("ExternalSharingMasterId", typeof(string), WellKnownPropertySet.ExternalSharing, "ExternalSharingMasterId");

		// Token: 0x04004B07 RID: 19207
		public static readonly GuidNamePropertyDefinition ExternalSharingSyncState = InternalSchema.CreateGuidNameProperty("ExternalSharingSyncState", typeof(string), WellKnownPropertySet.ExternalSharing, "ExternalSharingSyncState", PropertyFlags.Streamable, new PropertyDefinitionConstraint[0]);

		// Token: 0x04004B08 RID: 19208
		public static readonly GuidNamePropertyDefinition SubscriptionLastSuccessfulSyncTime = InternalSchema.CreateGuidNameProperty("ExternalSharingLastSuccessfulSyncTime", typeof(ExDateTime), WellKnownPropertySet.ExternalSharing, "ExternalSharingLastSuccessfulSyncTime");

		// Token: 0x04004B09 RID: 19209
		public static readonly PropertyTagPropertyDefinition MimeSkeleton = PropertyTagPropertyDefinition.InternalCreate("MimeSkeleton", (PropTag)1693450498U, PropertyFlags.Streamable);

		// Token: 0x04004B0A RID: 19210
		public static readonly GuidNamePropertyDefinition ExchangeApplicationFlags = InternalSchema.CreateGuidNameProperty("ExchangeApplicationFlags", typeof(int), WellKnownPropertySet.Common, "ExchangeApplicationFlags");

		// Token: 0x04004B0B RID: 19211
		public static readonly StorePropertyDefinition ConversationId = new ConversationIdFromIndexProperty();

		// Token: 0x04004B0C RID: 19212
		public static readonly PropertyTagPropertyDefinition ConversationTopicHashEntries = PropertyTagPropertyDefinition.InternalCreate("ConversationTopicHashEntries", PropTag.ConversationTopicHashEntries);

		// Token: 0x04004B0D RID: 19213
		public static readonly PropertyTagPropertyDefinition MapiConversationFamilyId = PropertyTagPropertyDefinition.InternalCreate("MapiConversationFamilyId", PropTag.ConversationFamilyId);

		// Token: 0x04004B0E RID: 19214
		public static readonly StorePropertyDefinition ConversationFamilyId = new ConversationFamilyIdProperty();

		// Token: 0x04004B0F RID: 19215
		public static readonly StorePropertyDefinition MessageSentRepresentingType = new MessageSentRepresentingTypeProperty();

		// Token: 0x04004B10 RID: 19216
		public static readonly StorePropertyDefinition ConversationFamilyIndex = new ConversationFamilyIndexProperty();

		// Token: 0x04004B11 RID: 19217
		public static readonly StorePropertyDefinition IsFromFavoriteSender = new ExchangeApplicationFlagsProperty(Microsoft.Exchange.Data.Storage.ExchangeApplicationFlags.IsFromFavoriteSender);

		// Token: 0x04004B12 RID: 19218
		public static readonly StorePropertyDefinition IsFromPerson = new ExchangeApplicationFlagsProperty(Microsoft.Exchange.Data.Storage.ExchangeApplicationFlags.IsFromPerson);

		// Token: 0x04004B13 RID: 19219
		public static readonly StorePropertyDefinition IsSpecificMessageReplyStamped = new ExchangeApplicationFlagsProperty(Microsoft.Exchange.Data.Storage.ExchangeApplicationFlags.IsSpecificMessageReplyStamped);

		// Token: 0x04004B14 RID: 19220
		public static readonly StorePropertyDefinition IsSpecificMessageReply = new ExchangeApplicationFlagsProperty(Microsoft.Exchange.Data.Storage.ExchangeApplicationFlags.IsSpecificMessageReply);

		// Token: 0x04004B15 RID: 19221
		public static readonly StorePropertyDefinition RelyOnConversationIndex = new ExchangeApplicationFlagsProperty(Microsoft.Exchange.Data.Storage.ExchangeApplicationFlags.RelyOnConversationIndex);

		// Token: 0x04004B16 RID: 19222
		public static readonly StorePropertyDefinition SupportsSideConversation = new ExchangeApplicationFlagsProperty(Microsoft.Exchange.Data.Storage.ExchangeApplicationFlags.SupportsSideConversation);

		// Token: 0x04004B17 RID: 19223
		public static readonly StorePropertyDefinition IsGroupEscalationMessage = new ExchangeApplicationFlagsProperty(Microsoft.Exchange.Data.Storage.ExchangeApplicationFlags.IsGroupEscalationMessage);

		// Token: 0x04004B18 RID: 19224
		public static readonly StorePropertyDefinition IsClutterOverridden = new ExchangeApplicationFlagsProperty(Microsoft.Exchange.Data.Storage.ExchangeApplicationFlags.IsClutterOverridden);

		// Token: 0x04004B19 RID: 19225
		public static readonly GuidIdPropertyDefinition ConversationActionMoveFolderId = InternalSchema.CreateGuidIdProperty("ConversationActionMoveFolderId", typeof(byte[]), WellKnownPropertySet.Common, 34246);

		// Token: 0x04004B1A RID: 19226
		public static readonly GuidIdPropertyDefinition ConversationActionMoveStoreId = InternalSchema.CreateGuidIdProperty("ConversationActionMoveStoreId", typeof(byte[]), WellKnownPropertySet.Common, 34247);

		// Token: 0x04004B1B RID: 19227
		public static readonly GuidIdPropertyDefinition ConversationActionMaxDeliveryTime = InternalSchema.CreateGuidIdProperty("ConversationActionMaxDeliveryTime", typeof(ExDateTime), WellKnownPropertySet.Common, 34248);

		// Token: 0x04004B1C RID: 19228
		public static readonly GuidIdPropertyDefinition ConversationActionVersion = InternalSchema.CreateGuidIdProperty("ConversationActionVersion", typeof(int), WellKnownPropertySet.Common, 34251);

		// Token: 0x04004B1D RID: 19229
		public static readonly GuidIdPropertyDefinition ConversationActionPolicyTag = InternalSchema.CreateGuidIdProperty("ConversationActionPolicyTag", typeof(byte[]), WellKnownPropertySet.Common, 34254);

		// Token: 0x04004B1E RID: 19230
		public static readonly GuidNamePropertyDefinition ConversationActionLastMoveFolderId = InternalSchema.CreateGuidNameProperty("ConversationActionLastMoveFolderId", typeof(byte[]), WellKnownPropertySet.Conversations, "ConversationActionLastMoveFolderId");

		// Token: 0x04004B1F RID: 19231
		public static readonly GuidNamePropertyDefinition ConversationActionLastCategorySet = InternalSchema.CreateGuidNameProperty("ConversationActionLastCategorySet", typeof(string[]), WellKnownPropertySet.Conversations, "ConversationActionLastCategorySet", PropertyFlags.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(1, 255),
			new CharacterConstraint(Category.ProhibitedCharacters, false)
		});

		// Token: 0x04004B20 RID: 19232
		public static readonly PropertyTagPropertyDefinition ActivityId = PropertyTagPropertyDefinition.InternalCreate("ActivityId", PropTag.InferenceActivityId);

		// Token: 0x04004B21 RID: 19233
		public static readonly PropertyTagPropertyDefinition ActivityItemIdBytes = PropertyTagPropertyDefinition.InternalCreate("ActivityItemIdBytes", PropTag.InferenceItemId);

		// Token: 0x04004B22 RID: 19234
		public static readonly PropertyTagPropertyDefinition ActivityTimeStamp = PropertyTagPropertyDefinition.InternalCreate("ActivityTimeStamp", PropTag.InferenceTimeStamp);

		// Token: 0x04004B23 RID: 19235
		public static readonly PropertyTagPropertyDefinition ActivityClientId = PropertyTagPropertyDefinition.InternalCreate("ActivityClientId", PropTag.InferenceClientId);

		// Token: 0x04004B24 RID: 19236
		public static readonly PropertyTagPropertyDefinition ActivityWindowId = PropertyTagPropertyDefinition.InternalCreate("ActivityWindowId", PropTag.InferenceWindowId);

		// Token: 0x04004B25 RID: 19237
		public static readonly PropertyTagPropertyDefinition ActivitySessionId = PropertyTagPropertyDefinition.InternalCreate("ActivitySessionId", PropTag.InferenceSessionId);

		// Token: 0x04004B26 RID: 19238
		public static readonly PropertyTagPropertyDefinition ActivityFolderIdBytes = PropertyTagPropertyDefinition.InternalCreate("ActivityFolderIdBytes", PropTag.InferenceFolderId);

		// Token: 0x04004B27 RID: 19239
		public static readonly PropertyTagPropertyDefinition ActivityDeleteType = PropertyTagPropertyDefinition.InternalCreate("ActivityDeleteType", PropTag.InferenceDeleteType);

		// Token: 0x04004B28 RID: 19240
		public static readonly PropertyTagPropertyDefinition ActivityOofEnabled = PropertyTagPropertyDefinition.InternalCreate("ActivityOofEnabled", PropTag.InferenceOofEnabled);

		// Token: 0x04004B29 RID: 19241
		public static readonly PropertyTagPropertyDefinition ActivityBrowser = PropertyTagPropertyDefinition.InternalCreate("ActivityBrowser", PropTag.MemberEmail);

		// Token: 0x04004B2A RID: 19242
		public static readonly PropertyTagPropertyDefinition ActivityLocaleId = PropertyTagPropertyDefinition.InternalCreate("ActivityLocaleId", PropTag.InferenceLocaleId);

		// Token: 0x04004B2B RID: 19243
		public static readonly PropertyTagPropertyDefinition ActivityLocation = PropertyTagPropertyDefinition.InternalCreate("ActivityLocation", PropTag.InferenceLocation);

		// Token: 0x04004B2C RID: 19244
		public static readonly PropertyTagPropertyDefinition ActivityConversationId = PropertyTagPropertyDefinition.InternalCreate("ActivityConversationId", PropTag.InferenceConversationId);

		// Token: 0x04004B2D RID: 19245
		public static readonly PropertyTagPropertyDefinition ActivityTimeZone = PropertyTagPropertyDefinition.InternalCreate("ActivityTimeZone", PropTag.InferenceTimeZone);

		// Token: 0x04004B2E RID: 19246
		public static readonly PropertyTagPropertyDefinition ActivityIpAddress = PropertyTagPropertyDefinition.InternalCreate("ActivityIpAddress", PropTag.InferenceIpAddress);

		// Token: 0x04004B2F RID: 19247
		public static readonly PropertyTagPropertyDefinition ActivityCategory = PropertyTagPropertyDefinition.InternalCreate("ActivityCategory", PropTag.InferenceCategory);

		// Token: 0x04004B30 RID: 19248
		public static readonly PropertyTagPropertyDefinition ActivityAttachmentIdBytes = PropertyTagPropertyDefinition.InternalCreate("ActivityAttachmentIdBytes", PropTag.InferenceAttachmentId);

		// Token: 0x04004B31 RID: 19249
		public static readonly PropertyTagPropertyDefinition ActivityGlobalObjectIdBytes = PropertyTagPropertyDefinition.InternalCreate("ActivityGlobalObjectIdBytes", PropTag.InferenceGlobalObjectId);

		// Token: 0x04004B32 RID: 19250
		public static readonly PropertyTagPropertyDefinition ActivityModuleSelected = PropertyTagPropertyDefinition.InternalCreate("ActivityModuleSelected", PropTag.InferenceModuleSelected);

		// Token: 0x04004B33 RID: 19251
		public static readonly PropertyTagPropertyDefinition ActivityLayoutType = PropertyTagPropertyDefinition.InternalCreate("ActivityLayoutType", PropTag.InferenceLayoutType);

		// Token: 0x04004B34 RID: 19252
		public static readonly EmbeddedParticipantProperty ReceivedBy = new EmbeddedParticipantProperty("ReceivedBy", ParticipantEntryIdConsumer.SupportsADParticipantEntryId, InternalSchema.ReceivedByName, InternalSchema.ReceivedByEmailAddress, InternalSchema.ReceivedByAddrType, InternalSchema.ReceivedByEntryId, InternalSchema.ReceivedBySmtpAddress, null, null, null);

		// Token: 0x04004B35 RID: 19253
		public static readonly EmbeddedParticipantProperty ReceivedRepresenting = new EmbeddedParticipantProperty("ReceivedRepresenting", ParticipantEntryIdConsumer.SupportsADParticipantEntryId, InternalSchema.ReceivedRepresentingDisplayName, InternalSchema.ReceivedRepresentingEmailAddress, InternalSchema.ReceivedRepresentingAddressType, InternalSchema.ReceivedRepresentingEntryId, InternalSchema.ReceivedRepresentingSmtpAddress, null, null, null);

		// Token: 0x04004B36 RID: 19254
		public static readonly SenderParticipantProperty Sender = new SenderParticipantProperty("Sender", InternalSchema.SenderDisplayName, InternalSchema.SenderEmailAddress, InternalSchema.SenderAddressType, InternalSchema.SenderEntryId, InternalSchema.SenderSmtpAddress, null, InternalSchema.SenderSID, null);

		// Token: 0x04004B37 RID: 19255
		public static readonly SenderParticipantProperty From = new SenderParticipantProperty("From", InternalSchema.SentRepresentingDisplayName, InternalSchema.SentRepresentingEmailAddress, InternalSchema.SentRepresentingType, InternalSchema.SentRepresentingEntryId, InternalSchema.SentRepresentingSmtpAddress, InternalSchema.SipUri, InternalSchema.SentRepresentingSID, null);

		// Token: 0x04004B38 RID: 19256
		public static readonly EmbeddedParticipantProperty OriginalFrom = new EmbeddedParticipantProperty("OriginalFrom", ParticipantEntryIdConsumer.SupportsADParticipantEntryId, InternalSchema.OriginalSentRepresentingDisplayName, InternalSchema.OriginalSentRepresentingEmailAddress, InternalSchema.OriginalSentRepresentingAddressType, InternalSchema.OriginalSentRepresentingEntryId, InternalSchema.OriginalSentRepresentingSmtpAddress, null, null, null);

		// Token: 0x04004B39 RID: 19257
		public static readonly EmbeddedParticipantProperty OriginalSender = new EmbeddedParticipantProperty("OriginalSender", ParticipantEntryIdConsumer.SupportsADParticipantEntryId, InternalSchema.OriginalSenderDisplayName, InternalSchema.OriginalSenderEmailAddress, InternalSchema.OriginalSenderAddressType, InternalSchema.OriginalSenderEntryId, InternalSchema.OriginalSenderSmtpAddress, null, null, null);

		// Token: 0x04004B3A RID: 19258
		public static readonly EmbeddedParticipantProperty OriginalAuthor = new EmbeddedParticipantProperty("OriginalAuthor", ParticipantEntryIdConsumer.SupportsADParticipantEntryId, InternalSchema.OriginalAuthorName, InternalSchema.OriginalAuthorEmailAddress, InternalSchema.OriginalAuthorAddressType, InternalSchema.OriginalAuthorEntryId, InternalSchema.OriginalAuthorSmtpAddress, null, null, null);

		// Token: 0x04004B3B RID: 19259
		public static readonly EmbeddedParticipantProperty ReadReceiptAddressee = new EmbeddedParticipantProperty("ReadReceiptAddressee", ParticipantEntryIdConsumer.SupportsADParticipantEntryId, InternalSchema.ReadReceiptDisplayName, InternalSchema.ReadReceiptEmailAddress, InternalSchema.ReadReceiptAddrType, InternalSchema.ReadReceiptEntryId, InternalSchema.ReadReceiptSmtpAddress, null, null, null);

		// Token: 0x04004B3C RID: 19260
		public static readonly EmbeddedParticipantProperty RecipientBaseParticipant = new RecipientBaseParticipantProperty();

		// Token: 0x04004B3D RID: 19261
		public static readonly ContactEmailSlotParticipantProperty ContactEmail1 = new ContactEmailSlotParticipantProperty(EmailAddressIndex.Email1, InternalSchema.Email1DisplayName, InternalSchema.Email1EmailAddress, InternalSchema.Email1AddrType, InternalSchema.Email1OriginalEntryID, InternalSchema.Email1OriginalDisplayName, new PropertyDependency[0]);

		// Token: 0x04004B3E RID: 19262
		public static readonly ContactEmailSlotParticipantProperty ContactEmail2 = new ContactEmailSlotParticipantProperty(EmailAddressIndex.Email2, InternalSchema.Email2DisplayName, InternalSchema.Email2EmailAddress, InternalSchema.Email2AddrType, InternalSchema.Email2OriginalEntryID, InternalSchema.Email2OriginalDisplayName, new PropertyDependency[0]);

		// Token: 0x04004B3F RID: 19263
		public static readonly ContactEmailSlotParticipantProperty ContactEmail3 = new ContactEmailSlotParticipantProperty(EmailAddressIndex.Email3, InternalSchema.Email3DisplayName, InternalSchema.Email3EmailAddress, InternalSchema.Email3AddrType, InternalSchema.Email3OriginalEntryID, InternalSchema.Email3OriginalDisplayName, new PropertyDependency[0]);

		// Token: 0x04004B40 RID: 19264
		public static readonly ContactFaxSlotParticipantProperty ContactOtherFax = new ContactFaxSlotParticipantProperty(EmailAddressIndex.OtherFax, InternalSchema.Fax1OriginalDisplayName, InternalSchema.Fax1EmailAddress, InternalSchema.Fax1AddrType, InternalSchema.Fax1OriginalEntryID, InternalSchema.OtherFax);

		// Token: 0x04004B41 RID: 19265
		public static readonly ContactFaxSlotParticipantProperty ContactBusinessFax = new ContactFaxSlotParticipantProperty(EmailAddressIndex.BusinessFax, InternalSchema.Fax2OriginalDisplayName, InternalSchema.Fax2EmailAddress, InternalSchema.Fax2AddrType, InternalSchema.Fax2OriginalEntryID, InternalSchema.FaxNumber);

		// Token: 0x04004B42 RID: 19266
		public static readonly ContactFaxSlotParticipantProperty ContactHomeFax = new ContactFaxSlotParticipantProperty(EmailAddressIndex.HomeFax, InternalSchema.Fax3OriginalDisplayName, InternalSchema.Fax3EmailAddress, InternalSchema.Fax3AddrType, InternalSchema.Fax3OriginalEntryID, InternalSchema.HomeFax);

		// Token: 0x04004B43 RID: 19267
		public static readonly DistributionListParticipantProperty DistributionListParticipant = new DistributionListParticipantProperty();

		// Token: 0x04004B44 RID: 19268
		public static readonly SimpleVirtualPropertyDefinition AnrViewParticipant = new SimpleVirtualPropertyDefinition("ItemAsParticipant", typeof(Participant), PropertyFlags.ReadOnly, new PropertyDefinitionConstraint[0]);

		// Token: 0x04004B45 RID: 19269
		public static readonly StorePropertyDefinition PrimarySmtpAddress = new PrimarySmtpAddressProperty();

		// Token: 0x04004B46 RID: 19270
		public static readonly PropertyTagPropertyDefinition LogonRightsOnMailbox = PropertyTagPropertyDefinition.InternalCreate("LogonRightsOnMailbox", (PropTag)1736245251U);

		// Token: 0x04004B47 RID: 19271
		public static readonly StorePropertyDefinition CanActAsOwner = new FlagsProperty("CanActAsOwner", InternalSchema.LogonRightsOnMailbox, 1, PropertyDefinitionConstraint.None);

		// Token: 0x04004B48 RID: 19272
		public static readonly StorePropertyDefinition CanSendAs = new FlagsProperty("CanSendAs", InternalSchema.LogonRightsOnMailbox, 2, PropertyDefinitionConstraint.None);

		// Token: 0x04004B49 RID: 19273
		public static readonly PropertyTagPropertyDefinition MergeMidsetDeleted = PropertyTagPropertyDefinition.InternalCreate("MergeMidsetDeleted", (PropTag)242876674U);

		// Token: 0x04004B4A RID: 19274
		public static readonly PropertyTagPropertyDefinition MailboxMiscFlags = PropertyTagPropertyDefinition.InternalCreate("MailboxMiscFlags", PropTag.MailboxMiscFlags);

		// Token: 0x04004B4B RID: 19275
		public static readonly PropertyTagPropertyDefinition MailboxGuid = PropertyTagPropertyDefinition.InternalCreate("MailboxGuid", PropTag.UserGuid);

		// Token: 0x04004B4C RID: 19276
		public static readonly PropertyTagPropertyDefinition MailboxNumber = PropertyTagPropertyDefinition.InternalCreate("MailboxNumber", PropTag.MailboxNumber);

		// Token: 0x04004B4D RID: 19277
		public static readonly PropertyTagPropertyDefinition InTransitStatus = PropertyTagPropertyDefinition.InternalCreate("InTransitStatus", PropTag.InTransitStatus);

		// Token: 0x04004B4E RID: 19278
		public static readonly StorePropertyDefinition PublicFolderFreeBusy = new PublicFolderFreeBusyProperty();

		// Token: 0x04004B4F RID: 19279
		public static readonly ReplyAllDisplayNamesProperty ReplyAllDisplayNames = new ReplyAllDisplayNamesProperty();

		// Token: 0x04004B50 RID: 19280
		public static readonly ReplyAllParticipantsProperty ReplyAllParticipants = new ReplyAllParticipantsProperty();

		// Token: 0x04004B51 RID: 19281
		public static readonly ReplyDisplayNamesProperty ReplyDisplayNames = new ReplyDisplayNamesProperty();

		// Token: 0x04004B52 RID: 19282
		public static StorePropertyDefinition LastDelegatedAuditTime = new SimpleVirtualPropertyDefinition("LastDelegatedAuditTime", typeof(ExDateTime), PropertyFlags.None, new PropertyDefinitionConstraint[0]);

		// Token: 0x04004B53 RID: 19283
		public static StorePropertyDefinition LastExternalAuditTime = new SimpleVirtualPropertyDefinition("LastExternalAuditTime", typeof(ExDateTime), PropertyFlags.None, new PropertyDefinitionConstraint[0]);

		// Token: 0x04004B54 RID: 19284
		public static StorePropertyDefinition LastNonOwnerAuditTime = new SimpleVirtualPropertyDefinition("LastNonOwnerAuditTime", typeof(ExDateTime), PropertyFlags.None, new PropertyDefinitionConstraint[0]);

		// Token: 0x04004B55 RID: 19285
		public static readonly PropertyTagPropertyDefinition UnsearchableItemsStream = PropertyTagPropertyDefinition.InternalCreate("UnsearchableItemsStream", PropTag.UnsearchableItemsStream, PropertyFlags.ReadOnly | PropertyFlags.Streamable);

		// Token: 0x04004B56 RID: 19286
		public static readonly PropertyTagPropertyDefinition AnnotationToken = PropertyTagPropertyDefinition.InternalCreate("AnnotationToken", PropTag.AnnotationToken, PropertyFlags.Streamable);

		// Token: 0x04004B57 RID: 19287
		public static readonly PersonIdProperty PersonId = new PersonIdProperty();

		// Token: 0x04004B58 RID: 19288
		public static readonly BirthdayContactIdProperty BirthdayContactId = new BirthdayContactIdProperty();

		// Token: 0x04004B59 RID: 19289
		public static readonly AttributionDisplayNameProperty AttributionDisplayName = new AttributionDisplayNameProperty();

		// Token: 0x04004B5A RID: 19290
		public static readonly IsContactWritableProperty IsContactWritable = new IsContactWritableProperty();

		// Token: 0x04004B5B RID: 19291
		public static readonly PropertyTagPropertyDefinition ResolveMethod = PropertyTagPropertyDefinition.InternalCreate("PR_RESOLVE_METHOD", (PropTag)1072103427U);

		// Token: 0x04004B5C RID: 19292
		public static readonly PropertyTagPropertyDefinition ReplicaListBinary = PropertyTagPropertyDefinition.InternalCreate("ReplicaListBinary", PropTag.ReplicaList);

		// Token: 0x04004B5D RID: 19293
		public static readonly ReplicaListProperty ReplicaList = new ReplicaListProperty();

		// Token: 0x04004B5E RID: 19294
		public static readonly PropertyTagPropertyDefinition CorrelationId = PropertyTagPropertyDefinition.InternalCreate("CorrelationId", PropTag.CorrelationId, PropertyFlags.ReadOnly);

		// Token: 0x04004B5F RID: 19295
		public static readonly GuidNamePropertyDefinition UnifiedInboxFolderEntryId = InternalSchema.CreateGuidNameProperty("UnifiedInboxFolderEntryId", typeof(byte[]), WellKnownPropertySet.Address, "UnifiedInboxFolderEntryId");

		// Token: 0x04004B60 RID: 19296
		public static readonly GuidNamePropertyDefinition TemporarySavesFolderEntryId = InternalSchema.CreateGuidNameProperty("TemporarySavesFolderEntryId", typeof(byte[]), WellKnownPropertySet.Common, "TemporarySavesFolderEntryId");

		// Token: 0x04004B61 RID: 19297
		public static readonly PropertyTagPropertyDefinition ItemsPendingUpgrade = PropertyTagPropertyDefinition.InternalCreate("ItemsPendingUpgrade", PropTag.ItemsPendingUpgrade);

		// Token: 0x04004B62 RID: 19298
		public static readonly PropertyTagPropertyDefinition LastLogonTime = PropertyTagPropertyDefinition.InternalCreate("LastLogonTime", PropTag.LastLogonTime);

		// Token: 0x04004B63 RID: 19299
		public static readonly GuidNamePropertyDefinition ExtractionResult = InternalSchema.CreateGuidNameProperty("ExtractionResult", typeof(string), WellKnownPropertySet.Inference, "ExtractionResult");

		// Token: 0x04004B64 RID: 19300
		public static readonly GuidNamePropertyDefinition TriageFeatureVector = InternalSchema.CreateGuidNameProperty("TriageFeatureVector", typeof(byte[]), WellKnownPropertySet.Inference, "TriageFeatureVector", PropertyFlags.Streamable, new PropertyDefinitionConstraint[0]);

		// Token: 0x04004B65 RID: 19301
		public static readonly GuidNamePropertyDefinition InferenceClassificationTrackingEx = InternalSchema.CreateGuidNameProperty("InferenceClassificationTrackingEx", typeof(string), WellKnownPropertySet.Inference, "InferenceClassificationTrackingEx");

		// Token: 0x04004B66 RID: 19302
		public static readonly GuidNamePropertyDefinition LatestMessageWordCount = InternalSchema.CreateGuidNameProperty("LatestMessageWordCount", typeof(int), WellKnownPropertySet.Inference, "LatestMessageWordCount");

		// Token: 0x04004B67 RID: 19303
		public static readonly GuidNamePropertyDefinition UnClutteredViewFolderEntryId = InternalSchema.CreateGuidNameProperty("UnClutteredViewFolderEntryId", typeof(byte[]), WellKnownPropertySet.Inference, "UnClutteredViewFolderEntryId");

		// Token: 0x04004B68 RID: 19304
		public static readonly GuidNamePropertyDefinition ClutteredViewFolderEntryId = InternalSchema.CreateGuidNameProperty("ClutteredViewFolderEntryId", typeof(byte[]), WellKnownPropertySet.Inference, "ClutteredViewFolderEntryId");

		// Token: 0x04004B69 RID: 19305
		public static readonly GuidNamePropertyDefinition InferenceProcessingNeeded = InternalSchema.CreateGuidNameProperty("InferenceProcessingNeeded", typeof(bool), WellKnownPropertySet.Inference, "InferenceProcessingNeeded");

		// Token: 0x04004B6A RID: 19306
		public static readonly GuidNamePropertyDefinition InferenceProcessingActions = InternalSchema.CreateGuidNameProperty("InferenceProcessingActions", typeof(long), WellKnownPropertySet.Inference, "InferenceProcessingActions");

		// Token: 0x04004B6B RID: 19307
		public static readonly GuidNamePropertyDefinition UserActivityFolderEntryId = InternalSchema.CreateGuidNameProperty("UserActivityFolderEntryId", typeof(byte[]), WellKnownPropertySet.Inference, "UserActivityFolderEntryId");

		// Token: 0x04004B6C RID: 19308
		public static readonly GuidNamePropertyDefinition ClutterFolderEntryId = InternalSchema.CreateGuidNameProperty("ClutterFolderEntryId", typeof(byte[]), WellKnownPropertySet.Inference, "ClutterFolderEntryId");

		// Token: 0x04004B6D RID: 19309
		public static readonly GuidNamePropertyDefinition ConversationLoadRequiredByInference = InternalSchema.CreateGuidNameProperty("ConversationLoadRequiredByInference", typeof(bool), WellKnownPropertySet.Inference, "ConversationLoadRequiredByInference");

		// Token: 0x04004B6E RID: 19310
		public static readonly GuidNamePropertyDefinition InferenceActionTruth = InternalSchema.CreateGuidNameProperty("InferenceActionTruth", typeof(int), WellKnownPropertySet.Inference, "InferenceActionTruth");

		// Token: 0x04004B6F RID: 19311
		public static readonly GuidNamePropertyDefinition InferenceConversationClutterActionApplied = InternalSchema.CreateGuidNameProperty("InferenceConversationClutterActionApplied", typeof(bool), WellKnownPropertySet.Inference, "InferenceConversationClutterActionApplied");

		// Token: 0x04004B70 RID: 19312
		public static readonly GuidNamePropertyDefinition InferenceNeverClutterOverrideApplied = InternalSchema.CreateGuidNameProperty("InferenceNeverClutterOverrideApplied", typeof(bool), WellKnownPropertySet.Inference, "InferenceNeverClutterOverrideApplied");

		// Token: 0x04004B71 RID: 19313
		public static readonly GuidNamePropertyDefinition InferenceClassificationResult = InternalSchema.CreateGuidNameProperty("InferenceClassificationResult", typeof(int), WellKnownPropertySet.Inference, "InferenceClassificationResult");

		// Token: 0x04004B72 RID: 19314
		public static readonly GuidNamePropertyDefinition ItemMovedByRule = InternalSchema.CreateGuidNameProperty("ItemMovedByRule", typeof(bool), WellKnownPropertySet.Common, "ItemMovedByRule");

		// Token: 0x04004B73 RID: 19315
		public static readonly GuidNamePropertyDefinition ItemMovedByConversationAction = InternalSchema.CreateGuidNameProperty("ItemMovedByConversationAction", typeof(bool), WellKnownPropertySet.Common, "ItemMovedByConversationAction");

		// Token: 0x04004B74 RID: 19316
		public static readonly GuidNamePropertyDefinition IsStopProcessingRuleApplicable = InternalSchema.CreateGuidNameProperty("IsStopProcessingRuleApplicable", typeof(bool), WellKnownPropertySet.Common, "IsStopProcessingRuleApplicable");

		// Token: 0x04004B75 RID: 19317
		public static readonly GuidNamePropertyDefinition InferenceMessageIdentifier = InternalSchema.CreateGuidNameProperty("InferenceMessageIdentifier", typeof(Guid), WellKnownPropertySet.Inference, "InferenceMessageIdentifier");

		// Token: 0x04004B76 RID: 19318
		public static readonly GuidNamePropertyDefinition InferenceUniqueActionLabelData = InternalSchema.CreateGuidNameProperty("InferenceUniqueActionLabelData", typeof(byte[]), WellKnownPropertySet.Inference, "InferenceUniqueActionLabelData");

		// Token: 0x04004B77 RID: 19319
		public static readonly GuidNamePropertyDefinition NeedGroupExpansion = InternalSchema.CreateGuidNameProperty("NeedGroupExpansion", typeof(bool), WellKnownPropertySet.Compliance, "NeedGroupExpansion");

		// Token: 0x04004B78 RID: 19320
		public static readonly GuidNamePropertyDefinition GroupExpansionRecipients = InternalSchema.CreateGuidNameProperty("GroupExpansionRecipients", typeof(string), WellKnownPropertySet.Compliance, "GroupExpansionRecipients");

		// Token: 0x04004B79 RID: 19321
		public static readonly GuidNamePropertyDefinition GroupExpansionError = InternalSchema.CreateGuidNameProperty("GroupExpansionError", typeof(int), WellKnownPropertySet.Compliance, "GroupExpansionError");

		// Token: 0x04004B7A RID: 19322
		public static readonly PropertyTagPropertyDefinition ToGroupExpansionRecipientsInternal = PropertyTagPropertyDefinition.InternalCreate("ToGroupExpansionRecipientsInternal", PropTag.ToGroupExpansionRecipients, PropertyFlags.ReadOnly | PropertyFlags.Streamable);

		// Token: 0x04004B7B RID: 19323
		public static readonly PropertyTagPropertyDefinition CcGroupExpansionRecipientsInternal = PropertyTagPropertyDefinition.InternalCreate("CcGroupExpansionRecipientsInternal", PropTag.CcGroupExpansionRecipients, PropertyFlags.ReadOnly | PropertyFlags.Streamable);

		// Token: 0x04004B7C RID: 19324
		public static readonly PropertyTagPropertyDefinition BccGroupExpansionRecipientsInternal = PropertyTagPropertyDefinition.InternalCreate("BccGroupExpansionRecipientsInternal", PropTag.BccGroupExpansionRecipients, PropertyFlags.ReadOnly | PropertyFlags.Streamable);

		// Token: 0x04004B7D RID: 19325
		public static readonly StorePropertyDefinition ToGroupExpansionRecipients = new RecipientToIndexProperty("ToGroupExpansionRecipients", new RecipientItemType?(RecipientItemType.To), InternalSchema.ToGroupExpansionRecipientsInternal);

		// Token: 0x04004B7E RID: 19326
		public static readonly StorePropertyDefinition CcGroupExpansionRecipients = new RecipientToIndexProperty("CcGroupExpansionRecipients", new RecipientItemType?(RecipientItemType.Cc), InternalSchema.CcGroupExpansionRecipientsInternal);

		// Token: 0x04004B7F RID: 19327
		public static readonly StorePropertyDefinition BccGroupExpansionRecipients = new RecipientToIndexProperty("BccGroupExpansionRecipients", new RecipientItemType?(RecipientItemType.Bcc), InternalSchema.BccGroupExpansionRecipientsInternal);

		// Token: 0x04004B80 RID: 19328
		public static readonly GuidNamePropertyDefinition WorkingSetId = InternalSchema.CreateGuidNameProperty("WorkingSetId", typeof(string), WellKnownPropertySet.WorkingSet, "WorkingSetId");

		// Token: 0x04004B81 RID: 19329
		public static readonly GuidNamePropertyDefinition WorkingSetSource = InternalSchema.CreateGuidNameProperty("WorkingSetSource", typeof(int), WellKnownPropertySet.WorkingSet, "WorkingSetSource");

		// Token: 0x04004B82 RID: 19330
		public static readonly GuidNamePropertyDefinition WorkingSetSourcePartition = InternalSchema.CreateGuidNameProperty("WorkingSetSourcePartition", typeof(string), WellKnownPropertySet.WorkingSet, "WorkingSetSourcePartition");

		// Token: 0x04004B83 RID: 19331
		public static readonly GuidNamePropertyDefinition WorkingSetSourcePartitionInternal = InternalSchema.CreateGuidNameProperty("WorkingSetSourcePartitionInternal", typeof(string), WellKnownPropertySet.WorkingSet, "WorkingSetSourcePartitionInternal");

		// Token: 0x04004B84 RID: 19332
		public static readonly GuidNamePropertyDefinition WorkingSetFlags = InternalSchema.CreateGuidNameProperty("WorkingSetFlags", typeof(int), WellKnownPropertySet.WorkingSet, "WorkingSetFlags");

		// Token: 0x04004B85 RID: 19333
		public static readonly GuidNamePropertyDefinition WorkingSetFolderEntryId = InternalSchema.CreateGuidNameProperty("WorkingSetFolderEntryId", typeof(byte[]), WellKnownPropertySet.WorkingSet, "WorkingSetFolderEntryId");

		// Token: 0x04004B86 RID: 19334
		public static readonly GuidNamePropertyDefinition ParkedMessagesFolderEntryId = InternalSchema.CreateGuidNameProperty("ParkedMessagesFolderEntryId", typeof(byte[]), WellKnownPropertySet.CalendarAssistant, "ParkedMessagesFolderEntryId");

		// Token: 0x04004B87 RID: 19335
		public static readonly StorePropertyDefinition ReplyToBlobExists = new PropertyExistenceTracker(InternalSchema.MapiReplyToBlob);

		// Token: 0x04004B88 RID: 19336
		public static readonly StorePropertyDefinition ReplyToNamesExists = new PropertyExistenceTracker(InternalSchema.MapiReplyToNames);

		// Token: 0x04004B89 RID: 19337
		public static readonly PropertyExistenceTracker ExtractedMeetingsExists = new PropertyExistenceTracker(InternalSchema.XmlExtractedMeetings);

		// Token: 0x04004B8A RID: 19338
		public static readonly PropertyExistenceTracker ExtractedTasksExists = new PropertyExistenceTracker(InternalSchema.XmlExtractedTasks);

		// Token: 0x04004B8B RID: 19339
		public static readonly PropertyExistenceTracker ExtractedAddressesExists = new PropertyExistenceTracker(InternalSchema.XmlExtractedAddresses);

		// Token: 0x04004B8C RID: 19340
		public static readonly PropertyExistenceTracker ExtractedKeywordsExists = new PropertyExistenceTracker(InternalSchema.XmlExtractedKeywords);

		// Token: 0x04004B8D RID: 19341
		public static readonly PropertyExistenceTracker ExtractedUrlsExists = new PropertyExistenceTracker(InternalSchema.XmlExtractedUrls);

		// Token: 0x04004B8E RID: 19342
		public static readonly PropertyExistenceTracker ExtractedPhonesExists = new PropertyExistenceTracker(InternalSchema.XmlExtractedPhones);

		// Token: 0x04004B8F RID: 19343
		public static readonly PropertyExistenceTracker ExtractedEmailsExists = new PropertyExistenceTracker(InternalSchema.XmlExtractedEmails);

		// Token: 0x04004B90 RID: 19344
		public static readonly PropertyExistenceTracker ExtractedContactsExists = new PropertyExistenceTracker(InternalSchema.XmlExtractedContacts);

		// Token: 0x04004B91 RID: 19345
		public static readonly PropertyTagPropertyDefinition ConsumerSharingCalendarSubscriptionCount = PropertyTagPropertyDefinition.InternalCreate("ConsumerSharingCalendarSubscriptionCount", PropTag.ConsumerSharingCalendarSubscriptionCount);

		// Token: 0x04004B92 RID: 19346
		public static readonly GuidNamePropertyDefinition ConsumerCalendarGuid = InternalSchema.CreateGuidNameProperty("ConsumerCalendarGuid", typeof(Guid), WellKnownPropertySet.ConsumerCalendar, null);

		// Token: 0x04004B93 RID: 19347
		public static readonly GuidNamePropertyDefinition ConsumerCalendarOwnerId = InternalSchema.CreateGuidNameProperty("ConsumerCalendarOwnerId", typeof(long), WellKnownPropertySet.ConsumerCalendar, null);

		// Token: 0x04004B94 RID: 19348
		public static readonly GuidNamePropertyDefinition ConsumerCalendarPrivateFreeBusyId = InternalSchema.CreateGuidNameProperty("ConsumerCalendarPrivateFreeBusyId", typeof(Guid), WellKnownPropertySet.ConsumerCalendar, null);

		// Token: 0x04004B95 RID: 19349
		public static readonly GuidNamePropertyDefinition ConsumerCalendarPrivateDetailId = InternalSchema.CreateGuidNameProperty("ConsumerCalendarPrivateDetailId", typeof(Guid), WellKnownPropertySet.ConsumerCalendar, null);

		// Token: 0x04004B96 RID: 19350
		public static readonly GuidNamePropertyDefinition ConsumerCalendarPublishVisibility = InternalSchema.CreateGuidNameProperty("ConsumerCalendarPublishVisibility", typeof(int), WellKnownPropertySet.ConsumerCalendar, null);

		// Token: 0x04004B97 RID: 19351
		public static readonly GuidNamePropertyDefinition ConsumerCalendarSharingInvitations = InternalSchema.CreateGuidNameProperty("ConsumerCalendarSharingInvitations", typeof(string), WellKnownPropertySet.ConsumerCalendar, null);

		// Token: 0x04004B98 RID: 19352
		public static readonly GuidNamePropertyDefinition ConsumerCalendarPermissionLevel = InternalSchema.CreateGuidNameProperty("ConsumerCalendarPermissionLevel", typeof(int), WellKnownPropertySet.ConsumerCalendar, null);

		// Token: 0x04004B99 RID: 19353
		public static readonly GuidNamePropertyDefinition ConsumerCalendarSynchronizationState = InternalSchema.CreateGuidNameProperty("ConsumerCalendarSynchronizationState", typeof(string), WellKnownPropertySet.ConsumerCalendar, null);

		// Token: 0x04004B9A RID: 19354
		public static readonly GuidNamePropertyDefinition SenderClass = InternalSchema.CreateGuidNameProperty("ItemSenderClass", typeof(short), WellKnownPropertySet.Common, "ItemSenderClass");

		// Token: 0x04004B9B RID: 19355
		public static readonly GuidNamePropertyDefinition CurrentFolderReason = InternalSchema.CreateGuidNameProperty("ItemCurrentFolderReason", typeof(short), WellKnownPropertySet.Common, "ItemCurrentFolderReason");

		// Token: 0x04004B9C RID: 19356
		public static readonly StorePropertyDefinition InferenceOLKUserActivityLoggingEnabled = new InferenceOLKUserActivityLoggingEnabledSmartProperty();

		// Token: 0x04004B9D RID: 19357
		public static readonly SmartPropertyDefinition ActivityItemId = new ActivityObjectIdProperty("ActivityItemId", InternalSchema.ActivityItemIdBytes);

		// Token: 0x04004B9E RID: 19358
		public static readonly SmartPropertyDefinition ActivityFolderId = new ActivityObjectIdProperty("ActivityFolderId", InternalSchema.ActivityFolderIdBytes);

		// Token: 0x02000C6C RID: 3180
		internal enum AdditionalPropTags
		{
			// Token: 0x04004BA4 RID: 19364
			AttachCalendarHidden = 2147352587,
			// Token: 0x04004BA5 RID: 19365
			IsContactPhoto = 2147418123,
			// Token: 0x04004BA6 RID: 19366
			StorageQuota = 1736114179,
			// Token: 0x04004BA7 RID: 19367
			LocallyDelivered = 1732575490,
			// Token: 0x04004BA8 RID: 19368
			ContentFilterPCL = 1082392579,
			// Token: 0x04004BA9 RID: 19369
			LastModifierName = 1073348639,
			// Token: 0x04004BAA RID: 19370
			CreatorEntryId = 1073283330,
			// Token: 0x04004BAB RID: 19371
			LastModifierEntryId = 1073414402,
			// Token: 0x04004BAC RID: 19372
			RecipientEntryId = 1610023170,
			// Token: 0x04004BAD RID: 19373
			DisplayTypeEx = 956628995,
			// Token: 0x04004BAE RID: 19374
			SenderFlags = 1075380227,
			// Token: 0x04004BAF RID: 19375
			SentRepresentingFlags = 1075445763,
			// Token: 0x04004BB0 RID: 19376
			UrlCompName = 284360735,
			// Token: 0x04004BB1 RID: 19377
			UrlCompNamePostfix = 241238019,
			// Token: 0x04004BB2 RID: 19378
			ReadReceiptDisplayName = 1076559903,
			// Token: 0x04004BB3 RID: 19379
			ReadReceiptEmailAddress = 1076494367,
			// Token: 0x04004BB4 RID: 19380
			ReadReceiptAddrType = 1076428831,
			// Token: 0x04004BB5 RID: 19381
			ReadReceiptEntryId = 4587778,
			// Token: 0x04004BB6 RID: 19382
			OriginalMessageId = 273023007,
			// Token: 0x04004BB7 RID: 19383
			SenderSmtpAddress = 1560346655,
			// Token: 0x04004BB8 RID: 19384
			SentRepresentingSmtpAddress = 1560412191,
			// Token: 0x04004BB9 RID: 19385
			OriginalSenderSmtpAddress = 1560477727,
			// Token: 0x04004BBA RID: 19386
			OriginalSentRepresentingSmtpAddress = 1560543263,
			// Token: 0x04004BBB RID: 19387
			ReadReceiptSmtpAddress = 1560608799,
			// Token: 0x04004BBC RID: 19388
			OriginalAuthorSmtpAddress = 1560674335,
			// Token: 0x04004BBD RID: 19389
			ReceivedBySmtpAddress = 1560739871,
			// Token: 0x04004BBE RID: 19390
			ReceivedRepresentingSmtpAddress = 1560805407,
			// Token: 0x04004BBF RID: 19391
			ReportingMta = 1746927647,
			// Token: 0x04004BC0 RID: 19392
			RemoteMta = 203489311,
			// Token: 0x04004BC1 RID: 19393
			SendOutlookRecallReport = 1745027083,
			// Token: 0x04004BC2 RID: 19394
			BlockStatus = 278265859,
			// Token: 0x04004BC3 RID: 19395
			UserX509Certificates = 980422914,
			// Token: 0x04004BC4 RID: 19396
			RulesTable = 1071710221,
			// Token: 0x04004BC5 RID: 19397
			MsgHasBeenDelegated = 1071841291,
			// Token: 0x04004BC6 RID: 19398
			MoveToFolderEntryId = 1072955650,
			// Token: 0x04004BC7 RID: 19399
			MoveToStoreEntryId = 1072890114,
			// Token: 0x04004BC8 RID: 19400
			OriginalMessageEntryId = 1715863810,
			// Token: 0x04004BC9 RID: 19401
			OriginalMessageSvrEId = 1732313346,
			// Token: 0x04004BCA RID: 19402
			SentMailSvrEId = 1732247810,
			// Token: 0x04004BCB RID: 19403
			ConversationsFolderEntryId = 904659202,
			// Token: 0x04004BCC RID: 19404
			RuleTriggerHistory = 1072824578,
			// Token: 0x04004BCD RID: 19405
			ReportEntryId = 4522242,
			// Token: 0x04004BCE RID: 19406
			RssServerLockStartTime = 1610612739,
			// Token: 0x04004BCF RID: 19407
			RssServerLockTimeout = 1610678275,
			// Token: 0x04004BD0 RID: 19408
			RssServerLockClientName = 1610743839,
			// Token: 0x04004BD1 RID: 19409
			LegacyScheduleFolderEntryId = 1713242370,
			// Token: 0x04004BD2 RID: 19410
			LegacyShortcutsFolderEntryId = 1714422018,
			// Token: 0x04004BD3 RID: 19411
			HomePostOfficeBox = 979238943,
			// Token: 0x04004BD4 RID: 19412
			OtherPostOfficeBox = 979632159,
			// Token: 0x04004BD5 RID: 19413
			SendReadNotifications = 1722089483,
			// Token: 0x04004BD6 RID: 19414
			VoiceMessageDuration = 1744896003,
			// Token: 0x04004BD7 RID: 19415
			SenderTelephoneNumber = 1744961567,
			// Token: 0x04004BD8 RID: 19416
			VoiceMessageSenderName = 1745027103,
			// Token: 0x04004BD9 RID: 19417
			FaxNumberOfPages = 1745092611,
			// Token: 0x04004BDA RID: 19418
			VoiceMessageAttachmentOrder = 1745158175,
			// Token: 0x04004BDB RID: 19419
			CallId = 1745223711,
			// Token: 0x04004BDC RID: 19420
			InternetReferences = 272171039,
			// Token: 0x04004BDD RID: 19421
			InReplyTo = 272760863,
			// Token: 0x04004BDE RID: 19422
			MapiInternetCpid = 1071513603,
			// Token: 0x04004BDF RID: 19423
			TextAttachmentCharset = 924516383,
			// Token: 0x04004BE0 RID: 19424
			ListHelp = 272826399,
			// Token: 0x04004BE1 RID: 19425
			ListSubscribe = 272891935,
			// Token: 0x04004BE2 RID: 19426
			ListUnsubscribe = 272957471,
			// Token: 0x04004BE3 RID: 19427
			MapiFlagStatus = 277872643,
			// Token: 0x04004BE4 RID: 19428
			IconIndex = 276824067,
			// Token: 0x04004BE5 RID: 19429
			LastVerbExecuted = 276889603,
			// Token: 0x04004BE6 RID: 19430
			LastVerbExecutionTime = 276955200,
			// Token: 0x04004BE7 RID: 19431
			ReplyForwardStatus = 2081095711,
			// Token: 0x04004BE8 RID: 19432
			PopImapPoisonMessageStamp = 2081161247,
			// Token: 0x04004BE9 RID: 19433
			IsHidden = 284426251,
			// Token: 0x04004BEA RID: 19434
			AttachCalendarFlags = 2147287043,
			// Token: 0x04004BEB RID: 19435
			AttachCalendarLinkId = 2147090435,
			// Token: 0x04004BEC RID: 19436
			AttachContentBase = 923861023,
			// Token: 0x04004BED RID: 19437
			AttachContentId = 923926559,
			// Token: 0x04004BEE RID: 19438
			Not822Renderable = 1733492747,
			// Token: 0x04004BEF RID: 19439
			BodyContentLocation = 269746207,
			// Token: 0x04004BF0 RID: 19440
			Codepage = 1073545219,
			// Token: 0x04004BF1 RID: 19441
			AttachInConflict = 1718353931,
			// Token: 0x04004BF2 RID: 19442
			AppointmentExceptionEndTime = 2147221568,
			// Token: 0x04004BF3 RID: 19443
			AppointmentExceptionStartTime = 2147156032,
			// Token: 0x04004BF4 RID: 19444
			AttachPayloadProviderGuidString = 924385311,
			// Token: 0x04004BF5 RID: 19445
			AttachPayloadClass = 924450847,
			// Token: 0x04004BF6 RID: 19446
			FolderViewWebInfo = 920584450,
			// Token: 0x04004BF7 RID: 19447
			ChildCount = 1714946051,
			// Token: 0x04004BF8 RID: 19448
			ELCFolderComment = 1731395615,
			// Token: 0x04004BF9 RID: 19449
			ELCPolicyIds = 1731330079,
			// Token: 0x04004BFA RID: 19450
			ExtendedFolderFlags = 920256770,
			// Token: 0x04004BFB RID: 19451
			AccessRights = 1715011587,
			// Token: 0x04004BFC RID: 19452
			ArticleId = 237174787,
			// Token: 0x04004BFD RID: 19453
			SyncCustomState = 2080506114,
			// Token: 0x04004BFE RID: 19454
			SyncFolderSourceKey = 2080571650,
			// Token: 0x04004BFF RID: 19455
			SyncFolderChangeKey = 2080637186,
			// Token: 0x04004C00 RID: 19456
			SyncFolderLastModificationTime = 2080702528,
			// Token: 0x04004C01 RID: 19457
			SyncState = 2081030402,
			// Token: 0x04004C02 RID: 19458
			ImapSubscribeList = 1710624799,
			// Token: 0x04004C03 RID: 19459
			IsContentIndexingEnabled = 240910347,
			// Token: 0x04004C04 RID: 19460
			AdditionalRenEntryIds = 920129794,
			// Token: 0x04004C05 RID: 19461
			AdditionalRenEntryIdsEx = 920191234,
			// Token: 0x04004C06 RID: 19462
			RemindersSearchFolderEntryId = 919929090,
			// Token: 0x04004C07 RID: 19463
			ElcRootFolderEntryId = 904397058,
			// Token: 0x04004C08 RID: 19464
			CommunicatorHistoryFolderEntryId = 904462594,
			// Token: 0x04004C09 RID: 19465
			SyncRootFolderEntryId = 904528130,
			// Token: 0x04004C0A RID: 19466
			UMVoicemailFolderEntryId = 904593666,
			// Token: 0x04004C0B RID: 19467
			EHAMigrationFolderEntryId = 904724738,
			// Token: 0x04004C0C RID: 19468
			UMFaxFolderEntryId = 918487298,
			// Token: 0x04004C0D RID: 19469
			AllItemsFolderEntryId = 904790274,
			// Token: 0x04004C0E RID: 19470
			IsProcessed = 2097217547,
			// Token: 0x04004C0F RID: 19471
			RecipientTrackStatus = 1610547203,
			// Token: 0x04004C10 RID: 19472
			RecipientTrackStatusTime = 1610285120,
			// Token: 0x04004C11 RID: 19473
			RecipientSipUri = 1608843295,
			// Token: 0x04004C12 RID: 19474
			UserConfigurationDictionary = 2080833794,
			// Token: 0x04004C13 RID: 19475
			UserConfigurationStream = 2080964866,
			// Token: 0x04004C14 RID: 19476
			UserConfigurationType = 2080768003,
			// Token: 0x04004C15 RID: 19477
			UserConfigurationXml = 2080899330,
			// Token: 0x04004C16 RID: 19478
			AdminFolderFlags = 1731002371,
			// Token: 0x04004C17 RID: 19479
			AttachmentContent = 1718419487,
			// Token: 0x04004C18 RID: 19480
			ItemColor = 278200323,
			// Token: 0x04004C19 RID: 19481
			FlagCompleteTime = 277938240,
			// Token: 0x04004C1A RID: 19482
			DisplayName7Bit = 973013023,
			// Token: 0x04004C1B RID: 19483
			DisplayType = 956301315,
			// Token: 0x04004C1C RID: 19484
			NdrStatusCode = 203423747,
			// Token: 0x04004C1D RID: 19485
			SwappedTodoData = 237830402,
			// Token: 0x04004C1E RID: 19486
			SwappedTodoStore = 237764866,
			// Token: 0x04004C1F RID: 19487
			RecipientProposed = 1608581131,
			// Token: 0x04004C20 RID: 19488
			RecipientProposedStartTime = 1608712256,
			// Token: 0x04004C21 RID: 19489
			RecipientProposedEndTime = 1608777792,
			// Token: 0x04004C22 RID: 19490
			RecipientOrder = 1608450051,
			// Token: 0x04004C23 RID: 19491
			ShortTermEntryIdFromObject = 1718747394,
			// Token: 0x04004C24 RID: 19492
			AssociatedSearchFolderId = 1749156098,
			// Token: 0x04004C25 RID: 19493
			AssociatedSearchFolderFlags = 1749549059,
			// Token: 0x04004C26 RID: 19494
			AssociatedSearchFolderExpiration = 1748631555,
			// Token: 0x04004C27 RID: 19495
			AssociatedSearchFolderLastUsedTime = 1748238339,
			// Token: 0x04004C28 RID: 19496
			AssociatedSearchFolderTemplateId = 1749090307,
			// Token: 0x04004C29 RID: 19497
			AssociatedSearchFolderTag = 1749483523,
			// Token: 0x04004C2A RID: 19498
			AssociatedSearchFolderStorageType = 1749417987,
			// Token: 0x04004C2B RID: 19499
			AssociatedSearchFolderDefinition = 1749352706,
			// Token: 0x04004C2C RID: 19500
			NavigationNodeGroupClassId = 1749156098,
			// Token: 0x04004C2D RID: 19501
			NavigationNodeOutlookTagId = 1749483523,
			// Token: 0x04004C2E RID: 19502
			NavigationNodeType = 1749614595,
			// Token: 0x04004C2F RID: 19503
			NavigationNodeFlags = 1749680131,
			// Token: 0x04004C30 RID: 19504
			NavigationNodeOrdinal = 1749745922,
			// Token: 0x04004C31 RID: 19505
			NavigationNodeEntryId = 1749811458,
			// Token: 0x04004C32 RID: 19506
			NavigationNodeRecordKey = 1749876994,
			// Token: 0x04004C33 RID: 19507
			NavigationNodeStoreEntryId = 1749942530,
			// Token: 0x04004C34 RID: 19508
			NavigationNodeClassId = 1750008066,
			// Token: 0x04004C35 RID: 19509
			NavigationNodeParentGroupClassId = 1750073602,
			// Token: 0x04004C36 RID: 19510
			NavigationNodeGroupName = 1750138911,
			// Token: 0x04004C37 RID: 19511
			NavigationNodeGroupSection = 1750204419,
			// Token: 0x04004C38 RID: 19512
			NavigationNodeCalendarColor = 1750269955,
			// Token: 0x04004C39 RID: 19513
			NavigationNodeAddressBookEntryId = 1750335746,
			// Token: 0x04004C3A RID: 19514
			NavigationNodeAddressBookStoreEntryId = 1754333442,
			// Token: 0x04004C3B RID: 19515
			DelegateNames = 1749684255,
			// Token: 0x04004C3C RID: 19516
			DelegateEntryIds = 1749356802,
			// Token: 0x04004C3D RID: 19517
			DelegateEntryIds2 = 1752174850,
			// Token: 0x04004C3E RID: 19518
			DelegateFlags = 1751846915,
			// Token: 0x04004C3F RID: 19519
			DelegateFlags2 = 1752240131,
			// Token: 0x04004C40 RID: 19520
			DelegateBossWantsCopy = 1749155851,
			// Token: 0x04004C41 RID: 19521
			DelegateBossWantsInfo = 1749745675,
			// Token: 0x04004C42 RID: 19522
			DelegateDontMail = 1749221387,
			// Token: 0x04004C43 RID: 19523
			FreeBusyEntryIds = 920916226,
			// Token: 0x04004C44 RID: 19524
			DeletedOnTime = 1720647744,
			// Token: 0x04004C45 RID: 19525
			IsSoftDeleted = 1735393291,
			// Token: 0x04004C46 RID: 19526
			MapiToDoItemFlag = 237699075,
			// Token: 0x04004C47 RID: 19527
			ScheduleInfoMonthsTentative = 1750142979,
			// Token: 0x04004C48 RID: 19528
			ScheduleInfoFreeBusyTentative = 1750208770,
			// Token: 0x04004C49 RID: 19529
			ScheduleInfoMonthsBusy = 1750274051,
			// Token: 0x04004C4A RID: 19530
			ScheduleInfoFreeBusyBusy = 1750339842,
			// Token: 0x04004C4B RID: 19531
			ScheduleInfoMonthsOof = 1750405123,
			// Token: 0x04004C4C RID: 19532
			ScheduleInfoFreeBusyOof = 1750470914,
			// Token: 0x04004C4D RID: 19533
			ScheduleInfoMonthsMerged = 1750011907,
			// Token: 0x04004C4E RID: 19534
			ScheduleInfoFreeBusyMerged = 1750077698,
			// Token: 0x04004C4F RID: 19535
			ScheduleInfoRecipientLegacyDn = 1749614622,
			// Token: 0x04004C50 RID: 19536
			OutlookFreeBusyMonthCount = 1751711747,
			// Token: 0x04004C51 RID: 19537
			LocalDirectory = 1747452162,
			// Token: 0x04004C52 RID: 19538
			MemberEmailLocalDirectory = 1747517471,
			// Token: 0x04004C53 RID: 19539
			MemberExternalIdLocalDirectory = 1747583007,
			// Token: 0x04004C54 RID: 19540
			MemberSIDLocalDirectory = 1747648770,
			// Token: 0x04004C55 RID: 19541
			ContentAggregationSubscriptions = 1765015810,
			// Token: 0x04004C56 RID: 19542
			ContentAggregationMessageIndex = 1765081346,
			// Token: 0x04004C57 RID: 19543
			FailedInboundICalAsAttachment = 924581899,
			// Token: 0x04004C58 RID: 19544
			BodyContentId = 269811743,
			// Token: 0x04004C59 RID: 19545
			MimeSkeleton = 1693450498,
			// Token: 0x04004C5A RID: 19546
			LogonRightsOnMailbox = 1736245251,
			// Token: 0x04004C5B RID: 19547
			MergeMidsetDeleted = 242876674,
			// Token: 0x04004C5C RID: 19548
			RuleActions = 1719664894,
			// Token: 0x04004C5D RID: 19549
			RuleCondition = 1719206141,
			// Token: 0x04004C5E RID: 19550
			ExtendedRuleCondition = 244973826,
			// Token: 0x04004C5F RID: 19551
			ExtendedRuleSizeLimit = 245039107,
			// Token: 0x04004C60 RID: 19552
			SystemFolderEntryId = 905773314,
			// Token: 0x04004C61 RID: 19553
			AppointmentTombstonesId = 1751777538,
			// Token: 0x04004C62 RID: 19554
			MapiRulesData = 1071710466,
			// Token: 0x04004C63 RID: 19555
			FolderFlags = 1722286083,
			// Token: 0x04004C64 RID: 19556
			ResolveMethod = 1072103427,
			// Token: 0x04004C65 RID: 19557
			ParentFid = 1732837396,
			// Token: 0x04004C66 RID: 19558
			Fid = 1732771860,
			// Token: 0x04004C67 RID: 19559
			ActivityId = 1342308355,
			// Token: 0x04004C68 RID: 19560
			ActivityItemIdBytes = 1342374146,
			// Token: 0x04004C69 RID: 19561
			ActivityTimeStamp = 1342242880,
			// Token: 0x04004C6A RID: 19562
			ActivityClientId = 1342504963,
			// Token: 0x04004C6B RID: 19563
			LogName = 1610678303,
			// Token: 0x04004C6C RID: 19564
			SentRepresentingSimpleDisplayName = 1076953119,
			// Token: 0x04004C6D RID: 19565
			SentRepresentingOrgAddressType = 1078001695,
			// Token: 0x04004C6E RID: 19566
			SentRepresentingOrgEmailAddr = 1078067231,
			// Token: 0x04004C6F RID: 19567
			SentRepresentingSID = 239993090,
			// Token: 0x04004C70 RID: 19568
			SentRepresentingGuid = 239141122,
			// Token: 0x04004C71 RID: 19569
			PartOfContentIndexing = 910491659,
			// Token: 0x04004C72 RID: 19570
			SenderSID = 239927554,
			// Token: 0x04004C73 RID: 19571
			SenderGuid = 239075586,
			// Token: 0x04004C74 RID: 19572
			ParticipantSID = 203686146,
			// Token: 0x04004C75 RID: 19573
			ParticipantGuid = 203751682,
			// Token: 0x04004C76 RID: 19574
			SearchIsPartiallyIndexed = 248381451,
			// Token: 0x04004C77 RID: 19575
			ConversationCreatorSID = 240845058
		}

		// Token: 0x02000C6D RID: 3181
		internal enum ToDoItemFlags
		{
			// Token: 0x04004C79 RID: 19577
			IsToDoItem = 1,
			// Token: 0x04004C7A RID: 19578
			IsFlagSetForRecipient = 8
		}

		// Token: 0x02000C6E RID: 3182
		[Flags]
		internal enum LogonRightsOnMailboxFlags
		{
			// Token: 0x04004C7C RID: 19580
			None = 0,
			// Token: 0x04004C7D RID: 19581
			CanActAsOwner = 1,
			// Token: 0x04004C7E RID: 19582
			CanSendAs = 2
		}
	}
}
