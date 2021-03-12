using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008B5 RID: 2229
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class ReferenceAssemblyAttribute : Attribute
	{
		// Token: 0x06005CC1 RID: 23745 RVA: 0x00144F06 File Offset: 0x00143106
		[__DynamicallyInvokable]
		public ReferenceAssemblyAttribute()
		{
		}

		// Token: 0x06005CC2 RID: 23746 RVA: 0x00144F0E File Offset: 0x0014310E
		[__DynamicallyInvokable]
		public ReferenceAssemblyAttribute(string description)
		{
			this._description = description;
		}

		// Token: 0x17001008 RID: 4104
		// (get) Token: 0x06005CC3 RID: 23747 RVA: 0x00144F1D File Offset: 0x0014311D
		[__DynamicallyInvokable]
		public string Description
		{
			[__DynamicallyInvokable]
			get
			{
				return this._description;
			}
		}

		// Token: 0x04002980 RID: 10624
		private string _description;
	}
}
