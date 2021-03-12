using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000367 RID: 871
	[DataContract]
	public class UMMailboxPickerObject : RecipientPickerObject
	{
		// Token: 0x06002FF1 RID: 12273 RVA: 0x00091F64 File Offset: 0x00090164
		public UMMailboxPickerObject(ReducedRecipient recipient) : base(recipient)
		{
		}
	}
}
