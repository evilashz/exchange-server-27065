using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.MsgStorage.Internal
{
	// Token: 0x02000096 RID: 150
	internal class ComStorage : IDisposable
	{
		// Token: 0x060004DE RID: 1246 RVA: 0x00016B3A File Offset: 0x00014D3A
		private ComStorage(Interop.IStorage iStorage)
		{
			this.iStorage = iStorage;
			this.isDisposed = false;
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x00016B50 File Offset: 0x00014D50
		// (set) Token: 0x060004E0 RID: 1248 RVA: 0x00016B98 File Offset: 0x00014D98
		public Guid StorageClass
		{
			get
			{
				this.CheckDisposed("ComStorage::get_Class");
				System.Runtime.InteropServices.ComTypes.STATSTG statstg;
				this.GetStat(out statstg);
				return statstg.clsid;
			}
			set
			{
				this.CheckDisposed("ComStorage::set_Class");
				Util.InvokeComCall(MsgStorageErrorCode.FailedWrite, delegate
				{
					this.iStorage.SetClass(ref value);
				});
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x00016BD6 File Offset: 0x00014DD6
		protected Interop.IStorage IStorage
		{
			get
			{
				this.CheckDisposed("ComStorage::get_Storage");
				return this.iStorage;
			}
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00016BEC File Offset: 0x00014DEC
		public static ComStorage CreateFileStorage(string filename, ComStorage.OpenMode openMode)
		{
			Util.ThrowOnNullArgument(filename, "filename");
			ComStorage.CheckOpenMode(openMode, "openMode", ComStorage.validCreateModes);
			object obj = null;
			ComStorage result;
			try
			{
				Guid iidistorage = Interop.IIDIStorage;
				int num = Interop.StgCreateStorageEx(filename, (uint)openMode, 0, 0U, IntPtr.Zero, IntPtr.Zero, ref iidistorage, out obj);
				if (num != 0)
				{
					throw new MsgStorageException(MsgStorageErrorCode.CreateFileFailed, MsgStorageStrings.FailedCreateStorage(filename), num);
				}
				Interop.IStorage storage = obj as Interop.IStorage;
				if (storage == null)
				{
					throw new MsgStorageException(MsgStorageErrorCode.CreateFileFailed, MsgStorageStrings.FailedCreateStorage(filename));
				}
				obj = null;
				result = new ComStorage(storage);
			}
			finally
			{
				if (obj != null)
				{
					Marshal.ReleaseComObject(obj);
				}
			}
			return result;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00016C88 File Offset: 0x00014E88
		public static ComStorage CreateStorageOnStream(Stream stream, ComStorage.OpenMode openMode)
		{
			Util.ThrowOnNullArgument(stream, "stream");
			ComStorage.CheckOpenMode(openMode, "openMode", ComStorage.validCreateModes);
			ComStorage comStorage = null;
			Interop.IStorage storage = null;
			try
			{
				LockBytesOnStream lockBytes = new LockBytesOnStream(stream);
				int num = Interop.StgCreateDocfileOnILockBytes(lockBytes, (uint)openMode, 0, out storage);
				if (num != 0)
				{
					throw new MsgStorageException(MsgStorageErrorCode.CreateStorageOnStreamFailed, MsgStorageStrings.FailedCreateStorage("ILockBytes"), num);
				}
				comStorage = new ComStorage(storage);
			}
			finally
			{
				if (comStorage == null && storage != null)
				{
					Marshal.ReleaseComObject(storage);
				}
			}
			return comStorage;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00016D04 File Offset: 0x00014F04
		public static ComStorage OpenFileStorage(string filename, ComStorage.OpenMode openMode)
		{
			Util.ThrowOnNullArgument(filename, "filename");
			ComStorage.CheckOpenMode(openMode, "openMode", ComStorage.validOpenModes);
			object obj = null;
			ComStorage result;
			try
			{
				Guid iidistorage = Interop.IIDIStorage;
				int num = Interop.StgOpenStorageEx(filename, (uint)openMode, 0, 0U, IntPtr.Zero, IntPtr.Zero, ref iidistorage, out obj);
				if (num != 0)
				{
					throw new MsgStorageException(MsgStorageErrorCode.OpenStorageFileFailed, MsgStorageStrings.FailedOpenStorage(filename), num);
				}
				Interop.IStorage storage = obj as Interop.IStorage;
				if (storage == null)
				{
					throw new MsgStorageException(MsgStorageErrorCode.OpenStorageFileFailed, MsgStorageStrings.FailedOpenStorage(filename));
				}
				obj = null;
				result = new ComStorage(storage);
			}
			finally
			{
				if (obj != null)
				{
					Marshal.ReleaseComObject(obj);
				}
			}
			return result;
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00016DA0 File Offset: 0x00014FA0
		public static ComStorage OpenStorageOnStream(Stream stream, ComStorage.OpenMode openMode)
		{
			Util.ThrowOnNullArgument(stream, "stream");
			ComStorage.CheckOpenMode(openMode, "openMode", ComStorage.validOpenModes);
			ComStorage comStorage = null;
			Interop.IStorage storage = null;
			try
			{
				LockBytesOnStream lockBytes = new LockBytesOnStream(stream);
				int num = Interop.StgOpenStorageOnILockBytes(lockBytes, IntPtr.Zero, (uint)openMode, IntPtr.Zero, 0U, out storage);
				if (num != 0)
				{
					throw new MsgStorageException(MsgStorageErrorCode.OpenStorageOnStreamFailed, MsgStorageStrings.FailedOpenStorage("ILockBytes"), num);
				}
				comStorage = new ComStorage(storage);
			}
			finally
			{
				if (comStorage == null && storage != null)
				{
					Marshal.ReleaseComObject(storage);
				}
			}
			return comStorage;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00016E54 File Offset: 0x00015054
		public static void CopyStorageContent(ComStorage source, ComStorage destination)
		{
			Util.ThrowOnNullArgument(source, "source");
			Util.ThrowOnNullArgument(destination, "destination");
			Util.InvokeComCall(MsgStorageErrorCode.FailedCopyStorage, delegate
			{
				source.IStorage.CopyTo(0U, null, IntPtr.Zero, destination.IStorage);
			});
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00016ED8 File Offset: 0x000150D8
		public ComStream CreateStream(string streamName, ComStorage.OpenMode openMode)
		{
			this.CheckDisposed("ComStorage::CreateStream");
			Util.ThrowOnNullArgument(streamName, "streamName");
			ComStorage.CheckOpenMode(openMode, "openMode", ComStorage.validCreateModes);
			Interop.IStream iStream = null;
			Util.InvokeComCall(MsgStorageErrorCode.FailedCreateStream, delegate
			{
				this.iStorage.CreateStream(streamName, (uint)openMode, 0U, 0U, out iStream);
			});
			return new ComStream(iStream);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00016F88 File Offset: 0x00015188
		public ComStream OpenStream(string streamName, ComStorage.OpenMode openMode)
		{
			this.CheckDisposed("ComStorage::OpenStream");
			Util.ThrowOnNullArgument(streamName, "streamName");
			ComStorage.CheckOpenMode(openMode, "openMode", ComStorage.validOpenModes);
			Interop.IStream iStream = null;
			Util.InvokeComCall(MsgStorageErrorCode.FailedOpenStream, delegate
			{
				this.iStorage.OpenStream(streamName, IntPtr.Zero, (uint)openMode, 0U, out iStream);
			});
			return new ComStream(iStream);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00017034 File Offset: 0x00015234
		public ComStorage CreateStorage(string storageName, ComStorage.OpenMode mode)
		{
			this.CheckDisposed("ComStorage::CreateStorage");
			Util.ThrowOnNullArgument(storageName, "storageName");
			ComStorage.CheckOpenMode(mode, "mode", ComStorage.validCreateModes);
			Interop.IStorage iNewStorage = null;
			Util.InvokeComCall(MsgStorageErrorCode.FailedCreateSubstorage, delegate
			{
				this.iStorage.CreateStorage(storageName, (uint)mode, 0U, 0U, out iNewStorage);
			});
			return new ComStorage(iNewStorage);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x000170E4 File Offset: 0x000152E4
		public ComStorage OpenStorage(string storageName, ComStorage.OpenMode mode)
		{
			this.CheckDisposed("ComStorage::OpenStorage");
			Util.ThrowOnNullArgument(storageName, "storageName");
			ComStorage.CheckOpenMode(mode, "mode", ComStorage.validOpenModes);
			Interop.IStorage iOpenStorage = null;
			Util.InvokeComCall(MsgStorageErrorCode.FailedOpenSubstorage, delegate
			{
				this.iStorage.OpenStorage(storageName, null, (uint)mode, IntPtr.Zero, 0U, out iOpenStorage);
			});
			return new ComStorage(iOpenStorage);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00017194 File Offset: 0x00015394
		public void CopyElement(string sourceElementName, ComStorage targetStorage, string targetElementName)
		{
			this.CheckDisposed("ComStorage::OpenStorage");
			Util.ThrowOnNullArgument(sourceElementName, "sourceElementName");
			Util.ThrowOnNullArgument(targetStorage, "targetStorage");
			Util.ThrowOnNullArgument(targetElementName, "targetElementName");
			Util.InvokeComCall(MsgStorageErrorCode.FailedCopyStorage, delegate
			{
				this.iStorage.MoveElementTo(sourceElementName, targetStorage.IStorage, targetElementName, 1U);
			});
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00017244 File Offset: 0x00015444
		public void MoveElement(string sourceElementName, ComStorage targetStorage, string targetElementName)
		{
			this.CheckDisposed("ComStorage::OpenStorage");
			Util.ThrowOnNullArgument(sourceElementName, "sourceElementName");
			Util.ThrowOnNullArgument(targetStorage, "targetStorage");
			Util.ThrowOnNullArgument(targetElementName, "targetElementName");
			Util.InvokeComCall(MsgStorageErrorCode.FailedCopyStorage, delegate
			{
				this.iStorage.MoveElementTo(sourceElementName, targetStorage.IStorage, targetElementName, 0U);
			});
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x000172CF File Offset: 0x000154CF
		public void Flush()
		{
			this.CheckDisposed("ComStorage::Flush");
			Util.InvokeComCall(MsgStorageErrorCode.FailedWrite, delegate
			{
				this.iStorage.Commit(0U);
			});
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00017310 File Offset: 0x00015510
		public void GetStat(out System.Runtime.InteropServices.ComTypes.STATSTG statStg)
		{
			this.CheckDisposed("ComStorage::GetStat");
			System.Runtime.InteropServices.ComTypes.STATSTG internalStatStg = default(System.Runtime.InteropServices.ComTypes.STATSTG);
			Util.InvokeComCall(MsgStorageErrorCode.FailedRead, delegate
			{
				this.iStorage.Stat(out internalStatStg, 1U);
			});
			statStg = internalStatStg;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0001735F File Offset: 0x0001555F
		public void WriteBytesToStream(string streamName, byte[] data)
		{
			this.WriteBytesToStream(streamName, data, data.Length);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00017410 File Offset: 0x00015610
		public unsafe void WriteBytesToStream(string streamName, byte[] data, int length)
		{
			this.CheckDisposed("ComStream::WriteToStream");
			Util.ThrowOnNullArgument(streamName, "streamName");
			Util.ThrowOnNullArgument(data, "data");
			Util.InvokeComCall(MsgStorageErrorCode.FailedWrite, delegate
			{
				Interop.IStream stream = null;
				try
				{
					int num = 0;
					this.iStorage.CreateStream(streamName, 17U, 0U, 0U, out stream);
					if (length != 0)
					{
						try
						{
							fixed (byte* ptr = &data[0])
							{
								stream.Write(ptr, length, out num);
							}
						}
						finally
						{
							byte* ptr = null;
						}
					}
					if (num != length)
					{
						throw new MsgStorageException(MsgStorageErrorCode.FailedWrite, MsgStorageStrings.FailedWrite(streamName));
					}
				}
				finally
				{
					if (stream != null)
					{
						Marshal.ReleaseComObject(stream);
					}
				}
			});
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00017510 File Offset: 0x00015710
		public unsafe int ReadFromStream(string streamName, byte[] buffer, int size)
		{
			this.CheckDisposed("ComStream::ReadFromStream");
			Util.ThrowOnNullArgument(streamName, "streamName");
			Util.ThrowOnNullArgument(buffer, "buffer");
			Util.ThrowOnOutOfRange(size >= 0, "size");
			int result = 0;
			Util.InvokeComCall(MsgStorageErrorCode.FailedRead, delegate
			{
				Interop.IStream stream = null;
				try
				{
					this.iStorage.OpenStream(streamName, IntPtr.Zero, 16U, 0U, out stream);
					int result = 0;
					if (size != 0)
					{
						try
						{
							fixed (byte* ptr = &buffer[0])
							{
								stream.Read(ptr, size, out result);
							}
						}
						finally
						{
							byte* ptr = null;
						}
					}
					result = result;
				}
				finally
				{
					if (stream != null)
					{
						Marshal.ReleaseComObject(stream);
					}
				}
			});
			return result;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0001769C File Offset: 0x0001589C
		public unsafe byte[] ReadFromStreamMaxLength(string streamName, int maxSize)
		{
			byte[] result = null;
			Util.InvokeComCall(MsgStorageErrorCode.FailedRead, delegate
			{
				Interop.IStream stream = null;
				try
				{
					this.iStorage.OpenStream(streamName, IntPtr.Zero, 16U, 0U, out stream);
					System.Runtime.InteropServices.ComTypes.STATSTG statstg;
					stream.Stat(out statstg, 1U);
					if (statstg.cbSize > (long)maxSize)
					{
						throw new MsgStorageException(MsgStorageErrorCode.StorageStreamTooLong, MsgStorageStrings.StreamTooBig(streamName, statstg.cbSize));
					}
					int num = (int)statstg.cbSize;
					int num2 = 0;
					result = new byte[num];
					if (result.Length != 0)
					{
						try
						{
							fixed (byte* ptr = &result[0])
							{
								stream.Read(ptr, result.Length, out num2);
							}
						}
						finally
						{
							byte* ptr = null;
						}
					}
					if (num2 != result.Length)
					{
						throw new MsgStorageException(MsgStorageErrorCode.StorageStreamTruncated, MsgStorageStrings.FailedRead(streamName));
					}
				}
				finally
				{
					if (stream != null)
					{
						Marshal.ReleaseComObject(stream);
					}
				}
			});
			return result;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x000176E3 File Offset: 0x000158E3
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x000176F2 File Offset: 0x000158F2
		protected void CheckDisposed(string methodName)
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException(methodName);
			}
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00017703 File Offset: 0x00015903
		protected virtual void InternalDispose(bool disposing)
		{
			if (disposing && this.iStorage != null)
			{
				Marshal.ReleaseComObject(this.iStorage);
			}
			this.iStorage = null;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00017724 File Offset: 0x00015924
		private static void CheckOpenMode(ComStorage.OpenMode value, string paramName, params ComStorage.OpenMode[] validValues)
		{
			if (validValues.Length != 0)
			{
				for (int num = 0; num != validValues.Length; num++)
				{
					if (value == validValues[num])
					{
						return;
					}
				}
				string message = string.Format("Invalid parameter value: {0}", value.ToString());
				throw new ArgumentOutOfRangeException(paramName, message);
			}
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00017769 File Offset: 0x00015969
		private void Dispose(bool disposing)
		{
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				this.InternalDispose(disposing);
			}
		}

		// Token: 0x04000511 RID: 1297
		private static ComStorage.OpenMode[] validCreateModes = new ComStorage.OpenMode[]
		{
			ComStorage.OpenMode.ReadWrite,
			ComStorage.OpenMode.Write,
			(ComStorage.OpenMode)4114,
			ComStorage.OpenMode.CreateWrite
		};

		// Token: 0x04000512 RID: 1298
		private static ComStorage.OpenMode[] validOpenModes = new ComStorage.OpenMode[]
		{
			ComStorage.OpenMode.Read,
			ComStorage.OpenMode.ReadWrite,
			ComStorage.OpenMode.Write
		};

		// Token: 0x04000513 RID: 1299
		private Interop.IStorage iStorage;

		// Token: 0x04000514 RID: 1300
		private bool isDisposed;

		// Token: 0x02000097 RID: 151
		[Flags]
		public enum OpenMode
		{
			// Token: 0x04000516 RID: 1302
			Read = 16,
			// Token: 0x04000517 RID: 1303
			Write = 17,
			// Token: 0x04000518 RID: 1304
			ReadWrite = 18,
			// Token: 0x04000519 RID: 1305
			Create = 4096,
			// Token: 0x0400051A RID: 1306
			CreateWrite = 4113
		}
	}
}
