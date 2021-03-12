using System;

namespace System.Reflection
{
	// Token: 0x020005F0 RID: 1520
	[__DynamicallyInvokable]
	public abstract class ReflectionContext
	{
		// Token: 0x0600477E RID: 18302 RVA: 0x00102ACA File Offset: 0x00100CCA
		[__DynamicallyInvokable]
		protected ReflectionContext()
		{
		}

		// Token: 0x0600477F RID: 18303
		[__DynamicallyInvokable]
		public abstract Assembly MapAssembly(Assembly assembly);

		// Token: 0x06004780 RID: 18304
		[__DynamicallyInvokable]
		public abstract TypeInfo MapType(TypeInfo type);

		// Token: 0x06004781 RID: 18305 RVA: 0x00102AD2 File Offset: 0x00100CD2
		[__DynamicallyInvokable]
		public virtual TypeInfo GetTypeForObject(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return this.MapType(value.GetType().GetTypeInfo());
		}
	}
}
