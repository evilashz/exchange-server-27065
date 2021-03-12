using System;
using System.Security;
using System.Web;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000068 RID: 104
	public class SecureHttpBuffer : DisposeTrackableBase
	{
		// Token: 0x0600032F RID: 815 RVA: 0x0001387C File Offset: 0x00011A7C
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
			this.buffer = new SecureArray<char>(new char[size]);
			this.response = response;
			this.currentPosition = 0;
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000330 RID: 816 RVA: 0x000138CB File Offset: 0x00011ACB
		public int Size
		{
			get
			{
				base.CheckDisposed();
				return this.buffer.ArrayValue.Length;
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x000138E0 File Offset: 0x00011AE0
		public void CopyAtCurrentPosition(string value)
		{
			base.CheckDisposed();
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.AdjustSizeAtCurrentPosition(value.Length);
			value.CopyTo(0, this.buffer.ArrayValue, this.currentPosition, value.Length);
			this.currentPosition += value.Length;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00013940 File Offset: 0x00011B40
		public void CopyAtCurrentPosition(SecureString secureValue)
		{
			base.CheckDisposed();
			if (secureValue == null)
			{
				throw new ArgumentNullException("secureValue");
			}
			using (SecureArray<char> secureArray = secureValue.ConvertToSecureCharArray())
			{
				this.CopyAtCurrentPosition(secureArray);
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0001398C File Offset: 0x00011B8C
		public void CopyAtCurrentPosition(SecureArray<char> secureArray)
		{
			base.CheckDisposed();
			if (secureArray == null)
			{
				throw new ArgumentNullException("secureArray");
			}
			this.AdjustSizeAtCurrentPosition(secureArray.ArrayValue.Length);
			secureArray.ArrayValue.CopyTo(this.buffer.ArrayValue, this.currentPosition);
			this.currentPosition += secureArray.ArrayValue.Length;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x000139EC File Offset: 0x00011BEC
		public void Flush()
		{
			base.CheckDisposed();
			if (this.currentPosition > 0)
			{
				this.response.Write(this.buffer.ArrayValue, 0, this.currentPosition);
				Array.Clear(this.buffer.ArrayValue, 0, this.buffer.ArrayValue.Length);
				this.currentPosition = 0;
				this.response.Flush();
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00013A55 File Offset: 0x00011C55
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.buffer != null)
			{
				this.Flush();
				this.buffer.Dispose();
				this.buffer = null;
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00013A7A File Offset: 0x00011C7A
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SecureHttpBuffer>(this);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00013A84 File Offset: 0x00011C84
		private void AdjustSizeAtCurrentPosition(int length)
		{
			int num = this.currentPosition + 1 + length;
			if (num > this.Size)
			{
				this.Resize(Math.Max(this.Size * 2, num));
			}
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00013ABC File Offset: 0x00011CBC
		private void Resize(int newSize)
		{
			using (SecureArray<char> secureArray = this.buffer)
			{
				this.buffer = new SecureArray<char>(newSize);
				secureArray.ArrayValue.CopyTo(this.buffer.ArrayValue, 0);
			}
		}

		// Token: 0x04000230 RID: 560
		private SecureArray<char> buffer;

		// Token: 0x04000231 RID: 561
		private int currentPosition;

		// Token: 0x04000232 RID: 562
		private HttpResponse response;
	}
}
