using System;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000026 RID: 38
	internal interface IAirSyncUser
	{
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060002B9 RID: 697
		IAirSyncContext Context { get; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060002BA RID: 698
		Guid DeviceBehaviorCacheGuid { get; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060002BB RID: 699
		IStandardBudget Budget { get; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060002BC RID: 700
		IIdentity Identity { get; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060002BD RID: 701
		WindowsIdentity WindowsIdentity { get; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060002BE RID: 702
		byte[] SID { get; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060002BF RID: 703
		ExchangePrincipal ExchangePrincipal { get; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060002C0 RID: 704
		WindowsPrincipal WindowsPrincipal { get; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060002C1 RID: 705
		bool IsEnabled { get; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060002C2 RID: 706
		ActiveSyncMiniRecipient ADUser { get; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060002C3 RID: 707
		OrganizationId OrganizationId { get; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060002C4 RID: 708
		// (set) Token: 0x060002C5 RID: 709
		bool MailboxIsOnE12Server { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060002C6 RID: 710
		bool IsMonitoringTestUser { get; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060002C7 RID: 711
		ClientSecurityContextWrapper ClientSecurityContextWrapper { get; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060002C8 RID: 712
		string Name { get; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060002C9 RID: 713
		string ServerFullyQualifiedDomainName { get; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060002CA RID: 714
		Guid MailboxGuid { get; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060002CB RID: 715
		string DisplayName { get; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060002CC RID: 716
		string SmtpAddress { get; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060002CD RID: 717
		bool IrmEnabled { get; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060002CE RID: 718
		string WindowsLiveId { get; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060002CF RID: 719
		IEasFeaturesManager Features { get; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060002D0 RID: 720
		BudgetKey BudgetKey { get; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060002D1 RID: 721
		bool IsConsumerOrganizationUser { get; }

		// Token: 0x060002D2 RID: 722
		BackOffValue GetBudgetBackOffValue();

		// Token: 0x060002D3 RID: 723
		void DisposeBudget();

		// Token: 0x060002D4 RID: 724
		void PrepareToHang();

		// Token: 0x060002D5 RID: 725
		void AcquireBudget();

		// Token: 0x060002D6 RID: 726
		void SetBudgetDiagnosticValues(bool start);

		// Token: 0x060002D7 RID: 727
		void InitializeADUser();

		// Token: 0x060002D8 RID: 728
		ITokenBucket GetBudgetTokenBucket();
	}
}
