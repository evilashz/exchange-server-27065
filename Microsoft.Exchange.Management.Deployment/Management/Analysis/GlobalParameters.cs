using System;

namespace Microsoft.Exchange.Management.Analysis
{
	// Token: 0x02000040 RID: 64
	public class GlobalParameters
	{
		// Token: 0x0600017E RID: 382 RVA: 0x000072B4 File Offset: 0x000054B4
		public GlobalParameters(string targetDir, Version exchangeVersion, int adamPort, int adamSSLPort, bool createPublicDB, bool customerFeedbackEnabled, string newProvisionedServerName, string removeProvisionedServerName, string globalCatalog, string domainController, string prepareDomain, bool prepareSCT, bool prepareOrganization, bool prepareSchema, bool prepareAllDomains, string adInitError, string languagePackDir, bool languagesAvailableToInstall, bool sufficientLanguagePackDiskSpace, bool languagePacksInstalled, string alreadyInstalledUMLanguages, bool languagePackVersioning, bool activeDirectorySplitPermissions, string[] setupRoles, bool ignoreFileInUse, bool hostingDeploymentEnabled, string pathToDCHybridConfigFile, bool isDatacenter)
		{
			this.TargetDir = targetDir;
			this.ExchangeVersion = exchangeVersion;
			this.AdamPort = adamPort;
			this.AdamSSLPort = adamSSLPort;
			this.CreatePublicDB = createPublicDB;
			this.CustomerFeedbackEnabled = customerFeedbackEnabled;
			this.NewProvisionedServerName = newProvisionedServerName;
			this.RemoveProvisionedServerName = removeProvisionedServerName;
			this.GlobalCatalog = globalCatalog;
			this.DomainController = domainController;
			this.PrepareDomain = prepareDomain;
			this.PrepareSCT = prepareSCT;
			this.PrepareOrganization = prepareOrganization;
			this.PrepareSchema = prepareSchema;
			this.PrepareAllDomains = prepareAllDomains;
			this.AdInitError = adInitError;
			this.LanguagePackDir = languagePackDir;
			this.LanguagesAvailableToInstall = languagesAvailableToInstall;
			this.SufficientLanguagePackDiskSpace = sufficientLanguagePackDiskSpace;
			this.LanguagePacksInstalled = languagePacksInstalled;
			this.AlreadyInstalledUMLanguages = alreadyInstalledUMLanguages;
			this.LanguagePackVersioning = languagePackVersioning;
			this.ActiveDirectorySplitPermissions = activeDirectorySplitPermissions;
			this.SetupRoles = setupRoles;
			this.IgnoreFileInUse = ignoreFileInUse;
			this.HostingDeploymentEnabled = hostingDeploymentEnabled;
			this.PathToDCHybridConfigFile = pathToDCHybridConfigFile;
			this.IsDatacenter = isDatacenter;
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600017F RID: 383 RVA: 0x000073A4 File Offset: 0x000055A4
		// (set) Token: 0x06000180 RID: 384 RVA: 0x000073AC File Offset: 0x000055AC
		public bool IsDatacenter { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000181 RID: 385 RVA: 0x000073B5 File Offset: 0x000055B5
		// (set) Token: 0x06000182 RID: 386 RVA: 0x000073BD File Offset: 0x000055BD
		public string PathToDCHybridConfigFile { get; private set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000183 RID: 387 RVA: 0x000073C6 File Offset: 0x000055C6
		// (set) Token: 0x06000184 RID: 388 RVA: 0x000073CE File Offset: 0x000055CE
		public string TargetDir { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000185 RID: 389 RVA: 0x000073D7 File Offset: 0x000055D7
		// (set) Token: 0x06000186 RID: 390 RVA: 0x000073DF File Offset: 0x000055DF
		public Version ExchangeVersion { get; private set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000187 RID: 391 RVA: 0x000073E8 File Offset: 0x000055E8
		// (set) Token: 0x06000188 RID: 392 RVA: 0x000073F0 File Offset: 0x000055F0
		public int AdamPort { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000189 RID: 393 RVA: 0x000073F9 File Offset: 0x000055F9
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00007401 File Offset: 0x00005601
		public int AdamSSLPort { get; private set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000740A File Offset: 0x0000560A
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00007412 File Offset: 0x00005612
		public bool CreatePublicDB { get; private set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600018D RID: 397 RVA: 0x0000741B File Offset: 0x0000561B
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00007423 File Offset: 0x00005623
		public bool CustomerFeedbackEnabled { get; private set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600018F RID: 399 RVA: 0x0000742C File Offset: 0x0000562C
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00007434 File Offset: 0x00005634
		public string NewProvisionedServerName { get; private set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000743D File Offset: 0x0000563D
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00007445 File Offset: 0x00005645
		public string RemoveProvisionedServerName { get; private set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000744E File Offset: 0x0000564E
		// (set) Token: 0x06000194 RID: 404 RVA: 0x00007456 File Offset: 0x00005656
		public string GlobalCatalog { get; private set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000195 RID: 405 RVA: 0x0000745F File Offset: 0x0000565F
		// (set) Token: 0x06000196 RID: 406 RVA: 0x00007467 File Offset: 0x00005667
		public string DomainController { get; private set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00007470 File Offset: 0x00005670
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00007478 File Offset: 0x00005678
		public string PrepareDomain { get; private set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00007481 File Offset: 0x00005681
		// (set) Token: 0x0600019A RID: 410 RVA: 0x00007489 File Offset: 0x00005689
		public bool PrepareSCT { get; private set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00007492 File Offset: 0x00005692
		// (set) Token: 0x0600019C RID: 412 RVA: 0x0000749A File Offset: 0x0000569A
		public bool PrepareOrganization { get; private set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600019D RID: 413 RVA: 0x000074A3 File Offset: 0x000056A3
		// (set) Token: 0x0600019E RID: 414 RVA: 0x000074AB File Offset: 0x000056AB
		public bool PrepareSchema { get; private set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600019F RID: 415 RVA: 0x000074B4 File Offset: 0x000056B4
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x000074BC File Offset: 0x000056BC
		public bool PrepareAllDomains { get; private set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x000074C5 File Offset: 0x000056C5
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x000074CD File Offset: 0x000056CD
		public string AdInitError { get; private set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x000074D6 File Offset: 0x000056D6
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x000074DE File Offset: 0x000056DE
		public string LanguagePackDir { get; private set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x000074E7 File Offset: 0x000056E7
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x000074EF File Offset: 0x000056EF
		public bool LanguagesAvailableToInstall { get; private set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x000074F8 File Offset: 0x000056F8
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x00007500 File Offset: 0x00005700
		public bool SufficientLanguagePackDiskSpace { get; private set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00007509 File Offset: 0x00005709
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00007511 File Offset: 0x00005711
		public bool LanguagePacksInstalled { get; private set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000751A File Offset: 0x0000571A
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00007522 File Offset: 0x00005722
		public string AlreadyInstalledUMLanguages { get; private set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000752B File Offset: 0x0000572B
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00007533 File Offset: 0x00005733
		public bool LanguagePackVersioning { get; private set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000753C File Offset: 0x0000573C
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00007544 File Offset: 0x00005744
		public bool ActiveDirectorySplitPermissions { get; private set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000754D File Offset: 0x0000574D
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x00007555 File Offset: 0x00005755
		public string[] SetupRoles { get; private set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000755E File Offset: 0x0000575E
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x00007566 File Offset: 0x00005766
		public bool IgnoreFileInUse { get; private set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x0000756F File Offset: 0x0000576F
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x00007577 File Offset: 0x00005777
		public bool HostingDeploymentEnabled { get; private set; }
	}
}
