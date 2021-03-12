using System;

namespace Microsoft.Exchange.Setup.Parser
{
	// Token: 0x0200000A RID: 10
	internal class SetupParameter
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00004432 File Offset: 0x00002632
		public SetupParameter(string name, object val)
		{
			this.Name = name;
			this.Value = val;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00004448 File Offset: 0x00002648
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00004450 File Offset: 0x00002650
		public string Name { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00004459 File Offset: 0x00002659
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00004461 File Offset: 0x00002661
		public object Value { get; private set; }
	}
}
