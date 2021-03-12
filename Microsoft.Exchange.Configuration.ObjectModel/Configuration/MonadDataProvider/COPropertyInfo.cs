using System;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001CD RID: 461
	internal class COPropertyInfo
	{
		// Token: 0x06001056 RID: 4182 RVA: 0x00031C19 File Offset: 0x0002FE19
		public COPropertyInfo(string name, Type type)
		{
			this.Name = name;
			this.Type = type;
		}

		// Token: 0x04000382 RID: 898
		public string Name;

		// Token: 0x04000383 RID: 899
		public Type Type;
	}
}
