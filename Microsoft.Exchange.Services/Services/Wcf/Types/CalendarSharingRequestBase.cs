using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A16 RID: 2582
	[DataContract]
	public class CalendarSharingRequestBase
	{
		// Token: 0x17001044 RID: 4164
		// (get) Token: 0x060048DE RID: 18654 RVA: 0x00101ED9 File Offset: 0x001000D9
		// (set) Token: 0x060048DF RID: 18655 RVA: 0x00101EE1 File Offset: 0x001000E1
		[DataMember]
		public FolderId CalendarId { get; set; }

		// Token: 0x17001045 RID: 4165
		// (get) Token: 0x060048E0 RID: 18656 RVA: 0x00101EEA File Offset: 0x001000EA
		// (set) Token: 0x060048E1 RID: 18657 RVA: 0x00101EF2 File Offset: 0x001000F2
		internal StoreObjectId CalendarStoreId { get; private set; }

		// Token: 0x060048E2 RID: 18658 RVA: 0x00101EFC File Offset: 0x001000FC
		internal virtual void ValidateRequest()
		{
			if (this.CalendarId == null)
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(), FaultParty.Sender);
			}
			IdHeaderInformation idHeaderInformation = ServiceIdConverter.ConvertFromConcatenatedId(this.CalendarId.Id, BasicTypes.Folder, null);
			this.CalendarStoreId = StoreObjectId.FromProviderSpecificId(idHeaderInformation.StoreIdBytes, StoreObjectType.CalendarFolder);
		}
	}
}
