using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000903 RID: 2307
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ParticipantSet : IEnumerable<IParticipant>, IEnumerable
	{
		// Token: 0x17001834 RID: 6196
		// (get) Token: 0x060056B6 RID: 22198 RVA: 0x00165ADD File Offset: 0x00163CDD
		public int Count
		{
			get
			{
				return this.participantPerEmailAddress.Count;
			}
		}

		// Token: 0x060056B7 RID: 22199 RVA: 0x00165AEC File Offset: 0x00163CEC
		public bool IsSubsetOf(ParticipantSet other)
		{
			if (this.Count > other.Count)
			{
				return false;
			}
			foreach (IParticipant participant in this)
			{
				if (!other.Contains(participant))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060056B8 RID: 22200 RVA: 0x00165B50 File Offset: 0x00163D50
		public void UnionWith(IEnumerable<IParticipant> other)
		{
			foreach (IParticipant participant in other)
			{
				this.Add(participant);
			}
		}

		// Token: 0x060056B9 RID: 22201 RVA: 0x00165B9C File Offset: 0x00163D9C
		public void ExceptWith(IEnumerable<IParticipant> others)
		{
			foreach (IParticipant participant in others)
			{
				if (this.Contains(participant))
				{
					this.Remove(participant);
				}
			}
		}

		// Token: 0x060056BA RID: 22202 RVA: 0x00165BF0 File Offset: 0x00163DF0
		public bool Add(IParticipant participant)
		{
			if (!this.Contains(participant))
			{
				this.participantPerEmailAddress.Add(participant);
				this.participantPerSmtpEmailAddress.Add(participant);
				return true;
			}
			return false;
		}

		// Token: 0x060056BB RID: 22203 RVA: 0x00165C18 File Offset: 0x00163E18
		public bool Contains(IParticipant participant)
		{
			return this.participantPerEmailAddress.Contains(participant) || this.participantPerSmtpEmailAddress.Contains(participant);
		}

		// Token: 0x060056BC RID: 22204 RVA: 0x00165C36 File Offset: 0x00163E36
		public IEnumerator<IParticipant> GetEnumerator()
		{
			return this.participantPerEmailAddress.GetEnumerator();
		}

		// Token: 0x060056BD RID: 22205 RVA: 0x00165C48 File Offset: 0x00163E48
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new InvalidOperationException("ParticipantSet:IEnumerable.GetEnumerator: Uses the generic version");
		}

		// Token: 0x060056BE RID: 22206 RVA: 0x00165C84 File Offset: 0x00163E84
		private void Remove(IParticipant participant)
		{
			IParticipant participant2 = this.participantPerEmailAddress.FirstOrDefault((IParticipant p) => ParticipantComparer.EmailAddress.Equals(p, participant));
			if (participant2 == null)
			{
				participant2 = this.participantPerSmtpEmailAddress.FirstOrDefault((IParticipant p) => ParticipantComparer.SmtpEmailAddress.Equals(p, participant));
			}
			this.participantPerEmailAddress.Remove(participant2);
			this.participantPerSmtpEmailAddress.Remove(participant2);
		}

		// Token: 0x04002E4E RID: 11854
		private HashSet<IParticipant> participantPerEmailAddress = new HashSet<IParticipant>(ParticipantComparer.EmailAddress);

		// Token: 0x04002E4F RID: 11855
		private HashSet<IParticipant> participantPerSmtpEmailAddress = new HashSet<IParticipant>(ParticipantComparer.SmtpEmailAddress);
	}
}
