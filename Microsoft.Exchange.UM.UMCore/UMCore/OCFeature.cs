using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.ClientAccess.Messages;
using Microsoft.Exchange.UM.TroubleshootingTool.Shared;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000193 RID: 403
	internal class OCFeature
	{
		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x00033A6E File Offset: 0x00031C6E
		// (set) Token: 0x06000BEA RID: 3050 RVA: 0x00033A76 File Offset: 0x00031C76
		internal OCFeatureType FeatureType
		{
			get
			{
				return this.ocFeature;
			}
			set
			{
				this.ocFeature = value;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x00033A7F File Offset: 0x00031C7F
		internal bool SkipPin
		{
			get
			{
				return this.skipPin;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x00033A87 File Offset: 0x00031C87
		internal string Subject
		{
			get
			{
				return this.subject;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x00033A8F File Offset: 0x00031C8F
		internal bool IsUrgent
		{
			get
			{
				return this.isUrgent;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x00033A97 File Offset: 0x00031C97
		internal string ReferredBy
		{
			get
			{
				return this.referredBy;
			}
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x00033AA0 File Offset: 0x00031CA0
		internal void Parse(CallContext context, IList<PlatformSignalingHeader> headers, string resourcePath)
		{
			string text = null;
			RouterUtils.TryGetHeaderValue(headers, "Referred-By", out this.referredBy);
			RouterUtils.TryGetHeaderValue(headers, "Subject", out this.subject);
			RouterUtils.TryGetHeaderValue(headers, "Priority", out text);
			this.isUrgent = (0 == string.Compare(text, "urgent", StringComparison.InvariantCultureIgnoreCase));
			string featureData = null;
			if (RouterUtils.TryGetHeaderValue(headers, "Ms-Exchange-Command", out text))
			{
				this.ParseMsExchangeCommandHeader(context, text, out this.skipPin, out featureData);
			}
			else
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "ProcessOCFeature: Ms-Exchange-Command header not specified", new object[0]);
			}
			if (string.IsNullOrEmpty(resourcePath))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "ProcessOCFeature: No local-resource-path found, no special OC feature requested", new object[0]);
				return;
			}
			try
			{
				this.SelectOCFeature(context, (OCFeatureType)Enum.Parse(typeof(OCFeatureType), resourcePath, true), featureData);
			}
			catch (ArgumentException ex)
			{
				CallIdTracer.TraceError(ExTraceGlobals.CallSessionTracer, this, "ProcessOCFeature: Enum.Parse - {0}", new object[]
				{
					ex
				});
				throw CallRejectedException.Create(Strings.OCFeatureDataValidation(Strings.OCFeatureInvalidLocalResourcePath(resourcePath)), CallEndingReason.OCFeatureInvalidLocalResourcePath, "local-resource-path: {0}.", new object[]
				{
					resourcePath
				});
			}
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x00033BC8 File Offset: 0x00031DC8
		private void ParseMsExchangeCommandHeader(CallContext context, string headerValue, out bool skipPin, out string itemId)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "ParseMsExchangeCommandHeader: {0}", new object[]
			{
				headerValue
			});
			skipPin = false;
			itemId = null;
			string[] array = headerValue.Split(new char[]
			{
				';'
			}, StringSplitOptions.RemoveEmptyEntries);
			foreach (string text in array)
			{
				string text2 = text.Trim();
				if (string.Equals(text2, "skip-pin", StringComparison.InvariantCultureIgnoreCase))
				{
					skipPin = true;
					if (skipPin && !string.IsNullOrEmpty(context.Extension))
					{
						CallIdTracer.TraceError(ExTraceGlobals.CallSessionTracer, this, "Skip-pin specified on a non subscriber access call (diversion={0})", new object[]
						{
							context.Extension
						});
						skipPin = false;
					}
				}
				else
				{
					int num = text2.IndexOf("itemId=", StringComparison.InvariantCultureIgnoreCase);
					if (num >= 0)
					{
						itemId = text2.Substring(num + "itemId=".Length);
						if (itemId.Length >= 2 && itemId[0] == '"' && itemId[itemId.Length - 1] == '"')
						{
							itemId = itemId.Substring(1, itemId.Length - 2);
						}
					}
				}
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "ParseMsExchangeCommandHeader: skipPin={0} itemId={1}", new object[]
			{
				headerValue,
				skipPin,
				itemId
			});
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00033D28 File Offset: 0x00031F28
		private void SelectOCFeature(CallContext context, OCFeatureType feature, string featureData)
		{
			this.ocFeature = feature;
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "SelectOCFeature: Selecting OCFeature={0}, data={1}", new object[]
			{
				this.ocFeature,
				featureData
			});
			if (feature == OCFeatureType.None)
			{
				return;
			}
			bool flag = feature == OCFeatureType.Greeting || feature == OCFeatureType.Voicemail;
			if (flag)
			{
				if (!string.IsNullOrEmpty(context.Extension))
				{
					CallIdTracer.TraceError(ExTraceGlobals.CallSessionTracer, this, "SelectOCFeature: Subscriber access feature {0} cannot specify diversion {1}", new object[]
					{
						feature,
						context.Extension
					});
					throw CallRejectedException.Create(Strings.OCFeatureDataValidation(Strings.OCFeatureSACannotHaveDiversion(feature.ToString())), CallEndingReason.OCFeatureSACannotHaveDiversion, "Feature requested: {0}.", new object[]
					{
						feature
					});
				}
			}
			else if (string.IsNullOrEmpty(context.Extension))
			{
				CallIdTracer.TraceError(ExTraceGlobals.CallSessionTracer, this, "SelectOCFeature: Call answering feature {0} cannot have an empty diversion", new object[]
				{
					feature
				});
				throw CallRejectedException.Create(Strings.OCFeatureDataValidation(Strings.OCFeatureCAMustHaveDiversion(feature.ToString())), CallEndingReason.OCFeatureCAMustHaveDiversion, "Feature requested: {0}.", new object[]
				{
					feature.ToString()
				});
			}
			switch (this.ocFeature)
			{
			case OCFeatureType.Greeting:
				context.WebServiceRequest = new PlayOnPhoneGreetingRequest
				{
					GreetingType = UMGreetingType.NormalCustom,
					DialString = string.Empty
				};
				return;
			case OCFeatureType.Voicemail:
				if (!string.IsNullOrEmpty(featureData))
				{
					this.ValidateVoicemailItemId(featureData);
					this.ocFeature = OCFeatureType.SingleVoicemail;
					context.WebServiceRequest = new PlayOnPhoneMessageRequest
					{
						ObjectId = featureData,
						DialString = string.Empty
					};
					return;
				}
				break;
			default:
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "OCFeature={0} requires no additional preparation", new object[]
				{
					this.ocFeature
				});
				break;
			}
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x00033EFC File Offset: 0x000320FC
		private void ValidateVoicemailItemId(string itemId)
		{
			try
			{
				Convert.FromBase64String(itemId);
			}
			catch (Exception ex)
			{
				if (ex is FormatException || ex is ArgumentException)
				{
					CallIdTracer.TraceError(ExTraceGlobals.CallSessionTracer, this, "ValidateVoicemailItemId: {0}", new object[]
					{
						ex
					});
					throw CallRejectedException.Create(Strings.OCFeatureDataValidation(Strings.OCFeatureInvalidItemId(itemId)), ex, CallEndingReason.OCFeatureInvalidItemId, "Item ID: {0}.", new object[]
					{
						itemId
					});
				}
				throw;
			}
		}

		// Token: 0x04000A02 RID: 2562
		private OCFeatureType ocFeature;

		// Token: 0x04000A03 RID: 2563
		private bool skipPin;

		// Token: 0x04000A04 RID: 2564
		private string subject;

		// Token: 0x04000A05 RID: 2565
		private bool isUrgent;

		// Token: 0x04000A06 RID: 2566
		private string referredBy;
	}
}
