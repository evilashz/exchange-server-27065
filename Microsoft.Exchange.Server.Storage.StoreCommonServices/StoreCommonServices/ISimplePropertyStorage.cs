using System;
using System.IO;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000087 RID: 135
	public interface ISimplePropertyStorage : ISimpleReadOnlyPropertyStorage
	{
		// Token: 0x060004E5 RID: 1253
		void SetBlobProperty(Context context, StorePropTag propTag, object value);

		// Token: 0x060004E6 RID: 1254
		void SetPhysicalColumn(Context context, PhysicalColumn column, object value);

		// Token: 0x060004E7 RID: 1255
		ErrorCode OpenPhysicalColumnReadStream(Context context, PhysicalColumn column, out Stream stream);

		// Token: 0x060004E8 RID: 1256
		ErrorCode OpenPhysicalColumnWriteStream(Context context, PhysicalColumn column, out Stream stream);
	}
}
