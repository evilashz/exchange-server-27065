using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.ApplicationLogic.PeopleIKnowService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000904 RID: 2308
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ModernGroupMemberComparerByRelevanceScore : IComparer<ModernGroupMemberType>
	{
		// Token: 0x06004303 RID: 17155 RVA: 0x000DF73C File Offset: 0x000DD93C
		public ModernGroupMemberComparerByRelevanceScore(string serializedPeopleIKnowGraph, ITracer tracer)
		{
			IPeopleIKnowSerializerFactory serializerFactory = new PeopleIKnowSerializerFactory();
			IPeopleIKnowServiceFactory peopleIKnowServiceFactory = new PeopleIKnowServiceFactory(serializerFactory);
			IPeopleIKnowService peopleIKnowService = peopleIKnowServiceFactory.CreatePeopleIKnowService(tracer);
			this.personaComparerByRelevanceScore = peopleIKnowService.GetRelevancyComparer(serializedPeopleIKnowGraph);
		}

		// Token: 0x06004304 RID: 17156 RVA: 0x000DF774 File Offset: 0x000DD974
		public int Compare(ModernGroupMemberType member1, ModernGroupMemberType member2)
		{
			bool flag = false;
			bool flag2 = false;
			if (member1 == null || member1.Persona == null || member1.Persona.EmailAddress == null)
			{
				flag = true;
			}
			if (member2 == null || member2.Persona == null || member2.Persona.EmailAddress == null)
			{
				flag2 = true;
			}
			if (flag && flag2)
			{
				return 0;
			}
			if (flag)
			{
				return 1;
			}
			if (flag2)
			{
				return -1;
			}
			return this.personaComparerByRelevanceScore.Compare(member1.Persona.EmailAddress.EmailAddress, member2.Persona.EmailAddress.EmailAddress);
		}

		// Token: 0x0400270E RID: 9998
		private readonly IComparer<string> personaComparerByRelevanceScore;
	}
}
