using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000FC RID: 252
	[AttributeUsage(AttributeTargets.Class)]
	public class DDIPropertyExistInDataObjectAttribute : DDIValidateAttribute
	{
		// Token: 0x0600094D RID: 2381 RVA: 0x00020958 File Offset: 0x0001EB58
		public DDIPropertyExistInDataObjectAttribute() : base("DDIPropertyExistInDataObjectAttribute")
		{
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00020994 File Offset: 0x0001EB94
		public override List<string> Validate(object target, PageConfigurableProfile profile)
		{
			List<string> list = new List<string>();
			ColumnProfile columnProfile = target as ColumnProfile;
			if (columnProfile != null && !columnProfile.PersistWholeObject)
			{
				string mappingProperty = columnProfile.MappingProperty;
				string dataObjectName = columnProfile.DataObjectName;
				if (!string.IsNullOrEmpty(dataObjectName) && !string.IsNullOrEmpty(mappingProperty))
				{
					if (profile.DataObjectProfiles.Any((DataObjectProfile dataobject) => dataObjectName.Equals(dataobject.Name)))
					{
						Type type = profile.DataObjectProfiles.First((DataObjectProfile dataobject) => dataObjectName.Equals(dataobject.Name)).Type;
						PropertyInfo property = type.GetProperty(mappingProperty, BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
						if (property == null)
						{
							list.Add(string.Format("{0} is not a valid property in data object {1}", mappingProperty, type.FullName));
						}
					}
				}
			}
			return list;
		}
	}
}
