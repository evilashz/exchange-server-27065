using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000FE RID: 254
	[AttributeUsage(AttributeTargets.Property)]
	public class DDIValidCommandTextAttribute : DDIValidateAttribute
	{
		// Token: 0x06000951 RID: 2385 RVA: 0x00020AD0 File Offset: 0x0001ECD0
		public DDIValidCommandTextAttribute() : base("DDIValidCommandTextAttribute")
		{
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x00020B2C File Offset: 0x0001ED2C
		private void UpdateCmdlets()
		{
			IEnumerable<Assembly> source = from assemb in AppDomain.CurrentDomain.GetAssemblies()
			where CmdletAssemblyHelper.IsCmdletAssembly(assemb.GetName().Name)
			select assemb;
			if (source.Count<Assembly>() == 0)
			{
				throw new ArgumentException("Microsoft.Exchange.Management dll is not loaded");
			}
			DDIValidCommandTextAttribute.cmdlets.Add("New-EdgeSubscription".ToUpper());
			IEnumerable<object> enumerable = from type in DDIValidationHelper.GetAssemblyTypes(source.First<Assembly>())
			where !type.IsAbstract && type.GetCustomAttributes(typeof(CmdletAttribute), false).Count<object>() == 1
			select type.GetCustomAttributes(typeof(CmdletAttribute), false)[0];
			foreach (object obj in enumerable)
			{
				CmdletAttribute cmdletAttribute = (CmdletAttribute)obj;
				DDIValidCommandTextAttribute.cmdlets.Add((cmdletAttribute.VerbName + "-" + cmdletAttribute.NounName).ToUpper());
			}
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x00020C3C File Offset: 0x0001EE3C
		public override List<string> Validate(object target, PageConfigurableProfile profile)
		{
			if (target != null && !(target is string))
			{
				throw new ArgumentException("DDIValidCommandTextAttribute can only be applied to String property");
			}
			if (DDIValidCommandTextAttribute.cmdlets.Count == 0)
			{
				this.UpdateCmdlets();
			}
			List<string> list = new List<string>();
			string text = target as string;
			if (!string.IsNullOrEmpty(text) && !DDIValidCommandTextAttribute.cmdlets.Contains(text.ToUpper()))
			{
				list.Add(string.Format("{0} is not a valid cmdlet name", target));
			}
			return list;
		}

		// Token: 0x0400040C RID: 1036
		internal static List<string> cmdlets = new List<string>();
	}
}
