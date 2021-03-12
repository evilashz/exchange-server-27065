using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200016C RID: 364
	public class DDICharSeparatorItemsAttribute : DDICollectionDecoratorAttribute
	{
		// Token: 0x17001A8E RID: 6798
		// (get) Token: 0x06002215 RID: 8725 RVA: 0x00066C66 File Offset: 0x00064E66
		// (set) Token: 0x06002216 RID: 8726 RVA: 0x00066C6E File Offset: 0x00064E6E
		[DefaultValue(" ")]
		public string Separators { get; set; }

		// Token: 0x06002217 RID: 8727 RVA: 0x00066C77 File Offset: 0x00064E77
		public DDICharSeparatorItemsAttribute()
		{
			this.Separators = ", ";
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x00066C8C File Offset: 0x00064E8C
		public override List<string> Validate(object target, Service profile)
		{
			string text = target as string;
			List<string> target2 = new List<string>();
			if (!string.IsNullOrEmpty(text))
			{
				target2 = text.Split(this.Separators.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList<string>();
			}
			return base.Validate(target2, profile);
		}
	}
}
