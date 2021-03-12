using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000003 RID: 3
	internal class RoleCmdlet
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000021BB File Offset: 0x000003BB
		public RoleCmdlet(string snapin, string theCmdlet, RoleParameters[] cmdletParameters, string theCmdletType)
		{
			this.snapin = snapin;
			this.cmdlet = theCmdlet;
			this.parameters = cmdletParameters;
			this.cmdletType = theCmdletType;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021E0 File Offset: 0x000003E0
		private string GetFormattedCmdletType()
		{
			string a;
			if ((a = this.cmdletType) != null)
			{
				if (a == "c")
				{
					return string.Format("{0},{1},{2}", this.cmdletType, this.cmdlet, this.snapin);
				}
				if (a == "a" || a == "s" || a == "w")
				{
					return string.Format("{0},{1}", this.cmdletType, this.cmdlet);
				}
			}
			throw new ArgumentOutOfRangeException("cmdletType");
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000226C File Offset: 0x0000046C
		public bool TryGenerateRoleEntry(List<string> enabledFeatures, ref string roleEntry)
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			bool flag = false;
			stringBuilder.Append(this.GetFormattedCmdletType());
			foreach (RoleParameters roleParameters in this.parameters)
			{
				flag |= roleParameters.TryAddToRoleEntry(enabledFeatures, stringBuilder);
			}
			if (this.cmdletType == "w")
			{
				flag = true;
			}
			if (flag)
			{
				roleEntry = stringBuilder.ToString();
			}
			return flag;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000022DC File Offset: 0x000004DC
		public bool TryGenerateRoleEntryFilteringProhibitedActions(List<string> enabledFeatures, List<string> prohibitedActions, ref string roleEntry)
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			bool flag = false;
			stringBuilder.Append(this.GetFormattedCmdletType());
			foreach (RoleParameters roleParameters in this.parameters)
			{
				flag |= roleParameters.TryAddToRoleEntryFilteringProhibitedActions(enabledFeatures, prohibitedActions, stringBuilder);
			}
			if (flag)
			{
				roleEntry = stringBuilder.ToString();
			}
			return flag;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000233C File Offset: 0x0000053C
		public List<string> GetRequiredFeaturesForAllParameters()
		{
			List<string> list = new List<string>();
			foreach (RoleParameters roleParameters in this.parameters)
			{
				if (roleParameters.RequiredFeatures != null && roleParameters.RequiredFeatures.Length > 0)
				{
					list.AddRange(roleParameters.RequiredFeatures);
				}
			}
			return list.Distinct(StringComparer.OrdinalIgnoreCase).ToList<string>();
		}

		// Token: 0x04000003 RID: 3
		private RoleParameters[] parameters;

		// Token: 0x04000004 RID: 4
		private readonly string snapin;

		// Token: 0x04000005 RID: 5
		private readonly string cmdlet;

		// Token: 0x04000006 RID: 6
		private readonly string cmdletType;
	}
}
