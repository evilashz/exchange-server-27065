using System;
using System.Text;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.InfoWorker.Common
{
	// Token: 0x0200000C RID: 12
	internal sealed class ACRTraceString
	{
		// Token: 0x06000025 RID: 37 RVA: 0x0000271E File Offset: 0x0000091E
		public ACRTraceString(ConflictResolutionResult saveResults)
		{
			if (saveResults == null)
			{
				throw new ArgumentNullException("saveResults");
			}
			this.saveResults = saveResults;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000273C File Offset: 0x0000093C
		public override string ToString()
		{
			if (this.saveResults == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder("Save results: ");
			stringBuilder.AppendLine(this.saveResults.SaveStatus.ToString());
			if (this.saveResults.PropertyConflicts != null && this.saveResults.PropertyConflicts.Length > 0)
			{
				for (int i = 0; i < this.saveResults.PropertyConflicts.Length; i++)
				{
					PropertyConflict propertyConflict = this.saveResults.PropertyConflicts[i];
					stringBuilder.AppendFormat("Resolvable: {0} Property: {1}\n", propertyConflict.ConflictResolvable, propertyConflict.PropertyDefinition);
					stringBuilder.AppendFormat("\tOriginal value: {0}\n", propertyConflict.OriginalValue);
					stringBuilder.AppendFormat("\tClient value: {0}\n", propertyConflict.ClientValue);
					stringBuilder.AppendFormat("\tServer value: {0}\n", propertyConflict.ServerValue);
					stringBuilder.AppendFormat("\tResolved value: {0}\n", propertyConflict.ResolvedValue);
				}
			}
			else
			{
				stringBuilder.Append("Zero properties in conflict");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400002B RID: 43
		private ConflictResolutionResult saveResults;
	}
}
