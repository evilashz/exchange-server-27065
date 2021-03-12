using System;

namespace Microsoft.Exchange.Services.OnlineMeetings
{
	// Token: 0x02000027 RID: 39
	internal static class UcwaTokens
	{
		// Token: 0x06000114 RID: 276 RVA: 0x000056F2 File Offset: 0x000038F2
		internal static string Normalize(string token)
		{
			if (token == null)
			{
				throw new ArgumentNullException("token");
			}
			return token.ToLowerInvariant();
		}

		// Token: 0x0400010E RID: 270
		public const string Options = "scheduled/schedulingoptions";

		// Token: 0x0400010F RID: 271
		public const string Summaries = "scheduled/summaries";

		// Token: 0x04000110 RID: 272
		public const string DefaultValues = "onlinemeetingdefaultvalues";

		// Token: 0x04000111 RID: 273
		public const string MyOnlineMeetings = "myOnlineMeetings";

		// Token: 0x04000112 RID: 274
		public const string OnlineMeetingCustomization = "onlinemeetinginvitationcustomization";

		// Token: 0x04000113 RID: 275
		public const string OnlineMeetingPhoneDialIn = "phonedialininformation";

		// Token: 0x04000114 RID: 276
		public const string OnlineMeetingPolicies = "onlinemeetingpolicies";

		// Token: 0x04000115 RID: 277
		public const string OnlineMeetingEligibleValues = "onlinemeetingeligiblevalues";

		// Token: 0x04000116 RID: 278
		public const string AssignedMeeting = "myassignedonlinemeeting";
	}
}
