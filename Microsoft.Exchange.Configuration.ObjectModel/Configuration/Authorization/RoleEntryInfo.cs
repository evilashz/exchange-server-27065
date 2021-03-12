using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x0200022A RID: 554
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RoleEntryInfo
	{
		// Token: 0x060013B9 RID: 5049 RVA: 0x00045EE9 File Offset: 0x000440E9
		internal RoleEntryInfo(RoleEntry roleEntry, ExchangeRoleAssignment roleAssignment)
		{
			this.RoleEntry = roleEntry;
			this.RoleAssignment = roleAssignment;
			this.ScopeSet = null;
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x00045F06 File Offset: 0x00044106
		internal RoleEntryInfo(RoleEntry roleEntry)
		{
			this.RoleEntry = roleEntry;
			this.RoleAssignment = null;
			this.ScopeSet = null;
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x060013BB RID: 5051 RVA: 0x00045F23 File Offset: 0x00044123
		internal static IComparer<RoleEntryInfo> NameComparer
		{
			get
			{
				return RoleEntryInfo.nameRoleEntryInfoComparer;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x060013BC RID: 5052 RVA: 0x00045F2A File Offset: 0x0004412A
		internal static IComparer<RoleEntryInfo> NameAndInstanceHashCodeComparer
		{
			get
			{
				return RoleEntryInfo.nameAndInstanceHashCodeRoleEntryInfoComparer;
			}
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x00045F34 File Offset: 0x00044134
		internal static RoleEntryInfo GetRoleInfoForCmdlet(string cmdletFullName)
		{
			RoleEntry roleEntry;
			if (cmdletFullName.Equals("Impersonate-ExchangeUser", StringComparison.OrdinalIgnoreCase))
			{
				roleEntry = new ApplicationPermissionRoleEntry("a," + cmdletFullName);
			}
			else
			{
				string str = RoleEntryInfo.ConvertToCommaSeparatedCmdletName(cmdletFullName);
				roleEntry = new CmdletRoleEntry("c," + str);
			}
			return new RoleEntryInfo(roleEntry);
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x00045F80 File Offset: 0x00044180
		internal static RoleEntryInfo GetRoleInfoForScript(string scriptName)
		{
			return new RoleEntryInfo(new ScriptRoleEntry("s," + scriptName));
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x00045F97 File Offset: 0x00044197
		internal static RoleEntryInfo GetRoleInfoForWebMethod(string webMethodName)
		{
			return new RoleEntryInfo(new WebServiceRoleEntry("w," + webMethodName));
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x00045FB0 File Offset: 0x000441B0
		internal static bool IsExchangeCmdlet(CmdletRoleEntry cmdletRoleEntry)
		{
			foreach (string value in ExchangeRunspaceConfiguration.ExchangeSnapins)
			{
				if (cmdletRoleEntry.PSSnapinName.Equals(value, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x0004600C File Offset: 0x0004420C
		private static string ConvertToCommaSeparatedCmdletName(string cmdletFullName)
		{
			int num = cmdletFullName.IndexOf('\\');
			string result;
			if (-1 == num)
			{
				result = cmdletFullName;
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder(cmdletFullName.Length);
				stringBuilder.Append(cmdletFullName, 1 + num, cmdletFullName.Length - num - 1);
				stringBuilder.Append(',');
				stringBuilder.Append(cmdletFullName, 0, num);
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x04000539 RID: 1337
		internal readonly RoleEntry RoleEntry;

		// Token: 0x0400053A RID: 1338
		internal readonly ExchangeRoleAssignment RoleAssignment;

		// Token: 0x0400053B RID: 1339
		internal RoleAssignmentScopeSet ScopeSet;

		// Token: 0x0400053C RID: 1340
		private static RoleEntryInfo.NameRoleEntryInfoComparer nameRoleEntryInfoComparer = new RoleEntryInfo.NameRoleEntryInfoComparer();

		// Token: 0x0400053D RID: 1341
		private static RoleEntryInfo.NameAndInstanceHashCodeRoleEntryInfoComparer nameAndInstanceHashCodeRoleEntryInfoComparer = new RoleEntryInfo.NameAndInstanceHashCodeRoleEntryInfoComparer();

		// Token: 0x0200022B RID: 555
		private class NameRoleEntryInfoComparer : IComparer<RoleEntryInfo>
		{
			// Token: 0x060013C3 RID: 5059 RVA: 0x0004607C File Offset: 0x0004427C
			public int Compare(RoleEntryInfo a, RoleEntryInfo b)
			{
				return RoleEntry.CompareRoleEntriesByName(a.RoleEntry, b.RoleEntry);
			}
		}

		// Token: 0x0200022C RID: 556
		private class NameAndInstanceHashCodeRoleEntryInfoComparer : IComparer<RoleEntryInfo>
		{
			// Token: 0x060013C5 RID: 5061 RVA: 0x00046097 File Offset: 0x00044297
			public int Compare(RoleEntryInfo a, RoleEntryInfo b)
			{
				return RoleEntry.CompareRoleEntriesByNameAndInstanceHashCode(a.RoleEntry, b.RoleEntry);
			}
		}
	}
}
