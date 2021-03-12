using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000717 RID: 1815
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct StreamingContext
	{
		// Token: 0x0600510F RID: 20751 RVA: 0x0011C1D2 File Offset: 0x0011A3D2
		public StreamingContext(StreamingContextStates state)
		{
			this = new StreamingContext(state, null);
		}

		// Token: 0x06005110 RID: 20752 RVA: 0x0011C1DC File Offset: 0x0011A3DC
		public StreamingContext(StreamingContextStates state, object additional)
		{
			this.m_state = state;
			this.m_additionalContext = additional;
		}

		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x06005111 RID: 20753 RVA: 0x0011C1EC File Offset: 0x0011A3EC
		public object Context
		{
			get
			{
				return this.m_additionalContext;
			}
		}

		// Token: 0x06005112 RID: 20754 RVA: 0x0011C1F4 File Offset: 0x0011A3F4
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is StreamingContext && (((StreamingContext)obj).m_additionalContext == this.m_additionalContext && ((StreamingContext)obj).m_state == this.m_state);
		}

		// Token: 0x06005113 RID: 20755 RVA: 0x0011C229 File Offset: 0x0011A429
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return (int)this.m_state;
		}

		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x06005114 RID: 20756 RVA: 0x0011C231 File Offset: 0x0011A431
		public StreamingContextStates State
		{
			get
			{
				return this.m_state;
			}
		}

		// Token: 0x040023A6 RID: 9126
		internal object m_additionalContext;

		// Token: 0x040023A7 RID: 9127
		internal StreamingContextStates m_state;
	}
}
