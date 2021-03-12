using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020001BB RID: 443
	internal class EseDatabasePatchFileIO : DisposeTrackableBase
	{
		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001139 RID: 4409 RVA: 0x000465C2 File Offset: 0x000447C2
		private static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.IncrementalReseederTracer;
			}
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x000465C9 File Offset: 0x000447C9
		public static EseDatabasePatchFileIO OpenRead(string edbFilePath)
		{
			return new EseDatabasePatchFileIO(edbFilePath, null, true, false);
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x000465D4 File Offset: 0x000447D4
		public static EseDatabasePatchFileIO OpenWrite(string edbFilePath, EseDatabasePatchState header)
		{
			if (header == null)
			{
				throw new ArgumentNullException("header");
			}
			return new EseDatabasePatchFileIO(edbFilePath, header, false, false);
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x000465F0 File Offset: 0x000447F0
		public static void GetNames(string edbFilePath, out string doneName, out string inprogressName)
		{
			using (EseDatabasePatchFileIO eseDatabasePatchFileIO = new EseDatabasePatchFileIO(edbFilePath, null, true, true))
			{
				doneName = eseDatabasePatchFileIO.m_patchFileNameDone;
				inprogressName = eseDatabasePatchFileIO.m_patchFileNameInProgress;
			}
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x00046634 File Offset: 0x00044834
		public static bool IsLegacyPatchFilePresent(string edbFilePath)
		{
			string path;
			string path2;
			EseDatabasePatchFileIO.GetLegacyNames(edbFilePath, out path, out path2);
			return File.Exists(path) || File.Exists(path2);
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x0004665C File Offset: 0x0004485C
		public static void DeleteAll(string edbFilePath)
		{
			using (EseDatabasePatchFileIO eseDatabasePatchFileIO = new EseDatabasePatchFileIO(edbFilePath, null, true, true))
			{
				EseDatabasePatchFileIO.DeletePatchFile(eseDatabasePatchFileIO.m_patchFileNameInProgress);
				EseDatabasePatchFileIO.DeletePatchFile(eseDatabasePatchFileIO.m_patchFileNameDone);
				string fileFullPath;
				string fileFullPath2;
				EseDatabasePatchFileIO.GetLegacyNames(edbFilePath, out fileFullPath, out fileFullPath2);
				EseDatabasePatchFileIO.DeletePatchFile(fileFullPath2);
				EseDatabasePatchFileIO.DeletePatchFile(fileFullPath);
			}
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x000466BC File Offset: 0x000448BC
		public void FinishWriting()
		{
			this.AssertWriteOperationValid();
			this.m_fileStream.Flush(true);
			this.DisposeFileStream();
			File.Move(this.m_patchFileNameInProgress, this.m_patchFileNameDone);
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001140 RID: 4416 RVA: 0x000466E7 File Offset: 0x000448E7
		public string InProgressName
		{
			get
			{
				return this.m_patchFileNameInProgress;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x000466EF File Offset: 0x000448EF
		public string DoneName
		{
			get
			{
				return this.m_patchFileNameDone;
			}
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x000466F8 File Offset: 0x000448F8
		public EseDatabasePatchState ReadHeader()
		{
			this.AssertReadOperationValid();
			EseDatabasePatchState eseDatabasePatchState = null;
			this.SeekToStart();
			byte[] array = new byte[32768L];
			this.ReadFromFile(array, array.Length);
			int num = 0;
			int num2 = BitConverter.ToInt32(array, num);
			num += 4;
			if (num2 > array.Length - 4 || num2 <= 0)
			{
				EseDatabasePatchFileIO.Tracer.TraceError<int, int>((long)this.GetHashCode(), "ReadHeader(): Serialized header state in bytes ({0}) is invalid. Must be >=0 and < pre-allocated fixed byte size of {1} bytes.", num2, array.Length);
				throw new PagePatchInvalidFileException(this.m_fileStream.Name);
			}
			byte[] array2 = new byte[num2];
			Array.Copy(array, num, array2, 0, num2);
			Exception ex = null;
			try
			{
				eseDatabasePatchState = SerializationServices.Deserialize<EseDatabasePatchState>(array2);
			}
			catch (SerializationException ex2)
			{
				ex = ex2;
			}
			catch (TargetInvocationException ex3)
			{
				ex = ex3;
			}
			catch (DecoderFallbackException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				EseDatabasePatchFileIO.Tracer.TraceError<Exception>((long)this.GetHashCode(), "ReadHeader deserialize failed: {0}", ex);
				throw new PagePatchInvalidFileException(this.m_fileStream.Name, ex);
			}
			this.m_header = eseDatabasePatchState;
			return eseDatabasePatchState;
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x00046808 File Offset: 0x00044A08
		public void WriteHeader()
		{
			this.AssertWriteOperationValid();
			if (this.m_header.NumPagesToPatch > 3276800)
			{
				throw new PagePatchTooManyPagesToPatchException(this.m_header.NumPagesToPatch, 3276800);
			}
			byte[] array = new byte[32768L];
			byte[] array2 = SerializationServices.Serialize(this.m_header);
			DiagCore.RetailAssert(array2.Length <= array.Length - 4, "Serialized header state in bytes ({0}) is greater than pre-allocated fixed byte size of {1} bytes.", new object[]
			{
				array2.Length,
				array.Length
			});
			int num = 0;
			Array.Copy(BitConverter.GetBytes(array2.Length), 0, array, num, 4);
			num += 4;
			Array.Copy(array2, 0, array, num, array2.Length);
			this.SeekToStart();
			this.m_fileStream.Write(array, 0, array.Length);
			this.m_fileStream.Flush(true);
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x00046A4C File Offset: 0x00044C4C
		public IEnumerable<long> ReadPageNumbers()
		{
			this.AssertReadOperationValid();
			if (this.m_header == null)
			{
				this.ReadHeader();
			}
			int numPages = this.m_header.NumPagesToPatch;
			byte[] pgNoBytes = new byte[8];
			for (int i = 0; i < numPages; i++)
			{
				this.ZeroBuffer(pgNoBytes);
				this.SeekToPageListAtIndex(i);
				this.ReadFromFile(pgNoBytes, 8);
				yield return BitConverter.ToInt64(pgNoBytes, 0);
			}
			yield break;
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x00046A6C File Offset: 0x00044C6C
		public void WritePageNumbers(IEnumerable<long> pageNumbers)
		{
			this.AssertWriteOperationValid();
			this.SeekToPageListStart();
			foreach (long value in pageNumbers)
			{
				this.m_fileStream.Write(BitConverter.GetBytes(value), 0, 8);
			}
			this.m_pageNumbersWritten = true;
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x00046AD4 File Offset: 0x00044CD4
		public byte[] ReadPageData(int index)
		{
			this.AssertReadOperationValid();
			if (this.m_header == null)
			{
				this.ReadHeader();
			}
			this.SeekToPageDataAtIndex(index);
			byte[] array = new byte[this.m_header.PageSizeBytes];
			this.ReadFromFile(array, array.Length);
			return array;
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x00046B1C File Offset: 0x00044D1C
		public void WritePageData(byte[] data)
		{
			this.AssertWriteOperationValid();
			DiagCore.AssertOrWatson(this.m_pageNumbersWritten, "WritePageNumbers() should be called first!", new object[0]);
			if (data.LongLength != this.m_header.PageSizeBytes)
			{
				throw new PagePatchInvalidPageSizeException(data.LongLength, this.m_header.PageSizeBytes);
			}
			this.m_fileStream.Write(data, 0, data.Length);
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x00046B80 File Offset: 0x00044D80
		private EseDatabasePatchFileIO(string edbFilePath, EseDatabasePatchState header, bool readOnly, bool skipIO)
		{
			this.m_fReadOnly = readOnly;
			this.m_fSkipIO = skipIO;
			this.m_header = header;
			this.m_patchFileNameInProgress = edbFilePath + ".patch-progress";
			this.m_patchFileNameDone = edbFilePath + ".patch-done";
			if (!skipIO)
			{
				if (this.m_fReadOnly)
				{
					EseDatabasePatchFileIO.Tracer.TraceDebug<string>((long)this.GetHashCode(), "EseDatabasePatchFileIO: Opening the completed patch file in read-only mode: {0}", this.m_patchFileNameDone);
					this.m_fileStream = File.OpenRead(this.m_patchFileNameDone);
					return;
				}
				if (File.Exists(this.m_patchFileNameDone))
				{
					EseDatabasePatchFileIO.Tracer.TraceDebug<string>((long)this.GetHashCode(), "EseDatabasePatchFileIO: Re-opening the completed patch file in write-mode: {0}", this.m_patchFileNameDone);
					this.m_fileStream = File.Open(this.m_patchFileNameDone, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
					return;
				}
				if (File.Exists(this.m_patchFileNameInProgress))
				{
					EseDatabasePatchFileIO.Tracer.TraceDebug<string>((long)this.GetHashCode(), "EseDatabasePatchFileIO: Re-opening the in-progress patch file in write-mode: {0}", this.m_patchFileNameInProgress);
					this.m_fileStream = File.Open(this.m_patchFileNameInProgress, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
					return;
				}
				EseDatabasePatchFileIO.Tracer.TraceDebug<string>((long)this.GetHashCode(), "EseDatabasePatchFileIO: Creating the in-progress patch file: {0}", this.m_patchFileNameInProgress);
				this.m_fileStream = File.Open(this.m_patchFileNameInProgress, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
			}
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x00046CB4 File Offset: 0x00044EB4
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.DisposeFileStream();
			}
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x00046CBF File Offset: 0x00044EBF
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<EseDatabasePatchFileIO>(this);
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x00046CC8 File Offset: 0x00044EC8
		private static void DeletePatchFile(string fileFullPath)
		{
			Exception ex = FileCleanup.Delete(fileFullPath);
			if (ex != null)
			{
				EseDatabasePatchFileIO.Tracer.TraceError<string, Exception>(0L, "Failed to clean up one of the patch files: {0} ; Error: {1}", fileFullPath, ex);
				throw new PagePatchFileDeletionException(fileFullPath, ex.Message, ex);
			}
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x00046D00 File Offset: 0x00044F00
		private static void GetLegacyNames(string edbFilePath, out string oldDoneName, out string oldInProgressName)
		{
			oldDoneName = edbFilePath + ".reseed";
			oldInProgressName = edbFilePath + ".reseed-progress";
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x00046D1C File Offset: 0x00044F1C
		private void SeekToStart()
		{
			this.m_fileStream.Seek(0L, SeekOrigin.Begin);
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x00046D2D File Offset: 0x00044F2D
		private void SeekToPageListStart()
		{
			this.m_fileStream.Seek(32768L, SeekOrigin.Begin);
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x00046D42 File Offset: 0x00044F42
		private void SeekToPageListAtIndex(int index)
		{
			this.SeekToPageListStart();
			this.m_fileStream.Seek((long)(index * 8), SeekOrigin.Current);
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x00046D5B File Offset: 0x00044F5B
		private void SeekToPageDataAtIndex(int index)
		{
			this.SeekToPageListAtIndex(this.m_header.NumPagesToPatch);
			this.m_fileStream.Seek((long)index * this.m_header.PageSizeBytes, SeekOrigin.Current);
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x00046D8C File Offset: 0x00044F8C
		private void ZeroBuffer(byte[] buffer)
		{
			for (int i = 0; i < buffer.Length; i++)
			{
				buffer[i] = 0;
			}
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x00046DAB File Offset: 0x00044FAB
		private void DisposeFileStream()
		{
			if (this.m_fileStream != null && !this.m_fileStreamClosed)
			{
				this.m_fileStream.Dispose();
				this.m_fileStreamClosed = true;
			}
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x00046DD0 File Offset: 0x00044FD0
		private void ReadFromFile(byte[] buffer, int numBytesToRead)
		{
			int num = this.m_fileStream.Read(buffer, 0, numBytesToRead);
			if (num != numBytesToRead)
			{
				throw new PagePatchFileReadException(this.m_fileStream.Name, (long)num, (long)numBytesToRead);
			}
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x00046E05 File Offset: 0x00045005
		private void AssertWriteOperationValid()
		{
			DiagCore.AssertOrWatson(!this.m_fSkipIO, "m_fSkipIO was true but we're doing a write operation?", new object[0]);
			DiagCore.AssertOrWatson(!this.m_fReadOnly, "m_fReadOnly was true but we're doing a write operation?", new object[0]);
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x00046E39 File Offset: 0x00045039
		private void AssertReadOperationValid()
		{
			DiagCore.AssertOrWatson(!this.m_fSkipIO, "m_fSkipIO was true but we're doing a read operation?", new object[0]);
		}

		// Token: 0x04000686 RID: 1670
		private const string PatchFileInProgressSuffix = ".patch-progress";

		// Token: 0x04000687 RID: 1671
		private const string PatchFileDoneSuffix = ".patch-done";

		// Token: 0x04000688 RID: 1672
		private const string OldReseedSuffix = ".reseed-progress";

		// Token: 0x04000689 RID: 1673
		private const string OldReseedDoneSuffix = ".reseed";

		// Token: 0x0400068A RID: 1674
		public const long HeaderSizeBytes = 32768L;

		// Token: 0x0400068B RID: 1675
		public const int MaxNumPagesSupported = 3276800;

		// Token: 0x0400068C RID: 1676
		private readonly bool m_fReadOnly;

		// Token: 0x0400068D RID: 1677
		private readonly bool m_fSkipIO;

		// Token: 0x0400068E RID: 1678
		private bool m_pageNumbersWritten;

		// Token: 0x0400068F RID: 1679
		private bool m_fileStreamClosed;

		// Token: 0x04000690 RID: 1680
		private FileStream m_fileStream;

		// Token: 0x04000691 RID: 1681
		private EseDatabasePatchState m_header;

		// Token: 0x04000692 RID: 1682
		private readonly string m_patchFileNameInProgress;

		// Token: 0x04000693 RID: 1683
		private readonly string m_patchFileNameDone;
	}
}
