using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser
{
	// Token: 0x0200019C RID: 412
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct IteratorCacher<TDelegate> where TDelegate : class
	{
		// Token: 0x06000817 RID: 2071 RVA: 0x0001BE07 File Offset: 0x0001A007
		internal IteratorCacher(TDelegate sample, Func<TDelegate, IEnumerator<FastTransferStateMachine?>> factory)
		{
			this.iteratorInstance = factory(sample);
			IteratorCacher<TDelegate>.EnsureInitializerDelegate(((Delegate)((object)sample)).GetMethodInfo(), this.iteratorInstance.GetType());
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0001BE36 File Offset: 0x0001A036
		internal object GetInstance()
		{
			Util.DisposeIfPresent(this.iteratorInstance as IDisposable);
			return this.iteratorInstance;
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0001BE4E File Offset: 0x0001A04E
		internal TDelegate GetInitializer()
		{
			return IteratorCacher<TDelegate>.delegateInstance;
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0001BE58 File Offset: 0x0001A058
		private static void EnsureInitializerDelegate(MethodInfo method, Type displayClassType)
		{
			if (IteratorCacher<TDelegate>.delegateInstance != null)
			{
				return;
			}
			ConstructorInfo[] array = displayClassType.GetTypeInfo().DeclaredConstructors.ToArray<ConstructorInfo>();
			ParameterInfo[] parameters = method.GetParameters();
			List<Type> list = new List<Type>();
			foreach (ParameterInfo parameterInfo in parameters)
			{
				list.Add(parameterInfo.ParameterType);
			}
			DynamicMethod dynamicMethod = new DynamicMethod(displayClassType.Name, typeof(IEnumerator<FastTransferStateMachine?>), list.ToArray(), typeof(FastTransferStateMachineFactory).GetTypeInfo().Module, true);
			ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
			ilgenerator.DeclareLocal(displayClassType);
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Castclass, displayClassType);
			ilgenerator.Emit(OpCodes.Stloc_0);
			ilgenerator.Emit(OpCodes.Ldloc_0);
			ilgenerator.Emit(OpCodes.Ldc_I4_0);
			ilgenerator.Emit(OpCodes.Call, array[0]);
			for (int j = 1; j < parameters.Length; j++)
			{
				FieldInfo declaredField = displayClassType.GetTypeInfo().GetDeclaredField(parameters[j].Name);
				if (declaredField == null)
				{
					throw new ArgumentException(string.Format("Cannot find field for parameter: {0}", parameters[j].Name));
				}
				ilgenerator.Emit(OpCodes.Ldloc_0);
				if (j < IteratorCacher<TDelegate>.Ldarg_N.Length)
				{
					ilgenerator.Emit(IteratorCacher<TDelegate>.Ldarg_N[j]);
				}
				else
				{
					ilgenerator.Emit(OpCodes.Ldarg_S, (byte)j);
				}
				ilgenerator.Emit(OpCodes.Stfld, declaredField);
			}
			ilgenerator.Emit(OpCodes.Ldloc_0);
			ilgenerator.Emit(OpCodes.Ret);
			IteratorCacher<TDelegate>.delegateInstance = (TDelegate)((object)dynamicMethod.CreateDelegate(typeof(TDelegate)));
		}

		// Token: 0x040003DE RID: 990
		private static readonly OpCode[] Ldarg_N = new OpCode[]
		{
			OpCodes.Ldarg_0,
			OpCodes.Ldarg_1,
			OpCodes.Ldarg_2,
			OpCodes.Ldarg_3
		};

		// Token: 0x040003DF RID: 991
		private static TDelegate delegateInstance;

		// Token: 0x040003E0 RID: 992
		private readonly object iteratorInstance;
	}
}
