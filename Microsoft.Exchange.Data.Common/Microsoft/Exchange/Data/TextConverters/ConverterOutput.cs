using System;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x0200017A RID: 378
	internal abstract class ConverterOutput : ITextSink, IDisposable
	{
		// Token: 0x06001031 RID: 4145 RVA: 0x0007741E File Offset: 0x0007561E
		public ConverterOutput()
		{
			this.stringBuffer = new char[128];
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001032 RID: 4146
		public abstract bool CanAcceptMore { get; }

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001033 RID: 4147 RVA: 0x00077436 File Offset: 0x00075636
		bool ITextSink.IsEnough
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001034 RID: 4148 RVA: 0x00077439 File Offset: 0x00075639
		// (set) Token: 0x06001035 RID: 4149 RVA: 0x00077441 File Offset: 0x00075641
		internal IReportBytes ReportBytes
		{
			get
			{
				return this.reportBytes;
			}
			set
			{
				this.reportBytes = value;
			}
		}

		// Token: 0x06001036 RID: 4150
		public abstract void Write(char[] buffer, int offset, int count, IFallback fallback);

		// Token: 0x06001037 RID: 4151
		public abstract void Flush();

		// Token: 0x06001038 RID: 4152 RVA: 0x0007744A File Offset: 0x0007564A
		public void Write(char[] buffer, int offset, int count)
		{
			this.Write(buffer, offset, count, null);
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x00077456 File Offset: 0x00075656
		public virtual void Write(string text)
		{
			this.Write(text, 0, text.Length, null);
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x00077467 File Offset: 0x00075667
		public void Write(string text, IFallback fallback)
		{
			this.Write(text, 0, text.Length, fallback);
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x00077478 File Offset: 0x00075678
		public void Write(string text, int offset, int count)
		{
			this.Write(text, offset, count, null);
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x00077484 File Offset: 0x00075684
		public void Write(string text, int offset, int count, IFallback fallback)
		{
			if (this.stringBuffer.Length < count)
			{
				this.stringBuffer = new char[count * 2];
			}
			text.CopyTo(offset, this.stringBuffer, 0, count);
			this.Write(this.stringBuffer, 0, count, fallback);
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x000774BE File Offset: 0x000756BE
		public void Write(char ch)
		{
			this.Write(ch, null);
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x000774C8 File Offset: 0x000756C8
		public void Write(char ch, IFallback fallback)
		{
			this.stringBuffer[0] = ch;
			this.Write(this.stringBuffer, 0, 1, fallback);
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x000774E2 File Offset: 0x000756E2
		public void Write(int ucs32Literal)
		{
			this.Write(ucs32Literal, null);
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x000774EC File Offset: 0x000756EC
		public void Write(int ucs32Literal, IFallback fallback)
		{
			int count = 1;
			if (ucs32Literal > 65535)
			{
				if (fallback != null && fallback is HtmlWriter)
				{
					uint num = (uint)ucs32Literal;
					int num2 = (num < 10U) ? 1 : ((num < 100U) ? 2 : ((num < 1000U) ? 3 : ((num < 10000U) ? 4 : ((num < 100000U) ? 5 : ((num < 1000000U) ? 6 : 7)))));
					int num3 = 2 + num2;
					this.stringBuffer[0] = '&';
					this.stringBuffer[1] = '#';
					this.stringBuffer[num3] = ';';
					while (num != 0U)
					{
						uint num4 = num % 10U;
						this.stringBuffer[--num3] = (char)(num4 + 48U);
						num /= 10U;
					}
					count = 3 + num2;
					this.Write(this.stringBuffer, 0, count, null);
					return;
				}
				this.stringBuffer[0] = ParseSupport.HighSurrogateCharFromUcs4(ucs32Literal);
				this.stringBuffer[1] = ParseSupport.LowSurrogateCharFromUcs4(ucs32Literal);
				count = 2;
			}
			else
			{
				this.stringBuffer[0] = (char)ucs32Literal;
			}
			this.Write(this.stringBuffer, 0, count, fallback);
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x000775E8 File Offset: 0x000757E8
		public ITextSink PrepareSink(IFallback fallback)
		{
			this.fallback = fallback;
			return this;
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x000775F2 File Offset: 0x000757F2
		void ITextSink.Write(char[] buffer, int offset, int count)
		{
			this.Write(buffer, offset, count, this.fallback);
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x00077603 File Offset: 0x00075803
		void ITextSink.Write(int ucs32Literal)
		{
			this.Write(ucs32Literal, this.fallback);
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x00077612 File Offset: 0x00075812
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x00077621 File Offset: 0x00075821
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x040010E8 RID: 4328
		private const int StringBufferMax = 128;

		// Token: 0x040010E9 RID: 4329
		protected char[] stringBuffer;

		// Token: 0x040010EA RID: 4330
		protected IReportBytes reportBytes;

		// Token: 0x040010EB RID: 4331
		private IFallback fallback;
	}
}
