using System;
using System.Globalization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002F3 RID: 755
	internal interface IPlatformBuilder
	{
		// Token: 0x06001705 RID: 5893
		BaseUMVoipPlatform CreateVoipPlatform();

		// Token: 0x06001706 RID: 5894
		BaseCallRouterPlatform CreateCallRouterVoipPlatform(LocalizedString serviceName, LocalizedString serverName, UMADSettings config);

		// Token: 0x06001707 RID: 5895
		PlatformSipUri CreateSipUri(string uri);

		// Token: 0x06001708 RID: 5896
		bool TryCreateSipUri(string uriString, out PlatformSipUri sipUri);

		// Token: 0x06001709 RID: 5897
		PlatformSipUri CreateSipUri(SipUriScheme scheme, string user, string host);

		// Token: 0x0600170A RID: 5898
		PlatformSignalingHeader CreateSignalingHeader(string name, string value);

		// Token: 0x0600170B RID: 5899
		bool TryCreateOfflineTranscriber(CultureInfo transcriptionLanguage, out BaseUMOfflineTranscriber transcriber);

		// Token: 0x0600170C RID: 5900
		bool TryCreateMobileRecognizer(Guid requestId, CultureInfo culture, SpeechRecognitionEngineType engineType, int maxAlternates, out IMobileRecognizer recognizer);
	}
}
