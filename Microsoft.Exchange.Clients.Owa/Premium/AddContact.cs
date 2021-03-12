using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000436 RID: 1078
	public class AddContact : OwaForm, IRegistryOnlyForm
	{
		// Token: 0x060026F0 RID: 9968 RVA: 0x000DE6FE File Offset: 0x000DC8FE
		public AddContact() : base(false)
		{
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x060026F1 RID: 9969 RVA: 0x000DE712 File Offset: 0x000DC912
		protected Infobar Infobar
		{
			get
			{
				return this.infobar;
			}
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x060026F2 RID: 9970 RVA: 0x000DE71A File Offset: 0x000DC91A
		protected AddContactToolbar Toolbar
		{
			get
			{
				return this.toolbar;
			}
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x060026F3 RID: 9971 RVA: 0x000DE722 File Offset: 0x000DC922
		protected RecipientWell RecipientWell
		{
			get
			{
				return this.recipientWell;
			}
		}

		// Token: 0x060026F4 RID: 9972 RVA: 0x000DE72A File Offset: 0x000DC92A
		protected override void OnLoad(EventArgs e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			base.OnLoad(e);
			Utilities.MakePageCacheable(base.Response);
			this.toolbar = new AddContactToolbar();
			this.recipientWell = new MessageRecipientWell();
		}

		// Token: 0x04001B45 RID: 6981
		private Infobar infobar = new Infobar();

		// Token: 0x04001B46 RID: 6982
		private AddContactToolbar toolbar;

		// Token: 0x04001B47 RID: 6983
		private MessageRecipientWell recipientWell;
	}
}
