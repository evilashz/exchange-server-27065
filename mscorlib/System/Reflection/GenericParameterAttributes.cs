using System;

namespace System.Reflection
{
	// Token: 0x020005BC RID: 1468
	[Flags]
	[__DynamicallyInvokable]
	public enum GenericParameterAttributes
	{
		// Token: 0x04001C0B RID: 7179
		[__DynamicallyInvokable]
		None = 0,
		// Token: 0x04001C0C RID: 7180
		[__DynamicallyInvokable]
		VarianceMask = 3,
		// Token: 0x04001C0D RID: 7181
		[__DynamicallyInvokable]
		Covariant = 1,
		// Token: 0x04001C0E RID: 7182
		[__DynamicallyInvokable]
		Contravariant = 2,
		// Token: 0x04001C0F RID: 7183
		[__DynamicallyInvokable]
		SpecialConstraintMask = 28,
		// Token: 0x04001C10 RID: 7184
		[__DynamicallyInvokable]
		ReferenceTypeConstraint = 4,
		// Token: 0x04001C11 RID: 7185
		[__DynamicallyInvokable]
		NotNullableValueTypeConstraint = 8,
		// Token: 0x04001C12 RID: 7186
		[__DynamicallyInvokable]
		DefaultConstructorConstraint = 16
	}
}
