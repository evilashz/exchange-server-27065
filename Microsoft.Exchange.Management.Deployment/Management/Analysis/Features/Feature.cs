using System;

namespace Microsoft.Exchange.Management.Analysis.Features
{
	// Token: 0x0200005D RID: 93
	internal abstract class Feature
	{
		// Token: 0x06000237 RID: 567 RVA: 0x00008281 File Offset: 0x00006481
		public Feature(bool allowsMultiple, bool isInheritable)
		{
			this.AllowsMultiple = allowsMultiple;
			this.IsInheritable = isInheritable;
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00008297 File Offset: 0x00006497
		// (set) Token: 0x06000239 RID: 569 RVA: 0x0000829F File Offset: 0x0000649F
		public bool AllowsMultiple { get; private set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600023A RID: 570 RVA: 0x000082A8 File Offset: 0x000064A8
		// (set) Token: 0x0600023B RID: 571 RVA: 0x000082B0 File Offset: 0x000064B0
		public bool IsInheritable { get; private set; }

		// Token: 0x0600023C RID: 572 RVA: 0x000082BC File Offset: 0x000064BC
		public override string ToString()
		{
			string text = "Feature";
			string text2 = base.GetType().Name;
			if (text2.EndsWith(text, StringComparison.OrdinalIgnoreCase))
			{
				text2 = text2.Remove(text2.Length - text.Length);
			}
			return text2;
		}
	}
}
