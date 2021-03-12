using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001D2 RID: 466
	[DataContract]
	public abstract class RecipientPickerFilterBase : RecipientFilter
	{
		// Token: 0x17001B80 RID: 7040
		// (get) Token: 0x06002553 RID: 9555 RVA: 0x000724AF File Offset: 0x000706AF
		public override string RbacScope
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17001B81 RID: 7041
		// (get) Token: 0x06002554 RID: 9556 RVA: 0x000724B6 File Offset: 0x000706B6
		protected override RecipientTypeDetails[] RecipientTypeDetailsParam
		{
			get
			{
				return null;
			}
		}
	}
}
