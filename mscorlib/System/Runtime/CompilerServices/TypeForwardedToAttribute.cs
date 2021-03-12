using System;
using System.Reflection;
using System.Security;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008B3 RID: 2227
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
	[__DynamicallyInvokable]
	public sealed class TypeForwardedToAttribute : Attribute
	{
		// Token: 0x06005CBB RID: 23739 RVA: 0x00144E77 File Offset: 0x00143077
		[__DynamicallyInvokable]
		public TypeForwardedToAttribute(Type destination)
		{
			this._destination = destination;
		}

		// Token: 0x17001006 RID: 4102
		// (get) Token: 0x06005CBC RID: 23740 RVA: 0x00144E86 File Offset: 0x00143086
		[__DynamicallyInvokable]
		public Type Destination
		{
			[__DynamicallyInvokable]
			get
			{
				return this._destination;
			}
		}

		// Token: 0x06005CBD RID: 23741 RVA: 0x00144E90 File Offset: 0x00143090
		[SecurityCritical]
		internal static TypeForwardedToAttribute[] GetCustomAttribute(RuntimeAssembly assembly)
		{
			Type[] array = null;
			RuntimeAssembly.GetForwardedTypes(assembly.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<Type[]>(ref array));
			TypeForwardedToAttribute[] array2 = new TypeForwardedToAttribute[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = new TypeForwardedToAttribute(array[i]);
			}
			return array2;
		}

		// Token: 0x0400297E RID: 10622
		private Type _destination;
	}
}
