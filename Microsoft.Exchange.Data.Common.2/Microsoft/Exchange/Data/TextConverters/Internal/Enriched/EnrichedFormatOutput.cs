using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.TextConverters.Internal.Format;
using Microsoft.Exchange.Data.TextConverters.Internal.Html;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Enriched
{
	// Token: 0x020001C0 RID: 448
	internal class EnrichedFormatOutput : FormatOutput, IRestartable, IFallback
	{
		// Token: 0x0600135D RID: 4957 RVA: 0x00087A71 File Offset: 0x00085C71
		public EnrichedFormatOutput(ConverterOutput output, Injection injection, bool fallbacks, Stream formatTraceStream, Stream formatOutputTraceStream) : base(formatOutputTraceStream)
		{
			this.output = output;
			this.injection = injection;
			this.fallbacks = fallbacks;
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x0600135E RID: 4958 RVA: 0x00087A97 File Offset: 0x00085C97
		public override bool OutputCodePageSameAsInput
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (set) Token: 0x0600135F RID: 4959 RVA: 0x00087A9A File Offset: 0x00085C9A
		public override Encoding OutputEncoding
		{
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001360 RID: 4960 RVA: 0x00087AA1 File Offset: 0x00085CA1
		public override bool CanAcceptMoreOutput
		{
			get
			{
				return this.output.CanAcceptMore;
			}
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x00087AAE File Offset: 0x00085CAE
		bool IRestartable.CanRestart()
		{
			return this.output is IRestartable && ((IRestartable)this.output).CanRestart();
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x00087AD0 File Offset: 0x00085CD0
		void IRestartable.Restart()
		{
			((IRestartable)this.output).Restart();
			base.Restart();
			this.blockEmpty = true;
			this.blockEnd = false;
			this.lineLength = 0;
			this.insideNofill = 0;
			this.listLevel = 0;
			this.listIndex = 0;
			this.spaceBefore = 0;
			if (this.injection != null)
			{
				this.injection.Reset();
			}
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x00087B37 File Offset: 0x00085D37
		void IRestartable.DisableRestart()
		{
			if (this.output is IRestartable)
			{
				((IRestartable)this.output).DisableRestart();
			}
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x00087B56 File Offset: 0x00085D56
		public override bool Flush()
		{
			if (!base.Flush())
			{
				return false;
			}
			this.output.Flush();
			return true;
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x00087B6E File Offset: 0x00085D6E
		byte[] IFallback.GetUnsafeAsciiMap(out byte unsafeAsciiMask)
		{
			unsafeAsciiMask = 1;
			return HtmlSupport.UnsafeAsciiMap;
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x00087B78 File Offset: 0x00085D78
		bool IFallback.HasUnsafeUnicode()
		{
			return false;
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x00087B7B File Offset: 0x00085D7B
		bool IFallback.TreatNonAsciiAsUnsafe(string charset)
		{
			return false;
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x00087B7E File Offset: 0x00085D7E
		bool IFallback.IsUnsafeUnicode(char ch, bool isFirstChar)
		{
			return false;
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x00087B84 File Offset: 0x00085D84
		bool IFallback.FallBackChar(char ch, char[] outputBuffer, ref int outputBufferCount, int outputEnd)
		{
			string text = null;
			if (ch == '<')
			{
				text = "<<";
			}
			else if (this.fallbacks)
			{
				text = this.GetSubstitute(ch);
			}
			if (text != null)
			{
				if (outputEnd - outputBufferCount < text.Length)
				{
					return false;
				}
				text.CopyTo(0, outputBuffer, outputBufferCount, text.Length);
				outputBufferCount += text.Length;
			}
			else
			{
				outputBuffer[outputBufferCount++] = ch;
			}
			return true;
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x00087BEC File Offset: 0x00085DEC
		protected override bool StartDocument()
		{
			if (this.injection != null)
			{
				bool haveHead = this.injection.HaveHead;
			}
			this.ApplyCharFormat();
			return true;
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x00087C0C File Offset: 0x00085E0C
		protected override void EndDocument()
		{
			this.RevertCharFormat();
			if (this.lineLength != 0)
			{
				this.output.Write("\r\n");
				if (!this.blockEnd)
				{
					this.output.Write("\r\n");
				}
			}
			if (this.injection != null)
			{
				bool haveTail = this.injection.HaveTail;
			}
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x00087C64 File Offset: 0x00085E64
		protected override bool StartBlockContainer()
		{
			if (!this.blockEmpty)
			{
				if (this.lineLength != 0 && this.insideNofill == 0)
				{
					this.output.Write("\r\n");
				}
				this.output.Write("\r\n");
				this.lineLength = 0;
				this.blockEmpty = true;
			}
			this.blockEnd = false;
			PropertyValue effectiveProperty = base.GetEffectiveProperty(PropertyId.Margins);
			if (!effectiveProperty.IsNull && effectiveProperty.IsAbsLength)
			{
				this.spaceBefore = Math.Max(this.spaceBefore, effectiveProperty.PointsInteger);
			}
			PropertyValue effectiveProperty2 = base.GetEffectiveProperty(PropertyId.Preformatted);
			if (!effectiveProperty2.IsNull && effectiveProperty2.Bool)
			{
				this.output.Write("<Nofill>");
				this.lineLength = "<Nofill>".Length;
				this.insideNofill++;
			}
			else
			{
				StringBuilder stringBuilder = null;
				PropertyValue effectiveProperty3 = base.GetEffectiveProperty(PropertyId.QuotingLevelDelta);
				if (!effectiveProperty3.IsNull && effectiveProperty3.IsInteger && effectiveProperty3.Integer > 0)
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder();
					}
					for (int i = 0; i < effectiveProperty3.Integer; i++)
					{
						stringBuilder.Append("<Excerpt>");
					}
				}
				PropertyValue effectiveProperty4 = base.GetEffectiveProperty(PropertyId.RightToLeft);
				bool flag = effectiveProperty4.IsNull || !effectiveProperty4.Bool;
				PropertyValue propertyValue = flag ? base.GetEffectiveProperty(PropertyId.LeftPadding) : base.GetEffectiveProperty(PropertyId.RightPadding);
				PropertyValue propertyValue2 = flag ? base.GetEffectiveProperty(PropertyId.RightPadding) : base.GetEffectiveProperty(PropertyId.LeftPadding);
				PropertyValue effectiveProperty5 = base.GetEffectiveProperty(PropertyId.FirstLineIndent);
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				if (!propertyValue.IsNull && propertyValue.IsAbsLength)
				{
					num = (propertyValue.PointsInteger + 12) / 30;
					num = EnrichedFormatOutput.CheckRange(0, num, 50);
				}
				if (!effectiveProperty5.IsNull && effectiveProperty5.IsAbsLength)
				{
					num3 = (effectiveProperty5.PointsInteger + ((effectiveProperty5.PointsInteger > 0) ? 12 : -12)) / 30;
					num3 = EnrichedFormatOutput.CheckRange(-50, num3, 50);
				}
				if (!propertyValue2.IsNull && propertyValue2.IsAbsLength)
				{
					num2 = propertyValue2.PointsInteger / 30;
					num2 = EnrichedFormatOutput.CheckRange(0, num2, 50);
				}
				if (num != 0 || num2 != 0 || num3 != 0)
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder();
					}
					int num4 = 0;
					if (num3 < 0)
					{
						num4 = -num3;
						num3 = 0;
						num -= num4;
						if (num < 0)
						{
							num = 0;
						}
					}
					stringBuilder.Append("<ParaIndent><Param>");
					bool flag2 = false;
					while (num-- != 0)
					{
						if (flag2)
						{
							stringBuilder.Append(',');
						}
						stringBuilder.Append("Left");
						flag2 = true;
					}
					while (num2-- != 0)
					{
						if (flag2)
						{
							stringBuilder.Append(',');
						}
						stringBuilder.Append("Right");
						flag2 = true;
					}
					while (num3-- != 0)
					{
						if (flag2)
						{
							stringBuilder.Append(',');
						}
						stringBuilder.Append("In");
						flag2 = true;
					}
					while (num4-- != 0)
					{
						if (flag2)
						{
							stringBuilder.Append(',');
						}
						stringBuilder.Append("Out");
						flag2 = true;
					}
					stringBuilder.Append("</Param>");
				}
				PropertyValue effectiveProperty6 = base.GetEffectiveProperty(PropertyId.TextAlignment);
				if (!effectiveProperty6.IsNull)
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder();
					}
					switch (effectiveProperty6.Enum)
					{
					case 1:
						stringBuilder.Append("<Center>");
						break;
					case 3:
						stringBuilder.Append("<FlushLeft>");
						break;
					case 4:
						stringBuilder.Append("<FlushRight>");
						break;
					case 6:
						stringBuilder.Append("<FlushBoth>");
						break;
					}
				}
				if (stringBuilder != null && stringBuilder.Length != 0)
				{
					this.lineLength += stringBuilder.Length;
					this.output.Write(stringBuilder.ToString());
				}
			}
			return true;
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x0008802C File Offset: 0x0008622C
		protected override void EndBlockContainer()
		{
			PropertyValue effectiveProperty = base.GetEffectiveProperty(PropertyId.Preformatted);
			if (!effectiveProperty.IsNull && effectiveProperty.Bool)
			{
				this.insideNofill--;
				this.output.Write("</Nofill>");
				this.lineLength += "</Nofill>".Length;
			}
			else
			{
				StringBuilder stringBuilder = null;
				PropertyValue effectiveProperty2 = base.GetEffectiveProperty(PropertyId.TextAlignment);
				if (!effectiveProperty2.IsNull)
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder();
					}
					switch (effectiveProperty2.Enum)
					{
					case 1:
						stringBuilder.Append("</Center>");
						break;
					case 3:
						stringBuilder.Append("</FlushLeft>");
						break;
					case 4:
						stringBuilder.Append("</FlushRight>");
						break;
					case 6:
						stringBuilder.Append("</FlushBoth>");
						break;
					}
				}
				PropertyValue effectiveProperty3 = base.GetEffectiveProperty(PropertyId.LeftPadding);
				PropertyValue effectiveProperty4 = base.GetEffectiveProperty(PropertyId.RightPadding);
				PropertyValue effectiveProperty5 = base.GetEffectiveProperty(PropertyId.FirstLineIndent);
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				if (!effectiveProperty3.IsNull && effectiveProperty3.IsAbsLength)
				{
					num = (effectiveProperty3.PointsInteger + 12) / 30;
					num = EnrichedFormatOutput.CheckRange(0, num, 50);
				}
				if (!effectiveProperty5.IsNull && effectiveProperty5.IsAbsLength)
				{
					num3 = (effectiveProperty5.PointsInteger + ((effectiveProperty5.PointsInteger > 0) ? 12 : -12)) / 30;
					num3 = EnrichedFormatOutput.CheckRange(-50, num3, 50);
				}
				if (!effectiveProperty4.IsNull && effectiveProperty4.IsAbsLength)
				{
					num2 = effectiveProperty4.PointsInteger / 30;
					num2 = EnrichedFormatOutput.CheckRange(0, num2, 50);
				}
				if (num != 0 || num2 != 0 || num3 != 0)
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder();
					}
					stringBuilder.Append("</ParaIndent>");
				}
				PropertyValue effectiveProperty6 = base.GetEffectiveProperty(PropertyId.QuotingLevelDelta);
				if (!effectiveProperty6.IsNull && effectiveProperty6.IsInteger && effectiveProperty6.Integer > 0)
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder();
					}
					for (int i = 0; i < effectiveProperty6.Integer; i++)
					{
						stringBuilder.Append("</Excerpt>");
					}
				}
				if (stringBuilder != null && stringBuilder.Length != 0)
				{
					this.lineLength += stringBuilder.Length;
					this.output.Write(stringBuilder.ToString());
				}
			}
			this.blockEnd = true;
			PropertyValue effectiveProperty7 = base.GetEffectiveProperty(PropertyId.BottomMargin);
			if (!effectiveProperty7.IsNull && effectiveProperty7.IsAbsLength)
			{
				this.spaceBefore = Math.Max(this.spaceBefore, effectiveProperty7.PointsInteger);
			}
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x000882A8 File Offset: 0x000864A8
		protected override bool StartTable()
		{
			if (!this.blockEmpty)
			{
				if (this.lineLength != 0 && this.insideNofill == 0)
				{
					this.output.Write("\r\n");
				}
				this.output.Write("\r\n");
				this.lineLength = 0;
				this.blockEmpty = true;
			}
			this.blockEnd = false;
			return true;
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x00088303 File Offset: 0x00086503
		protected override void EndTable()
		{
			this.blockEnd = true;
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x0008830C File Offset: 0x0008650C
		protected override bool StartTableCaption()
		{
			if (!this.blockEmpty)
			{
				if (this.lineLength != 0 && this.insideNofill == 0)
				{
					this.output.Write("\r\n");
				}
				this.output.Write("\r\n");
				this.lineLength = 0;
				this.blockEmpty = true;
			}
			this.blockEnd = false;
			this.ApplyCharFormat();
			return true;
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x0008836D File Offset: 0x0008656D
		protected override void EndTableCaption()
		{
			this.RevertCharFormat();
			this.blockEnd = true;
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x0008837C File Offset: 0x0008657C
		protected override bool StartTableExtraContent()
		{
			if (!this.blockEmpty)
			{
				if (this.lineLength != 0 && this.insideNofill == 0)
				{
					this.output.Write("\r\n");
				}
				this.output.Write("\r\n");
				this.lineLength = 0;
				this.blockEmpty = true;
			}
			this.blockEnd = false;
			this.ApplyCharFormat();
			return true;
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x000883DD File Offset: 0x000865DD
		protected override void EndTableExtraContent()
		{
			this.RevertCharFormat();
			this.blockEnd = true;
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x000883EC File Offset: 0x000865EC
		protected override bool StartTableRow()
		{
			if (!this.blockEmpty)
			{
				if (this.lineLength != 0 && this.insideNofill == 0)
				{
					this.output.Write("\r\n");
				}
				this.output.Write("\r\n");
				this.lineLength = 0;
				this.blockEmpty = true;
			}
			this.blockEnd = false;
			return true;
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x00088447 File Offset: 0x00086647
		protected override void EndTableRow()
		{
			this.blockEnd = true;
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x00088450 File Offset: 0x00086650
		protected override bool StartTableCell()
		{
			this.ApplyCharFormat();
			return true;
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x0008845C File Offset: 0x0008665C
		protected override void EndTableCell()
		{
			this.RevertCharFormat();
			if (!base.CurrentNode.NextSibling.IsNull)
			{
				this.output.Write("\t");
			}
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x00088498 File Offset: 0x00086698
		protected override bool StartList()
		{
			this.listLevel++;
			this.StartBlockContainer();
			if (this.listLevel == 1)
			{
				PropertyValue property = base.CurrentNode.Parent.GetProperty(PropertyId.ListStart);
				if (!property.IsNull)
				{
					this.listIndex = property.Integer;
				}
				else
				{
					this.listIndex = 1;
				}
			}
			PropertyValue effectiveProperty = base.GetEffectiveProperty(PropertyId.RightToLeft);
			bool flag = effectiveProperty.IsNull || !effectiveProperty.Bool;
			int num = 0;
			PropertyValue propertyValue = flag ? base.CurrentNode.Parent.GetProperty(PropertyId.LeftMargin) : base.CurrentNode.Parent.GetProperty(PropertyId.RightMargin);
			if (!propertyValue.IsNull && propertyValue.IsAbsLength)
			{
				num = propertyValue.PointsInteger / 30;
				num = EnrichedFormatOutput.CheckRange(0, num, 50);
			}
			num++;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<ParaIndent><Param>");
			bool flag2 = false;
			while (num-- != 0)
			{
				if (flag2)
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append("Left");
				flag2 = true;
			}
			stringBuilder.Append("</Param>");
			this.lineLength += stringBuilder.Length;
			this.output.Write(stringBuilder.ToString());
			this.ApplyCharFormat();
			return true;
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x00088600 File Offset: 0x00086800
		protected override void EndList()
		{
			this.RevertCharFormat();
			this.output.Write("</ParaIndent>");
			this.lineLength += "</Paraindent>".Length;
			this.EndBlockContainer();
			this.listLevel--;
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x00088650 File Offset: 0x00086850
		protected override bool StartListItem()
		{
			this.StartBlockContainer();
			this.ApplyCharFormat();
			PropertyValue effectiveProperty = base.GetEffectiveProperty(PropertyId.ListStyle);
			if (effectiveProperty.IsNull || effectiveProperty.Enum == 1 || this.listLevel > 1)
			{
				this.output.Write("*   ");
				this.lineLength += 2;
			}
			else if (effectiveProperty.Enum != 2)
			{
				this.output.Write("*   ");
				this.lineLength += 2;
			}
			else
			{
				string text = this.listIndex.ToString();
				this.output.Write(text);
				this.output.Write(". ");
				if (text.Length == 1)
				{
					this.output.Write(' ');
				}
				this.listIndex++;
				this.lineLength += text.Length + ((text.Length == 1) ? 3 : 2);
			}
			this.blockEmpty = false;
			return true;
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x00088751 File Offset: 0x00086951
		protected override void EndListItem()
		{
			this.RevertCharFormat();
			this.EndBlockContainer();
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x0008875F File Offset: 0x0008695F
		protected override bool StartHyperLink()
		{
			this.ApplyCharFormat();
			return true;
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x00088768 File Offset: 0x00086968
		protected override void EndHyperLink()
		{
			this.RevertCharFormat();
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x00088770 File Offset: 0x00086970
		protected override void StartEndImage()
		{
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x00088774 File Offset: 0x00086974
		protected override void StartEndHorizontalLine()
		{
			if (!this.blockEmpty)
			{
				if (this.lineLength != 0 && this.insideNofill == 0)
				{
					this.output.Write("\r\n");
				}
				this.output.Write("\r\n");
			}
			this.output.Write("________________________________\r\n\r\n");
			this.lineLength = 0;
			this.blockEmpty = true;
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x000887D7 File Offset: 0x000869D7
		protected override bool StartInlineContainer()
		{
			this.ApplyCharFormat();
			return true;
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x000887E0 File Offset: 0x000869E0
		protected override void EndInlineContainer()
		{
			this.RevertCharFormat();
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x000887E8 File Offset: 0x000869E8
		protected override void StartEndArea()
		{
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x000887EA File Offset: 0x000869EA
		protected override bool StartOption()
		{
			return true;
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x000887F0 File Offset: 0x000869F0
		protected override bool StartText()
		{
			if (this.blockEnd)
			{
				if (this.lineLength != 0 && this.insideNofill == 0)
				{
					this.output.Write("\r\n");
					this.lineLength = 0;
				}
				this.output.Write("\r\n");
				this.blockEnd = false;
			}
			this.blockEmpty = false;
			this.ApplyCharFormat();
			return true;
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x00088854 File Offset: 0x00086A54
		protected override bool ContinueText(uint beginTextPosition, uint endTextPosition)
		{
			if (beginTextPosition != endTextPosition)
			{
				TextRun textRun = base.FormatStore.GetTextRun(beginTextPosition);
				do
				{
					int num = textRun.EffectiveLength;
					TextRunType type = textRun.Type;
					if (type <= TextRunType.NbSp)
					{
						if (type != TextRunType.NonSpace)
						{
							if (type == TextRunType.NbSp)
							{
								this.lineLength += num;
								while (num-- != 0)
								{
									this.output.Write(' ');
								}
							}
						}
						else
						{
							this.lineLength += num;
							int num2 = 0;
							do
							{
								char[] buffer;
								int offset;
								int num3;
								textRun.GetChunk(num2, out buffer, out offset, out num3);
								this.output.Write(buffer, offset, num3, this);
								num2 += num3;
							}
							while (num2 != num);
						}
					}
					else if (type != TextRunType.Space)
					{
						if (type != TextRunType.Tabulation)
						{
							if (type == TextRunType.NewLine)
							{
								if (this.insideNofill != 0)
								{
									while (num-- != 0)
									{
										this.output.Write("\r\n");
									}
								}
								else
								{
									if (this.lineLength != 0)
									{
										this.output.Write("\r\n");
									}
									while (num-- != 0)
									{
										this.output.Write("\r\n");
									}
								}
								this.lineLength = 0;
								this.blockEmpty = true;
							}
						}
						else
						{
							while (num-- != 0)
							{
								this.output.Write('\t');
								this.lineLength = (this.lineLength + 8) / 8 * 8;
							}
						}
					}
					else
					{
						if (this.lineLength + num > 80 && this.lineLength != 0 && this.insideNofill == 0)
						{
							this.output.Write("\r\n");
							num--;
							this.lineLength = 0;
						}
						if (num != 0)
						{
							this.lineLength += num;
							while (num-- != 0)
							{
								this.output.Write(' ');
							}
						}
					}
					textRun.MoveNext();
				}
				while (textRun.Position < endTextPosition);
			}
			return true;
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x00088A4D File Offset: 0x00086C4D
		protected override void EndText()
		{
			this.RevertCharFormat();
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x00088A55 File Offset: 0x00086C55
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.output != null && this.output != null)
			{
				((IDisposable)this.output).Dispose();
			}
			this.output = null;
			base.Dispose(disposing);
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x00088A83 File Offset: 0x00086C83
		private static int CheckRange(int min, int value, int max)
		{
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x00088A94 File Offset: 0x00086C94
		private void ApplyCharFormat()
		{
			StringBuilder stringBuilder = null;
			FlagProperties effectiveFlags = base.GetEffectiveFlags();
			PropertyValue effectiveProperty = base.GetEffectiveProperty(PropertyId.FontFace);
			PropertyValue effectiveProperty2 = base.GetEffectiveProperty(PropertyId.FontSize);
			if (!effectiveProperty.IsNull && effectiveProperty.IsString && base.FormatStore.GetStringValue(effectiveProperty).GetString().Equals("Courier New") && !effectiveProperty2.IsNull && effectiveProperty2.IsRelativeHtmlFontUnits && effectiveProperty2.RelativeHtmlFontUnits == -1)
			{
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder();
				}
				stringBuilder.Append("<Fixed>");
			}
			else
			{
				if (!effectiveProperty.IsNull)
				{
					string text = null;
					if (effectiveProperty.IsMultiValue)
					{
						MultiValue multiValue = base.FormatStore.GetMultiValue(effectiveProperty);
						if (multiValue.Length != 0)
						{
							text = multiValue.GetStringValue(0).GetString();
						}
					}
					else
					{
						text = base.FormatStore.GetStringValue(effectiveProperty).GetString();
					}
					if (text != null)
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder();
						}
						stringBuilder.Append("<FontFamily><Param>");
						stringBuilder.Append(text);
						stringBuilder.Append("</Param>");
					}
				}
				if (!effectiveProperty2.IsNull && !effectiveProperty2.IsAbsLength && !effectiveProperty2.IsHtmlFontUnits && effectiveProperty2.IsRelativeHtmlFontUnits && effectiveProperty2.RelativeHtmlFontUnits != 0)
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder();
					}
					if (effectiveProperty2.RelativeHtmlFontUnits > 0)
					{
						stringBuilder.Append("<Bigger>");
						for (int i = 1; i < effectiveProperty2.RelativeHtmlFontUnits; i++)
						{
							stringBuilder.Append("<Bigger>");
						}
					}
					else
					{
						stringBuilder.Append("<Smaller>");
						for (int j = -1; j > effectiveProperty2.RelativeHtmlFontUnits; j--)
						{
							stringBuilder.Append("<Smaller>");
						}
					}
				}
			}
			PropertyValue value = base.GetEffectiveProperty(PropertyId.FontColor);
			if (value.IsEnum)
			{
				value = HtmlSupport.TranslateSystemColor(value);
			}
			if (value.IsColor && value.Color.RGB != 0U)
			{
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder();
				}
				uint num = value.Color.Red << 8;
				uint num2 = value.Color.Green << 8;
				uint num3 = value.Color.Blue << 8;
				if ((num & 256U) != 0U)
				{
					num += 255U;
				}
				if ((num2 & 256U) != 0U)
				{
					num2 += 255U;
				}
				if ((num3 & 256U) != 0U)
				{
					num3 += 255U;
				}
				stringBuilder.Append("<Color><Param>");
				stringBuilder.Append(string.Format("{0:X4},{1:X4},{2:X4}", num, num2, num3));
				stringBuilder.Append("</Param>");
			}
			if (effectiveFlags.IsDefinedAndOn(PropertyId.FirstFlag))
			{
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder();
				}
				stringBuilder.Append("<Bold>");
			}
			if (effectiveFlags.IsDefinedAndOn(PropertyId.Italic))
			{
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder();
				}
				stringBuilder.Append("<Italic>");
			}
			if (effectiveFlags.IsDefinedAndOn(PropertyId.Underline))
			{
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder();
				}
				stringBuilder.Append("<Underline>");
			}
			if (stringBuilder != null && stringBuilder.Length != 0)
			{
				this.lineLength += stringBuilder.Length;
				this.output.Write(stringBuilder.ToString());
			}
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x00088DDC File Offset: 0x00086FDC
		private void RevertCharFormat()
		{
			StringBuilder stringBuilder = null;
			FlagProperties effectiveFlags = base.GetEffectiveFlags();
			if (effectiveFlags.IsDefinedAndOn(PropertyId.Underline))
			{
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder();
				}
				stringBuilder.Append("</Underline>");
			}
			if (effectiveFlags.IsDefinedAndOn(PropertyId.Italic))
			{
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder();
				}
				stringBuilder.Append("</Italic>");
			}
			if (effectiveFlags.IsDefinedAndOn(PropertyId.FirstFlag))
			{
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder();
				}
				stringBuilder.Append("</Bold>");
			}
			PropertyValue value = base.GetEffectiveProperty(PropertyId.FontColor);
			if (value.IsEnum)
			{
				value = HtmlSupport.TranslateSystemColor(value);
			}
			if (value.IsColor && value.Color.RGB != 0U)
			{
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder();
				}
				stringBuilder.Append("</Color>");
			}
			PropertyValue effectiveProperty = base.GetEffectiveProperty(PropertyId.FontFace);
			PropertyValue effectiveProperty2 = base.GetEffectiveProperty(PropertyId.FontSize);
			if (!effectiveProperty.IsNull && effectiveProperty.IsString && base.FormatStore.GetStringValue(effectiveProperty).GetString().Equals("Courier New") && !effectiveProperty2.IsNull && effectiveProperty2.IsRelativeHtmlFontUnits && effectiveProperty2.RelativeHtmlFontUnits == -1)
			{
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder();
				}
				stringBuilder.Append("</Fixed>");
			}
			else
			{
				if (!effectiveProperty2.IsNull && !effectiveProperty2.IsAbsLength && !effectiveProperty2.IsHtmlFontUnits && effectiveProperty2.IsRelativeHtmlFontUnits && effectiveProperty2.RelativeHtmlFontUnits != 0)
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder();
					}
					if (effectiveProperty2.RelativeHtmlFontUnits > 0)
					{
						stringBuilder.Append("</Bigger>");
						for (int i = 1; i < effectiveProperty2.RelativeHtmlFontUnits; i++)
						{
							stringBuilder.Append("</Bigger>");
						}
					}
					else
					{
						stringBuilder.Append("</Smaller>");
						for (int j = -1; j > effectiveProperty2.RelativeHtmlFontUnits; j--)
						{
							stringBuilder.Append("</Smaller>");
						}
					}
				}
				if (!effectiveProperty.IsNull)
				{
					string text = null;
					if (effectiveProperty.IsMultiValue)
					{
						MultiValue multiValue = base.FormatStore.GetMultiValue(effectiveProperty);
						if (multiValue.Length != 0)
						{
							text = multiValue.GetStringValue(0).GetString();
						}
					}
					else
					{
						text = base.FormatStore.GetStringValue(effectiveProperty).GetString();
					}
					if (text != null)
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder();
						}
						stringBuilder.Append("</FontFamily>");
					}
				}
			}
			if (stringBuilder != null && stringBuilder.Length != 0)
			{
				this.lineLength += stringBuilder.Length;
				this.output.Write(stringBuilder.ToString());
			}
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x0008905B File Offset: 0x0008725B
		private string GetSubstitute(char ch)
		{
			return AsciiEncoderFallback.GetCharacterFallback(ch);
		}

		// Token: 0x0400135E RID: 4958
		private ConverterOutput output;

		// Token: 0x0400135F RID: 4959
		private Injection injection;

		// Token: 0x04001360 RID: 4960
		private bool fallbacks;

		// Token: 0x04001361 RID: 4961
		private bool blockEmpty = true;

		// Token: 0x04001362 RID: 4962
		private bool blockEnd;

		// Token: 0x04001363 RID: 4963
		private int lineLength;

		// Token: 0x04001364 RID: 4964
		private int insideNofill;

		// Token: 0x04001365 RID: 4965
		private int listLevel;

		// Token: 0x04001366 RID: 4966
		private int listIndex;

		// Token: 0x04001367 RID: 4967
		private int spaceBefore;
	}
}
