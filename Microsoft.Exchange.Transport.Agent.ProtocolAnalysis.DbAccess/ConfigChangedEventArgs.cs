using System;
using Microsoft.Exchange.Configuration.Common;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.DbAccess
{
	// Token: 0x02000004 RID: 4
	internal sealed class ConfigChangedEventArgs : EventArgs
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002116 File Offset: 0x00000316
		public ConfigChangedEventArgs(PropertyBag fields)
		{
			this.fields = fields;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002125 File Offset: 0x00000325
		public PropertyBag Fields
		{
			get
			{
				return this.fields;
			}
		}

		// Token: 0x04000001 RID: 1
		private readonly PropertyBag fields;
	}
}
