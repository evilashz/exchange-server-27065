using System;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings
{
	// Token: 0x02000666 RID: 1638
	[Serializable]
	public class SettingOverride : ADConfigurationObject
	{
		// Token: 0x06004C8F RID: 19599 RVA: 0x0011AD3C File Offset: 0x00118F3C
		public static ADObjectId GetContainerId(bool isFlight)
		{
			ADObjectId rootOrgContainerIdForLocalForest = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest();
			return rootOrgContainerIdForLocalForest.GetDescendantId(isFlight ? SettingOverride.FlightsRelativePath : SettingOverride.SettingsRelativePath);
		}

		// Token: 0x06004C90 RID: 19600 RVA: 0x0011AD66 File Offset: 0x00118F66
		public static void Validate(VariantConfigurationOverride o)
		{
			SettingOverride.Validate(o, false);
		}

		// Token: 0x06004C91 RID: 19601 RVA: 0x0011AD84 File Offset: 0x00118F84
		public static void Validate(VariantConfigurationOverride o, bool criticalOnly)
		{
			try
			{
				new VariantConfigurationOverrideValidation(o, criticalOnly).Validate();
			}
			catch (NullOverrideException innerException)
			{
				throw new SettingOverrideNullException(innerException);
			}
			catch (FlightNameValidationException ex)
			{
				throw new SettingOverrideInvalidFlightNameException(ex.Override.ComponentName, string.Join(", ", ex.AllowedValues), ex);
			}
			catch (ComponentNameValidationException ex2)
			{
				throw new SettingOverrideInvalidComponentNameException(ex2.Override.ComponentName, string.Join(", ", ex2.AllowedValues), ex2);
			}
			catch (SectionNameValidationException ex3)
			{
				throw new SettingOverrideInvalidSectionNameException(ex3.Override.ComponentName, ex3.Override.SectionName, string.Join(", ", ex3.AllowedValues), ex3);
			}
			catch (ParameterNameValidationException ex4)
			{
				throw new SettingOverrideInvalidParameterNameException(ex4.Override.ComponentName, ex4.Override.SectionName, ex4.ParameterName, string.Join(", ", ex4.AllowedValues), ex4);
			}
			catch (ParameterSyntaxValidationException ex5)
			{
				throw new SettingOverrideInvalidParameterSyntaxException(ex5.Override.ComponentName, ex5.Override.SectionName, ex5.ParameterLine, ex5);
			}
			catch (VariantNameValidationException ex6)
			{
				throw new SettingOverrideInvalidVariantNameException(ex6.Override.ComponentName, ex6.Override.SectionName, ex6.VariantName, string.Join(", ", ex6.AllowedValues), ex6);
			}
			catch (VariantValueValidationException ex7)
			{
				throw new SettingOverrideInvalidVariantValueException(ex7.Override.ComponentName, ex7.Override.SectionName, ex7.Value, ex7.Variant.Type.Name, ex7.Format, ex7);
			}
			catch (SyntaxValidationException ex8)
			{
				throw new SettingOverrideSyntaxException(ex8.Override.ComponentName, ex8.Override.SectionName, "@(\"" + string.Join("\", \"", Array.ConvertAll<string, string>(ex8.Override.Parameters, (string parameter) => parameter.Replace("\"", "^\""))) + "\")", (ex8.InnerException != null) ? ex8.InnerException.Message : ex8.Message, ex8);
			}
			catch (OverrideValidationException ex9)
			{
				throw new SettingOverrideGenericException(ex9.GetType().Name, ex9.Override.ComponentName, ex9.Override.SectionName, SettingOverride.FormatParameters(ex9.Override.Parameters), ex9);
			}
			catch (Exception ex10)
			{
				throw new SettingOverrideUnexpectedException(ex10.GetType().Name, ex10);
			}
		}

		// Token: 0x06004C92 RID: 19602 RVA: 0x0011B060 File Offset: 0x00119260
		public void SetName(string name, bool isFlight)
		{
			base.SetId(SettingOverride.GetContainerId(isFlight).GetChildId(name));
		}

		// Token: 0x06004C93 RID: 19603 RVA: 0x0011B074 File Offset: 0x00119274
		public VariantConfigurationOverride GetVariantConfigurationOverride()
		{
			if (this.FlightName != null)
			{
				return new VariantConfigurationFlightOverride(this.FlightName, this.Parameters.ToArray());
			}
			return new VariantConfigurationSettingOverride(this.ComponentName, this.SectionName, this.Parameters.ToArray());
		}

		// Token: 0x17001932 RID: 6450
		// (get) Token: 0x06004C94 RID: 19604 RVA: 0x0011B0B1 File Offset: 0x001192B1
		public string ComponentName
		{
			get
			{
				return this.Xml.ComponentName;
			}
		}

		// Token: 0x17001933 RID: 6451
		// (get) Token: 0x06004C95 RID: 19605 RVA: 0x0011B0BE File Offset: 0x001192BE
		public string SectionName
		{
			get
			{
				return this.Xml.SectionName;
			}
		}

		// Token: 0x17001934 RID: 6452
		// (get) Token: 0x06004C96 RID: 19606 RVA: 0x0011B0CB File Offset: 0x001192CB
		public string FlightName
		{
			get
			{
				return this.Xml.FlightName;
			}
		}

		// Token: 0x17001935 RID: 6453
		// (get) Token: 0x06004C97 RID: 19607 RVA: 0x0011B0D8 File Offset: 0x001192D8
		public string ModifiedBy
		{
			get
			{
				return this.Xml.ModifiedBy;
			}
		}

		// Token: 0x17001936 RID: 6454
		// (get) Token: 0x06004C98 RID: 19608 RVA: 0x0011B0E5 File Offset: 0x001192E5
		public string Reason
		{
			get
			{
				return this.Xml.Reason;
			}
		}

		// Token: 0x17001937 RID: 6455
		// (get) Token: 0x06004C99 RID: 19609 RVA: 0x0011B0F2 File Offset: 0x001192F2
		public Version MinVersion
		{
			get
			{
				return this.Xml.MinVersion;
			}
		}

		// Token: 0x17001938 RID: 6456
		// (get) Token: 0x06004C9A RID: 19610 RVA: 0x0011B0FF File Offset: 0x001192FF
		public Version MaxVersion
		{
			get
			{
				return this.Xml.MaxVersion;
			}
		}

		// Token: 0x17001939 RID: 6457
		// (get) Token: 0x06004C9B RID: 19611 RVA: 0x0011B10C File Offset: 0x0011930C
		public Version FixVersion
		{
			get
			{
				return this.Xml.FixVersion;
			}
		}

		// Token: 0x1700193A RID: 6458
		// (get) Token: 0x06004C9C RID: 19612 RVA: 0x0011B119 File Offset: 0x00119319
		public string[] Server
		{
			get
			{
				return this.Xml.Server;
			}
		}

		// Token: 0x1700193B RID: 6459
		// (get) Token: 0x06004C9D RID: 19613 RVA: 0x0011B126 File Offset: 0x00119326
		public MultiValuedProperty<string> Parameters
		{
			get
			{
				return this.Xml.Parameters;
			}
		}

		// Token: 0x1700193C RID: 6460
		// (get) Token: 0x06004C9E RID: 19614 RVA: 0x0011B133 File Offset: 0x00119333
		internal bool Applies
		{
			get
			{
				return this.Xml != null && this.Xml.Applies && this.Xml.Parameters.Count > 0;
			}
		}

		// Token: 0x1700193D RID: 6461
		// (get) Token: 0x06004C9F RID: 19615 RVA: 0x0011B15F File Offset: 0x0011935F
		// (set) Token: 0x06004CA0 RID: 19616 RVA: 0x0011B171 File Offset: 0x00119371
		public string XmlRaw
		{
			get
			{
				return (string)this[SettingOverrideSchema.ConfigurationXmlRaw];
			}
			set
			{
				this[SettingOverrideSchema.ConfigurationXmlRaw] = value;
			}
		}

		// Token: 0x1700193E RID: 6462
		// (get) Token: 0x06004CA1 RID: 19617 RVA: 0x0011B17F File Offset: 0x0011937F
		// (set) Token: 0x06004CA2 RID: 19618 RVA: 0x0011B191 File Offset: 0x00119391
		internal SettingOverrideXml Xml
		{
			get
			{
				return (SettingOverrideXml)this[SettingOverrideSchema.ConfigurationXml];
			}
			set
			{
				this[SettingOverrideSchema.ConfigurationXml] = value;
			}
		}

		// Token: 0x1700193F RID: 6463
		// (get) Token: 0x06004CA3 RID: 19619 RVA: 0x0011B19F File Offset: 0x0011939F
		internal override ADObjectSchema Schema
		{
			get
			{
				return SettingOverride.schema;
			}
		}

		// Token: 0x17001940 RID: 6464
		// (get) Token: 0x06004CA4 RID: 19620 RVA: 0x0011B1A6 File Offset: 0x001193A6
		internal override string MostDerivedObjectClass
		{
			get
			{
				return SettingOverride.mostDerivedClass;
			}
		}

		// Token: 0x17001941 RID: 6465
		// (get) Token: 0x06004CA5 RID: 19621 RVA: 0x0011B1AD File Offset: 0x001193AD
		internal override ADObjectId ParentPath
		{
			get
			{
				return SettingOverride.SettingsRelativePath;
			}
		}

		// Token: 0x06004CA6 RID: 19622 RVA: 0x0011B1B4 File Offset: 0x001193B4
		private static string FormatParameters(string[] parameters)
		{
			return "{'" + string.Join("', '", parameters) + "'}";
		}

		// Token: 0x04003466 RID: 13414
		public static ADObjectId SettingsRelativePath = new ADObjectId("CN=Setting Overrides,CN=Global Settings");

		// Token: 0x04003467 RID: 13415
		public static ADObjectId FlightsRelativePath = new ADObjectId("CN=Flight Overrides,CN=Global Settings");

		// Token: 0x04003468 RID: 13416
		private static SettingOverrideSchema schema = ObjectSchema.GetInstance<SettingOverrideSchema>();

		// Token: 0x04003469 RID: 13417
		private static string mostDerivedClass = "msExchConfigSettings";
	}
}
