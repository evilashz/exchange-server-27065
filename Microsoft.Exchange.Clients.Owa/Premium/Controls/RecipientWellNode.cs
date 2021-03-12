using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003F2 RID: 1010
	public class RecipientWellNode
	{
		// Token: 0x06002504 RID: 9476 RVA: 0x000D665C File Offset: 0x000D485C
		internal RecipientWellNode(string displayName, string smtpAddress, string routingAddress, string routingType, string alias, AddressOrigin addressOrigin, int recipientFlags, string sipUri, string mobilePhoneNumber)
		{
			this.displayName = displayName;
			this.smtpAddress = smtpAddress;
			this.routingAddress = routingAddress;
			this.routingType = routingType;
			this.alias = alias;
			this.addressOrigin = addressOrigin;
			this.recipientFlags = recipientFlags;
			this.sipUri = sipUri;
			this.mobilePhoneNumber = mobilePhoneNumber;
		}

		// Token: 0x06002505 RID: 9477 RVA: 0x000D66B4 File Offset: 0x000D48B4
		internal RecipientWellNode(string displayName, string smtpAddress, AddressOrigin addressOrigin)
		{
			this.displayName = displayName;
			this.smtpAddress = smtpAddress;
			this.addressOrigin = addressOrigin;
		}

		// Token: 0x06002506 RID: 9478 RVA: 0x000D66D1 File Offset: 0x000D48D1
		internal RecipientWellNode(string displayName, string smtpAddress)
		{
			this.displayName = displayName;
			this.smtpAddress = smtpAddress;
			this.addressOrigin = AddressOrigin.Unknown;
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06002507 RID: 9479 RVA: 0x000D66EE File Offset: 0x000D48EE
		// (set) Token: 0x06002508 RID: 9480 RVA: 0x000D66F6 File Offset: 0x000D48F6
		internal AddressOrigin AddressOrigin
		{
			get
			{
				return this.addressOrigin;
			}
			set
			{
				this.addressOrigin = value;
			}
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06002509 RID: 9481 RVA: 0x000D66FF File Offset: 0x000D48FF
		// (set) Token: 0x0600250A RID: 9482 RVA: 0x000D6707 File Offset: 0x000D4907
		public int RecipientFlags
		{
			get
			{
				return this.recipientFlags;
			}
			set
			{
				this.recipientFlags = value;
			}
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x0600250B RID: 9483 RVA: 0x000D6710 File Offset: 0x000D4910
		// (set) Token: 0x0600250C RID: 9484 RVA: 0x000D6718 File Offset: 0x000D4918
		public string Alias
		{
			get
			{
				return this.alias;
			}
			set
			{
				this.alias = value;
			}
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x0600250D RID: 9485 RVA: 0x000D6721 File Offset: 0x000D4921
		// (set) Token: 0x0600250E RID: 9486 RVA: 0x000D6729 File Offset: 0x000D4929
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.displayName = value;
			}
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x0600250F RID: 9487 RVA: 0x000D6732 File Offset: 0x000D4932
		// (set) Token: 0x06002510 RID: 9488 RVA: 0x000D673A File Offset: 0x000D493A
		public string SmtpAddress
		{
			get
			{
				return this.smtpAddress;
			}
			set
			{
				this.smtpAddress = value;
			}
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06002511 RID: 9489 RVA: 0x000D6743 File Offset: 0x000D4943
		// (set) Token: 0x06002512 RID: 9490 RVA: 0x000D674B File Offset: 0x000D494B
		public string SipUri
		{
			get
			{
				return this.sipUri;
			}
			set
			{
				this.sipUri = value;
			}
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06002513 RID: 9491 RVA: 0x000D6754 File Offset: 0x000D4954
		// (set) Token: 0x06002514 RID: 9492 RVA: 0x000D675C File Offset: 0x000D495C
		public string MobilePhoneNumber
		{
			get
			{
				return this.mobilePhoneNumber;
			}
			set
			{
				this.mobilePhoneNumber = value;
			}
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06002515 RID: 9493 RVA: 0x000D6765 File Offset: 0x000D4965
		// (set) Token: 0x06002516 RID: 9494 RVA: 0x000D676D File Offset: 0x000D496D
		public string RoutingAddress
		{
			get
			{
				return this.routingAddress;
			}
			set
			{
				this.routingAddress = value;
			}
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06002517 RID: 9495 RVA: 0x000D6776 File Offset: 0x000D4976
		// (set) Token: 0x06002518 RID: 9496 RVA: 0x000D677E File Offset: 0x000D497E
		public string RoutingType
		{
			get
			{
				return this.routingType;
			}
			set
			{
				this.routingType = value;
			}
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06002519 RID: 9497 RVA: 0x000D6787 File Offset: 0x000D4987
		// (set) Token: 0x0600251A RID: 9498 RVA: 0x000D678F File Offset: 0x000D498F
		public ADObjectId ADObjectId
		{
			get
			{
				return this.adObjectId;
			}
			set
			{
				this.adObjectId = value;
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x0600251B RID: 9499 RVA: 0x000D6798 File Offset: 0x000D4998
		// (set) Token: 0x0600251C RID: 9500 RVA: 0x000D67A0 File Offset: 0x000D49A0
		internal StoreObjectId StoreObjectId
		{
			get
			{
				return this.storeObjectId;
			}
			set
			{
				this.storeObjectId = value;
			}
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x0600251D RID: 9501 RVA: 0x000D67A9 File Offset: 0x000D49A9
		// (set) Token: 0x0600251E RID: 9502 RVA: 0x000D67B1 File Offset: 0x000D49B1
		internal EmailAddressIndex EmailAddressIndex
		{
			get
			{
				return this.emailAddressIndex;
			}
			set
			{
				this.emailAddressIndex = value;
			}
		}

		// Token: 0x0600251F RID: 9503 RVA: 0x000D67BC File Offset: 0x000D49BC
		internal static bool Render(TextWriter writer, UserContext userContext, string displayName, string smtpAddress, string routingAddress, string routingType, string alias, AddressOrigin addressOrigin, int recipientFlags, StoreObjectId storeObjectId, EmailAddressIndex emailAddressIndex, ADObjectId adObjectId, RecipientWellNode.RenderFlags flags, string sipUri, string mobilePhoneNumber)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (string.CompareOrdinal(routingType, "SMTP") == 0 && ImceaAddress.IsImceaAddress(routingAddress) && Utilities.TryDecodeImceaAddress(routingAddress, ref routingType, ref routingAddress))
			{
				smtpAddress = null;
			}
			bool flag = (flags & RecipientWellNode.RenderFlags.ReadOnly) != RecipientWellNode.RenderFlags.None;
			bool flag2 = false;
			string text = "rwRR";
			if (string.CompareOrdinal(routingType, "MAPIPDL") != 0 && (string.IsNullOrEmpty(routingAddress) || string.IsNullOrEmpty(routingType)))
			{
				if (string.IsNullOrEmpty(displayName))
				{
					ExTraceGlobals.MailDataTracer.TraceDebug(0L, "Found recipient without an email address or display name");
					return false;
				}
				text = "rwUR";
				flag2 = true;
				routingAddress = null;
				smtpAddress = null;
				routingType = null;
			}
			else if (string.IsNullOrEmpty(displayName))
			{
				if (!string.IsNullOrEmpty(smtpAddress))
				{
					displayName = smtpAddress;
				}
				else if (!string.IsNullOrEmpty(routingAddress))
				{
					displayName = routingAddress;
				}
			}
			if (flag)
			{
				text = (flag2 ? "rwURO" : "rwRRO");
			}
			if ((recipientFlags & 1) != 0)
			{
				text += " rwDL";
			}
			if ((flags & RecipientWellNode.RenderFlags.RenderCommas) != RecipientWellNode.RenderFlags.None)
			{
				writer.Write(userContext.DirectionMark);
				writer.Write("; ");
			}
			if ((flags & RecipientWellNode.RenderFlags.RenderSkinnyHtml) != RecipientWellNode.RenderFlags.None)
			{
				if (!flag)
				{
					writer.Write("<span>");
				}
				writer.Write("<span class=\"");
			}
			else if (flag)
			{
				writer.Write("<span id=\"spnR\" ");
				writer.Write(" ");
				Utilities.RenderScriptHandler(writer, "oncontextmenu", "onRwCm(event);");
				writer.Write(" ");
				Utilities.RenderScriptHandler(writer, "ondblclick", "onDblClkReadRcp(event);");
				writer.Write(" ");
				Utilities.RenderScriptHandler(writer, "onclick", "onClkRcp(event);");
				writer.Write(" class=\"");
			}
			else
			{
				writer.Write("<span tabindex=\"-1\" contenteditable=\"false\">");
				if (userContext.BrowserType == BrowserType.IE)
				{
					writer.Write("<span tabindex=\"-1\" contenteditable=\"true\" id=\"spnR\" ");
				}
				else
				{
					writer.Write("<span tabindex=\"-1\" contenteditable=\"false\" id=\"spnR\" ");
				}
				writer.Write(" ");
				Utilities.RenderScriptHandler(writer, "oncontextmenu", "onContextMenuSpnRw(event);");
				writer.Write(" ");
				Utilities.RenderScriptHandler(writer, "onclick", "onClkRcp(event);");
				writer.Write(" ");
				Utilities.RenderScriptHandler(writer, "onkeydown", "onKDRcp(event);");
				writer.Write(" ");
				Utilities.RenderScriptHandler(writer, "ondblclick", "onDblClkRcp(event);");
				writer.Write(" ondrop=\"return(false);\" class=\"");
			}
			writer.Write(text);
			writer.Write("\" _ao=\"{0}\" _rf=\"{1}\" _rt=\"", (int)addressOrigin, recipientFlags);
			if (routingType != null)
			{
				Utilities.SanitizeHtmlEncode(routingType, writer);
			}
			writer.Write("\" _em=\"");
			if (routingAddress != null)
			{
				Utilities.SanitizeHtmlEncode(routingAddress, writer);
			}
			if (storeObjectId != null)
			{
				writer.Write("\" _id=\"");
				Utilities.SanitizeHtmlEncode(storeObjectId.ToBase64String(), writer);
				writer.Write("\" _ei=\"");
				writer.Write((int)emailAddressIndex);
			}
			else if (adObjectId != null)
			{
				writer.Write("\" _id=\"");
				Utilities.SanitizeHtmlEncode(Convert.ToBase64String(adObjectId.ObjectGuid.ToByteArray()), writer);
			}
			writer.Write("\" title=\"");
			if (smtpAddress != null)
			{
				Utilities.SanitizeHtmlEncode(smtpAddress, writer);
			}
			if (!flag || addressOrigin == AddressOrigin.OneOff || (addressOrigin == AddressOrigin.Directory && !userContext.IsFeatureEnabled(Feature.GlobalAddressList)))
			{
				if (smtpAddress != null)
				{
					writer.Write("\" _sa=\"");
					Utilities.SanitizeHtmlEncode(smtpAddress, writer);
				}
				else if (routingType != null && routingAddress != null)
				{
					writer.Write("\" _imcea=\"");
					Utilities.SanitizeHtmlEncode(ImceaAddress.Encode(routingType, routingAddress, OwaConfigurationManager.Configuration.DefaultAcceptedDomain.DomainName.ToString()), writer);
				}
			}
			if (userContext.IsInstantMessageEnabled() && userContext.InstantMessagingType == InstantMessagingTypeOptions.Ocs && sipUri != null)
			{
				writer.Write("\" ");
				if (adObjectId == null)
				{
					writer.Write("_sipTrsp=1 ");
				}
				writer.Write("_uri=\"");
				Utilities.SanitizeHtmlEncode(sipUri, writer);
			}
			if (userContext.IsSmsEnabled && mobilePhoneNumber != null)
			{
				writer.Write("\" _mo=\"");
				Utilities.SanitizeHtmlEncode(mobilePhoneNumber, writer);
			}
			if ((smtpAddress != null || routingAddress != null || mobilePhoneNumber != null || Utilities.IsMapiPDL(routingType)) && (!flag || addressOrigin == AddressOrigin.OneOff || (addressOrigin == AddressOrigin.Store && (!userContext.IsFeatureEnabled(Feature.Contacts) || userContext.IsSmsEnabled)) || (addressOrigin == AddressOrigin.Directory && (!userContext.IsFeatureEnabled(Feature.GlobalAddressList) || userContext.IsSmsEnabled))) && displayName != null)
			{
				writer.Write("\" _dn=\"");
				Utilities.SanitizeHtmlEncode(displayName, writer);
			}
			if (!flag && alias != null)
			{
				writer.Write("\" _al=\"");
				Utilities.SanitizeHtmlEncode(alias, writer);
			}
			writer.Write("\">");
			if (userContext.IsInstantMessageEnabled() && flag && !string.IsNullOrEmpty(routingType) && ((string.CompareOrdinal(routingType, "EX") == 0 && (recipientFlags & 1) == 0) || string.CompareOrdinal(routingType, "SMTP") == 0))
			{
				RenderingUtilities.RenderPresenceJellyBean(writer, userContext, true, "onRwCmJb(event);", false, null);
			}
			if (displayName != null)
			{
				Utilities.SanitizeHtmlEncode(displayName, writer);
			}
			RecipientWellNode.RenderFormattedAddress(writer, userContext, displayName, smtpAddress, routingAddress, routingType);
			writer.Write("</span>");
			if (!flag)
			{
				writer.Write("</span>");
			}
			return true;
		}

		// Token: 0x06002520 RID: 9504 RVA: 0x000D6C78 File Offset: 0x000D4E78
		public void Render(TextWriter writer, UserContext userContext, RecipientWellNode.RenderFlags flags)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			RecipientWellNode.Render(writer, userContext, this.displayName, this.smtpAddress, this.routingAddress, this.routingType, this.alias, this.addressOrigin, this.recipientFlags, this.storeObjectId, this.emailAddressIndex, this.adObjectId, flags, this.sipUri, this.mobilePhoneNumber);
		}

		// Token: 0x06002521 RID: 9505 RVA: 0x000D6CE4 File Offset: 0x000D4EE4
		internal static void RenderFormattedAddress(TextWriter writer, UserContext userContext, string displayName, string smtpAddress, string routingAddress, string routingType)
		{
			string text = null;
			if (!string.IsNullOrEmpty(smtpAddress) && !string.IsNullOrEmpty(displayName) && string.CompareOrdinal(routingType, "SMTP") == 0)
			{
				if (displayName.IndexOf(smtpAddress, StringComparison.OrdinalIgnoreCase) == -1)
				{
					text = smtpAddress;
				}
			}
			else if (!string.IsNullOrEmpty(displayName) && Utilities.IsMobileRoutingType(routingType))
			{
				if (displayName.IndexOf(routingAddress, StringComparison.OrdinalIgnoreCase) == -1)
				{
					text = routingAddress;
				}
			}
			else if (!string.IsNullOrEmpty(displayName) && Utilities.IsCustomRoutingType(routingAddress, routingType))
			{
				string text2 = routingType + ": " + routingAddress;
				if (displayName.IndexOf(text2, StringComparison.OrdinalIgnoreCase) == -1)
				{
					text = text2;
				}
			}
			if (text != null)
			{
				writer.Write(" ");
				writer.Write(userContext.DirectionMark);
				writer.Write("[");
				writer.Write(Utilities.SanitizeHtmlEncode(text));
				writer.Write("]");
				writer.Write(userContext.DirectionMark);
			}
		}

		// Token: 0x04001998 RID: 6552
		private ADObjectId adObjectId;

		// Token: 0x04001999 RID: 6553
		private StoreObjectId storeObjectId;

		// Token: 0x0400199A RID: 6554
		private string alias;

		// Token: 0x0400199B RID: 6555
		private string displayName;

		// Token: 0x0400199C RID: 6556
		private string smtpAddress;

		// Token: 0x0400199D RID: 6557
		private string sipUri;

		// Token: 0x0400199E RID: 6558
		private string mobilePhoneNumber;

		// Token: 0x0400199F RID: 6559
		private string routingAddress;

		// Token: 0x040019A0 RID: 6560
		private string routingType;

		// Token: 0x040019A1 RID: 6561
		private AddressOrigin addressOrigin;

		// Token: 0x040019A2 RID: 6562
		private int recipientFlags;

		// Token: 0x040019A3 RID: 6563
		private EmailAddressIndex emailAddressIndex;

		// Token: 0x020003F3 RID: 1011
		[Flags]
		public enum RenderFlags
		{
			// Token: 0x040019A5 RID: 6565
			None = 0,
			// Token: 0x040019A6 RID: 6566
			RenderSkinnyHtml = 1,
			// Token: 0x040019A7 RID: 6567
			RenderCommas = 2,
			// Token: 0x040019A8 RID: 6568
			ReadOnly = 4
		}
	}
}
