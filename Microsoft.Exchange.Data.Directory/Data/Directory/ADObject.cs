using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Security.AccessControl;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000021 RID: 33
	[Serializable]
	public abstract class ADObject : ADRawEntry, IVersionable, IADObject, IADRawEntry, IConfigurable, IPropertyBag, IReadOnlyPropertyBag
	{
		// Token: 0x17000069 RID: 105
		internal override object this[PropertyDefinition propertyDefinition]
		{
			get
			{
				object result;
				try
				{
					result = base[propertyDefinition];
				}
				catch (ArgumentException)
				{
					ExWatson.AddExtraData(string.Format("\r\nthis.Identity = {0}\r\nthis.ObjectState = {1}\r\nthis.IsReadOnly = {2}\r\nthis.session = {3}\r\nthis.session.ReadOnly = {4}\r\nthis.propertyBag.Keys.Count = {5}\r\nthis.ObjectSchema.AllProperties.Count = {6}\r\n", new object[]
					{
						this.Identity,
						base.ObjectState,
						base.IsReadOnly,
						(this.m_Session == null) ? "<null>" : this.m_Session.ToString(),
						(this.m_Session == null) ? "<null>" : this.m_Session.ReadOnly.ToString(),
						this.propertyBag.Keys.Count,
						this.Schema.AllProperties.Count
					}));
					ExWatson.AddExtraData("this.propertyBag.Keys = " + ADObject.CollectionToString(this.propertyBag.Keys));
					ExWatson.AddExtraData("this.Schema.AllProperties = " + ADObject.CollectionToString(this.Schema.AllProperties));
					throw;
				}
				return result;
			}
			set
			{
				base[propertyDefinition] = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000C21A File Offset: 0x0000A41A
		internal virtual bool IsShareable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000C220 File Offset: 0x0000A420
		private static string CollectionToString(ICollection collection)
		{
			List<PropertyDefinition> list = new List<PropertyDefinition>(collection.Cast<PropertyDefinition>());
			List<string> list2 = new List<string>(list.Count);
			foreach (PropertyDefinition propertyDefinition in list)
			{
				string item = string.Empty;
				if (propertyDefinition != null)
				{
					item = propertyDefinition.ToString();
				}
				list2.Add(item);
			}
			list2.Sort(StringComparer.OrdinalIgnoreCase);
			return string.Join(", ", list2.ToArray());
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000C2B4 File Offset: 0x0000A4B4
		internal static object PropertyValueFromEqualityFilter(SinglePropertyFilter filter)
		{
			return ADObject.PropertyValueFromComparisonFilter(filter, new List<ComparisonOperator>
			{
				ComparisonOperator.Equal
			});
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000C2D8 File Offset: 0x0000A4D8
		internal static object PropertyValueFromComparisonFilter(SinglePropertyFilter filter, List<ComparisonOperator> allowedOperators)
		{
			string name = filter.Property.Name;
			ComparisonFilter comparisonFilter = filter as ComparisonFilter;
			if (comparisonFilter == null)
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForProperty(name, filter.GetType(), typeof(ComparisonFilter)));
			}
			if (allowedOperators != null && !allowedOperators.Contains(comparisonFilter.ComparisonOperator))
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedOperatorForProperty(name, comparisonFilter.ComparisonOperator.ToString()));
			}
			object propertyValue = comparisonFilter.PropertyValue;
			if (propertyValue == null)
			{
				return ((ADPropertyDefinition)filter.Property).DefaultValue;
			}
			if (!ADObject.CorrectType(filter.Property, propertyValue.GetType()))
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedPropertyValueType(name, propertyValue.GetType(), filter.Property.Type));
			}
			return propertyValue;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000C390 File Offset: 0x0000A590
		internal static SinglePropertyFilter DummyCustomFilterBuilderDelegate(SinglePropertyFilter filter)
		{
			return filter;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000C394 File Offset: 0x0000A594
		private static bool CorrectType(PropertyDefinition propertyDefinition, Type valueType)
		{
			if (valueType == propertyDefinition.Type)
			{
				return true;
			}
			if (propertyDefinition.Type.GetTypeInfo().IsGenericType && propertyDefinition.Type.GetTypeInfo().GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				Type[] genericTypeArguments = propertyDefinition.Type.GetTypeInfo().GenericTypeArguments;
				return genericTypeArguments[0] == valueType;
			}
			return false;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000C400 File Offset: 0x0000A600
		internal static ComparisonFilter ObjectCategoryFilter(string objectCategory)
		{
			return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, objectCategory);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000C410 File Offset: 0x0000A610
		internal static ComparisonFilter ObjectClassFilter(string objectClass, bool isAtomic)
		{
			return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, objectClass)
			{
				IsAtomic = isAtomic
			};
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000C432 File Offset: 0x0000A632
		internal static ComparisonFilter ObjectClassFilter(string objectClass)
		{
			return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, objectClass);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000C440 File Offset: 0x0000A640
		internal static QueryFilter BoolFilterBuilder(SinglePropertyFilter filter, QueryFilter trueFilter)
		{
			if (!(filter is ComparisonFilter))
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForProperty(filter.Property.Name, filter.GetType(), typeof(ComparisonFilter)));
			}
			ComparisonFilter comparisonFilter = (ComparisonFilter)filter;
			if (comparisonFilter.ComparisonOperator != ComparisonOperator.Equal && ComparisonOperator.NotEqual != comparisonFilter.ComparisonOperator)
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedOperatorForProperty(comparisonFilter.Property.Name, comparisonFilter.ComparisonOperator.ToString()));
			}
			if ((comparisonFilter.ComparisonOperator == ComparisonOperator.Equal && (bool)comparisonFilter.PropertyValue) || (ComparisonOperator.NotEqual == comparisonFilter.ComparisonOperator && !(bool)comparisonFilter.PropertyValue))
			{
				return trueFilter;
			}
			return new NotFilter(trueFilter);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000C4EC File Offset: 0x0000A6EC
		internal virtual void Initialize()
		{
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000C4EE File Offset: 0x0000A6EE
		internal override void SetIsReadOnly(bool valueToSet)
		{
			if (base.IsReadOnly && !valueToSet)
			{
				this.m_Session = null;
			}
			base.SetIsReadOnly(valueToSet);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000C50C File Offset: 0x0000A70C
		internal bool IsApplicableToTenant()
		{
			ObjectScopeAttribute objectScopeAttribute;
			return this.IsApplicableToTenant(out objectScopeAttribute);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000C521 File Offset: 0x0000A721
		internal bool IsApplicableToTenant(out ObjectScopeAttribute objectScope)
		{
			objectScope = base.GetType().GetTypeInfo().GetCustomAttribute<ObjectScopeAttribute>();
			return objectScope != null && objectScope.IsTenant;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000C544 File Offset: 0x0000A744
		internal bool IsGlobal()
		{
			ObjectScopeAttribute customAttribute = base.GetType().GetTypeInfo().GetCustomAttribute<ObjectScopeAttribute>();
			return customAttribute != null && customAttribute.ConfigScope == ConfigScopes.Global;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000C570 File Offset: 0x0000A770
		public ADObject()
		{
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001EC RID: 492
		internal abstract ADObjectSchema Schema { get; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000C578 File Offset: 0x0000A778
		internal sealed override ObjectSchema ObjectSchema
		{
			get
			{
				return this.Schema;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001EE RID: 494
		internal abstract string MostDerivedObjectClass { get; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000C580 File Offset: 0x0000A780
		internal virtual string ObjectCategoryName
		{
			get
			{
				return this.MostDerivedObjectClass;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000C588 File Offset: 0x0000A788
		internal virtual QueryFilter ImplicitFilter
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, this.MostDerivedObjectClass);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000C59C File Offset: 0x0000A79C
		internal virtual QueryFilter VersioningFilter
		{
			get
			{
				return new NotFilter(new AndFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ADObjectSchema.ExchangeVersion, this.MaximumSupportedExchangeObjectVersion.NextMajorVersion),
					new ExistsFilter(ADObjectSchema.ExchangeVersion)
				}));
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000C5E1 File Offset: 0x0000A7E1
		ObjectSchema IVersionable.ObjectSchema
		{
			get
			{
				return this.Schema;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000C5E9 File Offset: 0x0000A7E9
		public new ExchangeObjectVersion ExchangeVersion
		{
			get
			{
				return base.ExchangeVersion;
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000C5F4 File Offset: 0x0000A7F4
		internal static object WhenCreatedGetter(IPropertyBag propertyBag)
		{
			string text = (string)propertyBag[ADObjectSchema.WhenCreatedRaw];
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			return new DateTime?(DateTime.ParseExact(text, "yyyyMMddHHmmss'.0Z'", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal));
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000C638 File Offset: 0x0000A838
		internal static object WhenCreatedUTCGetter(IPropertyBag propertyBag)
		{
			string text = (string)propertyBag[ADObjectSchema.WhenCreatedRaw];
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			return new DateTime?(DateTime.ParseExact(text, "yyyyMMddHHmmss'.0Z'", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal));
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000C67C File Offset: 0x0000A87C
		internal static object WhenChangedGetter(IPropertyBag propertyBag)
		{
			string text = (string)propertyBag[ADObjectSchema.WhenChangedRaw];
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			return new DateTime?(DateTime.ParseExact(text, "yyyyMMddHHmmss'.0Z'", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal));
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000C6C0 File Offset: 0x0000A8C0
		internal static object WhenChangedUTCGetter(IPropertyBag propertyBag)
		{
			string text = (string)propertyBag[ADObjectSchema.WhenChangedRaw];
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			return new DateTime?(DateTime.ParseExact(text, "yyyyMMddHHmmss'.0Z'", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal));
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000C704 File Offset: 0x0000A904
		internal static object CanonicalNameGetter(IPropertyBag propertyBag)
		{
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[ADObjectSchema.RawCanonicalName];
			if (multiValuedProperty != null && multiValuedProperty.Count > 0)
			{
				return multiValuedProperty[0].TrimEnd(new char[]
				{
					'/'
				});
			}
			return string.Empty;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000C750 File Offset: 0x0000A950
		internal static object DistinguishedNameGetter(IPropertyBag propertyBag)
		{
			ADObjectId adobjectId = (ADObjectId)propertyBag[ADObjectSchema.Id];
			if (adobjectId != null)
			{
				return adobjectId.DistinguishedName;
			}
			return string.Empty;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000C780 File Offset: 0x0000A980
		internal static void DistinguishedNameSetter(object value, IPropertyBag propertyBag)
		{
			ADObjectId adobjectId = (ADObjectId)propertyBag[ADObjectSchema.Id];
			propertyBag[ADObjectSchema.Id] = new ADObjectId((string)value, (adobjectId == null) ? Guid.Empty : adobjectId.ObjectGuid);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000C7C4 File Offset: 0x0000A9C4
		internal static object GuidGetter(IPropertyBag propertyBag)
		{
			ADObjectId adobjectId = (ADObjectId)propertyBag[ADObjectSchema.Id];
			return (adobjectId == null) ? Guid.Empty : adobjectId.ObjectGuid;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000C834 File Offset: 0x0000AA34
		internal static GetterDelegate FlagGetterDelegate(int mask, ProviderPropertyDefinition propertyDefinition)
		{
			return delegate(IPropertyBag bag)
			{
				int num = (int)bag[propertyDefinition];
				bool value = 0 != (mask & num);
				return BoxedConstants.GetBool(value);
			};
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000C8B8 File Offset: 0x0000AAB8
		internal static SetterDelegate FlagSetterDelegate(int mask, ProviderPropertyDefinition propertyDefinition)
		{
			return delegate(object value, IPropertyBag bag)
			{
				int num = (int)bag[propertyDefinition];
				bag[propertyDefinition] = (((bool)value) ? (num | mask) : (num & ~mask));
			};
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000C928 File Offset: 0x0000AB28
		internal static GetterDelegate LongFlagGetterDelegate(long mask, ProviderPropertyDefinition propertyDefinition)
		{
			return delegate(IPropertyBag bag)
			{
				long num = (long)bag[propertyDefinition];
				bool value = 0L != (mask & num);
				return BoxedConstants.GetBool(value);
			};
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000C9AC File Offset: 0x0000ABAC
		internal static SetterDelegate LongFlagSetterDelegate(long mask, ProviderPropertyDefinition propertyDefinition)
		{
			return delegate(object value, IPropertyBag bag)
			{
				long num = (long)bag[propertyDefinition];
				bag[propertyDefinition] = (((bool)value) ? (num | mask) : (num & ~mask));
			};
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000CA60 File Offset: 0x0000AC60
		internal static GetterDelegate OWAFlagGetterDelegate(int mask, ProviderPropertyDefinition propertyDefinition)
		{
			return delegate(IPropertyBag bag)
			{
				uint? num = (uint?)bag[propertyDefinition];
				if (num != null)
				{
					long num2 = (long)mask;
					uint? num3 = num;
					bool value = 0L != ((num3 != null) ? new long?(num2 & (long)((ulong)num3.GetValueOrDefault())) : null);
					return BoxedConstants.GetBool(value);
				}
				return null;
			};
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000CB9C File Offset: 0x0000AD9C
		internal static SetterDelegate OWAFlagSetterDelegate(int mask, ProviderPropertyDefinition propertyDefinition, string description)
		{
			return delegate(object value, IPropertyBag bag)
			{
				uint? num = (uint?)bag[propertyDefinition];
				if (num == null)
				{
					num = new uint?(0U);
				}
				if (value != null)
				{
					PropertyDefinition propertyDefinition2 = propertyDefinition;
					uint? num5;
					if (!(bool)value)
					{
						uint? num2 = num;
						long num3 = (long)(~(long)mask);
						long? num4 = (num2 != null) ? new long?((long)((ulong)num2.GetValueOrDefault() & (ulong)num3)) : null;
						num5 = ((num4 != null) ? new uint?((uint)num4.GetValueOrDefault()) : null);
					}
					else
					{
						num5 = (num | new uint?((uint)mask));
					}
					bag[propertyDefinition2] = num5;
					return;
				}
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.ExceptionOwaCannotSetPropertyOnE12VirtualDirectoryToNull(description), propertyDefinition, value), null);
			};
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000CBD0 File Offset: 0x0000ADD0
		internal static object NameGetter(IPropertyBag propertyBag)
		{
			return propertyBag[ADObjectSchema.RawName];
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000CBDD File Offset: 0x0000ADDD
		internal static void NameSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[ADObjectSchema.RawName] = value;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000CBEB File Offset: 0x0000ADEB
		internal static object OrganizationIdGetter(IPropertyBag propertyBag)
		{
			return OrganizationId.Getter(propertyBag);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000CBF3 File Offset: 0x0000ADF3
		internal static void OrganizationIdSetter(object value, IPropertyBag propertyBag)
		{
			OrganizationId.Setter(value, propertyBag);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000CBFC File Offset: 0x0000ADFC
		internal static object ExchangeObjectIdGetter(IPropertyBag propertyBag)
		{
			Guid guid = (Guid)propertyBag[ADObjectSchema.ExchangeObjectIdRaw];
			if (guid.Equals(Guid.Empty))
			{
				guid = (Guid)propertyBag[ADObjectSchema.Guid];
			}
			return guid;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000CC3F File Offset: 0x0000AE3F
		internal static void ExchangeObjectIdSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[ADObjectSchema.ExchangeObjectIdRaw] = (Guid)value;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000CC58 File Offset: 0x0000AE58
		internal static object CorrelationIdGetter(IPropertyBag propertyBag)
		{
			Guid guid = (Guid)propertyBag[ADObjectSchema.CorrelationIdRaw];
			if (guid.Equals(Guid.Empty))
			{
				guid = (Guid)propertyBag[ADObjectSchema.Guid];
			}
			return guid;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000CC9B File Offset: 0x0000AE9B
		internal static void CorrelationIdSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[ADObjectSchema.CorrelationIdRaw] = (Guid)value;
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000CCB3 File Offset: 0x0000AEB3
		// (set) Token: 0x0600020B RID: 523 RVA: 0x0000CCC5 File Offset: 0x0000AEC5
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
		public string Name
		{
			get
			{
				return (string)this[ADObjectSchema.Name];
			}
			set
			{
				this[ADObjectSchema.Name] = value;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0000CCD3 File Offset: 0x0000AED3
		// (set) Token: 0x0600020D RID: 525 RVA: 0x0000CCE5 File Offset: 0x0000AEE5
		public string DistinguishedName
		{
			get
			{
				return (string)this[ADObjectSchema.DistinguishedName];
			}
			internal set
			{
				this[ADObjectSchema.DistinguishedName] = value;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000CCF4 File Offset: 0x0000AEF4
		public override ObjectId Identity
		{
			get
			{
				ObjectId originalId = this.OriginalId;
				object obj;
				if (base.TryConvertOutputProperty(originalId, ADObjectSchema.Id, out obj))
				{
					return (ObjectId)obj;
				}
				return originalId;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000CD20 File Offset: 0x0000AF20
		public Guid Guid
		{
			get
			{
				return (Guid)this[ADObjectSchema.Guid];
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000CD32 File Offset: 0x0000AF32
		// (set) Token: 0x06000211 RID: 529 RVA: 0x0000CD44 File Offset: 0x0000AF44
		public ADObjectId ObjectCategory
		{
			get
			{
				return (ADObjectId)this[ADObjectSchema.ObjectCategory];
			}
			internal set
			{
				this[ADObjectSchema.ObjectCategory] = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000CD52 File Offset: 0x0000AF52
		public MultiValuedProperty<string> ObjectClass
		{
			get
			{
				return (MultiValuedProperty<string>)this[ADObjectSchema.ObjectClass];
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000CD64 File Offset: 0x0000AF64
		internal ADObjectId OriginalId
		{
			get
			{
				object obj;
				this.propertyBag.TryGetOriginalValue(ADObjectSchema.Id, out obj);
				return ((ADObjectId)obj) ?? base.Id;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000CD94 File Offset: 0x0000AF94
		public DateTime? WhenChanged
		{
			get
			{
				return (DateTime?)this[ADObjectSchema.WhenChanged];
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000CDA6 File Offset: 0x0000AFA6
		public DateTime? WhenCreated
		{
			get
			{
				return (DateTime?)this[ADObjectSchema.WhenCreated];
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000CDB8 File Offset: 0x0000AFB8
		public DateTime? WhenChangedUTC
		{
			get
			{
				return (DateTime?)this[ADObjectSchema.WhenChangedUTC];
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000CDCA File Offset: 0x0000AFCA
		public DateTime? WhenCreatedUTC
		{
			get
			{
				return (DateTime?)this[ADObjectSchema.WhenCreatedUTC];
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000CDDC File Offset: 0x0000AFDC
		// (set) Token: 0x06000219 RID: 537 RVA: 0x0000CDEE File Offset: 0x0000AFEE
		internal ADObjectId SharedConfiguration
		{
			get
			{
				return (ADObjectId)this[ADObjectSchema.SharedConfiguration];
			}
			set
			{
				this[ADObjectSchema.SharedConfiguration] = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000CDFC File Offset: 0x0000AFFC
		// (set) Token: 0x0600021B RID: 539 RVA: 0x0000CE0E File Offset: 0x0000B00E
		internal byte[] ReplicationSignature
		{
			get
			{
				return (byte[])this[ADObjectSchema.ReplicationSignature];
			}
			set
			{
				this[ADObjectSchema.ReplicationSignature] = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000CE1C File Offset: 0x0000B01C
		// (set) Token: 0x0600021D RID: 541 RVA: 0x0000CE2E File Offset: 0x0000B02E
		internal Guid CorrelationId
		{
			get
			{
				return (Guid)this[ADObjectSchema.CorrelationId];
			}
			set
			{
				this[ADObjectSchema.CorrelationId] = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000CE41 File Offset: 0x0000B041
		// (set) Token: 0x0600021F RID: 543 RVA: 0x0000CE53 File Offset: 0x0000B053
		internal Guid ExchangeObjectId
		{
			get
			{
				return (Guid)this[ADObjectSchema.ExchangeObjectId];
			}
			set
			{
				this[ADObjectSchema.ExchangeObjectId] = value;
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000CE68 File Offset: 0x0000B068
		protected void SetObjectClass(string valueToSet)
		{
			MultiValuedProperty<string> value = new MultiValuedProperty<string>(true, ADObjectSchema.ObjectClass, new string[]
			{
				valueToSet
			});
			this.propertyBag.SetField(ADObjectSchema.ObjectClass, value);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000CE9F File Offset: 0x0000B09F
		internal RawSecurityDescriptor ReadSecurityDescriptor()
		{
			if (this.m_Session != null)
			{
				return this.m_Session.ReadSecurityDescriptor(base.Id);
			}
			return null;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000CEBC File Offset: 0x0000B0BC
		internal SecurityDescriptor ReadSecurityDescriptorBlob()
		{
			if (this.m_Session != null)
			{
				return this.m_Session.ReadSecurityDescriptorBlob(base.Id);
			}
			return null;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000CED9 File Offset: 0x0000B0D9
		internal void SaveSecurityDescriptor(RawSecurityDescriptor sd)
		{
			this.SaveSecurityDescriptor(sd, false);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000CEE3 File Offset: 0x0000B0E3
		internal void SaveSecurityDescriptor(RawSecurityDescriptor sd, bool modifyOwner)
		{
			if (this.m_Session != null)
			{
				this.m_Session.SaveSecurityDescriptor(this, sd, modifyOwner);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000CEFB File Offset: 0x0000B0FB
		internal ADObjectId ConfigurationUnit
		{
			get
			{
				return (ADObjectId)this[ADObjectSchema.ConfigurationUnit];
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000CF0D File Offset: 0x0000B10D
		internal ADObjectId OrganizationalUnitRoot
		{
			get
			{
				return (ADObjectId)this[ADObjectSchema.OrganizationalUnitRoot];
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000CF1F File Offset: 0x0000B11F
		// (set) Token: 0x06000228 RID: 552 RVA: 0x0000CF31 File Offset: 0x0000B131
		public OrganizationId OrganizationId
		{
			get
			{
				return (OrganizationId)this[ADObjectSchema.OrganizationId];
			}
			internal set
			{
				this[ADObjectSchema.OrganizationId] = value;
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000CF3F File Offset: 0x0000B13F
		public override string ToString()
		{
			if (this.Identity != null)
			{
				return this.Identity.ToString();
			}
			if (!string.IsNullOrEmpty(this.Name))
			{
				return this.Name;
			}
			return base.ToString();
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000CF70 File Offset: 0x0000B170
		internal void ProvisionalClone(IPropertyBag source)
		{
			foreach (PropertyDefinition propertyDefinition in this.Schema.AllProperties)
			{
				ADPropertyDefinition adpropertyDefinition = (ADPropertyDefinition)propertyDefinition;
				if (adpropertyDefinition.IncludeInProvisionalClone && !adpropertyDefinition.IsReadOnly && !adpropertyDefinition.IsCalculated && !adpropertyDefinition.IsTaskPopulated)
				{
					object obj = source[adpropertyDefinition];
					if (obj != null && this[adpropertyDefinition] != obj && (!adpropertyDefinition.IsMultivalued || ((MultiValuedPropertyBase)obj).Count != 0))
					{
						this[adpropertyDefinition] = obj;
					}
				}
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000D018 File Offset: 0x0000B218
		internal virtual void StampPersistableDefaultValues()
		{
			object obj = new object();
			foreach (PropertyDefinition propertyDefinition in this.Schema.AllProperties)
			{
				ADPropertyDefinition adpropertyDefinition = (ADPropertyDefinition)propertyDefinition;
				if (adpropertyDefinition.DefaultValue != null && !adpropertyDefinition.IsReadOnly && adpropertyDefinition.PersistDefaultValue && !this.ExchangeVersion.IsOlderThan(adpropertyDefinition.VersionAdded) && !this.propertyBag.TryGetField(adpropertyDefinition, ref obj))
				{
					this[adpropertyDefinition] = adpropertyDefinition.DefaultValue;
				}
			}
			ADLegacyVersionableObject adlegacyVersionableObject = this as ADLegacyVersionableObject;
			if (adlegacyVersionableObject != null)
			{
				adlegacyVersionableObject.StampDefaultMinAdminVersion();
			}
			this.SetObjectClass(this.MostDerivedObjectClass);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000D0DC File Offset: 0x0000B2DC
		internal static ADPropertyDefinition LegacyDnProperty(ADPropertyDefinitionFlags flags)
		{
			string text = "[a-zA-Z0-9 @!%&'),-.:<>}_\"\\|\\(\\*\\+\\?\\[\\]\\{]+";
			return new ADPropertyDefinition("LegacyExchangeDN", ExchangeObjectVersion.Exchange2003, typeof(string), "legacyExchangeDN", flags, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
			{
				new NoLeadingOrTrailingWhitespaceConstraint(),
				new RegexConstraint(string.Concat(new string[]
				{
					"^(/o=",
					text,
					"|/o=",
					text,
					"/ou=",
					text,
					"(/cn=",
					text,
					")*)$"
				}), RegexOptions.Compiled | RegexOptions.Singleline, DataStrings.LegacyDNPatternDescription)
			}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000D1C4 File Offset: 0x0000B3C4
		internal static GetterDelegate InvertFlagGetterDelegate(ProviderPropertyDefinition propertyDefinition, int mask)
		{
			return delegate(IPropertyBag bag)
			{
				int num = (int)bag[propertyDefinition];
				bool value = 0 == (mask & num);
				return BoxedConstants.GetBool(value);
			};
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000D230 File Offset: 0x0000B430
		internal static GetterDelegate FlagGetterDelegate(ProviderPropertyDefinition propertyDefinition, int mask)
		{
			return delegate(IPropertyBag bag)
			{
				int num = (int)bag[propertyDefinition];
				return BoxedConstants.GetBool(0 != (mask & num));
			};
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000D2B4 File Offset: 0x0000B4B4
		internal static SetterDelegate FlagSetterDelegate(ProviderPropertyDefinition propertyDefinition, int mask)
		{
			return delegate(object value, IPropertyBag bag)
			{
				int num = (int)bag[propertyDefinition];
				bag[propertyDefinition] = (((bool)value) ? (num | mask) : (num & ~mask));
			};
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000D338 File Offset: 0x0000B538
		internal static SetterDelegate InvertFlagSetterDelegate(ProviderPropertyDefinition propertyDefinition, int mask)
		{
			return delegate(object value, IPropertyBag bag)
			{
				int num = (int)bag[propertyDefinition];
				bag[propertyDefinition] = (((bool)value) ? (num & ~mask) : (num | mask));
			};
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000D365 File Offset: 0x0000B565
		internal static ADPropertyDefinition BitfieldProperty(string name, int shift, int length, ADPropertyDefinition fieldProperty)
		{
			return ADObject.BitfieldProperty(name, shift, length, fieldProperty, null);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000D374 File Offset: 0x0000B574
		internal static ADPropertyDefinition BitfieldProperty(string name, int shift, int length, ADPropertyDefinition fieldProperty, PropertyDefinitionConstraint constraint)
		{
			if (fieldProperty.Type == typeof(int))
			{
				return ADObject.Int32BitfieldProperty(name, shift, length, fieldProperty, constraint);
			}
			if (fieldProperty.Type == typeof(long))
			{
				return ADObject.Int64BitfieldProperty(name, shift, length, fieldProperty, constraint);
			}
			throw new Exception("BitfieldProperty does not support underlying property of type " + fieldProperty.Type.FullName);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000D570 File Offset: 0x0000B770
		internal static ADPropertyDefinition Int32BitfieldProperty(string name, int shift, int length, ADPropertyDefinition fieldProperty, PropertyDefinitionConstraint constraint)
		{
			object defaultValue = 0;
			GetterDelegate getterDelegate = delegate(IPropertyBag bag)
			{
				object obj = null;
				if (!(bag as ADPropertyBag).TryGetField(fieldProperty, ref obj))
				{
					return defaultValue;
				}
				if (obj == null)
				{
					return defaultValue;
				}
				int num = (int)obj;
				int num2 = (1 << length) - 1;
				return num2 & num >> shift;
			};
			SetterDelegate setterDelegate = delegate(object value, IPropertyBag bag)
			{
				int num = (int)bag[fieldProperty];
				int num2 = (1 << length) - 1;
				int num3 = (int)value;
				num &= ~(num2 << shift);
				num |= (num3 & num2) << shift;
				bag[fieldProperty] = num;
			};
			CustomFilterBuilderDelegate customFilterBuilderDelegate = delegate(SinglePropertyFilter filter)
			{
				ComparisonFilter comparisonFilter = filter as ComparisonFilter;
				if (comparisonFilter == null || comparisonFilter.ComparisonOperator != ComparisonOperator.Equal)
				{
					throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForProperty(filter.Property.Name, filter.GetType(), typeof(ComparisonFilter)));
				}
				int num = (int)comparisonFilter.PropertyValue;
				int num2 = (1 << length) - 1;
				uint num3 = (uint)((uint)(num & num2) << shift);
				uint num4 = (uint)((uint)(~(uint)num & num2) << shift);
				QueryFilter queryFilter = new NotFilter(new BitMaskOrFilter(fieldProperty, (ulong)num4));
				if (num != 0)
				{
					return new AndFilter(new QueryFilter[]
					{
						new BitMaskAndFilter(fieldProperty, (ulong)num3),
						queryFilter
					});
				}
				return queryFilter;
			};
			PropertyDefinitionConstraint[] readConstraints;
			if (constraint == null)
			{
				readConstraints = PropertyDefinitionConstraint.None;
			}
			else
			{
				readConstraints = new PropertyDefinitionConstraint[]
				{
					constraint
				};
			}
			return new ADPropertyDefinition(name, fieldProperty.VersionAdded, typeof(int), null, ADPropertyDefinitionFlags.Calculated, defaultValue, readConstraints, PropertyDefinitionConstraint.None, new ADPropertyDefinition[]
			{
				fieldProperty
			}, customFilterBuilderDelegate, getterDelegate, setterDelegate, null, null);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000D708 File Offset: 0x0000B908
		internal static ADPropertyDefinition Int64BitfieldProperty(string name, int shift, int length, ADPropertyDefinition fieldProperty, PropertyDefinitionConstraint constraint)
		{
			object defaultValue = 0L;
			GetterDelegate getterDelegate = delegate(IPropertyBag bag)
			{
				object obj = null;
				if (!(bag as ADPropertyBag).TryGetField(fieldProperty, ref obj))
				{
					return defaultValue;
				}
				if (obj == null)
				{
					return defaultValue;
				}
				long num = (long)obj;
				long num2 = (1L << length) - 1L;
				return num2 & num >> shift;
			};
			SetterDelegate setterDelegate = delegate(object value, IPropertyBag bag)
			{
				long num = (long)bag[fieldProperty];
				long num2 = (1L << length) - 1L;
				long num3 = (long)value;
				num &= ~(num2 << shift);
				num |= (num3 & num2) << shift;
				bag[fieldProperty] = num;
			};
			PropertyDefinitionConstraint[] readConstraints;
			if (constraint == null)
			{
				readConstraints = PropertyDefinitionConstraint.None;
			}
			else
			{
				readConstraints = new PropertyDefinitionConstraint[]
				{
					constraint
				};
			}
			return new ADPropertyDefinition(name, fieldProperty.VersionAdded, typeof(long), null, ADPropertyDefinitionFlags.Calculated, defaultValue, readConstraints, PropertyDefinitionConstraint.None, new ADPropertyDefinition[]
			{
				fieldProperty
			}, null, getterDelegate, setterDelegate, null, null);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000D7B8 File Offset: 0x0000B9B8
		internal static ADPropertyDefinition BitfieldProperty(string name, int shift, ADPropertyDefinition fieldProperty)
		{
			if (fieldProperty.Type == typeof(int))
			{
				return ADObject.Int32BitfieldProperty(name, shift, fieldProperty);
			}
			if (fieldProperty.Type == typeof(long))
			{
				return ADObject.Int64BitfieldProperty(name, shift, fieldProperty);
			}
			throw new Exception("BitfieldProperty does not support underlying property of type " + fieldProperty.Type.FullName);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000D8EC File Offset: 0x0000BAEC
		internal static ADPropertyDefinition Int32BitfieldProperty(string name, int shift, ADPropertyDefinition fieldProperty)
		{
			CustomFilterBuilderDelegate customFilterBuilderDelegate = delegate(SinglePropertyFilter filter)
			{
				ComparisonFilter comparisonFilter = filter as ComparisonFilter;
				if (comparisonFilter == null || comparisonFilter.ComparisonOperator != ComparisonOperator.Equal)
				{
					throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForProperty(filter.Property.Name, filter.GetType(), typeof(ComparisonFilter)));
				}
				int num = 1;
				int num2 = ((bool)comparisonFilter.PropertyValue) ? 1 : 0;
				int num3 = (1 << num) - 1;
				uint num4 = (uint)((uint)(num2 & num3) << shift);
				uint num5 = (uint)((uint)(~(uint)num2 & num3) << shift);
				QueryFilter queryFilter = new NotFilter(new BitMaskOrFilter(fieldProperty, (ulong)num5));
				if (num2 != 0)
				{
					return new AndFilter(new QueryFilter[]
					{
						new BitMaskAndFilter(fieldProperty, (ulong)num4),
						queryFilter
					});
				}
				return queryFilter;
			};
			return new ADPropertyDefinition(name, fieldProperty.VersionAdded, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ADPropertyDefinition[]
			{
				fieldProperty
			}, customFilterBuilderDelegate, ADObject.FlagGetterDelegate(1 << shift, fieldProperty), ADObject.FlagSetterDelegate(1 << shift, fieldProperty), null, null);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000D988 File Offset: 0x0000BB88
		internal static ADPropertyDefinition Int64BitfieldProperty(string name, int shift, ADPropertyDefinition fieldProperty)
		{
			return new ADPropertyDefinition(name, fieldProperty.VersionAdded, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ADPropertyDefinition[]
			{
				fieldProperty
			}, null, ADObject.LongFlagGetterDelegate(1L << shift, fieldProperty), ADObject.LongFlagSetterDelegate(1L << shift, fieldProperty), null, null);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000D9E8 File Offset: 0x0000BBE8
		internal static QueryFilter ExchangeObjectIdFilterBuilder(SinglePropertyFilter filter)
		{
			if (filter is ExistsFilter)
			{
				return new ExistsFilter(ADObjectSchema.ExchangeObjectIdRaw);
			}
			Guid guid = (Guid)ADObject.PropertyValueFromEqualityFilter(filter);
			return new OrFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ExchangeObjectIdRaw, guid),
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Guid, guid)
			});
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000DA4C File Offset: 0x0000BC4C
		internal static QueryFilter CorrelationIdFilterBuilder(SinglePropertyFilter filter)
		{
			if (filter is ExistsFilter)
			{
				return new ExistsFilter(ADObjectSchema.CorrelationIdRaw);
			}
			Guid guid = (Guid)ADObject.PropertyValueFromEqualityFilter(filter);
			return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.CorrelationIdRaw, guid);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000DA89 File Offset: 0x0000BC89
		internal static bool IsRecipientDirSynced(bool isDirSynced)
		{
			ExTraceGlobals.ADObjectTracer.TraceDebug<string>(0L, "<ADObject::IsRecipientDirSynced> return ({0})", isDirSynced.ToString());
			return isDirSynced;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000DAA4 File Offset: 0x0000BCA4
		internal bool IsConsumerOrganization()
		{
			return Globals.IsConsumerOrganization(this.OrganizationId);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000DAB4 File Offset: 0x0000BCB4
		protected override void ValidateRead(List<ValidationError> errors)
		{
			bool flag = true;
			if (this.OrganizationalUnitRoot != null && this.ConfigurationUnit != null)
			{
				string distinguishedName = base.Id.DistinguishedName;
				string distinguishedName2 = this.OrganizationalUnitRoot.DistinguishedName;
				string distinguishedName3 = this.ConfigurationUnit.DistinguishedName;
				if (!string.IsNullOrEmpty(distinguishedName3) && !distinguishedName3.ToLower().StartsWith("cn=configuration,"))
				{
					flag = false;
					errors.Add(new ObjectValidationError(DirectoryStrings.ErrorInvalidOrganizationId(distinguishedName, distinguishedName2 ?? "<null>", distinguishedName3 ?? "<null>"), this.Identity, string.Empty));
				}
			}
			else if (this.OrganizationalUnitRoot != null || this.ConfigurationUnit != null)
			{
				flag = false;
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorInvalidOrganizationId(base.Id.ToDNString(), (this.OrganizationalUnitRoot != null) ? this.OrganizationalUnitRoot.ToDNString() : "<null>", (this.ConfigurationUnit != null) ? this.ConfigurationUnit.ToDNString() : "<null>"), this.Identity, string.Empty));
			}
			foreach (ADPropertyDefinition adpropertyDefinition in this.Schema.AllADObjectLinkProperties)
			{
				MultiValuedProperty<ADObjectId> multiValuedProperty = null;
				if (adpropertyDefinition.IsMultivalued)
				{
					try
					{
						multiValuedProperty = (MultiValuedProperty<ADObjectId>)this.propertyBag[adpropertyDefinition];
						goto IL_16E;
					}
					catch (DataValidationException)
					{
						goto IL_16E;
					}
					goto IL_13E;
				}
				goto IL_13E;
				IL_16E:
				if (multiValuedProperty != null && (multiValuedProperty.Count != 1 || multiValuedProperty[0] != null))
				{
					foreach (ADObjectId adobjectId in multiValuedProperty)
					{
						if (!string.IsNullOrEmpty(adobjectId.DistinguishedName))
						{
							if ((base.Id == null || !base.Id.IsDeleted) && adobjectId.IsDeleted)
							{
								ExTraceGlobals.ValidationTracer.TraceWarning<string, string, string>(0L, "Object [{0}]. Property [{1}] is set to value [{2}], it is pointing to Deleted Objects container of Active Directory.", this.DistinguishedName.ToString(), adpropertyDefinition.Name, adobjectId.ToString());
								Globals.LogEvent(DirectoryEventLogConstants.Tuple_DeletedObjectIdLinked, adpropertyDefinition.Name, new object[]
								{
									this.DistinguishedName.ToString(),
									adpropertyDefinition.Name,
									adobjectId.ToString()
								});
							}
							if (!adpropertyDefinition.IsValidateInSameOrganization || (flag && this.ShouldValidatePropertyLinkInSameOrganization(adpropertyDefinition)))
							{
								this.ValidateSingleADObjectLinkValue(adpropertyDefinition, adobjectId, errors);
							}
						}
					}
					continue;
				}
				continue;
				IL_13E:
				ADObjectId adobjectId2 = null;
				try
				{
					adobjectId2 = (ADObjectId)this.propertyBag[adpropertyDefinition];
				}
				catch (DataValidationException)
				{
				}
				if (adobjectId2 != null)
				{
					multiValuedProperty = new MultiValuedProperty<ADObjectId>();
					multiValuedProperty.Add(adobjectId2);
					goto IL_16E;
				}
				goto IL_16E;
			}
			base.ValidateRead(errors);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000DDC4 File Offset: 0x0000BFC4
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			if (base.ObjectState == ObjectState.New)
			{
				this.StampPersistableDefaultValues();
			}
			base.ValidateWrite(errors);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000DDDB File Offset: 0x0000BFDB
		internal virtual bool ShouldValidatePropertyLinkInSameOrganization(ADPropertyDefinition property)
		{
			return true;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000DDE0 File Offset: 0x0000BFE0
		private void ValidateSingleADObjectLinkValue(ADPropertyDefinition propertyDefinition, ADObjectId value, List<ValidationError> errors)
		{
			if (this.m_Session != null && this.m_Session.GetType().Name.Equals("TopologyDiscoverySession"))
			{
				return;
			}
			if (this.m_Session != null && !value.IsDescendantOf(this.m_Session.GetRootDomainNamingContext()))
			{
				return;
			}
			if (propertyDefinition.IsValidateInFirstOrganization && this.m_Session != null)
			{
				ADObjectId adobjectId = null;
				try
				{
					adobjectId = this.m_Session.SessionSettings.RootOrgId;
				}
				catch (OrgContainerNotFoundException)
				{
				}
				if (adobjectId != null && !value.IsDescendantOf(adobjectId) && adobjectId.DomainId == value.DomainId && !value.DistinguishedName.ToLower().Contains(",cn=deleted objects,"))
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.ErrorLinkedADObjectNotInFirstOrganization(propertyDefinition.Name, value.ToString(), this.Identity.ToString(), adobjectId.ToString()), propertyDefinition, value));
					return;
				}
			}
			else if (propertyDefinition.IsValidateInSameOrganization)
			{
				if (this.OrganizationId.Equals(OrganizationId.ForestWideOrgId) && this.m_Session != null)
				{
					ADObjectId adobjectId2 = null;
					try
					{
						adobjectId2 = this.m_Session.SessionSettings.RootOrgId;
					}
					catch (OrgContainerNotFoundException)
					{
					}
					catch (TenantOrgContainerNotFoundException)
					{
					}
					bool flag = true;
					try
					{
						if (adobjectId2 != null && adobjectId2.DescendantDN(1).Name.ToLower().Equals("configuration"))
						{
							flag = false;
						}
					}
					catch (InvalidOperationException)
					{
					}
					if (adobjectId2 != null && flag)
					{
						ADObjectId childId = value.DomainId.GetChildId("OU", "Microsoft Exchange Hosted Organizations");
						ADObjectId configurationUnitsRoot = this.m_Session.GetConfigurationUnitsRoot();
						if ((value.IsDescendantOf(childId) || value.IsDescendantOf(configurationUnitsRoot)) && (!(this is ADConfigurationObject) || !base.Id.IsDescendantOf(configurationUnitsRoot)))
						{
							errors.Add(new PropertyValidationError(DirectoryStrings.ErrorLinkedADObjectNotInSameOrganization(propertyDefinition.Name, value.ToString(), this.Identity.ToString(), this.OrganizationId.ToString()), propertyDefinition, value));
							return;
						}
					}
				}
				else if (!this.OrganizationId.Equals(OrganizationId.ForestWideOrgId) && !value.DistinguishedName.ToLower().Contains(",cn=deleted objects,") && !value.IsDescendantOf(this.OrganizationId.OrganizationalUnit) && !value.IsDescendantOf(this.OrganizationId.ConfigurationUnit) && (!propertyDefinition.IsValidateInSharedConfig || this.SharedConfiguration == null || !value.IsDescendantOf(this.SharedConfiguration)))
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.ErrorLinkedADObjectNotInSameOrganization(propertyDefinition.Name, value.ToString(), this.Identity.ToString(), this.OrganizationId.ToString()), propertyDefinition, value));
				}
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000E0A4 File Offset: 0x0000C2A4
		internal override bool SkipFullPropertyValidation(ProviderPropertyDefinition propertyDefinition)
		{
			return (propertyDefinition.Name == ADObjectSchema.Name.Name && base.ObjectState == ObjectState.New) || base.SkipFullPropertyValidation(propertyDefinition);
		}

		// Token: 0x04000083 RID: 131
		internal const string HostedOrganizationsOrganizationalUnit = "Microsoft Exchange Hosted Organizations";

		// Token: 0x04000084 RID: 132
		internal static readonly string ConfigurationUnits = "ConfigurationUnits";

		// Token: 0x04000085 RID: 133
		internal static QueryFilter ObjectClassExistsFilter = new ExistsFilter(ADObjectSchema.ObjectClass);

		// Token: 0x04000086 RID: 134
		[NonSerialized]
		internal IDirectorySession m_Session;
	}
}
