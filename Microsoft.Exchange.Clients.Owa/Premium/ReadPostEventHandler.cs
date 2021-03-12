using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004D0 RID: 1232
	[OwaEventNamespace("ReadPost")]
	internal sealed class ReadPostEventHandler : ItemEventHandler
	{
		// Token: 0x06002EFD RID: 12029 RVA: 0x0010E682 File Offset: 0x0010C882
		public static void Register()
		{
			OwaEventRegistry.RegisterHandler(typeof(ReadPostEventHandler));
		}

		// Token: 0x06002EFE RID: 12030 RVA: 0x0010E694 File Offset: 0x0010C894
		[OwaEvent("Save")]
		[OwaEventParameter("Subj", typeof(string))]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId))]
		[OwaEventParameter("CK", typeof(string))]
		public void Save()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "ReadPostEventHandler.Savepost");
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("Id");
			string changeKey = (string)base.GetParameter("CK");
			using (PostItem item = Utilities.GetItem<PostItem>(base.UserContext, owaStoreObjectId, changeKey, false, new PropertyDefinition[0]))
			{
				ExTraceGlobals.MailTracer.TraceDebug((long)this.GetHashCode(), "Saving post. ");
				object parameter = base.GetParameter("Subj");
				if (parameter != null)
				{
					try
					{
						item.Subject = (string)parameter;
					}
					catch (PropertyValidationException ex)
					{
						throw new OwaInvalidRequestException(ex.Message);
					}
				}
				Utilities.SaveItem(item, true);
				item.Load();
				this.Writer.Write("<div id=ck>");
				this.Writer.Write(item.Id.ChangeKeyAsBase64String());
				this.Writer.Write("</div>");
			}
		}

		// Token: 0x040020E3 RID: 8419
		public const string EventNamespace = "ReadPost";

		// Token: 0x040020E4 RID: 8420
		public const string Subject = "Subj";
	}
}
