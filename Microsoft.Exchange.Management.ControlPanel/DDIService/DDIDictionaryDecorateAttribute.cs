using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200016B RID: 363
	public class DDIDictionaryDecorateAttribute : DDICollectionDecoratorAttribute
	{
		// Token: 0x17001A8D RID: 6797
		// (get) Token: 0x06002211 RID: 8721 RVA: 0x00066C00 File Offset: 0x00064E00
		// (set) Token: 0x06002212 RID: 8722 RVA: 0x00066C08 File Offset: 0x00064E08
		[DefaultValue(true)]
		public bool UseKeys { get; set; }

		// Token: 0x06002213 RID: 8723 RVA: 0x00066C11 File Offset: 0x00064E11
		public DDIDictionaryDecorateAttribute()
		{
			this.UseKeys = true;
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x00066C20 File Offset: 0x00064E20
		public override List<string> Validate(object target, Service profile)
		{
			IEnumerable target2 = null;
			if (target != null)
			{
				IDictionary dictionary = target as IDictionary;
				if (dictionary == null)
				{
					throw new ArgumentException("DDIDictionaryDecorateAttribute can only be applied to type which implemented the IDictionary interface");
				}
				target2 = (this.UseKeys ? dictionary.Keys : dictionary.Values);
			}
			return base.Validate(target2, profile);
		}
	}
}
