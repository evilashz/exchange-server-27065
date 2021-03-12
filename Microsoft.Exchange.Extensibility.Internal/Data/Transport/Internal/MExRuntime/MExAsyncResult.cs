using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Transport.Internal.MExRuntime
{
	// Token: 0x02000076 RID: 118
	internal sealed class MExAsyncResult : IAsyncResult
	{
		// Token: 0x0600039A RID: 922 RVA: 0x00012381 File Offset: 0x00010581
		internal MExAsyncResult(AsyncCallback completionCallback, object state)
		{
			this.asyncState = state;
			this.asyncWaitHandle = null;
			this.completedSynchronously = true;
			this.isCompleted = false;
			this.completionCallback = completionCallback;
			this.asyncException = null;
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600039B RID: 923 RVA: 0x000123BE File Offset: 0x000105BE
		public object AsyncState
		{
			get
			{
				return this.asyncState;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600039C RID: 924 RVA: 0x000123C8 File Offset: 0x000105C8
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				lock (this.syncRoot)
				{
					if (this.asyncWaitHandle == null)
					{
						this.asyncWaitHandle = new ManualResetEvent(false);
					}
				}
				return this.asyncWaitHandle;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0001241C File Offset: 0x0001061C
		public bool CompletedSynchronously
		{
			get
			{
				return this.completedSynchronously;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600039E RID: 926 RVA: 0x00012424 File Offset: 0x00010624
		public bool IsCompleted
		{
			get
			{
				return this.isCompleted;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600039F RID: 927 RVA: 0x0001242C File Offset: 0x0001062C
		// (set) Token: 0x060003A0 RID: 928 RVA: 0x00012434 File Offset: 0x00010634
		internal Exception AsyncException
		{
			get
			{
				return this.asyncException;
			}
			set
			{
				this.asyncException = value;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0001243D File Offset: 0x0001063D
		// (set) Token: 0x060003A2 RID: 930 RVA: 0x00012445 File Offset: 0x00010645
		internal string FaultyAgentName
		{
			get
			{
				return this.faultyAgentName;
			}
			set
			{
				this.faultyAgentName = value;
			}
		}

		// Token: 0x170000EB RID: 235
		// (set) Token: 0x060003A3 RID: 931 RVA: 0x0001244E File Offset: 0x0001064E
		internal string EventTopic
		{
			set
			{
				this.eventTopic = value;
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00012458 File Offset: 0x00010658
		internal static void WrapAndRethrowException(Exception e, LocalizedString message)
		{
			Exception ex = null;
			Type type = e.GetType();
			Type[] types = new Type[]
			{
				typeof(string),
				typeof(Exception)
			};
			object[] array = new object[]
			{
				message,
				e
			};
			Type[] types2 = new Type[]
			{
				typeof(LocalizedString),
				typeof(Exception)
			};
			object[] array2 = new object[]
			{
				message,
				e
			};
			do
			{
				try
				{
					bool flag = true;
					ConstructorInfo constructor = type.GetConstructor(types2);
					if (constructor == null)
					{
						flag = false;
						constructor = type.GetConstructor(types);
					}
					if (constructor != null)
					{
						ex = (Exception)Activator.CreateInstance(type, flag ? array2 : array);
					}
				}
				catch (TargetInvocationException)
				{
					ex = null;
				}
				catch (MemberAccessException)
				{
					ex = null;
				}
				catch (InvalidComObjectException)
				{
					ex = null;
				}
				catch (COMException)
				{
					ex = null;
				}
				catch (TypeLoadException)
				{
					ex = null;
				}
				if (ex == null)
				{
					type = type.BaseType;
				}
			}
			while (ex == null && type != typeof(object));
			throw ex;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x000125B0 File Offset: 0x000107B0
		internal void SetAsync()
		{
			this.completedSynchronously = false;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x000125BC File Offset: 0x000107BC
		internal void InvokeCompleted()
		{
			lock (this.syncRoot)
			{
				this.isCompleted = true;
				if (this.asyncWaitHandle != null)
				{
					this.asyncWaitHandle.Set();
				}
			}
			if (this.completionCallback != null)
			{
				this.completionCallback(this);
			}
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00012628 File Offset: 0x00010828
		internal void EndInvoke()
		{
			if (this.asyncException != null)
			{
				MExAsyncResult.WrapAndRethrowException(this.asyncException, new LocalizedString(MExRuntimeStrings.AgentFault(this.faultyAgentName, this.eventTopic)));
			}
			WaitHandle waitHandle = null;
			lock (this.syncRoot)
			{
				if (!this.isCompleted)
				{
					waitHandle = this.AsyncWaitHandle;
				}
			}
			if (waitHandle != null)
			{
				waitHandle.WaitOne();
			}
			if (this.asyncException != null)
			{
				MExAsyncResult.WrapAndRethrowException(this.asyncException, new LocalizedString(MExRuntimeStrings.AgentFault(this.faultyAgentName, this.eventTopic)));
			}
		}

		// Token: 0x04000470 RID: 1136
		private object syncRoot = new object();

		// Token: 0x04000471 RID: 1137
		private object asyncState;

		// Token: 0x04000472 RID: 1138
		private ManualResetEvent asyncWaitHandle;

		// Token: 0x04000473 RID: 1139
		private bool completedSynchronously;

		// Token: 0x04000474 RID: 1140
		private bool isCompleted;

		// Token: 0x04000475 RID: 1141
		private AsyncCallback completionCallback;

		// Token: 0x04000476 RID: 1142
		private Exception asyncException;

		// Token: 0x04000477 RID: 1143
		private string faultyAgentName;

		// Token: 0x04000478 RID: 1144
		private string eventTopic;
	}
}
