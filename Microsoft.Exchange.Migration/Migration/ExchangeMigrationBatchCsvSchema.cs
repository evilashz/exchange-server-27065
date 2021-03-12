using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200009A RID: 154
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ExchangeMigrationBatchCsvSchema : MigrationBatchCsvSchema
	{
		// Token: 0x060008DB RID: 2267 RVA: 0x00026492 File Offset: 0x00024692
		public ExchangeMigrationBatchCsvSchema() : base(ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("MigrationSourceStagedExchangeCSVMailboxMaximumCount"), ExchangeMigrationBatchCsvSchema.requiredColumns, ExchangeMigrationBatchCsvSchema.optionalColumns, null)
		{
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x000264B0 File Offset: 0x000246B0
		protected override PropertyDefinitionConstraint[] ClearTextPasswordConstraints
		{
			get
			{
				return new PropertyDefinitionConstraint[]
				{
					new StringLengthConstraint(6, 16),
					new NoLeadingOrTrailingWhitespaceConstraint()
				};
			}
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x000264D8 File Offset: 0x000246D8
		public override void ValidateRow(CsvRow row)
		{
			base.ValidateRow(row);
			string text;
			bool flag;
			if (row.TryGetColumnValue("ForceChangePassword", out text) && !string.IsNullOrEmpty(text) && !bool.TryParse(text, out flag))
			{
				PropertyValidationError error = new PropertyValidationError(DataStrings.ConstraintViolationValueIsNotAllowed("true, false", text), ExchangeMigrationBatchCsvSchema.ForceChangePasswordPropertyDefinition, null);
				base.OnPropertyValidationError(row, "ForceChangePassword", error);
			}
		}

		// Token: 0x04000362 RID: 866
		public const string ForceChangePasswordColumnName = "ForceChangePassword";

		// Token: 0x04000363 RID: 867
		private const int PasswordMaxLength = 16;

		// Token: 0x04000364 RID: 868
		private const int PasswordMinLength = 6;

		// Token: 0x04000365 RID: 869
		private static readonly ProviderPropertyDefinition ForceChangePasswordPropertyDefinition = ADUserSchema.ResetPasswordOnNextLogon;

		// Token: 0x04000366 RID: 870
		private static readonly Dictionary<string, ProviderPropertyDefinition> requiredColumns = new Dictionary<string, ProviderPropertyDefinition>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"EmailAddress",
				MigrationCsvSchemaBase.GetDefaultPropertyDefinition("EmailAddress", MigrationCsvSchemaBase.EmailAddressConstraint)
			}
		};

		// Token: 0x04000367 RID: 871
		private static readonly Dictionary<string, ProviderPropertyDefinition> optionalColumns = new Dictionary<string, ProviderPropertyDefinition>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"Password",
				MigrationCsvSchemaBase.GetDefaultPropertyDefinition("Password", MigrationBatchCsvSchema.BadPasswordConstraint)
			},
			{
				"ForceChangePassword",
				ExchangeMigrationBatchCsvSchema.ForceChangePasswordPropertyDefinition
			},
			{
				"Username",
				MigrationCsvSchemaBase.GetDefaultPropertyDefinition("Username", MigrationCsvSchemaBase.EmailAddressConstraint)
			}
		};
	}
}
