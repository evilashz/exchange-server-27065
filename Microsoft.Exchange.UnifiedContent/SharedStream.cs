using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;

namespace Microsoft.Exchange.UnifiedContent
{
	// Token: 0x0200000C RID: 12
	public class SharedStream : Stream
	{
		// Token: 0x06000060 RID: 96 RVA: 0x000030C0 File Offset: 0x000012C0
		private SharedStream(string sPath, long lCapacity, FileSecurity fileSecurity)
		{
			this.Path = sPath;
			this.lCapacity = lCapacity;
			this.fileSecurity = fileSecurity;
			this.lSize = 0L;
			this.fOverflow = false;
			this.DeleteFileOnDispose = true;
			MemoryMappedFileSecurity memoryMappedFileSecurity = new MemoryMappedFileSecurity();
			memoryMappedFileSecurity.SetSecurityDescriptorBinaryForm(SharedStream.BuildSecurityDescriptor(SharedStream.defaultAllowedList).GetSecurityDescriptorBinaryForm());
			this.Name = Guid.NewGuid().ToString();
			this.mapping = MemoryMappedFile.CreateNew(this.Name, this.lCapacity, MemoryMappedFileAccess.ReadWrite, MemoryMappedFileOptions.None, memoryMappedFileSecurity, HandleInheritability.None);
			this.streamMapping = this.mapping.CreateViewStream();
			this.WriteHeader();
			this.streamCurrent = this.streamMapping;
			this.streamCurrent.Position = 24L;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003188 File Offset: 0x00001388
		private SharedStream(string sPath, string sName, FileSecurity fileSecurity)
		{
			this.Path = sPath;
			this.fileSecurity = fileSecurity;
			this.lSize = 0L;
			this.DeleteFileOnDispose = false;
			this.Name = sName;
			this.mapping = MemoryMappedFile.OpenExisting(sName);
			this.streamMapping = this.mapping.CreateViewStream();
			this.streamCurrent = this.streamMapping;
			this.CheckForOverflow();
			this.streamCurrent.Position = (long)this.iHeaderSize;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003208 File Offset: 0x00001408
		~SharedStream()
		{
			this.Dispose(false);
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003238 File Offset: 0x00001438
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00003240 File Offset: 0x00001440
		public string Name { get; private set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003249 File Offset: 0x00001449
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00003251 File Offset: 0x00001451
		public string Path { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000067 RID: 103 RVA: 0x0000325C File Offset: 0x0000145C
		public string SharedName
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(this.Path);
				stringBuilder.Append("|");
				stringBuilder.Append(this.Name);
				stringBuilder.Append("|");
				stringBuilder.Append(SharedStream.sessionId);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000032AC File Offset: 0x000014AC
		public string SharedFullName
		{
			get
			{
				this.CheckForOverflow();
				if (this.fOverflow)
				{
					return System.IO.Path.Combine(this.Path, this.Name);
				}
				return string.Format("Session\\{0}\\{1}", SharedStream.sessionId, this.Name);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000032E8 File Offset: 0x000014E8
		public long Capacity
		{
			get
			{
				this.CheckForOverflow();
				return this.lCapacity;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000032F8 File Offset: 0x000014F8
		public bool Overflowed
		{
			get
			{
				long position = this.streamMapping.Position;
				BinaryReader binaryReader = new BinaryReader(this.streamMapping);
				this.streamMapping.Position = 1L;
				byte b = binaryReader.ReadByte();
				this.streamMapping.Position = position;
				return 0 != b;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003344 File Offset: 0x00001544
		// (set) Token: 0x0600006C RID: 108 RVA: 0x0000334C File Offset: 0x0000154C
		public bool DeleteFileOnDispose { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003355 File Offset: 0x00001555
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003358 File Offset: 0x00001558
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600006F RID: 111 RVA: 0x0000335B File Offset: 0x0000155B
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000070 RID: 112 RVA: 0x0000335E File Offset: 0x0000155E
		public override long Length
		{
			get
			{
				return this.ReadSize();
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003366 File Offset: 0x00001566
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00003381 File Offset: 0x00001581
		public override long Position
		{
			get
			{
				this.CheckForOverflow();
				return this.streamCurrent.Position - (long)this.iHeaderSize;
			}
			set
			{
				this.CheckForOverflow();
				if (value < 0L)
				{
					throw new IOException("New position is before beginning of file.");
				}
				this.streamCurrent.Position = value + (long)this.iHeaderSize;
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000033AD File Offset: 0x000015AD
		public static SharedStream Create(string sPath, long lCapacity = 1048576L, FileSecurity fileSecurity = null)
		{
			return new SharedStream(sPath, lCapacity, fileSecurity);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000033B7 File Offset: 0x000015B7
		public static SharedStream Open(string sPath, string sName, FileSecurity fileSecurity = null)
		{
			return new SharedStream(sPath, sName, fileSecurity);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000033C4 File Offset: 0x000015C4
		public static SharedStream Open(string sSharedName, FileSecurity fileSecurity = null)
		{
			string[] array = sSharedName.Split(new char[]
			{
				'|'
			});
			if (array.Length != 3)
			{
				throw new ArgumentException("SharedStream: SharedName is invalid.");
			}
			return new SharedStream(array[0], array[1], fileSecurity);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003402 File Offset: 0x00001602
		public override void Flush()
		{
			if (this.file != null)
			{
				this.file.Flush();
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003418 File Offset: 0x00001618
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.CheckForOverflow();
			if (this.streamCurrent == null)
			{
				throw new InvalidOperationException("Stream is null.");
			}
			return this.streamCurrent.Read(buffer, offset, (int)Math.Min((long)count, this.lSize - (this.streamCurrent.Position - (long)this.iHeaderSize)));
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003470 File Offset: 0x00001670
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.CheckForOverflow();
			switch (origin)
			{
			case SeekOrigin.Begin:
				this.Position = offset;
				break;
			case SeekOrigin.Current:
				this.Position += offset;
				break;
			case SeekOrigin.End:
				this.Position = this.lSize - offset;
				break;
			default:
				throw new ArgumentException("Invalid seek origin");
			}
			return this.Position;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000034D3 File Offset: 0x000016D3
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000034DC File Offset: 0x000016DC
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.CheckForOverflow();
			if (this.streamCurrent.Position + (long)count > this.lCapacity)
			{
				if (this.file == null)
				{
					this.fOverflow = true;
					this.WriteHeader();
					if (this.fileSecurity == null)
					{
						this.file = new FileStream(System.IO.Path.Combine(this.Path, this.Name), FileMode.Create, FileAccess.ReadWrite, FileShare.Read | FileShare.Write | FileShare.Delete);
					}
					else
					{
						this.file = new FileStream(System.IO.Path.Combine(this.Path, this.Name), FileMode.Create, FileSystemRights.Modify, FileShare.Read | FileShare.Write | FileShare.Delete, 8192, FileOptions.RandomAccess, this.fileSecurity);
					}
					long position = this.streamMapping.Position;
					this.streamMapping.Position = 0L;
					this.streamMapping.CopyTo(this.file);
					this.file.Position = position;
					this.lCapacity = long.MaxValue;
					this.WriteHeader();
				}
				this.streamCurrent = this.file;
			}
			if (this.Position + (long)count > this.lSize)
			{
				this.UpdateSize(this.Position + (long)count);
			}
			this.streamCurrent.Write(buffer, offset, count);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003604 File Offset: 0x00001804
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (this.fDisposed || !disposing)
			{
				return;
			}
			if (this.Overflowed && this.DeleteFileOnDispose)
			{
				FileStream fileStream = new FileStream(System.IO.Path.Combine(this.Path, this.Name), FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read | FileShare.Write | FileShare.Delete, 4096, FileOptions.DeleteOnClose);
				fileStream.Dispose();
			}
			if (this.streamMapping != null)
			{
				this.streamMapping.Dispose();
				this.streamMapping = null;
			}
			if (this.mapping != null)
			{
				this.mapping.Dispose();
				this.mapping = null;
			}
			if (this.file != null)
			{
				this.file.Dispose();
				this.file = null;
			}
			this.fDisposed = true;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000036B4 File Offset: 0x000018B4
		private static FileSecurity BuildSecurityDescriptor(IEnumerable<WellKnownSidType> allowedList)
		{
			FileSecurity fileSecurity = new FileSecurity();
			foreach (WellKnownSidType sidType in allowedList)
			{
				SecurityIdentifier identity = new SecurityIdentifier(sidType, null);
				fileSecurity.AddAccessRule(new FileSystemAccessRule(identity, FileSystemRights.FullControl, AccessControlType.Allow));
			}
			return fileSecurity;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003718 File Offset: 0x00001918
		private void CheckForOverflow()
		{
			if (this.fOverflow)
			{
				return;
			}
			long position = this.streamCurrent.Position;
			BinaryReader binaryReader = new BinaryReader(this.streamMapping);
			long position2 = this.streamMapping.Position;
			this.streamMapping.Position = 0L;
			this.iHeaderSize = (int)binaryReader.ReadByte();
			if (this.iHeaderSize != 24)
			{
				throw new ArgumentException("Invalid header size.");
			}
			if (binaryReader.ReadByte() != 0)
			{
				this.file = File.Open(System.IO.Path.Combine(this.Path, this.Name), FileMode.Open, FileAccess.ReadWrite, FileShare.Read | FileShare.Write | FileShare.Delete);
				this.file.Position = Math.Max(this.file.Length, 24L);
				this.streamCurrent = this.file;
				this.fOverflow = true;
			}
			else
			{
				this.fOverflow = false;
			}
			this.streamMapping.Position = 8L;
			this.lSize = (long)binaryReader.ReadUInt64();
			this.streamMapping.Position = 16L;
			this.lCapacity = (long)binaryReader.ReadUInt64();
			this.streamMapping.Position = position2;
			this.streamCurrent.Position = position;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000382C File Offset: 0x00001A2C
		private void WriteHeader()
		{
			long position = this.streamMapping.Position;
			this.streamMapping.Position = 0L;
			this.streamMapping.WriteByte((byte)this.iHeaderSize);
			this.streamMapping.WriteByte(this.fOverflow ? 1 : 0);
			BinaryWriter binaryWriter = new BinaryWriter(this.streamMapping);
			this.streamMapping.Position = 8L;
			binaryWriter.Write(this.lSize);
			this.streamMapping.Position = 16L;
			binaryWriter.Write(this.lCapacity);
			this.streamMapping.Position = position;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000038C8 File Offset: 0x00001AC8
		private long ReadSize()
		{
			long position = this.streamMapping.Position;
			BinaryReader binaryReader = new BinaryReader(this.streamMapping);
			this.streamMapping.Position = 8L;
			this.lSize = (long)binaryReader.ReadUInt64();
			this.streamMapping.Position = position;
			return this.lSize;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003918 File Offset: 0x00001B18
		private void UpdateSize(long size)
		{
			this.lSize = size;
			long position = this.streamMapping.Position;
			this.streamMapping.Position = 8L;
			BinaryWriter binaryWriter = new BinaryWriter(this.streamMapping);
			binaryWriter.Write(this.lSize);
			this.streamMapping.Position = position;
		}

		// Token: 0x04000039 RID: 57
		public const int HeaderSize = 24;

		// Token: 0x0400003A RID: 58
		private const int DefaultCapacity = 1048576;

		// Token: 0x0400003B RID: 59
		private const int HeaderContentOverflowOffset = 1;

		// Token: 0x0400003C RID: 60
		private const int HeaderContentSizeOffset = 8;

		// Token: 0x0400003D RID: 61
		private const int HeaderCapacityOffset = 16;

		// Token: 0x0400003E RID: 62
		private static readonly int sessionId = Process.GetCurrentProcess().SessionId;

		// Token: 0x0400003F RID: 63
		private static readonly WellKnownSidType[] defaultAllowedList = new WellKnownSidType[]
		{
			WellKnownSidType.WorldSid,
			WellKnownSidType.SelfSid,
			WellKnownSidType.NetworkServiceSid,
			WellKnownSidType.BuiltinAdministratorsSid,
			WellKnownSidType.LocalServiceSid
		};

		// Token: 0x04000040 RID: 64
		private readonly FileSecurity fileSecurity;

		// Token: 0x04000041 RID: 65
		private MemoryMappedFile mapping;

		// Token: 0x04000042 RID: 66
		private MemoryMappedViewStream streamMapping;

		// Token: 0x04000043 RID: 67
		private FileStream file;

		// Token: 0x04000044 RID: 68
		private long lCapacity;

		// Token: 0x04000045 RID: 69
		private long lSize;

		// Token: 0x04000046 RID: 70
		private int iHeaderSize = 24;

		// Token: 0x04000047 RID: 71
		private bool fOverflow;

		// Token: 0x04000048 RID: 72
		private bool fDisposed;

		// Token: 0x04000049 RID: 73
		private Stream streamCurrent;
	}
}
