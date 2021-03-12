using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200021A RID: 538
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DataConversionException : DataValidationException
	{
		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x00039B0C File Offset: 0x00037D0C
		public new PropertyConversionError Error
		{
			get
			{
				return (PropertyConversionError)base.Error;
			}
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x00039B19 File Offset: 0x00037D19
		public DataConversionException(PropertyConversionError error) : base(error, error.Exception)
		{
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x00039B28 File Offset: 0x00037D28
		protected DataConversionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x00039B32 File Offset: 0x00037D32
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
