using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000F8 RID: 248
	[AttributeUsage(AttributeTargets.Property)]
	public class DDIDataObjectNameExistAttribute : DDIValidateAttribute
	{
		// Token: 0x06000943 RID: 2371 RVA: 0x00020632 File Offset: 0x0001E832
		public DDIDataObjectNameExistAttribute() : base("DDIDataObjectNameExistAttribute")
		{
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x00020660 File Offset: 0x0001E860
		public override List<string> Validate(object target, PageConfigurableProfile profile)
		{
			if (target != null && !(target is string))
			{
				throw new ArgumentException("DDIDataObjectNameExistAttribute can only apply to String property");
			}
			string dataObjectName = target as string;
			List<string> list = new List<string>();
			if (!string.IsNullOrEmpty(dataObjectName) && profile.DataObjectProfiles.All((DataObjectProfile dataObject) => !dataObject.Name.Equals(dataObjectName)))
			{
				list.Add(string.Format("{0} is not a valid data object name", target));
			}
			return list;
		}
	}
}
