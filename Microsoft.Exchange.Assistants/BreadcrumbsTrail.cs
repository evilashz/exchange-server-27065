using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000097 RID: 151
	internal sealed class BreadcrumbsTrail : Breadcrumbs<Breadcrumb>
	{
		// Token: 0x06000482 RID: 1154 RVA: 0x0001850F File Offset: 0x0001670F
		public BreadcrumbsTrail(string name, TrailLength length) : base((int)length)
		{
			this.Name = name;
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0001851F File Offset: 0x0001671F
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x00018527 File Offset: 0x00016727
		public string Name { get; private set; }

		// Token: 0x06000485 RID: 1157 RVA: 0x00018530 File Offset: 0x00016730
		public void Drop(string message)
		{
			base.Drop(new Breadcrumb(message));
		}
	}
}
