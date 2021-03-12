using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.DataConverter;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000774 RID: 1908
	[XmlType(TypeName = "PathToExtendedFieldType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class ExtendedPropertyUri : PropertyPath
	{
		// Token: 0x060038E4 RID: 14564 RVA: 0x000C92A4 File Offset: 0x000C74A4
		static ExtendedPropertyUri()
		{
			ExtendedPropertyUri.AddPropertySetEntry(ExtendedPropertyUri.PSETIDMeeting, DistinguishedPropertySet.Meeting, ExchangeVersion.Exchange2007);
			ExtendedPropertyUri.AddPropertySetEntry(ExtendedPropertyUri.PSETIDAppointment, DistinguishedPropertySet.Appointment, ExchangeVersion.Exchange2007);
			ExtendedPropertyUri.AddPropertySetEntry(ExtendedPropertyUri.PSETIDCommon, DistinguishedPropertySet.Common, ExchangeVersion.Exchange2007);
			ExtendedPropertyUri.AddPropertySetEntry(ExtendedPropertyUri.PSETIDPublicStrings, DistinguishedPropertySet.PublicStrings, ExchangeVersion.Exchange2007);
			ExtendedPropertyUri.AddPropertySetEntry(ExtendedPropertyUri.PSETIDAddress, DistinguishedPropertySet.Address, ExchangeVersion.Exchange2007);
			ExtendedPropertyUri.AddPropertySetEntry(ExtendedPropertyUri.PSETIDInternetHeaders, DistinguishedPropertySet.InternetHeaders, ExchangeVersion.Exchange2007);
			ExtendedPropertyUri.AddPropertySetEntry(ExtendedPropertyUri.PSETIDCalendarAssistant, DistinguishedPropertySet.CalendarAssistant, ExchangeVersion.Exchange2007);
			ExtendedPropertyUri.AddPropertySetEntry(ExtendedPropertyUri.PSETIDUnifiedMessaging, DistinguishedPropertySet.UnifiedMessaging, ExchangeVersion.Exchange2007);
			ExtendedPropertyUri.AddPropertySetEntry(ExtendedPropertyUri.PSETIDTask, DistinguishedPropertySet.Task, ExchangeVersion.Exchange2007SP1);
			ExtendedPropertyUri.AddPropertySetEntry(ExtendedPropertyUri.PSETIDSharing, DistinguishedPropertySet.Sharing, ExchangeVersion.Exchange2012);
			ExtendedPropertyUri.mapiTypeToTypeMap = new Dictionary<MapiPropertyType, Type>();
			ExtendedPropertyUri.AddMapiTypeEntry(MapiPropertyType.ApplicationTime, typeof(double));
			ExtendedPropertyUri.AddMapiTypeEntry(MapiPropertyType.ApplicationTimeArray, typeof(double[]));
			ExtendedPropertyUri.AddMapiTypeEntry(MapiPropertyType.Binary, typeof(byte[]));
			ExtendedPropertyUri.AddMapiTypeEntry(MapiPropertyType.BinaryArray, typeof(byte[][]));
			ExtendedPropertyUri.AddMapiTypeEntry(MapiPropertyType.Boolean, typeof(bool));
			ExtendedPropertyUri.AddMapiTypeEntry(MapiPropertyType.CLSID, typeof(Guid));
			ExtendedPropertyUri.AddMapiTypeEntry(MapiPropertyType.CLSIDArray, typeof(Guid[]));
			ExtendedPropertyUri.AddMapiTypeEntry(MapiPropertyType.Currency, typeof(long));
			ExtendedPropertyUri.AddMapiTypeEntry(MapiPropertyType.CurrencyArray, typeof(long[]));
			ExtendedPropertyUri.AddMapiTypeEntry(MapiPropertyType.Double, typeof(double));
			ExtendedPropertyUri.AddMapiTypeEntry(MapiPropertyType.DoubleArray, typeof(double[]));
			ExtendedPropertyUri.AddMapiTypeEntry(MapiPropertyType.Float, typeof(float));
			ExtendedPropertyUri.AddMapiTypeEntry(MapiPropertyType.FloatArray, typeof(float[]));
			ExtendedPropertyUri.AddMapiTypeEntry(MapiPropertyType.Integer, typeof(int));
			ExtendedPropertyUri.AddMapiTypeEntry(MapiPropertyType.IntegerArray, typeof(int[]));
			ExtendedPropertyUri.AddMapiTypeEntry(MapiPropertyType.Long, typeof(long));
			ExtendedPropertyUri.AddMapiTypeEntry(MapiPropertyType.LongArray, typeof(long[]));
			ExtendedPropertyUri.AddMapiTypeEntry(MapiPropertyType.Short, typeof(short));
			ExtendedPropertyUri.AddMapiTypeEntry(MapiPropertyType.ShortArray, typeof(short[]));
			ExtendedPropertyUri.AddMapiTypeEntry(MapiPropertyType.SystemTime, typeof(ExDateTime));
			ExtendedPropertyUri.AddMapiTypeEntry(MapiPropertyType.SystemTimeArray, typeof(ExDateTime[]));
			ExtendedPropertyUri.AddMapiTypeEntry(MapiPropertyType.String, typeof(string));
			ExtendedPropertyUri.AddMapiTypeEntry(MapiPropertyType.StringArray, typeof(string[]));
		}

		// Token: 0x060038E5 RID: 14565 RVA: 0x000C95B5 File Offset: 0x000C77B5
		private static void AddMapiTypeEntry(MapiPropertyType mapiType, Type dotNetType)
		{
			ExtendedPropertyUri.mapiTypeToTypeMap.Add(mapiType, dotNetType);
		}

		// Token: 0x060038E6 RID: 14566 RVA: 0x000C95C3 File Offset: 0x000C77C3
		private static void AddPropertySetEntry(Guid guid, DistinguishedPropertySet propertySet, ExchangeVersion minimumSupportedVersion)
		{
			ExtendedPropertyUri.guidToDistinguishedMap.Add(guid, new ExtendedPropertyUri.DistinguishedPropertySetInformation(propertySet, minimumSupportedVersion));
			ExtendedPropertyUri.distinguishedToGuidMap.Add(propertySet, guid);
		}

		// Token: 0x060038E7 RID: 14567 RVA: 0x000C95E3 File Offset: 0x000C77E3
		public ExtendedPropertyUri()
		{
		}

		// Token: 0x060038E8 RID: 14568 RVA: 0x000C95EC File Offset: 0x000C77EC
		internal ExtendedPropertyUri(NativeStorePropertyDefinition propertyDefinition)
		{
			this.ResetSpecified();
			GuidIdPropertyDefinition guidIdPropertyDefinition = propertyDefinition as GuidIdPropertyDefinition;
			if (guidIdPropertyDefinition != null)
			{
				this.Initialize(guidIdPropertyDefinition);
			}
			GuidNamePropertyDefinition guidNamePropertyDefinition = propertyDefinition as GuidNamePropertyDefinition;
			if (guidNamePropertyDefinition != null)
			{
				this.Initialize(guidNamePropertyDefinition);
			}
			PropertyTagPropertyDefinition propertyTagPropertyDefinition = propertyDefinition as PropertyTagPropertyDefinition;
			if (propertyTagPropertyDefinition != null)
			{
				this.Initialize(propertyTagPropertyDefinition);
			}
			this.ConfigureCommonProperties(propertyDefinition);
			this.Classify();
		}

		// Token: 0x060038E9 RID: 14569 RVA: 0x000C9646 File Offset: 0x000C7846
		private void Initialize(PropertyTagPropertyDefinition propertyDefinition)
		{
			this.BreakApartPropertyTag(propertyDefinition);
		}

		// Token: 0x060038EA RID: 14570 RVA: 0x000C964F File Offset: 0x000C784F
		private void Initialize(GuidNamePropertyDefinition propertyDefinition)
		{
			this.ExtractPropertySet(propertyDefinition.Guid);
			this.PropertyName = propertyDefinition.PropertyName;
		}

		// Token: 0x060038EB RID: 14571 RVA: 0x000C9669 File Offset: 0x000C7869
		private void Initialize(GuidIdPropertyDefinition propertyDefinition)
		{
			this.ExtractPropertySet(propertyDefinition.Guid);
			this.PropertyId = propertyDefinition.Id;
		}

		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x060038EC RID: 14572 RVA: 0x000C9683 File Offset: 0x000C7883
		// (set) Token: 0x060038ED RID: 14573 RVA: 0x000C968B File Offset: 0x000C788B
		[IgnoreDataMember]
		[XmlAttribute(AttributeName = "DistinguishedPropertySetId")]
		public DistinguishedPropertySet DistinguishedPropertySetId
		{
			get
			{
				return this.distinguishedPropertySet;
			}
			set
			{
				this.distinguishedPropertySetSpecified = true;
				this.distinguishedPropertySet = value;
			}
		}

		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x060038EE RID: 14574 RVA: 0x000C969B File Offset: 0x000C789B
		// (set) Token: 0x060038EF RID: 14575 RVA: 0x000C96B2 File Offset: 0x000C78B2
		[XmlIgnore]
		[DataMember(Name = "DistinguishedPropertySetId", IsRequired = false, EmitDefaultValue = false)]
		public string DistinguishedPropertySetIdString
		{
			get
			{
				if (this.DistinguishedPropertySetIdSpecified)
				{
					return EnumUtilities.ToString<DistinguishedPropertySet>(this.DistinguishedPropertySetId);
				}
				return null;
			}
			set
			{
				this.DistinguishedPropertySetId = EnumUtilities.Parse<DistinguishedPropertySet>(value);
			}
		}

		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x060038F0 RID: 14576 RVA: 0x000C96C0 File Offset: 0x000C78C0
		// (set) Token: 0x060038F1 RID: 14577 RVA: 0x000C96E6 File Offset: 0x000C78E6
		[DataMember(Name = "PropertySetId", IsRequired = false, EmitDefaultValue = false)]
		[XmlAttribute("PropertySetId")]
		public string PropertySetId
		{
			get
			{
				if (!(this.propertySetId == Guid.Empty))
				{
					return this.propertySetId.ToString("D");
				}
				return null;
			}
			set
			{
				this.propertySetId = ((value == null) ? Guid.Empty : new Guid(value));
			}
		}

		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x060038F2 RID: 14578 RVA: 0x000C96FE File Offset: 0x000C78FE
		// (set) Token: 0x060038F3 RID: 14579 RVA: 0x000C972C File Offset: 0x000C792C
		[DataMember(Name = "PropertyTag", IsRequired = false, EmitDefaultValue = false)]
		[XmlAttribute("PropertyTag")]
		public string PropertyTag
		{
			get
			{
				if (!this.propertyTagSpecified)
				{
					return null;
				}
				return "0x" + this.propertyTagId.ToString("x", CultureInfo.InvariantCulture);
			}
			set
			{
				NumberStyles style = NumberStyles.Integer;
				string s;
				if (value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
				{
					s = value.Replace("0x", string.Empty).Replace("0X", string.Empty);
					style = NumberStyles.HexNumber;
				}
				else
				{
					s = value;
				}
				this.propertyTagId = ushort.Parse(s, style, CultureInfo.InvariantCulture);
				this.propertyTagSpecified = true;
			}
		}

		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x060038F4 RID: 14580 RVA: 0x000C978C File Offset: 0x000C798C
		// (set) Token: 0x060038F5 RID: 14581 RVA: 0x000C9794 File Offset: 0x000C7994
		[DataMember(Name = "PropertyName", IsRequired = false, EmitDefaultValue = false)]
		[XmlAttribute("PropertyName")]
		public string PropertyName { get; set; }

		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x060038F6 RID: 14582 RVA: 0x000C979D File Offset: 0x000C799D
		// (set) Token: 0x060038F7 RID: 14583 RVA: 0x000C97A5 File Offset: 0x000C79A5
		[DataMember(Name = "PropertyId", IsRequired = false, EmitDefaultValue = false)]
		[XmlAttribute("PropertyId")]
		public int PropertyId
		{
			get
			{
				return this.propertyId;
			}
			set
			{
				this.propertyIdSpecified = true;
				this.propertyId = value;
			}
		}

		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x060038F8 RID: 14584 RVA: 0x000C97B5 File Offset: 0x000C79B5
		// (set) Token: 0x060038F9 RID: 14585 RVA: 0x000C97BD File Offset: 0x000C79BD
		[XmlAttribute("PropertyType")]
		[IgnoreDataMember]
		public MapiPropertyType PropertyType { get; set; }

		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x060038FA RID: 14586 RVA: 0x000C97C6 File Offset: 0x000C79C6
		// (set) Token: 0x060038FB RID: 14587 RVA: 0x000C97D3 File Offset: 0x000C79D3
		[XmlIgnore]
		[DataMember(Name = "PropertyType")]
		public string PropertyTypeString
		{
			get
			{
				return EnumUtilities.ToString<MapiPropertyType>(this.PropertyType);
			}
			set
			{
				this.PropertyType = EnumUtilities.Parse<MapiPropertyType>(value);
			}
		}

		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x060038FC RID: 14588 RVA: 0x000C97E1 File Offset: 0x000C79E1
		internal Guid PropertySetIdGuid
		{
			get
			{
				return this.propertySetId;
			}
		}

		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x060038FD RID: 14589 RVA: 0x000C97E9 File Offset: 0x000C79E9
		internal ushort PropertyTagId
		{
			get
			{
				return this.propertyTagId;
			}
		}

		// Token: 0x060038FE RID: 14590 RVA: 0x000C97F1 File Offset: 0x000C79F1
		internal static bool IsRequestableType(MapiPropertyType propertyType)
		{
			return propertyType != MapiPropertyType.Error && propertyType != MapiPropertyType.Null && propertyType != MapiPropertyType.Object && propertyType != MapiPropertyType.ObjectArray;
		}

		// Token: 0x060038FF RID: 14591 RVA: 0x000C980E File Offset: 0x000C7A0E
		private Guid GetEffectivePropertySet()
		{
			if (this.propertySetId != Guid.Empty)
			{
				return this.propertySetId;
			}
			return ExtendedPropertyUri.GuidForDistinguishedPropertySet(this.distinguishedPropertySet);
		}

		// Token: 0x06003900 RID: 14592 RVA: 0x000C9834 File Offset: 0x000C7A34
		internal NativeStorePropertyDefinition ToPropertyDefinition()
		{
			if (!ExtendedPropertyUri.IsRequestableType(this.PropertyType))
			{
				throw new UnsupportedMapiPropertyTypeException();
			}
			ExtendedPropertyClassification extendedPropertyClassification = this.Classify();
			NativeStorePropertyDefinition result;
			try
			{
				switch (extendedPropertyClassification)
				{
				case ExtendedPropertyClassification.PropertyTag:
				{
					uint num = this.ConstructPropertyTag();
					if (num >= 2147483648U)
					{
						throw new NoPropertyTagForCustomPropertyException();
					}
					result = PropertyTagPropertyDefinition.CreateCustom("PropTag_" + num.ToString("X", CultureInfo.InvariantCulture), num);
					break;
				}
				case ExtendedPropertyClassification.GuidId:
					result = GuidIdPropertyDefinition.CreateCustom("GuidId_" + this.PropertyId.ToString(CultureInfo.InvariantCulture), this.GetTypeForMapiPropertyType(), this.GetEffectivePropertySet(), this.propertyId, PropertyFlags.None);
					break;
				case ExtendedPropertyClassification.GuidName:
					result = GuidNamePropertyDefinition.CreateCustom("GuidName_" + this.PropertyName, this.GetTypeForMapiPropertyType(), this.GetEffectivePropertySet(), this.PropertyName, PropertyFlags.None);
					break;
				default:
					result = null;
					break;
				}
			}
			catch (InvalidPropertyTypeException innerException)
			{
				throw new InvalidExtendedPropertyException(this, innerException);
			}
			catch (ArgumentException innerException2)
			{
				throw new InvalidExtendedPropertyException(this, innerException2);
			}
			return result;
		}

		// Token: 0x06003901 RID: 14593 RVA: 0x000C994C File Offset: 0x000C7B4C
		public static bool AreEqual(ExtendedPropertyUri first, ExtendedPropertyUri second)
		{
			if (object.ReferenceEquals(first, second))
			{
				return true;
			}
			if (first == null || second == null)
			{
				return false;
			}
			ExtendedPropertyClassification extendedPropertyClassification;
			ExtendedPropertyClassification extendedPropertyClassification2;
			if (!first.TryClassify(out extendedPropertyClassification) || !second.TryClassify(out extendedPropertyClassification2))
			{
				return false;
			}
			if (extendedPropertyClassification != extendedPropertyClassification2)
			{
				return false;
			}
			switch (extendedPropertyClassification)
			{
			case ExtendedPropertyClassification.PropertyTag:
				return first.PropertyTagId == second.PropertyTagId;
			case ExtendedPropertyClassification.GuidId:
				return first.PropertyId == second.PropertyId && first.GetEffectivePropertySet().Equals(second.GetEffectivePropertySet());
			case ExtendedPropertyClassification.GuidName:
				return first.PropertyName.Equals(second.PropertyName) && first.GetEffectivePropertySet().Equals(second.GetEffectivePropertySet());
			default:
				return false;
			}
		}

		// Token: 0x06003902 RID: 14594 RVA: 0x000C9A00 File Offset: 0x000C7C00
		private void BreakApartPropertyTag(PropertyTagPropertyDefinition propertyDefinition)
		{
			if (propertyDefinition.PropertyTag >= 2147483648U)
			{
				throw new UnsupportedPropertyDefinitionException(propertyDefinition.Name);
			}
			ushort num = (ushort)((propertyDefinition.PropertyTag & 4294901760U) >> 16);
			ushort num2 = (ushort)(propertyDefinition.PropertyTag & 65535U);
			if (!EnumValidator.IsValidEnum<MapiPropertyType>((MapiPropertyType)num2))
			{
				throw new UnsupportedMapiPropertyTypeException();
			}
			this.PropertyType = (MapiPropertyType)num2;
			this.propertyTagId = num;
			this.propertyTagSpecified = true;
		}

		// Token: 0x06003903 RID: 14595 RVA: 0x000C9A68 File Offset: 0x000C7C68
		private uint ConstructPropertyTag()
		{
			return (uint)(((int)this.propertyTagId << 16) + this.PropertyType);
		}

		// Token: 0x06003904 RID: 14596 RVA: 0x000C9A7A File Offset: 0x000C7C7A
		private static Guid GuidForDistinguishedPropertySet(DistinguishedPropertySet distinguishedPropertySet)
		{
			return ExtendedPropertyUri.distinguishedToGuidMap[distinguishedPropertySet];
		}

		// Token: 0x06003905 RID: 14597 RVA: 0x000C9A88 File Offset: 0x000C7C88
		private static bool TryGetDistinguishedPropertySetForGuid(Guid guid, out DistinguishedPropertySet distinguishedPropertySetToReturn)
		{
			distinguishedPropertySetToReturn = DistinguishedPropertySet.Meeting;
			ExtendedPropertyUri.DistinguishedPropertySetInformation distinguishedPropertySetInformation;
			if (!ExtendedPropertyUri.guidToDistinguishedMap.TryGetValue(guid, out distinguishedPropertySetInformation))
			{
				return false;
			}
			if (!ExchangeVersion.Current.Supports(distinguishedPropertySetInformation.MinimumSupportedVersion))
			{
				return false;
			}
			distinguishedPropertySetToReturn = distinguishedPropertySetInformation.DistinguishedPropertySet;
			return true;
		}

		// Token: 0x06003906 RID: 14598 RVA: 0x000C9AC6 File Offset: 0x000C7CC6
		private Type GetTypeForMapiPropertyType()
		{
			return ExtendedPropertyUri.mapiTypeToTypeMap[this.PropertyType];
		}

		// Token: 0x06003907 RID: 14599 RVA: 0x000C9AD8 File Offset: 0x000C7CD8
		private ExtendedPropertyClassification Classify()
		{
			ExtendedPropertyClassification result;
			if (!this.TryClassify(out result))
			{
				throw new InvalidExtendedPropertyException(this);
			}
			return result;
		}

		// Token: 0x06003908 RID: 14600 RVA: 0x000C9AF8 File Offset: 0x000C7CF8
		private bool TryClassify(out ExtendedPropertyClassification classification)
		{
			classification = ExtendedPropertyClassification.PropertyTag;
			if (this.distinguishedPropertySetSpecified || this.propertySetId != Guid.Empty)
			{
				if (this.distinguishedPropertySetSpecified && this.propertySetId != Guid.Empty)
				{
					return false;
				}
				if (this.propertyTagSpecified)
				{
					return false;
				}
				if (!string.IsNullOrEmpty(this.PropertyName) && this.PropertyIdSpecified)
				{
					return false;
				}
				if (string.IsNullOrEmpty(this.PropertyName) && !this.PropertyIdSpecified)
				{
					return false;
				}
				classification = (this.PropertyIdSpecified ? ExtendedPropertyClassification.GuidId : ExtendedPropertyClassification.GuidName);
			}
			else
			{
				if (!this.propertyTagSpecified)
				{
					return false;
				}
				if (!string.IsNullOrEmpty(this.PropertyName) || this.PropertyIdSpecified)
				{
					return false;
				}
				classification = ExtendedPropertyClassification.PropertyTag;
			}
			return true;
		}

		// Token: 0x06003909 RID: 14601 RVA: 0x000C9BB0 File Offset: 0x000C7DB0
		private void ConfigureCommonProperties(NativeStorePropertyDefinition propertyDefinition)
		{
			MapiPropertyType mapiPropertyType = (MapiPropertyType)propertyDefinition.MapiPropertyType;
			if (!EnumValidator.IsValidEnum<MapiPropertyType>(mapiPropertyType))
			{
				throw new UnsupportedMapiPropertyTypeException();
			}
			this.PropertyType = mapiPropertyType;
		}

		// Token: 0x0600390A RID: 14602 RVA: 0x000C9BDC File Offset: 0x000C7DDC
		private void ExtractPropertySet(Guid guid)
		{
			DistinguishedPropertySet distinguishedPropertySetId;
			if (ExtendedPropertyUri.TryGetDistinguishedPropertySetForGuid(guid, out distinguishedPropertySetId))
			{
				this.DistinguishedPropertySetId = distinguishedPropertySetId;
				return;
			}
			this.propertySetId = guid;
		}

		// Token: 0x0600390B RID: 14603 RVA: 0x000C9C02 File Offset: 0x000C7E02
		private void ResetSpecified()
		{
			this.distinguishedPropertySetSpecified = false;
			this.propertyTagSpecified = false;
			this.propertyIdSpecified = false;
		}

		// Token: 0x0600390C RID: 14604 RVA: 0x000C9C1C File Offset: 0x000C7E1C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.distinguishedPropertySetSpecified)
			{
				stringBuilder.AppendFormat("DistinguishedPropertySetId = {0}, ", this.distinguishedPropertySet);
			}
			if (this.propertySetId != Guid.Empty)
			{
				stringBuilder.AppendFormat("PropertySetId = {0}, ", this.PropertySetId);
			}
			if (this.propertyIdSpecified)
			{
				stringBuilder.AppendFormat("PropertyId = {0}, ", this.propertyId);
			}
			if (!string.IsNullOrEmpty(this.PropertyName))
			{
				stringBuilder.AppendFormat("PropertyName = {0}, ", this.PropertyName);
			}
			if (this.propertyTagSpecified)
			{
				stringBuilder.AppendFormat("PropertyTag = {0}, ", this.PropertyTag);
			}
			if (stringBuilder.Length == 0)
			{
				stringBuilder.Append("[Missing all optional attributes], ");
			}
			stringBuilder.Append("PropertyType = " + this.PropertyType);
			return stringBuilder.ToString();
		}

		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x0600390D RID: 14605 RVA: 0x000C9D00 File Offset: 0x000C7F00
		// (set) Token: 0x0600390E RID: 14606 RVA: 0x000C9D08 File Offset: 0x000C7F08
		[XmlIgnore]
		public bool DistinguishedPropertySetIdSpecified
		{
			get
			{
				return this.distinguishedPropertySetSpecified;
			}
			set
			{
				this.distinguishedPropertySetSpecified = value;
			}
		}

		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x0600390F RID: 14607 RVA: 0x000C9D11 File Offset: 0x000C7F11
		// (set) Token: 0x06003910 RID: 14608 RVA: 0x000C9D19 File Offset: 0x000C7F19
		[XmlIgnore]
		public bool PropertyIdSpecified
		{
			get
			{
				return this.propertyIdSpecified;
			}
			set
			{
				this.propertyIdSpecified = value;
			}
		}

		// Token: 0x06003911 RID: 14609 RVA: 0x000C9D22 File Offset: 0x000C7F22
		internal static bool IsExtendedPropertyUriXml(XmlElement extendedPropertyXml)
		{
			return extendedPropertyXml.LocalName == "ExtendedFieldURI";
		}

		// Token: 0x06003912 RID: 14610 RVA: 0x000C9D34 File Offset: 0x000C7F34
		internal new static ExtendedPropertyUri Parse(XmlElement extendedPropertyXml)
		{
			ExtendedPropertyUri extendedPropertyUri = new ExtendedPropertyUri();
			try
			{
				foreach (object obj in extendedPropertyXml.Attributes)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)obj;
					if (xmlAttribute.LocalName == "DistinguishedPropertySetId")
					{
						extendedPropertyUri.DistinguishedPropertySetId = (DistinguishedPropertySet)Enum.Parse(typeof(DistinguishedPropertySet), xmlAttribute.Value);
					}
					else if (xmlAttribute.LocalName == "PropertyId")
					{
						extendedPropertyUri.PropertyId = int.Parse(xmlAttribute.Value, CultureInfo.InvariantCulture);
					}
					else if (xmlAttribute.LocalName == "PropertyName")
					{
						extendedPropertyUri.PropertyName = xmlAttribute.Value;
					}
					else if (xmlAttribute.LocalName == "PropertySetId")
					{
						extendedPropertyUri.PropertySetId = xmlAttribute.Value;
					}
					else if (xmlAttribute.LocalName == "PropertyTag")
					{
						extendedPropertyUri.PropertyTag = xmlAttribute.Value;
					}
					else if (xmlAttribute.LocalName == "PropertyType")
					{
						extendedPropertyUri.PropertyType = (MapiPropertyType)Enum.Parse(typeof(MapiPropertyType), xmlAttribute.Value);
					}
				}
			}
			catch (ArgumentException innerException)
			{
				throw new InvalidExtendedPropertyException(extendedPropertyUri, innerException);
			}
			extendedPropertyUri.Classify();
			return extendedPropertyUri;
		}

		// Token: 0x06003913 RID: 14611 RVA: 0x000C9EC4 File Offset: 0x000C80C4
		internal override XmlElement ToXml(XmlElement parentElement)
		{
			XmlElement xmlElement = ServiceXml.CreateElement(parentElement, "ExtendedFieldURI", "http://schemas.microsoft.com/exchange/services/2006/types");
			ExtendedPropertyClassification extendedPropertyClassification = this.Classify();
			switch (extendedPropertyClassification)
			{
			case ExtendedPropertyClassification.PropertyTag:
				ServiceXml.CreateAttribute(xmlElement, "PropertyTag", this.PropertyTag);
				break;
			case ExtendedPropertyClassification.GuidId:
			case ExtendedPropertyClassification.GuidName:
			{
				DistinguishedPropertySet distinguishedPropertySet;
				if (ExtendedPropertyUri.TryGetDistinguishedPropertySetForGuid(this.GetEffectivePropertySet(), out distinguishedPropertySet))
				{
					ServiceXml.CreateAttribute(xmlElement, "DistinguishedPropertySetId", distinguishedPropertySet.ToString());
				}
				else
				{
					ServiceXml.CreateAttribute(xmlElement, "PropertySetId", this.propertySetId.ToString());
				}
				if (extendedPropertyClassification == ExtendedPropertyClassification.GuidId)
				{
					ServiceXml.CreateAttribute(xmlElement, "PropertyId", this.propertyId.ToString(CultureInfo.InvariantCulture));
				}
				else
				{
					ServiceXml.CreateAttribute(xmlElement, "PropertyName", this.PropertyName);
				}
				break;
			}
			}
			ServiceXml.CreateAttribute(xmlElement, "PropertyType", this.PropertyType.ToString());
			return xmlElement;
		}

		// Token: 0x04001FC3 RID: 8131
		internal static readonly Guid PSETIDMeeting = new Guid("{6ED8DA90-450B-101B-98DA-00AA003F1305}");

		// Token: 0x04001FC4 RID: 8132
		internal static readonly Guid PSETIDAppointment = new Guid("{00062002-0000-0000-C000-000000000046}");

		// Token: 0x04001FC5 RID: 8133
		internal static readonly Guid PSETIDCommon = new Guid("{00062008-0000-0000-C000-000000000046}");

		// Token: 0x04001FC6 RID: 8134
		internal static readonly Guid PSETIDPublicStrings = new Guid("{00020329-0000-0000-C000-000000000046}");

		// Token: 0x04001FC7 RID: 8135
		internal static readonly Guid PSETIDAddress = new Guid("{00062004-0000-0000-C000-000000000046}");

		// Token: 0x04001FC8 RID: 8136
		internal static readonly Guid PSETIDInternetHeaders = new Guid("{00020386-0000-0000-C000-000000000046}");

		// Token: 0x04001FC9 RID: 8137
		internal static readonly Guid PSETIDCalendarAssistant = new Guid("{11000E07-B51B-40D6-AF21-CAA85EDAB1D0}");

		// Token: 0x04001FCA RID: 8138
		internal static readonly Guid PSETIDUnifiedMessaging = new Guid("{4442858E-A9E3-4E80-B900-317A210CC15B}");

		// Token: 0x04001FCB RID: 8139
		internal static readonly Guid PSETIDTask = new Guid("{00062003-0000-0000-C000-000000000046}");

		// Token: 0x04001FCC RID: 8140
		internal static readonly Guid PSETIDSharing = new Guid("{00062040-0000-0000-C000-000000000046}");

		// Token: 0x04001FCD RID: 8141
		internal static ExtendedPropertyUri Placeholder = new ExtendedPropertyUri();

		// Token: 0x04001FCE RID: 8142
		private static Dictionary<Guid, ExtendedPropertyUri.DistinguishedPropertySetInformation> guidToDistinguishedMap = new Dictionary<Guid, ExtendedPropertyUri.DistinguishedPropertySetInformation>();

		// Token: 0x04001FCF RID: 8143
		private static Dictionary<DistinguishedPropertySet, Guid> distinguishedToGuidMap = new Dictionary<DistinguishedPropertySet, Guid>();

		// Token: 0x04001FD0 RID: 8144
		private static Dictionary<MapiPropertyType, Type> mapiTypeToTypeMap;

		// Token: 0x04001FD1 RID: 8145
		private DistinguishedPropertySet distinguishedPropertySet;

		// Token: 0x04001FD2 RID: 8146
		private bool distinguishedPropertySetSpecified;

		// Token: 0x04001FD3 RID: 8147
		private Guid propertySetId;

		// Token: 0x04001FD4 RID: 8148
		private ushort propertyTagId;

		// Token: 0x04001FD5 RID: 8149
		private bool propertyTagSpecified;

		// Token: 0x04001FD6 RID: 8150
		private int propertyId;

		// Token: 0x04001FD7 RID: 8151
		private bool propertyIdSpecified;

		// Token: 0x02000775 RID: 1909
		private class DistinguishedPropertySetInformation
		{
			// Token: 0x06003914 RID: 14612 RVA: 0x000C9FAC File Offset: 0x000C81AC
			internal DistinguishedPropertySetInformation(DistinguishedPropertySet distinguishedPropertySet, ExchangeVersion minimumSupportedVersion)
			{
				this.distinguishedPropertySet = distinguishedPropertySet;
				this.minimumSupportedVersion = minimumSupportedVersion;
			}

			// Token: 0x17000D75 RID: 3445
			// (get) Token: 0x06003915 RID: 14613 RVA: 0x000C9FC2 File Offset: 0x000C81C2
			internal DistinguishedPropertySet DistinguishedPropertySet
			{
				get
				{
					return this.distinguishedPropertySet;
				}
			}

			// Token: 0x17000D76 RID: 3446
			// (get) Token: 0x06003916 RID: 14614 RVA: 0x000C9FCA File Offset: 0x000C81CA
			internal ExchangeVersion MinimumSupportedVersion
			{
				get
				{
					return this.minimumSupportedVersion;
				}
			}

			// Token: 0x04001FDA RID: 8154
			private DistinguishedPropertySet distinguishedPropertySet;

			// Token: 0x04001FDB RID: 8155
			private ExchangeVersion minimumSupportedVersion;
		}
	}
}
