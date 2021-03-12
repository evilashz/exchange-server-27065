using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020000A1 RID: 161
	[AttributeUsage(AttributeTargets.Method)]
	[ComVisible(true)]
	public sealed class LoaderOptimizationAttribute : Attribute
	{
		// Token: 0x06000960 RID: 2400 RVA: 0x0001E8CA File Offset: 0x0001CACA
		public LoaderOptimizationAttribute(byte value)
		{
			this._val = value;
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0001E8D9 File Offset: 0x0001CAD9
		public LoaderOptimizationAttribute(LoaderOptimization value)
		{
			this._val = (byte)value;
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x0001E8E9 File Offset: 0x0001CAE9
		public LoaderOptimization Value
		{
			get
			{
				return (LoaderOptimization)this._val;
			}
		}

		// Token: 0x040003BE RID: 958
		internal byte _val;
	}
}
