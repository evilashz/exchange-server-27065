using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser
{
	// Token: 0x02000175 RID: 373
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class FastTransferStateMachineFactory
	{
		// Token: 0x0600073F RID: 1855 RVA: 0x00019C48 File Offset: 0x00017E48
		internal IEnumerator<FastTransferStateMachine?> Serialize(FastTransferDownloadContext context, IPropertyBag propertyBag, AnnotatedPropertyValue annotatedPropertyValue)
		{
			object instance = this.serializeCacher.GetInstance();
			return this.serializeCacher.GetInitializer()(instance, context, propertyBag, annotatedPropertyValue);
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00019C7C File Offset: 0x00017E7C
		internal IEnumerator<FastTransferStateMachine?> SerializeFixedSize(FastTransferDownloadContext context, PropertyValue propertyValue)
		{
			object instance = this.serializeFixedSizeCacher.GetInstance();
			return this.serializeFixedSizeCacher.GetInitializer()(instance, context, propertyValue);
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00019CB0 File Offset: 0x00017EB0
		internal IEnumerator<FastTransferStateMachine?> SerializeVariableSize(FastTransferDownloadContext context, PropertyTag propertyTag, Stream propertyReadStream)
		{
			object instance = this.serializeVariableSizeCacher.GetInstance();
			return this.serializeVariableSizeCacher.GetInitializer()(instance, context, propertyTag, propertyReadStream);
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00019CE4 File Offset: 0x00017EE4
		internal IEnumerator<FastTransferStateMachine?> Deserialize(FastTransferUploadContext context, IPropertyBag propertyBag)
		{
			object instance = this.deserializeCacher.GetInstance();
			return this.deserializeCacher.GetInitializer()(instance, context, propertyBag);
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00019D18 File Offset: 0x00017F18
		internal IEnumerator<FastTransferStateMachine?> DeserializeVariableSizeProperty(FastTransferUploadContext context, IPropertyBag propertyBag, PropertyTag propertyTag, int codePage)
		{
			object instance = this.deserializeVariableSizePropertyCacher.GetInstance();
			return this.deserializeVariableSizePropertyCacher.GetInitializer()(instance, context, propertyBag, propertyTag, codePage);
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00019D50 File Offset: 0x00017F50
		internal IEnumerator<FastTransferStateMachine?> SkipPropertyIfExists(FastTransferUploadContext context, PropertyTag propertyTagToSkip)
		{
			object instance = this.skipPropertyIfExistsCacher.GetInstance();
			return this.skipPropertyIfExistsCacher.GetInitializer()(instance, context, propertyTagToSkip);
		}

		// Token: 0x04000399 RID: 921
		private readonly IteratorCacher<FastTransferStateMachineFactory.SerializeInitializerDelegate> serializeCacher = new IteratorCacher<FastTransferStateMachineFactory.SerializeInitializerDelegate>(new FastTransferStateMachineFactory.SerializeInitializerDelegate(FastTransferPropertyValue.DownloadImpl.Serialize_CreateDisplayClass), (FastTransferStateMachineFactory.SerializeInitializerDelegate del) => del(null, null, null, default(AnnotatedPropertyValue)));

		// Token: 0x0400039A RID: 922
		private readonly IteratorCacher<FastTransferStateMachineFactory.SerializeFixedSizePropertyInitializerDelegate> serializeFixedSizeCacher = new IteratorCacher<FastTransferStateMachineFactory.SerializeFixedSizePropertyInitializerDelegate>(new FastTransferStateMachineFactory.SerializeFixedSizePropertyInitializerDelegate(FastTransferPropertyValue.DownloadImpl.SerializeFixedSizeProperty_CreateDisplayClass), (FastTransferStateMachineFactory.SerializeFixedSizePropertyInitializerDelegate del) => del(null, null, default(PropertyValue)));

		// Token: 0x0400039B RID: 923
		private readonly IteratorCacher<FastTransferStateMachineFactory.SerializeVariableSizePropertyInitializerDelegate> serializeVariableSizeCacher = new IteratorCacher<FastTransferStateMachineFactory.SerializeVariableSizePropertyInitializerDelegate>(new FastTransferStateMachineFactory.SerializeVariableSizePropertyInitializerDelegate(FastTransferPropertyValue.DownloadImpl.SerializeVariableSizeProperty_CreateDisplayClass), (FastTransferStateMachineFactory.SerializeVariableSizePropertyInitializerDelegate del) => del(null, null, default(PropertyTag), null));

		// Token: 0x0400039C RID: 924
		private readonly IteratorCacher<FastTransferStateMachineFactory.DeserializeInitializerDelegate> deserializeCacher = new IteratorCacher<FastTransferStateMachineFactory.DeserializeInitializerDelegate>(new FastTransferStateMachineFactory.DeserializeInitializerDelegate(FastTransferPropertyValue.UploadImpl.Deserialize_CreateDisplayClass), (FastTransferStateMachineFactory.DeserializeInitializerDelegate del) => del(null, null, null));

		// Token: 0x0400039D RID: 925
		private readonly IteratorCacher<FastTransferStateMachineFactory.DeserializeVariableSizePropertyInitializerDelegate> deserializeVariableSizePropertyCacher = new IteratorCacher<FastTransferStateMachineFactory.DeserializeVariableSizePropertyInitializerDelegate>(new FastTransferStateMachineFactory.DeserializeVariableSizePropertyInitializerDelegate(FastTransferPropertyValue.UploadImpl.DeserializeVariableSizeProperty_CreateDisplayClass), (FastTransferStateMachineFactory.DeserializeVariableSizePropertyInitializerDelegate del) => del(null, null, null, default(PropertyTag), 0));

		// Token: 0x0400039E RID: 926
		private readonly IteratorCacher<FastTransferStateMachineFactory.SkipPropertyIfExistsInitializerDelegate> skipPropertyIfExistsCacher = new IteratorCacher<FastTransferStateMachineFactory.SkipPropertyIfExistsInitializerDelegate>(new FastTransferStateMachineFactory.SkipPropertyIfExistsInitializerDelegate(FastTransferPropertyValue.UploadImpl.SkipPropertyIfExists_CreateDisplayClass), (FastTransferStateMachineFactory.SkipPropertyIfExistsInitializerDelegate del) => del(null, null, default(PropertyTag)));

		// Token: 0x02000176 RID: 374
		// (Invoke) Token: 0x0600074D RID: 1869
		private delegate IEnumerator<FastTransferStateMachine?> SerializeInitializerDelegate(object instance, FastTransferDownloadContext context, IPropertyBag propertyBag, AnnotatedPropertyValue annotatedPropertyValue);

		// Token: 0x02000177 RID: 375
		// (Invoke) Token: 0x06000751 RID: 1873
		private delegate IEnumerator<FastTransferStateMachine?> SerializeFixedSizePropertyInitializerDelegate(object instance, FastTransferDownloadContext context, PropertyValue propertyValue);

		// Token: 0x02000178 RID: 376
		// (Invoke) Token: 0x06000755 RID: 1877
		private delegate IEnumerator<FastTransferStateMachine?> SerializeVariableSizePropertyInitializerDelegate(object instance, FastTransferDownloadContext context, PropertyTag propertyTag, Stream propertyReadStream);

		// Token: 0x02000179 RID: 377
		// (Invoke) Token: 0x06000759 RID: 1881
		private delegate IEnumerator<FastTransferStateMachine?> DeserializeInitializerDelegate(object instance, FastTransferUploadContext context, IPropertyBag propertyBag);

		// Token: 0x0200017A RID: 378
		// (Invoke) Token: 0x0600075D RID: 1885
		private delegate IEnumerator<FastTransferStateMachine?> DeserializeVariableSizePropertyInitializerDelegate(object instance, FastTransferUploadContext context, IPropertyBag propertyBag, PropertyTag propertyTag, int codePage);

		// Token: 0x0200017B RID: 379
		// (Invoke) Token: 0x06000761 RID: 1889
		private delegate IEnumerator<FastTransferStateMachine?> SkipPropertyIfExistsInitializerDelegate(object instance, FastTransferUploadContext context, PropertyTag propertyTagToSkip);
	}
}
