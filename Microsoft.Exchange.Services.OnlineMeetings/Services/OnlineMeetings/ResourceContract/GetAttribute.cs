using System;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000040 RID: 64
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	internal class GetAttribute : HttpMethodAttribute
	{
		// Token: 0x0600022F RID: 559 RVA: 0x00007D18 File Offset: 0x00005F18
		public GetAttribute(Type outputType) : base("Get")
		{
			this.outputType = outputType;
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00007D2C File Offset: 0x00005F2C
		public Type OutputType
		{
			get
			{
				return this.outputType;
			}
		}

		// Token: 0x04000172 RID: 370
		private readonly Type outputType;
	}
}
