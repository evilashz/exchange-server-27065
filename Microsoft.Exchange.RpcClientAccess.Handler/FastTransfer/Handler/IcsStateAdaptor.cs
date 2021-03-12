using System;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler
{
	// Token: 0x02000070 RID: 112
	internal sealed class IcsStateAdaptor : BaseObject, IIcsState, IDisposable
	{
		// Token: 0x0600047A RID: 1146 RVA: 0x0001FFB0 File Offset: 0x0001E1B0
		public IcsStateAdaptor(IcsState icsState, ReferenceCount<CoreFolder> propertyMappingReference)
		{
			this.propertyMappingReference = propertyMappingReference;
			this.propertyMappingReference.AddRef();
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.propertyBag = IcsStateHelper.CreateInMemoryPropertyBag(propertyMappingReference.ReferencedObject);
				icsState.Checkpoint(this.propertyBag);
				foreach (PropertyTag propertyTag in FastTransferIcsState.AllIcsStateProperties)
				{
					AnnotatedPropertyValue annotatedProperty = this.propertyBag.GetAnnotatedProperty(propertyTag);
					if (!annotatedProperty.PropertyValue.IsError && annotatedProperty.PropertyValue.PropertyTag.PropertyType == PropertyType.Binary)
					{
						byte[] valueAssert = annotatedProperty.PropertyValue.GetValueAssert<byte[]>();
						if (valueAssert != null && valueAssert.Length > 0)
						{
							using (BufferReader bufferReader = Reader.CreateBufferReader(valueAssert))
							{
								try
								{
									IdSet.ParseWithReplGuids(bufferReader);
								}
								catch (BufferParseException)
								{
									int num = Math.Min(valueAssert.Length, 512);
									StringBuilder stringBuilder = new StringBuilder(num * 2);
									for (int j = valueAssert.Length - num; j < valueAssert.Length; j++)
									{
										stringBuilder.Append(valueAssert[j].ToString("X2"));
									}
									throw new RopExecutionException(string.Format("ICS state property {0} appears to be corrupt [{1}][{2}]", annotatedProperty.PropertyTag, valueAssert.Length, stringBuilder.ToString()), (ErrorCode)2147500037U);
								}
							}
						}
					}
				}
				disposeGuard.Success();
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x00020190 File Offset: 0x0001E390
		IPropertyBag IIcsState.PropertyBag
		{
			get
			{
				base.CheckDisposed();
				return this.propertyBag;
			}
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0002019E File Offset: 0x0001E39E
		void IIcsState.Flush()
		{
			throw new NotSupportedException("We have no known scenarios for IcsState upload");
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x000201AA File Offset: 0x0001E3AA
		protected override void InternalDispose()
		{
			this.propertyMappingReference.Release();
			base.InternalDispose();
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x000201BE File Offset: 0x0001E3BE
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<IcsStateAdaptor>(this);
		}

		// Token: 0x040001A1 RID: 417
		private readonly IPropertyBag propertyBag;

		// Token: 0x040001A2 RID: 418
		private readonly ReferenceCount<CoreFolder> propertyMappingReference;
	}
}
