using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200018D RID: 397
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class OwaEventObjectIdAttribute : Attribute
	{
		// Token: 0x06000E8E RID: 3726 RVA: 0x0005C95C File Offset: 0x0005AB5C
		public OwaEventObjectIdAttribute(Type objectIdType)
		{
			if (objectIdType == null)
			{
				throw new ArgumentNullException("objectIdType");
			}
			if (!objectIdType.IsSubclassOf(typeof(ObjectId)))
			{
				throw new ArgumentException("objectIdType is not subclass of Microsoft.Exchange.Data.ObjectId, it is type: " + objectIdType.ToString());
			}
			this.objectIdType = objectIdType;
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000E8F RID: 3727 RVA: 0x0005C9B2 File Offset: 0x0005ABB2
		public Type ObjectIdType
		{
			get
			{
				return this.objectIdType;
			}
		}

		// Token: 0x040009F2 RID: 2546
		private Type objectIdType;
	}
}
