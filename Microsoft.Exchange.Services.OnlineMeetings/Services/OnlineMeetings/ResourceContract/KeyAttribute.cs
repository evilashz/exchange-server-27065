using System;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000043 RID: 67
	[AttributeUsage(AttributeTargets.Class)]
	internal class KeyAttribute : Attribute
	{
		// Token: 0x06000237 RID: 567 RVA: 0x00007D6C File Offset: 0x00005F6C
		public KeyAttribute(string key)
		{
			this.key = key;
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00007D7B File Offset: 0x00005F7B
		public string Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x04000175 RID: 373
		private readonly string key;
	}
}
