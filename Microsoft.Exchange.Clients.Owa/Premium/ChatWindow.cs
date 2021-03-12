using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000444 RID: 1092
	public class ChatWindow : OwaForm, IRegistryOnlyForm
	{
		// Token: 0x0600275E RID: 10078 RVA: 0x000E0532 File Offset: 0x000DE732
		public ChatWindow() : base(false)
		{
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x0600275F RID: 10079 RVA: 0x000E0546 File Offset: 0x000DE746
		protected Infobar Infobar
		{
			get
			{
				return this.infobar;
			}
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x06002760 RID: 10080 RVA: 0x000E054E File Offset: 0x000DE74E
		protected RecipientWell EditRecipientWell
		{
			get
			{
				return this.editRecipientWell;
			}
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x06002761 RID: 10081 RVA: 0x000E0556 File Offset: 0x000DE756
		protected RecipientWell ParticipantRecipientWell
		{
			get
			{
				return this.participantRecipientWell;
			}
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x000E055E File Offset: 0x000DE75E
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			Utilities.MakePageCacheable(base.Response);
			this.editRecipientWell = null;
			this.participantRecipientWell = new MessageRecipientWell();
		}

		// Token: 0x04001B90 RID: 7056
		private Infobar infobar = new Infobar();

		// Token: 0x04001B91 RID: 7057
		private MessageRecipientWell editRecipientWell;

		// Token: 0x04001B92 RID: 7058
		private MessageRecipientWell participantRecipientWell;
	}
}
