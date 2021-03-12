using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005AD RID: 1453
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	internal struct CustomAttributeNamedParameter
	{
		// Token: 0x0600444A RID: 17482 RVA: 0x000FAB00 File Offset: 0x000F8D00
		public CustomAttributeNamedParameter(string argumentName, CustomAttributeEncoding fieldOrProperty, CustomAttributeType type)
		{
			if (argumentName == null)
			{
				throw new ArgumentNullException("argumentName");
			}
			this.m_argumentName = argumentName;
			this.m_fieldOrProperty = fieldOrProperty;
			this.m_padding = fieldOrProperty;
			this.m_type = type;
			this.m_encodedArgument = default(CustomAttributeEncodedArgument);
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x0600444B RID: 17483 RVA: 0x000FAB38 File Offset: 0x000F8D38
		public CustomAttributeEncodedArgument EncodedArgument
		{
			get
			{
				return this.m_encodedArgument;
			}
		}

		// Token: 0x04001BC4 RID: 7108
		private string m_argumentName;

		// Token: 0x04001BC5 RID: 7109
		private CustomAttributeEncoding m_fieldOrProperty;

		// Token: 0x04001BC6 RID: 7110
		private CustomAttributeEncoding m_padding;

		// Token: 0x04001BC7 RID: 7111
		private CustomAttributeType m_type;

		// Token: 0x04001BC8 RID: 7112
		private CustomAttributeEncodedArgument m_encodedArgument;
	}
}
