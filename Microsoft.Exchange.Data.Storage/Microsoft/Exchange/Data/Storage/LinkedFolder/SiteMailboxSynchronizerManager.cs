using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x02000990 RID: 2448
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SiteMailboxSynchronizerManager
	{
		// Token: 0x06005A52 RID: 23122 RVA: 0x00176CD8 File Offset: 0x00174ED8
		private SiteMailboxSynchronizerManager()
		{
			TimeSpan timeSpan = TimeSpan.FromSeconds((double)StoreSession.GetConfigFromRegistry("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\SiteMailbox", "SyncManagerInterval", 600, (int x) => x > 0));
			this.disposeUnusedSiteMailboxSynchronizerTimer = new Timer(new TimerCallback(this.DisposeUnusedSiteMailboxSynchronizer), null, timeSpan, timeSpan);
		}

		// Token: 0x06005A53 RID: 23123 RVA: 0x00176D54 File Offset: 0x00174F54
		private void DisposeUnusedSiteMailboxSynchronizer(object state)
		{
			lock (this.lockObject)
			{
				List<Guid> list = new List<Guid>();
				foreach (KeyValuePair<Guid, SiteMailboxSynchronizerManager.SiteMailboxSynchronizerAndReferenceCount> keyValuePair in this.siteFolderMailboxSynchronizers)
				{
					if (keyValuePair.Value.ReferenceCount <= 0 && keyValuePair.Value.SiteMailboxSynchronizer.TryToDispose())
					{
						list.Add(keyValuePair.Key);
					}
				}
				foreach (Guid key in list)
				{
					this.siteFolderMailboxSynchronizers.Remove(key);
				}
			}
		}

		// Token: 0x170018E1 RID: 6369
		// (get) Token: 0x06005A54 RID: 23124 RVA: 0x00176E48 File Offset: 0x00175048
		public static SiteMailboxSynchronizerManager Instance
		{
			get
			{
				return SiteMailboxSynchronizerManager.singleton;
			}
		}

		// Token: 0x06005A55 RID: 23125 RVA: 0x00176E50 File Offset: 0x00175050
		public SiteMailboxSynchronizerReference GetSiteMailboxSynchronizer(IExchangePrincipal siteMailboxPrincipal, string client)
		{
			SiteMailboxSynchronizerReference result;
			lock (this.lockObject)
			{
				SiteMailboxSynchronizerManager.SiteMailboxSynchronizerAndReferenceCount siteMailboxSynchronizerAndReferenceCount;
				if (!this.siteFolderMailboxSynchronizers.TryGetValue(siteMailboxPrincipal.MailboxInfo.MailboxGuid, out siteMailboxSynchronizerAndReferenceCount))
				{
					SiteMailboxSynchronizer siteMailboxSynchronizer = new SiteMailboxSynchronizer(siteMailboxPrincipal, client);
					siteMailboxSynchronizerAndReferenceCount = new SiteMailboxSynchronizerManager.SiteMailboxSynchronizerAndReferenceCount(siteMailboxSynchronizer);
					this.siteFolderMailboxSynchronizers[siteMailboxPrincipal.MailboxInfo.MailboxGuid] = siteMailboxSynchronizerAndReferenceCount;
				}
				siteMailboxSynchronizerAndReferenceCount.ReferenceCount++;
				result = new SiteMailboxSynchronizerReference(siteMailboxSynchronizerAndReferenceCount.SiteMailboxSynchronizer, new Action<SiteMailboxSynchronizer>(this.OnReferenceDisposed));
			}
			return result;
		}

		// Token: 0x06005A56 RID: 23126 RVA: 0x00176EF4 File Offset: 0x001750F4
		private void OnReferenceDisposed(SiteMailboxSynchronizer siteMailboxSynchronizer)
		{
			lock (this.lockObject)
			{
				SiteMailboxSynchronizerManager.SiteMailboxSynchronizerAndReferenceCount siteMailboxSynchronizerAndReferenceCount;
				if (!this.siteFolderMailboxSynchronizers.TryGetValue(siteMailboxSynchronizer.MailboxGuid, out siteMailboxSynchronizerAndReferenceCount) || siteMailboxSynchronizerAndReferenceCount.ReferenceCount == 0)
				{
					throw new InvalidOperationException(("The site mailbox synchronizer is already been removed. This should not happen. ReferenceCount = " + siteMailboxSynchronizerAndReferenceCount != null) ? siteMailboxSynchronizerAndReferenceCount.ReferenceCount.ToString() : "Null");
				}
				siteMailboxSynchronizerAndReferenceCount.ReferenceCount--;
			}
		}

		// Token: 0x040031DE RID: 12766
		private const string RegKeySiteMailbox = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\SiteMailbox";

		// Token: 0x040031DF RID: 12767
		private const string RegValueSyncManagerInterval = "SyncManagerInterval";

		// Token: 0x040031E0 RID: 12768
		private static readonly SiteMailboxSynchronizerManager singleton = new SiteMailboxSynchronizerManager();

		// Token: 0x040031E1 RID: 12769
		private readonly Timer disposeUnusedSiteMailboxSynchronizerTimer;

		// Token: 0x040031E2 RID: 12770
		private readonly Dictionary<Guid, SiteMailboxSynchronizerManager.SiteMailboxSynchronizerAndReferenceCount> siteFolderMailboxSynchronizers = new Dictionary<Guid, SiteMailboxSynchronizerManager.SiteMailboxSynchronizerAndReferenceCount>();

		// Token: 0x040031E3 RID: 12771
		private readonly object lockObject = new object();

		// Token: 0x02000991 RID: 2449
		private class SiteMailboxSynchronizerAndReferenceCount
		{
			// Token: 0x06005A59 RID: 23129 RVA: 0x00176F8C File Offset: 0x0017518C
			public SiteMailboxSynchronizerAndReferenceCount(SiteMailboxSynchronizer siteMailboxSynchronizer)
			{
				this.SiteMailboxSynchronizer = siteMailboxSynchronizer;
			}

			// Token: 0x040031E5 RID: 12773
			public readonly SiteMailboxSynchronizer SiteMailboxSynchronizer;

			// Token: 0x040031E6 RID: 12774
			public int ReferenceCount;
		}
	}
}
