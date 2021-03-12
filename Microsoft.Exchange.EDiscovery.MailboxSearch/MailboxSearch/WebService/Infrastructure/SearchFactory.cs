using System;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.External;
using Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model;
using Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Infrastructure
{
	// Token: 0x0200003A RID: 58
	internal class SearchFactory
	{
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x00013C98 File Offset: 0x00011E98
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x00013C9F File Offset: 0x00011E9F
		public static SearchFactory Current
		{
			get
			{
				return SearchFactory.current;
			}
			protected set
			{
				SearchFactory.current = value;
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00013CA7 File Offset: 0x00011EA7
		public virtual ISearchPolicy GetSearchPolicy(IRecipientSession recipientSession, CallerInfo callerInfo, ExchangeRunspaceConfiguration runspaceConfiguration, IBudget budget = null)
		{
			Recorder.Trace(2L, TraceType.InfoTrace, "SearchFactory.GetSearchPolicy");
			return new SearchPolicy(recipientSession, callerInfo, runspaceConfiguration, budget);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00013CC0 File Offset: 0x00011EC0
		public virtual IThrottlingPolicy GetThrottlingPolicy(ISearchPolicy policy)
		{
			Recorder.Trace(2L, TraceType.InfoTrace, "SearchFactory.GetThrottlingPolicy");
			DiscoveryTenantBudgetKey discoveryTenantBudgetKey = new DiscoveryTenantBudgetKey(policy.RecipientSession.SessionSettings.CurrentOrganizationId, BudgetType.PowerShell);
			return discoveryTenantBudgetKey.Lookup();
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00013CF8 File Offset: 0x00011EF8
		public virtual VariantConfigurationSnapshot GetVariantConfigurationSnapshot(ISearchPolicy policy)
		{
			Recorder.Trace(2L, TraceType.InfoTrace, "SearchFactory.GetVariantConfigurationSnapshot");
			VariantConfigurationSnapshot variantConfigurationSnapshot = null;
			if (policy.RunspaceConfiguration != null && policy.RunspaceConfiguration.ExecutingUser != null)
			{
				Recorder.Trace(2L, TraceType.InfoTrace, "SearchFactory.GetVariantConfigurationSnapshot Loading User Snapshpt");
				ADUser user = new ADUser(policy.RecipientSession, policy.RunspaceConfiguration.ExecutingUser.propertyBag);
				variantConfigurationSnapshot = VariantConfiguration.GetSnapshot(user.GetContext(null), null, null);
			}
			if (variantConfigurationSnapshot == null)
			{
				Recorder.Trace(2L, TraceType.InfoTrace, "SearchFactory.GetVariantConfigurationSnapshot User Snapshot Failed, Loading Global Snapshot");
				variantConfigurationSnapshot = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null);
			}
			return variantConfigurationSnapshot;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00013D7F File Offset: 0x00011F7F
		public virtual IDirectoryProvider GetDirectoryProvider(ISearchPolicy policy)
		{
			Recorder.Trace(2L, TraceType.InfoTrace, "SearchFactory.GetDirectoryProvider");
			return new ActiveDirectoryProvider();
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00013D93 File Offset: 0x00011F93
		public virtual IServerProvider GetServerProvider(ISearchPolicy policy)
		{
			Recorder.Trace(2L, TraceType.InfoTrace, "SearchFactory.GetServerProvider");
			return new AutoDiscoveryServerProvider();
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00013DA7 File Offset: 0x00011FA7
		public virtual ISearchConfigurationProvider GetSearchConfigurationProvider(ISearchPolicy policy)
		{
			Recorder.Trace(2L, TraceType.InfoTrace, "SearchFactory.GetSearchConfigurationProvider");
			return new ArbitrationSearchConfigurationProvider();
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00013DBB File Offset: 0x00011FBB
		public virtual IExchangeProxy GetProxy(ISearchPolicy policy, FanoutParameters parameter)
		{
			Recorder.Trace(2L, TraceType.InfoTrace, "SearchFactory.GetProxy");
			return new ExchangeProxy(policy, parameter);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00013DD4 File Offset: 0x00011FD4
		public virtual ISourceConverter GetSourceConverter(ISearchPolicy policy, SourceType sourceFrom)
		{
			Recorder.Trace(2L, TraceType.InfoTrace, "SearchFactory.GetSourceConverter SourceType:", sourceFrom);
			if (sourceFrom == SourceType.PublicFolder || sourceFrom == SourceType.AllPublicFolders)
			{
				return new PublicFolderSourceConverter();
			}
			Recorder.Trace(2L, TraceType.InfoTrace, "SearchFactory.GetSourceConverter No Converter SourceType:", sourceFrom);
			return null;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00013E18 File Offset: 0x00012018
		public virtual ISearchResultProvider GetSearchResultProvider(ISearchPolicy policy, SearchType searchType)
		{
			Recorder.Trace(2L, TraceType.InfoTrace, "SearchFactory.GetSearchResultProvider");
			if (searchType == SearchType.NonIndexedItemPreview || searchType == SearchType.NonIndexedItemStatistics)
			{
				return new LocalNonIndexedResultProvider();
			}
			if (policy.ExecutionSettings.DiscoveryUseFastSearch && searchType == SearchType.Preview)
			{
				return new FastLocalSearchResultsProvider();
			}
			return new LocalSearchResultsProvider();
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00013E51 File Offset: 0x00012051
		public virtual IConfigurationSession GetConfigurationSession(ISearchPolicy policy)
		{
			Recorder.Trace(2L, TraceType.InfoTrace, "SearchFactory.GetConfigurationSession");
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, policy.RecipientSession.SessionSettings, 222, "GetConfigurationSession", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\MailboxSearch\\WebService\\Infrastructure\\SearchFactory.cs");
		}

		// Token: 0x04000133 RID: 307
		private static SearchFactory current = new SearchFactory();
	}
}
