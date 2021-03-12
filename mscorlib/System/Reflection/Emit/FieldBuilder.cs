using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
	// Token: 0x0200060E RID: 1550
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_FieldBuilder))]
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class FieldBuilder : FieldInfo, _FieldBuilder
	{
		// Token: 0x06004959 RID: 18777 RVA: 0x00108B4C File Offset: 0x00106D4C
		[SecurityCritical]
		internal FieldBuilder(TypeBuilder typeBuilder, string fieldName, Type type, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers, FieldAttributes attributes)
		{
			if (fieldName == null)
			{
				throw new ArgumentNullException("fieldName");
			}
			if (fieldName.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "fieldName");
			}
			if (fieldName[0] == '\0')
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IllegalName"), "fieldName");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type == typeof(void))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_BadFieldType"));
			}
			this.m_fieldName = fieldName;
			this.m_typeBuilder = typeBuilder;
			this.m_fieldType = type;
			this.m_Attributes = (attributes & ~FieldAttributes.ReservedMask);
			SignatureHelper fieldSigHelper = SignatureHelper.GetFieldSigHelper(this.m_typeBuilder.Module);
			fieldSigHelper.AddArgument(type, requiredCustomModifiers, optionalCustomModifiers);
			int sigLength;
			byte[] signature = fieldSigHelper.InternalGetSignature(out sigLength);
			this.m_fieldTok = TypeBuilder.DefineField(this.m_typeBuilder.GetModuleBuilder().GetNativeHandle(), typeBuilder.TypeToken.Token, fieldName, signature, sigLength, this.m_Attributes);
			this.m_tkField = new FieldToken(this.m_fieldTok, type);
		}

		// Token: 0x0600495A RID: 18778 RVA: 0x00108C6A File Offset: 0x00106E6A
		[SecurityCritical]
		internal void SetData(byte[] data, int size)
		{
			ModuleBuilder.SetFieldRVAContent(this.m_typeBuilder.GetModuleBuilder().GetNativeHandle(), this.m_tkField.Token, data, size);
		}

		// Token: 0x0600495B RID: 18779 RVA: 0x00108C8E File Offset: 0x00106E8E
		internal TypeBuilder GetTypeBuilder()
		{
			return this.m_typeBuilder;
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x0600495C RID: 18780 RVA: 0x00108C96 File Offset: 0x00106E96
		internal int MetadataTokenInternal
		{
			get
			{
				return this.m_fieldTok;
			}
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x0600495D RID: 18781 RVA: 0x00108C9E File Offset: 0x00106E9E
		public override Module Module
		{
			get
			{
				return this.m_typeBuilder.Module;
			}
		}

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x0600495E RID: 18782 RVA: 0x00108CAB File Offset: 0x00106EAB
		public override string Name
		{
			get
			{
				return this.m_fieldName;
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x0600495F RID: 18783 RVA: 0x00108CB3 File Offset: 0x00106EB3
		public override Type DeclaringType
		{
			get
			{
				if (this.m_typeBuilder.m_isHiddenGlobalType)
				{
					return null;
				}
				return this.m_typeBuilder;
			}
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x06004960 RID: 18784 RVA: 0x00108CCA File Offset: 0x00106ECA
		public override Type ReflectedType
		{
			get
			{
				if (this.m_typeBuilder.m_isHiddenGlobalType)
				{
					return null;
				}
				return this.m_typeBuilder;
			}
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x06004961 RID: 18785 RVA: 0x00108CE1 File Offset: 0x00106EE1
		public override Type FieldType
		{
			get
			{
				return this.m_fieldType;
			}
		}

		// Token: 0x06004962 RID: 18786 RVA: 0x00108CE9 File Offset: 0x00106EE9
		public override object GetValue(object obj)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004963 RID: 18787 RVA: 0x00108CFA File Offset: 0x00106EFA
		public override void SetValue(object obj, object val, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x06004964 RID: 18788 RVA: 0x00108D0B File Offset: 0x00106F0B
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
			}
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x06004965 RID: 18789 RVA: 0x00108D1C File Offset: 0x00106F1C
		public override FieldAttributes Attributes
		{
			get
			{
				return this.m_Attributes;
			}
		}

		// Token: 0x06004966 RID: 18790 RVA: 0x00108D24 File Offset: 0x00106F24
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004967 RID: 18791 RVA: 0x00108D35 File Offset: 0x00106F35
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004968 RID: 18792 RVA: 0x00108D46 File Offset: 0x00106F46
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicModule"));
		}

		// Token: 0x06004969 RID: 18793 RVA: 0x00108D57 File Offset: 0x00106F57
		public FieldToken GetToken()
		{
			return this.m_tkField;
		}

		// Token: 0x0600496A RID: 18794 RVA: 0x00108D60 File Offset: 0x00106F60
		[SecuritySafeCritical]
		public void SetOffset(int iOffset)
		{
			this.m_typeBuilder.ThrowIfCreated();
			TypeBuilder.SetFieldLayoutOffset(this.m_typeBuilder.GetModuleBuilder().GetNativeHandle(), this.GetToken().Token, iOffset);
		}

		// Token: 0x0600496B RID: 18795 RVA: 0x00108D9C File Offset: 0x00106F9C
		[SecuritySafeCritical]
		[Obsolete("An alternate API is available: Emit the MarshalAs custom attribute instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public void SetMarshal(UnmanagedMarshal unmanagedMarshal)
		{
			if (unmanagedMarshal == null)
			{
				throw new ArgumentNullException("unmanagedMarshal");
			}
			this.m_typeBuilder.ThrowIfCreated();
			byte[] array = unmanagedMarshal.InternalGetBytes();
			TypeBuilder.SetFieldMarshal(this.m_typeBuilder.GetModuleBuilder().GetNativeHandle(), this.GetToken().Token, array, array.Length);
		}

		// Token: 0x0600496C RID: 18796 RVA: 0x00108DF0 File Offset: 0x00106FF0
		[SecuritySafeCritical]
		public void SetConstant(object defaultValue)
		{
			this.m_typeBuilder.ThrowIfCreated();
			TypeBuilder.SetConstantValue(this.m_typeBuilder.GetModuleBuilder(), this.GetToken().Token, this.m_fieldType, defaultValue);
		}

		// Token: 0x0600496D RID: 18797 RVA: 0x00108E30 File Offset: 0x00107030
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
			ModuleBuilder moduleBuilder = this.m_typeBuilder.Module as ModuleBuilder;
			this.m_typeBuilder.ThrowIfCreated();
			TypeBuilder.DefineCustomAttribute(moduleBuilder, this.m_tkField.Token, moduleBuilder.GetConstructorToken(con).Token, binaryAttribute, false, false);
		}

		// Token: 0x0600496E RID: 18798 RVA: 0x00108EA0 File Offset: 0x001070A0
		[SecuritySafeCritical]
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			this.m_typeBuilder.ThrowIfCreated();
			ModuleBuilder mod = this.m_typeBuilder.Module as ModuleBuilder;
			customBuilder.CreateCustomAttribute(mod, this.m_tkField.Token);
		}

		// Token: 0x0600496F RID: 18799 RVA: 0x00108EE9 File Offset: 0x001070E9
		void _FieldBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004970 RID: 18800 RVA: 0x00108EF0 File Offset: 0x001070F0
		void _FieldBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004971 RID: 18801 RVA: 0x00108EF7 File Offset: 0x001070F7
		void _FieldBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004972 RID: 18802 RVA: 0x00108EFE File Offset: 0x001070FE
		void _FieldBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001DFA RID: 7674
		private int m_fieldTok;

		// Token: 0x04001DFB RID: 7675
		private FieldToken m_tkField;

		// Token: 0x04001DFC RID: 7676
		private TypeBuilder m_typeBuilder;

		// Token: 0x04001DFD RID: 7677
		private string m_fieldName;

		// Token: 0x04001DFE RID: 7678
		private FieldAttributes m_Attributes;

		// Token: 0x04001DFF RID: 7679
		private Type m_fieldType;
	}
}
