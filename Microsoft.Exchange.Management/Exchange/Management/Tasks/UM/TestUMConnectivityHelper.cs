using System;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.UM.Rpc;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D4E RID: 3406
	public class TestUMConnectivityHelper
	{
		// Token: 0x02000D4F RID: 3407
		[Serializable]
		public abstract class UMConnectivityResults : UMDiagnosticObject
		{
			// Token: 0x1700289D RID: 10397
			// (get) Token: 0x060082BE RID: 33470 RVA: 0x0021776B File Offset: 0x0021596B
			// (set) Token: 0x060082BF RID: 33471 RVA: 0x00217773 File Offset: 0x00215973
			public string UmIPAddress
			{
				get
				{
					return this.umIPAddress;
				}
				set
				{
					this.umIPAddress = value;
				}
			}

			// Token: 0x1700289E RID: 10398
			// (get) Token: 0x060082C0 RID: 33472 RVA: 0x0021777C File Offset: 0x0021597C
			// (set) Token: 0x060082C1 RID: 33473 RVA: 0x00217784 File Offset: 0x00215984
			public bool EntireOperationSuccess
			{
				get
				{
					return this.entireOperationSuccess;
				}
				set
				{
					this.entireOperationSuccess = value;
				}
			}

			// Token: 0x1700289F RID: 10399
			// (get) Token: 0x060082C2 RID: 33474 RVA: 0x0021778D File Offset: 0x0021598D
			// (set) Token: 0x060082C3 RID: 33475 RVA: 0x00217795 File Offset: 0x00215995
			public LocalizedString ReasonForFailure
			{
				get
				{
					return this.reasonForFailure;
				}
				set
				{
					this.reasonForFailure = value;
				}
			}

			// Token: 0x170028A0 RID: 10400
			// (get) Token: 0x060082C4 RID: 33476 RVA: 0x002177A0 File Offset: 0x002159A0
			public override ObjectId Identity
			{
				get
				{
					return new ConfigObjectId(Guid.NewGuid().ToString());
				}
			}

			// Token: 0x060082C5 RID: 33477 RVA: 0x002177C5 File Offset: 0x002159C5
			protected UMConnectivityResults()
			{
				this.UmIPAddress = string.Empty;
				this.ReasonForFailure = LocalizedString.Empty;
				this.EntireOperationSuccess = false;
			}

			// Token: 0x04003F82 RID: 16258
			private string umIPAddress;

			// Token: 0x04003F83 RID: 16259
			private LocalizedString reasonForFailure;

			// Token: 0x04003F84 RID: 16260
			private bool entireOperationSuccess;
		}

		// Token: 0x02000D50 RID: 3408
		[Serializable]
		public class LocalUMConnectivityOptionsResults : TestUMConnectivityHelper.UMConnectivityResults
		{
			// Token: 0x170028A1 RID: 10401
			// (get) Token: 0x060082C6 RID: 33478 RVA: 0x002177EA File Offset: 0x002159EA
			// (set) Token: 0x060082C7 RID: 33479 RVA: 0x002177F2 File Offset: 0x002159F2
			public string Diagnostics
			{
				get
				{
					return this.diagnostics;
				}
				set
				{
					this.diagnostics = value;
				}
			}

			// Token: 0x04003F85 RID: 16261
			private string diagnostics = string.Empty;
		}

		// Token: 0x02000D51 RID: 3409
		[Serializable]
		public class UMConnectivityCallResults : TestUMConnectivityHelper.UMConnectivityResults
		{
			// Token: 0x060082C9 RID: 33481 RVA: 0x0021780E File Offset: 0x00215A0E
			public UMConnectivityCallResults()
			{
				this.currCalls = "0";
			}

			// Token: 0x170028A2 RID: 10402
			// (get) Token: 0x060082CA RID: 33482 RVA: 0x00217821 File Offset: 0x00215A21
			// (set) Token: 0x060082CB RID: 33483 RVA: 0x00217829 File Offset: 0x00215A29
			public string CurrCalls
			{
				get
				{
					return this.currCalls;
				}
				set
				{
					this.currCalls = value;
				}
			}

			// Token: 0x170028A3 RID: 10403
			// (get) Token: 0x060082CC RID: 33484 RVA: 0x00217832 File Offset: 0x00215A32
			// (set) Token: 0x060082CD RID: 33485 RVA: 0x0021783A File Offset: 0x00215A3A
			public double Latency
			{
				get
				{
					return this.latency;
				}
				set
				{
					this.latency = value;
				}
			}

			// Token: 0x170028A4 RID: 10404
			// (get) Token: 0x060082CE RID: 33486 RVA: 0x00217843 File Offset: 0x00215A43
			// (set) Token: 0x060082CF RID: 33487 RVA: 0x0021784B File Offset: 0x00215A4B
			public bool OutBoundSIPCallSuccess
			{
				get
				{
					return this.outBoundSIPCallSuccess;
				}
				set
				{
					this.outBoundSIPCallSuccess = value;
				}
			}

			// Token: 0x04003F86 RID: 16262
			private string currCalls;

			// Token: 0x04003F87 RID: 16263
			private double latency;

			// Token: 0x04003F88 RID: 16264
			private bool outBoundSIPCallSuccess;
		}

		// Token: 0x02000D52 RID: 3410
		[Serializable]
		public class LocalUMConnectivityResults : TestUMConnectivityHelper.UMConnectivityCallResults
		{
			// Token: 0x060082D0 RID: 33488 RVA: 0x00217854 File Offset: 0x00215A54
			public LocalUMConnectivityResults()
			{
				this.totalQueuedMessages = string.Empty;
				this.umserverAcceptingCallAnsweringMessages = true;
			}

			// Token: 0x170028A5 RID: 10405
			// (get) Token: 0x060082D1 RID: 33489 RVA: 0x0021786E File Offset: 0x00215A6E
			// (set) Token: 0x060082D2 RID: 33490 RVA: 0x00217876 File Offset: 0x00215A76
			public bool UmserverAcceptingCallAnsweringMessages
			{
				get
				{
					return this.umserverAcceptingCallAnsweringMessages;
				}
				set
				{
					this.umserverAcceptingCallAnsweringMessages = value;
				}
			}

			// Token: 0x170028A6 RID: 10406
			// (get) Token: 0x060082D3 RID: 33491 RVA: 0x0021787F File Offset: 0x00215A7F
			// (set) Token: 0x060082D4 RID: 33492 RVA: 0x00217887 File Offset: 0x00215A87
			public string TotalQueuedMessages
			{
				get
				{
					return this.totalQueuedMessages;
				}
				set
				{
					this.totalQueuedMessages = value;
				}
			}

			// Token: 0x04003F89 RID: 16265
			private string totalQueuedMessages;

			// Token: 0x04003F8A RID: 16266
			private bool umserverAcceptingCallAnsweringMessages;
		}

		// Token: 0x02000D53 RID: 3411
		[Serializable]
		public class RemoteUMConnectivityResults : TestUMConnectivityHelper.UMConnectivityCallResults
		{
			// Token: 0x060082D5 RID: 33493 RVA: 0x00217890 File Offset: 0x00215A90
			public RemoteUMConnectivityResults()
			{
				this.expectedDiagnosticSequence = string.Empty;
				this.receivedDiagnosticSequence = string.Empty;
			}

			// Token: 0x060082D6 RID: 33494 RVA: 0x002178B0 File Offset: 0x00215AB0
			public RemoteUMConnectivityResults(TestUMConnectivityHelper.UMConnectivityResults results) : this()
			{
				TestUMConnectivityHelper.UMConnectivityCallResults umconnectivityCallResults = (TestUMConnectivityHelper.UMConnectivityCallResults)results;
				base.CurrCalls = umconnectivityCallResults.CurrCalls;
				base.Latency = umconnectivityCallResults.Latency;
				base.UmIPAddress = umconnectivityCallResults.UmIPAddress;
				base.OutBoundSIPCallSuccess = umconnectivityCallResults.OutBoundSIPCallSuccess;
				base.EntireOperationSuccess = umconnectivityCallResults.EntireOperationSuccess;
				base.ReasonForFailure = umconnectivityCallResults.ReasonForFailure;
			}

			// Token: 0x170028A7 RID: 10407
			// (get) Token: 0x060082D7 RID: 33495 RVA: 0x00217912 File Offset: 0x00215B12
			// (set) Token: 0x060082D8 RID: 33496 RVA: 0x0021791A File Offset: 0x00215B1A
			public string ExpectedDigits
			{
				get
				{
					return this.expectedDiagnosticSequence;
				}
				set
				{
					this.expectedDiagnosticSequence = value;
				}
			}

			// Token: 0x170028A8 RID: 10408
			// (get) Token: 0x060082D9 RID: 33497 RVA: 0x00217923 File Offset: 0x00215B23
			// (set) Token: 0x060082DA RID: 33498 RVA: 0x0021792B File Offset: 0x00215B2B
			public string ReceivedDigits
			{
				get
				{
					return this.receivedDiagnosticSequence;
				}
				set
				{
					this.receivedDiagnosticSequence = value;
				}
			}

			// Token: 0x04003F8B RID: 16267
			private string expectedDiagnosticSequence;

			// Token: 0x04003F8C RID: 16268
			private string receivedDiagnosticSequence;
		}

		// Token: 0x02000D54 RID: 3412
		[Serializable]
		public class TUILogonEnumerateResults : TestUMConnectivityHelper.LocalUMConnectivityResults
		{
			// Token: 0x060082DB RID: 33499 RVA: 0x00217934 File Offset: 0x00215B34
			public TUILogonEnumerateResults()
			{
				this.mailboxServer = string.Empty;
				this.gotPin = true;
			}

			// Token: 0x170028A9 RID: 10409
			// (get) Token: 0x060082DC RID: 33500 RVA: 0x0021794E File Offset: 0x00215B4E
			// (set) Token: 0x060082DD RID: 33501 RVA: 0x00217956 File Offset: 0x00215B56
			public string MailboxServerBeingTested
			{
				get
				{
					return this.mailboxServer;
				}
				set
				{
					this.mailboxServer = value;
				}
			}

			// Token: 0x170028AA RID: 10410
			// (get) Token: 0x060082DE RID: 33502 RVA: 0x0021795F File Offset: 0x00215B5F
			// (set) Token: 0x060082DF RID: 33503 RVA: 0x00217967 File Offset: 0x00215B67
			public bool SuccessfullyRetrievedPINForValidUMUser
			{
				get
				{
					return this.gotPin;
				}
				set
				{
					this.gotPin = value;
				}
			}

			// Token: 0x04003F8D RID: 16269
			private string mailboxServer;

			// Token: 0x04003F8E RID: 16270
			private bool gotPin;
		}

		// Token: 0x02000D55 RID: 3413
		[Serializable]
		public class ResetPinResults : UMDiagnosticObject
		{
			// Token: 0x060082E0 RID: 33504 RVA: 0x00217970 File Offset: 0x00215B70
			public ResetPinResults()
			{
				this.mailboxServer = string.Empty;
			}

			// Token: 0x170028AB RID: 10411
			// (get) Token: 0x060082E1 RID: 33505 RVA: 0x00217983 File Offset: 0x00215B83
			// (set) Token: 0x060082E2 RID: 33506 RVA: 0x0021798B File Offset: 0x00215B8B
			public string MailboxServerBeingTested
			{
				get
				{
					return this.mailboxServer;
				}
				set
				{
					this.mailboxServer = value;
				}
			}

			// Token: 0x170028AC RID: 10412
			// (get) Token: 0x060082E3 RID: 33507 RVA: 0x00217994 File Offset: 0x00215B94
			// (set) Token: 0x060082E4 RID: 33508 RVA: 0x0021799C File Offset: 0x00215B9C
			public double Latency
			{
				get
				{
					return this.latency;
				}
				set
				{
					this.latency = value;
				}
			}

			// Token: 0x170028AD RID: 10413
			// (get) Token: 0x060082E5 RID: 33509 RVA: 0x002179A5 File Offset: 0x00215BA5
			// (set) Token: 0x060082E6 RID: 33510 RVA: 0x002179AD File Offset: 0x00215BAD
			public bool SuccessfullyResetPINForValidUMUser
			{
				get
				{
					return this.resetPinSuccess;
				}
				set
				{
					this.resetPinSuccess = value;
				}
			}

			// Token: 0x170028AE RID: 10414
			// (get) Token: 0x060082E7 RID: 33511 RVA: 0x002179B6 File Offset: 0x00215BB6
			// (set) Token: 0x060082E8 RID: 33512 RVA: 0x002179BE File Offset: 0x00215BBE
			public bool SuccessfullyResetPasswordForValidUMUser
			{
				get
				{
					return this.resetPasswdSuccess;
				}
				set
				{
					this.resetPasswdSuccess = value;
				}
			}

			// Token: 0x170028AF RID: 10415
			// (get) Token: 0x060082E9 RID: 33513 RVA: 0x002179C8 File Offset: 0x00215BC8
			public override ObjectId Identity
			{
				get
				{
					return new ConfigObjectId(Guid.NewGuid().ToString());
				}
			}

			// Token: 0x04003F8F RID: 16271
			private string mailboxServer;

			// Token: 0x04003F90 RID: 16272
			private bool resetPasswdSuccess;

			// Token: 0x04003F91 RID: 16273
			private double latency;

			// Token: 0x04003F92 RID: 16274
			private bool resetPinSuccess;
		}

		// Token: 0x02000D56 RID: 3414
		internal class TestMailboxUserDetails
		{
			// Token: 0x060082EA RID: 33514 RVA: 0x002179ED File Offset: 0x00215BED
			internal TestMailboxUserDetails(string phone, string pin, string dialPlan, string mbxServer)
			{
				this.phone = phone;
				this.pin = pin;
				this.mailboxServer = mbxServer;
				this.dialplan = dialPlan;
			}

			// Token: 0x170028B0 RID: 10416
			// (get) Token: 0x060082EB RID: 33515 RVA: 0x00217A12 File Offset: 0x00215C12
			internal string Phone
			{
				get
				{
					return this.phone;
				}
			}

			// Token: 0x170028B1 RID: 10417
			// (get) Token: 0x060082EC RID: 33516 RVA: 0x00217A1A File Offset: 0x00215C1A
			internal string DialPlan
			{
				get
				{
					return this.dialplan;
				}
			}

			// Token: 0x170028B2 RID: 10418
			// (get) Token: 0x060082ED RID: 33517 RVA: 0x00217A22 File Offset: 0x00215C22
			internal string Pin
			{
				get
				{
					return this.pin;
				}
			}

			// Token: 0x170028B3 RID: 10419
			// (get) Token: 0x060082EE RID: 33518 RVA: 0x00217A2A File Offset: 0x00215C2A
			internal string MailboxServer
			{
				get
				{
					return this.mailboxServer;
				}
			}

			// Token: 0x04003F93 RID: 16275
			private readonly string phone;

			// Token: 0x04003F94 RID: 16276
			private readonly string pin;

			// Token: 0x04003F95 RID: 16277
			private readonly string mailboxServer;

			// Token: 0x04003F96 RID: 16278
			private readonly string dialplan;
		}

		// Token: 0x02000D57 RID: 3415
		internal class UsersForResetPin
		{
			// Token: 0x060082EF RID: 33519 RVA: 0x00217A32 File Offset: 0x00215C32
			internal UsersForResetPin(ExchangePrincipal exp, NetworkCredential netc, ADUser aduser, string server)
			{
				this.ep = exp;
				this.nc = netc;
				this.user = aduser;
				this.mailboxServer = server;
			}

			// Token: 0x170028B4 RID: 10420
			// (get) Token: 0x060082F0 RID: 33520 RVA: 0x00217A57 File Offset: 0x00215C57
			internal ExchangePrincipal ExPrincipal
			{
				get
				{
					return this.ep;
				}
			}

			// Token: 0x170028B5 RID: 10421
			// (get) Token: 0x060082F1 RID: 33521 RVA: 0x00217A5F File Offset: 0x00215C5F
			internal NetworkCredential NetCreds
			{
				get
				{
					return this.nc;
				}
			}

			// Token: 0x170028B6 RID: 10422
			// (get) Token: 0x060082F2 RID: 33522 RVA: 0x00217A67 File Offset: 0x00215C67
			internal ADUser Aduser
			{
				get
				{
					return this.user;
				}
			}

			// Token: 0x170028B7 RID: 10423
			// (get) Token: 0x060082F3 RID: 33523 RVA: 0x00217A6F File Offset: 0x00215C6F
			internal string MailboxServer
			{
				get
				{
					return this.mailboxServer;
				}
			}

			// Token: 0x04003F97 RID: 16279
			private ExchangePrincipal ep;

			// Token: 0x04003F98 RID: 16280
			private NetworkCredential nc;

			// Token: 0x04003F99 RID: 16281
			private ADUser user;

			// Token: 0x04003F9A RID: 16282
			private readonly string mailboxServer;
		}
	}
}
