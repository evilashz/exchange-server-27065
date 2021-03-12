using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory.ResourceHealth
{
	// Token: 0x02000A00 RID: 2560
	[Serializable]
	internal class ResourceUnhealthyException : TransientException
	{
		// Token: 0x0600768E RID: 30350 RVA: 0x001868AF File Offset: 0x00184AAF
		public ResourceUnhealthyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if (info != null)
			{
				this.resourceKey = (ResourceKey)info.GetValue("resource", typeof(ResourceKey));
			}
		}

		// Token: 0x0600768F RID: 30351 RVA: 0x001868DC File Offset: 0x00184ADC
		public ResourceUnhealthyException(ResourceKey resourceKey) : base(DirectoryStrings.ExceptionResourceUnhealthy(resourceKey))
		{
			this.resourceKey = resourceKey;
		}

		// Token: 0x06007690 RID: 30352 RVA: 0x001868F1 File Offset: 0x00184AF1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			if (info != null)
			{
				info.AddValue("resource", this.resourceKey);
			}
		}

		// Token: 0x17002A6E RID: 10862
		// (get) Token: 0x06007691 RID: 30353 RVA: 0x0018690F File Offset: 0x00184B0F
		public ResourceKey ResourceKey
		{
			get
			{
				return this.resourceKey;
			}
		}

		// Token: 0x04004BE3 RID: 19427
		private const string ResourceKeyField = "resource";

		// Token: 0x04004BE4 RID: 19428
		private ResourceKey resourceKey;
	}
}
