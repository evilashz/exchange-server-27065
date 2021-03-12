using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200004C RID: 76
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationBatchCsvSchema : MigrationCsvSchemaBase
	{
		// Token: 0x0600038A RID: 906 RVA: 0x0000D0DF File Offset: 0x0000B2DF
		public MigrationBatchCsvSchema() : base(50000, MigrationBatchCsvSchema.requiredColumns, MigrationBatchCsvSchema.optionalColumns, null)
		{
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000D0F7 File Offset: 0x0000B2F7
		protected MigrationBatchCsvSchema(int maximumRowCount, Dictionary<string, ProviderPropertyDefinition> requiredColumns, Dictionary<string, ProviderPropertyDefinition> optionalColumns, IEnumerable<string> prohibitedColumns) : base(maximumRowCount, requiredColumns, optionalColumns, prohibitedColumns)
		{
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000D104 File Offset: 0x0000B304
		protected virtual PropertyDefinitionConstraint[] ClearTextPasswordConstraints
		{
			get
			{
				return new PropertyDefinitionConstraint[]
				{
					new StringLengthConstraint(1, 256),
					new NoLeadingOrTrailingWhitespaceConstraint()
				};
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000D12F File Offset: 0x0000B32F
		private ProviderPropertyDefinition PasswordPropertyDefinition
		{
			get
			{
				return MigrationCsvSchemaBase.GetDefaultPropertyDefinition("Password", this.ClearTextPasswordConstraints);
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000D144 File Offset: 0x0000B344
		public static PropertyConstraintViolationError ValidatePasswordIsNotBadPasswordValue(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag, PropertyDefinitionConstraint owner)
		{
			string a = value as string;
			if (string.Equals(a, "<Invalid Password>?`~! 23aeb1e0-b53e-41be-9565-7ca88c97b5b2\teda6458e-c843-45c3-b97f-73aa3c06ba9c"))
			{
				return new PropertyConstraintViolationError(ServerStrings.MigrationInvalidPassword, propertyDefinition, value, owner);
			}
			return null;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000D174 File Offset: 0x0000B374
		public override void ProcessRow(CsvRow row, out MigrationBatchError error)
		{
			error = null;
			base.ProcessRow(row, out error);
			string text;
			if (error == null && row.TryGetColumnValue("Password", out text) && !string.IsNullOrEmpty(text))
			{
				error = this.ValidateClearTextPassword(text, row, this.PasswordPropertyDefinition);
			}
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000D1B8 File Offset: 0x0000B3B8
		protected virtual MigrationBatchError ValidateClearTextPassword(string clearTextPassword, CsvRow row, ProviderPropertyDefinition propertyDefinition)
		{
			PropertyValidationError propertyValidationError = propertyDefinition.ValidateValue(clearTextPassword, false);
			if (propertyValidationError != null)
			{
				row[propertyDefinition.Name] = "<Invalid Password>?`~! 23aeb1e0-b53e-41be-9565-7ca88c97b5b2\teda6458e-c843-45c3-b97f-73aa3c06ba9c";
				return base.CreateValidationError(row, ServerStrings.ColumnError(propertyDefinition.Name, propertyValidationError.Description));
			}
			row[propertyDefinition.Name] = MigrationServiceFactory.Instance.GetCryptoAdapter().ClearStringToEncryptedString(clearTextPassword);
			return null;
		}

		// Token: 0x040000F8 RID: 248
		public const int InternalMaximumRowCount = 50000;

		// Token: 0x040000F9 RID: 249
		public const int HeaderRowIndex = 0;

		// Token: 0x040000FA RID: 250
		internal const string UsernameColumnName = "Username";

		// Token: 0x040000FB RID: 251
		internal const string PasswordColumnName = "Password";

		// Token: 0x040000FC RID: 252
		internal const string UserRootFolderName = "UserRoot";

		// Token: 0x040000FD RID: 253
		internal const string BadPasswordValue = "<Invalid Password>?`~! 23aeb1e0-b53e-41be-9565-7ca88c97b5b2\teda6458e-c843-45c3-b97f-73aa3c06ba9c";

		// Token: 0x040000FE RID: 254
		private const int PasswordMaxLength = 256;

		// Token: 0x040000FF RID: 255
		internal static readonly PropertyDefinitionConstraint[] BadPasswordConstraint = new DelegateConstraint[]
		{
			new DelegateConstraint(new ValidationDelegate(MigrationBatchCsvSchema.ValidatePasswordIsNotBadPasswordValue))
		};

		// Token: 0x04000100 RID: 256
		private static readonly Dictionary<string, ProviderPropertyDefinition> requiredColumns = new Dictionary<string, ProviderPropertyDefinition>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"EmailAddress",
				MigrationCsvSchemaBase.GetDefaultPropertyDefinition("EmailAddress", MigrationCsvSchemaBase.EmailAddressConstraint)
			},
			{
				"Username",
				ADRecipientSchema.DisplayName
			},
			{
				"Password",
				MigrationCsvSchemaBase.GetDefaultPropertyDefinition("Password", MigrationBatchCsvSchema.BadPasswordConstraint)
			}
		};

		// Token: 0x04000101 RID: 257
		private static readonly PropertyDefinitionConstraint[] UserRootFolderConstraints = new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 1024),
			new NoLeadingOrTrailingWhitespaceConstraint()
		};

		// Token: 0x04000102 RID: 258
		private static readonly Dictionary<string, ProviderPropertyDefinition> optionalColumns = new Dictionary<string, ProviderPropertyDefinition>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"UserRoot",
				MigrationCsvSchemaBase.GetDefaultPropertyDefinition("Password", MigrationBatchCsvSchema.UserRootFolderConstraints)
			}
		};
	}
}
