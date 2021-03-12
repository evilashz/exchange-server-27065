using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.Deployment;
using Microsoft.Exchange.Setup.CommonBase;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200003D RID: 61
	internal interface ISetupContext
	{
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600025D RID: 605
		// (set) Token: 0x0600025E RID: 606
		bool? ActiveDirectorySplitPermissions { get; set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600025F RID: 607
		// (set) Token: 0x06000260 RID: 608
		ushort AdamLdapPort { get; set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000261 RID: 609
		// (set) Token: 0x06000262 RID: 610
		ushort AdamSslPort { get; set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000263 RID: 611
		LocalizedException ADInitializationError { get; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000264 RID: 612
		// (set) Token: 0x06000265 RID: 613
		bool ADInitializedSuccessfully { get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000266 RID: 614
		// (set) Token: 0x06000267 RID: 615
		NonRootLocalLongFullPath BackupInstalledPath { get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000268 RID: 616
		// (set) Token: 0x06000269 RID: 617
		Version BackupInstalledVersion { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600026A RID: 618
		// (set) Token: 0x0600026B RID: 619
		bool CanOrgBeRemoved { get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600026C RID: 620
		Dictionary<string, LanguageInfo> CollectedLanguagePacks { get; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600026D RID: 621
		// (set) Token: 0x0600026E RID: 622
		string CurrentWizardPageName { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600026F RID: 623
		// (set) Token: 0x06000270 RID: 624
		bool DisableAMFiltering { get; set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000271 RID: 625
		// (set) Token: 0x06000272 RID: 626
		string DomainController { get; set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000273 RID: 627
		// (set) Token: 0x06000274 RID: 628
		CultureInfo ExchangeCulture { get; set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000275 RID: 629
		// (set) Token: 0x06000276 RID: 630
		bool ExchangeOrganizationExists { get; set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000277 RID: 631
		// (set) Token: 0x06000278 RID: 632
		string ExchangeServerName { get; set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000279 RID: 633
		// (set) Token: 0x0600027A RID: 634
		bool? GlobalCustomerFeedbackEnabled { get; set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600027B RID: 635
		// (set) Token: 0x0600027C RID: 636
		bool HasBridgeheadServers { get; set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600027D RID: 637
		// (set) Token: 0x0600027E RID: 638
		bool HasE14OrLaterServers { get; set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600027F RID: 639
		// (set) Token: 0x06000280 RID: 640
		bool HasLegacyServers { get; set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000281 RID: 641
		// (set) Token: 0x06000282 RID: 642
		bool HasMailboxServers { get; set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000283 RID: 643
		bool HasNewProvisionedServerParameters { get; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000284 RID: 644
		bool HasPrepareADParameters { get; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000285 RID: 645
		bool HasRemoveProvisionedServerParameters { get; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000286 RID: 646
		bool HasRolesToInstall { get; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000287 RID: 647
		bool HostingDeployment { get; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000288 RID: 648
		// (set) Token: 0x06000289 RID: 649
		IndustryType Industry { get; set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600028A RID: 650
		// (set) Token: 0x0600028B RID: 651
		InstallationModes InstallationMode { get; set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600028C RID: 652
		HashSet<string> InstalledLanguagePacks { get; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600028D RID: 653
		// (set) Token: 0x0600028E RID: 654
		NonRootLocalLongFullPath InstalledPath { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600028F RID: 655
		// (set) Token: 0x06000290 RID: 656
		RoleCollection InstalledRolesAD { get; set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000291 RID: 657
		// (set) Token: 0x06000292 RID: 658
		RoleCollection InstalledRolesLocal { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000293 RID: 659
		// (set) Token: 0x06000294 RID: 660
		List<CultureInfo> InstalledUMLanguagePacks { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000295 RID: 661
		// (set) Token: 0x06000296 RID: 662
		Version InstalledVersion { get; set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000297 RID: 663
		// (set) Token: 0x06000298 RID: 664
		bool InstallWindowsComponents { get; set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000299 RID: 665
		// (set) Token: 0x0600029A RID: 666
		bool IsBackupKeyPresent { get; set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600029B RID: 667
		// (set) Token: 0x0600029C RID: 668
		bool IsCleanMachine { get; set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600029D RID: 669
		// (set) Token: 0x0600029E RID: 670
		bool IsDatacenter { get; set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600029F RID: 671
		// (set) Token: 0x060002A0 RID: 672
		bool IsDatacenterDedicated { get; set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060002A1 RID: 673
		// (set) Token: 0x060002A2 RID: 674
		bool TreatPreReqErrorsAsWarnings { get; set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060002A3 RID: 675
		bool IsDomainConfigUpdateRequired { get; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060002A4 RID: 676
		// (set) Token: 0x060002A5 RID: 677
		bool IsE12Schema { get; set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060002A6 RID: 678
		// (set) Token: 0x060002A7 RID: 679
		bool IsFfo { get; set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060002A8 RID: 680
		// (set) Token: 0x060002A9 RID: 681
		bool IsLanaguagePacksInstalled { get; set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060002AA RID: 682
		// (set) Token: 0x060002AB RID: 683
		bool IsLanguagePackOperation { get; set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060002AC RID: 684
		// (set) Token: 0x060002AD RID: 685
		bool IsLonghornServer { get; set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060002AE RID: 686
		// (set) Token: 0x060002AF RID: 687
		bool IsOrgConfigUpdateRequired { get; set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060002B0 RID: 688
		// (set) Token: 0x060002B1 RID: 689
		bool IsPartnerHosted { get; set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060002B2 RID: 690
		bool IsProvisionedServer { get; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060002B3 RID: 691
		// (set) Token: 0x060002B4 RID: 692
		bool IsRestoredFromPreviousState { get; set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060002B5 RID: 693
		// (set) Token: 0x060002B6 RID: 694
		bool IsSchemaUpdateRequired { get; set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060002B7 RID: 695
		bool IsServerFoundInAD { get; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060002B8 RID: 696
		// (set) Token: 0x060002B9 RID: 697
		bool IsUmLanguagePackOperation { get; set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060002BA RID: 698
		bool IsW3SVCStartOk { get; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060002BB RID: 699
		// (set) Token: 0x060002BC RID: 700
		LongPath LanguagePackPath { get; set; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060002BD RID: 701
		bool LanguagePackSourceIsBundle { get; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060002BE RID: 702
		Dictionary<string, Array> LanguagePacksToInstall { get; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060002BF RID: 703
		Dictionary<string, long> LanguagesToInstall { get; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060002C0 RID: 704
		// (set) Token: 0x060002C1 RID: 705
		bool NeedToUpdateLanguagePacks { get; set; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060002C2 RID: 706
		string NewProvisionedServerName { get; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060002C3 RID: 707
		// (set) Token: 0x060002C4 RID: 708
		IOrganizationName OrganizationName { get; set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060002C5 RID: 709
		// (set) Token: 0x060002C6 RID: 710
		IOrganizationName OrganizationNameFoundInAD { get; set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060002C7 RID: 711
		// (set) Token: 0x060002C8 RID: 712
		LocalizedException OrganizationNameValidationException { get; set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060002C9 RID: 713
		// (set) Token: 0x060002CA RID: 714
		bool? OriginalGlobalCustomerFeedbackEnabled { get; set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060002CB RID: 715
		// (set) Token: 0x060002CC RID: 716
		IndustryType OriginalIndustry { get; set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060002CD RID: 717
		// (set) Token: 0x060002CE RID: 718
		bool? OriginalServerCustomerFeedbackEnabled { get; set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060002CF RID: 719
		// (set) Token: 0x060002D0 RID: 720
		Dictionary<string, object> ParsedArguments { get; set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060002D1 RID: 721
		// (set) Token: 0x060002D2 RID: 722
		RoleCollection PartiallyConfiguredRoles { get; set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060002D3 RID: 723
		string RemoveProvisionedServerName { get; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060002D4 RID: 724
		// (set) Token: 0x060002D5 RID: 725
		RoleCollection RequestedRoles { get; set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060002D6 RID: 726
		// (set) Token: 0x060002D7 RID: 727
		Version RunningVersion { get; set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060002D8 RID: 728
		// (set) Token: 0x060002D9 RID: 729
		List<CultureInfo> SelectedCultures { get; set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060002DA RID: 730
		// (set) Token: 0x060002DB RID: 731
		bool? ServerCustomerFeedbackEnabled { get; set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060002DC RID: 732
		// (set) Token: 0x060002DD RID: 733
		LongPath SourceDir { get; set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060002DE RID: 734
		Dictionary<string, LanguageInfo> SourceLanguagePacks { get; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060002DF RID: 735
		// (set) Token: 0x060002E0 RID: 736
		bool StartTransportService { get; set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060002E1 RID: 737
		// (set) Token: 0x060002E2 RID: 738
		NonRootLocalLongFullPath TargetDir { get; set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060002E3 RID: 739
		// (set) Token: 0x060002E4 RID: 740
		RoleCollection UnpackedDatacenterRoles { get; set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060002E5 RID: 741
		// (set) Token: 0x060002E6 RID: 742
		RoleCollection UnpackedRoles { get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060002E7 RID: 743
		// (set) Token: 0x060002E8 RID: 744
		LongPath UpdatesDir { get; set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060002E9 RID: 745
		// (set) Token: 0x060002EA RID: 746
		bool WatsonEnabled { get; set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060002EB RID: 747
		LocalizedException RegistryError { get; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060002EC RID: 748
		string TenantOrganizationConfig { get; }

		// Token: 0x060002ED RID: 749
		bool IsInstalledAD(string roleName);

		// Token: 0x060002EE RID: 750
		bool IsInstalledLocal(string roleName);

		// Token: 0x060002EF RID: 751
		bool IsInstalledLocalOrAD(string roleName);

		// Token: 0x060002F0 RID: 752
		bool IsPartiallyConfigured(string roleName);

		// Token: 0x060002F1 RID: 753
		bool IsRequested(string roleName);

		// Token: 0x060002F2 RID: 754
		bool IsUnpacked(string roleName);

		// Token: 0x060002F3 RID: 755
		void UpdateIsW3SVCStartOk();

		// Token: 0x060002F4 RID: 756
		bool IsUnpackedOrInstalledAD(string roleName);

		// Token: 0x060002F5 RID: 757
		IOrganizationName ParseOrganizationName(string name);
	}
}
