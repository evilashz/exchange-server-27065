using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000080 RID: 128
	internal class ClassFactory
	{
		// Token: 0x06000454 RID: 1108 RVA: 0x0000FDF8 File Offset: 0x0000DFF8
		private ClassFactory()
		{
			AssemblyName name = new AssemblyName("DynamicClasses");
			AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.Run);
			this.module = assemblyBuilder.DefineDynamicModule("Module");
			this.classes = new Dictionary<Signature, Type>();
			this.rwLock = new ReaderWriterLock();
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000FE4C File Offset: 0x0000E04C
		public Type GetDynamicClass(IEnumerable<DynamicProperty> properties)
		{
			this.rwLock.AcquireReaderLock(-1);
			Type result;
			try
			{
				Signature signature = new Signature(properties);
				Type type;
				if (!this.classes.TryGetValue(signature, out type))
				{
					type = this.CreateDynamicClass(signature.properties);
					this.classes.Add(signature, type);
				}
				result = type;
			}
			finally
			{
				this.rwLock.ReleaseReaderLock();
			}
			return result;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000FEB8 File Offset: 0x0000E0B8
		private Type CreateDynamicClass(DynamicProperty[] properties)
		{
			LockCookie lockCookie = this.rwLock.UpgradeToWriterLock(-1);
			Type result;
			try
			{
				string name = "DynamicClass" + (this.classCount + 1);
				TypeBuilder typeBuilder = this.module.DefineType(name, TypeAttributes.Public, typeof(DynamicClass));
				FieldInfo[] fields = this.GenerateProperties(typeBuilder, properties);
				this.GenerateEquals(typeBuilder, fields);
				this.GenerateGetHashCode(typeBuilder, fields);
				Type type = typeBuilder.CreateType();
				this.classCount++;
				result = type;
			}
			finally
			{
				this.rwLock.DowngradeFromWriterLock(ref lockCookie);
			}
			return result;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000FF58 File Offset: 0x0000E158
		private FieldInfo[] GenerateProperties(TypeBuilder tb, DynamicProperty[] properties)
		{
			FieldInfo[] array = new FieldBuilder[properties.Length];
			for (int i = 0; i < properties.Length; i++)
			{
				DynamicProperty dynamicProperty = properties[i];
				FieldBuilder fieldBuilder = tb.DefineField("_" + dynamicProperty.Name, dynamicProperty.Type, FieldAttributes.Private);
				PropertyBuilder propertyBuilder = tb.DefineProperty(dynamicProperty.Name, PropertyAttributes.HasDefault, dynamicProperty.Type, null);
				MethodBuilder methodBuilder = tb.DefineMethod("get_" + dynamicProperty.Name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.HideBySig | MethodAttributes.SpecialName, dynamicProperty.Type, Type.EmptyTypes);
				ILGenerator ilgenerator = methodBuilder.GetILGenerator();
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Ldfld, fieldBuilder);
				ilgenerator.Emit(OpCodes.Ret);
				MethodBuilder methodBuilder2 = tb.DefineMethod("set_" + dynamicProperty.Name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.HideBySig | MethodAttributes.SpecialName, null, new Type[]
				{
					dynamicProperty.Type
				});
				ILGenerator ilgenerator2 = methodBuilder2.GetILGenerator();
				ilgenerator2.Emit(OpCodes.Ldarg_0);
				ilgenerator2.Emit(OpCodes.Ldarg_1);
				ilgenerator2.Emit(OpCodes.Stfld, fieldBuilder);
				ilgenerator2.Emit(OpCodes.Ret);
				propertyBuilder.SetGetMethod(methodBuilder);
				propertyBuilder.SetSetMethod(methodBuilder2);
				array[i] = fieldBuilder;
			}
			return array;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00010098 File Offset: 0x0000E298
		private void GenerateEquals(TypeBuilder tb, FieldInfo[] fields)
		{
			MethodBuilder methodBuilder = tb.DefineMethod("Equals", MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig, typeof(bool), new Type[]
			{
				typeof(object)
			});
			ILGenerator ilgenerator = methodBuilder.GetILGenerator();
			LocalBuilder local = ilgenerator.DeclareLocal(tb);
			Label label = ilgenerator.DefineLabel();
			ilgenerator.Emit(OpCodes.Ldarg_1);
			ilgenerator.Emit(OpCodes.Isinst, tb);
			ilgenerator.Emit(OpCodes.Stloc, local);
			ilgenerator.Emit(OpCodes.Ldloc, local);
			ilgenerator.Emit(OpCodes.Brtrue_S, label);
			ilgenerator.Emit(OpCodes.Ldc_I4_0);
			ilgenerator.Emit(OpCodes.Ret);
			ilgenerator.MarkLabel(label);
			foreach (FieldInfo fieldInfo in fields)
			{
				Type fieldType = fieldInfo.FieldType;
				Type type = typeof(EqualityComparer<>).MakeGenericType(new Type[]
				{
					fieldType
				});
				label = ilgenerator.DefineLabel();
				ilgenerator.EmitCall(OpCodes.Call, type.GetMethod("get_Default"), null);
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Ldfld, fieldInfo);
				ilgenerator.Emit(OpCodes.Ldloc, local);
				ilgenerator.Emit(OpCodes.Ldfld, fieldInfo);
				ilgenerator.EmitCall(OpCodes.Callvirt, type.GetMethod("Equals", new Type[]
				{
					fieldType,
					fieldType
				}), null);
				ilgenerator.Emit(OpCodes.Brtrue_S, label);
				ilgenerator.Emit(OpCodes.Ldc_I4_0);
				ilgenerator.Emit(OpCodes.Ret);
				ilgenerator.MarkLabel(label);
			}
			ilgenerator.Emit(OpCodes.Ldc_I4_1);
			ilgenerator.Emit(OpCodes.Ret);
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00010250 File Offset: 0x0000E450
		private void GenerateGetHashCode(TypeBuilder tb, FieldInfo[] fields)
		{
			MethodBuilder methodBuilder = tb.DefineMethod("GetHashCode", MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig, typeof(int), Type.EmptyTypes);
			ILGenerator ilgenerator = methodBuilder.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldc_I4_0);
			foreach (FieldInfo fieldInfo in fields)
			{
				Type fieldType = fieldInfo.FieldType;
				Type type = typeof(EqualityComparer<>).MakeGenericType(new Type[]
				{
					fieldType
				});
				ilgenerator.EmitCall(OpCodes.Call, type.GetMethod("get_Default"), null);
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Ldfld, fieldInfo);
				ilgenerator.EmitCall(OpCodes.Callvirt, type.GetMethod("GetHashCode", new Type[]
				{
					fieldType
				}), null);
				ilgenerator.Emit(OpCodes.Xor);
			}
			ilgenerator.Emit(OpCodes.Ret);
		}

		// Token: 0x0400011D RID: 285
		public static readonly ClassFactory Instance = new ClassFactory();

		// Token: 0x0400011E RID: 286
		private ModuleBuilder module;

		// Token: 0x0400011F RID: 287
		private Dictionary<Signature, Type> classes;

		// Token: 0x04000120 RID: 288
		private int classCount;

		// Token: 0x04000121 RID: 289
		private ReaderWriterLock rwLock;
	}
}
