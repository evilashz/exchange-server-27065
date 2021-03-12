using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x020000A5 RID: 165
	internal abstract class QueryableObjectImplBase<T> : QueryableObject where T : SimpleProviderObjectSchema
	{
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x0001A263 File Offset: 0x00018463
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return QueryableObjectImplBase<T>.schema;
			}
		}

		// Token: 0x040002F4 RID: 756
		private static readonly ObjectSchema schema = ObjectSchema.GetInstance(typeof(T));
	}
}
