using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000414 RID: 1044
	[DataContract]
	public class NewJournalRule : JournalRuleObjectProperties
	{
		// Token: 0x170020DA RID: 8410
		// (get) Token: 0x06003518 RID: 13592 RVA: 0x000A5525 File Offset: 0x000A3725
		public sealed override string AssociatedCmdlet
		{
			get
			{
				return "New-JournalRule";
			}
		}

		// Token: 0x170020DB RID: 8411
		// (get) Token: 0x06003519 RID: 13593 RVA: 0x000A552C File Offset: 0x000A372C
		// (set) Token: 0x0600351A RID: 13594 RVA: 0x000A553E File Offset: 0x000A373E
		[DataMember]
		public bool? Enabled
		{
			get
			{
				return (bool?)base["Enabled"];
			}
			set
			{
				base["Enabled"] = value;
			}
		}
	}
}
