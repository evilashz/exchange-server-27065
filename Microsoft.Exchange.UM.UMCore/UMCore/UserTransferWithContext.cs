using System;
using System.Collections;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.TroubleshootingTool.Shared;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000232 RID: 562
	internal class UserTransferWithContext : ReferredByHeaderHandler
	{
		// Token: 0x06001050 RID: 4176 RVA: 0x00048B97 File Offset: 0x00046D97
		public UserTransferWithContext()
		{
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x00048B9F File Offset: 0x00046D9F
		public UserTransferWithContext(string referredByHostUri)
		{
			this.referredByHostUri = referredByHostUri;
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x00048BB0 File Offset: 0x00046DB0
		public static bool TryParseReferredByHeader(string referredByHeader, UMDialPlan dialplan, out UMRecipient subscriber, out UserTransferWithContext.DeserializedReferredByHeader parsedHeader)
		{
			ValidateArgument.NotNullOrEmpty(referredByHeader, "ReferredByHeader");
			ValidateArgument.NotNull(dialplan, "UMDialPlan");
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, null, "UserTransferWithContext::TryParseReferredByHeader() header value = [{0}]", new object[]
			{
				referredByHeader
			});
			UserTransferWithContext userTransferWithContext = new UserTransferWithContext();
			parsedHeader = null;
			subscriber = null;
			if (!userTransferWithContext.TryDeserialize(referredByHeader, out parsedHeader))
			{
				return false;
			}
			PIIMessage data = PIIMessage.Create(PIIType._PhoneNumber, parsedHeader.Extension);
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, null, data, "UserTransferWithContext::TryParseReferredByHeader() extension = _PhoneNumber, Calltype = {0}", new object[]
			{
				parsedHeader.TypeOfTransferredCall
			});
			try
			{
				subscriber = UMRecipient.Factory.FromExtension<UMRecipient>(parsedHeader.Extension, dialplan, null);
			}
			catch (LocalizedException ex)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, null, data, "Unable to validate entered mailbox digits '_PhoneNumber' due to exception e '{0}'", new object[]
				{
					ex
				});
			}
			if (subscriber == null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, null, data, "UserTransferWithContext::TryParseReferredByHeader Could not look up subscriber with extension = _PhoneNumber", new object[0]);
			}
			return subscriber != null;
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x00048CAC File Offset: 0x00046EAC
		internal bool TryDeserialize(string header, out UserTransferWithContext.DeserializedReferredByHeader headerValues)
		{
			Hashtable hashtable = base.ParseHeader(header);
			headerValues = new UserTransferWithContext.DeserializedReferredByHeader();
			if (hashtable.Count == 0)
			{
				return false;
			}
			if (!hashtable.ContainsKey("c"))
			{
				CallIdTracer.TraceWarning(ExTraceGlobals.CallSessionTracer, this, "TryDeserialize: Could not find a Command parameter", new object[0]);
				return false;
			}
			if (string.Equals((string)hashtable["c"], "ms-ova-data", StringComparison.OrdinalIgnoreCase))
			{
				headerValues.TypeOfTransferredCall = 3;
			}
			else
			{
				if (!string.Equals((string)hashtable["c"], "ms-ca-data", StringComparison.OrdinalIgnoreCase))
				{
					CallIdTracer.TraceWarning(ExTraceGlobals.CallSessionTracer, this, "TryDeserialize: Invalid Command value : '{0}'", new object[]
					{
						(string)hashtable["c"]
					});
					return false;
				}
				headerValues.TypeOfTransferredCall = 4;
			}
			if (!hashtable.ContainsKey("v") || !string.Equals((string)hashtable["v"], "1.0", StringComparison.OrdinalIgnoreCase))
			{
				CallIdTracer.TraceWarning(ExTraceGlobals.CallSessionTracer, this, "TryDeserialize: Could not find a Valid Version parameter", new object[0]);
				return false;
			}
			if (!hashtable.ContainsKey("extension") || string.IsNullOrEmpty((string)hashtable["extension"]))
			{
				CallIdTracer.TraceWarning(ExTraceGlobals.CallSessionTracer, this, "TryDeserialize: Could not find a Valid Extension parameter", new object[0]);
				return false;
			}
			headerValues.Extension = (string)hashtable["extension"];
			if (headerValues.TypeOfTransferredCall == 4)
			{
				if (!hashtable.ContainsKey("phone-context") || string.IsNullOrEmpty((string)hashtable["phone-context"]))
				{
					CallIdTracer.TraceWarning(ExTraceGlobals.CallSessionTracer, this, "TryDeserialize: Could not find a Valid PhoneContext parameter", new object[0]);
					return false;
				}
				headerValues.PhoneContext = (string)hashtable["phone-context"];
			}
			return true;
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x00048E68 File Offset: 0x00047068
		internal PlatformSipUri SerializeCACallTransferWithContextUri(string extension, string phoneContext)
		{
			if (string.IsNullOrEmpty(extension))
			{
				throw new ArgumentNullException("extension");
			}
			if (string.IsNullOrEmpty(phoneContext))
			{
				throw new ArgumentNullException("phoneContext");
			}
			return base.FrameHeader(new Hashtable
			{
				{
					"v",
					"1.0"
				},
				{
					"c",
					"ms-ca-data"
				},
				{
					"Extension",
					extension
				},
				{
					"phone-context",
					phoneContext
				}
			});
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x00048EE0 File Offset: 0x000470E0
		internal PlatformSipUri SerializeSACallTransferWithContextUri(string extension)
		{
			if (string.IsNullOrEmpty(extension))
			{
				throw new ArgumentNullException("extension");
			}
			return base.FrameHeader(new Hashtable
			{
				{
					"v",
					"1.0"
				},
				{
					"c",
					"ms-ova-data"
				},
				{
					"Extension",
					extension
				}
			});
		}

		// Token: 0x04000B8F RID: 2959
		private const string OvaData = "ms-ova-data";

		// Token: 0x04000B90 RID: 2960
		private const string CurrentVersion = "1.0";

		// Token: 0x04000B91 RID: 2961
		private const string CAData = "ms-ca-data";

		// Token: 0x02000233 RID: 563
		internal class DeserializedReferredByHeader
		{
			// Token: 0x06001056 RID: 4182 RVA: 0x00048F39 File Offset: 0x00047139
			internal DeserializedReferredByHeader()
			{
				this.Extension = string.Empty;
				this.PhoneContext = string.Empty;
				this.TypeOfTransferredCall = 0;
			}

			// Token: 0x170003FE RID: 1022
			// (get) Token: 0x06001057 RID: 4183 RVA: 0x00048F5E File Offset: 0x0004715E
			// (set) Token: 0x06001058 RID: 4184 RVA: 0x00048F66 File Offset: 0x00047166
			internal string Extension { get; set; }

			// Token: 0x170003FF RID: 1023
			// (get) Token: 0x06001059 RID: 4185 RVA: 0x00048F6F File Offset: 0x0004716F
			// (set) Token: 0x0600105A RID: 4186 RVA: 0x00048F77 File Offset: 0x00047177
			internal string PhoneContext { get; set; }

			// Token: 0x17000400 RID: 1024
			// (get) Token: 0x0600105B RID: 4187 RVA: 0x00048F80 File Offset: 0x00047180
			// (set) Token: 0x0600105C RID: 4188 RVA: 0x00048F88 File Offset: 0x00047188
			internal CallType TypeOfTransferredCall { get; set; }
		}
	}
}
