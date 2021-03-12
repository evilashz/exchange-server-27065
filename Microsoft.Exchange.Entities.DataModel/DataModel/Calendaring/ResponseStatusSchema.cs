using System;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring
{
	// Token: 0x02000079 RID: 121
	public class ResponseStatusSchema : TypeSchema
	{
		// Token: 0x06000353 RID: 851 RVA: 0x000067B9 File Offset: 0x000049B9
		public ResponseStatusSchema()
		{
			base.RegisterPropertyDefinition(ResponseStatusSchema.StaticResponseProperty);
			base.RegisterPropertyDefinition(ResponseStatusSchema.StaticTimeProperty);
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000354 RID: 852 RVA: 0x000067D7 File Offset: 0x000049D7
		public TypedPropertyDefinition<ResponseType> ResponseProperty
		{
			get
			{
				return ResponseStatusSchema.StaticResponseProperty;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000355 RID: 853 RVA: 0x000067DE File Offset: 0x000049DE
		public TypedPropertyDefinition<ExDateTime> TimeProperty
		{
			get
			{
				return ResponseStatusSchema.StaticTimeProperty;
			}
		}

		// Token: 0x0400019B RID: 411
		private static readonly TypedPropertyDefinition<ResponseType> StaticResponseProperty = new TypedPropertyDefinition<ResponseType>("ResponseStatus.Response", ResponseType.None, true);

		// Token: 0x0400019C RID: 412
		private static readonly TypedPropertyDefinition<ExDateTime> StaticTimeProperty = new TypedPropertyDefinition<ExDateTime>("ResponseStatus.Time", default(ExDateTime), true);
	}
}
