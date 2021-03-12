using System;

namespace System.Runtime.ExceptionServices
{
	// Token: 0x0200077D RID: 1917
	[__DynamicallyInvokable]
	public sealed class ExceptionDispatchInfo
	{
		// Token: 0x060053E4 RID: 21476 RVA: 0x0012974C File Offset: 0x0012794C
		private ExceptionDispatchInfo(Exception exception)
		{
			this.m_Exception = exception;
			this.m_remoteStackTrace = exception.RemoteStackTrace;
			object stackTrace;
			object dynamicMethods;
			this.m_Exception.GetStackTracesDeepCopy(out stackTrace, out dynamicMethods);
			this.m_stackTrace = stackTrace;
			this.m_dynamicMethods = dynamicMethods;
			this.m_IPForWatsonBuckets = exception.IPForWatsonBuckets;
			this.m_WatsonBuckets = exception.WatsonBuckets;
		}

		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x060053E5 RID: 21477 RVA: 0x001297A7 File Offset: 0x001279A7
		internal UIntPtr IPForWatsonBuckets
		{
			get
			{
				return this.m_IPForWatsonBuckets;
			}
		}

		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x060053E6 RID: 21478 RVA: 0x001297AF File Offset: 0x001279AF
		internal object WatsonBuckets
		{
			get
			{
				return this.m_WatsonBuckets;
			}
		}

		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x060053E7 RID: 21479 RVA: 0x001297B7 File Offset: 0x001279B7
		internal object BinaryStackTraceArray
		{
			get
			{
				return this.m_stackTrace;
			}
		}

		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x060053E8 RID: 21480 RVA: 0x001297BF File Offset: 0x001279BF
		internal object DynamicMethodArray
		{
			get
			{
				return this.m_dynamicMethods;
			}
		}

		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x060053E9 RID: 21481 RVA: 0x001297C7 File Offset: 0x001279C7
		internal string RemoteStackTrace
		{
			get
			{
				return this.m_remoteStackTrace;
			}
		}

		// Token: 0x060053EA RID: 21482 RVA: 0x001297CF File Offset: 0x001279CF
		[__DynamicallyInvokable]
		public static ExceptionDispatchInfo Capture(Exception source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source", Environment.GetResourceString("ArgumentNull_Obj"));
			}
			return new ExceptionDispatchInfo(source);
		}

		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x060053EB RID: 21483 RVA: 0x001297EF File Offset: 0x001279EF
		[__DynamicallyInvokable]
		public Exception SourceException
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_Exception;
			}
		}

		// Token: 0x060053EC RID: 21484 RVA: 0x001297F7 File Offset: 0x001279F7
		[__DynamicallyInvokable]
		public void Throw()
		{
			this.m_Exception.RestoreExceptionDispatchInfo(this);
			throw this.m_Exception;
		}

		// Token: 0x04002665 RID: 9829
		private Exception m_Exception;

		// Token: 0x04002666 RID: 9830
		private string m_remoteStackTrace;

		// Token: 0x04002667 RID: 9831
		private object m_stackTrace;

		// Token: 0x04002668 RID: 9832
		private object m_dynamicMethods;

		// Token: 0x04002669 RID: 9833
		private UIntPtr m_IPForWatsonBuckets;

		// Token: 0x0400266A RID: 9834
		private object m_WatsonBuckets;
	}
}
