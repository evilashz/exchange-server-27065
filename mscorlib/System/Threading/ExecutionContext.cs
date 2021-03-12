using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004CB RID: 1227
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class ExecutionContext : IDisposable, ISerializable
	{
		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06003AD6 RID: 15062 RVA: 0x000DEA66 File Offset: 0x000DCC66
		// (set) Token: 0x06003AD7 RID: 15063 RVA: 0x000DEA73 File Offset: 0x000DCC73
		internal bool isNewCapture
		{
			get
			{
				return (this._flags & (ExecutionContext.Flags)5) > ExecutionContext.Flags.None;
			}
			set
			{
				if (value)
				{
					this._flags |= ExecutionContext.Flags.IsNewCapture;
					return;
				}
				this._flags &= (ExecutionContext.Flags)(-2);
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06003AD8 RID: 15064 RVA: 0x000DEA96 File Offset: 0x000DCC96
		// (set) Token: 0x06003AD9 RID: 15065 RVA: 0x000DEAA3 File Offset: 0x000DCCA3
		internal bool isFlowSuppressed
		{
			get
			{
				return (this._flags & ExecutionContext.Flags.IsFlowSuppressed) > ExecutionContext.Flags.None;
			}
			set
			{
				if (value)
				{
					this._flags |= ExecutionContext.Flags.IsFlowSuppressed;
					return;
				}
				this._flags &= (ExecutionContext.Flags)(-3);
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06003ADA RID: 15066 RVA: 0x000DEAC6 File Offset: 0x000DCCC6
		internal static ExecutionContext PreAllocatedDefault
		{
			[SecuritySafeCritical]
			get
			{
				return ExecutionContext.s_dummyDefaultEC;
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06003ADB RID: 15067 RVA: 0x000DEACD File Offset: 0x000DCCCD
		internal bool IsPreAllocatedDefault
		{
			get
			{
				return (this._flags & ExecutionContext.Flags.IsPreAllocatedDefault) != ExecutionContext.Flags.None;
			}
		}

		// Token: 0x06003ADC RID: 15068 RVA: 0x000DEADC File Offset: 0x000DCCDC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal ExecutionContext()
		{
		}

		// Token: 0x06003ADD RID: 15069 RVA: 0x000DEAE4 File Offset: 0x000DCCE4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal ExecutionContext(bool isPreAllocatedDefault)
		{
			if (isPreAllocatedDefault)
			{
				this._flags = ExecutionContext.Flags.IsPreAllocatedDefault;
			}
		}

		// Token: 0x06003ADE RID: 15070 RVA: 0x000DEAF8 File Offset: 0x000DCCF8
		[SecurityCritical]
		internal static object GetLocalValue(IAsyncLocal local)
		{
			return Thread.CurrentThread.GetExecutionContextReader().GetLocalValue(local);
		}

		// Token: 0x06003ADF RID: 15071 RVA: 0x000DEB18 File Offset: 0x000DCD18
		[SecurityCritical]
		internal static void SetLocalValue(IAsyncLocal local, object newValue, bool needChangeNotifications)
		{
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			object obj = null;
			bool flag = mutableExecutionContext._localValues != null && mutableExecutionContext._localValues.TryGetValue(local, out obj);
			if (obj == newValue)
			{
				return;
			}
			IAsyncLocalValueMap asyncLocalValueMap = mutableExecutionContext._localValues;
			if (asyncLocalValueMap == null)
			{
				asyncLocalValueMap = AsyncLocalValueMap.Create(local, newValue, !needChangeNotifications);
			}
			else
			{
				asyncLocalValueMap = asyncLocalValueMap.Set(local, newValue, !needChangeNotifications);
			}
			mutableExecutionContext._localValues = asyncLocalValueMap;
			if (needChangeNotifications)
			{
				if (!flag)
				{
					IAsyncLocal[] array = mutableExecutionContext._localChangeNotifications;
					if (array == null)
					{
						array = new IAsyncLocal[]
						{
							local
						};
					}
					else
					{
						int num = array.Length;
						Array.Resize<IAsyncLocal>(ref array, num + 1);
						array[num] = local;
					}
					mutableExecutionContext._localChangeNotifications = array;
				}
				local.OnValueChanged(obj, newValue, false);
			}
		}

		// Token: 0x06003AE0 RID: 15072 RVA: 0x000DEBC8 File Offset: 0x000DCDC8
		[SecurityCritical]
		[HandleProcessCorruptedStateExceptions]
		internal static void OnAsyncLocalContextChanged(ExecutionContext previous, ExecutionContext current)
		{
			IAsyncLocal[] array = (previous == null) ? null : previous._localChangeNotifications;
			if (array != null)
			{
				foreach (IAsyncLocal asyncLocal in array)
				{
					object obj = null;
					if (previous != null && previous._localValues != null)
					{
						previous._localValues.TryGetValue(asyncLocal, out obj);
					}
					object obj2 = null;
					if (current != null && current._localValues != null)
					{
						current._localValues.TryGetValue(asyncLocal, out obj2);
					}
					if (obj != obj2)
					{
						asyncLocal.OnValueChanged(obj, obj2, true);
					}
				}
			}
			IAsyncLocal[] array3 = (current == null) ? null : current._localChangeNotifications;
			if (array3 != null && array3 != array)
			{
				try
				{
					foreach (IAsyncLocal asyncLocal2 in array3)
					{
						object obj3 = null;
						if (previous == null || previous._localValues == null || !previous._localValues.TryGetValue(asyncLocal2, out obj3))
						{
							object obj4 = null;
							if (current != null && current._localValues != null)
							{
								current._localValues.TryGetValue(asyncLocal2, out obj4);
							}
							if (obj3 != obj4)
							{
								asyncLocal2.OnValueChanged(obj3, obj4, true);
							}
						}
					}
				}
				catch (Exception exception)
				{
					Environment.FailFast(Environment.GetResourceString("ExecutionContext_ExceptionInAsyncLocalNotification"), exception);
				}
			}
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06003AE1 RID: 15073 RVA: 0x000DECF8 File Offset: 0x000DCEF8
		// (set) Token: 0x06003AE2 RID: 15074 RVA: 0x000DED13 File Offset: 0x000DCF13
		internal LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get
			{
				if (this._logicalCallContext == null)
				{
					this._logicalCallContext = new LogicalCallContext();
				}
				return this._logicalCallContext;
			}
			[SecurityCritical]
			set
			{
				this._logicalCallContext = value;
			}
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06003AE3 RID: 15075 RVA: 0x000DED1C File Offset: 0x000DCF1C
		// (set) Token: 0x06003AE4 RID: 15076 RVA: 0x000DED37 File Offset: 0x000DCF37
		internal IllogicalCallContext IllogicalCallContext
		{
			get
			{
				if (this._illogicalCallContext == null)
				{
					this._illogicalCallContext = new IllogicalCallContext();
				}
				return this._illogicalCallContext;
			}
			set
			{
				this._illogicalCallContext = value;
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06003AE5 RID: 15077 RVA: 0x000DED40 File Offset: 0x000DCF40
		// (set) Token: 0x06003AE6 RID: 15078 RVA: 0x000DED48 File Offset: 0x000DCF48
		internal SynchronizationContext SynchronizationContext
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this._syncContext;
			}
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			set
			{
				this._syncContext = value;
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06003AE7 RID: 15079 RVA: 0x000DED51 File Offset: 0x000DCF51
		// (set) Token: 0x06003AE8 RID: 15080 RVA: 0x000DED59 File Offset: 0x000DCF59
		internal SynchronizationContext SynchronizationContextNoFlow
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this._syncContextNoFlow;
			}
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			set
			{
				this._syncContextNoFlow = value;
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06003AE9 RID: 15081 RVA: 0x000DED62 File Offset: 0x000DCF62
		// (set) Token: 0x06003AEA RID: 15082 RVA: 0x000DED6A File Offset: 0x000DCF6A
		internal HostExecutionContext HostExecutionContext
		{
			get
			{
				return this._hostExecutionContext;
			}
			set
			{
				this._hostExecutionContext = value;
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06003AEB RID: 15083 RVA: 0x000DED73 File Offset: 0x000DCF73
		// (set) Token: 0x06003AEC RID: 15084 RVA: 0x000DED7B File Offset: 0x000DCF7B
		internal SecurityContext SecurityContext
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this._securityContext;
			}
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			set
			{
				this._securityContext = value;
				if (value != null)
				{
					this._securityContext.ExecutionContext = this;
				}
			}
		}

		// Token: 0x06003AED RID: 15085 RVA: 0x000DED93 File Offset: 0x000DCF93
		public void Dispose()
		{
			if (this.IsPreAllocatedDefault)
			{
				return;
			}
			if (this._hostExecutionContext != null)
			{
				this._hostExecutionContext.Dispose();
			}
			if (this._securityContext != null)
			{
				this._securityContext.Dispose();
			}
		}

		// Token: 0x06003AEE RID: 15086 RVA: 0x000DEDC4 File Offset: 0x000DCFC4
		[SecurityCritical]
		[__DynamicallyInvokable]
		public static void Run(ExecutionContext executionContext, ContextCallback callback, object state)
		{
			if (executionContext == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullContext"));
			}
			if (!executionContext.isNewCapture)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotNewCaptureContext"));
			}
			ExecutionContext.Run(executionContext, callback, state, false);
		}

		// Token: 0x06003AEF RID: 15087 RVA: 0x000DEDFA File Offset: 0x000DCFFA
		[SecurityCritical]
		[FriendAccessAllowed]
		internal static void Run(ExecutionContext executionContext, ContextCallback callback, object state, bool preserveSyncCtx)
		{
			ExecutionContext.RunInternal(executionContext, callback, state, preserveSyncCtx);
		}

		// Token: 0x06003AF0 RID: 15088 RVA: 0x000DEE08 File Offset: 0x000DD008
		[SecurityCritical]
		[HandleProcessCorruptedStateExceptions]
		internal static void RunInternal(ExecutionContext executionContext, ContextCallback callback, object state, bool preserveSyncCtx)
		{
			if (!executionContext.IsPreAllocatedDefault)
			{
				executionContext.isNewCapture = false;
			}
			Thread currentThread = Thread.CurrentThread;
			ExecutionContextSwitcher executionContextSwitcher = default(ExecutionContextSwitcher);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				ExecutionContext.Reader executionContextReader = currentThread.GetExecutionContextReader();
				if ((executionContextReader.IsNull || executionContextReader.IsDefaultFTContext(preserveSyncCtx)) && SecurityContext.CurrentlyInDefaultFTSecurityContext(executionContextReader) && executionContext.IsDefaultFTContext(preserveSyncCtx) && executionContextReader.HasSameLocalValues(executionContext))
				{
					ExecutionContext.EstablishCopyOnWriteScope(currentThread, true, ref executionContextSwitcher);
				}
				else
				{
					if (executionContext.IsPreAllocatedDefault)
					{
						executionContext = new ExecutionContext();
					}
					executionContextSwitcher = ExecutionContext.SetExecutionContext(executionContext, preserveSyncCtx);
				}
				callback(state);
			}
			finally
			{
				executionContextSwitcher.Undo();
			}
		}

		// Token: 0x06003AF1 RID: 15089 RVA: 0x000DEEB0 File Offset: 0x000DD0B0
		[SecurityCritical]
		internal static void EstablishCopyOnWriteScope(ref ExecutionContextSwitcher ecsw)
		{
			ExecutionContext.EstablishCopyOnWriteScope(Thread.CurrentThread, false, ref ecsw);
		}

		// Token: 0x06003AF2 RID: 15090 RVA: 0x000DEEC0 File Offset: 0x000DD0C0
		[SecurityCritical]
		private static void EstablishCopyOnWriteScope(Thread currentThread, bool knownNullWindowsIdentity, ref ExecutionContextSwitcher ecsw)
		{
			ecsw.outerEC = currentThread.GetExecutionContextReader();
			ecsw.outerECBelongsToScope = currentThread.ExecutionContextBelongsToCurrentScope;
			ecsw.cachedAlwaysFlowImpersonationPolicy = SecurityContext.AlwaysFlowImpersonationPolicy;
			if (!knownNullWindowsIdentity)
			{
				ecsw.wi = SecurityContext.GetCurrentWI(ecsw.outerEC, ecsw.cachedAlwaysFlowImpersonationPolicy);
			}
			ecsw.wiIsValid = true;
			currentThread.ExecutionContextBelongsToCurrentScope = false;
			ecsw.thread = currentThread;
		}

		// Token: 0x06003AF3 RID: 15091 RVA: 0x000DEF20 File Offset: 0x000DD120
		[SecurityCritical]
		[HandleProcessCorruptedStateExceptions]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static ExecutionContextSwitcher SetExecutionContext(ExecutionContext executionContext, bool preserveSyncCtx)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			ExecutionContextSwitcher executionContextSwitcher = default(ExecutionContextSwitcher);
			Thread currentThread = Thread.CurrentThread;
			ExecutionContext.Reader executionContextReader = currentThread.GetExecutionContextReader();
			executionContextSwitcher.thread = currentThread;
			executionContextSwitcher.outerEC = executionContextReader;
			executionContextSwitcher.outerECBelongsToScope = currentThread.ExecutionContextBelongsToCurrentScope;
			if (preserveSyncCtx)
			{
				executionContext.SynchronizationContext = executionContextReader.SynchronizationContext;
			}
			executionContext.SynchronizationContextNoFlow = executionContextReader.SynchronizationContextNoFlow;
			currentThread.SetExecutionContext(executionContext, true);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				ExecutionContext.OnAsyncLocalContextChanged(executionContextReader.DangerousGetRawExecutionContext(), executionContext);
				SecurityContext securityContext = executionContext.SecurityContext;
				if (securityContext != null)
				{
					SecurityContext.Reader securityContext2 = executionContextReader.SecurityContext;
					executionContextSwitcher.scsw = SecurityContext.SetSecurityContext(securityContext, securityContext2, false, ref stackCrawlMark);
				}
				else if (!SecurityContext.CurrentlyInDefaultFTSecurityContext(executionContextSwitcher.outerEC))
				{
					SecurityContext.Reader securityContext3 = executionContextReader.SecurityContext;
					executionContextSwitcher.scsw = SecurityContext.SetSecurityContext(SecurityContext.FullTrustSecurityContext, securityContext3, false, ref stackCrawlMark);
				}
				HostExecutionContext hostExecutionContext = executionContext.HostExecutionContext;
				if (hostExecutionContext != null)
				{
					executionContextSwitcher.hecsw = HostExecutionContextManager.SetHostExecutionContextInternal(hostExecutionContext);
				}
			}
			catch
			{
				executionContextSwitcher.UndoNoThrow();
				throw;
			}
			return executionContextSwitcher;
		}

		// Token: 0x06003AF4 RID: 15092 RVA: 0x000DF028 File Offset: 0x000DD228
		[SecuritySafeCritical]
		public ExecutionContext CreateCopy()
		{
			if (!this.isNewCapture)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotCopyUsedContext"));
			}
			ExecutionContext executionContext = new ExecutionContext();
			executionContext.isNewCapture = true;
			executionContext._syncContext = ((this._syncContext == null) ? null : this._syncContext.CreateCopy());
			executionContext._localValues = this._localValues;
			executionContext._localChangeNotifications = this._localChangeNotifications;
			executionContext._hostExecutionContext = ((this._hostExecutionContext == null) ? null : this._hostExecutionContext.CreateCopy());
			if (this._securityContext != null)
			{
				executionContext._securityContext = this._securityContext.CreateCopy();
				executionContext._securityContext.ExecutionContext = executionContext;
			}
			if (this._logicalCallContext != null)
			{
				executionContext.LogicalCallContext = (LogicalCallContext)this.LogicalCallContext.Clone();
			}
			return executionContext;
		}

		// Token: 0x06003AF5 RID: 15093 RVA: 0x000DF0F0 File Offset: 0x000DD2F0
		[SecuritySafeCritical]
		internal ExecutionContext CreateMutableCopy()
		{
			ExecutionContext executionContext = new ExecutionContext();
			executionContext._syncContext = this._syncContext;
			executionContext._syncContextNoFlow = this._syncContextNoFlow;
			executionContext._hostExecutionContext = ((this._hostExecutionContext == null) ? null : this._hostExecutionContext.CreateCopy());
			if (this._securityContext != null)
			{
				executionContext._securityContext = this._securityContext.CreateMutableCopy();
				executionContext._securityContext.ExecutionContext = executionContext;
			}
			if (this._logicalCallContext != null)
			{
				executionContext.LogicalCallContext = (LogicalCallContext)this.LogicalCallContext.Clone();
			}
			if (this._illogicalCallContext != null)
			{
				executionContext.IllogicalCallContext = this.IllogicalCallContext.CreateCopy();
			}
			executionContext._localValues = this._localValues;
			executionContext._localChangeNotifications = this._localChangeNotifications;
			executionContext.isFlowSuppressed = this.isFlowSuppressed;
			return executionContext;
		}

		// Token: 0x06003AF6 RID: 15094 RVA: 0x000DF1B8 File Offset: 0x000DD3B8
		[SecurityCritical]
		public static AsyncFlowControl SuppressFlow()
		{
			if (ExecutionContext.IsFlowSuppressed())
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotSupressFlowMultipleTimes"));
			}
			AsyncFlowControl result = default(AsyncFlowControl);
			result.Setup();
			return result;
		}

		// Token: 0x06003AF7 RID: 15095 RVA: 0x000DF1EC File Offset: 0x000DD3EC
		[SecuritySafeCritical]
		public static void RestoreFlow()
		{
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			if (!mutableExecutionContext.isFlowSuppressed)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotRestoreUnsupressedFlow"));
			}
			mutableExecutionContext.isFlowSuppressed = false;
		}

		// Token: 0x06003AF8 RID: 15096 RVA: 0x000DF224 File Offset: 0x000DD424
		public static bool IsFlowSuppressed()
		{
			return Thread.CurrentThread.GetExecutionContextReader().IsFlowSuppressed;
		}

		// Token: 0x06003AF9 RID: 15097 RVA: 0x000DF244 File Offset: 0x000DD444
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static ExecutionContext Capture()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ExecutionContext.Capture(ref stackCrawlMark, ExecutionContext.CaptureOptions.None);
		}

		// Token: 0x06003AFA RID: 15098 RVA: 0x000DF25C File Offset: 0x000DD45C
		[SecuritySafeCritical]
		[FriendAccessAllowed]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static ExecutionContext FastCapture()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ExecutionContext.Capture(ref stackCrawlMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
		}

		// Token: 0x06003AFB RID: 15099 RVA: 0x000DF274 File Offset: 0x000DD474
		[SecurityCritical]
		internal static ExecutionContext Capture(ref StackCrawlMark stackMark, ExecutionContext.CaptureOptions options)
		{
			ExecutionContext.Reader executionContextReader = Thread.CurrentThread.GetExecutionContextReader();
			if (executionContextReader.IsFlowSuppressed)
			{
				return null;
			}
			SecurityContext securityContext = SecurityContext.Capture(executionContextReader, ref stackMark);
			HostExecutionContext hostExecutionContext = HostExecutionContextManager.CaptureHostExecutionContext();
			SynchronizationContext synchronizationContext = null;
			LogicalCallContext logicalCallContext = null;
			if (!executionContextReader.IsNull)
			{
				if ((options & ExecutionContext.CaptureOptions.IgnoreSyncCtx) == ExecutionContext.CaptureOptions.None)
				{
					synchronizationContext = ((executionContextReader.SynchronizationContext == null) ? null : executionContextReader.SynchronizationContext.CreateCopy());
				}
				if (executionContextReader.LogicalCallContext.HasInfo)
				{
					logicalCallContext = executionContextReader.LogicalCallContext.Clone();
				}
			}
			IAsyncLocalValueMap asyncLocalValueMap = null;
			IAsyncLocal[] array = null;
			if (!executionContextReader.IsNull)
			{
				asyncLocalValueMap = executionContextReader.DangerousGetRawExecutionContext()._localValues;
				array = executionContextReader.DangerousGetRawExecutionContext()._localChangeNotifications;
			}
			if ((options & ExecutionContext.CaptureOptions.OptimizeDefaultCase) != ExecutionContext.CaptureOptions.None && securityContext == null && hostExecutionContext == null && synchronizationContext == null && (logicalCallContext == null || !logicalCallContext.HasInfo) && asyncLocalValueMap == null && array == null)
			{
				return ExecutionContext.s_dummyDefaultEC;
			}
			ExecutionContext executionContext = new ExecutionContext();
			executionContext.SecurityContext = securityContext;
			if (executionContext.SecurityContext != null)
			{
				executionContext.SecurityContext.ExecutionContext = executionContext;
			}
			executionContext._hostExecutionContext = hostExecutionContext;
			executionContext._syncContext = synchronizationContext;
			executionContext.LogicalCallContext = logicalCallContext;
			executionContext._localValues = asyncLocalValueMap;
			executionContext._localChangeNotifications = array;
			executionContext.isNewCapture = true;
			return executionContext;
		}

		// Token: 0x06003AFC RID: 15100 RVA: 0x000DF3A4 File Offset: 0x000DD5A4
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			if (this._logicalCallContext != null)
			{
				info.AddValue("LogicalCallContext", this._logicalCallContext, typeof(LogicalCallContext));
			}
		}

		// Token: 0x06003AFD RID: 15101 RVA: 0x000DF3D8 File Offset: 0x000DD5D8
		[SecurityCritical]
		private ExecutionContext(SerializationInfo info, StreamingContext context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name.Equals("LogicalCallContext"))
				{
					this._logicalCallContext = (LogicalCallContext)enumerator.Value;
				}
			}
		}

		// Token: 0x06003AFE RID: 15102 RVA: 0x000DF420 File Offset: 0x000DD620
		[SecurityCritical]
		internal bool IsDefaultFTContext(bool ignoreSyncCtx)
		{
			return this._hostExecutionContext == null && (ignoreSyncCtx || this._syncContext == null) && (this._securityContext == null || this._securityContext.IsDefaultFTSecurityContext()) && (this._logicalCallContext == null || !this._logicalCallContext.HasInfo) && (this._illogicalCallContext == null || !this._illogicalCallContext.HasUserData);
		}

		// Token: 0x040018D3 RID: 6355
		private HostExecutionContext _hostExecutionContext;

		// Token: 0x040018D4 RID: 6356
		private SynchronizationContext _syncContext;

		// Token: 0x040018D5 RID: 6357
		private SynchronizationContext _syncContextNoFlow;

		// Token: 0x040018D6 RID: 6358
		private SecurityContext _securityContext;

		// Token: 0x040018D7 RID: 6359
		[SecurityCritical]
		private LogicalCallContext _logicalCallContext;

		// Token: 0x040018D8 RID: 6360
		private IllogicalCallContext _illogicalCallContext;

		// Token: 0x040018D9 RID: 6361
		private ExecutionContext.Flags _flags;

		// Token: 0x040018DA RID: 6362
		private IAsyncLocalValueMap _localValues;

		// Token: 0x040018DB RID: 6363
		private IAsyncLocal[] _localChangeNotifications;

		// Token: 0x040018DC RID: 6364
		private static readonly ExecutionContext s_dummyDefaultEC = new ExecutionContext(true);

		// Token: 0x02000BB7 RID: 2999
		private enum Flags
		{
			// Token: 0x04003528 RID: 13608
			None,
			// Token: 0x04003529 RID: 13609
			IsNewCapture,
			// Token: 0x0400352A RID: 13610
			IsFlowSuppressed,
			// Token: 0x0400352B RID: 13611
			IsPreAllocatedDefault = 4
		}

		// Token: 0x02000BB8 RID: 3000
		internal struct Reader
		{
			// Token: 0x06006E28 RID: 28200 RVA: 0x0017ADBD File Offset: 0x00178FBD
			public Reader(ExecutionContext ec)
			{
				this.m_ec = ec;
			}

			// Token: 0x06006E29 RID: 28201 RVA: 0x0017ADC6 File Offset: 0x00178FC6
			public ExecutionContext DangerousGetRawExecutionContext()
			{
				return this.m_ec;
			}

			// Token: 0x17001300 RID: 4864
			// (get) Token: 0x06006E2A RID: 28202 RVA: 0x0017ADCE File Offset: 0x00178FCE
			public bool IsNull
			{
				get
				{
					return this.m_ec == null;
				}
			}

			// Token: 0x06006E2B RID: 28203 RVA: 0x0017ADD9 File Offset: 0x00178FD9
			[SecurityCritical]
			public bool IsDefaultFTContext(bool ignoreSyncCtx)
			{
				return this.m_ec.IsDefaultFTContext(ignoreSyncCtx);
			}

			// Token: 0x17001301 RID: 4865
			// (get) Token: 0x06006E2C RID: 28204 RVA: 0x0017ADE7 File Offset: 0x00178FE7
			public bool IsFlowSuppressed
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return !this.IsNull && this.m_ec.isFlowSuppressed;
				}
			}

			// Token: 0x06006E2D RID: 28205 RVA: 0x0017ADFE File Offset: 0x00178FFE
			public bool IsSame(ExecutionContext.Reader other)
			{
				return this.m_ec == other.m_ec;
			}

			// Token: 0x17001302 RID: 4866
			// (get) Token: 0x06006E2E RID: 28206 RVA: 0x0017AE0E File Offset: 0x0017900E
			public SynchronizationContext SynchronizationContext
			{
				get
				{
					if (!this.IsNull)
					{
						return this.m_ec.SynchronizationContext;
					}
					return null;
				}
			}

			// Token: 0x17001303 RID: 4867
			// (get) Token: 0x06006E2F RID: 28207 RVA: 0x0017AE25 File Offset: 0x00179025
			public SynchronizationContext SynchronizationContextNoFlow
			{
				get
				{
					if (!this.IsNull)
					{
						return this.m_ec.SynchronizationContextNoFlow;
					}
					return null;
				}
			}

			// Token: 0x17001304 RID: 4868
			// (get) Token: 0x06006E30 RID: 28208 RVA: 0x0017AE3C File Offset: 0x0017903C
			public SecurityContext.Reader SecurityContext
			{
				[SecurityCritical]
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					return new SecurityContext.Reader(this.IsNull ? null : this.m_ec.SecurityContext);
				}
			}

			// Token: 0x17001305 RID: 4869
			// (get) Token: 0x06006E31 RID: 28209 RVA: 0x0017AE59 File Offset: 0x00179059
			public LogicalCallContext.Reader LogicalCallContext
			{
				[SecurityCritical]
				get
				{
					return new LogicalCallContext.Reader(this.IsNull ? null : this.m_ec.LogicalCallContext);
				}
			}

			// Token: 0x17001306 RID: 4870
			// (get) Token: 0x06006E32 RID: 28210 RVA: 0x0017AE76 File Offset: 0x00179076
			public IllogicalCallContext.Reader IllogicalCallContext
			{
				[SecurityCritical]
				get
				{
					return new IllogicalCallContext.Reader(this.IsNull ? null : this.m_ec.IllogicalCallContext);
				}
			}

			// Token: 0x06006E33 RID: 28211 RVA: 0x0017AE94 File Offset: 0x00179094
			[SecurityCritical]
			public object GetLocalValue(IAsyncLocal local)
			{
				if (this.IsNull)
				{
					return null;
				}
				if (this.m_ec._localValues == null)
				{
					return null;
				}
				object result;
				this.m_ec._localValues.TryGetValue(local, out result);
				return result;
			}

			// Token: 0x06006E34 RID: 28212 RVA: 0x0017AED0 File Offset: 0x001790D0
			[SecurityCritical]
			public bool HasSameLocalValues(ExecutionContext other)
			{
				IAsyncLocalValueMap asyncLocalValueMap = this.IsNull ? null : this.m_ec._localValues;
				IAsyncLocalValueMap asyncLocalValueMap2 = (other == null) ? null : other._localValues;
				return asyncLocalValueMap == asyncLocalValueMap2;
			}

			// Token: 0x06006E35 RID: 28213 RVA: 0x0017AF05 File Offset: 0x00179105
			[SecurityCritical]
			public bool HasLocalValues()
			{
				return !this.IsNull && this.m_ec._localValues != null;
			}

			// Token: 0x0400352C RID: 13612
			private ExecutionContext m_ec;
		}

		// Token: 0x02000BB9 RID: 3001
		[Flags]
		internal enum CaptureOptions
		{
			// Token: 0x0400352E RID: 13614
			None = 0,
			// Token: 0x0400352F RID: 13615
			IgnoreSyncCtx = 1,
			// Token: 0x04003530 RID: 13616
			OptimizeDefaultCase = 2
		}
	}
}
