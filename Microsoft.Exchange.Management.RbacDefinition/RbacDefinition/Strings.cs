using System;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.RbacDefinition
{
	// Token: 0x0200002E RID: 46
	internal static class Strings
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00049380 File Offset: 0x00047580
		public static LocalizedString ExOrgReadAdminSGroupNotFoundException(Guid guid)
		{
			return new LocalizedString("ExOrgReadAdminSGroupNotFoundException", Strings.ResourceManager, new object[]
			{
				guid
			});
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000493B0 File Offset: 0x000475B0
		public static LocalizedString ExOrgAdminSGroupNotFoundException(Guid guid)
		{
			return new LocalizedString("ExOrgAdminSGroupNotFoundException", Strings.ResourceManager, new object[]
			{
				guid
			});
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000493E0 File Offset: 0x000475E0
		public static LocalizedString ExPublicFolderAdminSGroupNotFoundException(Guid guid)
		{
			return new LocalizedString("ExPublicFolderAdminSGroupNotFoundException", Strings.ResourceManager, new object[]
			{
				guid
			});
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00049410 File Offset: 0x00047610
		public static LocalizedString ExMailboxAdminSGroupNotFoundException(Guid guid)
		{
			return new LocalizedString("ExMailboxAdminSGroupNotFoundException", Strings.ResourceManager, new object[]
			{
				guid
			});
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00049440 File Offset: 0x00047640
		public static LocalizedString ExRbacRoleGroupNotFoundException(Guid guid, string groupName)
		{
			return new LocalizedString("ExRbacRoleGroupNotFoundException", Strings.ResourceManager, new object[]
			{
				guid,
				groupName
			});
		}

		// Token: 0x0400004F RID: 79
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Management.RbacDefinition.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x0200002F RID: 47
		private enum ParamIDs
		{
			// Token: 0x04000051 RID: 81
			ExOrgReadAdminSGroupNotFoundException,
			// Token: 0x04000052 RID: 82
			ExOrgAdminSGroupNotFoundException,
			// Token: 0x04000053 RID: 83
			ExPublicFolderAdminSGroupNotFoundException,
			// Token: 0x04000054 RID: 84
			ExMailboxAdminSGroupNotFoundException,
			// Token: 0x04000055 RID: 85
			ExRbacRoleGroupNotFoundException
		}
	}
}
