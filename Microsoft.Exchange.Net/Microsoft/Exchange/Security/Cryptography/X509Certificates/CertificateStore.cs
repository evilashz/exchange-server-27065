using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000A83 RID: 2691
	internal class CertificateStore : IDisposable
	{
		// Token: 0x06003A1F RID: 14879 RVA: 0x00093AF1 File Offset: 0x00091CF1
		public CertificateStore(StoreName storeName, StoreLocation storeLocation)
		{
			this.storeType = StoreType.System;
			this.store = new X509Store(storeName, storeLocation);
		}

		// Token: 0x06003A20 RID: 14880 RVA: 0x00093B10 File Offset: 0x00091D10
		public CertificateStore(StoreType type, string serviceStoreName)
		{
			switch (type)
			{
			case StoreType.System:
				this.store = new X509Store(serviceStoreName, StoreLocation.LocalMachine);
				break;
			case StoreType.Memory:
				this.serviceStoreName = string.Empty;
				break;
			case StoreType.Service:
				if (string.IsNullOrEmpty(serviceStoreName))
				{
					throw new ArgumentNullException("serviceStoreName");
				}
				this.serviceStoreName = serviceStoreName;
				break;
			default:
				throw new ArgumentException(NetException.StoreTypeUnsupported, "type");
			}
			this.storeType = type;
		}

		// Token: 0x06003A21 RID: 14881 RVA: 0x00093B90 File Offset: 0x00091D90
		public static X509Store Open(StoreType type, string path, OpenFlags flags)
		{
			CapiNativeMethods.CertificateStoreOptions certificateStoreOptions = (CapiNativeMethods.CertificateStoreOptions)0U;
			if ((flags & OpenFlags.OpenExistingOnly) == OpenFlags.OpenExistingOnly)
			{
				certificateStoreOptions |= CapiNativeMethods.CertificateStoreOptions.OpenExisting;
			}
			if ((flags & OpenFlags.ReadWrite) != OpenFlags.ReadWrite)
			{
				certificateStoreOptions |= CapiNativeMethods.CertificateStoreOptions.ReadOnly;
			}
			switch (type)
			{
			case StoreType.Memory:
				certificateStoreOptions |= CapiNativeMethods.CertificateStoreOptions.Create;
				return CertificateStore.StoreOpen(CapiNativeMethods.CertificateStoreProvider.Memory, certificateStoreOptions, null);
			case StoreType.Service:
				certificateStoreOptions |= CapiNativeMethods.CertificateStoreOptions.Services;
				return CertificateStore.StoreOpen(CapiNativeMethods.CertificateStoreProvider.System, certificateStoreOptions, path);
			case StoreType.Physical:
				certificateStoreOptions |= CapiNativeMethods.CertificateStoreOptions.LocalMachine;
				return CertificateStore.StoreOpen(CapiNativeMethods.CertificateStoreProvider.Physical, certificateStoreOptions, path);
			default:
				throw new ArgumentException(type.ToString(), "type");
			}
		}

		// Token: 0x06003A22 RID: 14882 RVA: 0x00093C1D File Offset: 0x00091E1D
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x06003A23 RID: 14883 RVA: 0x00093C28 File Offset: 0x00091E28
		public int Bookmark
		{
			get
			{
				if (this.changed != null && this.changed.WaitOne(0, false))
				{
					this.version++;
					IntPtr intPtr = this.changed.SafeWaitHandle.DangerousGetHandle();
					using (SafeCertStoreHandle safeCertStoreHandle = SafeCertStoreHandle.Clone(this.store))
					{
						if (!CapiNativeMethods.CertControlStore(safeCertStoreHandle, 0U, CapiNativeMethods.StoreControl.Resync, ref intPtr))
						{
							int lastWin32Error = Marshal.GetLastWin32Error();
							ExTraceGlobals.CertificateTracer.TraceError<int>(0L, "Attempt to reset event notification on Certificate store failed with 0x{0:X}", lastWin32Error);
						}
					}
				}
				return this.version;
			}
		}

		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x06003A24 RID: 14884 RVA: 0x00093CC0 File Offset: 0x00091EC0
		public X509Store BaseStore
		{
			get
			{
				return this.store;
			}
		}

		// Token: 0x06003A25 RID: 14885 RVA: 0x00093CC8 File Offset: 0x00091EC8
		public void Open(OpenFlags flags)
		{
			this.changed = new EventWaitHandle(false, EventResetMode.AutoReset);
			if (this.storeType == StoreType.System)
			{
				this.store.Open(flags);
			}
			else
			{
				this.store = CertificateStore.Open(this.storeType, this.serviceStoreName, flags);
			}
			IntPtr intPtr = this.changed.SafeWaitHandle.DangerousGetHandle();
			using (SafeCertStoreHandle safeCertStoreHandle = SafeCertStoreHandle.Clone(this.store))
			{
				if (!CapiNativeMethods.CertControlStore(safeCertStoreHandle, 0U, CapiNativeMethods.StoreControl.NotifiyChange, ref intPtr))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					ExTraceGlobals.CertificateTracer.TraceError<int>(0L, "Attempt to set event notification on Certificate store failed with 0x{0:X}", lastWin32Error);
				}
			}
		}

		// Token: 0x06003A26 RID: 14886 RVA: 0x00093D70 File Offset: 0x00091F70
		public void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003A27 RID: 14887 RVA: 0x00093D80 File Offset: 0x00091F80
		private void Dispose(bool disposing)
		{
			if (disposing && this.store != null)
			{
				if (this.changed != null)
				{
					IntPtr intPtr = this.changed.SafeWaitHandle.DangerousGetHandle();
					using (SafeCertStoreHandle safeCertStoreHandle = SafeCertStoreHandle.Clone(this.store))
					{
						CapiNativeMethods.CertControlStore(safeCertStoreHandle, 0U, CapiNativeMethods.StoreControl.CancelNotifyChange, ref intPtr);
					}
					this.changed.Close();
					this.changed = null;
				}
				this.store.Close();
				this.store = null;
			}
		}

		// Token: 0x06003A28 RID: 14888 RVA: 0x00093E08 File Offset: 0x00092008
		private static X509Store StoreOpen(CapiNativeMethods.CertificateStoreProvider provider, CapiNativeMethods.CertificateStoreOptions options, string path)
		{
			X509Store result;
			using (SafeCertStoreHandle safeCertStoreHandle = CapiNativeMethods.CertOpenStore((IntPtr)((int)provider), CapiNativeMethods.EncodeType.X509Asn | CapiNativeMethods.EncodeType.Pkcs7Asn, IntPtr.Zero, options, path))
			{
				if (safeCertStoreHandle.IsInvalid)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error == 2)
					{
						throw new ArgumentOutOfRangeException(path, "path");
					}
					throw new CryptographicException(lastWin32Error);
				}
				else
				{
					result = new X509Store(safeCertStoreHandle.DangerousGetHandle());
				}
			}
			return result;
		}

		// Token: 0x04003254 RID: 12884
		private string serviceStoreName;

		// Token: 0x04003255 RID: 12885
		private StoreType storeType;

		// Token: 0x04003256 RID: 12886
		private X509Store store;

		// Token: 0x04003257 RID: 12887
		private int version;

		// Token: 0x04003258 RID: 12888
		private EventWaitHandle changed;
	}
}
