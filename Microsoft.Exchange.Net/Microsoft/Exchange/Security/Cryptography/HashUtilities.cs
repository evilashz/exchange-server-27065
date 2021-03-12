using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.Security.Cryptography
{
	// Token: 0x02000AB3 RID: 2739
	internal static class HashUtilities
	{
		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x06003AF8 RID: 15096 RVA: 0x00097C08 File Offset: 0x00095E08
		public static SafeCryptProvHandle StaticAESHandle
		{
			get
			{
				if (HashUtilities.staticAESProvider == null)
				{
					lock (HashUtilities.InternalSyncObject)
					{
						if (HashUtilities.staticAESProvider == null)
						{
							SafeCryptProvHandle safeCryptProvHandle;
							if (!CapiNativeMethods.CryptAcquireContext(out safeCryptProvHandle, null, null, CapiNativeMethods.ProviderType.AES, (CapiNativeMethods.AcquireContext)4026531840U))
							{
								throw new CryptographicException(Marshal.GetLastWin32Error());
							}
							HashUtilities.staticAESProvider = safeCryptProvHandle;
						}
					}
				}
				return HashUtilities.staticAESProvider;
			}
		}

		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x06003AF9 RID: 15097 RVA: 0x00097C78 File Offset: 0x00095E78
		private static object InternalSyncObject
		{
			get
			{
				if (HashUtilities.internalSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange(ref HashUtilities.internalSyncObject, value, null);
				}
				return HashUtilities.internalSyncObject;
			}
		}

		// Token: 0x04003381 RID: 13185
		private static object internalSyncObject;

		// Token: 0x04003382 RID: 13186
		private static SafeCryptProvHandle staticAESProvider;
	}
}
