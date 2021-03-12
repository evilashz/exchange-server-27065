using System;
using System.Net;
using Microsoft.Exchange.Common.Cache;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000421 RID: 1057
	internal interface IInboundProxyLayer
	{
		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x060030E4 RID: 12516
		ulong SessionId { get; }

		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x060030E5 RID: 12517
		string NextHopFqdn { get; }

		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x060030E6 RID: 12518
		IInboundProxyDestinationTracker InboundProxyDestinationTracker { get; }

		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x060030E7 RID: 12519
		IPEndPoint ClientEndPoint { get; }

		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x060030E8 RID: 12520
		string ClientHelloDomain { get; }

		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x060030E9 RID: 12521
		IEhloOptions SmtpInEhloOptions { get; }

		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x060030EA RID: 12522
		long BytesRead { get; }

		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x060030EB RID: 12523
		long BytesWritten { get; }

		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x060030EC RID: 12524
		bool IsBdat { get; }

		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x060030ED RID: 12525
		long OutboundChunkSize { get; }

		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x060030EE RID: 12526
		bool IsLastChunk { get; }

		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x060030EF RID: 12527
		uint XProxyFromSeqNum { get; }

		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x060030F0 RID: 12528
		Permission Permissions { get; }

		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x060030F1 RID: 12529
		AuthenticationSource AuthenticationSource { get; }

		// Token: 0x060030F2 RID: 12530
		void AckMessage(AckStatus status, SmtpResponse response, string source, SessionSetupFailureReason failureReason);

		// Token: 0x060030F3 RID: 12531
		void AckMessage(AckStatus status, SmtpResponse response, bool replaceFailureResponse, string source, SessionSetupFailureReason failureReason);

		// Token: 0x060030F4 RID: 12532
		void AckConnection(AckStatus status, SmtpResponse response, SessionSetupFailureReason failureReason);

		// Token: 0x060030F5 RID: 12533
		void AckCommandSuccessful();

		// Token: 0x060030F6 RID: 12534
		void BeginReadData(InboundProxyLayer.ReadCompletionCallback readCompleteCallback);

		// Token: 0x060030F7 RID: 12535
		void WaitForNewCommand(InboundBdatProxyLayer.CommandReceivedCallback commandReceivedCallback);

		// Token: 0x060030F8 RID: 12536
		void Shutdown();

		// Token: 0x060030F9 RID: 12537
		void ReleaseMailItem();

		// Token: 0x060030FA RID: 12538
		void ReturnBuffer(BufferCacheEntry bufferHolder);
	}
}
