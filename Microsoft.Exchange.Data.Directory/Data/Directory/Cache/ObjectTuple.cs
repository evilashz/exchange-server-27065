using System;

namespace Microsoft.Exchange.Data.Directory.Cache
{
	// Token: 0x0200008D RID: 141
	internal class ObjectTuple
	{
		// Token: 0x06000717 RID: 1815 RVA: 0x00025DC6 File Offset: 0x00023FC6
		public ObjectTuple(ObjectType objType, SimpleADObject adObject)
		{
			this.ObjType = objType;
			this.ADObject = adObject;
		}

		// Token: 0x040002AE RID: 686
		public ObjectType ObjType;

		// Token: 0x040002AF RID: 687
		public SimpleADObject ADObject;
	}
}
