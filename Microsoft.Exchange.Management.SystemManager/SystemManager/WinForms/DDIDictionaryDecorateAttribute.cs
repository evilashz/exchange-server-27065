using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000F7 RID: 247
	public class DDIDictionaryDecorateAttribute : DDICollectionDecoratorAttribute
	{
		// Token: 0x1700025E RID: 606
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x000205CC File Offset: 0x0001E7CC
		// (set) Token: 0x06000940 RID: 2368 RVA: 0x000205D4 File Offset: 0x0001E7D4
		[DefaultValue(true)]
		public bool UseKeys { get; set; }

		// Token: 0x06000941 RID: 2369 RVA: 0x000205DD File Offset: 0x0001E7DD
		public DDIDictionaryDecorateAttribute()
		{
			this.UseKeys = true;
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x000205EC File Offset: 0x0001E7EC
		public override List<string> Validate(object target, PageConfigurableProfile profile)
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
