using System;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000079 RID: 121
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class MapiStreamWrapper : Stream, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000877 RID: 2167 RVA: 0x000400D8 File Offset: 0x0003E2D8
		private static T CallMapiWithReturnValue<T>(string methodName, object obj, StoreSession session, StorageGlobals.MapiCallWithReturnValue<T> mapiCall)
		{
			bool flag = false;
			T result;
			try
			{
				if (session != null)
				{
					session.BeginMapiCall();
					session.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				result = mapiCall();
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.ExWrappedStreamFailure, ex, session, obj, "{0}. MapiException = {1}.", new object[]
				{
					string.Format(methodName, new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.ExWrappedStreamFailure, ex2, session, obj, "{0}. MapiException = {1}.", new object[]
				{
					string.Format(methodName, new object[0]),
					ex2
				});
			}
			finally
			{
				try
				{
					if (session != null)
					{
						session.EndMapiCall();
						if (flag)
						{
							session.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
			return result;
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x000401EC File Offset: 0x0003E3EC
		internal static void CallMapi(string methodName, object obj, StoreSession session, StorageGlobals.MapiCall mapiCall)
		{
			bool flag = false;
			try
			{
				if (session != null)
				{
					session.BeginMapiCall();
					session.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				mapiCall();
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.ExWrappedStreamFailure, ex, session, obj, "{0}. MapiException = {1}.", new object[]
				{
					string.Format(methodName, new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.ExWrappedStreamFailure, ex2, session, obj, "{0}. MapiException = {1}.", new object[]
				{
					string.Format(methodName, new object[0]),
					ex2
				});
			}
			finally
			{
				try
				{
					if (session != null)
					{
						session.EndMapiCall();
						if (flag)
						{
							session.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x000402FC File Offset: 0x0003E4FC
		internal MapiStreamWrapper(MapiStream stream, StoreSession session)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.disposeTracker = this.GetDisposeTracker();
				this.session = session;
				if (stream == null)
				{
					throw new ArgumentNullException("stream");
				}
				this.mapiStream = stream;
				disposeGuard.Success();
			}
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00040368 File Offset: 0x0003E568
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MapiStreamWrapper>(this);
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00040370 File Offset: 0x0003E570
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00040394 File Offset: 0x0003E594
		protected override void Dispose(bool isDisposing)
		{
			base.Dispose(isDisposing);
			if (isDisposing)
			{
				this.isDisposed = true;
				this.disposeTracker.Dispose();
				MapiStreamWrapper.CallMapi("Dispose", this, this.session, delegate
				{
					this.mapiStream.Dispose();
				});
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x000403EE File Offset: 0x0003E5EE
		public override bool CanRead
		{
			get
			{
				this.CheckDisposed("CanRead");
				return MapiStreamWrapper.CallMapiWithReturnValue<bool>("CanRead", this, this.session, () => this.mapiStream.CanRead);
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x00040425 File Offset: 0x0003E625
		public override bool CanWrite
		{
			get
			{
				this.CheckDisposed("CanWrite");
				return MapiStreamWrapper.CallMapiWithReturnValue<bool>("CanWrite", this, this.session, () => this.mapiStream.CanWrite);
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x0004045C File Offset: 0x0003E65C
		public override bool CanSeek
		{
			get
			{
				this.CheckDisposed("CanSeek");
				return MapiStreamWrapper.CallMapiWithReturnValue<bool>("CanSeek", this, this.session, () => this.mapiStream.CanSeek);
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000880 RID: 2176 RVA: 0x00040493 File Offset: 0x0003E693
		public override long Length
		{
			get
			{
				this.CheckDisposed("Length:get");
				return MapiStreamWrapper.CallMapiWithReturnValue<long>("Length", this, this.session, () => this.mapiStream.Length);
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x000404CA File Offset: 0x0003E6CA
		// (set) Token: 0x06000882 RID: 2178 RVA: 0x00040514 File Offset: 0x0003E714
		public override long Position
		{
			get
			{
				this.CheckDisposed("Position:get");
				return MapiStreamWrapper.CallMapiWithReturnValue<long>("Position::get", this, this.session, () => this.mapiStream.Position);
			}
			set
			{
				this.CheckDisposed("Position:set");
				MapiStreamWrapper.CallMapi("Position::set", this, this.session, delegate
				{
					this.mapiStream.Position = value;
				});
			}
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0004058C File Offset: 0x0003E78C
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.CheckDisposed("Read");
			return MapiStreamWrapper.CallMapiWithReturnValue<int>("Read", this, this.session, () => this.mapiStream.Read(buffer, offset, count));
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x000405F0 File Offset: 0x0003E7F0
		public override void Flush()
		{
			this.CheckDisposed("Flush");
			MapiStreamWrapper.CallMapi("Flush", this, this.session, delegate
			{
				this.mapiStream.Flush();
			});
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00040640 File Offset: 0x0003E840
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.CheckDisposed("Seek");
			return MapiStreamWrapper.CallMapiWithReturnValue<long>("Seek", this, this.session, () => this.mapiStream.Seek(offset, origin));
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x000406B0 File Offset: 0x0003E8B0
		public override void SetLength(long length)
		{
			this.CheckDisposed("SetLength");
			MapiStreamWrapper.CallMapi("SetLength", this, this.session, delegate
			{
				this.mapiStream.SetLength(length);
			});
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00040728 File Offset: 0x0003E928
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.CheckDisposed("Write");
			MapiStreamWrapper.CallMapi("Write", this, this.session, delegate
			{
				this.mapiStream.Write(buffer, offset, count);
			});
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x000407AC File Offset: 0x0003E9AC
		public void LockRegion(long offset, long cb, int lockType)
		{
			this.CheckDisposed("LockRegion");
			MapiStreamWrapper.CallMapi("LockRegion", this, this.session, delegate
			{
				this.mapiStream.LockRegion(offset, cb, lockType);
			});
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00040830 File Offset: 0x0003EA30
		public void UnlockRegion(long offset, long cb, int lockType)
		{
			this.CheckDisposed("UnlockRegion");
			MapiStreamWrapper.CallMapi("UnlockRegion", this, this.session, delegate
			{
				this.mapiStream.UnlockRegion(offset, cb, lockType);
			});
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00040887 File Offset: 0x0003EA87
		private void CheckDisposed(string methodName)
		{
			if (this.isDisposed)
			{
				StorageGlobals.TraceFailedCheckDisposed(this, methodName);
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x0400023D RID: 573
		private readonly MapiStream mapiStream;

		// Token: 0x0400023E RID: 574
		private readonly DisposeTracker disposeTracker;

		// Token: 0x0400023F RID: 575
		private StoreSession session;

		// Token: 0x04000240 RID: 576
		private bool isDisposed;
	}
}
