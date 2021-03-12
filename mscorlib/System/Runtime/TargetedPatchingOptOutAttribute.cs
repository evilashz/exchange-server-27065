using System;

namespace System.Runtime
{
	// Token: 0x020006EC RID: 1772
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public sealed class TargetedPatchingOptOutAttribute : Attribute
	{
		// Token: 0x06005012 RID: 20498 RVA: 0x00119BF6 File Offset: 0x00117DF6
		public TargetedPatchingOptOutAttribute(string reason)
		{
			this.m_reason = reason;
		}

		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x06005013 RID: 20499 RVA: 0x00119C05 File Offset: 0x00117E05
		public string Reason
		{
			get
			{
				return this.m_reason;
			}
		}

		// Token: 0x06005014 RID: 20500 RVA: 0x00119C0D File Offset: 0x00117E0D
		private TargetedPatchingOptOutAttribute()
		{
		}

		// Token: 0x04002341 RID: 9025
		private string m_reason;
	}
}
