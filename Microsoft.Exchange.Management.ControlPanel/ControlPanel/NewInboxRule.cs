using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003FE RID: 1022
	[DataContract]
	public class NewInboxRule : InboxRuleParameters
	{
		// Token: 0x0600349A RID: 13466 RVA: 0x000A3790 File Offset: 0x000A1990
		public NewInboxRule()
		{
			this.OnDeserializing(default(StreamingContext));
		}

		// Token: 0x0600349B RID: 13467 RVA: 0x000A37B2 File Offset: 0x000A19B2
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
		}

		// Token: 0x170020A6 RID: 8358
		// (get) Token: 0x0600349C RID: 13468 RVA: 0x000A37B4 File Offset: 0x000A19B4
		public sealed override string AssociatedCmdlet
		{
			get
			{
				return "New-InboxRule";
			}
		}

		// Token: 0x170020A7 RID: 8359
		// (get) Token: 0x0600349D RID: 13469 RVA: 0x000A37BB File Offset: 0x000A19BB
		// (set) Token: 0x0600349E RID: 13470 RVA: 0x000A37CD File Offset: 0x000A19CD
		public Identity FromMessageId
		{
			get
			{
				return Identity.FromIdParameter(base["FromMessageId"]);
			}
			set
			{
				base["FromMessageId"] = value.ToIdParameter();
			}
		}

		// Token: 0x170020A8 RID: 8360
		// (get) Token: 0x0600349F RID: 13471 RVA: 0x000A37E0 File Offset: 0x000A19E0
		// (set) Token: 0x060034A0 RID: 13472 RVA: 0x000A37F4 File Offset: 0x000A19F4
		public bool? ValidateOnly
		{
			get
			{
				return (bool?)base["ValidateOnly"];
			}
			set
			{
				base["ValidateOnly"] = (value ?? false);
			}
		}
	}
}
