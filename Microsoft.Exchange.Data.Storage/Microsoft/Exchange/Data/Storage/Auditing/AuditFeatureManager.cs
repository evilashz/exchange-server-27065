using System;
using System.IO;
using System.Security;
using System.Threading;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Storage.Auditing
{
	// Token: 0x02000F30 RID: 3888
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class AuditFeatureManager
	{
		// Token: 0x060085C1 RID: 34241 RVA: 0x0024AAA8 File Offset: 0x00248CA8
		public static bool IsPartitionedMailboxLogEnabled(IExchangePrincipal exchangePrincipal)
		{
			if (AuditFeatureManager.partitionedMailboxLogOverride != null)
			{
				return AuditFeatureManager.partitionedMailboxLogOverride();
			}
			if (AuditFeatureManager.isPartitionedMailboxLogEnabled == null)
			{
				bool? flag = AuditFeatureManager.ReadTertiaryValueFromRegistry("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Audit\\Parameters", "PartitionedMailboxLogEnabled");
				if (flag != null)
				{
					AuditFeatureManager.isPartitionedMailboxLogEnabled = flag;
				}
				else
				{
					AuditFeatureManager.isPartitionedMailboxLogEnabled = new bool?(VariantConfiguration.InvariantNoFlightingSnapshot.Ipaed.PartitionedMailboxAuditLogs.Enabled);
				}
			}
			return AuditFeatureManager.isPartitionedMailboxLogEnabled.Value;
		}

		// Token: 0x060085C2 RID: 34242 RVA: 0x0024AB28 File Offset: 0x00248D28
		public static bool IsPartitionedAdminLogEnabled(IExchangePrincipal exchangePrincipal)
		{
			return AuditFeatureManager.IsEnabled(AuditFeatureManager.partitionedAuditLogOverride, AuditFeatureManager.isPartitionedAdminLogEnabled, exchangePrincipal, (VariantConfigurationSnapshot.IpaedSettingsIni settings) => settings.PartitionedAdminAuditLogs);
		}

		// Token: 0x060085C3 RID: 34243 RVA: 0x0024AB58 File Offset: 0x00248D58
		public static bool IsAdminAuditCmdletBlockListEnabled()
		{
			if (AuditFeatureManager.adminAuditCmdletBlockListOverride != null)
			{
				return AuditFeatureManager.adminAuditCmdletBlockListOverride();
			}
			return VariantConfiguration.InvariantNoFlightingSnapshot.Ipaed.AdminAuditCmdletBlockList.Enabled;
		}

		// Token: 0x060085C4 RID: 34244 RVA: 0x0024AB90 File Offset: 0x00248D90
		public static bool IsAdminAuditEventLogThrottlingEnabled()
		{
			if (AuditFeatureManager.AdminAuditEventLogThrottlingOverride != null)
			{
				return AuditFeatureManager.AdminAuditEventLogThrottlingOverride();
			}
			return VariantConfiguration.InvariantNoFlightingSnapshot.Ipaed.AdminAuditEventLogThrottling.Enabled;
		}

		// Token: 0x060085C5 RID: 34245 RVA: 0x0024ABCF File Offset: 0x00248DCF
		public static bool IsAdminAuditLocalQueueEnabled(IExchangePrincipal exchangePrincipal)
		{
			return AuditFeatureManager.IsEnabled(AuditFeatureManager.AdminAuditLocalQueueOverride, AuditFeatureManager.isAdminAuditLocalQueueEnabled, exchangePrincipal, (VariantConfigurationSnapshot.IpaedSettingsIni settings) => settings.AdminAuditLocalQueue);
		}

		// Token: 0x060085C6 RID: 34246 RVA: 0x0024AC07 File Offset: 0x00248E07
		public static bool IsMailboxAuditLocalQueueEnabled(IExchangePrincipal exchangePrincipal)
		{
			return AuditFeatureManager.IsEnabled(AuditFeatureManager.MailboxAuditLocalQueueOverride, AuditFeatureManager.isMailboxAuditLocalQueueEnabled, exchangePrincipal, (VariantConfigurationSnapshot.IpaedSettingsIni settings) => settings.MailboxAuditLocalQueue);
		}

		// Token: 0x060085C7 RID: 34247 RVA: 0x0024AC38 File Offset: 0x00248E38
		public static bool IsExternalAccessCheckOnDedicatedEnabled()
		{
			if (AuditFeatureManager.ExternalAccessCheckOnDedicatedEnabledOverride != null)
			{
				return AuditFeatureManager.ExternalAccessCheckOnDedicatedEnabledOverride();
			}
			return VariantConfiguration.InvariantNoFlightingSnapshot.Ipaed.AdminAuditExternalAccessCheckOnDedicated.Enabled;
		}

		// Token: 0x060085C8 RID: 34248 RVA: 0x0024AC70 File Offset: 0x00248E70
		public static bool IsAuditConfigFromUCCPolicyEnabled(MailboxSession mailboxSession, IExchangePrincipal exchangePrincipal)
		{
			if (AuditFeatureManager.AuditConfigFromUCCPolicyOverride != null)
			{
				return AuditFeatureManager.AuditConfigFromUCCPolicyOverride();
			}
			if (AuditFeatureManager.isAuditConfigFromUCCPolicyInRegistry)
			{
				bool? flag = AuditFeatureManager.ReadTertiaryValueFromRegistry("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Audit\\Parameters", "AuditConfigFromUCCPolicyEnabled");
				AuditFeatureManager.isAuditConfigFromUCCPolicyInRegistry = (flag != null);
				if (flag != null)
				{
					return flag.Value;
				}
			}
			if (mailboxSession != null)
			{
				return mailboxSession.IsAuditConfigFromUCCPolicyEnabled;
			}
			if (exchangePrincipal != null)
			{
				VariantConfigurationSnapshot configuration = exchangePrincipal.GetConfiguration();
				if (configuration != null)
				{
					return configuration.Ipaed.AuditConfigFromUCCPolicy.Enabled;
				}
			}
			return false;
		}

		// Token: 0x060085C9 RID: 34249 RVA: 0x0024ACF0 File Offset: 0x00248EF0
		public static bool IsFolderBindExtendedThrottlingEnabled()
		{
			if (AuditFeatureManager.folderBindExtendedThrottlingOverride != null)
			{
				return AuditFeatureManager.folderBindExtendedThrottlingOverride();
			}
			return VariantConfiguration.InvariantNoFlightingSnapshot.Ipaed.FolderBindExtendedThrottling.Enabled;
		}

		// Token: 0x060085CA RID: 34250 RVA: 0x0024AD28 File Offset: 0x00248F28
		private static AuditFeatureManager.Tertiary IsFeatureEnabled(string registrySubkeyPath, string valueName)
		{
			AuditFeatureManager.Tertiary result = AuditFeatureManager.Tertiary.Unknown;
			try
			{
				object value = RegistryReader.Instance.GetValue<object>(Registry.LocalMachine, registrySubkeyPath, valueName, AuditFeatureManager.sentinelDefault);
				if (value != null && value is int)
				{
					result = (((int)value > 0) ? AuditFeatureManager.Tertiary.True : AuditFeatureManager.Tertiary.False);
				}
			}
			catch (IOException)
			{
			}
			catch (SecurityException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
			return result;
		}

		// Token: 0x060085CB RID: 34251 RVA: 0x0024AD9C File Offset: 0x00248F9C
		private static bool? ReadTertiaryValueFromRegistry(string registrySubkeyPath, string valueName)
		{
			switch (AuditFeatureManager.IsFeatureEnabled(registrySubkeyPath, valueName))
			{
			case AuditFeatureManager.Tertiary.False:
				return new bool?(false);
			case AuditFeatureManager.Tertiary.True:
				return new bool?(true);
			default:
				return null;
			}
		}

		// Token: 0x060085CC RID: 34252 RVA: 0x0024ADD8 File Offset: 0x00248FD8
		private static bool IsEnabled(Func<bool> overrideCallback, Lazy<bool?> cachedValue, IExchangePrincipal exchangePrincipal, Func<VariantConfigurationSnapshot.IpaedSettingsIni, IFeature> getFlightFeature)
		{
			if (overrideCallback != null)
			{
				return overrideCallback();
			}
			bool? value = cachedValue.Value;
			bool result;
			if (value != null)
			{
				result = value.Value;
			}
			else
			{
				VariantConfigurationSnapshot configuration = exchangePrincipal.GetConfiguration();
				result = getFlightFeature(configuration.Ipaed).Enabled;
			}
			return result;
		}

		// Token: 0x060085CD RID: 34253 RVA: 0x0024AE34 File Offset: 0x00249034
		internal static void TestOnlyResetPartitionedAdminLogEnabled()
		{
			AuditFeatureManager.isPartitionedAdminLogEnabled = new Lazy<bool?>(() => AuditFeatureManager.ReadTertiaryValueFromRegistry("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Audit\\Parameters", "PartitionedAdminLogEnabled"), LazyThreadSafetyMode.ExecutionAndPublication);
		}

		// Token: 0x04005983 RID: 22915
		public const string AuditKeyBase = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Audit\\Parameters";

		// Token: 0x04005984 RID: 22916
		public const string PartitionedMailboxLogEnabled = "PartitionedMailboxLogEnabled";

		// Token: 0x04005985 RID: 22917
		public const string PartitionedAdminLogEnabled = "PartitionedAdminLogEnabled";

		// Token: 0x04005986 RID: 22918
		public const string AdminAuditLocalQueueEnabled = "AdminAuditLocalQueueEnabled";

		// Token: 0x04005987 RID: 22919
		public const string MailboxAuditLocalQueueEnabled = "MailboxAuditLocalQueueEnabled";

		// Token: 0x04005988 RID: 22920
		public const string AuditConfigFromUCCPolicyEnabled = "AuditConfigFromUCCPolicyEnabled";

		// Token: 0x04005989 RID: 22921
		private static Func<bool> partitionedMailboxLogOverride = null;

		// Token: 0x0400598A RID: 22922
		private static Func<bool> partitionedAuditLogOverride = null;

		// Token: 0x0400598B RID: 22923
		private static Func<bool> adminAuditCmdletBlockListOverride = null;

		// Token: 0x0400598C RID: 22924
		private static Func<bool> AdminAuditEventLogThrottlingOverride = null;

		// Token: 0x0400598D RID: 22925
		private static Func<bool> ExternalAccessCheckOnDedicatedEnabledOverride = null;

		// Token: 0x0400598E RID: 22926
		private static Func<bool> AdminAuditLocalQueueOverride = null;

		// Token: 0x0400598F RID: 22927
		private static Func<bool> MailboxAuditLocalQueueOverride = null;

		// Token: 0x04005990 RID: 22928
		private static Func<bool> AuditConfigFromUCCPolicyOverride = null;

		// Token: 0x04005991 RID: 22929
		private static Func<bool> folderBindExtendedThrottlingOverride = null;

		// Token: 0x04005992 RID: 22930
		private static bool? isPartitionedMailboxLogEnabled = null;

		// Token: 0x04005993 RID: 22931
		private static Lazy<bool?> isPartitionedAdminLogEnabled = new Lazy<bool?>(() => AuditFeatureManager.ReadTertiaryValueFromRegistry("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Audit\\Parameters", "PartitionedAdminLogEnabled"), LazyThreadSafetyMode.ExecutionAndPublication);

		// Token: 0x04005994 RID: 22932
		private static Lazy<bool?> isAdminAuditLocalQueueEnabled = new Lazy<bool?>(() => AuditFeatureManager.ReadTertiaryValueFromRegistry("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Audit\\Parameters", "AdminAuditLocalQueueEnabled"), LazyThreadSafetyMode.ExecutionAndPublication);

		// Token: 0x04005995 RID: 22933
		private static Lazy<bool?> isMailboxAuditLocalQueueEnabled = new Lazy<bool?>(() => AuditFeatureManager.ReadTertiaryValueFromRegistry("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Audit\\Parameters", "MailboxAuditLocalQueueEnabled"), LazyThreadSafetyMode.ExecutionAndPublication);

		// Token: 0x04005996 RID: 22934
		private static bool isAuditConfigFromUCCPolicyInRegistry = true;

		// Token: 0x04005997 RID: 22935
		private static readonly object sentinelDefault = new object();

		// Token: 0x02000F31 RID: 3889
		private enum Tertiary
		{
			// Token: 0x040059A0 RID: 22944
			False,
			// Token: 0x040059A1 RID: 22945
			True,
			// Token: 0x040059A2 RID: 22946
			Unknown
		}
	}
}
