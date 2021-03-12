using System;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000779 RID: 1913
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SaveConflictException : StorageTransientException
	{
		// Token: 0x060048C5 RID: 18629 RVA: 0x0013178B File Offset: 0x0012F98B
		public SaveConflictException(LocalizedString message, ConflictResolutionResult conflictResolutionResult) : base(message)
		{
			this.conflictResolutionResult = conflictResolutionResult;
		}

		// Token: 0x060048C6 RID: 18630 RVA: 0x0013179B File Offset: 0x0012F99B
		public SaveConflictException(LocalizedString message, Exception inner) : base(message, inner)
		{
			this.conflictResolutionResult = SaveConflictException.mapiIrresolvableConflict;
		}

		// Token: 0x060048C7 RID: 18631 RVA: 0x001317B0 File Offset: 0x0012F9B0
		public SaveConflictException(LocalizedString message) : base(message)
		{
			this.conflictResolutionResult = SaveConflictException.mapiIrresolvableConflict;
		}

		// Token: 0x170014F9 RID: 5369
		// (get) Token: 0x060048C8 RID: 18632 RVA: 0x001317C4 File Offset: 0x0012F9C4
		public ConflictResolutionResult ConflictResolutionResult
		{
			get
			{
				return this.conflictResolutionResult;
			}
		}

		// Token: 0x170014FA RID: 5370
		// (get) Token: 0x060048C9 RID: 18633 RVA: 0x001317CC File Offset: 0x0012F9CC
		public override string Message
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(base.Message);
				if (this.conflictResolutionResult != null && this.conflictResolutionResult.PropertyConflicts != null)
				{
					stringBuilder.AppendLine("SaveStatus: " + this.conflictResolutionResult.SaveStatus.ToString());
					stringBuilder.AppendLine("PropertyConflicts:");
					foreach (PropertyConflict propertyConflict in this.conflictResolutionResult.PropertyConflicts)
					{
						stringBuilder.AppendLine(propertyConflict.ToString());
					}
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x04002771 RID: 10097
		private static readonly ConflictResolutionResult mapiIrresolvableConflict = new ConflictResolutionResult(SaveResult.IrresolvableConflict, Array<PropertyConflict>.Empty);

		// Token: 0x04002772 RID: 10098
		private readonly ConflictResolutionResult conflictResolutionResult;
	}
}
