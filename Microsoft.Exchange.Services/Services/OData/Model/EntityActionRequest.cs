using System;
using Microsoft.Exchange.Services.OData.Web;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EB8 RID: 3768
	internal abstract class EntityActionRequest<TEntity> : CustomActionRequest where TEntity : Entity
	{
		// Token: 0x06006211 RID: 25105 RVA: 0x00133573 File Offset: 0x00131773
		public EntityActionRequest(ODataContext odataContext) : base(odataContext)
		{
		}

		// Token: 0x17001676 RID: 5750
		// (get) Token: 0x06006212 RID: 25106 RVA: 0x0013357C File Offset: 0x0013177C
		// (set) Token: 0x06006213 RID: 25107 RVA: 0x00133584 File Offset: 0x00131784
		public string Id { get; protected set; }

		// Token: 0x06006214 RID: 25108 RVA: 0x00133590 File Offset: 0x00131790
		public override void LoadFromHttpRequest()
		{
			base.LoadFromHttpRequest();
			if (base.ODataContext.ODataPath.ParentOfEntitySegment is KeySegment)
			{
				this.Id = base.ODataContext.ODataPath.ParentOfEntitySegment.GetIdKey();
				return;
			}
			if (base.ODataContext.ODataPath.ParentOfEntitySegment is NavigationPropertySegment)
			{
				this.Id = base.ODataContext.ODataPath.ParentOfEntitySegment.GetPropertyName();
			}
		}

		// Token: 0x06006215 RID: 25109 RVA: 0x00133608 File Offset: 0x00131808
		public override void Validate()
		{
			base.Validate();
			ValidationHelper.ValidateIdEmpty(this.Id);
		}
	}
}
