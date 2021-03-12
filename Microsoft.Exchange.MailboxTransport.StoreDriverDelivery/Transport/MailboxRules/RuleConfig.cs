using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.MailboxRules;

namespace Microsoft.Exchange.Transport.MailboxRules
{
	// Token: 0x02000091 RID: 145
	internal class RuleConfig : IRuleConfig
	{
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x0001A035 File Offset: 0x00018235
		public static RuleConfig Instance
		{
			get
			{
				return RuleConfig.instance;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x0001A03C File Offset: 0x0001823C
		public object SCLJunkThreshold
		{
			get
			{
				return this.sclJunkThreshold;
			}
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0001A044 File Offset: 0x00018244
		public static void Load()
		{
			lock (RuleConfig.configLock)
			{
				GlobalConfigurationBase<UceContentFilter, RuleConfig.UceContentFilterConfiguration>.Start();
			}
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0001A084 File Offset: 0x00018284
		public static void UnLoad()
		{
			lock (RuleConfig.configLock)
			{
				GlobalConfigurationBase<UceContentFilter, RuleConfig.UceContentFilterConfiguration>.Stop();
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0001A0C4 File Offset: 0x000182C4
		internal static void SetSCLJunkThreshold(int threshold)
		{
			RuleConfig.instance.sclJunkThreshold = threshold;
		}

		// Token: 0x040002BE RID: 702
		private const int DefaultSCLJunkThreshold = 4;

		// Token: 0x040002BF RID: 703
		private static object configLock = new object();

		// Token: 0x040002C0 RID: 704
		private static RuleConfig instance = new RuleConfig();

		// Token: 0x040002C1 RID: 705
		private object sclJunkThreshold = 4;

		// Token: 0x02000092 RID: 146
		private sealed class UceContentFilterConfiguration : GlobalConfigurationBase<UceContentFilter, RuleConfig.UceContentFilterConfiguration>
		{
			// Token: 0x1700016F RID: 367
			// (get) Token: 0x060004FE RID: 1278 RVA: 0x0001A100 File Offset: 0x00018300
			protected override string ConfigObjectName
			{
				get
				{
					return "UceContentFilter";
				}
			}

			// Token: 0x17000170 RID: 368
			// (get) Token: 0x060004FF RID: 1279 RVA: 0x0001A107 File Offset: 0x00018307
			protected override string ReloadFailedString
			{
				get
				{
					return "Failed to load UceContentFilter config";
				}
			}

			// Token: 0x06000500 RID: 1280 RVA: 0x0001A110 File Offset: 0x00018310
			protected override ADObjectId GetObjectId(IConfigurationSession session)
			{
				ADObjectId relativePath = new ADObjectId("CN=UCE Content Filter,CN=Message Delivery,CN=Global Settings");
				return session.GetOrgContainerId().GetDescendantId(relativePath);
			}

			// Token: 0x06000501 RID: 1281 RVA: 0x0001A134 File Offset: 0x00018334
			protected override void HandleObjectLoaded()
			{
				RuleConfig.SetSCLJunkThreshold(base.ConfigObject.SCLJunkThreshold);
			}

			// Token: 0x06000502 RID: 1282 RVA: 0x0001A146 File Offset: 0x00018346
			protected override bool HandleObjectNotFound()
			{
				RuleConfig.SetSCLJunkThreshold(4);
				return true;
			}
		}
	}
}
