using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.PeopleIKnowService
{
	// Token: 0x0200018B RID: 395
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class PersonaComparerByRelevanceScore : IComparer<string>
	{
		// Token: 0x06000F38 RID: 3896 RVA: 0x0003D5A8 File Offset: 0x0003B7A8
		internal PersonaComparerByRelevanceScore(PeopleIKnowGraph peopleIKnowGraph)
		{
			ArgumentValidator.ThrowIfNull("peopleIKnowGraph", peopleIKnowGraph);
			this.emailAddressToRelevanceScoreMapping = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
			RelevantPerson[] relevantPeople = peopleIKnowGraph.RelevantPeople;
			if (relevantPeople != null)
			{
				foreach (RelevantPerson relevantPerson in relevantPeople)
				{
					if (!string.IsNullOrEmpty(relevantPerson.EmailAddress))
					{
						this.emailAddressToRelevanceScoreMapping[relevantPerson.EmailAddress] = relevantPerson.RelevanceScore;
					}
				}
			}
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x0003D618 File Offset: 0x0003B818
		public int Compare(string emailAddress1, string emailAddress2)
		{
			int maxValue;
			if (!this.emailAddressToRelevanceScoreMapping.TryGetValue(emailAddress1, out maxValue))
			{
				maxValue = int.MaxValue;
			}
			int maxValue2;
			if (!this.emailAddressToRelevanceScoreMapping.TryGetValue(emailAddress2, out maxValue2))
			{
				maxValue2 = int.MaxValue;
			}
			return Comparer<int>.Default.Compare(maxValue, maxValue2);
		}

		// Token: 0x04000806 RID: 2054
		internal const int IrrelevantScore = 2147483647;

		// Token: 0x04000807 RID: 2055
		private readonly Dictionary<string, int> emailAddressToRelevanceScoreMapping;
	}
}
