using System;

namespace Microsoft.Exchange.Data.QueueViewer
{
	// Token: 0x02000273 RID: 627
	// (Invoke) Token: 0x060014EC RID: 5356
	internal delegate int CompareFieldWithFieldDelegate<ObjectType>(ObjectType dataObject1, ObjectType dataObject2) where ObjectType : PagedDataObject;
}
