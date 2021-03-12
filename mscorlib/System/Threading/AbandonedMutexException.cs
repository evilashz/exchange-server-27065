using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x020004B7 RID: 1207
	[ComVisible(false)]
	[__DynamicallyInvokable]
	[Serializable]
	public class AbandonedMutexException : SystemException
	{
		// Token: 0x06003A4E RID: 14926 RVA: 0x000DD8FB File Offset: 0x000DBAFB
		[__DynamicallyInvokable]
		public AbandonedMutexException() : base(Environment.GetResourceString("Threading.AbandonedMutexException"))
		{
			base.SetErrorCode(-2146233043);
		}

		// Token: 0x06003A4F RID: 14927 RVA: 0x000DD91F File Offset: 0x000DBB1F
		[__DynamicallyInvokable]
		public AbandonedMutexException(string message) : base(message)
		{
			base.SetErrorCode(-2146233043);
		}

		// Token: 0x06003A50 RID: 14928 RVA: 0x000DD93A File Offset: 0x000DBB3A
		[__DynamicallyInvokable]
		public AbandonedMutexException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146233043);
		}

		// Token: 0x06003A51 RID: 14929 RVA: 0x000DD956 File Offset: 0x000DBB56
		[__DynamicallyInvokable]
		public AbandonedMutexException(int location, WaitHandle handle) : base(Environment.GetResourceString("Threading.AbandonedMutexException"))
		{
			base.SetErrorCode(-2146233043);
			this.SetupException(location, handle);
		}

		// Token: 0x06003A52 RID: 14930 RVA: 0x000DD982 File Offset: 0x000DBB82
		[__DynamicallyInvokable]
		public AbandonedMutexException(string message, int location, WaitHandle handle) : base(message)
		{
			base.SetErrorCode(-2146233043);
			this.SetupException(location, handle);
		}

		// Token: 0x06003A53 RID: 14931 RVA: 0x000DD9A5 File Offset: 0x000DBBA5
		[__DynamicallyInvokable]
		public AbandonedMutexException(string message, Exception inner, int location, WaitHandle handle) : base(message, inner)
		{
			base.SetErrorCode(-2146233043);
			this.SetupException(location, handle);
		}

		// Token: 0x06003A54 RID: 14932 RVA: 0x000DD9CA File Offset: 0x000DBBCA
		private void SetupException(int location, WaitHandle handle)
		{
			this.m_MutexIndex = location;
			if (handle != null)
			{
				this.m_Mutex = (handle as Mutex);
			}
		}

		// Token: 0x06003A55 RID: 14933 RVA: 0x000DD9E2 File Offset: 0x000DBBE2
		protected AbandonedMutexException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06003A56 RID: 14934 RVA: 0x000DD9F3 File Offset: 0x000DBBF3
		[__DynamicallyInvokable]
		public Mutex Mutex
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_Mutex;
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06003A57 RID: 14935 RVA: 0x000DD9FB File Offset: 0x000DBBFB
		[__DynamicallyInvokable]
		public int MutexIndex
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_MutexIndex;
			}
		}

		// Token: 0x040018A9 RID: 6313
		private int m_MutexIndex = -1;

		// Token: 0x040018AA RID: 6314
		private Mutex m_Mutex;
	}
}
