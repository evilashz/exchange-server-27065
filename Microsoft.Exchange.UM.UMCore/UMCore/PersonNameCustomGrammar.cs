using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001A5 RID: 421
	internal class PersonNameCustomGrammar : PersonCustomGrammar
	{
		// Token: 0x06000C6A RID: 3178 RVA: 0x00035FCD File Offset: 0x000341CD
		internal PersonNameCustomGrammar(CultureInfo transctiptionLanguage, List<ContactInfo> persons) : base(transctiptionLanguage, persons)
		{
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x00035FD7 File Offset: 0x000341D7
		internal override string FileName
		{
			get
			{
				return "ExtPersonName.grxml";
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000C6C RID: 3180 RVA: 0x00035FDE File Offset: 0x000341DE
		internal override string Rule
		{
			get
			{
				return "ExtPersonName";
			}
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x00035FE8 File Offset: 0x000341E8
		protected override List<GrammarItemBase> GetItems(ContactInfo person)
		{
			List<GrammarItemBase> list = new List<GrammarItemBase>(3);
			string tag = string.Format(CultureInfo.InvariantCulture, "out.{0} = \"{1}\";", new object[]
			{
				person.EwsType,
				person.EwsId
			});
			if (!string.IsNullOrEmpty(person.DisplayName))
			{
				ExclusionList instance = ExclusionList.Instance;
				if (instance != null)
				{
					List<Replacement> list2 = null;
					switch (instance.GetReplacementStrings(person.DisplayName, RecipientType.Contact, out list2))
					{
					case MatchResult.None:
					case MatchResult.MatchWithNoReplacements:
					case MatchResult.NotFound:
						goto IL_FC;
					case MatchResult.NoMatch:
						list.Add(new GrammarItem(person.DisplayName, tag, base.TranscriptionLanguage));
						goto IL_FC;
					case MatchResult.MatchWithReplacements:
						using (List<Replacement>.Enumerator enumerator = list2.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								Replacement replacement = enumerator.Current;
								list.Add(new GrammarItem(replacement.ReplacementString, tag, base.TranscriptionLanguage));
							}
							goto IL_FC;
						}
						break;
					default:
						goto IL_FC;
					}
				}
				list.Add(new GrammarItem(person.DisplayName, tag, base.TranscriptionLanguage));
			}
			IL_FC:
			if (!string.IsNullOrEmpty(person.FirstName))
			{
				list.Add(new GrammarItem(person.FirstName, tag, base.TranscriptionLanguage));
			}
			if (!string.IsNullOrEmpty(person.LastName))
			{
				list.Add(new GrammarItem(person.LastName, tag, base.TranscriptionLanguage));
			}
			return list;
		}
	}
}
