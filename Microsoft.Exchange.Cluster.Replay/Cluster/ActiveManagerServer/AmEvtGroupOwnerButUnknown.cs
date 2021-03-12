using System;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000080 RID: 128
	internal class AmEvtGroupOwnerButUnknown : AmEvtBase
	{
		// Token: 0x06000521 RID: 1313 RVA: 0x0001ABF5 File Offset: 0x00018DF5
		internal AmEvtGroupOwnerButUnknown()
		{
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0001ABFD File Offset: 0x00018DFD
		public override string ToString()
		{
			return string.Format("{0}: Params: (<none>)", base.GetType().Name);
		}
	}
}
