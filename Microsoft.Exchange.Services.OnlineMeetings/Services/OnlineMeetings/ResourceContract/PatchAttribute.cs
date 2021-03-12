using System;
using System.Net;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000047 RID: 71
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	internal class PatchAttribute : HttpMethodAttribute
	{
		// Token: 0x0600024B RID: 587 RVA: 0x00007E62 File Offset: 0x00006062
		public PatchAttribute(Type inputType) : base("Patch")
		{
			this.inputType = inputType;
			base.StatusCode = HttpStatusCode.OK;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00007E81 File Offset: 0x00006081
		public PatchAttribute(Type inputType, Type outputType) : base("Patch")
		{
			this.inputType = inputType;
			this.outputType = outputType;
			base.StatusCode = HttpStatusCode.OK;
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00007EA7 File Offset: 0x000060A7
		public Type InputType
		{
			get
			{
				return this.inputType;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00007EAF File Offset: 0x000060AF
		public Type OutputType
		{
			get
			{
				return this.outputType;
			}
		}

		// Token: 0x0400017E RID: 382
		private readonly Type inputType;

		// Token: 0x0400017F RID: 383
		private readonly Type outputType;
	}
}
