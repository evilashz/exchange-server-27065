using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser
{
	// Token: 0x0200016E RID: 366
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class FastTransferPropList : FastTransferObject, IFastTransferProcessor<FastTransferDownloadContext>, IFastTransferProcessor<FastTransferUploadContext>, IDisposable
	{
		// Token: 0x06000701 RID: 1793 RVA: 0x00018AF7 File Offset: 0x00016CF7
		internal FastTransferPropList(IPropertyBag propertyBag) : this(propertyBag, IncludeAllPropertyFilter.Instance, null)
		{
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00018B06 File Offset: 0x00016D06
		internal FastTransferPropList(IPropertyBag propertyBag, IEnumerable<PropertyTag> requestedProperties) : this(propertyBag, IncludeAllPropertyFilter.Instance, requestedProperties)
		{
			Util.ThrowOnNullArgument(requestedProperties, "requestedProperties");
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00018B20 File Offset: 0x00016D20
		internal FastTransferPropList(IPropertyBag propertyBag, IPropertyFilter propertyFilter) : this(propertyBag, propertyFilter, null)
		{
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00018B2B File Offset: 0x00016D2B
		internal FastTransferPropList(IPropertyBag propertyBag, IPropertyFilter propertyFilter, IEnumerable<PropertyTag> requestedProperties) : base(false)
		{
			Util.ThrowOnNullArgument(propertyBag, "propertyBag");
			Util.ThrowOnNullArgument(propertyFilter, "propertyFilter");
			this.propertyBag = propertyBag;
			this.requestedProperties = requestedProperties;
			this.propertyFilter = propertyFilter;
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x00018B5F File Offset: 0x00016D5F
		// (set) Token: 0x06000706 RID: 1798 RVA: 0x00018B67 File Offset: 0x00016D67
		public bool ThrowOnPropertyError { get; set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x00018B70 File Offset: 0x00016D70
		// (set) Token: 0x06000708 RID: 1800 RVA: 0x00018B78 File Offset: 0x00016D78
		public bool SkipPropertyError { get; set; }

		// Token: 0x06000709 RID: 1801 RVA: 0x00018D40 File Offset: 0x00016F40
		IEnumerator<FastTransferStateMachine?> IFastTransferProcessor<FastTransferDownloadContext>.Process(FastTransferDownloadContext context)
		{
			foreach (AnnotatedPropertyValue annotatedPropertyValue in this.GetAnnotatedPropertiesToDownload())
			{
				if (annotatedPropertyValue.PropertyValue.IsError)
				{
					if (this.ThrowOnPropertyError)
					{
						string format = "Required property {0} should have a value";
						AnnotatedPropertyValue annotatedPropertyValue2 = annotatedPropertyValue;
						throw new RopExecutionException(string.Format(format, annotatedPropertyValue2.PropertyTag), (ErrorCode)2147942487U);
					}
					if (this.SkipPropertyError)
					{
						continue;
					}
				}
				yield return new FastTransferStateMachine?(FastTransferPropertyValue.Serialize(context, this.propertyBag, annotatedPropertyValue));
			}
			yield break;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00018D88 File Offset: 0x00016F88
		private IEnumerable<AnnotatedPropertyValue> GetAnnotatedPropertiesToDownload()
		{
			IEnumerable<AnnotatedPropertyValue> source;
			if (this.requestedProperties == null)
			{
				source = this.propertyBag.GetAnnotatedProperties();
			}
			else
			{
				source = from propertyTag in this.requestedProperties
				select this.propertyBag.GetAnnotatedProperty(propertyTag);
			}
			return from annotatedPropertyValue in source
			where this.propertyFilter.IncludeProperty(annotatedPropertyValue.PropertyTag)
			select annotatedPropertyValue;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00018F18 File Offset: 0x00017118
		IEnumerator<FastTransferStateMachine?> IFastTransferProcessor<FastTransferUploadContext>.Process(FastTransferUploadContext context)
		{
			PropertyTag propertyTag;
			while (!context.NoMoreData && !context.DataInterface.TryPeekMarker(out propertyTag) && !FastTransferPropList.MetaProperties.Contains(propertyTag))
			{
				if (this.requestedProperties != null && !this.requestedProperties.Contains(propertyTag, PropertyTag.PropertyIdComparer))
				{
					IPropertyBag throwPropertyBag = new SingleMemberPropertyBag(propertyTag);
					yield return new FastTransferStateMachine?(FastTransferPropertyValue.DeserializeInto(context, throwPropertyBag));
				}
				else
				{
					yield return new FastTransferStateMachine?(FastTransferPropertyValue.DeserializeInto(context, this.propertyBag));
				}
			}
			yield break;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00018F3B File Offset: 0x0001713B
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<FastTransferPropList>(this);
		}

		// Token: 0x04000389 RID: 905
		private readonly IPropertyBag propertyBag;

		// Token: 0x0400038A RID: 906
		private readonly IEnumerable<PropertyTag> requestedProperties;

		// Token: 0x0400038B RID: 907
		private readonly IPropertyFilter propertyFilter;

		// Token: 0x0400038C RID: 908
		internal static readonly ICollection<PropertyTag> MetaProperties = new ReadOnlyCollection<PropertyTag>(new PropertyTag[]
		{
			PropertyTag.FXDelProp,
			PropertyTag.EcWarning,
			PropertyTag.NewFXFolder,
			PropertyTag.IncrSyncGroupId,
			PropertyTag.IncrSyncMsgPartial
		});
	}
}
