using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000208 RID: 520
	internal sealed class DistributionListMemberComparerByDisplayName : IComparer<IDistributionListMember>
	{
		// Token: 0x06001430 RID: 5168 RVA: 0x00048CA5 File Offset: 0x00046EA5
		public DistributionListMemberComparerByDisplayName(CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			this.culture = culture;
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x00048CC4 File Offset: 0x00046EC4
		public int Compare(IDistributionListMember member1, IDistributionListMember member2)
		{
			if (member1 == null)
			{
				throw new ArgumentNullException("member1");
			}
			if (member2 == null)
			{
				throw new ArgumentNullException("member2");
			}
			if (member1.Participant == null)
			{
				if (!(member2.Participant == null))
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (member2.Participant == null)
				{
					return 1;
				}
				return string.Compare(member1.Participant.DisplayName, member2.Participant.DisplayName, this.culture, CompareOptions.None);
			}
		}

		// Token: 0x04000AF1 RID: 2801
		private readonly CultureInfo culture;
	}
}
