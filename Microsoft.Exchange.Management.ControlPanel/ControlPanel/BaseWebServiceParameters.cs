using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200069A RID: 1690
	[DataContract]
	public sealed class BaseWebServiceParameters : WebServiceParameters
	{
		// Token: 0x170027BF RID: 10175
		// (get) Token: 0x0600488F RID: 18575 RVA: 0x000DDF48 File Offset: 0x000DC148
		public override string AssociatedCmdlet
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170027C0 RID: 10176
		// (get) Token: 0x06004890 RID: 18576 RVA: 0x000DDF4F File Offset: 0x000DC14F
		public override string RbacScope
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
