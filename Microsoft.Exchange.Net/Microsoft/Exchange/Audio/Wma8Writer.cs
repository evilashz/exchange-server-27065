using System;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x02000624 RID: 1572
	internal class Wma8Writer : WmaWriter
	{
		// Token: 0x06001C61 RID: 7265 RVA: 0x00033B90 File Offset: 0x00031D90
		internal Wma8Writer(string outputFile, WaveFormat waveFormat) : this(outputFile, waveFormat, WmaCodec.Wma9Voice)
		{
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x00033B9B File Offset: 0x00031D9B
		internal Wma8Writer(string outputFile, WaveFormat waveFormat, WmaCodec codec) : base(outputFile, waveFormat, codec)
		{
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06001C63 RID: 7267 RVA: 0x00033BA6 File Offset: 0x00031DA6
		protected override string Wma9VoiceProfileString
		{
			get
			{
				return "<profile version=\"589824\" storageformat=\"1\" name=\"UMWma\" description=\"Streams: 1 audio\"> <streamconfig majortype=\"{73647561-0000-0010-8000-00AA00389B71}\" streamnumber=\"1\" streamname=\"Audio1\" inputname=\"\" bitrate=\"8000\" bufferwindow=\"3000\" reliabletransport=\"0\" decodercomplexity=\"\" rfc1766langid=\"en-us\" > <wmmediatype subtype=\"{0000000A-0000-0010-8000-00AA00389B71}\" bfixedsizesamples=\"1\" btemporalcompression=\"1\" lsamplesize=\"3435973836\"> <waveformatex wFormatTag=\"10\" nChannels=\"1\" nSamplesPerSec=\"8000\" nAvgBytesPerSec=\"1000\" nBlockAlign=\"300\" wBitsPerSample=\"16\" codecdata=\"100004000000000000000000000000000000CF881A0005C92D499019400000000000000000000000000000000000\"/> </wmmediatype> </streamconfig> </profile> \0";
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06001C64 RID: 7268 RVA: 0x00033BAD File Offset: 0x00031DAD
		protected override string WmaPcmProfileString
		{
			get
			{
				return "<profile version=\"589824\" storageformat=\"1\" name=\"PCM\" description=\"Streams: 1 audio\"><streamconfig majortype=\"{73647561-0000-0010-8000-00AA00389B71}\" streamnumber=\"1\" streamname=\"Audio1\" inputname=\"\" bitrate=\"128000\" bufferwindow=\"0\" reliabletransport=\"0\" decodercomplexity=\"\" rfc1766langid=\"en-us\"><wmmediatype subtype=\"{00000001-0000-0010-8000-00AA00389B71}\" bfixedsizesamples=\"1\" btemporalcompression=\"0\" lsamplesize=\"2\"><waveformatex wFormatTag=\"1\" nChannels=\"1\" nSamplesPerSec=\"8000\" nAvgBytesPerSec=\"16000\" nBlockAlign=\"2\" wBitsPerSample=\"16\" codecdata=\"00000000000062FE7A31C707020E30003000\" /></wmmediatype></streamconfig></profile> \0";
			}
		}
	}
}
