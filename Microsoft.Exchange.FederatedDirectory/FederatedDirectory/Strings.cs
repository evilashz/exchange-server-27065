using System;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.FederatedDirectory
{
	// Token: 0x02000018 RID: 24
	internal static class Strings
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x00005AD0 File Offset: 0x00003CD0
		public static LocalizedString FailedToAddPendingMember(string smtpAddress, string error)
		{
			return new LocalizedString("FailedToAddPendingMember", Strings.ResourceManager, new object[]
			{
				smtpAddress,
				error
			});
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005AFC File Offset: 0x00003CFC
		public static LocalizedString FailedToRemoveMember(string smtpAddress, string error)
		{
			return new LocalizedString("FailedToRemoveMember", Strings.ResourceManager, new object[]
			{
				smtpAddress,
				error
			});
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005B28 File Offset: 0x00003D28
		public static LocalizedString GroupMailboxFailedUpdate(string group, string error)
		{
			return new LocalizedString("GroupMailboxFailedUpdate", Strings.ResourceManager, new object[]
			{
				group,
				error
			});
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005B54 File Offset: 0x00003D54
		public static LocalizedString FailedToAddOwner(string smtpAddress, string error)
		{
			return new LocalizedString("FailedToAddOwner", Strings.ResourceManager, new object[]
			{
				smtpAddress,
				error
			});
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005B80 File Offset: 0x00003D80
		public static LocalizedString FailedToRemoveOwner(string smtpAddress, string error)
		{
			return new LocalizedString("FailedToRemoveOwner", Strings.ResourceManager, new object[]
			{
				smtpAddress,
				error
			});
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005BAC File Offset: 0x00003DAC
		public static LocalizedString FailedToAddMember(string smtpAddress, string error)
		{
			return new LocalizedString("FailedToAddMember", Strings.ResourceManager, new object[]
			{
				smtpAddress,
				error
			});
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005BD8 File Offset: 0x00003DD8
		public static LocalizedString FailedToRemovePendingMember(string smtpAddress, string error)
		{
			return new LocalizedString("FailedToRemovePendingMember", Strings.ResourceManager, new object[]
			{
				smtpAddress,
				error
			});
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005C04 File Offset: 0x00003E04
		public static LocalizedString GroupMailboxFailedCreate(string group, string error)
		{
			return new LocalizedString("GroupMailboxFailedCreate", Strings.ResourceManager, new object[]
			{
				group,
				error
			});
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005C30 File Offset: 0x00003E30
		public static LocalizedString PartiallyFailedToUpdateGroup(string group)
		{
			return new LocalizedString("PartiallyFailedToUpdateGroup", Strings.ResourceManager, new object[]
			{
				group
			});
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005C58 File Offset: 0x00003E58
		public static LocalizedString GroupMailboxFailedDelete(string group, string error)
		{
			return new LocalizedString("GroupMailboxFailedDelete", Strings.ResourceManager, new object[]
			{
				group,
				error
			});
		}

		// Token: 0x04000086 RID: 134
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.FederatedDirectory.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000019 RID: 25
		private enum ParamIDs
		{
			// Token: 0x04000088 RID: 136
			FailedToAddPendingMember,
			// Token: 0x04000089 RID: 137
			FailedToRemoveMember,
			// Token: 0x0400008A RID: 138
			GroupMailboxFailedUpdate,
			// Token: 0x0400008B RID: 139
			FailedToAddOwner,
			// Token: 0x0400008C RID: 140
			FailedToRemoveOwner,
			// Token: 0x0400008D RID: 141
			FailedToAddMember,
			// Token: 0x0400008E RID: 142
			FailedToRemovePendingMember,
			// Token: 0x0400008F RID: 143
			GroupMailboxFailedCreate,
			// Token: 0x04000090 RID: 144
			PartiallyFailedToUpdateGroup,
			// Token: 0x04000091 RID: 145
			GroupMailboxFailedDelete
		}
	}
}
