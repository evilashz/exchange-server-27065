using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001DD RID: 477
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UMInvalidPartnerMessageException : LocalizedException
	{
		// Token: 0x06000F72 RID: 3954 RVA: 0x00036756 File Offset: 0x00034956
		public UMInvalidPartnerMessageException(string fieldName) : base(Strings.UMInvalidPartnerMessageException(fieldName))
		{
			this.fieldName = fieldName;
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x0003676B File Offset: 0x0003496B
		public UMInvalidPartnerMessageException(string fieldName, Exception innerException) : base(Strings.UMInvalidPartnerMessageException(fieldName), innerException)
		{
			this.fieldName = fieldName;
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x00036781 File Offset: 0x00034981
		protected UMInvalidPartnerMessageException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fieldName = (string)info.GetValue("fieldName", typeof(string));
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x000367AB File Offset: 0x000349AB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fieldName", this.fieldName);
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000F76 RID: 3958 RVA: 0x000367C6 File Offset: 0x000349C6
		public string FieldName
		{
			get
			{
				return this.fieldName;
			}
		}

		// Token: 0x040007AE RID: 1966
		private readonly string fieldName;
	}
}
