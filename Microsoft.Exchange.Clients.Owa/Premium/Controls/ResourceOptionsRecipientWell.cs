using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Directory;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200040B RID: 1035
	public class ResourceOptionsRecipientWell : RecipientWell
	{
		// Token: 0x0600256A RID: 9578 RVA: 0x000D89B9 File Offset: 0x000D6BB9
		internal ResourceOptionsRecipientWell(CalendarConfiguration resourceConfig)
		{
			this.calendarConfig = resourceConfig;
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x000D89C8 File Offset: 0x000D6BC8
		protected override void RenderAdditionalExpandos(TextWriter writer)
		{
			writer.Write(" _fRsrc=1");
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x000D89D8 File Offset: 0x000D6BD8
		public override bool HasRecipients(RecipientWellType type)
		{
			MultiValuedProperty<string> addressList = this.GetAddressList(type);
			return addressList != null && addressList.Count > 0;
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x000D89FC File Offset: 0x000D6BFC
		internal override void RenderContents(TextWriter writer, UserContext userContext, RecipientWellType type, RecipientWellNode.RenderFlags flags, RenderRecipientWellNode wellNode)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (!this.HasRecipients(type))
			{
				return;
			}
			RecipientWellNode.RenderFlags renderFlags = flags & ~RecipientWellNode.RenderFlags.RenderCommas;
			bool flag = true;
			string smtpAddress = null;
			string alias = null;
			int num = 0;
			MultiValuedProperty<string> addressList = this.GetAddressList(type);
			IRecipientSession recipientSession = Utilities.CreateADRecipientSession(ConsistencyMode.IgnoreInvalid, userContext);
			foreach (string text in addressList)
			{
				ADObjectId adObjectId = null;
				ADRecipient adrecipient = recipientSession.FindByLegacyExchangeDN(text);
				bool flag2 = (flags & RecipientWellNode.RenderFlags.ReadOnly) != RecipientWellNode.RenderFlags.None;
				if (adrecipient != null)
				{
					adObjectId = adrecipient.Id;
					smtpAddress = adrecipient.PrimarySmtpAddress.ToString();
					if (flag2)
					{
						alias = adrecipient.Alias;
					}
					if (adrecipient is IADDistributionList)
					{
						num |= 1;
					}
					if (DirectoryAssistance.IsADRecipientRoom(adrecipient))
					{
						num |= 2;
					}
				}
				if (wellNode(writer, userContext, (adrecipient != null) ? adrecipient.DisplayName : text.ToString(), smtpAddress, (adrecipient != null) ? adrecipient.LegacyExchangeDN : text.ToString(), "EX", alias, (adrecipient != null) ? AddressOrigin.Directory : AddressOrigin.Unknown, num, null, EmailAddressIndex.None, adObjectId, renderFlags, null, null) && flag)
				{
					flag = false;
					if ((flags & RecipientWellNode.RenderFlags.RenderCommas) != RecipientWellNode.RenderFlags.None)
					{
						renderFlags |= RecipientWellNode.RenderFlags.RenderCommas;
					}
				}
			}
		}

		// Token: 0x0600256E RID: 9582 RVA: 0x000D8B58 File Offset: 0x000D6D58
		private MultiValuedProperty<string> GetAddressList(RecipientWellType type)
		{
			if (type == RecipientWellType.To)
			{
				return this.calendarConfig.BookInPolicy;
			}
			if (type == RecipientWellType.Cc)
			{
				return this.calendarConfig.RequestInPolicy;
			}
			if (type == RecipientWellType.Bcc)
			{
				return this.calendarConfig.RequestOutOfPolicy;
			}
			throw new ArgumentException("invalid RecipientWellType");
		}

		// Token: 0x040019E5 RID: 6629
		private CalendarConfiguration calendarConfig;
	}
}
