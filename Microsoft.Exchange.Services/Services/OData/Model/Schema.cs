using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E50 RID: 3664
	internal abstract class Schema
	{
		// Token: 0x17001550 RID: 5456
		// (get) Token: 0x06005E1E RID: 24094 RVA: 0x0012596E File Offset: 0x00123B6E
		public virtual ReadOnlyCollection<PropertyDefinition> DeclaredProperties
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17001551 RID: 5457
		// (get) Token: 0x06005E1F RID: 24095 RVA: 0x00125975 File Offset: 0x00123B75
		public virtual ReadOnlyCollection<PropertyDefinition> AllProperties
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17001552 RID: 5458
		// (get) Token: 0x06005E20 RID: 24096 RVA: 0x0012597C File Offset: 0x00123B7C
		public virtual ReadOnlyCollection<PropertyDefinition> DefaultProperties
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17001553 RID: 5459
		// (get) Token: 0x06005E21 RID: 24097 RVA: 0x00125983 File Offset: 0x00123B83
		public virtual ReadOnlyCollection<PropertyDefinition> MandatoryCreationProperties
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17001554 RID: 5460
		// (get) Token: 0x06005E22 RID: 24098 RVA: 0x0012598A File Offset: 0x00123B8A
		public virtual EdmEntityType EdmEntityType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06005E23 RID: 24099 RVA: 0x001259B0 File Offset: 0x00123BB0
		public bool TryGetPropertyByName(string name, out PropertyDefinition propertyDefinition)
		{
			ArgumentValidator.ThrowIfNull("name", name);
			propertyDefinition = this.AllProperties.FirstOrDefault((PropertyDefinition x) => string.Equals(x.Name, name, StringComparison.Ordinal));
			return propertyDefinition != null;
		}

		// Token: 0x06005E24 RID: 24100 RVA: 0x001259FC File Offset: 0x00123BFC
		public virtual void RegisterEdmModel(EdmModel model)
		{
			ArgumentValidator.ThrowIfNull("model", model);
			foreach (PropertyDefinition propertyDefinition in this.DeclaredProperties)
			{
				propertyDefinition.Validate(base.GetType());
				if (propertyDefinition.IsNavigation)
				{
					EdmMultiplicity targetMultiplicity = 1;
					if (propertyDefinition.Type.GetInterface(typeof(IEnumerable).Name) != null)
					{
						targetMultiplicity = 3;
					}
					EdmNavigationPropertyInfo edmNavigationPropertyInfo = new EdmNavigationPropertyInfo
					{
						Name = propertyDefinition.Name,
						Target = propertyDefinition.NavigationTargetEntity,
						TargetMultiplicity = targetMultiplicity,
						ContainsTarget = true
					};
					this.EdmEntityType.AddUnidirectionalNavigation(edmNavigationPropertyInfo);
				}
				else
				{
					EdmStructuralProperty edmStructuralProperty = new EdmStructuralProperty(this.EdmEntityType, propertyDefinition.Name, propertyDefinition.EdmType);
					this.EdmEntityType.AddProperty(edmStructuralProperty);
					if (propertyDefinition.Equals(EntitySchema.Id))
					{
						this.EdmEntityType.AddKeys(new IEdmStructuralProperty[]
						{
							edmStructuralProperty
						});
					}
				}
			}
			model.AddElement(this.EdmEntityType);
		}
	}
}
