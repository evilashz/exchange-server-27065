using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E8B RID: 3723
	internal class DirectoryPropertyProvider : PropertyProvider
	{
		// Token: 0x06006100 RID: 24832 RVA: 0x0012EA4E File Offset: 0x0012CC4E
		public DirectoryPropertyProvider(ADPropertyDefinition adPropertyDefinition)
		{
			ArgumentValidator.ThrowIfNull("adPropertyDefinition", adPropertyDefinition);
			this.ADPropertyDefinition = adPropertyDefinition;
		}

		// Token: 0x06006101 RID: 24833 RVA: 0x0012EA68 File Offset: 0x0012CC68
		public override void GetPropertyFromDataSource(Entity entity, PropertyDefinition property, object dataSource)
		{
			ArgumentValidator.ThrowIfNull("entity", entity);
			ArgumentValidator.ThrowIfNull("property", property);
			ArgumentValidator.ThrowIfNull("dataSource", dataSource);
			this.GetProperty(entity, property, (ADRawEntry)dataSource);
		}

		// Token: 0x06006102 RID: 24834 RVA: 0x0012EA99 File Offset: 0x0012CC99
		public override void SetPropertyToDataSource(Entity entity, PropertyDefinition property, object dataSource)
		{
			ArgumentValidator.ThrowIfNull("entity", entity);
			ArgumentValidator.ThrowIfNull("property", property);
			ArgumentValidator.ThrowIfNull("dataSource", dataSource);
			this.SetProperty(entity, property, (ADRawEntry)dataSource);
		}

		// Token: 0x17001657 RID: 5719
		// (get) Token: 0x06006103 RID: 24835 RVA: 0x0012EACA File Offset: 0x0012CCCA
		// (set) Token: 0x06006104 RID: 24836 RVA: 0x0012EAD2 File Offset: 0x0012CCD2
		public ADPropertyDefinition ADPropertyDefinition { get; private set; }

		// Token: 0x06006105 RID: 24837 RVA: 0x0012EADB File Offset: 0x0012CCDB
		public virtual object GetQueryConstant(object value)
		{
			return value;
		}

		// Token: 0x06006106 RID: 24838 RVA: 0x0012EADE File Offset: 0x0012CCDE
		protected virtual void GetProperty(Entity entity, PropertyDefinition property, ADRawEntry adObject)
		{
			if (this.ADPropertyDefinition != null)
			{
				entity[property] = adObject[this.ADPropertyDefinition];
			}
		}

		// Token: 0x06006107 RID: 24839 RVA: 0x0012EAFB File Offset: 0x0012CCFB
		protected virtual void SetProperty(Entity entity, PropertyDefinition property, ADRawEntry adObject)
		{
			if (this.ADPropertyDefinition != null)
			{
				adObject[this.ADPropertyDefinition] = entity[property];
			}
		}
	}
}
