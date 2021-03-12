using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x0200014A RID: 330
	internal class AirSyncEmailBodyProperty : AirSyncBodyProperty, IBodyProperty, IMIMERelatedProperty, IProperty
	{
		// Token: 0x06000FD4 RID: 4052 RVA: 0x00059E7B File Offset: 0x0005807B
		public AirSyncEmailBodyProperty(string xmlNodeNamespace) : base(xmlNodeNamespace, "Body", "BodyTruncated", "BodySize", true, false, false)
		{
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x00059E98 File Offset: 0x00058098
		protected override void InternalCopyFrom(IProperty srcProperty)
		{
			IBodyContentProperty bodyContentProperty = srcProperty as IBodyContentProperty;
			if (bodyContentProperty == null)
			{
				throw new UnexpectedTypeException("IBodyContentProperty", srcProperty);
			}
			try
			{
				bodyContentProperty.PreProcessProperty();
				base.InternalCopyFrom(srcProperty);
			}
			finally
			{
				bodyContentProperty.PostProcessProperty();
			}
		}
	}
}
