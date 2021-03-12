using System;
using Microsoft.Exchange.Data.Internal;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x02000059 RID: 89
	internal class MimeParser
	{
		// Token: 0x0600030F RID: 783 RVA: 0x00011599 File Offset: 0x0000F799
		public MimeParser(bool expectBinaryContent)
		{
			this.parseInlineAttachments = true;
			this.expectBinaryContent = expectBinaryContent;
			this.Reset();
		}

		// Token: 0x06000310 RID: 784 RVA: 0x000115B5 File Offset: 0x0000F7B5
		public MimeParser(bool parseEmbeddedMessages, bool parseInlineAttachments, bool expectBinaryContent)
		{
			this.parseEmbeddedMessages = parseEmbeddedMessages;
			this.parseInlineAttachments = parseInlineAttachments;
			this.expectBinaryContent = expectBinaryContent;
			this.Reset();
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000311 RID: 785 RVA: 0x000115D8 File Offset: 0x0000F7D8
		public bool IsEndOfFile
		{
			get
			{
				return this.state == MimeParser.ParseState.EndOfFile;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000312 RID: 786 RVA: 0x000115E3 File Offset: 0x0000F7E3
		public int Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000313 RID: 787 RVA: 0x000115EB File Offset: 0x0000F7EB
		public int Depth
		{
			get
			{
				return this.parseStackTop;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000314 RID: 788 RVA: 0x000115F3 File Offset: 0x0000F7F3
		public int PartDepth
		{
			get
			{
				if (this.parseStackTop != 0)
				{
					return this.parseStack[this.parseStackTop - 1].PartDepth;
				}
				return 0;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00011617 File Offset: 0x0000F817
		public int HeaderNameLength
		{
			get
			{
				return this.headerNameLength;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0001161F File Offset: 0x0000F81F
		public int HeaderDataOffset
		{
			get
			{
				return this.headerDataOffset;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000317 RID: 791 RVA: 0x00011627 File Offset: 0x0000F827
		public bool IsHeaderComplete
		{
			get
			{
				return this.headerComplete;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0001162F File Offset: 0x0000F82F
		// (set) Token: 0x06000319 RID: 793 RVA: 0x00011637 File Offset: 0x0000F837
		public MimeComplianceStatus ComplianceStatus
		{
			get
			{
				return this.compliance;
			}
			set
			{
				this.compliance = value;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600031A RID: 794 RVA: 0x00011640 File Offset: 0x0000F840
		public bool IsMime
		{
			get
			{
				return this.mime;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600031B RID: 795 RVA: 0x00011648 File Offset: 0x0000F848
		public MajorContentType ContentType
		{
			get
			{
				return this.currentLevel.ContentType;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00011655 File Offset: 0x0000F855
		public ContentTransferEncoding TransferEncoding
		{
			get
			{
				return this.currentLevel.TransferEncoding;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600031D RID: 797 RVA: 0x00011662 File Offset: 0x0000F862
		public ContentTransferEncoding InlineFormat
		{
			get
			{
				return this.inlineFormat;
			}
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0001166C File Offset: 0x0000F86C
		public void Reset()
		{
			this.state = MimeParser.ParseState.Headers;
			this.currentOffset = 0;
			this.lineOffset = 0;
			this.mime = false;
			this.currentLevel.Reset(true);
			this.parseStackTop = 0;
			this.position = 0;
			this.lastTokenLength = 0;
			this.firstHeader = true;
			this.nextBoundaryLevel = -1;
			this.nextBoundaryEnd = false;
			this.headerNameLength = 0;
			this.headerDataOffset = 0;
			this.headerComplete = false;
			this.inlineFormat = ContentTransferEncoding.Unknown;
			this.compliance = MimeComplianceStatus.Compliant;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x000116EE File Offset: 0x0000F8EE
		public void SetMIME()
		{
			if (this.parseStackTop == 0 || this.parseStack[this.parseStackTop - 1].ContentType == MajorContentType.MessageRfc822)
			{
				if (this.parseStackTop == 0)
				{
					this.mime = true;
				}
				this.currentLevel.IsMime = true;
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0001172E File Offset: 0x0000F92E
		public void SetContentType(MajorContentType contentType, MimeString boundaryValue)
		{
			this.currentLevel.SetContentType(contentType, boundaryValue);
			if (contentType != MajorContentType.Multipart)
			{
				this.nextBoundaryLevel = -1;
			}
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00011748 File Offset: 0x0000F948
		public void SetTransferEncoding(ContentTransferEncoding encoding)
		{
			this.currentLevel.TransferEncoding = encoding;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00011756 File Offset: 0x0000F956
		public void SetStreamMode()
		{
			this.state = MimeParser.ParseState.Body;
			this.currentLevel.StreamMode = true;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0001176B File Offset: 0x0000F96B
		public void ReportConsumedData(int lengthConsumed)
		{
			this.lastTokenLength -= lengthConsumed;
			this.position += lengthConsumed;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0001178C File Offset: 0x0000F98C
		public MimeToken Parse(byte[] data, int start, int end, bool flush)
		{
			int num = start + this.currentOffset;
			int num2 = start + this.lineOffset;
			switch (this.state)
			{
			case MimeParser.ParseState.Headers:
			{
				bool flag = false;
				int num3 = ByteString.IndexOf(data, 10, num, end - num);
				if (num3 == -1)
				{
					num3 = end;
				}
				if (num3 == end)
				{
					if ((end - start <= 998 && !flush) || (!flush && end - start <= 999 && data[end - 1] == 13))
					{
						this.currentOffset = end - start;
						return new MimeToken(MimeTokenId.None, 0);
					}
				}
				else if (num3 == start || data[num3 - 1] != 13)
				{
					this.compliance |= MimeComplianceStatus.BareLinefeedInHeader;
					flag = true;
				}
				else
				{
					num3--;
				}
				this.headerNameLength = 0;
				this.headerDataOffset = 0;
				int num4;
				if (num3 - start > 998)
				{
					this.compliance |= MimeComplianceStatus.InvalidWrapping;
					this.currentOffset = num3 - (start + 998);
					this.lineOffset = num2 - (start + 998);
					num3 = start + 998;
					num4 = 0;
				}
				else
				{
					this.currentOffset = 0;
					this.lineOffset = ((num3 == end) ? (num2 - num3) : 0);
					num4 = ((num3 == end) ? 0 : (flag ? 1 : 2));
				}
				if (num3 == start)
				{
					this.state = MimeParser.ParseState.EndOfHeaders;
					this.lastTokenLength = num4;
					return new MimeToken(MimeTokenId.EndOfHeaders, this.lastTokenLength);
				}
				if (num4 != 0 && num3 + num4 < end && data[num3 + num4] != 32 && data[num3 + num4] != 9)
				{
					this.headerComplete = true;
				}
				else
				{
					this.headerComplete = false;
				}
				if (!this.firstHeader && (num2 < start || data[num2] == 32 || data[num2] == 9))
				{
					this.lastTokenLength = num3 + num4 - start;
					return new MimeToken(MimeTokenId.HeaderContinuation, this.lastTokenLength);
				}
				this.firstHeader = false;
				int num5 = 0;
				this.headerNameLength = MimeScan.FindEndOf(MimeScan.Token.Field, data, start, num3 - start, out num5, false);
				if (this.headerNameLength == 0)
				{
					this.compliance |= MimeComplianceStatus.InvalidHeader;
					this.lastTokenLength = num3 + num4 - start;
					return new MimeToken(MimeTokenId.Header, this.lastTokenLength);
				}
				int num6 = start + this.headerNameLength;
				if (num6 == num3 || data[num6] != 58)
				{
					num6 += MimeScan.SkipLwsp(data, num6, num3 - num6);
					if (num6 == num3 || data[num6] != 58)
					{
						this.headerNameLength = 0;
						if (this.mime && (this.parseStackTop > 0 || this.currentLevel.ContentType == MajorContentType.Multipart) && num3 - num2 > 2 && data[num2] == 45 && data[num2 + 1] == 45 && this.FindBoundary(data, num2, num3, out this.nextBoundaryLevel, out this.nextBoundaryEnd))
						{
							this.compliance |= MimeComplianceStatus.MissingBodySeparator;
							if (this.nextBoundaryLevel != this.parseStackTop)
							{
								this.compliance |= MimeComplianceStatus.MissingBoundary;
							}
							this.lineOffset = 0;
							this.currentOffset = num3 - start;
							this.state = MimeParser.ParseState.EndOfHeaders;
							return new MimeToken(MimeTokenId.EndOfHeaders, 0);
						}
						this.compliance |= MimeComplianceStatus.InvalidHeader;
						this.lastTokenLength = num3 + num4 - start;
						return new MimeToken(MimeTokenId.Header, this.lastTokenLength);
					}
				}
				this.headerDataOffset = num6 + 1 - start;
				this.lastTokenLength = num3 + num4 - start;
				return new MimeToken(MimeTokenId.Header, this.lastTokenLength);
			}
			case MimeParser.ParseState.EndOfHeaders:
				this.CheckMimeConstraints();
				if (this.mime && this.parseEmbeddedMessages && this.currentLevel.ContentType == MajorContentType.MessageRfc822 && !this.currentLevel.StreamMode)
				{
					this.PushLevel(false);
					this.lastTokenLength = 0;
					return new MimeToken(MimeTokenId.EmbeddedStart, 0);
				}
				this.state = MimeParser.ParseState.Body;
				break;
			case MimeParser.ParseState.Body:
				break;
			default:
				return new MimeToken(MimeTokenId.EndOfFile, 0);
			}
			return this.ParseBody(data, start, end, flush, num2, num);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00011B14 File Offset: 0x0000FD14
		private static bool IsUUEncodeBegin(byte[] data, int line, int nextNL)
		{
			MimeString mimeString = new MimeString(data, line, nextNL - line);
			if (mimeString.Length < 13 || !mimeString.HasPrefixEq(MimeParser.UUBegin, 0, 6))
			{
				return false;
			}
			int num = 6;
			while (num < 10 && 48 <= mimeString[num] && 55 >= mimeString[num])
			{
				num++;
			}
			return num != 6 && 32 == mimeString[num];
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00011B84 File Offset: 0x0000FD84
		private static bool IsUUEncodeEnd(byte[] data, int line, int nextNL)
		{
			MimeString mimeString = new MimeString(data, line, nextNL - line);
			return mimeString.Length >= 3 && mimeString.HasPrefixEq(MimeParser.UUEnd, 0, 3);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00011BBC File Offset: 0x0000FDBC
		private MimeToken ParseBody(byte[] data, int start, int end, bool flush, int line, int current)
		{
			int num = (line <= start) ? 0 : (line - start);
			int num2;
			int num3;
			bool flag2;
			for (;;)
			{
				if (this.expectBinaryContent)
				{
					num2 = ByteString.IndexOf(data, 10, current, end - current);
				}
				else
				{
					bool flag;
					num2 = ByteString.IndexOf(data, 10, current, end - current, out flag);
					if (flag)
					{
						this.compliance |= MimeComplianceStatus.UnexpectedBinaryContent;
					}
				}
				if (num2 == -1)
				{
					num2 = end;
				}
				if (num2 == end)
				{
					num3 = 0;
					if (end - start != 0 && data[end - 1] == 13 && !flush)
					{
						num2--;
						end--;
					}
				}
				else if (num2 == start || data[num2 - 1] != 13)
				{
					if (this.currentLevel.TransferEncoding != ContentTransferEncoding.Binary)
					{
						this.compliance |= MimeComplianceStatus.BareLinefeedInBody;
					}
					num3 = 1;
				}
				else
				{
					num2--;
					num3 = 2;
				}
				if (num2 - line > 998 && this.currentLevel.TransferEncoding != ContentTransferEncoding.Binary)
				{
					this.compliance |= MimeComplianceStatus.InvalidWrapping;
				}
				if (this.nextBoundaryLevel != -1)
				{
					goto IL_2AF;
				}
				if (!this.mime || line < start || (num2 != end && num2 == line) || (num2 != line && data[line] != 45) || num2 - line > 998)
				{
					if (!this.parseInlineAttachments || this.currentLevel.IsMime || line < start || (num2 != end && num2 == line) || (num2 != line && ((this.inlineFormat == ContentTransferEncoding.Unknown && (data[line] | 32) != 98) || (this.inlineFormat == ContentTransferEncoding.UUEncode && (data[line] | 32) != 101))) || num2 - line > 998)
					{
						if (num2 == end)
						{
							break;
						}
						current = num2 + num3;
						line = current;
						num = num3;
						continue;
					}
					else
					{
						flag2 = false;
					}
				}
				else
				{
					flag2 = true;
				}
				if (num2 == end && !flush)
				{
					goto Block_27;
				}
				if (!flag2 || num2 - line <= 2 || (this.parseStackTop == 0 && this.currentLevel.ContentType != MajorContentType.Multipart) || data[line + 1] != 45 || !this.FindBoundary(data, line, num2, out this.nextBoundaryLevel, out this.nextBoundaryEnd))
				{
					if (!this.parseInlineAttachments || this.currentLevel.IsMime || !this.IsInlineBoundary(data, line, num2, end, out this.nextBoundaryLevel, out this.nextBoundaryEnd))
					{
						if (num2 == end)
						{
							goto Block_36;
						}
						current = num2 + num3;
						line = current;
						num = num3;
						continue;
					}
					else
					{
						flag2 = false;
					}
				}
				if (this.nextBoundaryLevel != this.parseStackTop || (!this.currentLevel.Epilogue && !this.nextBoundaryEnd))
				{
					goto IL_293;
				}
				this.compliance |= MimeComplianceStatus.MissingBoundary;
				this.nextBoundaryLevel = -1;
				this.currentLevel.Epilogue = true;
				if (num2 == end)
				{
					goto IL_28F;
				}
				current = num2 + num3;
				line = current;
				num = num3;
			}
			int num4 = end;
			goto IL_2BB;
			Block_27:
			num4 = ((line < start + num || !flag2) ? line : (line - num));
			goto IL_2BB;
			Block_36:
			num4 = end;
			goto IL_2BB;
			IL_28F:
			num4 = end;
			goto IL_2BB;
			IL_293:
			if (line - start > (flag2 ? num : 0))
			{
				num4 = line - (flag2 ? num : 0);
				goto IL_2BB;
			}
			IL_2AF:
			return this.ProcessBoundary(start, line, num2, num3);
			IL_2BB:
			this.lineOffset = line - num4;
			this.currentOffset = num2 - num4;
			if (num4 != start)
			{
				this.lastTokenLength = num4 - start;
				return new MimeToken(MimeTokenId.PartData, this.lastTokenLength);
			}
			if (!flush)
			{
				return new MimeToken(MimeTokenId.None, 0);
			}
			return this.ProcessEOF();
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00011EC4 File Offset: 0x000100C4
		private void PushLevel(bool inheritMime)
		{
			if (this.parseStack == null || this.parseStackTop == this.parseStack.Length)
			{
				int num = (this.parseStack == null) ? 4 : (this.parseStack.Length * 2);
				MimeParser.ParseLevel[] destinationArray = new MimeParser.ParseLevel[num];
				if (this.parseStack != null)
				{
					Array.Copy(this.parseStack, 0, destinationArray, 0, this.parseStackTop);
				}
				for (int i = 0; i < this.parseStackTop; i++)
				{
					this.parseStack[i] = default(MimeParser.ParseLevel);
				}
				this.parseStack = destinationArray;
			}
			if (this.currentLevel.ContentType != MajorContentType.MessageRfc822)
			{
				this.currentLevel.PartDepth = ((this.parseStackTop == 0) ? 1 : (this.parseStack[this.parseStackTop - 1].PartDepth + 1));
			}
			this.parseStack[this.parseStackTop++] = this.currentLevel;
			this.currentLevel.Reset(!inheritMime);
			this.state = MimeParser.ParseState.Headers;
			this.firstHeader = true;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00011FD0 File Offset: 0x000101D0
		private void CheckMimeConstraints()
		{
			if (!this.mime)
			{
				this.currentLevel.SetContentType(MajorContentType.Other, default(MimeString));
				this.currentLevel.TransferEncoding = ContentTransferEncoding.SevenBit;
				return;
			}
			if ((this.currentLevel.ContentType == MajorContentType.Multipart || this.currentLevel.ContentType == MajorContentType.MessageRfc822 || this.currentLevel.ContentType == MajorContentType.Message) && this.currentLevel.TransferEncoding > ContentTransferEncoding.Binary)
			{
				this.compliance |= MimeComplianceStatus.InvalidTransferEncoding;
			}
			if (this.parseStackTop != 0 && this.currentLevel.TransferEncoding <= ContentTransferEncoding.Binary && this.currentLevel.TransferEncoding > this.parseStack[this.parseStackTop - 1].TransferEncoding)
			{
				this.compliance |= MimeComplianceStatus.InvalidTransferEncoding;
			}
		}

		// Token: 0x0600032A RID: 810 RVA: 0x000120A0 File Offset: 0x000102A0
		private bool FindBoundary(byte[] data, int line, int nextNL, out int nextBoundaryLevel, out bool nextBoundaryEnd)
		{
			while (nextNL > line && MimeScan.IsLWSP(data[nextNL - 1]))
			{
				nextNL--;
			}
			uint num = ByteString.ComputeCrc(data, line, nextNL - line);
			bool flag;
			if (this.currentLevel.IsBoundary(data, line, nextNL - line, (long)((ulong)num), out flag))
			{
				nextBoundaryLevel = this.parseStackTop;
				nextBoundaryEnd = flag;
				return true;
			}
			for (int i = this.parseStackTop - 1; i >= 0; i--)
			{
				if (this.parseStack[i].IsBoundary(data, line, nextNL - line, (long)((ulong)num), out flag))
				{
					nextBoundaryLevel = i;
					nextBoundaryEnd = flag;
					return true;
				}
			}
			nextBoundaryLevel = -1;
			nextBoundaryEnd = false;
			return false;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00012138 File Offset: 0x00010338
		private bool IsInlineBoundary(byte[] data, int line, int nextNL, int end, out int nextBoundaryLevel, out bool nextBoundaryEnd)
		{
			ContentTransferEncoding contentTransferEncoding = this.inlineFormat;
			if (contentTransferEncoding != ContentTransferEncoding.Unknown)
			{
				if (contentTransferEncoding == ContentTransferEncoding.UUEncode)
				{
					if ((data[line] | 32) == 101 && nextNL - line >= 3 && MimeParser.IsUUEncodeEnd(data, line, nextNL))
					{
						nextBoundaryLevel = -100;
						nextBoundaryEnd = true;
						return true;
					}
				}
			}
			else if ((data[line] | 32) == 98 && nextNL - line >= 11 && nextNL != end && MimeParser.IsUUEncodeBegin(data, line, nextNL))
			{
				nextBoundaryLevel = -100;
				nextBoundaryEnd = false;
				return true;
			}
			nextBoundaryLevel = -1;
			nextBoundaryEnd = false;
			return false;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x000121B0 File Offset: 0x000103B0
		private MimeToken ProcessBoundary(int start, int line, int nextNL, int sizeNL)
		{
			if (this.nextBoundaryLevel < 0)
			{
				this.lineOffset = 0;
				this.currentOffset = 0;
				if (!this.nextBoundaryEnd)
				{
					this.inlineFormat = ((this.nextBoundaryLevel == -100) ? ContentTransferEncoding.UUEncode : ContentTransferEncoding.BinHex);
					this.nextBoundaryLevel = -1;
					this.lastTokenLength = nextNL + sizeNL - start;
					return new MimeToken(MimeTokenId.InlineStart, this.lastTokenLength);
				}
				this.inlineFormat = ContentTransferEncoding.Unknown;
				this.nextBoundaryLevel = -1;
				this.lastTokenLength = nextNL + sizeNL - start;
				return new MimeToken(MimeTokenId.InlineEnd, this.lastTokenLength);
			}
			else
			{
				if (this.nextBoundaryLevel == this.parseStackTop)
				{
					this.lineOffset = 0;
					this.currentOffset = 0;
					this.nextBoundaryLevel = -1;
					this.PushLevel(true);
					this.lastTokenLength = nextNL + sizeNL - start;
					return new MimeToken(MimeTokenId.NestedStart, this.lastTokenLength);
				}
				if (this.nextBoundaryLevel == this.parseStackTop - 1)
				{
					if (this.currentLevel.ContentType == MajorContentType.Multipart && !this.currentLevel.Epilogue)
					{
						this.compliance |= MimeComplianceStatus.MissingBoundary;
					}
					this.lineOffset = 0;
					this.currentOffset = 0;
					this.nextBoundaryLevel = -1;
					if (this.nextBoundaryEnd)
					{
						this.currentLevel = this.parseStack[--this.parseStackTop];
						this.currentLevel.Epilogue = true;
						this.parseStack[this.parseStackTop].Reset(false);
						this.lastTokenLength = nextNL + sizeNL - start;
						return new MimeToken(MimeTokenId.NestedEnd, this.lastTokenLength);
					}
					this.currentLevel.Reset(false);
					this.state = MimeParser.ParseState.Headers;
					this.firstHeader = true;
					this.lastTokenLength = nextNL + sizeNL - start;
					return new MimeToken(MimeTokenId.NestedNext, this.lastTokenLength);
				}
				else
				{
					this.lineOffset = line - start;
					this.currentOffset = nextNL - start;
					if (this.inlineFormat != ContentTransferEncoding.Unknown)
					{
						this.compliance |= MimeComplianceStatus.MissingBoundary;
						this.inlineFormat = ContentTransferEncoding.Unknown;
						return new MimeToken(MimeTokenId.InlineEnd, 0);
					}
					this.currentLevel = this.parseStack[--this.parseStackTop];
					this.currentLevel.Epilogue = true;
					this.parseStack[this.parseStackTop].Reset(false);
					if (this.currentLevel.ContentType == MajorContentType.MessageRfc822)
					{
						return new MimeToken(MimeTokenId.EmbeddedEnd, 0);
					}
					this.compliance |= MimeComplianceStatus.MissingBoundary;
					return new MimeToken(MimeTokenId.NestedEnd, 0);
				}
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00012418 File Offset: 0x00010618
		private MimeToken ProcessEOF()
		{
			if (this.inlineFormat != ContentTransferEncoding.Unknown)
			{
				this.compliance |= MimeComplianceStatus.MissingBoundary;
				this.inlineFormat = ContentTransferEncoding.Unknown;
				return new MimeToken(MimeTokenId.InlineEnd, 0);
			}
			if (this.parseStackTop == 0)
			{
				this.state = MimeParser.ParseState.EndOfFile;
				this.currentLevel.Reset(true);
				return new MimeToken(MimeTokenId.EndOfFile, 0);
			}
			this.currentLevel = this.parseStack[--this.parseStackTop];
			this.currentLevel.Epilogue = true;
			this.parseStack[this.parseStackTop].Reset(false);
			if (this.currentLevel.ContentType == MajorContentType.MessageRfc822)
			{
				return new MimeToken(MimeTokenId.EmbeddedEnd, 0);
			}
			this.compliance |= MimeComplianceStatus.MissingBoundary;
			return new MimeToken(MimeTokenId.NestedEnd, 0);
		}

		// Token: 0x0400028E RID: 654
		private static readonly byte[] UUBegin = ByteString.StringToBytes("begin ", true);

		// Token: 0x0400028F RID: 655
		private static readonly byte[] UUEnd = ByteString.StringToBytes("end", true);

		// Token: 0x04000290 RID: 656
		private MimeParser.ParseState state;

		// Token: 0x04000291 RID: 657
		private int currentOffset;

		// Token: 0x04000292 RID: 658
		private int lineOffset;

		// Token: 0x04000293 RID: 659
		private bool mime;

		// Token: 0x04000294 RID: 660
		private MimeParser.ParseLevel currentLevel;

		// Token: 0x04000295 RID: 661
		private MimeParser.ParseLevel[] parseStack;

		// Token: 0x04000296 RID: 662
		private int parseStackTop;

		// Token: 0x04000297 RID: 663
		private int position;

		// Token: 0x04000298 RID: 664
		private int lastTokenLength;

		// Token: 0x04000299 RID: 665
		private bool firstHeader;

		// Token: 0x0400029A RID: 666
		private bool parseEmbeddedMessages;

		// Token: 0x0400029B RID: 667
		private bool parseInlineAttachments;

		// Token: 0x0400029C RID: 668
		private int nextBoundaryLevel;

		// Token: 0x0400029D RID: 669
		private bool nextBoundaryEnd;

		// Token: 0x0400029E RID: 670
		private MimeComplianceStatus compliance;

		// Token: 0x0400029F RID: 671
		private bool expectBinaryContent;

		// Token: 0x040002A0 RID: 672
		private int headerNameLength;

		// Token: 0x040002A1 RID: 673
		private int headerDataOffset;

		// Token: 0x040002A2 RID: 674
		private bool headerComplete;

		// Token: 0x040002A3 RID: 675
		private ContentTransferEncoding inlineFormat;

		// Token: 0x0200005A RID: 90
		private enum ParseState
		{
			// Token: 0x040002A5 RID: 677
			Headers,
			// Token: 0x040002A6 RID: 678
			EndOfHeaders,
			// Token: 0x040002A7 RID: 679
			Body,
			// Token: 0x040002A8 RID: 680
			EndOfFile
		}

		// Token: 0x0200005B RID: 91
		private struct ParseLevel
		{
			// Token: 0x170000F3 RID: 243
			// (get) Token: 0x0600032F RID: 815 RVA: 0x00012507 File Offset: 0x00010707
			public MajorContentType ContentType
			{
				get
				{
					return this.contentType;
				}
			}

			// Token: 0x170000F4 RID: 244
			// (get) Token: 0x06000330 RID: 816 RVA: 0x0001250F File Offset: 0x0001070F
			// (set) Token: 0x06000331 RID: 817 RVA: 0x00012517 File Offset: 0x00010717
			public ContentTransferEncoding TransferEncoding
			{
				get
				{
					return this.transferEncoding;
				}
				set
				{
					this.transferEncoding = value;
				}
			}

			// Token: 0x170000F5 RID: 245
			// (get) Token: 0x06000332 RID: 818 RVA: 0x00012520 File Offset: 0x00010720
			// (set) Token: 0x06000333 RID: 819 RVA: 0x00012528 File Offset: 0x00010728
			public int PartDepth
			{
				get
				{
					return this.partDepth;
				}
				set
				{
					this.partDepth = value;
				}
			}

			// Token: 0x170000F6 RID: 246
			// (get) Token: 0x06000334 RID: 820 RVA: 0x00012531 File Offset: 0x00010731
			// (set) Token: 0x06000335 RID: 821 RVA: 0x00012539 File Offset: 0x00010739
			public bool IsMime
			{
				get
				{
					return this.mime;
				}
				set
				{
					this.mime = value;
				}
			}

			// Token: 0x170000F7 RID: 247
			// (get) Token: 0x06000336 RID: 822 RVA: 0x00012542 File Offset: 0x00010742
			// (set) Token: 0x06000337 RID: 823 RVA: 0x0001254A File Offset: 0x0001074A
			public bool StreamMode
			{
				get
				{
					return this.streamMode;
				}
				set
				{
					this.streamMode = value;
				}
			}

			// Token: 0x06000338 RID: 824 RVA: 0x00012554 File Offset: 0x00010754
			public void Reset(bool cleanMimeState)
			{
				this.streamMode = false;
				this.contentType = MajorContentType.Other;
				this.Epilogue = false;
				this.transferEncoding = ContentTransferEncoding.SevenBit;
				if (cleanMimeState)
				{
					this.mime = false;
				}
				this.boundaryValue = default(MimeString);
				this.boundaryCrc = 0U;
				this.endBoundaryCrc = 0U;
				this.partDepth = 0;
			}

			// Token: 0x06000339 RID: 825 RVA: 0x000125A8 File Offset: 0x000107A8
			public void SetContentType(MajorContentType contentType, MimeString boundaryValue)
			{
				if (contentType == MajorContentType.Multipart)
				{
					int srcOffset;
					int num;
					byte[] data = boundaryValue.GetData(out srcOffset, out num);
					int num2 = MimeString.TwoDashes.Length + num + MimeString.TwoDashes.Length;
					byte[] array = new byte[num2];
					int num3 = MimeString.TwoDashes.Length;
					Buffer.BlockCopy(MimeString.TwoDashes, 0, array, 0, num3);
					Buffer.BlockCopy(data, srcOffset, array, num3, num);
					num3 += num;
					this.boundaryCrc = ByteString.ComputeCrc(array, 0, num3);
					Buffer.BlockCopy(MimeString.TwoDashes, 0, array, num3, MimeString.TwoDashes.Length);
					num3 += MimeString.TwoDashes.Length;
					this.endBoundaryCrc = ByteString.ComputeCrc(array, 0, num3);
					this.boundaryValue = new MimeString(array, 0, num3);
				}
				else
				{
					this.boundaryValue = default(MimeString);
					this.boundaryCrc = 0U;
					this.endBoundaryCrc = 0U;
				}
				this.contentType = contentType;
			}

			// Token: 0x0600033A RID: 826 RVA: 0x00012684 File Offset: 0x00010884
			public bool IsBoundary(byte[] bytes, int offset, int length, long crc, out bool term)
			{
				if (crc == (long)((ulong)this.boundaryCrc) && this.boundaryValue.Length - 2 == length)
				{
					term = false;
					return this.boundaryValue.HasPrefixEq(bytes, offset, length);
				}
				if (crc == (long)((ulong)this.endBoundaryCrc) && this.boundaryValue.Length == length)
				{
					return term = this.boundaryValue.HasPrefixEq(bytes, offset, length);
				}
				return term = false;
			}

			// Token: 0x040002A9 RID: 681
			private bool mime;

			// Token: 0x040002AA RID: 682
			private bool streamMode;

			// Token: 0x040002AB RID: 683
			private MajorContentType contentType;

			// Token: 0x040002AC RID: 684
			public bool Epilogue;

			// Token: 0x040002AD RID: 685
			private ContentTransferEncoding transferEncoding;

			// Token: 0x040002AE RID: 686
			private MimeString boundaryValue;

			// Token: 0x040002AF RID: 687
			private int partDepth;

			// Token: 0x040002B0 RID: 688
			private uint boundaryCrc;

			// Token: 0x040002B1 RID: 689
			private uint endBoundaryCrc;
		}
	}
}
