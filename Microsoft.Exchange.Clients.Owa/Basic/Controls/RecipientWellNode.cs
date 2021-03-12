using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000082 RID: 130
	public class RecipientWellNode
	{
		// Token: 0x0600038F RID: 911 RVA: 0x00020993 File Offset: 0x0001EB93
		internal RecipientWellNode(string displayName, string smtpAddress, string routingAddress, string routingType, AddressOrigin addressOrigin, int recipientFlags)
		{
			this.displayName = displayName;
			this.smtpAddress = smtpAddress;
			this.routingAddress = routingAddress;
			this.routingType = routingType;
			this.addressOrigin = addressOrigin;
			this.recipientFlags = recipientFlags;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x000209C8 File Offset: 0x0001EBC8
		internal RecipientWellNode(string displayName, string smtpAddress, AddressOrigin addressOrigin)
		{
			this.displayName = displayName;
			this.smtpAddress = smtpAddress;
			this.addressOrigin = addressOrigin;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x000209E5 File Offset: 0x0001EBE5
		internal RecipientWellNode(string displayName, string smtpAddress)
		{
			this.displayName = displayName;
			this.smtpAddress = smtpAddress;
			this.addressOrigin = AddressOrigin.Unknown;
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000392 RID: 914 RVA: 0x00020A02 File Offset: 0x0001EC02
		// (set) Token: 0x06000393 RID: 915 RVA: 0x00020A0A File Offset: 0x0001EC0A
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

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000394 RID: 916 RVA: 0x00020A13 File Offset: 0x0001EC13
		// (set) Token: 0x06000395 RID: 917 RVA: 0x00020A1B File Offset: 0x0001EC1B
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

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000396 RID: 918 RVA: 0x00020A24 File Offset: 0x0001EC24
		// (set) Token: 0x06000397 RID: 919 RVA: 0x00020A2C File Offset: 0x0001EC2C
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

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000398 RID: 920 RVA: 0x00020A35 File Offset: 0x0001EC35
		// (set) Token: 0x06000399 RID: 921 RVA: 0x00020A3D File Offset: 0x0001EC3D
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

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600039A RID: 922 RVA: 0x00020A46 File Offset: 0x0001EC46
		// (set) Token: 0x0600039B RID: 923 RVA: 0x00020A4E File Offset: 0x0001EC4E
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

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600039C RID: 924 RVA: 0x00020A57 File Offset: 0x0001EC57
		// (set) Token: 0x0600039D RID: 925 RVA: 0x00020A5F File Offset: 0x0001EC5F
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

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600039E RID: 926 RVA: 0x00020A68 File Offset: 0x0001EC68
		// (set) Token: 0x0600039F RID: 927 RVA: 0x00020A70 File Offset: 0x0001EC70
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

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x00020A79 File Offset: 0x0001EC79
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x00020A81 File Offset: 0x0001EC81
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

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x00020A8A File Offset: 0x0001EC8A
		// (set) Token: 0x060003A3 RID: 931 RVA: 0x00020A92 File Offset: 0x0001EC92
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

		// Token: 0x060003A4 RID: 932 RVA: 0x00020A9C File Offset: 0x0001EC9C
		internal static bool Render(UserContext userContext, TextWriter writer, string displayName, string smtpAddress, string routingAddress, string routingType, AddressOrigin addressOrigin, int recipientFlags, int readItemType, RecipientItemType recipientWell, ADObjectId adObjectId, StoreObjectId storeObjectId, RecipientWellNode.RenderFlags flags, string idString, bool isWebPartRequest)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			bool flag = (flags & RecipientWellNode.RenderFlags.ReadOnly) != RecipientWellNode.RenderFlags.None;
			bool flag2 = false;
			if (string.CompareOrdinal(routingType, "MAPIPDL") != 0 && (string.IsNullOrEmpty(routingAddress) || string.IsNullOrEmpty(routingType)))
			{
				if (string.IsNullOrEmpty(displayName))
				{
					ExTraceGlobals.MailDataTracer.TraceDebug(0L, "Found recipient without an email address or display name");
					return false;
				}
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
			if ((flags & RecipientWellNode.RenderFlags.RenderCommas) != RecipientWellNode.RenderFlags.None)
			{
				writer.Write("; ");
			}
			writer.Write("<span class=\"");
			if (flag2)
			{
				writer.Write("rwURO\">");
				if (displayName != null)
				{
					Utilities.HtmlEncode(displayName, writer);
				}
			}
			else
			{
				writer.Write("rwRO");
				if ((recipientFlags & 1) != 0)
				{
					writer.Write(" rwDL");
				}
				writer.Write("\">");
			}
			bool flag3 = false;
			if (!string.IsNullOrEmpty(idString))
			{
				if ((userContext.IsFeatureEnabled(Feature.Contacts) && storeObjectId != null) || adObjectId != null)
				{
					writer.Write("<a href=\"#\"");
					if (storeObjectId != null)
					{
						writer.Write(" id=\"");
						Utilities.HtmlEncode(storeObjectId.ToBase64String(), writer);
					}
					else if (adObjectId != null)
					{
						writer.Write(" id=\"");
						Utilities.HtmlEncode(Utilities.GetBase64StringFromADObjectId(adObjectId), writer);
					}
					writer.Write("\" onClick=\"return onClkRcpt(this,{0}", readItemType);
					if (!flag)
					{
						writer.Write(",{0}", (int)recipientWell);
					}
					writer.Write(");\">");
					flag3 = true;
				}
				else if (routingType == "SMTP" && !string.IsNullOrEmpty(smtpAddress) && flag && !isWebPartRequest)
				{
					writer.Write("<a href=\"");
					Utilities.HtmlEncode("?ae=Item&t=IPM.Note&a=New&to=", writer);
					Utilities.HtmlEncode(Utilities.UrlEncode(smtpAddress), writer);
					if (!string.IsNullOrEmpty(displayName))
					{
						Utilities.HtmlEncode("&nm=", writer);
						Utilities.HtmlEncode(Utilities.UrlEncode(displayName), writer);
					}
					writer.Write("\" class=\"emadr\">");
					flag3 = true;
				}
			}
			if (!flag2)
			{
				if (displayName != null)
				{
					Utilities.HtmlEncode(displayName, writer);
				}
				RecipientWellNode.RenderFormattedAddress(writer, displayName, smtpAddress, routingAddress, routingType);
			}
			if (flag3)
			{
				writer.Write("</a>");
			}
			if (!flag && !string.IsNullOrEmpty(idString))
			{
				writer.Write(" <span class=\"sq\">[<a href=\"#\" onClick=\"return onClkRmRcp('");
				writer.Write(idString);
				writer.Write("')\">");
				writer.Write(LocalizedStrings.GetHtmlEncoded(341226654));
				writer.Write("</a>]</span>");
			}
			writer.Write("</span>");
			return true;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00020D18 File Offset: 0x0001EF18
		internal static void RenderFormattedAddress(TextWriter writer, string displayName, string smtpAddress, string routingAddress, string routingType)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!string.IsNullOrEmpty(displayName) && !string.IsNullOrEmpty(routingType) && !string.IsNullOrEmpty(smtpAddress) && string.CompareOrdinal(routingType, "SMTP") == 0 && displayName.IndexOf(smtpAddress, StringComparison.OrdinalIgnoreCase) == -1)
			{
				stringBuilder.Append(" [");
				stringBuilder.Append(Utilities.HtmlEncode(smtpAddress));
				stringBuilder.Append("]");
			}
			else if (!string.IsNullOrEmpty(displayName) && Utilities.IsCustomRoutingType(routingAddress, routingType))
			{
				string text = routingType + ": " + routingAddress;
				if (displayName.IndexOf(text, StringComparison.OrdinalIgnoreCase) == -1)
				{
					stringBuilder.Append(" [");
					stringBuilder.Append(Utilities.HtmlEncode(text));
					stringBuilder.Append("]");
				}
			}
			if (!string.IsNullOrEmpty(stringBuilder.ToString()))
			{
				writer.Write(stringBuilder);
			}
		}

		// Token: 0x040002CF RID: 719
		private ADObjectId adObjectId;

		// Token: 0x040002D0 RID: 720
		private StoreObjectId storeObjectId;

		// Token: 0x040002D1 RID: 721
		private string displayName;

		// Token: 0x040002D2 RID: 722
		private string smtpAddress;

		// Token: 0x040002D3 RID: 723
		private string routingAddress;

		// Token: 0x040002D4 RID: 724
		private string routingType;

		// Token: 0x040002D5 RID: 725
		private AddressOrigin addressOrigin;

		// Token: 0x040002D6 RID: 726
		private int recipientFlags;

		// Token: 0x040002D7 RID: 727
		private EmailAddressIndex emailAddressIndex;

		// Token: 0x02000083 RID: 131
		[Flags]
		public enum RenderFlags
		{
			// Token: 0x040002D9 RID: 729
			None = 0,
			// Token: 0x040002DA RID: 730
			RenderCommas = 2,
			// Token: 0x040002DB RID: 731
			ReadOnly = 4
		}
	}
}
