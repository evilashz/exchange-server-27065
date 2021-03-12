using System;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000045 RID: 69
	[AttributeUsage(AttributeTargets.Class)]
	internal class NamespaceAttribute : Attribute
	{
		// Token: 0x06000247 RID: 583 RVA: 0x00007E34 File Offset: 0x00006034
		public NamespaceAttribute(string name)
		{
			this.name = name;
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00007E43 File Offset: 0x00006043
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x0400017C RID: 380
		private readonly string name;
	}
}
