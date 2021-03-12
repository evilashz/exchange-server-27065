using System;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008B7 RID: 2231
	[Serializable]
	public sealed class RuntimeWrappedException : Exception
	{
		// Token: 0x06005CC7 RID: 23751 RVA: 0x00144F3E File Offset: 0x0014313E
		private RuntimeWrappedException(object thrownObject) : base(Environment.GetResourceString("RuntimeWrappedException"))
		{
			base.SetErrorCode(-2146233026);
			this.m_wrappedException = thrownObject;
		}

		// Token: 0x1700100A RID: 4106
		// (get) Token: 0x06005CC8 RID: 23752 RVA: 0x00144F62 File Offset: 0x00143162
		public object WrappedException
		{
			get
			{
				return this.m_wrappedException;
			}
		}

		// Token: 0x06005CC9 RID: 23753 RVA: 0x00144F6A File Offset: 0x0014316A
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("WrappedException", this.m_wrappedException, typeof(object));
		}

		// Token: 0x06005CCA RID: 23754 RVA: 0x00144F9D File Offset: 0x0014319D
		internal RuntimeWrappedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.m_wrappedException = info.GetValue("WrappedException", typeof(object));
		}

		// Token: 0x04002982 RID: 10626
		private object m_wrappedException;
	}
}
