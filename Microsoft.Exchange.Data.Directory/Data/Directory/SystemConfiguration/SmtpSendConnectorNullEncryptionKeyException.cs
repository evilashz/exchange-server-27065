using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000AC9 RID: 2761
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SmtpSendConnectorNullEncryptionKeyException : LocalizedException
	{
		// Token: 0x060080B0 RID: 32944 RVA: 0x001A5B51 File Offset: 0x001A3D51
		public SmtpSendConnectorNullEncryptionKeyException() : base(DirectoryStrings.NullPasswordEncryptionKey)
		{
		}

		// Token: 0x060080B1 RID: 32945 RVA: 0x001A5B5E File Offset: 0x001A3D5E
		public SmtpSendConnectorNullEncryptionKeyException(Exception innerException) : base(DirectoryStrings.NullPasswordEncryptionKey, innerException)
		{
		}

		// Token: 0x060080B2 RID: 32946 RVA: 0x001A5B6C File Offset: 0x001A3D6C
		protected SmtpSendConnectorNullEncryptionKeyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060080B3 RID: 32947 RVA: 0x001A5B76 File Offset: 0x001A3D76
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
