using System;
using System.Globalization;

namespace System.Reflection.Emit
{
	// Token: 0x0200063C RID: 1596
	internal sealed class ConstructorOnTypeBuilderInstantiation : ConstructorInfo
	{
		// Token: 0x06004DBB RID: 19899 RVA: 0x001173BC File Offset: 0x001155BC
		internal static ConstructorInfo GetConstructor(ConstructorInfo Constructor, TypeBuilderInstantiation type)
		{
			return new ConstructorOnTypeBuilderInstantiation(Constructor, type);
		}

		// Token: 0x06004DBC RID: 19900 RVA: 0x001173C5 File Offset: 0x001155C5
		internal ConstructorOnTypeBuilderInstantiation(ConstructorInfo constructor, TypeBuilderInstantiation type)
		{
			this.m_ctor = constructor;
			this.m_type = type;
		}

		// Token: 0x06004DBD RID: 19901 RVA: 0x001173DB File Offset: 0x001155DB
		internal override Type[] GetParameterTypes()
		{
			return this.m_ctor.GetParameterTypes();
		}

		// Token: 0x06004DBE RID: 19902 RVA: 0x001173E8 File Offset: 0x001155E8
		internal override Type GetReturnType()
		{
			return this.DeclaringType;
		}

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x06004DBF RID: 19903 RVA: 0x001173F0 File Offset: 0x001155F0
		public override MemberTypes MemberType
		{
			get
			{
				return this.m_ctor.MemberType;
			}
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x06004DC0 RID: 19904 RVA: 0x001173FD File Offset: 0x001155FD
		public override string Name
		{
			get
			{
				return this.m_ctor.Name;
			}
		}

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x06004DC1 RID: 19905 RVA: 0x0011740A File Offset: 0x0011560A
		public override Type DeclaringType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x06004DC2 RID: 19906 RVA: 0x00117412 File Offset: 0x00115612
		public override Type ReflectedType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x06004DC3 RID: 19907 RVA: 0x0011741A File Offset: 0x0011561A
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_ctor.GetCustomAttributes(inherit);
		}

		// Token: 0x06004DC4 RID: 19908 RVA: 0x00117428 File Offset: 0x00115628
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_ctor.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06004DC5 RID: 19909 RVA: 0x00117437 File Offset: 0x00115637
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_ctor.IsDefined(attributeType, inherit);
		}

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x06004DC6 RID: 19910 RVA: 0x00117448 File Offset: 0x00115648
		internal int MetadataTokenInternal
		{
			get
			{
				ConstructorBuilder constructorBuilder = this.m_ctor as ConstructorBuilder;
				if (constructorBuilder != null)
				{
					return constructorBuilder.MetadataTokenInternal;
				}
				return this.m_ctor.MetadataToken;
			}
		}

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x06004DC7 RID: 19911 RVA: 0x0011747C File Offset: 0x0011567C
		public override Module Module
		{
			get
			{
				return this.m_ctor.Module;
			}
		}

		// Token: 0x06004DC8 RID: 19912 RVA: 0x00117489 File Offset: 0x00115689
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x06004DC9 RID: 19913 RVA: 0x00117491 File Offset: 0x00115691
		public override ParameterInfo[] GetParameters()
		{
			return this.m_ctor.GetParameters();
		}

		// Token: 0x06004DCA RID: 19914 RVA: 0x0011749E File Offset: 0x0011569E
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.m_ctor.GetMethodImplementationFlags();
		}

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x06004DCB RID: 19915 RVA: 0x001174AB File Offset: 0x001156AB
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this.m_ctor.MethodHandle;
			}
		}

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x06004DCC RID: 19916 RVA: 0x001174B8 File Offset: 0x001156B8
		public override MethodAttributes Attributes
		{
			get
			{
				return this.m_ctor.Attributes;
			}
		}

		// Token: 0x06004DCD RID: 19917 RVA: 0x001174C5 File Offset: 0x001156C5
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x06004DCE RID: 19918 RVA: 0x001174CC File Offset: 0x001156CC
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.m_ctor.CallingConvention;
			}
		}

		// Token: 0x06004DCF RID: 19919 RVA: 0x001174D9 File Offset: 0x001156D9
		public override Type[] GetGenericArguments()
		{
			return this.m_ctor.GetGenericArguments();
		}

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x06004DD0 RID: 19920 RVA: 0x001174E6 File Offset: 0x001156E6
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x06004DD1 RID: 19921 RVA: 0x001174E9 File Offset: 0x001156E9
		public override bool ContainsGenericParameters
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x06004DD2 RID: 19922 RVA: 0x001174EC File Offset: 0x001156EC
		public override bool IsGenericMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004DD3 RID: 19923 RVA: 0x001174EF File Offset: 0x001156EF
		public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0400211C RID: 8476
		internal ConstructorInfo m_ctor;

		// Token: 0x0400211D RID: 8477
		private TypeBuilderInstantiation m_type;
	}
}
