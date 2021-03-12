using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A08 RID: 2568
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AddSharedCalendarRequest
	{
		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x06004880 RID: 18560 RVA: 0x001018D1 File Offset: 0x000FFAD1
		// (set) Token: 0x06004881 RID: 18561 RVA: 0x001018D9 File Offset: 0x000FFAD9
		[DataMember]
		public ItemId MessageId { get; set; }

		// Token: 0x17001021 RID: 4129
		// (get) Token: 0x06004882 RID: 18562 RVA: 0x001018E2 File Offset: 0x000FFAE2
		// (set) Token: 0x06004883 RID: 18563 RVA: 0x001018EA File Offset: 0x000FFAEA
		internal StoreObjectId MessageStoreId { get; private set; }

		// Token: 0x06004884 RID: 18564 RVA: 0x001018F4 File Offset: 0x000FFAF4
		internal void ValidateRequest()
		{
			if (this.MessageId == null || string.IsNullOrEmpty(this.MessageId.Id))
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(), FaultParty.Sender);
			}
			this.MessageStoreId = ServiceIdConverter.ConvertFromConcatenatedId(this.MessageId.Id, BasicTypes.Item, null).ToStoreObjectId();
		}
	}
}
