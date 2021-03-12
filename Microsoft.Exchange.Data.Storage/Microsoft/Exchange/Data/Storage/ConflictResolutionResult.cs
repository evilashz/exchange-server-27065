using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AE0 RID: 2784
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConflictResolutionResult
	{
		// Token: 0x06006501 RID: 25857 RVA: 0x001ACB22 File Offset: 0x001AAD22
		public ConflictResolutionResult(SaveResult saveResult, PropertyConflict[] propertyConflicts)
		{
			EnumValidator.ThrowIfInvalid<SaveResult>(saveResult, "saveResult");
			this.SaveStatus = saveResult;
			this.PropertyConflicts = propertyConflicts;
		}

		// Token: 0x06006502 RID: 25858 RVA: 0x001ACB44 File Offset: 0x001AAD44
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.SaveStatus);
			if (this.PropertyConflicts != null)
			{
				stringBuilder.Append(" (");
				foreach (PropertyConflict value in this.PropertyConflicts)
				{
					stringBuilder.Append(value);
				}
				stringBuilder.Append(")");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400398A RID: 14730
		public readonly SaveResult SaveStatus;

		// Token: 0x0400398B RID: 14731
		public readonly PropertyConflict[] PropertyConflicts;

		// Token: 0x0400398C RID: 14732
		internal static readonly ConflictResolutionResult Success = new ConflictResolutionResult(SaveResult.Success, null);

		// Token: 0x0400398D RID: 14733
		internal static readonly ConflictResolutionResult Failure = new ConflictResolutionResult(SaveResult.IrresolvableConflict, null);

		// Token: 0x0400398E RID: 14734
		internal static readonly ConflictResolutionResult SuccessWithoutSaving = new ConflictResolutionResult(SaveResult.SuccessWithoutSaving, null);
	}
}
