using System;
using System.ComponentModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200066B RID: 1643
	public class StringValuePair : ValuePair
	{
		// Token: 0x17002765 RID: 10085
		// (get) Token: 0x0600474D RID: 18253 RVA: 0x000D84C2 File Offset: 0x000D66C2
		// (set) Token: 0x0600474E RID: 18254 RVA: 0x000D84CA File Offset: 0x000D66CA
		public override object Value
		{
			get
			{
				return this.StringValue;
			}
			set
			{
				throw new NotSupportedException("Please use StringValue property instead");
			}
		}

		// Token: 0x17002766 RID: 10086
		// (get) Token: 0x0600474F RID: 18255 RVA: 0x000D84D6 File Offset: 0x000D66D6
		// (set) Token: 0x06004750 RID: 18256 RVA: 0x000D84DE File Offset: 0x000D66DE
		[DefaultValue("")]
		public string StringValue { get; set; }
	}
}
