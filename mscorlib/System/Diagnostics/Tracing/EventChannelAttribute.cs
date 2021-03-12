using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x020003FC RID: 1020
	[AttributeUsage(AttributeTargets.Field)]
	internal class EventChannelAttribute : Attribute
	{
		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06003426 RID: 13350 RVA: 0x000C9CA6 File Offset: 0x000C7EA6
		// (set) Token: 0x06003427 RID: 13351 RVA: 0x000C9CAE File Offset: 0x000C7EAE
		public bool Enabled { get; set; }

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06003428 RID: 13352 RVA: 0x000C9CB7 File Offset: 0x000C7EB7
		// (set) Token: 0x06003429 RID: 13353 RVA: 0x000C9CBF File Offset: 0x000C7EBF
		public EventChannelType EventChannelType { get; set; }
	}
}
