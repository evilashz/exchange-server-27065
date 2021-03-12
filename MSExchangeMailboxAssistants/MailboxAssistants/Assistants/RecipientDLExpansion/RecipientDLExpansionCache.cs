using System;
using System.Collections.Generic;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.RecipientDLExpansion;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.RecipientDLExpansion
{
	// Token: 0x0200025C RID: 604
	internal class RecipientDLExpansionCache : LazyLookupTimeoutCache<string, List<RecipientToIndex>>
	{
		// Token: 0x06001670 RID: 5744 RVA: 0x0007EB86 File Offset: 0x0007CD86
		public RecipientDLExpansionCache() : base(1, 1000, false, TimeSpan.FromMinutes(60.0), TimeSpan.FromMinutes(60.0))
		{
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001671 RID: 5745 RVA: 0x0007EBB1 File Offset: 0x0007CDB1
		public static RecipientDLExpansionCache Instance
		{
			get
			{
				return RecipientDLExpansionCache.instance;
			}
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x0007EBB8 File Offset: 0x0007CDB8
		internal static string GetUniqueLookupKey(OrganizationId organizationId, ADRecipient recipient)
		{
			if (organizationId == null)
			{
				throw new ArgumentNullException("organizationId");
			}
			if (recipient == null)
			{
				throw new ArgumentNullException("recipient");
			}
			return string.Format("{0}:{1}", organizationId.ToString(), recipient.LegacyExchangeDN);
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x0007EBF2 File Offset: 0x0007CDF2
		internal void Add(string key, List<RecipientToIndex> value)
		{
			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentNullException("key");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			base.Remove(key);
			this.TryPerformAdd(key, value);
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x0007EC26 File Offset: 0x0007CE26
		protected override List<RecipientToIndex> CreateOnCacheMiss(string key, ref bool shouldAdd)
		{
			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentNullException("key");
			}
			RecipientDLExpansionCache.Tracer.TraceDebug<string>((long)this.GetHashCode(), "[RecipientDLExpansionCache] CreateOnCacheMiss is called for key: {0}", key);
			shouldAdd = false;
			return null;
		}

		// Token: 0x04000D30 RID: 3376
		private static readonly RecipientDLExpansionCache instance = new RecipientDLExpansionCache();

		// Token: 0x04000D31 RID: 3377
		private static readonly Trace Tracer = ExTraceGlobals.RecipientDLExpansionEventBasedAssistantTracer;
	}
}
