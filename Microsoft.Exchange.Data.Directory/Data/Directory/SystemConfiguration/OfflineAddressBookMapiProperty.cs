using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200051B RID: 1307
	[Serializable]
	public sealed class OfflineAddressBookMapiProperty : IEquatable<OfflineAddressBookMapiProperty>
	{
		// Token: 0x060039B9 RID: 14777 RVA: 0x000DEC30 File Offset: 0x000DCE30
		private static int MapiIdFromPropTag(uint proptag)
		{
			return (int)((proptag & 4294901760U) >> 16);
		}

		// Token: 0x060039BA RID: 14778 RVA: 0x000DEC3C File Offset: 0x000DCE3C
		public OfflineAddressBookMapiProperty()
		{
			this.mapiPropTag = 0U;
			this.mapiPropName = string.Empty;
			this.option = OfflineAddressBookMapiPropertyOption.Value;
		}

		// Token: 0x060039BB RID: 14779 RVA: 0x000DEC6F File Offset: 0x000DCE6F
		public OfflineAddressBookMapiProperty(uint mapiPropTag, string mapiPropName, OfflineAddressBookMapiPropertyOption option)
		{
			this.mapiPropTag = mapiPropTag;
			this.mapiPropName = mapiPropName;
			this.option = option;
		}

		// Token: 0x060039BC RID: 14780 RVA: 0x000DECA0 File Offset: 0x000DCEA0
		public OfflineAddressBookMapiProperty(PSObject psObject)
		{
			string arg = psObject.Properties["Name"].Value.ToString();
			string arg2 = psObject.Properties["Type"].Value.ToString();
			OfflineAddressBookMapiProperty offlineAddressBookMapiProperty = OfflineAddressBookMapiProperty.Parse(string.Format("{0}, {1}", arg, arg2));
			this.mapiPropTag = offlineAddressBookMapiProperty.mapiPropTag;
			this.mapiPropName = offlineAddressBookMapiProperty.mapiPropName;
			this.option = offlineAddressBookMapiProperty.Type;
		}

		// Token: 0x17001213 RID: 4627
		// (get) Token: 0x060039BD RID: 14781 RVA: 0x000DED31 File Offset: 0x000DCF31
		public string Name
		{
			get
			{
				return this.mapiPropName;
			}
		}

		// Token: 0x17001214 RID: 4628
		// (get) Token: 0x060039BE RID: 14782 RVA: 0x000DED39 File Offset: 0x000DCF39
		public OfflineAddressBookMapiPropertyOption Type
		{
			get
			{
				return this.option;
			}
		}

		// Token: 0x17001215 RID: 4629
		// (get) Token: 0x060039BF RID: 14783 RVA: 0x000DED41 File Offset: 0x000DCF41
		internal uint PropertyTag
		{
			get
			{
				return this.mapiPropTag;
			}
		}

		// Token: 0x17001216 RID: 4630
		// (get) Token: 0x060039C0 RID: 14784 RVA: 0x000DED49 File Offset: 0x000DCF49
		internal int MapiID
		{
			get
			{
				return OfflineAddressBookMapiProperty.MapiIdFromPropTag(this.mapiPropTag);
			}
		}

		// Token: 0x060039C1 RID: 14785 RVA: 0x000DED58 File Offset: 0x000DCF58
		public bool Equals(OfflineAddressBookMapiProperty oabmp)
		{
			if (oabmp == null)
			{
				return false;
			}
			if (this.Type != oabmp.Type)
			{
				return false;
			}
			if (this.PropertyTag != 0U && oabmp.PropertyTag != 0U)
			{
				return this.PropertyTag == oabmp.PropertyTag;
			}
			return string.Compare(this.mapiPropName, oabmp.mapiPropName, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060039C2 RID: 14786 RVA: 0x000DEDAE File Offset: 0x000DCFAE
		public override bool Equals(object o)
		{
			return this.Equals(o as OfflineAddressBookMapiProperty);
		}

		// Token: 0x060039C3 RID: 14787 RVA: 0x000DEDBC File Offset: 0x000DCFBC
		public override int GetHashCode()
		{
			return this.MapiID;
		}

		// Token: 0x060039C4 RID: 14788 RVA: 0x000DEDC4 File Offset: 0x000DCFC4
		public static OfflineAddressBookMapiProperty GetOABMapiProperty(uint mapiPropTag, OfflineAddressBookMapiPropertyOption option)
		{
			string value = string.Empty;
			if (Enum.IsDefined(typeof(PropTag), mapiPropTag))
			{
				value = ((PropTag)mapiPropTag).ToString();
			}
			if (string.IsNullOrEmpty(value) && Enum.IsDefined(typeof(CustomPropTag), mapiPropTag))
			{
				value = ((CustomPropTag)mapiPropTag).ToString();
			}
			int key = OfflineAddressBookMapiProperty.MapiIdFromPropTag(mapiPropTag);
			if (string.IsNullOrEmpty(value))
			{
				OfflineAddressBookMapiProperty.ReadSchema();
				if (OfflineAddressBookMapiProperty.mapiIdToSchemaAttr.ContainsKey(key))
				{
					ADSchemaAttributeObject adschemaAttributeObject = OfflineAddressBookMapiProperty.mapiIdToSchemaAttr[key];
					value = adschemaAttributeObject.LdapDisplayName;
				}
			}
			if (string.IsNullOrEmpty(value))
			{
				value = string.Format("0x{0:x8}", mapiPropTag);
			}
			return new OfflineAddressBookMapiProperty(mapiPropTag, value, option);
		}

		// Token: 0x060039C5 RID: 14789 RVA: 0x000DEE7C File Offset: 0x000DD07C
		public static OfflineAddressBookMapiProperty Parse(string s)
		{
			OfflineAddressBookMapiPropertyOption offlineAddressBookMapiPropertyOption = OfflineAddressBookMapiPropertyOption.Value;
			if (string.IsNullOrEmpty(s))
			{
				throw new ArgumentNullException();
			}
			string[] array = s.Split(new char[]
			{
				','
			});
			if (array.Length != 2)
			{
				throw new FormatException(DirectoryStrings.InvalidOABMapiPropertyParseStringException(s));
			}
			string text = array[0].Trim();
			string text2 = array[1].Trim();
			try
			{
				offlineAddressBookMapiPropertyOption = (OfflineAddressBookMapiPropertyOption)Enum.Parse(typeof(OfflineAddressBookMapiPropertyOption), text2, true);
			}
			catch (ArgumentException)
			{
				throw new ArgumentException(DirectoryStrings.InvalidOABMapiPropertyTypeException(text2));
			}
			return new OfflineAddressBookMapiProperty(0U, text, offlineAddressBookMapiPropertyOption);
		}

		// Token: 0x060039C6 RID: 14790 RVA: 0x000DEF1C File Offset: 0x000DD11C
		internal static OfflineAddressBookMapiProperty ParseSerializationString(string fullString)
		{
			string[] array = fullString.Split(new char[]
			{
				','
			});
			if (array.Length != 3)
			{
				throw new FormatException(DirectoryStrings.InvalidOABMapiPropertyParseStringException(fullString));
			}
			string text = array[0].Trim();
			uint num = uint.Parse(array[1].Trim());
			OfflineAddressBookMapiPropertyOption offlineAddressBookMapiPropertyOption = (OfflineAddressBookMapiPropertyOption)Enum.Parse(typeof(OfflineAddressBookMapiPropertyOption), array[2].Trim());
			return new OfflineAddressBookMapiProperty(num, text, offlineAddressBookMapiPropertyOption);
		}

		// Token: 0x060039C7 RID: 14791 RVA: 0x000DEF94 File Offset: 0x000DD194
		internal void ResolveMapiPropTag()
		{
			if (this.mapiPropTag != 0U)
			{
				return;
			}
			int num = 0;
			if (int.TryParse(this.mapiPropName, NumberStyles.Integer, null, out num) || (CultureInfo.InvariantCulture.CompareInfo.IsPrefix(this.mapiPropName, "0x", CompareOptions.IgnoreCase) && int.TryParse(this.mapiPropName.Substring(2), NumberStyles.HexNumber, null, out num)))
			{
				OfflineAddressBookMapiProperty.ReadSchema();
				ADSchemaAttributeObject adschemaAttributeObject;
				int num2;
				int num3;
				if (num >= 1 && num <= 65534)
				{
					if (!OfflineAddressBookMapiProperty.mapiIdToSchemaAttr.ContainsKey(num))
					{
						throw new ArgumentException(DirectoryStrings.UnableToResolveMapiIdException(num));
					}
					adschemaAttributeObject = OfflineAddressBookMapiProperty.mapiIdToSchemaAttr[num];
					num2 = adschemaAttributeObject.MapiID;
					num3 = OfflineAddressBookMapiProperty.MapiPropTypeFromSchemaSyntax(adschemaAttributeObject.OMSyntax, !adschemaAttributeObject.IsSingleValued);
				}
				else
				{
					num2 = OfflineAddressBookMapiProperty.MapiIdFromPropTag((uint)num);
					int num4 = num & 65535;
					if (!OfflineAddressBookMapiProperty.mapiIdToSchemaAttr.ContainsKey(num2))
					{
						throw new ArgumentException(DirectoryStrings.UnableToResolveMapiIdException(num2));
					}
					adschemaAttributeObject = OfflineAddressBookMapiProperty.mapiIdToSchemaAttr[num2];
					num3 = OfflineAddressBookMapiProperty.MapiPropTypeFromSchemaSyntax(adschemaAttributeObject.OMSyntax, !adschemaAttributeObject.IsSingleValued);
					if (num3 != num4)
					{
						if ((num3 & 4096) != (num4 & 4096) || (((num3 & -4097) != 30 || (num4 & -4097) != 31) && ((num3 & -4097) != 31 || (num4 & -4097) != 30)))
						{
							throw new ArgumentException(DirectoryStrings.MismatchedMapiPropertyTypesException(num4, num3));
						}
						num3 = num4;
					}
				}
				this.mapiPropTag = (uint)(num3 | num2 << 16);
				this.mapiPropName = ((adschemaAttributeObject == null) ? string.Format("0x{0:x8}", this.mapiPropTag) : adschemaAttributeObject.LdapDisplayName);
			}
			if (this.mapiPropTag == 0U)
			{
				try
				{
					this.mapiPropTag = (uint)Enum.Parse(typeof(PropTag), this.mapiPropName, true);
					this.mapiPropName = ((PropTag)this.mapiPropTag).ToString();
				}
				catch (ArgumentException)
				{
				}
			}
			if (this.mapiPropTag == 0U)
			{
				try
				{
					this.mapiPropTag = (uint)Enum.Parse(typeof(CustomPropTag), this.mapiPropName, true);
					this.mapiPropName = ((CustomPropTag)this.mapiPropTag).ToString();
				}
				catch (ArgumentException)
				{
				}
			}
			if (this.mapiPropTag == 0U)
			{
				OfflineAddressBookMapiProperty.ReadSchema();
				if (OfflineAddressBookMapiProperty.schemaNameToSchemaAttr.ContainsKey(this.mapiPropName.ToLower()))
				{
					ADSchemaAttributeObject adschemaAttributeObject2 = OfflineAddressBookMapiProperty.schemaNameToSchemaAttr[this.mapiPropName.ToLower()];
					int num2 = adschemaAttributeObject2.MapiID;
					int num3 = OfflineAddressBookMapiProperty.MapiPropTypeFromSchemaSyntax(adschemaAttributeObject2.OMSyntax, !adschemaAttributeObject2.IsSingleValued);
					this.mapiPropTag = (uint)(num3 | num2 << 16);
					this.mapiPropName = adschemaAttributeObject2.Name;
				}
			}
			if (this.mapiPropTag == 0U && OfflineAddressBookMapiProperty.ldapDisplayNameToSchemaAttr.ContainsKey(this.mapiPropName.ToLower()))
			{
				ADSchemaAttributeObject adschemaAttributeObject3 = OfflineAddressBookMapiProperty.ldapDisplayNameToSchemaAttr[this.mapiPropName.ToLower()];
				int num2 = adschemaAttributeObject3.MapiID;
				int num3 = OfflineAddressBookMapiProperty.MapiPropTypeFromSchemaSyntax(adschemaAttributeObject3.OMSyntax, !adschemaAttributeObject3.IsSingleValued);
				this.mapiPropTag = (uint)(num3 | num2 << 16);
				this.mapiPropName = adschemaAttributeObject3.LdapDisplayName;
			}
			if (this.mapiPropTag == 0U)
			{
				throw new ArgumentException(DirectoryStrings.UnableToResolveMapiPropertyNameException(this.mapiPropName));
			}
		}

		// Token: 0x060039C8 RID: 14792 RVA: 0x000DF2E4 File Offset: 0x000DD4E4
		public override string ToString()
		{
			return string.Format("{0}, {1}", this.mapiPropName, this.option.ToString());
		}

		// Token: 0x060039C9 RID: 14793 RVA: 0x000DF306 File Offset: 0x000DD506
		internal string ToSerializationString()
		{
			return string.Format("{0}, {1}, {2}", this.mapiPropName, this.PropertyTag, this.option.ToString());
		}

		// Token: 0x060039CA RID: 14794 RVA: 0x000DF334 File Offset: 0x000DD534
		private static void ReadSchema()
		{
			lock (OfflineAddressBookMapiProperty.schemaReadLock)
			{
				if (!OfflineAddressBookMapiProperty.schemaDictionariesPopulated)
				{
					IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 633, "ReadSchema", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\OfflineAddressBookMapiProperty.cs");
					PropertyDefinition mapiID = ADSchemaAttributeSchema.MapiID;
					ADSchemaAttributeObject[] array = tenantOrTopologyConfigurationSession.Find<ADSchemaAttributeObject>(tenantOrTopologyConfigurationSession.SchemaNamingContext, QueryScope.SubTree, new ExistsFilter(mapiID), null, int.MaxValue);
					OfflineAddressBookMapiProperty.schemaNameToSchemaAttr = new Dictionary<string, ADSchemaAttributeObject>();
					OfflineAddressBookMapiProperty.ldapDisplayNameToSchemaAttr = new Dictionary<string, ADSchemaAttributeObject>();
					OfflineAddressBookMapiProperty.mapiIdToSchemaAttr = new Dictionary<int, ADSchemaAttributeObject>();
					foreach (ADSchemaAttributeObject adschemaAttributeObject in array)
					{
						OfflineAddressBookMapiProperty.schemaNameToSchemaAttr.Add(adschemaAttributeObject.Name.ToLower(), adschemaAttributeObject);
						OfflineAddressBookMapiProperty.ldapDisplayNameToSchemaAttr.Add(adschemaAttributeObject.LdapDisplayName.ToLower(), adschemaAttributeObject);
						OfflineAddressBookMapiProperty.mapiIdToSchemaAttr.Add(adschemaAttributeObject.MapiID, adschemaAttributeObject);
					}
					OfflineAddressBookMapiProperty.schemaDictionariesPopulated = true;
				}
			}
		}

		// Token: 0x060039CB RID: 14795 RVA: 0x000DF438 File Offset: 0x000DD638
		private static int MapiPropTypeFromSchemaSyntax(AttributeSyntax syntax, bool multiValued)
		{
			int num;
			if (syntax <= AttributeSyntax.Enumeration)
			{
				switch (syntax)
				{
				case AttributeSyntax.Boolean:
					num = 11;
					goto IL_BD;
				case AttributeSyntax.Integer:
					break;
				case (AttributeSyntax)3:
				case (AttributeSyntax)5:
				case AttributeSyntax.ObjectIdentifier:
					goto IL_A2;
				case AttributeSyntax.Sid:
					goto IL_95;
				default:
					if (syntax != AttributeSyntax.Enumeration)
					{
						goto IL_A2;
					}
					break;
				}
				num = 3;
				goto IL_BD;
			}
			switch (syntax)
			{
			case AttributeSyntax.Numeric:
			case AttributeSyntax.Printable:
			case AttributeSyntax.Teletex:
			case AttributeSyntax.IA5:
			case AttributeSyntax.CaseSensitive:
				break;
			case (AttributeSyntax)21:
			case (AttributeSyntax)25:
			case (AttributeSyntax)26:
				goto IL_A2;
			case AttributeSyntax.UTCTime:
			case AttributeSyntax.GeneralizedTime:
				num = 64;
				goto IL_BD;
			default:
				switch (syntax)
				{
				case AttributeSyntax.Unicode:
					break;
				case AttributeSyntax.Interval:
					num = 20;
					goto IL_BD;
				case AttributeSyntax.NTSecDesc:
					goto IL_95;
				default:
					if (syntax != AttributeSyntax.AccessPoint)
					{
						goto IL_A2;
					}
					num = 30;
					goto IL_BD;
				}
				break;
			}
			num = 31;
			goto IL_BD;
			IL_95:
			num = 258;
			goto IL_BD;
			IL_A2:
			throw new InvalidOperationException(DirectoryStrings.UnsupportedADSyntaxException(syntax.ToString()));
			IL_BD:
			if (multiValued)
			{
				num |= 4096;
			}
			return num;
		}

		// Token: 0x17001217 RID: 4631
		// (get) Token: 0x060039CC RID: 14796 RVA: 0x000DF510 File Offset: 0x000DD710
		public static MultiValuedProperty<OfflineAddressBookMapiProperty> DefaultOABPropertyList
		{
			get
			{
				if (OfflineAddressBookMapiProperty.defaultOABPropertyList == null)
				{
					lock (OfflineAddressBookMapiProperty.defaultOABPropertyListInitializationLock)
					{
						if (OfflineAddressBookMapiProperty.defaultOABPropertyList == null)
						{
							MultiValuedProperty<OfflineAddressBookMapiProperty> multiValuedProperty = new MultiValuedProperty<OfflineAddressBookMapiProperty>();
							foreach (OfflineAddressBookMapiProperty item in OfflineAddressBookMapiProperty.coreDefaultOABPropertyList)
							{
								multiValuedProperty.Add(item);
							}
							OfflineAddressBookMapiProperty.defaultOABPropertyList = multiValuedProperty;
						}
					}
				}
				return OfflineAddressBookMapiProperty.defaultOABPropertyList;
			}
		}

		// Token: 0x04002772 RID: 10098
		private static bool schemaDictionariesPopulated = false;

		// Token: 0x04002773 RID: 10099
		private static object schemaReadLock = new object();

		// Token: 0x04002774 RID: 10100
		private static object defaultOABPropertyListInitializationLock = new object();

		// Token: 0x04002775 RID: 10101
		private static Dictionary<string, ADSchemaAttributeObject> schemaNameToSchemaAttr = null;

		// Token: 0x04002776 RID: 10102
		private static Dictionary<string, ADSchemaAttributeObject> ldapDisplayNameToSchemaAttr = null;

		// Token: 0x04002777 RID: 10103
		private static Dictionary<int, ADSchemaAttributeObject> mapiIdToSchemaAttr = null;

		// Token: 0x04002778 RID: 10104
		private static OfflineAddressBookMapiProperty[] coreDefaultOABPropertyList = new OfflineAddressBookMapiProperty[]
		{
			new OfflineAddressBookMapiProperty(805371935U, "DisplayName", OfflineAddressBookMapiPropertyOption.ANR),
			new OfflineAddressBookMapiProperty(2358378527U, "PhoneticDisplayName", OfflineAddressBookMapiPropertyOption.ANR),
			new OfflineAddressBookMapiProperty(973078559U, "Account", OfflineAddressBookMapiPropertyOption.ANR),
			new OfflineAddressBookMapiProperty(974192671U, "Surname", OfflineAddressBookMapiPropertyOption.ANR),
			new OfflineAddressBookMapiProperty(2358181919U, "PhoneticSurname", OfflineAddressBookMapiPropertyOption.ANR),
			new OfflineAddressBookMapiProperty(973471775U, "GivenName", OfflineAddressBookMapiPropertyOption.ANR),
			new OfflineAddressBookMapiProperty(2358116383U, "PhoneticGivenName", OfflineAddressBookMapiPropertyOption.ANR),
			new OfflineAddressBookMapiProperty(2148470815U, "ProxyAddresses", OfflineAddressBookMapiPropertyOption.ANR),
			new OfflineAddressBookMapiProperty(974716959U, "OfficeLocation", OfflineAddressBookMapiPropertyOption.ANR),
			new OfflineAddressBookMapiProperty(956301315U, "DisplayType", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(268304387U, "ObjectType", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(977272843U, "SendRichInfo", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(973602847U, "BusinessTelephoneNumber", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(973733919U, "Initials", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(975765535U, "StreetAddress", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(975634463U, "Locality", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(975699999U, "StateOrProvince", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(975831071U, "PostalCode", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(975568927U, "Country", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(974585887U, "Title", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(974520351U, "CompanyName", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(2358312991U, "PhoneticCompanyName", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(976224287U, "Assistant", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(974651423U, "DepartmentName", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(2358247455U, "PhoneticDepartmentName", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(2148597791U, "TargetAddress", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(973668383U, "HomeTelephoneNumber", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(974852127U, "otherTelephone", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(976162847U, "otherHomePhone", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(975372319U, "PrimaryFaxNumber", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(974913567U, "MobileTelephoneNumber", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(976093215U, "AssistantTelephoneNumber", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(975241247U, "PagerTelephoneNumber", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(805568543U, "Comment", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(975307010U, "UserCertificate", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(980422914U, "userSMIMECertificate", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(2355761410U, "X509Cert", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(2147876894U, "HomeMdbA", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(973013022U, "DisplayNamePrintableA", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(956628995U, "DisplayTypeEx", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(2355953922U, "ObjectGuid", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(2360086559U, "MailTipTranslations", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(2154430467U, "MaxReceiveSize", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(2360672267U, "ModerationEnabled", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(2363621379U, "TotalMemberCount", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(2363686915U, "ExternalMemberCount", OfflineAddressBookMapiPropertyOption.Value),
			new OfflineAddressBookMapiProperty(2359165186U, "ThumbnailPhoto", OfflineAddressBookMapiPropertyOption.Indicator),
			new OfflineAddressBookMapiProperty(2361524482U, "UmSpokenName", OfflineAddressBookMapiPropertyOption.Indicator),
			new OfflineAddressBookMapiProperty(2362966029U, "AcceptMessagesOnlyFrom", OfflineAddressBookMapiPropertyOption.Indicator),
			new OfflineAddressBookMapiProperty(2363031565U, "RejectMessagesFrom", OfflineAddressBookMapiPropertyOption.Indicator),
			new OfflineAddressBookMapiProperty(2363097101U, "AcceptMessagesOnlyFromDLMembers", OfflineAddressBookMapiPropertyOption.Indicator),
			new OfflineAddressBookMapiProperty(2363162637U, "RejectMessagesFromDLMembers", OfflineAddressBookMapiPropertyOption.Indicator)
		};

		// Token: 0x04002779 RID: 10105
		private static MultiValuedProperty<OfflineAddressBookMapiProperty> defaultOABPropertyList;

		// Token: 0x0400277A RID: 10106
		private uint mapiPropTag;

		// Token: 0x0400277B RID: 10107
		private string mapiPropName = string.Empty;

		// Token: 0x0400277C RID: 10108
		private OfflineAddressBookMapiPropertyOption option = OfflineAddressBookMapiPropertyOption.Value;
	}
}
