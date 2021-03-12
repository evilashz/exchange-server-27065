using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000246 RID: 582
	[ComVisible(true)]
	public abstract class RandomNumberGenerator : IDisposable
	{
		// Token: 0x060020BF RID: 8383 RVA: 0x00072935 File Offset: 0x00070B35
		public static RandomNumberGenerator Create()
		{
			return RandomNumberGenerator.Create("System.Security.Cryptography.RandomNumberGenerator");
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x00072941 File Offset: 0x00070B41
		public static RandomNumberGenerator Create(string rngName)
		{
			return (RandomNumberGenerator)CryptoConfig.CreateFromName(rngName);
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x0007294E File Offset: 0x00070B4E
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x0007295D File Offset: 0x00070B5D
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x060020C3 RID: 8387
		public abstract void GetBytes(byte[] data);

		// Token: 0x060020C4 RID: 8388 RVA: 0x00072960 File Offset: 0x00070B60
		public virtual void GetBytes(byte[] data, int offset, int count)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (offset + count > data.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (count > 0)
			{
				byte[] array = new byte[count];
				this.GetBytes(array);
				Array.Copy(array, 0, data, offset, count);
			}
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x000729E1 File Offset: 0x00070BE1
		public virtual void GetNonZeroBytes(byte[] data)
		{
			throw new NotImplementedException();
		}
	}
}
