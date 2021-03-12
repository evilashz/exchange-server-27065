using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;

namespace Microsoft.Exchange.Management.CompliancePolicy.LocStrings
{
	// Token: 0x02000014 RID: 20
	internal static class Strings
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x0000373C File Offset: 0x0000193C
		static Strings()
		{
			Strings.stringIDs.Add(978984437U, "ResolvedServer");
			Strings.stringIDs.Add(883701452U, "TaskNotFound");
			Strings.stringIDs.Add(1421563760U, "ResolvedOrg");
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000037B3 File Offset: 0x000019B3
		public static string ResolvedServer
		{
			get
			{
				return Strings.ResourceManager.GetString("ResolvedServer");
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000037C4 File Offset: 0x000019C4
		public static string TaskNotFound
		{
			get
			{
				return Strings.ResourceManager.GetString("TaskNotFound");
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x000037D5 File Offset: 0x000019D5
		public static string ResolvedOrg
		{
			get
			{
				return Strings.ResourceManager.GetString("ResolvedOrg");
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000037E6 File Offset: 0x000019E6
		public static string GetLocalizedString(Strings.IDs key)
		{
			return Strings.ResourceManager.GetString(Strings.stringIDs[(uint)key]);
		}

		// Token: 0x04000049 RID: 73
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(3);

		// Token: 0x0400004A RID: 74
		private static ResourceManager ResourceManager = new ResourceManager("Microsoft.Exchange.Management.CompliancePolicy.LocStrings.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000015 RID: 21
		public enum IDs : uint
		{
			// Token: 0x0400004C RID: 76
			ResolvedServer = 978984437U,
			// Token: 0x0400004D RID: 77
			TaskNotFound = 883701452U,
			// Token: 0x0400004E RID: 78
			ResolvedOrg = 1421563760U
		}
	}
}
