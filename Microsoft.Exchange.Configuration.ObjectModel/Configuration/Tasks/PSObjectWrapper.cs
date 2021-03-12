using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200006F RID: 111
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PSObjectWrapper : IPropertyBag, IReadOnlyPropertyBag
	{
		// Token: 0x06000464 RID: 1124 RVA: 0x0000FBDC File Offset: 0x0000DDDC
		public PSObjectWrapper(PSObject mshObject)
		{
			this.mshObject = mshObject;
			this.innerPropBag = (mshObject.BaseObject as IPropertyBag);
		}

		// Token: 0x17000112 RID: 274
		public object this[PropertyDefinition propertyDefinition]
		{
			get
			{
				object result = null;
				Type type = propertyDefinition.Type;
				if (((ProviderPropertyDefinition)propertyDefinition).IsMultivalued)
				{
					type = typeof(MultiValuedProperty<>).MakeGenericType(new Type[]
					{
						type
					});
				}
				if (this.innerPropBag != null)
				{
					LanguagePrimitives.TryConvertTo(this.innerPropBag[propertyDefinition], type, out result);
				}
				else if (this.mshObject.Properties[propertyDefinition.Name] != null)
				{
					LanguagePrimitives.TryConvertTo(this.mshObject.Properties[propertyDefinition.Name].Value, type, out result);
				}
				return result;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000FC9E File Offset: 0x0000DE9E
		public object[] GetProperties(ICollection<PropertyDefinition> propertyDefinitionArray)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000FCA5 File Offset: 0x0000DEA5
		public void SetProperties(ICollection<PropertyDefinition> propertyDefinitionArray, object[] propertyValuesArray)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000111 RID: 273
		private PSObject mshObject;

		// Token: 0x04000112 RID: 274
		private IPropertyBag innerPropBag;
	}
}
