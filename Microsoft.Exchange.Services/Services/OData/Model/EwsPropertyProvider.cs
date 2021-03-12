using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E89 RID: 3721
	internal abstract class EwsPropertyProvider : PropertyProvider
	{
		// Token: 0x1700164E RID: 5710
		// (get) Token: 0x060060D8 RID: 24792 RVA: 0x0012E654 File Offset: 0x0012C854
		// (set) Token: 0x060060D9 RID: 24793 RVA: 0x0012E65C File Offset: 0x0012C85C
		public bool IsMultiValueProperty { get; protected set; }

		// Token: 0x060060DA RID: 24794 RVA: 0x0012E665 File Offset: 0x0012C865
		public EwsPropertyProvider(PropertyInformation propertyInformation)
		{
			ArgumentValidator.ThrowIfNull("propertyInformation", propertyInformation);
			this.PropertyInformation = propertyInformation;
		}

		// Token: 0x060060DB RID: 24795 RVA: 0x0012E67F File Offset: 0x0012C87F
		public EwsPropertyProvider(ReadOnlyCollection<PropertyInformation> propertyInformationList)
		{
			ArgumentValidator.ThrowIfNull("propertyInformationList", propertyInformationList);
			this.PropertyInformationList = propertyInformationList;
		}

		// Token: 0x060060DC RID: 24796 RVA: 0x0012E699 File Offset: 0x0012C899
		public override void GetPropertyFromDataSource(Entity entity, PropertyDefinition property, object dataSource)
		{
			ArgumentValidator.ThrowIfNull("entity", entity);
			ArgumentValidator.ThrowIfNull("property", property);
			ArgumentValidator.ThrowIfNull("dataSource", dataSource);
			this.GetProperty(entity, property, (ServiceObject)dataSource);
		}

		// Token: 0x060060DD RID: 24797 RVA: 0x0012E6CA File Offset: 0x0012C8CA
		public override void SetPropertyToDataSource(Entity entity, PropertyDefinition property, object dataSource)
		{
			ArgumentValidator.ThrowIfNull("entity", entity);
			ArgumentValidator.ThrowIfNull("property", property);
			ArgumentValidator.ThrowIfNull("dataSource", dataSource);
			this.SetProperty(entity, property, (ServiceObject)dataSource);
		}

		// Token: 0x1700164F RID: 5711
		// (get) Token: 0x060060DE RID: 24798 RVA: 0x0012E6FB File Offset: 0x0012C8FB
		// (set) Token: 0x060060DF RID: 24799 RVA: 0x0012E703 File Offset: 0x0012C903
		public PropertyInformation PropertyInformation { get; private set; }

		// Token: 0x17001650 RID: 5712
		// (get) Token: 0x060060E0 RID: 24800 RVA: 0x0012E70C File Offset: 0x0012C90C
		// (set) Token: 0x060060E1 RID: 24801 RVA: 0x0012E714 File Offset: 0x0012C914
		public ReadOnlyCollection<PropertyInformation> PropertyInformationList { get; private set; }

		// Token: 0x060060E2 RID: 24802 RVA: 0x0012E720 File Offset: 0x0012C920
		public virtual PropertyUpdate GetPropertyUpdate(ServiceObject ewsObject, object value)
		{
			PropertyUpdate propertyUpdate;
			if (this.IsDeletingProperty(value))
			{
				propertyUpdate = EwsPropertyProvider.DeleteItemPropertyUpdateDelegate(ewsObject);
			}
			else
			{
				propertyUpdate = EwsPropertyProvider.SetItemPropertyUpdateDelegate(ewsObject);
			}
			propertyUpdate.PropertyPath = this.PropertyInformation.PropertyPath;
			return propertyUpdate;
		}

		// Token: 0x060060E3 RID: 24803 RVA: 0x0012E764 File Offset: 0x0012C964
		public virtual List<PropertyUpdate> GetPropertyUpdateList(Entity entity, PropertyDefinition Prop, object value)
		{
			throw new NotImplementedException("GetPropertyUpdateList is not implemented for this property provider");
		}

		// Token: 0x060060E4 RID: 24804 RVA: 0x0012E770 File Offset: 0x0012C970
		public virtual string GetQueryConstant(object value)
		{
			if (value != null)
			{
				return value.ToString();
			}
			return null;
		}

		// Token: 0x060060E5 RID: 24805 RVA: 0x0012E77D File Offset: 0x0012C97D
		protected virtual bool IsDeletingProperty(object value)
		{
			return value == null;
		}

		// Token: 0x060060E6 RID: 24806 RVA: 0x0012E784 File Offset: 0x0012C984
		protected virtual void GetProperty(Entity entity, PropertyDefinition property, ServiceObject ewsObject)
		{
			if (this.PropertyInformation != null)
			{
				if (ewsObject.PropertyBag.Contains(this.PropertyInformation))
				{
					entity[property] = ewsObject[this.PropertyInformation];
					return;
				}
				if (property.EdmType.IsNullable)
				{
					entity[property] = null;
				}
			}
		}

		// Token: 0x060060E7 RID: 24807 RVA: 0x0012E7D5 File Offset: 0x0012C9D5
		protected virtual void SetProperty(Entity entity, PropertyDefinition property, ServiceObject ewsObject)
		{
			if (this.PropertyInformation != null && entity.PropertyBag.Contains(property))
			{
				ewsObject[this.PropertyInformation] = entity[property];
			}
		}

		// Token: 0x04003481 RID: 13441
		public static readonly Func<ServiceObject, PropertyUpdate> SetItemPropertyUpdateDelegate = (ServiceObject s) => new SetItemPropertyUpdate
		{
			Item = (ItemType)s
		};

		// Token: 0x04003482 RID: 13442
		public static readonly Func<ServiceObject, PropertyUpdate> DeleteItemPropertyUpdateDelegate = (ServiceObject s) => new DeleteItemPropertyUpdate();

		// Token: 0x04003483 RID: 13443
		public static readonly Func<ServiceObject, PropertyUpdate> SetFolderPropertyUpdateDelegate = (ServiceObject s) => new SetFolderPropertyUpdate
		{
			Folder = (BaseFolderType)s
		};

		// Token: 0x04003484 RID: 13444
		public static readonly Func<ServiceObject, PropertyUpdate> DeleteFolderPropertyUpdateDelegate = (ServiceObject s) => new DeleteFolderPropertyUpdate();
	}
}
