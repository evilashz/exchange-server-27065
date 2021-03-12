using System;

namespace Microsoft.Exchange.Audio
{
	// Token: 0x02000623 RID: 1571
	internal class Wma16Writer : WmaWriter
	{
		// Token: 0x06001C5D RID: 7261 RVA: 0x00033B6C File Offset: 0x00031D6C
		internal Wma16Writer(string outputFile, WaveFormat waveFormat) : this(outputFile, waveFormat, WmaCodec.Wma9Voice)
		{
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x00033B77 File Offset: 0x00031D77
		internal Wma16Writer(string outputFile, WaveFormat waveFormat, WmaCodec codec) : base(outputFile, waveFormat, codec)
		{
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06001C5F RID: 7263 RVA: 0x00033B82 File Offset: 0x00031D82
		protected override string Wma9VoiceProfileString
		{
			get
			{
				return "<profile version=\"589824\" storageformat=\"1\" name=\"UMWma16\" description=\"\"> <streamconfig majortype=\"{73647561-0000-0010-8000-00AA00389B71}\" streamnumber=\"1\" streamname=\"Audio Stream\" inputname=\"Audio409\" bitrate=\"16000\" bufferwindow=\"-1\" reliabletransport=\"0\" decodercomplexity=\"\" rfc1766langid=\"en-us\" > <wmmediatype subtype=\"{00000161-0000-0010-8000-00AA00389B71}\" bfixedsizesamples=\"1\" btemporalcompression=\"0\" lsamplesize=\"640\"> <waveformatex wFormatTag=\"353\" nChannels=\"1\" nSamplesPerSec=\"16000\" nAvgBytesPerSec=\"2000\" nBlockAlign=\"640\" wBitsPerSample=\"16\" codecdata=\"002200002E0080070000\"/> </wmmediatype> </streamconfig> </profile> \0";
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06001C60 RID: 7264 RVA: 0x00033B89 File Offset: 0x00031D89
		protected override string WmaPcmProfileString
		{
			get
			{
				return "<profile version=\"589824\" storageformat=\"1\" name=\"PCM\" description=\"Streams: 1 audio\"><streamconfig majortype=\"{73647561-0000-0010-8000-00AA00389B71}\" streamnumber=\"1\" streamname=\"Audio1\" inputname=\"\" bitrate=\"256000\" bufferwindow=\"0\" reliabletransport=\"0\" decodercomplexity=\"\" rfc1766langid=\"en-us\"><wmmediatype subtype=\"{00000001-0000-0010-8000-00AA00389B71}\" bfixedsizesamples=\"1\" btemporalcompression=\"0\" lsamplesize=\"2\"><waveformatex wFormatTag=\"1\" nChannels=\"1\" nSamplesPerSec=\"16000\" nAvgBytesPerSec=\"32000\" nBlockAlign=\"2\" wBitsPerSample=\"16\" codecdata=\"00000000000065FE7B37C707020030534C02\" /></wmmediatype></streamconfig></profile> \0";
			}
		}
	}
}
