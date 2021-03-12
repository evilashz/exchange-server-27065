using System;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;

namespace Microsoft.Exchange.Data.Search.AqsParser
{
	// Token: 0x02000D04 RID: 3332
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PolicyTagAdProvider : IPolicyTagProvider
	{
		// Token: 0x17001E96 RID: 7830
		// (get) Token: 0x060072C3 RID: 29379 RVA: 0x001FC600 File Offset: 0x001FA800
		public PolicyTag[] PolicyTags
		{
			get
			{
				OrganizationId currentOrganizationId = this.configurationSession.SessionSettings.CurrentOrganizationId;
				IConfigurationSession configurationSession;
				if (SharedConfiguration.IsDehydratedConfiguration(currentOrganizationId))
				{
					configurationSession = SharedConfiguration.CreateScopedToSharedConfigADSession(currentOrganizationId);
				}
				else
				{
					configurationSession = this.configurationSession;
				}
				RetentionPolicyTag[] array = configurationSession.FindPaged<RetentionPolicyTag>(null, QueryScope.SubTree, null, null, 0).ToArray<RetentionPolicyTag>();
				PolicyTagAdProvider.Tracer.TraceDebug<int>((long)this.GetHashCode(), "PolicyTagADResolver resolving {0} RetentionPolicyTags", array.Count<RetentionPolicyTag>());
				return (from x in array
				select new PolicyTag
				{
					Name = x.Name,
					PolicyGuid = x.RetentionId,
					Description = x.Comment,
					Type = x.Type
				}).ToArray<PolicyTag>();
			}
		}

		// Token: 0x060072C4 RID: 29380 RVA: 0x001FC68B File Offset: 0x001FA88B
		public PolicyTagAdProvider(IConfigurationSession configurationSession)
		{
			if (configurationSession == null)
			{
				throw new ArgumentNullException("configurationSession");
			}
			this.configurationSession = configurationSession;
		}

		// Token: 0x04005026 RID: 20518
		private IConfigurationSession configurationSession;

		// Token: 0x04005027 RID: 20519
		protected static readonly Trace Tracer = ExTraceGlobals.SearchTracer;
	}
}
