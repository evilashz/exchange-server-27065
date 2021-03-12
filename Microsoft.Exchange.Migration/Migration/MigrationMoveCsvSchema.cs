using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.Management.Migration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200007A RID: 122
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationMoveCsvSchema : MigrationBatchCsvSchema
	{
		// Token: 0x060006D4 RID: 1748 RVA: 0x0001F04C File Offset: 0x0001D24C
		protected MigrationMoveCsvSchema(int maximumRowCount, Dictionary<string, ProviderPropertyDefinition> requiredColumns, Dictionary<string, ProviderPropertyDefinition> optionalColumns, IEnumerable<string> prohibitedColumns) : base(maximumRowCount, requiredColumns, optionalColumns, prohibitedColumns)
		{
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0001F059 File Offset: 0x0001D259
		protected override MigrationBatchError ValidateClearTextPassword(string clearTextPassword, CsvRow row, ProviderPropertyDefinition propertyDefinition)
		{
			return null;
		}

		// Token: 0x040002DB RID: 731
		internal const string TargetDatabaseColumnName = "TargetDatabase";

		// Token: 0x040002DC RID: 732
		internal const string TargetArchiveDatabaseColumnName = "TargetArchiveDatabase";

		// Token: 0x040002DD RID: 733
		internal const string BadItemLimitColumnName = "BadItemLimit";

		// Token: 0x040002DE RID: 734
		internal const string LargeItemLimitColumnName = "LargeItemLimit";

		// Token: 0x040002DF RID: 735
		internal const string MailboxTypeColumnName = "MailboxType";

		// Token: 0x040002E0 RID: 736
		internal static readonly SimpleProviderPropertyDefinition BadItemLimit = new SimpleProviderPropertyDefinition("BadItemLimit", ExchangeObjectVersion.Exchange2012, typeof(Unlimited<int>), PropertyDefinitionFlags.None, Unlimited<int>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new MigrationMoveCsvSchema.CsvRangedUnlimitedConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None);

		// Token: 0x040002E1 RID: 737
		internal static readonly SimpleProviderPropertyDefinition LargeItemLimit = new SimpleProviderPropertyDefinition("LargeItemLimit", ExchangeObjectVersion.Exchange2012, typeof(Unlimited<int>), PropertyDefinitionFlags.None, Unlimited<int>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new MigrationMoveCsvSchema.CsvRangedUnlimitedConstraint<int>(0, int.MaxValue)
		}, PropertyDefinitionConstraint.None);

		// Token: 0x040002E2 RID: 738
		internal static readonly ProviderPropertyDefinition MailboxTypePropertyDefinition = new SimpleProviderPropertyDefinition("MailboxType", ExchangeObjectVersion.Exchange2012, typeof(MigrationMailboxType), PropertyDefinitionFlags.None, MigrationMailboxType.PrimaryAndArchive, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(MigrationMailboxType))
		}, PropertyDefinitionConstraint.None);

		// Token: 0x0200007B RID: 123
		private class CsvRangedUnlimitedConstraint<T> : RangedUnlimitedConstraint<T> where T : struct, IComparable
		{
			// Token: 0x060006D7 RID: 1751 RVA: 0x0001F135 File Offset: 0x0001D335
			public CsvRangedUnlimitedConstraint(T minValue, T maxValue) : base(minValue, maxValue)
			{
			}

			// Token: 0x060006D8 RID: 1752 RVA: 0x0001F140 File Offset: 0x0001D340
			public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
			{
				Unlimited<T> unlimited;
				if (value is string)
				{
					unlimited = Unlimited<T>.Parse((string)value);
				}
				else
				{
					unlimited = (Unlimited<T>)value;
				}
				return base.Validate(unlimited, propertyDefinition, propertyBag);
			}
		}
	}
}
