using System;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC.Logging
{
	// Token: 0x020000B0 RID: 176
	public class StatisticsLogEntry : AssistantLogEntryBase
	{
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x00033842 File Offset: 0x00031A42
		// (set) Token: 0x060006BF RID: 1727 RVA: 0x0003384A File Offset: 0x00031A4A
		[LogField]
		public string ProcessingStartTime { get; set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x00033853 File Offset: 0x00031A53
		// (set) Token: 0x060006C1 RID: 1729 RVA: 0x0003385B File Offset: 0x00031A5B
		[LogField]
		public string ProcessingEndTime { get; set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x00033864 File Offset: 0x00031A64
		// (set) Token: 0x060006C3 RID: 1731 RVA: 0x0003386C File Offset: 0x00031A6C
		[LogField]
		public string OrganizationName { get; set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x00033875 File Offset: 0x00031A75
		// (set) Token: 0x060006C5 RID: 1733 RVA: 0x0003387D File Offset: 0x00031A7D
		[LogField]
		public Guid MailboxGuid { get; set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x00033886 File Offset: 0x00031A86
		// (set) Token: 0x060006C7 RID: 1735 RVA: 0x0003388E File Offset: 0x00031A8E
		[LogField]
		public bool IsArchive { get; set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x00033897 File Offset: 0x00031A97
		// (set) Token: 0x060006C9 RID: 1737 RVA: 0x0003389F File Offset: 0x00031A9F
		[LogField]
		public bool IsArchiveOverWarningQuota { get; set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x000338A8 File Offset: 0x00031AA8
		// (set) Token: 0x060006CB RID: 1739 RVA: 0x000338B0 File Offset: 0x00031AB0
		[LogField]
		public bool IsOnDemandJob { get; set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x000338B9 File Offset: 0x00031AB9
		// (set) Token: 0x060006CD RID: 1741 RVA: 0x000338C1 File Offset: 0x00031AC1
		[LogField]
		public bool IsFullCrawlNeeded { get; set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x000338CA File Offset: 0x00031ACA
		// (set) Token: 0x060006CF RID: 1743 RVA: 0x000338D2 File Offset: 0x00031AD2
		[LogField]
		public bool IsLitigationHoldEnabled { get; set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x000338DB File Offset: 0x00031ADB
		// (set) Token: 0x060006D1 RID: 1745 RVA: 0x000338E3 File Offset: 0x00031AE3
		[LogField]
		public bool IsInPlaceHoldEnabled { get; set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x000338EC File Offset: 0x00031AEC
		// (set) Token: 0x060006D3 RID: 1747 RVA: 0x000338F4 File Offset: 0x00031AF4
		[LogField]
		public bool IsAuditEnabled { get; set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x000338FD File Offset: 0x00031AFD
		// (set) Token: 0x060006D5 RID: 1749 RVA: 0x00033905 File Offset: 0x00031B05
		[LogField]
		public long TotalProcessingTime { get; set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0003390E File Offset: 0x00031B0E
		// (set) Token: 0x060006D7 RID: 1751 RVA: 0x00033916 File Offset: 0x00031B16
		[LogField]
		public long TagSubAssistantProcessingTime { get; set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x0003391F File Offset: 0x00031B1F
		// (set) Token: 0x060006D9 RID: 1753 RVA: 0x00033927 File Offset: 0x00031B27
		[LogField]
		public long FolderSubAssistantProcessingTime { get; set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x00033930 File Offset: 0x00031B30
		// (set) Token: 0x060006DB RID: 1755 RVA: 0x00033938 File Offset: 0x00031B38
		[LogField]
		public long CleanupSubAssistantProcessingTime { get; set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x00033941 File Offset: 0x00031B41
		// (set) Token: 0x060006DD RID: 1757 RVA: 0x00033949 File Offset: 0x00031B49
		[LogField]
		public long TagProvisionerProcessingTime { get; set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x00033952 File Offset: 0x00031B52
		// (set) Token: 0x060006DF RID: 1759 RVA: 0x0003395A File Offset: 0x00031B5A
		[LogField]
		public long TagEnforcerProcessingTime { get; set; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x00033963 File Offset: 0x00031B63
		// (set) Token: 0x060006E1 RID: 1761 RVA: 0x0003396B File Offset: 0x00031B6B
		[LogField]
		public long DumpsterExpirationEnforcerProcessingTime { get; set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x00033974 File Offset: 0x00031B74
		// (set) Token: 0x060006E3 RID: 1763 RVA: 0x0003397C File Offset: 0x00031B7C
		[LogField]
		public long CalendarLogExpirationEnforcerProcessingTime { get; set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x00033985 File Offset: 0x00031B85
		// (set) Token: 0x060006E5 RID: 1765 RVA: 0x0003398D File Offset: 0x00031B8D
		[LogField]
		public long AuditExpirationEnforcerProcessingTime { get; set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x00033996 File Offset: 0x00031B96
		// (set) Token: 0x060006E7 RID: 1767 RVA: 0x0003399E File Offset: 0x00031B9E
		[LogField]
		public long DumpsterQuotaEnforcerProcessingTime { get; set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x000339A7 File Offset: 0x00031BA7
		// (set) Token: 0x060006E9 RID: 1769 RVA: 0x000339AF File Offset: 0x00031BAF
		[LogField]
		public long DiscoveryHoldEnforcerProcessingTime { get; set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x000339B8 File Offset: 0x00031BB8
		// (set) Token: 0x060006EB RID: 1771 RVA: 0x000339C0 File Offset: 0x00031BC0
		[LogField]
		public long SupplementExpirationEnforcerProcessingTime { get; set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x000339C9 File Offset: 0x00031BC9
		// (set) Token: 0x060006ED RID: 1773 RVA: 0x000339D1 File Offset: 0x00031BD1
		[LogField]
		public long EHAHiddenFolderCleanupEnforcerProcessingTime { get; set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x000339DA File Offset: 0x00031BDA
		// (set) Token: 0x060006EF RID: 1775 RVA: 0x000339E2 File Offset: 0x00031BE2
		[LogField]
		public long MigrateToArchiveEnforcerProcessingTime { get; set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x000339EB File Offset: 0x00031BEB
		// (set) Token: 0x060006F1 RID: 1777 RVA: 0x000339F3 File Offset: 0x00031BF3
		[LogField]
		public long EHAMigratedMessageMoveEnforcerProcessingTime { get; set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x000339FC File Offset: 0x00031BFC
		// (set) Token: 0x060006F3 RID: 1779 RVA: 0x00033A04 File Offset: 0x00031C04
		[LogField]
		public long EHAMigratedMessageDeletionEnforcerProcessingTime { get; set; }

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x00033A0D File Offset: 0x00031C0D
		// (set) Token: 0x060006F5 RID: 1781 RVA: 0x00033A15 File Offset: 0x00031C15
		[LogField]
		public long HoldCleanupEnforcerProcessingTime { get; set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x00033A1E File Offset: 0x00031C1E
		// (set) Token: 0x060006F7 RID: 1783 RVA: 0x00033A26 File Offset: 0x00031C26
		[LogField]
		public long NumberOfFoldersUpdated { get; set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x00033A2F File Offset: 0x00031C2F
		// (set) Token: 0x060006F9 RID: 1785 RVA: 0x00033A37 File Offset: 0x00031C37
		[LogField]
		public long NumberOfFoldersTaggedByPersonalArchiveTag { get; set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x00033A40 File Offset: 0x00031C40
		// (set) Token: 0x060006FB RID: 1787 RVA: 0x00033A48 File Offset: 0x00031C48
		[LogField]
		public long NumberOfFoldersTaggedByPersonalExpiryTag { get; set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x00033A51 File Offset: 0x00031C51
		// (set) Token: 0x060006FD RID: 1789 RVA: 0x00033A59 File Offset: 0x00031C59
		[LogField]
		public long NumberOfFoldersTaggedBySystemExpiryTag { get; set; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x00033A62 File Offset: 0x00031C62
		// (set) Token: 0x060006FF RID: 1791 RVA: 0x00033A6A File Offset: 0x00031C6A
		[LogField]
		public long NumberOfFoldersTaggedByUncertaionExpiryTag { get; set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x00033A73 File Offset: 0x00031C73
		// (set) Token: 0x06000701 RID: 1793 RVA: 0x00033A7B File Offset: 0x00031C7B
		[LogField]
		public long NumberOfItemsUpdated { get; set; }

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x00033A84 File Offset: 0x00031C84
		// (set) Token: 0x06000703 RID: 1795 RVA: 0x00033A8C File Offset: 0x00031C8C
		[LogField]
		public long NumberOfItemsTaggedByPersonalArchiveTag { get; set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x00033A95 File Offset: 0x00031C95
		// (set) Token: 0x06000705 RID: 1797 RVA: 0x00033A9D File Offset: 0x00031C9D
		[LogField]
		public long NumberOfItemsTaggedByPersonalExpiryTag { get; set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x00033AA6 File Offset: 0x00031CA6
		// (set) Token: 0x06000707 RID: 1799 RVA: 0x00033AAE File Offset: 0x00031CAE
		[LogField]
		public long NumberOfItemsTaggedByDefaultExpiryTag { get; set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x00033AB7 File Offset: 0x00031CB7
		// (set) Token: 0x06000709 RID: 1801 RVA: 0x00033ABF File Offset: 0x00031CBF
		[LogField]
		public long NumberOfItemsTaggedBySystemExpiryTag { get; set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x00033AC8 File Offset: 0x00031CC8
		// (set) Token: 0x0600070B RID: 1803 RVA: 0x00033AD0 File Offset: 0x00031CD0
		[LogField]
		public long NumberOfItemsTaggedByUncertaionExpiryTag { get; set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x00033AD9 File Offset: 0x00031CD9
		// (set) Token: 0x0600070D RID: 1805 RVA: 0x00033AE1 File Offset: 0x00031CE1
		[LogField]
		public long NumberOfItemsDeletedByPersonalTag { get; set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x00033AEA File Offset: 0x00031CEA
		// (set) Token: 0x0600070F RID: 1807 RVA: 0x00033AF2 File Offset: 0x00031CF2
		[LogField]
		public long NumberOfItemsDeletedByDefaultTag { get; set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x00033AFB File Offset: 0x00031CFB
		// (set) Token: 0x06000711 RID: 1809 RVA: 0x00033B03 File Offset: 0x00031D03
		[LogField]
		public long NumberOfItemsDeletedBySystemTag { get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x00033B0C File Offset: 0x00031D0C
		// (set) Token: 0x06000713 RID: 1811 RVA: 0x00033B14 File Offset: 0x00031D14
		[LogField]
		public long NumberOfItemsActuallyDeletedByTag { get; set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x00033B1D File Offset: 0x00031D1D
		// (set) Token: 0x06000715 RID: 1813 RVA: 0x00033B25 File Offset: 0x00031D25
		[LogField]
		public long NumberOfItemsArchivedByPersonalTag { get; set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x00033B2E File Offset: 0x00031D2E
		// (set) Token: 0x06000717 RID: 1815 RVA: 0x00033B36 File Offset: 0x00031D36
		[LogField]
		public long NumberOfItemsArchivedByDefaultTag { get; set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x00033B3F File Offset: 0x00031D3F
		// (set) Token: 0x06000719 RID: 1817 RVA: 0x00033B47 File Offset: 0x00031D47
		[LogField]
		public long NumberOfItemsActuallyArchivedByTag { get; set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x00033B50 File Offset: 0x00031D50
		// (set) Token: 0x0600071B RID: 1819 RVA: 0x00033B58 File Offset: 0x00031D58
		[LogField]
		public long NumberOfItemsDeletedByDumpsterExpirationEnforcer { get; set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x00033B61 File Offset: 0x00031D61
		// (set) Token: 0x0600071D RID: 1821 RVA: 0x00033B69 File Offset: 0x00031D69
		[LogField]
		public long NumberOfItemsActuallyDeletedByDumpsterExpirationEnforcer { get; set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x00033B72 File Offset: 0x00031D72
		// (set) Token: 0x0600071F RID: 1823 RVA: 0x00033B7A File Offset: 0x00031D7A
		[LogField]
		public long NumberOfItemsArchivedByDumpsterExpirationEnforcer { get; set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x00033B83 File Offset: 0x00031D83
		// (set) Token: 0x06000721 RID: 1825 RVA: 0x00033B8B File Offset: 0x00031D8B
		[LogField]
		public long NumberOfItemsActuallyArchivedByDumpsterExpirationEnforcer { get; set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x00033B94 File Offset: 0x00031D94
		// (set) Token: 0x06000723 RID: 1827 RVA: 0x00033B9C File Offset: 0x00031D9C
		[LogField]
		public long NumberOfItemsMovedToPurgesByDumpsterExpirationEnforcer { get; set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x00033BA5 File Offset: 0x00031DA5
		// (set) Token: 0x06000725 RID: 1829 RVA: 0x00033BAD File Offset: 0x00031DAD
		[LogField]
		public long NumberOfItemsActuallyMovedToPurgesByDumpsterExpirationEnforcer { get; set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x00033BB6 File Offset: 0x00031DB6
		// (set) Token: 0x06000727 RID: 1831 RVA: 0x00033BBE File Offset: 0x00031DBE
		[LogField]
		public long NumberOfItemsDeletedByAuditExpirationEnforcer { get; set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x00033BC7 File Offset: 0x00031DC7
		// (set) Token: 0x06000729 RID: 1833 RVA: 0x00033BCF File Offset: 0x00031DCF
		[LogField]
		public long NumberOfItemsDeletedByCalendarLogExpirationEnforcer { get; set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x00033BD8 File Offset: 0x00031DD8
		// (set) Token: 0x0600072B RID: 1835 RVA: 0x00033BE0 File Offset: 0x00031DE0
		[LogField]
		public long NumberOfItemsDeletedByDumpsterQuotaEnforcer { get; set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x00033BE9 File Offset: 0x00031DE9
		// (set) Token: 0x0600072D RID: 1837 RVA: 0x00033BF1 File Offset: 0x00031DF1
		[LogField]
		public long NumberOfItemsActuallyDeletedByDumpsterQuotaEnforcer { get; set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x00033BFA File Offset: 0x00031DFA
		// (set) Token: 0x0600072F RID: 1839 RVA: 0x00033C02 File Offset: 0x00031E02
		[LogField]
		public long NumberOfItemsDeletedBySupplementExpirationEnforcer { get; set; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x00033C0B File Offset: 0x00031E0B
		// (set) Token: 0x06000731 RID: 1841 RVA: 0x00033C13 File Offset: 0x00031E13
		[LogField]
		public long NumberOfItemsDeletedByDiscoveryHoldEnforcer { get; set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x00033C1C File Offset: 0x00031E1C
		// (set) Token: 0x06000733 RID: 1843 RVA: 0x00033C24 File Offset: 0x00031E24
		[LogField]
		public long NumberOfItemsActuallyDeletedByDiscoveryHoldEnforcer { get; set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x00033C2D File Offset: 0x00031E2D
		// (set) Token: 0x06000735 RID: 1845 RVA: 0x00033C35 File Offset: 0x00031E35
		[LogField]
		public long NumberOfItemsMovedByMigrateToArchiveEnforcer { get; set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x00033C3E File Offset: 0x00031E3E
		// (set) Token: 0x06000737 RID: 1847 RVA: 0x00033C46 File Offset: 0x00031E46
		[LogField]
		public long NumberOfMigratedItemsDeletedDueToMigrationExpiryDate { get; set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x00033C4F File Offset: 0x00031E4F
		// (set) Token: 0x06000739 RID: 1849 RVA: 0x00033C57 File Offset: 0x00031E57
		[LogField]
		public long NumberOfMigratedItemsMovedDueToMigrationExpiryDate { get; set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x00033C60 File Offset: 0x00031E60
		// (set) Token: 0x0600073B RID: 1851 RVA: 0x00033C68 File Offset: 0x00031E68
		[LogField]
		public long NumberOfItemsInDiscoveryHoldFolderBeforeProcessing { get; set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x00033C71 File Offset: 0x00031E71
		// (set) Token: 0x0600073D RID: 1853 RVA: 0x00033C79 File Offset: 0x00031E79
		[LogField]
		public long SizeOfDeletionByDiscoveryHoldEnforcer { get; set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x00033C82 File Offset: 0x00031E82
		// (set) Token: 0x0600073F RID: 1855 RVA: 0x00033C8A File Offset: 0x00031E8A
		[LogField]
		public long DiscoveryHoldFolderSizeBeforeProcessing { get; set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x00033C93 File Offset: 0x00031E93
		// (set) Token: 0x06000741 RID: 1857 RVA: 0x00033C9B File Offset: 0x00031E9B
		[LogField]
		public long EhaMigrationMessageCount { get; set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x00033CA4 File Offset: 0x00031EA4
		// (set) Token: 0x06000743 RID: 1859 RVA: 0x00033CAC File Offset: 0x00031EAC
		[LogField]
		public long NumberOfItemsDeletedFromInboxByEHAHiddenFolderCleanupEnforcer { get; set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x00033CB5 File Offset: 0x00031EB5
		// (set) Token: 0x06000745 RID: 1861 RVA: 0x00033CBD File Offset: 0x00031EBD
		[LogField]
		public long NumberOfItemsDeletedFromSentByEHAHiddenFolderCleanupEnforcer { get; set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x00033CC6 File Offset: 0x00031EC6
		// (set) Token: 0x06000747 RID: 1863 RVA: 0x00033CCE File Offset: 0x00031ECE
		[LogField]
		public long NumberOfItemsDeterminedDuplicateByHoldCleanupEnforcer { get; set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x00033CD7 File Offset: 0x00031ED7
		// (set) Token: 0x06000749 RID: 1865 RVA: 0x00033CDF File Offset: 0x00031EDF
		[LogField]
		public long SizeOfItemsDeterminedDuplicateByHoldCleanupEnforcer { get; set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x00033CE8 File Offset: 0x00031EE8
		// (set) Token: 0x0600074B RID: 1867 RVA: 0x00033CF0 File Offset: 0x00031EF0
		[LogField]
		public long NumberOfItemsAnalyzedByHoldCleanupEnforcer { get; set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x00033CF9 File Offset: 0x00031EF9
		// (set) Token: 0x0600074D RID: 1869 RVA: 0x00033D01 File Offset: 0x00031F01
		[LogField]
		public long NumberOfItemsSkippedByHoldCleanupEnforcer { get; set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x00033D0A File Offset: 0x00031F0A
		// (set) Token: 0x0600074F RID: 1871 RVA: 0x00033D12 File Offset: 0x00031F12
		[LogField]
		public long NumberOfBatchesFailedToExpireInExpirationExecutor { get; set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x00033D1B File Offset: 0x00031F1B
		// (set) Token: 0x06000751 RID: 1873 RVA: 0x00033D23 File Offset: 0x00031F23
		[LogField]
		public long NumberOfBatchesFailedToMoveInArchiveProcessor { get; set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x00033D2C File Offset: 0x00031F2C
		// (set) Token: 0x06000753 RID: 1875 RVA: 0x00033D34 File Offset: 0x00031F34
		[LogField]
		public long NumberOfItemsSkippedDueToSizeRestrictionInArchiveProcessor { get; set; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x00033D3D File Offset: 0x00031F3D
		// (set) Token: 0x06000755 RID: 1877 RVA: 0x00033D45 File Offset: 0x00031F45
		[LogField]
		public string ExceptionType { get; set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x00033D4E File Offset: 0x00031F4E
		// (set) Token: 0x06000757 RID: 1879 RVA: 0x00033D56 File Offset: 0x00031F56
		[LogField]
		public int RetryCount { get; set; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x00033D5F File Offset: 0x00031F5F
		// (set) Token: 0x06000759 RID: 1881 RVA: 0x00033D67 File Offset: 0x00031F67
		[LogField]
		public string DeletionAgeLimit { get; set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x00033D70 File Offset: 0x00031F70
		// (set) Token: 0x0600075B RID: 1883 RVA: 0x00033D78 File Offset: 0x00031F78
		[LogField]
		public string LastProcessedEnforcer { get; set; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x00033D81 File Offset: 0x00031F81
		// (set) Token: 0x0600075D RID: 1885 RVA: 0x00033D89 File Offset: 0x00031F89
		[LogField]
		public bool MoveToArchiveLimitReached { get; set; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x00033D92 File Offset: 0x00031F92
		// (set) Token: 0x0600075F RID: 1887 RVA: 0x00033D9A File Offset: 0x00031F9A
		[LogField]
		public string FailedToLoadUnifiedPolicies { get; set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x00033DA3 File Offset: 0x00031FA3
		// (set) Token: 0x06000761 RID: 1889 RVA: 0x00033DAB File Offset: 0x00031FAB
		[LogField]
		public string AdminAuditRecordAgeLimit { get; set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x00033DB4 File Offset: 0x00031FB4
		// (set) Token: 0x06000763 RID: 1891 RVA: 0x00033DBC File Offset: 0x00031FBC
		[LogField]
		public string MailboxAuditRecordAgeLimit { get; set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x00033DC5 File Offset: 0x00031FC5
		// (set) Token: 0x06000765 RID: 1893 RVA: 0x00033DCD File Offset: 0x00031FCD
		[LogField]
		public string OldestExpiringAuditLog { get; set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x00033DD6 File Offset: 0x00031FD6
		// (set) Token: 0x06000767 RID: 1895 RVA: 0x00033DDE File Offset: 0x00031FDE
		[LogField]
		public bool IsAdminAuditLog { get; set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x00033DE7 File Offset: 0x00031FE7
		// (set) Token: 0x06000769 RID: 1897 RVA: 0x00033DEF File Offset: 0x00031FEF
		[LogField]
		public string ArchiveProcessor { get; set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x00033DF8 File Offset: 0x00031FF8
		// (set) Token: 0x0600076B RID: 1899 RVA: 0x00033E00 File Offset: 0x00032000
		[LogField]
		public bool IsInactiveMailbox { get; set; }
	}
}
