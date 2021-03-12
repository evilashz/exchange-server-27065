using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant.UMGrammarGenerator
{
	// Token: 0x020001C2 RID: 450
	internal class UserGrammarGenerator : IGrammarGeneratorInterface
	{
		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001164 RID: 4452 RVA: 0x00065EE0 File Offset: 0x000640E0
		// (set) Token: 0x06001165 RID: 4453 RVA: 0x00065EE8 File Offset: 0x000640E8
		private Logger Logger { get; set; }

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001166 RID: 4454 RVA: 0x00065EF1 File Offset: 0x000640F1
		// (set) Token: 0x06001167 RID: 4455 RVA: 0x00065EF9 File Offset: 0x000640F9
		private OrganizationId OrgId { get; set; }

		// Token: 0x06001168 RID: 4456 RVA: 0x00065F02 File Offset: 0x00064102
		public UserGrammarGenerator(Logger logger, OrganizationId orgId)
		{
			this.Logger = logger;
			this.Logger.TraceDebug(this, "Entering UserGrammarGenerator constructor", new object[0]);
			this.OrgId = orgId;
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x00065FBC File Offset: 0x000641BC
		public List<DirectoryGrammar> GetGrammarList()
		{
			this.Logger.TraceDebug(this, "Entering UserGrammarGenerator.GetGrammarList", new object[0]);
			List<DirectoryGrammar> list = new List<DirectoryGrammar>();
			HashSet<Guid> addressListGuids = new HashSet<Guid>();
			this.Logger.TraceDebug(this, "Adding GAL grammar", new object[0]);
			list.Add(new GalUserGrammar());
			Exception ex = Utilities.RunSafeADOperation(ExTraceGlobals.UMGrammarGeneratorTracer, delegate
			{
				IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(this.OrgId);
				iadsystemConfigurationLookup.GetGlobalAddressLists(addressListGuids);
			}, "UserGrammarGenerator.GetGrammarList - Getting gals");
			if (ex != null)
			{
				this.Logger.TraceError(this, "Couldn't get list of gals. Error='{0}'", new object[]
				{
					ex
				});
			}
			IEnumerable<Guid> dialPlanGuids = null;
			ex = Utilities.RunSafeADOperation(ExTraceGlobals.UMGrammarGeneratorTracer, delegate
			{
				IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(this.OrgId);
				dialPlanGuids = iadsystemConfigurationLookup.GetAutoAttendantDialPlans();
			}, "UserGrammarGenerator.GetGrammarList - Getting dial plan of all auto attendants");
			if (ex == null)
			{
				using (IEnumerator<Guid> enumerator = dialPlanGuids.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Guid guid = enumerator.Current;
						this.Logger.TraceDebug(this, "Adding dial plan grammar for '{0}'", new object[]
						{
							guid
						});
						list.Add(new DialPlanGrammar(guid));
					}
					goto IL_147;
				}
			}
			this.Logger.TraceError(this, "Couldn't get list of auto attendant dial plans. Error='{0}'", new object[]
			{
				ex
			});
			IL_147:
			if (VariantConfiguration.InvariantNoFlightingSnapshot.UM.AddressListGrammars.Enabled)
			{
				ex = Utilities.RunSafeADOperation(ExTraceGlobals.UMGrammarGeneratorTracer, delegate
				{
					IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(this.OrgId);
					iadsystemConfigurationLookup.GetAutoAttendantAddressLists(addressListGuids);
				}, "UserGrammarGenerator.GetGrammarList - Getting address lists of all auto attendants");
				if (ex != null)
				{
					this.Logger.TraceError(this, "Couldn't get list of auto attendant address lists. Error='{0}'", new object[]
					{
						ex
					});
				}
			}
			foreach (Guid guid2 in addressListGuids)
			{
				this.Logger.TraceDebug(this, "Adding grammar for address list '{0}'", new object[]
				{
					guid2
				});
				list.Add(new AddressListGrammar(guid2));
			}
			this.ValidateGrammarFiles(list);
			return list;
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x0600116A RID: 4458 RVA: 0x000661F8 File Offset: 0x000643F8
		public string ADEntriesFileName
		{
			get
			{
				return "User";
			}
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x00066200 File Offset: 0x00064400
		internal void ValidateGrammarFiles(List<DirectoryGrammar> grammars)
		{
			if (VariantConfiguration.InvariantNoFlightingSnapshot.UM.DirectoryGrammarCountLimit.Enabled && grammars.Count > 150)
			{
				this.Logger.TraceDebug(this, "Too many grammar files ({0}) for tenant '{1}'", new object[]
				{
					grammars.Count,
					this.Logger.RunData.TenantId
				});
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_GrammarFileMaxCountExceeded, null, new object[]
				{
					grammars.Count,
					this.Logger.RunData.TenantId,
					this.Logger.RunData.RunId
				});
				grammars.RemoveRange(150, grammars.Count - 150);
			}
		}

		// Token: 0x04000AE0 RID: 2784
		private const int MaxTenantGrammars = 150;
	}
}
