﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000179 RID: 377
	[AttributeUsage(AttributeTargets.Property)]
	public class DDIValidCodeBehindMethodAttribute : DDIValidateAttribute
	{
		// Token: 0x06002237 RID: 8759 RVA: 0x0006737C File Offset: 0x0006557C
		public DDIValidCodeBehindMethodAttribute() : base("DDIValidCodeBehindMethodAttribute")
		{
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x000673A4 File Offset: 0x000655A4
		public override List<string> Validate(object target, Service profile)
		{
			if (target != null && !(target is string))
			{
				throw new ArgumentException("DDIValidCodeBehindMethodAttribute can only be applied to String property");
			}
			List<string> list = new List<string>();
			string methodName = target as string;
			if (!string.IsNullOrEmpty(methodName))
			{
				Type @class = profile.Class;
				if (@class == null)
				{
					list.Add(string.Format("Code behind method {0} is used, but the code hehind class in not specified.", methodName));
				}
				else if ((from c in profile.Class.GetMethods()
				where c.Name == methodName
				select c).Count<MethodInfo>() != 1)
				{
					list.Add(string.Format("Code behind method {0} was NOT found or defined multiple times in class {1}.", methodName, @class));
				}
				else
				{
					MethodInfo method = profile.Class.GetMethod(methodName, new Type[]
					{
						typeof(DataRow),
						typeof(DataTable),
						typeof(DataObjectStore)
					});
					if (method == null)
					{
						method = profile.Class.GetMethod(methodName, new Type[]
						{
							typeof(DataRow),
							typeof(DataTable),
							typeof(DataObjectStore),
							typeof(PowerShellResults[])
						});
						if (method == null)
						{
							throw new NotImplementedException("The specified method " + methodName + " should implement one of the two signatures: (DataRow, DataTable, DataObjectStore) or (DataRow, DataTable, DataObjectStore, PowerShellResults[]) .");
						}
					}
				}
			}
			return list;
		}
	}
}
