using System;
using System.Security;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.InfoWorker.Common.OOF;
using Microsoft.Exchange.UM.ClientAccess;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004C9 RID: 1225
	[OwaEventNamespace("Options")]
	internal sealed class OptionsEventHandler : OwaEventHandlerBase
	{
		// Token: 0x06002EC7 RID: 11975 RVA: 0x0010CC3A File Offset: 0x0010AE3A
		public static void Register()
		{
			OwaEventRegistry.RegisterHandler(typeof(OptionsEventHandler));
		}

		// Token: 0x06002EC8 RID: 11976 RVA: 0x0010CC4B File Offset: 0x0010AE4B
		[OwaEvent("IsSmsConfigured")]
		public void IsSmsConfigured()
		{
			ExTraceGlobals.UserOptionsCallTracer.TraceDebug((long)this.GetHashCode(), "OptionsEventHandler.IsSmsConfigured");
			this.Writer.Write(UnifiedMessagingOptions.IsSMSConfigured(base.UserContext) ? 1 : 0);
		}

		// Token: 0x06002EC9 RID: 11977 RVA: 0x0010CC80 File Offset: 0x0010AE80
		[OwaEventParameter("vmPvSnd", typeof(bool), false, true)]
		[OwaEventParameter("vmPvRec", typeof(bool), false, true)]
		[OwaEventParameter("sms", typeof(int), false, true)]
		[OwaEventParameter("tpFdr", typeof(string), false, true)]
		[OwaEvent("SaveUmOpts")]
		[OwaEventSegmentation(Feature.UMIntegration)]
		[OwaEventParameter("tpNum", typeof(string), false, true)]
		[OwaEventParameter("vGr", typeof(bool), false, true)]
		[OwaEventParameter("vSo", typeof(bool), false, true)]
		[OwaEventParameter("mc", typeof(bool), false, true)]
		[OwaEventParameter("plvm", typeof(bool), false, true)]
		public void SaveUnifiedMessagingOptions()
		{
			ExTraceGlobals.UserOptionsCallTracer.TraceDebug((long)this.GetHashCode(), "OptionsEventHandler.SaveUnifiedMessagingOptions");
			using (UMClientCommon umclientCommon = new UMClientCommon(base.UserContext.ExchangePrincipal))
			{
				if (!umclientCommon.IsUMEnabled())
				{
					throw new OwaEventHandlerException("Failed to reset pin because user is not enabled for Unified Messaging", LocalizedStrings.GetNonEncoded(1301599396));
				}
				if (base.IsParameterSet("tpNum"))
				{
					umclientCommon.SetPlayOnPhoneDialString((string)base.GetParameter("tpNum"));
				}
				if (base.IsParameterSet("vGr"))
				{
					umclientCommon.SetOofStatus(!(bool)base.GetParameter("vGr"));
				}
				if (base.IsParameterSet("vSo"))
				{
					umclientCommon.SetUnReadVoiceMailReadingOrder((bool)base.GetParameter("vSo"));
				}
				if (base.IsParameterSet("mc"))
				{
					umclientCommon.SetMissedCallNotificationEnabled((bool)base.GetParameter("mc"));
				}
				if (base.IsParameterSet("plvm"))
				{
					umclientCommon.SetPinlessAccessToVoicemail((bool)base.GetParameter("plvm"));
				}
				if (base.IsParameterSet("sms"))
				{
					umclientCommon.SetSMSNotificationOption((UMSMSNotificationOptions)base.GetParameter("sms"));
				}
				if (base.IsParameterSet("vmPvRec"))
				{
					umclientCommon.SetReceivedVoiceMailPreview((bool)base.GetParameter("vmPvRec"));
				}
				if (base.IsParameterSet("vmPvSnd"))
				{
					umclientCommon.SetSentVoiceMailPreview((bool)base.GetParameter("vmPvSnd"));
				}
				if (base.IsParameterSet("tpFdr"))
				{
					string telephoneAccessFolderEmail = Utilities.ProviderSpecificIdFromStoreObjectId((string)base.GetParameter("tpFdr"));
					umclientCommon.SetTelephoneAccessFolderEmail(telephoneAccessFolderEmail);
				}
			}
		}

		// Token: 0x06002ECA RID: 11978 RVA: 0x0010CE40 File Offset: 0x0010B040
		[OwaEventParameter("vGr", typeof(bool), false, false)]
		[OwaEventSegmentation(Feature.UMIntegration)]
		[OwaEventParameter("tpNum", typeof(string), false, false)]
		[OwaEvent("RecUmGr")]
		public void RecordUnifiedMessagingGreeting()
		{
			ExTraceGlobals.UserOptionsCallTracer.TraceDebug((long)this.GetHashCode(), "OptionsEventHandler.RecordUnifiedMessagingGreeting");
			bool flag = (bool)base.GetParameter("vGr");
			string dialString = (string)base.GetParameter("tpNum");
			string lastUMCallId = base.UserContext.LastUMCallId;
			using (UMClientCommon umclientCommon = new UMClientCommon(base.UserContext.ExchangePrincipal))
			{
				if (!umclientCommon.IsPlayOnPhoneEnabled())
				{
					throw new OwaEventHandlerException("Failed to record greeting because user doesn't have Play On Phone Enabled", LocalizedStrings.GetNonEncoded(1385527863));
				}
				if (!string.IsNullOrEmpty(lastUMCallId))
				{
					UMCallState umcallState = UMCallState.Disconnected;
					try
					{
						UMCallInfo callInfo = umclientCommon.GetCallInfo(lastUMCallId);
						umcallState = callInfo.CallState;
					}
					catch (InvalidCallIdException)
					{
						ExTraceGlobals.UnifiedMessagingTracer.TraceDebug((long)this.GetHashCode(), "Failed to get call status from Unified Messaging");
					}
					catch (Exception exception)
					{
						if (!OptionsEventHandler.HandleUMException(exception))
						{
							throw;
						}
					}
					if (umcallState != UMCallState.Disconnected)
					{
						throw new OwaEventHandlerException(LocalizedStrings.GetNonEncoded(460647110), LocalizedStrings.GetNonEncoded(460647110));
					}
					base.UserContext.LastUMCallId = null;
				}
				UMGreetingType greetingType = flag ? UMGreetingType.NormalCustom : UMGreetingType.OofCustom;
				try
				{
					base.UserContext.LastUMCallId = umclientCommon.PlayOnPhoneGreeting(greetingType, dialString);
				}
				catch (Exception exception2)
				{
					if (!OptionsEventHandler.HandleUMException(exception2))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x06002ECB RID: 11979 RVA: 0x0010CFA4 File Offset: 0x0010B1A4
		[OwaEvent("ResUmPw")]
		[OwaEventSegmentation(Feature.UMIntegration)]
		public void ResetUnifiedMessagingPassword()
		{
			ExTraceGlobals.UserOptionsCallTracer.TraceDebug((long)this.GetHashCode(), "OptionsEventHandler.ResetUnifiedMessagingPassword");
			using (UMClientCommon umclientCommon = new UMClientCommon(base.UserContext.ExchangePrincipal))
			{
				if (!umclientCommon.IsUMEnabled())
				{
					throw new OwaEventHandlerException("Failed to reset pin because user is not enabled for Unified Messaging", LocalizedStrings.GetNonEncoded(1301599396));
				}
				try
				{
					umclientCommon.ResetPIN();
				}
				catch (ResetPINException innerException)
				{
					throw new OwaEventHandlerException("Unable to Reset PIN in Unified Messaging", LocalizedStrings.GetNonEncoded(51129530), innerException);
				}
			}
		}

		// Token: 0x06002ECC RID: 11980 RVA: 0x0010D03C File Offset: 0x0010B23C
		[OwaEventSegmentation(Feature.UMIntegration)]
		[OwaEvent("GetMailTree")]
		public void GetMailFolderTree()
		{
			ExTraceGlobals.UserOptionsCallTracer.TraceDebug((long)this.GetHashCode(), "OptionsEventHandler.GetMailFolderTree");
			FolderTree folderTree = MailboxFolderTree.CreateMailboxFolderTree(base.UserContext, base.UserContext.MailboxSession, FolderTreeRenderType.MailFolderWithoutSearchFolders, true);
			string text = "divFPErr";
			this.Writer.Write("<div id=\"divFPTrR\">");
			Infobar infobar = new Infobar(text, "infobar");
			infobar.Render(this.Writer);
			NavigationHost.RenderTreeRegionDivStart(this.Writer, null);
			NavigationHost.RenderTreeDivStart(this.Writer, "fptree");
			folderTree.ErrDiv = text;
			folderTree.Render(this.Writer);
			NavigationHost.RenderTreeDivEnd(this.Writer);
			NavigationHost.RenderTreeRegionDivEnd(this.Writer);
			this.Writer.Write("</div>");
		}

		// Token: 0x06002ECD RID: 11981 RVA: 0x0010D0FC File Offset: 0x0010B2FC
		[OwaEvent("DsblOof")]
		public void DisableOof()
		{
			ExTraceGlobals.UserOptionsCallTracer.TraceDebug((long)this.GetHashCode(), "OptionsEventHandler.DisableOof");
			UserOofSettings userOofSettings = UserOofSettings.GetUserOofSettings(base.UserContext.MailboxSession);
			userOofSettings.OofState = OofState.Disabled;
			userOofSettings.Save(base.UserContext.MailboxSession);
		}

		// Token: 0x06002ECE RID: 11982 RVA: 0x0010D148 File Offset: 0x0010B348
		[OwaEventParameter("pckCrt", typeof(bool), false, true)]
		[OwaEventParameter("snCrtSub", typeof(string), false, true)]
		[OwaEventParameter("snCrtId", typeof(string), false, true)]
		[OwaEvent("SaveSMimeOpts")]
		[OwaEventParameter("addSig", typeof(bool))]
		[OwaEventParameter("enCntnt", typeof(bool))]
		public void SaveSMimeOptions()
		{
			ExTraceGlobals.UserOptionsCallTracer.TraceDebug((long)this.GetHashCode(), "OptionsEventHandler.SaveSMimeOptions");
			base.UserContext.UserOptions.SmimeEncrypt = (bool)base.GetParameter("enCntnt");
			base.UserContext.UserOptions.SmimeSign = (bool)base.GetParameter("addSig");
			if (base.IsParameterSet("pckCrt"))
			{
				base.UserContext.UserOptions.ManuallyPickCertificate = (bool)base.GetParameter("pckCrt");
				if (base.IsParameterSet("snCrtSub") && base.IsParameterSet("snCrtId"))
				{
					base.UserContext.UserOptions.SigningCertificateSubject = (string)base.GetParameter("snCrtSub");
					base.UserContext.UserOptions.SigningCertificateId = (string)base.GetParameter("snCrtId");
				}
			}
			base.UserContext.UserOptions.CommitChanges();
		}

		// Token: 0x06002ECF RID: 11983 RVA: 0x0010D244 File Offset: 0x0010B444
		[OwaEventParameter("anrFst", typeof(bool))]
		[OwaEventParameter("optAcc", typeof(bool))]
		[OwaEvent("SaveGenOpts")]
		public void SaveGeneralSettingsOptions()
		{
			ExTraceGlobals.UserOptionsCallTracer.TraceDebug((long)this.GetHashCode(), "OptionsEventHandler.SaveGeneralSettingsOptions");
			if (base.UserContext.IsFeatureEnabled(Feature.Contacts))
			{
				base.UserContext.UserOptions.CheckNameInContactsFirst = (bool)base.GetParameter("anrFst");
			}
			base.UserContext.UserOptions.IsOptimizedForAccessibility = (bool)base.GetParameter("optAcc");
			base.UserContext.UserOptions.CommitChanges();
		}

		// Token: 0x06002ED0 RID: 11984 RVA: 0x0010D2C8 File Offset: 0x0010B4C8
		[OwaEventVerb(OwaEventVerb.Get)]
		[OwaEvent("GetOptsMnu")]
		public void GetOptionsMenu()
		{
			OptionsContextMenu optionsContextMenu = new OptionsContextMenu(base.UserContext);
			optionsContextMenu.Render(this.Writer);
		}

		// Token: 0x06002ED1 RID: 11985 RVA: 0x0010D2F0 File Offset: 0x0010B4F0
		[OwaEventParameter("thmId", typeof(string))]
		[OwaEvent("SaveThmOpts")]
		public void SaveThemesSettingsOptions()
		{
			ExTraceGlobals.UserOptionsCallTracer.TraceDebug((long)this.GetHashCode(), "OptionsEventHandler.SaveThemesSettingsOptions");
			if (base.UserContext.IsFeatureEnabled(Feature.Themes) && base.IsParameterSet("thmId"))
			{
				string text = (string)base.GetParameter("thmId");
				uint idFromStorageId = ThemeManager.GetIdFromStorageId(text);
				if (idFromStorageId == 4294967295U)
				{
					throw new OwaEventHandlerException("The theme doesn't exist any more in the server", LocalizedStrings.GetNonEncoded(-1332063254));
				}
				if (idFromStorageId == base.UserContext.DefaultTheme.Id)
				{
					base.UserContext.UserOptions.ThemeStorageId = string.Empty;
				}
				else
				{
					base.UserContext.UserOptions.ThemeStorageId = text;
				}
				base.UserContext.Theme = ThemeManager.Themes[(int)((UIntPtr)idFromStorageId)];
				base.UserContext.UserOptions.CommitChanges();
			}
		}

		// Token: 0x06002ED2 RID: 11986 RVA: 0x0010D3C8 File Offset: 0x0010B5C8
		[OwaEvent("ChangePassword")]
		[OwaEventSegmentation(Feature.ChangePassword)]
		[OwaEventParameter("oldPwd", typeof(string))]
		[OwaEventParameter("newPwd", typeof(string))]
		public void ChangePassword()
		{
			ExTraceGlobals.OehCallTracer.TraceDebug((long)this.GetHashCode(), "OptionsEventHandler.ChangePassword");
			using (SecureString secureString = Utilities.SecureStringFromString((string)base.GetParameter("oldPwd")))
			{
				using (SecureString secureString2 = Utilities.SecureStringFromString((string)base.GetParameter("newPwd")))
				{
					switch (Utilities.ChangePassword(base.OwaContext.LogonIdentity, secureString, secureString2))
					{
					case Utilities.ChangePasswordResult.InvalidCredentials:
						this.RenderErrorInfobar(LocalizedStrings.GetHtmlEncoded(866665304));
						break;
					case Utilities.ChangePasswordResult.LockedOut:
						this.RenderErrorInfobar(LocalizedStrings.GetHtmlEncoded(-1179631159));
						break;
					case Utilities.ChangePasswordResult.BadNewPassword:
						this.RenderErrorInfobar(LocalizedStrings.GetHtmlEncoded(-782268049));
						break;
					case Utilities.ChangePasswordResult.OtherError:
						this.RenderErrorInfobar(LocalizedStrings.GetHtmlEncoded(-1821890470));
						break;
					}
				}
			}
		}

		// Token: 0x06002ED3 RID: 11987 RVA: 0x0010D4C0 File Offset: 0x0010B6C0
		[OwaEventParameter("btnSt", typeof(int), false, true)]
		[OwaEvent("SaveFmtBrSt")]
		[OwaEventParameter("mruFnts", typeof(string), false, true)]
		public void SaveFormatBarState()
		{
			ExTraceGlobals.UserOptionsCallTracer.TraceDebug((long)this.GetHashCode(), "OptionsEventHandler.SaveFormatBarState");
			if (base.IsParameterSet("btnSt"))
			{
				base.UserContext.UserOptions.FormatBarState = (FormatBarButtonGroups)base.GetParameter("btnSt");
			}
			if (base.IsParameterSet("mruFnts"))
			{
				base.UserContext.UserOptions.MruFonts = (string)base.GetParameter("mruFnts");
			}
			base.UserContext.UserOptions.CommitChanges();
		}

		// Token: 0x06002ED4 RID: 11988 RVA: 0x0010D550 File Offset: 0x0010B750
		[OwaEvent("DisablePont")]
		[OwaEventParameter("pt", typeof(int))]
		public void DisablePont()
		{
			ExTraceGlobals.UserOptionsCallTracer.TraceDebug((long)this.GetHashCode(), "OptionsEventHandler.DisablePont");
			base.UserContext.UserOptions.EnabledPonts &= ~(PontType)base.GetParameter("pt");
			base.UserContext.UserOptions.CommitChanges();
		}

		// Token: 0x06002ED5 RID: 11989 RVA: 0x0010D5AC File Offset: 0x0010B7AC
		private static bool HandleUMException(Exception exception)
		{
			if (exception is InvalidObjectIdException)
			{
				throw new OwaEventHandlerException("Failed to play greeting", LocalizedStrings.GetNonEncoded(982875461));
			}
			if (exception is DialingRulesException)
			{
				throw new OwaEventHandlerException("Failed to play greeting", LocalizedStrings.GetNonEncoded(852800569));
			}
			if (exception is IPGatewayNotFoundException)
			{
				throw new OwaEventHandlerException("Failed to play greeting", LocalizedStrings.GetNonEncoded(-600244331));
			}
			if (exception is UMServerNotFoundException)
			{
				throw new OwaEventHandlerException("Failed to play greeting", LocalizedStrings.GetNonEncoded(-620095015));
			}
			if (exception is TransportException)
			{
				throw new OwaEventHandlerException("Failed to play greeting", LocalizedStrings.GetNonEncoded(-2078057245));
			}
			if (exception is InvalidPhoneNumberException)
			{
				throw new OwaEventHandlerException("Failed to play greeting", LocalizedStrings.GetNonEncoded(-74757282));
			}
			if (exception is InvalidSipUriException)
			{
				throw new OwaEventHandlerException("Failed to play greeting", LocalizedStrings.GetNonEncoded(-74757282));
			}
			return false;
		}

		// Token: 0x06002ED6 RID: 11990 RVA: 0x0010D685 File Offset: 0x0010B885
		private void RenderErrorInfobar(string messageHtml)
		{
			this.Writer.Write("<div id=eib>");
			this.Writer.Write(messageHtml);
			this.Writer.Write("</div>");
		}

		// Token: 0x0400208C RID: 8332
		public const string EventNamespace = "Options";

		// Token: 0x0400208D RID: 8333
		public const string MethodSaveUnifiedMessagingOptions = "SaveUmOpts";

		// Token: 0x0400208E RID: 8334
		public const string MethodGetUnifiedMessagingOptions = "IsSmsConfigured";

		// Token: 0x0400208F RID: 8335
		public const string MethodRecordUnifiedMessagingGreeting = "RecUmGr";

		// Token: 0x04002090 RID: 8336
		public const string MethodResetUnifiedMessagingPassword = "ResUmPw";

		// Token: 0x04002091 RID: 8337
		public const string MethodGetMailFolderTree = "GetMailTree";

		// Token: 0x04002092 RID: 8338
		public const string MethodDisableOof = "DsblOof";

		// Token: 0x04002093 RID: 8339
		public const string MethodSaveSMimeOptions = "SaveSMimeOpts";

		// Token: 0x04002094 RID: 8340
		public const string MethodSaveGeneralSettingsOptions = "SaveGenOpts";

		// Token: 0x04002095 RID: 8341
		public const string MethodSaveFormatBarState = "SaveFmtBrSt";

		// Token: 0x04002096 RID: 8342
		public const string MethodGetOptionsMenu = "GetOptsMnu";

		// Token: 0x04002097 RID: 8343
		public const string MethodSaveThemesSettingsOptions = "SaveThmOpts";

		// Token: 0x04002098 RID: 8344
		public const string MethodDisablePont = "DisablePont";

		// Token: 0x04002099 RID: 8345
		public const string MethodChangePassword = "ChangePassword";

		// Token: 0x0400209A RID: 8346
		public const string MethodSendMeMailboxLog = "SdLg";

		// Token: 0x0400209B RID: 8347
		public const string OldPassword = "oldPwd";

		// Token: 0x0400209C RID: 8348
		public const string NewPassword = "newPwd";

		// Token: 0x0400209D RID: 8349
		public const string DialString = "tpNum";

		// Token: 0x0400209E RID: 8350
		public const string UmOofStatus = "vGr";

		// Token: 0x0400209F RID: 8351
		public const string UmUnreadVoiceMailSortOrder = "vSo";

		// Token: 0x040020A0 RID: 8352
		public const string MissedCall = "mc";

		// Token: 0x040020A1 RID: 8353
		public const string PinlessVoicemail = "plvm";

		// Token: 0x040020A2 RID: 8354
		public const string VoicemailPreviewReceive = "vmPvRec";

		// Token: 0x040020A3 RID: 8355
		public const string VoicemailPreviewSend = "vmPvSnd";

		// Token: 0x040020A4 RID: 8356
		public const string SMSNotificationOption = "sms";

		// Token: 0x040020A5 RID: 8357
		public const string TelephoneAccessFolder = "tpFdr";

		// Token: 0x040020A6 RID: 8358
		public const string EncryptContent = "enCntnt";

		// Token: 0x040020A7 RID: 8359
		public const string AddSignature = "addSig";

		// Token: 0x040020A8 RID: 8360
		public const string ManuallyPickCertificate = "pckCrt";

		// Token: 0x040020A9 RID: 8361
		public const string SigningCertificateSubject = "snCrtSub";

		// Token: 0x040020AA RID: 8362
		public const string SigningCertificateId = "snCrtId";

		// Token: 0x040020AB RID: 8363
		public const string EmptyDeletedItems = "emDel";

		// Token: 0x040020AC RID: 8364
		public const string AnrFirst = "anrFst";

		// Token: 0x040020AD RID: 8365
		public const string OptimizeForAccessibility = "optAcc";

		// Token: 0x040020AE RID: 8366
		public const string ThemeStorageId = "thmId";

		// Token: 0x040020AF RID: 8367
		public const string FormatBarState = "btnSt";

		// Token: 0x040020B0 RID: 8368
		public const string MruFonts = "mruFnts";

		// Token: 0x040020B1 RID: 8369
		public const string PontType = "pt";
	}
}
