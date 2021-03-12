using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant.UMGrammarGenerator
{
	// Token: 0x020001B5 RID: 437
	internal abstract class UMGrammarTenantCache
	{
		// Token: 0x06001109 RID: 4361 RVA: 0x000631BC File Offset: 0x000613BC
		protected UMGrammarTenantCache()
		{
			this.syncTenantsNeedingGrammars = new Dictionary<Guid, Dictionary<Guid, OrganizationId>>();
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x0600110A RID: 4362 RVA: 0x000631DC File Offset: 0x000613DC
		public static UMGrammarTenantCache Instance
		{
			get
			{
				if (UMGrammarTenantCache.syncTenantCache == null)
				{
					lock (UMGrammarTenantCache.staticLock)
					{
						if (UMGrammarTenantCache.syncTenantCache == null)
						{
							if (VariantConfiguration.InvariantNoFlightingSnapshot.UM.DatacenterUMGrammarTenantCache.Enabled)
							{
								UMTracer.DebugTrace("UMGrammarTenantCache.Instance - Microsoft Hosted topology", new object[0]);
								UMGrammarTenantCache.syncTenantCache = new DatacenterUMGrammarTenantCache();
							}
							else if (Datacenter.IsPartnerHostedOnly(true))
							{
								UMTracer.DebugTrace("UMGrammarTenantCache.Instance - Partner Hosted topology", new object[0]);
								UMGrammarTenantCache.syncTenantCache = new PartnerHostedUMGrammarTenantCache();
							}
							else
							{
								UMTracer.DebugTrace("UMGrammarTenantCache.Instance - Enterprise topology", new object[0]);
								UMGrammarTenantCache.syncTenantCache = new EnterpriseUMGrammarTenantCache();
							}
						}
					}
				}
				return UMGrammarTenantCache.syncTenantCache;
			}
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x000632A0 File Offset: 0x000614A0
		public CultureInfo[] GetGrammarCultures()
		{
			UMTracer.DebugTrace("UMGrammarTenantCache.GetGrammarCultures", new object[0]);
			CultureInfo[] array;
			lock (this.instanceLock)
			{
				array = new CultureInfo[this.syncGrammarCultures.Count];
				this.syncGrammarCultures.CopyTo(array);
			}
			return array;
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x00063308 File Offset: 0x00061508
		public int GetGrammarCultureCount()
		{
			UMTracer.DebugTrace("UMGrammarTenantCache.GetGrammarCultureCount", new object[0]);
			int count;
			lock (this.instanceLock)
			{
				count = this.syncGrammarCultures.Count;
			}
			return count;
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x00063360 File Offset: 0x00061560
		public List<DirectoryProcessorMailboxData> GetMailboxesNeedingGrammars(Guid databaseGuid)
		{
			UMTracer.DebugTrace("UMGrammarTenantCache.GetMailboxesNeedingGrammars for database {0}", new object[]
			{
				databaseGuid
			});
			List<DirectoryProcessorMailboxData> list = new List<DirectoryProcessorMailboxData>(10);
			lock (this.instanceLock)
			{
				if (this.syncTenantsNeedingGrammars.ContainsKey(databaseGuid))
				{
					Dictionary<Guid, OrganizationId> dictionary = this.syncTenantsNeedingGrammars[databaseGuid];
					foreach (KeyValuePair<Guid, OrganizationId> keyValuePair in dictionary)
					{
						list.Add(new DirectoryProcessorMailboxData(keyValuePair.Value, databaseGuid, keyValuePair.Key));
					}
				}
			}
			return list;
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x00063430 File Offset: 0x00061630
		public void Update(Guid databaseGuid)
		{
			UMTracer.DebugTrace("UMGrammarTenantCache.Update for database {0}", new object[]
			{
				databaseGuid
			});
			List<ADUser> allSystemMailboxes = this.GetAllSystemMailboxes(databaseGuid);
			Dictionary<Guid, OrganizationId> tenantsNeedingGrammars = this.GetTenantsNeedingGrammars(allSystemMailboxes);
			HashSet<CultureInfo> hashSet = this.InternalGetGrammarCultures();
			lock (this.instanceLock)
			{
				this.syncTenantsNeedingGrammars[databaseGuid] = tenantsNeedingGrammars;
				this.syncGrammarCultures = hashSet;
			}
		}

		// Token: 0x0600110F RID: 4367
		protected abstract Dictionary<Guid, OrganizationId> GetTenantsNeedingGrammars(List<ADUser> systemMailboxes);

		// Token: 0x06001110 RID: 4368
		protected abstract HashSet<CultureInfo> InternalGetGrammarCultures();

		// Token: 0x06001111 RID: 4369 RVA: 0x000634E0 File Offset: 0x000616E0
		private List<ADUser> GetAllSystemMailboxes(Guid databaseGuid)
		{
			UMTracer.DebugTrace("UMGrammarTenantCache.GetAllSystemMailboxes for database '{0}'", new object[]
			{
				databaseGuid
			});
			List<ADUser> systemMailboxes = new List<ADUser>();
			Exception ex = Utilities.RunSafeADOperation(ExTraceGlobals.UMGrammarGeneratorTracer, delegate
			{
				systemMailboxes.AddRange(OrganizationMailbox.FindByDatabaseId(OrganizationCapability.UMGrammar, new ADObjectId(databaseGuid)));
			}, "GetAllSystemMailboxes: Getting all system mailboxes in a given database");
			if (ex != null)
			{
				UMTracer.ErrorTrace("GetAllSystemMailboxes: Failed in AD operation for database '{0}'. Error='{1}'", new object[]
				{
					databaseGuid.ToString(),
					ex
				});
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_GrammarGenerationCouldntFindSystemMailbox, null, new object[]
				{
					databaseGuid.ToString(),
					CommonUtil.ToEventLogString(ex)
				});
			}
			return systemMailboxes;
		}

		// Token: 0x04000AB3 RID: 2739
		private static object staticLock = new object();

		// Token: 0x04000AB4 RID: 2740
		private static readonly Trace Tracer = ExTraceGlobals.UMGrammarGeneratorTracer;

		// Token: 0x04000AB5 RID: 2741
		private static UMGrammarTenantCache syncTenantCache;

		// Token: 0x04000AB6 RID: 2742
		private object instanceLock = new object();

		// Token: 0x04000AB7 RID: 2743
		private Dictionary<Guid, Dictionary<Guid, OrganizationId>> syncTenantsNeedingGrammars;

		// Token: 0x04000AB8 RID: 2744
		private HashSet<CultureInfo> syncGrammarCultures;
	}
}
