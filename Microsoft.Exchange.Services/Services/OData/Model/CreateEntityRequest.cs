using System;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EBB RID: 3771
	internal abstract class CreateEntityRequest<TEntity> : ODataRequest<TEntity> where TEntity : Entity
	{
		// Token: 0x0600621E RID: 25118 RVA: 0x001336AD File Offset: 0x001318AD
		public CreateEntityRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x17001679 RID: 5753
		// (get) Token: 0x0600621F RID: 25119 RVA: 0x001336B6 File Offset: 0x001318B6
		// (set) Token: 0x06006220 RID: 25120 RVA: 0x001336BE File Offset: 0x001318BE
		public TEntity Template { get; protected set; }

		// Token: 0x06006221 RID: 25121 RVA: 0x001336C7 File Offset: 0x001318C7
		public override void LoadFromHttpRequest()
		{
			base.LoadFromHttpRequest();
			this.Template = base.ReadPostBodyAsEntity<TEntity>();
		}

		// Token: 0x06006222 RID: 25122 RVA: 0x001336DC File Offset: 0x001318DC
		public override void Validate()
		{
			base.Validate();
			TEntity template = this.Template;
			foreach (PropertyDefinition propertyDefinition in template.Schema.MandatoryCreationProperties)
			{
				TEntity template2 = this.Template;
				if (!template2.PropertyBag.Contains(propertyDefinition))
				{
					throw new MandatoryPropertyMissingException(propertyDefinition.Name);
				}
			}
		}
	}
}
