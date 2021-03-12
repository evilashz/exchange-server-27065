using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000291 RID: 657
	[ComVisible(true)]
	public class SHA1Managed : SHA1
	{
		// Token: 0x0600234B RID: 9035 RVA: 0x0007FD74 File Offset: 0x0007DF74
		public SHA1Managed()
		{
			if (CryptoConfig.AllowOnlyFipsAlgorithms && AppContextSwitches.UseLegacyFipsThrow)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Cryptography_NonCompliantFIPSAlgorithm"));
			}
			if (CryptoConfig.AllowOnlyFipsAlgorithms)
			{
				this._impl = new SHA1CryptoServiceProvider();
				return;
			}
			this._stateSHA1 = new uint[5];
			this._buffer = new byte[64];
			this._expandedBuffer = new uint[80];
			this.InitializeState();
		}

		// Token: 0x0600234C RID: 9036 RVA: 0x0007FDE4 File Offset: 0x0007DFE4
		public override void Initialize()
		{
			if (this._impl != null)
			{
				this._impl.Initialize();
				return;
			}
			this.InitializeState();
			Array.Clear(this._buffer, 0, this._buffer.Length);
			Array.Clear(this._expandedBuffer, 0, this._expandedBuffer.Length);
		}

		// Token: 0x0600234D RID: 9037 RVA: 0x0007FE33 File Offset: 0x0007E033
		protected override void HashCore(byte[] rgb, int ibStart, int cbSize)
		{
			if (this._impl != null)
			{
				this._impl.TransformBlock(rgb, ibStart, cbSize, null, 0);
				return;
			}
			this._HashData(rgb, ibStart, cbSize);
		}

		// Token: 0x0600234E RID: 9038 RVA: 0x0007FE58 File Offset: 0x0007E058
		protected override byte[] HashFinal()
		{
			if (this._impl != null)
			{
				this._impl.TransformFinalBlock(Array.Empty<byte>(), 0, 0);
				return this._impl.Hash;
			}
			return this._EndHash();
		}

		// Token: 0x0600234F RID: 9039 RVA: 0x0007FE87 File Offset: 0x0007E087
		protected override void Dispose(bool disposing)
		{
			if (disposing && this._impl != null)
			{
				this._impl.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06002350 RID: 9040 RVA: 0x0007FEA8 File Offset: 0x0007E0A8
		private void InitializeState()
		{
			this._count = 0L;
			this._stateSHA1[0] = 1732584193U;
			this._stateSHA1[1] = 4023233417U;
			this._stateSHA1[2] = 2562383102U;
			this._stateSHA1[3] = 271733878U;
			this._stateSHA1[4] = 3285377520U;
		}

		// Token: 0x06002351 RID: 9041 RVA: 0x0007FF00 File Offset: 0x0007E100
		[SecuritySafeCritical]
		private unsafe void _HashData(byte[] partIn, int ibStart, int cbSize)
		{
			int i = cbSize;
			int num = ibStart;
			int num2 = (int)(this._count & 63L);
			this._count += (long)i;
			fixed (uint* stateSHA = this._stateSHA1)
			{
				fixed (byte* buffer = this._buffer)
				{
					fixed (uint* expandedBuffer = this._expandedBuffer)
					{
						if (num2 > 0 && num2 + i >= 64)
						{
							Buffer.InternalBlockCopy(partIn, num, this._buffer, num2, 64 - num2);
							num += 64 - num2;
							i -= 64 - num2;
							SHA1Managed.SHATransform(expandedBuffer, stateSHA, buffer);
							num2 = 0;
						}
						while (i >= 64)
						{
							Buffer.InternalBlockCopy(partIn, num, this._buffer, 0, 64);
							num += 64;
							i -= 64;
							SHA1Managed.SHATransform(expandedBuffer, stateSHA, buffer);
						}
						if (i > 0)
						{
							Buffer.InternalBlockCopy(partIn, num, this._buffer, num2, i);
						}
					}
				}
			}
		}

		// Token: 0x06002352 RID: 9042 RVA: 0x00080014 File Offset: 0x0007E214
		private byte[] _EndHash()
		{
			byte[] array = new byte[20];
			int num = 64 - (int)(this._count & 63L);
			if (num <= 8)
			{
				num += 64;
			}
			byte[] array2 = new byte[num];
			array2[0] = 128;
			long num2 = this._count * 8L;
			array2[num - 8] = (byte)(num2 >> 56 & 255L);
			array2[num - 7] = (byte)(num2 >> 48 & 255L);
			array2[num - 6] = (byte)(num2 >> 40 & 255L);
			array2[num - 5] = (byte)(num2 >> 32 & 255L);
			array2[num - 4] = (byte)(num2 >> 24 & 255L);
			array2[num - 3] = (byte)(num2 >> 16 & 255L);
			array2[num - 2] = (byte)(num2 >> 8 & 255L);
			array2[num - 1] = (byte)(num2 & 255L);
			this._HashData(array2, 0, array2.Length);
			Utils.DWORDToBigEndian(array, this._stateSHA1, 5);
			this.HashValue = array;
			return array;
		}

		// Token: 0x06002353 RID: 9043 RVA: 0x00080100 File Offset: 0x0007E300
		[SecurityCritical]
		private unsafe static void SHATransform(uint* expandedBuffer, uint* state, byte* block)
		{
			uint num = *state;
			uint num2 = state[1];
			uint num3 = state[2];
			uint num4 = state[3];
			uint num5 = state[4];
			Utils.DWORDFromBigEndian(expandedBuffer, 16, block);
			SHA1Managed.SHAExpand(expandedBuffer);
			int i;
			for (i = 0; i < 20; i += 5)
			{
				num5 += (num << 5 | num >> 27) + (num4 ^ (num2 & (num3 ^ num4))) + expandedBuffer[i] + 1518500249U;
				num2 = (num2 << 30 | num2 >> 2);
				num4 += (num5 << 5 | num5 >> 27) + (num3 ^ (num & (num2 ^ num3))) + expandedBuffer[i + 1] + 1518500249U;
				num = (num << 30 | num >> 2);
				num3 += (num4 << 5 | num4 >> 27) + (num2 ^ (num5 & (num ^ num2))) + expandedBuffer[i + 2] + 1518500249U;
				num5 = (num5 << 30 | num5 >> 2);
				num2 += (num3 << 5 | num3 >> 27) + (num ^ (num4 & (num5 ^ num))) + expandedBuffer[i + 3] + 1518500249U;
				num4 = (num4 << 30 | num4 >> 2);
				num += (num2 << 5 | num2 >> 27) + (num5 ^ (num3 & (num4 ^ num5))) + expandedBuffer[i + 4] + 1518500249U;
				num3 = (num3 << 30 | num3 >> 2);
			}
			while (i < 40)
			{
				num5 += (num << 5 | num >> 27) + (num2 ^ num3 ^ num4) + expandedBuffer[i] + 1859775393U;
				num2 = (num2 << 30 | num2 >> 2);
				num4 += (num5 << 5 | num5 >> 27) + (num ^ num2 ^ num3) + expandedBuffer[i + 1] + 1859775393U;
				num = (num << 30 | num >> 2);
				num3 += (num4 << 5 | num4 >> 27) + (num5 ^ num ^ num2) + expandedBuffer[i + 2] + 1859775393U;
				num5 = (num5 << 30 | num5 >> 2);
				num2 += (num3 << 5 | num3 >> 27) + (num4 ^ num5 ^ num) + expandedBuffer[i + 3] + 1859775393U;
				num4 = (num4 << 30 | num4 >> 2);
				num += (num2 << 5 | num2 >> 27) + (num3 ^ num4 ^ num5) + expandedBuffer[i + 4] + 1859775393U;
				num3 = (num3 << 30 | num3 >> 2);
				i += 5;
			}
			while (i < 60)
			{
				num5 += (num << 5 | num >> 27) + ((num2 & num3) | (num4 & (num2 | num3))) + expandedBuffer[i] + 2400959708U;
				num2 = (num2 << 30 | num2 >> 2);
				num4 += (num5 << 5 | num5 >> 27) + ((num & num2) | (num3 & (num | num2))) + expandedBuffer[i + 1] + 2400959708U;
				num = (num << 30 | num >> 2);
				num3 += (num4 << 5 | num4 >> 27) + ((num5 & num) | (num2 & (num5 | num))) + expandedBuffer[i + 2] + 2400959708U;
				num5 = (num5 << 30 | num5 >> 2);
				num2 += (num3 << 5 | num3 >> 27) + ((num4 & num5) | (num & (num4 | num5))) + expandedBuffer[i + 3] + 2400959708U;
				num4 = (num4 << 30 | num4 >> 2);
				num += (num2 << 5 | num2 >> 27) + ((num3 & num4) | (num5 & (num3 | num4))) + expandedBuffer[i + 4] + 2400959708U;
				num3 = (num3 << 30 | num3 >> 2);
				i += 5;
			}
			while (i < 80)
			{
				num5 += (num << 5 | num >> 27) + (num2 ^ num3 ^ num4) + expandedBuffer[i] + 3395469782U;
				num2 = (num2 << 30 | num2 >> 2);
				num4 += (num5 << 5 | num5 >> 27) + (num ^ num2 ^ num3) + expandedBuffer[i + 1] + 3395469782U;
				num = (num << 30 | num >> 2);
				num3 += (num4 << 5 | num4 >> 27) + (num5 ^ num ^ num2) + expandedBuffer[i + 2] + 3395469782U;
				num5 = (num5 << 30 | num5 >> 2);
				num2 += (num3 << 5 | num3 >> 27) + (num4 ^ num5 ^ num) + expandedBuffer[i + 3] + 3395469782U;
				num4 = (num4 << 30 | num4 >> 2);
				num += (num2 << 5 | num2 >> 27) + (num3 ^ num4 ^ num5) + expandedBuffer[i + 4] + 3395469782U;
				num3 = (num3 << 30 | num3 >> 2);
				i += 5;
			}
			*state += num;
			state[1] += num2;
			state[2] += num3;
			state[3] += num4;
			state[4] += num5;
		}

		// Token: 0x06002354 RID: 9044 RVA: 0x00080560 File Offset: 0x0007E760
		[SecurityCritical]
		private unsafe static void SHAExpand(uint* x)
		{
			for (int i = 16; i < 80; i++)
			{
				uint num = x[i - 3] ^ x[i - 8] ^ x[i - 14] ^ x[i - 16];
				x[i] = (num << 1 | num >> 31);
			}
		}

		// Token: 0x04000CDB RID: 3291
		private SHA1 _impl;

		// Token: 0x04000CDC RID: 3292
		private byte[] _buffer;

		// Token: 0x04000CDD RID: 3293
		private long _count;

		// Token: 0x04000CDE RID: 3294
		private uint[] _stateSHA1;

		// Token: 0x04000CDF RID: 3295
		private uint[] _expandedBuffer;
	}
}
