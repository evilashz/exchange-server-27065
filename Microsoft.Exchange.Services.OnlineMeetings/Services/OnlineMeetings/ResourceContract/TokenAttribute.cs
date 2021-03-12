using System;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x0200004A RID: 74
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
	internal class TokenAttribute : Attribute
	{
		// Token: 0x06000258 RID: 600 RVA: 0x00007F79 File Offset: 0x00006179
		public TokenAttribute(string name)
		{
			this.name = name;
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00007F88 File Offset: 0x00006188
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000184 RID: 388
		private readonly string name;
	}
}
