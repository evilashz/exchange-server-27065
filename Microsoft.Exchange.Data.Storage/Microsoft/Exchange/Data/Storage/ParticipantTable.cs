using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020008E5 RID: 2277
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ParticipantTable
	{
		// Token: 0x06005561 RID: 21857 RVA: 0x001622CB File Offset: 0x001604CB
		public ParticipantTable()
		{
			this.data = new Dictionary<RecipientItemType, ParticipantSet>();
		}

		// Token: 0x06005562 RID: 21858 RVA: 0x001622E0 File Offset: 0x001604E0
		public ParticipantTable(IDictionary<RecipientItemType, IEnumerable<IParticipant>> otherRecipientTable) : this()
		{
			foreach (KeyValuePair<RecipientItemType, IEnumerable<IParticipant>> keyValuePair in otherRecipientTable)
			{
				this.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x170017D5 RID: 6101
		public ParticipantSet this[RecipientItemType type]
		{
			get
			{
				return this.GetOrCreateParticipantHash(type);
			}
			set
			{
				this.Add(type, value);
			}
		}

		// Token: 0x06005565 RID: 21861 RVA: 0x00162350 File Offset: 0x00160550
		public Dictionary<RecipientItemType, HashSet<IParticipant>> ToDictionary()
		{
			Dictionary<RecipientItemType, HashSet<IParticipant>> dictionary = new Dictionary<RecipientItemType, HashSet<IParticipant>>();
			foreach (KeyValuePair<RecipientItemType, ParticipantSet> keyValuePair in this.data)
			{
				dictionary[keyValuePair.Key] = new HashSet<IParticipant>(keyValuePair.Value);
			}
			return dictionary;
		}

		// Token: 0x06005566 RID: 21862 RVA: 0x001623BC File Offset: 0x001605BC
		public Dictionary<RecipientItemType, ParticipantSet> ToParticipantSet()
		{
			return this.data;
		}

		// Token: 0x06005567 RID: 21863 RVA: 0x001623CD File Offset: 0x001605CD
		public List<IParticipant> ToList()
		{
			return this.data.SelectMany((KeyValuePair<RecipientItemType, ParticipantSet> d) => d.Value).ToList<IParticipant>();
		}

		// Token: 0x06005568 RID: 21864 RVA: 0x001623FC File Offset: 0x001605FC
		public void Add(RecipientItemType type, params IParticipant[] participants)
		{
			this.Add(type, participants.AsEnumerable<IParticipant>());
		}

		// Token: 0x06005569 RID: 21865 RVA: 0x0016240C File Offset: 0x0016060C
		public void Add(RecipientItemType type, IEnumerable<IParticipant> participants)
		{
			ParticipantSet orCreateParticipantHash = this.GetOrCreateParticipantHash(type);
			orCreateParticipantHash.UnionWith(participants);
		}

		// Token: 0x170017D6 RID: 6102
		// (get) Token: 0x0600556A RID: 21866 RVA: 0x00162428 File Offset: 0x00160628
		public bool Any
		{
			get
			{
				foreach (KeyValuePair<RecipientItemType, ParticipantSet> keyValuePair in this.data)
				{
					if (keyValuePair.Value.Any<IParticipant>())
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x0600556B RID: 21867 RVA: 0x0016248C File Offset: 0x0016068C
		private ParticipantSet GetOrCreateParticipantHash(RecipientItemType type)
		{
			ParticipantSet participantSet;
			if (!this.data.TryGetValue(type, out participantSet))
			{
				participantSet = new ParticipantSet();
				this.data.Add(type, participantSet);
			}
			return participantSet;
		}

		// Token: 0x04002DDC RID: 11740
		private Dictionary<RecipientItemType, ParticipantSet> data;
	}
}
