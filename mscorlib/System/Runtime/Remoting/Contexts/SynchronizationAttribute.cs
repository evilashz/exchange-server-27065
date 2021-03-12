using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x020007EC RID: 2028
	[SecurityCritical]
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[Serializable]
	public class SynchronizationAttribute : ContextAttribute, IContributeServerContextSink, IContributeClientContextSink
	{
		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x060057C4 RID: 22468 RVA: 0x001347B4 File Offset: 0x001329B4
		// (set) Token: 0x060057C5 RID: 22469 RVA: 0x001347BC File Offset: 0x001329BC
		public virtual bool Locked
		{
			get
			{
				return this._locked;
			}
			set
			{
				this._locked = value;
			}
		}

		// Token: 0x17000EA7 RID: 3751
		// (get) Token: 0x060057C6 RID: 22470 RVA: 0x001347C5 File Offset: 0x001329C5
		public virtual bool IsReEntrant
		{
			get
			{
				return this._bReEntrant;
			}
		}

		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x060057C7 RID: 22471 RVA: 0x001347CD File Offset: 0x001329CD
		// (set) Token: 0x060057C8 RID: 22472 RVA: 0x001347D5 File Offset: 0x001329D5
		internal string SyncCallOutLCID
		{
			get
			{
				return this._syncLcid;
			}
			set
			{
				this._syncLcid = value;
			}
		}

		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x060057C9 RID: 22473 RVA: 0x001347DE File Offset: 0x001329DE
		internal ArrayList AsyncCallOutLCIDList
		{
			get
			{
				return this._asyncLcidList;
			}
		}

		// Token: 0x060057CA RID: 22474 RVA: 0x001347E8 File Offset: 0x001329E8
		internal bool IsKnownLCID(IMessage reqMsg)
		{
			string logicalCallID = ((LogicalCallContext)reqMsg.Properties[Message.CallContextKey]).RemotingData.LogicalCallID;
			return logicalCallID.Equals(this._syncLcid) || this._asyncLcidList.Contains(logicalCallID);
		}

		// Token: 0x060057CB RID: 22475 RVA: 0x00134831 File Offset: 0x00132A31
		public SynchronizationAttribute() : this(4, false)
		{
		}

		// Token: 0x060057CC RID: 22476 RVA: 0x0013483B File Offset: 0x00132A3B
		public SynchronizationAttribute(bool reEntrant) : this(4, reEntrant)
		{
		}

		// Token: 0x060057CD RID: 22477 RVA: 0x00134845 File Offset: 0x00132A45
		public SynchronizationAttribute(int flag) : this(flag, false)
		{
		}

		// Token: 0x060057CE RID: 22478 RVA: 0x0013484F File Offset: 0x00132A4F
		public SynchronizationAttribute(int flag, bool reEntrant) : base("Synchronization")
		{
			this._bReEntrant = reEntrant;
			if (flag - 1 <= 1 || flag == 4 || flag == 8)
			{
				this._flavor = flag;
				return;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), "flag");
		}

		// Token: 0x060057CF RID: 22479 RVA: 0x0013488D File Offset: 0x00132A8D
		internal void Dispose()
		{
			if (this._waitHandle != null)
			{
				this._waitHandle.Unregister(null);
			}
		}

		// Token: 0x060057D0 RID: 22480 RVA: 0x001348A4 File Offset: 0x00132AA4
		[SecurityCritical]
		[ComVisible(true)]
		public override bool IsContextOK(Context ctx, IConstructionCallMessage msg)
		{
			if (ctx == null)
			{
				throw new ArgumentNullException("ctx");
			}
			if (msg == null)
			{
				throw new ArgumentNullException("msg");
			}
			bool result = true;
			if (this._flavor == 8)
			{
				result = false;
			}
			else
			{
				SynchronizationAttribute synchronizationAttribute = (SynchronizationAttribute)ctx.GetProperty("Synchronization");
				if ((this._flavor == 1 && synchronizationAttribute != null) || (this._flavor == 4 && synchronizationAttribute == null))
				{
					result = false;
				}
				if (this._flavor == 4)
				{
					this._cliCtxAttr = synchronizationAttribute;
				}
			}
			return result;
		}

		// Token: 0x060057D1 RID: 22481 RVA: 0x00134918 File Offset: 0x00132B18
		[SecurityCritical]
		[ComVisible(true)]
		public override void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
		{
			if (this._flavor == 1 || this._flavor == 2 || ctorMsg == null)
			{
				return;
			}
			if (this._cliCtxAttr != null)
			{
				ctorMsg.ContextProperties.Add(this._cliCtxAttr);
				this._cliCtxAttr = null;
				return;
			}
			ctorMsg.ContextProperties.Add(this);
		}

		// Token: 0x060057D2 RID: 22482 RVA: 0x0013496C File Offset: 0x00132B6C
		internal virtual void InitIfNecessary()
		{
			lock (this)
			{
				if (this._asyncWorkEvent == null)
				{
					this._asyncWorkEvent = new AutoResetEvent(false);
					this._workItemQueue = new Queue();
					this._asyncLcidList = new ArrayList();
					WaitOrTimerCallback callBack = new WaitOrTimerCallback(this.DispatcherCallBack);
					this._waitHandle = ThreadPool.RegisterWaitForSingleObject(this._asyncWorkEvent, callBack, null, SynchronizationAttribute._timeOut, false);
				}
			}
		}

		// Token: 0x060057D3 RID: 22483 RVA: 0x001349F4 File Offset: 0x00132BF4
		private void DispatcherCallBack(object stateIgnored, bool ignored)
		{
			Queue workItemQueue = this._workItemQueue;
			WorkItem work;
			lock (workItemQueue)
			{
				work = (WorkItem)this._workItemQueue.Dequeue();
			}
			this.ExecuteWorkItem(work);
			this.HandleWorkCompletion();
		}

		// Token: 0x060057D4 RID: 22484 RVA: 0x00134A4C File Offset: 0x00132C4C
		internal virtual void HandleThreadExit()
		{
			this.HandleWorkCompletion();
		}

		// Token: 0x060057D5 RID: 22485 RVA: 0x00134A54 File Offset: 0x00132C54
		internal virtual void HandleThreadReEntry()
		{
			WorkItem workItem = new WorkItem(null, null, null);
			workItem.SetDummy();
			this.HandleWorkRequest(workItem);
		}

		// Token: 0x060057D6 RID: 22486 RVA: 0x00134A78 File Offset: 0x00132C78
		internal virtual void HandleWorkCompletion()
		{
			WorkItem workItem = null;
			bool flag = false;
			Queue workItemQueue = this._workItemQueue;
			lock (workItemQueue)
			{
				if (this._workItemQueue.Count >= 1)
				{
					workItem = (WorkItem)this._workItemQueue.Peek();
					flag = true;
					workItem.SetSignaled();
				}
				else
				{
					this._locked = false;
				}
			}
			if (flag)
			{
				if (workItem.IsAsync())
				{
					this._asyncWorkEvent.Set();
					return;
				}
				WorkItem obj = workItem;
				lock (obj)
				{
					Monitor.Pulse(workItem);
				}
			}
		}

		// Token: 0x060057D7 RID: 22487 RVA: 0x00134B30 File Offset: 0x00132D30
		internal virtual void HandleWorkRequest(WorkItem work)
		{
			if (!this.IsNestedCall(work._reqMsg))
			{
				if (work.IsAsync())
				{
					bool flag = true;
					Queue workItemQueue = this._workItemQueue;
					lock (workItemQueue)
					{
						work.SetWaiting();
						this._workItemQueue.Enqueue(work);
						if (!this._locked && this._workItemQueue.Count == 1)
						{
							work.SetSignaled();
							this._locked = true;
							this._asyncWorkEvent.Set();
						}
						return;
					}
				}
				lock (work)
				{
					Queue workItemQueue2 = this._workItemQueue;
					bool flag;
					lock (workItemQueue2)
					{
						if (!this._locked && this._workItemQueue.Count == 0)
						{
							this._locked = true;
							flag = false;
						}
						else
						{
							flag = true;
							work.SetWaiting();
							this._workItemQueue.Enqueue(work);
						}
					}
					if (flag)
					{
						Monitor.Wait(work);
						if (!work.IsDummy())
						{
							this.DispatcherCallBack(null, true);
							return;
						}
						Queue workItemQueue3 = this._workItemQueue;
						lock (workItemQueue3)
						{
							this._workItemQueue.Dequeue();
							return;
						}
					}
					if (!work.IsDummy())
					{
						work.SetSignaled();
						this.ExecuteWorkItem(work);
						this.HandleWorkCompletion();
					}
					return;
				}
			}
			work.SetSignaled();
			work.Execute();
		}

		// Token: 0x060057D8 RID: 22488 RVA: 0x00134CD0 File Offset: 0x00132ED0
		internal void ExecuteWorkItem(WorkItem work)
		{
			work.Execute();
		}

		// Token: 0x060057D9 RID: 22489 RVA: 0x00134CD8 File Offset: 0x00132ED8
		internal bool IsNestedCall(IMessage reqMsg)
		{
			bool flag = false;
			if (!this.IsReEntrant)
			{
				string syncCallOutLCID = this.SyncCallOutLCID;
				if (syncCallOutLCID != null)
				{
					LogicalCallContext logicalCallContext = (LogicalCallContext)reqMsg.Properties[Message.CallContextKey];
					if (logicalCallContext != null && syncCallOutLCID.Equals(logicalCallContext.RemotingData.LogicalCallID))
					{
						flag = true;
					}
				}
				if (!flag && this.AsyncCallOutLCIDList.Count > 0)
				{
					LogicalCallContext logicalCallContext2 = (LogicalCallContext)reqMsg.Properties[Message.CallContextKey];
					if (this.AsyncCallOutLCIDList.Contains(logicalCallContext2.RemotingData.LogicalCallID))
					{
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x060057DA RID: 22490 RVA: 0x00134D6C File Offset: 0x00132F6C
		[SecurityCritical]
		public virtual IMessageSink GetServerContextSink(IMessageSink nextSink)
		{
			this.InitIfNecessary();
			return new SynchronizedServerContextSink(this, nextSink);
		}

		// Token: 0x060057DB RID: 22491 RVA: 0x00134D88 File Offset: 0x00132F88
		[SecurityCritical]
		public virtual IMessageSink GetClientContextSink(IMessageSink nextSink)
		{
			this.InitIfNecessary();
			return new SynchronizedClientContextSink(this, nextSink);
		}

		// Token: 0x040027C0 RID: 10176
		public const int NOT_SUPPORTED = 1;

		// Token: 0x040027C1 RID: 10177
		public const int SUPPORTED = 2;

		// Token: 0x040027C2 RID: 10178
		public const int REQUIRED = 4;

		// Token: 0x040027C3 RID: 10179
		public const int REQUIRES_NEW = 8;

		// Token: 0x040027C4 RID: 10180
		private const string PROPERTY_NAME = "Synchronization";

		// Token: 0x040027C5 RID: 10181
		private static readonly int _timeOut = -1;

		// Token: 0x040027C6 RID: 10182
		[NonSerialized]
		internal AutoResetEvent _asyncWorkEvent;

		// Token: 0x040027C7 RID: 10183
		[NonSerialized]
		private RegisteredWaitHandle _waitHandle;

		// Token: 0x040027C8 RID: 10184
		[NonSerialized]
		internal Queue _workItemQueue;

		// Token: 0x040027C9 RID: 10185
		[NonSerialized]
		internal bool _locked;

		// Token: 0x040027CA RID: 10186
		internal bool _bReEntrant;

		// Token: 0x040027CB RID: 10187
		internal int _flavor;

		// Token: 0x040027CC RID: 10188
		[NonSerialized]
		private SynchronizationAttribute _cliCtxAttr;

		// Token: 0x040027CD RID: 10189
		[NonSerialized]
		private string _syncLcid;

		// Token: 0x040027CE RID: 10190
		[NonSerialized]
		private ArrayList _asyncLcidList;
	}
}
