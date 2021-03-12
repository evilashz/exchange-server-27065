using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.TextMessaging.MobileDriver.Resources
{
	// Token: 0x02000051 RID: 81
	internal static class Strings
	{
		// Token: 0x06000221 RID: 545 RVA: 0x0000C444 File Offset: 0x0000A644
		static Strings()
		{
			Strings.stringIDs.Add(684812396U, "ConstNull");
			Strings.stringIDs.Add(2765603176U, "ErrorEmailMessageNotFound");
			Strings.stringIDs.Add(268030502U, "ErrorNotSupportMultimediaMessage");
			Strings.stringIDs.Add(298068888U, "ErrorEmptyCalNotifContent");
			Strings.stringIDs.Add(893814531U, "ErrorTooManyParts");
			Strings.stringIDs.Add(3337871027U, "ErrorAvaliableServiceNotFound");
			Strings.stringIDs.Add(406232425U, "ErrorEmailNotificationDeadLoop");
			Strings.stringIDs.Add(597584490U, "ErrorNeutralCodingScheme");
			Strings.stringIDs.Add(1045457712U, "ConstNa");
			Strings.stringIDs.Add(3260461220U, "ErrorInvalidPhoneNumber");
			Strings.stringIDs.Add(234345771U, "ErrorTooManyRecipients");
			Strings.stringIDs.Add(4114712759U, "calNotifAllDayEventsDesc");
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000C570 File Offset: 0x0000A770
		public static LocalizedString ConstNull
		{
			get
			{
				return new LocalizedString("ConstNull", "Ex4302BE", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000C590 File Offset: 0x0000A790
		public static LocalizedString ErrorNotAcknowledged(string number, string ecpLink)
		{
			return new LocalizedString("ErrorNotAcknowledged", "Ex0B9AAF", false, true, Strings.ResourceManager, new object[]
			{
				number,
				ecpLink
			});
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000C5C4 File Offset: 0x0000A7C4
		public static LocalizedString ErrorNoP2pDeliveryPoint(string ecpLink)
		{
			return new LocalizedString("ErrorNoP2pDeliveryPoint", "ExDBC203", false, true, Strings.ResourceManager, new object[]
			{
				ecpLink
			});
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000C5F3 File Offset: 0x0000A7F3
		public static LocalizedString ErrorEmailMessageNotFound
		{
			get
			{
				return new LocalizedString("ErrorEmailMessageNotFound", "ExCA033E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000C614 File Offset: 0x0000A814
		public static LocalizedString notifCountOfEventsDesc(string number)
		{
			return new LocalizedString("notifCountOfEventsDesc", "Ex2AE6E1", false, true, Strings.ResourceManager, new object[]
			{
				number
			});
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000C644 File Offset: 0x0000A844
		public static LocalizedString ErrorUnableDeliverForEas(string number, string error)
		{
			return new LocalizedString("ErrorUnableDeliverForEas", "Ex6744C1", false, true, Strings.ResourceManager, new object[]
			{
				number,
				error
			});
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000C677 File Offset: 0x0000A877
		public static LocalizedString ErrorNotSupportMultimediaMessage
		{
			get
			{
				return new LocalizedString("ErrorNotSupportMultimediaMessage", "Ex3CE24B", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000C695 File Offset: 0x0000A895
		public static LocalizedString ErrorEmptyCalNotifContent
		{
			get
			{
				return new LocalizedString("ErrorEmptyCalNotifContent", "Ex7E26A4", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000C6B4 File Offset: 0x0000A8B4
		public static LocalizedString ErrorNoM2pDeliveryPointForEmailAlert(string ecpLink)
		{
			return new LocalizedString("ErrorNoM2pDeliveryPointForEmailAlert", "Ex35983F", false, true, Strings.ResourceManager, new object[]
			{
				ecpLink
			});
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000C6E4 File Offset: 0x0000A8E4
		public static LocalizedString ErrorNoProviderForTextMessage(string textMessagingSlabLink)
		{
			return new LocalizedString("ErrorNoProviderForTextMessage", "ExD7F292", false, true, Strings.ResourceManager, new object[]
			{
				textMessagingSlabLink
			});
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000C713 File Offset: 0x0000A913
		public static LocalizedString ErrorTooManyParts
		{
			get
			{
				return new LocalizedString("ErrorTooManyParts", "Ex4324C5", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000C734 File Offset: 0x0000A934
		public static LocalizedString ErrorObjectNotFound(string identity)
		{
			return new LocalizedString("ErrorObjectNotFound", "ExBD415B", false, true, Strings.ResourceManager, new object[]
			{
				identity
			});
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000C764 File Offset: 0x0000A964
		public static LocalizedString ErrorCannotParseSettings(string error)
		{
			return new LocalizedString("ErrorCannotParseSettings", "Ex362A58", false, true, Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000C793 File Offset: 0x0000A993
		public static LocalizedString ErrorAvaliableServiceNotFound
		{
			get
			{
				return new LocalizedString("ErrorAvaliableServiceNotFound", "Ex812156", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000C7B4 File Offset: 0x0000A9B4
		public static LocalizedString ErrorCantBeCoded(string codingScheme, string text)
		{
			return new LocalizedString("ErrorCantBeCoded", "Ex0EE29C", false, true, Strings.ResourceManager, new object[]
			{
				codingScheme,
				text
			});
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000C7E7 File Offset: 0x0000A9E7
		public static LocalizedString ErrorEmailNotificationDeadLoop
		{
			get
			{
				return new LocalizedString("ErrorEmailNotificationDeadLoop", "ExC71CAD", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000C805 File Offset: 0x0000AA05
		public static LocalizedString ErrorNeutralCodingScheme
		{
			get
			{
				return new LocalizedString("ErrorNeutralCodingScheme", "ExCC2AFE", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000C824 File Offset: 0x0000AA24
		public static LocalizedString ErrorNoProviderForNotification(string textMessagingSlabLink, string notificationSetupWizardLink)
		{
			return new LocalizedString("ErrorNoProviderForNotification", "ExE077BF", false, true, Strings.ResourceManager, new object[]
			{
				textMessagingSlabLink,
				notificationSetupWizardLink
			});
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000C858 File Offset: 0x0000AA58
		public static LocalizedString ErrorInvalidCalNotifContent(string content, string error)
		{
			return new LocalizedString("ErrorInvalidCalNotifContent", "Ex581BCF", false, true, Strings.ResourceManager, new object[]
			{
				content,
				error
			});
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000C88C File Offset: 0x0000AA8C
		public static LocalizedString ErrorUnableDeliverForSmtpToSmsGateway(string recipient)
		{
			return new LocalizedString("ErrorUnableDeliverForSmtpToSmsGateway", "Ex4FB268", false, true, Strings.ResourceManager, new object[]
			{
				recipient
			});
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000C8BC File Offset: 0x0000AABC
		public static LocalizedString ErrorServiceUnsupported(string type)
		{
			return new LocalizedString("ErrorServiceUnsupported", "Ex412370", false, true, Strings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000C8EC File Offset: 0x0000AAEC
		public static LocalizedString ErrorNotSupportM2pWhenEasEnabled(string ecpLink)
		{
			return new LocalizedString("ErrorNotSupportM2pWhenEasEnabled", "Ex07D127", false, true, Strings.ResourceManager, new object[]
			{
				ecpLink
			});
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000C91C File Offset: 0x0000AB1C
		public static LocalizedString ErrorInvalidMobileSessionMode(string method, string mode)
		{
			return new LocalizedString("ErrorInvalidMobileSessionMode", "Ex96906E", false, true, Strings.ResourceManager, new object[]
			{
				method,
				mode
			});
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000C950 File Offset: 0x0000AB50
		public static LocalizedString ErrorUnknownCalNotifType(string type)
		{
			return new LocalizedString("ErrorUnknownCalNotifType", "Ex945030", false, true, Strings.ResourceManager, new object[]
			{
				type
			});
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000C97F File Offset: 0x0000AB7F
		public static LocalizedString ConstNa
		{
			get
			{
				return new LocalizedString("ConstNa", "ExBF1BBB", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000C9A0 File Offset: 0x0000ABA0
		public static LocalizedString ErrorInvalidState(string name, string value)
		{
			return new LocalizedString("ErrorInvalidState", "Ex8D2802", false, true, Strings.ResourceManager, new object[]
			{
				name,
				value
			});
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000C9D3 File Offset: 0x0000ABD3
		public static LocalizedString ErrorInvalidPhoneNumber
		{
			get
			{
				return new LocalizedString("ErrorInvalidPhoneNumber", "Ex2A46FF", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000C9F1 File Offset: 0x0000ABF1
		public static LocalizedString ErrorTooManyRecipients
		{
			get
			{
				return new LocalizedString("ErrorTooManyRecipients", "ExDA4CEE", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000CA0F File Offset: 0x0000AC0F
		public static LocalizedString calNotifAllDayEventsDesc
		{
			get
			{
				return new LocalizedString("calNotifAllDayEventsDesc", "Ex9576F3", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000CA30 File Offset: 0x0000AC30
		public static LocalizedString ErrorNoM2pDeliveryPoint(string ecpLink)
		{
			return new LocalizedString("ErrorNoM2pDeliveryPoint", "Ex0DBFE1", false, true, Strings.ResourceManager, new object[]
			{
				ecpLink
			});
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000CA5F File Offset: 0x0000AC5F
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000121 RID: 289
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(12);

		// Token: 0x04000122 RID: 290
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.TextMessaging.MobileDriver.Resources.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000052 RID: 82
		public enum IDs : uint
		{
			// Token: 0x04000124 RID: 292
			ConstNull = 684812396U,
			// Token: 0x04000125 RID: 293
			ErrorEmailMessageNotFound = 2765603176U,
			// Token: 0x04000126 RID: 294
			ErrorNotSupportMultimediaMessage = 268030502U,
			// Token: 0x04000127 RID: 295
			ErrorEmptyCalNotifContent = 298068888U,
			// Token: 0x04000128 RID: 296
			ErrorTooManyParts = 893814531U,
			// Token: 0x04000129 RID: 297
			ErrorAvaliableServiceNotFound = 3337871027U,
			// Token: 0x0400012A RID: 298
			ErrorEmailNotificationDeadLoop = 406232425U,
			// Token: 0x0400012B RID: 299
			ErrorNeutralCodingScheme = 597584490U,
			// Token: 0x0400012C RID: 300
			ConstNa = 1045457712U,
			// Token: 0x0400012D RID: 301
			ErrorInvalidPhoneNumber = 3260461220U,
			// Token: 0x0400012E RID: 302
			ErrorTooManyRecipients = 234345771U,
			// Token: 0x0400012F RID: 303
			calNotifAllDayEventsDesc = 4114712759U
		}

		// Token: 0x02000053 RID: 83
		private enum ParamIDs
		{
			// Token: 0x04000131 RID: 305
			ErrorNotAcknowledged,
			// Token: 0x04000132 RID: 306
			ErrorNoP2pDeliveryPoint,
			// Token: 0x04000133 RID: 307
			notifCountOfEventsDesc,
			// Token: 0x04000134 RID: 308
			ErrorUnableDeliverForEas,
			// Token: 0x04000135 RID: 309
			ErrorNoM2pDeliveryPointForEmailAlert,
			// Token: 0x04000136 RID: 310
			ErrorNoProviderForTextMessage,
			// Token: 0x04000137 RID: 311
			ErrorObjectNotFound,
			// Token: 0x04000138 RID: 312
			ErrorCannotParseSettings,
			// Token: 0x04000139 RID: 313
			ErrorCantBeCoded,
			// Token: 0x0400013A RID: 314
			ErrorNoProviderForNotification,
			// Token: 0x0400013B RID: 315
			ErrorInvalidCalNotifContent,
			// Token: 0x0400013C RID: 316
			ErrorUnableDeliverForSmtpToSmsGateway,
			// Token: 0x0400013D RID: 317
			ErrorServiceUnsupported,
			// Token: 0x0400013E RID: 318
			ErrorNotSupportM2pWhenEasEnabled,
			// Token: 0x0400013F RID: 319
			ErrorInvalidMobileSessionMode,
			// Token: 0x04000140 RID: 320
			ErrorUnknownCalNotifType,
			// Token: 0x04000141 RID: 321
			ErrorInvalidState,
			// Token: 0x04000142 RID: 322
			ErrorNoM2pDeliveryPoint
		}
	}
}
