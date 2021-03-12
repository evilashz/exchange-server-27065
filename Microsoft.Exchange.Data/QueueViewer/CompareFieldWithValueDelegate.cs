using System;

namespace Microsoft.Exchange.Data.QueueViewer
{
	// Token: 0x02000272 RID: 626
	// (Invoke) Token: 0x060014E8 RID: 5352
	internal delegate int CompareFieldWithValueDelegate<ObjectType>(ObjectType dataObject, object value) where ObjectType : PagedDataObject;
}
