using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000005 RID: 5
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class XO1CsvSchema : MigrationCsvSchemaBase
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002708 File Offset: 0x00000908
		public XO1CsvSchema() : base(int.MaxValue, XO1CsvSchema.requiredColumns, XO1CsvSchema.InternalOptionalColumns, null)
		{
		}

		// Token: 0x04000009 RID: 9
		public const string PuidColumn = "Puid";

		// Token: 0x0400000A RID: 10
		public const string FirstNameColumn = "FirstName";

		// Token: 0x0400000B RID: 11
		public const string LastNameColumn = "LastName";

		// Token: 0x0400000C RID: 12
		public const string AccountSizeColumn = "AccountSize";

		// Token: 0x0400000D RID: 13
		public const string TimeZoneColumn = "TimeZone";

		// Token: 0x0400000E RID: 14
		public const string LcidColumn = "Lcid";

		// Token: 0x0400000F RID: 15
		public const string AliasesColumn = "Aliases";

		// Token: 0x04000010 RID: 16
		public const char AliasesDelimiter = '\u0001';

		// Token: 0x04000011 RID: 17
		internal static readonly PropertyDefinitionConstraint[] PuidConstraint = new PropertyDefinitionConstraint[]
		{
			new MigrationCsvSchemaBase.CsvRangedValueConstraint<long>(long.MinValue, long.MaxValue)
		};

		// Token: 0x04000012 RID: 18
		internal static readonly PropertyDefinitionConstraint[] AccountSizeConstraint = new PropertyDefinitionConstraint[]
		{
			new MigrationCsvSchemaBase.CsvRangedValueConstraint<long>(long.MinValue, long.MaxValue)
		};

		// Token: 0x04000013 RID: 19
		internal static readonly PropertyDefinitionConstraint[] LcidConstraint = new PropertyDefinitionConstraint[]
		{
			new XO1CsvSchema.InternalLcidConstraint()
		};

		// Token: 0x04000014 RID: 20
		internal static readonly PropertyDefinitionConstraint[] TimeZoneConstraint = new PropertyDefinitionConstraint[]
		{
			new XO1CsvSchema.InternalTimeZoneConstraint()
		};

		// Token: 0x04000015 RID: 21
		internal static readonly PropertyDefinitionConstraint[] AliasesConstraint = new PropertyDefinitionConstraint[]
		{
			new XO1CsvSchema.InternalValidSmtpAddressListConstraint()
		};

		// Token: 0x04000016 RID: 22
		private static readonly Dictionary<string, ProviderPropertyDefinition> requiredColumns = new Dictionary<string, ProviderPropertyDefinition>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"EmailAddress",
				MigrationCsvSchemaBase.GetDefaultPropertyDefinition("EmailAddress", MigrationCsvSchemaBase.EmailAddressConstraint)
			},
			{
				"Puid",
				MigrationCsvSchemaBase.GetDefaultPropertyDefinition<long>("Puid", XO1CsvSchema.PuidConstraint)
			},
			{
				"AccountSize",
				MigrationCsvSchemaBase.GetDefaultPropertyDefinition<long>("AccountSize", XO1CsvSchema.AccountSizeConstraint)
			},
			{
				"Lcid",
				MigrationCsvSchemaBase.GetDefaultPropertyDefinition("Lcid", XO1CsvSchema.LcidConstraint)
			},
			{
				"TimeZone",
				MigrationCsvSchemaBase.GetDefaultPropertyDefinition("TimeZone", XO1CsvSchema.TimeZoneConstraint)
			}
		};

		// Token: 0x04000017 RID: 23
		private static readonly Dictionary<string, ProviderPropertyDefinition> InternalOptionalColumns = new Dictionary<string, ProviderPropertyDefinition>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"FirstName",
				ADUserSchema.FirstName
			},
			{
				"LastName",
				ADUserSchema.LastName
			},
			{
				"Aliases",
				MigrationCsvSchemaBase.GetDefaultPropertyDefinition("Aliases", XO1CsvSchema.AliasesConstraint)
			}
		};

		// Token: 0x02000006 RID: 6
		private class InternalLcidConstraint : PropertyDefinitionConstraint
		{
			// Token: 0x06000017 RID: 23 RVA: 0x000028A4 File Offset: 0x00000AA4
			public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
			{
				Exception ex = null;
				try
				{
					int culture = Convert.ToInt32(value);
					CultureInfo.GetCultureInfo(culture);
				}
				catch (FormatException ex2)
				{
					ex = ex2;
				}
				catch (InvalidCastException ex3)
				{
					ex = ex3;
				}
				catch (OverflowException ex4)
				{
					ex = ex4;
				}
				catch (CultureNotFoundException)
				{
					return new PropertyConstraintViolationError(DataStrings.ConstraintViolationEnumValueNotAllowed(value.ToString()), propertyDefinition, value, this);
				}
				if (ex != null)
				{
					return new PropertyConstraintViolationError(DataStrings.PropertyTypeMismatch(value.GetType().ToString(), typeof(int).ToString()), propertyDefinition, value, this);
				}
				return null;
			}
		}

		// Token: 0x02000007 RID: 7
		private class InternalTimeZoneConstraint : PropertyDefinitionConstraint
		{
			// Token: 0x06000019 RID: 25 RVA: 0x00002958 File Offset: 0x00000B58
			public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
			{
				string text = value.ToString();
				if (string.IsNullOrEmpty(text))
				{
					return new PropertyConstraintViolationError(DataStrings.ConstraintViolationValueIsNullOrEmpty, propertyDefinition, value, this);
				}
				try
				{
					if (ExTimeZone.UtcTimeZone.Id.Equals(text, StringComparison.Ordinal))
					{
						return null;
					}
					ExTimeZoneValue.Parse(text);
				}
				catch (FormatException)
				{
					return new PropertyConstraintViolationError(DataStrings.ConstraintViolationEnumValueNotAllowed(value.ToString()), propertyDefinition, value, this);
				}
				return null;
			}
		}

		// Token: 0x02000008 RID: 8
		private class InternalValidSmtpAddressListConstraint : PropertyDefinitionConstraint
		{
			// Token: 0x0600001B RID: 27 RVA: 0x000029D4 File Offset: 0x00000BD4
			public InternalValidSmtpAddressListConstraint()
			{
				this.emailAddressConstraint = new ValidSmtpAddressConstraint();
			}

			// Token: 0x0600001C RID: 28 RVA: 0x000029E8 File Offset: 0x00000BE8
			public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
			{
				string text = value.ToString();
				if (string.IsNullOrEmpty(text))
				{
					return new PropertyConstraintViolationError(DataStrings.ConstraintViolationValueIsNullOrEmpty, propertyDefinition, value, this);
				}
				foreach (string value2 in text.Split(new char[]
				{
					'\u0001'
				}))
				{
					PropertyConstraintViolationError propertyConstraintViolationError = this.emailAddressConstraint.Validate(value2, propertyDefinition, propertyBag);
					if (propertyConstraintViolationError != null)
					{
						return propertyConstraintViolationError;
					}
				}
				return null;
			}

			// Token: 0x04000018 RID: 24
			private ValidSmtpAddressConstraint emailAddressConstraint;
		}
	}
}
