using System;

namespace Microsoft.Exchange.Data.QueueViewer
{
	// Token: 0x02000271 RID: 625
	// (Invoke) Token: 0x060014E4 RID: 5348
	internal delegate bool MatchFieldWithTextDelegate<ObjectType>(ObjectType dataObject, object matchPattern, MatchOptions matchOptions) where ObjectType : PagedDataObject;
}
