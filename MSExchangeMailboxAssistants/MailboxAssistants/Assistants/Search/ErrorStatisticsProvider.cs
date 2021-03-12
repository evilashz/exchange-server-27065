using System;
using System.Collections.Generic;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.Fast;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Search
{
	// Token: 0x02000190 RID: 400
	internal class ErrorStatisticsProvider : IDisposeTrackable, IDisposable
	{
		// Token: 0x06000FBB RID: 4027 RVA: 0x0005D108 File Offset: 0x0005B308
		public ErrorStatisticsProvider(IDiagnosticsSession diagnosticsSession, Guid mdbGuid, SearchConfig config)
		{
			this.mdbGuid = mdbGuid;
			this.diagnosticsSession = diagnosticsSession;
			this.parameters = new FailedItemParameters(FailureMode.Permanent, FieldSet.IndexRepairAssistant);
			this.timeoutCache = new ExactTimeoutCache<Guid, IFailedItemStorage>(delegate(Guid guid, IFailedItemStorage storage, RemoveReason reason)
			{
				storage.Dispose();
			}, null, null, 1, true);
			this.config = config;
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x0005D178 File Offset: 0x0005B378
		public ErrorStatistics GetErrorStatistics(Guid mailboxGuid)
		{
			IFailedItemStorage failedItemStorage = this.GetFailedItemStorage();
			long itemsCount = failedItemStorage.GetItemsCount(mailboxGuid);
			this.parameters.MailboxGuid = new Guid?(mailboxGuid);
			ICollection<IFailureEntry> failedItems = failedItemStorage.GetFailedItems(this.parameters);
			Dictionary<EvaluationErrors, long> dictionary = new Dictionary<EvaluationErrors, long>();
			foreach (IFailureEntry failureEntry in failedItems)
			{
				long num;
				dictionary.TryGetValue(failureEntry.ErrorCode, out num);
				num = (dictionary[failureEntry.ErrorCode] = num + 1L);
			}
			return new ErrorStatistics(itemsCount, dictionary);
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x0005D220 File Offset: 0x0005B420
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ErrorStatisticsProvider>(this);
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x0005D228 File Offset: 0x0005B428
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x0005D23D File Offset: 0x0005B43D
		public void Dispose()
		{
			this.diagnosticsSession.TraceDebug<Guid>("Disposing ErrorStatisticsProvider for Database {0}", this.mdbGuid);
			this.timeoutCache.Dispose();
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x0005D260 File Offset: 0x0005B460
		private IFailedItemStorage GetFailedItemStorage()
		{
			IFailedItemStorage failedItemStorage;
			if (!this.timeoutCache.TryGetValue(this.mdbGuid, out failedItemStorage))
			{
				failedItemStorage = Factory.Current.CreateFailedItemStorage(this.config, FastIndexVersion.GetIndexSystemName(this.mdbGuid));
				this.timeoutCache.TryAddSliding(this.mdbGuid, failedItemStorage, this.config.IndexRepairAssistantConnectionReleaseTimeout);
			}
			return failedItemStorage;
		}

		// Token: 0x04000A07 RID: 2567
		private readonly IDiagnosticsSession diagnosticsSession;

		// Token: 0x04000A08 RID: 2568
		private readonly ExactTimeoutCache<Guid, IFailedItemStorage> timeoutCache;

		// Token: 0x04000A09 RID: 2569
		private readonly SearchConfig config;

		// Token: 0x04000A0A RID: 2570
		private readonly Guid mdbGuid;

		// Token: 0x04000A0B RID: 2571
		private readonly DisposeTracker disposeTracker;

		// Token: 0x04000A0C RID: 2572
		private FailedItemParameters parameters;
	}
}
