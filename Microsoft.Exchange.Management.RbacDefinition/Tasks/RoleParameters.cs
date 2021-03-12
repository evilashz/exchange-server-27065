using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000002 RID: 2
	internal class RoleParameters
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public RoleParameters(string[] neededFeatures, string cmdletParameters)
		{
			this.parameters = cmdletParameters;
			this.requiredFeatures = neededFeatures;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020E8 File Offset: 0x000002E8
		public bool TryAddToRoleEntry(List<string> enabledFeatures, StringBuilder sb)
		{
			bool result = false;
			if (enabledFeatures == null)
			{
				result = true;
				if (!string.IsNullOrEmpty(this.parameters))
				{
					sb.Append(",");
					sb.Append(this.parameters);
				}
			}
			else
			{
				foreach (string item in this.requiredFeatures)
				{
					if (enabledFeatures.Contains(item))
					{
						result = true;
						if (!string.IsNullOrEmpty(this.parameters))
						{
							sb.Append(",");
							sb.Append(this.parameters);
							break;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002174 File Offset: 0x00000374
		public bool TryAddToRoleEntryFilteringProhibitedActions(List<string> enabledFeatures, List<string> prohibitedActions, StringBuilder sb)
		{
			if (prohibitedActions == null)
			{
				throw new ArgumentNullException("prohibitedActions");
			}
			IEnumerable<string> source = this.RequiredFeatures.Intersect(prohibitedActions, StringComparer.OrdinalIgnoreCase);
			return source.Count<string>() == 0 && this.TryAddToRoleEntry(enabledFeatures, sb);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000021B3 File Offset: 0x000003B3
		public string[] RequiredFeatures
		{
			get
			{
				return this.requiredFeatures;
			}
		}

		// Token: 0x04000001 RID: 1
		private string[] requiredFeatures;

		// Token: 0x04000002 RID: 2
		private readonly string parameters;
	}
}
