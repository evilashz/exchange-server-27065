using System;
using System.Text;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000095 RID: 149
	internal class ACRTraceString
	{
		// Token: 0x06000515 RID: 1301 RVA: 0x0001A8A4 File Offset: 0x00018AA4
		public ACRTraceString(ConflictResolutionResult saveResults)
		{
			if (saveResults == null)
			{
				throw new ArgumentNullException("saveResults");
			}
			this.saveResults = saveResults;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0001A8C4 File Offset: 0x00018AC4
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
					stringBuilder.AppendFormat("\tResolved value: {0}\n", propertyConflict.ResolvedValue);
				}
			}
			else
			{
				stringBuilder.Append("Zero properties in conflict");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040002C6 RID: 710
		private ConflictResolutionResult saveResults;
	}
}
