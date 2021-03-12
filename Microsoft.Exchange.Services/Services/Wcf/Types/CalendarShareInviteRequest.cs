using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A12 RID: 2578
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CalendarShareInviteRequest
	{
		// Token: 0x17001034 RID: 4148
		// (get) Token: 0x060048B6 RID: 18614 RVA: 0x00101B27 File Offset: 0x000FFD27
		// (set) Token: 0x060048B7 RID: 18615 RVA: 0x00101B2F File Offset: 0x000FFD2F
		[DataMember]
		public ItemId CalendarId { get; set; }

		// Token: 0x17001035 RID: 4149
		// (get) Token: 0x060048B8 RID: 18616 RVA: 0x00101B38 File Offset: 0x000FFD38
		// (set) Token: 0x060048B9 RID: 18617 RVA: 0x00101B40 File Offset: 0x000FFD40
		[DataMember]
		public string Subject { get; set; }

		// Token: 0x17001036 RID: 4150
		// (get) Token: 0x060048BA RID: 18618 RVA: 0x00101B49 File Offset: 0x000FFD49
		// (set) Token: 0x060048BB RID: 18619 RVA: 0x00101B51 File Offset: 0x000FFD51
		[DataMember]
		public BodyContentType Body { get; set; }

		// Token: 0x17001037 RID: 4151
		// (get) Token: 0x060048BC RID: 18620 RVA: 0x00101B5A File Offset: 0x000FFD5A
		// (set) Token: 0x060048BD RID: 18621 RVA: 0x00101B62 File Offset: 0x000FFD62
		[DataMember]
		public CalendarSharingRecipient[] SharingRecipients { get; set; }

		// Token: 0x17001038 RID: 4152
		// (get) Token: 0x060048BE RID: 18622 RVA: 0x00101B6B File Offset: 0x000FFD6B
		// (set) Token: 0x060048BF RID: 18623 RVA: 0x00101B73 File Offset: 0x000FFD73
		internal StoreObjectId CalendarStoreId { get; private set; }

		// Token: 0x17001039 RID: 4153
		// (get) Token: 0x060048C0 RID: 18624 RVA: 0x00101B7C File Offset: 0x000FFD7C
		// (set) Token: 0x060048C1 RID: 18625 RVA: 0x00101B84 File Offset: 0x000FFD84
		internal string CalendarName { get; private set; }

		// Token: 0x060048C2 RID: 18626 RVA: 0x00101B98 File Offset: 0x000FFD98
		internal void ValidateRequest(MailboxSession session, ADRecipientSessionContext adRecipientSessionContext)
		{
			if (this.CalendarId == null || string.IsNullOrEmpty(this.CalendarId.Id) || this.Body == null || this.Subject == null || this.SharingRecipients == null || this.SharingRecipients.Length == 0 || this.SharingRecipients.Length > 50)
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(), FaultParty.Sender);
			}
			Array.ForEach<CalendarSharingRecipient>(this.SharingRecipients, delegate(CalendarSharingRecipient action)
			{
				action.ValidateRequest();
			});
			this.CalendarStoreId = ServiceIdConverter.ConvertFromConcatenatedId(this.CalendarId.Id, BasicTypes.Folder, null).ToStoreObjectId();
			using (CalendarFolder calendarFolder = CalendarFolder.Bind(session, this.CalendarStoreId))
			{
				this.CalendarStoreId = calendarFolder.StoreObjectId;
				this.CalendarName = calendarFolder.DisplayName;
			}
			this.PopulateADRecipients(adRecipientSessionContext);
		}

		// Token: 0x060048C3 RID: 18627 RVA: 0x00101CB0 File Offset: 0x000FFEB0
		private void PopulateADRecipients(ADRecipientSessionContext adRecipientSessionContext)
		{
			IRecipientSession adrecipientSession = adRecipientSessionContext.GetADRecipientSession();
			List<ProxyAddress> paddresses = new List<ProxyAddress>(this.SharingRecipients.Length);
			Array.ForEach<CalendarSharingRecipient>(this.SharingRecipients, delegate(CalendarSharingRecipient rep)
			{
				paddresses.Add(ProxyAddress.Parse(rep.EmailAddress.EmailAddress));
			});
			Result<ADRecipient>[] array = adrecipientSession.FindByProxyAddresses(paddresses.ToArray());
			for (int i = 0; i < array.Length; i++)
			{
				this.SharingRecipients[i].ADRecipient = array[i].Data;
			}
		}

		// Token: 0x040029B4 RID: 10676
		private const int MaxNumberOfRecipientsAllowed = 50;
	}
}
