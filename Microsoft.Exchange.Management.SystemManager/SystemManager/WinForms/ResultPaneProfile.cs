using System;
using System.ComponentModel;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000D7 RID: 215
	public class ResultPaneProfile : IHasPermission
	{
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x000198AE File Offset: 0x00017AAE
		// (set) Token: 0x06000794 RID: 1940 RVA: 0x000198B6 File Offset: 0x00017AB6
		[TypeConverter(typeof(DDIObjectTypeConverter))]
		public Type Type { get; set; }

		// Token: 0x06000796 RID: 1942 RVA: 0x000198C7 File Offset: 0x00017AC7
		public virtual bool HasPermission()
		{
			return false;
		}
	}
}
