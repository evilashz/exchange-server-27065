using System;
using System.Globalization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000150 RID: 336
	[Serializable]
	internal class MbxPropertyDefinition : SimpleProviderPropertyDefinition
	{
		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000E52 RID: 3666 RVA: 0x0004291C File Offset: 0x00040B1C
		// (set) Token: 0x06000E53 RID: 3667 RVA: 0x00042924 File Offset: 0x00040B24
		public PropTag PropTag { get; private set; }

		// Token: 0x06000E54 RID: 3668 RVA: 0x00042930 File Offset: 0x00040B30
		internal MbxPropertyDefinition(string name, PropTag propTag, Type type, PropertyDefinitionFlags flags, object defaultValue, ProviderPropertyDefinition[] supportingProperties, GetterDelegate getterDelegate = null, SetterDelegate setterDelegate = null) : this(name, propTag, ExchangeObjectVersion.Exchange2003, type, flags, defaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, supportingProperties, getterDelegate, setterDelegate)
		{
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x00042960 File Offset: 0x00040B60
		internal MbxPropertyDefinition(string name, PropTag propTag, ExchangeObjectVersion versionAdded, Type type, PropertyDefinitionFlags flags, object defaultValue, PropertyDefinitionConstraint[] readConstraints, PropertyDefinitionConstraint[] writeConstraints, ProviderPropertyDefinition[] supportingProperties, GetterDelegate getterDelegate, SetterDelegate setterDelegate) : base(name, versionAdded, type, flags, defaultValue, readConstraints, writeConstraints, supportingProperties, null, getterDelegate, setterDelegate)
		{
			this.PropTag = propTag;
			if (propTag == PropTag.Null && name == "Null")
			{
				throw new ArgumentException("Name should not be 'Null' is PropTag is Null");
			}
			if (propTag == PropTag.Null != this.IsCalculated)
			{
				throw new ArgumentException("PropTag must be Null IFF Calculated property");
			}
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x000429C0 File Offset: 0x00040BC0
		internal static MbxPropertyDefinition NullableBoolPropertyDefinition(PropTag propTag, string name = null, bool multivalued = false)
		{
			return new MbxPropertyDefinition(name ?? propTag.ToString(), propTag, ExchangeObjectVersion.Exchange2003, typeof(bool?), multivalued ? PropertyDefinitionFlags.MultiValued : PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null);
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00042A4C File Offset: 0x00040C4C
		internal static MbxPropertyDefinition BoolFromNullableBoolPropertyDefinition(string name, MbxPropertyDefinition rawPropertyDefinition, bool multivalued = false)
		{
			return new MbxPropertyDefinition(name, PropTag.Null, ExchangeObjectVersion.Exchange2003, typeof(bool), (multivalued ? PropertyDefinitionFlags.MultiValued : PropertyDefinitionFlags.None) | PropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new MbxPropertyDefinition[]
			{
				rawPropertyDefinition
			}, (IPropertyBag propertyBag) => propertyBag[rawPropertyDefinition] != null && (bool)propertyBag[rawPropertyDefinition], delegate(object value, IPropertyBag propertyBag)
			{
				propertyBag[rawPropertyDefinition] = value;
			});
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x00042AC0 File Offset: 0x00040CC0
		internal static MbxPropertyDefinition StringPropertyDefinition(PropTag propTag, string name = null, bool multivalued = false)
		{
			return new MbxPropertyDefinition(name ?? propTag.ToString(), propTag, ExchangeObjectVersion.Exchange2003, typeof(string), multivalued ? PropertyDefinitionFlags.MultiValued : PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null);
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x00042B54 File Offset: 0x00040D54
		internal static MbxPropertyDefinition SmtpDomainFromStringPropertyDefinition(string name, MbxPropertyDefinition rawPropertyDefinition, bool multivalued = false)
		{
			return new MbxPropertyDefinition(name, PropTag.Null, ExchangeObjectVersion.Exchange2003, typeof(SmtpDomain), (multivalued ? PropertyDefinitionFlags.MultiValued : PropertyDefinitionFlags.None) | PropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new MbxPropertyDefinition[]
			{
				rawPropertyDefinition
			}, delegate(IPropertyBag propertyBag)
			{
				if (propertyBag[rawPropertyDefinition] != null)
				{
					return SmtpDomain.Parse((string)propertyBag[rawPropertyDefinition]);
				}
				return null;
			}, delegate(object value, IPropertyBag propertyBag)
			{
				propertyBag[rawPropertyDefinition] = ((SmtpDomain)value).ToString();
			});
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x00042C0C File Offset: 0x00040E0C
		internal static MbxPropertyDefinition CountryInfoFromStringPropertyDefinition(string name, MbxPropertyDefinition rawPropertyDefinition, bool multivalued = false)
		{
			return new MbxPropertyDefinition(name, PropTag.Null, ExchangeObjectVersion.Exchange2003, typeof(CountryInfo), (multivalued ? PropertyDefinitionFlags.MultiValued : PropertyDefinitionFlags.None) | PropertyDefinitionFlags.Calculated, multivalued ? null : null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new MbxPropertyDefinition[]
			{
				rawPropertyDefinition
			}, delegate(IPropertyBag propertyBag)
			{
				if (propertyBag[rawPropertyDefinition] != null)
				{
					return CountryInfo.Parse((string)propertyBag[rawPropertyDefinition]);
				}
				return null;
			}, delegate(object value, IPropertyBag propertyBag)
			{
				propertyBag[rawPropertyDefinition] = ((CountryInfo)value).ToString();
			});
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x00042DCC File Offset: 0x00040FCC
		internal static MbxPropertyDefinition ProxyAddressFromStringPropertyDefinition(string name, MbxPropertyDefinition rawPropertyDefinition, bool multivalued = false)
		{
			GetterDelegate getterDelegate = delegate(IPropertyBag propertyBag)
			{
				if (propertyBag[rawPropertyDefinition] != null)
				{
					return ProxyAddress.Parse((string)propertyBag[rawPropertyDefinition]);
				}
				return null;
			};
			SetterDelegate setterDelegate = delegate(object value, IPropertyBag propertyBag)
			{
				propertyBag[rawPropertyDefinition] = ((ProxyAddress)value).ToString();
			};
			GetterDelegate getterDelegate2 = delegate(IPropertyBag propertyBag)
			{
				MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[rawPropertyDefinition];
				MultiValuedProperty<ProxyAddress> multiValuedProperty2 = new MultiValuedProperty<ProxyAddress>();
				foreach (string proxyAddressString in multiValuedProperty)
				{
					multiValuedProperty2.Add(ProxyAddress.Parse(proxyAddressString));
				}
				return multiValuedProperty2;
			};
			SetterDelegate setterDelegate2 = delegate(object value, IPropertyBag propertyBag)
			{
				MultiValuedProperty<ProxyAddress> multiValuedProperty = (MultiValuedProperty<ProxyAddress>)value;
				MultiValuedProperty<string> multiValuedProperty2 = (MultiValuedProperty<string>)propertyBag[rawPropertyDefinition];
				for (int i = multiValuedProperty2.Count - 1; i >= 0; i--)
				{
					if (!string.IsNullOrEmpty(multiValuedProperty2[i]))
					{
						multiValuedProperty2.RemoveAt(i);
					}
				}
				foreach (ProxyAddress proxyAddress in multiValuedProperty)
				{
					multiValuedProperty2.Add(proxyAddress.ToString());
				}
			};
			return new MbxPropertyDefinition(name, PropTag.Null, ExchangeObjectVersion.Exchange2003, typeof(ProxyAddress), (multivalued ? PropertyDefinitionFlags.MultiValued : PropertyDefinitionFlags.None) | PropertyDefinitionFlags.Calculated, multivalued ? null : null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new MbxPropertyDefinition[]
			{
				rawPropertyDefinition
			}, multivalued ? getterDelegate2 : getterDelegate, multivalued ? setterDelegate2 : setterDelegate);
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x00042EF0 File Offset: 0x000410F0
		internal static MbxPropertyDefinition SmtpAddressFromStringPropertyDefinition(string name, MbxPropertyDefinition rawPropertyDefinition, bool multivalued = false)
		{
			return new MbxPropertyDefinition(name, PropTag.Null, ExchangeObjectVersion.Exchange2003, typeof(SmtpAddress), (multivalued ? PropertyDefinitionFlags.MultiValued : PropertyDefinitionFlags.None) | PropertyDefinitionFlags.Calculated, multivalued ? null : default(SmtpAddress), PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new MbxPropertyDefinition[]
			{
				rawPropertyDefinition
			}, (IPropertyBag propertyBag) => (propertyBag[rawPropertyDefinition] == null) ? default(SmtpAddress) : SmtpAddress.Parse((string)propertyBag[rawPropertyDefinition]), delegate(object value, IPropertyBag propertyBag)
			{
				propertyBag[rawPropertyDefinition] = ((SmtpAddress)value).ToString();
			});
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x00042FBC File Offset: 0x000411BC
		internal static MbxPropertyDefinition CultureInfoFromStringPropertyDefinition(string name, MbxPropertyDefinition rawPropertyDefinition, bool multivalued = false)
		{
			return new MbxPropertyDefinition(name, PropTag.Null, ExchangeObjectVersion.Exchange2003, typeof(CultureInfo), (multivalued ? PropertyDefinitionFlags.MultiValued : PropertyDefinitionFlags.None) | PropertyDefinitionFlags.Calculated, multivalued ? null : null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new MbxPropertyDefinition[]
			{
				rawPropertyDefinition
			}, delegate(IPropertyBag propertyBag)
			{
				if (propertyBag[rawPropertyDefinition] != null)
				{
					return new CultureInfo((string)propertyBag[rawPropertyDefinition]);
				}
				return null;
			}, delegate(object value, IPropertyBag propertyBag)
			{
				propertyBag[rawPropertyDefinition] = ((CultureInfo)value).ToString();
			});
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x00043094 File Offset: 0x00041294
		internal static MbxPropertyDefinition ByteQuantifiedSizeFromStringPropertyDefinition(string name, MbxPropertyDefinition rawPropertyDefinition, bool multivalued = false)
		{
			return new MbxPropertyDefinition(name, PropTag.Null, ExchangeObjectVersion.Exchange2003, typeof(ByteQuantifiedSize?), (multivalued ? PropertyDefinitionFlags.MultiValued : PropertyDefinitionFlags.None) | PropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new MbxPropertyDefinition[]
			{
				rawPropertyDefinition
			}, delegate(IPropertyBag propertyBag)
			{
				if (propertyBag[rawPropertyDefinition] != null)
				{
					return ByteQuantifiedSize.Parse((string)propertyBag[rawPropertyDefinition]);
				}
				return null;
			}, delegate(object value, IPropertyBag propertyBag)
			{
				propertyBag[rawPropertyDefinition] = ((ByteQuantifiedSize)value).ToString();
			});
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x00043180 File Offset: 0x00041380
		internal static MbxPropertyDefinition UnlimitedByteQuantifiedSizeFromStringPropertyDefinition(string name, MbxPropertyDefinition rawPropertyDefinition, bool multivalued = false)
		{
			return new MbxPropertyDefinition(name, PropTag.Null, ExchangeObjectVersion.Exchange2003, typeof(Unlimited<ByteQuantifiedSize>), (multivalued ? PropertyDefinitionFlags.MultiValued : PropertyDefinitionFlags.None) | PropertyDefinitionFlags.Calculated, multivalued ? null : default(Unlimited<ByteQuantifiedSize>), PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new MbxPropertyDefinition[]
			{
				rawPropertyDefinition
			}, (IPropertyBag propertyBag) => (propertyBag[rawPropertyDefinition] == null) ? default(Unlimited<ByteQuantifiedSize>) : Unlimited<ByteQuantifiedSize>.Parse((string)propertyBag[rawPropertyDefinition]), delegate(object value, IPropertyBag propertyBag)
			{
				propertyBag[rawPropertyDefinition] = ((Unlimited<ByteQuantifiedSize>)value).ToString();
			});
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x0004327C File Offset: 0x0004147C
		internal static MbxPropertyDefinition UnlimitedInt32FromStringPropertyDefinition(string name, MbxPropertyDefinition rawPropertyDefinition, bool multivalued = false)
		{
			return new MbxPropertyDefinition(name, PropTag.Null, ExchangeObjectVersion.Exchange2003, typeof(Unlimited<int>), (multivalued ? PropertyDefinitionFlags.MultiValued : PropertyDefinitionFlags.None) | PropertyDefinitionFlags.Calculated, multivalued ? null : default(Unlimited<int>), PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new MbxPropertyDefinition[]
			{
				rawPropertyDefinition
			}, (IPropertyBag propertyBag) => (propertyBag[rawPropertyDefinition] == null) ? default(Unlimited<int>) : Unlimited<int>.Parse((string)propertyBag[rawPropertyDefinition]), delegate(object value, IPropertyBag propertyBag)
			{
				propertyBag[rawPropertyDefinition] = ((Unlimited<int>)value).ToString();
			});
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x00043348 File Offset: 0x00041548
		internal static MbxPropertyDefinition TextMessagingStateBaseFromStringPropertyDefinition(string name, MbxPropertyDefinition rawPropertyDefinition, bool multivalued = false)
		{
			return new MbxPropertyDefinition(name, PropTag.Null, ExchangeObjectVersion.Exchange2003, typeof(TextMessagingStateBase), (multivalued ? PropertyDefinitionFlags.MultiValued : PropertyDefinitionFlags.None) | PropertyDefinitionFlags.Calculated, multivalued ? null : null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new MbxPropertyDefinition[]
			{
				rawPropertyDefinition
			}, delegate(IPropertyBag propertyBag)
			{
				if (propertyBag[rawPropertyDefinition] != null)
				{
					return TextMessagingStateBase.ParseFromADString((string)propertyBag[rawPropertyDefinition]);
				}
				return null;
			}, delegate(object value, IPropertyBag propertyBag)
			{
				propertyBag[rawPropertyDefinition] = ((TextMessagingStateBase)value).ToADString();
			});
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x000433BC File Offset: 0x000415BC
		internal static MbxPropertyDefinition NullableInt32PropertyDefinition(PropTag propTag, string name = null, bool multivalued = false)
		{
			return new MbxPropertyDefinition(name ?? propTag.ToString(), propTag, ExchangeObjectVersion.Exchange2003, typeof(int?), multivalued ? PropertyDefinitionFlags.MultiValued : PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null);
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x00043570 File Offset: 0x00041770
		internal static MbxPropertyDefinition Int32FromNullableInt32PropertyDefinition(string name, MbxPropertyDefinition rawPropertyDefinition, bool multivalued = false)
		{
			GetterDelegate getterDelegate = (IPropertyBag propertyBag) => (propertyBag[rawPropertyDefinition] == null) ? 0 : ((int)propertyBag[rawPropertyDefinition]);
			SetterDelegate setterDelegate = delegate(object value, IPropertyBag propertyBag)
			{
				propertyBag[rawPropertyDefinition] = (((int?)value) ?? 0);
			};
			GetterDelegate getterDelegate2 = delegate(IPropertyBag propertyBag)
			{
				MultiValuedProperty<int> multiValuedProperty = (MultiValuedProperty<int>)propertyBag[rawPropertyDefinition];
				MultiValuedProperty<int> multiValuedProperty2 = new MultiValuedProperty<int>();
				foreach (int value in multiValuedProperty)
				{
					int? num = new int?(value);
					if (num != null)
					{
						multiValuedProperty2.Add(num.Value);
					}
				}
				return multiValuedProperty2;
			};
			SetterDelegate setterDelegate2 = delegate(object value, IPropertyBag propertyBag)
			{
				MultiValuedProperty<int> multiValuedProperty = (MultiValuedProperty<int>)value;
				MultiValuedProperty<int> multiValuedProperty2 = (MultiValuedProperty<int>)propertyBag[rawPropertyDefinition];
				for (int i = multiValuedProperty2.Count - 1; i >= 0; i--)
				{
					multiValuedProperty2.RemoveAt(i);
				}
				foreach (int item in multiValuedProperty)
				{
					multiValuedProperty2.Add(item);
				}
			};
			return new MbxPropertyDefinition(name, PropTag.Null, ExchangeObjectVersion.Exchange2003, typeof(int), (multivalued ? PropertyDefinitionFlags.MultiValued : PropertyDefinitionFlags.None) | PropertyDefinitionFlags.Calculated, multivalued ? null : 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new MbxPropertyDefinition[]
			{
				rawPropertyDefinition
			}, multivalued ? getterDelegate2 : getterDelegate, multivalued ? setterDelegate2 : setterDelegate);
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x0004367C File Offset: 0x0004187C
		internal static MbxPropertyDefinition EnumFromNullableInt32PropertyDefinition<T>(string name, MbxPropertyDefinition rawPropertyDefinition, bool multivalued = false) where T : struct
		{
			return new MbxPropertyDefinition(name, PropTag.Null, ExchangeObjectVersion.Exchange2003, typeof(T), (multivalued ? PropertyDefinitionFlags.MultiValued : PropertyDefinitionFlags.None) | PropertyDefinitionFlags.Calculated, multivalued ? null : default(T), PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new MbxPropertyDefinition[]
			{
				rawPropertyDefinition
			}, (IPropertyBag propertyBag) => (propertyBag[rawPropertyDefinition] == null) ? default(T) : ((T)((object)propertyBag[rawPropertyDefinition])), delegate(object value, IPropertyBag propertyBag)
			{
				propertyBag[rawPropertyDefinition] = (int)value;
			});
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x0004372C File Offset: 0x0004192C
		internal static MbxPropertyDefinition NullableEnumFromNullableInt32PropertyDefinition<T>(string name, MbxPropertyDefinition rawPropertyDefinition, bool multivalued = false) where T : struct
		{
			if (!typeof(T).IsEnum)
			{
				throw new ArgumentException("T must be an enum type");
			}
			return new MbxPropertyDefinition(name, PropTag.Null, ExchangeObjectVersion.Exchange2003, typeof(T?), (multivalued ? PropertyDefinitionFlags.MultiValued : PropertyDefinitionFlags.None) | PropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new MbxPropertyDefinition[]
			{
				rawPropertyDefinition
			}, (IPropertyBag propertyBag) => (T?)propertyBag[rawPropertyDefinition], delegate(object value, IPropertyBag propertyBag)
			{
				propertyBag[rawPropertyDefinition] = value;
			});
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x000437B8 File Offset: 0x000419B8
		internal static MbxPropertyDefinition BinaryPropertyDefinition(PropTag propTag, string name = null, bool multivalued = false)
		{
			return new MbxPropertyDefinition(name ?? propTag.ToString(), propTag, ExchangeObjectVersion.Exchange2003, typeof(byte[]), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null);
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x00043848 File Offset: 0x00041A48
		internal static MbxPropertyDefinition GeoCoordinatesFromStringPropertyDefinition(string name, MbxPropertyDefinition rawPropertyDefinition, bool multivalued = false)
		{
			return new MbxPropertyDefinition(name, PropTag.Null, ExchangeObjectVersion.Exchange2003, typeof(GeoCoordinates), (multivalued ? PropertyDefinitionFlags.MultiValued : PropertyDefinitionFlags.None) | PropertyDefinitionFlags.Calculated, multivalued ? null : null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new MbxPropertyDefinition[]
			{
				rawPropertyDefinition
			}, delegate(IPropertyBag propertyBag)
			{
				if (propertyBag[rawPropertyDefinition] != null)
				{
					return new GeoCoordinates((string)propertyBag[rawPropertyDefinition]);
				}
				return null;
			}, delegate(object value, IPropertyBag propertyBag)
			{
				propertyBag[rawPropertyDefinition] = ((GeoCoordinates)value).ToString();
			});
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x000438BC File Offset: 0x00041ABC
		internal static MbxPropertyDefinition NullableDateTimePropertyDefinition(PropTag propTag, string name = null, bool multivalued = false)
		{
			return new MbxPropertyDefinition(name ?? propTag.ToString(), propTag, ExchangeObjectVersion.Exchange2003, typeof(DateTime?), multivalued ? PropertyDefinitionFlags.MultiValued : PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null);
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x0004395C File Offset: 0x00041B5C
		internal static MbxPropertyDefinition DateTimeFromNullableDateTimePropertyDefinition(string name, MbxPropertyDefinition rawPropertyDefinition, bool multivalued = false)
		{
			return new MbxPropertyDefinition(name, PropTag.Null, ExchangeObjectVersion.Exchange2003, typeof(DateTime), (multivalued ? PropertyDefinitionFlags.MultiValued : PropertyDefinitionFlags.None) | PropertyDefinitionFlags.Calculated, multivalued ? null : default(DateTime), PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new MbxPropertyDefinition[]
			{
				rawPropertyDefinition
			}, (IPropertyBag propertyBag) => (propertyBag[rawPropertyDefinition] == null) ? default(DateTime) : ((DateTime)propertyBag[rawPropertyDefinition]), delegate(object value, IPropertyBag propertyBag)
			{
				propertyBag[rawPropertyDefinition] = value;
			});
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x000439DC File Offset: 0x00041BDC
		internal static MbxPropertyDefinition NullableInt64PropertyDefinition(PropTag propTag, string name = null, bool multivalued = false)
		{
			return new MbxPropertyDefinition(name ?? propTag.ToString(), propTag, ExchangeObjectVersion.Exchange2003, typeof(long?), multivalued ? PropertyDefinitionFlags.MultiValued : PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null);
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x00043AA4 File Offset: 0x00041CA4
		internal static MbxPropertyDefinition EnhancedTimeSpanFromNullableInt64PropertyDefinition(string name, MbxPropertyDefinition rawPropertyDefinition, bool multivalued = false)
		{
			return new MbxPropertyDefinition(name, PropTag.Null, ExchangeObjectVersion.Exchange2003, typeof(EnhancedTimeSpan), (multivalued ? PropertyDefinitionFlags.MultiValued : PropertyDefinitionFlags.None) | PropertyDefinitionFlags.Calculated, multivalued ? null : EnhancedTimeSpan.FromDays(90.0), PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new MbxPropertyDefinition[]
			{
				rawPropertyDefinition
			}, (IPropertyBag propertyBag) => (propertyBag[rawPropertyDefinition] == null) ? default(EnhancedTimeSpan) : new TimeSpan((long)propertyBag[rawPropertyDefinition]), delegate(object value, IPropertyBag propertyBag)
			{
				propertyBag[rawPropertyDefinition] = ((TimeSpan)value).Ticks;
			});
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x00043B2C File Offset: 0x00041D2C
		internal static MbxPropertyDefinition NullableGuidPropertyDefinition(PropTag propTag, string name = null, bool multivalued = false)
		{
			return new MbxPropertyDefinition(name ?? propTag.ToString(), propTag, ExchangeObjectVersion.Exchange2003, typeof(Guid?), multivalued ? PropertyDefinitionFlags.MultiValued : PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null);
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x00043BBC File Offset: 0x00041DBC
		internal static MbxPropertyDefinition GuidFromNullableGuidPropertyDefinition(string name, MbxPropertyDefinition rawPropertyDefinition, bool multivalued = false)
		{
			return new MbxPropertyDefinition(name, PropTag.Null, ExchangeObjectVersion.Exchange2003, typeof(Guid), (multivalued ? PropertyDefinitionFlags.MultiValued : PropertyDefinitionFlags.None) | PropertyDefinitionFlags.Calculated, multivalued ? null : Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new MbxPropertyDefinition[]
			{
				rawPropertyDefinition
			}, (IPropertyBag propertyBag) => (propertyBag[rawPropertyDefinition] == null) ? Guid.Empty : ((Guid)propertyBag[rawPropertyDefinition]), delegate(object value, IPropertyBag propertyBag)
			{
				propertyBag[rawPropertyDefinition] = value;
			});
		}
	}
}
