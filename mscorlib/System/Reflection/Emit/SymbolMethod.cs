using System;
using System.Globalization;
using System.Security;

namespace System.Reflection.Emit
{
	// Token: 0x0200061F RID: 1567
	internal sealed class SymbolMethod : MethodInfo
	{
		// Token: 0x06004AB5 RID: 19125 RVA: 0x0010D87C File Offset: 0x0010BA7C
		[SecurityCritical]
		internal SymbolMethod(ModuleBuilder mod, MethodToken token, Type arrayClass, string methodName, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			this.m_mdMethod = token;
			this.m_returnType = returnType;
			if (parameterTypes != null)
			{
				this.m_parameterTypes = new Type[parameterTypes.Length];
				Array.Copy(parameterTypes, this.m_parameterTypes, parameterTypes.Length);
			}
			else
			{
				this.m_parameterTypes = EmptyArray<Type>.Value;
			}
			this.m_module = mod;
			this.m_containingType = arrayClass;
			this.m_name = methodName;
			this.m_callingConvention = callingConvention;
			this.m_signature = SignatureHelper.GetMethodSigHelper(mod, callingConvention, returnType, null, null, parameterTypes, null, null);
		}

		// Token: 0x06004AB6 RID: 19126 RVA: 0x0010D903 File Offset: 0x0010BB03
		internal override Type[] GetParameterTypes()
		{
			return this.m_parameterTypes;
		}

		// Token: 0x06004AB7 RID: 19127 RVA: 0x0010D90B File Offset: 0x0010BB0B
		internal MethodToken GetToken(ModuleBuilder mod)
		{
			return mod.GetArrayMethodToken(this.m_containingType, this.m_name, this.m_callingConvention, this.m_returnType, this.m_parameterTypes);
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x06004AB8 RID: 19128 RVA: 0x0010D931 File Offset: 0x0010BB31
		public override Module Module
		{
			get
			{
				return this.m_module;
			}
		}

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x06004AB9 RID: 19129 RVA: 0x0010D939 File Offset: 0x0010BB39
		public override Type ReflectedType
		{
			get
			{
				return this.m_containingType;
			}
		}

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x06004ABA RID: 19130 RVA: 0x0010D941 File Offset: 0x0010BB41
		public override string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x06004ABB RID: 19131 RVA: 0x0010D949 File Offset: 0x0010BB49
		public override Type DeclaringType
		{
			get
			{
				return this.m_containingType;
			}
		}

		// Token: 0x06004ABC RID: 19132 RVA: 0x0010D951 File Offset: 0x0010BB51
		public override ParameterInfo[] GetParameters()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
		}

		// Token: 0x06004ABD RID: 19133 RVA: 0x0010D962 File Offset: 0x0010BB62
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06004ABE RID: 19134 RVA: 0x0010D973 File Offset: 0x0010BB73
		public override MethodAttributes Attributes
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
			}
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x06004ABF RID: 19135 RVA: 0x0010D984 File Offset: 0x0010BB84
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.m_callingConvention;
			}
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x06004AC0 RID: 19136 RVA: 0x0010D98C File Offset: 0x0010BB8C
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
			}
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06004AC1 RID: 19137 RVA: 0x0010D99D File Offset: 0x0010BB9D
		public override Type ReturnType
		{
			get
			{
				return this.m_returnType;
			}
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x06004AC2 RID: 19138 RVA: 0x0010D9A5 File Offset: 0x0010BBA5
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004AC3 RID: 19139 RVA: 0x0010D9A8 File Offset: 0x0010BBA8
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
		}

		// Token: 0x06004AC4 RID: 19140 RVA: 0x0010D9B9 File Offset: 0x0010BBB9
		public override MethodInfo GetBaseDefinition()
		{
			return this;
		}

		// Token: 0x06004AC5 RID: 19141 RVA: 0x0010D9BC File Offset: 0x0010BBBC
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
		}

		// Token: 0x06004AC6 RID: 19142 RVA: 0x0010D9CD File Offset: 0x0010BBCD
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
		}

		// Token: 0x06004AC7 RID: 19143 RVA: 0x0010D9DE File Offset: 0x0010BBDE
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_SymbolMethod"));
		}

		// Token: 0x06004AC8 RID: 19144 RVA: 0x0010D9EF File Offset: 0x0010BBEF
		public Module GetModule()
		{
			return this.m_module;
		}

		// Token: 0x06004AC9 RID: 19145 RVA: 0x0010D9F7 File Offset: 0x0010BBF7
		public MethodToken GetToken()
		{
			return this.m_mdMethod;
		}

		// Token: 0x04001E8B RID: 7819
		private ModuleBuilder m_module;

		// Token: 0x04001E8C RID: 7820
		private Type m_containingType;

		// Token: 0x04001E8D RID: 7821
		private string m_name;

		// Token: 0x04001E8E RID: 7822
		private CallingConventions m_callingConvention;

		// Token: 0x04001E8F RID: 7823
		private Type m_returnType;

		// Token: 0x04001E90 RID: 7824
		private MethodToken m_mdMethod;

		// Token: 0x04001E91 RID: 7825
		private Type[] m_parameterTypes;

		// Token: 0x04001E92 RID: 7826
		private SignatureHelper m_signature;
	}
}
