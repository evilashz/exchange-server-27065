using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000085 RID: 133
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ProvisioningDataFactory
	{
		// Token: 0x0600078C RID: 1932 RVA: 0x00022B70 File Offset: 0x00020D70
		internal static IProvisioningData Create(ProvisioningType type)
		{
			switch (type)
			{
			case ProvisioningType.User:
				return new UserProvisioningData();
			case ProvisioningType.Contact:
				return new ContactProvisioningData();
			case ProvisioningType.Group:
				return new GroupProvisioningData();
			case ProvisioningType.GroupMember:
				return new MemberProvisioningData();
			case ProvisioningType.UserUpdate:
				return new UserUpdateProvisioningData();
			case ProvisioningType.ContactUpdate:
				return new ContactUpdateProvisioningData();
			case ProvisioningType.MailEnabledUser:
				return new MailEnabledUserProvisioningData();
			case ProvisioningType.MailEnabledUserUpdate:
				return new MailEnabledUserUpdateProvisioningData();
			case ProvisioningType.XO1User:
				return new XO1UserProvisioningData();
			default:
				throw new NotSupportedException(type.ToString());
			}
		}
	}
}
