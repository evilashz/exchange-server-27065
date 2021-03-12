using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200016F RID: 367
	[AttributeUsage(AttributeTargets.Property)]
	public class DDIDataObjectNameExistAttribute : DDIValidateAttribute
	{
		// Token: 0x0600221C RID: 8732 RVA: 0x00066CE3 File Offset: 0x00064EE3
		public DDIDataObjectNameExistAttribute() : base("DDIDataObjectNameExistAttribute")
		{
		}

		// Token: 0x0600221D RID: 8733 RVA: 0x00066D10 File Offset: 0x00064F10
		public override List<string> Validate(object target, Service profile)
		{
			if (target != null && !(target is string))
			{
				throw new ArgumentException("DDIDataObjectNameExistAttribute can only apply to String property");
			}
			string dataObjectName = target as string;
			List<string> list = new List<string>();
			if (!string.IsNullOrEmpty(dataObjectName) && profile.DataObjects.All((DataObject dataObject) => !dataObject.Name.Equals(dataObjectName)))
			{
				list.Add(string.Format("{0} is not a valid data object name", target));
			}
			return list;
		}
	}
}
