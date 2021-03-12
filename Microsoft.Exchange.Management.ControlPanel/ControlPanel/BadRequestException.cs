using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000012 RID: 18
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class BadRequestException : LocalizedException
	{
		// Token: 0x06001869 RID: 6249 RVA: 0x0004B758 File Offset: 0x00049958
		public BadRequestException() : base(Strings.BadRequestMessage)
		{
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x0004B765 File Offset: 0x00049965
		public BadRequestException(Exception innerException) : base(Strings.BadRequestMessage, innerException)
		{
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x0004B773 File Offset: 0x00049973
		protected BadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x0004B77D File Offset: 0x0004997D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
