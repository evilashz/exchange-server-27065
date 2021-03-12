using System;
using System.Globalization;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000144 RID: 324
	internal class GalGrammarFile : SearchGrammarFile
	{
		// Token: 0x06000900 RID: 2304 RVA: 0x00026E73 File Offset: 0x00025073
		internal GalGrammarFile(CultureInfo culture, string filePath) : base(culture, true)
		{
			this.galGrammarFilePath = filePath;
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x00026E84 File Offset: 0x00025084
		public override Uri BaseUri
		{
			get
			{
				return new Uri(Utils.GrammarPathFromCulture(base.Culture));
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x00026E96 File Offset: 0x00025096
		internal override string FilePath
		{
			get
			{
				return this.galGrammarFilePath;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x00026E9E File Offset: 0x0002509E
		internal override bool HasEntries
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00026EA4 File Offset: 0x000250A4
		internal static GrammarIdentifier GetGrammarIdentifier(CallContext callContext)
		{
			GrammarIdentifier grammarIdentifier = null;
			switch (callContext.CallType)
			{
			case 1:
			{
				UMDialPlan dialPlan = callContext.DialPlan;
				switch (dialPlan.ContactScope)
				{
				case CallSomeoneScopeEnum.DialPlan:
					grammarIdentifier = new GrammarIdentifier(dialPlan.OrganizationId, callContext.Culture, GrammarFileNames.GetFileNameForDialPlan(dialPlan));
					break;
				case CallSomeoneScopeEnum.GlobalAddressList:
					grammarIdentifier = new GrammarIdentifier(dialPlan.OrganizationId, callContext.Culture, GrammarFileNames.GetFileNameForGALUser());
					break;
				case CallSomeoneScopeEnum.AddressList:
					if (dialPlan.ContactAddressList != null)
					{
						grammarIdentifier = new GrammarIdentifier(dialPlan.OrganizationId, callContext.Culture, GrammarFileNames.GetFileNameForCustomAddressList(dialPlan.ContactAddressList));
					}
					else
					{
						grammarIdentifier = new GrammarIdentifier(dialPlan.OrganizationId, callContext.Culture, GrammarFileNames.GetFileNameForGALUser());
					}
					break;
				}
				break;
			}
			case 2:
			{
				UMAutoAttendant autoAttendantInfo = callContext.AutoAttendantInfo;
				switch (autoAttendantInfo.ContactScope)
				{
				case DialScopeEnum.DialPlan:
					grammarIdentifier = new GrammarIdentifier(autoAttendantInfo.OrganizationId, callContext.Culture, GrammarFileNames.GetFileNameForDialPlan(callContext.DialPlan));
					break;
				case DialScopeEnum.GlobalAddressList:
					grammarIdentifier = new GrammarIdentifier(autoAttendantInfo.OrganizationId, callContext.Culture, GrammarFileNames.GetFileNameForGALUser());
					break;
				case DialScopeEnum.AddressList:
					if (autoAttendantInfo.ContactAddressList != null)
					{
						grammarIdentifier = new GrammarIdentifier(autoAttendantInfo.OrganizationId, callContext.Culture, GrammarFileNames.GetFileNameForCustomAddressList(autoAttendantInfo.ContactAddressList));
					}
					else
					{
						grammarIdentifier = new GrammarIdentifier(autoAttendantInfo.OrganizationId, callContext.Culture, GrammarFileNames.GetFileNameForGALUser());
					}
					break;
				}
				break;
			}
			case 3:
			{
				UMSubscriber callerInfo = callContext.CallerInfo;
				ADRecipient adrecipient = callerInfo.ADRecipient;
				ExAssert.RetailAssert(adrecipient != null, "subscriber.ADRecipient = null");
				ExAssert.RetailAssert(adrecipient.OrganizationId != null, "subscriber.ADRecipient.OrganizationId = null");
				if (adrecipient != null && adrecipient.AddressBookPolicy != null && adrecipient.GlobalAddressListFromAddressBookPolicy != null)
				{
					grammarIdentifier = new GrammarIdentifier(adrecipient.OrganizationId, callContext.Culture, GrammarFileNames.GetFileNameForCustomAddressList(adrecipient.GlobalAddressListFromAddressBookPolicy));
				}
				else
				{
					grammarIdentifier = new GrammarIdentifier(adrecipient.OrganizationId, callContext.Culture, GrammarFileNames.GetFileNameForGALUser());
				}
				break;
			}
			default:
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Unhandled CallType {0}", new object[]
				{
					callContext.CallType
				}));
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, null, "GalGrammarFile::GetGrammarIdentifier() - Grammar = '{0}'", new object[]
			{
				grammarIdentifier
			});
			return grammarIdentifier;
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00027100 File Offset: 0x00025300
		internal static void LogErrorEvent(CallContext callContext)
		{
			if (callContext.CallType == 3)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_ContactsNoGrammarFileWarning, null, new object[0]);
				return;
			}
			string name = callContext.AutoAttendantInfo.Name;
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_AutoAttendantNoGrammarFileWarning, null, new object[]
			{
				name
			});
		}

		// Token: 0x040008C6 RID: 2246
		private readonly string galGrammarFilePath;
	}
}
