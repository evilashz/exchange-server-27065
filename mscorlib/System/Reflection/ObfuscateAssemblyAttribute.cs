using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005E2 RID: 1506
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	[ComVisible(true)]
	public sealed class ObfuscateAssemblyAttribute : Attribute
	{
		// Token: 0x060046CF RID: 18127 RVA: 0x0010135C File Offset: 0x000FF55C
		public ObfuscateAssemblyAttribute(bool assemblyIsPrivate)
		{
			this.m_assemblyIsPrivate = assemblyIsPrivate;
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x060046D0 RID: 18128 RVA: 0x00101372 File Offset: 0x000FF572
		public bool AssemblyIsPrivate
		{
			get
			{
				return this.m_assemblyIsPrivate;
			}
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x060046D1 RID: 18129 RVA: 0x0010137A File Offset: 0x000FF57A
		// (set) Token: 0x060046D2 RID: 18130 RVA: 0x00101382 File Offset: 0x000FF582
		public bool StripAfterObfuscation
		{
			get
			{
				return this.m_strip;
			}
			set
			{
				this.m_strip = value;
			}
		}

		// Token: 0x04001D08 RID: 7432
		private bool m_assemblyIsPrivate;

		// Token: 0x04001D09 RID: 7433
		private bool m_strip = true;
	}
}
