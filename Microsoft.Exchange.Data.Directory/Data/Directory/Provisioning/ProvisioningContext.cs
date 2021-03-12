using System;
using System.Collections;

namespace Microsoft.Exchange.Data.Directory.Provisioning
{
	// Token: 0x02000785 RID: 1925
	internal class ProvisioningContext
	{
		// Token: 0x1700227A RID: 8826
		// (get) Token: 0x06006058 RID: 24664 RVA: 0x00147A0E File Offset: 0x00145C0E
		public string TaskName
		{
			get
			{
				return this.taskName;
			}
		}

		// Token: 0x1700227B RID: 8827
		// (get) Token: 0x06006059 RID: 24665 RVA: 0x00147A16 File Offset: 0x00145C16
		public IDictionary UserSpecifiedParameters
		{
			get
			{
				return this.userSpecifiedParameters;
			}
		}

		// Token: 0x0600605A RID: 24666 RVA: 0x00147A1E File Offset: 0x00145C1E
		public ProvisioningContext(string taskName, IDictionary userSpecifiedParameters)
		{
			this.taskName = taskName;
			this.userSpecifiedParameters = userSpecifiedParameters;
		}

		// Token: 0x040040C6 RID: 16582
		private string taskName;

		// Token: 0x040040C7 RID: 16583
		private IDictionary userSpecifiedParameters;
	}
}
