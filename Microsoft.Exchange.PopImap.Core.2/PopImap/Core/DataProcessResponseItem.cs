using System;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x0200000D RID: 13
	internal class DataProcessResponseItem : IResponseItem, IDisposeTrackable, IDisposable
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x00004B17 File Offset: 0x00002D17
		public DataProcessResponseItem() : this(null, null, null, false)
		{
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004B23 File Offset: 0x00002D23
		public DataProcessResponseItem(object stateData, Action<ProtocolSession, DataProcessResponseItem> dataProcessDelegate) : this(stateData, dataProcessDelegate, null, true)
		{
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004B2F File Offset: 0x00002D2F
		public DataProcessResponseItem(object stateData, Action<ProtocolSession, DataProcessResponseItem> dataProcessDelegate, Action<DataProcessResponseItem> dataCleanupDelegate) : this(stateData, dataProcessDelegate, dataCleanupDelegate, true)
		{
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004B3B File Offset: 0x00002D3B
		public DataProcessResponseItem(object stateData, Action<ProtocolSession, DataProcessResponseItem> dataProcessDelegate, Action<DataProcessResponseItem> dataCleanupDelegate, bool shouldConnectToStore)
		{
			this.responseQueue = new ResponseQueue(5, false);
			this.disposeTracker = this.GetDisposeTracker();
			this.stateData = stateData;
			this.dataProcessDelegate = dataProcessDelegate;
			this.dataCleanupDelegate = dataCleanupDelegate;
			this.shouldConnectToStore = shouldConnectToStore;
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00004B79 File Offset: 0x00002D79
		public Action<ProtocolSession, DataProcessResponseItem> LateDataProcessDelegate
		{
			get
			{
				return this.dataProcessDelegate;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00004B81 File Offset: 0x00002D81
		public object StateData
		{
			get
			{
				return this.stateData;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00004B89 File Offset: 0x00002D89
		public BaseSession.SendCompleteDelegate SendCompleteDelegate
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004B8C File Offset: 0x00002D8C
		public void BindData(object stateData, Action<ProtocolSession, DataProcessResponseItem> dataProcessDelegate, bool shouldConnectToStore)
		{
			this.stateData = stateData;
			this.dataProcessDelegate = dataProcessDelegate;
			this.dataCleanupDelegate = null;
			this.shouldConnectToStore = shouldConnectToStore;
			this.responseQueue.Clear();
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004BB8 File Offset: 0x00002DB8
		public int GetNextChunk(BaseSession session, out byte[] buffer, out int offset)
		{
			ProtocolSession protocolSession = session as ProtocolSession;
			if (protocolSession == null)
			{
				throw new ArgumentException("session is not a valid ProtocolSession");
			}
			buffer = null;
			offset = 0;
			IStandardBudget perCallBudget = null;
			if (this.dataProcessDelegate != null)
			{
				bool flag = false;
				bool flag2 = false;
				ResponseFactory responseFactory = protocolSession.ResponseFactory;
				if (protocolSession.Disconnected || responseFactory == null)
				{
					return 0;
				}
				try
				{
					ActivityContext.SetThreadScope(protocolSession.ActivityScope);
					if (this.shouldConnectToStore)
					{
						Monitor.Enter(responseFactory.Store);
						flag2 = true;
					}
					if (this.shouldConnectToStore)
					{
						try
						{
							perCallBudget = responseFactory.AcquirePerCommandBudget();
						}
						catch (OverBudgetException exception)
						{
							responseFactory.LogHandledException(exception);
							return 0;
						}
						catch (ADTransientException exception2)
						{
							responseFactory.LogHandledException(exception2);
							return 0;
						}
					}
					try
					{
						if (this.shouldConnectToStore && !responseFactory.IsStoreConnected)
						{
							responseFactory.ConnectToTheStore();
							flag = true;
						}
						this.dataProcessDelegate(protocolSession, this);
					}
					catch (StorageTransientException exception3)
					{
						responseFactory.LogHandledException(exception3);
						return 0;
					}
					catch (StoragePermanentException exception4)
					{
						responseFactory.LogHandledException(exception4);
						return 0;
					}
				}
				catch (Exception exception5)
				{
					if (protocolSession.CheckNonCriticalException(exception5))
					{
						responseFactory.LogHandledException(exception5);
						return 0;
					}
					throw;
				}
				finally
				{
					if (this.dataCleanupDelegate != null)
					{
						this.dataCleanupDelegate(this);
					}
					if (flag)
					{
						responseFactory.DisconnectFromTheStore();
					}
					if (flag2)
					{
						Monitor.Exit(responseFactory.Store);
					}
					protocolSession.EnforceMicroDelayAndDisposeCostHandles(perCallBudget);
				}
				this.dataProcessDelegate = null;
				this.dataCleanupDelegate = null;
				if (!this.responseQueue.IsSending)
				{
					return 0;
				}
			}
			return this.responseQueue.GetNextChunk(session, out buffer, out offset);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004D84 File Offset: 0x00002F84
		public void EnqueueResponseItem(IResponseItem responseItem)
		{
			this.responseQueue.Enqueue(responseItem);
			if (!this.responseQueue.IsSending)
			{
				this.responseQueue.DequeueForSend();
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004DAA File Offset: 0x00002FAA
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<DataProcessResponseItem>(this);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004DB2 File Offset: 0x00002FB2
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
				this.disposeTracker = null;
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004DCE File Offset: 0x00002FCE
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004DDD File Offset: 0x00002FDD
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
					this.disposeTracker = null;
				}
				if (this.responseQueue != null)
				{
					this.responseQueue.Dispose();
					this.responseQueue = null;
				}
			}
		}

		// Token: 0x04000061 RID: 97
		private Action<ProtocolSession, DataProcessResponseItem> dataProcessDelegate;

		// Token: 0x04000062 RID: 98
		private Action<DataProcessResponseItem> dataCleanupDelegate;

		// Token: 0x04000063 RID: 99
		private object stateData;

		// Token: 0x04000064 RID: 100
		private ResponseQueue responseQueue;

		// Token: 0x04000065 RID: 101
		private bool shouldConnectToStore;

		// Token: 0x04000066 RID: 102
		private DisposeTracker disposeTracker;
	}
}
