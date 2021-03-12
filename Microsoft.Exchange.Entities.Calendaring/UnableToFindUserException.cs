using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Entities.Calendaring
{
	// Token: 0x02000011 RID: 17
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UnableToFindUserException : StoragePermanentException
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00002D4E File Offset: 0x00000F4E
		public UnableToFindUserException(ADOperationErrorCode operationErrorCode) : base(CalendaringStrings.UnableToFindUser(operationErrorCode))
		{
			this.operationErrorCode = operationErrorCode;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002D63 File Offset: 0x00000F63
		public UnableToFindUserException(ADOperationErrorCode operationErrorCode, Exception innerException) : base(CalendaringStrings.UnableToFindUser(operationErrorCode), innerException)
		{
			this.operationErrorCode = operationErrorCode;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002D79 File Offset: 0x00000F79
		protected UnableToFindUserException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.operationErrorCode = (ADOperationErrorCode)info.GetValue("operationErrorCode", typeof(ADOperationErrorCode));
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002DA3 File Offset: 0x00000FA3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("operationErrorCode", this.operationErrorCode);
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002DC3 File Offset: 0x00000FC3
		public ADOperationErrorCode OperationErrorCode
		{
			get
			{
				return this.operationErrorCode;
			}
		}

		// Token: 0x04000037 RID: 55
		private readonly ADOperationErrorCode operationErrorCode;
	}
}
