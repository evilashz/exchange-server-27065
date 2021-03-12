using System;
using System.IO;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Mime;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x020000E6 RID: 230
	internal interface IBody
	{
		// Token: 0x06000575 RID: 1397
		BodyFormat GetBodyFormat();

		// Token: 0x06000576 RID: 1398
		string GetCharsetName();

		// Token: 0x06000577 RID: 1399
		MimePart GetMimePart();

		// Token: 0x06000578 RID: 1400
		Stream GetContentReadStream();

		// Token: 0x06000579 RID: 1401
		bool TryGetContentReadStream(out Stream stream);

		// Token: 0x0600057A RID: 1402
		Stream GetContentWriteStream(Charset charset);

		// Token: 0x0600057B RID: 1403
		void SetNewContent(DataStorage storage, long start, long end);

		// Token: 0x0600057C RID: 1404
		bool ConversionNeeded(int[] validCodepages);
	}
}
