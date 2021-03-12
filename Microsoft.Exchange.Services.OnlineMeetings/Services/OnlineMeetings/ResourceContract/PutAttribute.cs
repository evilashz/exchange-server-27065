using System;
using System.Net;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000049 RID: 73
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	internal class PutAttribute : HttpMethodAttribute
	{
		// Token: 0x06000254 RID: 596 RVA: 0x00007F24 File Offset: 0x00006124
		public PutAttribute(Type inputType) : base("Put")
		{
			this.inputType = inputType;
			base.StatusCode = HttpStatusCode.OK;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00007F43 File Offset: 0x00006143
		public PutAttribute(Type inputType, Type outputType) : base("Post")
		{
			this.inputType = inputType;
			this.outputType = outputType;
			base.StatusCode = HttpStatusCode.OK;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00007F69 File Offset: 0x00006169
		public Type InputType
		{
			get
			{
				return this.inputType;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00007F71 File Offset: 0x00006171
		public Type OutputType
		{
			get
			{
				return this.outputType;
			}
		}

		// Token: 0x04000182 RID: 386
		private readonly Type inputType;

		// Token: 0x04000183 RID: 387
		private readonly Type outputType;
	}
}
