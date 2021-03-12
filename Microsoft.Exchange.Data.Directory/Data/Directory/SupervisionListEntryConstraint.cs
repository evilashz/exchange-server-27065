using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020001A1 RID: 417
	internal sealed class SupervisionListEntryConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x060011B4 RID: 4532 RVA: 0x000566DD File Offset: 0x000548DD
		public SupervisionListEntryConstraint(bool oneOffEntry)
		{
			this.oneOffEntry = oneOffEntry;
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x000566EC File Offset: 0x000548EC
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			ADObjectIdWithString adobjectIdWithString = value as ADObjectIdWithString;
			if (adobjectIdWithString == null)
			{
				return null;
			}
			string stringValue = adobjectIdWithString.StringValue;
			if (stringValue.Equals(string.Empty) || stringValue.IndexOf(SupervisionListEntryConstraint.Delimiter) == 0 || stringValue.LastIndexOf(SupervisionListEntryConstraint.Delimiter) == stringValue.Length - 1)
			{
				return new PropertyConstraintViolationError(DirectoryStrings.ConstraintViolationSupervisionListEntryStringPartIsInvalid, propertyDefinition, value, this);
			}
			if (this.oneOffEntry)
			{
				int num = stringValue.LastIndexOf(SupervisionListEntryConstraint.Delimiter);
				string address = stringValue.Substring(num + 1, stringValue.Length - (num + 1));
				if (num == -1 || !SmtpAddress.IsValidSmtpAddress(address))
				{
					return new PropertyConstraintViolationError(DirectoryStrings.ConstraintViolationOneOffSupervisionListEntryStringPartIsInvalid, propertyDefinition, value, this);
				}
			}
			return null;
		}

		// Token: 0x04000A38 RID: 2616
		public static readonly char Delimiter = ',';

		// Token: 0x04000A39 RID: 2617
		public static readonly char[] SupervisionTagInvalidChars = new char[]
		{
			',',
			'@'
		};

		// Token: 0x04000A3A RID: 2618
		private readonly bool oneOffEntry;
	}
}
