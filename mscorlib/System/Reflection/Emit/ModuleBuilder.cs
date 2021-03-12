using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
	// Token: 0x02000624 RID: 1572
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_ModuleBuilder))]
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class ModuleBuilder : Module, _ModuleBuilder
	{
		// Token: 0x06004AEC RID: 19180
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr nCreateISymWriterForDynamicModule(Module module, string filename);

		// Token: 0x06004AED RID: 19181 RVA: 0x0010E7C4 File Offset: 0x0010C9C4
		internal static string UnmangleTypeName(string typeName)
		{
			int num = typeName.Length - 1;
			for (;;)
			{
				num = typeName.LastIndexOf('+', num);
				if (num == -1)
				{
					break;
				}
				bool flag = true;
				int num2 = num;
				while (typeName[--num2] == '\\')
				{
					flag = !flag;
				}
				if (flag)
				{
					break;
				}
				num = num2;
			}
			return typeName.Substring(num + 1);
		}

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06004AEE RID: 19182 RVA: 0x0010E812 File Offset: 0x0010CA12
		internal AssemblyBuilder ContainingAssemblyBuilder
		{
			get
			{
				return this.m_assemblyBuilder;
			}
		}

		// Token: 0x06004AEF RID: 19183 RVA: 0x0010E81A File Offset: 0x0010CA1A
		internal ModuleBuilder(AssemblyBuilder assemblyBuilder, InternalModuleBuilder internalModuleBuilder)
		{
			this.m_internalModuleBuilder = internalModuleBuilder;
			this.m_assemblyBuilder = assemblyBuilder;
		}

		// Token: 0x06004AF0 RID: 19184 RVA: 0x0010E830 File Offset: 0x0010CA30
		internal void AddType(string name, Type type)
		{
			this.m_TypeBuilderDict.Add(name, type);
		}

		// Token: 0x06004AF1 RID: 19185 RVA: 0x0010E840 File Offset: 0x0010CA40
		internal void CheckTypeNameConflict(string strTypeName, Type enclosingType)
		{
			Type type = null;
			if (this.m_TypeBuilderDict.TryGetValue(strTypeName, out type) && type.DeclaringType == enclosingType)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_DuplicateTypeName"));
			}
		}

		// Token: 0x06004AF2 RID: 19186 RVA: 0x0010E878 File Offset: 0x0010CA78
		private Type GetType(string strFormat, Type baseType)
		{
			if (strFormat == null || strFormat.Equals(string.Empty))
			{
				return baseType;
			}
			char[] bFormat = strFormat.ToCharArray();
			return SymbolType.FormCompoundType(bFormat, baseType, 0);
		}

		// Token: 0x06004AF3 RID: 19187 RVA: 0x0010E8A6 File Offset: 0x0010CAA6
		internal void CheckContext(params Type[][] typess)
		{
			this.ContainingAssemblyBuilder.CheckContext(typess);
		}

		// Token: 0x06004AF4 RID: 19188 RVA: 0x0010E8B4 File Offset: 0x0010CAB4
		internal void CheckContext(params Type[] types)
		{
			this.ContainingAssemblyBuilder.CheckContext(types);
		}

		// Token: 0x06004AF5 RID: 19189
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int GetTypeRef(RuntimeModule module, string strFullName, RuntimeModule refedModule, string strRefedModuleFileName, int tkResolution);

		// Token: 0x06004AF6 RID: 19190
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int GetMemberRef(RuntimeModule module, RuntimeModule refedModule, int tr, int defToken);

		// Token: 0x06004AF7 RID: 19191 RVA: 0x0010E8C2 File Offset: 0x0010CAC2
		[SecurityCritical]
		private int GetMemberRef(Module refedModule, int tr, int defToken)
		{
			return ModuleBuilder.GetMemberRef(this.GetNativeHandle(), ModuleBuilder.GetRuntimeModuleFromModule(refedModule).GetNativeHandle(), tr, defToken);
		}

		// Token: 0x06004AF8 RID: 19192
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int GetMemberRefFromSignature(RuntimeModule module, int tr, string methodName, byte[] signature, int length);

		// Token: 0x06004AF9 RID: 19193 RVA: 0x0010E8DC File Offset: 0x0010CADC
		[SecurityCritical]
		private int GetMemberRefFromSignature(int tr, string methodName, byte[] signature, int length)
		{
			return ModuleBuilder.GetMemberRefFromSignature(this.GetNativeHandle(), tr, methodName, signature, length);
		}

		// Token: 0x06004AFA RID: 19194
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int GetMemberRefOfMethodInfo(RuntimeModule module, int tr, IRuntimeMethodInfo method);

		// Token: 0x06004AFB RID: 19195 RVA: 0x0010E8F0 File Offset: 0x0010CAF0
		[SecurityCritical]
		private int GetMemberRefOfMethodInfo(int tr, RuntimeMethodInfo method)
		{
			if (this.ContainingAssemblyBuilder.ProfileAPICheck && (method.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", new object[]
				{
					method.FullName
				}));
			}
			return ModuleBuilder.GetMemberRefOfMethodInfo(this.GetNativeHandle(), tr, method);
		}

		// Token: 0x06004AFC RID: 19196 RVA: 0x0010E944 File Offset: 0x0010CB44
		[SecurityCritical]
		private int GetMemberRefOfMethodInfo(int tr, RuntimeConstructorInfo method)
		{
			if (this.ContainingAssemblyBuilder.ProfileAPICheck && (method.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", new object[]
				{
					method.FullName
				}));
			}
			return ModuleBuilder.GetMemberRefOfMethodInfo(this.GetNativeHandle(), tr, method);
		}

		// Token: 0x06004AFD RID: 19197
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int GetMemberRefOfFieldInfo(RuntimeModule module, int tkType, RuntimeTypeHandle declaringType, int tkField);

		// Token: 0x06004AFE RID: 19198 RVA: 0x0010E998 File Offset: 0x0010CB98
		[SecurityCritical]
		private int GetMemberRefOfFieldInfo(int tkType, RuntimeTypeHandle declaringType, RuntimeFieldInfo runtimeField)
		{
			if (this.ContainingAssemblyBuilder.ProfileAPICheck)
			{
				RtFieldInfo rtFieldInfo = runtimeField as RtFieldInfo;
				if (rtFieldInfo != null && (rtFieldInfo.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", new object[]
					{
						rtFieldInfo.FullName
					}));
				}
			}
			return ModuleBuilder.GetMemberRefOfFieldInfo(this.GetNativeHandle(), tkType, declaringType, runtimeField.MetadataToken);
		}

		// Token: 0x06004AFF RID: 19199
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int GetTokenFromTypeSpec(RuntimeModule pModule, byte[] signature, int length);

		// Token: 0x06004B00 RID: 19200 RVA: 0x0010E9FF File Offset: 0x0010CBFF
		[SecurityCritical]
		private int GetTokenFromTypeSpec(byte[] signature, int length)
		{
			return ModuleBuilder.GetTokenFromTypeSpec(this.GetNativeHandle(), signature, length);
		}

		// Token: 0x06004B01 RID: 19201
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int GetArrayMethodToken(RuntimeModule module, int tkTypeSpec, string methodName, byte[] signature, int sigLength);

		// Token: 0x06004B02 RID: 19202
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int GetStringConstant(RuntimeModule module, string str, int length);

		// Token: 0x06004B03 RID: 19203
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void PreSavePEFile(RuntimeModule module, int portableExecutableKind, int imageFileMachine);

		// Token: 0x06004B04 RID: 19204
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void SavePEFile(RuntimeModule module, string fileName, int entryPoint, int isExe, bool isManifestFile);

		// Token: 0x06004B05 RID: 19205
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void AddResource(RuntimeModule module, string strName, byte[] resBytes, int resByteCount, int tkFile, int attribute, int portableExecutableKind, int imageFileMachine);

		// Token: 0x06004B06 RID: 19206
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void SetModuleName(RuntimeModule module, string strModuleName);

		// Token: 0x06004B07 RID: 19207
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void SetFieldRVAContent(RuntimeModule module, int fdToken, byte[] data, int length);

		// Token: 0x06004B08 RID: 19208
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void DefineNativeResourceFile(RuntimeModule module, string strFilename, int portableExecutableKind, int ImageFileMachine);

		// Token: 0x06004B09 RID: 19209
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void DefineNativeResourceBytes(RuntimeModule module, byte[] pbResource, int cbResource, int portableExecutableKind, int imageFileMachine);

		// Token: 0x06004B0A RID: 19210 RVA: 0x0010EA10 File Offset: 0x0010CC10
		[SecurityCritical]
		internal void DefineNativeResource(PortableExecutableKinds portableExecutableKind, ImageFileMachine imageFileMachine)
		{
			string strResourceFileName = this.m_moduleData.m_strResourceFileName;
			byte[] resourceBytes = this.m_moduleData.m_resourceBytes;
			if (strResourceFileName != null)
			{
				ModuleBuilder.DefineNativeResourceFile(this.GetNativeHandle(), strResourceFileName, (int)portableExecutableKind, (int)imageFileMachine);
				return;
			}
			if (resourceBytes != null)
			{
				ModuleBuilder.DefineNativeResourceBytes(this.GetNativeHandle(), resourceBytes, resourceBytes.Length, (int)portableExecutableKind, (int)imageFileMachine);
			}
		}

		// Token: 0x06004B0B RID: 19211 RVA: 0x0010EA5C File Offset: 0x0010CC5C
		internal virtual Type FindTypeBuilderWithName(string strTypeName, bool ignoreCase)
		{
			if (ignoreCase)
			{
				using (Dictionary<string, Type>.KeyCollection.Enumerator enumerator = this.m_TypeBuilderDict.Keys.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						if (string.Compare(text, strTypeName, StringComparison.OrdinalIgnoreCase) == 0)
						{
							return this.m_TypeBuilderDict[text];
						}
					}
					goto IL_62;
				}
			}
			Type result;
			if (this.m_TypeBuilderDict.TryGetValue(strTypeName, out result))
			{
				return result;
			}
			IL_62:
			return null;
		}

		// Token: 0x06004B0C RID: 19212 RVA: 0x0010EAE0 File Offset: 0x0010CCE0
		internal void SetEntryPoint(MethodToken entryPoint)
		{
			this.m_EntryPoint = entryPoint;
		}

		// Token: 0x06004B0D RID: 19213 RVA: 0x0010EAEC File Offset: 0x0010CCEC
		[SecurityCritical]
		internal void PreSave(string fileName, PortableExecutableKinds portableExecutableKind, ImageFileMachine imageFileMachine)
		{
			if (this.m_moduleData.m_isSaved)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("InvalidOperation_ModuleHasBeenSaved"), this.m_moduleData.m_strModuleName));
			}
			if (!this.m_moduleData.m_fGlobalBeenCreated && this.m_moduleData.m_fHasGlobal)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_GlobalFunctionNotBaked"));
			}
			foreach (Type type in this.m_TypeBuilderDict.Values)
			{
				TypeBuilder typeBuilder;
				if (type is TypeBuilder)
				{
					typeBuilder = (TypeBuilder)type;
				}
				else
				{
					EnumBuilder enumBuilder = (EnumBuilder)type;
					typeBuilder = enumBuilder.m_typeBuilder;
				}
				if (!typeBuilder.IsCreated())
				{
					throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("NotSupported_NotAllTypesAreBaked"), typeBuilder.FullName));
				}
			}
			ModuleBuilder.PreSavePEFile(this.GetNativeHandle(), (int)portableExecutableKind, (int)imageFileMachine);
		}

		// Token: 0x06004B0E RID: 19214 RVA: 0x0010EBF0 File Offset: 0x0010CDF0
		[SecurityCritical]
		internal void Save(string fileName, bool isAssemblyFile, PortableExecutableKinds portableExecutableKind, ImageFileMachine imageFileMachine)
		{
			if (this.m_moduleData.m_embeddedRes != null)
			{
				for (ResWriterData resWriterData = this.m_moduleData.m_embeddedRes; resWriterData != null; resWriterData = resWriterData.m_nextResWriter)
				{
					if (resWriterData.m_resWriter != null)
					{
						resWriterData.m_resWriter.Generate();
					}
					byte[] array = new byte[resWriterData.m_memoryStream.Length];
					resWriterData.m_memoryStream.Flush();
					resWriterData.m_memoryStream.Position = 0L;
					resWriterData.m_memoryStream.Read(array, 0, array.Length);
					ModuleBuilder.AddResource(this.GetNativeHandle(), resWriterData.m_strName, array, array.Length, this.m_moduleData.FileToken, (int)resWriterData.m_attribute, (int)portableExecutableKind, (int)imageFileMachine);
				}
			}
			this.DefineNativeResource(portableExecutableKind, imageFileMachine);
			PEFileKinds isExe = isAssemblyFile ? this.ContainingAssemblyBuilder.m_assemblyData.m_peFileKind : PEFileKinds.Dll;
			ModuleBuilder.SavePEFile(this.GetNativeHandle(), fileName, this.m_EntryPoint.Token, (int)isExe, isAssemblyFile);
			this.m_moduleData.m_isSaved = true;
		}

		// Token: 0x06004B0F RID: 19215 RVA: 0x0010ECE4 File Offset: 0x0010CEE4
		[SecurityCritical]
		private int GetTypeRefNested(Type type, Module refedModule, string strRefedModuleFileName)
		{
			Type declaringType = type.DeclaringType;
			int tkResolution = 0;
			string text = type.FullName;
			if (declaringType != null)
			{
				tkResolution = this.GetTypeRefNested(declaringType, refedModule, strRefedModuleFileName);
				text = ModuleBuilder.UnmangleTypeName(text);
			}
			if (this.ContainingAssemblyBuilder.ProfileAPICheck)
			{
				RuntimeType runtimeType = type as RuntimeType;
				if (runtimeType != null && (runtimeType.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", new object[]
					{
						runtimeType.FullName
					}));
				}
			}
			return ModuleBuilder.GetTypeRef(this.GetNativeHandle(), text, ModuleBuilder.GetRuntimeModuleFromModule(refedModule).GetNativeHandle(), strRefedModuleFileName, tkResolution);
		}

		// Token: 0x06004B10 RID: 19216 RVA: 0x0010ED7C File Offset: 0x0010CF7C
		[SecurityCritical]
		internal MethodToken InternalGetConstructorToken(ConstructorInfo con, bool usingRef)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			ConstructorBuilder constructorBuilder;
			int str;
			ConstructorOnTypeBuilderInstantiation constructorOnTypeBuilderInstantiation;
			RuntimeConstructorInfo method;
			if ((constructorBuilder = (con as ConstructorBuilder)) != null)
			{
				if (!usingRef && constructorBuilder.Module.Equals(this))
				{
					return constructorBuilder.GetToken();
				}
				int token = this.GetTypeTokenInternal(con.ReflectedType).Token;
				str = this.GetMemberRef(con.ReflectedType.Module, token, constructorBuilder.GetToken().Token);
			}
			else if ((constructorOnTypeBuilderInstantiation = (con as ConstructorOnTypeBuilderInstantiation)) != null)
			{
				if (usingRef)
				{
					throw new InvalidOperationException();
				}
				int token = this.GetTypeTokenInternal(con.DeclaringType).Token;
				str = this.GetMemberRef(con.DeclaringType.Module, token, constructorOnTypeBuilderInstantiation.MetadataTokenInternal);
			}
			else if ((method = (con as RuntimeConstructorInfo)) != null && !con.ReflectedType.IsArray)
			{
				int token = this.GetTypeTokenInternal(con.ReflectedType).Token;
				str = this.GetMemberRefOfMethodInfo(token, method);
			}
			else
			{
				ParameterInfo[] parameters = con.GetParameters();
				if (parameters == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidConstructorInfo"));
				}
				int num = parameters.Length;
				Type[] array = new Type[num];
				Type[][] array2 = new Type[num][];
				Type[][] array3 = new Type[num][];
				for (int i = 0; i < num; i++)
				{
					if (parameters[i] == null)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_InvalidConstructorInfo"));
					}
					array[i] = parameters[i].ParameterType;
					array2[i] = parameters[i].GetRequiredCustomModifiers();
					array3[i] = parameters[i].GetOptionalCustomModifiers();
				}
				int token = this.GetTypeTokenInternal(con.ReflectedType).Token;
				SignatureHelper methodSigHelper = SignatureHelper.GetMethodSigHelper(this, con.CallingConvention, null, null, null, array, array2, array3);
				int length;
				byte[] signature = methodSigHelper.InternalGetSignature(out length);
				str = this.GetMemberRefFromSignature(token, con.Name, signature, length);
			}
			return new MethodToken(str);
		}

		// Token: 0x06004B11 RID: 19217 RVA: 0x0010EF7D File Offset: 0x0010D17D
		[SecurityCritical]
		internal void Init(string strModuleName, string strFileName, int tkFile)
		{
			this.m_moduleData = new ModuleBuilderData(this, strModuleName, strFileName, tkFile);
			this.m_TypeBuilderDict = new Dictionary<string, Type>();
		}

		// Token: 0x06004B12 RID: 19218 RVA: 0x0010EF99 File Offset: 0x0010D199
		[SecurityCritical]
		internal void ModifyModuleName(string name)
		{
			this.m_moduleData.ModifyModuleName(name);
			ModuleBuilder.SetModuleName(this.GetNativeHandle(), name);
		}

		// Token: 0x06004B13 RID: 19219 RVA: 0x0010EFB3 File Offset: 0x0010D1B3
		internal void SetSymWriter(ISymbolWriter writer)
		{
			this.m_iSymWriter = writer;
		}

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x06004B14 RID: 19220 RVA: 0x0010EFBC File Offset: 0x0010D1BC
		internal object SyncRoot
		{
			get
			{
				return this.ContainingAssemblyBuilder.SyncRoot;
			}
		}

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x06004B15 RID: 19221 RVA: 0x0010EFC9 File Offset: 0x0010D1C9
		internal InternalModuleBuilder InternalModule
		{
			get
			{
				return this.m_internalModuleBuilder;
			}
		}

		// Token: 0x06004B16 RID: 19222 RVA: 0x0010EFD1 File Offset: 0x0010D1D1
		internal override ModuleHandle GetModuleHandle()
		{
			return new ModuleHandle(this.GetNativeHandle());
		}

		// Token: 0x06004B17 RID: 19223 RVA: 0x0010EFDE File Offset: 0x0010D1DE
		internal RuntimeModule GetNativeHandle()
		{
			return this.InternalModule.GetNativeHandle();
		}

		// Token: 0x06004B18 RID: 19224 RVA: 0x0010EFEC File Offset: 0x0010D1EC
		private static RuntimeModule GetRuntimeModuleFromModule(Module m)
		{
			ModuleBuilder moduleBuilder = m as ModuleBuilder;
			if (moduleBuilder != null)
			{
				return moduleBuilder.InternalModule;
			}
			return m as RuntimeModule;
		}

		// Token: 0x06004B19 RID: 19225 RVA: 0x0010F018 File Offset: 0x0010D218
		[SecurityCritical]
		private int GetMemberRefToken(MethodBase method, IEnumerable<Type> optionalParameterTypes)
		{
			int cGenericParameters = 0;
			if (method.IsGenericMethod)
			{
				if (!method.IsGenericMethodDefinition)
				{
					throw new InvalidOperationException();
				}
				cGenericParameters = method.GetGenericArguments().Length;
			}
			if (optionalParameterTypes != null && (method.CallingConvention & CallingConventions.VarArgs) == (CallingConventions)0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotAVarArgCallingConvention"));
			}
			MethodInfo methodInfo = method as MethodInfo;
			Type[] parameterTypes;
			Type methodBaseReturnType;
			if (method.DeclaringType.IsGenericType)
			{
				MethodOnTypeBuilderInstantiation methodOnTypeBuilderInstantiation;
				MethodBase methodBase;
				ConstructorOnTypeBuilderInstantiation constructorOnTypeBuilderInstantiation;
				if ((methodOnTypeBuilderInstantiation = (method as MethodOnTypeBuilderInstantiation)) != null)
				{
					methodBase = methodOnTypeBuilderInstantiation.m_method;
				}
				else if ((constructorOnTypeBuilderInstantiation = (method as ConstructorOnTypeBuilderInstantiation)) != null)
				{
					methodBase = constructorOnTypeBuilderInstantiation.m_ctor;
				}
				else if (method is MethodBuilder || method is ConstructorBuilder)
				{
					methodBase = method;
				}
				else if (method.IsGenericMethod)
				{
					methodBase = methodInfo.GetGenericMethodDefinition();
					methodBase = methodBase.Module.ResolveMethod(method.MetadataToken, (methodBase.DeclaringType != null) ? methodBase.DeclaringType.GetGenericArguments() : null, methodBase.GetGenericArguments());
				}
				else
				{
					methodBase = method.Module.ResolveMethod(method.MetadataToken, (method.DeclaringType != null) ? method.DeclaringType.GetGenericArguments() : null, null);
				}
				parameterTypes = methodBase.GetParameterTypes();
				methodBaseReturnType = MethodBuilder.GetMethodBaseReturnType(methodBase);
			}
			else
			{
				parameterTypes = method.GetParameterTypes();
				methodBaseReturnType = MethodBuilder.GetMethodBaseReturnType(method);
			}
			int length;
			byte[] signature = this.GetMemberRefSignature(method.CallingConvention, methodBaseReturnType, parameterTypes, optionalParameterTypes, cGenericParameters).InternalGetSignature(out length);
			int tr;
			if (method.DeclaringType.IsGenericType)
			{
				int length2;
				byte[] signature2 = SignatureHelper.GetTypeSigToken(this, method.DeclaringType).InternalGetSignature(out length2);
				tr = this.GetTokenFromTypeSpec(signature2, length2);
			}
			else if (!method.Module.Equals(this))
			{
				tr = this.GetTypeToken(method.DeclaringType).Token;
			}
			else if (methodInfo != null)
			{
				tr = this.GetMethodToken(methodInfo).Token;
			}
			else
			{
				tr = this.GetConstructorToken(method as ConstructorInfo).Token;
			}
			return this.GetMemberRefFromSignature(tr, method.Name, signature, length);
		}

		// Token: 0x06004B1A RID: 19226 RVA: 0x0010F220 File Offset: 0x0010D420
		[SecurityCritical]
		internal SignatureHelper GetMemberRefSignature(CallingConventions call, Type returnType, Type[] parameterTypes, IEnumerable<Type> optionalParameterTypes, int cGenericParameters)
		{
			int num = (parameterTypes == null) ? 0 : parameterTypes.Length;
			SignatureHelper methodSigHelper = SignatureHelper.GetMethodSigHelper(this, call, returnType, cGenericParameters);
			for (int i = 0; i < num; i++)
			{
				methodSigHelper.AddArgument(parameterTypes[i]);
			}
			if (optionalParameterTypes != null)
			{
				int num2 = 0;
				foreach (Type clsArgument in optionalParameterTypes)
				{
					if (num2 == 0)
					{
						methodSigHelper.AddSentinel();
					}
					methodSigHelper.AddArgument(clsArgument);
					num2++;
				}
			}
			return methodSigHelper;
		}

		// Token: 0x06004B1B RID: 19227 RVA: 0x0010F2B0 File Offset: 0x0010D4B0
		public override bool Equals(object obj)
		{
			return this.InternalModule.Equals(obj);
		}

		// Token: 0x06004B1C RID: 19228 RVA: 0x0010F2BE File Offset: 0x0010D4BE
		public override int GetHashCode()
		{
			return this.InternalModule.GetHashCode();
		}

		// Token: 0x06004B1D RID: 19229 RVA: 0x0010F2CB File Offset: 0x0010D4CB
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.InternalModule.GetCustomAttributes(inherit);
		}

		// Token: 0x06004B1E RID: 19230 RVA: 0x0010F2D9 File Offset: 0x0010D4D9
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.InternalModule.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06004B1F RID: 19231 RVA: 0x0010F2E8 File Offset: 0x0010D4E8
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.InternalModule.IsDefined(attributeType, inherit);
		}

		// Token: 0x06004B20 RID: 19232 RVA: 0x0010F2F7 File Offset: 0x0010D4F7
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return this.InternalModule.GetCustomAttributesData();
		}

		// Token: 0x06004B21 RID: 19233 RVA: 0x0010F304 File Offset: 0x0010D504
		public override Type[] GetTypes()
		{
			object syncRoot = this.SyncRoot;
			Type[] typesNoLock;
			lock (syncRoot)
			{
				typesNoLock = this.GetTypesNoLock();
			}
			return typesNoLock;
		}

		// Token: 0x06004B22 RID: 19234 RVA: 0x0010F348 File Offset: 0x0010D548
		internal Type[] GetTypesNoLock()
		{
			int count = this.m_TypeBuilderDict.Count;
			Type[] array = new Type[this.m_TypeBuilderDict.Count];
			int num = 0;
			foreach (Type type in this.m_TypeBuilderDict.Values)
			{
				EnumBuilder enumBuilder = type as EnumBuilder;
				TypeBuilder typeBuilder;
				if (enumBuilder != null)
				{
					typeBuilder = enumBuilder.m_typeBuilder;
				}
				else
				{
					typeBuilder = (TypeBuilder)type;
				}
				if (typeBuilder.IsCreated())
				{
					array[num++] = typeBuilder.UnderlyingSystemType;
				}
				else
				{
					array[num++] = type;
				}
			}
			return array;
		}

		// Token: 0x06004B23 RID: 19235 RVA: 0x0010F404 File Offset: 0x0010D604
		[ComVisible(true)]
		public override Type GetType(string className)
		{
			return this.GetType(className, false, false);
		}

		// Token: 0x06004B24 RID: 19236 RVA: 0x0010F40F File Offset: 0x0010D60F
		[ComVisible(true)]
		public override Type GetType(string className, bool ignoreCase)
		{
			return this.GetType(className, false, ignoreCase);
		}

		// Token: 0x06004B25 RID: 19237 RVA: 0x0010F41C File Offset: 0x0010D61C
		[ComVisible(true)]
		public override Type GetType(string className, bool throwOnError, bool ignoreCase)
		{
			object syncRoot = this.SyncRoot;
			Type typeNoLock;
			lock (syncRoot)
			{
				typeNoLock = this.GetTypeNoLock(className, throwOnError, ignoreCase);
			}
			return typeNoLock;
		}

		// Token: 0x06004B26 RID: 19238 RVA: 0x0010F464 File Offset: 0x0010D664
		private Type GetTypeNoLock(string className, bool throwOnError, bool ignoreCase)
		{
			Type type = this.InternalModule.GetType(className, throwOnError, ignoreCase);
			if (type != null)
			{
				return type;
			}
			string text = null;
			string text2 = null;
			int num;
			for (int i = 0; i <= className.Length; i = num + 1)
			{
				num = className.IndexOfAny(new char[]
				{
					'[',
					'*',
					'&'
				}, i);
				if (num == -1)
				{
					text = className;
					text2 = null;
					break;
				}
				int num2 = 0;
				int num3 = num - 1;
				while (num3 >= 0 && className[num3] == '\\')
				{
					num2++;
					num3--;
				}
				if (num2 % 2 != 1)
				{
					text = className.Substring(0, num);
					text2 = className.Substring(num);
					break;
				}
			}
			if (text == null)
			{
				text = className;
				text2 = null;
			}
			text = text.Replace("\\\\", "\\").Replace("\\[", "[").Replace("\\*", "*").Replace("\\&", "&");
			if (text2 != null)
			{
				type = this.InternalModule.GetType(text, false, ignoreCase);
			}
			if (type == null)
			{
				type = this.FindTypeBuilderWithName(text, ignoreCase);
				if (type == null && this.Assembly is AssemblyBuilder)
				{
					List<ModuleBuilder> moduleBuilderList = this.ContainingAssemblyBuilder.m_assemblyData.m_moduleBuilderList;
					int count = moduleBuilderList.Count;
					int num4 = 0;
					while (num4 < count && type == null)
					{
						ModuleBuilder moduleBuilder = moduleBuilderList[num4];
						type = moduleBuilder.FindTypeBuilderWithName(text, ignoreCase);
						num4++;
					}
				}
				if (type == null)
				{
					return null;
				}
			}
			if (text2 == null)
			{
				return type;
			}
			return this.GetType(text2, type);
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x06004B27 RID: 19239 RVA: 0x0010F5F0 File Offset: 0x0010D7F0
		public override string FullyQualifiedName
		{
			[SecuritySafeCritical]
			get
			{
				string text = this.m_moduleData.m_strFileName;
				if (text == null)
				{
					return null;
				}
				if (this.ContainingAssemblyBuilder.m_assemblyData.m_strDir != null)
				{
					text = Path.Combine(this.ContainingAssemblyBuilder.m_assemblyData.m_strDir, text);
					text = Path.UnsafeGetFullPath(text);
				}
				if (this.ContainingAssemblyBuilder.m_assemblyData.m_strDir != null && text != null)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, text).Demand();
				}
				return text;
			}
		}

		// Token: 0x06004B28 RID: 19240 RVA: 0x0010F660 File Offset: 0x0010D860
		public override byte[] ResolveSignature(int metadataToken)
		{
			return this.InternalModule.ResolveSignature(metadataToken);
		}

		// Token: 0x06004B29 RID: 19241 RVA: 0x0010F66E File Offset: 0x0010D86E
		public override MethodBase ResolveMethod(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			return this.InternalModule.ResolveMethod(metadataToken, genericTypeArguments, genericMethodArguments);
		}

		// Token: 0x06004B2A RID: 19242 RVA: 0x0010F67E File Offset: 0x0010D87E
		public override FieldInfo ResolveField(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			return this.InternalModule.ResolveField(metadataToken, genericTypeArguments, genericMethodArguments);
		}

		// Token: 0x06004B2B RID: 19243 RVA: 0x0010F68E File Offset: 0x0010D88E
		public override Type ResolveType(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			return this.InternalModule.ResolveType(metadataToken, genericTypeArguments, genericMethodArguments);
		}

		// Token: 0x06004B2C RID: 19244 RVA: 0x0010F69E File Offset: 0x0010D89E
		public override MemberInfo ResolveMember(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			return this.InternalModule.ResolveMember(metadataToken, genericTypeArguments, genericMethodArguments);
		}

		// Token: 0x06004B2D RID: 19245 RVA: 0x0010F6AE File Offset: 0x0010D8AE
		public override string ResolveString(int metadataToken)
		{
			return this.InternalModule.ResolveString(metadataToken);
		}

		// Token: 0x06004B2E RID: 19246 RVA: 0x0010F6BC File Offset: 0x0010D8BC
		public override void GetPEKind(out PortableExecutableKinds peKind, out ImageFileMachine machine)
		{
			this.InternalModule.GetPEKind(out peKind, out machine);
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x06004B2F RID: 19247 RVA: 0x0010F6CB File Offset: 0x0010D8CB
		public override int MDStreamVersion
		{
			get
			{
				return this.InternalModule.MDStreamVersion;
			}
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x06004B30 RID: 19248 RVA: 0x0010F6D8 File Offset: 0x0010D8D8
		public override Guid ModuleVersionId
		{
			get
			{
				return this.InternalModule.ModuleVersionId;
			}
		}

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x06004B31 RID: 19249 RVA: 0x0010F6E5 File Offset: 0x0010D8E5
		public override int MetadataToken
		{
			get
			{
				return this.InternalModule.MetadataToken;
			}
		}

		// Token: 0x06004B32 RID: 19250 RVA: 0x0010F6F2 File Offset: 0x0010D8F2
		public override bool IsResource()
		{
			return this.InternalModule.IsResource();
		}

		// Token: 0x06004B33 RID: 19251 RVA: 0x0010F6FF File Offset: 0x0010D8FF
		public override FieldInfo[] GetFields(BindingFlags bindingFlags)
		{
			return this.InternalModule.GetFields(bindingFlags);
		}

		// Token: 0x06004B34 RID: 19252 RVA: 0x0010F70D File Offset: 0x0010D90D
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			return this.InternalModule.GetField(name, bindingAttr);
		}

		// Token: 0x06004B35 RID: 19253 RVA: 0x0010F71C File Offset: 0x0010D91C
		public override MethodInfo[] GetMethods(BindingFlags bindingFlags)
		{
			return this.InternalModule.GetMethods(bindingFlags);
		}

		// Token: 0x06004B36 RID: 19254 RVA: 0x0010F72A File Offset: 0x0010D92A
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			return this.InternalModule.GetMethodInternal(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x06004B37 RID: 19255 RVA: 0x0010F740 File Offset: 0x0010D940
		public override string ScopeName
		{
			get
			{
				return this.InternalModule.ScopeName;
			}
		}

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x06004B38 RID: 19256 RVA: 0x0010F74D File Offset: 0x0010D94D
		public override string Name
		{
			get
			{
				return this.InternalModule.Name;
			}
		}

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x06004B39 RID: 19257 RVA: 0x0010F75A File Offset: 0x0010D95A
		public override Assembly Assembly
		{
			get
			{
				return this.m_assemblyBuilder;
			}
		}

		// Token: 0x06004B3A RID: 19258 RVA: 0x0010F762 File Offset: 0x0010D962
		public override X509Certificate GetSignerCertificate()
		{
			return this.InternalModule.GetSignerCertificate();
		}

		// Token: 0x06004B3B RID: 19259 RVA: 0x0010F770 File Offset: 0x0010D970
		[SecuritySafeCritical]
		public TypeBuilder DefineType(string name)
		{
			object syncRoot = this.SyncRoot;
			TypeBuilder result;
			lock (syncRoot)
			{
				result = this.DefineTypeNoLock(name, TypeAttributes.NotPublic, null, null, PackingSize.Unspecified, 0);
			}
			return result;
		}

		// Token: 0x06004B3C RID: 19260 RVA: 0x0010F7B8 File Offset: 0x0010D9B8
		[SecuritySafeCritical]
		public TypeBuilder DefineType(string name, TypeAttributes attr)
		{
			object syncRoot = this.SyncRoot;
			TypeBuilder result;
			lock (syncRoot)
			{
				result = this.DefineTypeNoLock(name, attr, null, null, PackingSize.Unspecified, 0);
			}
			return result;
		}

		// Token: 0x06004B3D RID: 19261 RVA: 0x0010F800 File Offset: 0x0010DA00
		[SecuritySafeCritical]
		public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent)
		{
			object syncRoot = this.SyncRoot;
			TypeBuilder result;
			lock (syncRoot)
			{
				this.CheckContext(new Type[]
				{
					parent
				});
				result = this.DefineTypeNoLock(name, attr, parent, null, PackingSize.Unspecified, 0);
			}
			return result;
		}

		// Token: 0x06004B3E RID: 19262 RVA: 0x0010F858 File Offset: 0x0010DA58
		[SecuritySafeCritical]
		public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent, int typesize)
		{
			object syncRoot = this.SyncRoot;
			TypeBuilder result;
			lock (syncRoot)
			{
				result = this.DefineTypeNoLock(name, attr, parent, null, PackingSize.Unspecified, typesize);
			}
			return result;
		}

		// Token: 0x06004B3F RID: 19263 RVA: 0x0010F8A4 File Offset: 0x0010DAA4
		[SecuritySafeCritical]
		public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent, PackingSize packingSize, int typesize)
		{
			object syncRoot = this.SyncRoot;
			TypeBuilder result;
			lock (syncRoot)
			{
				result = this.DefineTypeNoLock(name, attr, parent, null, packingSize, typesize);
			}
			return result;
		}

		// Token: 0x06004B40 RID: 19264 RVA: 0x0010F8F0 File Offset: 0x0010DAF0
		[SecuritySafeCritical]
		[ComVisible(true)]
		public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent, Type[] interfaces)
		{
			object syncRoot = this.SyncRoot;
			TypeBuilder result;
			lock (syncRoot)
			{
				result = this.DefineTypeNoLock(name, attr, parent, interfaces, PackingSize.Unspecified, 0);
			}
			return result;
		}

		// Token: 0x06004B41 RID: 19265 RVA: 0x0010F93C File Offset: 0x0010DB3C
		[SecurityCritical]
		private TypeBuilder DefineTypeNoLock(string name, TypeAttributes attr, Type parent, Type[] interfaces, PackingSize packingSize, int typesize)
		{
			return new TypeBuilder(name, attr, parent, interfaces, this, packingSize, typesize, null);
		}

		// Token: 0x06004B42 RID: 19266 RVA: 0x0010F950 File Offset: 0x0010DB50
		[SecuritySafeCritical]
		public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent, PackingSize packsize)
		{
			object syncRoot = this.SyncRoot;
			TypeBuilder result;
			lock (syncRoot)
			{
				result = this.DefineTypeNoLock(name, attr, parent, packsize);
			}
			return result;
		}

		// Token: 0x06004B43 RID: 19267 RVA: 0x0010F998 File Offset: 0x0010DB98
		[SecurityCritical]
		private TypeBuilder DefineTypeNoLock(string name, TypeAttributes attr, Type parent, PackingSize packsize)
		{
			return new TypeBuilder(name, attr, parent, null, this, packsize, 0, null);
		}

		// Token: 0x06004B44 RID: 19268 RVA: 0x0010F9A8 File Offset: 0x0010DBA8
		[SecuritySafeCritical]
		public EnumBuilder DefineEnum(string name, TypeAttributes visibility, Type underlyingType)
		{
			this.CheckContext(new Type[]
			{
				underlyingType
			});
			object syncRoot = this.SyncRoot;
			EnumBuilder result;
			lock (syncRoot)
			{
				EnumBuilder enumBuilder = this.DefineEnumNoLock(name, visibility, underlyingType);
				this.m_TypeBuilderDict[name] = enumBuilder;
				result = enumBuilder;
			}
			return result;
		}

		// Token: 0x06004B45 RID: 19269 RVA: 0x0010FA0C File Offset: 0x0010DC0C
		[SecurityCritical]
		private EnumBuilder DefineEnumNoLock(string name, TypeAttributes visibility, Type underlyingType)
		{
			return new EnumBuilder(name, underlyingType, visibility, this);
		}

		// Token: 0x06004B46 RID: 19270 RVA: 0x0010FA17 File Offset: 0x0010DC17
		public IResourceWriter DefineResource(string name, string description)
		{
			return this.DefineResource(name, description, ResourceAttributes.Public);
		}

		// Token: 0x06004B47 RID: 19271 RVA: 0x0010FA24 File Offset: 0x0010DC24
		public IResourceWriter DefineResource(string name, string description, ResourceAttributes attribute)
		{
			object syncRoot = this.SyncRoot;
			IResourceWriter result;
			lock (syncRoot)
			{
				result = this.DefineResourceNoLock(name, description, attribute);
			}
			return result;
		}

		// Token: 0x06004B48 RID: 19272 RVA: 0x0010FA6C File Offset: 0x0010DC6C
		private IResourceWriter DefineResourceNoLock(string name, string description, ResourceAttributes attribute)
		{
			if (this.IsTransient())
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadResourceContainer"));
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "name");
			}
			if (this.m_assemblyBuilder.IsPersistable())
			{
				this.m_assemblyBuilder.m_assemblyData.CheckResNameConflict(name);
				MemoryStream memoryStream = new MemoryStream();
				ResourceWriter resourceWriter = new ResourceWriter(memoryStream);
				ResWriterData resWriterData = new ResWriterData(resourceWriter, memoryStream, name, string.Empty, string.Empty, attribute);
				resWriterData.m_nextResWriter = this.m_moduleData.m_embeddedRes;
				this.m_moduleData.m_embeddedRes = resWriterData;
				return resourceWriter;
			}
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadResourceContainer"));
		}

		// Token: 0x06004B49 RID: 19273 RVA: 0x0010FB2C File Offset: 0x0010DD2C
		public void DefineManifestResource(string name, Stream stream, ResourceAttributes attribute)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				this.DefineManifestResourceNoLock(name, stream, attribute);
			}
		}

		// Token: 0x06004B4A RID: 19274 RVA: 0x0010FB8C File Offset: 0x0010DD8C
		private void DefineManifestResourceNoLock(string name, Stream stream, ResourceAttributes attribute)
		{
			if (this.IsTransient())
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadResourceContainer"));
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "name");
			}
			if (this.m_assemblyBuilder.IsPersistable())
			{
				this.m_assemblyBuilder.m_assemblyData.CheckResNameConflict(name);
				ResWriterData resWriterData = new ResWriterData(null, stream, name, string.Empty, string.Empty, attribute);
				resWriterData.m_nextResWriter = this.m_moduleData.m_embeddedRes;
				this.m_moduleData.m_embeddedRes = resWriterData;
				return;
			}
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadResourceContainer"));
		}

		// Token: 0x06004B4B RID: 19275 RVA: 0x0010FC3C File Offset: 0x0010DE3C
		public void DefineUnmanagedResource(byte[] resource)
		{
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				this.DefineUnmanagedResourceInternalNoLock(resource);
			}
		}

		// Token: 0x06004B4C RID: 19276 RVA: 0x0010FC80 File Offset: 0x0010DE80
		internal void DefineUnmanagedResourceInternalNoLock(byte[] resource)
		{
			if (resource == null)
			{
				throw new ArgumentNullException("resource");
			}
			if (this.m_moduleData.m_strResourceFileName != null || this.m_moduleData.m_resourceBytes != null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NativeResourceAlreadyDefined"));
			}
			this.m_moduleData.m_resourceBytes = new byte[resource.Length];
			Array.Copy(resource, this.m_moduleData.m_resourceBytes, resource.Length);
		}

		// Token: 0x06004B4D RID: 19277 RVA: 0x0010FCEC File Offset: 0x0010DEEC
		[SecuritySafeCritical]
		public void DefineUnmanagedResource(string resourceFileName)
		{
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				this.DefineUnmanagedResourceFileInternalNoLock(resourceFileName);
			}
		}

		// Token: 0x06004B4E RID: 19278 RVA: 0x0010FD30 File Offset: 0x0010DF30
		[SecurityCritical]
		internal void DefineUnmanagedResourceFileInternalNoLock(string resourceFileName)
		{
			if (resourceFileName == null)
			{
				throw new ArgumentNullException("resourceFileName");
			}
			if (this.m_moduleData.m_resourceBytes != null || this.m_moduleData.m_strResourceFileName != null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NativeResourceAlreadyDefined"));
			}
			string text = Path.UnsafeGetFullPath(resourceFileName);
			new FileIOPermission(FileIOPermissionAccess.Read, text).Demand();
			new EnvironmentPermission(PermissionState.Unrestricted).Assert();
			try
			{
				if (!File.UnsafeExists(resourceFileName))
				{
					throw new FileNotFoundException(Environment.GetResourceString("IO.FileNotFound_FileName", new object[]
					{
						resourceFileName
					}), resourceFileName);
				}
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			this.m_moduleData.m_strResourceFileName = text;
		}

		// Token: 0x06004B4F RID: 19279 RVA: 0x0010FDDC File Offset: 0x0010DFDC
		public MethodBuilder DefineGlobalMethod(string name, MethodAttributes attributes, Type returnType, Type[] parameterTypes)
		{
			return this.DefineGlobalMethod(name, attributes, CallingConventions.Standard, returnType, parameterTypes);
		}

		// Token: 0x06004B50 RID: 19280 RVA: 0x0010FDEC File Offset: 0x0010DFEC
		public MethodBuilder DefineGlobalMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			return this.DefineGlobalMethod(name, attributes, callingConvention, returnType, null, null, parameterTypes, null, null);
		}

		// Token: 0x06004B51 RID: 19281 RVA: 0x0010FE0C File Offset: 0x0010E00C
		public MethodBuilder DefineGlobalMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] requiredReturnTypeCustomModifiers, Type[] optionalReturnTypeCustomModifiers, Type[] parameterTypes, Type[][] requiredParameterTypeCustomModifiers, Type[][] optionalParameterTypeCustomModifiers)
		{
			object syncRoot = this.SyncRoot;
			MethodBuilder result;
			lock (syncRoot)
			{
				result = this.DefineGlobalMethodNoLock(name, attributes, callingConvention, returnType, requiredReturnTypeCustomModifiers, optionalReturnTypeCustomModifiers, parameterTypes, requiredParameterTypeCustomModifiers, optionalParameterTypeCustomModifiers);
			}
			return result;
		}

		// Token: 0x06004B52 RID: 19282 RVA: 0x0010FE60 File Offset: 0x0010E060
		private MethodBuilder DefineGlobalMethodNoLock(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] requiredReturnTypeCustomModifiers, Type[] optionalReturnTypeCustomModifiers, Type[] parameterTypes, Type[][] requiredParameterTypeCustomModifiers, Type[][] optionalParameterTypeCustomModifiers)
		{
			if (this.m_moduleData.m_fGlobalBeenCreated)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_GlobalsHaveBeenCreated"));
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "name");
			}
			if ((attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_GlobalFunctionHasToBeStatic"));
			}
			this.CheckContext(new Type[]
			{
				returnType
			});
			this.CheckContext(new Type[][]
			{
				requiredReturnTypeCustomModifiers,
				optionalReturnTypeCustomModifiers,
				parameterTypes
			});
			this.CheckContext(requiredParameterTypeCustomModifiers);
			this.CheckContext(optionalParameterTypeCustomModifiers);
			this.m_moduleData.m_fHasGlobal = true;
			return this.m_moduleData.m_globalTypeBuilder.DefineMethod(name, attributes, callingConvention, returnType, requiredReturnTypeCustomModifiers, optionalReturnTypeCustomModifiers, parameterTypes, requiredParameterTypeCustomModifiers, optionalParameterTypeCustomModifiers);
		}

		// Token: 0x06004B53 RID: 19283 RVA: 0x0010FF34 File Offset: 0x0010E134
		public MethodBuilder DefinePInvokeMethod(string name, string dllName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, CallingConvention nativeCallConv, CharSet nativeCharSet)
		{
			return this.DefinePInvokeMethod(name, dllName, name, attributes, callingConvention, returnType, parameterTypes, nativeCallConv, nativeCharSet);
		}

		// Token: 0x06004B54 RID: 19284 RVA: 0x0010FF58 File Offset: 0x0010E158
		public MethodBuilder DefinePInvokeMethod(string name, string dllName, string entryName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, CallingConvention nativeCallConv, CharSet nativeCharSet)
		{
			object syncRoot = this.SyncRoot;
			MethodBuilder result;
			lock (syncRoot)
			{
				result = this.DefinePInvokeMethodNoLock(name, dllName, entryName, attributes, callingConvention, returnType, parameterTypes, nativeCallConv, nativeCharSet);
			}
			return result;
		}

		// Token: 0x06004B55 RID: 19285 RVA: 0x0010FFAC File Offset: 0x0010E1AC
		private MethodBuilder DefinePInvokeMethodNoLock(string name, string dllName, string entryName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, CallingConvention nativeCallConv, CharSet nativeCharSet)
		{
			if ((attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_GlobalFunctionHasToBeStatic"));
			}
			this.CheckContext(new Type[]
			{
				returnType
			});
			this.CheckContext(parameterTypes);
			this.m_moduleData.m_fHasGlobal = true;
			return this.m_moduleData.m_globalTypeBuilder.DefinePInvokeMethod(name, dllName, entryName, attributes, callingConvention, returnType, parameterTypes, nativeCallConv, nativeCharSet);
		}

		// Token: 0x06004B56 RID: 19286 RVA: 0x00110014 File Offset: 0x0010E214
		public void CreateGlobalFunctions()
		{
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				this.CreateGlobalFunctionsNoLock();
			}
		}

		// Token: 0x06004B57 RID: 19287 RVA: 0x00110054 File Offset: 0x0010E254
		private void CreateGlobalFunctionsNoLock()
		{
			if (this.m_moduleData.m_fGlobalBeenCreated)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotADebugModule"));
			}
			this.m_moduleData.m_globalTypeBuilder.CreateType();
			this.m_moduleData.m_fGlobalBeenCreated = true;
		}

		// Token: 0x06004B58 RID: 19288 RVA: 0x00110090 File Offset: 0x0010E290
		public FieldBuilder DefineInitializedData(string name, byte[] data, FieldAttributes attributes)
		{
			object syncRoot = this.SyncRoot;
			FieldBuilder result;
			lock (syncRoot)
			{
				result = this.DefineInitializedDataNoLock(name, data, attributes);
			}
			return result;
		}

		// Token: 0x06004B59 RID: 19289 RVA: 0x001100D8 File Offset: 0x0010E2D8
		private FieldBuilder DefineInitializedDataNoLock(string name, byte[] data, FieldAttributes attributes)
		{
			if (this.m_moduleData.m_fGlobalBeenCreated)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_GlobalsHaveBeenCreated"));
			}
			this.m_moduleData.m_fHasGlobal = true;
			return this.m_moduleData.m_globalTypeBuilder.DefineInitializedData(name, data, attributes);
		}

		// Token: 0x06004B5A RID: 19290 RVA: 0x00110118 File Offset: 0x0010E318
		public FieldBuilder DefineUninitializedData(string name, int size, FieldAttributes attributes)
		{
			object syncRoot = this.SyncRoot;
			FieldBuilder result;
			lock (syncRoot)
			{
				result = this.DefineUninitializedDataNoLock(name, size, attributes);
			}
			return result;
		}

		// Token: 0x06004B5B RID: 19291 RVA: 0x00110160 File Offset: 0x0010E360
		private FieldBuilder DefineUninitializedDataNoLock(string name, int size, FieldAttributes attributes)
		{
			if (this.m_moduleData.m_fGlobalBeenCreated)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_GlobalsHaveBeenCreated"));
			}
			this.m_moduleData.m_fHasGlobal = true;
			return this.m_moduleData.m_globalTypeBuilder.DefineUninitializedData(name, size, attributes);
		}

		// Token: 0x06004B5C RID: 19292 RVA: 0x0011019E File Offset: 0x0010E39E
		[SecurityCritical]
		internal TypeToken GetTypeTokenInternal(Type type)
		{
			return this.GetTypeTokenInternal(type, false);
		}

		// Token: 0x06004B5D RID: 19293 RVA: 0x001101A8 File Offset: 0x0010E3A8
		[SecurityCritical]
		private TypeToken GetTypeTokenInternal(Type type, bool getGenericDefinition)
		{
			object syncRoot = this.SyncRoot;
			TypeToken typeTokenWorkerNoLock;
			lock (syncRoot)
			{
				typeTokenWorkerNoLock = this.GetTypeTokenWorkerNoLock(type, getGenericDefinition);
			}
			return typeTokenWorkerNoLock;
		}

		// Token: 0x06004B5E RID: 19294 RVA: 0x001101EC File Offset: 0x0010E3EC
		[SecuritySafeCritical]
		public TypeToken GetTypeToken(Type type)
		{
			return this.GetTypeTokenInternal(type, true);
		}

		// Token: 0x06004B5F RID: 19295 RVA: 0x001101F8 File Offset: 0x0010E3F8
		[SecurityCritical]
		private TypeToken GetTypeTokenWorkerNoLock(Type type, bool getGenericDefinition)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.CheckContext(new Type[]
			{
				type
			});
			if (type.IsByRef)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_CannotGetTypeTokenForByRef"));
			}
			if ((type.IsGenericType && (!type.IsGenericTypeDefinition || !getGenericDefinition)) || type.IsGenericParameter || type.IsArray || type.IsPointer)
			{
				int length;
				byte[] signature = SignatureHelper.GetTypeSigToken(this, type).InternalGetSignature(out length);
				return new TypeToken(this.GetTokenFromTypeSpec(signature, length));
			}
			Module module = type.Module;
			if (module.Equals(this))
			{
				EnumBuilder enumBuilder = type as EnumBuilder;
				TypeBuilder typeBuilder;
				if (enumBuilder != null)
				{
					typeBuilder = enumBuilder.m_typeBuilder;
				}
				else
				{
					typeBuilder = (type as TypeBuilder);
				}
				if (typeBuilder != null)
				{
					return typeBuilder.TypeToken;
				}
				GenericTypeParameterBuilder genericTypeParameterBuilder;
				if ((genericTypeParameterBuilder = (type as GenericTypeParameterBuilder)) != null)
				{
					return new TypeToken(genericTypeParameterBuilder.MetadataTokenInternal);
				}
				return new TypeToken(this.GetTypeRefNested(type, this, string.Empty));
			}
			else
			{
				ModuleBuilder moduleBuilder = module as ModuleBuilder;
				bool flag = (moduleBuilder != null) ? moduleBuilder.IsTransient() : ((RuntimeModule)module).IsTransientInternal();
				if (!this.IsTransient() && flag)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadTransientModuleReference"));
				}
				string strRefedModuleFileName = string.Empty;
				if (module.Assembly.Equals(this.Assembly))
				{
					if (moduleBuilder == null)
					{
						moduleBuilder = this.ContainingAssemblyBuilder.GetModuleBuilder((InternalModuleBuilder)module);
					}
					strRefedModuleFileName = moduleBuilder.m_moduleData.m_strFileName;
				}
				return new TypeToken(this.GetTypeRefNested(type, module, strRefedModuleFileName));
			}
		}

		// Token: 0x06004B60 RID: 19296 RVA: 0x00110399 File Offset: 0x0010E599
		public TypeToken GetTypeToken(string name)
		{
			return this.GetTypeToken(this.InternalModule.GetType(name, false, true));
		}

		// Token: 0x06004B61 RID: 19297 RVA: 0x001103B0 File Offset: 0x0010E5B0
		[SecuritySafeCritical]
		public MethodToken GetMethodToken(MethodInfo method)
		{
			object syncRoot = this.SyncRoot;
			MethodToken methodTokenNoLock;
			lock (syncRoot)
			{
				methodTokenNoLock = this.GetMethodTokenNoLock(method, true);
			}
			return methodTokenNoLock;
		}

		// Token: 0x06004B62 RID: 19298 RVA: 0x001103F4 File Offset: 0x0010E5F4
		[SecurityCritical]
		internal MethodToken GetMethodTokenInternal(MethodInfo method)
		{
			object syncRoot = this.SyncRoot;
			MethodToken methodTokenNoLock;
			lock (syncRoot)
			{
				methodTokenNoLock = this.GetMethodTokenNoLock(method, false);
			}
			return methodTokenNoLock;
		}

		// Token: 0x06004B63 RID: 19299 RVA: 0x00110438 File Offset: 0x0010E638
		[SecurityCritical]
		private MethodToken GetMethodTokenNoLock(MethodInfo method, bool getGenericTypeDefinition)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			MethodBuilder methodBuilder;
			int str;
			if ((methodBuilder = (method as MethodBuilder)) != null)
			{
				int metadataTokenInternal = methodBuilder.MetadataTokenInternal;
				if (method.Module.Equals(this))
				{
					return new MethodToken(metadataTokenInternal);
				}
				if (method.DeclaringType == null)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotImportGlobalFromDifferentModule"));
				}
				int tr = getGenericTypeDefinition ? this.GetTypeToken(method.DeclaringType).Token : this.GetTypeTokenInternal(method.DeclaringType).Token;
				str = this.GetMemberRef(method.DeclaringType.Module, tr, metadataTokenInternal);
			}
			else
			{
				if (method is MethodOnTypeBuilderInstantiation)
				{
					return new MethodToken(this.GetMemberRefToken(method, null));
				}
				SymbolMethod symbolMethod;
				if ((symbolMethod = (method as SymbolMethod)) != null)
				{
					if (symbolMethod.GetModule() == this)
					{
						return symbolMethod.GetToken();
					}
					return symbolMethod.GetToken(this);
				}
				else
				{
					Type declaringType = method.DeclaringType;
					if (declaringType == null)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotImportGlobalFromDifferentModule"));
					}
					if (declaringType.IsArray)
					{
						ParameterInfo[] parameters = method.GetParameters();
						Type[] array = new Type[parameters.Length];
						for (int i = 0; i < parameters.Length; i++)
						{
							array[i] = parameters[i].ParameterType;
						}
						return this.GetArrayMethodToken(declaringType, method.Name, method.CallingConvention, method.ReturnType, array);
					}
					RuntimeMethodInfo method2;
					if ((method2 = (method as RuntimeMethodInfo)) != null)
					{
						int tr = getGenericTypeDefinition ? this.GetTypeToken(method.DeclaringType).Token : this.GetTypeTokenInternal(method.DeclaringType).Token;
						str = this.GetMemberRefOfMethodInfo(tr, method2);
					}
					else
					{
						ParameterInfo[] parameters2 = method.GetParameters();
						Type[] array2 = new Type[parameters2.Length];
						Type[][] array3 = new Type[array2.Length][];
						Type[][] array4 = new Type[array2.Length][];
						for (int j = 0; j < parameters2.Length; j++)
						{
							array2[j] = parameters2[j].ParameterType;
							array3[j] = parameters2[j].GetRequiredCustomModifiers();
							array4[j] = parameters2[j].GetOptionalCustomModifiers();
						}
						int tr = getGenericTypeDefinition ? this.GetTypeToken(method.DeclaringType).Token : this.GetTypeTokenInternal(method.DeclaringType).Token;
						SignatureHelper methodSigHelper;
						try
						{
							methodSigHelper = SignatureHelper.GetMethodSigHelper(this, method.CallingConvention, method.ReturnType, method.ReturnParameter.GetRequiredCustomModifiers(), method.ReturnParameter.GetOptionalCustomModifiers(), array2, array3, array4);
						}
						catch (NotImplementedException)
						{
							methodSigHelper = SignatureHelper.GetMethodSigHelper(this, method.ReturnType, array2);
						}
						int length;
						byte[] signature = methodSigHelper.InternalGetSignature(out length);
						str = this.GetMemberRefFromSignature(tr, method.Name, signature, length);
					}
				}
			}
			return new MethodToken(str);
		}

		// Token: 0x06004B64 RID: 19300 RVA: 0x0011071C File Offset: 0x0010E91C
		[SecuritySafeCritical]
		public MethodToken GetConstructorToken(ConstructorInfo constructor, IEnumerable<Type> optionalParameterTypes)
		{
			if (constructor == null)
			{
				throw new ArgumentNullException("constructor");
			}
			object syncRoot = this.SyncRoot;
			MethodToken result;
			lock (syncRoot)
			{
				result = new MethodToken(this.GetMethodTokenInternal(constructor, optionalParameterTypes, false));
			}
			return result;
		}

		// Token: 0x06004B65 RID: 19301 RVA: 0x0011077C File Offset: 0x0010E97C
		[SecuritySafeCritical]
		public MethodToken GetMethodToken(MethodInfo method, IEnumerable<Type> optionalParameterTypes)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			object syncRoot = this.SyncRoot;
			MethodToken result;
			lock (syncRoot)
			{
				result = new MethodToken(this.GetMethodTokenInternal(method, optionalParameterTypes, true));
			}
			return result;
		}

		// Token: 0x06004B66 RID: 19302 RVA: 0x001107DC File Offset: 0x0010E9DC
		[SecurityCritical]
		internal int GetMethodTokenInternal(MethodBase method, IEnumerable<Type> optionalParameterTypes, bool useMethodDef)
		{
			MethodInfo methodInfo = method as MethodInfo;
			int num;
			if (method.IsGenericMethod)
			{
				MethodInfo methodInfo2 = methodInfo;
				bool isGenericMethodDefinition = methodInfo.IsGenericMethodDefinition;
				if (!isGenericMethodDefinition)
				{
					methodInfo2 = methodInfo.GetGenericMethodDefinition();
				}
				if (!this.Equals(methodInfo2.Module) || (methodInfo2.DeclaringType != null && methodInfo2.DeclaringType.IsGenericType))
				{
					num = this.GetMemberRefToken(methodInfo2, null);
				}
				else
				{
					num = this.GetMethodTokenInternal(methodInfo2).Token;
				}
				if (isGenericMethodDefinition && useMethodDef)
				{
					return num;
				}
				int sigLength;
				byte[] signature = SignatureHelper.GetMethodSpecSigHelper(this, methodInfo.GetGenericArguments()).InternalGetSignature(out sigLength);
				num = TypeBuilder.DefineMethodSpec(this.GetNativeHandle(), num, signature, sigLength);
			}
			else if ((method.CallingConvention & CallingConventions.VarArgs) == (CallingConventions)0 && (method.DeclaringType == null || !method.DeclaringType.IsGenericType))
			{
				if (methodInfo != null)
				{
					num = this.GetMethodTokenInternal(methodInfo).Token;
				}
				else
				{
					num = this.GetConstructorToken(method as ConstructorInfo).Token;
				}
			}
			else
			{
				num = this.GetMemberRefToken(method, optionalParameterTypes);
			}
			return num;
		}

		// Token: 0x06004B67 RID: 19303 RVA: 0x001108E8 File Offset: 0x0010EAE8
		[SecuritySafeCritical]
		public MethodToken GetArrayMethodToken(Type arrayClass, string methodName, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			object syncRoot = this.SyncRoot;
			MethodToken arrayMethodTokenNoLock;
			lock (syncRoot)
			{
				arrayMethodTokenNoLock = this.GetArrayMethodTokenNoLock(arrayClass, methodName, callingConvention, returnType, parameterTypes);
			}
			return arrayMethodTokenNoLock;
		}

		// Token: 0x06004B68 RID: 19304 RVA: 0x00110934 File Offset: 0x0010EB34
		[SecurityCritical]
		private MethodToken GetArrayMethodTokenNoLock(Type arrayClass, string methodName, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			if (arrayClass == null)
			{
				throw new ArgumentNullException("arrayClass");
			}
			if (methodName == null)
			{
				throw new ArgumentNullException("methodName");
			}
			if (methodName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "methodName");
			}
			if (!arrayClass.IsArray)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_HasToBeArrayClass"));
			}
			this.CheckContext(new Type[]
			{
				returnType,
				arrayClass
			});
			this.CheckContext(parameterTypes);
			SignatureHelper methodSigHelper = SignatureHelper.GetMethodSigHelper(this, callingConvention, returnType, null, null, parameterTypes, null, null);
			int sigLength;
			byte[] signature = methodSigHelper.InternalGetSignature(out sigLength);
			TypeToken typeTokenInternal = this.GetTypeTokenInternal(arrayClass);
			return new MethodToken(ModuleBuilder.GetArrayMethodToken(this.GetNativeHandle(), typeTokenInternal.Token, methodName, signature, sigLength));
		}

		// Token: 0x06004B69 RID: 19305 RVA: 0x001109F0 File Offset: 0x0010EBF0
		[SecuritySafeCritical]
		public MethodInfo GetArrayMethod(Type arrayClass, string methodName, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			this.CheckContext(new Type[]
			{
				returnType,
				arrayClass
			});
			this.CheckContext(parameterTypes);
			MethodToken arrayMethodToken = this.GetArrayMethodToken(arrayClass, methodName, callingConvention, returnType, parameterTypes);
			return new SymbolMethod(this, arrayMethodToken, arrayClass, methodName, callingConvention, returnType, parameterTypes);
		}

		// Token: 0x06004B6A RID: 19306 RVA: 0x00110A36 File Offset: 0x0010EC36
		[SecuritySafeCritical]
		[ComVisible(true)]
		public MethodToken GetConstructorToken(ConstructorInfo con)
		{
			return this.InternalGetConstructorToken(con, false);
		}

		// Token: 0x06004B6B RID: 19307 RVA: 0x00110A40 File Offset: 0x0010EC40
		[SecuritySafeCritical]
		public FieldToken GetFieldToken(FieldInfo field)
		{
			object syncRoot = this.SyncRoot;
			FieldToken fieldTokenNoLock;
			lock (syncRoot)
			{
				fieldTokenNoLock = this.GetFieldTokenNoLock(field);
			}
			return fieldTokenNoLock;
		}

		// Token: 0x06004B6C RID: 19308 RVA: 0x00110A84 File Offset: 0x0010EC84
		[SecurityCritical]
		private FieldToken GetFieldTokenNoLock(FieldInfo field)
		{
			if (field == null)
			{
				throw new ArgumentNullException("con");
			}
			FieldBuilder fieldBuilder;
			int field2;
			RuntimeFieldInfo runtimeField;
			FieldOnTypeBuilderInstantiation fieldOnTypeBuilderInstantiation;
			if ((fieldBuilder = (field as FieldBuilder)) != null)
			{
				if (field.DeclaringType != null && field.DeclaringType.IsGenericType)
				{
					int length;
					byte[] signature = SignatureHelper.GetTypeSigToken(this, field.DeclaringType).InternalGetSignature(out length);
					int num = this.GetTokenFromTypeSpec(signature, length);
					field2 = this.GetMemberRef(this, num, fieldBuilder.GetToken().Token);
				}
				else
				{
					if (fieldBuilder.Module.Equals(this))
					{
						return fieldBuilder.GetToken();
					}
					if (field.DeclaringType == null)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotImportGlobalFromDifferentModule"));
					}
					int num = this.GetTypeTokenInternal(field.DeclaringType).Token;
					field2 = this.GetMemberRef(field.ReflectedType.Module, num, fieldBuilder.GetToken().Token);
				}
			}
			else if ((runtimeField = (field as RuntimeFieldInfo)) != null)
			{
				if (field.DeclaringType == null)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotImportGlobalFromDifferentModule"));
				}
				if (field.DeclaringType != null && field.DeclaringType.IsGenericType)
				{
					int length2;
					byte[] signature2 = SignatureHelper.GetTypeSigToken(this, field.DeclaringType).InternalGetSignature(out length2);
					int num = this.GetTokenFromTypeSpec(signature2, length2);
					field2 = this.GetMemberRefOfFieldInfo(num, field.DeclaringType.GetTypeHandleInternal(), runtimeField);
				}
				else
				{
					int num = this.GetTypeTokenInternal(field.DeclaringType).Token;
					field2 = this.GetMemberRefOfFieldInfo(num, field.DeclaringType.GetTypeHandleInternal(), runtimeField);
				}
			}
			else if ((fieldOnTypeBuilderInstantiation = (field as FieldOnTypeBuilderInstantiation)) != null)
			{
				FieldInfo fieldInfo = fieldOnTypeBuilderInstantiation.FieldInfo;
				int length3;
				byte[] signature3 = SignatureHelper.GetTypeSigToken(this, field.DeclaringType).InternalGetSignature(out length3);
				int num = this.GetTokenFromTypeSpec(signature3, length3);
				field2 = this.GetMemberRef(fieldInfo.ReflectedType.Module, num, fieldOnTypeBuilderInstantiation.MetadataTokenInternal);
			}
			else
			{
				int num = this.GetTypeTokenInternal(field.ReflectedType).Token;
				SignatureHelper fieldSigHelper = SignatureHelper.GetFieldSigHelper(this);
				fieldSigHelper.AddArgument(field.FieldType, field.GetRequiredCustomModifiers(), field.GetOptionalCustomModifiers());
				int length4;
				byte[] signature4 = fieldSigHelper.InternalGetSignature(out length4);
				field2 = this.GetMemberRefFromSignature(num, field.Name, signature4, length4);
			}
			return new FieldToken(field2, field.GetType());
		}

		// Token: 0x06004B6D RID: 19309 RVA: 0x00110CF4 File Offset: 0x0010EEF4
		[SecuritySafeCritical]
		public StringToken GetStringConstant(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return new StringToken(ModuleBuilder.GetStringConstant(this.GetNativeHandle(), str, str.Length));
		}

		// Token: 0x06004B6E RID: 19310 RVA: 0x00110D1C File Offset: 0x0010EF1C
		[SecuritySafeCritical]
		public SignatureToken GetSignatureToken(SignatureHelper sigHelper)
		{
			if (sigHelper == null)
			{
				throw new ArgumentNullException("sigHelper");
			}
			int sigLength;
			byte[] signature = sigHelper.InternalGetSignature(out sigLength);
			return new SignatureToken(TypeBuilder.GetTokenFromSig(this.GetNativeHandle(), signature, sigLength), this);
		}

		// Token: 0x06004B6F RID: 19311 RVA: 0x00110D54 File Offset: 0x0010EF54
		[SecuritySafeCritical]
		public SignatureToken GetSignatureToken(byte[] sigBytes, int sigLength)
		{
			if (sigBytes == null)
			{
				throw new ArgumentNullException("sigBytes");
			}
			byte[] array = new byte[sigBytes.Length];
			Array.Copy(sigBytes, array, sigBytes.Length);
			return new SignatureToken(TypeBuilder.GetTokenFromSig(this.GetNativeHandle(), array, sigLength), this);
		}

		// Token: 0x06004B70 RID: 19312 RVA: 0x00110D98 File Offset: 0x0010EF98
		[SecuritySafeCritical]
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (binaryAttribute == null)
			{
				throw new ArgumentNullException("binaryAttribute");
			}
			TypeBuilder.DefineCustomAttribute(this, 1, this.GetConstructorToken(con).Token, binaryAttribute, false, false);
		}

		// Token: 0x06004B71 RID: 19313 RVA: 0x00110DE0 File Offset: 0x0010EFE0
		[SecuritySafeCritical]
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			customBuilder.CreateCustomAttribute(this, 1);
		}

		// Token: 0x06004B72 RID: 19314 RVA: 0x00110DF8 File Offset: 0x0010EFF8
		public ISymbolWriter GetSymWriter()
		{
			return this.m_iSymWriter;
		}

		// Token: 0x06004B73 RID: 19315 RVA: 0x00110E00 File Offset: 0x0010F000
		public ISymbolDocumentWriter DefineDocument(string url, Guid language, Guid languageVendor, Guid documentType)
		{
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			object syncRoot = this.SyncRoot;
			ISymbolDocumentWriter result;
			lock (syncRoot)
			{
				result = this.DefineDocumentNoLock(url, language, languageVendor, documentType);
			}
			return result;
		}

		// Token: 0x06004B74 RID: 19316 RVA: 0x00110E58 File Offset: 0x0010F058
		private ISymbolDocumentWriter DefineDocumentNoLock(string url, Guid language, Guid languageVendor, Guid documentType)
		{
			if (this.m_iSymWriter == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotADebugModule"));
			}
			return this.m_iSymWriter.DefineDocument(url, language, languageVendor, documentType);
		}

		// Token: 0x06004B75 RID: 19317 RVA: 0x00110E84 File Offset: 0x0010F084
		[SecuritySafeCritical]
		public void SetUserEntryPoint(MethodInfo entryPoint)
		{
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				this.SetUserEntryPointNoLock(entryPoint);
			}
		}

		// Token: 0x06004B76 RID: 19318 RVA: 0x00110EC8 File Offset: 0x0010F0C8
		[SecurityCritical]
		private void SetUserEntryPointNoLock(MethodInfo entryPoint)
		{
			if (entryPoint == null)
			{
				throw new ArgumentNullException("entryPoint");
			}
			if (this.m_iSymWriter == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotADebugModule"));
			}
			if (entryPoint.DeclaringType != null)
			{
				if (!entryPoint.Module.Equals(this))
				{
					throw new InvalidOperationException(Environment.GetResourceString("Argument_NotInTheSameModuleBuilder"));
				}
			}
			else
			{
				MethodBuilder methodBuilder = entryPoint as MethodBuilder;
				if (methodBuilder != null && methodBuilder.GetModuleBuilder() != this)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Argument_NotInTheSameModuleBuilder"));
				}
			}
			SymbolToken userEntryPoint = new SymbolToken(this.GetMethodTokenInternal(entryPoint).Token);
			this.m_iSymWriter.SetUserEntryPoint(userEntryPoint);
		}

		// Token: 0x06004B77 RID: 19319 RVA: 0x00110F80 File Offset: 0x0010F180
		public void SetSymCustomAttribute(string name, byte[] data)
		{
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				this.SetSymCustomAttributeNoLock(name, data);
			}
		}

		// Token: 0x06004B78 RID: 19320 RVA: 0x00110FC4 File Offset: 0x0010F1C4
		private void SetSymCustomAttributeNoLock(string name, byte[] data)
		{
			if (this.m_iSymWriter == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotADebugModule"));
			}
		}

		// Token: 0x06004B79 RID: 19321 RVA: 0x00110FDE File Offset: 0x0010F1DE
		public bool IsTransient()
		{
			return this.InternalModule.IsTransientInternal();
		}

		// Token: 0x06004B7A RID: 19322 RVA: 0x00110FEB File Offset: 0x0010F1EB
		void _ModuleBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B7B RID: 19323 RVA: 0x00110FF2 File Offset: 0x0010F1F2
		void _ModuleBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B7C RID: 19324 RVA: 0x00110FF9 File Offset: 0x0010F1F9
		void _ModuleBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B7D RID: 19325 RVA: 0x00111000 File Offset: 0x0010F200
		void _ModuleBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001E9A RID: 7834
		private Dictionary<string, Type> m_TypeBuilderDict;

		// Token: 0x04001E9B RID: 7835
		private ISymbolWriter m_iSymWriter;

		// Token: 0x04001E9C RID: 7836
		internal ModuleBuilderData m_moduleData;

		// Token: 0x04001E9D RID: 7837
		private MethodToken m_EntryPoint;

		// Token: 0x04001E9E RID: 7838
		internal InternalModuleBuilder m_internalModuleBuilder;

		// Token: 0x04001E9F RID: 7839
		private AssemblyBuilder m_assemblyBuilder;
	}
}
