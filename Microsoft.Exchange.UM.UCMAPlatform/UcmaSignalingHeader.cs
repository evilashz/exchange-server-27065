using System;
using Microsoft.Exchange.UM.UMCore;
using Microsoft.Rtc.Signaling;

namespace Microsoft.Exchange.UM.UcmaPlatform
{
	// Token: 0x0200005C RID: 92
	internal class UcmaSignalingHeader : PlatformSignalingHeader
	{
		// Token: 0x06000400 RID: 1024 RVA: 0x00012540 File Offset: 0x00010740
		public UcmaSignalingHeader(string name, string value) : base(name, value)
		{
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0001254C File Offset: 0x0001074C
		public static UcmaSignalingHeader FromSignalingHeader(SignalingHeader h, string context)
		{
			string text = null;
			string value = null;
			UcmaSignalingHeader result;
			try
			{
				text = h.Name;
				value = h.GetValue();
				result = new UcmaSignalingHeader(text, value);
			}
			catch (MessageParsingException)
			{
				throw new InvalidSIPHeaderException(context, text, value);
			}
			return result;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00012590 File Offset: 0x00010790
		public override PlatformSipUri ParseUri()
		{
			PlatformSipUri result;
			try
			{
				SignalingHeader signalingHeader = new SignalingHeader(base.Name, base.Value);
				SignalingHeaderParser signalingHeaderParser = new SignalingHeaderParser(signalingHeader);
				result = Platform.Builder.CreateSipUri(signalingHeaderParser.Uri.ToString());
			}
			catch (MessageParsingException ex)
			{
				throw new ArgumentException(ex.Message);
			}
			return result;
		}
	}
}
