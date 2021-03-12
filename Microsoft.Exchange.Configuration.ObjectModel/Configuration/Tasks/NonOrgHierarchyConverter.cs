using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200020C RID: 524
	internal class NonOrgHierarchyConverter
	{
		// Token: 0x0600123E RID: 4670 RVA: 0x00039540 File Offset: 0x00037740
		public NonOrgHierarchyConverter(OrganizationId orgHierarchyToIgnore)
		{
			this.orgHierarchyToIgnore = orgHierarchyToIgnore;
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x00039550 File Offset: 0x00037750
		internal bool TryConvertKeyToNonOrgHierarchy(ConvertOutputPropertyEventArgs args, out object convertedValue)
		{
			convertedValue = null;
			ConfigurableObject configurableObject = args.ConfigurableObject;
			PropertyDefinition property = args.Property;
			string propertyInStr = args.PropertyInStr;
			object value = args.Value;
			if (value == null)
			{
				return false;
			}
			if (!PswsKeyProperties.IsKeyProperty(configurableObject, property, propertyInStr))
			{
				return false;
			}
			if (value is INonOrgHierarchy)
			{
				((INonOrgHierarchy)value).OrgHierarchyToIgnore = this.orgHierarchyToIgnore;
				convertedValue = value;
				return true;
			}
			return false;
		}

		// Token: 0x04000462 RID: 1122
		private OrganizationId orgHierarchyToIgnore;
	}
}
