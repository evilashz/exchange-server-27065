using System;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.OData.Core;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E42 RID: 3650
	internal abstract class ServiceModel
	{
		// Token: 0x06005E05 RID: 24069 RVA: 0x00125476 File Offset: 0x00123676
		public ServiceModel()
		{
			this.EdmModel = this.BuildModel();
		}

		// Token: 0x17001549 RID: 5449
		// (get) Token: 0x06005E06 RID: 24070 RVA: 0x0012548A File Offset: 0x0012368A
		// (set) Token: 0x06005E07 RID: 24071 RVA: 0x00125492 File Offset: 0x00123692
		public IEdmModel EdmModel { get; private set; }

		// Token: 0x1700154A RID: 5450
		// (get) Token: 0x06005E08 RID: 24072 RVA: 0x0012549B File Offset: 0x0012369B
		public virtual ODataVersion ProtocolVersion
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06005E09 RID: 24073
		protected abstract EdmModel BuildModel();

		// Token: 0x040032BB RID: 12987
		public static readonly LazyMember<ServiceModel> Version1Model = new LazyMember<ServiceModel>(() => new Version1Model());
	}
}
