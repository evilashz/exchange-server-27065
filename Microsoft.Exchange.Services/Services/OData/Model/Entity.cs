using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E4F RID: 3663
	internal abstract class Entity
	{
		// Token: 0x1700154B RID: 5451
		// (get) Token: 0x06005E13 RID: 24083 RVA: 0x00125838 File Offset: 0x00123A38
		internal PropertyBag PropertyBag
		{
			get
			{
				return this.propertyBag;
			}
		}

		// Token: 0x1700154C RID: 5452
		// (get) Token: 0x06005E14 RID: 24084 RVA: 0x00125840 File Offset: 0x00123A40
		internal virtual EntitySchema Schema
		{
			get
			{
				return EntitySchema.SchemaInstance;
			}
		}

		// Token: 0x1700154D RID: 5453
		internal object this[PropertyDefinition propertyDefinition]
		{
			get
			{
				ArgumentValidator.ThrowIfNull("propertyDefinition", propertyDefinition);
				return this.PropertyBag[propertyDefinition];
			}
			set
			{
				ArgumentValidator.ThrowIfNull("propertyDefinition", propertyDefinition);
				this.PropertyBag[propertyDefinition] = value;
			}
		}

		// Token: 0x1700154E RID: 5454
		// (get) Token: 0x06005E17 RID: 24087 RVA: 0x0012587A File Offset: 0x00123A7A
		// (set) Token: 0x06005E18 RID: 24088 RVA: 0x0012588C File Offset: 0x00123A8C
		public string Id
		{
			get
			{
				return (string)this[EntitySchema.Id];
			}
			set
			{
				this[EntitySchema.Id] = value;
			}
		}

		// Token: 0x06005E19 RID: 24089 RVA: 0x0012589C File Offset: 0x00123A9C
		internal virtual Uri GetWebUri(ODataContext odataContext)
		{
			ArgumentValidator.ThrowIfNull("odataContext", odataContext);
			if (this.Id == null)
			{
				return null;
			}
			string uriString = string.Format("{0}Users('{1}')/{2}('{3}')", new object[]
			{
				odataContext.HttpContext.GetServiceRootUri(),
				odataContext.TargetMailbox.PrimarySmtpAddress,
				this.UserRootNavigationName,
				this.Id
			});
			return new Uri(uriString);
		}

		// Token: 0x1700154F RID: 5455
		// (get) Token: 0x06005E1A RID: 24090 RVA: 0x0012590A File Offset: 0x00123B0A
		protected virtual string UserRootNavigationName
		{
			get
			{
				return string.Format("{0}s", this.Schema.EdmEntityType.Name);
			}
		}

		// Token: 0x040032F5 RID: 13045
		internal static readonly EdmEntityType EdmEntityType = new EdmEntityType(typeof(Entity).Namespace, typeof(Entity).Name, null, true, false);

		// Token: 0x040032F6 RID: 13046
		private PropertyBag propertyBag = new PropertyBag();
	}
}
