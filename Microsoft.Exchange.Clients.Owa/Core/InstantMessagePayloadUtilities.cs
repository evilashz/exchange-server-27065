using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Services.Protocols;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000144 RID: 324
	internal static class InstantMessagePayloadUtilities
	{
		// Token: 0x06000AFB RID: 2811 RVA: 0x0004DB30 File Offset: 0x0004BD30
		internal static void GenerateUnavailablePayload(InstantMessagePayload payload, Exception exception, string errorLocation, InstantMessageFailure errorCode, bool recurseThroughException)
		{
			InstantMessagePayloadUtilities.GenerateUnavailablePayload(payload, exception, errorLocation, errorCode, 0, recurseThroughException);
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0004DB40 File Offset: 0x0004BD40
		internal static void GenerateUnavailablePayload(InstantMessagePayload payload, Exception exception, string errorLocation, InstantMessageFailure errorCode, int reconnectInterval, bool recurseThroughException)
		{
			if (exception != null && recurseThroughException)
			{
				while (exception.InnerException != null)
				{
					exception = exception.InnerException;
				}
			}
			string text = string.Empty;
			if (exception != null)
			{
				if (exception is SoapException)
				{
					SoapException ex = exception as SoapException;
					text = string.Format(CultureInfo.InvariantCulture, "Exception Message: {0}, Node: {1}, Code: {2}", new object[]
					{
						exception.Message,
						ex.Node,
						ex.Code.ToString()
					});
				}
				else
				{
					text = "Exception Message: " + exception.Message;
				}
			}
			ExTraceGlobals.InstantMessagingTracer.TraceError<string>(0L, errorLocation, text);
			if (payload == null)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceError(0L, "InstantMessagePayloadUtilities.GenerateUnavailablePayload. Payload is null.");
				return;
			}
			int length;
			lock (payload)
			{
				length = payload.Length;
				payload.Append("UN(");
				payload.Append((int)errorCode);
				payload.Append(",");
				if (reconnectInterval >= 0)
				{
					payload.Append(reconnectInterval);
					payload.Append(",");
				}
				payload.Append("'");
				payload.Append(Utilities.JavascriptEncode(string.IsNullOrEmpty(text) ? (errorLocation ?? string.Empty) : text));
				payload.Append("');");
			}
			payload.PickupData(length);
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0004DC94 File Offset: 0x0004BE94
		internal static void GenerateMessageNotDeliveredPayload(InstantMessagePayload payload, string chatId)
		{
			InstantMessagePayloadUtilities.GenerateMessageNotDeliveredPayload(payload, chatId, false);
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0004DCA0 File Offset: 0x0004BEA0
		internal static void GenerateInstantMessageUnavailablePayload(InstantMessagePayload payload, string methodName, InstantMessageFailure errorCode, Exception exception)
		{
			string arg = string.Empty;
			if (exception != null && exception.Message != null)
			{
				arg = exception.Message;
			}
			ExTraceGlobals.InstantMessagingTracer.TraceError<string, int, string>(0L, "{0} failed with error code {1}. {2}", methodName, (int)errorCode, arg);
			if (payload == null)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceError(0L, "InstantMessagePayloadUtilities.GenerateInstantMessageUnavailablePayload. Payload is null.");
				return;
			}
			int length;
			lock (payload)
			{
				length = payload.Length;
				payload.Append("UN(");
				payload.Append((int)errorCode);
				payload.Append(");");
			}
			payload.PickupData(length);
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0004DD44 File Offset: 0x0004BF44
		internal static void GenerateMessageNotDeliveredPayload(InstantMessagePayload payload, string methodName, int conversationId, Exception exception)
		{
			string arg = string.Empty;
			if (exception != null && exception.Message != null)
			{
				arg = exception.Message;
			}
			ExTraceGlobals.InstantMessagingTracer.TraceError<string, string>(0L, "{0} failed. {1}", methodName, arg);
			InstantMessagePayloadUtilities.GenerateMessageNotDeliveredPayload(payload, conversationId.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0004DD8E File Offset: 0x0004BF8E
		internal static void GenerateMessageNotDeliveredPayload(InstantMessagePayload payload, string chatId, bool serverPolicy)
		{
			if (serverPolicy)
			{
				InstantMessagePayloadUtilities.GenerateInstantMessageAlertPayload(payload, chatId, InstantMessageAlert.FailedDeliveryDueToServerPolicy);
				return;
			}
			InstantMessagePayloadUtilities.GenerateInstantMessageAlertPayload(payload, chatId, InstantMessageAlert.FailedDelivery);
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0004DDA6 File Offset: 0x0004BFA6
		internal static void GenerateInstantMessageAlertPayload(InstantMessagePayload payload, string chatId, InstantMessageAlert alertType)
		{
			InstantMessagePayloadUtilities.GenerateInstantMessageAlertPayload(payload, chatId, alertType, null);
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0004DDB4 File Offset: 0x0004BFB4
		internal static void GenerateInstantMessageAlertPayload(InstantMessagePayload payload, string chatId, InstantMessageAlert alertType, string imAddress)
		{
			if (payload == null)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceError(0L, "InstantMessagePayloadUtilities.GenerateInstantMessageAlertPayload. Payload is null.");
				return;
			}
			int length;
			lock (payload)
			{
				length = payload.Length;
				payload.Append("IMA(\"");
				payload.Append(Utilities.JavascriptEncode(chatId));
				payload.Append("\", ");
				payload.Append((int)alertType);
				if (imAddress != null)
				{
					payload.Append(", \"");
					payload.Append(Utilities.JavascriptEncode(imAddress));
					payload.Append("\"");
				}
				payload.Append(");");
			}
			payload.PickupData(length);
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0004DE68 File Offset: 0x0004C068
		internal static void GenerateUpdatePresencePayload(InstantMessagePayload payload, int presence)
		{
			if (payload == null)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceError(0L, "InstantMessagePayloadUtilities.GenerateUpdatePresencePayload. Payload is null.");
				return;
			}
			int length;
			lock (payload)
			{
				length = payload.Length;
				payload.Append("IMUP(");
				payload.Append(presence);
				payload.Append(");");
			}
			payload.PickupData(length);
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0004DEE0 File Offset: 0x0004C0E0
		internal static void GenerateParticipantJoinedPayload(InstantMessagePayload payload, string chatId, string imAddress, string displayName)
		{
			if (payload == null)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceError(0L, "InstantMessagePayloadUtilities.GenerateParticipantJoinedPayload. Payload is null.");
				return;
			}
			int length;
			lock (payload)
			{
				length = payload.Length;
				payload.Append("ACC(\"");
				payload.Append(chatId);
				payload.Append("\", \"");
				payload.Append(Utilities.JavascriptEncode(imAddress));
				payload.Append("\", \"");
				payload.Append(Utilities.JavascriptEncode(displayName));
				payload.Append("\");");
			}
			payload.PickupData(length);
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0004DF84 File Offset: 0x0004C184
		internal static void GenerateParticipantLeftPayload(InstantMessagePayload payload, string chatId, string imAddress)
		{
			if (payload == null)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceError(0L, "InstantMessagePayloadUtilities.GenerateParticipantLeftPayload. Payload is null.");
				return;
			}
			int length;
			lock (payload)
			{
				length = payload.Length;
				payload.Append("RCC(\"");
				payload.Append(chatId);
				payload.Append("\", \"");
				payload.Append(Utilities.JavascriptEncode(imAddress));
				payload.Append("\");");
			}
			payload.PickupData(length);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0004E010 File Offset: 0x0004C210
		internal static void GenerateInstantMessageConversationClosePayload(InstantMessagePayload payload, string chatId)
		{
			if (payload == null)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceError(0L, "InstantMessagePayloadUtilities.GenerateInstantMessageConversationClosePayload. Payload is null.");
				return;
			}
			int length;
			lock (payload)
			{
				length = payload.Length;
				payload.Append("CLSC(\"");
				payload.Append(chatId);
				payload.Append("\");");
			}
			payload.PickupData(length);
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0004E088 File Offset: 0x0004C288
		internal static void GenerateMaxChatSessionCountPayload(InstantMessagePayload payload)
		{
			if (payload == null)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceError(0L, "InstantMessagePayloadUtilities.GenerateMaxChatSessionCountPayload. Payload is null.");
				return;
			}
			int length;
			lock (payload)
			{
				length = payload.Length;
				payload.Append("MCht();");
			}
			payload.PickupData(length);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0004E0EC File Offset: 0x0004C2EC
		internal static void GenerateInstantMessageSignInPayload(InstantMessagePayload payload, bool isStarted)
		{
			if (payload == null)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceError(0L, "InstantMessagePayloadUtilities.GenerateInstantMessageSignInPayload. Payload is null.");
				return;
			}
			int length;
			lock (payload)
			{
				length = payload.Length;
				payload.Append("SI(");
				payload.Append(isStarted ? "1" : "0");
				payload.Append(");");
			}
			payload.PickupData(length);
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0004E170 File Offset: 0x0004C370
		internal static void GenerateProxiesPayload(InstantMessagePayload payload, Dictionary<string, string[]> proxyAddresses)
		{
			if (payload == null)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceError(0L, "InstantMessagePayloadUtilities.GenerateInstantMessageSignInPayload. Payload is null.");
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int length;
			lock (payload)
			{
				length = payload.Length;
				payload.Append("PXY([");
				foreach (string text in proxyAddresses.Keys)
				{
					if (stringBuilder.Length != 0)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append("['");
					stringBuilder.Append(Utilities.JavascriptEncode(text));
					stringBuilder.Append("',[");
					string[] array = null;
					if (proxyAddresses.TryGetValue(text, out array) && array != null && array.Length > 0)
					{
						for (int i = 0; i < array.Length; i++)
						{
							if (i != 0)
							{
								stringBuilder.Append(",");
							}
							stringBuilder.Append("'");
							stringBuilder.Append(Utilities.JavascriptEncode(array[i]));
							stringBuilder.Append("'");
						}
					}
					stringBuilder.Append("]]");
				}
				stringBuilder.Append("]);");
				payload.Append(stringBuilder);
				stringBuilder = null;
			}
			payload.PickupData(length);
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0004E30C File Offset: 0x0004C50C
		internal static void GenerateBuddyListPayload(InstantMessagePayload payload, IEnumerable<InstantMessageGroup> groups, IEnumerable<InstantMessageBuddy> buddies)
		{
			ExTraceGlobals.InstantMessagingTracer.TraceDebug(0L, "InstantMessagePayloadUtilities.GenerateBuddyListPayload.");
			if (payload == null)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceError(0L, "InstantMessagePayloadUtilities.GenerateBuddyListPayload. Payload is null.");
				return;
			}
			int length;
			lock (payload)
			{
				length = payload.Length;
				payload.Append("APS([[");
				if (groups != null)
				{
					payload.Append(string.Join(",", (from g in groups
					where g.VisibleOnClient
					select g.SerializeToJavascript()).ToArray<string>()));
				}
				payload.Append("],[");
				if (buddies != null)
				{
					payload.Append(string.Join(",", (from b in buddies
					select b.SerializeToJavascript()).ToArray<string>()));
				}
				payload.Append("]]);");
			}
			payload.PickupData(length);
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0004E438 File Offset: 0x0004C638
		internal static void GenerateDeletedGroupsPayload(InstantMessagePayload payload, List<InstantMessageGroup> groups)
		{
			ExTraceGlobals.InstantMessagingTracer.TraceDebug(0L, "InstantMessagePayloadUtilities.GenerateDeletedGroupsPayload.");
			if (payload == null)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceError(0L, "InstantMessagePayloadUtilities.GenerateDeletedGroupsPayload. Payload is null.");
				return;
			}
			if (groups == null || groups.Count == 0)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceDebug(0L, "InstantMessagePayloadUtilities.GenerateDeletedGroupsPayload. Empty group list, ignoring");
				return;
			}
			int length;
			lock (payload)
			{
				length = payload.Length;
				payload.Append("RG([");
				payload.Append(string.Join(",", (from g in groups
				select g.SerializeIdToJavascript()).ToArray<string>()));
				payload.Append("]);");
			}
			payload.PickupData(length);
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0004E5AC File Offset: 0x0004C7AC
		internal static void GenerateDeletedFromGroupsPayload(InstantMessagePayload payload, List<InstantMessageBuddy> buddies)
		{
			ExTraceGlobals.InstantMessagingTracer.TraceDebug(0L, "InstantMessagePayloadUtilities.GenerateDeletedFromGroupsPayload.");
			if (payload == null)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceError(0L, "InstantMessagePayloadUtilities.GenerateDeletedFromGroupsPayload. Payload is null.");
				return;
			}
			if (buddies == null || buddies.Count == 0)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceDebug(0L, "InstantMessagePayloadUtilities.GenerateDeletedFromGroupsPayload. Empty buddy list, ignoring");
				return;
			}
			int length;
			lock (payload)
			{
				length = payload.Length;
				payload.Append("RCG([");
				payload.Append(string.Join(",", (from b in buddies
				select string.Join(",", (from gId in b.GroupIds
				select string.Concat(new string[]
				{
					"['",
					Utilities.JavascriptEncode(InstantMessageUtilities.ToGroupFormat(gId)),
					"',",
					b.SerializeSipToJavascript(),
					"]"
				})).ToArray<string>())).ToArray<string>()));
				payload.Append("]);");
			}
			payload.PickupData(length);
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0004E688 File Offset: 0x0004C888
		internal static void GenerateDeletedBuddiesPayload(InstantMessagePayload payload, List<InstantMessageBuddy> buddies)
		{
			ExTraceGlobals.InstantMessagingTracer.TraceDebug(0L, "InstantMessagePayloadUtilities.GenerateDeletedBuddiesPayload.");
			if (payload == null)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceError(0L, "InstantMessagePayloadUtilities.GenerateDeletedBuddiesPayload. Payload is null.");
				return;
			}
			if (buddies == null || buddies.Count == 0)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceDebug(0L, "InstantMessagePayloadUtilities.GenerateDeletedBuddiesPayload. Empty buddies list, ignoring");
				return;
			}
			int length;
			lock (payload)
			{
				length = payload.Length;
				payload.Append("RC([");
				payload.Append(string.Join(",", (from b in buddies
				select b.SerializeSipToJavascript()).ToArray<string>()));
				payload.Append("]);");
			}
			payload.PickupData(length);
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0004E75C File Offset: 0x0004C95C
		internal static void GenerateGroupRenamePayload(InstantMessagePayload payload, InstantMessageGroup group)
		{
			ExTraceGlobals.InstantMessagingTracer.TraceDebug(0L, "InstantMessagePayloadUtilities.GenerateGroupRenamePayload.");
			if (payload == null)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceError(0L, "InstantMessagePayloadUtilities.GenerateGroupRenamePayload. Payload is null.");
				return;
			}
			if (group == null)
			{
				ExTraceGlobals.InstantMessagingTracer.TraceDebug(0L, "InstantMessagePayloadUtilities.GenerateGroupRenamePayload. Null group, ignoring");
				return;
			}
			int length;
			lock (payload)
			{
				length = payload.Length;
				payload.Append("GPC(");
				payload.Append(group.SerializeIdAndNameToJavascript());
				payload.Append(");");
			}
			payload.PickupData(length);
		}
	}
}
