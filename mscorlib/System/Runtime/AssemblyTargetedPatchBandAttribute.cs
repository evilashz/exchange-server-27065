using System;

namespace System.Runtime
{
	// Token: 0x020006EB RID: 1771
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyTargetedPatchBandAttribute : Attribute
	{
		// Token: 0x06005010 RID: 20496 RVA: 0x00119BDF File Offset: 0x00117DDF
		public AssemblyTargetedPatchBandAttribute(string targetedPatchBand)
		{
			this.m_targetedPatchBand = targetedPatchBand;
		}

		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x06005011 RID: 20497 RVA: 0x00119BEE File Offset: 0x00117DEE
		public string TargetedPatchBand
		{
			get
			{
				return this.m_targetedPatchBand;
			}
		}

		// Token: 0x04002340 RID: 9024
		private string m_targetedPatchBand;
	}
}
