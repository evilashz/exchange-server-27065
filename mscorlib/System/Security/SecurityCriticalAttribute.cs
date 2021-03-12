using System;

namespace System.Security
{
	// Token: 0x020001C7 RID: 455
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	[__DynamicallyInvokable]
	public sealed class SecurityCriticalAttribute : Attribute
	{
		// Token: 0x06001C20 RID: 7200 RVA: 0x00060DD6 File Offset: 0x0005EFD6
		[__DynamicallyInvokable]
		public SecurityCriticalAttribute()
		{
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x00060DDE File Offset: 0x0005EFDE
		public SecurityCriticalAttribute(SecurityCriticalScope scope)
		{
			this._val = scope;
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06001C22 RID: 7202 RVA: 0x00060DED File Offset: 0x0005EFED
		[Obsolete("SecurityCriticalScope is only used for .NET 2.0 transparency compatibility.")]
		public SecurityCriticalScope Scope
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x040009C5 RID: 2501
		private SecurityCriticalScope _val;
	}
}
