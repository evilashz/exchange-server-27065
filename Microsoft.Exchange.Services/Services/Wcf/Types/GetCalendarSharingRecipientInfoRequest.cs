using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A29 RID: 2601
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetCalendarSharingRecipientInfoRequest
	{
		// Token: 0x1700106B RID: 4203
		// (get) Token: 0x0600495A RID: 18778 RVA: 0x00102637 File Offset: 0x00100837
		// (set) Token: 0x0600495B RID: 18779 RVA: 0x0010263F File Offset: 0x0010083F
		[DataMember]
		public string[] EmailAddresses { get; set; }

		// Token: 0x1700106C RID: 4204
		// (get) Token: 0x0600495C RID: 18780 RVA: 0x00102648 File Offset: 0x00100848
		// (set) Token: 0x0600495D RID: 18781 RVA: 0x00102650 File Offset: 0x00100850
		[DataMember]
		public ItemId CalendarId { get; set; }

		// Token: 0x1700106D RID: 4205
		// (get) Token: 0x0600495E RID: 18782 RVA: 0x00102659 File Offset: 0x00100859
		// (set) Token: 0x0600495F RID: 18783 RVA: 0x00102661 File Offset: 0x00100861
		internal StoreObjectId CalendarStoreId { get; private set; }

		// Token: 0x1700106E RID: 4206
		// (get) Token: 0x06004960 RID: 18784 RVA: 0x0010266A File Offset: 0x0010086A
		// (set) Token: 0x06004961 RID: 18785 RVA: 0x00102672 File Offset: 0x00100872
		internal List<KeyValuePair<SmtpAddress, ADRecipient>> Recipients { get; private set; }

		// Token: 0x06004962 RID: 18786 RVA: 0x00102698 File Offset: 0x00100898
		internal void ValidateRequest(ADRecipientSessionContext adRecipientSessionContext)
		{
			if (this.EmailAddresses == null || this.EmailAddresses.Length == 0)
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(), FaultParty.Sender);
			}
			List<SmtpAddress> list = new List<SmtpAddress>(this.EmailAddresses.Length);
			try
			{
				Array.ForEach<string>(this.EmailAddresses, delegate(string email)
				{
					list.Add(SmtpAddress.Parse(email));
				});
			}
			catch (FormatException innerException)
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(innerException), FaultParty.Sender);
			}
			if (this.CalendarId != null && this.CalendarId.Id != null)
			{
				this.CalendarStoreId = ServiceIdConverter.ConvertFromConcatenatedId(this.CalendarId.Id, BasicTypes.Folder, null).ToStoreObjectId();
			}
			this.PopulateADRecipients(list, adRecipientSessionContext);
		}

		// Token: 0x06004963 RID: 18787 RVA: 0x00102784 File Offset: 0x00100984
		private void PopulateADRecipients(List<SmtpAddress> smtpList, ADRecipientSessionContext adRecipientSessionContext)
		{
			IRecipientSession adrecipientSession = adRecipientSessionContext.GetADRecipientSession();
			List<ProxyAddress> paddresses = new List<ProxyAddress>();
			smtpList.ForEach(delegate(SmtpAddress smtp)
			{
				paddresses.Add(ProxyAddress.Parse(smtp.ToString()));
			});
			Result<ADRecipient>[] array = adrecipientSession.FindByProxyAddresses(paddresses.ToArray());
			List<KeyValuePair<SmtpAddress, ADRecipient>> list = new List<KeyValuePair<SmtpAddress, ADRecipient>>();
			for (int i = 0; i < array.Length; i++)
			{
				list.Add(new KeyValuePair<SmtpAddress, ADRecipient>(smtpList[i], array[i].Data));
			}
			this.Recipients = list;
		}
	}
}
