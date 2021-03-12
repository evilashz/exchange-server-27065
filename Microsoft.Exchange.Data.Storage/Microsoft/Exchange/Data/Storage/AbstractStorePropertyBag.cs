using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002BB RID: 699
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AbstractStorePropertyBag : IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag
	{
		// Token: 0x170008F5 RID: 2293
		public virtual object this[PropertyDefinition propertyDefinition]
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06001D3A RID: 7482 RVA: 0x00085B60 File Offset: 0x00083D60
		public virtual bool IsDirty
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001D3B RID: 7483 RVA: 0x00085B67 File Offset: 0x00083D67
		public virtual void SetProperties(ICollection<PropertyDefinition> propertyDefinitionArray, object[] propertyValuesArray)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001D3C RID: 7484 RVA: 0x00085B6E File Offset: 0x00083D6E
		public virtual object[] GetProperties(ICollection<PropertyDefinition> propertyDefinitionArray)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001D3D RID: 7485 RVA: 0x00085B75 File Offset: 0x00083D75
		public virtual bool IsPropertyDirty(PropertyDefinition propertyDefinition)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001D3E RID: 7486 RVA: 0x00085B7C File Offset: 0x00083D7C
		public virtual void Load()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001D3F RID: 7487 RVA: 0x00085B83 File Offset: 0x00083D83
		public virtual void Load(ICollection<PropertyDefinition> propertyDefinitions)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001D40 RID: 7488 RVA: 0x00085B8A File Offset: 0x00083D8A
		public virtual Stream OpenPropertyStream(PropertyDefinition propertyDefinition, PropertyOpenMode openMode)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001D41 RID: 7489 RVA: 0x00085B91 File Offset: 0x00083D91
		public virtual object TryGetProperty(PropertyDefinition propertyDefinition)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001D42 RID: 7490 RVA: 0x00085B98 File Offset: 0x00083D98
		public virtual void Delete(PropertyDefinition propertyDefinition)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x00085B9F File Offset: 0x00083D9F
		public virtual T GetValueOrDefault<T>(PropertyDefinition propertyDefinition, T defaultValue)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x00085BA6 File Offset: 0x00083DA6
		public virtual void SetOrDeleteProperty(PropertyDefinition propertyDefinition, object propertyValue)
		{
			throw new NotImplementedException();
		}
	}
}
