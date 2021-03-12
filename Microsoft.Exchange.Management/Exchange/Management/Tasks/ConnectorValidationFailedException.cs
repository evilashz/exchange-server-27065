using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001025 RID: 4133
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConnectorValidationFailedException : LocalizedException
	{
		// Token: 0x0600AF74 RID: 44916 RVA: 0x002946A5 File Offset: 0x002928A5
		public ConnectorValidationFailedException() : base(Strings.ConnectorValidationFailedId)
		{
		}

		// Token: 0x0600AF75 RID: 44917 RVA: 0x002946B2 File Offset: 0x002928B2
		public ConnectorValidationFailedException(Exception innerException) : base(Strings.ConnectorValidationFailedId, innerException)
		{
		}

		// Token: 0x0600AF76 RID: 44918 RVA: 0x002946C0 File Offset: 0x002928C0
		protected ConnectorValidationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AF77 RID: 44919 RVA: 0x002946CA File Offset: 0x002928CA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
