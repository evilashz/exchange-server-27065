using System;
using System.Security.Principal;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000ADF RID: 2783
	public static class SecurityIdentity
	{
		// Token: 0x06003BC1 RID: 15297 RVA: 0x0009973C File Offset: 0x0009793C
		public static SecurityIdentifier GetGroupSecurityIdentifier(Guid mailboxGuid, SecurityIdentity.GroupMailboxMemberType groupMailboxMemberType)
		{
			byte[] array = mailboxGuid.ToByteArray();
			if (array.Length != 16)
			{
				throw new ArgumentException(string.Format("Mailformed mailbox guid {0}", mailboxGuid));
			}
			string sddlForm = string.Concat(new object[]
			{
				"S-1-8-",
				BitConverter.ToUInt32(array, 0),
				"-",
				BitConverter.ToUInt32(array, 4),
				"-",
				BitConverter.ToUInt32(array, 8),
				"-",
				BitConverter.ToUInt32(array, 12),
				"-",
				(int)groupMailboxMemberType
			});
			return new SecurityIdentifier(sddlForm);
		}

		// Token: 0x02000AE0 RID: 2784
		public enum GroupMailboxMemberType
		{
			// Token: 0x0400349F RID: 13471
			Owner,
			// Token: 0x040034A0 RID: 13472
			Member
		}
	}
}
