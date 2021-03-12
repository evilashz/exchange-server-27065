using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000695 RID: 1685
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class DelegateSessionCache : IEnumerable<DelegateSessionEntry>, IEnumerable
	{
		// Token: 0x060044F1 RID: 17649 RVA: 0x0012560C File Offset: 0x0012380C
		internal DelegateSessionCache(int capacity)
		{
			if (capacity > 10 || capacity < 1)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			capacity = TestBridgeManager.GetSize(capacity);
			this.capacity = capacity;
			this.dataSet = new List<DelegateSessionEntry>(this.capacity);
		}

		// Token: 0x060044F2 RID: 17650 RVA: 0x00125648 File Offset: 0x00123848
		internal static void SetTestBridge(ITestBridgeDelegateSessionCache bridge)
		{
			TestBridgeManager.SetBridge(bridge);
		}

		// Token: 0x060044F3 RID: 17651 RVA: 0x00125650 File Offset: 0x00123850
		internal static void UnSetTestBridge()
		{
			TestBridgeManager.SetBridge(null);
		}

		// Token: 0x060044F4 RID: 17652 RVA: 0x00125658 File Offset: 0x00123858
		internal bool TryGet(IExchangePrincipal principal, OpenBy openBy, out DelegateSessionEntry entry)
		{
			entry = null;
			if (principal == null || string.IsNullOrEmpty(principal.LegacyDn))
			{
				return false;
			}
			entry = this.FindEntry(principal);
			if (entry == null)
			{
				return false;
			}
			entry.Access(openBy);
			return true;
		}

		// Token: 0x060044F5 RID: 17653 RVA: 0x00125687 File Offset: 0x00123887
		internal DelegateSessionEntry Add(DelegateSessionEntry entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			return this.AddNewEntry(entry);
		}

		// Token: 0x060044F6 RID: 17654 RVA: 0x001256A0 File Offset: 0x001238A0
		internal void Clear()
		{
			foreach (DelegateSessionEntry delegateSessionEntry in this.dataSet)
			{
				delegateSessionEntry.ForceDispose();
			}
			this.dataSet.Clear();
		}

		// Token: 0x060044F7 RID: 17655 RVA: 0x00125700 File Offset: 0x00123900
		internal void RemoveEntry(DelegateSessionEntry entry)
		{
			this.dataSet.Remove(entry);
			entry.ForceDispose();
		}

		// Token: 0x060044F8 RID: 17656 RVA: 0x00125715 File Offset: 0x00123915
		public IEnumerator<DelegateSessionEntry> GetEnumerator()
		{
			return this.dataSet.GetEnumerator();
		}

		// Token: 0x060044F9 RID: 17657 RVA: 0x00125727 File Offset: 0x00123927
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060044FA RID: 17658 RVA: 0x00125730 File Offset: 0x00123930
		private DelegateSessionEntry AddNewEntry(DelegateSessionEntry entry)
		{
			DelegateSessionEntry delegateSessionEntry = null;
			if (this.dataSet.Count >= this.capacity)
			{
				ExTraceGlobals.SessionTracer.TraceDebug<int, int>((long)this.GetHashCode(), "DelegateSessionCache::AddNewEntry. Overflow process. Capacity = {0}, Count = {1}.", this.capacity, this.dataSet.Count);
				foreach (DelegateSessionEntry delegateSessionEntry2 in this.dataSet)
				{
					if (delegateSessionEntry2.ExternalRefCount <= 0 && (delegateSessionEntry == null || delegateSessionEntry2.LastAccessed < delegateSessionEntry.LastAccessed))
					{
						delegateSessionEntry = delegateSessionEntry2;
					}
				}
				if (delegateSessionEntry == null)
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendLine("CallStack for active DelegateSessionEntry:");
					foreach (DelegateSessionEntry delegateSessionEntry3 in this.dataSet)
					{
						if (delegateSessionEntry3.ExternalRefCount > 0)
						{
							stringBuilder.AppendLine(delegateSessionEntry3.GetCallStack());
						}
					}
					InvalidOperationException ex = new InvalidOperationException(string.Format("We cannot find a candidate from our internal cache. The consumer must remove unused sessions. Size = {0}, Capacity = {1}.", this.dataSet.Count, this.capacity));
					ex.Data["ActiveEntryCallStack"] = stringBuilder.ToString();
					throw ex;
				}
				TestBridgeManager.Removed(delegateSessionEntry.LegacyDn);
				this.dataSet.Remove(delegateSessionEntry);
				delegateSessionEntry.ForceDispose();
				delegateSessionEntry = null;
			}
			this.dataSet.Add(entry);
			TestBridgeManager.Added(entry.LegacyDn);
			return entry;
		}

		// Token: 0x060044FB RID: 17659 RVA: 0x001258C0 File Offset: 0x00123AC0
		[Conditional("DEBUG")]
		private void DebugCheckNewEntry(IExchangePrincipal principal)
		{
			this.FindEntry(principal);
		}

		// Token: 0x060044FC RID: 17660 RVA: 0x00125914 File Offset: 0x00123B14
		private DelegateSessionEntry FindEntry(IExchangePrincipal principal)
		{
			return this.dataSet.Find((DelegateSessionEntry e) => string.Compare(e.MailboxSession.MailboxOwnerLegacyDN, principal.LegacyDn, StringComparison.OrdinalIgnoreCase) == 0 && e.MailboxSession.MailboxOwner.IsCrossSiteAccessAllowed == principal.IsCrossSiteAccessAllowed);
		}

		// Token: 0x04002581 RID: 9601
		private const int MaxSize = 10;

		// Token: 0x04002582 RID: 9602
		private int capacity;

		// Token: 0x04002583 RID: 9603
		private readonly List<DelegateSessionEntry> dataSet;
	}
}
