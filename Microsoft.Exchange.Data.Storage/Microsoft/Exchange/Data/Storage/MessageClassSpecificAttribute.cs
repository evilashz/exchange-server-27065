using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C88 RID: 3208
	[AttributeUsage(AttributeTargets.Field)]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class MessageClassSpecificAttribute : Attribute
	{
		// Token: 0x06007056 RID: 28758 RVA: 0x001F1A22 File Offset: 0x001EFC22
		public MessageClassSpecificAttribute()
		{
			this.messageClass = string.Empty;
		}

		// Token: 0x06007057 RID: 28759 RVA: 0x001F1A35 File Offset: 0x001EFC35
		public MessageClassSpecificAttribute(string messageClass)
		{
			this.messageClass = messageClass;
		}

		// Token: 0x17001E31 RID: 7729
		// (get) Token: 0x06007058 RID: 28760 RVA: 0x001F1A44 File Offset: 0x001EFC44
		public string MessageClass
		{
			get
			{
				return this.messageClass;
			}
		}

		// Token: 0x04004D7D RID: 19837
		private string messageClass;
	}
}
