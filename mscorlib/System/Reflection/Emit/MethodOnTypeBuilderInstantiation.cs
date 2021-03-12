using System;
using System.Globalization;

namespace System.Reflection.Emit
{
	// Token: 0x0200063B RID: 1595
	internal sealed class MethodOnTypeBuilderInstantiation : MethodInfo
	{
		// Token: 0x06004D9E RID: 19870 RVA: 0x0011722A File Offset: 0x0011542A
		internal static MethodInfo GetMethod(MethodInfo method, TypeBuilderInstantiation type)
		{
			return new MethodOnTypeBuilderInstantiation(method, type);
		}

		// Token: 0x06004D9F RID: 19871 RVA: 0x00117233 File Offset: 0x00115433
		internal MethodOnTypeBuilderInstantiation(MethodInfo method, TypeBuilderInstantiation type)
		{
			this.m_method = method;
			this.m_type = type;
		}

		// Token: 0x06004DA0 RID: 19872 RVA: 0x00117249 File Offset: 0x00115449
		internal override Type[] GetParameterTypes()
		{
			return this.m_method.GetParameterTypes();
		}

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06004DA1 RID: 19873 RVA: 0x00117256 File Offset: 0x00115456
		public override MemberTypes MemberType
		{
			get
			{
				return this.m_method.MemberType;
			}
		}

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x06004DA2 RID: 19874 RVA: 0x00117263 File Offset: 0x00115463
		public override string Name
		{
			get
			{
				return this.m_method.Name;
			}
		}

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06004DA3 RID: 19875 RVA: 0x00117270 File Offset: 0x00115470
		public override Type DeclaringType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x06004DA4 RID: 19876 RVA: 0x00117278 File Offset: 0x00115478
		public override Type ReflectedType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x06004DA5 RID: 19877 RVA: 0x00117280 File Offset: 0x00115480
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_method.GetCustomAttributes(inherit);
		}

		// Token: 0x06004DA6 RID: 19878 RVA: 0x0011728E File Offset: 0x0011548E
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_method.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06004DA7 RID: 19879 RVA: 0x0011729D File Offset: 0x0011549D
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_method.IsDefined(attributeType, inherit);
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x06004DA8 RID: 19880 RVA: 0x001172AC File Offset: 0x001154AC
		internal int MetadataTokenInternal
		{
			get
			{
				MethodBuilder methodBuilder = this.m_method as MethodBuilder;
				if (methodBuilder != null)
				{
					return methodBuilder.MetadataTokenInternal;
				}
				return this.m_method.MetadataToken;
			}
		}

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x06004DA9 RID: 19881 RVA: 0x001172E0 File Offset: 0x001154E0
		public override Module Module
		{
			get
			{
				return this.m_method.Module;
			}
		}

		// Token: 0x06004DAA RID: 19882 RVA: 0x001172ED File Offset: 0x001154ED
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x06004DAB RID: 19883 RVA: 0x001172F5 File Offset: 0x001154F5
		public override ParameterInfo[] GetParameters()
		{
			return this.m_method.GetParameters();
		}

		// Token: 0x06004DAC RID: 19884 RVA: 0x00117302 File Offset: 0x00115502
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.m_method.GetMethodImplementationFlags();
		}

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x06004DAD RID: 19885 RVA: 0x0011730F File Offset: 0x0011550F
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this.m_method.MethodHandle;
			}
		}

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x06004DAE RID: 19886 RVA: 0x0011731C File Offset: 0x0011551C
		public override MethodAttributes Attributes
		{
			get
			{
				return this.m_method.Attributes;
			}
		}

		// Token: 0x06004DAF RID: 19887 RVA: 0x00117329 File Offset: 0x00115529
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x06004DB0 RID: 19888 RVA: 0x00117330 File Offset: 0x00115530
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.m_method.CallingConvention;
			}
		}

		// Token: 0x06004DB1 RID: 19889 RVA: 0x0011733D File Offset: 0x0011553D
		public override Type[] GetGenericArguments()
		{
			return this.m_method.GetGenericArguments();
		}

		// Token: 0x06004DB2 RID: 19890 RVA: 0x0011734A File Offset: 0x0011554A
		public override MethodInfo GetGenericMethodDefinition()
		{
			return this.m_method;
		}

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x06004DB3 RID: 19891 RVA: 0x00117352 File Offset: 0x00115552
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return this.m_method.IsGenericMethodDefinition;
			}
		}

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x06004DB4 RID: 19892 RVA: 0x0011735F File Offset: 0x0011555F
		public override bool ContainsGenericParameters
		{
			get
			{
				return this.m_method.ContainsGenericParameters;
			}
		}

		// Token: 0x06004DB5 RID: 19893 RVA: 0x0011736C File Offset: 0x0011556C
		public override MethodInfo MakeGenericMethod(params Type[] typeArgs)
		{
			if (!this.IsGenericMethodDefinition)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericMethodDefinition"));
			}
			return MethodBuilderInstantiation.MakeGenericMethod(this, typeArgs);
		}

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x06004DB6 RID: 19894 RVA: 0x0011738D File Offset: 0x0011558D
		public override bool IsGenericMethod
		{
			get
			{
				return this.m_method.IsGenericMethod;
			}
		}

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x06004DB7 RID: 19895 RVA: 0x0011739A File Offset: 0x0011559A
		public override Type ReturnType
		{
			get
			{
				return this.m_method.ReturnType;
			}
		}

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x06004DB8 RID: 19896 RVA: 0x001173A7 File Offset: 0x001155A7
		public override ParameterInfo ReturnParameter
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x06004DB9 RID: 19897 RVA: 0x001173AE File Offset: 0x001155AE
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06004DBA RID: 19898 RVA: 0x001173B5 File Offset: 0x001155B5
		public override MethodInfo GetBaseDefinition()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0400211A RID: 8474
		internal MethodInfo m_method;

		// Token: 0x0400211B RID: 8475
		private TypeBuilderInstantiation m_type;
	}
}
