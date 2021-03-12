using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000104 RID: 260
	[AttributeUsage(AttributeTargets.Property)]
	public class DDIValidPropertyPageNameAttribute : DDIValidateAttribute
	{
		// Token: 0x06000964 RID: 2404 RVA: 0x00020FBC File Offset: 0x0001F1BC
		public DDIValidPropertyPageNameAttribute() : base("DDIValidPropertyPageNameAttribute")
		{
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x00021004 File Offset: 0x0001F204
		private void UpdatePropertyPageNames()
		{
			IEnumerable<Assembly> enumerable = from assem in AppDomain.CurrentDomain.GetAssemblies()
			where assem.GetName().Name == "Microsoft.Exchange.Management.SnapIn.Esm"
			select assem;
			if (enumerable.Count<Assembly>() == 0)
			{
				throw new ArgumentException("Microsoft.Exchange.Management.SnapIn.Esm not loaded");
			}
			foreach (Assembly assembly in enumerable)
			{
				IEnumerable<string> enumerable2 = from type in DDIValidationHelper.GetAssemblyTypes(assembly)
				where !type.IsAbstract && type.IsSubclassOf(typeof(ExchangePropertyPageControl))
				select type.Name;
				foreach (string item in enumerable2)
				{
					DDIValidPropertyPageNameAttribute.propertyPageNames.Add(item);
				}
			}
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0002111C File Offset: 0x0001F31C
		public override List<string> Validate(object target, PageConfigurableProfile profile)
		{
			if (target != null && !(target is string))
			{
				throw new ArgumentException("DDIValidPropertyPageNameAttribute can only be applied to String");
			}
			if (DDIValidPropertyPageNameAttribute.propertyPageNames.Count == 0)
			{
				this.UpdatePropertyPageNames();
			}
			List<string> list = new List<string>();
			if (target != null && !DDIValidPropertyPageNameAttribute.propertyPageNames.Contains(target as string))
			{
				list.Add(target + " is not a valid property page name");
			}
			return list;
		}

		// Token: 0x04000411 RID: 1041
		private static HashSet<string> propertyPageNames = new HashSet<string>();
	}
}
