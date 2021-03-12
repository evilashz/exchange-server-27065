using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;

namespace Microsoft.Exchange.Data.MsgStorage.Internal
{
	// Token: 0x02000099 RID: 153
	internal static class Interop
	{
		// Token: 0x0600050D RID: 1293
		[DllImport("ole32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int StgOpenStorageEx([MarshalAs(UnmanagedType.LPWStr)] [In] string fileName, [In] uint grfMode, [In] int stgfmt, [In] uint grfAttrs, [In] IntPtr stgOptions, [In] IntPtr reserved2, [In] ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object result);

		// Token: 0x0600050E RID: 1294
		[DllImport("ole32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int StgCreateStorageEx([MarshalAs(UnmanagedType.LPWStr)] [In] string fileName, [In] uint grfMode, [In] int stgfmt, [In] uint grfAttrs, [In] IntPtr stgOptions, [In] IntPtr reserved2, [In] ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object result);

		// Token: 0x0600050F RID: 1295
		[DllImport("ole32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int StgOpenStorageOnILockBytes([MarshalAs(UnmanagedType.Interface)] [In] Interop.ILockBytes lockBytes, [In] IntPtr pStgPriority, [In] uint grfMode, [In] IntPtr snbExclude, [In] uint reserved, [MarshalAs(UnmanagedType.Interface)] out Interop.IStorage newStorage);

		// Token: 0x06000510 RID: 1296
		[DllImport("ole32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int StgCreateDocfileOnILockBytes([MarshalAs(UnmanagedType.Interface)] [In] Interop.ILockBytes lockBytes, [In] uint grfMode, [In] int reserved, [MarshalAs(UnmanagedType.Interface)] out Interop.IStorage newStorage);

		// Token: 0x0400051D RID: 1309
		public static Guid IIDIStorage = new Guid("0000000B-0000-0000-C000-000000000046");

		// Token: 0x0200009A RID: 154
		[Guid("0000000C-0000-0000-C000-000000000046")]
		[SuppressUnmanagedCodeSecurity]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IStream
		{
			// Token: 0x06000512 RID: 1298
			[MethodImpl(MethodImplOptions.InternalCall)]
			unsafe void Read([In] byte* buffer, [In] int bufferSize, out int bytesRead);

			// Token: 0x06000513 RID: 1299
			[MethodImpl(MethodImplOptions.InternalCall)]
			unsafe void Write([In] byte* buffer, [In] int bufferSize, out int bytesWritten);

			// Token: 0x06000514 RID: 1300
			[MethodImpl(MethodImplOptions.InternalCall)]
			void Seek([In] long offset, [In] int origin, out long newPosition);

			// Token: 0x06000515 RID: 1301
			[MethodImpl(MethodImplOptions.InternalCall)]
			void SetSize([In] long newSize);

			// Token: 0x06000516 RID: 1302
			[MethodImpl(MethodImplOptions.InternalCall)]
			void CopyTo([MarshalAs(UnmanagedType.Interface)] [In] Interop.IStream pstm, [In] long bytesToCopy, out long bytesRead, out long pcbWritten);

			// Token: 0x06000517 RID: 1303
			[MethodImpl(MethodImplOptions.InternalCall)]
			void Commit([In] uint commitFlags);

			// Token: 0x06000518 RID: 1304
			[MethodImpl(MethodImplOptions.InternalCall)]
			void Revert();

			// Token: 0x06000519 RID: 1305
			[MethodImpl(MethodImplOptions.InternalCall)]
			void LockRegion([In] long offset, [In] long size, [In] int lockType);

			// Token: 0x0600051A RID: 1306
			[MethodImpl(MethodImplOptions.InternalCall)]
			void UnlockRegion([In] ulong offset, [In] ulong size, [In] uint lockType);

			// Token: 0x0600051B RID: 1307
			[MethodImpl(MethodImplOptions.InternalCall)]
			void Stat(out System.Runtime.InteropServices.ComTypes.STATSTG statStg, [In] uint statFlag);

			// Token: 0x0600051C RID: 1308
			[MethodImpl(MethodImplOptions.InternalCall)]
			void Clone([MarshalAs(UnmanagedType.Interface)] out Interop.IStream newCopy);
		}

		// Token: 0x0200009B RID: 155
		[Guid("0000000D-0000-0000-C000-000000000046")]
		[SuppressUnmanagedCodeSecurity]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IEnumStatStg
		{
			// Token: 0x0600051D RID: 1309
			[MethodImpl(MethodImplOptions.InternalCall)]
			void Next([In] int celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] System.Runtime.InteropServices.ComTypes.STATSTG[] rgelt, out int pceltFetched);

			// Token: 0x0600051E RID: 1310
			[MethodImpl(MethodImplOptions.InternalCall)]
			void Skip([In] int celt);

			// Token: 0x0600051F RID: 1311
			[MethodImpl(MethodImplOptions.InternalCall)]
			void Reset();

			// Token: 0x06000520 RID: 1312
			[MethodImpl(MethodImplOptions.InternalCall)]
			void Clone([MarshalAs(UnmanagedType.Interface)] out Interop.IEnumStatStg ppenum);
		}

		// Token: 0x0200009C RID: 156
		[SuppressUnmanagedCodeSecurity]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("0000000B-0000-0000-C000-000000000046")]
		[ComImport]
		public interface IStorage
		{
			// Token: 0x06000521 RID: 1313
			[MethodImpl(MethodImplOptions.InternalCall)]
			void CreateStream([MarshalAs(UnmanagedType.LPWStr)] [In] string pwcsName, [In] uint grfMode, [In] uint reserved1, [In] uint reserved2, [MarshalAs(UnmanagedType.Interface)] out Interop.IStream ppstm);

			// Token: 0x06000522 RID: 1314
			[MethodImpl(MethodImplOptions.InternalCall)]
			void OpenStream([MarshalAs(UnmanagedType.LPWStr)] [In] string pwcsName, [In] IntPtr reserved1, [In] uint grfMode, [In] uint reserved2, [MarshalAs(UnmanagedType.Interface)] out Interop.IStream ppstm);

			// Token: 0x06000523 RID: 1315
			[MethodImpl(MethodImplOptions.InternalCall)]
			void CreateStorage([MarshalAs(UnmanagedType.LPWStr)] [In] string pwcsName, [In] uint grfMode, [In] uint reserved1, [In] uint reserved2, [MarshalAs(UnmanagedType.Interface)] out Interop.IStorage ppstg);

			// Token: 0x06000524 RID: 1316
			[MethodImpl(MethodImplOptions.InternalCall)]
			void OpenStorage([MarshalAs(UnmanagedType.LPWStr)] [In] string pwcsName, [MarshalAs(UnmanagedType.Interface)] [In] Interop.IStorage pstgPriority, [In] uint grfMode, [In] IntPtr snbExclude, [In] uint reserved, [MarshalAs(UnmanagedType.Interface)] out Interop.IStorage ppstg);

			// Token: 0x06000525 RID: 1317
			[MethodImpl(MethodImplOptions.InternalCall)]
			void CopyTo([In] uint ciidExclude, [MarshalAs(UnmanagedType.LPArray)] [In] Guid[] rgiidExclude, [In] IntPtr snbExclude, [MarshalAs(UnmanagedType.Interface)] [In] Interop.IStorage pstgDest);

			// Token: 0x06000526 RID: 1318
			[MethodImpl(MethodImplOptions.InternalCall)]
			void MoveElementTo([MarshalAs(UnmanagedType.LPWStr)] [In] string pwcsName, [MarshalAs(UnmanagedType.Interface)] [In] Interop.IStorage pstgDest, [MarshalAs(UnmanagedType.LPWStr)] [In] string pwcsNewName, [In] uint grfFlags);

			// Token: 0x06000527 RID: 1319
			[MethodImpl(MethodImplOptions.InternalCall)]
			void Commit([In] uint grfCommitFlags);

			// Token: 0x06000528 RID: 1320
			[MethodImpl(MethodImplOptions.InternalCall)]
			void Revert();

			// Token: 0x06000529 RID: 1321
			[MethodImpl(MethodImplOptions.InternalCall)]
			void EnumElements([In] uint reserved1, [In] IntPtr reserved2, [In] uint reserved3, [MarshalAs(UnmanagedType.Interface)] out Interop.IEnumStatStg ppenum);

			// Token: 0x0600052A RID: 1322
			[MethodImpl(MethodImplOptions.InternalCall)]
			void DestroyElement([MarshalAs(UnmanagedType.LPWStr)] [In] string pwcsName);

			// Token: 0x0600052B RID: 1323
			[MethodImpl(MethodImplOptions.InternalCall)]
			void RenameElement([MarshalAs(UnmanagedType.LPWStr)] [In] string pwcsOldName, [MarshalAs(UnmanagedType.LPWStr)] [In] string pwcsNewName);

			// Token: 0x0600052C RID: 1324
			[MethodImpl(MethodImplOptions.InternalCall)]
			void SetElementTimes([MarshalAs(UnmanagedType.LPWStr)] [In] string pwcsName, [In] System.Runtime.InteropServices.ComTypes.FILETIME pctime, [In] System.Runtime.InteropServices.ComTypes.FILETIME patime, [In] System.Runtime.InteropServices.ComTypes.FILETIME pmtime);

			// Token: 0x0600052D RID: 1325
			[MethodImpl(MethodImplOptions.InternalCall)]
			void SetClass(ref Guid clsid);

			// Token: 0x0600052E RID: 1326
			[MethodImpl(MethodImplOptions.InternalCall)]
			void SetStateBits([In] uint grfStateBits, [In] uint grfMask);

			// Token: 0x0600052F RID: 1327
			[MethodImpl(MethodImplOptions.InternalCall)]
			void Stat(out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, [In] uint grfStatFlag);
		}

		// Token: 0x0200009D RID: 157
		[Guid("0000000A-0000-0000-C000-000000000046")]
		[SuppressUnmanagedCodeSecurity]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface ILockBytes
		{
			// Token: 0x06000530 RID: 1328
			[MethodImpl(MethodImplOptions.InternalCall)]
			void ReadAt([In] long offset, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [Out] byte[] buffer, [In] int bufferSize, out int bytesRead);

			// Token: 0x06000531 RID: 1329
			[MethodImpl(MethodImplOptions.InternalCall)]
			void WriteAt([In] long offset, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [In] byte[] buffer, [In] int bufferSize, out int pcbWritten);

			// Token: 0x06000532 RID: 1330
			[MethodImpl(MethodImplOptions.InternalCall)]
			void Flush();

			// Token: 0x06000533 RID: 1331
			[MethodImpl(MethodImplOptions.InternalCall)]
			void SetSize([In] long newSize);

			// Token: 0x06000534 RID: 1332
			[MethodImpl(MethodImplOptions.InternalCall)]
			void LockRegion([In] long offset, [In] long length, [In] uint lockType);

			// Token: 0x06000535 RID: 1333
			[MethodImpl(MethodImplOptions.InternalCall)]
			void UnlockRegion([In] long offset, [In] long length, [In] int lockType);

			// Token: 0x06000536 RID: 1334
			[MethodImpl(MethodImplOptions.InternalCall)]
			void Stat(out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, [In] uint statFlag);
		}

		// Token: 0x0200009E RID: 158
		[Flags]
		internal enum StorageOpenMode : uint
		{
			// Token: 0x0400051F RID: 1311
			Read = 0U,
			// Token: 0x04000520 RID: 1312
			Write = 1U,
			// Token: 0x04000521 RID: 1313
			ReadWrite = 2U,
			// Token: 0x04000522 RID: 1314
			ShareExclusive = 16U,
			// Token: 0x04000523 RID: 1315
			ShareDenyWrite = 32U,
			// Token: 0x04000524 RID: 1316
			ShareDenyRead = 48U,
			// Token: 0x04000525 RID: 1317
			ShareDenyNone = 64U,
			// Token: 0x04000526 RID: 1318
			Priority = 262144U,
			// Token: 0x04000527 RID: 1319
			Create = 4096U,
			// Token: 0x04000528 RID: 1320
			Convert = 131072U,
			// Token: 0x04000529 RID: 1321
			FailIfThere = 0U,
			// Token: 0x0400052A RID: 1322
			Direct = 0U,
			// Token: 0x0400052B RID: 1323
			Transacted = 65536U,
			// Token: 0x0400052C RID: 1324
			NoScratch = 1048576U,
			// Token: 0x0400052D RID: 1325
			NoSnapshot = 2097152U,
			// Token: 0x0400052E RID: 1326
			Simple = 134217728U,
			// Token: 0x0400052F RID: 1327
			DirectSWMR = 4194304U,
			// Token: 0x04000530 RID: 1328
			DeleteOnRelease = 67108864U
		}

		// Token: 0x0200009F RID: 159
		internal enum STGTY
		{
			// Token: 0x04000532 RID: 1330
			Storage = 1,
			// Token: 0x04000533 RID: 1331
			Stream,
			// Token: 0x04000534 RID: 1332
			LockBytes,
			// Token: 0x04000535 RID: 1333
			Property
		}

		// Token: 0x020000A0 RID: 160
		internal enum MoveCopyMode
		{
			// Token: 0x04000537 RID: 1335
			Move,
			// Token: 0x04000538 RID: 1336
			Copy
		}

		// Token: 0x020000A1 RID: 161
		internal enum StatFlags
		{
			// Token: 0x0400053A RID: 1338
			Default,
			// Token: 0x0400053B RID: 1339
			NoName
		}

		// Token: 0x020000A2 RID: 162
		internal enum StorageFormat
		{
			// Token: 0x0400053D RID: 1341
			Storage,
			// Token: 0x0400053E RID: 1342
			File = 3,
			// Token: 0x0400053F RID: 1343
			Any,
			// Token: 0x04000540 RID: 1344
			DocFile
		}
	}
}
