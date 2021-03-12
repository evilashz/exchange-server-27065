using System;
using System.Net;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000048 RID: 72
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	internal class PostAttribute : HttpMethodAttribute
	{
		// Token: 0x0600024F RID: 591 RVA: 0x00007EB7 File Offset: 0x000060B7
		public PostAttribute() : base("Post")
		{
			base.StatusCode = HttpStatusCode.NoContent;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00007ECF File Offset: 0x000060CF
		public PostAttribute(Type inputType) : base("Post")
		{
			this.inputType = inputType;
			base.StatusCode = HttpStatusCode.NoContent;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00007EEE File Offset: 0x000060EE
		public PostAttribute(Type inputType, Type outputType) : base("Post")
		{
			this.inputType = inputType;
			this.outputType = outputType;
			base.StatusCode = HttpStatusCode.Created;
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00007F14 File Offset: 0x00006114
		public Type InputType
		{
			get
			{
				return this.inputType;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00007F1C File Offset: 0x0000611C
		public Type OutputType
		{
			get
			{
				return this.outputType;
			}
		}

		// Token: 0x04000180 RID: 384
		private readonly Type inputType;

		// Token: 0x04000181 RID: 385
		private readonly Type outputType;
	}
}
