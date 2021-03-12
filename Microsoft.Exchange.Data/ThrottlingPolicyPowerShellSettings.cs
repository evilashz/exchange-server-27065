using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001B7 RID: 439
	internal class ThrottlingPolicyPowerShellSettings : ThrottlingPolicyBaseSettingsWithCommonAttributes
	{
		// Token: 0x06000F4E RID: 3918 RVA: 0x0002EEAD File Offset: 0x0002D0AD
		public ThrottlingPolicyPowerShellSettings()
		{
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x0002EEB8 File Offset: 0x0002D0B8
		private ThrottlingPolicyPowerShellSettings(string value) : base(value)
		{
			Unlimited<uint>? maxTenantConcurrency = this.MaxTenantConcurrency;
			Unlimited<uint>? maxOperations = this.MaxOperations;
			Unlimited<uint>? maxCmdletsTimePeriod = this.MaxCmdletsTimePeriod;
			Unlimited<uint>? maxCmdletQueueDepth = this.MaxCmdletQueueDepth;
			Unlimited<uint>? exchangeMaxCmdlets = this.ExchangeMaxCmdlets;
			Unlimited<uint>? maxDestructiveCmdlets = this.MaxDestructiveCmdlets;
			Unlimited<uint>? maxDestructiveCmdletsTimePeriod = this.MaxDestructiveCmdletsTimePeriod;
			Unlimited<uint>? maxCmdlets = this.MaxCmdlets;
			Unlimited<uint>? maxRunspaces = this.MaxRunspaces;
			Unlimited<uint>? maxTenantRunspaces = this.MaxTenantRunspaces;
			Unlimited<uint>? maxRunspacesTimePeriod = this.MaxRunspacesTimePeriod;
			Unlimited<uint>? pswsMaxConcurrency = this.PswsMaxConcurrency;
			Unlimited<uint>? pswsMaxRequest = this.PswsMaxRequest;
			Unlimited<uint>? pswsMaxRequestTimePeriod = this.PswsMaxRequestTimePeriod;
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x0002EF2E File Offset: 0x0002D12E
		public static ThrottlingPolicyPowerShellSettings Parse(string stateToParse)
		{
			return new ThrottlingPolicyPowerShellSettings(stateToParse);
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06000F51 RID: 3921 RVA: 0x0002EF36 File Offset: 0x0002D136
		// (set) Token: 0x06000F52 RID: 3922 RVA: 0x0002EF43 File Offset: 0x0002D143
		internal Unlimited<uint>? MaxTenantConcurrency
		{
			get
			{
				return base.GetValueFromPropertyBag("TenantConcur");
			}
			set
			{
				base.SetValueInPropertyBag("TenantConcur", value);
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06000F53 RID: 3923 RVA: 0x0002EF51 File Offset: 0x0002D151
		// (set) Token: 0x06000F54 RID: 3924 RVA: 0x0002EF5E File Offset: 0x0002D15E
		internal Unlimited<uint>? MaxOperations
		{
			get
			{
				return base.GetValueFromPropertyBag("MaxOps");
			}
			set
			{
				base.SetValueInPropertyBag("MaxOps", value);
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x0002EF6C File Offset: 0x0002D16C
		// (set) Token: 0x06000F56 RID: 3926 RVA: 0x0002EF79 File Offset: 0x0002D179
		internal Unlimited<uint>? MaxCmdletsTimePeriod
		{
			get
			{
				return base.GetValueFromPropertyBag("MaxCmdPeriod");
			}
			set
			{
				base.SetValueInPropertyBag("MaxCmdPeriod", value);
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06000F57 RID: 3927 RVA: 0x0002EF87 File Offset: 0x0002D187
		// (set) Token: 0x06000F58 RID: 3928 RVA: 0x0002EF94 File Offset: 0x0002D194
		internal Unlimited<uint>? MaxCmdletQueueDepth
		{
			get
			{
				return base.GetValueFromPropertyBag("MaxCmdQueue");
			}
			set
			{
				base.SetValueInPropertyBag("MaxCmdQueue", value);
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06000F59 RID: 3929 RVA: 0x0002EFA2 File Offset: 0x0002D1A2
		// (set) Token: 0x06000F5A RID: 3930 RVA: 0x0002EFAF File Offset: 0x0002D1AF
		internal Unlimited<uint>? ExchangeMaxCmdlets
		{
			get
			{
				return base.GetValueFromPropertyBag("ExMaxCmd");
			}
			set
			{
				base.SetValueInPropertyBag("ExMaxCmd", value);
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06000F5B RID: 3931 RVA: 0x0002EFBD File Offset: 0x0002D1BD
		// (set) Token: 0x06000F5C RID: 3932 RVA: 0x0002EFCA File Offset: 0x0002D1CA
		internal Unlimited<uint>? MaxDestructiveCmdlets
		{
			get
			{
				return base.GetValueFromPropertyBag("MaxDestruct");
			}
			set
			{
				base.SetValueInPropertyBag("MaxDestruct", value);
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06000F5D RID: 3933 RVA: 0x0002EFD8 File Offset: 0x0002D1D8
		// (set) Token: 0x06000F5E RID: 3934 RVA: 0x0002EFE5 File Offset: 0x0002D1E5
		internal Unlimited<uint>? MaxDestructiveCmdletsTimePeriod
		{
			get
			{
				return base.GetValueFromPropertyBag("MaxDestructPeriod");
			}
			set
			{
				base.SetValueInPropertyBag("MaxDestructPeriod", value);
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06000F5F RID: 3935 RVA: 0x0002EFF3 File Offset: 0x0002D1F3
		// (set) Token: 0x06000F60 RID: 3936 RVA: 0x0002F000 File Offset: 0x0002D200
		internal Unlimited<uint>? MaxCmdlets
		{
			get
			{
				return base.GetValueFromPropertyBag("MaxCmdlet");
			}
			set
			{
				base.SetValueInPropertyBag("MaxCmdlet", value);
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06000F61 RID: 3937 RVA: 0x0002F00E File Offset: 0x0002D20E
		// (set) Token: 0x06000F62 RID: 3938 RVA: 0x0002F01B File Offset: 0x0002D21B
		internal Unlimited<uint>? MaxRunspaces
		{
			get
			{
				return base.GetValueFromPropertyBag("MaxRun");
			}
			set
			{
				base.SetValueInPropertyBag("MaxRun", value);
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06000F63 RID: 3939 RVA: 0x0002F029 File Offset: 0x0002D229
		// (set) Token: 0x06000F64 RID: 3940 RVA: 0x0002F036 File Offset: 0x0002D236
		internal Unlimited<uint>? MaxTenantRunspaces
		{
			get
			{
				return base.GetValueFromPropertyBag("MaxTenantRun");
			}
			set
			{
				base.SetValueInPropertyBag("MaxTenantRun", value);
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06000F65 RID: 3941 RVA: 0x0002F044 File Offset: 0x0002D244
		// (set) Token: 0x06000F66 RID: 3942 RVA: 0x0002F051 File Offset: 0x0002D251
		internal Unlimited<uint>? MaxRunspacesTimePeriod
		{
			get
			{
				return base.GetValueFromPropertyBag("MaxRunPeriod");
			}
			set
			{
				base.SetValueInPropertyBag("MaxRunPeriod", value);
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06000F67 RID: 3943 RVA: 0x0002F05F File Offset: 0x0002D25F
		// (set) Token: 0x06000F68 RID: 3944 RVA: 0x0002F06C File Offset: 0x0002D26C
		internal Unlimited<uint>? PswsMaxConcurrency
		{
			get
			{
				return base.GetValueFromPropertyBag("PswsMaxConn");
			}
			set
			{
				base.SetValueInPropertyBag("PswsMaxConn", value);
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06000F69 RID: 3945 RVA: 0x0002F07A File Offset: 0x0002D27A
		// (set) Token: 0x06000F6A RID: 3946 RVA: 0x0002F087 File Offset: 0x0002D287
		internal Unlimited<uint>? PswsMaxRequest
		{
			get
			{
				return base.GetValueFromPropertyBag("PswsMaxRequest");
			}
			set
			{
				base.SetValueInPropertyBag("PswsMaxRequest", value);
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06000F6B RID: 3947 RVA: 0x0002F095 File Offset: 0x0002D295
		// (set) Token: 0x06000F6C RID: 3948 RVA: 0x0002F0A2 File Offset: 0x0002D2A2
		internal Unlimited<uint>? PswsMaxRequestTimePeriod
		{
			get
			{
				return base.GetValueFromPropertyBag("PswsMaxRequestPeriod");
			}
			set
			{
				base.SetValueInPropertyBag("PswsMaxRequestPeriod", value);
			}
		}

		// Token: 0x04000925 RID: 2341
		private const string TenantConcurrencyPrefix = "TenantConcur";

		// Token: 0x04000926 RID: 2342
		private const string MaxOperationsPrefix = "MaxOps";

		// Token: 0x04000927 RID: 2343
		private const string MaxCmdletsTimePeriodPrefix = "MaxCmdPeriod";

		// Token: 0x04000928 RID: 2344
		private const string ExchangeMaxCmdletsPrefix = "ExMaxCmd";

		// Token: 0x04000929 RID: 2345
		private const string MaxCmdletQueueDepthPrefix = "MaxCmdQueue";

		// Token: 0x0400092A RID: 2346
		private const string MaxDestructiveCmdletsPrefix = "MaxDestruct";

		// Token: 0x0400092B RID: 2347
		private const string MaxDestructiveCmdletsTimePeriodPrefix = "MaxDestructPeriod";

		// Token: 0x0400092C RID: 2348
		private const string MaxCmdletsPrefix = "MaxCmdlet";

		// Token: 0x0400092D RID: 2349
		private const string MaxRunspacesPrefix = "MaxRun";

		// Token: 0x0400092E RID: 2350
		private const string MaxTenantRunspacesPrefix = "MaxTenantRun";

		// Token: 0x0400092F RID: 2351
		private const string MaxRunspacesTimePeriodPrefix = "MaxRunPeriod";

		// Token: 0x04000930 RID: 2352
		private const string PswsMaxConcurrencyPrefix = "PswsMaxConn";

		// Token: 0x04000931 RID: 2353
		private const string PswsMaxRequestPrefix = "PswsMaxRequest";

		// Token: 0x04000932 RID: 2354
		private const string PswsMaxRequestTimePeriodPrefix = "PswsMaxRequestPeriod";
	}
}
