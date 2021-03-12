using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200003A RID: 58
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class IMAPUnsupportedVersionException : IMAPException
	{
		// Token: 0x060001B8 RID: 440 RVA: 0x00005B8C File Offset: 0x00003D8C
		public IMAPUnsupportedVersionException() : base(Strings.IMAPUnsupportedVersionException)
		{
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00005B9E File Offset: 0x00003D9E
		public IMAPUnsupportedVersionException(Exception innerException) : base(Strings.IMAPUnsupportedVersionException, innerException)
		{
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00005BB1 File Offset: 0x00003DB1
		protected IMAPUnsupportedVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00005BBB File Offset: 0x00003DBB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
