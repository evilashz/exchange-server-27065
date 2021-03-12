using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000346 RID: 838
	public static class RecipientObjectResolverExtensions
	{
		// Token: 0x06002F50 RID: 12112 RVA: 0x00090630 File Offset: 0x0008E830
		public static IEnumerable<RecipientObjectResolverRow> ResolveRecipients(this MultiValuedProperty<ADObjectId> identities)
		{
			return RecipientObjectResolver.Instance.ResolveObjects(identities);
		}

		// Token: 0x06002F51 RID: 12113 RVA: 0x0009063D File Offset: 0x0008E83D
		public static IEnumerable<PeopleRecipientObject> ResolveRecipientsForPeoplePicker(this MultiValuedProperty<ADObjectId> identities)
		{
			return RecipientObjectResolver.Instance.ResolvePeople(identities);
		}

		// Token: 0x06002F52 RID: 12114 RVA: 0x00090660 File Offset: 0x0008E860
		public static IEnumerable<string> ResolveRecipientsForSDO(this MultiValuedProperty<ADObjectId> identities, int maxNumber, Func<RecipientObjectResolverRow, string> convert)
		{
			List<string> list = new List<string>();
			if (identities != null)
			{
				IEnumerable<ADObjectId> identities2 = identities.Take(maxNumber);
				IEnumerable<RecipientObjectResolverRow> source = RecipientObjectResolver.Instance.ResolveObjects(identities2);
				list.AddRange(from resolvedRecipient in source
				select convert(resolvedRecipient));
				if (identities.Count > maxNumber)
				{
					list.Add(Strings.EllipsisText);
				}
			}
			return new MultiValuedProperty<string>(list);
		}
	}
}
