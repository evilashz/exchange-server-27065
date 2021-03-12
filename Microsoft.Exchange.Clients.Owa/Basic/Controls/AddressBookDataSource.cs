using System;
using System.Collections;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Directory;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000005 RID: 5
	internal sealed class AddressBookDataSource : ListViewDataSource
	{
		// Token: 0x0600002B RID: 43 RVA: 0x000027AB File Offset: 0x000009AB
		public AddressBookDataSource(Hashtable properties, string searchString, AddressBookBase addressBook, AddressBookBase.RecipientCategory recipientCategory, UserContext userContext) : base(properties)
		{
			if (addressBook == null)
			{
				throw new ArgumentNullException("addresslist");
			}
			this.addressBook = addressBook;
			this.searchString = searchString;
			this.recipientCategory = recipientCategory;
			this.userContext = userContext;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000027E0 File Offset: 0x000009E0
		public override void LoadData(int startRange, int endRange)
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "ADDataSource.LoadData(Start)");
			int lcid = Culture.GetUserCulture().LCID;
			string preferredDC = this.userContext.PreferredDC;
			ExTraceGlobals.MailCallTracer.TraceDebug<string>((long)this.GetHashCode(), "AddressBookDataSource.LoadData: preferred DC in user context = '{0}'", preferredDC);
			if (startRange < 1)
			{
				throw new ArgumentOutOfRangeException("startRange", "startRange must be greater than 0");
			}
			if (endRange < startRange)
			{
				throw new ArgumentOutOfRangeException("endRange", "endRange must be greater than or equal to startRange");
			}
			PropertyDefinition[] properties = base.CreateProperyTable();
			int num = endRange - startRange + 1;
			int pagesToSkip = startRange / num;
			string cookie = null;
			if (DirectoryAssistance.IsEmptyAddressList(this.userContext))
			{
				base.Items = new object[0][];
			}
			else if (!string.IsNullOrEmpty(this.searchString))
			{
				base.Items = AddressBookBase.PagedSearch(DirectoryAssistance.IsVirtualAddressList(this.userContext) ? this.userContext.MailboxIdentity.GetOWAMiniRecipient().QueryBaseDN : null, DirectoryAssistance.IsVirtualAddressList(this.userContext) ? null : this.addressBook, this.userContext.ExchangePrincipal.MailboxInfo.OrganizationId, this.recipientCategory, this.searchString, ref cookie, pagesToSkip, num, out this.itemsTouched, ref lcid, ref preferredDC, properties);
			}
			else
			{
				ExTraceGlobals.MailCallTracer.TraceDebug<OrganizationId>((long)this.GetHashCode(), "AddressBookDataSource.LoadData: browse: OrganizationId of address book = '{0}'", this.addressBook.OrganizationId);
				int num2;
				base.Items = this.addressBook.BrowseTo(ref cookie, this.userContext.MailboxIdentity.GetOWAMiniRecipient().QueryBaseDN, ref lcid, ref preferredDC, startRange, num + 1, out num2, DirectoryAssistance.IsVirtualAddressList(this.userContext), properties);
				if (base.Items != null && base.Items.Length < num + 1)
				{
					cookie = null;
				}
			}
			this.userContext.PreferredDC = preferredDC;
			ExTraceGlobals.MailCallTracer.TraceDebug<string>((long)this.GetHashCode(), "AddressBookDataSource.LoadData: stamped preferred DC = '{0}' onto user context.", preferredDC);
			base.Cookie = cookie;
			base.StartRange = startRange;
			if (base.Items == null || base.Items.Length == 0)
			{
				base.EndRange = 0;
				return;
			}
			if (base.Items.Length < num)
			{
				base.EndRange = startRange + base.Items.Length - 1;
				return;
			}
			base.EndRange = endRange;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000029FA File Offset: 0x00000BFA
		public override int TotalCount
		{
			get
			{
				return this.itemsTouched;
			}
		}

		// Token: 0x04000011 RID: 17
		private AddressBookBase.RecipientCategory recipientCategory;

		// Token: 0x04000012 RID: 18
		private AddressBookBase addressBook;

		// Token: 0x04000013 RID: 19
		private string searchString;

		// Token: 0x04000014 RID: 20
		private UserContext userContext;

		// Token: 0x04000015 RID: 21
		private int itemsTouched;
	}
}
