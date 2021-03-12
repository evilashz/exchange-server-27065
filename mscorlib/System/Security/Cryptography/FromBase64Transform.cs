using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Cryptography
{
	// Token: 0x02000250 RID: 592
	[ComVisible(true)]
	public class FromBase64Transform : ICryptoTransform, IDisposable
	{
		// Token: 0x060020FF RID: 8447 RVA: 0x00072E54 File Offset: 0x00071054
		public FromBase64Transform() : this(FromBase64TransformMode.IgnoreWhiteSpaces)
		{
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x00072E5D File Offset: 0x0007105D
		public FromBase64Transform(FromBase64TransformMode whitespaces)
		{
			this._whitespaces = whitespaces;
			this._inputIndex = 0;
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06002101 RID: 8449 RVA: 0x00072E7F File Offset: 0x0007107F
		public int InputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06002102 RID: 8450 RVA: 0x00072E82 File Offset: 0x00071082
		public int OutputBlockSize
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06002103 RID: 8451 RVA: 0x00072E85 File Offset: 0x00071085
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06002104 RID: 8452 RVA: 0x00072E88 File Offset: 0x00071088
		public virtual bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002105 RID: 8453 RVA: 0x00072E8C File Offset: 0x0007108C
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
			if (this._inputBuffer == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_Generic"));
			}
			byte[] array = new byte[inputCount];
			int num;
			if (this._whitespaces == FromBase64TransformMode.IgnoreWhiteSpaces)
			{
				array = this.DiscardWhiteSpaces(inputBuffer, inputOffset, inputCount);
				num = array.Length;
			}
			else
			{
				Buffer.InternalBlockCopy(inputBuffer, inputOffset, array, 0, inputCount);
				num = inputCount;
			}
			if (num + this._inputIndex < 4)
			{
				Buffer.InternalBlockCopy(array, 0, this._inputBuffer, this._inputIndex, num);
				this._inputIndex += num;
				return 0;
			}
			int num2 = (num + this._inputIndex) / 4;
			byte[] array2 = new byte[this._inputIndex + num];
			Buffer.InternalBlockCopy(this._inputBuffer, 0, array2, 0, this._inputIndex);
			Buffer.InternalBlockCopy(array, 0, array2, this._inputIndex, num);
			this._inputIndex = (num + this._inputIndex) % 4;
			Buffer.InternalBlockCopy(array, num - this._inputIndex, this._inputBuffer, 0, this._inputIndex);
			char[] chars = Encoding.ASCII.GetChars(array2, 0, 4 * num2);
			byte[] array3 = Convert.FromBase64CharArray(chars, 0, 4 * num2);
			Buffer.BlockCopy(array3, 0, outputBuffer, outputOffset, array3.Length);
			return array3.Length;
		}

		// Token: 0x06002106 RID: 8454 RVA: 0x00073000 File Offset: 0x00071200
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
			if (this._inputBuffer == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_Generic"));
			}
			byte[] array = new byte[inputCount];
			int num;
			if (this._whitespaces == FromBase64TransformMode.IgnoreWhiteSpaces)
			{
				array = this.DiscardWhiteSpaces(inputBuffer, inputOffset, inputCount);
				num = array.Length;
			}
			else
			{
				Buffer.InternalBlockCopy(inputBuffer, inputOffset, array, 0, inputCount);
				num = inputCount;
			}
			if (num + this._inputIndex < 4)
			{
				this.Reset();
				return EmptyArray<byte>.Value;
			}
			int num2 = (num + this._inputIndex) / 4;
			byte[] array2 = new byte[this._inputIndex + num];
			Buffer.InternalBlockCopy(this._inputBuffer, 0, array2, 0, this._inputIndex);
			Buffer.InternalBlockCopy(array, 0, array2, this._inputIndex, num);
			this._inputIndex = (num + this._inputIndex) % 4;
			Buffer.InternalBlockCopy(array, num - this._inputIndex, this._inputBuffer, 0, this._inputIndex);
			char[] chars = Encoding.ASCII.GetChars(array2, 0, 4 * num2);
			byte[] result = Convert.FromBase64CharArray(chars, 0, 4 * num2);
			this.Reset();
			return result;
		}

		// Token: 0x06002107 RID: 8455 RVA: 0x00073150 File Offset: 0x00071350
		private byte[] DiscardWhiteSpaces(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			int num = 0;
			for (int i = 0; i < inputCount; i++)
			{
				if (char.IsWhiteSpace((char)inputBuffer[inputOffset + i]))
				{
					num++;
				}
			}
			byte[] array = new byte[inputCount - num];
			num = 0;
			for (int i = 0; i < inputCount; i++)
			{
				if (!char.IsWhiteSpace((char)inputBuffer[inputOffset + i]))
				{
					array[num++] = inputBuffer[inputOffset + i];
				}
			}
			return array;
		}

		// Token: 0x06002108 RID: 8456 RVA: 0x000731AB File Offset: 0x000713AB
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x000731BA File Offset: 0x000713BA
		private void Reset()
		{
			this._inputIndex = 0;
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x000731C3 File Offset: 0x000713C3
		public void Clear()
		{
			this.Dispose();
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x000731CB File Offset: 0x000713CB
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._inputBuffer != null)
				{
					Array.Clear(this._inputBuffer, 0, this._inputBuffer.Length);
				}
				this._inputBuffer = null;
				this._inputIndex = 0;
			}
		}

		// Token: 0x0600210C RID: 8460 RVA: 0x000731FC File Offset: 0x000713FC
		~FromBase64Transform()
		{
			this.Dispose(false);
		}

		// Token: 0x04000BED RID: 3053
		private byte[] _inputBuffer = new byte[4];

		// Token: 0x04000BEE RID: 3054
		private int _inputIndex;

		// Token: 0x04000BEF RID: 3055
		private FromBase64TransformMode _whitespaces;
	}
}
