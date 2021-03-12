using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Search.TokenOperators
{
	// Token: 0x0200001A RID: 26
	internal class ItemDepot : IDisposeTrackable, IDisposable
	{
		// Token: 0x060000EB RID: 235 RVA: 0x00005178 File Offset: 0x00003378
		private ItemDepot()
		{
			this.internalDepot = new Dictionary<string, Dictionary<string, IDisposable>>(16);
			this.associatedRecordInfos = new Dictionary<string, RecordInformation>(1);
			this.tracingContext = this.GetHashCode();
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000EC RID: 236 RVA: 0x000051D2 File Offset: 0x000033D2
		internal static ItemDepot Instance
		{
			get
			{
				return ItemDepot.instance;
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000051DC File Offset: 0x000033DC
		public void DepositItem(string flowIdentifier, IDisposable item, string itemPath)
		{
			lock (this.depotLock)
			{
				if (!this.internalDepot.ContainsKey(flowIdentifier))
				{
					this.internalDepot.Add(flowIdentifier, new Dictionary<string, IDisposable>(8));
				}
				this.internalDepot[flowIdentifier].Add(itemPath, item);
				ExTraceGlobals.ErrorOperatorTracer.TraceError<int>((long)this.tracingContext, "Addition to depot for new CorrelationId. Current number of tracked CorrelationIds: {0}", this.internalDepot.Keys.Count);
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005270 File Offset: 0x00003470
		public IDisposable GetItem(string flowIdentifier, string itemPath)
		{
			lock (this.depotLock)
			{
				if (this.internalDepot.ContainsKey(flowIdentifier) && this.internalDepot[flowIdentifier].ContainsKey(itemPath))
				{
					return this.internalDepot[flowIdentifier][itemPath];
				}
			}
			return null;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000052E4 File Offset: 0x000034E4
		public void DisposeItems(string flowIdentifier)
		{
			lock (this.depotLock)
			{
				if (this.internalDepot.ContainsKey(flowIdentifier))
				{
					foreach (IDisposable disposable in this.internalDepot[flowIdentifier].Values)
					{
						try
						{
							disposable.Dispose();
						}
						catch (Exception arg)
						{
							ExTraceGlobals.ErrorOperatorTracer.TraceError<Exception>((long)this.tracingContext, "Exception while disposing item: {0}", arg);
						}
					}
					this.internalDepot[flowIdentifier].Clear();
				}
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000053B4 File Offset: 0x000035B4
		public void CleanupFlowInstance(string flowIdentifier)
		{
			lock (this.depotLock)
			{
				if (this.internalDepot.ContainsKey(flowIdentifier))
				{
					this.DisposeItems(flowIdentifier);
					this.internalDepot.Remove(flowIdentifier);
				}
			}
			lock (this.identitiesLock)
			{
				this.associatedRecordInfos.Remove(flowIdentifier);
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005448 File Offset: 0x00003648
		public void SaveAssociatedRecordInformation(string flowIdentifier, Guid correlationId, long documentId, Guid tenantId, IIdentity identity, int errorCode, int attemptCount, string indexSystemName)
		{
			lock (this.identitiesLock)
			{
				RecordInformation value = new RecordInformation
				{
					CorrelationId = correlationId,
					DocumentId = documentId,
					TenantId = tenantId,
					CompositeItemId = identity,
					ErrorCode = errorCode,
					AttemptCount = attemptCount,
					IndexSystemName = indexSystemName
				};
				this.associatedRecordInfos[flowIdentifier] = value;
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000054D8 File Offset: 0x000036D8
		public RecordInformation GetAssociatedRecordInformation(string flowIdentifier, Guid correlationId)
		{
			RecordInformation result;
			lock (this.identitiesLock)
			{
				if (!this.associatedRecordInfos.ContainsKey(flowIdentifier))
				{
					throw new ArgumentOutOfRangeException("flowIdentifier");
				}
				result = this.associatedRecordInfos[flowIdentifier];
			}
			return result;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000553C File Offset: 0x0000373C
		public void ClearAssociatedRecordInformation(string flowIdentifier)
		{
			lock (this.identitiesLock)
			{
				this.associatedRecordInfos.Remove(flowIdentifier);
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005584 File Offset: 0x00003784
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ItemDepot>(this);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000558C File Offset: 0x0000378C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000559B File Offset: 0x0000379B
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000055B0 File Offset: 0x000037B0
		protected virtual void Dispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				lock (this.depotLock)
				{
					if (this.disposeTracker != null)
					{
						this.disposeTracker.Dispose();
						this.disposeTracker = null;
					}
					foreach (string flowIdentifier in this.internalDepot.Keys)
					{
						this.DisposeItems(flowIdentifier);
					}
					this.internalDepot.Clear();
				}
			}
		}

		// Token: 0x04000062 RID: 98
		private static readonly ItemDepot instance = new ItemDepot();

		// Token: 0x04000063 RID: 99
		private readonly int tracingContext;

		// Token: 0x04000064 RID: 100
		private readonly object depotLock = new object();

		// Token: 0x04000065 RID: 101
		private readonly Dictionary<string, Dictionary<string, IDisposable>> internalDepot;

		// Token: 0x04000066 RID: 102
		private readonly Dictionary<string, RecordInformation> associatedRecordInfos;

		// Token: 0x04000067 RID: 103
		private readonly object identitiesLock = new object();

		// Token: 0x04000068 RID: 104
		private DisposeTracker disposeTracker;
	}
}
