using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x020003F9 RID: 1017
	[AttributeUsage(AttributeTargets.Class)]
	[__DynamicallyInvokable]
	public sealed class EventSourceAttribute : Attribute
	{
		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06003408 RID: 13320 RVA: 0x000C9B8D File Offset: 0x000C7D8D
		// (set) Token: 0x06003409 RID: 13321 RVA: 0x000C9B95 File Offset: 0x000C7D95
		[__DynamicallyInvokable]
		public string Name { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x0600340A RID: 13322 RVA: 0x000C9B9E File Offset: 0x000C7D9E
		// (set) Token: 0x0600340B RID: 13323 RVA: 0x000C9BA6 File Offset: 0x000C7DA6
		[__DynamicallyInvokable]
		public string Guid { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x0600340C RID: 13324 RVA: 0x000C9BAF File Offset: 0x000C7DAF
		// (set) Token: 0x0600340D RID: 13325 RVA: 0x000C9BB7 File Offset: 0x000C7DB7
		[__DynamicallyInvokable]
		public string LocalizationResources { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

		// Token: 0x0600340E RID: 13326 RVA: 0x000C9BC0 File Offset: 0x000C7DC0
		[__DynamicallyInvokable]
		public EventSourceAttribute()
		{
		}
	}
}
