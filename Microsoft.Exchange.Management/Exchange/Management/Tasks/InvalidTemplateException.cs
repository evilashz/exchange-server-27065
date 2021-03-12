using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010E6 RID: 4326
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidTemplateException : LocalizedException
	{
		// Token: 0x0600B36D RID: 45933 RVA: 0x0029B3A5 File Offset: 0x002995A5
		public InvalidTemplateException() : base(Strings.InvalidTemplate)
		{
		}

		// Token: 0x0600B36E RID: 45934 RVA: 0x0029B3B2 File Offset: 0x002995B2
		public InvalidTemplateException(Exception innerException) : base(Strings.InvalidTemplate, innerException)
		{
		}

		// Token: 0x0600B36F RID: 45935 RVA: 0x0029B3C0 File Offset: 0x002995C0
		protected InvalidTemplateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B370 RID: 45936 RVA: 0x0029B3CA File Offset: 0x002995CA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
