using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200018B RID: 395
	[AttributeUsage(AttributeTargets.Field)]
	internal class RightGuidAttribute : Attribute
	{
		// Token: 0x06000CC4 RID: 3268 RVA: 0x00027C16 File Offset: 0x00025E16
		public RightGuidAttribute(string guid)
		{
			this.Guid = new Guid(guid);
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x00027C2A File Offset: 0x00025E2A
		// (set) Token: 0x06000CC6 RID: 3270 RVA: 0x00027C32 File Offset: 0x00025E32
		public Guid Guid { get; private set; }
	}
}
