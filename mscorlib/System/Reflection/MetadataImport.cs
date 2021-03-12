using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection
{
	// Token: 0x020005D1 RID: 1489
	internal struct MetadataImport
	{
		// Token: 0x06004576 RID: 17782 RVA: 0x000FE152 File Offset: 0x000FC352
		public override int GetHashCode()
		{
			return ValueType.GetHashCodeOfPtr(this.m_metadataImport2);
		}

		// Token: 0x06004577 RID: 17783 RVA: 0x000FE15F File Offset: 0x000FC35F
		public override bool Equals(object obj)
		{
			return obj is MetadataImport && this.Equals((MetadataImport)obj);
		}

		// Token: 0x06004578 RID: 17784 RVA: 0x000FE177 File Offset: 0x000FC377
		private bool Equals(MetadataImport import)
		{
			return import.m_metadataImport2 == this.m_metadataImport2;
		}

		// Token: 0x06004579 RID: 17785
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetMarshalAs(IntPtr pNativeType, int cNativeType, out int unmanagedType, out int safeArraySubType, out string safeArrayUserDefinedSubType, out int arraySubType, out int sizeParamIndex, out int sizeConst, out string marshalType, out string marshalCookie, out int iidParamIndex);

		// Token: 0x0600457A RID: 17786 RVA: 0x000FE18C File Offset: 0x000FC38C
		[SecurityCritical]
		internal static void GetMarshalAs(ConstArray nativeType, out UnmanagedType unmanagedType, out VarEnum safeArraySubType, out string safeArrayUserDefinedSubType, out UnmanagedType arraySubType, out int sizeParamIndex, out int sizeConst, out string marshalType, out string marshalCookie, out int iidParamIndex)
		{
			int num;
			int num2;
			int num3;
			MetadataImport._GetMarshalAs(nativeType.Signature, nativeType.Length, out num, out num2, out safeArrayUserDefinedSubType, out num3, out sizeParamIndex, out sizeConst, out marshalType, out marshalCookie, out iidParamIndex);
			unmanagedType = (UnmanagedType)num;
			safeArraySubType = (VarEnum)num2;
			arraySubType = (UnmanagedType)num3;
		}

		// Token: 0x0600457B RID: 17787 RVA: 0x000FE1C7 File Offset: 0x000FC3C7
		internal static void ThrowError(int hResult)
		{
			throw new MetadataException(hResult);
		}

		// Token: 0x0600457C RID: 17788 RVA: 0x000FE1CF File Offset: 0x000FC3CF
		internal MetadataImport(IntPtr metadataImport2, object keepalive)
		{
			this.m_metadataImport2 = metadataImport2;
			this.m_keepalive = keepalive;
		}

		// Token: 0x0600457D RID: 17789
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _Enum(IntPtr scope, int type, int parent, out MetadataEnumResult result);

		// Token: 0x0600457E RID: 17790 RVA: 0x000FE1DF File Offset: 0x000FC3DF
		[SecurityCritical]
		public void Enum(MetadataTokenType type, int parent, out MetadataEnumResult result)
		{
			MetadataImport._Enum(this.m_metadataImport2, (int)type, parent, out result);
		}

		// Token: 0x0600457F RID: 17791 RVA: 0x000FE1EF File Offset: 0x000FC3EF
		[SecurityCritical]
		public void EnumNestedTypes(int mdTypeDef, out MetadataEnumResult result)
		{
			this.Enum(MetadataTokenType.TypeDef, mdTypeDef, out result);
		}

		// Token: 0x06004580 RID: 17792 RVA: 0x000FE1FE File Offset: 0x000FC3FE
		[SecurityCritical]
		public void EnumCustomAttributes(int mdToken, out MetadataEnumResult result)
		{
			this.Enum(MetadataTokenType.CustomAttribute, mdToken, out result);
		}

		// Token: 0x06004581 RID: 17793 RVA: 0x000FE20D File Offset: 0x000FC40D
		[SecurityCritical]
		public void EnumParams(int mdMethodDef, out MetadataEnumResult result)
		{
			this.Enum(MetadataTokenType.ParamDef, mdMethodDef, out result);
		}

		// Token: 0x06004582 RID: 17794 RVA: 0x000FE21C File Offset: 0x000FC41C
		[SecurityCritical]
		public void EnumFields(int mdTypeDef, out MetadataEnumResult result)
		{
			this.Enum(MetadataTokenType.FieldDef, mdTypeDef, out result);
		}

		// Token: 0x06004583 RID: 17795 RVA: 0x000FE22B File Offset: 0x000FC42B
		[SecurityCritical]
		public void EnumProperties(int mdTypeDef, out MetadataEnumResult result)
		{
			this.Enum(MetadataTokenType.Property, mdTypeDef, out result);
		}

		// Token: 0x06004584 RID: 17796 RVA: 0x000FE23A File Offset: 0x000FC43A
		[SecurityCritical]
		public void EnumEvents(int mdTypeDef, out MetadataEnumResult result)
		{
			this.Enum(MetadataTokenType.Event, mdTypeDef, out result);
		}

		// Token: 0x06004585 RID: 17797
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string _GetDefaultValue(IntPtr scope, int mdToken, out long value, out int length, out int corElementType);

		// Token: 0x06004586 RID: 17798 RVA: 0x000FE24C File Offset: 0x000FC44C
		[SecurityCritical]
		public string GetDefaultValue(int mdToken, out long value, out int length, out CorElementType corElementType)
		{
			int num;
			string result = MetadataImport._GetDefaultValue(this.m_metadataImport2, mdToken, out value, out length, out num);
			corElementType = (CorElementType)num;
			return result;
		}

		// Token: 0x06004587 RID: 17799
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void _GetUserString(IntPtr scope, int mdToken, void** name, out int length);

		// Token: 0x06004588 RID: 17800 RVA: 0x000FE270 File Offset: 0x000FC470
		[SecurityCritical]
		public unsafe string GetUserString(int mdToken)
		{
			void* ptr;
			int num;
			MetadataImport._GetUserString(this.m_metadataImport2, mdToken, &ptr, out num);
			if (ptr == null)
			{
				return null;
			}
			char[] array = new char[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = (char)Marshal.ReadInt16((IntPtr)((void*)((byte*)ptr + (IntPtr)i * 2)));
			}
			return new string(array);
		}

		// Token: 0x06004589 RID: 17801
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void _GetName(IntPtr scope, int mdToken, void** name);

		// Token: 0x0600458A RID: 17802 RVA: 0x000FE2C4 File Offset: 0x000FC4C4
		[SecurityCritical]
		public unsafe Utf8String GetName(int mdToken)
		{
			void* pStringHeap;
			MetadataImport._GetName(this.m_metadataImport2, mdToken, &pStringHeap);
			return new Utf8String(pStringHeap);
		}

		// Token: 0x0600458B RID: 17803
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void _GetNamespace(IntPtr scope, int mdToken, void** namesp);

		// Token: 0x0600458C RID: 17804 RVA: 0x000FE2E8 File Offset: 0x000FC4E8
		[SecurityCritical]
		public unsafe Utf8String GetNamespace(int mdToken)
		{
			void* pStringHeap;
			MetadataImport._GetNamespace(this.m_metadataImport2, mdToken, &pStringHeap);
			return new Utf8String(pStringHeap);
		}

		// Token: 0x0600458D RID: 17805
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void _GetEventProps(IntPtr scope, int mdToken, void** name, out int eventAttributes);

		// Token: 0x0600458E RID: 17806 RVA: 0x000FE30C File Offset: 0x000FC50C
		[SecurityCritical]
		public unsafe void GetEventProps(int mdToken, out void* name, out EventAttributes eventAttributes)
		{
			void* ptr;
			int num;
			MetadataImport._GetEventProps(this.m_metadataImport2, mdToken, &ptr, out num);
			name = ptr;
			eventAttributes = (EventAttributes)num;
		}

		// Token: 0x0600458F RID: 17807
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetFieldDefProps(IntPtr scope, int mdToken, out int fieldAttributes);

		// Token: 0x06004590 RID: 17808 RVA: 0x000FE330 File Offset: 0x000FC530
		[SecurityCritical]
		public void GetFieldDefProps(int mdToken, out FieldAttributes fieldAttributes)
		{
			int num;
			MetadataImport._GetFieldDefProps(this.m_metadataImport2, mdToken, out num);
			fieldAttributes = (FieldAttributes)num;
		}

		// Token: 0x06004591 RID: 17809
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void _GetPropertyProps(IntPtr scope, int mdToken, void** name, out int propertyAttributes, out ConstArray signature);

		// Token: 0x06004592 RID: 17810 RVA: 0x000FE350 File Offset: 0x000FC550
		[SecurityCritical]
		public unsafe void GetPropertyProps(int mdToken, out void* name, out PropertyAttributes propertyAttributes, out ConstArray signature)
		{
			void* ptr;
			int num;
			MetadataImport._GetPropertyProps(this.m_metadataImport2, mdToken, &ptr, out num, out signature);
			name = ptr;
			propertyAttributes = (PropertyAttributes)num;
		}

		// Token: 0x06004593 RID: 17811
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetParentToken(IntPtr scope, int mdToken, out int tkParent);

		// Token: 0x06004594 RID: 17812 RVA: 0x000FE378 File Offset: 0x000FC578
		[SecurityCritical]
		public int GetParentToken(int tkToken)
		{
			int result;
			MetadataImport._GetParentToken(this.m_metadataImport2, tkToken, out result);
			return result;
		}

		// Token: 0x06004595 RID: 17813
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetParamDefProps(IntPtr scope, int parameterToken, out int sequence, out int attributes);

		// Token: 0x06004596 RID: 17814 RVA: 0x000FE394 File Offset: 0x000FC594
		[SecurityCritical]
		public void GetParamDefProps(int parameterToken, out int sequence, out ParameterAttributes attributes)
		{
			int num;
			MetadataImport._GetParamDefProps(this.m_metadataImport2, parameterToken, out sequence, out num);
			attributes = (ParameterAttributes)num;
		}

		// Token: 0x06004597 RID: 17815
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetGenericParamProps(IntPtr scope, int genericParameter, out int flags);

		// Token: 0x06004598 RID: 17816 RVA: 0x000FE3B4 File Offset: 0x000FC5B4
		[SecurityCritical]
		public void GetGenericParamProps(int genericParameter, out GenericParameterAttributes attributes)
		{
			int num;
			MetadataImport._GetGenericParamProps(this.m_metadataImport2, genericParameter, out num);
			attributes = (GenericParameterAttributes)num;
		}

		// Token: 0x06004599 RID: 17817
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetScopeProps(IntPtr scope, out Guid mvid);

		// Token: 0x0600459A RID: 17818 RVA: 0x000FE3D2 File Offset: 0x000FC5D2
		[SecurityCritical]
		public void GetScopeProps(out Guid mvid)
		{
			MetadataImport._GetScopeProps(this.m_metadataImport2, out mvid);
		}

		// Token: 0x0600459B RID: 17819 RVA: 0x000FE3E0 File Offset: 0x000FC5E0
		[SecurityCritical]
		public ConstArray GetMethodSignature(MetadataToken token)
		{
			if (token.IsMemberRef)
			{
				return this.GetMemberRefProps(token);
			}
			return this.GetSigOfMethodDef(token);
		}

		// Token: 0x0600459C RID: 17820
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetSigOfMethodDef(IntPtr scope, int methodToken, ref ConstArray signature);

		// Token: 0x0600459D RID: 17821 RVA: 0x000FE404 File Offset: 0x000FC604
		[SecurityCritical]
		public ConstArray GetSigOfMethodDef(int methodToken)
		{
			ConstArray result = default(ConstArray);
			MetadataImport._GetSigOfMethodDef(this.m_metadataImport2, methodToken, ref result);
			return result;
		}

		// Token: 0x0600459E RID: 17822
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetSignatureFromToken(IntPtr scope, int methodToken, ref ConstArray signature);

		// Token: 0x0600459F RID: 17823 RVA: 0x000FE428 File Offset: 0x000FC628
		[SecurityCritical]
		public ConstArray GetSignatureFromToken(int token)
		{
			ConstArray result = default(ConstArray);
			MetadataImport._GetSignatureFromToken(this.m_metadataImport2, token, ref result);
			return result;
		}

		// Token: 0x060045A0 RID: 17824
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetMemberRefProps(IntPtr scope, int memberTokenRef, out ConstArray signature);

		// Token: 0x060045A1 RID: 17825 RVA: 0x000FE44C File Offset: 0x000FC64C
		[SecurityCritical]
		public ConstArray GetMemberRefProps(int memberTokenRef)
		{
			ConstArray result = default(ConstArray);
			MetadataImport._GetMemberRefProps(this.m_metadataImport2, memberTokenRef, out result);
			return result;
		}

		// Token: 0x060045A2 RID: 17826
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetCustomAttributeProps(IntPtr scope, int customAttributeToken, out int constructorToken, out ConstArray signature);

		// Token: 0x060045A3 RID: 17827 RVA: 0x000FE470 File Offset: 0x000FC670
		[SecurityCritical]
		public void GetCustomAttributeProps(int customAttributeToken, out int constructorToken, out ConstArray signature)
		{
			MetadataImport._GetCustomAttributeProps(this.m_metadataImport2, customAttributeToken, out constructorToken, out signature);
		}

		// Token: 0x060045A4 RID: 17828
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetClassLayout(IntPtr scope, int typeTokenDef, out int packSize, out int classSize);

		// Token: 0x060045A5 RID: 17829 RVA: 0x000FE480 File Offset: 0x000FC680
		[SecurityCritical]
		public void GetClassLayout(int typeTokenDef, out int packSize, out int classSize)
		{
			MetadataImport._GetClassLayout(this.m_metadataImport2, typeTokenDef, out packSize, out classSize);
		}

		// Token: 0x060045A6 RID: 17830
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool _GetFieldOffset(IntPtr scope, int typeTokenDef, int fieldTokenDef, out int offset);

		// Token: 0x060045A7 RID: 17831 RVA: 0x000FE490 File Offset: 0x000FC690
		[SecurityCritical]
		public bool GetFieldOffset(int typeTokenDef, int fieldTokenDef, out int offset)
		{
			return MetadataImport._GetFieldOffset(this.m_metadataImport2, typeTokenDef, fieldTokenDef, out offset);
		}

		// Token: 0x060045A8 RID: 17832
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetSigOfFieldDef(IntPtr scope, int fieldToken, ref ConstArray fieldMarshal);

		// Token: 0x060045A9 RID: 17833 RVA: 0x000FE4A0 File Offset: 0x000FC6A0
		[SecurityCritical]
		public ConstArray GetSigOfFieldDef(int fieldToken)
		{
			ConstArray result = default(ConstArray);
			MetadataImport._GetSigOfFieldDef(this.m_metadataImport2, fieldToken, ref result);
			return result;
		}

		// Token: 0x060045AA RID: 17834
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _GetFieldMarshal(IntPtr scope, int fieldToken, ref ConstArray fieldMarshal);

		// Token: 0x060045AB RID: 17835 RVA: 0x000FE4C4 File Offset: 0x000FC6C4
		[SecurityCritical]
		public ConstArray GetFieldMarshal(int fieldToken)
		{
			ConstArray result = default(ConstArray);
			MetadataImport._GetFieldMarshal(this.m_metadataImport2, fieldToken, ref result);
			return result;
		}

		// Token: 0x060045AC RID: 17836
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void _GetPInvokeMap(IntPtr scope, int token, out int attributes, void** importName, void** importDll);

		// Token: 0x060045AD RID: 17837 RVA: 0x000FE4E8 File Offset: 0x000FC6E8
		[SecurityCritical]
		public unsafe void GetPInvokeMap(int token, out PInvokeAttributes attributes, out string importName, out string importDll)
		{
			int num;
			void* pStringHeap;
			void* pStringHeap2;
			MetadataImport._GetPInvokeMap(this.m_metadataImport2, token, out num, &pStringHeap, &pStringHeap2);
			importName = new Utf8String(pStringHeap).ToString();
			importDll = new Utf8String(pStringHeap2).ToString();
			attributes = (PInvokeAttributes)num;
		}

		// Token: 0x060045AE RID: 17838
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool _IsValidToken(IntPtr scope, int token);

		// Token: 0x060045AF RID: 17839 RVA: 0x000FE539 File Offset: 0x000FC739
		[SecurityCritical]
		public bool IsValidToken(int token)
		{
			return MetadataImport._IsValidToken(this.m_metadataImport2, token);
		}

		// Token: 0x04001C98 RID: 7320
		private IntPtr m_metadataImport2;

		// Token: 0x04001C99 RID: 7321
		private object m_keepalive;

		// Token: 0x04001C9A RID: 7322
		internal static readonly MetadataImport EmptyImport = new MetadataImport((IntPtr)0, null);
	}
}
