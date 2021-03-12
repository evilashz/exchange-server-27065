using System;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000008 RID: 8
	internal static class PropertyTagExtensions
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002737 File Offset: 0x00000937
		public static bool IsBody(this PropertyId propertyId)
		{
			return propertyId == PropertyId.Body;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002741 File Offset: 0x00000941
		public static bool IsRtfCompressed(this PropertyId propertyId)
		{
			return propertyId == PropertyId.RtfCompressed;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000274B File Offset: 0x0000094B
		public static bool IsHtml(this PropertyId propertyId)
		{
			return propertyId == PropertyId.Html;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002755 File Offset: 0x00000955
		public static bool IsRtfInSync(this PropertyId propertyId)
		{
			return propertyId == PropertyId.RtfInSync;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000275F File Offset: 0x0000095F
		public static bool IsBody(this PropertyTag propertyTag)
		{
			return propertyTag.PropertyId.IsBody() && propertyTag.IsStringProperty;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002778 File Offset: 0x00000978
		public static bool IsRtfCompressed(this PropertyTag propertyTag)
		{
			return propertyTag == PropertyTag.RtfCompressed;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002785 File Offset: 0x00000985
		public static bool IsHtml(this PropertyTag propertyTag)
		{
			return propertyTag == PropertyTag.Html;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002792 File Offset: 0x00000992
		public static bool IsBodyProperty(this PropertyTag propertyTag)
		{
			return propertyTag.PropertyId.IsBody() || propertyTag.PropertyId.IsRtfCompressed() || propertyTag.PropertyId.IsHtml();
		}
	}
}
