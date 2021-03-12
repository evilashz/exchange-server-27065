using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008CF RID: 2255
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class IDispatchConstantAttribute : CustomConstantAttribute
	{
		// Token: 0x17001019 RID: 4121
		// (get) Token: 0x06005D35 RID: 23861 RVA: 0x001469FB File Offset: 0x00144BFB
		public override object Value
		{
			get
			{
				return new DispatchWrapper(null);
			}
		}
	}
}
