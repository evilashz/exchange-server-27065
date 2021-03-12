using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.OData.Web;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E85 RID: 3717
	internal class PropertyDefinition : IEquatable<PropertyDefinition>
	{
		// Token: 0x060060B1 RID: 24753 RVA: 0x0012E340 File Offset: 0x0012C540
		public PropertyDefinition(string name, Type type)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("name", name);
			ArgumentValidator.ThrowIfNull("type", type);
			this.Name = name;
			this.Type = type;
		}

		// Token: 0x17001642 RID: 5698
		// (get) Token: 0x060060B2 RID: 24754 RVA: 0x0012E36C File Offset: 0x0012C56C
		// (set) Token: 0x060060B3 RID: 24755 RVA: 0x0012E374 File Offset: 0x0012C574
		public string Name { get; private set; }

		// Token: 0x17001643 RID: 5699
		// (get) Token: 0x060060B4 RID: 24756 RVA: 0x0012E37D File Offset: 0x0012C57D
		// (set) Token: 0x060060B5 RID: 24757 RVA: 0x0012E385 File Offset: 0x0012C585
		public Type Type { get; private set; }

		// Token: 0x17001644 RID: 5700
		// (get) Token: 0x060060B6 RID: 24758 RVA: 0x0012E38E File Offset: 0x0012C58E
		// (set) Token: 0x060060B7 RID: 24759 RVA: 0x0012E396 File Offset: 0x0012C596
		public IEdmTypeReference EdmType { get; set; }

		// Token: 0x17001645 RID: 5701
		// (get) Token: 0x060060B8 RID: 24760 RVA: 0x0012E39F File Offset: 0x0012C59F
		// (set) Token: 0x060060B9 RID: 24761 RVA: 0x0012E3A7 File Offset: 0x0012C5A7
		public PropertyDefinitionFlags Flags { get; set; }

		// Token: 0x17001646 RID: 5702
		// (get) Token: 0x060060BA RID: 24762 RVA: 0x0012E3B0 File Offset: 0x0012C5B0
		// (set) Token: 0x060060BB RID: 24763 RVA: 0x0012E3B8 File Offset: 0x0012C5B8
		public object DefaultValue { get; set; }

		// Token: 0x17001647 RID: 5703
		// (get) Token: 0x060060BC RID: 24764 RVA: 0x0012E3C1 File Offset: 0x0012C5C1
		// (set) Token: 0x060060BD RID: 24765 RVA: 0x0012E3C9 File Offset: 0x0012C5C9
		public PropertyProvider EwsPropertyProvider { get; set; }

		// Token: 0x17001648 RID: 5704
		// (get) Token: 0x060060BE RID: 24766 RVA: 0x0012E3D2 File Offset: 0x0012C5D2
		// (set) Token: 0x060060BF RID: 24767 RVA: 0x0012E3DA File Offset: 0x0012C5DA
		public PropertyProvider ADDriverPropertyProvider { get; set; }

		// Token: 0x17001649 RID: 5705
		// (get) Token: 0x060060C0 RID: 24768 RVA: 0x0012E3E3 File Offset: 0x0012C5E3
		// (set) Token: 0x060060C1 RID: 24769 RVA: 0x0012E3EB File Offset: 0x0012C5EB
		public PropertyProvider DataEntityPropertyProvider { get; set; }

		// Token: 0x1700164A RID: 5706
		// (get) Token: 0x060060C2 RID: 24770 RVA: 0x0012E3F4 File Offset: 0x0012C5F4
		// (set) Token: 0x060060C3 RID: 24771 RVA: 0x0012E3FC File Offset: 0x0012C5FC
		public IODataPropertyValueConverter ODataPropertyValueConverter { get; set; }

		// Token: 0x1700164B RID: 5707
		// (get) Token: 0x060060C4 RID: 24772 RVA: 0x0012E405 File Offset: 0x0012C605
		// (set) Token: 0x060060C5 RID: 24773 RVA: 0x0012E40D File Offset: 0x0012C60D
		public IEdmEntityType NavigationTargetEntity { get; set; }

		// Token: 0x1700164C RID: 5708
		// (get) Token: 0x060060C6 RID: 24774 RVA: 0x0012E416 File Offset: 0x0012C616
		public bool IsNavigation
		{
			get
			{
				return this.Flags.HasFlag(PropertyDefinitionFlags.Navigation);
			}
		}

		// Token: 0x060060C7 RID: 24775 RVA: 0x0012E42E File Offset: 0x0012C62E
		public bool Equals(PropertyDefinition other)
		{
			return other != null && string.Equals(this.Name, other.Name);
		}

		// Token: 0x060060C8 RID: 24776 RVA: 0x0012E446 File Offset: 0x0012C646
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x060060C9 RID: 24777 RVA: 0x0012E44E File Offset: 0x0012C64E
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x060060CA RID: 24778 RVA: 0x0012E45C File Offset: 0x0012C65C
		public void Validate(Type entitySchemaType)
		{
			ArgumentValidator.ThrowIfNull("entitySchemaType", entitySchemaType);
			if (this.IsNavigation)
			{
				if (this.NavigationTargetEntity == null)
				{
					throw new ArgumentException(string.Format("Navigation property definition {0}.{1} requires NavigationTargetEntity.", entitySchemaType.FullName, this.Name));
				}
			}
			else
			{
				if (this.EdmType == null)
				{
					throw new ArgumentException(string.Format("Non-navigation property definition {0}.{1} requires EdmType.", entitySchemaType.FullName, this.Name));
				}
				if (this.ODataPropertyValueConverter == null && (this.EdmType is EdmCollectionTypeReference || this.EdmType is EdmComplexTypeReference))
				{
					throw new ArgumentException(string.Format("Non-primitive property definition {0}.{1} requires ODataPropertyValueConverter.", entitySchemaType.FullName, this.Name));
				}
				if (this.ADDriverPropertyProvider == null && this.DataEntityPropertyProvider == null && this.EwsPropertyProvider == null)
				{
					throw new ArgumentException(string.Format("At least one of the property providers should be assigned to property definition {0}.{1}.", entitySchemaType.FullName, this.Name));
				}
			}
		}
	}
}
