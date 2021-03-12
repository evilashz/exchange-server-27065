using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001A5 RID: 421
	[Serializable]
	public sealed class SharingPolicyDomain
	{
		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x0002C718 File Offset: 0x0002A918
		// (set) Token: 0x06000DBA RID: 3514 RVA: 0x0002C720 File Offset: 0x0002A920
		public string Domain { get; private set; }

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000DBB RID: 3515 RVA: 0x0002C729 File Offset: 0x0002A929
		// (set) Token: 0x06000DBC RID: 3516 RVA: 0x0002C731 File Offset: 0x0002A931
		public SharingPolicyAction Actions { get; private set; }

		// Token: 0x06000DBD RID: 3517 RVA: 0x0002C73A File Offset: 0x0002A93A
		public SharingPolicyDomain(string domain, SharingPolicyAction actions)
		{
			domain = (StringComparer.OrdinalIgnoreCase.Equals(domain, "Anonymous") ? "Anonymous" : domain);
			SharingPolicyDomain.Validate(domain, actions);
			this.Domain = domain;
			this.Actions = actions;
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x0002C774 File Offset: 0x0002A974
		public static SharingPolicyDomain Parse(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			string[] array = input.Split(new char[]
			{
				':'
			});
			if (array == null || array.Length != 2)
			{
				throw new StrongTypeFormatException(DataStrings.SharingPolicyDomainInvalid(input), string.Empty);
			}
			string domain = array[0].Trim();
			SharingPolicyAction actions;
			try
			{
				actions = (SharingPolicyAction)Enum.Parse(typeof(SharingPolicyAction), array[1], true);
			}
			catch (ArgumentException)
			{
				throw new StrongTypeFormatException(DataStrings.SharingPolicyDomainInvalidAction(array[1]), string.Empty);
			}
			return new SharingPolicyDomain(domain, actions);
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x0002C818 File Offset: 0x0002AA18
		public override bool Equals(object otherObject)
		{
			SharingPolicyDomain sharingPolicyDomain = otherObject as SharingPolicyDomain;
			return sharingPolicyDomain != null && this.Actions == sharingPolicyDomain.Actions && StringComparer.OrdinalIgnoreCase.Equals(this.Domain, sharingPolicyDomain.Domain);
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x0002C85C File Offset: 0x0002AA5C
		public override int GetHashCode()
		{
			return this.Domain.GetHashCode();
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0002C869 File Offset: 0x0002AA69
		public override string ToString()
		{
			return this.Domain + ":" + this.Actions.ToString();
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x0002C88C File Offset: 0x0002AA8C
		private static void Validate(string domain, SharingPolicyAction actions)
		{
			if (!SmtpAddress.IsValidDomain(domain) && domain != "*" && domain != "Anonymous")
			{
				throw new StrongTypeFormatException(DataStrings.SharingPolicyDomainInvalidDomain(domain), "Domain");
			}
			if (domain == "Anonymous" && (actions & SharingPolicyAction.ContactsSharing) != (SharingPolicyAction)0)
			{
				throw new StrongTypeFormatException(DataStrings.SharingPolicyDomainInvalidActionForAnonymous(actions.ToString()), "Actions");
			}
			if (domain != "Anonymous" && ((actions & SharingPolicyAction.MeetingFullDetails) == SharingPolicyAction.MeetingFullDetails || (actions & SharingPolicyAction.MeetingLimitedDetails) == SharingPolicyAction.MeetingLimitedDetails || (actions & SharingPolicyAction.MeetingFullDetailsWithAttendees) == SharingPolicyAction.MeetingFullDetailsWithAttendees))
			{
				throw new StrongTypeFormatException(DataStrings.SharingPolicyDomainInvalidActionForDomain(actions.ToString()), "Actions");
			}
		}

		// Token: 0x0400087B RID: 2171
		internal const string Asterisk = "*";

		// Token: 0x0400087C RID: 2172
		internal const string Anonymous = "Anonymous";
	}
}
