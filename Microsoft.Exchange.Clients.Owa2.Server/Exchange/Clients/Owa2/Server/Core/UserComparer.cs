using System;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000228 RID: 552
	internal class UserComparer : IStringComparer
	{
		// Token: 0x060014ED RID: 5357 RVA: 0x0004A4F7 File Offset: 0x000486F7
		private UserComparer()
		{
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x0004A4FF File Offset: 0x000486FF
		public static UserComparer CreateInstance()
		{
			return UserComparer.intance;
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x0004A506 File Offset: 0x00048706
		public bool Equals(string userX, string userY)
		{
			if (string.IsNullOrEmpty(userX))
			{
				throw new ArgumentNullException("userX");
			}
			if (string.IsNullOrEmpty(userY))
			{
				throw new ArgumentNullException("userY");
			}
			return userX.Equals(userY, StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x04000B55 RID: 2901
		private static UserComparer intance = new UserComparer();
	}
}
