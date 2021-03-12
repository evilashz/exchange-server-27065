using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200041E RID: 1054
	[DataContract]
	public class MessageCategoryFilter : WebServiceParameters
	{
		// Token: 0x170020EC RID: 8428
		// (get) Token: 0x0600354D RID: 13645 RVA: 0x000A5997 File Offset: 0x000A3B97
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-MessageCategory";
			}
		}

		// Token: 0x170020ED RID: 8429
		// (get) Token: 0x0600354E RID: 13646 RVA: 0x000A599E File Offset: 0x000A3B9E
		public override string RbacScope
		{
			get
			{
				return "@R:Self";
			}
		}
	}
}
