using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	// Token: 0x0200030D RID: 781
	[ComVisible(false)]
	public enum WellKnownSidType
	{
		// Token: 0x04000FF1 RID: 4081
		NullSid,
		// Token: 0x04000FF2 RID: 4082
		WorldSid,
		// Token: 0x04000FF3 RID: 4083
		LocalSid,
		// Token: 0x04000FF4 RID: 4084
		CreatorOwnerSid,
		// Token: 0x04000FF5 RID: 4085
		CreatorGroupSid,
		// Token: 0x04000FF6 RID: 4086
		CreatorOwnerServerSid,
		// Token: 0x04000FF7 RID: 4087
		CreatorGroupServerSid,
		// Token: 0x04000FF8 RID: 4088
		NTAuthoritySid,
		// Token: 0x04000FF9 RID: 4089
		DialupSid,
		// Token: 0x04000FFA RID: 4090
		NetworkSid,
		// Token: 0x04000FFB RID: 4091
		BatchSid,
		// Token: 0x04000FFC RID: 4092
		InteractiveSid,
		// Token: 0x04000FFD RID: 4093
		ServiceSid,
		// Token: 0x04000FFE RID: 4094
		AnonymousSid,
		// Token: 0x04000FFF RID: 4095
		ProxySid,
		// Token: 0x04001000 RID: 4096
		EnterpriseControllersSid,
		// Token: 0x04001001 RID: 4097
		SelfSid,
		// Token: 0x04001002 RID: 4098
		AuthenticatedUserSid,
		// Token: 0x04001003 RID: 4099
		RestrictedCodeSid,
		// Token: 0x04001004 RID: 4100
		TerminalServerSid,
		// Token: 0x04001005 RID: 4101
		RemoteLogonIdSid,
		// Token: 0x04001006 RID: 4102
		LogonIdsSid,
		// Token: 0x04001007 RID: 4103
		LocalSystemSid,
		// Token: 0x04001008 RID: 4104
		LocalServiceSid,
		// Token: 0x04001009 RID: 4105
		NetworkServiceSid,
		// Token: 0x0400100A RID: 4106
		BuiltinDomainSid,
		// Token: 0x0400100B RID: 4107
		BuiltinAdministratorsSid,
		// Token: 0x0400100C RID: 4108
		BuiltinUsersSid,
		// Token: 0x0400100D RID: 4109
		BuiltinGuestsSid,
		// Token: 0x0400100E RID: 4110
		BuiltinPowerUsersSid,
		// Token: 0x0400100F RID: 4111
		BuiltinAccountOperatorsSid,
		// Token: 0x04001010 RID: 4112
		BuiltinSystemOperatorsSid,
		// Token: 0x04001011 RID: 4113
		BuiltinPrintOperatorsSid,
		// Token: 0x04001012 RID: 4114
		BuiltinBackupOperatorsSid,
		// Token: 0x04001013 RID: 4115
		BuiltinReplicatorSid,
		// Token: 0x04001014 RID: 4116
		BuiltinPreWindows2000CompatibleAccessSid,
		// Token: 0x04001015 RID: 4117
		BuiltinRemoteDesktopUsersSid,
		// Token: 0x04001016 RID: 4118
		BuiltinNetworkConfigurationOperatorsSid,
		// Token: 0x04001017 RID: 4119
		AccountAdministratorSid,
		// Token: 0x04001018 RID: 4120
		AccountGuestSid,
		// Token: 0x04001019 RID: 4121
		AccountKrbtgtSid,
		// Token: 0x0400101A RID: 4122
		AccountDomainAdminsSid,
		// Token: 0x0400101B RID: 4123
		AccountDomainUsersSid,
		// Token: 0x0400101C RID: 4124
		AccountDomainGuestsSid,
		// Token: 0x0400101D RID: 4125
		AccountComputersSid,
		// Token: 0x0400101E RID: 4126
		AccountControllersSid,
		// Token: 0x0400101F RID: 4127
		AccountCertAdminsSid,
		// Token: 0x04001020 RID: 4128
		AccountSchemaAdminsSid,
		// Token: 0x04001021 RID: 4129
		AccountEnterpriseAdminsSid,
		// Token: 0x04001022 RID: 4130
		AccountPolicyAdminsSid,
		// Token: 0x04001023 RID: 4131
		AccountRasAndIasServersSid,
		// Token: 0x04001024 RID: 4132
		NtlmAuthenticationSid,
		// Token: 0x04001025 RID: 4133
		DigestAuthenticationSid,
		// Token: 0x04001026 RID: 4134
		SChannelAuthenticationSid,
		// Token: 0x04001027 RID: 4135
		ThisOrganizationSid,
		// Token: 0x04001028 RID: 4136
		OtherOrganizationSid,
		// Token: 0x04001029 RID: 4137
		BuiltinIncomingForestTrustBuildersSid,
		// Token: 0x0400102A RID: 4138
		BuiltinPerformanceMonitoringUsersSid,
		// Token: 0x0400102B RID: 4139
		BuiltinPerformanceLoggingUsersSid,
		// Token: 0x0400102C RID: 4140
		BuiltinAuthorizationAccessSid,
		// Token: 0x0400102D RID: 4141
		WinBuiltinTerminalServerLicenseServersSid,
		// Token: 0x0400102E RID: 4142
		MaxDefined = 60
	}
}
