using System;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring
{
	// Token: 0x02000077 RID: 119
	public class ResponseStatus : SchematizedObject<ResponseStatusSchema>
	{
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00006697 File Offset: 0x00004897
		// (set) Token: 0x0600034A RID: 842 RVA: 0x000066AA File Offset: 0x000048AA
		public ResponseType Response
		{
			get
			{
				return base.GetPropertyValueOrDefault<ResponseType>(base.Schema.ResponseProperty);
			}
			set
			{
				base.SetPropertyValue<ResponseType>(base.Schema.ResponseProperty, value);
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600034B RID: 843 RVA: 0x000066BE File Offset: 0x000048BE
		// (set) Token: 0x0600034C RID: 844 RVA: 0x000066D1 File Offset: 0x000048D1
		public ExDateTime Time
		{
			get
			{
				return base.GetPropertyValueOrDefault<ExDateTime>(base.Schema.TimeProperty);
			}
			set
			{
				base.SetPropertyValue<ExDateTime>(base.Schema.TimeProperty, value);
			}
		}

		// Token: 0x02000078 RID: 120
		public static class Accessors
		{
			// Token: 0x04000195 RID: 405
			public static readonly EntityPropertyAccessor<ResponseStatus, ResponseType> Response = new EntityPropertyAccessor<ResponseStatus, ResponseType>(SchematizedObject<ResponseStatusSchema>.SchemaInstance.ResponseProperty, (ResponseStatus status) => status.Response, delegate(ResponseStatus status, ResponseType responseType)
			{
				status.Response = responseType;
			});

			// Token: 0x04000196 RID: 406
			public static readonly EntityPropertyAccessor<ResponseStatus, ExDateTime> Time = new EntityPropertyAccessor<ResponseStatus, ExDateTime>(SchematizedObject<ResponseStatusSchema>.SchemaInstance.TimeProperty, (ResponseStatus status) => status.Time, delegate(ResponseStatus status, ExDateTime time)
			{
				status.Time = time;
			});
		}
	}
}
