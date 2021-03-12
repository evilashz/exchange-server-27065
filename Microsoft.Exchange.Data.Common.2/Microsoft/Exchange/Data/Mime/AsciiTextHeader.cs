using System;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x02000024 RID: 36
	public class AsciiTextHeader : Header
	{
		// Token: 0x060001A5 RID: 421 RVA: 0x000077C7 File Offset: 0x000059C7
		public AsciiTextHeader(string name, string value) : this(name, Header.GetHeaderId(name, true))
		{
			this.Value = value;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000077DE File Offset: 0x000059DE
		internal AsciiTextHeader(string name, HeaderId headerId) : base(name, headerId)
		{
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x000077E8 File Offset: 0x000059E8
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x000077F1 File Offset: 0x000059F1
		public sealed override string Value
		{
			get
			{
				return base.GetRawValue(true);
			}
			set
			{
				base.SetRawValue(value, true, true);
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x000077FC File Offset: 0x000059FC
		public sealed override bool RequiresSMTPUTF8
		{
			get
			{
				return !MimeString.IsPureASCII(base.Lines);
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000780C File Offset: 0x00005A0C
		public sealed override MimeNode Clone()
		{
			AsciiTextHeader asciiTextHeader = new AsciiTextHeader(base.Name, base.HeaderId);
			this.CopyTo(asciiTextHeader);
			return asciiTextHeader;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00007834 File Offset: 0x00005A34
		public sealed override void CopyTo(object destination)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (destination == this)
			{
				return;
			}
			if (!(destination is AsciiTextHeader))
			{
				base.CopyTo(destination);
				return;
			}
			base.CopyTo(destination);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000786D File Offset: 0x00005A6D
		public sealed override bool IsValueValid(string value)
		{
			return true;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00007870 File Offset: 0x00005A70
		internal override MimeNode ValidateNewChild(MimeNode newChild, MimeNode refChild)
		{
			throw new NotSupportedException(Strings.AddingChildrenToAsciiTextHeader);
		}

		// Token: 0x040000DE RID: 222
		internal const bool AllowUTF8Value = true;
	}
}
