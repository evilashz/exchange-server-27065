using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200023C RID: 572
	public struct ADProviderTags
	{
		// Token: 0x04000EC4 RID: 3780
		public const int TopologyProvider = 0;

		// Token: 0x04000EC5 RID: 3781
		public const int ADTopology = 1;

		// Token: 0x04000EC6 RID: 3782
		public const int Connection = 2;

		// Token: 0x04000EC7 RID: 3783
		public const int ConnectionDetails = 3;

		// Token: 0x04000EC8 RID: 3784
		public const int GetConnection = 4;

		// Token: 0x04000EC9 RID: 3785
		public const int ADFind = 5;

		// Token: 0x04000ECA RID: 3786
		public const int ADRead = 6;

		// Token: 0x04000ECB RID: 3787
		public const int ADReadDetails = 7;

		// Token: 0x04000ECC RID: 3788
		public const int ADSave = 8;

		// Token: 0x04000ECD RID: 3789
		public const int ADSaveDetails = 9;

		// Token: 0x04000ECE RID: 3790
		public const int ADDelete = 10;

		// Token: 0x04000ECF RID: 3791
		public const int Validation = 11;

		// Token: 0x04000ED0 RID: 3792
		public const int ADNotifications = 12;

		// Token: 0x04000ED1 RID: 3793
		public const int DirectoryException = 13;

		// Token: 0x04000ED2 RID: 3794
		public const int LdapFilterBuilder = 14;

		// Token: 0x04000ED3 RID: 3795
		public const int ADPropertyRequest = 15;

		// Token: 0x04000ED4 RID: 3796
		public const int ADObject = 17;

		// Token: 0x04000ED5 RID: 3797
		public const int ContentTypeMapping = 19;

		// Token: 0x04000ED6 RID: 3798
		public const int LcidMapper = 20;

		// Token: 0x04000ED7 RID: 3799
		public const int RecipientUpdateService = 21;

		// Token: 0x04000ED8 RID: 3800
		public const int UMAutoAttendant = 22;

		// Token: 0x04000ED9 RID: 3801
		public const int ExchangeTopology = 23;

		// Token: 0x04000EDA RID: 3802
		public const int PerfCounters = 24;

		// Token: 0x04000EDB RID: 3803
		public const int ClientThrottling = 25;

		// Token: 0x04000EDC RID: 3804
		public const int ServerSettingsProvider = 27;

		// Token: 0x04000EDD RID: 3805
		public const int RetryManager = 29;

		// Token: 0x04000EDE RID: 3806
		public const int SystemConfigurationCache = 30;

		// Token: 0x04000EDF RID: 3807
		public const int FederatedIdentity = 31;

		// Token: 0x04000EE0 RID: 3808
		public const int FaultInjection = 32;

		// Token: 0x04000EE1 RID: 3809
		public const int AddressList = 33;

		// Token: 0x04000EE2 RID: 3810
		public const int NspiRpcClientConnection = 34;

		// Token: 0x04000EE3 RID: 3811
		public const int ScopeVerification = 35;

		// Token: 0x04000EE4 RID: 3812
		public const int SchemaInitialization = 36;

		// Token: 0x04000EE5 RID: 3813
		public const int IsMemberOfResolver = 37;

		// Token: 0x04000EE6 RID: 3814
		public const int OwaSegmentation = 39;

		// Token: 0x04000EE7 RID: 3815
		public const int ADPerformance = 40;

		// Token: 0x04000EE8 RID: 3816
		public const int ResourceHealthManager = 41;

		// Token: 0x04000EE9 RID: 3817
		public const int BudgetDelay = 42;

		// Token: 0x04000EEA RID: 3818
		public const int GLS = 43;

		// Token: 0x04000EEB RID: 3819
		public const int MServ = 44;

		// Token: 0x04000EEC RID: 3820
		public const int TenantRelocation = 45;

		// Token: 0x04000EED RID: 3821
		public const int StateManagement = 46;

		// Token: 0x04000EEE RID: 3822
		public const int ServerComponentStateManager = 48;

		// Token: 0x04000EEF RID: 3823
		public const int SessionSettings = 49;

		// Token: 0x04000EF0 RID: 3824
		public const int ADConfigLoader = 50;

		// Token: 0x04000EF1 RID: 3825
		public const int SlimTenant = 51;

		// Token: 0x04000EF2 RID: 3826
		public const int TenantUpgradeServicelet = 52;

		// Token: 0x04000EF3 RID: 3827
		public const int DirectoryTasks = 53;

		// Token: 0x04000EF4 RID: 3828
		public const int Compliance = 54;

		// Token: 0x04000EF5 RID: 3829
		public static Guid guid = new Guid("0c6a4049-bb65-4ea6-9f0c-12808260c2f1");
	}
}
