using System;

namespace Microsoft.Exchange.Data.MailboxSignature
{
	// Token: 0x02000237 RID: 567
	internal static class TenantHintHelper
	{
		// Token: 0x06001390 RID: 5008 RVA: 0x0003BE24 File Offset: 0x0003A024
		public static byte[] ParseTenantHint(MailboxSignatureSectionMetadata metadata, byte[] buffer, ref int offset)
		{
			byte[] array = new byte[metadata.Length];
			Buffer.BlockCopy(buffer, offset, array, 0, metadata.Length);
			offset += metadata.Length;
			return array;
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x0003BE59 File Offset: 0x0003A059
		public static int SerializeTenantHint(byte[] tenantHintBlob, byte[] buffer, int offset)
		{
			if (buffer != null && tenantHintBlob != null)
			{
				Buffer.BlockCopy(tenantHintBlob, 0, buffer, offset, tenantHintBlob.Length);
			}
			if (tenantHintBlob == null)
			{
				return 0;
			}
			return tenantHintBlob.Length;
		}

		// Token: 0x04000B76 RID: 2934
		public const short RequiredTenantHintVersion = 1;
	}
}
