using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020008F3 RID: 2291
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, Inherited = false)]
	[Obsolete("This attribute is deprecated and will be removed in a future version.", false)]
	[ComVisible(true)]
	public sealed class IDispatchImplAttribute : Attribute
	{
		// Token: 0x06005EEE RID: 24302 RVA: 0x00146C25 File Offset: 0x00144E25
		public IDispatchImplAttribute(IDispatchImplType implType)
		{
			this._val = implType;
		}

		// Token: 0x06005EEF RID: 24303 RVA: 0x00146C34 File Offset: 0x00144E34
		public IDispatchImplAttribute(short implType)
		{
			this._val = (IDispatchImplType)implType;
		}

		// Token: 0x170010C3 RID: 4291
		// (get) Token: 0x06005EF0 RID: 24304 RVA: 0x00146C43 File Offset: 0x00144E43
		public IDispatchImplType Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x040029BF RID: 10687
		internal IDispatchImplType _val;
	}
}
