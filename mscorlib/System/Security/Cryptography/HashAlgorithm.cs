using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000268 RID: 616
	[ComVisible(true)]
	public abstract class HashAlgorithm : IDisposable, ICryptoTransform
	{
		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x060021CF RID: 8655 RVA: 0x00077B30 File Offset: 0x00075D30
		public virtual int HashSize
		{
			get
			{
				return this.HashSizeValue;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x060021D0 RID: 8656 RVA: 0x00077B38 File Offset: 0x00075D38
		public virtual byte[] Hash
		{
			get
			{
				if (this.m_bDisposed)
				{
					throw new ObjectDisposedException(null);
				}
				if (this.State != 0)
				{
					throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_HashNotYetFinalized"));
				}
				return (byte[])this.HashValue.Clone();
			}
		}

		// Token: 0x060021D1 RID: 8657 RVA: 0x00077B71 File Offset: 0x00075D71
		public static HashAlgorithm Create()
		{
			return HashAlgorithm.Create("System.Security.Cryptography.HashAlgorithm");
		}

		// Token: 0x060021D2 RID: 8658 RVA: 0x00077B7D File Offset: 0x00075D7D
		public static HashAlgorithm Create(string hashName)
		{
			return (HashAlgorithm)CryptoConfig.CreateFromName(hashName);
		}

		// Token: 0x060021D3 RID: 8659 RVA: 0x00077B8C File Offset: 0x00075D8C
		public byte[] ComputeHash(Stream inputStream)
		{
			if (this.m_bDisposed)
			{
				throw new ObjectDisposedException(null);
			}
			byte[] array = new byte[4096];
			int num;
			do
			{
				num = inputStream.Read(array, 0, 4096);
				if (num > 0)
				{
					this.HashCore(array, 0, num);
				}
			}
			while (num > 0);
			this.HashValue = this.HashFinal();
			byte[] result = (byte[])this.HashValue.Clone();
			this.Initialize();
			return result;
		}

		// Token: 0x060021D4 RID: 8660 RVA: 0x00077BF8 File Offset: 0x00075DF8
		public byte[] ComputeHash(byte[] buffer)
		{
			if (this.m_bDisposed)
			{
				throw new ObjectDisposedException(null);
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.HashCore(buffer, 0, buffer.Length);
			this.HashValue = this.HashFinal();
			byte[] result = (byte[])this.HashValue.Clone();
			this.Initialize();
			return result;
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x00077C54 File Offset: 0x00075E54
		public byte[] ComputeHash(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0 || count > buffer.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"));
			}
			if (buffer.Length - count < offset)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (this.m_bDisposed)
			{
				throw new ObjectDisposedException(null);
			}
			this.HashCore(buffer, offset, count);
			this.HashValue = this.HashFinal();
			byte[] result = (byte[])this.HashValue.Clone();
			this.Initialize();
			return result;
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x060021D6 RID: 8662 RVA: 0x00077CF6 File Offset: 0x00075EF6
		public virtual int InputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x060021D7 RID: 8663 RVA: 0x00077CF9 File Offset: 0x00075EF9
		public virtual int OutputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x060021D8 RID: 8664 RVA: 0x00077CFC File Offset: 0x00075EFC
		public virtual bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x060021D9 RID: 8665 RVA: 0x00077CFF File Offset: 0x00075EFF
		public virtual bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x00077D04 File Offset: 0x00075F04
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			if (inputBuffer == null)
			{
				throw new ArgumentNullException("inputBuffer");
			}
			if (inputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("inputOffset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (inputCount < 0 || inputCount > inputBuffer.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"));
			}
			if (inputBuffer.Length - inputCount < inputOffset)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (this.m_bDisposed)
			{
				throw new ObjectDisposedException(null);
			}
			this.State = 1;
			this.HashCore(inputBuffer, inputOffset, inputCount);
			if (outputBuffer != null && (inputBuffer != outputBuffer || inputOffset != outputOffset))
			{
				Buffer.BlockCopy(inputBuffer, inputOffset, outputBuffer, outputOffset, inputCount);
			}
			return inputCount;
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x00077DA4 File Offset: 0x00075FA4
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			if (inputBuffer == null)
			{
				throw new ArgumentNullException("inputBuffer");
			}
			if (inputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("inputOffset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (inputCount < 0 || inputCount > inputBuffer.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"));
			}
			if (inputBuffer.Length - inputCount < inputOffset)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (this.m_bDisposed)
			{
				throw new ObjectDisposedException(null);
			}
			this.HashCore(inputBuffer, inputOffset, inputCount);
			this.HashValue = this.HashFinal();
			byte[] array;
			if (inputCount != 0)
			{
				array = new byte[inputCount];
				Buffer.InternalBlockCopy(inputBuffer, inputOffset, array, 0, inputCount);
			}
			else
			{
				array = EmptyArray<byte>.Value;
			}
			this.State = 0;
			return array;
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x00077E52 File Offset: 0x00076052
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x00077E61 File Offset: 0x00076061
		public void Clear()
		{
			((IDisposable)this).Dispose();
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x00077E69 File Offset: 0x00076069
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.HashValue != null)
				{
					Array.Clear(this.HashValue, 0, this.HashValue.Length);
				}
				this.HashValue = null;
				this.m_bDisposed = true;
			}
		}

		// Token: 0x060021DF RID: 8671
		public abstract void Initialize();

		// Token: 0x060021E0 RID: 8672
		protected abstract void HashCore(byte[] array, int ibStart, int cbSize);

		// Token: 0x060021E1 RID: 8673
		protected abstract byte[] HashFinal();

		// Token: 0x04000C4F RID: 3151
		protected int HashSizeValue;

		// Token: 0x04000C50 RID: 3152
		protected internal byte[] HashValue;

		// Token: 0x04000C51 RID: 3153
		protected int State;

		// Token: 0x04000C52 RID: 3154
		private bool m_bDisposed;
	}
}
