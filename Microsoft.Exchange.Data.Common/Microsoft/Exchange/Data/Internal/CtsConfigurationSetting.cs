using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Internal
{
	// Token: 0x02000142 RID: 322
	internal class CtsConfigurationSetting
	{
		// Token: 0x06000C8D RID: 3213 RVA: 0x0006E85E File Offset: 0x0006CA5E
		internal CtsConfigurationSetting(string name)
		{
			this.name = name;
			this.arguments = new List<CtsConfigurationArgument>();
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x0006E878 File Offset: 0x0006CA78
		internal void AddArgument(string name, string value)
		{
			this.arguments.Add(new CtsConfigurationArgument(name, value));
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000C8F RID: 3215 RVA: 0x0006E88C File Offset: 0x0006CA8C
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x0006E894 File Offset: 0x0006CA94
		public IList<CtsConfigurationArgument> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x04000F12 RID: 3858
		private string name;

		// Token: 0x04000F13 RID: 3859
		private IList<CtsConfigurationArgument> arguments;
	}
}
