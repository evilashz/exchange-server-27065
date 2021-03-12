using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000046 RID: 70
	internal class ExactUserComparer : IStringComparer
	{
		// Token: 0x060002C6 RID: 710 RVA: 0x000105AF File Offset: 0x0000E7AF
		private ExactUserComparer()
		{
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x000105B7 File Offset: 0x0000E7B7
		public static ExactUserComparer CreateInstance()
		{
			return ExactUserComparer.instance;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x000105BE File Offset: 0x0000E7BE
		public bool Equals(string userX, string userY)
		{
			return !string.IsNullOrWhiteSpace(userX) && !string.IsNullOrWhiteSpace(userY) && string.Equals(userX, userY, StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x040001CF RID: 463
		private static readonly ExactUserComparer instance = new ExactUserComparer();
	}
}
