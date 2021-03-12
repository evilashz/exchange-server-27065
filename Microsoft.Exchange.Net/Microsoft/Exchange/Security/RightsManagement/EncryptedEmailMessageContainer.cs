using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.RightsManagement.StructuredStorage;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x02000986 RID: 2438
	internal abstract class EncryptedEmailMessageContainer<T> : IDisposeTrackable, IDisposable where T : EncryptedEmailMessage, new()
	{
		// Token: 0x060034E6 RID: 13542 RVA: 0x00086007 File Offset: 0x00084207
		public EncryptedEmailMessageContainer()
		{
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x060034E7 RID: 13543 RVA: 0x0008601B File Offset: 0x0008421B
		public EncryptedEmailMessageContainer(T emailMessage)
		{
			if (emailMessage == null)
			{
				throw new ArgumentNullException("emailMessage");
			}
			this.emailMessage = emailMessage;
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x060034E8 RID: 13544 RVA: 0x00086049 File Offset: 0x00084249
		public T EmailMessage
		{
			get
			{
				return this.emailMessage;
			}
		}

		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x060034E9 RID: 13545 RVA: 0x00086051 File Offset: 0x00084251
		protected virtual string EncryptedEmailMessageStreamName
		{
			get
			{
				return "EncryptedContents";
			}
		}

		// Token: 0x060034EA RID: 13546 RVA: 0x00086058 File Offset: 0x00084258
		internal static T ReadEncryptedEmailMessage(IStorage rootStorage, string contentStreamName, EncryptedEmailMessageBinding messageBinding, CreateStreamCallbackDelegate createBodyStreamCallback, CreateStreamCallbackDelegate createAttachmentStreamCallback)
		{
			T result = Activator.CreateInstance<T>();
			IStream stream = null;
			IStorage storage = null;
			try
			{
				stream = DrmEmailUtils.EnsureStream(rootStorage, contentStreamName);
				try
				{
					storage = messageBinding.ConvertToEncryptedStorage(stream, false);
				}
				catch (COMException innerException)
				{
					throw new InvalidRpmsgFormatException(DrmStrings.InvalidRpmsgFormat("InvalidDRMContentEncryptedStorage"), innerException);
				}
				result.Load(storage, createBodyStreamCallback, createAttachmentStreamCallback);
			}
			finally
			{
				if (storage != null)
				{
					Marshal.ReleaseComObject(storage);
					storage = null;
				}
				if (stream != null)
				{
					Marshal.ReleaseComObject(stream);
					stream = null;
				}
			}
			return result;
		}

		// Token: 0x060034EB RID: 13547 RVA: 0x000860E4 File Offset: 0x000842E4
		internal static void WriteEncryptedEmailMessage(IStorage rootStorage, string contentStreamName, EncryptedEmailMessageBinding messageBinding, EncryptedEmailMessage emailMessage)
		{
			IStream stream = null;
			IStorage storage = null;
			try
			{
				stream = rootStorage.CreateStream(contentStreamName, 4114, 0, 0);
				storage = messageBinding.ConvertToEncryptedStorage(stream, true);
				emailMessage.Save(storage, messageBinding);
				storage.Commit(STGC.STGC_DEFAULT);
				stream.Commit(STGC.STGC_DEFAULT);
			}
			finally
			{
				if (storage != null)
				{
					Marshal.ReleaseComObject(storage);
					storage = null;
				}
				if (stream != null)
				{
					Marshal.ReleaseComObject(stream);
					stream = null;
				}
			}
		}

		// Token: 0x060034EC RID: 13548 RVA: 0x00086150 File Offset: 0x00084350
		public void Load(Stream stream, CreateStreamCallbackDelegate createStreamCallback)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (createStreamCallback == null)
			{
				throw new ArgumentNullException("createStreamCallback");
			}
			if (this.emailMessage != null || this.rootStorage != null)
			{
				throw new InvalidOperationException("The object is already loaded.");
			}
			bool flag = false;
			Stream stream2 = null;
			IStorage storage = null;
			try
			{
				EncryptedEmailMessageContainer<T>.Tracer.TraceDebug((long)this.GetHashCode(), "Loading the encrypted message stream");
				stream2 = createStreamCallback(null);
				EncryptedEmailMessageContainer<T>.Tracer.TraceDebug((long)this.GetHashCode(), "Decompressing the message stream");
				DrmEmailCompression.DecompressStream(stream, stream2, true);
				try
				{
					storage = DrmEmailUtils.OpenStorageOverStream(stream2);
				}
				catch (COMException innerException)
				{
					throw new InvalidRpmsgFormatException(DrmStrings.InvalidRpmsgFormat("DecompressNotStorage"), innerException);
				}
				IStream o = DrmEmailUtils.EnsureStream(storage, this.EncryptedEmailMessageStreamName);
				Marshal.ReleaseComObject(o);
				EncryptedEmailMessageContainer<T>.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Loaded the encrypted message content stream with name:{0}", this.EncryptedEmailMessageStreamName);
				this.ReadBinding(storage);
				flag = true;
				EncryptedEmailMessageContainer<T>.Tracer.TraceDebug((long)this.GetHashCode(), "Loaded the binding information from the message stream.");
				this.rootStorage = storage;
				this.temporaryStream = stream2;
			}
			finally
			{
				if (!flag)
				{
					if (storage != null)
					{
						Marshal.ReleaseComObject(storage);
					}
					if (stream2 != null)
					{
						stream2.Close();
					}
				}
			}
		}

		// Token: 0x060034ED RID: 13549 RVA: 0x00086290 File Offset: 0x00084490
		public void Save(Stream stream, EncryptedEmailMessageBinding messageBinding)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (messageBinding == null)
			{
				throw new ArgumentNullException("messageBinding");
			}
			if (this.emailMessage == null)
			{
				throw new InvalidOperationException("Object must be loaded to perform this operation.");
			}
			IStorage storage = null;
			try
			{
				EncryptedEmailMessageContainer<T>.Tracer.TraceDebug((long)this.GetHashCode(), "Saving the encrypted message stream");
				storage = DrmEmailUtils.CreateStorageOverStream(stream);
				EncryptedEmailMessageContainer<T>.Tracer.TraceDebug((long)this.GetHashCode(), "Writing the email message");
				EncryptedEmailMessageContainer<T>.WriteEncryptedEmailMessage(storage, this.EncryptedEmailMessageStreamName, messageBinding, this.emailMessage);
				EncryptedEmailMessageContainer<T>.Tracer.TraceDebug((long)this.GetHashCode(), "Writing the email message binding");
				this.WriteBinding(storage);
				EncryptedEmailMessageContainer<T>.Tracer.TraceDebug((long)this.GetHashCode(), "Writing the email message container succeeded");
				storage.Commit(STGC.STGC_DEFAULT);
			}
			finally
			{
				if (storage != null)
				{
					Marshal.ReleaseComObject(storage);
					storage = null;
				}
			}
			DrmEmailCompression.CompressStream(stream, true);
			EncryptedEmailMessageContainer<T>.Tracer.TraceDebug((long)this.GetHashCode(), "Save completed and compressed stream");
		}

		// Token: 0x060034EE RID: 13550 RVA: 0x00086394 File Offset: 0x00084594
		public void Bind(EncryptedEmailMessageBinding messageBinding, CreateStreamCallbackDelegate createBodyStreamCallback, CreateStreamCallbackDelegate createAttachmentStreamCallback)
		{
			if (messageBinding == null)
			{
				throw new ArgumentNullException("messageBinding");
			}
			if (createBodyStreamCallback == null)
			{
				throw new ArgumentNullException("createBodyStreamCallback");
			}
			if (createAttachmentStreamCallback == null)
			{
				throw new ArgumentNullException("createAttachmentStreamCallback");
			}
			if (this.emailMessage != null)
			{
				throw new InvalidOperationException("The object is already bound.");
			}
			EncryptedEmailMessageContainer<T>.Tracer.TraceDebug((long)this.GetHashCode(), "Binding encrypted message storage");
			this.emailMessage = EncryptedEmailMessageContainer<T>.ReadEncryptedEmailMessage(this.rootStorage, this.EncryptedEmailMessageStreamName, messageBinding, createBodyStreamCallback, createAttachmentStreamCallback);
			EncryptedEmailMessageContainer<T>.Tracer.TraceDebug((long)this.GetHashCode(), "Binding encrypted message storage succeeded");
			Marshal.ReleaseComObject(this.rootStorage);
			this.rootStorage = null;
			this.temporaryStream.Close();
			this.temporaryStream = null;
			this.OnBound();
		}

		// Token: 0x060034EF RID: 13551 RVA: 0x00086454 File Offset: 0x00084654
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<EncryptedEmailMessageContainer<T>>(this);
		}

		// Token: 0x060034F0 RID: 13552 RVA: 0x0008645C File Offset: 0x0008465C
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060034F1 RID: 13553 RVA: 0x00086471 File Offset: 0x00084671
		public void Dispose()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060034F2 RID: 13554
		protected abstract void ReadBinding(IStorage bindingRootStorage);

		// Token: 0x060034F3 RID: 13555
		protected abstract void WriteBinding(IStorage bindingRootStorage);

		// Token: 0x060034F4 RID: 13556 RVA: 0x00086493 File Offset: 0x00084693
		protected virtual void OnBound()
		{
		}

		// Token: 0x060034F5 RID: 13557 RVA: 0x00086498 File Offset: 0x00084698
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed && disposing)
			{
				EncryptedEmailMessageContainer<T>.Tracer.TraceDebug((long)this.GetHashCode(), "Disposing the encrypted email message container");
				if (this.rootStorage != null)
				{
					Marshal.ReleaseComObject(this.rootStorage);
					this.rootStorage = null;
				}
				if (this.temporaryStream != null)
				{
					this.temporaryStream.Close();
					this.temporaryStream = null;
				}
			}
			this.disposed = true;
		}

		// Token: 0x04002CFA RID: 11514
		private static readonly Trace Tracer = ExTraceGlobals.RightsManagementTracer;

		// Token: 0x04002CFB RID: 11515
		private DisposeTracker disposeTracker;

		// Token: 0x04002CFC RID: 11516
		private bool disposed;

		// Token: 0x04002CFD RID: 11517
		private T emailMessage;

		// Token: 0x04002CFE RID: 11518
		private IStorage rootStorage;

		// Token: 0x04002CFF RID: 11519
		private Stream temporaryStream;
	}
}
