using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001A4 RID: 420
	internal class PersonInfoCustomGrammar : PersonCustomGrammar
	{
		// Token: 0x06000C66 RID: 3174 RVA: 0x00035F34 File Offset: 0x00034134
		internal PersonInfoCustomGrammar(CultureInfo transctiptionLanguage, List<ContactInfo> persons) : base(transctiptionLanguage, persons)
		{
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x00035F3E File Offset: 0x0003413E
		internal override string FileName
		{
			get
			{
				return "ExtCallerInfo.grxml";
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x00035F45 File Offset: 0x00034145
		internal override string Rule
		{
			get
			{
				return "ExtCallerInfo";
			}
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x00035F4C File Offset: 0x0003414C
		protected override List<GrammarItemBase> GetItems(ContactInfo person)
		{
			List<GrammarItemBase> list = new List<GrammarItemBase>(3);
			if (!string.IsNullOrEmpty(person.Company))
			{
				list.Add(new GrammarItem(person.Company, base.TranscriptionLanguage));
			}
			if (!string.IsNullOrEmpty(person.City))
			{
				list.Add(new GrammarItem(person.City, base.TranscriptionLanguage));
			}
			if (!string.IsNullOrEmpty(person.Country))
			{
				list.Add(new GrammarItem(person.Country, base.TranscriptionLanguage));
			}
			return list;
		}
	}
}
