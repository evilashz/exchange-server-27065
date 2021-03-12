using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008D0 RID: 2256
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class IUnknownConstantAttribute : CustomConstantAttribute
	{
		// Token: 0x1700101A RID: 4122
		// (get) Token: 0x06005D37 RID: 23863 RVA: 0x00146A0B File Offset: 0x00144C0B
		public override object Value
		{
			get
			{
				return new UnknownWrapper(null);
			}
		}
	}
}
