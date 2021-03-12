using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.WorkloadManagement;
using Microsoft.Exchange.Diagnostics.WorkloadManagement.Implementation;

namespace Microsoft.Exchange.Diagnostics.WorkloadManagement
{
	// Token: 0x020001EE RID: 494
	internal class ActivityContextState
	{
		// Token: 0x06000E02 RID: 3586 RVA: 0x0003A15C File Offset: 0x0003835C
		internal ActivityContextState(ActivityScopeImpl activityScopeImpl, ConcurrentDictionary<Enum, object> metadata)
		{
			this.activityId = new Guid?(activityScopeImpl.ActivityId);
			this.properties = metadata;
			this.ActivityType = activityScopeImpl.ActivityType;
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x0003A188 File Offset: 0x00038388
		internal ActivityContextState(Guid? activityId, ConcurrentDictionary<Enum, object> metadata)
		{
			this.activityId = activityId;
			this.properties = metadata;
			this.ActivityType = ActivityType.Request;
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000E04 RID: 3588 RVA: 0x0003A1A5 File Offset: 0x000383A5
		public Guid? ActivityId
		{
			get
			{
				return this.activityId;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000E05 RID: 3589 RVA: 0x0003A1AD File Offset: 0x000383AD
		// (set) Token: 0x06000E06 RID: 3590 RVA: 0x0003A1B5 File Offset: 0x000383B5
		internal ActivityType ActivityType { get; set; }

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000E07 RID: 3591 RVA: 0x0003A34C File Offset: 0x0003854C
		internal IEnumerable<KeyValuePair<Enum, object>> Properties
		{
			get
			{
				foreach (KeyValuePair<Enum, object> kvp in this.properties)
				{
					yield return kvp;
				}
				yield break;
			}
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0003A36C File Offset: 0x0003856C
		internal static ActivityContextState DeserializeFrom(HttpRequestMessageProperty wcfMessage)
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			if (wcfMessage != null && wcfMessage.Headers != null && wcfMessage.Headers.Keys != null)
			{
				text = wcfMessage.Headers["X-MSExchangeActivityCtx"];
				text2 = wcfMessage.Headers["client-request-id"];
				text3 = wcfMessage.Headers["return-client-request-id"];
				ExTraceGlobals.ActivityContextTracer.TraceDebug(0L, "ActivityContext.DeserializeFromMessage - in the wcfMessage header, Serialized ActivityScope ({0}) was {1}, {2} was {3}, {4} was {5} ", new object[]
				{
					"X-MSExchangeActivityCtx",
					text,
					"client-request-id",
					text2,
					"return-client-request-id",
					text3
				});
			}
			return ActivityContextState.DeserializeFromString(text, text2, text3);
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x0003A414 File Offset: 0x00038614
		internal static ActivityContextState DeserializeFrom(OperationContext wcfOperationContext)
		{
			string text = null;
			if (wcfOperationContext != null && wcfOperationContext.IncomingMessageHeaders != null)
			{
				int num = wcfOperationContext.IncomingMessageHeaders.FindHeader("X-MSExchangeActivityCtx", string.Empty);
				if (num >= 0)
				{
					text = wcfOperationContext.IncomingMessageHeaders.GetHeader<string>(num);
				}
				ExTraceGlobals.ActivityContextTracer.TraceDebug<string, string>(0L, "ActivityContext.DeserializeFromMessage - in the wcfOperationContext header, Serialized ActivityScope ({0}) was {1}", "X-MSExchangeActivityCtx", text);
			}
			return ActivityContextState.DeserializeFromString(text, null, null);
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x0003A474 File Offset: 0x00038674
		internal static ActivityContextState DeserializeFrom(HttpRequest httpRequest)
		{
			if (httpRequest == null)
			{
				return null;
			}
			return ActivityContextState.DeserializeFrom(new HttpRequestWrapper(httpRequest));
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x0003A488 File Offset: 0x00038688
		internal static ActivityContextState DeserializeFrom(HttpRequestBase httpRequestBase)
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			if (httpRequestBase != null && httpRequestBase.Headers != null && httpRequestBase.Headers.Keys != null)
			{
				text = httpRequestBase.Headers["X-MSExchangeActivityCtx"];
				text2 = httpRequestBase.Headers["client-request-id"];
				text3 = httpRequestBase.Headers["return-client-request-id"];
				ExTraceGlobals.ActivityContextTracer.TraceDebug(0L, "ActivityContext.DeserializeFromMessage - in the httpRequest header, Serialized ActivityScope ({0}) was {1}, {2} was {3}, {4} was {5}", new object[]
				{
					"X-MSExchangeActivityCtx",
					text,
					"client-request-id",
					text2,
					"return-client-request-id",
					text3
				});
			}
			return ActivityContextState.DeserializeFromString(text, text2, text3);
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x0003A52E File Offset: 0x0003872E
		internal static string GetMinimalSerializedScope(Guid activityId)
		{
			return ActivityContextState.minimalSerializedActivityContextStatePrefix + activityId.ToString("D") + ActivityContextState.minimalSerializedActivityContextStateSuffix;
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x0003A54C File Offset: 0x0003874C
		private static bool TryDeserializeMinimalFromString(string activityCtxString, out Guid? activityId)
		{
			activityId = null;
			if (activityCtxString.Length != ActivityContextState.minimalSerializedActivityContextStateLength)
			{
				return false;
			}
			if (!activityCtxString.StartsWith(ActivityContextState.minimalSerializedActivityContextStatePrefix))
			{
				return false;
			}
			if (!activityCtxString.EndsWith(ActivityContextState.minimalSerializedActivityContextStateSuffix))
			{
				return false;
			}
			string input = activityCtxString.Substring(ActivityContextState.minimalSerializedActivityContextStatePrefix.Length, ActivityContextState.minimalSerializedActivityIdLength);
			Guid value;
			if (!Guid.TryParse(input, out value))
			{
				return false;
			}
			activityId = new Guid?(value);
			return true;
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x0003A5BC File Offset: 0x000387BC
		private static ActivityContextState DeserializeFromString(string activityCtxString, string clientRequestIdString, string returnClientRequestIdString)
		{
			Version v = null;
			Guid? guid = null;
			ConcurrentDictionary<Enum, object> concurrentDictionary = new ConcurrentDictionary<Enum, object>();
			if (!string.IsNullOrWhiteSpace(activityCtxString) && !ActivityContextState.TryDeserializeMinimalFromString(activityCtxString, out guid))
			{
				string[][] validatedSubcomponents = ActivityContextState.GetValidatedSubcomponents(activityCtxString);
				if (validatedSubcomponents != null)
				{
					Guid empty = Guid.Empty;
					bool flag = Version.TryParse(validatedSubcomponents[0][1], out v);
					if (flag && v == ActivityContextState.SerializationVersion)
					{
						flag = (flag && Guid.TryParse(validatedSubcomponents[1][1], out empty));
						if (flag)
						{
							guid = new Guid?(empty);
							string value = HttpUtility.UrlDecode(validatedSubcomponents[2][1]);
							if (!string.IsNullOrWhiteSpace(value))
							{
								concurrentDictionary.TryAdd(ActivityStandardMetadata.Component, value);
							}
							ActivityContextState.TryDeserializePayload(concurrentDictionary, validatedSubcomponents[3][1]);
						}
						else
						{
							ExTraceGlobals.ActivityContextTracer.TraceDebug<string>(0L, "Format of serialized ActivityScope was incorrect: {0}", activityCtxString);
						}
					}
					else
					{
						ExTraceGlobals.ActivityContextTracer.TraceDebug<string>(0L, "Failed to parse the version or the versions are different, version value {0}", validatedSubcomponents[0][1]);
					}
				}
				else
				{
					ExTraceGlobals.ActivityContextTracer.TraceDebug<string>(0L, "Format of serialized ActivityScope was incorrect: {0}", activityCtxString);
				}
			}
			if (!string.IsNullOrWhiteSpace(clientRequestIdString))
			{
				if (clientRequestIdString.Length > 256)
				{
					clientRequestIdString = clientRequestIdString.Substring(0, 256);
				}
				if (!string.IsNullOrWhiteSpace(clientRequestIdString))
				{
					concurrentDictionary.TryAdd(ActivityStandardMetadata.ClientRequestId, clientRequestIdString);
				}
				bool flag2 = false;
				bool.TryParse(returnClientRequestIdString, out flag2);
				if (flag2)
				{
					concurrentDictionary.TryAdd(ActivityStandardMetadata.ReturnClientRequestId, "true");
				}
			}
			if (guid == null && concurrentDictionary.Count == 0)
			{
				return null;
			}
			return new ActivityContextState(guid, concurrentDictionary);
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x0003A738 File Offset: 0x00038938
		private static string[][] GetValidatedSubcomponents(string serializedString)
		{
			string[] array = serializedString.Split(ActivityContextState.SerializedElementDelimiter, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length != 4)
			{
				return null;
			}
			string[][] array2 = new string[array.Length][];
			array2[0] = array[0].Split(ActivityContextState.SerializedKeyValueDelimiter);
			array2[1] = array[1].Split(ActivityContextState.SerializedKeyValueDelimiter);
			array2[2] = array[2].Split(ActivityContextState.SerializedKeyValueDelimiter);
			int num = array[3].IndexOf(ActivityContextState.SerializedKeyValueDelimiter[0]);
			if (num <= 0)
			{
				return null;
			}
			array2[3] = new string[]
			{
				array[3].Substring(0, num),
				array[3].Substring(num + 1)
			};
			if (array2[0].Length != 2 || string.Compare(array2[0][0], "V", StringComparison.OrdinalIgnoreCase) != 0 || array2[1].Length != 2 || string.Compare(array2[1][0], "Id", StringComparison.OrdinalIgnoreCase) != 0 || array2[2].Length != 2 || string.Compare(array2[2][0], "C", StringComparison.OrdinalIgnoreCase) != 0)
			{
				return null;
			}
			return array2;
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x0003A820 File Offset: 0x00038A20
		private static void TryDeserializePayload(ConcurrentDictionary<Enum, object> metadata, string payloadValue)
		{
			if (payloadValue.Length > 2048 || string.IsNullOrEmpty(payloadValue))
			{
				ExTraceGlobals.ActivityContextTracer.TraceDebug<int>(0L, "Payload too big or empty. Payload length was {0}.", (payloadValue == null) ? -1 : payloadValue.Length);
				return;
			}
			try
			{
				byte[] buffer = Convert.FromBase64String(payloadValue);
				using (MemoryStream memoryStream = new MemoryStream(buffer))
				{
					using (BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.UTF8))
					{
						while (memoryStream.Position < memoryStream.Length)
						{
							string enumName = binaryReader.ReadString();
							if (memoryStream.Position < memoryStream.Length)
							{
								string value = binaryReader.ReadString();
								Enum @enum = ActivityContext.LookupEnum(enumName);
								if (!string.IsNullOrEmpty(value) && ActivityContextState.PayloadAllowedMetadata.Contains(@enum))
								{
									metadata.TryAdd(@enum, value);
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				ExTraceGlobals.ActivityContextTracer.TraceDebug<string, string>(0L, "Malformed payload value: '{0}'. Exception: {1}", payloadValue, ex.ToString());
			}
		}

		// Token: 0x04000A5D RID: 2653
		internal const string SerializationHeaderName = "X-MSExchangeActivityCtx";

		// Token: 0x04000A5E RID: 2654
		internal const string ClientRequestIdHeaderName = "client-request-id";

		// Token: 0x04000A5F RID: 2655
		internal const string ReturnClientRequestIdHeaderName = "return-client-request-id";

		// Token: 0x04000A60 RID: 2656
		internal const int MaximumClientRequestIdLength = 256;

		// Token: 0x04000A61 RID: 2657
		internal const string RequestIdResponseHeaderName = "request-id";

		// Token: 0x04000A62 RID: 2658
		internal const string SerializedVersionKey = "V";

		// Token: 0x04000A63 RID: 2659
		internal const string SerializedActivityIdKey = "Id";

		// Token: 0x04000A64 RID: 2660
		internal const string SerializedComponentKey = "C";

		// Token: 0x04000A65 RID: 2661
		internal const string SerializedPayloadKey = "P";

		// Token: 0x04000A66 RID: 2662
		internal const int MaximumPayloadStrLength = 2048;

		// Token: 0x04000A67 RID: 2663
		internal const int MaximumPayloadBinaryLength = 1024;

		// Token: 0x04000A68 RID: 2664
		internal static readonly Version SerializationVersion = new Version(1, 0, 0, 0);

		// Token: 0x04000A69 RID: 2665
		internal static readonly HashSet<Enum> PayloadAllowedMetadata = new HashSet<Enum>
		{
			ActivityStandardMetadata.Action,
			ActivityStandardMetadata.ClientRequestId
		};

		// Token: 0x04000A6A RID: 2666
		internal static readonly char[] SerializedElementDelimiter = new char[]
		{
			';'
		};

		// Token: 0x04000A6B RID: 2667
		internal static readonly char[] SerializedKeyValueDelimiter = new char[]
		{
			'='
		};

		// Token: 0x04000A6C RID: 2668
		private static readonly string minimalSerializedActivityContextStatePrefix = string.Concat(new object[]
		{
			"V",
			ActivityContextState.SerializedKeyValueDelimiter[0],
			ActivityContextState.SerializationVersion,
			ActivityContextState.SerializedElementDelimiter[0],
			"Id",
			ActivityContextState.SerializedKeyValueDelimiter[0]
		});

		// Token: 0x04000A6D RID: 2669
		private static readonly string minimalSerializedActivityContextStateSuffix = string.Concat(new object[]
		{
			ActivityContextState.SerializedElementDelimiter[0],
			"C",
			ActivityContextState.SerializedKeyValueDelimiter[0],
			string.Empty,
			ActivityContextState.SerializedElementDelimiter[0],
			"P",
			ActivityContextState.SerializedKeyValueDelimiter[0],
			string.Empty
		});

		// Token: 0x04000A6E RID: 2670
		private static readonly int minimalSerializedActivityContextStateLength = ActivityContextState.GetMinimalSerializedScope(Guid.NewGuid()).Length;

		// Token: 0x04000A6F RID: 2671
		private static readonly int minimalSerializedActivityIdLength = ActivityContextState.minimalSerializedActivityContextStateLength - ActivityContextState.minimalSerializedActivityContextStatePrefix.Length - ActivityContextState.minimalSerializedActivityContextStateSuffix.Length;

		// Token: 0x04000A70 RID: 2672
		private readonly Guid? activityId;

		// Token: 0x04000A71 RID: 2673
		private ConcurrentDictionary<Enum, object> properties;
	}
}
