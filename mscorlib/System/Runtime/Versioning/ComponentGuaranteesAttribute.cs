using System;

namespace System.Runtime.Versioning
{
	// Token: 0x020006F1 RID: 1777
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	public sealed class ComponentGuaranteesAttribute : Attribute
	{
		// Token: 0x06005035 RID: 20533 RVA: 0x0011A2A0 File Offset: 0x001184A0
		public ComponentGuaranteesAttribute(ComponentGuaranteesOptions guarantees)
		{
			this._guarantees = guarantees;
		}

		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x06005036 RID: 20534 RVA: 0x0011A2AF File Offset: 0x001184AF
		public ComponentGuaranteesOptions Guarantees
		{
			get
			{
				return this._guarantees;
			}
		}

		// Token: 0x04002353 RID: 9043
		private ComponentGuaranteesOptions _guarantees;
	}
}
