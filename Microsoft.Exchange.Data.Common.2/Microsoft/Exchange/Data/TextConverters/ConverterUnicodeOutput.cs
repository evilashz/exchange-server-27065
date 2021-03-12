using System;
using System.IO;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x0200017E RID: 382
	internal class ConverterUnicodeOutput : ConverterOutput, IRestartable, IReusable
	{
		// Token: 0x06001069 RID: 4201 RVA: 0x00078790 File Offset: 0x00076990
		public ConverterUnicodeOutput(object destination, bool push, bool restartable)
		{
			if (push)
			{
				this.pushSink = (destination as TextWriter);
			}
			else
			{
				this.pullSink = (destination as ConverterReader);
				this.pullSink.SetSource(this);
			}
			this.canRestart = restartable;
			this.restartable = restartable;
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x0600106A RID: 4202 RVA: 0x000787EE File Offset: 0x000769EE
		public override bool CanAcceptMore
		{
			get
			{
				return this.canRestart || this.pullSink == null || this.cache.Length == 0;
			}
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x00078810 File Offset: 0x00076A10
		bool IRestartable.CanRestart()
		{
			return this.canRestart;
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x00078818 File Offset: 0x00076A18
		void IRestartable.Restart()
		{
			this.Reinitialize();
			this.canRestart = false;
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x00078827 File Offset: 0x00076A27
		void IRestartable.DisableRestart()
		{
			this.canRestart = false;
			this.FlushCached();
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x00078838 File Offset: 0x00076A38
		void IReusable.Initialize(object newSourceOrDestination)
		{
			if (this.pushSink != null && newSourceOrDestination != null)
			{
				TextWriter textWriter = newSourceOrDestination as TextWriter;
				if (textWriter == null)
				{
					throw new InvalidOperationException("cannot reinitialize this converter - new output should be a TextWriter object");
				}
				this.pushSink = textWriter;
			}
			this.Reinitialize();
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x00078874 File Offset: 0x00076A74
		public override void Write(char[] buffer, int offset, int count, IFallback fallback)
		{
			byte unsafeAsciiMask = 0;
			byte[] unsafeAsciiMap = (fallback == null) ? null : fallback.GetUnsafeAsciiMap(out unsafeAsciiMask);
			bool hasUnsafeUnicode = fallback != null && fallback.HasUnsafeUnicode();
			if (this.cache.Length == 0)
			{
				if (!this.canRestart)
				{
					if (this.pullSink != null)
					{
						char[] array;
						int num;
						int num2;
						this.pullSink.GetOutputBuffer(out array, out num, out num2);
						if (num2 != 0)
						{
							if (fallback != null)
							{
								int num3 = num;
								while (count != 0 && num2 != 0)
								{
									char c = buffer[offset];
									if (ConverterUnicodeOutput.IsUnsafeCharacter(c, unsafeAsciiMap, unsafeAsciiMask, hasUnsafeUnicode, this.isFirstChar, fallback))
									{
										int num4 = num;
										if (!fallback.FallBackChar(c, array, ref num, num + num2))
										{
											break;
										}
										num2 -= num - num4;
									}
									else
									{
										array[num++] = c;
										num2--;
									}
									this.isFirstChar = false;
									count--;
									offset++;
								}
								this.pullSink.ReportOutput(num - num3);
							}
							else
							{
								int num5 = Math.Min(num2, count);
								Buffer.BlockCopy(buffer, offset * 2, array, num * 2, num5 * 2);
								this.isFirstChar = false;
								count -= num5;
								offset += num5;
								this.pullSink.ReportOutput(num5);
								num += num5;
								num2 -= num5;
							}
						}
						while (count != 0)
						{
							if (fallback != null)
							{
								char[] array2;
								int num6;
								int num7;
								this.cache.GetBuffer(16, out array2, out num6, out num7);
								int num8 = num6;
								while (count != 0 && num7 != 0)
								{
									char c2 = buffer[offset];
									if (ConverterUnicodeOutput.IsUnsafeCharacter(c2, unsafeAsciiMap, unsafeAsciiMask, hasUnsafeUnicode, this.isFirstChar, fallback))
									{
										int num9 = num6;
										if (!fallback.FallBackChar(c2, array2, ref num6, num6 + num7))
										{
											break;
										}
										num7 -= num6 - num9;
									}
									else
									{
										array2[num6++] = c2;
										num7--;
									}
									this.isFirstChar = false;
									count--;
									offset++;
								}
								this.cache.Commit(num6 - num8);
							}
							else
							{
								int size = Math.Min(count, 256);
								char[] array2;
								int num6;
								int num7;
								this.cache.GetBuffer(size, out array2, out num6, out num7);
								int num10 = Math.Min(num7, count);
								Buffer.BlockCopy(buffer, offset * 2, array2, num6 * 2, num10 * 2);
								this.isFirstChar = false;
								this.cache.Commit(num10);
								offset += num10;
								count -= num10;
							}
						}
						while (num2 != 0)
						{
							if (this.cache.Length == 0)
							{
								return;
							}
							char[] src;
							int num11;
							int val;
							this.cache.GetData(out src, out num11, out val);
							int num12 = Math.Min(val, num2);
							Buffer.BlockCopy(src, num11 * 2, array, num * 2, num12 * 2);
							this.cache.ReportRead(num12);
							this.pullSink.ReportOutput(num12);
							num += num12;
							num2 -= num12;
						}
					}
					else
					{
						if (fallback != null)
						{
							char[] array3;
							int num13;
							int num14;
							this.cache.GetBuffer(1024, out array3, out num13, out num14);
							int num15 = num13;
							int num16 = num14;
							while (count != 0)
							{
								while (count != 0 && num14 != 0)
								{
									char c3 = buffer[offset];
									if (ConverterUnicodeOutput.IsUnsafeCharacter(c3, unsafeAsciiMap, unsafeAsciiMask, hasUnsafeUnicode, this.isFirstChar, fallback))
									{
										int num17 = num13;
										if (!fallback.FallBackChar(c3, array3, ref num13, num13 + num14))
										{
											break;
										}
										num14 -= num13 - num17;
									}
									else
									{
										array3[num13++] = c3;
										num14--;
									}
									this.isFirstChar = false;
									count--;
									offset++;
								}
								if (num13 - num15 != 0)
								{
									this.pushSink.Write(array3, num15, num13 - num15);
									num13 = num15;
									num14 = num16;
								}
							}
							return;
						}
						if (count != 0)
						{
							this.pushSink.Write(buffer, offset, count);
							this.isFirstChar = false;
						}
					}
					return;
				}
			}
			while (count != 0)
			{
				if (fallback != null)
				{
					char[] array4;
					int num18;
					int num19;
					this.cache.GetBuffer(16, out array4, out num18, out num19);
					int num20 = num18;
					while (count != 0 && num19 != 0)
					{
						char c4 = buffer[offset];
						if (ConverterUnicodeOutput.IsUnsafeCharacter(c4, unsafeAsciiMap, unsafeAsciiMask, hasUnsafeUnicode, this.isFirstChar, fallback))
						{
							int num21 = num18;
							if (!fallback.FallBackChar(c4, array4, ref num18, num18 + num19))
							{
								break;
							}
							num19 -= num18 - num21;
						}
						else
						{
							array4[num18++] = c4;
							num19--;
						}
						this.isFirstChar = false;
						count--;
						offset++;
					}
					this.cache.Commit(num18 - num20);
				}
				else
				{
					int size2 = Math.Min(count, 256);
					char[] array4;
					int num18;
					int num19;
					this.cache.GetBuffer(size2, out array4, out num18, out num19);
					int num22 = Math.Min(num19, count);
					Buffer.BlockCopy(buffer, offset * 2, array4, num18 * 2, num22 * 2);
					this.isFirstChar = false;
					this.cache.Commit(num22);
					offset += num22;
					count -= num22;
				}
			}
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x00078CF8 File Offset: 0x00076EF8
		public override void Flush()
		{
			if (this.endOfFile)
			{
				return;
			}
			this.canRestart = false;
			this.FlushCached();
			if (this.pullSink == null)
			{
				this.pushSink.Flush();
			}
			else if (this.cache.Length == 0)
			{
				this.pullSink.ReportEndOfFile();
			}
			this.endOfFile = true;
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x00078D50 File Offset: 0x00076F50
		public bool GetOutputChunk(out char[] chunkBuffer, out int chunkOffset, out int chunkLength)
		{
			if (this.cache.Length == 0 || this.canRestart)
			{
				chunkBuffer = null;
				chunkOffset = 0;
				chunkLength = 0;
				return false;
			}
			this.cache.GetData(out chunkBuffer, out chunkOffset, out chunkLength);
			return true;
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x00078D81 File Offset: 0x00076F81
		public void ReportOutput(int readCount)
		{
			this.cache.ReportRead(readCount);
			if (this.cache.Length == 0 && this.endOfFile)
			{
				this.pullSink.ReportEndOfFile();
			}
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x00078DB0 File Offset: 0x00076FB0
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.cache != null && this.cache is IDisposable)
			{
				((IDisposable)this.cache).Dispose();
			}
			this.cache = null;
			this.pushSink = null;
			this.pullSink = null;
			base.Dispose(disposing);
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x00078E04 File Offset: 0x00077004
		private static bool IsUnsafeCharacter(char ch, byte[] unsafeAsciiMap, byte unsafeAsciiMask, bool hasUnsafeUnicode, bool isFirstChar, IFallback fallback)
		{
			return unsafeAsciiMap != null && (((int)ch < unsafeAsciiMap.Length && (unsafeAsciiMap[(int)ch] & unsafeAsciiMask) != 0) || (hasUnsafeUnicode && ch >= '\u007f' && fallback.IsUnsafeUnicode(ch, isFirstChar)));
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x00078E48 File Offset: 0x00077048
		private void Reinitialize()
		{
			this.endOfFile = false;
			this.cache.Reset();
			this.canRestart = this.restartable;
			this.isFirstChar = true;
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x00078E70 File Offset: 0x00077070
		private bool FlushCached()
		{
			if (this.canRestart || this.cache.Length == 0)
			{
				return false;
			}
			if (this.pullSink == null)
			{
				while (this.cache.Length != 0)
				{
					char[] buffer;
					int num;
					int num2;
					this.cache.GetData(out buffer, out num, out num2);
					this.pushSink.Write(buffer, num, num2);
					this.cache.ReportRead(num2);
				}
			}
			else
			{
				char[] buffer;
				int num;
				int count;
				this.pullSink.GetOutputBuffer(out buffer, out num, out count);
				int num2 = this.cache.Read(buffer, num, count);
				this.pullSink.ReportOutput(num2);
				if (this.cache.Length == 0 && this.endOfFile)
				{
					this.pullSink.ReportEndOfFile();
				}
			}
			return true;
		}

		// Token: 0x0400110B RID: 4363
		private const int FallbackExpansionMax = 16;

		// Token: 0x0400110C RID: 4364
		private TextWriter pushSink;

		// Token: 0x0400110D RID: 4365
		private ConverterReader pullSink;

		// Token: 0x0400110E RID: 4366
		private bool endOfFile;

		// Token: 0x0400110F RID: 4367
		private bool restartable;

		// Token: 0x04001110 RID: 4368
		private bool canRestart;

		// Token: 0x04001111 RID: 4369
		private bool isFirstChar = true;

		// Token: 0x04001112 RID: 4370
		private TextCache cache = new TextCache();
	}
}
