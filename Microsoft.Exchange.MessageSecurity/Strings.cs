using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MessageSecurity
{
	// Token: 0x02000020 RID: 32
	internal static class Strings
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x0000597C File Offset: 0x00003B7C
		static Strings()
		{
			Strings.stringIDs.Add(470791283U, "NoConfigAdminRoleObjectFound");
			Strings.stringIDs.Add(1274848177U, "NoRootFound");
			Strings.stringIDs.Add(698953728U, "MoreThanOneRootFound");
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000059F3 File Offset: 0x00003BF3
		public static LocalizedString NoConfigAdminRoleObjectFound
		{
			get
			{
				return new LocalizedString("NoConfigAdminRoleObjectFound", "ExEE2DC4", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00005A14 File Offset: 0x00003C14
		public static LocalizedString NoDirectoryServiceObjectsFound(string containerDn)
		{
			return new LocalizedString("NoDirectoryServiceObjectsFound", "Ex4DE9DC", false, true, Strings.ResourceManager, new object[]
			{
				containerDn
			});
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00005A43 File Offset: 0x00003C43
		public static LocalizedString NoRootFound
		{
			get
			{
				return new LocalizedString("NoRootFound", "Ex1E271C", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00005A61 File Offset: 0x00003C61
		public static LocalizedString MoreThanOneRootFound
		{
			get
			{
				return new LocalizedString("MoreThanOneRootFound", "ExD91B34", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00005A7F File Offset: 0x00003C7F
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x0400008E RID: 142
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(3);

		// Token: 0x0400008F RID: 143
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.MessageSecurity.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000021 RID: 33
		public enum IDs : uint
		{
			// Token: 0x04000091 RID: 145
			NoConfigAdminRoleObjectFound = 470791283U,
			// Token: 0x04000092 RID: 146
			NoRootFound = 1274848177U,
			// Token: 0x04000093 RID: 147
			MoreThanOneRootFound = 698953728U
		}

		// Token: 0x02000022 RID: 34
		private enum ParamIDs
		{
			// Token: 0x04000095 RID: 149
			NoDirectoryServiceObjectsFound
		}
	}
}
