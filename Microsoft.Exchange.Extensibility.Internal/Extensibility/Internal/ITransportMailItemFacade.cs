using System;
using System.Net;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;

namespace Microsoft.Exchange.Extensibility.Internal
{
	// Token: 0x0200004A RID: 74
	internal interface ITransportMailItemFacade
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002D2 RID: 722
		long RecordId { get; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002D3 RID: 723
		IMailRecipientCollectionFacade Recipients { get; }

		// Token: 0x170000A1 RID: 161
		// (set) Token: 0x060002D4 RID: 724
		RoutingAddress From { set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002D5 RID: 725
		RoutingAddress OriginalFrom { get; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002D6 RID: 726
		EmailMessage Message { get; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002D7 RID: 727
		// (set) Token: 0x060002D8 RID: 728
		DeliveryPriority Priority { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002D9 RID: 729
		// (set) Token: 0x060002DA RID: 730
		string PrioritizationReason { get; set; }

		// Token: 0x170000A6 RID: 166
		// (set) Token: 0x060002DB RID: 731
		MimeDocument MimeDocument { set; }

		// Token: 0x060002DC RID: 732
		bool IsJournalReport();

		// Token: 0x060002DD RID: 733
		void PrepareJournalReport();

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002DE RID: 734
		// (set) Token: 0x060002DF RID: 735
		string ReceiveConnectorName { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002E0 RID: 736
		bool PipelineTracingEnabled { get; }

		// Token: 0x060002E1 RID: 737
		void CommitLazy();

		// Token: 0x060002E2 RID: 738
		IAsyncResult BeginCommitForReceive(AsyncCallback callback, object context);

		// Token: 0x060002E3 RID: 739
		bool EndCommitForReceive(IAsyncResult ar, out Exception exception);

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002E4 RID: 740
		object ADRecipientCacheAsObject { get; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002E5 RID: 741
		object OrganizationIdAsObject { get; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002E6 RID: 742
		Guid ExternalOrganizationId { get; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002E7 RID: 743
		ITransportSettingsFacade TransportSettings { get; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002E8 RID: 744
		bool IsOriginating { get; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002E9 RID: 745
		MailDirectionality Directionality { get; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002EA RID: 746
		bool IsProbe { get; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002EB RID: 747
		// (set) Token: 0x060002EC RID: 748
		bool FallbackToRawOverride { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002ED RID: 749
		// (set) Token: 0x060002EE RID: 750
		IPAddress SourceIPAddress { get; set; }
	}
}
