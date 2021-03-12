using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.FastTransfer;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Protocols.MAPI;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.FastTransfer
{
	// Token: 0x0200000B RID: 11
	internal class FastTransferPropertyBag : DisposableBase, IPropertyBag
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00003588 File Offset: 0x00001788
		public FastTransferPropertyBag(FastTransferDownloadContext downloadContext, MapiPropBagBase mapiPropBag, bool excludeProps, HashSet<StorePropTag> propList)
		{
			this.context = downloadContext;
			this.mapiPropBag = mapiPropBag;
			this.excludeProps = excludeProps;
			this.propList = propList;
			this.forUpload = ((byte)(downloadContext.SendOptions & FastTransferSendOption.Upload) == 3);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000035DA File Offset: 0x000017DA
		public FastTransferPropertyBag(FastTransferUploadContext uploadContext, MapiPropBagBase mapiPropBag)
		{
			this.context = uploadContext;
			this.mapiPropBag = mapiPropBag;
			this.excludeProps = true;
			this.propList = null;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000059 RID: 89 RVA: 0x0000360E File Offset: 0x0000180E
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00003616 File Offset: 0x00001816
		public MapiPropBagBase MapiPropBag
		{
			get
			{
				return this.mapiPropBag;
			}
			set
			{
				this.mapiPropBag = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005B RID: 91 RVA: 0x0000361F File Offset: 0x0000181F
		public bool ReadOnly
		{
			get
			{
				return this.context is FastTransferDownloadContext;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005C RID: 92 RVA: 0x0000362F File Offset: 0x0000182F
		public FastTransferContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003637 File Offset: 0x00001837
		public FastTransferDownloadContext DownloadContext
		{
			get
			{
				return this.context as FastTransferDownloadContext;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003644 File Offset: 0x00001844
		public FastTransferUploadContext UploadContext
		{
			get
			{
				return this.context as FastTransferUploadContext;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00003651 File Offset: 0x00001851
		public bool ExcludeProps
		{
			get
			{
				return this.excludeProps;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003659 File Offset: 0x00001859
		public HashSet<StorePropTag> PropList
		{
			get
			{
				return this.propList;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003661 File Offset: 0x00001861
		public ISession Session
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003888 File Offset: 0x00001A88
		internal IEnumerable<PropertyTag> GetPropertyList()
		{
			this.loadedProperties = this.LoadAllPropertiesImp();
			if (ExTraceGlobals.SourceSendTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(200);
				stringBuilder.Append("Send Props=[");
				stringBuilder.AppendAsString(this.loadedProperties);
				stringBuilder.Append("]");
				ExTraceGlobals.SourceSendTracer.TraceDebug(0L, stringBuilder.ToString());
			}
			foreach (Property property in this.loadedProperties)
			{
				Property property2 = property;
				yield return new PropertyTag(property2.Tag.PropTag);
			}
			yield break;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000038A8 File Offset: 0x00001AA8
		public AnnotatedPropertyValue GetAnnotatedProperty(PropertyTag propertyTag)
		{
			PropertyValue property = this.GetProperty(propertyTag);
			PropertyTag propertyTag2 = (property.PropertyTag.PropertyType == PropertyType.Error) ? propertyTag : property.PropertyTag;
			NamedProperty namedProperty = null;
			if (propertyTag2.IsNamedProperty)
			{
				this.Session.TryResolveToNamedProperty(propertyTag2, out namedProperty);
			}
			return new AnnotatedPropertyValue(propertyTag2, property, namedProperty);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003A98 File Offset: 0x00001C98
		public IEnumerable<AnnotatedPropertyValue> GetAnnotatedProperties()
		{
			foreach (PropertyTag propertyTag in this.GetPropertyList())
			{
				yield return this.GetAnnotatedProperty(propertyTag);
			}
			yield break;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003AB5 File Offset: 0x00001CB5
		protected bool ForUpload
		{
			get
			{
				return this.forUpload;
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003AC0 File Offset: 0x00001CC0
		private PropertyValue GetProperty(PropertyTag propertyTag)
		{
			StorePropTag storePropTag = LegacyHelper.ConvertFromLegacyPropTag(propertyTag, this.GetObjectTypeImp(), this.Context.Logon.MapiMailbox, true);
			Property prop;
			if (storePropTag == PropTag.Folder.PackedNamedProps)
			{
				prop = this.packedNamedProperties;
			}
			else
			{
				bool flag = false;
				int num = -1;
				if (this.loadedProperties != null)
				{
					num = this.loadedProperties.BinarySearch(new Property(storePropTag, null), PropertyComparerByTag.Comparer);
					flag = (num >= 0 && this.loadedProperties[num].Value != null);
				}
				if (flag)
				{
					prop = this.loadedProperties[num];
				}
				else
				{
					prop = this.GetPropertyImp(storePropTag);
				}
				if (prop.Tag.PropType == PropertyType.SvrEid)
				{
					prop = new Property(prop.Tag, Helper.ConvertServerEIdFromOursToExportFormat(this.mapiPropBag.Logon, (byte[])prop.Value));
				}
			}
			if (ExTraceGlobals.SourceSendTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				stringBuilder.Append("Send Property=[");
				prop.AppendToString(stringBuilder);
				stringBuilder.Append("]]");
				ExTraceGlobals.SourceSendTracer.TraceDebug(0L, stringBuilder.ToString());
			}
			return RcaTypeHelpers.MassageOutgoingProperty(prop, true);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003C00 File Offset: 0x00001E00
		public void SetProperty(PropertyValue propertyValue)
		{
			StorePropTag tag = LegacyHelper.ConvertFromLegacyPropTag(propertyValue.PropertyTag, this.GetObjectTypeImp(), this.Context.Logon.MapiMailbox, true);
			object value = propertyValue.Value;
			RcaTypeHelpers.MassageIncomingPropertyValue(propertyValue.PropertyTag, ref value);
			this.SetPropertyImp(new Property(tag, value));
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003C5C File Offset: 0x00001E5C
		public void Delete(PropertyTag propertyTag)
		{
			StorePropTag propTag = LegacyHelper.ConvertFromLegacyPropTag(propertyTag, this.GetObjectTypeImp(), this.Context.Logon.MapiMailbox, true);
			this.DeleteImp(propTag);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003C94 File Offset: 0x00001E94
		public Stream GetPropertyStream(PropertyTag propertyTag)
		{
			StorePropTag propTag = LegacyHelper.ConvertFromLegacyPropTag(propertyTag, this.GetObjectTypeImp(), this.Context.Logon.MapiMailbox, true);
			return this.GetPropertyStreamImp(propTag);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003CCC File Offset: 0x00001ECC
		public Stream SetPropertyStream(PropertyTag propertyTag, long dataSizeEstimate)
		{
			StorePropTag propTag = LegacyHelper.ConvertFromLegacyPropTag(propertyTag, this.GetObjectTypeImp(), this.Context.Logon.MapiMailbox, true);
			return this.SetPropertyStreamImp(propTag, dataSizeEstimate);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003D04 File Offset: 0x00001F04
		protected static void MovePropertyTagToSpecificPosition(List<StorePropTag> propertyList, StorePropTag propTag, int position)
		{
			int i = 0;
			while (i < propertyList.Count)
			{
				if (propertyList[i] == propTag)
				{
					if (i != position)
					{
						propertyList[i] = propertyList[position];
						propertyList[position] = propTag;
						return;
					}
					break;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003D4C File Offset: 0x00001F4C
		protected static void ResetPropertyIfPresent(List<Property> propertyList, StorePropTag propTag)
		{
			Property property = new Property(propTag, null);
			int num = propertyList.BinarySearch(property, PropertyComparerByTag.Comparer);
			if (num >= 0)
			{
				propertyList[num] = property;
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003D7C File Offset: 0x00001F7C
		protected static void AddNullPropertyIfNotPresent(List<Property> propertyList, StorePropTag propTag)
		{
			Property item = new Property(propTag, null);
			int num = propertyList.BinarySearch(item, PropertyComparerByTag.Comparer);
			if (num < 0)
			{
				propertyList.Insert(~num, item);
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003DAC File Offset: 0x00001FAC
		protected void Reinitialize(MapiPropBagBase mapiPropBag)
		{
			this.mapiPropBag = mapiPropBag;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003DB5 File Offset: 0x00001FB5
		protected bool ForMoveUser
		{
			get
			{
				return this.mapiPropBag.Logon.IsMoveUser;
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003DC7 File Offset: 0x00001FC7
		protected virtual ObjectType GetObjectTypeImp()
		{
			return Helper.GetPropTagObjectType(this.mapiPropBag.MapiObjectType);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003DDC File Offset: 0x00001FDC
		protected virtual List<Property> LoadAllPropertiesImp()
		{
			List<StorePropTag> list = null;
			List<Property> list2;
			if (!this.excludeProps && this.propList != null)
			{
				list2 = new List<Property>(this.propList.Count);
				foreach (StorePropTag propTag in this.propList)
				{
					list2.Add(this.GetPropertyImp(propTag));
				}
				ValueHelper.SortAndRemoveDuplicates<Property>(list2, PropertyComparerByTag.Comparer);
			}
			else
			{
				list2 = this.mapiPropBag.GetAllProperties(this.Context.CurrentOperationContext, GetPropListFlags.FastTransfer, true);
			}
			if (list2.Count != 0)
			{
				int num = 0;
				for (int i = 0; i < list2.Count; i++)
				{
					StorePropTag tag = list2[i].Tag;
					if (this.IncludeTag(tag))
					{
						if (num == 0 || list2[num - 1].Tag.PropId != tag.PropId)
						{
							if (i != num)
							{
								list2[num] = list2[i];
							}
							num++;
						}
						else if (tag.PropType == PropertyType.Binary)
						{
							list2[num - 1] = list2[i];
						}
						else if (tag.PropType == PropertyType.Int64)
						{
							list2[num - 1] = list2[i];
						}
					}
					else if (tag.IsNamedProperty && this.IncludeToPackedNamedProperties(tag))
					{
						if (list == null)
						{
							list = new List<StorePropTag>();
						}
						list.Add(tag);
					}
				}
				if (num != list2.Count)
				{
					list2.RemoveRange(num, list2.Count - num);
				}
				if (list != null && list.Count != 0)
				{
					this.packedNamedProperties = this.ComputePackedNamedPropertiesProperty(list);
					Property property = new Property(PropTag.Folder.PackedNamedProps, this.packedNamedProperties);
					int num2 = list2.BinarySearch(property, PropertyComparerByTag.Comparer);
					if (num2 < 0)
					{
						list2.Insert(~num2, property);
					}
					else
					{
						list2[num2] = property;
					}
				}
			}
			return list2;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003FE0 File Offset: 0x000021E0
		protected virtual Property GetPropertyImp(StorePropTag propTag)
		{
			return this.mapiPropBag.GetOneProp(this.Context.CurrentOperationContext, propTag);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003FFC File Offset: 0x000021FC
		protected virtual void SetPropertyImp(Property property)
		{
			object obj = property.Value;
			if (property.Tag.PropType == PropertyType.SvrEid)
			{
				obj = Helper.ConvertServerEIdFromExportToOursFormat(this.mapiPropBag.Logon, (byte[])obj);
			}
			if (ExTraceGlobals.SourceSendTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				stringBuilder.Append("Receive Property=[");
				property.AppendToString(stringBuilder);
				stringBuilder.Append("]]");
				ExTraceGlobals.SourceSendTracer.TraceDebug(0L, stringBuilder.ToString());
			}
			if (property.Tag == PropTag.Folder.PackedNamedProps)
			{
				MDBEFCollection mdbefcollection = MDBEFCollection.CreateFrom((byte[])obj, this.context.Logon.Encoding);
				using (IEnumerator<AnnotatedPropertyValue> enumerator = mdbefcollection.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						AnnotatedPropertyValue annotatedPropertyValue = enumerator.Current;
						PropertyTag propertyTag = annotatedPropertyValue.PropertyTag;
						if (this.Session.TryResolveFromNamedProperty(annotatedPropertyValue.NamedProperty, ref propertyTag))
						{
							StorePropTag tag = LegacyHelper.ConvertFromLegacyPropTag(propertyTag, this.GetObjectTypeImp(), this.Context.Logon.MapiMailbox, true);
							object value = annotatedPropertyValue.PropertyValue.Value;
							RcaTypeHelpers.MassageIncomingPropertyValue(propertyTag, ref value);
							this.SetPropertyImp(new Property(tag, value));
						}
					}
					return;
				}
			}
			this.mapiPropBag.SetOneProp(this.Context.CurrentOperationContext, property.Tag, obj);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000417C File Offset: 0x0000237C
		protected virtual void DeleteImp(StorePropTag propTag)
		{
			this.mapiPropBag.DeleteOneProp(this.Context.CurrentOperationContext, propTag);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004198 File Offset: 0x00002398
		public virtual Stream GetPropertyStreamImp(StorePropTag propTag)
		{
			FastTransferPropertyBag.MapiStreamWrapper mapiStreamWrapper = new FastTransferPropertyBag.MapiStreamWrapper(this, true);
			mapiStreamWrapper.Configure(propTag);
			if (ExTraceGlobals.SourceSendTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				stringBuilder.Append("Send Property Stream=[tag=[");
				propTag.AppendToString(stringBuilder);
				stringBuilder.Append("]]");
				ExTraceGlobals.SourceSendTracer.TraceDebug(0L, stringBuilder.ToString());
			}
			return mapiStreamWrapper;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000041FC File Offset: 0x000023FC
		public virtual Stream SetPropertyStreamImp(StorePropTag propTag, long dataSize)
		{
			FastTransferPropertyBag.MapiStreamWrapper mapiStreamWrapper = new FastTransferPropertyBag.MapiStreamWrapper(this, false);
			mapiStreamWrapper.Configure(propTag, dataSize);
			if (ExTraceGlobals.SourceSendTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				stringBuilder.Append("Receive Property Stream=[tag=[");
				propTag.AppendToString(stringBuilder);
				stringBuilder.Append("] size=[");
				stringBuilder.Append(dataSize.ToString());
				stringBuilder.Append("]]");
				ExTraceGlobals.SourceSendTracer.TraceDebug(0L, stringBuilder.ToString());
			}
			return mapiStreamWrapper;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000427B File Offset: 0x0000247B
		protected virtual bool IncludeTag(StorePropTag propTag)
		{
			if (this.excludeProps)
			{
				return this.propList == null || !this.propList.Contains(propTag);
			}
			return this.propList != null && this.propList.Contains(propTag);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000042B5 File Offset: 0x000024B5
		protected virtual bool IncludeToPackedNamedProperties(StorePropTag propTag)
		{
			return false;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000042B8 File Offset: 0x000024B8
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<FastTransferPropertyBag>(this);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000042C0 File Offset: 0x000024C0
		protected override void InternalDispose(bool isCalledFromDispose)
		{
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000042C4 File Offset: 0x000024C4
		private Property ComputePackedNamedPropertiesProperty(List<StorePropTag> namedPropertiesList)
		{
			MDBEFCollection mdbefcollection = new MDBEFCollection();
			foreach (StorePropTag storePropTag in namedPropertiesList)
			{
				AnnotatedPropertyValue annotatedProperty = this.GetAnnotatedProperty(new PropertyTag(storePropTag.PropTag));
				mdbefcollection.AddAnnotatedProperty(annotatedProperty);
			}
			return new Property(PropTag.Folder.PackedNamedProps, mdbefcollection.Serialize(this.context.Logon.Encoding));
		}

		// Token: 0x0400002C RID: 44
		private readonly bool forUpload;

		// Token: 0x0400002D RID: 45
		private FastTransferContext context;

		// Token: 0x0400002E RID: 46
		private MapiPropBagBase mapiPropBag;

		// Token: 0x0400002F RID: 47
		private bool excludeProps;

		// Token: 0x04000030 RID: 48
		private HashSet<StorePropTag> propList;

		// Token: 0x04000031 RID: 49
		private List<Property> loadedProperties;

		// Token: 0x04000032 RID: 50
		private Property packedNamedProperties = Property.NotFoundError(PropTag.Folder.PackedNamedProps);

		// Token: 0x0200000C RID: 12
		internal class MapiStreamWrapper : Stream
		{
			// Token: 0x0600007C RID: 124 RVA: 0x0000434C File Offset: 0x0000254C
			public MapiStreamWrapper(FastTransferPropertyBag fastTransferPropBag, bool readOnly)
			{
				this.fastTransferPropBag = fastTransferPropBag;
				this.readOnly = readOnly;
			}

			// Token: 0x0600007D RID: 125 RVA: 0x00004364 File Offset: 0x00002564
			public void Configure(StorePropTag propTag)
			{
				this.propTag = propTag;
				this.ResetMemoryBufferState();
				this.mapiStream = this.fastTransferPropBag.MapiPropBag.OpenStream(this.fastTransferPropBag.MapiPropBag.CurrentOperationContext, StreamFlags.AllowRead, propTag, this.fastTransferPropBag.MapiPropBag.Logon.Session.CodePage);
				this.streamFlushedToProperty = false;
			}

			// Token: 0x0600007E RID: 126 RVA: 0x000043C7 File Offset: 0x000025C7
			public void Configure(StorePropTag propTag, long dataSize)
			{
				this.propTag = propTag;
				this.ResetMemoryBufferState();
				this.EnsureUnderlyingStorageIsAllocated(dataSize);
				this.streamFlushedToProperty = false;
			}

			// Token: 0x1700001D RID: 29
			// (get) Token: 0x0600007F RID: 127 RVA: 0x000043E4 File Offset: 0x000025E4
			public override bool CanRead
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700001E RID: 30
			// (get) Token: 0x06000080 RID: 128 RVA: 0x000043E7 File Offset: 0x000025E7
			public override bool CanSeek
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x06000081 RID: 129 RVA: 0x000043EA File Offset: 0x000025EA
			public override bool CanWrite
			{
				get
				{
					return !this.readOnly;
				}
			}

			// Token: 0x06000082 RID: 130 RVA: 0x000043F8 File Offset: 0x000025F8
			public override void Flush()
			{
				if (this.mapiStream != null)
				{
					this.mapiStream.Commit(this.fastTransferPropBag.MapiPropBag.CurrentOperationContext);
				}
				else if (this.buffer != null)
				{
					BufferPool pool = TempStream.Pool;
					object value;
					if (this.propTag.PropType != PropertyType.Unicode)
					{
						if (this.length == this.buffer.Length && this.buffer.Length != pool.BufferSize)
						{
							value = this.buffer;
							this.buffer = null;
						}
						else
						{
							byte[] array = new byte[this.length];
							Buffer.BlockCopy(this.buffer, 0, array, 0, this.length);
							value = array;
						}
					}
					else if (this.propTag.ExternalType == PropertyType.String8)
					{
						value = CTSGlobals.AsciiEncoding.GetString(this.buffer, 0, this.length);
					}
					else
					{
						value = Encoding.Unicode.GetString(this.buffer, 0, this.length);
					}
					this.fastTransferPropBag.SetPropertyImp(new Property(this.propTag, value));
					if (this.buffer != null && this.buffer.Length == pool.BufferSize)
					{
						pool.Release(this.buffer);
					}
					this.ResetMemoryBufferState();
				}
				this.streamFlushedToProperty = true;
			}

			// Token: 0x17000020 RID: 32
			// (get) Token: 0x06000083 RID: 131 RVA: 0x0000452A File Offset: 0x0000272A
			public override long Length
			{
				get
				{
					if (this.mapiStream != null)
					{
						return this.mapiStream.GetSize(this.CurrentOperationContext);
					}
					return (long)this.length;
				}
			}

			// Token: 0x17000021 RID: 33
			// (get) Token: 0x06000084 RID: 132 RVA: 0x0000454D File Offset: 0x0000274D
			// (set) Token: 0x06000085 RID: 133 RVA: 0x00004573 File Offset: 0x00002773
			public override long Position
			{
				get
				{
					if (this.mapiStream != null)
					{
						return this.mapiStream.Seek(this.CurrentOperationContext, 0L, SeekOrigin.Current);
					}
					return (long)this.position;
				}
				set
				{
					this.EnsureUnderlyingStorageIsAllocated(value);
					if (this.mapiStream != null)
					{
						this.mapiStream.Seek(this.CurrentOperationContext, value, SeekOrigin.Begin);
						return;
					}
					this.position = (int)value;
				}
			}

			// Token: 0x06000086 RID: 134 RVA: 0x000045A4 File Offset: 0x000027A4
			public override int Read(byte[] buffer, int offset, int count)
			{
				if (this.mapiStream != null)
				{
					return this.mapiStream.Read(this.CurrentOperationContext, buffer, offset, count);
				}
				if (offset + count > buffer.Length)
				{
					throw new ExExceptionStreamInvalidParameter((LID)37944U, "Read offset out or range.");
				}
				int num = Math.Min(count, this.length - this.position);
				Buffer.BlockCopy(this.buffer, this.position, buffer, 0, num);
				return num;
			}

			// Token: 0x06000087 RID: 135 RVA: 0x00004614 File Offset: 0x00002814
			public override long Seek(long offset, SeekOrigin origin)
			{
				long num = 0L;
				if (this.mapiStream == null)
				{
					switch (origin)
					{
					case SeekOrigin.Begin:
						num = offset;
						break;
					case SeekOrigin.Current:
						num = (long)this.position + offset;
						break;
					case SeekOrigin.End:
						num = (long)this.position + offset;
						break;
					}
					if (num < 0L)
					{
						throw new ExExceptionStreamSeekError((LID)33848U, "Seek offset out of range");
					}
					this.EnsureUnderlyingStorageIsAllocated(num);
				}
				if (this.mapiStream != null)
				{
					return this.mapiStream.Seek(this.CurrentOperationContext, offset, origin);
				}
				this.position = (int)num;
				return num;
			}

			// Token: 0x06000088 RID: 136 RVA: 0x000046A2 File Offset: 0x000028A2
			public override void SetLength(long value)
			{
				this.EnsureUnderlyingStorageIsAllocated(value);
				if (this.mapiStream != null)
				{
					this.mapiStream.SetSize(this.CurrentOperationContext, value);
					return;
				}
				this.length = (int)value;
			}

			// Token: 0x06000089 RID: 137 RVA: 0x000046D0 File Offset: 0x000028D0
			public override void Write(byte[] buffer, int offset, int count)
			{
				if (this.mapiStream == null)
				{
					this.EnsureUnderlyingStorageIsAllocated((long)(this.position + count));
				}
				if (this.mapiStream != null)
				{
					this.mapiStream.Write(this.CurrentOperationContext, buffer, offset, count);
					return;
				}
				Buffer.BlockCopy(buffer, offset, this.buffer, this.position, count);
				this.position += count;
				if (this.position > this.length)
				{
					this.length = this.position;
				}
			}

			// Token: 0x0600008A RID: 138 RVA: 0x00004750 File Offset: 0x00002950
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					if (this.mapiStream != null)
					{
						this.mapiStream.Dispose();
						this.mapiStream = null;
						return;
					}
					if (this.buffer != null)
					{
						BufferPool pool = TempStream.Pool;
						if (this.buffer.Length == pool.BufferSize)
						{
							pool.Release(this.buffer);
						}
						this.ResetMemoryBufferState();
					}
				}
			}

			// Token: 0x0600008B RID: 139 RVA: 0x000047AB File Offset: 0x000029AB
			private void ResetMemoryBufferState()
			{
				this.buffer = null;
				this.position = 0;
				this.length = 0;
			}

			// Token: 0x0600008C RID: 140 RVA: 0x000047C4 File Offset: 0x000029C4
			private void EnsureUnderlyingStorageIsAllocated(long dataSize)
			{
				if (this.mapiStream != null)
				{
					return;
				}
				BufferPool pool = TempStream.Pool;
				if (dataSize > (long)pool.BufferSize && this.fastTransferPropBag.MapiPropBag.MapiObjectType != MapiObjectType.Person && this.propTag != PropTag.Folder.PackedNamedProps)
				{
					this.mapiStream = this.fastTransferPropBag.MapiPropBag.OpenStream(this.fastTransferPropBag.MapiPropBag.CurrentOperationContext, StreamFlags.AllowCreate | StreamFlags.AllowRead | StreamFlags.AllowWrite, this.propTag, this.fastTransferPropBag.MapiPropBag.Logon.Session.CodePage);
					if (this.buffer != null)
					{
						this.mapiStream.Write(this.CurrentOperationContext, this.buffer, 0, this.length);
						this.mapiStream.Seek(this.CurrentOperationContext, (long)this.position, SeekOrigin.Begin);
					}
					this.buffer = null;
					this.length = 0;
					this.position = 0;
					return;
				}
				if (this.buffer == null || dataSize > (long)this.buffer.Length)
				{
					byte[] dst;
					if (dataSize <= (long)pool.BufferSize)
					{
						dst = pool.Acquire();
					}
					else
					{
						dst = new byte[(int)dataSize];
					}
					if (this.buffer != null)
					{
						Buffer.BlockCopy(this.buffer, 0, dst, 0, this.length);
						if (this.buffer.Length == pool.BufferSize)
						{
							pool.Release(this.buffer);
						}
					}
					this.buffer = dst;
				}
			}

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x0600008D RID: 141 RVA: 0x00004925 File Offset: 0x00002B25
			private MapiContext CurrentOperationContext
			{
				get
				{
					return this.fastTransferPropBag.Context.CurrentOperationContext;
				}
			}

			// Token: 0x04000033 RID: 51
			private FastTransferPropertyBag fastTransferPropBag;

			// Token: 0x04000034 RID: 52
			private StorePropTag propTag;

			// Token: 0x04000035 RID: 53
			private bool streamFlushedToProperty;

			// Token: 0x04000036 RID: 54
			private MapiStream mapiStream;

			// Token: 0x04000037 RID: 55
			private byte[] buffer;

			// Token: 0x04000038 RID: 56
			private int position;

			// Token: 0x04000039 RID: 57
			private int length;

			// Token: 0x0400003A RID: 58
			private bool readOnly;
		}
	}
}
