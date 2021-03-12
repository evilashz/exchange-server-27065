using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E8D RID: 3725
	internal class GenericPropertyProvider<T> : PropertyProvider
	{
		// Token: 0x06006112 RID: 24850 RVA: 0x0012EBC2 File Offset: 0x0012CDC2
		public override void GetPropertyFromDataSource(Entity entity, PropertyDefinition property, object dataSource)
		{
			ArgumentValidator.ThrowIfNull("entity", entity);
			ArgumentValidator.ThrowIfNull("property", property);
			ArgumentValidator.ThrowIfNull("dataSource", dataSource);
			this.GetProperty(entity, property, (T)((object)dataSource));
		}

		// Token: 0x06006113 RID: 24851 RVA: 0x0012EBF3 File Offset: 0x0012CDF3
		public override void SetPropertyToDataSource(Entity entity, PropertyDefinition property, object dataSource)
		{
			ArgumentValidator.ThrowIfNull("entity", entity);
			ArgumentValidator.ThrowIfNull("property", property);
			ArgumentValidator.ThrowIfNull("dataSource", dataSource);
			this.SetProperty(entity, property, (T)((object)dataSource));
		}

		// Token: 0x1700165B RID: 5723
		// (get) Token: 0x06006114 RID: 24852 RVA: 0x0012EC24 File Offset: 0x0012CE24
		// (set) Token: 0x06006115 RID: 24853 RVA: 0x0012EC2C File Offset: 0x0012CE2C
		public Action<Entity, PropertyDefinition, T> Getter { get; set; }

		// Token: 0x1700165C RID: 5724
		// (get) Token: 0x06006116 RID: 24854 RVA: 0x0012EC35 File Offset: 0x0012CE35
		// (set) Token: 0x06006117 RID: 24855 RVA: 0x0012EC3D File Offset: 0x0012CE3D
		public Action<Entity, PropertyDefinition, T> Setter { get; set; }

		// Token: 0x06006118 RID: 24856 RVA: 0x0012EC46 File Offset: 0x0012CE46
		protected virtual void GetProperty(Entity entity, PropertyDefinition property, T dataObject)
		{
			if (this.Getter != null)
			{
				this.Getter(entity, property, dataObject);
			}
		}

		// Token: 0x06006119 RID: 24857 RVA: 0x0012EC5E File Offset: 0x0012CE5E
		protected virtual void SetProperty(Entity entity, PropertyDefinition property, T dataObject)
		{
			if (this.Setter != null)
			{
				this.Setter(entity, property, dataObject);
			}
		}
	}
}
