using System;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C87 RID: 3207
	[AttributeUsage(AttributeTargets.Method)]
	public class JsonRequestWrapperTypeAttribute : Attribute
	{
		// Token: 0x17001435 RID: 5173
		// (get) Token: 0x06005703 RID: 22275 RVA: 0x00111829 File Offset: 0x0010FA29
		// (set) Token: 0x06005704 RID: 22276 RVA: 0x00111831 File Offset: 0x0010FA31
		public Type Type { get; set; }

		// Token: 0x06005705 RID: 22277 RVA: 0x0011183A File Offset: 0x0010FA3A
		public JsonRequestWrapperTypeAttribute(Type type)
		{
			this.Type = type;
		}
	}
}
