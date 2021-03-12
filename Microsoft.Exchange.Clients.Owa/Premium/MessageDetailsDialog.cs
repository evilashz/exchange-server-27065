using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x0200045B RID: 1115
	public class MessageDetailsDialog : OwaPage
	{
		// Token: 0x06002950 RID: 10576 RVA: 0x000E9558 File Offset: 0x000E7758
		protected override void OnLoad(EventArgs e)
		{
			this.message = Utilities.GetItemForRequest<MessageItem>(base.OwaContext, out this.parentItem, true, new PropertyDefinition[]
			{
				MessageItemSchema.TransportMessageHeaders
			});
			base.OnLoad(e);
		}

		// Token: 0x06002951 RID: 10577 RVA: 0x000E9594 File Offset: 0x000E7794
		protected override void OnUnload(EventArgs e)
		{
			if (this.parentItem != null)
			{
				this.parentItem.Dispose();
				this.parentItem = null;
			}
			if (this.message != null)
			{
				this.message.Dispose();
				this.message = null;
			}
		}

		// Token: 0x06002952 RID: 10578 RVA: 0x000E95CC File Offset: 0x000E77CC
		protected void RenderImportance()
		{
			if (this.message == null)
			{
				return;
			}
			switch (this.message.Importance)
			{
			case Importance.Low:
				base.Response.Write(LocalizedStrings.GetHtmlEncoded(1502599728));
				return;
			case Importance.Normal:
				base.Response.Write(LocalizedStrings.GetHtmlEncoded(1690472495));
				return;
			case Importance.High:
				base.Response.Write(LocalizedStrings.GetHtmlEncoded(-77932258));
				return;
			default:
				return;
			}
		}

		// Token: 0x06002953 RID: 10579 RVA: 0x000E9644 File Offset: 0x000E7844
		protected void RenderSensitivity()
		{
			if (this.message == null)
			{
				return;
			}
			switch (this.message.Sensitivity)
			{
			case Sensitivity.Normal:
				base.Response.Write(LocalizedStrings.GetHtmlEncoded(1690472495));
				return;
			case Sensitivity.Personal:
				base.Response.Write(LocalizedStrings.GetHtmlEncoded(567923294));
				return;
			case Sensitivity.Private:
				base.Response.Write(LocalizedStrings.GetHtmlEncoded(-1268489823));
				return;
			case Sensitivity.CompanyConfidential:
				base.Response.Write(LocalizedStrings.GetHtmlEncoded(-819101664));
				return;
			default:
				return;
			}
		}

		// Token: 0x06002954 RID: 10580 RVA: 0x000E96D4 File Offset: 0x000E78D4
		protected void RenderTransportMessageHeaders()
		{
			if (this.message == null)
			{
				return;
			}
			try
			{
				using (Stream stream = this.message.OpenPropertyStream(MessageItemSchema.TransportMessageHeaders, PropertyOpenMode.ReadOnly))
				{
					byte[] array = new byte[1024];
					int count;
					while ((count = stream.Read(array, 0, 1024)) > 0)
					{
						Utilities.HtmlEncode(Encoding.Unicode.GetString(array, 0, count), base.Response.Output);
					}
				}
			}
			catch (StoragePermanentException)
			{
			}
		}

		// Token: 0x04001C37 RID: 7223
		private MessageItem message;

		// Token: 0x04001C38 RID: 7224
		private Item parentItem;
	}
}
