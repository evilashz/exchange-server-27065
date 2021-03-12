using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Clients.EventLogs;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000147 RID: 327
	internal sealed class InstantMessageUtilities
	{
		// Token: 0x06000B1D RID: 2845 RVA: 0x0004EC14 File Offset: 0x0004CE14
		private InstantMessageUtilities()
		{
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0004EC1C File Offset: 0x0004CE1C
		internal static string GetUserPrincipalName(string emailAddress, UserContext userContext)
		{
			string text = string.Empty;
			try
			{
				IRecipientSession recipientSession = Utilities.CreateADRecipientSession(ConsistencyMode.IgnoreInvalid, userContext);
				ProxyAddress proxyAddress = ProxyAddress.Parse(emailAddress);
				ADRecipient adrecipient = recipientSession.FindByProxyAddress(proxyAddress);
				if (adrecipient != null)
				{
					text = adrecipient[ADUserSchema.UserPrincipalName].ToString();
				}
			}
			catch (Exception ex)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "IntantMessageUtilities.GetUserPrincipalName: Exception was thrown by User: {0}", ex.Message);
			}
			if (string.IsNullOrEmpty(text))
			{
				text = emailAddress;
			}
			return text;
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0004EC98 File Offset: 0x0004CE98
		internal static void SendProxiesToClient(string[] upns, UserContext userContext)
		{
			Dictionary<string, string[]> proxyAddresses = new Dictionary<string, string[]>();
			try
			{
				proxyAddresses = InstantMessageUtilities.GetProxyAddresses(upns, userContext);
			}
			catch (ThreadAbortException)
			{
			}
			catch (Exception exception)
			{
				InstantMessageUtilities.SendWatsonReport("InstantMessageUtilities.GetProxyAddresses", userContext, exception);
			}
			if (userContext != null && userContext.InstantMessageManager != null && userContext.InstantMessageManager.Provider != null && userContext.InstantMessageManager.Provider.Payload != null)
			{
				InstantMessagePayloadUtilities.GenerateProxiesPayload(userContext.InstantMessageManager.Provider.Payload, proxyAddresses);
			}
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0004ED24 File Offset: 0x0004CF24
		private static Dictionary<string, string[]> GetProxyAddresses(string[] upns, UserContext userContext)
		{
			Dictionary<string, string[]> dictionary = new Dictionary<string, string[]>();
			IRecipientSession recipientSession = Utilities.CreateADRecipientSession(ConsistencyMode.IgnoreInvalid, userContext);
			Result<OWAMiniRecipient>[] array = recipientSession.FindOWAMiniRecipientByUserPrincipalName(upns);
			for (int i = 0; i < array.Length; i++)
			{
				OWAMiniRecipient data = array[i].Data;
				string text = data.UserPrincipalName.ToString();
				if (!string.IsNullOrEmpty(text) && !dictionary.ContainsKey(text))
				{
					dictionary.Add(text, InstantMessageUtilities.GetProxyAddressesForRecipient(data));
				}
			}
			return dictionary;
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0004ED94 File Offset: 0x0004CF94
		private static string[] GetProxyAddressesForRecipient(OWAMiniRecipient owaMiniRecipient)
		{
			ProxyAddressCollection emailAddresses = owaMiniRecipient.EmailAddresses;
			string[] array;
			if (emailAddresses != null && emailAddresses.Count > 0)
			{
				array = new string[emailAddresses.Count];
				for (int i = 0; i < emailAddresses.Count; i++)
				{
					array[i] = emailAddresses[i].AddressString;
				}
			}
			else
			{
				array = new string[0];
			}
			return array;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0004EDEC File Offset: 0x0004CFEC
		internal static void SetSignedOutFlag(UserContext userContext, bool signedOut)
		{
			try
			{
				using (Folder folder = Utilities.SafeFolderBind(userContext.MailboxSession, DefaultFolderType.Root, new PropertyDefinition[]
				{
					ViewStateProperties.SignedOutOfIM
				}))
				{
					if (folder != null)
					{
						folder[ViewStateProperties.SignedOutOfIM] = signedOut;
						folder.Save();
					}
				}
			}
			catch (Exception arg)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceError<Exception>((long)userContext.GetHashCode(), "InstantMessageUtilities.SetSignedOutFlag. Exception message is {0}.", arg);
			}
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0004EE78 File Offset: 0x0004D078
		internal static bool IsSignedOut(UserContext userContext)
		{
			try
			{
				userContext.Lock();
				using (Folder folder = Utilities.SafeFolderBind(userContext.MailboxSession, DefaultFolderType.Root, new PropertyDefinition[]
				{
					ViewStateProperties.SignedOutOfIM
				}))
				{
					if (folder != null)
					{
						bool defaultValue = false;
						return Utilities.GetFolderProperty<bool>(folder, ViewStateProperties.SignedOutOfIM, defaultValue);
					}
				}
			}
			catch (Exception arg)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceError<Exception>((long)userContext.GetHashCode(), "InstantMessageUtilities.IsSignedOut. Exception message is {0}.", arg);
			}
			finally
			{
				if (userContext.LockedByCurrentThread())
				{
					userContext.Unlock();
				}
			}
			return false;
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0004EF28 File Offset: 0x0004D128
		internal static string GetSipUri(ADUser adUser)
		{
			return InstantMessageUtilities.GetSipUri(adUser.EmailAddresses);
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0004EF35 File Offset: 0x0004D135
		internal static string GetSipUri(OWAMiniRecipient owaMiniRecipient)
		{
			return InstantMessageUtilities.GetSipUri(owaMiniRecipient.EmailAddresses);
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0004EF42 File Offset: 0x0004D142
		internal static string GetSipUri(ADRecipient adRecipient)
		{
			return InstantMessageUtilities.GetSipUri(adRecipient.EmailAddresses);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0004EF50 File Offset: 0x0004D150
		internal static string GetSipUri(ProxyAddressCollection proxyAddresses)
		{
			foreach (ProxyAddress proxyAddress in proxyAddresses)
			{
				if (proxyAddress.ProxyAddressString.StartsWith("sip:", StringComparison.OrdinalIgnoreCase))
				{
					return proxyAddress.ProxyAddressString.ToLowerInvariant();
				}
			}
			return null;
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0004EFBC File Offset: 0x0004D1BC
		internal static string ToSipFormat(string imAddress)
		{
			if (imAddress == null)
			{
				throw new ArgumentNullException("imAddress");
			}
			return InstantMessageUtilities.PrefixString("sip:", imAddress);
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0004EFD7 File Offset: 0x0004D1D7
		internal static string ToGroupFormat(string groupId)
		{
			if (groupId == null)
			{
				throw new ArgumentNullException("groupId");
			}
			return InstantMessageUtilities.PrefixString("grp:", groupId);
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0004EFF4 File Offset: 0x0004D1F4
		internal static string FromSipFormat(string sipAddress)
		{
			if (string.IsNullOrEmpty(sipAddress))
			{
				return string.Empty;
			}
			if (sipAddress.Length > "sip:".Length && string.Compare(sipAddress.Substring(0, "sip:".Length), "sip:", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return sipAddress.Substring("sip:".Length);
			}
			return sipAddress;
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0004F054 File Offset: 0x0004D254
		public static string GetExtraWatsonData(UserContext userContext)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("OWA Version: ");
			stringBuilder.Append(Globals.ApplicationVersion);
			stringBuilder.AppendLine();
			if (userContext != null && !Globals.DisableBreadcrumbs)
			{
				stringBuilder.AppendLine();
				stringBuilder.AppendLine();
				stringBuilder.AppendLine(userContext.DumpBreadcrumbs());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0004F0B1 File Offset: 0x0004D2B1
		internal static void SendWatsonReport(string methodName, UserContext userContext, Exception exception)
		{
			ExTraceGlobals.InstantMessagingTracer.TraceError<string, string>(0L, "{0} failed. {1}", methodName, (exception.Message != null) ? exception.Message : string.Empty);
			InstantMessageUtilities.SendInstantMessageWatsonReport(userContext, exception);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0004F0E4 File Offset: 0x0004D2E4
		internal static void SendInstantMessageWatsonReport(UserContext userContext, Exception exception)
		{
			ExTraceGlobals.InstantMessagingTracer.TraceDebug<Type, string>(0L, "Exception: Type: {0} Error: {1}.", exception.GetType(), exception.Message);
			if (Globals.SendWatsonReports)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceDebug(0L, "Sending watson report");
				ReportOptions options = (exception is AccessViolationException || exception is InvalidProgramException || exception is TypeInitializationException) ? ReportOptions.ReportTerminateAfterSend : ReportOptions.None;
				ExWatson.AddExtraData(InstantMessageUtilities.GetExtraWatsonData(userContext));
				ExWatson.SendReport(exception, options, null);
			}
			if (exception is AccessViolationException)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceDebug(0L, "Shutting down OWA due to unrecoverable exception");
				Environment.Exit(1);
				return;
			}
			if ((exception is InvalidProgramException || exception is TypeInitializationException) && Interlocked.Exchange(ref InstantMessageUtilities.queuedDelayedRestart, 1) == 0)
			{
				new Thread(new ThreadStart(InstantMessageUtilities.DelayedRestartUponUnexecutableCode)).Start();
			}
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0004F1A9 File Offset: 0x0004D3A9
		private static string PrefixString(string prefix, string original)
		{
			if (original.Length > prefix.Length && string.Compare(original.Substring(0, prefix.Length), prefix, StringComparison.OrdinalIgnoreCase) == 0)
			{
				return original;
			}
			return prefix + original;
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0004F1D8 File Offset: 0x0004D3D8
		private static void DelayedRestartUponUnexecutableCode()
		{
			Thread.Sleep(90000);
			OwaDiagnostics.Logger.LogEvent(ClientsEventLogConstants.Tuple_OwaRestartingAfterFailedLoad, string.Empty, new object[0]);
			Environment.Exit(1);
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0004F208 File Offset: 0x0004D408
		public static void GetItemIMInfo(OwaStoreObjectId itemId, bool getNormalizedSubject, UserContext userContext, out string displayName, out string emailAddress, out string sipUri, out string subject)
		{
			if (itemId == null)
			{
				throw new ArgumentNullException("itemId");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			PropertyDefinition[] prefetchProperties = new PropertyDefinition[]
			{
				MessageItemSchema.SenderDisplayName,
				getNormalizedSubject ? ItemSchema.NormalizedSubject : ItemSchema.Subject,
				MessageItemSchema.SenderEmailAddress,
				MessageItemSchema.SenderAddressType
			};
			sipUri = string.Empty;
			using (Item item = Utilities.GetItem<Item>(userContext, itemId, prefetchProperties))
			{
				string text = string.Empty;
				object obj = item.TryGetProperty(MessageItemSchema.SenderAddressType);
				if (!(obj is PropertyError))
				{
					text = (string)obj;
				}
				emailAddress = string.Empty;
				obj = item.TryGetProperty(MessageItemSchema.SenderEmailAddress);
				if (!(obj is PropertyError))
				{
					emailAddress = (string)obj;
				}
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(emailAddress))
				{
					if (string.CompareOrdinal(text, "EX") == 0)
					{
						try
						{
							IRecipientSession recipientSession = Utilities.CreateADRecipientSession(ConsistencyMode.IgnoreInvalid, userContext);
							ADRecipient adrecipient = recipientSession.FindByLegacyExchangeDN(emailAddress);
							if (adrecipient != null)
							{
								sipUri = InstantMessageUtilities.GetSipUri(adrecipient);
							}
							goto IL_12E;
						}
						catch (NonUniqueRecipientException ex)
						{
							ExTraceGlobals.InstantMessagingTracer.TraceDebug<string>(0L, "IntantMessageUtilities.GetItemIMInfo: NonUniqueRecipientException was thrown by User: {0}", ex.Message);
							goto IL_12E;
						}
					}
					if (string.CompareOrdinal(text, "SMTP") == 0 && emailAddress != null)
					{
						sipUri = ContactUtilities.GetContactRecipientIMAddress(emailAddress, userContext, true);
					}
				}
				IL_12E:
				subject = string.Empty;
				obj = item.TryGetProperty(getNormalizedSubject ? ItemSchema.NormalizedSubject : ItemSchema.Subject);
				if (!(obj is PropertyError))
				{
					subject = (string)obj;
				}
				displayName = string.Empty;
				obj = item.TryGetProperty(MessageItemSchema.SenderDisplayName);
				if (!(obj is PropertyError))
				{
					displayName = (string)obj;
				}
			}
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0004F3D8 File Offset: 0x0004D5D8
		public static void RenderSingleEmailAddress(IListViewDataSource dataSource, TextWriter writer, string displayName, string emailAddress, string emailAddressForDisplay, string legacyExchangeDN, EmailAddressIndex emailAddressIndex, RecipientAddress.RecipientAddressFlags recipientAddressFlags, string routingType, string sipUri, string mobilePhoneNumber)
		{
			InstantMessageUtilities.RenderSingleEmailAddress(dataSource, writer, displayName, emailAddress, emailAddressForDisplay, legacyExchangeDN, emailAddressIndex, recipientAddressFlags, routingType, sipUri, mobilePhoneNumber, true);
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0004F400 File Offset: 0x0004D600
		public static void RenderSingleEmailAddress(IListViewDataSource dataSource, TextWriter writer, string displayName, string emailAddress, string emailAddressForDisplay, string legacyExchangeDN, EmailAddressIndex emailAddressIndex, RecipientAddress.RecipientAddressFlags recipientAddressFlags, string routingType, string sipUri, string mobilePhoneNumber, bool htmlEncodeEmailAddress)
		{
			writer.Write("<span id=\"ea\"");
			if (dataSource is FolderListViewDataSource)
			{
				writer.Write(" aO=1");
			}
			else
			{
				writer.Write(" aO=2");
			}
			writer.Write(" dn=\"");
			if (htmlEncodeEmailAddress)
			{
				Utilities.HtmlEncode(displayName, writer);
			}
			else
			{
				writer.Write(displayName);
			}
			writer.Write("\" rf=");
			int num = (int)recipientAddressFlags;
			Utilities.HtmlEncode(num.ToString(CultureInfo.InvariantCulture), writer);
			writer.Write(" rt=\"");
			if (!string.IsNullOrEmpty(routingType))
			{
				writer.Write(routingType);
			}
			else if (!string.IsNullOrEmpty(legacyExchangeDN))
			{
				writer.Write("EX\" lgDn=\"");
				Utilities.HtmlEncode(legacyExchangeDN, writer);
			}
			else if (recipientAddressFlags == RecipientAddress.RecipientAddressFlags.DistributionList)
			{
				writer.Write("MAPIPDL");
			}
			else
			{
				writer.Write("SMTP");
			}
			writer.Write("\" em=\"");
			if (htmlEncodeEmailAddress)
			{
				Utilities.HtmlEncode(emailAddress, writer);
			}
			else
			{
				writer.Write(emailAddress);
			}
			writer.Write("\" ei=\"");
			int num2 = (int)emailAddressIndex;
			Utilities.HtmlEncode(num2.ToString(), writer);
			if (!string.IsNullOrEmpty(sipUri))
			{
				writer.Write("\" uri=\"");
				if (htmlEncodeEmailAddress)
				{
					Utilities.HtmlEncode(sipUri, writer);
				}
				else
				{
					writer.Write(sipUri);
				}
			}
			if (!string.IsNullOrEmpty(mobilePhoneNumber))
			{
				writer.Write("\" mo=\"");
				if (htmlEncodeEmailAddress)
				{
					Utilities.HtmlEncode(mobilePhoneNumber, writer);
				}
				else
				{
					writer.Write(mobilePhoneNumber);
				}
			}
			writer.Write("\">");
			if (htmlEncodeEmailAddress)
			{
				Utilities.HtmlEncode(emailAddressForDisplay, writer);
			}
			else
			{
				writer.Write(emailAddressForDisplay);
			}
			writer.Write("</span>");
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0004F58C File Offset: 0x0004D78C
		public static string GetSingleEmailAddress(IListViewDataSource dataSource, string displayName, string emailAddress, string emailAddressForDisplay, string legacyExchangeDN, EmailAddressIndex emailAddressIndex, RecipientAddress.RecipientAddressFlags recipientAddressFlags, string routingType, string sipUri, string mobilePhoneNumber, bool htmlEncodeEmailAddress)
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (StringWriter stringWriter = new StringWriter(stringBuilder))
			{
				InstantMessageUtilities.RenderSingleEmailAddress(dataSource, stringWriter, displayName, emailAddress, emailAddressForDisplay, legacyExchangeDN, emailAddressIndex, recipientAddressFlags, routingType, sipUri, mobilePhoneNumber, htmlEncodeEmailAddress);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0004F5E0 File Offset: 0x0004D7E0
		public static string GetSingleEmailAddress(IListViewDataSource dataSource, string displayName, string emailAddress, string emailAddressForDisplay, string legacyExchangeDN, EmailAddressIndex emailAddressIndex, RecipientAddress.RecipientAddressFlags recipientAddressFlags, string routingType, string sipUri, string mobilePhoneNumber)
		{
			return InstantMessageUtilities.GetSingleEmailAddress(dataSource, displayName, emailAddress, emailAddressForDisplay, legacyExchangeDN, emailAddressIndex, recipientAddressFlags, routingType, sipUri, mobilePhoneNumber, true);
		}

		// Token: 0x040007F1 RID: 2033
		private const string SipPrefix = "sip:";

		// Token: 0x040007F2 RID: 2034
		private const string GroupPrefix = "grp:";

		// Token: 0x040007F3 RID: 2035
		internal const string DefaultMessageFormat = "text/plain;charset=utf-8";

		// Token: 0x040007F4 RID: 2036
		internal const int MaxConversationCount = 20;

		// Token: 0x040007F5 RID: 2037
		private static int queuedDelayedRestart;
	}
}
