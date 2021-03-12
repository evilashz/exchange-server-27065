using System;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Security.Principal;

namespace Microsoft.Exchange.Data.Internal
{
	// Token: 0x0200013B RID: 315
	internal class TemporaryDataStorage : ReadableWritableDataStorage
	{
		// Token: 0x06000C37 RID: 3127 RVA: 0x0006BEA8 File Offset: 0x0006A0A8
		public TemporaryDataStorage() : this(TemporaryDataStorage.DefaultAcquireBuffer, TemporaryDataStorage.DefaultReleaseBuffer)
		{
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0006BEBA File Offset: 0x0006A0BA
		public TemporaryDataStorage(Func<int, byte[]> acquireBuffer, Action<byte[]> releaseBuffer)
		{
			TemporaryDataStorage.Configure();
			this.buffer = new TemporaryDataStorage.VirtualBuffer(TemporaryDataStorage.DefaultBufferBlockSize, TemporaryDataStorage.DefaultBufferMaximumSize, (acquireBuffer != null) ? acquireBuffer : TemporaryDataStorage.DefaultAcquireBuffer, (releaseBuffer != null) ? releaseBuffer : TemporaryDataStorage.DefaultReleaseBuffer);
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x0006BEF2 File Offset: 0x0006A0F2
		public override long Length
		{
			get
			{
				base.ThrowIfDisposed();
				return this.totalLength;
			}
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x0006BF00 File Offset: 0x0006A100
		public static void Configure(int defaultMaximumSize, int defaultBlockSize, string defaultPath, Func<int, byte[]> defaultAcquireBuffer, Action<byte[]> defaultReleaseBuffer)
		{
			TemporaryDataStorage.DefaultBufferMaximumSize = defaultMaximumSize;
			TemporaryDataStorage.DefaultBufferBlockSize = defaultBlockSize;
			TemporaryDataStorage.DefaultPath = defaultPath;
			TemporaryDataStorage.DefaultAcquireBuffer = defaultAcquireBuffer;
			TemporaryDataStorage.DefaultReleaseBuffer = defaultReleaseBuffer;
			TemporaryDataStorage.Configured = false;
			TemporaryDataStorage.Configure();
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0006BF30 File Offset: 0x0006A130
		public override int Read(long position, byte[] buffer, int offset, int count)
		{
			base.ThrowIfDisposed();
			int result = 0;
			if (this.isReadOnly)
			{
				this.readOnlySemaphore.Wait();
				try
				{
					return this.InternalRead(position, buffer, offset, count);
				}
				finally
				{
					this.readOnlySemaphore.Release();
				}
			}
			result = this.InternalRead(position, buffer, offset, count);
			return result;
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x0006BF90 File Offset: 0x0006A190
		public override void Write(long position, byte[] buffer, int offset, int count)
		{
			base.ThrowIfDisposed();
			if (this.isReadOnly)
			{
				throw new InvalidOperationException("Write to read-only DataStorage");
			}
			if (count != 0)
			{
				if (position < (long)this.buffer.MaxBytes)
				{
					int num = this.buffer.Write(position, buffer, offset, count);
					offset += num;
					count -= num;
					position += (long)num;
					if (position > this.totalLength)
					{
						this.totalLength = position;
					}
				}
				else if (this.buffer.Length < this.buffer.MaxBytes)
				{
					this.buffer.SetLength((long)this.buffer.MaxBytes);
					this.totalLength = (long)this.buffer.MaxBytes;
				}
				if (count != 0)
				{
					if (this.fileStream == null)
					{
						this.fileStream = TempFileStream.CreateInstance();
						this.filePosition = 0L;
					}
					if (this.filePosition != position - (long)this.buffer.MaxBytes)
					{
						this.fileStream.Position = position - (long)this.buffer.MaxBytes;
					}
					this.fileStream.Write(buffer, offset, count);
					position += (long)count;
					this.filePosition = position - (long)this.buffer.MaxBytes;
					if (position > this.totalLength)
					{
						this.totalLength = position;
					}
				}
			}
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x0006C0CC File Offset: 0x0006A2CC
		public override void SetLength(long length)
		{
			base.ThrowIfDisposed();
			if (this.isReadOnly)
			{
				throw new InvalidOperationException("Write to read-only DataStorage");
			}
			this.totalLength = length;
			if (length <= (long)this.buffer.MaxBytes)
			{
				this.buffer.SetLength(length);
				if (this.fileStream != null)
				{
					this.fileStream.SetLength(0L);
					return;
				}
			}
			else
			{
				this.buffer.SetLength((long)this.buffer.MaxBytes);
				if (this.fileStream == null)
				{
					this.fileStream = TempFileStream.CreateInstance();
					this.filePosition = 0L;
				}
				this.fileStream.SetLength(length - (long)this.buffer.MaxBytes);
			}
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x0006C175 File Offset: 0x0006A375
		internal static void RefreshConfiguration()
		{
			TemporaryDataStorage.Configured = false;
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x0006C17F File Offset: 0x0006A37F
		internal static string GetTempPath()
		{
			TemporaryDataStorage.Configure();
			return TempFileStream.Path;
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x0006C18B File Offset: 0x0006A38B
		protected override void Dispose(bool disposing)
		{
			if (!base.IsDisposed)
			{
				if (disposing)
				{
					if (this.fileStream != null)
					{
						this.fileStream.Dispose();
					}
					this.buffer.Dispose();
				}
				this.fileStream = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x0006C1C4 File Offset: 0x0006A3C4
		private static void Configure()
		{
			if (TemporaryDataStorage.Configured)
			{
				return;
			}
			lock (TemporaryDataStorage.configurationLockObject)
			{
				if (!TemporaryDataStorage.Configured)
				{
					int num = TemporaryDataStorage.DefaultBufferMaximumSize;
					int num2 = TemporaryDataStorage.DefaultBufferBlockSize;
					string text = TemporaryDataStorage.DefaultPath;
					IList<CtsConfigurationSetting> configuration = ApplicationServices.Provider.GetConfiguration(null);
					foreach (CtsConfigurationSetting ctsConfigurationSetting in configuration)
					{
						if (ctsConfigurationSetting.Name.Equals("TemporaryStorage", StringComparison.OrdinalIgnoreCase))
						{
							foreach (CtsConfigurationArgument ctsConfigurationArgument in ctsConfigurationSetting.Arguments)
							{
								if (ctsConfigurationArgument.Name.Equals("Path", StringComparison.OrdinalIgnoreCase))
								{
									text = ctsConfigurationArgument.Value.Trim();
								}
								else if (ctsConfigurationArgument.Name.Equals("MaximumBufferSize", StringComparison.OrdinalIgnoreCase))
								{
									if (!int.TryParse(ctsConfigurationArgument.Value.Trim(), out num))
									{
										ApplicationServices.Provider.LogConfigurationErrorEvent();
										num = TemporaryDataStorage.DefaultBufferMaximumSize;
									}
									else if (num < 16 || num > 10240)
									{
										ApplicationServices.Provider.LogConfigurationErrorEvent();
										num = TemporaryDataStorage.DefaultBufferMaximumSize;
									}
									else
									{
										num *= 1024;
									}
								}
								else if (ctsConfigurationArgument.Name.Equals("BufferIncrement", StringComparison.OrdinalIgnoreCase))
								{
									if (!int.TryParse(ctsConfigurationArgument.Value.Trim(), out num2))
									{
										ApplicationServices.Provider.LogConfigurationErrorEvent();
										num2 = TemporaryDataStorage.DefaultBufferBlockSize;
									}
									else if (num2 < 4 || num2 > 64)
									{
										ApplicationServices.Provider.LogConfigurationErrorEvent();
										num2 = TemporaryDataStorage.DefaultBufferBlockSize;
									}
									else
									{
										num2 *= 1024;
									}
								}
								else
								{
									ApplicationServices.Provider.LogConfigurationErrorEvent();
								}
							}
						}
					}
					if (num < num2 || num % num2 != 0)
					{
						ApplicationServices.Provider.LogConfigurationErrorEvent();
						num = TemporaryDataStorage.DefaultBufferMaximumSize;
						num2 = TemporaryDataStorage.DefaultBufferBlockSize;
					}
					TemporaryDataStorage.DefaultBufferMaximumSize = num;
					TemporaryDataStorage.DefaultBufferBlockSize = num2;
					string systemTempPath = TemporaryDataStorage.GetSystemTempPath();
					if (text != null)
					{
						text = TemporaryDataStorage.ValidatePath(text);
					}
					if (text == null)
					{
						text = systemTempPath;
					}
					TempFileStream.SetTemporaryPath(text);
					TemporaryDataStorage.Configured = true;
				}
			}
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x0006C438 File Offset: 0x0006A638
		private int InternalRead(long position, byte[] buffer, int offset, int count)
		{
			int num = 0;
			if (position < (long)this.buffer.MaxBytes)
			{
				num = this.buffer.Read(position, buffer, offset, count);
				offset += num;
				count -= num;
				position += (long)num;
			}
			if (count != 0 && position >= (long)this.buffer.MaxBytes && this.fileStream != null)
			{
				if (this.filePosition != position - (long)this.buffer.MaxBytes)
				{
					this.fileStream.Position = position - (long)this.buffer.MaxBytes;
				}
				int num2 = this.fileStream.Read(buffer, offset, count);
				this.filePosition = position - (long)this.buffer.MaxBytes + (long)num2;
				num += num2;
			}
			return num;
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x0006C4F0 File Offset: 0x0006A6F0
		private static DirectorySecurity GetDirectorySecurity()
		{
			DirectorySecurity directorySecurity = new DirectorySecurity();
			directorySecurity.SetAccessRuleProtection(true, false);
			DirectorySecurity result;
			using (WindowsIdentity current = WindowsIdentity.GetCurrent())
			{
				directorySecurity.SetOwner(current.User);
				for (int i = 0; i < TemporaryDataStorage.DirectoryAccessRules.Length; i++)
				{
					directorySecurity.AddAccessRule(TemporaryDataStorage.DirectoryAccessRules[i]);
				}
				if (!current.User.IsWellKnown(WellKnownSidType.LocalSystemSid) && !current.User.IsWellKnown(WellKnownSidType.NetworkServiceSid) && !current.User.IsWellKnown(WellKnownSidType.LocalServiceSid))
				{
					directorySecurity.AddAccessRule(new FileSystemAccessRule(current.User, FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
				}
				result = directorySecurity;
			}
			return result;
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x0006C5A4 File Offset: 0x0006A7A4
		private static string ValidatePath(string path)
		{
			try
			{
				if (Path.IsPathRooted(path))
				{
					if (!Directory.Exists(path))
					{
						Directory.CreateDirectory(path, TemporaryDataStorage.GetDirectorySecurity());
					}
					new FileIOPermission(FileIOPermissionAccess.Write, path).Demand();
				}
				else
				{
					path = null;
				}
			}
			catch (PathTooLongException)
			{
				path = null;
			}
			catch (DirectoryNotFoundException)
			{
				path = null;
			}
			catch (IOException)
			{
				path = null;
			}
			catch (UnauthorizedAccessException)
			{
				path = null;
			}
			catch (ArgumentException)
			{
				path = null;
			}
			catch (NotSupportedException)
			{
				path = null;
			}
			return path;
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x0006C654 File Offset: 0x0006A854
		private static string GetSystemTempPath()
		{
			return Path.GetTempPath();
		}

		// Token: 0x04000EF6 RID: 3830
		public static int DefaultBufferBlockSize = 8192;

		// Token: 0x04000EF7 RID: 3831
		public static int DefaultBufferMaximumSize = TemporaryDataStorage.DefaultBufferBlockSize * 16;

		// Token: 0x04000EF8 RID: 3832
		public static string DefaultPath = null;

		// Token: 0x04000EF9 RID: 3833
		public static Func<int, byte[]> DefaultAcquireBuffer = null;

		// Token: 0x04000EFA RID: 3834
		public static Action<byte[]> DefaultReleaseBuffer = null;

		// Token: 0x04000EFB RID: 3835
		internal static volatile bool Configured = false;

		// Token: 0x04000EFC RID: 3836
		private static object configurationLockObject = new object();

		// Token: 0x04000EFD RID: 3837
		private long totalLength;

		// Token: 0x04000EFE RID: 3838
		private long filePosition;

		// Token: 0x04000EFF RID: 3839
		private Stream fileStream;

		// Token: 0x04000F00 RID: 3840
		private TemporaryDataStorage.VirtualBuffer buffer;

		// Token: 0x04000F01 RID: 3841
		private static readonly FileSystemAccessRule[] DirectoryAccessRules = new FileSystemAccessRule[]
		{
			new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null), FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow),
			new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null), FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow),
			new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.NetworkServiceSid, null), FileSystemRights.ReadData | FileSystemRights.WriteData | FileSystemRights.AppendData | FileSystemRights.ReadExtendedAttributes | FileSystemRights.WriteExtendedAttributes | FileSystemRights.ExecuteFile | FileSystemRights.DeleteSubdirectoriesAndFiles | FileSystemRights.ReadAttributes | FileSystemRights.WriteAttributes | FileSystemRights.Delete | FileSystemRights.ReadPermissions | FileSystemRights.Synchronize, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow),
			new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.LocalServiceSid, null), FileSystemRights.ReadData | FileSystemRights.WriteData | FileSystemRights.AppendData | FileSystemRights.ReadExtendedAttributes | FileSystemRights.WriteExtendedAttributes | FileSystemRights.ExecuteFile | FileSystemRights.DeleteSubdirectoriesAndFiles | FileSystemRights.ReadAttributes | FileSystemRights.WriteAttributes | FileSystemRights.Delete | FileSystemRights.ReadPermissions | FileSystemRights.Synchronize, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow)
		};

		// Token: 0x0200013C RID: 316
		private struct VirtualBuffer : IDisposable
		{
			// Token: 0x06000C47 RID: 3143 RVA: 0x0006C711 File Offset: 0x0006A911
			public VirtualBuffer(int blockSize, int maximumSize)
			{
				this = new TemporaryDataStorage.VirtualBuffer(blockSize, maximumSize, null, null);
			}

			// Token: 0x06000C48 RID: 3144 RVA: 0x0006C720 File Offset: 0x0006A920
			public VirtualBuffer(int blockSize, int maximumSize, Func<int, byte[]> acquireBuffer, Action<byte[]> releaseBuffer)
			{
				if ((acquireBuffer != null && releaseBuffer == null) || (acquireBuffer == null && releaseBuffer != null))
				{
					throw new ArgumentException("acquireBuffer and releaseBuffer should be both null or non-null");
				}
				this.blockSize = blockSize;
				this.maximumSize = maximumSize;
				this.length = 0L;
				this.firstBlock = null;
				this.followingBlocks = null;
				this.acquireBuffer = acquireBuffer;
				this.releaseBuffer = releaseBuffer;
			}

			// Token: 0x170003C4 RID: 964
			// (get) Token: 0x06000C49 RID: 3145 RVA: 0x0006C779 File Offset: 0x0006A979
			public int MaxBytes
			{
				get
				{
					return this.maximumSize;
				}
			}

			// Token: 0x170003C5 RID: 965
			// (get) Token: 0x06000C4A RID: 3146 RVA: 0x0006C781 File Offset: 0x0006A981
			public int BlockSize
			{
				get
				{
					return this.blockSize;
				}
			}

			// Token: 0x170003C6 RID: 966
			// (get) Token: 0x06000C4B RID: 3147 RVA: 0x0006C789 File Offset: 0x0006A989
			public int Length
			{
				get
				{
					return (int)this.length;
				}
			}

			// Token: 0x06000C4C RID: 3148 RVA: 0x0006C794 File Offset: 0x0006A994
			public int Read(long position, byte[] buffer, int offset, int count)
			{
				if (position >= this.length)
				{
					return 0;
				}
				int num = 0;
				if (position < (long)this.BlockSize)
				{
					int num2 = (int)Math.Min((long)this.BlockSize - position, this.length - position);
					if (num2 > count)
					{
						num2 = count;
					}
					Buffer.BlockCopy(this.firstBlock, (int)position, buffer, offset, num2);
					offset += num2;
					count -= num2;
					position += (long)num2;
					num += num2;
				}
				while (count != 0 && position < this.length)
				{
					int num3 = (int)((position - (long)this.BlockSize) / (long)this.BlockSize);
					int num4 = (int)((position - (long)this.BlockSize) % (long)this.BlockSize);
					int num5 = (int)Math.Min((long)(this.BlockSize - num4), this.length - position);
					if (num5 > count)
					{
						num5 = count;
					}
					Buffer.BlockCopy(this.followingBlocks[num3], num4, buffer, offset, num5);
					offset += num5;
					count -= num5;
					position += (long)num5;
					num += num5;
				}
				return num;
			}

			// Token: 0x06000C4D RID: 3149 RVA: 0x0006C888 File Offset: 0x0006AA88
			public int Write(long position, byte[] buffer, int offset, int count)
			{
				if (position > this.length)
				{
					this.SetLength(position);
				}
				if (position >= (long)this.MaxBytes)
				{
					return 0;
				}
				int num = 0;
				if (position < (long)this.BlockSize)
				{
					if (this.firstBlock == null)
					{
						this.firstBlock = this.GetBlock();
					}
					int num2 = (int)Math.Min((long)this.BlockSize - position, (long)count);
					Buffer.BlockCopy(buffer, offset, this.firstBlock, (int)position, num2);
					offset += num2;
					count -= num2;
					position += (long)num2;
					num += num2;
				}
				while (count != 0 && position < (long)this.MaxBytes)
				{
					if (this.followingBlocks == null)
					{
						this.followingBlocks = new byte[(this.MaxBytes - this.BlockSize) / this.BlockSize][];
					}
					int num3 = (int)((position - (long)this.BlockSize) / (long)this.BlockSize);
					int num4 = (int)((position - (long)this.BlockSize) % (long)this.BlockSize);
					if (this.followingBlocks[num3] == null)
					{
						this.followingBlocks[num3] = this.GetBlock();
					}
					int num5 = Math.Min(this.BlockSize - num4, count);
					Buffer.BlockCopy(buffer, offset, this.followingBlocks[num3], num4, num5);
					offset += num5;
					count -= num5;
					position += (long)num5;
					num += num5;
				}
				if (position > this.length)
				{
					this.length = position;
				}
				return num;
			}

			// Token: 0x06000C4E RID: 3150 RVA: 0x0006C9D8 File Offset: 0x0006ABD8
			public void SetLength(long length)
			{
				if (this.length < length)
				{
					if (this.length < (long)this.BlockSize)
					{
						int num = (int)Math.Min((long)this.BlockSize - this.length, length - this.length);
						if (this.firstBlock == null)
						{
							this.firstBlock = this.GetBlock();
						}
						else
						{
							Array.Clear(this.firstBlock, (int)this.length, num);
						}
						this.length += (long)num;
					}
					while (this.length < length)
					{
						if (this.length >= (long)this.MaxBytes)
						{
							return;
						}
						if (this.followingBlocks == null)
						{
							this.followingBlocks = new byte[(this.MaxBytes - this.BlockSize) / this.BlockSize][];
						}
						int num2 = (int)((this.length - (long)this.BlockSize) / (long)this.BlockSize);
						int num3 = (int)((this.length - (long)this.BlockSize) % (long)this.BlockSize);
						int num4 = (int)Math.Min((long)(this.BlockSize - num3), length - this.length);
						if (this.followingBlocks[num2] == null)
						{
							this.followingBlocks[num2] = this.GetBlock();
						}
						else
						{
							Array.Clear(this.followingBlocks[num2], num3, num4);
						}
						this.length += (long)num4;
					}
					return;
				}
				this.length = length;
			}

			// Token: 0x06000C4F RID: 3151 RVA: 0x0006CB28 File Offset: 0x0006AD28
			public void Dispose()
			{
				if (this.releaseBuffer != null)
				{
					if (this.firstBlock != null)
					{
						this.releaseBuffer(this.firstBlock);
						this.firstBlock = null;
					}
					if (this.followingBlocks != null)
					{
						foreach (byte[] array2 in this.followingBlocks)
						{
							if (array2 != null)
							{
								this.releaseBuffer(array2);
							}
						}
						this.followingBlocks = null;
					}
					this.releaseBuffer = null;
				}
				GC.SuppressFinalize(this);
			}

			// Token: 0x06000C50 RID: 3152 RVA: 0x0006CBAB File Offset: 0x0006ADAB
			private byte[] GetBlock()
			{
				if (this.acquireBuffer == null)
				{
					return new byte[this.BlockSize];
				}
				return this.acquireBuffer(this.BlockSize);
			}

			// Token: 0x04000F02 RID: 3842
			private int maximumSize;

			// Token: 0x04000F03 RID: 3843
			private int blockSize;

			// Token: 0x04000F04 RID: 3844
			private long length;

			// Token: 0x04000F05 RID: 3845
			private byte[] firstBlock;

			// Token: 0x04000F06 RID: 3846
			private byte[][] followingBlocks;

			// Token: 0x04000F07 RID: 3847
			private Func<int, byte[]> acquireBuffer;

			// Token: 0x04000F08 RID: 3848
			private Action<byte[]> releaseBuffer;
		}
	}
}
