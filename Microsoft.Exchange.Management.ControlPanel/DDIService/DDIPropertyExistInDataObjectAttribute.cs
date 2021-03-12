using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Management.SystemManager;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000176 RID: 374
	[AttributeUsage(AttributeTargets.Class)]
	public class DDIPropertyExistInDataObjectAttribute : DDIValidateAttribute
	{
		// Token: 0x0600222F RID: 8751 RVA: 0x00067162 File Offset: 0x00065362
		public DDIPropertyExistInDataObjectAttribute() : base("DDIPropertyExistInDataObjectAttribute")
		{
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x000671A0 File Offset: 0x000653A0
		public override List<string> Validate(object target, Service profile)
		{
			List<string> list = new List<string>();
			Variable variable = target as Variable;
			if (variable != null && !variable.PersistWholeObject)
			{
				string mappingProperty = variable.MappingProperty;
				string dataObjectName = variable.DataObjectName;
				if (!string.IsNullOrEmpty(dataObjectName) && !string.IsNullOrEmpty(mappingProperty))
				{
					if (profile.DataObjects.Any((DataObject dataobject) => dataObjectName.Equals(dataobject.Name)))
					{
						Type type = profile.DataObjects.First((DataObject dataobject) => dataObjectName.Equals(dataobject.Name)).Type;
						if (type != null)
						{
							PropertyInfo propertyEx = type.GetPropertyEx(mappingProperty);
							if (propertyEx == null)
							{
								list.Add(string.Format("{0} is not a valid property in data object {1}", mappingProperty, type.FullName));
							}
						}
					}
				}
			}
			return list;
		}
	}
}
