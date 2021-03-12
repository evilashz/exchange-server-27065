using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000898 RID: 2200
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	[Serializable]
	public sealed class DependencyAttribute : Attribute
	{
		// Token: 0x06005CA2 RID: 23714 RVA: 0x00144D86 File Offset: 0x00142F86
		public DependencyAttribute(string dependentAssemblyArgument, LoadHint loadHintArgument)
		{
			this.dependentAssembly = dependentAssemblyArgument;
			this.loadHint = loadHintArgument;
		}

		// Token: 0x17001004 RID: 4100
		// (get) Token: 0x06005CA3 RID: 23715 RVA: 0x00144D9C File Offset: 0x00142F9C
		public string DependentAssembly
		{
			get
			{
				return this.dependentAssembly;
			}
		}

		// Token: 0x17001005 RID: 4101
		// (get) Token: 0x06005CA4 RID: 23716 RVA: 0x00144DA4 File Offset: 0x00142FA4
		public LoadHint LoadHint
		{
			get
			{
				return this.loadHint;
			}
		}

		// Token: 0x04002976 RID: 10614
		private string dependentAssembly;

		// Token: 0x04002977 RID: 10615
		private LoadHint loadHint;
	}
}
