using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005AE RID: 1454
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	internal struct CustomAttributeCtorParameter
	{
		// Token: 0x0600444C RID: 17484 RVA: 0x000FAB40 File Offset: 0x000F8D40
		public CustomAttributeCtorParameter(CustomAttributeType type)
		{
			this.m_type = type;
			this.m_encodedArgument = default(CustomAttributeEncodedArgument);
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x0600444D RID: 17485 RVA: 0x000FAB55 File Offset: 0x000F8D55
		public CustomAttributeEncodedArgument CustomAttributeEncodedArgument
		{
			get
			{
				return this.m_encodedArgument;
			}
		}

		// Token: 0x04001BC9 RID: 7113
		private CustomAttributeType m_type;

		// Token: 0x04001BCA RID: 7114
		private CustomAttributeEncodedArgument m_encodedArgument;
	}
}
