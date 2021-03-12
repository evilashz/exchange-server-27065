using System;
using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x020008FE RID: 2302
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class MarshalAsAttribute : Attribute
	{
		// Token: 0x06005F01 RID: 24321 RVA: 0x00146DC7 File Offset: 0x00144FC7
		[SecurityCritical]
		internal static Attribute GetCustomAttribute(RuntimeParameterInfo parameter)
		{
			return MarshalAsAttribute.GetCustomAttribute(parameter.MetadataToken, parameter.GetRuntimeModule());
		}

		// Token: 0x06005F02 RID: 24322 RVA: 0x00146DDA File Offset: 0x00144FDA
		[SecurityCritical]
		internal static bool IsDefined(RuntimeParameterInfo parameter)
		{
			return MarshalAsAttribute.GetCustomAttribute(parameter) != null;
		}

		// Token: 0x06005F03 RID: 24323 RVA: 0x00146DE5 File Offset: 0x00144FE5
		[SecurityCritical]
		internal static Attribute GetCustomAttribute(RuntimeFieldInfo field)
		{
			return MarshalAsAttribute.GetCustomAttribute(field.MetadataToken, field.GetRuntimeModule());
		}

		// Token: 0x06005F04 RID: 24324 RVA: 0x00146DF8 File Offset: 0x00144FF8
		[SecurityCritical]
		internal static bool IsDefined(RuntimeFieldInfo field)
		{
			return MarshalAsAttribute.GetCustomAttribute(field) != null;
		}

		// Token: 0x06005F05 RID: 24325 RVA: 0x00146E04 File Offset: 0x00145004
		[SecurityCritical]
		internal static Attribute GetCustomAttribute(int token, RuntimeModule scope)
		{
			int num = 0;
			int sizeConst = 0;
			string text = null;
			string marshalCookie = null;
			string text2 = null;
			int iidParamIndex = 0;
			ConstArray fieldMarshal = ModuleHandle.GetMetadataImport(scope.GetNativeHandle()).GetFieldMarshal(token);
			if (fieldMarshal.Length == 0)
			{
				return null;
			}
			UnmanagedType val;
			VarEnum safeArraySubType;
			UnmanagedType arraySubType;
			MetadataImport.GetMarshalAs(fieldMarshal, out val, out safeArraySubType, out text2, out arraySubType, out num, out sizeConst, out text, out marshalCookie, out iidParamIndex);
			RuntimeType safeArrayUserDefinedSubType = (text2 == null || text2.Length == 0) ? null : RuntimeTypeHandle.GetTypeByNameUsingCARules(text2, scope);
			RuntimeType marshalTypeRef = null;
			try
			{
				marshalTypeRef = ((text == null) ? null : RuntimeTypeHandle.GetTypeByNameUsingCARules(text, scope));
			}
			catch (TypeLoadException)
			{
			}
			return new MarshalAsAttribute(val, safeArraySubType, safeArrayUserDefinedSubType, arraySubType, (short)num, sizeConst, text, marshalTypeRef, marshalCookie, iidParamIndex);
		}

		// Token: 0x06005F06 RID: 24326 RVA: 0x00146EB8 File Offset: 0x001450B8
		internal MarshalAsAttribute(UnmanagedType val, VarEnum safeArraySubType, RuntimeType safeArrayUserDefinedSubType, UnmanagedType arraySubType, short sizeParamIndex, int sizeConst, string marshalType, RuntimeType marshalTypeRef, string marshalCookie, int iidParamIndex)
		{
			this._val = val;
			this.SafeArraySubType = safeArraySubType;
			this.SafeArrayUserDefinedSubType = safeArrayUserDefinedSubType;
			this.IidParameterIndex = iidParamIndex;
			this.ArraySubType = arraySubType;
			this.SizeParamIndex = sizeParamIndex;
			this.SizeConst = sizeConst;
			this.MarshalType = marshalType;
			this.MarshalTypeRef = marshalTypeRef;
			this.MarshalCookie = marshalCookie;
		}

		// Token: 0x06005F07 RID: 24327 RVA: 0x00146F18 File Offset: 0x00145118
		[__DynamicallyInvokable]
		public MarshalAsAttribute(UnmanagedType unmanagedType)
		{
			this._val = unmanagedType;
		}

		// Token: 0x06005F08 RID: 24328 RVA: 0x00146F27 File Offset: 0x00145127
		[__DynamicallyInvokable]
		public MarshalAsAttribute(short unmanagedType)
		{
			this._val = (UnmanagedType)unmanagedType;
		}

		// Token: 0x170010C8 RID: 4296
		// (get) Token: 0x06005F09 RID: 24329 RVA: 0x00146F36 File Offset: 0x00145136
		[__DynamicallyInvokable]
		public UnmanagedType Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A43 RID: 10819
		internal UnmanagedType _val;

		// Token: 0x04002A44 RID: 10820
		[__DynamicallyInvokable]
		public VarEnum SafeArraySubType;

		// Token: 0x04002A45 RID: 10821
		[__DynamicallyInvokable]
		public Type SafeArrayUserDefinedSubType;

		// Token: 0x04002A46 RID: 10822
		[__DynamicallyInvokable]
		public int IidParameterIndex;

		// Token: 0x04002A47 RID: 10823
		[__DynamicallyInvokable]
		public UnmanagedType ArraySubType;

		// Token: 0x04002A48 RID: 10824
		[__DynamicallyInvokable]
		public short SizeParamIndex;

		// Token: 0x04002A49 RID: 10825
		[__DynamicallyInvokable]
		public int SizeConst;

		// Token: 0x04002A4A RID: 10826
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public string MarshalType;

		// Token: 0x04002A4B RID: 10827
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public Type MarshalTypeRef;

		// Token: 0x04002A4C RID: 10828
		[__DynamicallyInvokable]
		public string MarshalCookie;
	}
}
