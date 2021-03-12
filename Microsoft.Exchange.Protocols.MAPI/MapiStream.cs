using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.Mapi;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200007F RID: 127
	public class MapiStream : MapiBase
	{
		// Token: 0x0600044D RID: 1101 RVA: 0x0001E40A File Offset: 0x0001C60A
		internal MapiStream() : base(MapiObjectType.Stream)
		{
			this.streamSizeInvalid = false;
			this.shouldAllowRead = true;
			this.shouldAllowWrite = true;
			this.usingTempStream = false;
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0001E430 File Offset: 0x0001C630
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x0001E438 File Offset: 0x0001C638
		internal Stream Stream
		{
			get
			{
				return this.stream;
			}
			private set
			{
				this.stream = value;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x0001E441 File Offset: 0x0001C641
		public bool StreamSizeInvalid
		{
			get
			{
				return this.streamSizeInvalid;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x0001E449 File Offset: 0x0001C649
		public StorePropTag Ptag
		{
			get
			{
				return this.propTag;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x0001E451 File Offset: 0x0001C651
		internal CodePage CodePage
		{
			get
			{
				return this.codePage;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x0001E459 File Offset: 0x0001C659
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x0001E461 File Offset: 0x0001C661
		private bool ShouldAllowRead
		{
			get
			{
				return this.shouldAllowRead;
			}
			set
			{
				this.shouldAllowRead = value;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x0001E46A File Offset: 0x0001C66A
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x0001E472 File Offset: 0x0001C672
		internal bool ShouldAllowWrite
		{
			get
			{
				return this.shouldAllowWrite;
			}
			private set
			{
				this.shouldAllowWrite = value;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0001E47B File Offset: 0x0001C67B
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x0001E483 File Offset: 0x0001C683
		internal bool ShouldAppend
		{
			get
			{
				return this.shouldAppend;
			}
			private set
			{
				this.shouldAppend = value;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x0001E48C File Offset: 0x0001C68C
		// (set) Token: 0x0600045A RID: 1114 RVA: 0x0001E494 File Offset: 0x0001C694
		internal bool IsDirty
		{
			get
			{
				return this.isDirty;
			}
			private set
			{
				this.isDirty = value;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x0001E4A0 File Offset: 0x0001C6A0
		internal bool ReleaseMayNeedExclusiveLock
		{
			get
			{
				return this.shouldAllowWrite && this.IsDirty && (ConfigurationSchema.ConfigurableSharedLockStage.Value < 2 || base.ParentObject.MapiObjectType == MapiObjectType.Folder || base.ParentObject.MapiObjectType == MapiObjectType.Logon || base.ParentObject.MapiObjectType == MapiObjectType.Attachment || base.ParentObject.MapiObjectType == MapiObjectType.EmbeddedMessage);
			}
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0001E508 File Offset: 0x0001C708
		public static Stream CreateBufferedStream(Stream stream, int bufferSize)
		{
			BufferPoolCollection.BufferSize bufferSize2;
			BufferPoolCollection.AutoCleanupCollection.TryMatchBufferSize(bufferSize, out bufferSize2);
			BufferPool bufferPool = BufferPoolCollection.AutoCleanupCollection.Acquire(bufferSize2);
			return new PooledBufferedStream(stream, bufferPool, bufferSize, true);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0001E538 File Offset: 0x0001C738
		internal ErrorCode ConfigureStream(MapiContext context, MapiStream other, StreamFlags flags, CodePage codePage)
		{
			other.CheckDisposed();
			if (other.propTag.PropType == PropertyType.Binary)
			{
				codePage = CodePage.None;
			}
			if (other.CodePage != codePage)
			{
				return ErrorCode.CreateInvalidParameter((LID)33784U);
			}
			if (flags != StreamFlags.AllowRead)
			{
				return ErrorCode.CreateInvalidParameter((LID)50168U);
			}
			Stream stream = null;
			try
			{
				this.codePage = codePage;
				this.propTag = other.propTag;
				base.ParentObject = other.ParentObject;
				base.Logon = other.Logon;
				this.CheckRights(context, FolderSecurity.ExchangeSecurityDescriptorFolderRights.ReadProperty, AccessCheckOperation.StreamOpen, (LID)51047U);
				other.ConvertToTempStreamIfNeeded();
				stream = TempStream.CloneStream(other.Stream);
				this.IsDirty = false;
				stream.Seek(0L, SeekOrigin.Begin);
				this.originalLength = stream.Length;
				this.shouldAppend = false;
				this.shouldAllowRead = true;
				this.shouldAllowWrite = false;
				this.stream = stream;
				stream = null;
				this.usingTempStream = true;
				base.IsValid = true;
			}
			finally
			{
				if (stream != null)
				{
					stream.Dispose();
					stream = null;
				}
			}
			return ErrorCode.NoError;
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0001E654 File Offset: 0x0001C854
		internal ErrorCode ConfigureStream(MapiContext context, MapiPropBagBase parentPropertyBag, StreamFlags flags, StorePropTag propTag, CodePage codePage)
		{
			if (base.IsDisposed)
			{
				ExTraceGlobals.GeneralTracer.TraceError(0L, "mapiStream:ConfigureStream(): Configure called on a Dispose'd MapiStream! Throwing ExExceptionInvalidObject!");
				return ErrorCode.CreateInvalidObject((LID)48376U);
			}
			if (parentPropertyBag == null || !parentPropertyBag.IsValid || parentPropertyBag.Logon == null || parentPropertyBag.Logon.Session == null)
			{
				ExTraceGlobals.GeneralTracer.TraceError(0L, "mapiStream:ConfigureStream(): parentPropBag is null or invalid, throwing ExExceptionInvalidParameter");
				return ErrorCode.CreateInvalidParameter((LID)64760U);
			}
			if (PropertyType.Unicode != propTag.PropType && PropertyType.Binary != propTag.PropType && PropertyType.Object != propTag.PropType)
			{
				ExTraceGlobals.GeneralTracer.TraceError<StorePropTag>(0L, "mapistream:StorePropTag(): Only ptags for the following types are supported: PT_UNICODE, PT_STRING8, PT_BINARY, PT_OBJECT. Invalid ptag {0}. throwing ExExceptionInvalidParameter", propTag);
				return ErrorCode.CreateInvalidParameter((LID)40184U);
			}
			if (propTag.PropType == PropertyType.Binary)
			{
				codePage = CodePage.None;
			}
			Stream stream = null;
			try
			{
				this.codePage = codePage;
				this.propTag = propTag;
				bool flag = propTag.PropType == PropertyType.Unicode && propTag.ExternalType == PropertyType.String8 && codePage != CodePage.Unicode;
				Encoding encoding;
				if (flag)
				{
					if (!Charset.TryGetEncoding((int)codePage, out encoding))
					{
						ExTraceGlobals.GeneralTracer.TraceError<CodePage>(0L, "MapiStream:CodePage(): Unrecognized or invalid code page id: {0}. Throwing ExExceptionInvalidParameter", codePage);
						DiagnosticContext.TraceDword((LID)34664U, (uint)codePage);
						return ErrorCode.CreateInvalidParameter((LID)46328U);
					}
				}
				else
				{
					encoding = Charset.Unicode.GetEncoding();
				}
				bool flag2;
				if ((StreamFlags.AllowCreate & flags) == (StreamFlags)0)
				{
					Stream stream2 = null;
					try
					{
						ErrorCode dataReader = parentPropertyBag.GetDataReader(context, propTag, out stream2);
						if (dataReader != ErrorCode.NoError && dataReader != ErrorCodeValue.NotSupported)
						{
							return dataReader.Propagate((LID)33528U);
						}
						if (stream2 != null)
						{
							if (flag)
							{
								using (Stream readOnlyConverterStream = MapiStream.GetReadOnlyConverterStream(stream2, CodePage.Unicode, codePage))
								{
									MapiStream.CopyStreamToTempStream(readOnlyConverterStream, out stream);
									flag2 = true;
									goto IL_2CA;
								}
							}
							if (parentPropertyBag is MapiFolder || parentPropertyBag is MapiLogon)
							{
								MapiStream.CopyStreamToTempStream(stream2, out stream);
								flag2 = true;
							}
							else if (!ConfigurationSchema.DisableBypassTempStream.Value)
							{
								int num = (int)stream2.Length;
								int bufferSize;
								if (num <= 16300)
								{
									bufferSize = 16300;
								}
								else if (num <= 32600)
								{
									bufferSize = 32600;
								}
								else
								{
									bufferSize = 81500;
								}
								stream = MapiStream.CreateBufferedStream(stream2, bufferSize);
								stream2 = null;
								flag2 = false;
							}
							else
							{
								MapiStream.CopyStreamToTempStream(stream2, out stream);
								flag2 = true;
							}
						}
						else
						{
							object onePropValue = parentPropertyBag.GetOnePropValue(context, propTag);
							if (onePropValue == null)
							{
								return ErrorCode.CreateNotFound((LID)36088U);
							}
							if (onePropValue is string)
							{
								stream = TempStream.CreateInstance();
								byte[] bytes = encoding.GetBytes((string)onePropValue);
								stream.Write(bytes, 0, bytes.Length);
								flag2 = true;
							}
							else
							{
								stream = TempStream.CreateInstance();
								byte[] array = (byte[])onePropValue;
								stream.Write(array, 0, array.Length);
								flag2 = true;
							}
						}
						IL_2CA:
						goto IL_2EA;
					}
					finally
					{
						if (stream2 != null)
						{
							stream2.Dispose();
							stream2 = null;
						}
					}
				}
				stream = TempStream.CreateInstance();
				this.IsDirty = true;
				flag2 = true;
				IL_2EA:
				stream.Seek(0L, this.shouldAppend ? SeekOrigin.End : SeekOrigin.Begin);
				this.originalLength = stream.Length;
				this.shouldAppend = (StreamFlags.AllowAppend == (StreamFlags.AllowAppend & flags));
				this.shouldAllowRead = (StreamFlags.AllowRead == (StreamFlags.AllowRead & flags));
				this.shouldAllowWrite = (StreamFlags.AllowWrite == (StreamFlags.AllowWrite & flags));
				base.ParentObject = parentPropertyBag;
				base.Logon = parentPropertyBag.Logon;
				this.CheckRights(context, FolderSecurity.ExchangeSecurityDescriptorFolderRights.ReadProperty, AccessCheckOperation.StreamOpen, (LID)47975U);
				this.stream = stream;
				stream = null;
				this.usingTempStream = flag2;
				base.IsValid = true;
			}
			finally
			{
				if (stream != null)
				{
					stream.Dispose();
					stream = null;
				}
			}
			return ErrorCode.NoError;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0001EA30 File Offset: 0x0001CC30
		public void Commit(MapiContext context)
		{
			long num = -1L;
			try
			{
				base.ThrowIfNotValid(null);
				if (this.shouldAllowWrite && this.IsDirty && !this.streamSizeInvalid)
				{
					MapiStreamLock.CanAccess((LID)34728U, this, 0UL, 0UL, true);
					if (this.stream.CanSeek)
					{
						num = this.Stream.Position;
						this.stream.Seek(0L, SeekOrigin.Begin);
					}
					this.stream.Flush();
					bool flag = this.propTag.PropType == PropertyType.Unicode && this.propTag.ExternalType == PropertyType.String8 && this.codePage != CodePage.Unicode;
					Stream stream;
					ErrorCode dataWriter = base.ParentObject.GetDataWriter(context, this.propTag, out stream);
					if (dataWriter != ErrorCode.NoError && dataWriter != ErrorCodeValue.NotSupported)
					{
						throw new StoreException((LID)49912U, dataWriter);
					}
					using (stream)
					{
						if (stream != null)
						{
							if (flag)
							{
								using (Stream writeOnlyConverterStream = MapiStream.GetWriteOnlyConverterStream(stream, this.CodePage, CodePage.Unicode))
								{
									MapiStream.CopyStream(this.stream, writeOnlyConverterStream);
									goto IL_203;
								}
							}
							MapiStream.CopyStream(this.stream, stream);
						}
						else
						{
							object value;
							if (this.propTag.PropType == PropertyType.Unicode)
							{
								TextToText textToText = new TextToText(TextToTextConversionMode.ConvertCodePageOnly);
								textToText.InputEncoding = (flag ? Charset.GetEncoding((int)this.codePage) : Charset.Unicode.GetEncoding());
								TextReader textReader = new ConverterReader(this.Stream, textToText);
								value = textReader.ReadToEnd();
							}
							else
							{
								byte[] array = new byte[this.Stream.Length];
								int num2;
								for (int i = 0; i < array.Length; i += num2)
								{
									num2 = this.Stream.Read(array, i, array.Length - i);
								}
								value = array;
							}
							ErrorCode errorCode = base.ParentObject.SetOneProp(context, this.propTag, value);
							if (errorCode != ErrorCode.NoError)
							{
								throw new StoreException((LID)36624U, errorCode);
							}
						}
						IL_203:;
					}
					this.IsDirty = false;
				}
			}
			finally
			{
				if (num != -1L)
				{
					this.Stream.Seek(num, SeekOrigin.Begin);
				}
			}
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0001ECB8 File Offset: 0x0001CEB8
		public ErrorCode Clone(MapiContext context, out MapiStream stream)
		{
			this.CheckRights(context, FolderSecurity.ExchangeSecurityDescriptorFolderRights.ReadProperty | FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteProperty, AccessCheckOperation.StreamRead, (LID)64359U);
			stream = null;
			MapiStream mapiStream = null;
			base.ThrowIfNotValid(null);
			ErrorCode first = base.ParentObject.OpenStream(context, StreamFlags.AllowCreate | (this.shouldAppend ? StreamFlags.AllowAppend : ((StreamFlags)0)) | StreamFlags.AllowRead | StreamFlags.AllowWrite, this.propTag, this.CodePage, out mapiStream);
			if (first != ErrorCode.NoError)
			{
				return first.Propagate((LID)35712U);
			}
			mapiStream.ConvertToTempStreamIfNeeded();
			if (!this.ShouldAllowRead || !mapiStream.ShouldAllowWrite)
			{
				return ErrorCode.CreateNoAccess((LID)56568U);
			}
			if (this.StreamSizeInvalid || mapiStream.StreamSizeInvalid)
			{
				return ErrorCode.CreateMaxSubmissionExceeded((LID)35228U);
			}
			if (this.Stream != null)
			{
				long position = this.Stream.Position;
				this.Stream.Flush();
				MapiStream.CopyStream(this.Stream, mapiStream.Stream);
				mapiStream.Stream.Flush();
				mapiStream.Seek(context, 0L, SeekOrigin.Begin);
				mapiStream.IsDirty = true;
				this.Seek(context, position, SeekOrigin.Begin);
			}
			stream = mapiStream;
			return ErrorCode.NoError;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0001EDDC File Offset: 0x0001CFDC
		internal void CopyTo(MapiContext context, MapiStream destinationStream, long numBytesToCopy, out long numBytesRead, out long numBytesWritten)
		{
			this.CheckRights(context, FolderSecurity.ExchangeSecurityDescriptorFolderRights.ReadProperty, AccessCheckOperation.StreamRead, (LID)39783U);
			destinationStream.CheckRights(context, FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteProperty, AccessCheckOperation.StreamWrite, (LID)56167U);
			numBytesRead = 0L;
			numBytesWritten = 0L;
			long num = -1L;
			long num2 = -1L;
			destinationStream.ConvertToTempStreamIfNeeded();
			try
			{
				base.ThrowIfNotValid(null);
				if (!this.ShouldAllowRead || !destinationStream.ShouldAllowWrite)
				{
					throw new ExExceptionAccessDenied((LID)44280U, "this didn't allow read, destination didn't allow write!");
				}
				if (this.StreamSizeInvalid || destinationStream.StreamSizeInvalid)
				{
					throw new ExExceptionMaxSubmissionExceeded((LID)45468U, "Exceeded the size limitation");
				}
				if (this.Stream != null)
				{
					num = this.Stream.Position;
					num2 = destinationStream.Stream.Position;
					int num3 = (int)numBytesToCopy;
					if ((long)num3 > this.Stream.Length)
					{
						num3 = (int)this.Stream.Length;
					}
					byte[] buffer = new byte[num3];
					this.Stream.Flush();
					this.Stream.Seek(0L, SeekOrigin.Begin);
					this.Stream.Read(buffer, 0, num3);
					numBytesRead = (long)((int)this.Stream.Position);
					destinationStream.Stream.Write(buffer, 0, (int)numBytesRead);
					destinationStream.Stream.Flush();
					destinationStream.IsDirty = true;
					numBytesWritten = (long)((int)(destinationStream.Stream.Position - num2));
				}
			}
			finally
			{
				if (num != -1L && this.Stream != null)
				{
					this.Stream.Seek(num, SeekOrigin.Begin);
				}
				if (num2 != -1L)
				{
					destinationStream.Stream.Seek(num2, SeekOrigin.Begin);
				}
			}
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0001EF7C File Offset: 0x0001D17C
		public int Read(MapiContext context, byte[] buffer, int offset, int count)
		{
			if (!this.ShouldAllowRead)
			{
				throw new ExExceptionAccessDenied((LID)52472U, "Stream does not allow read -- throwing!");
			}
			this.CheckRights(context, FolderSecurity.ExchangeSecurityDescriptorFolderRights.ReadProperty, AccessCheckOperation.StreamRead, (LID)43879U);
			MapiStreamLock.CanAccess((LID)56232U, this, (ulong)this.Stream.Position, (ulong)((long)count), false);
			if (count == 0)
			{
				return 0;
			}
			int num = this.Stream.Read(buffer, offset, count);
			if (num == 0 && this.Stream.Position < this.Stream.Length)
			{
				throw new StoreException((LID)47132U, ErrorCodeValue.CorruptData);
			}
			return num;
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0001F020 File Offset: 0x0001D220
		public byte[] Read(MapiContext context, int numBytesToRead, out int actualBytesRead)
		{
			actualBytesRead = 0;
			base.ThrowIfNotValid(null);
			if (!this.ShouldAllowRead)
			{
				throw new ExExceptionAccessDenied((LID)62712U, "Stream does not allow read -- throwing!");
			}
			this.CheckRights(context, FolderSecurity.ExchangeSecurityDescriptorFolderRights.ReadProperty, AccessCheckOperation.StreamRead, (LID)60263U);
			byte[] array;
			if (this.Stream == null)
			{
				array = new byte[0];
			}
			else
			{
				MapiStreamLock.CanAccess((LID)43944U, this, (ulong)this.Stream.Position, (ulong)((long)numBytesToRead), false);
				array = new byte[numBytesToRead];
				while (numBytesToRead > 0)
				{
					int num = this.Stream.Read(array, actualBytesRead, numBytesToRead);
					if (num == 0)
					{
						break;
					}
					actualBytesRead += num;
					numBytesToRead -= num;
				}
			}
			return array;
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0001F0C6 File Offset: 0x0001D2C6
		public int Write(MapiContext context, byte[] bytesToWrite)
		{
			return this.Write(context, bytesToWrite, 0, bytesToWrite.Length);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0001F0D4 File Offset: 0x0001D2D4
		public int Write(MapiContext context, byte[] bytesToWrite, int offset, int length)
		{
			this.CheckRights(context, FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteProperty, AccessCheckOperation.StreamWrite, (LID)35687U);
			base.ThrowIfNotValid(null);
			if (!this.ShouldAllowWrite)
			{
				throw new ExExceptionStreamAccessDenied((LID)38136U, "Stream doesn't allow write.");
			}
			if (this.streamSizeInvalid)
			{
				return length;
			}
			this.ConvertToTempStreamIfNeeded();
			long position = this.Stream.Position;
			if (this.shouldAppend && position < this.originalLength)
			{
				throw new ExExceptionAccessDenied((LID)58616U, "Stream writes not allowed before the original length of the stream");
			}
			MapiStreamLock.CanAccess((LID)35752U, this, (ulong)this.Stream.Position, (ulong)((long)length), true);
			this.Stream.Write(bytesToWrite, offset, length);
			this.IsDirty = true;
			int result = (int)(this.Stream.Position - position);
			this.streamSizeInvalid |= base.ParentObject.IsStreamSizeInvalid(context, this.Stream.Length);
			return result;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0001F1C8 File Offset: 0x0001D3C8
		public long Seek(MapiContext context, long offset, SeekOrigin origin)
		{
			this.CheckRights(context, FolderSecurity.ExchangeSecurityDescriptorFolderRights.ReadProperty, AccessCheckOperation.StreamSeek, (LID)52071U);
			base.ThrowIfNotValid(null);
			if (this.Stream == null)
			{
				return 0L;
			}
			long num = 0L;
			switch (origin)
			{
			case SeekOrigin.Begin:
				num = offset;
				break;
			case SeekOrigin.Current:
				num = this.Stream.Position + offset;
				break;
			case SeekOrigin.End:
				num = this.Stream.Length + offset;
				break;
			}
			if (num < 0L || num > 2147483647L)
			{
				if (origin == SeekOrigin.Begin)
				{
					throw new ExExceptionStreamSeekError((LID)34040U, "Seek offset out of range");
				}
				throw new ExExceptionFail((LID)50424U, "Seek offset out of range");
			}
			else
			{
				if (num > this.Stream.Length && this.shouldAllowWrite)
				{
					this.SetSize(context, num);
				}
				if (num <= this.Stream.Length)
				{
					this.Stream.Seek(offset, origin);
					return this.Stream.Position;
				}
				this.Stream.Seek(0L, SeekOrigin.End);
				return num;
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0001F2C7 File Offset: 0x0001D4C7
		public long GetSize(MapiContext context)
		{
			base.ThrowIfNotValid(null);
			this.CheckRights(context, FolderSecurity.ExchangeSecurityDescriptorFolderRights.ReadProperty, AccessCheckOperation.StreamGetSize, (LID)45927U);
			if (this.Stream == null)
			{
				return 0L;
			}
			return this.Stream.Length;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0001F2FC File Offset: 0x0001D4FC
		public long SetSize(MapiContext context, long newSize)
		{
			base.ThrowIfNotValid(null);
			if (!this.shouldAllowWrite)
			{
				throw new ExExceptionStreamAccessDenied((LID)47352U, "Write to stream not allowed.");
			}
			this.CheckRights(context, FolderSecurity.ExchangeSecurityDescriptorFolderRights.WriteProperty, AccessCheckOperation.StreamSetSize, (LID)62311U);
			if (this.shouldAppend && newSize < this.originalLength)
			{
				throw new ExExceptionAccessDenied((LID)63736U, "Cannot set the size of the stream smaller than the current!");
			}
			this.streamSizeInvalid |= base.ParentObject.IsStreamSizeInvalid(context, newSize);
			if (this.streamSizeInvalid)
			{
				throw new ExExceptionMaxSubmissionExceeded((LID)53148U, "Exceeded the size limitation");
			}
			if (newSize < this.originalLength)
			{
				MapiStreamLock.CanAccess((LID)52136U, this, (ulong)newSize, (ulong)(this.originalLength - newSize), true);
			}
			else
			{
				MapiStreamLock.CanAccess((LID)62376U, this, (ulong)this.originalLength, (ulong)(newSize - this.originalLength), true);
			}
			this.ConvertToTempStreamIfNeeded();
			this.Stream.SetLength(newSize);
			this.IsDirty = true;
			return this.Stream.Length;
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0001F407 File Offset: 0x0001D607
		internal ErrorCode LockRegion(MapiContext context, ulong offset, ulong length, bool exclusive)
		{
			return MapiStreamLock.LockRegion(this, offset, length, exclusive);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0001F413 File Offset: 0x0001D613
		internal ErrorCode UnlockRegion(MapiContext context, ulong offset, ulong length, bool exclusive)
		{
			this.Stream.Flush();
			return MapiStreamLock.UnLockRegion(this, offset, length, exclusive);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0001F42A File Offset: 0x0001D62A
		internal void CheckRights(MapiContext context, FolderSecurity.ExchangeSecurityDescriptorFolderRights requestedRights, AccessCheckOperation operation, LID lid)
		{
			if (base.ParentObject == null)
			{
				return;
			}
			base.ParentObject.CheckPropertyRights(context, requestedRights, operation, lid);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0001F445 File Offset: 0x0001D645
		public override void OnRelease(MapiContext context)
		{
			this.Commit(context);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0001F44E File Offset: 0x0001D64E
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MapiStream>(this);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0001F458 File Offset: 0x0001D658
		protected override void InternalDispose(bool calledFromDispose)
		{
			try
			{
				if (calledFromDispose)
				{
					try
					{
						if (this.stream != null)
						{
							this.stream.Dispose();
						}
					}
					finally
					{
						MapiStreamLock.Release(this);
					}
				}
			}
			finally
			{
				base.InternalDispose(calledFromDispose);
			}
			this.propTag = StorePropTag.Invalid;
			this.originalLength = 0L;
			this.shouldAppend = false;
			this.shouldAllowRead = true;
			this.shouldAllowWrite = true;
			this.isDirty = false;
			this.stream = null;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0001F4E0 File Offset: 0x0001D6E0
		private static int CopyStream(Stream source, Stream destination)
		{
			return TempStream.CopyStream(source, destination);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0001F4EC File Offset: 0x0001D6EC
		private static int CopyStreamToTempStream(Stream source, out Stream destination)
		{
			Stream stream = null;
			int result;
			try
			{
				stream = TempStream.CreateInstance();
				int num = MapiStream.CopyStream(source, stream);
				destination = stream;
				stream = null;
				result = num;
			}
			finally
			{
				if (stream != null)
				{
					stream.Dispose();
					stream = null;
				}
			}
			return result;
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0001F530 File Offset: 0x0001D730
		private static Stream GetReadOnlyConverterStream(Stream sourceStream, CodePage sourceCodePage, CodePage targetCodePage)
		{
			return new ConverterStream(sourceStream, new TextToText(TextToTextConversionMode.ConvertCodePageOnly)
			{
				InputEncoding = CodePageMap.GetEncoding((int)sourceCodePage),
				OutputEncoding = CodePageMap.GetEncoding((int)targetCodePage)
			}, ConverterStreamAccess.Read);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0001F564 File Offset: 0x0001D764
		private static Stream GetWriteOnlyConverterStream(Stream targetStream, CodePage sourceCodePage, CodePage targetCodePage)
		{
			return new ConverterStream(targetStream, new TextToText(TextToTextConversionMode.ConvertCodePageOnly)
			{
				OutputEncoding = CodePageMap.GetEncoding((int)targetCodePage),
				InputEncoding = CodePageMap.GetEncoding((int)sourceCodePage)
			}, ConverterStreamAccess.Write);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0001F598 File Offset: 0x0001D798
		private void ConvertToTempStreamIfNeeded()
		{
			if (this.usingTempStream)
			{
				return;
			}
			if (this.stream == null)
			{
				return;
			}
			long position = this.stream.Position;
			this.stream.Position = 0L;
			Stream stream = null;
			try
			{
				MapiStream.CopyStreamToTempStream(this.stream, out stream);
				stream.Position = position;
				this.stream.Dispose();
				this.stream = stream;
				stream = null;
				this.usingTempStream = true;
			}
			finally
			{
				if (stream != null)
				{
					stream.Dispose();
				}
			}
		}

		// Token: 0x040002AB RID: 683
		private bool streamSizeInvalid;

		// Token: 0x040002AC RID: 684
		private Stream stream;

		// Token: 0x040002AD RID: 685
		private StorePropTag propTag;

		// Token: 0x040002AE RID: 686
		private CodePage codePage;

		// Token: 0x040002AF RID: 687
		private bool shouldAllowRead;

		// Token: 0x040002B0 RID: 688
		private bool shouldAllowWrite;

		// Token: 0x040002B1 RID: 689
		private long originalLength;

		// Token: 0x040002B2 RID: 690
		private bool shouldAppend;

		// Token: 0x040002B3 RID: 691
		private bool isDirty;

		// Token: 0x040002B4 RID: 692
		private bool usingTempStream;
	}
}
