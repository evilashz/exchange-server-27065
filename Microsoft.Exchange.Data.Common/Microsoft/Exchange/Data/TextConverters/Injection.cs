using System;
using System.IO;
using Microsoft.Exchange.Data.TextConverters.Internal.Rtf;
using Microsoft.Exchange.Data.TextConverters.Internal.Text;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000185 RID: 389
	internal abstract class Injection : IDisposable
	{
		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060010CB RID: 4299 RVA: 0x0007A4BF File Offset: 0x000786BF
		public HeaderFooterFormat HeaderFooterFormat
		{
			get
			{
				return this.injectionFormat;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060010CC RID: 4300 RVA: 0x0007A4C7 File Offset: 0x000786C7
		public bool HaveHead
		{
			get
			{
				return this.injectHead != null;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060010CD RID: 4301 RVA: 0x0007A4D5 File Offset: 0x000786D5
		public bool HaveTail
		{
			get
			{
				return this.injectTail != null;
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060010CE RID: 4302 RVA: 0x0007A4E3 File Offset: 0x000786E3
		public bool HeadDone
		{
			get
			{
				return this.headInjected;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060010CF RID: 4303 RVA: 0x0007A4EB File Offset: 0x000786EB
		public bool TailDone
		{
			get
			{
				return this.tailInjected;
			}
		}

		// Token: 0x060010D0 RID: 4304
		public abstract void Inject(bool head, TextOutput output);

		// Token: 0x060010D1 RID: 4305 RVA: 0x0007A4F3 File Offset: 0x000786F3
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x0007A502 File Offset: 0x00078702
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x0007A504 File Offset: 0x00078704
		public virtual void Reset()
		{
			this.headInjected = false;
			this.tailInjected = false;
		}

		// Token: 0x060010D4 RID: 4308
		public abstract void CompileForRtf(RtfOutput output);

		// Token: 0x060010D5 RID: 4309
		public abstract void InjectRtf(bool head, bool immediatelyAfterText);

		// Token: 0x060010D6 RID: 4310
		public abstract void InjectRtfFonts(int firstAvailableFontHandle);

		// Token: 0x060010D7 RID: 4311
		public abstract void InjectRtfColors(int nextColorIndex);

		// Token: 0x04001166 RID: 4454
		protected HeaderFooterFormat injectionFormat;

		// Token: 0x04001167 RID: 4455
		protected string injectHead;

		// Token: 0x04001168 RID: 4456
		protected string injectTail;

		// Token: 0x04001169 RID: 4457
		protected bool headInjected;

		// Token: 0x0400116A RID: 4458
		protected bool tailInjected;

		// Token: 0x0400116B RID: 4459
		protected bool testBoundaryConditions;

		// Token: 0x0400116C RID: 4460
		protected Stream traceStream;
	}
}
