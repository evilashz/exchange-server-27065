using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser
{
	// Token: 0x0200019E RID: 414
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MemoryPropertyBag : IPropertyBag
	{
		// Token: 0x06000828 RID: 2088 RVA: 0x0001C87B File Offset: 0x0001AA7B
		public MemoryPropertyBag(ISession session) : this(session, 1048576)
		{
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0001C889 File Offset: 0x0001AA89
		public MemoryPropertyBag(ISession session, int maxStreamLength)
		{
			Util.ThrowOnNullArgument(session, "session");
			this.session = session;
			this.maxStreamLength = maxStreamLength;
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0001C8BC File Offset: 0x0001AABC
		public virtual AnnotatedPropertyValue GetAnnotatedProperty(PropertyTag propertyTag)
		{
			PropertyValue property = this.GetProperty(propertyTag);
			return this.GetAnnotatedProperty(propertyTag, property);
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0001C8E9 File Offset: 0x0001AAE9
		public IEnumerable<AnnotatedPropertyValue> GetAnnotatedProperties()
		{
			return from propertyValue in this.properties.Values
			select this.GetAnnotatedProperty(propertyValue.PropertyTag, propertyValue);
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0001C908 File Offset: 0x0001AB08
		public void SetProperty(PropertyValue propertyValue)
		{
			this.properties[propertyValue.PropertyTag.PropertyId] = propertyValue;
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0001C930 File Offset: 0x0001AB30
		public void Delete(PropertyTag property)
		{
			this.properties.Remove(property.PropertyId);
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0001C948 File Offset: 0x0001AB48
		public Stream GetPropertyStream(PropertyTag propertyTag)
		{
			PropertyValue property = this.GetProperty(propertyTag);
			return MemoryPropertyBag.WrapPropertyReadStream(property);
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0001C963 File Offset: 0x0001AB63
		public Stream SetPropertyStream(PropertyTag property, long dataSizeEstimate)
		{
			return MemoryPropertyBag.WrapPropertyWriteStream(this, property, dataSizeEstimate, this.maxStreamLength);
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x0001C973 File Offset: 0x0001AB73
		public ISession Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0001C97C File Offset: 0x0001AB7C
		internal static PropertyValue ConvertToRequestedType(PropertyValue propertyValue, PropertyType requestedPropertyType)
		{
			if (!propertyValue.IsError && propertyValue.PropertyTag.PropertyType != requestedPropertyType)
			{
				throw Feature.NotImplemented(0, string.Format("Property value conversion is not implemented yet: requested = {0}, stored = {1}", requestedPropertyType, propertyValue));
			}
			return propertyValue;
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0001C9C4 File Offset: 0x0001ABC4
		internal static Stream WrapPropertyReadStream(PropertyValue propertyValue)
		{
			FastTransferPropertyValue.CheckVariableSizePropertyType(propertyValue.PropertyTag.PropertyType);
			byte[] buffer;
			switch (propertyValue.PropertyTag.PropertyType)
			{
			case PropertyType.String8:
			case PropertyType.Unicode:
				buffer = Encoding.Unicode.GetBytes(propertyValue.GetValueAssert<string>());
				break;
			default:
				buffer = propertyValue.GetValueAssert<byte[]>();
				break;
			}
			return new MemoryStream(buffer, false);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0001CA2D File Offset: 0x0001AC2D
		internal static Stream WrapPropertyWriteStream(IPropertyBag propertyBag, PropertyTag propertyTag, long dataSizeEstimate)
		{
			return MemoryPropertyBag.WrapPropertyWriteStream(propertyBag, propertyTag, dataSizeEstimate, 1048576);
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0001CA3C File Offset: 0x0001AC3C
		internal static Stream WrapPropertyWriteStream(IPropertyBag propertyBag, PropertyTag propertyTag, long dataSizeEstimate, int maxStreamLength)
		{
			return new MemoryPropertyBag.WriteMemoryStream(propertyBag, propertyTag, dataSizeEstimate, maxStreamLength);
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0001CA48 File Offset: 0x0001AC48
		private AnnotatedPropertyValue GetAnnotatedProperty(PropertyTag propertyTag, PropertyValue propertyValue)
		{
			NamedProperty namedProperty = null;
			if (propertyTag.IsNamedProperty)
			{
				this.Session.TryResolveToNamedProperty(propertyTag, out namedProperty);
			}
			return new AnnotatedPropertyValue(propertyTag, propertyValue, namedProperty);
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0001CA78 File Offset: 0x0001AC78
		private PropertyValue GetProperty(PropertyTag property)
		{
			PropertyValue propertyValue;
			if (this.properties.TryGetValue(property.PropertyId, out propertyValue))
			{
				return MemoryPropertyBag.ConvertToRequestedType(propertyValue, property.PropertyType);
			}
			return PropertyValue.Error(property.PropertyId, (ErrorCode)2147746063U);
		}

		// Token: 0x040003E2 RID: 994
		private readonly Dictionary<PropertyId, PropertyValue> properties = new Dictionary<PropertyId, PropertyValue>(PropertyIdComparer.Instance);

		// Token: 0x040003E3 RID: 995
		private readonly ISession session;

		// Token: 0x040003E4 RID: 996
		private readonly int maxStreamLength;

		// Token: 0x0200019F RID: 415
		internal sealed class WriteMemoryStream : MemoryStream, IDisposeTrackable, IDisposable
		{
			// Token: 0x06000838 RID: 2104 RVA: 0x0001CABA File Offset: 0x0001ACBA
			public WriteMemoryStream(IPropertyBag propertyBag, PropertyTag propertyTag, long capacity) : this(propertyBag, propertyTag, capacity, 1048576)
			{
			}

			// Token: 0x06000839 RID: 2105 RVA: 0x0001CACC File Offset: 0x0001ACCC
			public WriteMemoryStream(IPropertyBag propertyBag, PropertyTag propertyTag, long capacity, int maxStreamLength) : base(MemoryPropertyBag.WriteMemoryStream.CheckLength(capacity, maxStreamLength))
			{
				FastTransferPropertyValue.CheckVariableSizePropertyType(propertyTag.PropertyType);
				this.propertyBag = propertyBag;
				this.propertyTag = propertyTag;
				this.maxStreamLength = maxStreamLength;
				this.disposeTracker = this.GetDisposeTracker();
				this.isDirty = true;
			}

			// Token: 0x0600083A RID: 2106 RVA: 0x0001CB1C File Offset: 0x0001AD1C
			public override void Flush()
			{
				base.Flush();
				if (this.isDirty)
				{
					object value;
					if (this.propertyTag.PropertyType == PropertyType.Binary || this.propertyTag.PropertyType == PropertyType.Object || this.propertyTag.PropertyType == PropertyType.ServerId)
					{
						value = ((this.Length == (long)this.Capacity) ? this.GetBuffer() : this.ToArray());
					}
					else if (this.propertyTag.PropertyType == PropertyType.Unicode)
					{
						value = Encoding.Unicode.GetString(this.GetBuffer(), 0, (int)this.Length);
					}
					else
					{
						if (this.propertyTag.PropertyType != PropertyType.String8)
						{
							throw new InvalidOperationException(string.Format("Cannot stream properties of type {0}.", this.propertyTag.PropertyType));
						}
						Feature.Stubbed(54718, "String8 support in FastTransfer");
						value = CTSGlobals.AsciiEncoding.GetString(this.GetBuffer(), 0, (int)this.Length);
					}
					this.propertyBag.SetProperty(new PropertyValue(this.propertyTag, value));
					this.isDirty = false;
				}
			}

			// Token: 0x0600083B RID: 2107 RVA: 0x0001CC46 File Offset: 0x0001AE46
			public override void SetLength(long value)
			{
				MemoryPropertyBag.WriteMemoryStream.CheckLength(value, this.maxStreamLength);
				this.isDirty = true;
				base.SetLength(value);
			}

			// Token: 0x0600083C RID: 2108 RVA: 0x0001CC63 File Offset: 0x0001AE63
			public override void Write(byte[] buffer, int offset, int count)
			{
				MemoryPropertyBag.WriteMemoryStream.CheckLength(this.Length + (long)count, this.maxStreamLength);
				this.isDirty = true;
				base.Write(buffer, offset, count);
			}

			// Token: 0x0600083D RID: 2109 RVA: 0x0001CC8A File Offset: 0x0001AE8A
			public override void WriteByte(byte value)
			{
				MemoryPropertyBag.WriteMemoryStream.CheckLength(this.Length + 1L, this.maxStreamLength);
				this.isDirty = true;
				base.WriteByte(value);
			}

			// Token: 0x0600083E RID: 2110 RVA: 0x0001CCAF File Offset: 0x0001AEAF
			protected sealed override void Dispose(bool disposing)
			{
				if (disposing && !this.isDisposed)
				{
					this.isDisposed = true;
					this.InternalDispose();
					GC.SuppressFinalize(this);
				}
				base.Dispose(disposing);
			}

			// Token: 0x0600083F RID: 2111 RVA: 0x0001CCD6 File Offset: 0x0001AED6
			private static int CheckLength(long length, int maxStreamLength)
			{
				if (length > (long)maxStreamLength)
				{
					throw new RopExecutionException(string.Format("Memory property streams size limit exceeded. Size: '{0}', Limit: '{1}'", length, maxStreamLength), (ErrorCode)2147942414U);
				}
				return (int)length;
			}

			// Token: 0x06000840 RID: 2112 RVA: 0x0001CD00 File Offset: 0x0001AF00
			DisposeTracker IDisposeTrackable.GetDisposeTracker()
			{
				return this.GetDisposeTracker();
			}

			// Token: 0x06000841 RID: 2113 RVA: 0x0001CD08 File Offset: 0x0001AF08
			private DisposeTracker GetDisposeTracker()
			{
				return DisposeTracker.Get<MemoryPropertyBag.WriteMemoryStream>(this);
			}

			// Token: 0x06000842 RID: 2114 RVA: 0x0001CD10 File Offset: 0x0001AF10
			void IDisposeTrackable.SuppressDisposeTracker()
			{
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Suppress();
				}
			}

			// Token: 0x06000843 RID: 2115 RVA: 0x0001CD25 File Offset: 0x0001AF25
			private void InternalDispose()
			{
				Util.DisposeIfPresent(this.disposeTracker);
				this.Flush();
			}

			// Token: 0x06000844 RID: 2116 RVA: 0x0001CD38 File Offset: 0x0001AF38
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				MemoryPropertyBag.WriteMemoryStream.CheckLength(this.Length + (long)count, this.maxStreamLength);
				this.isDirty = true;
				return base.BeginWrite(buffer, offset, count, callback, state);
			}

			// Token: 0x06000845 RID: 2117 RVA: 0x0001CD63 File Offset: 0x0001AF63
			public override void EndWrite(IAsyncResult asyncResult)
			{
				MemoryPropertyBag.WriteMemoryStream.CheckLength(this.Length, this.maxStreamLength);
				this.isDirty = true;
				base.EndWrite(asyncResult);
			}

			// Token: 0x040003E5 RID: 997
			private readonly IPropertyBag propertyBag;

			// Token: 0x040003E6 RID: 998
			private readonly PropertyTag propertyTag;

			// Token: 0x040003E7 RID: 999
			private readonly int maxStreamLength;

			// Token: 0x040003E8 RID: 1000
			private bool isDisposed;

			// Token: 0x040003E9 RID: 1001
			private DisposeTracker disposeTracker;

			// Token: 0x040003EA RID: 1002
			private bool isDirty;
		}
	}
}
