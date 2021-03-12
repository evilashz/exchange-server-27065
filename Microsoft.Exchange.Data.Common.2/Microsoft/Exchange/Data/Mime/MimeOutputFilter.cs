using System;
using System.IO;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x02000034 RID: 52
	public abstract class MimeOutputFilter
	{
		// Token: 0x0600023B RID: 571 RVA: 0x00009EC8 File Offset: 0x000080C8
		public virtual bool FilterPart(MimePart part, Stream stream)
		{
			return false;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00009ECB File Offset: 0x000080CB
		public virtual bool FilterHeaderList(HeaderList headerList, Stream stream)
		{
			return false;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00009ECE File Offset: 0x000080CE
		public virtual bool FilterHeader(Header header, Stream stream)
		{
			return false;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00009ED1 File Offset: 0x000080D1
		public virtual bool FilterPartBody(MimePart part, Stream stream)
		{
			return false;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00009ED4 File Offset: 0x000080D4
		public virtual void ClosePart(MimePart part, Stream stream)
		{
		}
	}
}
