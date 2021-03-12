using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Directory;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000468 RID: 1128
	public class ReadADRecipientPage : OwaForm, IRegistryOnlyForm
	{
		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x06002A59 RID: 10841 RVA: 0x000ECF09 File Offset: 0x000EB109
		// (set) Token: 0x06002A5A RID: 10842 RVA: 0x000ECF11 File Offset: 0x000EB111
		internal ADObjectId ADObjectId
		{
			get
			{
				return this.adObjectId;
			}
			private set
			{
				this.adObjectId = value;
			}
		}

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x06002A5B RID: 10843 RVA: 0x000ECF1A File Offset: 0x000EB11A
		// (set) Token: 0x06002A5C RID: 10844 RVA: 0x000ECF22 File Offset: 0x000EB122
		internal ADRecipient ADRecipient
		{
			get
			{
				return this.adRecipient;
			}
			private set
			{
				this.adRecipient = value;
			}
		}

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x06002A5D RID: 10845 RVA: 0x000ECF2B File Offset: 0x000EB12B
		// (set) Token: 0x06002A5E RID: 10846 RVA: 0x000ECF33 File Offset: 0x000EB133
		internal IRecipientSession ADRecipientSession
		{
			get
			{
				return this.adRecipientSession;
			}
			private set
			{
				this.adRecipientSession = value;
			}
		}

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x06002A5F RID: 10847 RVA: 0x000ECF3C File Offset: 0x000EB13C
		// (set) Token: 0x06002A60 RID: 10848 RVA: 0x000ECF44 File Offset: 0x000EB144
		private protected bool RecipientOutOfSearchScope
		{
			protected get
			{
				return this.recipientOutOfSearchScope;
			}
			private set
			{
				this.recipientOutOfSearchScope = value;
			}
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x06002A61 RID: 10849 RVA: 0x000ECF4D File Offset: 0x000EB14D
		protected string Base64EncodedId
		{
			get
			{
				return Utilities.GetBase64StringFromADObjectId(this.adObjectId);
			}
		}

		// Token: 0x06002A62 RID: 10850 RVA: 0x000ECF5C File Offset: 0x000EB15C
		protected override void OnLoad(EventArgs e)
		{
			this.adObjectId = DirectoryAssistance.ParseADObjectId(Utilities.GetQueryStringParameter(base.Request, "id"));
			if (this.adObjectId == null)
			{
				throw new OwaInvalidRequestException();
			}
			string queryStringParameter = Utilities.GetQueryStringParameter(base.Request, "email", false);
			this.adRecipientSession = Utilities.CreateADRecipientSession(Microsoft.Exchange.Clients.Owa.Core.Culture.GetUserCulture().LCID, true, ConsistencyMode.FullyConsistent, true, base.UserContext, false);
			this.adRecipient = Utilities.CreateADRecipientFromProxyAddress(this.adObjectId, queryStringParameter, this.adRecipientSession);
			if (this.adRecipient == null)
			{
				if (base.UserContext.MailboxIdentity.GetOWAMiniRecipient().QueryBaseDN != null)
				{
					this.adRecipientSession = Utilities.CreateADRecipientSession(Microsoft.Exchange.Clients.Owa.Core.Culture.GetUserCulture().LCID, true, ConsistencyMode.FullyConsistent, false, base.UserContext);
					this.adRecipient = Utilities.CreateADRecipientFromProxyAddress(this.adObjectId, queryStringParameter, this.adRecipientSession);
					if (this.adRecipient != null)
					{
						this.RecipientOutOfSearchScope = true;
					}
				}
			}
			else
			{
				this.adObjectId = (ADObjectId)this.adRecipient[ADObjectSchema.Id];
				if (this.adRecipient.HiddenFromAddressListsEnabled)
				{
					this.RecipientOutOfSearchScope = true;
				}
			}
			if (this.adRecipient == null)
			{
				throw new OwaADObjectNotFoundException();
			}
			this.adRecipientSession = Utilities.CreateADRecipientSession(Microsoft.Exchange.Clients.Owa.Core.Culture.GetUserCulture().LCID, true, ConsistencyMode.IgnoreInvalid, !this.RecipientOutOfSearchScope, base.UserContext);
			string action = base.OwaContext.FormsRegistryContext.Action;
			base.IsPreviewForm = (action != null && action.Equals("Preview"));
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x000ED0CC File Offset: 0x000EB2CC
		protected void RenderPhoneticDisplayName()
		{
			if (base.UserContext.IsPhoneticNamesEnabled && !string.IsNullOrEmpty(this.adRecipient.PhoneticDisplayName))
			{
				Utilities.HtmlEncode(this.adRecipient.PhoneticDisplayName, base.Response.Output);
			}
		}

		// Token: 0x06002A64 RID: 10852 RVA: 0x000ED108 File Offset: 0x000EB308
		protected void RenderDisplayName()
		{
			Utilities.HtmlEncode(this.adRecipient.DisplayName, base.Response.Output);
		}

		// Token: 0x06002A65 RID: 10853 RVA: 0x000ED128 File Offset: 0x000EB328
		protected void RenderPresenceAndPhoto()
		{
			bool flag = this.adRecipient.RecipientType == RecipientType.Contact || this.adRecipient.RecipientType == RecipientType.UserMailbox || this.adRecipient.RecipientType == RecipientType.MailUser || this.adRecipient.RecipientType == RecipientType.User || this.adRecipient.RecipientType == RecipientType.MailContact;
			if (flag)
			{
				bool flag2 = base.UserContext.IsSenderPhotosFeatureEnabled(Feature.DisplayPhotos);
				if (base.UserContext.IsInstantMessageEnabled())
				{
					RenderingUtilities.RenderPresenceJellyBean(base.Response.Output, base.UserContext, false, string.Empty, flag2, "RcpJb");
				}
				if (flag2)
				{
					bool flag3 = this.adRecipient.ThumbnailPhoto != null && this.adRecipient.ThumbnailPhoto.Length > 0;
					bool flag4 = string.Equals(this.adRecipient.LegacyExchangeDN, base.UserContext.ExchangePrincipal.LegacyDn, StringComparison.OrdinalIgnoreCase);
					string srcUrl = (!flag3) ? string.Empty : RenderingUtilities.GetADPictureUrl(this.adRecipient.LegacyExchangeDN, "EX", base.UserContext, flag4);
					RenderingUtilities.RenderDisplayPicture(base.Response.Output, base.UserContext, srcUrl, 64, flag4, flag ? ThemeFileId.DoughboyPerson : ThemeFileId.DoughboyDL);
				}
			}
		}

		// Token: 0x04001C94 RID: 7316
		private ADObjectId adObjectId;

		// Token: 0x04001C95 RID: 7317
		private ADRecipient adRecipient;

		// Token: 0x04001C96 RID: 7318
		private IRecipientSession adRecipientSession;

		// Token: 0x04001C97 RID: 7319
		private bool recipientOutOfSearchScope;
	}
}
