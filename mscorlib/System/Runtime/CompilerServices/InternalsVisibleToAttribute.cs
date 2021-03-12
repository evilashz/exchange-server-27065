using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200088C RID: 2188
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
	[__DynamicallyInvokable]
	public sealed class InternalsVisibleToAttribute : Attribute
	{
		// Token: 0x06005C91 RID: 23697 RVA: 0x00144CB8 File Offset: 0x00142EB8
		[__DynamicallyInvokable]
		public InternalsVisibleToAttribute(string assemblyName)
		{
			this._assemblyName = assemblyName;
		}

		// Token: 0x17000FFF RID: 4095
		// (get) Token: 0x06005C92 RID: 23698 RVA: 0x00144CCE File Offset: 0x00142ECE
		[__DynamicallyInvokable]
		public string AssemblyName
		{
			[__DynamicallyInvokable]
			get
			{
				return this._assemblyName;
			}
		}

		// Token: 0x17001000 RID: 4096
		// (get) Token: 0x06005C93 RID: 23699 RVA: 0x00144CD6 File Offset: 0x00142ED6
		// (set) Token: 0x06005C94 RID: 23700 RVA: 0x00144CDE File Offset: 0x00142EDE
		public bool AllInternalsVisible
		{
			get
			{
				return this._allInternalsVisible;
			}
			set
			{
				this._allInternalsVisible = value;
			}
		}

		// Token: 0x0400295D RID: 10589
		private string _assemblyName;

		// Token: 0x0400295E RID: 10590
		private bool _allInternalsVisible = true;
	}
}
