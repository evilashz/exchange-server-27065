using System;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000388 RID: 904
	internal sealed class EcpFeatureQueryProcessor : RbacQuery.RbacQueryProcessor, INamedQueryProcessor
	{
		// Token: 0x17001F38 RID: 7992
		// (get) Token: 0x06003070 RID: 12400 RVA: 0x00093948 File Offset: 0x00091B48
		// (set) Token: 0x06003071 RID: 12401 RVA: 0x00093950 File Offset: 0x00091B50
		public string Name { get; private set; }

		// Token: 0x06003072 RID: 12402 RVA: 0x00093959 File Offset: 0x00091B59
		public EcpFeatureQueryProcessor(EcpFeature ecpFeature)
		{
			this.ecpFeature = ecpFeature;
			this.Name = ecpFeature.GetName();
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x00093974 File Offset: 0x00091B74
		public sealed override bool? TryIsInRole(ExchangeRunspaceConfiguration rbacConfiguration)
		{
			if (this.descriptor == null)
			{
				this.descriptor = this.ecpFeature.GetFeatureDescriptor();
			}
			bool value = LoginUtil.CheckUrlAccess(this.descriptor.ServerPath);
			return new bool?(value);
		}

		// Token: 0x0400236F RID: 9071
		private readonly EcpFeature ecpFeature;

		// Token: 0x04002370 RID: 9072
		private EcpFeatureDescriptor descriptor;
	}
}
