using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
	// Token: 0x02000630 RID: 1584
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_PropertyBuilder))]
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class PropertyBuilder : PropertyInfo, _PropertyBuilder
	{
		// Token: 0x06004BB1 RID: 19377 RVA: 0x00112575 File Offset: 0x00110775
		private PropertyBuilder()
		{
		}

		// Token: 0x06004BB2 RID: 19378 RVA: 0x00112580 File Offset: 0x00110780
		internal PropertyBuilder(ModuleBuilder mod, string name, SignatureHelper sig, PropertyAttributes attr, Type returnType, PropertyToken prToken, TypeBuilder containingType)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "name");
			}
			if (name[0] == '\0')
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IllegalName"), "name");
			}
			this.m_name = name;
			this.m_moduleBuilder = mod;
			this.m_signature = sig;
			this.m_attributes = attr;
			this.m_returnType = returnType;
			this.m_prToken = prToken;
			this.m_tkProperty = prToken.Token;
			this.m_containingType = containingType;
		}

		// Token: 0x06004BB3 RID: 19379 RVA: 0x0011261E File Offset: 0x0011081E
		[SecuritySafeCritical]
		public void SetConstant(object defaultValue)
		{
			this.m_containingType.ThrowIfCreated();
			TypeBuilder.SetConstantValue(this.m_moduleBuilder, this.m_prToken.Token, this.m_returnType, defaultValue);
		}

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x06004BB4 RID: 19380 RVA: 0x00112648 File Offset: 0x00110848
		public PropertyToken PropertyToken
		{
			get
			{
				return this.m_prToken;
			}
		}

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x06004BB5 RID: 19381 RVA: 0x00112650 File Offset: 0x00110850
		internal int MetadataTokenInternal
		{
			get
			{
				return this.m_tkProperty;
			}
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x06004BB6 RID: 19382 RVA: 0x00112658 File Offset: 0x00110858
		public override Module Module
		{
			get
			{
				return this.m_containingType.Module;
			}
		}

		// Token: 0x06004BB7 RID: 19383 RVA: 0x00112668 File Offset: 0x00110868
		[SecurityCritical]
		private void SetMethodSemantics(MethodBuilder mdBuilder, MethodSemanticsAttributes semantics)
		{
			if (mdBuilder == null)
			{
				throw new ArgumentNullException("mdBuilder");
			}
			this.m_containingType.ThrowIfCreated();
			TypeBuilder.DefineMethodSemantics(this.m_moduleBuilder.GetNativeHandle(), this.m_prToken.Token, semantics, mdBuilder.GetToken().Token);
		}

		// Token: 0x06004BB8 RID: 19384 RVA: 0x001126BE File Offset: 0x001108BE
		[SecuritySafeCritical]
		public void SetGetMethod(MethodBuilder mdBuilder)
		{
			this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.Getter);
			this.m_getMethod = mdBuilder;
		}

		// Token: 0x06004BB9 RID: 19385 RVA: 0x001126CF File Offset: 0x001108CF
		[SecuritySafeCritical]
		public void SetSetMethod(MethodBuilder mdBuilder)
		{
			this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.Setter);
			this.m_setMethod = mdBuilder;
		}

		// Token: 0x06004BBA RID: 19386 RVA: 0x001126E0 File Offset: 0x001108E0
		[SecuritySafeCritical]
		public void AddOtherMethod(MethodBuilder mdBuilder)
		{
			this.SetMethodSemantics(mdBuilder, MethodSemanticsAttributes.Other);
		}

		// Token: 0x06004BBB RID: 19387 RVA: 0x001126EC File Offset: 0x001108EC
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
			this.m_containingType.ThrowIfCreated();
			TypeBuilder.DefineCustomAttribute(this.m_moduleBuilder, this.m_prToken.Token, this.m_moduleBuilder.GetConstructorToken(con).Token, binaryAttribute, false, false);
		}

		// Token: 0x06004BBC RID: 19388 RVA: 0x00112753 File Offset: 0x00110953
		[SecuritySafeCritical]
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			this.m_containingType.ThrowIfCreated();
			customBuilder.CreateCustomAttribute(this.m_moduleBuilder, this.m_prToken.Token);
		}

		// Token: 0x06004BBD RID: 19389 RVA: 0x00112785 File Offset: 0x00110985
		public override object GetValue(object obj, object[] index)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004BBE RID: 19390 RVA: 0x00112796 File Offset: 0x00110996
		public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004BBF RID: 19391 RVA: 0x001127A7 File Offset: 0x001109A7
		public override void SetValue(object obj, object value, object[] index)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004BC0 RID: 19392 RVA: 0x001127B8 File Offset: 0x001109B8
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004BC1 RID: 19393 RVA: 0x001127C9 File Offset: 0x001109C9
		public override MethodInfo[] GetAccessors(bool nonPublic)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004BC2 RID: 19394 RVA: 0x001127DA File Offset: 0x001109DA
		public override MethodInfo GetGetMethod(bool nonPublic)
		{
			if (nonPublic || this.m_getMethod == null)
			{
				return this.m_getMethod;
			}
			if ((this.m_getMethod.Attributes & MethodAttributes.Public) == MethodAttributes.Public)
			{
				return this.m_getMethod;
			}
			return null;
		}

		// Token: 0x06004BC3 RID: 19395 RVA: 0x0011280C File Offset: 0x00110A0C
		public override MethodInfo GetSetMethod(bool nonPublic)
		{
			if (nonPublic || this.m_setMethod == null)
			{
				return this.m_setMethod;
			}
			if ((this.m_setMethod.Attributes & MethodAttributes.Public) == MethodAttributes.Public)
			{
				return this.m_setMethod;
			}
			return null;
		}

		// Token: 0x06004BC4 RID: 19396 RVA: 0x0011283E File Offset: 0x00110A3E
		public override ParameterInfo[] GetIndexParameters()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x06004BC5 RID: 19397 RVA: 0x0011284F File Offset: 0x00110A4F
		public override Type PropertyType
		{
			get
			{
				return this.m_returnType;
			}
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x06004BC6 RID: 19398 RVA: 0x00112857 File Offset: 0x00110A57
		public override PropertyAttributes Attributes
		{
			get
			{
				return this.m_attributes;
			}
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x06004BC7 RID: 19399 RVA: 0x0011285F File Offset: 0x00110A5F
		public override bool CanRead
		{
			get
			{
				return this.m_getMethod != null;
			}
		}

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x06004BC8 RID: 19400 RVA: 0x00112872 File Offset: 0x00110A72
		public override bool CanWrite
		{
			get
			{
				return this.m_setMethod != null;
			}
		}

		// Token: 0x06004BC9 RID: 19401 RVA: 0x00112885 File Offset: 0x00110A85
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004BCA RID: 19402 RVA: 0x00112896 File Offset: 0x00110A96
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004BCB RID: 19403 RVA: 0x001128A7 File Offset: 0x00110AA7
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004BCC RID: 19404 RVA: 0x001128B8 File Offset: 0x00110AB8
		void _PropertyBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004BCD RID: 19405 RVA: 0x001128BF File Offset: 0x00110ABF
		void _PropertyBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004BCE RID: 19406 RVA: 0x001128C6 File Offset: 0x00110AC6
		void _PropertyBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004BCF RID: 19407 RVA: 0x001128CD File Offset: 0x00110ACD
		void _PropertyBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x06004BD0 RID: 19408 RVA: 0x001128D4 File Offset: 0x00110AD4
		public override string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x06004BD1 RID: 19409 RVA: 0x001128DC File Offset: 0x00110ADC
		public override Type DeclaringType
		{
			get
			{
				return this.m_containingType;
			}
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x06004BD2 RID: 19410 RVA: 0x001128E4 File Offset: 0x00110AE4
		public override Type ReflectedType
		{
			get
			{
				return this.m_containingType;
			}
		}

		// Token: 0x040020D6 RID: 8406
		private string m_name;

		// Token: 0x040020D7 RID: 8407
		private PropertyToken m_prToken;

		// Token: 0x040020D8 RID: 8408
		private int m_tkProperty;

		// Token: 0x040020D9 RID: 8409
		private ModuleBuilder m_moduleBuilder;

		// Token: 0x040020DA RID: 8410
		private SignatureHelper m_signature;

		// Token: 0x040020DB RID: 8411
		private PropertyAttributes m_attributes;

		// Token: 0x040020DC RID: 8412
		private Type m_returnType;

		// Token: 0x040020DD RID: 8413
		private MethodInfo m_getMethod;

		// Token: 0x040020DE RID: 8414
		private MethodInfo m_setMethod;

		// Token: 0x040020DF RID: 8415
		private TypeBuilder m_containingType;
	}
}
