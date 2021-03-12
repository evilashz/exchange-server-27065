using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.ApplicationLogic.Extension;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x02000006 RID: 6
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CannotDisableMandatoryExtensionException : OwaExtensionOperationException
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00003441 File Offset: 0x00001641
		public CannotDisableMandatoryExtensionException() : base(Strings.ErrorCannotDisableMandatoryExtension)
		{
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000344E File Offset: 0x0000164E
		public CannotDisableMandatoryExtensionException(Exception innerException) : base(Strings.ErrorCannotDisableMandatoryExtension, innerException)
		{
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000345C File Offset: 0x0000165C
		protected CannotDisableMandatoryExtensionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003466 File Offset: 0x00001666
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
