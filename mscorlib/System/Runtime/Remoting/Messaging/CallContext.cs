using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200085E RID: 2142
	[SecurityCritical]
	[ComVisible(true)]
	[Serializable]
	public sealed class CallContext
	{
		// Token: 0x06005B8E RID: 23438 RVA: 0x001404C4 File Offset: 0x0013E6C4
		private CallContext()
		{
		}

		// Token: 0x06005B8F RID: 23439 RVA: 0x001404CC File Offset: 0x0013E6CC
		internal static LogicalCallContext SetLogicalCallContext(LogicalCallContext callCtx)
		{
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			LogicalCallContext logicalCallContext = mutableExecutionContext.LogicalCallContext;
			mutableExecutionContext.LogicalCallContext = callCtx;
			return logicalCallContext;
		}

		// Token: 0x06005B90 RID: 23440 RVA: 0x001404F4 File Offset: 0x0013E6F4
		[SecurityCritical]
		public static void FreeNamedDataSlot(string name)
		{
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			mutableExecutionContext.LogicalCallContext.FreeNamedDataSlot(name);
			mutableExecutionContext.IllogicalCallContext.FreeNamedDataSlot(name);
		}

		// Token: 0x06005B91 RID: 23441 RVA: 0x00140524 File Offset: 0x0013E724
		[SecurityCritical]
		public static object LogicalGetData(string name)
		{
			return Thread.CurrentThread.GetExecutionContextReader().LogicalCallContext.GetData(name);
		}

		// Token: 0x06005B92 RID: 23442 RVA: 0x0014054C File Offset: 0x0013E74C
		private static object IllogicalGetData(string name)
		{
			return Thread.CurrentThread.GetExecutionContextReader().IllogicalCallContext.GetData(name);
		}

		// Token: 0x17000FD4 RID: 4052
		// (get) Token: 0x06005B93 RID: 23443 RVA: 0x00140574 File Offset: 0x0013E774
		// (set) Token: 0x06005B94 RID: 23444 RVA: 0x0014059B File Offset: 0x0013E79B
		internal static IPrincipal Principal
		{
			[SecurityCritical]
			get
			{
				return Thread.CurrentThread.GetExecutionContextReader().LogicalCallContext.Principal;
			}
			[SecurityCritical]
			set
			{
				Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext.Principal = value;
			}
		}

		// Token: 0x17000FD5 RID: 4053
		// (get) Token: 0x06005B95 RID: 23445 RVA: 0x001405B4 File Offset: 0x0013E7B4
		// (set) Token: 0x06005B96 RID: 23446 RVA: 0x001405F0 File Offset: 0x0013E7F0
		public static object HostContext
		{
			[SecurityCritical]
			get
			{
				ExecutionContext.Reader executionContextReader = Thread.CurrentThread.GetExecutionContextReader();
				object hostContext = executionContextReader.IllogicalCallContext.HostContext;
				if (hostContext == null)
				{
					hostContext = executionContextReader.LogicalCallContext.HostContext;
				}
				return hostContext;
			}
			[SecurityCritical]
			set
			{
				ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
				if (value is ILogicalThreadAffinative)
				{
					mutableExecutionContext.IllogicalCallContext.HostContext = null;
					mutableExecutionContext.LogicalCallContext.HostContext = value;
					return;
				}
				mutableExecutionContext.IllogicalCallContext.HostContext = value;
				mutableExecutionContext.LogicalCallContext.HostContext = null;
			}
		}

		// Token: 0x06005B97 RID: 23447 RVA: 0x00140644 File Offset: 0x0013E844
		[SecurityCritical]
		public static object GetData(string name)
		{
			object obj = CallContext.LogicalGetData(name);
			if (obj == null)
			{
				return CallContext.IllogicalGetData(name);
			}
			return obj;
		}

		// Token: 0x06005B98 RID: 23448 RVA: 0x00140664 File Offset: 0x0013E864
		[SecurityCritical]
		public static void SetData(string name, object data)
		{
			if (data is ILogicalThreadAffinative)
			{
				CallContext.LogicalSetData(name, data);
				return;
			}
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			mutableExecutionContext.LogicalCallContext.FreeNamedDataSlot(name);
			mutableExecutionContext.IllogicalCallContext.SetData(name, data);
		}

		// Token: 0x06005B99 RID: 23449 RVA: 0x001406A8 File Offset: 0x0013E8A8
		[SecurityCritical]
		public static void LogicalSetData(string name, object data)
		{
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			mutableExecutionContext.IllogicalCallContext.FreeNamedDataSlot(name);
			mutableExecutionContext.LogicalCallContext.SetData(name, data);
		}

		// Token: 0x06005B9A RID: 23450 RVA: 0x001406DC File Offset: 0x0013E8DC
		[SecurityCritical]
		public static Header[] GetHeaders()
		{
			LogicalCallContext logicalCallContext = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
			return logicalCallContext.InternalGetHeaders();
		}

		// Token: 0x06005B9B RID: 23451 RVA: 0x00140700 File Offset: 0x0013E900
		[SecurityCritical]
		public static void SetHeaders(Header[] headers)
		{
			LogicalCallContext logicalCallContext = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
			logicalCallContext.InternalSetHeaders(headers);
		}
	}
}
