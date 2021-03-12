using System;
using System.IO;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Rtf
{
	// Token: 0x02000238 RID: 568
	internal class RtfTextExtractionConverter : IProducerConsumer, IDisposable
	{
		// Token: 0x06001764 RID: 5988 RVA: 0x000B648E File Offset: 0x000B468E
		public RtfTextExtractionConverter(RtfParser parser, ConverterOutput output, Stream traceStream, bool traceShowTokenNum, int traceStopOnTokenNum)
		{
			this.output = output;
			this.parser = parser;
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x000B64B0 File Offset: 0x000B46B0
		void IDisposable.Dispose()
		{
			if (this.parser != null && this.parser is IDisposable)
			{
				((IDisposable)this.parser).Dispose();
			}
			if (this.output != null && this.output != null)
			{
				((IDisposable)this.output).Dispose();
			}
			this.parser = null;
			this.output = null;
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x000B6514 File Offset: 0x000B4714
		public void Run()
		{
			if (!this.endOfFile)
			{
				RtfTokenId rtfTokenId = this.parser.Parse();
				if (rtfTokenId != RtfTokenId.None)
				{
					this.Process(rtfTokenId);
				}
			}
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x000B653F File Offset: 0x000B473F
		public bool Flush()
		{
			if (!this.endOfFile)
			{
				this.Run();
			}
			return this.endOfFile;
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x000B6558 File Offset: 0x000B4758
		private void Process(RtfTokenId tokenId)
		{
			switch (tokenId)
			{
			case RtfTokenId.EndOfFile:
				this.ProcessEOF();
				break;
			case RtfTokenId.Begin:
				this.ProcessBeginGroup();
				return;
			case RtfTokenId.End:
				this.ProcessEndGroup();
				return;
			case RtfTokenId.Binary:
			case (RtfTokenId)6:
				break;
			case RtfTokenId.Keywords:
				this.ProcessKeywords(this.parser.Token);
				return;
			case RtfTokenId.Text:
				this.ProcessText(this.parser.Token);
				return;
			default:
				return;
			}
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x000B65C4 File Offset: 0x000B47C4
		private void ProcessBeginGroup()
		{
			this.level++;
			this.firstKeyword = true;
			this.ignorableDestination = false;
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x000B65E4 File Offset: 0x000B47E4
		private void ProcessEndGroup()
		{
			if (this.skipLevel != 0)
			{
				if (this.level != this.skipLevel)
				{
					this.level--;
					return;
				}
				this.skipLevel = 0;
			}
			this.firstKeyword = false;
			if (this.level > 1)
			{
				this.level--;
				if (this.invisibleLevel != 2147483647 && this.level < this.invisibleLevel)
				{
					this.invisibleLevel = int.MaxValue;
				}
			}
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x000B6664 File Offset: 0x000B4864
		private void ProcessKeywords(RtfToken token)
		{
			if (this.skipLevel != 0 && this.level >= this.skipLevel)
			{
				return;
			}
			foreach (RtfKeyword rtfKeyword in token.Keywords)
			{
				if (this.firstKeyword)
				{
					if (rtfKeyword.Id == 1)
					{
						this.ignorableDestination = true;
						continue;
					}
					this.firstKeyword = false;
					short id = rtfKeyword.Id;
					if (id <= 201)
					{
						if (id <= 24)
						{
							if (id != 8 && id != 15 && id != 24)
							{
								goto IL_118;
							}
						}
						else if (id != 50 && id != 175 && id != 201)
						{
							goto IL_118;
						}
					}
					else if (id <= 252)
					{
						if (id != 210 && id != 246 && id != 252)
						{
							goto IL_118;
						}
					}
					else
					{
						switch (id)
						{
						case 255:
						case 257:
							break;
						case 256:
							goto IL_118;
						default:
							if (id != 268)
							{
								switch (id)
								{
								case 315:
								case 316:
								case 319:
									break;
								case 317:
								case 318:
									goto IL_118;
								default:
									goto IL_118;
								}
							}
							break;
						}
					}
					this.skipLevel = this.level;
					break;
					IL_118:
					if (this.ignorableDestination)
					{
						this.skipLevel = this.level;
						break;
					}
				}
				short id2 = rtfKeyword.Id;
				if (id2 <= 68)
				{
					if (id2 == 40 || id2 == 48 || id2 == 68)
					{
						this.output.Write("\r\n");
					}
				}
				else if (id2 != 119 && id2 != 126)
				{
					if (id2 == 136)
					{
						this.invisibleLevel = ((rtfKeyword.Value != 0) ? this.level : int.MaxValue);
					}
				}
				else
				{
					this.output.Write("\t");
				}
			}
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x000B6828 File Offset: 0x000B4A28
		private void ProcessText(RtfToken token)
		{
			if (this.skipLevel != 0 && this.level >= this.skipLevel)
			{
				return;
			}
			this.firstKeyword = false;
			if (this.level < this.invisibleLevel)
			{
				token.Text.WriteTo(this.output);
			}
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x000B6876 File Offset: 0x000B4A76
		private void ProcessEOF()
		{
			this.output.Write("\r\n");
			this.output.Flush();
			this.endOfFile = true;
		}

		// Token: 0x04001AAA RID: 6826
		private RtfParser parser;

		// Token: 0x04001AAB RID: 6827
		private bool endOfFile;

		// Token: 0x04001AAC RID: 6828
		private ConverterOutput output;

		// Token: 0x04001AAD RID: 6829
		private bool firstKeyword;

		// Token: 0x04001AAE RID: 6830
		private bool ignorableDestination;

		// Token: 0x04001AAF RID: 6831
		private int level;

		// Token: 0x04001AB0 RID: 6832
		private int skipLevel;

		// Token: 0x04001AB1 RID: 6833
		private int invisibleLevel = int.MaxValue;
	}
}
