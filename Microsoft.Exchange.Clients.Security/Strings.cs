using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x02000003 RID: 3
	public static class Strings
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002218 File Offset: 0x00000418
		static Strings()
		{
			Strings.stringIDs.Add(3266846781U, "AddToFavorites");
			Strings.stringIDs.Add(1297161044U, "ComponentNotInitialized");
			Strings.stringIDs.Add(2723658401U, "LiveIdLabel");
			Strings.stringIDs.Add(2216610333U, "LogonErrorLogoutUrlText");
			Strings.stringIDs.Add(3228633421U, "OutlookWebAccess");
			Strings.stringIDs.Add(641346049U, "ErrorUnexpectedFailure");
			Strings.stringIDs.Add(3147877247U, "GoThereNowButtonText");
			Strings.stringIDs.Add(2956867297U, "ComponentAlreadyInitialized");
			Strings.stringIDs.Add(4265046865U, "EducationMessage");
			Strings.stringIDs.Add(675314258U, "SignUpSuggestion");
			Strings.stringIDs.Add(3937131501U, "WhyMessage");
			Strings.stringIDs.Add(2886870364U, "GetLiveIdMessage");
			Strings.stringIDs.Add(933672694U, "ErrorTitle");
			Strings.stringIDs.Add(4082986936U, "NextButtonText");
			Strings.stringIDs.Add(579076706U, "InvalidLiveIdWarning");
			Strings.stringIDs.Add(3976540663U, "ConnectedToExchange");
			Strings.stringIDs.Add(2308016970U, "LogonCopyright");
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000023A8 File Offset: 0x000005A8
		public static LocalizedString AddToFavorites
		{
			get
			{
				return new LocalizedString("AddToFavorites", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000023C8 File Offset: 0x000005C8
		public static LocalizedString LiveExternalFCodeException(int fCode, string msppErrorString)
		{
			return new LocalizedString("LiveExternalFCodeException", "", false, false, Strings.ResourceManager, new object[]
			{
				fCode,
				msppErrorString
			});
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002400 File Offset: 0x00000600
		public static LocalizedString ComponentNotInitialized
		{
			get
			{
				return new LocalizedString("ComponentNotInitialized", "ExE5CD58", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002420 File Offset: 0x00000620
		public static LocalizedString LiveConfigurationHRESULTExceptionMessage(uint error, string textError)
		{
			return new LocalizedString("LiveConfigurationHRESULTExceptionMessage", "Ex844F7A", false, true, Strings.ResourceManager, new object[]
			{
				error,
				textError
			});
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002458 File Offset: 0x00000658
		public static LocalizedString LiveIdLabel
		{
			get
			{
				return new LocalizedString("LiveIdLabel", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002476 File Offset: 0x00000676
		public static LocalizedString LogonErrorLogoutUrlText
		{
			get
			{
				return new LocalizedString("LogonErrorLogoutUrlText", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002494 File Offset: 0x00000694
		public static LocalizedString LiveClientHRESULTExceptionMessage(uint error, string textError)
		{
			return new LocalizedString("LiveClientHRESULTExceptionMessage", "Ex21A033", false, true, Strings.ResourceManager, new object[]
			{
				error,
				textError
			});
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000024CC File Offset: 0x000006CC
		public static LocalizedString OutlookWebAccess
		{
			get
			{
				return new LocalizedString("OutlookWebAccess", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000024EA File Offset: 0x000006EA
		public static LocalizedString ErrorUnexpectedFailure
		{
			get
			{
				return new LocalizedString("ErrorUnexpectedFailure", "ExC6DD63", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002508 File Offset: 0x00000708
		public static LocalizedString LiveClientFCodeException(int fCode, string msppErrorString)
		{
			return new LocalizedString("LiveClientFCodeException", "", false, false, Strings.ResourceManager, new object[]
			{
				fCode,
				msppErrorString
			});
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002540 File Offset: 0x00000740
		public static LocalizedString LiveExternalUnknownFCodeException(string fCodeString, string msppErrorString)
		{
			return new LocalizedString("LiveExternalUnknownFCodeException", "", false, false, Strings.ResourceManager, new object[]
			{
				fCodeString,
				msppErrorString
			});
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002574 File Offset: 0x00000774
		public static LocalizedString LiveOperationExceptionMessage(uint error, string textError)
		{
			return new LocalizedString("LiveOperationExceptionMessage", "Ex7CFB70", false, true, Strings.ResourceManager, new object[]
			{
				error,
				textError
			});
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000025AC File Offset: 0x000007AC
		public static LocalizedString LiveTransientHRESULTExceptionMessage(uint error, string textError)
		{
			return new LocalizedString("LiveTransientHRESULTExceptionMessage", "Ex6AC7BF", false, true, Strings.ResourceManager, new object[]
			{
				error,
				textError
			});
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000025E4 File Offset: 0x000007E4
		public static LocalizedString GoThereNowButtonText
		{
			get
			{
				return new LocalizedString("GoThereNowButtonText", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002602 File Offset: 0x00000802
		public static LocalizedString ComponentAlreadyInitialized
		{
			get
			{
				return new LocalizedString("ComponentAlreadyInitialized", "Ex5F7B71", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002620 File Offset: 0x00000820
		public static LocalizedString EducationMessage
		{
			get
			{
				return new LocalizedString("EducationMessage", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002640 File Offset: 0x00000840
		public static LocalizedString LiveTransientFCodeException(int fCode, string msppErrorString)
		{
			return new LocalizedString("LiveTransientFCodeException", "", false, false, Strings.ResourceManager, new object[]
			{
				fCode,
				msppErrorString
			});
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002678 File Offset: 0x00000878
		public static LocalizedString SignUpSuggestion
		{
			get
			{
				return new LocalizedString("SignUpSuggestion", "Ex51B22A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002696 File Offset: 0x00000896
		public static LocalizedString WhyMessage
		{
			get
			{
				return new LocalizedString("WhyMessage", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000026B4 File Offset: 0x000008B4
		public static LocalizedString GetLiveIdMessage
		{
			get
			{
				return new LocalizedString("GetLiveIdMessage", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000026D4 File Offset: 0x000008D4
		public static LocalizedString LiveConfigurationFCodeException(int fCode, string msppErrorString)
		{
			return new LocalizedString("LiveConfigurationFCodeException", "", false, false, Strings.ResourceManager, new object[]
			{
				fCode,
				msppErrorString
			});
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000270C File Offset: 0x0000090C
		public static LocalizedString ErrorTitle
		{
			get
			{
				return new LocalizedString("ErrorTitle", "ExA60E2A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000272A File Offset: 0x0000092A
		public static LocalizedString NextButtonText
		{
			get
			{
				return new LocalizedString("NextButtonText", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002748 File Offset: 0x00000948
		public static LocalizedString InvalidLiveIdWarning
		{
			get
			{
				return new LocalizedString("InvalidLiveIdWarning", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002768 File Offset: 0x00000968
		public static LocalizedString LiveExternalHRESULTExceptionMessage(uint error, string textError)
		{
			return new LocalizedString("LiveExternalHRESULTExceptionMessage", "Ex6DE241", false, true, Strings.ResourceManager, new object[]
			{
				error,
				textError
			});
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000027A0 File Offset: 0x000009A0
		public static LocalizedString ConnectedToExchange
		{
			get
			{
				return new LocalizedString("ConnectedToExchange", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000027BE File Offset: 0x000009BE
		public static LocalizedString LogonCopyright
		{
			get
			{
				return new LocalizedString("LogonCopyright", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000027DC File Offset: 0x000009DC
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000007 RID: 7
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(17);

		// Token: 0x04000008 RID: 8
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Clients.Security.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000004 RID: 4
		public enum IDs : uint
		{
			// Token: 0x0400000A RID: 10
			AddToFavorites = 3266846781U,
			// Token: 0x0400000B RID: 11
			ComponentNotInitialized = 1297161044U,
			// Token: 0x0400000C RID: 12
			LiveIdLabel = 2723658401U,
			// Token: 0x0400000D RID: 13
			LogonErrorLogoutUrlText = 2216610333U,
			// Token: 0x0400000E RID: 14
			OutlookWebAccess = 3228633421U,
			// Token: 0x0400000F RID: 15
			ErrorUnexpectedFailure = 641346049U,
			// Token: 0x04000010 RID: 16
			GoThereNowButtonText = 3147877247U,
			// Token: 0x04000011 RID: 17
			ComponentAlreadyInitialized = 2956867297U,
			// Token: 0x04000012 RID: 18
			EducationMessage = 4265046865U,
			// Token: 0x04000013 RID: 19
			SignUpSuggestion = 675314258U,
			// Token: 0x04000014 RID: 20
			WhyMessage = 3937131501U,
			// Token: 0x04000015 RID: 21
			GetLiveIdMessage = 2886870364U,
			// Token: 0x04000016 RID: 22
			ErrorTitle = 933672694U,
			// Token: 0x04000017 RID: 23
			NextButtonText = 4082986936U,
			// Token: 0x04000018 RID: 24
			InvalidLiveIdWarning = 579076706U,
			// Token: 0x04000019 RID: 25
			ConnectedToExchange = 3976540663U,
			// Token: 0x0400001A RID: 26
			LogonCopyright = 2308016970U
		}

		// Token: 0x02000005 RID: 5
		private enum ParamIDs
		{
			// Token: 0x0400001C RID: 28
			LiveExternalFCodeException,
			// Token: 0x0400001D RID: 29
			LiveConfigurationHRESULTExceptionMessage,
			// Token: 0x0400001E RID: 30
			LiveClientHRESULTExceptionMessage,
			// Token: 0x0400001F RID: 31
			LiveClientFCodeException,
			// Token: 0x04000020 RID: 32
			LiveExternalUnknownFCodeException,
			// Token: 0x04000021 RID: 33
			LiveOperationExceptionMessage,
			// Token: 0x04000022 RID: 34
			LiveTransientHRESULTExceptionMessage,
			// Token: 0x04000023 RID: 35
			LiveTransientFCodeException,
			// Token: 0x04000024 RID: 36
			LiveConfigurationFCodeException,
			// Token: 0x04000025 RID: 37
			LiveExternalHRESULTExceptionMessage
		}
	}
}
