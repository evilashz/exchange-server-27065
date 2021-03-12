using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Reflection
{
	// Token: 0x020005E1 RID: 1505
	[Serializable]
	internal class RuntimeModule : Module
	{
		// Token: 0x060046A1 RID: 18081 RVA: 0x0010061F File Offset: 0x000FE81F
		internal RuntimeModule()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060046A2 RID: 18082
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetType(RuntimeModule module, string className, bool ignoreCase, bool throwOnError, ObjectHandleOnStack type);

		// Token: 0x060046A3 RID: 18083
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall")]
		private static extern bool nIsTransientInternal(RuntimeModule module);

		// Token: 0x060046A4 RID: 18084
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetScopeName(RuntimeModule module, StringHandleOnStack retString);

		// Token: 0x060046A5 RID: 18085
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetFullyQualifiedName(RuntimeModule module, StringHandleOnStack retString);

		// Token: 0x060046A6 RID: 18086
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RuntimeType[] GetTypes(RuntimeModule module);

		// Token: 0x060046A7 RID: 18087 RVA: 0x0010062C File Offset: 0x000FE82C
		[SecuritySafeCritical]
		internal RuntimeType[] GetDefinedTypes()
		{
			return RuntimeModule.GetTypes(this.GetNativeHandle());
		}

		// Token: 0x060046A8 RID: 18088
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsResource(RuntimeModule module);

		// Token: 0x060046A9 RID: 18089
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetSignerCertificate(RuntimeModule module, ObjectHandleOnStack retData);

		// Token: 0x060046AA RID: 18090 RVA: 0x0010063C File Offset: 0x000FE83C
		private static RuntimeTypeHandle[] ConvertToTypeHandleArray(Type[] genericArguments)
		{
			if (genericArguments == null)
			{
				return null;
			}
			int num = genericArguments.Length;
			RuntimeTypeHandle[] array = new RuntimeTypeHandle[num];
			for (int i = 0; i < num; i++)
			{
				Type type = genericArguments[i];
				if (type == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidGenericInstArray"));
				}
				type = type.UnderlyingSystemType;
				if (type == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidGenericInstArray"));
				}
				if (!(type is RuntimeType))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidGenericInstArray"));
				}
				array[i] = type.GetTypeHandleInternal();
			}
			return array;
		}

		// Token: 0x060046AB RID: 18091 RVA: 0x001006C8 File Offset: 0x000FE8C8
		[SecuritySafeCritical]
		public override byte[] ResolveSignature(int metadataToken)
		{
			MetadataToken metadataToken2 = new MetadataToken(metadataToken);
			if (!this.MetadataImport.IsValidToken(metadataToken2))
			{
				throw new ArgumentOutOfRangeException("metadataToken", Environment.GetResourceString("Argument_InvalidToken", new object[]
				{
					metadataToken2,
					this
				}));
			}
			if (!metadataToken2.IsMemberRef && !metadataToken2.IsMethodDef && !metadataToken2.IsTypeSpec && !metadataToken2.IsSignature && !metadataToken2.IsFieldDef)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidToken", new object[]
				{
					metadataToken2,
					this
				}), "metadataToken");
			}
			ConstArray constArray;
			if (metadataToken2.IsMemberRef)
			{
				constArray = this.MetadataImport.GetMemberRefProps(metadataToken);
			}
			else
			{
				constArray = this.MetadataImport.GetSignatureFromToken(metadataToken);
			}
			byte[] array = new byte[constArray.Length];
			for (int i = 0; i < constArray.Length; i++)
			{
				array[i] = constArray[i];
			}
			return array;
		}

		// Token: 0x060046AC RID: 18092 RVA: 0x001007CC File Offset: 0x000FE9CC
		[SecuritySafeCritical]
		public unsafe override MethodBase ResolveMethod(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			MetadataToken metadataToken2 = new MetadataToken(metadataToken);
			if (!this.MetadataImport.IsValidToken(metadataToken2))
			{
				throw new ArgumentOutOfRangeException("metadataToken", Environment.GetResourceString("Argument_InvalidToken", new object[]
				{
					metadataToken2,
					this
				}));
			}
			RuntimeTypeHandle[] typeInstantiationContext = RuntimeModule.ConvertToTypeHandleArray(genericTypeArguments);
			RuntimeTypeHandle[] methodInstantiationContext = RuntimeModule.ConvertToTypeHandleArray(genericMethodArguments);
			MethodBase methodBase;
			try
			{
				if (!metadataToken2.IsMethodDef && !metadataToken2.IsMethodSpec)
				{
					if (!metadataToken2.IsMemberRef)
					{
						throw new ArgumentException("metadataToken", Environment.GetResourceString("Argument_ResolveMethod", new object[]
						{
							metadataToken2,
							this
						}));
					}
					if (*(byte*)this.MetadataImport.GetMemberRefProps(metadataToken2).Signature.ToPointer() == 6)
					{
						throw new ArgumentException("metadataToken", Environment.GetResourceString("Argument_ResolveMethod", new object[]
						{
							metadataToken2,
							this
						}));
					}
				}
				IRuntimeMethodInfo runtimeMethodInfo = ModuleHandle.ResolveMethodHandleInternal(this.GetNativeHandle(), metadataToken2, typeInstantiationContext, methodInstantiationContext);
				Type type = RuntimeMethodHandle.GetDeclaringType(runtimeMethodInfo);
				if (type.IsGenericType || type.IsArray)
				{
					MetadataToken token = new MetadataToken(this.MetadataImport.GetParentToken(metadataToken2));
					if (metadataToken2.IsMethodSpec)
					{
						token = new MetadataToken(this.MetadataImport.GetParentToken(token));
					}
					type = this.ResolveType(token, genericTypeArguments, genericMethodArguments);
				}
				methodBase = RuntimeType.GetMethodBase(type as RuntimeType, runtimeMethodInfo);
			}
			catch (BadImageFormatException innerException)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_BadImageFormatExceptionResolve"), innerException);
			}
			return methodBase;
		}

		// Token: 0x060046AD RID: 18093 RVA: 0x00100990 File Offset: 0x000FEB90
		[SecurityCritical]
		private FieldInfo ResolveLiteralField(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			MetadataToken metadataToken2 = new MetadataToken(metadataToken);
			if (!this.MetadataImport.IsValidToken(metadataToken2) || !metadataToken2.IsFieldDef)
			{
				throw new ArgumentOutOfRangeException("metadataToken", string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString("Argument_InvalidToken", new object[]
				{
					metadataToken2,
					this
				}), Array.Empty<object>()));
			}
			string name = this.MetadataImport.GetName(metadataToken2).ToString();
			int parentToken = this.MetadataImport.GetParentToken(metadataToken2);
			Type type = this.ResolveType(parentToken, genericTypeArguments, genericMethodArguments);
			type.GetFields();
			FieldInfo field;
			try
			{
				field = type.GetField(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
			catch
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ResolveField", new object[]
				{
					metadataToken2,
					this
				}), "metadataToken");
			}
			return field;
		}

		// Token: 0x060046AE RID: 18094 RVA: 0x00100A90 File Offset: 0x000FEC90
		[SecuritySafeCritical]
		public unsafe override FieldInfo ResolveField(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			MetadataToken metadataToken2 = new MetadataToken(metadataToken);
			if (!this.MetadataImport.IsValidToken(metadataToken2))
			{
				throw new ArgumentOutOfRangeException("metadataToken", Environment.GetResourceString("Argument_InvalidToken", new object[]
				{
					metadataToken2,
					this
				}));
			}
			RuntimeTypeHandle[] typeInstantiationContext = RuntimeModule.ConvertToTypeHandleArray(genericTypeArguments);
			RuntimeTypeHandle[] methodInstantiationContext = RuntimeModule.ConvertToTypeHandleArray(genericMethodArguments);
			FieldInfo result;
			try
			{
				IRuntimeFieldInfo runtimeFieldInfo;
				if (!metadataToken2.IsFieldDef)
				{
					if (!metadataToken2.IsMemberRef)
					{
						throw new ArgumentException("metadataToken", Environment.GetResourceString("Argument_ResolveField", new object[]
						{
							metadataToken2,
							this
						}));
					}
					if (*(byte*)this.MetadataImport.GetMemberRefProps(metadataToken2).Signature.ToPointer() != 6)
					{
						throw new ArgumentException("metadataToken", Environment.GetResourceString("Argument_ResolveField", new object[]
						{
							metadataToken2,
							this
						}));
					}
					runtimeFieldInfo = ModuleHandle.ResolveFieldHandleInternal(this.GetNativeHandle(), metadataToken2, typeInstantiationContext, methodInstantiationContext);
				}
				runtimeFieldInfo = ModuleHandle.ResolveFieldHandleInternal(this.GetNativeHandle(), metadataToken, typeInstantiationContext, methodInstantiationContext);
				RuntimeType runtimeType = RuntimeFieldHandle.GetApproxDeclaringType(runtimeFieldInfo.Value);
				if (runtimeType.IsGenericType || runtimeType.IsArray)
				{
					int parentToken = ModuleHandle.GetMetadataImport(this.GetNativeHandle()).GetParentToken(metadataToken);
					runtimeType = (RuntimeType)this.ResolveType(parentToken, genericTypeArguments, genericMethodArguments);
				}
				result = RuntimeType.GetFieldInfo(runtimeType, runtimeFieldInfo);
			}
			catch (MissingFieldException)
			{
				result = this.ResolveLiteralField(metadataToken2, genericTypeArguments, genericMethodArguments);
			}
			catch (BadImageFormatException innerException)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_BadImageFormatExceptionResolve"), innerException);
			}
			return result;
		}

		// Token: 0x060046AF RID: 18095 RVA: 0x00100C5C File Offset: 0x000FEE5C
		[SecuritySafeCritical]
		public override Type ResolveType(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			MetadataToken metadataToken2 = new MetadataToken(metadataToken);
			if (metadataToken2.IsGlobalTypeDefToken)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ResolveModuleType", new object[]
				{
					metadataToken2
				}), "metadataToken");
			}
			if (!this.MetadataImport.IsValidToken(metadataToken2))
			{
				throw new ArgumentOutOfRangeException("metadataToken", Environment.GetResourceString("Argument_InvalidToken", new object[]
				{
					metadataToken2,
					this
				}));
			}
			if (!metadataToken2.IsTypeDef && !metadataToken2.IsTypeSpec && !metadataToken2.IsTypeRef)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ResolveType", new object[]
				{
					metadataToken2,
					this
				}), "metadataToken");
			}
			RuntimeTypeHandle[] typeInstantiationContext = RuntimeModule.ConvertToTypeHandleArray(genericTypeArguments);
			RuntimeTypeHandle[] methodInstantiationContext = RuntimeModule.ConvertToTypeHandleArray(genericMethodArguments);
			Type result;
			try
			{
				Type runtimeType = this.GetModuleHandle().ResolveTypeHandle(metadataToken, typeInstantiationContext, methodInstantiationContext).GetRuntimeType();
				if (runtimeType == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_ResolveType", new object[]
					{
						metadataToken2,
						this
					}), "metadataToken");
				}
				result = runtimeType;
			}
			catch (BadImageFormatException innerException)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_BadImageFormatExceptionResolve"), innerException);
			}
			return result;
		}

		// Token: 0x060046B0 RID: 18096 RVA: 0x00100DA8 File Offset: 0x000FEFA8
		[SecuritySafeCritical]
		public unsafe override MemberInfo ResolveMember(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			MetadataToken metadataToken2 = new MetadataToken(metadataToken);
			if (metadataToken2.IsProperty)
			{
				throw new ArgumentException(Environment.GetResourceString("InvalidOperation_PropertyInfoNotAvailable"));
			}
			if (metadataToken2.IsEvent)
			{
				throw new ArgumentException(Environment.GetResourceString("InvalidOperation_EventInfoNotAvailable"));
			}
			if (metadataToken2.IsMethodSpec || metadataToken2.IsMethodDef)
			{
				return this.ResolveMethod(metadataToken, genericTypeArguments, genericMethodArguments);
			}
			if (metadataToken2.IsFieldDef)
			{
				return this.ResolveField(metadataToken, genericTypeArguments, genericMethodArguments);
			}
			if (metadataToken2.IsTypeRef || metadataToken2.IsTypeDef || metadataToken2.IsTypeSpec)
			{
				return this.ResolveType(metadataToken, genericTypeArguments, genericMethodArguments);
			}
			if (!metadataToken2.IsMemberRef)
			{
				throw new ArgumentException("metadataToken", Environment.GetResourceString("Argument_ResolveMember", new object[]
				{
					metadataToken2,
					this
				}));
			}
			if (!this.MetadataImport.IsValidToken(metadataToken2))
			{
				throw new ArgumentOutOfRangeException("metadataToken", Environment.GetResourceString("Argument_InvalidToken", new object[]
				{
					metadataToken2,
					this
				}));
			}
			if (*(byte*)this.MetadataImport.GetMemberRefProps(metadataToken2).Signature.ToPointer() == 6)
			{
				return this.ResolveField(metadataToken2, genericTypeArguments, genericMethodArguments);
			}
			return this.ResolveMethod(metadataToken2, genericTypeArguments, genericMethodArguments);
		}

		// Token: 0x060046B1 RID: 18097 RVA: 0x00100EFC File Offset: 0x000FF0FC
		[SecuritySafeCritical]
		public override string ResolveString(int metadataToken)
		{
			MetadataToken metadataToken2 = new MetadataToken(metadataToken);
			if (!metadataToken2.IsString)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString("Argument_ResolveString"), metadataToken, this.ToString()));
			}
			if (!this.MetadataImport.IsValidToken(metadataToken2))
			{
				throw new ArgumentOutOfRangeException("metadataToken", string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString("Argument_InvalidToken", new object[]
				{
					metadataToken2,
					this
				}), Array.Empty<object>()));
			}
			string userString = this.MetadataImport.GetUserString(metadataToken);
			if (userString == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString("Argument_ResolveString"), metadataToken, this.ToString()));
			}
			return userString;
		}

		// Token: 0x060046B2 RID: 18098 RVA: 0x00100FC7 File Offset: 0x000FF1C7
		public override void GetPEKind(out PortableExecutableKinds peKind, out ImageFileMachine machine)
		{
			ModuleHandle.GetPEKind(this.GetNativeHandle(), out peKind, out machine);
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x060046B3 RID: 18099 RVA: 0x00100FD6 File Offset: 0x000FF1D6
		public override int MDStreamVersion
		{
			[SecuritySafeCritical]
			get
			{
				return ModuleHandle.GetMDStreamVersion(this.GetNativeHandle());
			}
		}

		// Token: 0x060046B4 RID: 18100 RVA: 0x00100FE3 File Offset: 0x000FF1E3
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetMethodInternal(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x060046B5 RID: 18101 RVA: 0x00100FF4 File Offset: 0x000FF1F4
		internal MethodInfo GetMethodInternal(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (this.RuntimeType == null)
			{
				return null;
			}
			if (types == null)
			{
				return this.RuntimeType.GetMethod(name, bindingAttr);
			}
			return this.RuntimeType.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x060046B6 RID: 18102 RVA: 0x0010102C File Offset: 0x000FF22C
		internal RuntimeType RuntimeType
		{
			get
			{
				if (this.m_runtimeType == null)
				{
					this.m_runtimeType = ModuleHandle.GetModuleType(this.GetNativeHandle());
				}
				return this.m_runtimeType;
			}
		}

		// Token: 0x060046B7 RID: 18103 RVA: 0x00101053 File Offset: 0x000FF253
		[SecuritySafeCritical]
		internal bool IsTransientInternal()
		{
			return RuntimeModule.nIsTransientInternal(this.GetNativeHandle());
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x060046B8 RID: 18104 RVA: 0x00101060 File Offset: 0x000FF260
		internal MetadataImport MetadataImport
		{
			[SecurityCritical]
			get
			{
				return ModuleHandle.GetMetadataImport(this.GetNativeHandle());
			}
		}

		// Token: 0x060046B9 RID: 18105 RVA: 0x0010106D File Offset: 0x000FF26D
		public override object[] GetCustomAttributes(bool inherit)
		{
			return CustomAttribute.GetCustomAttributes(this, typeof(object) as RuntimeType);
		}

		// Token: 0x060046BA RID: 18106 RVA: 0x00101084 File Offset: 0x000FF284
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "attributeType");
			}
			return CustomAttribute.GetCustomAttributes(this, runtimeType);
		}

		// Token: 0x060046BB RID: 18107 RVA: 0x001010D8 File Offset: 0x000FF2D8
		[SecuritySafeCritical]
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), "attributeType");
			}
			return CustomAttribute.IsDefined(this, runtimeType);
		}

		// Token: 0x060046BC RID: 18108 RVA: 0x0010112A File Offset: 0x000FF32A
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributesInternal(this);
		}

		// Token: 0x060046BD RID: 18109 RVA: 0x00101132 File Offset: 0x000FF332
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			UnitySerializationHolder.GetUnitySerializationInfo(info, 5, this.ScopeName, this.GetRuntimeAssembly());
		}

		// Token: 0x060046BE RID: 18110 RVA: 0x00101158 File Offset: 0x000FF358
		[SecuritySafeCritical]
		[ComVisible(true)]
		public override Type GetType(string className, bool throwOnError, bool ignoreCase)
		{
			if (className == null)
			{
				throw new ArgumentNullException("className");
			}
			RuntimeType result = null;
			RuntimeModule.GetType(this.GetNativeHandle(), className, throwOnError, ignoreCase, JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref result));
			return result;
		}

		// Token: 0x060046BF RID: 18111 RVA: 0x0010118C File Offset: 0x000FF38C
		[SecurityCritical]
		internal string GetFullyQualifiedName()
		{
			string result = null;
			RuntimeModule.GetFullyQualifiedName(this.GetNativeHandle(), JitHelpers.GetStringHandleOnStack(ref result));
			return result;
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x060046C0 RID: 18112 RVA: 0x001011B0 File Offset: 0x000FF3B0
		public override string FullyQualifiedName
		{
			[SecuritySafeCritical]
			get
			{
				string fullyQualifiedName = this.GetFullyQualifiedName();
				if (fullyQualifiedName != null)
				{
					bool flag = true;
					try
					{
						Path.GetFullPathInternal(fullyQualifiedName);
					}
					catch (ArgumentException)
					{
						flag = false;
					}
					if (flag)
					{
						new FileIOPermission(FileIOPermissionAccess.PathDiscovery, fullyQualifiedName).Demand();
					}
				}
				return fullyQualifiedName;
			}
		}

		// Token: 0x060046C1 RID: 18113 RVA: 0x001011F8 File Offset: 0x000FF3F8
		[SecuritySafeCritical]
		public override Type[] GetTypes()
		{
			return RuntimeModule.GetTypes(this.GetNativeHandle());
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x060046C2 RID: 18114 RVA: 0x00101208 File Offset: 0x000FF408
		public override Guid ModuleVersionId
		{
			[SecuritySafeCritical]
			get
			{
				Guid result;
				this.MetadataImport.GetScopeProps(out result);
				return result;
			}
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x060046C3 RID: 18115 RVA: 0x00101226 File Offset: 0x000FF426
		public override int MetadataToken
		{
			[SecuritySafeCritical]
			get
			{
				return ModuleHandle.GetToken(this.GetNativeHandle());
			}
		}

		// Token: 0x060046C4 RID: 18116 RVA: 0x00101233 File Offset: 0x000FF433
		public override bool IsResource()
		{
			return RuntimeModule.IsResource(this.GetNativeHandle());
		}

		// Token: 0x060046C5 RID: 18117 RVA: 0x00101240 File Offset: 0x000FF440
		public override FieldInfo[] GetFields(BindingFlags bindingFlags)
		{
			if (this.RuntimeType == null)
			{
				return new FieldInfo[0];
			}
			return this.RuntimeType.GetFields(bindingFlags);
		}

		// Token: 0x060046C6 RID: 18118 RVA: 0x00101263 File Offset: 0x000FF463
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this.RuntimeType == null)
			{
				return null;
			}
			return this.RuntimeType.GetField(name, bindingAttr);
		}

		// Token: 0x060046C7 RID: 18119 RVA: 0x00101290 File Offset: 0x000FF490
		public override MethodInfo[] GetMethods(BindingFlags bindingFlags)
		{
			if (this.RuntimeType == null)
			{
				return new MethodInfo[0];
			}
			return this.RuntimeType.GetMethods(bindingFlags);
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x060046C8 RID: 18120 RVA: 0x001012B4 File Offset: 0x000FF4B4
		public override string ScopeName
		{
			[SecuritySafeCritical]
			get
			{
				string result = null;
				RuntimeModule.GetScopeName(this.GetNativeHandle(), JitHelpers.GetStringHandleOnStack(ref result));
				return result;
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x060046C9 RID: 18121 RVA: 0x001012D8 File Offset: 0x000FF4D8
		public override string Name
		{
			[SecuritySafeCritical]
			get
			{
				string fullyQualifiedName = this.GetFullyQualifiedName();
				int num = fullyQualifiedName.LastIndexOf('\\');
				if (num == -1)
				{
					return fullyQualifiedName;
				}
				return new string(fullyQualifiedName.ToCharArray(), num + 1, fullyQualifiedName.Length - num - 1);
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x060046CA RID: 18122 RVA: 0x00101313 File Offset: 0x000FF513
		public override Assembly Assembly
		{
			get
			{
				return this.GetRuntimeAssembly();
			}
		}

		// Token: 0x060046CB RID: 18123 RVA: 0x0010131B File Offset: 0x000FF51B
		internal RuntimeAssembly GetRuntimeAssembly()
		{
			return this.m_runtimeAssembly;
		}

		// Token: 0x060046CC RID: 18124 RVA: 0x00101323 File Offset: 0x000FF523
		internal override ModuleHandle GetModuleHandle()
		{
			return new ModuleHandle(this);
		}

		// Token: 0x060046CD RID: 18125 RVA: 0x0010132B File Offset: 0x000FF52B
		internal RuntimeModule GetNativeHandle()
		{
			return this;
		}

		// Token: 0x060046CE RID: 18126 RVA: 0x00101330 File Offset: 0x000FF530
		[SecuritySafeCritical]
		public override X509Certificate GetSignerCertificate()
		{
			byte[] array = null;
			RuntimeModule.GetSignerCertificate(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<byte[]>(ref array));
			if (array == null)
			{
				return null;
			}
			return new X509Certificate(array);
		}

		// Token: 0x04001D02 RID: 7426
		private RuntimeType m_runtimeType;

		// Token: 0x04001D03 RID: 7427
		private RuntimeAssembly m_runtimeAssembly;

		// Token: 0x04001D04 RID: 7428
		private IntPtr m_pRefClass;

		// Token: 0x04001D05 RID: 7429
		private IntPtr m_pData;

		// Token: 0x04001D06 RID: 7430
		private IntPtr m_pGlobals;

		// Token: 0x04001D07 RID: 7431
		private IntPtr m_pFields;
	}
}
