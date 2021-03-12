using System;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C85 RID: 3205
	[AttributeUsage(AttributeTargets.Method)]
	public class JsonRequestFormatAttribute : Attribute
	{
		// Token: 0x1700142A RID: 5162
		// (get) Token: 0x060056EB RID: 22251 RVA: 0x00111743 File Offset: 0x0010F943
		// (set) Token: 0x060056EC RID: 22252 RVA: 0x0011174B File Offset: 0x0010F94B
		public JsonRequestFormat Format { get; set; }

		// Token: 0x060056ED RID: 22253 RVA: 0x00111754 File Offset: 0x0010F954
		public JsonRequestFormatAttribute()
		{
			this.Format = JsonRequestFormat.HeaderBodyFormat;
		}
	}
}
