using System;
using System.IO;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Performance;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001FF RID: 511
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PhotoRequest
	{
		// Token: 0x0600127B RID: 4731 RVA: 0x0004DFBD File Offset: 0x0004C1BD
		public PhotoRequest()
		{
			this.PerformanceLogger = NullPerformanceDataLogger.Instance;
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x0600127C RID: 4732 RVA: 0x0004DFD0 File Offset: 0x0004C1D0
		// (set) Token: 0x0600127D RID: 4733 RVA: 0x0004DFD8 File Offset: 0x0004C1D8
		public string TargetSmtpAddress { get; set; }

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x0600127E RID: 4734 RVA: 0x0004DFE1 File Offset: 0x0004C1E1
		// (set) Token: 0x0600127F RID: 4735 RVA: 0x0004DFE9 File Offset: 0x0004C1E9
		public string TargetPrimarySmtpAddress { get; set; }

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x0004DFF2 File Offset: 0x0004C1F2
		// (set) Token: 0x06001281 RID: 4737 RVA: 0x0004DFFA File Offset: 0x0004C1FA
		public ADObjectId TargetAdObjectId { get; set; }

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001282 RID: 4738 RVA: 0x0004E003 File Offset: 0x0004C203
		// (set) Token: 0x06001283 RID: 4739 RVA: 0x0004E00B File Offset: 0x0004C20B
		public ExchangePrincipal TargetPrincipal { get; set; }

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x0004E014 File Offset: 0x0004C214
		// (set) Token: 0x06001285 RID: 4741 RVA: 0x0004E01C File Offset: 0x0004C21C
		public ADRecipient TargetRecipient { get; set; }

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x0004E025 File Offset: 0x0004C225
		// (set) Token: 0x06001287 RID: 4743 RVA: 0x0004E02D File Offset: 0x0004C22D
		public PersonId TargetPersonId { get; set; }

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06001288 RID: 4744 RVA: 0x0004E036 File Offset: 0x0004C236
		// (set) Token: 0x06001289 RID: 4745 RVA: 0x0004E03E File Offset: 0x0004C23E
		public StoreObjectId TargetContactId { get; set; }

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x0600128A RID: 4746 RVA: 0x0004E047 File Offset: 0x0004C247
		// (set) Token: 0x0600128B RID: 4747 RVA: 0x0004E04F File Offset: 0x0004C24F
		public UserPhotoSize Size { get; set; }

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x0600128C RID: 4748 RVA: 0x0004E058 File Offset: 0x0004C258
		// (set) Token: 0x0600128D RID: 4749 RVA: 0x0004E060 File Offset: 0x0004C260
		public bool Preview { get; set; }

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x0600128E RID: 4750 RVA: 0x0004E069 File Offset: 0x0004C269
		// (set) Token: 0x0600128F RID: 4751 RVA: 0x0004E071 File Offset: 0x0004C271
		public string ETag { get; set; }

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001290 RID: 4752 RVA: 0x0004E07A File Offset: 0x0004C27A
		// (set) Token: 0x06001291 RID: 4753 RVA: 0x0004E082 File Offset: 0x0004C282
		public UploadCommand UploadCommand { get; set; }

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001292 RID: 4754 RVA: 0x0004E08B File Offset: 0x0004C28B
		// (set) Token: 0x06001293 RID: 4755 RVA: 0x0004E093 File Offset: 0x0004C293
		public Stream RawUploadedPhoto { get; set; }

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001294 RID: 4756 RVA: 0x0004E09C File Offset: 0x0004C29C
		// (set) Token: 0x06001295 RID: 4757 RVA: 0x0004E0A4 File Offset: 0x0004C2A4
		public PhotoPrincipal Requestor { get; set; }

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x0004E0AD File Offset: 0x0004C2AD
		// (set) Token: 0x06001297 RID: 4759 RVA: 0x0004E0B5 File Offset: 0x0004C2B5
		public IMailboxSession RequestorMailboxSession { get; set; }

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001298 RID: 4760 RVA: 0x0004E0BE File Offset: 0x0004C2BE
		// (set) Token: 0x06001299 RID: 4761 RVA: 0x0004E0C6 File Offset: 0x0004C2C6
		public ADObjectId UploadTo { get; set; }

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x0600129A RID: 4762 RVA: 0x0004E0CF File Offset: 0x0004C2CF
		// (set) Token: 0x0600129B RID: 4763 RVA: 0x0004E0D7 File Offset: 0x0004C2D7
		public IPerformanceDataLogger PerformanceLogger { get; set; }

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x0600129C RID: 4764 RVA: 0x0004E0E0 File Offset: 0x0004C2E0
		// (set) Token: 0x0600129D RID: 4765 RVA: 0x0004E0E8 File Offset: 0x0004C2E8
		public Func<ExchangePrincipal, IMailboxSession> HostOwnedTargetMailboxSessionGetter { get; set; }

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x0600129E RID: 4766 RVA: 0x0004E0F1 File Offset: 0x0004C2F1
		// (set) Token: 0x0600129F RID: 4767 RVA: 0x0004E0F9 File Offset: 0x0004C2F9
		public PhotoHandlers HandlersToSkip { get; set; }

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x060012A0 RID: 4768 RVA: 0x0004E102 File Offset: 0x0004C302
		// (set) Token: 0x060012A1 RID: 4769 RVA: 0x0004E10A File Offset: 0x0004C30A
		public bool Trace { get; set; }

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x060012A2 RID: 4770 RVA: 0x0004E113 File Offset: 0x0004C313
		// (set) Token: 0x060012A3 RID: 4771 RVA: 0x0004E11B File Offset: 0x0004C31B
		public bool? Self { get; set; }

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x060012A4 RID: 4772 RVA: 0x0004E124 File Offset: 0x0004C324
		// (set) Token: 0x060012A5 RID: 4773 RVA: 0x0004E12C File Offset: 0x0004C32C
		public bool? IsTargetKnownToBeLocalToThisServer { get; set; }

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x060012A6 RID: 4774 RVA: 0x0004E135 File Offset: 0x0004C335
		// (set) Token: 0x060012A7 RID: 4775 RVA: 0x0004E13D File Offset: 0x0004C33D
		public bool? IsTargetMailboxLikelyOnThisServer { get; set; }

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x060012A8 RID: 4776 RVA: 0x0004E146 File Offset: 0x0004C346
		// (set) Token: 0x060012A9 RID: 4777 RVA: 0x0004E14E File Offset: 0x0004C34E
		public bool RequestorFromExternalOrganization { get; set; }

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x060012AA RID: 4778 RVA: 0x0004E157 File Offset: 0x0004C357
		// (set) Token: 0x060012AB RID: 4779 RVA: 0x0004E15F File Offset: 0x0004C35F
		public string ClientRequestId { get; set; }

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x060012AC RID: 4780 RVA: 0x0004E168 File Offset: 0x0004C368
		// (set) Token: 0x060012AD RID: 4781 RVA: 0x0004E170 File Offset: 0x0004C370
		public object ClientContextForRemoteForestRequests { get; set; }

		// Token: 0x060012AE RID: 4782 RVA: 0x0004E179 File Offset: 0x0004C379
		public bool ShouldSkipHandlers(PhotoHandlers handlers)
		{
			return (this.HandlersToSkip & handlers) == handlers;
		}
	}
}
