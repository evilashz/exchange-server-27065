using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x0200007D RID: 125
	internal class SearchParticipant
	{
		// Token: 0x06000322 RID: 802 RVA: 0x0000A927 File Offset: 0x00008B27
		internal SearchParticipant()
		{
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000A92F File Offset: 0x00008B2F
		internal SearchParticipant(Participant participant)
		{
			this.SmtpAddress = participant.GetValueOrDefault<string>(ParticipantSchema.SmtpAddress);
			this.DisplayName = participant.DisplayName;
			this.SetRoutingType();
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0000A95A File Offset: 0x00008B5A
		// (set) Token: 0x06000325 RID: 805 RVA: 0x0000A962 File Offset: 0x00008B62
		internal string SmtpAddress { get; private set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0000A96B File Offset: 0x00008B6B
		// (set) Token: 0x06000327 RID: 807 RVA: 0x0000A973 File Offset: 0x00008B73
		internal string DisplayName { get; private set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000A97C File Offset: 0x00008B7C
		// (set) Token: 0x06000329 RID: 809 RVA: 0x0000A984 File Offset: 0x00008B84
		internal string RoutingType { get; private set; }

		// Token: 0x0600032A RID: 810 RVA: 0x0000A990 File Offset: 0x00008B90
		public static string ToParticipantString(Participant participant)
		{
			if (participant == null)
			{
				return string.Empty;
			}
			string valueOrDefault = participant.GetValueOrDefault<string>(ParticipantSchema.SmtpAddress);
			string displayName = participant.DisplayName;
			return SearchParticipant.ToParticipantString(valueOrDefault, displayName);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000A9C6 File Offset: 0x00008BC6
		public static string ToParticipantString(string smtpAddress, string displayName)
		{
			if (string.IsNullOrEmpty(smtpAddress) && string.IsNullOrEmpty(displayName))
			{
				return string.Empty;
			}
			return string.Format("{0} {1} {2}", smtpAddress, '|', displayName);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000A9F4 File Offset: 0x00008BF4
		public static SearchParticipant FromParticipantString(string participantString)
		{
			if (string.IsNullOrEmpty(participantString))
			{
				return null;
			}
			string[] array = participantString.Split(SearchParticipant.Delimiters, 2);
			if (array.Length != 2)
			{
				return null;
			}
			SearchParticipant searchParticipant = new SearchParticipant();
			searchParticipant.SmtpAddress = (string.IsNullOrWhiteSpace(array[0]) ? null : array[0].Trim().ToLowerInvariant());
			searchParticipant.DisplayName = (string.IsNullOrWhiteSpace(array[1]) ? null : array[1].Trim());
			searchParticipant.SetRoutingType();
			return searchParticipant;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000AA68 File Offset: 0x00008C68
		public override string ToString()
		{
			return SearchParticipant.ToParticipantString(this.SmtpAddress, this.DisplayName);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000AA7B File Offset: 0x00008C7B
		private void SetRoutingType()
		{
			this.RoutingType = (string.IsNullOrEmpty(this.SmtpAddress) ? null : "SMTP");
		}

		// Token: 0x04000168 RID: 360
		private const string SearchParticipantStringTemplate = "{0} {1} {2}";

		// Token: 0x04000169 RID: 361
		private const char Delimiter = '|';

		// Token: 0x0400016A RID: 362
		private static readonly char[] Delimiters = new char[]
		{
			'|'
		};
	}
}
