using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002B6 RID: 694
	internal sealed class CopyFolder : MoveCopyFolderCommandBase
	{
		// Token: 0x060012B1 RID: 4785 RVA: 0x0005B5B7 File Offset: 0x000597B7
		public CopyFolder(CallContext callContext, CopyFolderRequest request) : base(callContext, request)
		{
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x0005B5C1 File Offset: 0x000597C1
		protected override BaseInfoResponse CreateResponse()
		{
			return new CopyFolderResponse();
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x0005B5C8 File Offset: 0x000597C8
		protected override AggregateOperationResult DoOperation(StoreSession destinationSession, StoreSession sourceSession, StoreId sourceId)
		{
			return sourceSession.Copy(destinationSession, this.destinationFolder.Id, new StoreId[]
			{
				sourceId
			});
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x0005B5F3 File Offset: 0x000597F3
		protected override void SubclassValidateOperation(StoreSession storeSession, IdAndSession idAndSession)
		{
			if (idAndSession.Session is PublicFolderSession || this.destinationFolder.Session is PublicFolderSession)
			{
				throw new ServiceInvalidOperationException((CoreResources.IDs)4177991609U);
			}
		}
	}
}
