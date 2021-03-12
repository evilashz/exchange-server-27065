using System;
using System.Collections;
using System.Globalization;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200013B RID: 315
	internal abstract class ReferredByHeaderHandler
	{
		// Token: 0x060008D6 RID: 2262 RVA: 0x00026660 File Offset: 0x00024860
		protected Hashtable ParseHeader(string referredBy)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "ReferredByHeaderHandler::ParseHeader() SIP header '{0}': {1}", new object[]
			{
				"Referred-By",
				referredBy
			});
			Hashtable hashtable = new Hashtable();
			Hashtable result;
			try
			{
				PlatformSignalingHeader platformSignalingHeader = Platform.Builder.CreateSignalingHeader("Referred-By", referredBy);
				PlatformSipUri platformSipUri = platformSignalingHeader.ParseUri();
				foreach (PlatformSipUriParameter platformSipUriParameter in platformSipUri.GetParametersThatHaveValues())
				{
					if (!hashtable.ContainsKey(platformSipUriParameter.Name))
					{
						hashtable.Add(platformSipUriParameter.Name.ToLowerInvariant(), platformSipUriParameter.Value);
					}
				}
				result = hashtable;
			}
			catch (ArgumentException ex)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "ReferredByHeaderHandler::ParseHeader() Invalid SIP header '{0}': {1}", new object[]
				{
					"Referred-By",
					ex
				});
				result = hashtable;
			}
			return result;
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0002675C File Offset: 0x0002495C
		protected PlatformSipUri FrameHeader(Hashtable paramsToBeAdded)
		{
			PlatformSipUri platformSipUri = Platform.Builder.CreateSipUri(string.Format(CultureInfo.InvariantCulture, "sip:{0}", new object[]
			{
				string.IsNullOrEmpty(this.referredByHostUri) ? Utils.GetOwnerHostFqdn() : this.referredByHostUri
			}));
			foreach (object obj in paramsToBeAdded.Keys)
			{
				string text = (string)obj;
				platformSipUri.AddParameter(text, (string)paramsToBeAdded[text]);
			}
			return platformSipUri;
		}

		// Token: 0x040008BA RID: 2234
		protected string referredByHostUri;
	}
}
