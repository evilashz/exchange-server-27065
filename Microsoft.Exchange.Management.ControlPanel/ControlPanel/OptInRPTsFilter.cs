using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000270 RID: 624
	[DataContract]
	public class OptInRPTsFilter : AllAssociatedRPTsFilter
	{
		// Token: 0x06002994 RID: 10644 RVA: 0x00082D5C File Offset: 0x00080F5C
		public OptInRPTsFilter()
		{
			this.OnDeserializing(default(StreamingContext));
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x00082D7E File Offset: 0x00080F7E
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			base["OptionalInMailbox"] = true;
		}

		// Token: 0x040020DB RID: 8411
		public new const string RbacParameters = "?Types&Mailbox&OptionalInMailbox";
	}
}
