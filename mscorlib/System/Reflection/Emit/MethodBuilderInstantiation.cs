using System;
using System.Globalization;

namespace System.Reflection.Emit
{
	// Token: 0x0200061C RID: 1564
	internal sealed class MethodBuilderInstantiation : MethodInfo
	{
		// Token: 0x06004A62 RID: 19042 RVA: 0x0010CFA0 File Offset: 0x0010B1A0
		internal static MethodInfo MakeGenericMethod(MethodInfo method, Type[] inst)
		{
			if (!method.IsGenericMethodDefinition)
			{
				throw new InvalidOperationException();
			}
			return new MethodBuilderInstantiation(method, inst);
		}

		// Token: 0x06004A63 RID: 19043 RVA: 0x0010CFB7 File Offset: 0x0010B1B7
		internal MethodBuilderInstantiation(MethodInfo method, Type[] inst)
		{
			this.m_method = method;
			this.m_inst = inst;
		}

		// Token: 0x06004A64 RID: 19044 RVA: 0x0010CFCD File Offset: 0x0010B1CD
		internal override Type[] GetParameterTypes()
		{
			return this.m_method.GetParameterTypes();
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x06004A65 RID: 19045 RVA: 0x0010CFDA File Offset: 0x0010B1DA
		public override MemberTypes MemberType
		{
			get
			{
				return this.m_method.MemberType;
			}
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x06004A66 RID: 19046 RVA: 0x0010CFE7 File Offset: 0x0010B1E7
		public override string Name
		{
			get
			{
				return this.m_method.Name;
			}
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06004A67 RID: 19047 RVA: 0x0010CFF4 File Offset: 0x0010B1F4
		public override Type DeclaringType
		{
			get
			{
				return this.m_method.DeclaringType;
			}
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06004A68 RID: 19048 RVA: 0x0010D001 File Offset: 0x0010B201
		public override Type ReflectedType
		{
			get
			{
				return this.m_method.ReflectedType;
			}
		}

		// Token: 0x06004A69 RID: 19049 RVA: 0x0010D00E File Offset: 0x0010B20E
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_method.GetCustomAttributes(inherit);
		}

		// Token: 0x06004A6A RID: 19050 RVA: 0x0010D01C File Offset: 0x0010B21C
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_method.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06004A6B RID: 19051 RVA: 0x0010D02B File Offset: 0x0010B22B
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_method.IsDefined(attributeType, inherit);
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06004A6C RID: 19052 RVA: 0x0010D03A File Offset: 0x0010B23A
		public override Module Module
		{
			get
			{
				return this.m_method.Module;
			}
		}

		// Token: 0x06004A6D RID: 19053 RVA: 0x0010D047 File Offset: 0x0010B247
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x06004A6E RID: 19054 RVA: 0x0010D04F File Offset: 0x0010B24F
		public override ParameterInfo[] GetParameters()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004A6F RID: 19055 RVA: 0x0010D056 File Offset: 0x0010B256
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.m_method.GetMethodImplementationFlags();
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06004A70 RID: 19056 RVA: 0x0010D063 File Offset: 0x0010B263
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
			}
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06004A71 RID: 19057 RVA: 0x0010D074 File Offset: 0x0010B274
		public override MethodAttributes Attributes
		{
			get
			{
				return this.m_method.Attributes;
			}
		}

		// Token: 0x06004A72 RID: 19058 RVA: 0x0010D081 File Offset: 0x0010B281
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06004A73 RID: 19059 RVA: 0x0010D088 File Offset: 0x0010B288
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.m_method.CallingConvention;
			}
		}

		// Token: 0x06004A74 RID: 19060 RVA: 0x0010D095 File Offset: 0x0010B295
		public override Type[] GetGenericArguments()
		{
			return this.m_inst;
		}

		// Token: 0x06004A75 RID: 19061 RVA: 0x0010D09D File Offset: 0x0010B29D
		public override MethodInfo GetGenericMethodDefinition()
		{
			return this.m_method;
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06004A76 RID: 19062 RVA: 0x0010D0A5 File Offset: 0x0010B2A5
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x06004A77 RID: 19063 RVA: 0x0010D0A8 File Offset: 0x0010B2A8
		public override bool ContainsGenericParameters
		{
			get
			{
				for (int i = 0; i < this.m_inst.Length; i++)
				{
					if (this.m_inst[i].ContainsGenericParameters)
					{
						return true;
					}
				}
				return this.DeclaringType != null && this.DeclaringType.ContainsGenericParameters;
			}
		}

		// Token: 0x06004A78 RID: 19064 RVA: 0x0010D0F7 File Offset: 0x0010B2F7
		public override MethodInfo MakeGenericMethod(params Type[] arguments)
		{
			throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericMethodDefinition"));
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06004A79 RID: 19065 RVA: 0x0010D108 File Offset: 0x0010B308
		public override bool IsGenericMethod
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x06004A7A RID: 19066 RVA: 0x0010D10B File Offset: 0x0010B30B
		public override Type ReturnType
		{
			get
			{
				return this.m_method.ReturnType;
			}
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x06004A7B RID: 19067 RVA: 0x0010D118 File Offset: 0x0010B318
		public override ParameterInfo ReturnParameter
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x06004A7C RID: 19068 RVA: 0x0010D11F File Offset: 0x0010B31F
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06004A7D RID: 19069 RVA: 0x0010D126 File Offset: 0x0010B326
		public override MethodInfo GetBaseDefinition()
		{
			throw new NotSupportedException();
		}

		// Token: 0x04001E7E RID: 7806
		internal MethodInfo m_method;

		// Token: 0x04001E7F RID: 7807
		private Type[] m_inst;
	}
}
