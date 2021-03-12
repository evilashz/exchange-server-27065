using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Reporting;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.Hygiene.Cache.Data;
using Microsoft.Exchange.Hygiene.Data.Directory;
using Microsoft.Exchange.Hygiene.Data.Spam;
using Microsoft.Exchange.Hygiene.Data.Sync;
using Microsoft.Exchange.Security.Compliance;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000073 RID: 115
	internal static class DalHelper
	{
		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0000CC12 File Offset: 0x0000AE12
		public static PropertyDefinition[] GlobalProperties
		{
			get
			{
				return DalHelper.globalProperties.Value;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x0000CC20 File Offset: 0x0000AE20
		public static string RegionTag
		{
			get
			{
				if (DalHelper.regionTag == null)
				{
					lock (DalHelper.regionTagLock)
					{
						string forefrontRegionTag = DatacenterRegistry.GetForefrontRegionTag();
						if (!string.IsNullOrWhiteSpace(forefrontRegionTag))
						{
							DalHelper.regionTag = forefrontRegionTag;
						}
						else
						{
							DalHelper.regionTag = string.Empty;
						}
					}
				}
				return DalHelper.regionTag;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x0000CC88 File Offset: 0x0000AE88
		// (set) Token: 0x06000451 RID: 1105 RVA: 0x0000CC8F File Offset: 0x0000AE8F
		public static string ServiceTagContext
		{
			get
			{
				return DalHelper.serviceTagInTLS;
			}
			set
			{
				DalHelper.serviceTagInTLS = value;
			}
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000CC98 File Offset: 0x0000AE98
		public static void ForEachProperty(QueryFilter filter, Action<PropertyDefinition, object> doAction)
		{
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}
			if (doAction == null)
			{
				throw new ArgumentNullException("doAction");
			}
			if (filter is CompositeFilter)
			{
				CompositeFilter compositeFilter = filter as CompositeFilter;
				using (ReadOnlyCollection<QueryFilter>.Enumerator enumerator = compositeFilter.Filters.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						QueryFilter filter2 = enumerator.Current;
						DalHelper.ForEachProperty(filter2, doAction);
					}
					return;
				}
			}
			if (filter is NotFilter)
			{
				NotFilter notFilter = (NotFilter)filter;
				DalHelper.ForEachProperty(notFilter.Filter, doAction);
				return;
			}
			if (filter is ComparisonFilter)
			{
				ComparisonFilter comparisonFilter = (ComparisonFilter)filter;
				doAction(comparisonFilter.Property, comparisonFilter.PropertyValue);
				return;
			}
			if (filter is TextFilter)
			{
				TextFilter textFilter = (TextFilter)filter;
				string text = textFilter.Text ?? string.Empty;
				switch (textFilter.MatchOptions)
				{
				case MatchOptions.FullString:
					goto IL_13D;
				case MatchOptions.SubString:
				case MatchOptions.ExactPhrase:
					text = "%" + text + "%";
					goto IL_13D;
				case MatchOptions.Prefix:
					text += "%";
					goto IL_13D;
				case MatchOptions.Suffix:
					text = "%" + text;
					goto IL_13D;
				}
				throw new NotSupportedException(HygieneDataStrings.ErrorQueryFilterType(filter.ToString()));
				IL_13D:
				doAction(textFilter.Property, text);
				return;
			}
			if (filter is GenericBitMaskFilter)
			{
				GenericBitMaskFilter genericBitMaskFilter = (GenericBitMaskFilter)filter;
				PropertyDefinition property = genericBitMaskFilter.Property;
				Type type = DalHelper.ConvertToStoreType(property);
				TypeCode typeCode = Type.GetTypeCode(type);
				object arg;
				if (typeCode == TypeCode.Int32)
				{
					arg = (int)((uint)genericBitMaskFilter.Mask);
				}
				else
				{
					arg = genericBitMaskFilter.Mask;
				}
				doAction(property, arg);
				return;
			}
			if (filter is ExistsFilter)
			{
				ExistsFilter existsFilter = (ExistsFilter)filter;
				doAction(existsFilter.Property, null);
				return;
			}
			throw new NotSupportedException(HygieneDataStrings.ErrorQueryFilterType(filter.ToString()));
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000CE94 File Offset: 0x0000B094
		public static bool TryFindPropertyValueByName(QueryFilter queryFilter, string propertyName, out object propertyValue)
		{
			return DalHelper.TryFindPropertyValueByName(queryFilter, propertyName, StringComparison.OrdinalIgnoreCase, out propertyValue);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000CED0 File Offset: 0x0000B0D0
		public static bool TryFindPropertyValueByName(QueryFilter queryFilter, string propertyName, StringComparison comparisonOptions, out object propertyValue)
		{
			object localValue = null;
			bool foundResult = false;
			DalHelper.ForEachProperty(queryFilter, delegate(PropertyDefinition prop, object propValue)
			{
				if (string.Equals(prop.Name, propertyName, comparisonOptions))
				{
					localValue = propValue;
					foundResult = true;
				}
			});
			propertyValue = localValue;
			return foundResult;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000D010 File Offset: 0x0000B210
		public static IEnumerable<PropertyDefinition> GetPropertyDefinitions(object instance, bool isChangedOnly)
		{
			ConfigurablePropertyBag configurablePropertyBag = instance as ConfigurablePropertyBag;
			IEnumerable<PropertyDefinition> enumerable;
			if (configurablePropertyBag == null)
			{
				ConfigurableObject adObject = instance as ConfigurableObject;
				if (adObject == null)
				{
					throw new ArgumentException(HygieneDataStrings.ErrorInvalidInstanceType(instance.GetType().Name));
				}
				enumerable = (from propDef in adObject.ObjectSchema.AllProperties
				where propDef is HygienePropertyDefinition || propDef == ADObjectSchema.Id || propDef == ADObjectSchema.RawName || propDef == ADObjectSchema.ObjectState || propDef == AcceptedDomainSchema.DomainName || adObject is ADGroup || adObject is ADContact || adObject is TransportQueueStatistics || adObject is TransportQueueLog || adObject is TenantThrottleInfo || adObject is TransportProcessingQuotaConfig || adObject is FfoTenant || adObject is AdminAuditLogEventFacade
				select propDef).Concat(DalHelper.ADObjectExtensionProperties);
				if (instance is IPropertyBag && (bool)((IPropertyBag)instance)[CommonSyncProperties.ForwardSyncObjectProp])
				{
					enumerable = enumerable.Concat(DalHelper.SyncExtensionProperties);
				}
				enumerable = (from propDef in enumerable
				where !isChangedOnly || !(propDef is ProviderPropertyDefinition) || (!((ProviderPropertyDefinition)propDef).IsCalculated && adObject.IsModified((ProviderPropertyDefinition)propDef))
				select propDef).Concat(new ADPropertyDefinition[]
				{
					ADObjectSchema.OrganizationalUnitRoot
				});
			}
			else
			{
				enumerable = configurablePropertyBag.GetPropertyDefinitions(isChangedOnly);
				enumerable = enumerable.Concat(DalHelper.ADObjectExtensionProperties);
				if (instance is IPropertyBag && (bool)((IPropertyBag)instance)[CommonSyncProperties.ForwardSyncObjectProp])
				{
					enumerable = enumerable.Concat(DalHelper.SyncExtensionProperties);
				}
			}
			return enumerable;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000D140 File Offset: 0x0000B340
		public static IPropertyBag GetConfigPropertyBag(IConfigurable configObject)
		{
			IPropertyBag propertyBag = configObject as IPropertyBag;
			if (propertyBag == null)
			{
				throw new ArgumentException(HygieneDataStrings.ErrorUnsupportedInterface(configObject.GetType().FullName, typeof(IPropertyBag).FullName));
			}
			return propertyBag;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000D184 File Offset: 0x0000B384
		public static WstDataProviderSettings GetProviderSettings(QueryFilter queryFilter)
		{
			object obj;
			if (queryFilter != null && DalHelper.TryFindPropertyValueByName(queryFilter, DalHelper.WstDataProviderSettingsProp.Name, out obj))
			{
				return (WstDataProviderSettings)obj;
			}
			return WstDataProviderSettings.Default;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0000D1B4 File Offset: 0x0000B3B4
		public static Type ConvertToStoreType(PropertyDefinition prop)
		{
			Type type = prop.Type;
			Type underlyingType = Nullable.GetUnderlyingType(type);
			if (underlyingType != null)
			{
				type = underlyingType;
			}
			if (type.IsEnum)
			{
				return Enum.GetUnderlyingType(type);
			}
			if (type == typeof(ADObjectId) || type == typeof(ConfigObjectId) || type == typeof(ObjectId))
			{
				return typeof(Guid);
			}
			if (type == typeof(ByteQuantifiedSize))
			{
				return typeof(ulong);
			}
			if (type == typeof(SmtpAddress) || type == typeof(SmtpDomain) || type == typeof(SmtpDomainWithSubdomains) || type == typeof(ProxyAddress))
			{
				return typeof(string);
			}
			if (type == typeof(AddressSpace))
			{
				return typeof(string);
			}
			if (type == typeof(IPAddress))
			{
				return typeof(string);
			}
			if (type == typeof(byte[]))
			{
				return typeof(string);
			}
			if (type == typeof(CultureInfo))
			{
				return typeof(string);
			}
			if (type == typeof(NetID))
			{
				return typeof(string);
			}
			if (type == typeof(IPRange))
			{
				return typeof(string);
			}
			if (type == typeof(SecureString))
			{
				return typeof(string);
			}
			if (type == typeof(SmtpX509IdentifierEx))
			{
				return typeof(string);
			}
			if (type == typeof(ServiceProviderSettings))
			{
				return typeof(string);
			}
			if (type == typeof(TlsCertificate))
			{
				return typeof(string);
			}
			if (type == typeof(EnhancedTimeSpan))
			{
				return typeof(long);
			}
			if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Unlimited<>))
			{
				return typeof(string);
			}
			if (type == typeof(ExDateTime))
			{
				return typeof(DateTime);
			}
			if (type == typeof(AdminAuditLogCmdletParameter))
			{
				return typeof(string);
			}
			if (type == typeof(AdminAuditLogModifiedProperty))
			{
				return typeof(string);
			}
			return type;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0000D45C File Offset: 0x0000B65C
		public static object ConvertToStoreObject(object dalObject, SqlDbType type = SqlDbType.Variant)
		{
			if (dalObject == null)
			{
				return null;
			}
			ADObjectId adobjectId = dalObject as ADObjectId;
			if (adobjectId != null)
			{
				if (adobjectId.ObjectGuid != Guid.Empty || type == SqlDbType.UniqueIdentifier)
				{
					return adobjectId.ObjectGuid;
				}
				return adobjectId.Name;
			}
			else
			{
				ConfigObjectId configObjectId = dalObject as ConfigObjectId;
				if (configObjectId != null)
				{
					return new Guid(configObjectId.ToString());
				}
				ObjectId objectId = dalObject as ObjectId;
				if (objectId != null)
				{
					return new Guid(objectId.ToString());
				}
				NetID netID = dalObject as NetID;
				if (netID != null)
				{
					return netID.ToString();
				}
				if (dalObject is Enum)
				{
					return Convert.ChangeType(dalObject, Enum.GetUnderlyingType(dalObject.GetType()));
				}
				if (dalObject is ByteQuantifiedSize)
				{
					return ((ByteQuantifiedSize)dalObject).ToBytes();
				}
				if (dalObject is SmtpAddress)
				{
					return dalObject.ToString();
				}
				if (dalObject is ProxyAddress)
				{
					return dalObject.ToString();
				}
				if (dalObject is SmtpDomain)
				{
					return dalObject.ToString();
				}
				if (dalObject is AddressSpace)
				{
					return dalObject.ToString();
				}
				if (dalObject is IPAddress)
				{
					return dalObject.ToString();
				}
				if (dalObject is byte[])
				{
					if (type == SqlDbType.Binary || type == SqlDbType.VarBinary)
					{
						return dalObject;
					}
					return Convert.ToBase64String((byte[])dalObject);
				}
				else
				{
					if (dalObject is IPRange)
					{
						return dalObject.ToString();
					}
					if (dalObject is SmtpDomainWithSubdomains)
					{
						return dalObject.ToString();
					}
					if (dalObject is SecureString)
					{
						IntPtr ptr = Marshal.SecureStringToBSTR(dalObject as SecureString);
						try
						{
							return Marshal.PtrToStringBSTR(ptr);
						}
						finally
						{
							Marshal.FreeBSTR(ptr);
						}
					}
					if (dalObject is SmtpX509IdentifierEx)
					{
						return dalObject.ToString();
					}
					if (dalObject is ServiceProviderSettings)
					{
						return dalObject.ToString();
					}
					if (dalObject is TlsCertificate)
					{
						return dalObject.ToString();
					}
					if (dalObject is EnhancedTimeSpan)
					{
						return ((EnhancedTimeSpan)dalObject).Ticks;
					}
					Type type2 = dalObject.GetType();
					if (type2.IsGenericType && type2.GetGenericTypeDefinition() == typeof(Unlimited<>))
					{
						return dalObject.ToString();
					}
					if (dalObject is ExDateTime)
					{
						return (DateTime)((ExDateTime)dalObject);
					}
					if (dalObject is AdminAuditLogCmdletParameter)
					{
						return DalHelper.CustomSerializeAuditLogCmdletParameter(dalObject as AdminAuditLogCmdletParameter);
					}
					if (dalObject is AdminAuditLogModifiedProperty)
					{
						return DalHelper.CustomSerializeAuditLogModifiedProperty(dalObject as AdminAuditLogModifiedProperty);
					}
					return dalObject;
				}
			}
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000D6B0 File Offset: 0x0000B8B0
		public static object ConvertFromStoreObject(object obj, Type targetType)
		{
			if (obj == null || obj is DBNull)
			{
				return null;
			}
			if (targetType.IsAssignableFrom(obj.GetType()))
			{
				return obj;
			}
			Type underlyingType = Nullable.GetUnderlyingType(targetType);
			if (underlyingType != null)
			{
				targetType = underlyingType;
			}
			if (targetType == typeof(ADObjectId))
			{
				string dn;
				Guid objectGuid;
				if (obj is Guid)
				{
					dn = obj.ToString();
					objectGuid = (Guid)obj;
				}
				else
				{
					if (!(obj is Tuple<Guid, string>))
					{
						throw new InvalidOperationException(string.Format("Cannot convert type {0} to {1}", obj.GetType().Name, targetType.Name));
					}
					Tuple<Guid, string> tuple = (Tuple<Guid, string>)obj;
					dn = tuple.Item2;
					objectGuid = tuple.Item1;
				}
				return new ADObjectId(DalHelper.GetDistinguishedName(dn), objectGuid);
			}
			if ((targetType == typeof(ConfigObjectId) || targetType == typeof(ObjectId)) && obj is Guid)
			{
				return new ConfigObjectId(obj.ToString());
			}
			if (targetType == typeof(NetID))
			{
				return new NetID(obj.ToString());
			}
			if (targetType.IsEnum)
			{
				string text = obj as string;
				if (text != null)
				{
					return Enum.Parse(targetType, text);
				}
				return Enum.ToObject(targetType, obj);
			}
			else if (targetType == typeof(ByteQuantifiedSize))
			{
				if (obj is long)
				{
					return ByteQuantifiedSize.FromBytes((ulong)((long)obj));
				}
				return ByteQuantifiedSize.FromBytes((ulong)((decimal)obj));
			}
			else if (targetType == typeof(SmtpAddress))
			{
				string text2 = obj.ToString();
				if (string.IsNullOrEmpty(text2))
				{
					return null;
				}
				return SmtpAddress.Parse(text2);
			}
			else if (targetType == typeof(ProxyAddress))
			{
				string text3 = obj.ToString();
				if (!string.IsNullOrEmpty(text3))
				{
					return ProxyAddress.Parse(text3);
				}
				return null;
			}
			else if (targetType == typeof(SmtpDomain))
			{
				string value = obj.ToString();
				if (string.IsNullOrEmpty(value))
				{
					return null;
				}
				return SmtpDomain.Parse(obj.ToString());
			}
			else
			{
				if (targetType == typeof(AddressSpace))
				{
					AddressSpace result;
					AddressSpace.TryParse(obj.ToString(), out result);
					return result;
				}
				if (targetType == typeof(IPAddress))
				{
					string text4 = obj.ToString();
					if (string.IsNullOrEmpty(text4))
					{
						return null;
					}
					return IPAddress.Parse(text4);
				}
				else
				{
					if (targetType == typeof(byte[]))
					{
						return Convert.FromBase64String(obj.ToString());
					}
					if (targetType == typeof(IPRange))
					{
						IPRange result2 = null;
						IPRange.TryParse(obj.ToString(), out result2);
						return result2;
					}
					if (targetType == typeof(SmtpDomainWithSubdomains))
					{
						return new SmtpDomainWithSubdomains(obj.ToString());
					}
					if (targetType == typeof(SecureString))
					{
						return obj.ToString().ConvertToSecureString();
					}
					if (targetType == typeof(SmtpX509IdentifierEx))
					{
						return new SmtpX509IdentifierEx(obj.ToString());
					}
					if (targetType == typeof(ServiceProviderSettings))
					{
						return ServiceProviderSettings.Parse(obj.ToString());
					}
					if (targetType == typeof(TlsCertificate))
					{
						return new TlsCertificate(obj.ToString());
					}
					if (targetType == typeof(EnhancedTimeSpan) && obj is long)
					{
						return EnhancedTimeSpan.FromTicks((long)obj);
					}
					if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Unlimited<>))
					{
						MethodInfo method = targetType.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public, null, new Type[]
						{
							typeof(string)
						}, null);
						return method.Invoke(null, new object[]
						{
							obj.ToString()
						});
					}
					if (targetType == typeof(AdminAuditLogCmdletParameter))
					{
						return DalHelper.CustomDeserializeAuditLogCmdletParameter((string)obj);
					}
					if (targetType == typeof(AdminAuditLogModifiedProperty))
					{
						return DalHelper.CustomDeserializeAuditLogModifiedProperty(((string)obj).Trim());
					}
					object result3;
					try
					{
						result3 = Convert.ChangeType(obj, targetType);
					}
					catch (Exception innerException)
					{
						throw new InvalidOperationException(string.Format("Cannot convert type {0} to {1}", obj.GetType().Name, targetType.Name), innerException);
					}
					return result3;
				}
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000DAF0 File Offset: 0x0000BCF0
		public static string ToString(object obj)
		{
			if (obj == null)
			{
				return DalHelper.NullString;
			}
			return obj.ToString();
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000DC58 File Offset: 0x0000BE58
		public static IEnumerable<IEnumerable<T>> BatchSplit<T>(this IEnumerable<T> input, int size)
		{
			return from b in input.Select((T a, int i) => new
			{
				Item = a,
				Index = i
			})
			group b by b.Index / size into c
			select 
				from d in c
				select d.Item;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000DCA8 File Offset: 0x0000BEA8
		public static long FastHash(string value)
		{
			long num = 0L;
			long num2 = 31415L;
			foreach (char c in value.ToCharArray())
			{
				num = (num * num2 + (long)((ulong)c)) % 16381L;
				num2 = num2 * 27183L % 16380L;
			}
			return num;
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000DCFC File Offset: 0x0000BEFC
		public static long FastHash(byte[] value)
		{
			long num = 0L;
			long num2 = 31415L;
			for (int i = 0; i < value.Length; i++)
			{
				num = (num * num2 + (long)((ulong)value[i])) % 16381L;
				num2 = num2 * 27183L % 16380L;
			}
			return num;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0000DD44 File Offset: 0x0000BF44
		public static IEnumerable<string> GetAcceptedDomainBloomFilterKeys(AcceptedDomain domain)
		{
			if (domain == null || domain.DomainName == null)
			{
				return Enumerable.Empty<string>();
			}
			string text = domain.DomainName.Domain.ToLowerInvariant();
			if (domain.MatchSubDomains)
			{
				return new string[]
				{
					text,
					string.Format("*.{0}", text)
				};
			}
			return new string[]
			{
				text
			};
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0000DDA2 File Offset: 0x0000BFA2
		internal static bool IsPropertyReadonly(PropertyDefinition prop)
		{
			return prop is SimpleProviderPropertyDefinition && ((SimpleProviderPropertyDefinition)prop).IsReadOnly;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000DDB9 File Offset: 0x0000BFB9
		internal static bool IsPropertyMultiValued(PropertyDefinition prop)
		{
			return prop is SimpleProviderPropertyDefinition && ((SimpleProviderPropertyDefinition)prop).IsMultivalued;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000DDD0 File Offset: 0x0000BFD0
		internal static bool IsTypeMultiValuedProperty(Type userType)
		{
			return userType.IsGenericType && userType.GetGenericTypeDefinition() == typeof(MultiValuedProperty<>);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000DDF4 File Offset: 0x0000BFF4
		internal static void SetPropertyValue(object value, PropertyDefinition prop, IConfigurable iconfigObj)
		{
			ConfigurableObject configurableObject = iconfigObj as ConfigurableObject;
			ConfigurablePropertyBag configurablePropertyBag = (configurableObject == null) ? (iconfigObj as ConfigurablePropertyBag) : null;
			value = DalHelper.ConvertFromStoreObject(value, prop.Type);
			if (configurableObject != null)
			{
				if (!DalHelper.IsPropertyMultiValued(prop))
				{
					DalHelper.SetConfigurableObject(value, prop, configurableObject);
					return;
				}
				object obj;
				if (!configurableObject.TryGetValueWithoutDefault(prop, out obj) || ((MultiValuedPropertyBase)obj).Count == 0)
				{
					DalHelper.SetConfigurableObject(Activator.CreateInstance(typeof(MultiValuedProperty<>).MakeGenericType(new Type[]
					{
						prop.Type
					}), new object[]
					{
						value
					}), prop, configurableObject);
					return;
				}
				if (!DalHelper.IsPropertyReadonly(prop))
				{
					((MultiValuedPropertyBase)obj).Add(value);
					return;
				}
			}
			else if (DalHelper.IsPropertyMultiValued(prop))
			{
				if (configurablePropertyBag[prop] == null)
				{
					configurablePropertyBag[prop] = Activator.CreateInstance(typeof(MultiValuedProperty<>).MakeGenericType(new Type[]
					{
						prop.Type
					}), new object[]
					{
						value
					});
					((MultiValuedPropertyBase)configurablePropertyBag[prop]).SetIsReadOnly(false, null);
					return;
				}
				if (!DalHelper.IsPropertyReadonly(prop))
				{
					((MultiValuedPropertyBase)configurablePropertyBag[prop]).Add(value);
					return;
				}
			}
			else
			{
				configurablePropertyBag[prop] = value;
			}
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000DF34 File Offset: 0x0000C134
		internal static void SetConfigurableObject(object value, PropertyDefinition prop, ConfigurableObject exObj)
		{
			ProviderPropertyDefinition providerPropertyDefinition = (ProviderPropertyDefinition)prop;
			if (providerPropertyDefinition.VersionAdded.IsOlderThan(exObj.ExchangeVersion) || providerPropertyDefinition.VersionAdded.IsSameVersion(exObj.ExchangeVersion))
			{
				exObj.propertyBag.SetField(providerPropertyDefinition, value);
			}
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0000DF7C File Offset: 0x0000C17C
		internal static string GetTenantDistinguishedName(string tenantName)
		{
			return DalHelper.FfoRootDN.GetChildId(tenantName).DistinguishedName;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0000DF97 File Offset: 0x0000C197
		internal static IEnumerable<PropertyDefinition> GetDeclaredReflectedProperties(ADObject instance)
		{
			return (from field in instance.ObjectSchema.GetType().GetFields(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
			select field.GetValue(null)).OfType<PropertyDefinition>();
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000E000 File Offset: 0x0000C200
		internal static string GetTraceableObject(ADObject objectToTrace, IEnumerable<ProviderPropertyDefinition> properties)
		{
			return string.Join("|", from p in properties
			select string.Format("{0}={1}", p.Name, DalHelper.GetTraceableValue(objectToTrace[p])));
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000E040 File Offset: 0x0000C240
		internal static string GetTraceableValue(object objectToTrace)
		{
			IEnumerable enumerable = objectToTrace as IEnumerable;
			string text;
			if (objectToTrace is byte[])
			{
				text = Encoding.Default.GetString(objectToTrace as byte[]);
			}
			else if (enumerable != null && !(objectToTrace is string))
			{
				text = string.Join(",", from object i in enumerable
				select i.ToString());
			}
			else if (objectToTrace == null)
			{
				text = "<null>";
			}
			else
			{
				text = objectToTrace.ToString();
			}
			return text.Trim().Replace(Environment.NewLine, string.Empty);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000E278 File Offset: 0x0000C478
		internal static IEnumerable<string> GetPossibleCidrs(string ipString)
		{
			IPAddress ip;
			if (!IPAddress.TryParse(ipString, out ip) || ip.AddressFamily != AddressFamily.InterNetwork)
			{
				yield return ipString;
			}
			else
			{
				yield return ip.ToString();
				yield return string.Format("{0}/32", ip);
				for (int bitIndex = 1; bitIndex < 9; bitIndex++)
				{
					IPRange ipRange = IPRange.CreateIPAndCIDR(ip, (short)(32 - bitIndex));
					yield return ipRange.ToString();
				}
			}
			yield break;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000E438 File Offset: 0x0000C638
		internal static IEnumerable<string> GetIPsFromIPRange(IPRange ipRange)
		{
			IPRange.Format rangeFormat = ipRange.RangeFormat;
			if (rangeFormat != IPRange.Format.CIDR)
			{
				if (rangeFormat == IPRange.Format.SingleAddress)
				{
					yield return DalHelper.GetIPKey(ipRange.LowerBound.ToString());
				}
				else
				{
					yield return ipRange.ToString();
				}
			}
			else
			{
				IPvxAddress lowerBound = ipRange.LowerBound;
				short cidrLength = ipRange.CIDRLength;
				if (cidrLength <= 23)
				{
					throw new ArgumentException(string.Format("CIDR length in {0} must be greater than {1}", ipRange, 23));
				}
				yield return DalHelper.GetIPKey(lowerBound.ToString());
			}
			yield break;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000E458 File Offset: 0x0000C658
		internal static string GetIPKey(string ipString)
		{
			int num = ipString.LastIndexOf('.');
			return (num >= 0) ? ipString.Substring(0, num) : ipString;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0000E480 File Offset: 0x0000C680
		internal static byte[] GetSHA1Hash(string inputString)
		{
			if (inputString == null)
			{
				return null;
			}
			byte[] result;
			lock (DalHelper.shaHasher)
			{
				result = DalHelper.shaHasher.ComputeHash(Encoding.Unicode.GetBytes(inputString.ToLower()));
			}
			return result;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000E4DC File Offset: 0x0000C6DC
		internal static byte[] GetMDHash(string inputString)
		{
			if (inputString == null)
			{
				return null;
			}
			byte[] result;
			using (MessageDigestForNonCryptographicPurposes messageDigestForNonCryptographicPurposes = new MessageDigestForNonCryptographicPurposes())
			{
				result = messageDigestForNonCryptographicPurposes.ComputeHash(Encoding.Default.GetBytes(inputString));
			}
			return result;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000E560 File Offset: 0x0000C760
		internal static bool AreObjectsEqual(ADObject adObject1, ADObject adObject2, out string failureReason)
		{
			failureReason = null;
			if (object.ReferenceEquals(adObject1, adObject2))
			{
				return true;
			}
			if (adObject1 == null)
			{
				failureReason = "adObject1 is null";
				return false;
			}
			if (adObject2 == null)
			{
				failureReason = "adObject2 is null";
				return false;
			}
			if (adObject2.GetType() != adObject1.GetType())
			{
				failureReason = string.Format("Types mismatch. ADObject1 : {0}, ADObject2 : {1}", adObject1.GetType().FullName, adObject2.GetType().FullName);
				return false;
			}
			IEnumerable<ProviderPropertyDefinition> source = from property in DalHelper.GetDeclaredReflectedProperties(adObject1).OfType<ADPropertyDefinition>()
			where !property.IsCalculated && !property.IsReadOnly
			where !DalHelper.ArePropertyValuesEqual(adObject1, adObject2, property)
			select property;
			if (source.Any<ProviderPropertyDefinition>())
			{
				failureReason = string.Format("The value for properties on this object did not match: {0}", string.Join(",", (from property in source
				select property.Name).ToArray<string>()));
				return false;
			}
			return true;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000E694 File Offset: 0x0000C894
		internal static Dictionary<object, List<T>> SplitByPhysicalInstance<T>(IHashBucket hashBucket, PropertyDefinition partitionPropertyDefinition, IEnumerable<T> items, PropertyDefinition hashPropertyDefinition = null) where T : IConfigurable, IPropertyBag
		{
			if (partitionPropertyDefinition == null)
			{
				throw new ArgumentNullException("partitionPropertyDefinition");
			}
			if (hashBucket == null)
			{
				throw new ArgumentNullException("hashBucket.");
			}
			Dictionary<object, List<T>> dictionary = new Dictionary<object, List<T>>();
			if (items == null)
			{
				return dictionary;
			}
			foreach (T item in items)
			{
				object obj = item[partitionPropertyDefinition];
				if (obj == null)
				{
					throw new ArgumentNullException(string.Format("The partition key {0} cannot be null", partitionPropertyDefinition.Name));
				}
				int logicalHash = hashBucket.GetLogicalHash(obj.ToString());
				object physicalInstanceIdByHashValue = hashBucket.GetPhysicalInstanceIdByHashValue(logicalHash);
				List<T> list;
				if (!dictionary.TryGetValue(physicalInstanceIdByHashValue, out list))
				{
					list = new List<T>();
					dictionary.Add(physicalInstanceIdByHashValue, list);
				}
				if (hashPropertyDefinition != null)
				{
					item[hashPropertyDefinition] = logicalHash;
				}
				list.Add(item);
			}
			return dictionary;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000E784 File Offset: 0x0000C984
		internal static Dictionary<object, List<T>> SplitByPhysicalInstance<T>(IHashBucket hashBucket, IEnumerable<T> items, Func<T, string> itemFunc)
		{
			Dictionary<object, List<T>> dictionary = new Dictionary<object, List<T>>();
			foreach (T t in items)
			{
				object physicalInstanceId = hashBucket.GetPhysicalInstanceId(itemFunc(t));
				List<T> list;
				if (!dictionary.TryGetValue(physicalInstanceId, out list))
				{
					list = new List<T>();
					dictionary.Add(physicalInstanceId, list);
				}
				list.Add(t);
			}
			return dictionary;
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000E800 File Offset: 0x0000CA00
		internal static DataTable CreateDataTable(string tableName, HygienePropertyDefinition[] columnDefinitions, IEnumerable<ConfigurablePropertyBag> rows)
		{
			DataTable dataTable = new DataTable(tableName);
			DataColumnCollection columns = dataTable.Columns;
			foreach (HygienePropertyDefinition hygienePropertyDefinition in columnDefinitions)
			{
				if (!hygienePropertyDefinition.IsCalculated)
				{
					columns.Add(hygienePropertyDefinition.Name, (hygienePropertyDefinition.Type == typeof(byte[])) ? hygienePropertyDefinition.Type : DalHelper.ConvertToStoreType(hygienePropertyDefinition));
				}
			}
			foreach (ConfigurablePropertyBag configurablePropertyBag in rows)
			{
				DataRow dataRow = dataTable.NewRow();
				for (int j = 0; j < columnDefinitions.Length; j++)
				{
					HygienePropertyDefinition hygienePropertyDefinition2 = columnDefinitions[j];
					if (hygienePropertyDefinition2 != null && !hygienePropertyDefinition2.IsCalculated && configurablePropertyBag[hygienePropertyDefinition2] != null)
					{
						dataRow[j] = configurablePropertyBag[hygienePropertyDefinition2];
					}
				}
				dataTable.Rows.Add(dataRow);
			}
			return dataTable;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000E900 File Offset: 0x0000CB00
		internal static AdminAuditLogModifiedProperty CustomDeserializeAuditLogModifiedProperty(string serializedProperty)
		{
			string[] array = serializedProperty.Split(new string[]
			{
				"||"
			}, StringSplitOptions.None);
			if (array.Length != 3)
			{
				throw new ArgumentException(string.Format("Malformed serialized AuditLogModifiedProperty: '{0}'", serializedProperty));
			}
			AdminAuditLogModifiedProperty adminAuditLogModifiedProperty = AdminAuditLogModifiedProperty.Parse(string.Format("{0}={1}", DalHelper.UnescapePipe(array[0]), DalHelper.UnescapePipe(array[1])), false);
			adminAuditLogModifiedProperty.NewValue = DalHelper.UnescapePipe(array[2]);
			return adminAuditLogModifiedProperty;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000E96C File Offset: 0x0000CB6C
		internal static AdminAuditLogCmdletParameter CustomDeserializeAuditLogCmdletParameter(string serializedCmdlet)
		{
			string[] array = serializedCmdlet.Split(new string[]
			{
				"||"
			}, StringSplitOptions.None);
			if (array.Length != 2)
			{
				throw new ArgumentException(string.Format("Malformed serialized AuditLogCmdletParameter: {0}", serializedCmdlet));
			}
			return AdminAuditLogCmdletParameter.Parse(string.Format("{0}={1}", DalHelper.UnescapePipe(array[0]), DalHelper.UnescapePipe(array[1])));
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000E9C7 File Offset: 0x0000CBC7
		internal static string CustomSerializeAuditLogModifiedProperty(AdminAuditLogModifiedProperty property)
		{
			return string.Format("{0}||{1}||{2}", DalHelper.EscapePipe(property.Name), DalHelper.EscapePipe(property.OldValue), DalHelper.EscapePipe(property.NewValue));
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000E9F4 File Offset: 0x0000CBF4
		internal static string CustomSerializeAuditLogCmdletParameter(AdminAuditLogCmdletParameter parameter)
		{
			return string.Format("{0}||{1}", DalHelper.EscapePipe(parameter.Name), DalHelper.EscapePipe(parameter.Value));
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000EA18 File Offset: 0x0000CC18
		internal static bool IsTracerToken(IConfigurable configurable)
		{
			IPropertyBag propertyBag = configurable as IPropertyBag;
			return propertyBag != null && (bool)propertyBag[DalHelper.IsTracerTokenProp];
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000EA41 File Offset: 0x0000CC41
		private static string EscapePipe(string stringToEscape)
		{
			if (string.IsNullOrWhiteSpace(stringToEscape))
			{
				return string.Empty;
			}
			if (!stringToEscape.Contains("|"))
			{
				return stringToEscape;
			}
			return stringToEscape.Replace("|", "\\|");
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000EA70 File Offset: 0x0000CC70
		private static string UnescapePipe(string stringToUnescape)
		{
			if (string.IsNullOrWhiteSpace(stringToUnescape))
			{
				return string.Empty;
			}
			if (!stringToUnescape.Contains("|"))
			{
				return stringToUnescape;
			}
			return stringToUnescape.Replace("\\|", "|");
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000EAA0 File Offset: 0x0000CCA0
		private static bool ArePropertyValuesEqual(ADObject adObject1, ADObject adObject2, ADPropertyDefinition property)
		{
			object obj = adObject1[property];
			object obj2 = adObject2[property];
			if (adObject1 is TransportRule && (property == TransportRuleSchema.Xml || property == TransportRuleSchema.Priority))
			{
				return true;
			}
			if ((adObject1 is MalwareFilterPolicy && property == MalwareFilterPolicySchema.MalwareFilterPolicyFlags) || (adObject1 is HostedConnectionFilterPolicy && property == HostedConnectionFilterPolicySchema.ConnectionFilterFlags) || (adObject1 is HostedContentFilterPolicy && property == HostedContentFilterPolicySchema.SpamFilteringFlags) || (adObject1 is HostedContentFilterPolicy && property == HostedContentFilterPolicySchema.AsfSettings) || (adObject1 is HostedOutboundSpamFilterPolicy && property == HostedOutboundSpamFilterPolicySchema.OutboundSpamFilterFlags))
			{
				return true;
			}
			if ((property.Flags & ADPropertyDefinitionFlags.WriteOnce) == ADPropertyDefinitionFlags.WriteOnce)
			{
				return true;
			}
			if ((property.Flags & ADPropertyDefinitionFlags.PersistDefaultValue) == ADPropertyDefinitionFlags.PersistDefaultValue && DalHelper.ArePropertyValuesEqual(property.DefaultValue, obj2, property.IsMultivalued) && obj == null)
			{
				return true;
			}
			if (property.Type == typeof(ADObjectId))
			{
				return DalHelper.AreADObjectIdsEqual(obj, obj2, property.IsMultivalued);
			}
			if ((property.Flags & ADPropertyDefinitionFlags.PersistDefaultValue) == ADPropertyDefinitionFlags.PersistDefaultValue)
			{
				if (obj2 == null && DalHelper.ArePropertyValuesEqual(property.DefaultValue, obj, property.IsMultivalued))
				{
					return true;
				}
				if (obj == null && DalHelper.ArePropertyValuesEqual(property.DefaultValue, obj2, property.IsMultivalued))
				{
					return true;
				}
			}
			return DalHelper.ArePropertyValuesEqual(obj, obj2, property.IsMultivalued);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000EBE0 File Offset: 0x0000CDE0
		internal static bool AreADObjectIdsEqual(object leftHand, object rightHand, bool isMultiValued)
		{
			if (leftHand == null || rightHand == null)
			{
				return leftHand == null && rightHand == null;
			}
			if (isMultiValued)
			{
				MultiValuedPropertyBase multiValuedPropertyBase = leftHand as MultiValuedPropertyBase;
				MultiValuedPropertyBase multiValuedPropertyBase2 = rightHand as MultiValuedPropertyBase;
				if (multiValuedPropertyBase == null || multiValuedPropertyBase2 == null)
				{
					throw new InvalidOperationException("Attempting to compare non-MVP<ADObjectId> in an MVP<ADObjectId>-specific method.");
				}
				object[] array = (from ADObjectId val in multiValuedPropertyBase
				orderby val.Name
				select val).ToArray<ADObjectId>();
				object[] array2 = (from ADObjectId val in multiValuedPropertyBase2
				orderby val.Name
				select val).ToArray<ADObjectId>();
				if (array.Length != array2.Length)
				{
					return false;
				}
				for (int i = 0; i < array.Length; i++)
				{
					if (!DalHelper.AreADObjectIdsEqual(array[i], array2[i], false))
					{
						return false;
					}
				}
				return true;
			}
			else
			{
				ADObjectId adobjectId = (ADObjectId)leftHand;
				ADObjectId adobjectId2 = (ADObjectId)rightHand;
				if (adobjectId == null || adobjectId2 == null)
				{
					throw new InvalidOperationException("Attempting to compare non-ADObjectIds in an ADObjectId-specific method.");
				}
				return string.Equals(adobjectId.Name, adobjectId2.Name, StringComparison.InvariantCultureIgnoreCase);
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0000ED0C File Offset: 0x0000CF0C
		private static bool ArePropertyValuesEqual(object leftHand, object rightHand, bool isMultiValued)
		{
			if (leftHand == null || rightHand == null)
			{
				return leftHand == null && rightHand == null;
			}
			if (!isMultiValued)
			{
				string strB = DalHelper.ConvertToStoreObject(leftHand, SqlDbType.Variant).ToString();
				string strA = DalHelper.ConvertToStoreObject(rightHand, SqlDbType.Variant).ToString();
				return string.Compare(strA, strB, StringComparison.OrdinalIgnoreCase) == 0;
			}
			IEnumerable<string> first = from object obj in (MultiValuedPropertyBase)leftHand
			select DalHelper.ConvertToStoreObject(obj, SqlDbType.Variant).ToString() into val
			orderby val
			select val;
			IEnumerable<string> second = from object obj in (MultiValuedPropertyBase)rightHand
			select DalHelper.ConvertToStoreObject(obj, SqlDbType.Variant).ToString() into val
			orderby val
			select val;
			return first.SequenceEqual(second, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0000EE00 File Offset: 0x0000D000
		private static string GetDistinguishedName(string dn)
		{
			return DalHelper.FfoRootDN.GetChildId("TenantName").GetChildId(dn).DistinguishedName;
		}

		// Token: 0x04000294 RID: 660
		public const string WellKnownExoDalClientCallerId = "ExoDalClientCallerId";

		// Token: 0x04000295 RID: 661
		public const string FakeTenantName = "TenantName";

		// Token: 0x04000296 RID: 662
		public const int TotalBits = 32;

		// Token: 0x04000297 RID: 663
		public const int MaxCidrBits = 9;

		// Token: 0x04000298 RID: 664
		public static readonly ADObjectId FfoRootDN = new ADObjectId("OU=Microsoft Exchange Hosted Organizations,DC=FFO,DC=extest,DC=microsoft,DC=com", Guid.Empty);

		// Token: 0x04000299 RID: 665
		public static readonly HygienePropertyDefinition TenantIdProp = new HygienePropertyDefinition("tenantId", typeof(ADObjectId));

		// Token: 0x0400029A RID: 666
		public static readonly HygienePropertyDefinition SettingTypeProp = new HygienePropertyDefinition("settingType", typeof(string));

		// Token: 0x0400029B RID: 667
		public static readonly HygienePropertyDefinition ConfigIdProp = new HygienePropertyDefinition("configId", typeof(ADObjectId));

		// Token: 0x0400029C RID: 668
		public static readonly HygienePropertyDefinition ContainerProp = new HygienePropertyDefinition("container", typeof(string));

		// Token: 0x0400029D RID: 669
		public static readonly HygienePropertyDefinition PropertyNameProp = new HygienePropertyDefinition("PropertyName", typeof(string));

		// Token: 0x0400029E RID: 670
		public static readonly HygienePropertyDefinition PropertyValueStringProp = new HygienePropertyDefinition("PropertyValueString", typeof(string));

		// Token: 0x0400029F RID: 671
		public static readonly HygienePropertyDefinition PropertyValueGuidProp = new HygienePropertyDefinition("PropertyValueGuid", typeof(Guid));

		// Token: 0x040002A0 RID: 672
		public static readonly HygienePropertyDefinition PropertyValueIntegerProp = new HygienePropertyDefinition("PropertyValueInteger", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040002A1 RID: 673
		public static readonly HygienePropertyDefinition PropertyValueIntegerMaskProp = new HygienePropertyDefinition("PropertyValueIntegerMask", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040002A2 RID: 674
		public static readonly HygienePropertyDefinition StartTimeProp = new HygienePropertyDefinition("startTime", typeof(DateTime), SqlDateTime.MaxValue.Value, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040002A3 RID: 675
		public static readonly HygienePropertyDefinition StoredProcOutputBagProp = new HygienePropertyDefinition("storedProcOutputBag", typeof(object), null, ADPropertyDefinitionFlags.MultiValued);

		// Token: 0x040002A4 RID: 676
		public static readonly HygienePropertyDefinition CacheOutputBagProp = new HygienePropertyDefinition("cacheOutputBag", typeof(object), null, ADPropertyDefinitionFlags.MultiValued);

		// Token: 0x040002A5 RID: 677
		public static readonly HygienePropertyDefinition PageSizeProp = new HygienePropertyDefinition("pageSize", typeof(int), 1000, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040002A6 RID: 678
		public static readonly HygienePropertyDefinition ObjectStateProp = new HygienePropertyDefinition("ObjectState", typeof(ObjectState), ObjectState.New, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040002A7 RID: 679
		public static readonly HygienePropertyDefinition PageCookieProp = new HygienePropertyDefinition("pageCookie", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040002A8 RID: 680
		public static readonly HygienePropertyDefinition FinishedReadingAllPagesProp = new HygienePropertyDefinition("FinishedReadingAllPages", typeof(bool), false, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040002A9 RID: 681
		public static readonly HygienePropertyDefinition RetrievedAllPagesProp = new HygienePropertyDefinition("RetrievedAllPages", typeof(int), -1, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040002AA RID: 682
		public static readonly HygienePropertyDefinition PhysicalInstanceKeyProp = new HygienePropertyDefinition("physicalInstanceKey", typeof(object));

		// Token: 0x040002AB RID: 683
		public static readonly HygienePropertyDefinition FssCopyIdProp = new HygienePropertyDefinition("fssCopyId", typeof(object));

		// Token: 0x040002AC RID: 684
		public static readonly HygienePropertyDefinition BuildVersionProp = new HygienePropertyDefinition("buildVersion", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040002AD RID: 685
		public static readonly HygienePropertyDefinition SessionNameProp = new HygienePropertyDefinition("sessionName", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040002AE RID: 686
		public static readonly HygienePropertyDefinition HashBucketProp = new HygienePropertyDefinition("HashBucket", typeof(short), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040002AF RID: 687
		public static readonly HygienePropertyDefinition FilteringOperatorProp = new HygienePropertyDefinition("_QueryFilterOperator_", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040002B0 RID: 688
		public static readonly HygienePropertyDefinition ActiveOnlyProperty = KesSpamSchema.ActiveOnlyProperty;

		// Token: 0x040002B1 RID: 689
		public static readonly HygienePropertyDefinition IncludeTombstonesProperty = new HygienePropertyDefinition("IncludeTombstones", typeof(bool), false, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040002B2 RID: 690
		public static readonly HygienePropertyDefinition PropertyOldName = new HygienePropertyDefinition("OldName", typeof(string), string.Empty, ExchangeObjectVersion.Exchange2003, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040002B3 RID: 691
		public static readonly HygienePropertyDefinition IdOperator = new HygienePropertyDefinition("IdOperator", typeof(MatchOptions), MatchOptions.FullString, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040002B4 RID: 692
		public static readonly HygienePropertyDefinition WstDataProviderSettingsProp = new HygienePropertyDefinition("WstDataProviderSettingsProp", typeof(WstDataProviderSettings), WstDataProviderSettings.Default, ExchangeObjectVersion.Exchange2003, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040002B5 RID: 693
		public static readonly HygienePropertyDefinition LocalIdentifierProperty = new HygienePropertyDefinition("localIdentifier", typeof(ADObjectId), null, ExchangeObjectVersion.Exchange2003, ADPropertyDefinitionFlags.None);

		// Token: 0x040002B6 RID: 694
		public static readonly HygienePropertyDefinition CacheFailoverModeProp = new HygienePropertyDefinition("CacheFailoverMode", typeof(CacheFailoverMode), CacheFailoverMode.Default, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040002B7 RID: 695
		public static readonly HygienePropertyDefinition CachePrimingStateProp = new HygienePropertyDefinition("CachePrimingState", typeof(CachePrimingState), CachePrimingState.Unknown, ExchangeObjectVersion.Exchange2007, ADPropertyDefinitionFlags.None);

		// Token: 0x040002B8 RID: 696
		public static readonly HygienePropertyDefinition CacheHitProp = new HygienePropertyDefinition("CacheHit", typeof(bool), false, ExchangeObjectVersion.Exchange2007, ADPropertyDefinitionFlags.None);

		// Token: 0x040002B9 RID: 697
		public static readonly HygienePropertyDefinition BloomHitProp = new HygienePropertyDefinition("BloomHit", typeof(bool), false, ExchangeObjectVersion.Exchange2007, ADPropertyDefinitionFlags.None);

		// Token: 0x040002BA RID: 698
		public static readonly HygienePropertyDefinition InMemoryCacheHitProp = new HygienePropertyDefinition("InMemCacheHit", typeof(bool), false, ExchangeObjectVersion.Exchange2007, ADPropertyDefinitionFlags.None);

		// Token: 0x040002BB RID: 699
		public static readonly HygienePropertyDefinition IncludeTracerTokenProp = new HygienePropertyDefinition("IncludeTracerToken", typeof(bool), false, ExchangeObjectVersion.Exchange2007, ADPropertyDefinitionFlags.None);

		// Token: 0x040002BC RID: 700
		public static readonly HygienePropertyDefinition IsTracerTokenProp = new HygienePropertyDefinition("IsTracerToken", typeof(bool), false, ExchangeObjectVersion.Exchange2007, ADPropertyDefinitionFlags.None);

		// Token: 0x040002BD RID: 701
		public static readonly HygienePropertyDefinition WhenChangedProp = new HygienePropertyDefinition("WhenChangedUTC", typeof(DateTime?), null, ExchangeObjectVersion.Exchange2007, ADPropertyDefinitionFlags.None);

		// Token: 0x040002BE RID: 702
		public static readonly HygienePropertyDefinition TenantIds = new HygienePropertyDefinition("OrganizationalUnitRoots", typeof(Guid), null, ADPropertyDefinitionFlags.MultiValued);

		// Token: 0x040002BF RID: 703
		public static readonly string NullString = "<NULL>";

		// Token: 0x040002C0 RID: 704
		private static readonly Lazy<PropertyDefinition[]> globalProperties = new Lazy<PropertyDefinition[]>(() => (from field in typeof(DalHelper).GetFields(BindingFlags.Static | BindingFlags.Public)
		select field.GetValue(null)).OfType<PropertyDefinition>().ToArray<PropertyDefinition>());

		// Token: 0x040002C1 RID: 705
		private static readonly PropertyDefinition[] ADObjectExtensionProperties = new PropertyDefinition[]
		{
			DalHelper.BuildVersionProp,
			DalHelper.SessionNameProp,
			CommonSyncProperties.BatchIdProp,
			CommonSyncProperties.ObjectTypeProp,
			CommonSyncProperties.ProvisioningFlagsProperty,
			CommonSyncProperties.ServiceInstanceProp,
			CommonSyncProperties.SyncTypeProp,
			CommonSyncProperties.SyncOnlyProp,
			AuditHelper.UserIdProp,
			AuditHelper.AuditIdProp
		};

		// Token: 0x040002C2 RID: 706
		private static readonly PropertyDefinition[] SyncExtensionProperties = new PropertyDefinition[]
		{
			SyncGroupSchema.WellKnownObject
		};

		// Token: 0x040002C3 RID: 707
		private static string regionTag;

		// Token: 0x040002C4 RID: 708
		[ThreadStatic]
		private static string serviceTagInTLS;

		// Token: 0x040002C5 RID: 709
		private static object regionTagLock = new object();

		// Token: 0x040002C6 RID: 710
		private static SHA1Cng shaHasher = new SHA1Cng();
	}
}
