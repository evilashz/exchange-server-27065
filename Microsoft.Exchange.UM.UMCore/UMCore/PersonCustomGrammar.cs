using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001A3 RID: 419
	internal abstract class PersonCustomGrammar : CustomGrammarBase
	{
		// Token: 0x06000C63 RID: 3171 RVA: 0x00035EBA File Offset: 0x000340BA
		protected PersonCustomGrammar(CultureInfo transctiptionLanguage, List<ContactInfo> persons) : base(transctiptionLanguage)
		{
			this.persons = persons;
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x00035ECC File Offset: 0x000340CC
		protected override List<GrammarItemBase> GetItems()
		{
			List<GrammarItemBase> list = new List<GrammarItemBase>();
			foreach (ContactInfo contactInfo in this.persons)
			{
				if (!(contactInfo is DefaultContactInfo))
				{
					list.AddRange(this.GetItems(contactInfo));
				}
			}
			return list;
		}

		// Token: 0x06000C65 RID: 3173
		protected abstract List<GrammarItemBase> GetItems(ContactInfo person);

		// Token: 0x04000A2D RID: 2605
		private List<ContactInfo> persons;
	}
}
