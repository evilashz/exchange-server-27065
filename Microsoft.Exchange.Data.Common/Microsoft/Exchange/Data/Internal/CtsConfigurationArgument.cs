using System;

namespace Microsoft.Exchange.Data.Internal
{
	// Token: 0x02000143 RID: 323
	internal class CtsConfigurationArgument
	{
		// Token: 0x06000C91 RID: 3217 RVA: 0x0006E89C File Offset: 0x0006CA9C
		internal CtsConfigurationArgument(string name, string value)
		{
			this.name = name;
			this.value = value;
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x0006E8B2 File Offset: 0x0006CAB2
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x0006E8BA File Offset: 0x0006CABA
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x04000F14 RID: 3860
		private string name;

		// Token: 0x04000F15 RID: 3861
		private string value;
	}
}
