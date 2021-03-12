using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000159 RID: 345
	internal interface IDsnGeneratorComponent : ITransportComponent
	{
		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000EEC RID: 3820
		// (set) Token: 0x06000EED RID: 3821
		DsnMailOutHandlerDelegate DsnMailOutHandler { get; set; }

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000EEE RID: 3822
		DsnHumanReadableWriter DsnHumanReadableWriter { get; }

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000EEF RID: 3823
		QuarantineConfig QuarantineConfig { get; }

		// Token: 0x06000EF0 RID: 3824
		void GenerateDSNs(IReadOnlyMailItem mailItem);

		// Token: 0x06000EF1 RID: 3825
		void GenerateDSNs(IReadOnlyMailItem mailItem, DsnGenerator.CallerComponent callerComponent);

		// Token: 0x06000EF2 RID: 3826
		void GenerateDSNs(IReadOnlyMailItem mailItem, string remoteServer, DsnGenerator.CallerComponent callerComponent);

		// Token: 0x06000EF3 RID: 3827
		void GenerateDSNs(IReadOnlyMailItem mailItem, IEnumerable<MailRecipient> recipientList);

		// Token: 0x06000EF4 RID: 3828
		void GenerateDSNs(IReadOnlyMailItem mailItem, IEnumerable<MailRecipient> recipientList, string remoteServer);

		// Token: 0x06000EF5 RID: 3829
		void GenerateDSNs(IReadOnlyMailItem mailItem, IEnumerable<MailRecipient> recipientList, LastError lastQueueLevelError);

		// Token: 0x06000EF6 RID: 3830
		void GenerateDSNs(IReadOnlyMailItem mailItem, IEnumerable<MailRecipient> recipientList, string remoteServer, DsnGenerator.CallerComponent callerComponent, LastError lastQueueLevelError);

		// Token: 0x06000EF7 RID: 3831
		void GenerateNDRForInvalidAddresses(bool switchPoisonContext, IReadOnlyMailItem mailItem, List<DsnRecipientInfo> recipientList);

		// Token: 0x06000EF8 RID: 3832
		void GenerateNDRForInvalidAddresses(bool switchPoisonContext, IReadOnlyMailItem mailItem, List<DsnRecipientInfo> recipientList, DsnMailOutHandlerDelegate dsnMailOutHandler);

		// Token: 0x06000EF9 RID: 3833
		void DecorateStoreNdr(TransportMailItem transportMessage, RoutingAddress ndrRecipient);

		// Token: 0x06000EFA RID: 3834
		void MonitorJobs();

		// Token: 0x06000EFB RID: 3835
		void DecorateMfn(TransportMailItem transportMessage, string displayName, string emailAddress);

		// Token: 0x06000EFC RID: 3836
		void CreateStoreQuotaMessageBody(TransportMailItem transportMailItem, QuotaType quotaType, string quotaMessageClass, int? localeId, int currentSize, int? maxSize, string folderName, bool isPrimaryMailbox, bool isPublicFolderMailbox);
	}
}
