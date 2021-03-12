using System;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C88 RID: 3208
	[AttributeUsage(AttributeTargets.Method)]
	public class JsonResponseOptionsAttribute : Attribute
	{
		// Token: 0x17001436 RID: 5174
		// (get) Token: 0x06005706 RID: 22278 RVA: 0x00111849 File Offset: 0x0010FA49
		// (set) Token: 0x06005707 RID: 22279 RVA: 0x00111851 File Offset: 0x0010FA51
		public bool IsCacheable { get; set; }

		// Token: 0x06005708 RID: 22280 RVA: 0x0011185A File Offset: 0x0010FA5A
		public JsonResponseOptionsAttribute()
		{
			this.IsCacheable = false;
		}
	}
}
