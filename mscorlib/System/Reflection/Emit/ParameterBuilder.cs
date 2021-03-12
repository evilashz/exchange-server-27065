using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection.Emit
{
	// Token: 0x0200062E RID: 1582
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_ParameterBuilder))]
	[ComVisible(true)]
	public class ParameterBuilder : _ParameterBuilder
	{
		// Token: 0x06004B97 RID: 19351 RVA: 0x001122FC File Offset: 0x001104FC
		[SecuritySafeCritical]
		[Obsolete("An alternate API is available: Emit the MarshalAs custom attribute instead. http://go.microsoft.com/fwlink/?linkid=14202")]
		public virtual void SetMarshal(UnmanagedMarshal unmanagedMarshal)
		{
			if (unmanagedMarshal == null)
			{
				throw new ArgumentNullException("unmanagedMarshal");
			}
			byte[] array = unmanagedMarshal.InternalGetBytes();
			TypeBuilder.SetFieldMarshal(this.m_methodBuilder.GetModuleBuilder().GetNativeHandle(), this.m_pdToken.Token, array, array.Length);
		}

		// Token: 0x06004B98 RID: 19352 RVA: 0x00112344 File Offset: 0x00110544
		[SecuritySafeCritical]
		public virtual void SetConstant(object defaultValue)
		{
			TypeBuilder.SetConstantValue(this.m_methodBuilder.GetModuleBuilder(), this.m_pdToken.Token, (this.m_iPosition == 0) ? this.m_methodBuilder.ReturnType : this.m_methodBuilder.m_parameterTypes[this.m_iPosition - 1], defaultValue);
		}

		// Token: 0x06004B99 RID: 19353 RVA: 0x00112398 File Offset: 0x00110598
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
			TypeBuilder.DefineCustomAttribute(this.m_methodBuilder.GetModuleBuilder(), this.m_pdToken.Token, ((ModuleBuilder)this.m_methodBuilder.GetModule()).GetConstructorToken(con).Token, binaryAttribute, false, false);
		}

		// Token: 0x06004B9A RID: 19354 RVA: 0x00112403 File Offset: 0x00110603
		[SecuritySafeCritical]
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			customBuilder.CreateCustomAttribute((ModuleBuilder)this.m_methodBuilder.GetModule(), this.m_pdToken.Token);
		}

		// Token: 0x06004B9B RID: 19355 RVA: 0x00112434 File Offset: 0x00110634
		private ParameterBuilder()
		{
		}

		// Token: 0x06004B9C RID: 19356 RVA: 0x0011243C File Offset: 0x0011063C
		[SecurityCritical]
		internal ParameterBuilder(MethodBuilder methodBuilder, int sequence, ParameterAttributes attributes, string strParamName)
		{
			this.m_iPosition = sequence;
			this.m_strParamName = strParamName;
			this.m_methodBuilder = methodBuilder;
			this.m_strParamName = strParamName;
			this.m_attributes = attributes;
			this.m_pdToken = new ParameterToken(TypeBuilder.SetParamInfo(this.m_methodBuilder.GetModuleBuilder().GetNativeHandle(), this.m_methodBuilder.GetToken().Token, sequence, attributes, strParamName));
		}

		// Token: 0x06004B9D RID: 19357 RVA: 0x001124AB File Offset: 0x001106AB
		public virtual ParameterToken GetToken()
		{
			return this.m_pdToken;
		}

		// Token: 0x06004B9E RID: 19358 RVA: 0x001124B3 File Offset: 0x001106B3
		void _ParameterBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B9F RID: 19359 RVA: 0x001124BA File Offset: 0x001106BA
		void _ParameterBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004BA0 RID: 19360 RVA: 0x001124C1 File Offset: 0x001106C1
		void _ParameterBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004BA1 RID: 19361 RVA: 0x001124C8 File Offset: 0x001106C8
		void _ParameterBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x06004BA2 RID: 19362 RVA: 0x001124CF File Offset: 0x001106CF
		internal int MetadataTokenInternal
		{
			get
			{
				return this.m_pdToken.Token;
			}
		}

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x06004BA3 RID: 19363 RVA: 0x001124DC File Offset: 0x001106DC
		public virtual string Name
		{
			get
			{
				return this.m_strParamName;
			}
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x06004BA4 RID: 19364 RVA: 0x001124E4 File Offset: 0x001106E4
		public virtual int Position
		{
			get
			{
				return this.m_iPosition;
			}
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x06004BA5 RID: 19365 RVA: 0x001124EC File Offset: 0x001106EC
		public virtual int Attributes
		{
			get
			{
				return (int)this.m_attributes;
			}
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x06004BA6 RID: 19366 RVA: 0x001124F4 File Offset: 0x001106F4
		public bool IsIn
		{
			get
			{
				return (this.m_attributes & ParameterAttributes.In) > ParameterAttributes.None;
			}
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x06004BA7 RID: 19367 RVA: 0x00112501 File Offset: 0x00110701
		public bool IsOut
		{
			get
			{
				return (this.m_attributes & ParameterAttributes.Out) > ParameterAttributes.None;
			}
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x06004BA8 RID: 19368 RVA: 0x0011250E File Offset: 0x0011070E
		public bool IsOptional
		{
			get
			{
				return (this.m_attributes & ParameterAttributes.Optional) > ParameterAttributes.None;
			}
		}

		// Token: 0x040020CF RID: 8399
		private string m_strParamName;

		// Token: 0x040020D0 RID: 8400
		private int m_iPosition;

		// Token: 0x040020D1 RID: 8401
		private ParameterAttributes m_attributes;

		// Token: 0x040020D2 RID: 8402
		private MethodBuilder m_methodBuilder;

		// Token: 0x040020D3 RID: 8403
		private ParameterToken m_pdToken;
	}
}
