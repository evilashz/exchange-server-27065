using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000085 RID: 133
	internal class FacadeBase : ConfigurablePropertyBag
	{
		// Token: 0x060004DA RID: 1242 RVA: 0x00010604 File Offset: 0x0000E804
		protected FacadeBase(IConfigurable innerObj)
		{
			this.innerObj = innerObj;
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00010613 File Offset: 0x0000E813
		public IConfigurable InnerConfigurable
		{
			get
			{
				return this.innerObj;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x0001061B File Offset: 0x0000E81B
		public IPropertyBag InnerPropertyBag
		{
			get
			{
				return (IPropertyBag)this.InnerConfigurable;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x00010628 File Offset: 0x0000E828
		public override ObjectId Identity
		{
			get
			{
				return this.innerObj.Identity;
			}
		}

		// Token: 0x170001BA RID: 442
		public override object this[PropertyDefinition propertyDefinition]
		{
			get
			{
				return this.InnerPropertyBag[propertyDefinition];
			}
			set
			{
				ConfigurableObject configurableObject = this.InnerPropertyBag as ConfigurableObject;
				if (configurableObject != null)
				{
					DalHelper.SetConfigurableObject(value, propertyDefinition, configurableObject);
					return;
				}
				this.InnerPropertyBag[propertyDefinition] = value;
			}
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00010676 File Offset: 0x0000E876
		public virtual void FixIdPropertiesForWebservice(IConfigDataProvider dataProvider, ADObjectId orgId, bool isServer)
		{
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00010678 File Offset: 0x0000E878
		internal static T NewADObject<T>() where T : ADObject, new()
		{
			T result = Activator.CreateInstance<T>();
			result.ResetChangeTracking(true);
			return result;
		}

		// Token: 0x04000331 RID: 817
		private readonly IConfigurable innerObj;
	}
}
