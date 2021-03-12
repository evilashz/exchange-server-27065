using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Auditing;
using Microsoft.Exchange.Data.Storage.UnifiedPolicy;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ProvisioningAgent;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x02000015 RID: 21
	internal sealed class ConfigWrapper : ILifetimeTrackable
	{
		// Token: 0x0600008B RID: 139 RVA: 0x00004DAA File Offset: 0x00002FAA
		internal ConfigWrapper(IConfigurationPolicy configurationPolicy, LogMessageDelegate logMessage) : this(configurationPolicy, DateTime.UtcNow, logMessage)
		{
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004DBC File Offset: 0x00002FBC
		internal ConfigWrapper(IConfigurationPolicy configurationPolicy, DateTime createTime, LogMessageDelegate logMessage)
		{
			this.configurationPolicy = configurationPolicy;
			this.CreateTime = createTime;
			logMessage(Strings.EnteredInitializeConfig);
			if (ExTraceGlobals.AdminAuditLogTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.AdminAuditLogTracer.TraceDebug<ConfigWrapper>((long)this.GetHashCode(), "{0} Initializing adminAuditLogConfig.", this);
			}
			this.AdminAuditLogConfig = configurationPolicy.GetAdminAuditLogConfig();
			if (this.IsAuditConfigFromUCCPolicyEnabled())
			{
				AuditPolicyCacheEntry auditPolicyCacheEntry;
				AuditPolicyUtility.RetrieveAuditPolicy(configurationPolicy.OrganizationId, out auditPolicyCacheEntry);
				this.isAuditPolicyAvailable = (auditPolicyCacheEntry != null && auditPolicyCacheEntry.IsExist());
			}
			else
			{
				this.isAuditPolicyAvailable = false;
			}
			if (this.isAuditPolicyAvailable)
			{
				this.ParameterWildcardMatcher = new WildcardMatcher(ConfigWrapper.defaultAdminAuditLogParameters);
				this.CmdletWildcardMatcher = new WildcardMatcher(ConfigWrapper.defaultAdminAuditLogCmdlets);
				this.ExcludedCmdletWildcardMatcher = new WildcardMatcher(ConfigWrapper.defaultAdminAuditLogExcludedCmdlets);
			}
			else if (this.AdminAuditLogConfig != null)
			{
				this.ParameterWildcardMatcher = new WildcardMatcher(this.AdminAuditLogConfig.AdminAuditLogParameters);
				this.CmdletWildcardMatcher = new WildcardMatcher(this.AdminAuditLogConfig.AdminAuditLogCmdlets);
				this.ExcludedCmdletWildcardMatcher = new WildcardMatcher(this.AdminAuditLogConfig.AdminAuditLogExcludedCmdlets);
			}
			logMessage(Strings.ExitedInitializeConfig);
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00004EE1 File Offset: 0x000030E1
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00004EE9 File Offset: 0x000030E9
		public IAdminAuditLogConfig AdminAuditLogConfig { get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00004EF2 File Offset: 0x000030F2
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00004EFA File Offset: 0x000030FA
		public WildcardMatcher ParameterWildcardMatcher { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00004F03 File Offset: 0x00003103
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00004F0B File Offset: 0x0000310B
		public WildcardMatcher CmdletWildcardMatcher { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00004F14 File Offset: 0x00003114
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00004F1C File Offset: 0x0000311C
		public WildcardMatcher ExcludedCmdletWildcardMatcher { get; private set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00004F25 File Offset: 0x00003125
		public bool LoggingEnabled
		{
			get
			{
				return this.isAuditPolicyAvailable || (this.AdminAuditLogConfig != null && this.AdminAuditLogConfig.AdminAuditLogEnabled && (this.ArbitrationMailboxStatus != ArbitrationMailboxStatus.R4 || this.AdminAuditLogConfig.IsValidAuditLogMailboxAddress));
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00004F5D File Offset: 0x0000315D
		public bool LoggingCapable
		{
			get
			{
				return this.ArbitrationMailboxStatus != ArbitrationMailboxStatus.R4 || (this.AdminAuditLogConfig != null && this.AdminAuditLogConfig.IsValidAuditLogMailboxAddress);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00004F7E File Offset: 0x0000317E
		public ArbitrationMailboxStatus ArbitrationMailboxStatus
		{
			get
			{
				if (this.status == null)
				{
					this.status = new ArbitrationMailboxStatus?(this.configurationPolicy.CheckArbitrationMailboxStatus(out this.initialError));
				}
				return this.status.Value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00004FB4 File Offset: 0x000031B4
		public Exception Error
		{
			get
			{
				return this.initialError;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00004FBC File Offset: 0x000031BC
		public IAuditLog MailboxLogger
		{
			get
			{
				EnumValidator.ThrowIfInvalid<ArbitrationMailboxStatus>(this.ArbitrationMailboxStatus, ConfigWrapper.validStatusValues);
				if (this.auditLog == null)
				{
					this.auditLog = this.configurationPolicy.CreateLogger(this.ArbitrationMailboxStatus);
				}
				return this.auditLog;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00004FF3 File Offset: 0x000031F3
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00004FFB File Offset: 0x000031FB
		public DateTime CreateTime { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00005004 File Offset: 0x00003204
		// (set) Token: 0x0600009D RID: 157 RVA: 0x0000500C File Offset: 0x0000320C
		public DateTime LastAccessTime { get; set; }

		// Token: 0x0600009E RID: 158 RVA: 0x0000502C File Offset: 0x0000322C
		public bool ShouldLogBasedOnCmdletName(string cmdletName)
		{
			if (this.configurationPolicy.RunningOnDataCenter)
			{
				if (Array.FindIndex<string>(ConfigWrapper.MRSInternalCmdlets, (string x) => string.Equals(x, cmdletName, StringComparison.OrdinalIgnoreCase)) != -1)
				{
					if (ExTraceGlobals.AdminAuditLogTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.AdminAuditLogTracer.TraceDebug<ConfigWrapper, string>((long)this.GetHashCode(), "{0} Cmdlet name '{1}' is one of mrs internal hidden cmdlets. Skip logging", this, cmdletName);
					}
					return false;
				}
			}
			if (this.ExcludedCmdletWildcardMatcher != null && this.ExcludedCmdletWildcardMatcher.IsMatch(cmdletName))
			{
				if (ExTraceGlobals.AdminAuditLogTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.AdminAuditLogTracer.TraceDebug<ConfigWrapper, string, WildcardMatcher>((long)this.GetHashCode(), "{0} Cmdlet name '{1}' matches the exclusion setting '{2}'. Skip logging", this, cmdletName, this.ExcludedCmdletWildcardMatcher);
				}
				return false;
			}
			if ((this.AdminAuditLogConfig == null || !this.AdminAuditLogConfig.TestCmdletLoggingEnabled) && ConfigWrapper.TestCmdletWildcardMatcher.IsMatch(cmdletName))
			{
				if (ExTraceGlobals.AdminAuditLogTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.AdminAuditLogTracer.TraceDebug<ConfigWrapper, string>((long)this.GetHashCode(), "{0} Cmdlet name '{1}' is a test cmdlet and TestCmdletLoggingEnabled is false. Skip logging", this, cmdletName);
				}
				return false;
			}
			if (this.CmdletWildcardMatcher == null || !this.CmdletWildcardMatcher.IsMatch(cmdletName))
			{
				if (ExTraceGlobals.AdminAuditLogTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.AdminAuditLogTracer.TraceDebug<ConfigWrapper, string, WildcardMatcher>((long)this.GetHashCode(), "{0} Cmdlet name '{1}' does not match settings '{2}'. Skip logging.", this, cmdletName, this.CmdletWildcardMatcher);
				}
				return false;
			}
			if (ExTraceGlobals.AdminAuditLogTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.AdminAuditLogTracer.TraceDebug<ConfigWrapper, string>((long)this.GetHashCode(), "{0} Cmdlet name '{1}' matches settings.", this, cmdletName);
			}
			return true;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000051B8 File Offset: 0x000033B8
		public bool CmdletAlwaysLogged(string cmdletName)
		{
			bool flag = string.Equals(cmdletName, "Set-AdminAuditLogConfig", StringComparison.OrdinalIgnoreCase);
			return flag && this.LoggingCapable;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000051DD File Offset: 0x000033DD
		internal void ResetLogMailboxStatus()
		{
			this.status = null;
			this.initialError = null;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000051F4 File Offset: 0x000033F4
		private bool IsAuditConfigFromUCCPolicyEnabled()
		{
			Exception ex;
			this.configurationPolicy.CheckArbitrationMailboxStatus(out ex);
			return AuditFeatureManager.IsAuditConfigFromUCCPolicyEnabled(null, this.configurationPolicy.ExchangePrincipal);
		}

		// Token: 0x04000050 RID: 80
		private static ArbitrationMailboxStatus[] validStatusValues = new ArbitrationMailboxStatus[]
		{
			ArbitrationMailboxStatus.R4,
			ArbitrationMailboxStatus.R5,
			ArbitrationMailboxStatus.E15,
			ArbitrationMailboxStatus.FFO
		};

		// Token: 0x04000051 RID: 81
		private static readonly MultiValuedProperty<string> defaultAdminAuditLogCmdlets = new MultiValuedProperty<string>("*");

		// Token: 0x04000052 RID: 82
		private static readonly MultiValuedProperty<string> defaultAdminAuditLogParameters = new MultiValuedProperty<string>("*");

		// Token: 0x04000053 RID: 83
		private static readonly MultiValuedProperty<string> defaultAdminAuditLogExcludedCmdlets = new MultiValuedProperty<string>();

		// Token: 0x04000054 RID: 84
		private IConfigurationPolicy configurationPolicy;

		// Token: 0x04000055 RID: 85
		private static readonly WildcardMatcher TestCmdletWildcardMatcher = new WildcardMatcher("Test-*");

		// Token: 0x04000056 RID: 86
		private static readonly string[] MRSInternalCmdlets = new string[]
		{
			"Update-MovedMailbox"
		};

		// Token: 0x04000057 RID: 87
		private IAuditLog auditLog;

		// Token: 0x04000058 RID: 88
		private ArbitrationMailboxStatus? status;

		// Token: 0x04000059 RID: 89
		private Exception initialError;

		// Token: 0x0400005A RID: 90
		private readonly bool isAuditPolicyAvailable;
	}
}
