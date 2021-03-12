using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x02000048 RID: 72
	internal static class Strings
	{
		// Token: 0x060001CC RID: 460 RVA: 0x00005EC4 File Offset: 0x000040C4
		static Strings()
		{
			Strings.stringIDs.Add(3962995036U, "InvalidSerializedToken");
			Strings.stringIDs.Add(2766800417U, "InvalidListOfAppIds");
			Strings.stringIDs.Add(3216875091U, "InvalidRecipientsList");
			Strings.stringIDs.Add(4087543273U, "InvalidMnPayloadContent");
			Strings.stringIDs.Add(333453583U, "InvalidNotificationIdentifier");
			Strings.stringIDs.Add(816696259U, "InvalidPayload");
			Strings.stringIDs.Add(757184198U, "InvalidPlatform");
			Strings.stringIDs.Add(2677654617U, "InvalidAppId");
			Strings.stringIDs.Add(162180343U, "InvalidRecipientDeviceId");
			Strings.stringIDs.Add(2790684056U, "InvalidRecipient");
			Strings.stringIDs.Add(1125823870U, "InvalidRecipientAppId");
			Strings.stringIDs.Add(1327410019U, "InvalidWorkloadId");
			Strings.stringIDs.Add(3937582550U, "OutlookInvalidPayloadData");
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00006004 File Offset: 0x00004204
		public static LocalizedString InvalidSerializedToken
		{
			get
			{
				return new LocalizedString("InvalidSerializedToken", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000601B File Offset: 0x0000421B
		public static LocalizedString InvalidListOfAppIds
		{
			get
			{
				return new LocalizedString("InvalidListOfAppIds", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00006034 File Offset: 0x00004234
		public static LocalizedString InvalidTenantId(string tenantId)
		{
			return new LocalizedString("InvalidTenantId", Strings.ResourceManager, new object[]
			{
				tenantId
			});
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0000605C File Offset: 0x0000425C
		public static LocalizedString InvalidRecipientsList
		{
			get
			{
				return new LocalizedString("InvalidRecipientsList", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00006074 File Offset: 0x00004274
		public static LocalizedString PushNotificationServerExceptionMessage(string messageBody)
		{
			return new LocalizedString("PushNotificationServerExceptionMessage", Strings.ResourceManager, new object[]
			{
				messageBody
			});
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000609C File Offset: 0x0000429C
		public static LocalizedString ExceptionMessageTimeoutCall(string target, string message)
		{
			return new LocalizedString("ExceptionMessageTimeoutCall", Strings.ResourceManager, new object[]
			{
				target,
				message
			});
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x000060C8 File Offset: 0x000042C8
		public static LocalizedString InvalidMnPayloadContent
		{
			get
			{
				return new LocalizedString("InvalidMnPayloadContent", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x000060DF File Offset: 0x000042DF
		public static LocalizedString InvalidNotificationIdentifier
		{
			get
			{
				return new LocalizedString("InvalidNotificationIdentifier", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x000060F6 File Offset: 0x000042F6
		public static LocalizedString InvalidPayload
		{
			get
			{
				return new LocalizedString("InvalidPayload", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x0000610D File Offset: 0x0000430D
		public static LocalizedString InvalidPlatform
		{
			get
			{
				return new LocalizedString("InvalidPlatform", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00006124 File Offset: 0x00004324
		public static LocalizedString InvalidAppId
		{
			get
			{
				return new LocalizedString("InvalidAppId", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000613C File Offset: 0x0000433C
		public static LocalizedString ExceptionPushNotificationError(string server, string error)
		{
			return new LocalizedString("ExceptionPushNotificationError", Strings.ResourceManager, new object[]
			{
				server,
				error
			});
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00006168 File Offset: 0x00004368
		public static LocalizedString InvalidMnRecipientLastSubscriptionUpdate(string date)
		{
			return new LocalizedString("InvalidMnRecipientLastSubscriptionUpdate", Strings.ResourceManager, new object[]
			{
				date
			});
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00006190 File Offset: 0x00004390
		public static LocalizedString InvalidRecipientDeviceId
		{
			get
			{
				return new LocalizedString("InvalidRecipientDeviceId", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001DB RID: 475 RVA: 0x000061A7 File Offset: 0x000043A7
		public static LocalizedString InvalidRecipient
		{
			get
			{
				return new LocalizedString("InvalidRecipient", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001DC RID: 476 RVA: 0x000061BE File Offset: 0x000043BE
		public static LocalizedString InvalidRecipientAppId
		{
			get
			{
				return new LocalizedString("InvalidRecipientAppId", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001DD RID: 477 RVA: 0x000061D5 File Offset: 0x000043D5
		public static LocalizedString InvalidWorkloadId
		{
			get
			{
				return new LocalizedString("InvalidWorkloadId", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000061EC File Offset: 0x000043EC
		public static LocalizedString ExceptionEndpointNotFoundError(string server, string error)
		{
			return new LocalizedString("ExceptionEndpointNotFoundError", Strings.ResourceManager, new object[]
			{
				server,
				error
			});
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00006218 File Offset: 0x00004418
		public static LocalizedString InvalidTargetAppId(string notificationType)
		{
			return new LocalizedString("InvalidTargetAppId", Strings.ResourceManager, new object[]
			{
				notificationType
			});
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00006240 File Offset: 0x00004440
		public static LocalizedString OutlookInvalidPayloadData
		{
			get
			{
				return new LocalizedString("OutlookInvalidPayloadData", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00006257 File Offset: 0x00004457
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x040000B4 RID: 180
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(13);

		// Token: 0x040000B5 RID: 181
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.PushNotifications.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000049 RID: 73
		public enum IDs : uint
		{
			// Token: 0x040000B7 RID: 183
			InvalidSerializedToken = 3962995036U,
			// Token: 0x040000B8 RID: 184
			InvalidListOfAppIds = 2766800417U,
			// Token: 0x040000B9 RID: 185
			InvalidRecipientsList = 3216875091U,
			// Token: 0x040000BA RID: 186
			InvalidMnPayloadContent = 4087543273U,
			// Token: 0x040000BB RID: 187
			InvalidNotificationIdentifier = 333453583U,
			// Token: 0x040000BC RID: 188
			InvalidPayload = 816696259U,
			// Token: 0x040000BD RID: 189
			InvalidPlatform = 757184198U,
			// Token: 0x040000BE RID: 190
			InvalidAppId = 2677654617U,
			// Token: 0x040000BF RID: 191
			InvalidRecipientDeviceId = 162180343U,
			// Token: 0x040000C0 RID: 192
			InvalidRecipient = 2790684056U,
			// Token: 0x040000C1 RID: 193
			InvalidRecipientAppId = 1125823870U,
			// Token: 0x040000C2 RID: 194
			InvalidWorkloadId = 1327410019U,
			// Token: 0x040000C3 RID: 195
			OutlookInvalidPayloadData = 3937582550U
		}

		// Token: 0x0200004A RID: 74
		private enum ParamIDs
		{
			// Token: 0x040000C5 RID: 197
			InvalidTenantId,
			// Token: 0x040000C6 RID: 198
			PushNotificationServerExceptionMessage,
			// Token: 0x040000C7 RID: 199
			ExceptionMessageTimeoutCall,
			// Token: 0x040000C8 RID: 200
			ExceptionPushNotificationError,
			// Token: 0x040000C9 RID: 201
			InvalidMnRecipientLastSubscriptionUpdate,
			// Token: 0x040000CA RID: 202
			ExceptionEndpointNotFoundError,
			// Token: 0x040000CB RID: 203
			InvalidTargetAppId
		}
	}
}
