using System;
using System.IO;
using Microsoft.Exchange.Data.Transport.Email;

namespace Microsoft.Exchange.Data.Mime.Internal
{
	// Token: 0x02000057 RID: 87
	public static class MimeAppleTranscoder
	{
		// Token: 0x06000312 RID: 786 RVA: 0x0001111A File Offset: 0x0000F31A
		public static Stream ExtractDataFork(Stream macBinStream)
		{
			return MimeAppleTranscoder.ExtractDataFork(macBinStream);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00011122 File Offset: 0x0000F322
		public static bool IsMacBinStream(Stream stream)
		{
			return MimeAppleTranscoder.IsMacBinStream(stream);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0001112A File Offset: 0x0000F32A
		public static void WriteWholeApplefile(Stream dataForkStream, Stream outStream)
		{
			MimeAppleTranscoder.WriteWholeApplefile(dataForkStream, outStream);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00011133 File Offset: 0x0000F333
		public static void WriteWholeApplefile(Stream applefileStream, Stream dataForkStream, Stream outStream)
		{
			MimeAppleTranscoder.WriteWholeApplefile(applefileStream, dataForkStream, outStream);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0001113D File Offset: 0x0000F33D
		public static void MacBinToApplefile(Stream macBinStream, Stream outStream, out string fileName, out byte[] additionalInfo)
		{
			MimeAppleTranscoder.MacBinToApplefile(macBinStream, outStream, out fileName, out additionalInfo);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00011148 File Offset: 0x0000F348
		public static void ApplesingleToMacBin(Stream applesingleStream, Stream outAttachMacInfo, Stream outMacBinStream, out string fileName, out byte[] additionalInfo)
		{
			MimeAppleTranscoder.ApplesingleToMacBin(applesingleStream, outAttachMacInfo, outMacBinStream, out fileName, out additionalInfo);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00011155 File Offset: 0x0000F355
		public static void AppledoubleToMacBin(Stream resourceForkStream, Stream dataForkStream, Stream outMacBinStream, out string fileName, out byte[] additionalInfo)
		{
			MimeAppleTranscoder.AppledoubleToMacBin(resourceForkStream, dataForkStream, outMacBinStream, out fileName, out additionalInfo);
		}
	}
}
