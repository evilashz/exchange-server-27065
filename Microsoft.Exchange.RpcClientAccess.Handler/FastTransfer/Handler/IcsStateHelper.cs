using System;
using System.IO;
using System.Linq;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler
{
	// Token: 0x02000071 RID: 113
	internal sealed class IcsStateHelper : BaseObject
	{
		// Token: 0x0600047F RID: 1151 RVA: 0x000201C8 File Offset: 0x0001E3C8
		public IcsStateHelper(ReferenceCount<CoreFolder> propertyMappingReference)
		{
			this.propertyMappingReference = propertyMappingReference;
			this.propertyMappingReference.AddRef();
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.propertyBag = IcsStateHelper.CreateInMemoryPropertyBag(propertyMappingReference.ReferencedObject);
				this.icsState = new IcsState();
				disposeGuard.Success();
			}
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00020240 File Offset: 0x0001E440
		public FastTransferIcsState CreateIcsStateFastTransferObject()
		{
			base.CheckDisposed();
			this.EnsureClientInitialStateLoaded();
			FastTransferIcsState result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				IcsStateAdaptor disposable = new IcsStateAdaptor(this.icsState, this.propertyMappingReference);
				disposeGuard.Add<IcsStateAdaptor>(disposable);
				FastTransferIcsState fastTransferIcsState = new FastTransferIcsState(disposable);
				disposeGuard.Success();
				result = fastTransferIcsState;
			}
			return result;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x000202D0 File Offset: 0x0001E4D0
		public void BeginUploadProperty(PropertyTag property, uint dataSize)
		{
			base.CheckDisposed();
			this.icsState.CheckCanLoad(IcsStateOrigin.ClientInitial);
			PropertyTag propertyTag = FastTransferIcsState.AllIcsStateProperties.FirstOrDefault((PropertyTag icsStateProp) => icsStateProp.PropertyId == property.PropertyId);
			if (propertyTag == default(PropertyTag))
			{
				throw new RopExecutionException(string.Format("Expected an IcsState property. Found {0}.", property), (ErrorCode)2147746050U);
			}
			if (this.currentPropertyStream != null)
			{
				throw new RopExecutionException("Can't begin upload of a property before finishing upload of another one", (ErrorCode)2147500037U);
			}
			if (!this.propertyBag.GetAnnotatedProperty(propertyTag).PropertyValue.IsNotFound)
			{
				throw new RopExecutionException("Can't begin upload of a property for the second time", (ErrorCode)2147500037U);
			}
			this.currentPropertyStream = this.propertyBag.SetPropertyStream(propertyTag, (long)((ulong)dataSize));
			this.currentPropertyStream.SetLength((long)((ulong)dataSize));
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x000203A8 File Offset: 0x0001E5A8
		public void UploadPropertyData(ArraySegment<byte> data)
		{
			base.CheckDisposed();
			this.icsState.CheckCanLoad(IcsStateOrigin.ClientInitial);
			if (this.currentPropertyStream == null || (long)data.Count > this.currentPropertyStream.Length - this.currentPropertyStream.Position)
			{
				throw new RopExecutionException("Can't upload data for a property in excess of what was declared, or without initiating upload first with BeginUploadProperty", (ErrorCode)2147500037U);
			}
			this.currentPropertyStream.Write(data.Array, data.Offset, data.Count);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00020420 File Offset: 0x0001E620
		public void EndUploadProperty()
		{
			base.CheckDisposed();
			this.icsState.CheckCanLoad(IcsStateOrigin.ClientInitial);
			try
			{
				if (this.currentPropertyStream == null || this.currentPropertyStream.Length != this.currentPropertyStream.Position)
				{
					throw new RopExecutionException("Property upload was not initiated with BeginUploadProperty or data for a property ended prematurely", (ErrorCode)2147500037U);
				}
				this.needToLoadIcsStateFromPropertyBag = true;
			}
			finally
			{
				Util.DisposeIfPresent(this.currentPropertyStream);
				this.currentPropertyStream = null;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x0002049C File Offset: 0x0001E69C
		public IcsState IcsState
		{
			get
			{
				base.CheckDisposed();
				this.EnsureClientInitialStateLoaded();
				return this.icsState;
			}
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x000204B0 File Offset: 0x0001E6B0
		internal static IPropertyBag CreateInMemoryPropertyBag(ICoreObject propertyMappingReference)
		{
			return new MemoryPropertyBag(new SessionAdaptor(propertyMappingReference.Session));
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x000204C2 File Offset: 0x0001E6C2
		private void EnsureClientInitialStateLoaded()
		{
			if (this.currentPropertyStream != null)
			{
				throw new RopExecutionException("State property uploads in progress", (ErrorCode)2147500037U);
			}
			if (this.needToLoadIcsStateFromPropertyBag)
			{
				this.icsState.Load(IcsStateOrigin.ClientInitial, this.propertyBag);
				this.needToLoadIcsStateFromPropertyBag = false;
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x000204FD File Offset: 0x0001E6FD
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<IcsStateHelper>(this);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00020505 File Offset: 0x0001E705
		protected override void InternalDispose()
		{
			this.propertyMappingReference.Release();
			Util.DisposeIfPresent(this.currentPropertyStream);
			base.InternalDispose();
		}

		// Token: 0x040001A3 RID: 419
		private readonly IcsState icsState;

		// Token: 0x040001A4 RID: 420
		private readonly IPropertyBag propertyBag;

		// Token: 0x040001A5 RID: 421
		private readonly ReferenceCount<CoreFolder> propertyMappingReference;

		// Token: 0x040001A6 RID: 422
		private Stream currentPropertyStream;

		// Token: 0x040001A7 RID: 423
		private bool needToLoadIcsStateFromPropertyBag = true;
	}
}
