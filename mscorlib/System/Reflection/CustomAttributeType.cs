using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005B0 RID: 1456
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	internal struct CustomAttributeType
	{
		// Token: 0x06004450 RID: 17488 RVA: 0x000FAB5D File Offset: 0x000F8D5D
		public CustomAttributeType(CustomAttributeEncoding encodedType, CustomAttributeEncoding encodedArrayType, CustomAttributeEncoding encodedEnumType, string enumName)
		{
			this.m_encodedType = encodedType;
			this.m_encodedArrayType = encodedArrayType;
			this.m_encodedEnumType = encodedEnumType;
			this.m_enumName = enumName;
			this.m_padding = this.m_encodedType;
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x06004451 RID: 17489 RVA: 0x000FAB88 File Offset: 0x000F8D88
		public CustomAttributeEncoding EncodedType
		{
			get
			{
				return this.m_encodedType;
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06004452 RID: 17490 RVA: 0x000FAB90 File Offset: 0x000F8D90
		public CustomAttributeEncoding EncodedEnumType
		{
			get
			{
				return this.m_encodedEnumType;
			}
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06004453 RID: 17491 RVA: 0x000FAB98 File Offset: 0x000F8D98
		public CustomAttributeEncoding EncodedArrayType
		{
			get
			{
				return this.m_encodedArrayType;
			}
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06004454 RID: 17492 RVA: 0x000FABA0 File Offset: 0x000F8DA0
		[ComVisible(true)]
		public string EnumName
		{
			get
			{
				return this.m_enumName;
			}
		}

		// Token: 0x04001BCF RID: 7119
		private string m_enumName;

		// Token: 0x04001BD0 RID: 7120
		private CustomAttributeEncoding m_encodedType;

		// Token: 0x04001BD1 RID: 7121
		private CustomAttributeEncoding m_encodedEnumType;

		// Token: 0x04001BD2 RID: 7122
		private CustomAttributeEncoding m_encodedArrayType;

		// Token: 0x04001BD3 RID: 7123
		private CustomAttributeEncoding m_padding;
	}
}
