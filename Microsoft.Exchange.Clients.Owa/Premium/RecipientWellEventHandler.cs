using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Directory;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004D2 RID: 1234
	[OwaEventNamespace("RecipWell")]
	internal sealed class RecipientWellEventHandler : OwaEventHandlerBase
	{
		// Token: 0x06002F0A RID: 12042 RVA: 0x0010EEAB File Offset: 0x0010D0AB
		public static void Register()
		{
			OwaEventRegistry.RegisterStruct(typeof(RecipientInfoAC));
			OwaEventRegistry.RegisterHandler(typeof(RecipientWellEventHandler));
		}

		// Token: 0x06002F0B RID: 12043 RVA: 0x0010EECC File Offset: 0x0010D0CC
		[OwaEventParameter("AB", typeof(bool), false, true)]
		[OwaEventParameter("To", typeof(string), true, true)]
		[OwaEventParameter("Cc", typeof(string), true, true)]
		[OwaEventParameter("Bcc", typeof(string), true, true)]
		[OwaEventParameter("From", typeof(string), true, true)]
		[OwaEventParameter("Recips", typeof(RecipientInfoAC), true, true)]
		[OwaEventParameter("FSoneAnr", typeof(bool), false, true)]
		[OwaEventParameter("DefaultRT", typeof(string), false, true)]
		[OwaEventParameter("OnlyDefaultRT", typeof(bool), false, true)]
		[OwaEventParameter("RBT", typeof(RecipientBlockType), false, true)]
		[OwaEvent("Resolve")]
		public void Resolve()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "RecipientWellEventHandler.Resolve");
			AnrManager.Options anrOptions = this.ReadAnrOptions();
			this.ResolveIndividualWell("To", anrOptions);
			this.ResolveIndividualWell("Cc", anrOptions);
			this.ResolveIndividualWell("Bcc", anrOptions);
			this.ResolveIndividualWell("From", anrOptions);
			this.UpdateCache();
		}

		// Token: 0x06002F0C RID: 12044 RVA: 0x0010EF2C File Offset: 0x0010D12C
		private AnrManager.Options ReadAnrOptions()
		{
			AnrManager.Options options = new AnrManager.Options();
			if (base.IsParameterSet("AB"))
			{
				options.ResolveOnlyFromAddressBook = (bool)base.GetParameter("AB");
			}
			if (base.IsParameterSet("FSoneAnr"))
			{
				options.ResolveAgainstAllContacts = (bool)base.GetParameter("FSoneAnr");
			}
			if (base.IsParameterSet("DefaultRT"))
			{
				options.DefaultRoutingType = (string)base.GetParameter("DefaultRT");
			}
			if (base.IsParameterSet("OnlyDefaultRT"))
			{
				options.OnlyAllowDefaultRoutingType = (bool)base.GetParameter("OnlyDefaultRT");
			}
			if (base.IsParameterSet("RBT"))
			{
				options.RecipientBlockType = (RecipientBlockType)base.GetParameter("RBT");
			}
			return options;
		}

		// Token: 0x06002F0D RID: 12045 RVA: 0x0010EFF0 File Offset: 0x0010D1F0
		[OwaEvent("UpdateAC")]
		[OwaEventParameter("Recips", typeof(RecipientInfoAC), true, true)]
		public void UpdateCache()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "RecipientWellEventHandler.UpdateCache");
			RecipientInfoAC[] array = (RecipientInfoAC[])base.GetParameter("Recips");
			if (array != null && array.Length != 0)
			{
				AutoCompleteCache.UpdateAutoCompleteCacheFromRecipientInfoList(array, base.OwaContext.UserContext);
			}
		}

		// Token: 0x06002F0E RID: 12046 RVA: 0x0010F03D File Offset: 0x0010D23D
		[OwaEvent("ResolveIndividualWell")]
		[OwaEventParameter("Recips", typeof(string), true, true)]
		[OwaEventParameter("DefaultRT", typeof(string), false, true)]
		[OwaEventParameter("OnlyDefaultRT", typeof(bool), false, true)]
		[OwaEventParameter("RBT", typeof(RecipientBlockType), false, true)]
		public void ResolveIndividualWell()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "RecipientWellEventHandler.ResolveIndividualWell");
			this.ResolveIndividualWell("Recips");
		}

		// Token: 0x06002F0F RID: 12047 RVA: 0x0010F060 File Offset: 0x0010D260
		[OwaEventParameter("n", typeof(string), false, true)]
		[OwaEvent("ResolveOneRecipientForAnrMenu")]
		[OwaEventParameter("AB", typeof(bool), false, true)]
		[OwaEventParameter("FSoneAnr", typeof(bool), false, true)]
		[OwaEventParameter("DefaultRT", typeof(string), false, true)]
		[OwaEventParameter("OnlyDefaultRT", typeof(bool), false, true)]
		[OwaEventParameter("RBT", typeof(RecipientBlockType), false, true)]
		[OwaEventParameter("AddMenuMarkup", typeof(bool), false, true)]
		[OwaEventParameter("AddRecipientResults", typeof(bool), false, true)]
		public void ResolveOneRecipientForAnrMenu()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "RecipientWellEventHandler.ResolveOneRecipientForAnrMenu");
			bool flag = false;
			bool flag2 = false;
			if (base.IsParameterSet("AddMenuMarkup"))
			{
				flag = (bool)base.GetParameter("AddMenuMarkup");
			}
			if (base.IsParameterSet("AddRecipientResults"))
			{
				flag2 = (bool)base.GetParameter("AddRecipientResults");
			}
			StringBuilder stringBuilder = new StringBuilder();
			using (StringWriter stringWriter = new StringWriter(stringBuilder))
			{
				if (flag)
				{
					this.GetRecipientAnrContextMenu(stringWriter);
					RenderingUtilities.RenderStringVariable(this.Writer, "sHtmlMenu", stringWriter.ToString());
				}
				stringBuilder.Length = 0;
				if (flag2)
				{
					this.ResolveOneRecipientAndRenderResults(stringWriter);
					RenderingUtilities.RenderStringVariable(this.Writer, "rgRecips", stringWriter.ToString());
				}
			}
		}

		// Token: 0x06002F10 RID: 12048 RVA: 0x0010F134 File Offset: 0x0010D334
		[OwaEventParameter("OnlyDefaultRT", typeof(bool), false, true)]
		[OwaEventParameter("RBT", typeof(RecipientBlockType), false, true)]
		[OwaEvent("ResolveOneRecipient")]
		[OwaEventParameter("AB", typeof(bool), false, true)]
		[OwaEventParameter("FSoneAnr", typeof(bool), false, true)]
		[OwaEventParameter("DefaultRT", typeof(string), false, true)]
		[OwaEventParameter("n", typeof(string))]
		public void ResolveOneRecipient()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "RecipientWellEventHandler.ResolveOneRecipient");
			this.ResolveOneRecipientAndRenderResults(this.Writer);
		}

		// Token: 0x06002F11 RID: 12049 RVA: 0x0010F158 File Offset: 0x0010D358
		[OwaEvent("GetRecipProps")]
		[OwaEventParameter("id", typeof(string), false, true)]
		[OwaEventParameter("ao", typeof(AddressOrigin))]
		[OwaEventParameter("em", typeof(string), false, true)]
		[OwaEventParameter("rt", typeof(string), false, false)]
		public void GetRecipientMenuProperties()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "RecipientWellEventHandler.GetRecipientMenuProperties");
			AddressOrigin addressOrigin = (AddressOrigin)base.GetParameter("ao");
			string text = null;
			if (base.IsParameterSet("em"))
			{
				text = (string)base.GetParameter("em");
			}
			string routingType = (string)base.GetParameter("rt");
			switch (addressOrigin)
			{
			case AddressOrigin.Store:
			{
				if (!base.IsParameterSet("id"))
				{
					throw new OwaInvalidRequestException("Missing contact id.");
				}
				StoreObjectId contactId = Utilities.CreateStoreObjectId((string)base.GetParameter("id"));
				RecipientWellEventHandler.ContactRecipientProperties contactRecipientProperties = this.GetContactRecipientProperties(contactId, base.UserContext);
				this.RenderContactProperties(contactRecipientProperties);
				return;
			}
			case AddressOrigin.Directory:
			{
				if (text == null)
				{
					throw new OwaInvalidRequestException("Missing email address.");
				}
				ADObjectId adObjectId = null;
				if (base.IsParameterSet("id"))
				{
					adObjectId = DirectoryAssistance.ParseADObjectId((string)base.GetParameter("id"));
				}
				RecipientWellEventHandler.ADRecipientProperties adrecipientProperties = this.GetADRecipientProperties(adObjectId, text, routingType);
				this.Writer.Write("<div>");
				if (string.IsNullOrEmpty(adrecipientProperties.Office))
				{
					this.Writer.Write("<span id=\"{0}\">{1}</span>", "AdOf", LocalizedStrings.GetHtmlEncoded(1766818386));
				}
				else
				{
					this.Writer.Write("<span id=\"{0}\">{1}</span>", "AdOf", Utilities.HtmlEncode(adrecipientProperties.Office));
				}
				if (string.IsNullOrEmpty(adrecipientProperties.Phone))
				{
					this.Writer.Write("<span id=\"{0}\">{1}</span>", "AdPh", LocalizedStrings.GetHtmlEncoded(1766818386));
				}
				else
				{
					this.Writer.Write("<span id=\"{0}\">{1}</span>", "AdPh", Utilities.HtmlEncode(adrecipientProperties.Phone));
				}
				if (!string.IsNullOrEmpty(adrecipientProperties.Id))
				{
					this.Writer.Write("<span id=\"{0}\">{1}</span>", "AdId", Utilities.HtmlEncode(adrecipientProperties.Id));
				}
				this.Writer.Write("</div>");
				return;
			}
			case AddressOrigin.OneOff:
			{
				RecipientWellEventHandler.ContactRecipientProperties contactRecipientProperties2 = this.GetContactRecipientProperties(text, base.UserContext);
				this.RenderContactProperties(contactRecipientProperties2);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06002F12 RID: 12050 RVA: 0x0010F364 File Offset: 0x0010D564
		private void GetRecipientAnrContextMenu(TextWriter output)
		{
			RecipientAnrContextMenu recipientAnrContextMenu = new RecipientAnrContextMenu(base.UserContext);
			recipientAnrContextMenu.Render(output);
		}

		// Token: 0x06002F13 RID: 12051 RVA: 0x0010F384 File Offset: 0x0010D584
		private void ResolveOneRecipientAndRenderResults(TextWriter output)
		{
			string text = (string)base.GetParameter("n");
			if (text.IndexOf(Globals.HtmlDirectionCharacterString, StringComparison.Ordinal) != -1)
			{
				text = Utilities.RemoveHTMLDirectionCharacters(text);
			}
			List<RecipientAddress> list = new List<RecipientAddress>();
			AnrManager.Options options = this.ReadAnrOptions();
			AnrManager.ResolveOneRecipient(text, base.UserContext, list, options);
			if (list.Count == 0)
			{
				RecipientAddress recipientAddress = AnrManager.ResolveAnrStringToOneOffEmail(text, options);
				if (recipientAddress != null)
				{
					list.Add(recipientAddress);
				}
			}
			bool flag = false;
			output.Write("new Array(");
			foreach (RecipientAddress recipientAddress2 in list)
			{
				if (flag)
				{
					output.Write(",");
				}
				else
				{
					flag = true;
				}
				output.Write("new Recip(\"");
				if (recipientAddress2.DisplayName != null)
				{
					Utilities.JavascriptEncode(recipientAddress2.DisplayName, output);
				}
				output.Write("\",\"");
				if (recipientAddress2.RoutingAddress != null)
				{
					Utilities.JavascriptEncode(recipientAddress2.RoutingAddress, output);
				}
				output.Write("\",\"");
				if (recipientAddress2.SmtpAddress != null)
				{
					Utilities.JavascriptEncode(recipientAddress2.SmtpAddress, output);
				}
				output.Write("\",\"");
				if (recipientAddress2.Alias != null)
				{
					Utilities.JavascriptEncode(recipientAddress2.Alias, output);
				}
				output.Write("\",\"");
				if (recipientAddress2.RoutingType != null)
				{
					Utilities.JavascriptEncode(recipientAddress2.RoutingType, output);
				}
				output.Write("\",{0},\"", (int)recipientAddress2.AddressOrigin);
				if (recipientAddress2.ADObjectId != null)
				{
					Utilities.JavascriptEncode(Convert.ToBase64String(recipientAddress2.ADObjectId.ObjectGuid.ToByteArray()), output);
				}
				else if (recipientAddress2.StoreObjectId != null)
				{
					Utilities.JavascriptEncode(recipientAddress2.StoreObjectId.ToBase64String(), output);
				}
				output.Write("\",");
				output.Write(recipientAddress2.RecipientFlags);
				output.Write(",");
				output.Write((int)recipientAddress2.EmailAddressIndex);
				output.Write(",\"");
				if (base.UserContext.IsInstantMessageEnabled() && base.UserContext.InstantMessagingType == InstantMessagingTypeOptions.Ocs && recipientAddress2.SipUri != null)
				{
					Utilities.JavascriptEncode(recipientAddress2.SipUri, output);
				}
				output.Write("\",\"");
				if (base.UserContext.IsSmsEnabled && recipientAddress2.MobilePhoneNumber != null)
				{
					Utilities.JavascriptEncode(recipientAddress2.MobilePhoneNumber, output);
				}
				output.Write("\")");
			}
			output.Write(")");
		}

		// Token: 0x06002F14 RID: 12052 RVA: 0x0010F610 File Offset: 0x0010D810
		private void RenderContactProperties(RecipientWellEventHandler.ContactRecipientProperties contactRecipientProperties)
		{
			this.Writer.Write("<div>");
			if (string.IsNullOrEmpty(contactRecipientProperties.WorkPhone))
			{
				this.Writer.Write("<span id=\"{0}\">{1}</span>", "CWPh", LocalizedStrings.GetHtmlEncoded(1766818386));
			}
			else
			{
				this.Writer.Write("<span id=\"{0}\">{1}</span>", "CWPh", Utilities.HtmlEncode(contactRecipientProperties.WorkPhone));
			}
			if (string.IsNullOrEmpty(contactRecipientProperties.HomePhone))
			{
				this.Writer.Write("<span id=\"{0}\">{1}</span>", "CHPh", LocalizedStrings.GetHtmlEncoded(1766818386));
			}
			else
			{
				this.Writer.Write("<span id=\"{0}\">{1}</span>", "CHPh", Utilities.HtmlEncode(contactRecipientProperties.HomePhone));
			}
			if (string.IsNullOrEmpty(contactRecipientProperties.MobilePhone))
			{
				this.Writer.Write("<span id=\"{0}\">{1}</span>", "CMPh", LocalizedStrings.GetHtmlEncoded(1766818386));
			}
			else
			{
				this.Writer.Write("<span id=\"{0}\">{1}</span>", "CMPh", Utilities.HtmlEncode(contactRecipientProperties.MobilePhone));
			}
			this.Writer.Write("</div>");
		}

		// Token: 0x06002F15 RID: 12053 RVA: 0x0010F72D File Offset: 0x0010D92D
		private void ResolveIndividualWell(string name)
		{
			this.ResolveIndividualWell(name, this.ReadAnrOptions());
		}

		// Token: 0x06002F16 RID: 12054 RVA: 0x0010F73C File Offset: 0x0010D93C
		private void ResolveIndividualWell(string name, AnrManager.Options anrOptions)
		{
			string[] array = (string[])base.GetParameter(name);
			if (array == null)
			{
				return;
			}
			anrOptions.ResolveContactsFirst = base.UserContext.UserOptions.CheckNameInContactsFirst;
			if (name == "From")
			{
				anrOptions.ResolveOnlyFromAddressBook = true;
			}
			this.Writer.Write("<div id=\"{0}\">", name);
			RecipientCache recipientCache = AutoCompleteCache.TryGetCache(OwaContext.Current.UserContext);
			for (int i = 0; i < array.Length; i++)
			{
				RecipientWell.ResolveAndRenderChunk(this.Writer, array[i], null, recipientCache, base.UserContext, anrOptions);
			}
			this.Writer.Write("</div>");
		}

		// Token: 0x06002F17 RID: 12055 RVA: 0x0010F7F4 File Offset: 0x0010D9F4
		[OwaEvent("GetCache")]
		public void GetCache()
		{
			if (base.UserContext.CanActAsOwner)
			{
				RecipientCache.RunGetCacheOperationUnderDefaultExceptionHandler(delegate
				{
					this.GetCache(this.Writer, base.OwaContext, base.UserContext);
				}, this.GetHashCode());
			}
		}

		// Token: 0x06002F18 RID: 12056 RVA: 0x0010F830 File Offset: 0x0010DA30
		internal void GetCache(TextWriter writer, OwaContext owaContext, UserContext userContext)
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "RecipientWellEventHandler.GetCache");
			RecipientCache recipientCache = AutoCompleteCache.TryGetCache(userContext);
			if (recipientCache != null)
			{
				recipientCache.Sort();
				bool flag = false;
				if (!userContext.IsFeatureEnabled(Feature.Contacts))
				{
					flag = true;
				}
				bool flag2 = false;
				for (int i = 0; i < recipientCache.CacheLength; i++)
				{
					if (!flag || recipientCache.CacheEntries[i].AddressOrigin != AddressOrigin.Store)
					{
						if (flag2)
						{
							writer.Write(",");
						}
						flag2 = true;
						AutoCompleteCacheEntry.RenderEntryJavascript(writer, recipientCache.CacheEntries[i]);
					}
				}
			}
		}

		// Token: 0x06002F19 RID: 12057 RVA: 0x0010F8BD File Offset: 0x0010DABD
		[OwaEvent("SvCchBE")]
		public void SaveRecipientCacheInBE()
		{
			base.OwaContext.UserContext.CommitRecipientCaches();
		}

		// Token: 0x06002F1A RID: 12058 RVA: 0x0010F8D0 File Offset: 0x0010DAD0
		[OwaEvent("DeleteFromCache")]
		[OwaEventParameter("em", typeof(string), false, false)]
		[OwaEventParameter("id", typeof(string), false, false)]
		public void DeleteFromCache()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "RecipientWellEventHandler.DeleteFromCache");
			string email = (string)base.GetParameter("em");
			string id = (string)base.GetParameter("id");
			RecipientCache recipientCache = AutoCompleteCache.TryGetCache(base.OwaContext.UserContext);
			if (recipientCache != null)
			{
				recipientCache.RemoveEntry(email, id);
			}
		}

		// Token: 0x06002F1B RID: 12059 RVA: 0x0010F934 File Offset: 0x0010DB34
		[OwaEvent("GetSFCache")]
		public void GetSendFromCache()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "RecipientWellEventHandler.GetSendFromCache");
			RecipientInfoCacheEntry entry = AutoCompleteCacheEntry.ParseLogonExchangePrincipal(base.OwaContext.UserContext.ExchangePrincipal, base.OwaContext.UserContext.SipUri, base.OwaContext.UserContext.MobilePhoneNumber);
			AutoCompleteCacheEntry.RenderEntryJavascript(this.Writer, entry);
			this.Writer.Write(";");
			SubscriptionCache cache = SubscriptionCache.GetCache(base.OwaContext.UserContext);
			cache.RenderToJavascript(this.Writer);
			this.Writer.Write(";");
			SendFromCache sendFromCache = SendFromCache.TryGetCache(base.OwaContext.UserContext);
			if (sendFromCache != null)
			{
				sendFromCache.Sort();
				sendFromCache.RenderToJavascript(this.Writer);
			}
		}

		// Token: 0x06002F1C RID: 12060 RVA: 0x0010F9FC File Offset: 0x0010DBFC
		[OwaEvent("GetRecipientContextMenu")]
		[OwaEventParameter("recipJnkMnuType", typeof(RecipientJunkEmailContextMenuType), false, true)]
		public void GetRecipientContextMenu()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "RecipientWellEventHandler.GetRecipientContextMenu");
			RecipientJunkEmailContextMenuType recipientJunkEmailContextMenuType = RecipientJunkEmailContextMenuType.None;
			if (base.IsParameterSet("recipJnkMnuType"))
			{
				recipientJunkEmailContextMenuType = (RecipientJunkEmailContextMenuType)base.GetParameter("recipJnkMnuType");
			}
			RecipientContextMenu recipientContextMenu = new RecipientContextMenu(base.UserContext, recipientJunkEmailContextMenuType);
			recipientContextMenu.Render(this.Writer);
		}

		// Token: 0x06002F1D RID: 12061 RVA: 0x0010FA58 File Offset: 0x0010DC58
		[OwaEvent("GetOpnMbxRcp")]
		public void GetOpenMailboxRecipientWell()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "RecipientWellEventHandler.GetOpenMailboxRecipientWell");
			this.Writer.Write("<div id=divMbxCntr ");
			RenderingUtilities.RenderAttribute(this.Writer, "L_OpnMbx", LocalizedStrings.GetNonEncoded(464289237));
			RenderingUtilities.RenderAttribute(this.Writer, "L_CnclMbx", LocalizedStrings.GetNonEncoded(-1936577052));
			RenderingUtilities.RenderAttribute(this.Writer, " L_ErrMbx", LocalizedStrings.GetNonEncoded(-829086472));
			RenderingUtilities.RenderAttribute(this.Writer, "L_OpnOtMbx", LocalizedStrings.GetNonEncoded(-157319556));
			RenderingUtilities.RenderAttribute(this.Writer, "L_SlctMbx", LocalizedStrings.GetNonEncoded(-965833082));
			RenderingUtilities.RenderAttribute(this.Writer, "L_ErrMbxTtl", LocalizedStrings.GetNonEncoded(-14431770));
			this.Writer.Write(" >");
			EmptyRecipientWell emptyRecipientWell = new EmptyRecipientWell();
			emptyRecipientWell.Render(this.Writer, base.UserContext, RecipientWellType.OpenMailbox, RecipientWell.RenderFlags.None, string.Empty, string.Empty, string.Empty);
			this.Writer.Write("</div>");
		}

		// Token: 0x06002F1E RID: 12062 RVA: 0x0010FB74 File Offset: 0x0010DD74
		[OwaEvent("GetOpnMbxFldRcp")]
		public void GetOpenOtherMailboxFolderRecipientWell()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "RecipientWellEventHandler.GetOpenOtherMailboxFolderRecipientWell");
			this.Writer.Write("<div id=divPayLoad ");
			RenderingUtilities.RenderAttribute(this.Writer, "L_OpnInbx", LocalizedStrings.GetNonEncoded(-887616272));
			RenderingUtilities.RenderAttribute(this.Writer, "L_OpnCal", LocalizedStrings.GetNonEncoded(1936872779));
			RenderingUtilities.RenderAttribute(this.Writer, "L_OpnCnts", LocalizedStrings.GetNonEncoded(2042364774));
			RenderingUtilities.RenderAttribute(this.Writer, "L_OpnTsks", LocalizedStrings.GetNonEncoded(-1870011529));
			RenderingUtilities.RenderAttribute(this.Writer, "L_Nm", LocalizedStrings.GetNonEncoded(1226316874));
			this.Writer.Write(" >");
			this.RenderRecipientWellControl(1226316874, RecipientWellType.OpenOtherMailboxFolder);
			this.Writer.Write("</div>");
		}

		// Token: 0x06002F1F RID: 12063 RVA: 0x0010FC58 File Offset: 0x0010DE58
		public static void RenderRecipientWellControl(Strings.IDs addressBookButtonLabel, string id, UserContext userContext, TextWriter writer)
		{
			RecipientWellEventHandler.RenderRecipientWellContainerBegin(addressBookButtonLabel, userContext, writer);
			EmptyRecipientWell emptyRecipientWell = new EmptyRecipientWell();
			emptyRecipientWell.Render(writer, userContext, RecipientWellType.To, RecipientWell.RenderFlags.None, id, string.Empty, string.Empty);
			RecipientWellEventHandler.RenderRecipientWellContainerEnd(writer);
		}

		// Token: 0x06002F20 RID: 12064 RVA: 0x0010FC90 File Offset: 0x0010DE90
		public void RenderRecipientWellControl(Strings.IDs addressBookButtonLabel, RecipientWellType recipientWellType)
		{
			RecipientWellEventHandler.RenderRecipientWellContainerBegin(addressBookButtonLabel, base.UserContext, this.Writer);
			EmptyRecipientWell emptyRecipientWell = new EmptyRecipientWell();
			emptyRecipientWell.Render(this.Writer, base.UserContext, recipientWellType, RecipientWell.RenderFlags.None, string.Empty, string.Empty, string.Empty);
			RecipientWellEventHandler.RenderRecipientWellContainerEnd(this.Writer);
		}

		// Token: 0x06002F21 RID: 12065 RVA: 0x0010FCE4 File Offset: 0x0010DEE4
		private static void RenderRecipientWellContainerBegin(Strings.IDs addressBookButtonLabel, UserContext userContext, TextWriter writer)
		{
			writer.Write("<div id=\"divRecipPck\">");
			writer.Write("<a id=\"btnTo\" class=\"flatHvr addrBkBtn\" href=\"#\">");
			userContext.RenderThemeImage(writer, ThemeFileId.AddressBook, "vaM", new object[0]);
			writer.Write("&nbsp;<span class=\"vaM\">");
			writer.Write(LocalizedStrings.GetHtmlEncoded(addressBookButtonLabel));
			writer.Write("</span></a>");
		}

		// Token: 0x06002F22 RID: 12066 RVA: 0x0010FD3D File Offset: 0x0010DF3D
		private static void RenderRecipientWellContainerEnd(TextWriter writer)
		{
			writer.Write("</div>");
		}

		// Token: 0x06002F23 RID: 12067 RVA: 0x0010FD4C File Offset: 0x0010DF4C
		private RecipientWellEventHandler.ADRecipientProperties GetADRecipientProperties(ADObjectId adObjectId, string routingAddress, string routingType)
		{
			if (string.IsNullOrEmpty(routingAddress))
			{
				throw new ArgumentNullException("routingAddress");
			}
			if (string.IsNullOrEmpty(routingType))
			{
				throw new ArgumentNullException("routingType");
			}
			IRecipientSession recipientSession = Utilities.CreateADRecipientSession(Culture.GetUserCulture().LCID, true, ConsistencyMode.IgnoreInvalid, true, base.UserContext);
			ADRecipient adrecipient = null;
			RecipientWellEventHandler.ADRecipientProperties result = default(RecipientWellEventHandler.ADRecipientProperties);
			if (string.Equals(routingType, "EX", StringComparison.Ordinal))
			{
				try
				{
					CustomProxyAddress proxyAddress = new CustomProxyAddress((CustomProxyAddressPrefix)ProxyAddressPrefix.LegacyDN, routingAddress, true);
					adrecipient = recipientSession.FindByProxyAddress(proxyAddress);
					goto IL_FB;
				}
				catch (NonUniqueRecipientException ex)
				{
					ExTraceGlobals.CoreTracer.TraceDebug<string>((long)this.GetHashCode(), "GetADRecipientProperties: NonUniqueRecipientException was thrown by FindByProxyAddress: {0}", ex.Message);
					throw new OwaEventHandlerException("Unable to retrieve properties.", LocalizedStrings.GetNonEncoded(-1953304495));
				}
			}
			if (string.Equals(routingType, "SMTP", StringComparison.Ordinal))
			{
				try
				{
					SmtpProxyAddress proxyAddress2 = new SmtpProxyAddress(routingAddress, true);
					adrecipient = recipientSession.FindByProxyAddress(proxyAddress2);
				}
				catch (NonUniqueRecipientException ex2)
				{
					ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "GetADRecipientProperties: NonUniqueRecipientException was thrown by FindByProxyAddress: {0}", ex2.Message);
					throw new OwaEventHandlerException("Unable to retrieve properties.", LocalizedStrings.GetNonEncoded(-1953304495));
				}
			}
			IL_FB:
			if (adrecipient == null && adObjectId != null)
			{
				adrecipient = recipientSession.Read(adObjectId);
			}
			if (adrecipient == null)
			{
				throw new OwaEventHandlerException("Unable to retrieve properties.", LocalizedStrings.GetNonEncoded(-1953304495));
			}
			IADOrgPerson iadorgPerson = adrecipient as IADOrgPerson;
			if (iadorgPerson != null)
			{
				result.Office = iadorgPerson.Office;
				result.Phone = iadorgPerson.Phone;
				if (adrecipient.Id == null)
				{
					throw new OwaEventHandlerException("Unable to retrieve id for AD Recipient.", LocalizedStrings.GetNonEncoded(-1953304495));
				}
				result.Id = DirectoryAssistance.ToHtmlString(adrecipient.Id);
			}
			return result;
		}

		// Token: 0x06002F24 RID: 12068 RVA: 0x0010FEF0 File Offset: 0x0010E0F0
		public RecipientWellEventHandler.ContactRecipientProperties GetContactRecipientProperties(StoreObjectId contactId, UserContext userContext)
		{
			if (contactId == null)
			{
				throw new ArgumentNullException("contactId");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			Contact contact = null;
			RecipientWellEventHandler.ContactRecipientProperties result = default(RecipientWellEventHandler.ContactRecipientProperties);
			try
			{
				PropertyDefinition[] propsToReturn = new PropertyDefinition[]
				{
					ContactSchema.BusinessPhoneNumber,
					ContactSchema.MobilePhone,
					ContactSchema.HomePhone
				};
				contact = Contact.Bind(userContext.MailboxSession, contactId, propsToReturn);
				result.WorkPhone = (contact.TryGetProperty(ContactSchema.BusinessPhoneNumber) as string);
				result.HomePhone = (contact.TryGetProperty(ContactSchema.HomePhone) as string);
				result.MobilePhone = (contact.TryGetProperty(ContactSchema.MobilePhone) as string);
			}
			catch (ObjectNotFoundException arg)
			{
				ExTraceGlobals.ContactsDataTracer.TraceDebug<ObjectNotFoundException>(0L, "Contact item could not be found.  Exception: {0}", arg);
				base.RenderPartialFailure(string.Empty, null, ButtonDialogIcon.NotSet, OwaEventHandlerErrorCode.NotSet);
			}
			finally
			{
				if (contact != null)
				{
					contact.Dispose();
					contact = null;
				}
			}
			return result;
		}

		// Token: 0x06002F25 RID: 12069 RVA: 0x0010FFF0 File Offset: 0x0010E1F0
		public RecipientWellEventHandler.ContactRecipientProperties GetContactRecipientProperties(string email, UserContext userContext)
		{
			if (email == null)
			{
				throw new ArgumentNullException("email");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			RecipientWellEventHandler.ContactRecipientProperties result = default(RecipientWellEventHandler.ContactRecipientProperties);
			using (ContactsFolder contactsFolder = ContactsFolder.Bind(userContext.MailboxSession, DefaultFolderType.Contacts))
			{
				using (FindInfo<Contact> findInfo = contactsFolder.FindByEmailAddress(email, new PropertyDefinition[0]))
				{
					if (findInfo.FindStatus == FindStatus.Found)
					{
						Contact result2 = findInfo.Result;
						result2.Load(new PropertyDefinition[]
						{
							ContactSchema.BusinessPhoneNumber,
							ContactSchema.MobilePhone,
							ContactSchema.HomePhone
						});
						result.WorkPhone = (result2.TryGetProperty(ContactSchema.BusinessPhoneNumber) as string);
						result.HomePhone = (result2.TryGetProperty(ContactSchema.HomePhone) as string);
						result.MobilePhone = (result2.TryGetProperty(ContactSchema.MobilePhone) as string);
					}
					else
					{
						ExTraceGlobals.ContactsDataTracer.TraceDebug(0L, "Contact item could not be found.");
						base.RenderPartialFailure(string.Empty, null, ButtonDialogIcon.NotSet, OwaEventHandlerErrorCode.NotSet);
					}
				}
			}
			return result;
		}

		// Token: 0x06002F26 RID: 12070 RVA: 0x0011011C File Offset: 0x0010E31C
		[OwaEvent("ExpandPDL")]
		[OwaEventParameter("Recips", typeof(RecipientInfo), true)]
		public void ExpandDistributionList()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "RecipientWellEventHandler.ExpandDistributionList");
			RecipientInfo[] array = (RecipientInfo[])base.GetParameter("Recips");
			List<Participant> list = new List<Participant>();
			foreach (RecipientInfo recipientInfo in array)
			{
				if (recipientInfo.StoreObjectId == null || !Utilities.IsMapiPDL(recipientInfo.RoutingType))
				{
					throw new OwaEventHandlerException("The requested recipient is not personal distribution list");
				}
				list.AddRange(DistributionList.ExpandDeep(base.UserContext.MailboxSession, recipientInfo.StoreObjectId));
			}
			using (List<Participant>.Enumerator enumerator = list.GetEnumerator())
			{
				AdRecipientBatchQuery adRecipientBatchQuery = new AdRecipientBatchQuery(enumerator, base.UserContext);
				bool flag = false;
				foreach (Participant participant in list)
				{
					string smtpAddress = null;
					string sipUri = null;
					ADObjectId adObjectId = null;
					StoreObjectId storeObjectId = null;
					EmailAddressIndex emailAddressIndex = EmailAddressIndex.None;
					string mobilePhoneNumber = null;
					if (participant.RoutingType == "EX")
					{
						ADRecipient adRecipient = adRecipientBatchQuery.GetAdRecipient(participant.EmailAddress);
						if (adRecipient != null)
						{
							smtpAddress = adRecipient.PrimarySmtpAddress.ToString();
							adObjectId = adRecipient.Id;
							sipUri = InstantMessageUtilities.GetSipUri((ProxyAddressCollection)adRecipient[ADRecipientSchema.EmailAddresses]);
							mobilePhoneNumber = Utilities.NormalizePhoneNumber((string)adRecipient[ADOrgPersonSchema.MobilePhone]);
						}
					}
					else
					{
						smtpAddress = participant.EmailAddress;
						sipUri = participant.EmailAddress;
					}
					RecipientAddress.RecipientAddressFlags recipientAddressFlags = RecipientAddress.RecipientAddressFlags.None;
					if (participant.GetValueOrDefault<bool>(ParticipantSchema.IsDistributionList))
					{
						recipientAddressFlags |= RecipientAddress.RecipientAddressFlags.DistributionList;
					}
					else if (participant.GetValueOrDefault<bool>(ParticipantSchema.IsRoom))
					{
						recipientAddressFlags |= RecipientAddress.RecipientAddressFlags.Room;
					}
					StoreParticipantOrigin storeParticipantOrigin = participant.Origin as StoreParticipantOrigin;
					if (storeParticipantOrigin != null)
					{
						storeObjectId = storeParticipantOrigin.OriginItemId;
						emailAddressIndex = storeParticipantOrigin.EmailAddressIndex;
					}
					RecipientWellNode.Render(this.Writer, base.UserContext, participant.DisplayName, smtpAddress, participant.EmailAddress, participant.RoutingType, participant.GetValueOrDefault<string>(ParticipantSchema.Alias), RecipientAddress.ToAddressOrigin(participant), (int)recipientAddressFlags, storeObjectId, emailAddressIndex, adObjectId, flag ? RecipientWellNode.RenderFlags.RenderCommas : RecipientWellNode.RenderFlags.None, sipUri, mobilePhoneNumber);
					flag = true;
				}
			}
		}

		// Token: 0x040020EE RID: 8430
		public const string EventNamespace = "RecipWell";

		// Token: 0x040020EF RID: 8431
		public const string MethodResolve = "Resolve";

		// Token: 0x040020F0 RID: 8432
		public const string MethodResolveIndividualWell = "ResolveIndividualWell";

		// Token: 0x040020F1 RID: 8433
		public const string MethodResolveOneRecipient = "ResolveOneRecipient";

		// Token: 0x040020F2 RID: 8434
		public const string MethodResolveOneRecipientForAnrMenu = "ResolveOneRecipientForAnrMenu";

		// Token: 0x040020F3 RID: 8435
		public const string MethodGetRecipientMenuProperties = "GetRecipProps";

		// Token: 0x040020F4 RID: 8436
		public const string MethodGetRecipientContextMenu = "GetRecipientContextMenu";

		// Token: 0x040020F5 RID: 8437
		public const string MethodGetCache = "GetCache";

		// Token: 0x040020F6 RID: 8438
		public const string MethodGetSendFromCache = "GetSFCache";

		// Token: 0x040020F7 RID: 8439
		public const string MethodDeleteFromCache = "DeleteFromCache";

		// Token: 0x040020F8 RID: 8440
		public const string MethodGetOpenMailboxRecipientWell = "GetOpnMbxRcp";

		// Token: 0x040020F9 RID: 8441
		public const string MethodGetOpenOtherMailboxFolderRecipientWell = "GetOpnMbxFldRcp";

		// Token: 0x040020FA RID: 8442
		public const string MethodUpdateCache = "UpdateAC";

		// Token: 0x040020FB RID: 8443
		public const string MethodExpandDistributionList = "ExpandPDL";

		// Token: 0x040020FC RID: 8444
		public const string MethodSaveRecipientCacheInBE = "SvCchBE";

		// Token: 0x040020FD RID: 8445
		public const string To = "To";

		// Token: 0x040020FE RID: 8446
		public const string Cc = "Cc";

		// Token: 0x040020FF RID: 8447
		public const string Bcc = "Bcc";

		// Token: 0x04002100 RID: 8448
		public const string From = "From";

		// Token: 0x04002101 RID: 8449
		public const string Recipients = "Recips";

		// Token: 0x04002102 RID: 8450
		public const string FromAddressBook = "AB";

		// Token: 0x04002103 RID: 8451
		public const string FindSomeoneAnr = "FSoneAnr";

		// Token: 0x04002104 RID: 8452
		public const string DefaultRoutingType = "DefaultRT";

		// Token: 0x04002105 RID: 8453
		public const string OnlyAllowDefaultRoutingType = "OnlyDefaultRT";

		// Token: 0x04002106 RID: 8454
		public const string RecipientBlockType = "RBT";

		// Token: 0x04002107 RID: 8455
		public const string Name = "n";

		// Token: 0x04002108 RID: 8456
		public const string Origin = "ao";

		// Token: 0x04002109 RID: 8457
		public const string Id = "id";

		// Token: 0x0400210A RID: 8458
		public const string Email = "em";

		// Token: 0x0400210B RID: 8459
		public const string RoutingType = "rt";

		// Token: 0x0400210C RID: 8460
		public const string RecipientJunkEmailMenuType = "recipJnkMnuType";

		// Token: 0x0400210D RID: 8461
		public const string AddMenuMarkup = "AddMenuMarkup";

		// Token: 0x0400210E RID: 8462
		public const string AddRecipientResults = "AddRecipientResults";

		// Token: 0x0400210F RID: 8463
		private const string SpanTag = "<span id=\"{0}\">{1}</span>";

		// Token: 0x020004D3 RID: 1235
		public struct ADRecipientProperties
		{
			// Token: 0x04002110 RID: 8464
			public string Office;

			// Token: 0x04002111 RID: 8465
			public string Phone;

			// Token: 0x04002112 RID: 8466
			public string Id;
		}

		// Token: 0x020004D4 RID: 1236
		public struct ContactRecipientProperties
		{
			// Token: 0x04002113 RID: 8467
			public string WorkPhone;

			// Token: 0x04002114 RID: 8468
			public string HomePhone;

			// Token: 0x04002115 RID: 8469
			public string MobilePhone;
		}
	}
}
