using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200027C RID: 636
	internal class GetTaskPiiRedactionModule : PiiRedactionModuleBase
	{
		// Token: 0x060015E9 RID: 5609 RVA: 0x00051DB8 File Offset: 0x0004FFB8
		public GetTaskPiiRedactionModule(TaskContext context) : base(context)
		{
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x00051DC1 File Offset: 0x0004FFC1
		public override void Dispose()
		{
			if (this.piiSuppressionContext != null)
			{
				this.piiSuppressionContext.Dispose();
				this.piiSuppressionContext = null;
			}
			base.Dispose();
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x00051DE3 File Offset: 0x0004FFE3
		public override bool WriteObject(object input, out object output)
		{
			if (this.piiSuppressionContext != null)
			{
				this.piiSuppressionContext.Dispose();
			}
			this.piiSuppressionContext = base.CreatePiiSuppressionContext(input as IConfigurable);
			output = input;
			return true;
		}

		// Token: 0x040006B1 RID: 1713
		private IDisposable piiSuppressionContext;
	}
}
