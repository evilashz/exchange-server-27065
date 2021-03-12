using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000904 RID: 2308
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ParticipantComparer
	{
		// Token: 0x17001835 RID: 6197
		// (get) Token: 0x060056C0 RID: 22208 RVA: 0x00165D1A File Offset: 0x00163F1A
		public static IEqualityComparer<IParticipant> EmailAddress
		{
			get
			{
				return ParticipantComparer.EmailAddressEqualityComparerImpl.Default;
			}
		}

		// Token: 0x17001836 RID: 6198
		// (get) Token: 0x060056C1 RID: 22209 RVA: 0x00165D21 File Offset: 0x00163F21
		public static IEqualityComparer<IParticipant> SmtpEmailAddress
		{
			get
			{
				return ParticipantComparer.SmtpEmailAddressEqualityComparerImpl.Default;
			}
		}

		// Token: 0x17001837 RID: 6199
		// (get) Token: 0x060056C2 RID: 22210 RVA: 0x00165D28 File Offset: 0x00163F28
		public static IEqualityComparer<Participant> EmailAddressIgnoringRoutingType
		{
			get
			{
				return ParticipantComparer.EmailAddressIgnoringRoutingTypeEqualityComparerImpl.Default;
			}
		}

		// Token: 0x17001838 RID: 6200
		// (get) Token: 0x060056C3 RID: 22211 RVA: 0x00165D2F File Offset: 0x00163F2F
		public static IComparer<Participant> DisplayName
		{
			get
			{
				return ParticipantComparer.DisplayNameComparerImpl.Default;
			}
		}

		// Token: 0x02000905 RID: 2309
		private class EmailAddressIgnoringRoutingTypeEqualityComparerImpl : IEqualityComparer<Participant>
		{
			// Token: 0x060056C4 RID: 22212 RVA: 0x00165D36 File Offset: 0x00163F36
			public bool Equals(Participant x, Participant y)
			{
				return object.ReferenceEquals(x, y) || (x != null && y != null && x.EmailAddressEqualityComparer.Equals(x, y));
			}

			// Token: 0x060056C5 RID: 22213 RVA: 0x00165D64 File Offset: 0x00163F64
			public int GetHashCode(Participant x)
			{
				if (!(x != null))
				{
					return 0;
				}
				return x.EmailAddressEqualityComparer.GetHashCode(x);
			}

			// Token: 0x04002E50 RID: 11856
			public static ParticipantComparer.EmailAddressIgnoringRoutingTypeEqualityComparerImpl Default = new ParticipantComparer.EmailAddressIgnoringRoutingTypeEqualityComparerImpl();
		}

		// Token: 0x02000906 RID: 2310
		private class EmailAddressEqualityComparerImpl : IEqualityComparer<IParticipant>
		{
			// Token: 0x060056C8 RID: 22216 RVA: 0x00165D91 File Offset: 0x00163F91
			public bool Equals(IParticipant x, IParticipant y)
			{
				return object.ReferenceEquals(x, y) || (x != null && y != null && x.RoutingType == y.RoutingType && x.EmailAddressEqualityComparer.Equals(x, y));
			}

			// Token: 0x060056C9 RID: 22217 RVA: 0x00165DC6 File Offset: 0x00163FC6
			public int GetHashCode(IParticipant x)
			{
				if (x == null)
				{
					return 0;
				}
				return x.EmailAddressEqualityComparer.GetHashCode(x);
			}

			// Token: 0x04002E51 RID: 11857
			public static ParticipantComparer.EmailAddressEqualityComparerImpl Default = new ParticipantComparer.EmailAddressEqualityComparerImpl();
		}

		// Token: 0x02000907 RID: 2311
		private class SmtpEmailAddressEqualityComparerImpl : IEqualityComparer<IParticipant>
		{
			// Token: 0x060056CC RID: 22220 RVA: 0x00165DF0 File Offset: 0x00163FF0
			public int GetHashCode(IParticipant x)
			{
				string smtpAddress = this.GetSmtpAddress(x);
				if (smtpAddress == null)
				{
					return 0;
				}
				return StringComparer.OrdinalIgnoreCase.GetHashCode(smtpAddress);
			}

			// Token: 0x060056CD RID: 22221 RVA: 0x00165E15 File Offset: 0x00164015
			private string GetSmtpAddress(IParticipant x)
			{
				if (x == null)
				{
					return null;
				}
				if (x.SmtpEmailAddress != null)
				{
					return x.SmtpEmailAddress;
				}
				if (!(x.RoutingType == "SMTP"))
				{
					return null;
				}
				return x.EmailAddress;
			}

			// Token: 0x060056CE RID: 22222 RVA: 0x00165E45 File Offset: 0x00164045
			public bool Equals(IParticipant x, IParticipant y)
			{
				return object.ReferenceEquals(x, y) || this.CompareSmtpAddress(x, y);
			}

			// Token: 0x060056CF RID: 22223 RVA: 0x00165E5C File Offset: 0x0016405C
			private bool CompareSmtpAddress(IParticipant x, IParticipant y)
			{
				string smtpAddress = this.GetSmtpAddress(x);
				string smtpAddress2 = this.GetSmtpAddress(y);
				return smtpAddress != null && smtpAddress2 != null && SmtpRoutingTypeDriver.SmtpAddressEqualityComparer.Equals(smtpAddress, smtpAddress2);
			}

			// Token: 0x04002E52 RID: 11858
			public static ParticipantComparer.SmtpEmailAddressEqualityComparerImpl Default = new ParticipantComparer.SmtpEmailAddressEqualityComparerImpl();
		}

		// Token: 0x02000908 RID: 2312
		private class DisplayNameComparerImpl : IComparer<Participant>
		{
			// Token: 0x060056D2 RID: 22226 RVA: 0x00165EA1 File Offset: 0x001640A1
			public int Compare(Participant x, Participant y)
			{
				return StringComparer.OrdinalIgnoreCase.Compare(x.DisplayName, y.DisplayName);
			}

			// Token: 0x04002E53 RID: 11859
			public static ParticipantComparer.DisplayNameComparerImpl Default = new ParticipantComparer.DisplayNameComparerImpl();
		}
	}
}
