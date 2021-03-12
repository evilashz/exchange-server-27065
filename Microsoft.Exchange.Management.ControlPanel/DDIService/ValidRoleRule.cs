using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000181 RID: 385
	internal class ValidRoleRule : IDDIValidator
	{
		// Token: 0x06002250 RID: 8784 RVA: 0x00067B30 File Offset: 0x00065D30
		public List<string> Validate(object target, Service profile)
		{
			List<string> list = new List<string>();
			string text = target as string;
			if (text != null && !text.Equals("NA", StringComparison.OrdinalIgnoreCase))
			{
				try
				{
					foreach (string roleString in text.Split(new char[]
					{
						','
					}))
					{
						foreach (string rbacQuery in RbacPrincipal.SplitRoles(roleString))
						{
							new RbacQuery(rbacQuery);
						}
					}
				}
				catch (ArgumentException ex)
				{
					list.Add(ex.Message);
				}
			}
			return list;
		}
	}
}
