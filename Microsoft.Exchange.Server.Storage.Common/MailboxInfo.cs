using System;
using System.Security.Principal;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000062 RID: 98
	public class MailboxInfo
	{
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x0000F784 File Offset: 0x0000D984
		// (set) Token: 0x06000577 RID: 1399 RVA: 0x0000F78C File Offset: 0x0000D98C
		public Guid MdbGuid { get; private set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x0000F795 File Offset: 0x0000D995
		// (set) Token: 0x06000579 RID: 1401 RVA: 0x0000F79D File Offset: 0x0000D99D
		public Guid MailboxGuid { get; private set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x0000F7A6 File Offset: 0x0000D9A6
		// (set) Token: 0x0600057B RID: 1403 RVA: 0x0000F7AE File Offset: 0x0000D9AE
		public Guid? PartitionGuid { get; private set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x0000F7B7 File Offset: 0x0000D9B7
		// (set) Token: 0x0600057D RID: 1405 RVA: 0x0000F7BF File Offset: 0x0000D9BF
		public int Lcid { get; private set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x0000F7C8 File Offset: 0x0000D9C8
		// (set) Token: 0x0600057F RID: 1407 RVA: 0x0000F7D0 File Offset: 0x0000D9D0
		public Guid OwnerGuid { get; private set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x0000F7D9 File Offset: 0x0000D9D9
		// (set) Token: 0x06000581 RID: 1409 RVA: 0x0000F7E1 File Offset: 0x0000D9E1
		public string OwnerLegacyDN { get; private set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x0000F7EA File Offset: 0x0000D9EA
		// (set) Token: 0x06000583 RID: 1411 RVA: 0x0000F7F2 File Offset: 0x0000D9F2
		public string OwnerDisplayName { get; private set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x0000F7FB File Offset: 0x0000D9FB
		// (set) Token: 0x06000585 RID: 1413 RVA: 0x0000F803 File Offset: 0x0000DA03
		public string SimpleDisplayName { get; private set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x0000F80C File Offset: 0x0000DA0C
		// (set) Token: 0x06000587 RID: 1415 RVA: 0x0000F814 File Offset: 0x0000DA14
		public string OwnerDistinguishedName { get; private set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x0000F81D File Offset: 0x0000DA1D
		// (set) Token: 0x06000589 RID: 1417 RVA: 0x0000F825 File Offset: 0x0000DA25
		public bool IsMicrosoftExchangeRecipient { get; private set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x0000F82E File Offset: 0x0000DA2E
		// (set) Token: 0x0600058B RID: 1419 RVA: 0x0000F836 File Offset: 0x0000DA36
		public bool IsSystemAttendantRecipient { get; private set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x0000F83F File Offset: 0x0000DA3F
		// (set) Token: 0x0600058D RID: 1421 RVA: 0x0000F847 File Offset: 0x0000DA47
		public SecurityIdentifier MasterAccountSid { get; private set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x0000F850 File Offset: 0x0000DA50
		// (set) Token: 0x0600058F RID: 1423 RVA: 0x0000F858 File Offset: 0x0000DA58
		public SecurityIdentifier UserSid { get; private set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x0000F861 File Offset: 0x0000DA61
		// (set) Token: 0x06000591 RID: 1425 RVA: 0x0000F869 File Offset: 0x0000DA69
		public SecurityIdentifier[] UserSidHistory { get; private set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x0000F872 File Offset: 0x0000DA72
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x0000F87A File Offset: 0x0000DA7A
		public SecurityDescriptor ExchangeSecurityDescriptor { get; private set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x0000F883 File Offset: 0x0000DA83
		// (set) Token: 0x06000595 RID: 1429 RVA: 0x0000F88B File Offset: 0x0000DA8B
		public bool IsDisconnected { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x0000F894 File Offset: 0x0000DA94
		// (set) Token: 0x06000597 RID: 1431 RVA: 0x0000F89C File Offset: 0x0000DA9C
		public bool IsArchiveMailbox { get; private set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x0000F8A5 File Offset: 0x0000DAA5
		// (set) Token: 0x06000599 RID: 1433 RVA: 0x0000F8AD File Offset: 0x0000DAAD
		public bool IsDiscoveryMailbox { get; private set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x0000F8B6 File Offset: 0x0000DAB6
		// (set) Token: 0x0600059B RID: 1435 RVA: 0x0000F8BE File Offset: 0x0000DABE
		public bool IsSystemMailbox { get; private set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x0000F8C7 File Offset: 0x0000DAC7
		// (set) Token: 0x0600059D RID: 1437 RVA: 0x0000F8CF File Offset: 0x0000DACF
		public bool IsHealthMailbox { get; private set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x0000F8D8 File Offset: 0x0000DAD8
		// (set) Token: 0x0600059F RID: 1439 RVA: 0x0000F8E0 File Offset: 0x0000DAE0
		public MailboxInfo.MailboxType Type { get; private set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x0000F8E9 File Offset: 0x0000DAE9
		// (set) Token: 0x060005A1 RID: 1441 RVA: 0x0000F8F1 File Offset: 0x0000DAF1
		public MailboxInfo.MailboxTypeDetail TypeDetail { get; private set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0000F8FA File Offset: 0x0000DAFA
		// (set) Token: 0x060005A3 RID: 1443 RVA: 0x0000F902 File Offset: 0x0000DB02
		public int RulesQuota { get; private set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0000F90B File Offset: 0x0000DB0B
		// (set) Token: 0x060005A5 RID: 1445 RVA: 0x0000F913 File Offset: 0x0000DB13
		public UnlimitedBytes MaxSendSize { get; private set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0000F91C File Offset: 0x0000DB1C
		// (set) Token: 0x060005A7 RID: 1447 RVA: 0x0000F924 File Offset: 0x0000DB24
		public UnlimitedBytes MaxReceiveSize { get; private set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0000F92D File Offset: 0x0000DB2D
		// (set) Token: 0x060005A9 RID: 1449 RVA: 0x0000F935 File Offset: 0x0000DB35
		public QuotaStyle QuotaStyle { get; private set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x0000F93E File Offset: 0x0000DB3E
		// (set) Token: 0x060005AB RID: 1451 RVA: 0x0000F946 File Offset: 0x0000DB46
		public UnlimitedBytes MaxMessageSize { get; private set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x0000F94F File Offset: 0x0000DB4F
		// (set) Token: 0x060005AD RID: 1453 RVA: 0x0000F957 File Offset: 0x0000DB57
		public UnlimitedBytes MaxStreamSize { get; private set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x0000F960 File Offset: 0x0000DB60
		// (set) Token: 0x060005AF RID: 1455 RVA: 0x0000F968 File Offset: 0x0000DB68
		public QuotaInfo QuotaInfo { get; private set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0000F971 File Offset: 0x0000DB71
		// (set) Token: 0x060005B1 RID: 1457 RVA: 0x0000F979 File Offset: 0x0000DB79
		public UnlimitedBytes OrgWidePublicFolderWarningQuota { get; private set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0000F982 File Offset: 0x0000DB82
		// (set) Token: 0x060005B3 RID: 1459 RVA: 0x0000F98A File Offset: 0x0000DB8A
		public UnlimitedBytes OrgWidePublicFolderProhibitPostQuota { get; private set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0000F993 File Offset: 0x0000DB93
		// (set) Token: 0x060005B5 RID: 1461 RVA: 0x0000F99B File Offset: 0x0000DB9B
		public UnlimitedBytes OrgWidePublicFolderMaxItemSize { get; private set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x0000F9A4 File Offset: 0x0000DBA4
		// (set) Token: 0x060005B7 RID: 1463 RVA: 0x0000F9AC File Offset: 0x0000DBAC
		public bool IsTenantMailbox { get; private set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x0000F9B5 File Offset: 0x0000DBB5
		// (set) Token: 0x060005B9 RID: 1465 RVA: 0x0000F9BD File Offset: 0x0000DBBD
		public Guid ExternalDirectoryOrganizationId { get; private set; }

		// Token: 0x060005BA RID: 1466 RVA: 0x0000F9C8 File Offset: 0x0000DBC8
		public MailboxInfo(Guid mdbGuid, Guid mailboxGuid, Guid? partitionGuid, bool isTenantMailbox, bool isArchiveMailbox, bool isSystemMailbox, bool isHealthMailbox, bool isDiscoveryMailbox, MailboxInfo.MailboxType mailboxType, MailboxInfo.MailboxTypeDetail mailboxTypeDetail, Guid ownerGuid, string ownerLegacyDN, string ownerDisplayName, string simpleDisplayName, string ownerDistinguishedName, bool isMSExchangeRecipient, bool isSystemAttendantRecipient, SecurityIdentifier masterAccountSid, SecurityIdentifier userSid, SecurityIdentifier[] userSidHistory, SecurityDescriptor exchangeSecurityDescriptor, int lcid, int rulesQuota, UnlimitedBytes maxSendSize, UnlimitedBytes maxReceiveSize, QuotaStyle quotaStyle, QuotaInfo quotaInfo, UnlimitedBytes orgWidePublicFolderWarningQuota, UnlimitedBytes orgWidePublicFolderProhibitPostQuota, UnlimitedBytes orgWidePublicFolderMaxItemSize, Guid externalDirectoryOrganizationId)
		{
			this.MdbGuid = mdbGuid;
			this.MailboxGuid = mailboxGuid;
			this.PartitionGuid = partitionGuid;
			this.IsTenantMailbox = isTenantMailbox;
			this.IsArchiveMailbox = isArchiveMailbox;
			this.IsSystemMailbox = isSystemMailbox;
			this.IsHealthMailbox = isHealthMailbox;
			this.IsDiscoveryMailbox = isDiscoveryMailbox;
			this.Type = mailboxType;
			this.TypeDetail = mailboxTypeDetail;
			this.OwnerGuid = ownerGuid;
			this.OwnerLegacyDN = ownerLegacyDN;
			this.OwnerDisplayName = ownerDisplayName;
			this.SimpleDisplayName = simpleDisplayName;
			this.OwnerDistinguishedName = ownerDistinguishedName;
			this.IsMicrosoftExchangeRecipient = isMSExchangeRecipient;
			this.IsSystemAttendantRecipient = isSystemAttendantRecipient;
			this.MasterAccountSid = masterAccountSid;
			this.UserSid = userSid;
			this.UserSidHistory = userSidHistory;
			this.ExchangeSecurityDescriptor = exchangeSecurityDescriptor;
			this.IsDisconnected = false;
			this.Lcid = lcid;
			this.RulesQuota = ((rulesQuota > 0) ? rulesQuota : 65536);
			this.MaxSendSize = maxSendSize;
			this.MaxReceiveSize = maxReceiveSize;
			this.MaxMessageSize = maxSendSize;
			this.OrgWidePublicFolderWarningQuota = orgWidePublicFolderWarningQuota;
			this.OrgWidePublicFolderProhibitPostQuota = orgWidePublicFolderProhibitPostQuota;
			this.OrgWidePublicFolderMaxItemSize = orgWidePublicFolderMaxItemSize;
			this.ExternalDirectoryOrganizationId = externalDirectoryOrganizationId;
			this.QuotaStyle = quotaStyle;
			this.QuotaInfo = quotaInfo;
			if (maxReceiveSize > maxSendSize)
			{
				this.MaxMessageSize = maxReceiveSize;
			}
			this.MaxStreamSize = this.MaxMessageSize * 5L;
		}

		// Token: 0x02000063 RID: 99
		public enum MailboxType
		{
			// Token: 0x040005C0 RID: 1472
			Private,
			// Token: 0x040005C1 RID: 1473
			PublicFolderPrimary,
			// Token: 0x040005C2 RID: 1474
			PublicFolderSecondary
		}

		// Token: 0x02000064 RID: 100
		public enum MailboxTypeDetail
		{
			// Token: 0x040005C4 RID: 1476
			None,
			// Token: 0x040005C5 RID: 1477
			UserMailbox,
			// Token: 0x040005C6 RID: 1478
			SharedMailbox,
			// Token: 0x040005C7 RID: 1479
			TeamMailbox,
			// Token: 0x040005C8 RID: 1480
			GroupMailbox,
			// Token: 0x040005C9 RID: 1481
			AuxArchiveMailbox
		}
	}
}
