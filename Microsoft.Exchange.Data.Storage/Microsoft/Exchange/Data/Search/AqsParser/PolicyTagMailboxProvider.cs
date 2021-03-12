using System;
using System.Linq;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;

namespace Microsoft.Exchange.Data.Search.AqsParser
{
	// Token: 0x02000D05 RID: 3333
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PolicyTagMailboxProvider : IPolicyTagProvider
	{
		// Token: 0x17001E97 RID: 7831
		// (get) Token: 0x060072C7 RID: 29383 RVA: 0x001FC6B4 File Offset: 0x001FA8B4
		public PolicyTag[] PolicyTags
		{
			get
			{
				PolicyTagList policyTagList = this.mailboxSession.GetPolicyTagList((RetentionActionType)0);
				PolicyTagMailboxProvider.Tracer.TraceDebug<int>((long)this.GetHashCode(), "PolicyTagStoreResolver resolving {0} RetentionPolicyTags", (policyTagList == null) ? 0 : policyTagList.Count);
				if (policyTagList != null)
				{
					return policyTagList.Values.ToArray<PolicyTag>();
				}
				return null;
			}
		}

		// Token: 0x060072C8 RID: 29384 RVA: 0x001FC700 File Offset: 0x001FA900
		public PolicyTagMailboxProvider(MailboxSession mailboxSession)
		{
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			this.mailboxSession = mailboxSession;
		}

		// Token: 0x04005029 RID: 20521
		private MailboxSession mailboxSession;

		// Token: 0x0400502A RID: 20522
		protected static readonly Trace Tracer = ExTraceGlobals.SearchTracer;
	}
}
