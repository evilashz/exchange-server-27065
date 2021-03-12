using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics.Components.Security;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Security.Authentication.FederatedAuthService
{
	// Token: 0x0200004D RID: 77
	internal abstract class AccountValidationContextBase : IAccountValidationContext
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000DAED File Offset: 0x0000BCED
		// (set) Token: 0x0600020C RID: 524 RVA: 0x0000DAF5 File Offset: 0x0000BCF5
		public string ApplicationName { get; protected set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000DAFE File Offset: 0x0000BCFE
		// (set) Token: 0x0600020E RID: 526 RVA: 0x0000DB06 File Offset: 0x0000BD06
		public ExDateTime AccountAuthTime { get; protected set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000DB0F File Offset: 0x0000BD0F
		// (set) Token: 0x06000210 RID: 528 RVA: 0x0000DB17 File Offset: 0x0000BD17
		public OrganizationId OrgId { get; protected set; }

		// Token: 0x06000211 RID: 529 RVA: 0x0000DB20 File Offset: 0x0000BD20
		public AccountValidationContextBase(OrganizationId orgId, ExDateTime accountAuthTime, string appName)
		{
			this.OrgId = orgId;
			this.AccountAuthTime = accountAuthTime;
			this.ApplicationName = appName;
			this.protocolName = this.GetProtocolNameFromAppName(appName);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000DB4A File Offset: 0x0000BD4A
		internal void SetOrgId(OrganizationId orgId)
		{
			if (orgId == null)
			{
				throw new ArgumentNullException("OrgId");
			}
			this.OrgId = orgId;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000DB67 File Offset: 0x0000BD67
		internal void SetAppName(string appName)
		{
			if (string.IsNullOrEmpty(appName))
			{
				throw new ArgumentNullException("appName");
			}
			this.ApplicationName = appName;
			this.protocolName = this.GetProtocolNameFromAppName(appName);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000DB90 File Offset: 0x0000BD90
		public virtual AccountState CheckAccount()
		{
			if (!ConfigBase<AdDriverConfigSchema>.GetConfig<bool>("AccountValidationEnabled"))
			{
				return AccountState.AccountEnabled;
			}
			if (this.OrgId == null)
			{
				throw new ArgumentNullException("OrgId");
			}
			if (this.protocolName == ProtocolName.Unknown)
			{
				throw new ArgumentException("ProtocolName is unknown, cannot check Protocol Settings");
			}
			ExDateTime accountAuthTime = this.AccountAuthTime;
			AccountState result;
			if (Enum.TryParse<AccountState>(Globals.GetValueFromRegistry<int>("SOFTWARE\\Microsoft\\ExchangeServer\\v15", "AccountState", 0, ExTraceGlobals.AuthenticationTracer).ToString(), true, out result))
			{
				return result;
			}
			return AccountState.AccountEnabled;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000DC08 File Offset: 0x0000BE08
		private ProtocolName GetProtocolNameFromAppName(string appName)
		{
			if (string.IsNullOrEmpty(appName))
			{
				return ProtocolName.Unknown;
			}
			string value = appName.Split(new char[]
			{
				'.'
			})[2];
			ProtocolName result;
			if (Enum.TryParse<ProtocolName>(value, true, out result))
			{
				return result;
			}
			return ProtocolName.Unknown;
		}

		// Token: 0x040001F1 RID: 497
		public const string AccountValidationContextKey = "AccountValidationContext";

		// Token: 0x040001F2 RID: 498
		internal const string RegistryPath = "SOFTWARE\\Microsoft\\ExchangeServer\\v15";

		// Token: 0x040001F3 RID: 499
		internal const string AccountStateRegistryKey = "AccountState";

		// Token: 0x040001F4 RID: 500
		private ProtocolName protocolName;
	}
}
