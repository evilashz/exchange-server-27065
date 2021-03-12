using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Web;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000245 RID: 581
	public class SecureHttpBuffer
	{
		// Token: 0x06001381 RID: 4993 RVA: 0x000784D4 File Offset: 0x000766D4
		public SecureHttpBuffer(int size, HttpResponse response)
		{
			if (response == null)
			{
				throw new ArgumentNullException("response");
			}
			if (size < 0)
			{
				throw new ArgumentException("Size is not valid");
			}
			this.buffer = new char[size];
			this.size = size;
			this.response = response;
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001382 RID: 4994 RVA: 0x00078513 File Offset: 0x00076713
		// (set) Token: 0x06001383 RID: 4995 RVA: 0x00078520 File Offset: 0x00076720
		public int Size
		{
			get
			{
				return this.buffer.Length;
			}
			set
			{
				if (value != this.buffer.Length)
				{
					if (value < this.size)
					{
						throw new ArgumentException("new Value is smaller than the current list size");
					}
					if (value == 0)
					{
						Array.Clear(this.buffer, 0, this.size);
						this.buffer = new char[0];
						return;
					}
					char[] destinationArray = new char[value];
					if (this.size > 0)
					{
						Array.Copy(this.buffer, 0, destinationArray, 0, this.size);
						Array.Clear(this.buffer, 0, this.size);
					}
					this.buffer = destinationArray;
				}
			}
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x000785AC File Offset: 0x000767AC
		public void CopyAtCurrentPosition(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.AdjustSizeAtCurrentPosition(value.Length);
			value.CopyTo(0, this.buffer, this.currentPosition, value.Length);
			this.currentPosition += value.Length;
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x00078600 File Offset: 0x00076800
		public void CopyAtCurrentPosition(SecureString secureValue)
		{
			if (secureValue == null)
			{
				throw new ArgumentNullException("secureValue");
			}
			this.AdjustSizeAtCurrentPosition(secureValue.Length);
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = Marshal.SecureStringToBSTR(secureValue);
				Marshal.Copy(intPtr, this.buffer, this.currentPosition, secureValue.Length);
				this.currentPosition += secureValue.Length;
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.ZeroFreeBSTR(intPtr);
				}
			}
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x00078688 File Offset: 0x00076888
		private void AdjustSizeAtCurrentPosition(int length)
		{
			int num = this.currentPosition + 1 + length;
			if (num > this.size)
			{
				if (num < 2 * this.size)
				{
					this.Size = 2 * this.size;
					return;
				}
				this.Size = num;
			}
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x000786CA File Offset: 0x000768CA
		public void FlushBuffer()
		{
			this.response.Write(this.buffer, 0, this.currentPosition + 1);
			Array.Clear(this.buffer, 0, this.currentPosition + 1);
			this.response.Flush();
		}

		// Token: 0x04000D77 RID: 3447
		private char[] buffer;

		// Token: 0x04000D78 RID: 3448
		private int size;

		// Token: 0x04000D79 RID: 3449
		private int currentPosition;

		// Token: 0x04000D7A RID: 3450
		private HttpResponse response;
	}
}
