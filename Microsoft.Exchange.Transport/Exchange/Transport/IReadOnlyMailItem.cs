using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.SecureMail;
using Microsoft.Exchange.Transport.Configuration;
using Microsoft.Exchange.Transport.Storage;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200016B RID: 363
	internal interface IReadOnlyMailItem : ISystemProbeTraceable
	{
		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000FC1 RID: 4033
		ADRecipientCache<TransportMiniRecipient> ADRecipientCache { get; }

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000FC2 RID: 4034
		string Auth { get; }

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000FC3 RID: 4035
		MultilevelAuthMechanism AuthMethod { get; }

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000FC4 RID: 4036
		BodyType BodyType { get; }

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000FC5 RID: 4037
		DateTime DateReceived { get; }

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000FC6 RID: 4038
		DeferReason DeferReason { get; }

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000FC7 RID: 4039
		MailDirectionality Directionality { get; }

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000FC8 RID: 4040
		DsnFormat DsnFormat { get; }

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000FC9 RID: 4041
		DsnParameters DsnParameters { get; }

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000FCA RID: 4042
		// (set) Token: 0x06000FCB RID: 4043
		bool SuppressBodyInDsn { get; set; }

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000FCC RID: 4044
		string EnvId { get; }

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000FCD RID: 4045
		IReadOnlyExtendedPropertyCollection ExtendedProperties { get; }

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000FCE RID: 4046
		TimeSpan ExtensionToExpiryDuration { get; }

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000FCF RID: 4047
		RoutingAddress From { get; }

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000FD0 RID: 4048
		string HeloDomain { get; }

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000FD1 RID: 4049
		string InternetMessageId { get; }

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000FD2 RID: 4050
		Guid NetworkMessageId { get; }

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000FD3 RID: 4051
		bool IsActive { get; }

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000FD4 RID: 4052
		bool IsHeartbeat { get; }

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000FD5 RID: 4053
		LatencyTracker LatencyTracker { get; }

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000FD6 RID: 4054
		byte[] LegacyXexch50Blob { get; }

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000FD7 RID: 4055
		IEnumerable<string> LockReasonHistory { get; }

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000FD8 RID: 4056
		EmailMessage Message { get; }

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000FD9 RID: 4057
		string Oorg { get; }

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000FDA RID: 4058
		Guid ExternalOrganizationId { get; }

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000FDB RID: 4059
		string ExoAccountForest { get; }

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000FDC RID: 4060
		string ExoTenantContainer { get; }

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000FDD RID: 4061
		bool IsProbe { get; }

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000FDE RID: 4062
		string ProbeName { get; }

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000FDF RID: 4063
		bool PersistProbeTrace { get; }

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000FE0 RID: 4064
		RiskLevel RiskLevel { get; }

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000FE1 RID: 4065
		DeliveryPriority Priority { get; }

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000FE2 RID: 4066
		string PrioritizationReason { get; }

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000FE3 RID: 4067
		MimeDocument MimeDocument { get; }

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000FE4 RID: 4068
		string MimeFrom { get; }

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000FE5 RID: 4069
		RoutingAddress MimeSender { get; }

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000FE6 RID: 4070
		long MimeSize { get; }

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000FE7 RID: 4071
		OrganizationId OrganizationId { get; }

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000FE8 RID: 4072
		RoutingAddress OriginalFrom { get; }

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000FE9 RID: 4073
		int PoisonCount { get; }

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000FEA RID: 4074
		int PoisonForRemoteCount { get; }

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000FEB RID: 4075
		bool IsPoison { get; }

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000FEC RID: 4076
		string ReceiveConnectorName { get; }

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000FED RID: 4077
		IReadOnlyMailRecipientCollection Recipients { get; }

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000FEE RID: 4078
		long RecordId { get; }

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000FEF RID: 4079
		bool RetryDeliveryIfRejected { get; }

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000FF0 RID: 4080
		MimePart RootPart { get; }

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000FF1 RID: 4081
		int Scl { get; }

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000FF2 RID: 4082
		Guid ShadowMessageId { get; }

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000FF3 RID: 4083
		string ShadowServerContext { get; }

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000FF4 RID: 4084
		string ShadowServerDiscardId { get; }

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000FF5 RID: 4085
		IPAddress SourceIPAddress { get; }

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000FF6 RID: 4086
		string Subject { get; }

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000FF7 RID: 4087
		PerTenantTransportSettings TransportSettings { get; }

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000FF8 RID: 4088
		LazyBytes FastIndexBlob { get; }

		// Token: 0x06000FF9 RID: 4089
		void CacheTransportSettings();

		// Token: 0x06000FFA RID: 4090
		bool IsJournalReport();

		// Token: 0x06000FFB RID: 4091
		bool IsPfReplica();

		// Token: 0x06000FFC RID: 4092
		bool IsShadowed();

		// Token: 0x06000FFD RID: 4093
		bool IsDelayedAck();

		// Token: 0x06000FFE RID: 4094
		TransportMailItem NewCloneWithoutRecipients(bool shareRecipientCache);

		// Token: 0x06000FFF RID: 4095
		Stream OpenMimeReadStream();

		// Token: 0x06001000 RID: 4096
		Stream OpenMimeReadStream(bool downConvert);

		// Token: 0x06001001 RID: 4097
		void TrackSuccessfulConnectLatency(LatencyComponent connectComponent);

		// Token: 0x06001002 RID: 4098
		void AddDsnParameters(string key, object value);

		// Token: 0x06001003 RID: 4099
		void IncrementPoisonForRemoteCount();
	}
}
