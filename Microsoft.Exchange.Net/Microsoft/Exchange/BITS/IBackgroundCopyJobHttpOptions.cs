using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.BITS
{
	// Token: 0x02000657 RID: 1623
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("F1BD1079-9F01-4BDC-8036-F09B70095066")]
	[ComImport]
	internal interface IBackgroundCopyJobHttpOptions
	{
		// Token: 0x06001DB7 RID: 7607
		void SetClientCertificateByID(BG_CERT_STORE_LOCATION StoreLocation, [MarshalAs(UnmanagedType.LPWStr)] [In] string StoreName, [In] IntPtr pCertHashBlob);

		// Token: 0x06001DB8 RID: 7608
		void SetClientCertificateByName(BG_CERT_STORE_LOCATION StoreLocation, [MarshalAs(UnmanagedType.LPWStr)] [In] string StoreName, [MarshalAs(UnmanagedType.LPWStr)] [In] string SubjectName);

		// Token: 0x06001DB9 RID: 7609
		void RemoveClientCertificate();

		// Token: 0x06001DBA RID: 7610
		void GetClientCertificate(out BG_CERT_STORE_LOCATION pStoreLocation, [MarshalAs(UnmanagedType.LPWStr)] out string pStoreName, out IntPtr ppCertHashBlob, [MarshalAs(UnmanagedType.LPWStr)] out string pSubjectName);

		// Token: 0x06001DBB RID: 7611
		void SetCustomHeaders([MarshalAs(UnmanagedType.LPWStr)] [In] string RequestHeaders);

		// Token: 0x06001DBC RID: 7612
		void GetCustomHeaders([MarshalAs(UnmanagedType.LPWStr)] out string pRequestHeaders);

		// Token: 0x06001DBD RID: 7613
		void SetSecurityFlags(ulong Flags);

		// Token: 0x06001DBE RID: 7614
		void GetSecurityFlags(out ulong pFlags);
	}
}
