using System;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Mdb;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Exchange.Search.TokenOperators;

namespace Microsoft.Exchange.Search.MailboxOperators
{
	// Token: 0x02000009 RID: 9
	internal class ErrorBypassProducer : ErrorBypassProducerBase
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00002649 File Offset: 0x00000849
		public ErrorBypassProducer(ErrorBypassOperator errorBypassOperator, IRecordSetTypeDescriptor inputType, IEvaluationContext context) : base(errorBypassOperator, inputType, context)
		{
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002654 File Offset: 0x00000854
		protected override string GetFolderId(string identity)
		{
			MdbItemIdentity mdbItemIdentity = MdbItemIdentity.Parse(identity);
			byte[] parentEntryIdFromMessageEntryId = IdConverter.GetParentEntryIdFromMessageEntryId(mdbItemIdentity.ItemId.ProviderLevelItemId);
			return FolderIdHelper.GetIndexForFolderEntryId(parentEntryIdFromMessageEntryId);
		}
	}
}
