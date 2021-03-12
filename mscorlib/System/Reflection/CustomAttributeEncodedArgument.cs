using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection
{
	// Token: 0x020005AC RID: 1452
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	internal struct CustomAttributeEncodedArgument
	{
		// Token: 0x06004444 RID: 17476
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParseAttributeArguments(IntPtr pCa, int cCa, ref CustomAttributeCtorParameter[] CustomAttributeCtorParameters, ref CustomAttributeNamedParameter[] CustomAttributeTypedArgument, RuntimeAssembly assembly);

		// Token: 0x06004445 RID: 17477 RVA: 0x000FAAA0 File Offset: 0x000F8CA0
		[SecurityCritical]
		internal static void ParseAttributeArguments(ConstArray attributeBlob, ref CustomAttributeCtorParameter[] customAttributeCtorParameters, ref CustomAttributeNamedParameter[] customAttributeNamedParameters, RuntimeModule customAttributeModule)
		{
			if (customAttributeModule == null)
			{
				throw new ArgumentNullException("customAttributeModule");
			}
			if (customAttributeCtorParameters.Length != 0 || customAttributeNamedParameters.Length != 0)
			{
				CustomAttributeEncodedArgument.ParseAttributeArguments(attributeBlob.Signature, attributeBlob.Length, ref customAttributeCtorParameters, ref customAttributeNamedParameters, (RuntimeAssembly)customAttributeModule.Assembly);
			}
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06004446 RID: 17478 RVA: 0x000FAAE0 File Offset: 0x000F8CE0
		public CustomAttributeType CustomAttributeType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06004447 RID: 17479 RVA: 0x000FAAE8 File Offset: 0x000F8CE8
		public long PrimitiveValue
		{
			get
			{
				return this.m_primitiveValue;
			}
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06004448 RID: 17480 RVA: 0x000FAAF0 File Offset: 0x000F8CF0
		public CustomAttributeEncodedArgument[] ArrayValue
		{
			get
			{
				return this.m_arrayValue;
			}
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06004449 RID: 17481 RVA: 0x000FAAF8 File Offset: 0x000F8CF8
		public string StringValue
		{
			get
			{
				return this.m_stringValue;
			}
		}

		// Token: 0x04001BC0 RID: 7104
		private long m_primitiveValue;

		// Token: 0x04001BC1 RID: 7105
		private CustomAttributeEncodedArgument[] m_arrayValue;

		// Token: 0x04001BC2 RID: 7106
		private string m_stringValue;

		// Token: 0x04001BC3 RID: 7107
		private CustomAttributeType m_type;
	}
}
