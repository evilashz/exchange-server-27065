using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000466 RID: 1126
	[DataContract]
	public class NewUMCallAnsweringRule : UMCallAnsweringRuleParameters
	{
		// Token: 0x0600392A RID: 14634 RVA: 0x000AE1AC File Offset: 0x000AC3AC
		public NewUMCallAnsweringRule()
		{
			this.OnDeserializing(default(StreamingContext));
		}

		// Token: 0x0600392B RID: 14635 RVA: 0x000AE1CE File Offset: 0x000AC3CE
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
		}

		// Token: 0x1700229D RID: 8861
		// (get) Token: 0x0600392C RID: 14636 RVA: 0x000AE1D0 File Offset: 0x000AC3D0
		public sealed override string AssociatedCmdlet
		{
			get
			{
				return "New-UMCallAnsweringRule";
			}
		}
	}
}
